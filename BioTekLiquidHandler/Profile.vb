Public Class Profile
    Public name As String = Nothing
    Public instrumentType As BTILHCRunner.ClassLHCRunner.enumProductType = BTILHCRunner.ClassLHCRunner.enumProductType.eUndefined
    Public commPort As String = ""

    Private Const REG_PATH As String = "SOFTWARE\Velocity11\BioTek Liquid Handler\Profiles\"

    Public Function ToRegistry() As Integer
        Try
            Dim hive As String = REG_PATH & name
            My.Computer.Registry.LocalMachine.CreateSubKey(hive)
            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\" & hive, "Instrument type", Convert.ToString(Convert.ToInt16(instrumentType)))
            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\" & hive, "Serial port", commPort)
            Return 0
        Catch ex As Exception
            Debug.Print(ex.ToString())
            Return 1
        End Try
    End Function

    Public Shared Function FromRegistry(ByVal name As String) As Profile
        If My.Computer.Registry.LocalMachine.GetValue(REG_PATH, name, Nothing) Is Nothing Then
            Return Nothing
        End If

        Dim newProfile As New Profile
        newProfile.name = name

        Dim instrumentTypeValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\" & REG_PATH & name, "Instrument type", Nothing)
        If instrumentTypeValue IsNot Nothing Then
            Try
                newProfile.instrumentType = [Enum].Parse(GetType(BTILHCRunner.ClassLHCRunner.enumProductType), instrumentTypeValue)
            Catch ex As Exception
                Debug.Print(ex.ToString)
                Debug.Print(instrumentTypeValue)
            End Try
        End If

        If My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\" & REG_PATH & name, "Serial port", Nothing) IsNot Nothing Then
            newProfile.commPort = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\" & REG_PATH & name, "Serial port", Nothing)
        End If

        Return newProfile

    End Function

    Public Shared Function DeleteProfile(ByVal name As String) As Integer
        Try
            My.Computer.Registry.LocalMachine.DeleteSubKey(REG_PATH & name)
            Return 0

        Catch ex As Exception
            Return 1

        End Try
    End Function

    Public Shared Function GetProfiles() As String()
        Dim sProfiles As String()

        sProfiles = My.Computer.Registry.LocalMachine.OpenSubKey(REG_PATH, False).GetSubKeyNames()

        Return sProfiles

    End Function

End Class
