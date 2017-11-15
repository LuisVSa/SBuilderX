<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSurfer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSurfer))
        Me.ckAutoLine = New System.Windows.Forms.CheckBox
        Me.ckLineAltitude = New System.Windows.Forms.CheckBox
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdContinue = New System.Windows.Forms.Button
        Me.lbEnd = New System.Windows.Forms.Label
        Me.lbStart = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.lbLineColor = New System.Windows.Forms.Label
        Me.txtEndW = New System.Windows.Forms.TextBox
        Me.txtLineGuid = New System.Windows.Forms.TextBox
        Me.txtStartW = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.lbPolyColor = New System.Windows.Forms.Label
        Me.ckPolyAltitude = New System.Windows.Forms.CheckBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtPolyGuid = New System.Windows.Forms.TextBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ckAutoLine
        '
        Me.ckAutoLine.BackColor = System.Drawing.SystemColors.Control
        Me.ckAutoLine.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckAutoLine.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckAutoLine.Location = New System.Drawing.Point(16, 66)
        Me.ckAutoLine.Name = "ckAutoLine"
        Me.ckAutoLine.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckAutoLine.Size = New System.Drawing.Size(114, 35)
        Me.ckAutoLine.TabIndex = 22
        Me.ckAutoLine.Text = "Create closed line"
        Me.ckAutoLine.UseVisualStyleBackColor = False
        '
        'ckLineAltitude
        '
        Me.ckLineAltitude.BackColor = System.Drawing.SystemColors.Control
        Me.ckLineAltitude.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckLineAltitude.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckLineAltitude.Location = New System.Drawing.Point(17, 110)
        Me.ckLineAltitude.Name = "ckLineAltitude"
        Me.ckLineAltitude.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckLineAltitude.Size = New System.Drawing.Size(241, 21)
        Me.ckLineAltitude.TabIndex = 19
        Me.ckLineAltitude.Text = "Interpret 3rd parameter, if present, as altitude"
        Me.ckLineAltitude.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(142, 314)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(70, 25)
        Me.cmdCancel.TabIndex = 16
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdContinue
        '
        Me.cmdContinue.BackColor = System.Drawing.SystemColors.Control
        Me.cmdContinue.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdContinue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdContinue.Location = New System.Drawing.Point(240, 314)
        Me.cmdContinue.Name = "cmdContinue"
        Me.cmdContinue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdContinue.Size = New System.Drawing.Size(70, 25)
        Me.cmdContinue.TabIndex = 13
        Me.cmdContinue.Text = "Continue ..."
        Me.cmdContinue.UseVisualStyleBackColor = False
        '
        'lbEnd
        '
        Me.lbEnd.AutoSize = True
        Me.lbEnd.BackColor = System.Drawing.SystemColors.Control
        Me.lbEnd.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbEnd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbEnd.Location = New System.Drawing.Point(80, 68)
        Me.lbEnd.Name = "lbEnd"
        Me.lbEnd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbEnd.Size = New System.Drawing.Size(57, 13)
        Me.lbEnd.TabIndex = 21
        Me.lbEnd.Text = "End Width"
        '
        'lbStart
        '
        Me.lbStart.AutoSize = True
        Me.lbStart.BackColor = System.Drawing.SystemColors.Control
        Me.lbStart.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbStart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbStart.Location = New System.Drawing.Point(14, 68)
        Me.lbStart.Name = "lbStart"
        Me.lbStart.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbStart.Size = New System.Drawing.Size(60, 13)
        Me.lbStart.TabIndex = 20
        Me.lbStart.Text = "Start Width"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(14, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(244, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Guid of imported lines. Click on the box to change."
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(12, 314)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(112, 32)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Edit SBuilder.ini to change Separator"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lbLineColor)
        Me.GroupBox1.Controls.Add(Me.txtEndW)
        Me.GroupBox1.Controls.Add(Me.txtLineGuid)
        Me.GroupBox1.Controls.Add(Me.txtStartW)
        Me.GroupBox1.Controls.Add(Me.ckLineAltitude)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lbStart)
        Me.GroupBox1.Controls.Add(Me.lbEnd)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(298, 136)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Properties of Imported Lines"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(137, 87)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(113, 13)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Color (click to change)"
        '
        'lbLineColor
        '
        Me.lbLineColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbLineColor.Location = New System.Drawing.Point(256, 84)
        Me.lbLineColor.Name = "lbLineColor"
        Me.lbLineColor.Size = New System.Drawing.Size(23, 20)
        Me.lbLineColor.TabIndex = 23
        Me.lbLineColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtEndW
        '
        Me.txtEndW.Location = New System.Drawing.Point(80, 84)
        Me.txtEndW.Name = "txtEndW"
        Me.txtEndW.Size = New System.Drawing.Size(51, 20)
        Me.txtEndW.TabIndex = 22
        Me.txtEndW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtLineGuid
        '
        Me.txtLineGuid.BackColor = System.Drawing.Color.White
        Me.txtLineGuid.Location = New System.Drawing.Point(17, 41)
        Me.txtLineGuid.Name = "txtLineGuid"
        Me.txtLineGuid.ReadOnly = True
        Me.txtLineGuid.Size = New System.Drawing.Size(262, 20)
        Me.txtLineGuid.TabIndex = 21
        Me.txtLineGuid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtStartW
        '
        Me.txtStartW.Location = New System.Drawing.Point(17, 84)
        Me.txtStartW.Name = "txtStartW"
        Me.txtStartW.Size = New System.Drawing.Size(57, 20)
        Me.txtStartW.TabIndex = 20
        Me.txtStartW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.lbPolyColor)
        Me.GroupBox2.Controls.Add(Me.ckPolyAltitude)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtPolyGuid)
        Me.GroupBox2.Controls.Add(Me.ckAutoLine)
        Me.GroupBox2.Location = New System.Drawing.Point(13, 163)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(297, 132)
        Me.GroupBox2.TabIndex = 25
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Properties of Imported Polygons"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(136, 76)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(113, 13)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "Color (click to change)"
        '
        'lbPolyColor
        '
        Me.lbPolyColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbPolyColor.Location = New System.Drawing.Point(255, 74)
        Me.lbPolyColor.Name = "lbPolyColor"
        Me.lbPolyColor.Size = New System.Drawing.Size(23, 20)
        Me.lbPolyColor.TabIndex = 25
        Me.lbPolyColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ckPolyAltitude
        '
        Me.ckPolyAltitude.AutoSize = True
        Me.ckPolyAltitude.BackColor = System.Drawing.SystemColors.Control
        Me.ckPolyAltitude.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckPolyAltitude.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckPolyAltitude.Location = New System.Drawing.Point(16, 107)
        Me.ckPolyAltitude.Name = "ckPolyAltitude"
        Me.ckPolyAltitude.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckPolyAltitude.Size = New System.Drawing.Size(236, 17)
        Me.ckPolyAltitude.TabIndex = 24
        Me.ckPolyAltitude.Text = "Interpret 3rd parameter, if present, as altitude"
        Me.ckPolyAltitude.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(13, 24)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(265, 13)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Guid of imported polygons. Click on the box to change."
        '
        'txtPolyGuid
        '
        Me.txtPolyGuid.BackColor = System.Drawing.Color.White
        Me.txtPolyGuid.Location = New System.Drawing.Point(16, 40)
        Me.txtPolyGuid.Name = "txtPolyGuid"
        Me.txtPolyGuid.ReadOnly = True
        Me.txtPolyGuid.Size = New System.Drawing.Size(262, 20)
        Me.txtPolyGuid.TabIndex = 21
        Me.txtPolyGuid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frmSurfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(323, 355)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdContinue)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSurfer"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - BLN Import Properties"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents ckAutoLine As System.Windows.Forms.CheckBox
    Public WithEvents ckLineAltitude As System.Windows.Forms.CheckBox
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdContinue As System.Windows.Forms.Button
    Public WithEvents lbEnd As System.Windows.Forms.Label
    Public WithEvents lbStart As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtStartW As System.Windows.Forms.TextBox
    Friend WithEvents txtEndW As System.Windows.Forms.TextBox
    Friend WithEvents txtLineGuid As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPolyGuid As System.Windows.Forms.TextBox
    Friend WithEvents lbLineColor As System.Windows.Forms.Label
    Friend WithEvents lbPolyColor As System.Windows.Forms.Label
    Public WithEvents ckPolyAltitude As System.Windows.Forms.CheckBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label

End Class
