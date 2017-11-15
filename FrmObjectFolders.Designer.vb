<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmObjectFolders
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmObjectFolders))
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmdAPI = New System.Windows.Forms.Button()
        Me.cmdRyw12 = New System.Windows.Forms.Button()
        Me.txtAPIFolder = New System.Windows.Forms.TextBox()
        Me.txtRwy12Folder = New System.Windows.Forms.TextBox()
        Me.cmdASD = New System.Windows.Forms.Button()
        Me.txtASDFolder = New System.Windows.Forms.TextBox()
        Me.cmdLibObjects = New System.Windows.Forms.Button()
        Me.txtLibObjectsFolder = New System.Windows.Forms.TextBox()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(12, 48)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(138, 13)
        Me.Label8.TabIndex = 85
        Me.Label8.Text = "Path to Rwy12 programme :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(12, 87)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(137, 13)
        Me.Label9.TabIndex = 86
        Me.Label9.Text = "Path to API macros (*.API) :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(12, 126)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(145, 13)
        Me.Label10.TabIndex = 89
        Me.Label10.Text = "Path to ASD macros (*.SCM):"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(12, 9)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(117, 13)
        Me.Label11.TabIndex = 92
        Me.Label11.Text = "Path to Library Objects:"
        '
        'cmdAPI
        '
        Me.cmdAPI.BackColor = System.Drawing.Color.Transparent
        Me.cmdAPI.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAPI.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAPI.Location = New System.Drawing.Point(357, 101)
        Me.cmdAPI.Name = "cmdAPI"
        Me.cmdAPI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAPI.Size = New System.Drawing.Size(31, 22)
        Me.cmdAPI.TabIndex = 81
        Me.cmdAPI.Text = "..."
        Me.cmdAPI.UseVisualStyleBackColor = False
        '
        'cmdRyw12
        '
        Me.cmdRyw12.BackColor = System.Drawing.Color.Transparent
        Me.cmdRyw12.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRyw12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRyw12.Location = New System.Drawing.Point(357, 62)
        Me.cmdRyw12.Name = "cmdRyw12"
        Me.cmdRyw12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRyw12.Size = New System.Drawing.Size(31, 22)
        Me.cmdRyw12.TabIndex = 82
        Me.cmdRyw12.Text = "..."
        Me.cmdRyw12.UseVisualStyleBackColor = False
        '
        'txtAPIFolder
        '
        Me.txtAPIFolder.AcceptsReturn = True
        Me.txtAPIFolder.BackColor = System.Drawing.SystemColors.Window
        Me.txtAPIFolder.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAPIFolder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAPIFolder.Location = New System.Drawing.Point(15, 103)
        Me.txtAPIFolder.MaxLength = 0
        Me.txtAPIFolder.Name = "txtAPIFolder"
        Me.txtAPIFolder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAPIFolder.Size = New System.Drawing.Size(326, 20)
        Me.txtAPIFolder.TabIndex = 83
        '
        'txtRwy12Folder
        '
        Me.txtRwy12Folder.AcceptsReturn = True
        Me.txtRwy12Folder.BackColor = System.Drawing.SystemColors.Window
        Me.txtRwy12Folder.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRwy12Folder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRwy12Folder.Location = New System.Drawing.Point(15, 64)
        Me.txtRwy12Folder.MaxLength = 0
        Me.txtRwy12Folder.Name = "txtRwy12Folder"
        Me.txtRwy12Folder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRwy12Folder.Size = New System.Drawing.Size(326, 20)
        Me.txtRwy12Folder.TabIndex = 84
        '
        'cmdASD
        '
        Me.cmdASD.BackColor = System.Drawing.Color.Transparent
        Me.cmdASD.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdASD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdASD.Location = New System.Drawing.Point(357, 140)
        Me.cmdASD.Name = "cmdASD"
        Me.cmdASD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdASD.Size = New System.Drawing.Size(31, 22)
        Me.cmdASD.TabIndex = 87
        Me.cmdASD.Text = "..."
        Me.cmdASD.UseVisualStyleBackColor = False
        '
        'txtASDFolder
        '
        Me.txtASDFolder.AcceptsReturn = True
        Me.txtASDFolder.BackColor = System.Drawing.SystemColors.Window
        Me.txtASDFolder.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtASDFolder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtASDFolder.Location = New System.Drawing.Point(15, 142)
        Me.txtASDFolder.MaxLength = 0
        Me.txtASDFolder.Name = "txtASDFolder"
        Me.txtASDFolder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtASDFolder.Size = New System.Drawing.Size(326, 20)
        Me.txtASDFolder.TabIndex = 88
        '
        'cmdLibObjects
        '
        Me.cmdLibObjects.BackColor = System.Drawing.Color.Transparent
        Me.cmdLibObjects.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLibObjects.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLibObjects.Location = New System.Drawing.Point(357, 23)
        Me.cmdLibObjects.Name = "cmdLibObjects"
        Me.cmdLibObjects.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLibObjects.Size = New System.Drawing.Size(31, 22)
        Me.cmdLibObjects.TabIndex = 90
        Me.cmdLibObjects.Text = "..."
        Me.cmdLibObjects.UseVisualStyleBackColor = False
        '
        'txtLibObjectsFolder
        '
        Me.txtLibObjectsFolder.AcceptsReturn = True
        Me.txtLibObjectsFolder.BackColor = System.Drawing.SystemColors.Window
        Me.txtLibObjectsFolder.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLibObjectsFolder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLibObjectsFolder.Location = New System.Drawing.Point(15, 25)
        Me.txtLibObjectsFolder.MaxLength = 0
        Me.txtLibObjectsFolder.Name = "txtLibObjectsFolder"
        Me.txtLibObjectsFolder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLibObjectsFolder.Size = New System.Drawing.Size(326, 20)
        Me.txtLibObjectsFolder.TabIndex = 91
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(334, 178)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(54, 25)
        Me.cmdOK.TabIndex = 94
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(262, 178)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(54, 25)
        Me.cmdCancel.TabIndex = 93
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'FrmObjectFolders
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(407, 218)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cmdAPI)
        Me.Controls.Add(Me.cmdRyw12)
        Me.Controls.Add(Me.txtAPIFolder)
        Me.Controls.Add(Me.txtRwy12Folder)
        Me.Controls.Add(Me.cmdASD)
        Me.Controls.Add(Me.txtASDFolder)
        Me.Controls.Add(Me.cmdLibObjects)
        Me.Controls.Add(Me.txtLibObjectsFolder)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmObjectFolders"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - Object Folders"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents Label8 As Label
    Public WithEvents Label9 As Label
    Public WithEvents Label10 As Label
    Public WithEvents Label11 As Label
    Public WithEvents cmdAPI As Button
    Public WithEvents cmdRyw12 As Button
    Public WithEvents txtAPIFolder As TextBox
    Public WithEvents txtRwy12Folder As TextBox
    Public WithEvents cmdASD As Button
    Public WithEvents txtASDFolder As TextBox
    Public WithEvents cmdLibObjects As Button
    Public WithEvents txtLibObjectsFolder As TextBox
    Friend WithEvents cmdOK As Button
    Friend WithEvents cmdCancel As Button
End Class
