Imports BTILHCRunner
Imports System.Windows.Forms
Imports System.IO

Public Class BioTekVWorksPluginDriver
    Inherits IWorksDriver.CControllerClientClass
    Implements IWorksDriver.IWorksDriver
    Implements IWorksDriver.IWorksDiags

    Private lhcRunner As BTILHCRunner.ClassLHCRunner = New BTILHCRunner.ClassLHCRunner
    Private controllerInstance As IWorksDriver.CWorksController

    Private errorString As String = ""

    Friend activeProfile As Profile

    Private WithEvents frmDiags As Diags = New Diags(Me)

    ' This should be defined by BTILHCRunner but does not appear to be.
    Enum Runner_ReturnCode
        eError = 0
        eOK = 1
        eRegistration_Failure = 2
        eInterface_Failure = 3
        eInvalid_Product_Type = 4
        eOpen_File_Error = 5
        ePre_Run_Error = 6
    End Enum

    ' This should be defined by BTILHCRunner but does not appear to be.
    Enum RunStatus
        eUninitialized = 0
        eReady = 1
        eNotReady = 2
        eBusy = 3
        eError = 4
        eDone = 5
        eIncomplete = 6
        ePaused = 7
        eStopRequested = 8
        eStopping = 9
        eNotRequired = 10
    End Enum

    Public Sub Abort(ByVal ErrorContext As String) Implements IWorksDriver.IWorksDriver.Abort
        If activeProfile IsNot Nothing And activeProfile.initialized Then
            lhcRunner.LHC_PauseProtocol()
            lhcRunner.LHC_AbortProtocol()
        End If
    End Sub

    Public Sub Close() Implements IWorksDriver.IWorksDriver.Close
        'Nothing to do
    End Sub

    Public Function Command(ByVal CommandXML As String) As IWorksDriver.ReturnCode Implements IWorksDriver.IWorksDriver.Command
        '<?xml version='1.0' encoding='ASCII' ?>
        '<Velocity11 file='MetaData' md5sum='f4b2ddb342a1f5600f94f3ab51bd791c' version='1.0' >
        '	<Command Compiler='17' Description='Runs a program' Editor='16' Name='Run LHC Program' NextTaskToExecute='1' RequiresRefresh='0' TaskRequiresLocation='1' VisibleAvailability='1' >
        '		<Parameters >
        '			<Parameter Description='Name of LHC program' Name='Program name' Scriptable='1' Style='0' Type='9' Value='bdas' />
        '		</Parameters>
        '	</Command>
        '</Velocity11>

        If activeProfile Is Nothing Or activeProfile.initialized = False Then
            errorString = "Device is not initialized."
            Return IWorksDriver.ReturnCode.RETURN_FAIL
        End If

        Dim programPath As String = ""

        Dim xcommand As XDocument = XDocument.Parse(CommandXML)
        For Each parameter As XElement In xcommand...<Parameter>
            If parameter.@Name = "Program name" Then
                programPath = parameter.@Value
            End If
        Next

        If programPath = "" Then
            errorString = "Program path was not specified."
            Return IWorksDriver.ReturnCode.RETURN_FAIL
        End If

        'Verify file exists
        If System.IO.File.Exists(programPath) <> True Then
            errorString = "Could not find program file: " & programPath
            Return IWorksDriver.ReturnCode.RETURN_FAIL
        End If

        lhcRunner.LHC_LoadProtocolFromFile(programPath)

        lhcRunner.LHC_RunProtocol()
        While True
            Select Case lhcRunner.LHC_GetProtocolStatus()
                Case RunStatus.eBusy, RunStatus.eIncomplete
                    Threading.Thread.Sleep(1000)
                Case RunStatus.eDone, RunStatus.eReady
                    Return IWorksDriver.ReturnCode.RETURN_SUCCESS
                Case RunStatus.eError, RunStatus.eUninitialized
                    errorString = lhcRunner.LHC_GetErrorString(lhcRunner.LHC_GetLastErrorCode())
                    If errorString = "" Then
                        errorString = "Unkown error occurred."
                    End If
                    Return IWorksDriver.ReturnCode.RETURN_FAIL
                Case Else
                    errorString = "Unhandled status: " + lhcRunner.LHC_GetProtocolStatus()
                    Return IWorksDriver.ReturnCode.RETURN_FAIL
            End Select
        End While

        Return IWorksDriver.ReturnCode.RETURN_SUCCESS
    End Function

    Public Function Compile(ByVal iCompileType As IWorksDriver.CompileType, ByVal MetaDataXML As String) As String Implements IWorksDriver.IWorksDriver.Compile
        'No errors or warnings to report, basically ever.
        Return ""
    End Function

    Public Function ControllerQuery(ByVal Query As String) As String Implements IWorksDriver.IWorksDriver.ControllerQuery
        'Not implemented
        Return ""
    End Function

    Public Function Get32x32Bitmap(ByVal CommandName As String) As stdole.IPictureDisp Implements IWorksDriver.IWorksDriver.Get32x32Bitmap
        Dim img As System.Drawing.Bitmap = My.Resources.LHC_app
        Return (Microsoft.VisualBasic.Compatibility.VB6.Support.ImageToIPictureDisp(img))
    End Function

    Public Function GetDescription(ByVal CommandXML As String, ByVal Verbose As Boolean) As String Implements IWorksDriver.IWorksDriver.GetDescription
        'Verbose indicates if the description goes in the log or under a task icon. Not used here.
        Dim programName As String = ""

        Dim xcommand As XDocument = XDocument.Parse(CommandXML)
        For Each parameter As XElement In xcommand...<Parameter>
            If parameter.@Name = "Program name" Then
                Dim programPath As String = parameter.@Value
                programName = Path.GetFileName(programPath)
            End If
        Next

        If programName = "" Then
            programName = "LHC protocol"
        End If

        Return "Run " & programName
    End Function

    Public Function GetErrorInfo() As String Implements IWorksDriver.IWorksDriver.GetErrorInfo
        Return errorString
    End Function

    Public Function GetLayoutBitmap(ByVal LayoutInfoXML As String) As stdole.IPictureDisp Implements IWorksDriver.IWorksDriver.GetLayoutBitmap
        'Not implemented or necessary
        Return Nothing
    End Function

    Public Function GetMetaData(ByVal iDataType As IWorksDriver.MetaDataType, ByVal current_metadata As String) As String Implements IWorksDriver.IWorksDriver.GetMetaData
        Dim profilesNames As String() = Profile.GetProfiles()

        Dim metadata As XDocument =
            <?xml version='1.0' encoding='ASCII'?>
            <Velocity11 file='MetaData' version='1.0'>
                <MetaData>
                    <Device Description='BioTek Liquid Handler' DynamicLocations='0' MiscAttributes='0' HasBarcodeReader='0' HardwareManufacturer='BioTek Instruments' Name='BioTek Liquid Handler' PreferredTab='Liquid Handling' RegistryName='BioTekLHC\Profiles'>
                        <Parameters>
                            <Parameter Name='Profile' Style='0' Type='2'>
                                <Ranges>
                                    <%= From pn In profilesNames
                                        Select <Range Value=<%= pn %>/>
                                    %>
                                </Ranges>
                            </Parameter>
                        </Parameters>
                        <Locations>
                            <Location Name='Stage' Type='1'/>
                        </Locations>
                    </Device>
                    <Versions>
                        <Version Author='zbjornson' Date='June 15, 2014' Name='BioTek Liquid Handler' Version='1.0.0'/>
                    </Versions>
                    <Commands>
                        <Command Compiler='17' Description='Run an LHC protocol' Editor='10' Name='Run LHC protocol'>
                            <Parameters>
                                <Parameter Name='Program name' Description='Name of LHC program' Style='0' Type='9'>
                                </Parameter>
                            </Parameters>
                        </Command>
                    </Commands>
                </MetaData>
            </Velocity11>

        Return metadata.Declaration.ToString() + metadata.ToString()
    End Function

    Public Function Ignore(ByVal ErrorContext As String) As IWorksDriver.ReturnCode Implements IWorksDriver.IWorksDriver.Ignore
        'Any way to ignore? Don't think so.
        Return IWorksDriver.ReturnCode.RETURN_SUCCESS
    End Function

    Public Function Initialize(ByVal CommandXML As String) As IWorksDriver.ReturnCode Implements IWorksDriver.IWorksDriver.Initialize
        Dim xcommand As XDocument = XDocument.Parse(CommandXML)
        For Each parameter As XElement In xcommand...<Parameter>
            If parameter.@Name = "Profile" Then
                Dim profileName = parameter.@Value
                activeProfile = Profile.FromRegistry(profileName)
            End If
        Next

        If activeProfile Is Nothing Then
            errorString = "Error loading profile."
            Return IWorksDriver.ReturnCode.RETURN_FAIL
        End If

        Dim returnValue As Runner_ReturnCode

        returnValue = lhcRunner.LHC_SetProductType(activeProfile.instrumentType)
        If (returnValue <> Runner_ReturnCode.eOK) Then
            errorString = lhcRunner.LHC_GetErrorString(returnValue)
            Return IWorksDriver.ReturnCode.RETURN_FAIL
        End If

        returnValue = lhcRunner.LHC_SetCommunications(activeProfile.commPort)
        If (returnValue <> Runner_ReturnCode.eOK) Then
            errorString = lhcRunner.LHC_GetErrorString(returnValue)
            Return IWorksDriver.ReturnCode.RETURN_FAIL
        End If

        lhcRunner.LHC_TestCommunications()
        If (returnValue <> Runner_ReturnCode.eOK) Then
            errorString = lhcRunner.LHC_GetErrorString(returnValue)
            Return IWorksDriver.ReturnCode.RETURN_FAIL
        End If

        activeProfile.initialized = True

        Return IWorksDriver.ReturnCode.RETURN_SUCCESS
    End Function

    Public Function IsLocationAvailable(ByVal LocationAvailableXML As String) As Boolean Implements IWorksDriver.IWorksDriver.IsLocationAvailable
        Return True
    End Function

    Public Function MakeLocationAvailable(ByVal LocationAvailableXML As String) As IWorksDriver.ReturnCode Implements IWorksDriver.IWorksDriver.MakeLocationAvailable
        Return IWorksDriver.ReturnCode.RETURN_SUCCESS
    End Function

    Public Function PlateDroppedOff(ByVal PlateInfoXML As String) As IWorksDriver.ReturnCode Implements IWorksDriver.IWorksDriver.PlateDroppedOff
        Return IWorksDriver.ReturnCode.RETURN_SUCCESS
    End Function

    Public Function PlatePickedUp(ByVal PlateInfoXML As String) As IWorksDriver.ReturnCode Implements IWorksDriver.IWorksDriver.PlatePickedUp
        Return IWorksDriver.ReturnCode.RETURN_SUCCESS
    End Function

    Public Sub PlateTransferAborted(ByVal PlateInfoXML As String) Implements IWorksDriver.IWorksDriver.PlateTransferAborted

    End Sub

    Public Function PrepareForRun(ByVal LocationInfoXML As String) As IWorksDriver.ReturnCode Implements IWorksDriver.IWorksDriver.PrepareForRun
        Return IWorksDriver.ReturnCode.RETURN_SUCCESS
    End Function

    Public Function Retry(ByVal ErrorContext As String) As IWorksDriver.ReturnCode Implements IWorksDriver.IWorksDriver.Retry
        'TODO
        Return IWorksDriver.ReturnCode.RETURN_SUCCESS
    End Function

    'Formsy stuff. Would be nicer in Diags.vb but not sure how to make VWorks find the pointer to the class implementing IWorksDriver.IWorksDiags.
    Private Sub DiagsForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles frmDiags.FormClosed
        controllerInstance.OnCloseDiagsDialog(Me)
    End Sub

    Public Function CloseDiagsDialog() As IWorksDriver.ReturnCode Implements IWorksDriver.IWorksDiags.CloseDiagsDialog
        frmDiags.Hide()
        Return IWorksDriver.ReturnCode.RETURN_SUCCESS
    End Function

    Public Function IsDiagsDialogOpen() As IWorksDriver.ReturnCode Implements IWorksDriver.IWorksDiags.IsDiagsDialogOpen
        Return frmDiags.Visible
    End Function

    Public Sub ShowDiagsDialog(ByVal iSecurity As IWorksDriver.SecurityLevel) Implements IWorksDriver.IWorksDriver.ShowDiagsDialog
        ' This is obsolete, says the docs, except it's still used.
        frmDiags.Show()
    End Sub

    Public Sub ShowDiagsDialog(ByVal iSecurity As IWorksDriver.SecurityLevel, ByVal bModal As Boolean) Implements IWorksDriver.IWorksDiags.ShowDiagsDialog
        frmDiags.Show()
    End Sub

    Public Overrides Sub SetController(ByVal Controller As IWorksDriver.CWorksController)
        controllerInstance = Controller
    End Sub
End Class
