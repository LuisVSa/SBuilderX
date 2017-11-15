<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBackground
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBackground))
        Me.ckStartFSX = New System.Windows.Forms.CheckBox
        Me.ckCopyBGLs = New System.Windows.Forms.CheckBox
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdCompile = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmdSave = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ckStartFSX
        '
        Me.ckStartFSX.AutoSize = True
        Me.ckStartFSX.BackColor = System.Drawing.SystemColors.Control
        Me.ckStartFSX.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ckStartFSX.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckStartFSX.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckStartFSX.Location = New System.Drawing.Point(109, 29)
        Me.ckStartFSX.Name = "ckStartFSX"
        Me.ckStartFSX.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ckStartFSX.Size = New System.Drawing.Size(71, 17)
        Me.ckStartFSX.TabIndex = 27
        Me.ckStartFSX.Text = "Start FSX"
        Me.ckStartFSX.UseVisualStyleBackColor = False
        '
        'ckCopyBGLs
        '
        Me.ckCopyBGLs.BackColor = System.Drawing.SystemColors.Control
        Me.ckCopyBGLs.Checked = True
        Me.ckCopyBGLs.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckCopyBGLs.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckCopyBGLs.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckCopyBGLs.Location = New System.Drawing.Point(17, 20)
        Me.ckCopyBGLs.Name = "ckCopyBGLs"
        Me.ckCopyBGLs.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckCopyBGLs.Size = New System.Drawing.Size(97, 34)
        Me.ckCopyBGLs.TabIndex = 26
        Me.ckCopyBGLs.Text = "Copy BGL file to BGL folder"
        Me.ckCopyBGLs.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(226, 32)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(57, 25)
        Me.cmdCancel.TabIndex = 25
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdCompile
        '
        Me.cmdCompile.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCompile.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCompile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCompile.Location = New System.Drawing.Point(226, 112)
        Me.cmdCompile.Name = "cmdCompile"
        Me.cmdCompile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCompile.Size = New System.Drawing.Size(57, 25)
        Me.cmdCompile.TabIndex = 24
        Me.cmdCompile.Text = "Compile"
        Me.cmdCompile.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ckStartFSX)
        Me.GroupBox1.Controls.Add(Me.ckCopyBGLs)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 70)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(186, 67)
        Me.GroupBox1.TabIndex = 28
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Compile Options"
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(226, 70)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(57, 25)
        Me.cmdSave.TabIndex = 29
        Me.cmdSave.Text = "Save ..."
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(14, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(195, 48)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "You can save the background image as a Geotiff file or you can compile the backgr" & _
            "ound as a photo scenery BGL."
        '
        'frmBackground
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(298, 152)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdCompile)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBackground"
        Me.Text = "SBuilderX - Background"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents ckStartFSX As System.Windows.Forms.CheckBox
    Public WithEvents ckCopyBGLs As System.Windows.Forms.CheckBox
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdCompile As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
