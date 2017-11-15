<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLandsP
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLandsP))
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdClose = New System.Windows.Forms.Button
        Me.List1 = New System.Windows.Forms.ListBox
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.cmdAuto = New System.Windows.Forms.Button
        Me.cmdEdit = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.lbLand = New System.Windows.Forms.Label
        Me.ImgText = New System.Windows.Forms.PictureBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.ck4 = New System.Windows.Forms.RadioButton
        Me.ck2 = New System.Windows.Forms.RadioButton
        Me.ck1 = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.optDelete = New System.Windows.Forms.RadioButton
        Me.optRaster = New System.Windows.Forms.RadioButton
        Me.optClick = New System.Windows.Forms.RadioButton
        Me.Frame1.SuspendLayout()
        CType(Me.ImgText, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(213, 13)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "Click to see a larger image in a new window"
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(540, 256)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(60, 25)
        Me.cmdCancel.TabIndex = 50
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Location = New System.Drawing.Point(612, 256)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(62, 25)
        Me.cmdClose.TabIndex = 48
        Me.cmdClose.Text = "OK"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'List1
        '
        Me.List1.BackColor = System.Drawing.SystemColors.Window
        Me.List1.Cursor = System.Windows.Forms.Cursors.Default
        Me.List1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.List1.Location = New System.Drawing.Point(285, 56)
        Me.List1.Name = "List1"
        Me.List1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.List1.Size = New System.Drawing.Size(240, 225)
        Me.List1.TabIndex = 45
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cmdAuto)
        Me.Frame1.Controls.Add(Me.cmdEdit)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Frame1.Location = New System.Drawing.Point(540, 169)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(134, 68)
        Me.Frame1.TabIndex = 52
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Class Map"
        '
        'cmdAuto
        '
        Me.cmdAuto.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAuto.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAuto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAuto.Location = New System.Drawing.Point(72, 27)
        Me.cmdAuto.Name = "cmdAuto"
        Me.cmdAuto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAuto.Size = New System.Drawing.Size(48, 25)
        Me.cmdAuto.TabIndex = 12
        Me.cmdAuto.Text = "Make"
        Me.cmdAuto.UseVisualStyleBackColor = False
        '
        'cmdEdit
        '
        Me.cmdEdit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEdit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEdit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEdit.Location = New System.Drawing.Point(12, 27)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEdit.Size = New System.Drawing.Size(51, 25)
        Me.cmdEdit.TabIndex = 11
        Me.cmdEdit.Text = "Edit"
        Me.cmdEdit.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(282, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(184, 13)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "Click to Edit the Color"
        '
        'lbLand
        '
        Me.lbLand.BackColor = System.Drawing.SystemColors.Control
        Me.lbLand.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbLand.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbLand.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbLand.Location = New System.Drawing.Point(285, 25)
        Me.lbLand.Name = "lbLand"
        Me.lbLand.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbLand.Size = New System.Drawing.Size(240, 19)
        Me.lbLand.TabIndex = 46
        Me.lbLand.Text = "Label1"
        Me.lbLand.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ImgText
        '
        Me.ImgText.Cursor = System.Windows.Forms.Cursors.Default
        Me.ImgText.Location = New System.Drawing.Point(15, 25)
        Me.ImgText.Name = "ImgText"
        Me.ImgText.Size = New System.Drawing.Size(256, 256)
        Me.ImgText.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ImgText.TabIndex = 53
        Me.ImgText.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ck4)
        Me.GroupBox1.Controls.Add(Me.ck2)
        Me.GroupBox1.Controls.Add(Me.ck1)
        Me.GroupBox1.Location = New System.Drawing.Point(540, 114)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(134, 49)
        Me.GroupBox1.TabIndex = 54
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Brush Insert Size"
        '
        'ck4
        '
        Me.ck4.AutoSize = True
        Me.ck4.Location = New System.Drawing.Point(93, 19)
        Me.ck4.Name = "ck4"
        Me.ck4.Size = New System.Drawing.Size(37, 17)
        Me.ck4.TabIndex = 2
        Me.ck4.Text = "49"
        Me.ck4.UseVisualStyleBackColor = True
        '
        'ck2
        '
        Me.ck2.AutoSize = True
        Me.ck2.Location = New System.Drawing.Point(54, 19)
        Me.ck2.Name = "ck2"
        Me.ck2.Size = New System.Drawing.Size(31, 17)
        Me.ck2.TabIndex = 1
        Me.ck2.Text = "9"
        Me.ck2.UseVisualStyleBackColor = True
        '
        'ck1
        '
        Me.ck1.AutoSize = True
        Me.ck1.Checked = True
        Me.ck1.Location = New System.Drawing.Point(15, 19)
        Me.ck1.Name = "ck1"
        Me.ck1.Size = New System.Drawing.Size(31, 17)
        Me.ck1.TabIndex = 0
        Me.ck1.TabStop = True
        Me.ck1.Text = "1"
        Me.ck1.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.optDelete)
        Me.GroupBox2.Controls.Add(Me.optRaster)
        Me.GroupBox2.Controls.Add(Me.optClick)
        Me.GroupBox2.Location = New System.Drawing.Point(540, 9)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(134, 99)
        Me.GroupBox2.TabIndex = 55
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Mode"
        '
        'optDelete
        '
        Me.optDelete.AutoSize = True
        Me.optDelete.BackColor = System.Drawing.SystemColors.Control
        Me.optDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDelete.Location = New System.Drawing.Point(18, 65)
        Me.optDelete.Name = "optDelete"
        Me.optDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDelete.Size = New System.Drawing.Size(97, 17)
        Me.optDelete.TabIndex = 10
        Me.optDelete.TabStop = True
        Me.optDelete.Text = "Delete on Click"
        Me.optDelete.UseVisualStyleBackColor = False
        '
        'optRaster
        '
        Me.optRaster.AutoSize = True
        Me.optRaster.BackColor = System.Drawing.SystemColors.Control
        Me.optRaster.Cursor = System.Windows.Forms.Cursors.Default
        Me.optRaster.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optRaster.Location = New System.Drawing.Point(18, 44)
        Me.optRaster.Name = "optRaster"
        Me.optRaster.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optRaster.Size = New System.Drawing.Size(100, 17)
        Me.optRaster.TabIndex = 9
        Me.optRaster.TabStop = True
        Me.optRaster.Text = "Insert on Raster"
        Me.optRaster.UseVisualStyleBackColor = False
        '
        'optClick
        '
        Me.optClick.AutoSize = True
        Me.optClick.BackColor = System.Drawing.SystemColors.Control
        Me.optClick.Checked = True
        Me.optClick.Cursor = System.Windows.Forms.Cursors.Default
        Me.optClick.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optClick.Location = New System.Drawing.Point(18, 23)
        Me.optClick.Name = "optClick"
        Me.optClick.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optClick.Size = New System.Drawing.Size(92, 17)
        Me.optClick.TabIndex = 8
        Me.optClick.TabStop = True
        Me.optClick.Text = "Insert on Click"
        Me.optClick.UseVisualStyleBackColor = False
        '
        'frmLandsP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(690, 297)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.List1)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lbLand)
        Me.Controls.Add(Me.ImgText)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmLandsP"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "SBuilderX - Land Class Properties"
        Me.Frame1.ResumeLayout(False)
        CType(Me.ImgText, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents List1 As System.Windows.Forms.ListBox
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdAuto As System.Windows.Forms.Button
    Public WithEvents cmdEdit As System.Windows.Forms.Button
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lbLand As System.Windows.Forms.Label
    Public WithEvents ImgText As System.Windows.Forms.PictureBox
    Friend WithEvents ck4 As System.Windows.Forms.RadioButton
    Friend WithEvents ck2 As System.Windows.Forms.RadioButton
    Friend WithEvents ck1 As System.Windows.Forms.RadioButton
    Public WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents optDelete As System.Windows.Forms.RadioButton
    Public WithEvents optRaster As System.Windows.Forms.RadioButton
    Public WithEvents optClick As System.Windows.Forms.RadioButton
End Class
