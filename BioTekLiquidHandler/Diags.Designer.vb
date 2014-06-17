<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Diags
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Diags))
        Me.cmboProfile = New System.Windows.Forms.ComboBox()
        Me.cmboInstrumentType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmboPort = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnUpdateProfile = New System.Windows.Forms.Button()
        Me.btnDeleteProfile = New System.Windows.Forms.Button()
        Me.btnNewProfile = New System.Windows.Forms.Button()
        Me.btnInitializeProfile = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmboProfile
        '
        Me.cmboProfile.FormattingEnabled = True
        Me.cmboProfile.Location = New System.Drawing.Point(55, 12)
        Me.cmboProfile.Name = "cmboProfile"
        Me.cmboProfile.Size = New System.Drawing.Size(157, 21)
        Me.cmboProfile.TabIndex = 0
        '
        'cmboInstrumentType
        '
        Me.cmboInstrumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmboInstrumentType.FormattingEnabled = True
        Me.cmboInstrumentType.Location = New System.Drawing.Point(95, 19)
        Me.cmboInstrumentType.Name = "cmboInstrumentType"
        Me.cmboInstrumentType.Size = New System.Drawing.Size(121, 21)
        Me.cmboInstrumentType.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Instrument Type"
        '
        'cmboPort
        '
        Me.cmboPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmboPort.FormattingEnabled = True
        Me.cmboPort.Location = New System.Drawing.Point(95, 46)
        Me.cmboPort.Name = "cmboPort"
        Me.cmboPort.Size = New System.Drawing.Size(121, 21)
        Me.cmboPort.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(63, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Port"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Profile"
        '
        'btnUpdateProfile
        '
        Me.btnUpdateProfile.Location = New System.Drawing.Point(22, 68)
        Me.btnUpdateProfile.Name = "btnUpdateProfile"
        Me.btnUpdateProfile.Size = New System.Drawing.Size(92, 23)
        Me.btnUpdateProfile.TabIndex = 6
        Me.btnUpdateProfile.Text = "Update Profile"
        Me.btnUpdateProfile.UseVisualStyleBackColor = True
        '
        'btnDeleteProfile
        '
        Me.btnDeleteProfile.Location = New System.Drawing.Point(120, 39)
        Me.btnDeleteProfile.Name = "btnDeleteProfile"
        Me.btnDeleteProfile.Size = New System.Drawing.Size(92, 23)
        Me.btnDeleteProfile.TabIndex = 7
        Me.btnDeleteProfile.Text = "Delete Profile"
        Me.btnDeleteProfile.UseVisualStyleBackColor = True
        '
        'btnNewProfile
        '
        Me.btnNewProfile.Location = New System.Drawing.Point(22, 39)
        Me.btnNewProfile.Name = "btnNewProfile"
        Me.btnNewProfile.Size = New System.Drawing.Size(92, 23)
        Me.btnNewProfile.TabIndex = 8
        Me.btnNewProfile.Text = "New Profile"
        Me.btnNewProfile.UseVisualStyleBackColor = True
        '
        'btnInitializeProfile
        '
        Me.btnInitializeProfile.Location = New System.Drawing.Point(120, 68)
        Me.btnInitializeProfile.Name = "btnInitializeProfile"
        Me.btnInitializeProfile.Size = New System.Drawing.Size(92, 23)
        Me.btnInitializeProfile.TabIndex = 9
        Me.btnInitializeProfile.Text = "Initialize Profile"
        Me.btnInitializeProfile.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmboInstrumentType)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmboPort)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(218, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(226, 79)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Profile Settings"
        '
        'Diags
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(459, 104)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnInitializeProfile)
        Me.Controls.Add(Me.btnNewProfile)
        Me.Controls.Add(Me.btnDeleteProfile)
        Me.Controls.Add(Me.btnUpdateProfile)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmboProfile)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Diags"
        Me.Text = "Diagnostics - BioTek Liquid Handler Driver v1.0.0"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmboProfile As System.Windows.Forms.ComboBox
    Friend WithEvents cmboInstrumentType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmboPort As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnUpdateProfile As System.Windows.Forms.Button
    Friend WithEvents btnDeleteProfile As System.Windows.Forms.Button
    Friend WithEvents btnNewProfile As System.Windows.Forms.Button
    Friend WithEvents btnInitializeProfile As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
