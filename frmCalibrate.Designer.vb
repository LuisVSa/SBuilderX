<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCalibrate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCalibrate))
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.cmdLL2 = New System.Windows.Forms.Button
        Me.txtLon2 = New System.Windows.Forms.TextBox
        Me.txtLat2 = New System.Windows.Forms.TextBox
        Me.txtPX2 = New System.Windows.Forms.TextBox
        Me.txtPY2 = New System.Windows.Forms.TextBox
        Me.cmdPP2 = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmdCalibrate = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdHelp = New System.Windows.Forms.Button
        Me.Frame3 = New System.Windows.Forms.GroupBox
        Me.optLat = New System.Windows.Forms.RadioButton
        Me.optLon = New System.Windows.Forms.RadioButton
        Me.optLatLon = New System.Windows.Forms.RadioButton
        Me.Frame4 = New System.Windows.Forms.GroupBox
        Me.cmdPP1 = New System.Windows.Forms.Button
        Me.txtPY1 = New System.Windows.Forms.TextBox
        Me.txtPX1 = New System.Windows.Forms.TextBox
        Me.txtLat1 = New System.Windows.Forms.TextBox
        Me.txtLon1 = New System.Windows.Forms.TextBox
        Me.cmdLL1 = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Frame1.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cmdLL2)
        Me.Frame1.Controls.Add(Me.txtLon2)
        Me.Frame1.Controls.Add(Me.txtLat2)
        Me.Frame1.Controls.Add(Me.txtPX2)
        Me.Frame1.Controls.Add(Me.txtPY2)
        Me.Frame1.Controls.Add(Me.cmdPP2)
        Me.Frame1.Controls.Add(Me.Label6)
        Me.Frame1.Controls.Add(Me.Label5)
        Me.Frame1.Controls.Add(Me.Label4)
        Me.Frame1.Controls.Add(Me.Label3)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(12, 141)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(319, 118)
        Me.Frame1.TabIndex = 24
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Lat/Lon and Pixel X/Y of Point P2"
        '
        'cmdLL2
        '
        Me.cmdLL2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLL2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLL2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLL2.Location = New System.Drawing.Point(15, 54)
        Me.cmdLL2.Name = "cmdLL2"
        Me.cmdLL2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLL2.Size = New System.Drawing.Size(52, 25)
        Me.cmdLL2.TabIndex = 24
        Me.cmdLL2.Text = "Change"
        Me.cmdLL2.UseVisualStyleBackColor = False
        '
        'txtLon2
        '
        Me.txtLon2.AcceptsReturn = True
        Me.txtLon2.BackColor = System.Drawing.SystemColors.Window
        Me.txtLon2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLon2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLon2.Location = New System.Drawing.Point(75, 81)
        Me.txtLon2.MaxLength = 0
        Me.txtLon2.Name = "txtLon2"
        Me.txtLon2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLon2.Size = New System.Drawing.Size(103, 20)
        Me.txtLon2.TabIndex = 23
        Me.txtLon2.Text = "W001:00:00.0000"
        Me.txtLon2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtLat2
        '
        Me.txtLat2.AcceptsReturn = True
        Me.txtLat2.BackColor = System.Drawing.SystemColors.Window
        Me.txtLat2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLat2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLat2.Location = New System.Drawing.Point(75, 36)
        Me.txtLat2.MaxLength = 0
        Me.txtLat2.Name = "txtLat2"
        Me.txtLat2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLat2.Size = New System.Drawing.Size(103, 20)
        Me.txtLat2.TabIndex = 22
        Me.txtLat2.Text = "N01:00:00.0000"
        Me.txtLat2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPX2
        '
        Me.txtPX2.AcceptsReturn = True
        Me.txtPX2.BackColor = System.Drawing.SystemColors.Window
        Me.txtPX2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPX2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPX2.Location = New System.Drawing.Point(189, 36)
        Me.txtPX2.MaxLength = 0
        Me.txtPX2.Name = "txtPX2"
        Me.txtPX2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPX2.Size = New System.Drawing.Size(46, 20)
        Me.txtPX2.TabIndex = 21
        Me.txtPX2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPY2
        '
        Me.txtPY2.AcceptsReturn = True
        Me.txtPY2.BackColor = System.Drawing.SystemColors.Window
        Me.txtPY2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPY2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPY2.Location = New System.Drawing.Point(189, 81)
        Me.txtPY2.MaxLength = 0
        Me.txtPY2.Name = "txtPY2"
        Me.txtPY2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPY2.Size = New System.Drawing.Size(46, 20)
        Me.txtPY2.TabIndex = 20
        Me.txtPY2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdPP2
        '
        Me.cmdPP2.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPP2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPP2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPP2.Location = New System.Drawing.Point(243, 54)
        Me.cmdPP2.Name = "cmdPP2"
        Me.cmdPP2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPP2.Size = New System.Drawing.Size(55, 25)
        Me.cmdPP2.TabIndex = 19
        Me.cmdPP2.Text = "Change"
        Me.cmdPP2.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(75, 21)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(52, 13)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "Latitude :"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(75, 66)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(58, 13)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "Longitude :"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(189, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(46, 16)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "Pixel X"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(189, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(52, 19)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Pixel Y"
        '
        'cmdCalibrate
        '
        Me.cmdCalibrate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCalibrate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCalibrate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCalibrate.Location = New System.Drawing.Point(369, 234)
        Me.cmdCalibrate.Name = "cmdCalibrate"
        Me.cmdCalibrate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCalibrate.Size = New System.Drawing.Size(79, 25)
        Me.cmdCalibrate.TabIndex = 23
        Me.cmdCalibrate.Text = "Calibrate"
        Me.cmdCalibrate.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(369, 195)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(79, 25)
        Me.cmdCancel.TabIndex = 22
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdHelp
        '
        Me.cmdHelp.BackColor = System.Drawing.SystemColors.Control
        Me.cmdHelp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdHelp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHelp.Location = New System.Drawing.Point(369, 156)
        Me.cmdHelp.Name = "cmdHelp"
        Me.cmdHelp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdHelp.Size = New System.Drawing.Size(79, 25)
        Me.cmdHelp.TabIndex = 21
        Me.cmdHelp.Text = "Help"
        Me.cmdHelp.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.optLat)
        Me.Frame3.Controls.Add(Me.optLon)
        Me.Frame3.Controls.Add(Me.optLatLon)
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(348, 12)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(112, 106)
        Me.Frame3.TabIndex = 20
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "How to Calibrate"
        '
        'optLat
        '
        Me.optLat.BackColor = System.Drawing.SystemColors.Control
        Me.optLat.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLat.Location = New System.Drawing.Point(9, 21)
        Me.optLat.Name = "optLat"
        Me.optLat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLat.Size = New System.Drawing.Size(88, 30)
        Me.optLat.TabIndex = 14
        Me.optLat.TabStop = True
        Me.optLat.Text = "Use Latitude"
        Me.optLat.UseVisualStyleBackColor = False
        '
        'optLon
        '
        Me.optLon.BackColor = System.Drawing.SystemColors.Control
        Me.optLon.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLon.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLon.Location = New System.Drawing.Point(9, 48)
        Me.optLon.Name = "optLon"
        Me.optLon.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLon.Size = New System.Drawing.Size(97, 21)
        Me.optLon.TabIndex = 13
        Me.optLon.TabStop = True
        Me.optLon.Text = "Use Longitude"
        Me.optLon.UseVisualStyleBackColor = False
        '
        'optLatLon
        '
        Me.optLatLon.BackColor = System.Drawing.SystemColors.Control
        Me.optLatLon.Checked = True
        Me.optLatLon.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLatLon.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLatLon.Location = New System.Drawing.Point(9, 75)
        Me.optLatLon.Name = "optLatLon"
        Me.optLatLon.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLatLon.Size = New System.Drawing.Size(83, 17)
        Me.optLatLon.TabIndex = 12
        Me.optLatLon.TabStop = True
        Me.optLatLon.Text = "Use Both"
        Me.optLatLon.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.cmdPP1)
        Me.Frame4.Controls.Add(Me.txtPY1)
        Me.Frame4.Controls.Add(Me.txtPX1)
        Me.Frame4.Controls.Add(Me.txtLat1)
        Me.Frame4.Controls.Add(Me.txtLon1)
        Me.Frame4.Controls.Add(Me.cmdLL1)
        Me.Frame4.Controls.Add(Me.Label2)
        Me.Frame4.Controls.Add(Me.Label1)
        Me.Frame4.Controls.Add(Me.Label20)
        Me.Frame4.Controls.Add(Me.Label19)
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(12, 12)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(319, 118)
        Me.Frame4.TabIndex = 19
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Lat/Lon and Pixel X/Y of Point P1"
        '
        'cmdPP1
        '
        Me.cmdPP1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPP1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPP1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPP1.Location = New System.Drawing.Point(243, 54)
        Me.cmdPP1.Name = "cmdPP1"
        Me.cmdPP1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPP1.Size = New System.Drawing.Size(55, 25)
        Me.cmdPP1.TabIndex = 8
        Me.cmdPP1.Text = "Change"
        Me.cmdPP1.UseVisualStyleBackColor = False
        '
        'txtPY1
        '
        Me.txtPY1.AcceptsReturn = True
        Me.txtPY1.BackColor = System.Drawing.SystemColors.Window
        Me.txtPY1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPY1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPY1.Location = New System.Drawing.Point(189, 81)
        Me.txtPY1.MaxLength = 0
        Me.txtPY1.Name = "txtPY1"
        Me.txtPY1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPY1.Size = New System.Drawing.Size(46, 20)
        Me.txtPY1.TabIndex = 7
        Me.txtPY1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPX1
        '
        Me.txtPX1.AcceptsReturn = True
        Me.txtPX1.BackColor = System.Drawing.SystemColors.Window
        Me.txtPX1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPX1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPX1.Location = New System.Drawing.Point(189, 36)
        Me.txtPX1.MaxLength = 0
        Me.txtPX1.Name = "txtPX1"
        Me.txtPX1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPX1.Size = New System.Drawing.Size(46, 20)
        Me.txtPX1.TabIndex = 6
        Me.txtPX1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtLat1
        '
        Me.txtLat1.AcceptsReturn = True
        Me.txtLat1.BackColor = System.Drawing.SystemColors.Window
        Me.txtLat1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLat1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLat1.Location = New System.Drawing.Point(75, 36)
        Me.txtLat1.MaxLength = 0
        Me.txtLat1.Name = "txtLat1"
        Me.txtLat1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLat1.Size = New System.Drawing.Size(103, 20)
        Me.txtLat1.TabIndex = 3
        Me.txtLat1.Text = "N01:00:00.0000"
        Me.txtLat1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtLon1
        '
        Me.txtLon1.AcceptsReturn = True
        Me.txtLon1.BackColor = System.Drawing.SystemColors.Window
        Me.txtLon1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLon1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLon1.Location = New System.Drawing.Point(75, 81)
        Me.txtLon1.MaxLength = 0
        Me.txtLon1.Name = "txtLon1"
        Me.txtLon1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLon1.Size = New System.Drawing.Size(103, 20)
        Me.txtLon1.TabIndex = 2
        Me.txtLon1.Text = "W001:00:00.0000"
        Me.txtLon1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdLL1
        '
        Me.cmdLL1.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLL1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLL1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLL1.Location = New System.Drawing.Point(15, 54)
        Me.cmdLL1.Name = "cmdLL1"
        Me.cmdLL1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLL1.Size = New System.Drawing.Size(52, 25)
        Me.cmdLL1.TabIndex = 1
        Me.cmdLL1.Text = "Change"
        Me.cmdLL1.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(189, 66)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(52, 19)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Pixel Y"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(189, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(46, 16)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Pixel X"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(75, 66)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(58, 13)
        Me.Label20.TabIndex = 5
        Me.Label20.Text = "Longitude :"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(75, 21)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(52, 13)
        Me.Label19.TabIndex = 4
        Me.Label19.Text = "Latitude :"
        '
        'frmCalibrate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(480, 277)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.cmdCalibrate)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdHelp)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCalibrate"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SBuilderX - Map Calibration"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdLL2 As System.Windows.Forms.Button
    Public WithEvents txtLon2 As System.Windows.Forms.TextBox
    Public WithEvents txtLat2 As System.Windows.Forms.TextBox
    Public WithEvents txtPX2 As System.Windows.Forms.TextBox
    Public WithEvents txtPY2 As System.Windows.Forms.TextBox
    Public WithEvents cmdPP2 As System.Windows.Forms.Button
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents cmdCalibrate As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdHelp As System.Windows.Forms.Button
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents optLat As System.Windows.Forms.RadioButton
    Public WithEvents optLon As System.Windows.Forms.RadioButton
    Public WithEvents optLatLon As System.Windows.Forms.RadioButton
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents cmdPP1 As System.Windows.Forms.Button
    Public WithEvents txtPY1 As System.Windows.Forms.TextBox
    Public WithEvents txtPX1 As System.Windows.Forms.TextBox
    Public WithEvents txtLat1 As System.Windows.Forms.TextBox
    Public WithEvents txtLon1 As System.Windows.Forms.TextBox
    Public WithEvents cmdLL1 As System.Windows.Forms.Button
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label

End Class
