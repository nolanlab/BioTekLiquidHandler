Public Class BioTekVWorksPluginDiags
    Implements IWorksDriver.IWorksDiags

    ' What does this stuff do!?
    Dim frmDiags As Diags = New Diags()

    Public Function CloseDiagsDialog() As IWorksDriver.ReturnCode Implements IWorksDriver.IWorksDiags.CloseDiagsDialog
        frmDiags.Hide()
        Return IWorksDriver.ReturnCode.RETURN_SUCCESS
    End Function

    Public Function IsDiagsDialogOpen() As IWorksDriver.ReturnCode Implements IWorksDriver.IWorksDiags.IsDiagsDialogOpen
        Return frmDiags.Visible
    End Function

    Public Sub ShowDiagsDialog(ByVal iSecurity As IWorksDriver.SecurityLevel, ByVal bModal As Boolean) Implements IWorksDriver.IWorksDiags.ShowDiagsDialog
        frmDiags.Show()
    End Sub
End Class
