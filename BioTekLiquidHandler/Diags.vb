Public Class Diags

    Dim activeProfile As Profile

    Private Sub Diags_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Load options into the instrument type combo.
        'These aren't super pretty, but they're perfectly bound to the enum.
        Me.cmboInstrumentType.DataSource = System.Enum.GetValues(GetType(BTILHCRunner.ClassLHCRunner.enumProductType))

        'Load options into the comm port combo.
        For Each sp As String In My.Computer.Ports.SerialPortNames
            cmboPort.Items.Add(sp)
        Next

        'Load profiles into the profiles combo
        Dim profiles As String() = Profile.GetProfiles()
        For Each pr As String In profiles
            cmboProfile.Items.Add(pr)
        Next

        'Select a particular profile
        If cmboProfile.Items.Count > 0 Then
            cmboProfile.SelectedIndex = 0
            'Firing of cmboProfile.selectedIndexChange should take over from here
        End If

    End Sub

    Private Sub cmboInstrumentType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmboInstrumentType.SelectedIndexChanged
        If activeProfile IsNot Nothing Then
            activeProfile.instrumentType = CType(cmboInstrumentType.SelectedValue, BTILHCRunner.ClassLHCRunner.enumProductType)
            btnUpdateProfile.Enabled = True
        End If
    End Sub

    Private Sub cmboPort_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmboPort.SelectedIndexChanged
        If activeProfile IsNot Nothing Then
            activeProfile.commPort = cmboPort.SelectedItem
            btnUpdateProfile.Enabled = True
        End If
    End Sub

    Private Sub btnNewProfile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewProfile.Click
        Dim newProfileName = InputBox("Please enter name for new profile", "New Profile")
        If newProfileName <> "" Then
            activeProfile = New Profile
            activeProfile.name = newProfileName
            activeProfile.ToRegistry()
            cmboProfile.Items.Add(activeProfile)
            cmboProfile.SelectedIndex = cmboProfile.Items.IndexOf(newProfileName)
        End If
    End Sub

    Private Sub btnDeleteProfile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteProfile.Click
        If MsgBox("Are you sure?", vbYesNo) = vbYes Then
            Profile.DeleteProfile(activeProfile.name)
        End If
    End Sub

    Private Sub btnUpdateProfile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateProfile.Click
        activeProfile.ToRegistry()
        btnUpdateProfile.Enabled = False
    End Sub

    Private Sub btnInitializeProfile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInitializeProfile.Click

    End Sub

    Private Sub cmboProfile_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmboProfile.SelectedIndexChanged
        activeProfile = Profile.FromRegistry(cmboProfile.SelectedItem)
        If activeProfile IsNot Nothing Then
            cmboPort.SelectedIndex = cmboPort.Items.IndexOf(activeProfile.commPort)
            cmboInstrumentType.SelectedIndex = cmboInstrumentType.Items.IndexOf(activeProfile.instrumentType)
            cmboPort.Enabled = True
            cmboInstrumentType.Enabled = True
            btnUpdateProfile.Enabled = True
            btnDeleteProfile.Enabled = True
        Else
            cmboPort.Enabled = False
            cmboInstrumentType.Enabled = False
            btnUpdateProfile.Enabled = False
            btnDeleteProfile.Enabled = False
        End If
    End Sub

End Class