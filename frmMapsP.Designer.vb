<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMapsP
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMapsP))
        Me.cmdCalibrateMain = New System.Windows.Forms.Button
        Me.cmdData = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdOK = New System.Windows.Forms.Button
        Me.SSTab1 = New System.Windows.Forms.TabControl
        Me._SSTab1_TabPage4 = New System.Windows.Forms.TabPage
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtCellY = New System.Windows.Forms.TextBox
        Me.txtCellX = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.lbRows = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.lbCols = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtELon = New System.Windows.Forms.TextBox
        Me.txtSLat = New System.Windows.Forms.TextBox
        Me.txtWLon = New System.Windows.Forms.TextBox
        Me.txtNLat = New System.Windows.Forms.TextBox
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage
        Me.cmdSummer = New System.Windows.Forms.Button
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtBMPSummer = New System.Windows.Forms.TextBox
        Me.txtBMPSpring = New System.Windows.Forms.TextBox
        Me.txtBMPFall = New System.Windows.Forms.TextBox
        Me.txtBMPWinter = New System.Windows.Forms.TextBox
        Me.txtBMPHard = New System.Windows.Forms.TextBox
        Me.txtBMPNight = New System.Windows.Forms.TextBox
        Me.cmdSpring = New System.Windows.Forms.Button
        Me.cmdFall = New System.Windows.Forms.Button
        Me.cmdWinter = New System.Windows.Forms.Button
        Me.cmdHard = New System.Windows.Forms.Button
        Me.cmdNight = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.cmdGeoTiff = New System.Windows.Forms.Button
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage4.SuspendLayout()
        Me._SSTab1_TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdCalibrateMain
        '
        Me.cmdCalibrateMain.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCalibrateMain.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCalibrateMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCalibrateMain.Location = New System.Drawing.Point(178, 247)
        Me.cmdCalibrateMain.Name = "cmdCalibrateMain"
        Me.cmdCalibrateMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCalibrateMain.Size = New System.Drawing.Size(62, 25)
        Me.cmdCalibrateMain.TabIndex = 84
        Me.cmdCalibrateMain.Text = "Calibrate"
        Me.cmdCalibrateMain.UseVisualStyleBackColor = False
        '
        'cmdData
        '
        Me.cmdData.BackColor = System.Drawing.SystemColors.Control
        Me.cmdData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdData.Location = New System.Drawing.Point(97, 247)
        Me.cmdData.Name = "cmdData"
        Me.cmdData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdData.Size = New System.Drawing.Size(62, 25)
        Me.cmdData.TabIndex = 83
        Me.cmdData.Text = "Data File"
        Me.cmdData.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(259, 247)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(62, 25)
        Me.cmdCancel.TabIndex = 82
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(340, 247)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(62, 25)
        Me.cmdOK.TabIndex = 81
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'SSTab1
        '
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage4)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTab1.Location = New System.Drawing.Point(17, 13)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 0
        Me.SSTab1.Size = New System.Drawing.Size(388, 224)
        Me.SSTab1.TabIndex = 80
        '
        '_SSTab1_TabPage4
        '
        Me._SSTab1_TabPage4.BackColor = System.Drawing.Color.Transparent
        Me._SSTab1_TabPage4.Controls.Add(Me.Label1)
        Me._SSTab1_TabPage4.Controls.Add(Me.Label15)
        Me._SSTab1_TabPage4.Controls.Add(Me.Label14)
        Me._SSTab1_TabPage4.Controls.Add(Me.txtCellY)
        Me._SSTab1_TabPage4.Controls.Add(Me.txtCellX)
        Me._SSTab1_TabPage4.Controls.Add(Me.Label18)
        Me._SSTab1_TabPage4.Controls.Add(Me.txtName)
        Me._SSTab1_TabPage4.Controls.Add(Me.lbRows)
        Me._SSTab1_TabPage4.Controls.Add(Me.Label13)
        Me._SSTab1_TabPage4.Controls.Add(Me.Label12)
        Me._SSTab1_TabPage4.Controls.Add(Me.lbCols)
        Me._SSTab1_TabPage4.Controls.Add(Me.Label11)
        Me._SSTab1_TabPage4.Controls.Add(Me.Label10)
        Me._SSTab1_TabPage4.Controls.Add(Me.Label9)
        Me._SSTab1_TabPage4.Controls.Add(Me.Label8)
        Me._SSTab1_TabPage4.Controls.Add(Me.txtELon)
        Me._SSTab1_TabPage4.Controls.Add(Me.txtSLat)
        Me._SSTab1_TabPage4.Controls.Add(Me.txtWLon)
        Me._SSTab1_TabPage4.Controls.Add(Me.txtNLat)
        Me._SSTab1_TabPage4.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage4.Name = "_SSTab1_TabPage4"
        Me._SSTab1_TabPage4.Size = New System.Drawing.Size(380, 198)
        Me._SSTab1_TabPage4.TabIndex = 4
        Me._SSTab1_TabPage4.Text = "General"
        Me._SSTab1_TabPage4.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(359, 33)
        Me.Label1.TabIndex = 71
        Me.Label1.Text = "Class and Photo maps should have names starting by ""Class"" or ""Photo"". In additio" & _
            "n Photo Maps need to be placed in the ../Tools/Work/ folder."
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(207, 146)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(112, 16)
        Me.Label15.TabIndex = 69
        Me.Label15.Text = "CellYDimensionDeg"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(23, 146)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(112, 16)
        Me.Label14.TabIndex = 70
        Me.Label14.Text = "CellXDimensionDeg"
        '
        'txtCellY
        '
        Me.txtCellY.AcceptsReturn = True
        Me.txtCellY.BackColor = System.Drawing.SystemColors.Window
        Me.txtCellY.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCellY.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCellY.Location = New System.Drawing.Point(210, 165)
        Me.txtCellY.MaxLength = 0
        Me.txtCellY.Name = "txtCellY"
        Me.txtCellY.ReadOnly = True
        Me.txtCellY.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCellY.Size = New System.Drawing.Size(149, 20)
        Me.txtCellY.TabIndex = 67
        Me.txtCellY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtCellX
        '
        Me.txtCellX.AcceptsReturn = True
        Me.txtCellX.BackColor = System.Drawing.SystemColors.Window
        Me.txtCellX.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCellX.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCellX.Location = New System.Drawing.Point(26, 165)
        Me.txtCellX.MaxLength = 0
        Me.txtCellX.Name = "txtCellX"
        Me.txtCellX.ReadOnly = True
        Me.txtCellX.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCellX.Size = New System.Drawing.Size(149, 20)
        Me.txtCellX.TabIndex = 68
        Me.txtCellX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(4, 60)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(64, 13)
        Me.Label18.TabIndex = 66
        Me.Label18.Text = "Map Name"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtName
        '
        Me.txtName.AcceptsReturn = True
        Me.txtName.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtName.Location = New System.Drawing.Point(72, 57)
        Me.txtName.MaxLength = 0
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.Size = New System.Drawing.Size(103, 20)
        Me.txtName.TabIndex = 65
        '
        'lbRows
        '
        Me.lbRows.BackColor = System.Drawing.Color.Transparent
        Me.lbRows.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbRows.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbRows.Location = New System.Drawing.Point(316, 64)
        Me.lbRows.Name = "lbRows"
        Me.lbRows.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbRows.Size = New System.Drawing.Size(46, 19)
        Me.lbRows.TabIndex = 54
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(224, 64)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(95, 16)
        Me.Label13.TabIndex = 53
        Me.Label13.Text = "No of Rows  ="
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(221, 48)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(98, 16)
        Me.Label12.TabIndex = 52
        Me.Label12.Text = "No of Columns  ="
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lbCols
        '
        Me.lbCols.BackColor = System.Drawing.Color.Transparent
        Me.lbCols.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbCols.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbCols.Location = New System.Drawing.Point(316, 48)
        Me.lbCols.Name = "lbCols"
        Me.lbCols.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbCols.Size = New System.Drawing.Size(46, 19)
        Me.lbCols.TabIndex = 51
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(196, 117)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(61, 16)
        Me.Label11.TabIndex = 44
        Me.Label11.Text = "East Long"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(199, 88)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(58, 16)
        Me.Label10.TabIndex = 45
        Me.Label10.Text = "South Lat"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(12, 122)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(56, 16)
        Me.Label9.TabIndex = 46
        Me.Label9.Text = "West Lon"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(15, 92)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(55, 16)
        Me.Label8.TabIndex = 47
        Me.Label8.Text = "North Lat"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtELon
        '
        Me.txtELon.AcceptsReturn = True
        Me.txtELon.BackColor = System.Drawing.SystemColors.Window
        Me.txtELon.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtELon.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtELon.Location = New System.Drawing.Point(259, 114)
        Me.txtELon.MaxLength = 0
        Me.txtELon.Name = "txtELon"
        Me.txtELon.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtELon.Size = New System.Drawing.Size(100, 20)
        Me.txtELon.TabIndex = 40
        Me.txtELon.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSLat
        '
        Me.txtSLat.AcceptsReturn = True
        Me.txtSLat.BackColor = System.Drawing.SystemColors.Window
        Me.txtSLat.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSLat.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSLat.Location = New System.Drawing.Point(259, 84)
        Me.txtSLat.MaxLength = 0
        Me.txtSLat.Name = "txtSLat"
        Me.txtSLat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSLat.Size = New System.Drawing.Size(100, 20)
        Me.txtSLat.TabIndex = 41
        Me.txtSLat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtWLon
        '
        Me.txtWLon.AcceptsReturn = True
        Me.txtWLon.BackColor = System.Drawing.SystemColors.Window
        Me.txtWLon.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWLon.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWLon.Location = New System.Drawing.Point(72, 119)
        Me.txtWLon.MaxLength = 0
        Me.txtWLon.Name = "txtWLon"
        Me.txtWLon.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWLon.Size = New System.Drawing.Size(103, 20)
        Me.txtWLon.TabIndex = 42
        Me.txtWLon.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtNLat
        '
        Me.txtNLat.AcceptsReturn = True
        Me.txtNLat.BackColor = System.Drawing.SystemColors.Window
        Me.txtNLat.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNLat.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNLat.Location = New System.Drawing.Point(72, 88)
        Me.txtNLat.MaxLength = 0
        Me.txtNLat.Name = "txtNLat"
        Me.txtNLat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNLat.Size = New System.Drawing.Size(103, 20)
        Me.txtNLat.TabIndex = 43
        Me.txtNLat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.BackColor = System.Drawing.Color.Transparent
        Me._SSTab1_TabPage1.Controls.Add(Me.cmdSummer)
        Me._SSTab1_TabPage1.Controls.Add(Me.Label21)
        Me._SSTab1_TabPage1.Controls.Add(Me.txtBMPSummer)
        Me._SSTab1_TabPage1.Controls.Add(Me.txtBMPSpring)
        Me._SSTab1_TabPage1.Controls.Add(Me.txtBMPFall)
        Me._SSTab1_TabPage1.Controls.Add(Me.txtBMPWinter)
        Me._SSTab1_TabPage1.Controls.Add(Me.txtBMPHard)
        Me._SSTab1_TabPage1.Controls.Add(Me.txtBMPNight)
        Me._SSTab1_TabPage1.Controls.Add(Me.cmdSpring)
        Me._SSTab1_TabPage1.Controls.Add(Me.cmdFall)
        Me._SSTab1_TabPage1.Controls.Add(Me.cmdWinter)
        Me._SSTab1_TabPage1.Controls.Add(Me.cmdHard)
        Me._SSTab1_TabPage1.Controls.Add(Me.cmdNight)
        Me._SSTab1_TabPage1.Controls.Add(Me.Label2)
        Me._SSTab1_TabPage1.Controls.Add(Me.Label3)
        Me._SSTab1_TabPage1.Controls.Add(Me.Label4)
        Me._SSTab1_TabPage1.Controls.Add(Me.Label5)
        Me._SSTab1_TabPage1.Controls.Add(Me.Label6)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(380, 198)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "Seasons"
        Me._SSTab1_TabPage1.UseVisualStyleBackColor = True
        '
        'cmdSummer
        '
        Me.cmdSummer.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSummer.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSummer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSummer.Location = New System.Drawing.Point(329, 15)
        Me.cmdSummer.Name = "cmdSummer"
        Me.cmdSummer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSummer.Size = New System.Drawing.Size(34, 20)
        Me.cmdSummer.TabIndex = 52
        Me.cmdSummer.Text = " ..."
        Me.cmdSummer.UseVisualStyleBackColor = False
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(24, 17)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(53, 18)
        Me.Label21.TabIndex = 51
        Me.Label21.Text = "Summer"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBMPSummer
        '
        Me.txtBMPSummer.AcceptsReturn = True
        Me.txtBMPSummer.BackColor = System.Drawing.SystemColors.Window
        Me.txtBMPSummer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBMPSummer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBMPSummer.Location = New System.Drawing.Point(83, 17)
        Me.txtBMPSummer.MaxLength = 0
        Me.txtBMPSummer.Name = "txtBMPSummer"
        Me.txtBMPSummer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBMPSummer.Size = New System.Drawing.Size(228, 20)
        Me.txtBMPSummer.TabIndex = 49
        '
        'txtBMPSpring
        '
        Me.txtBMPSpring.AcceptsReturn = True
        Me.txtBMPSpring.BackColor = System.Drawing.SystemColors.Window
        Me.txtBMPSpring.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBMPSpring.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBMPSpring.Location = New System.Drawing.Point(83, 43)
        Me.txtBMPSpring.MaxLength = 0
        Me.txtBMPSpring.Name = "txtBMPSpring"
        Me.txtBMPSpring.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBMPSpring.Size = New System.Drawing.Size(228, 20)
        Me.txtBMPSpring.TabIndex = 14
        '
        'txtBMPFall
        '
        Me.txtBMPFall.AcceptsReturn = True
        Me.txtBMPFall.BackColor = System.Drawing.SystemColors.Window
        Me.txtBMPFall.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBMPFall.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBMPFall.Location = New System.Drawing.Point(83, 69)
        Me.txtBMPFall.MaxLength = 0
        Me.txtBMPFall.Name = "txtBMPFall"
        Me.txtBMPFall.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBMPFall.Size = New System.Drawing.Size(228, 20)
        Me.txtBMPFall.TabIndex = 13
        '
        'txtBMPWinter
        '
        Me.txtBMPWinter.AcceptsReturn = True
        Me.txtBMPWinter.BackColor = System.Drawing.SystemColors.Window
        Me.txtBMPWinter.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBMPWinter.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBMPWinter.Location = New System.Drawing.Point(83, 95)
        Me.txtBMPWinter.MaxLength = 0
        Me.txtBMPWinter.Name = "txtBMPWinter"
        Me.txtBMPWinter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBMPWinter.Size = New System.Drawing.Size(228, 20)
        Me.txtBMPWinter.TabIndex = 12
        '
        'txtBMPHard
        '
        Me.txtBMPHard.AcceptsReturn = True
        Me.txtBMPHard.BackColor = System.Drawing.SystemColors.Window
        Me.txtBMPHard.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBMPHard.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBMPHard.Location = New System.Drawing.Point(83, 121)
        Me.txtBMPHard.MaxLength = 0
        Me.txtBMPHard.Name = "txtBMPHard"
        Me.txtBMPHard.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBMPHard.Size = New System.Drawing.Size(228, 20)
        Me.txtBMPHard.TabIndex = 11
        '
        'txtBMPNight
        '
        Me.txtBMPNight.AcceptsReturn = True
        Me.txtBMPNight.BackColor = System.Drawing.SystemColors.Window
        Me.txtBMPNight.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBMPNight.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBMPNight.Location = New System.Drawing.Point(83, 160)
        Me.txtBMPNight.MaxLength = 0
        Me.txtBMPNight.Name = "txtBMPNight"
        Me.txtBMPNight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBMPNight.Size = New System.Drawing.Size(228, 20)
        Me.txtBMPNight.TabIndex = 10
        '
        'cmdSpring
        '
        Me.cmdSpring.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSpring.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSpring.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSpring.Location = New System.Drawing.Point(329, 42)
        Me.cmdSpring.Name = "cmdSpring"
        Me.cmdSpring.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSpring.Size = New System.Drawing.Size(34, 20)
        Me.cmdSpring.TabIndex = 8
        Me.cmdSpring.Text = "..."
        Me.cmdSpring.UseVisualStyleBackColor = False
        '
        'cmdFall
        '
        Me.cmdFall.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFall.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFall.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFall.Location = New System.Drawing.Point(329, 68)
        Me.cmdFall.Name = "cmdFall"
        Me.cmdFall.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFall.Size = New System.Drawing.Size(34, 20)
        Me.cmdFall.TabIndex = 7
        Me.cmdFall.Text = "..."
        Me.cmdFall.UseVisualStyleBackColor = False
        '
        'cmdWinter
        '
        Me.cmdWinter.BackColor = System.Drawing.SystemColors.Control
        Me.cmdWinter.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdWinter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdWinter.Location = New System.Drawing.Point(329, 94)
        Me.cmdWinter.Name = "cmdWinter"
        Me.cmdWinter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdWinter.Size = New System.Drawing.Size(34, 20)
        Me.cmdWinter.TabIndex = 6
        Me.cmdWinter.Text = "..."
        Me.cmdWinter.UseVisualStyleBackColor = False
        '
        'cmdHard
        '
        Me.cmdHard.BackColor = System.Drawing.SystemColors.Control
        Me.cmdHard.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdHard.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHard.Location = New System.Drawing.Point(329, 121)
        Me.cmdHard.Name = "cmdHard"
        Me.cmdHard.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdHard.Size = New System.Drawing.Size(34, 20)
        Me.cmdHard.TabIndex = 5
        Me.cmdHard.Text = "..."
        Me.cmdHard.UseVisualStyleBackColor = False
        '
        'cmdNight
        '
        Me.cmdNight.BackColor = System.Drawing.SystemColors.Control
        Me.cmdNight.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdNight.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdNight.Location = New System.Drawing.Point(329, 160)
        Me.cmdNight.Name = "cmdNight"
        Me.cmdNight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdNight.Size = New System.Drawing.Size(34, 20)
        Me.cmdNight.TabIndex = 3
        Me.cmdNight.Text = "..."
        Me.cmdNight.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(22, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(55, 16)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "Spring"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(22, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(55, 16)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Fall"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(22, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(55, 16)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Winter"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(2, 121)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(75, 19)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Hard Winter"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(22, 164)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(55, 16)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Night"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdGeoTiff
        '
        Me.cmdGeoTiff.BackColor = System.Drawing.SystemColors.Control
        Me.cmdGeoTiff.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGeoTiff.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGeoTiff.Location = New System.Drawing.Point(16, 247)
        Me.cmdGeoTiff.Name = "cmdGeoTiff"
        Me.cmdGeoTiff.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGeoTiff.Size = New System.Drawing.Size(62, 25)
        Me.cmdGeoTiff.TabIndex = 85
        Me.cmdGeoTiff.Text = "GeoTiff"
        Me.cmdGeoTiff.UseVisualStyleBackColor = False
        '
        'frmMapsP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(421, 284)
        Me.Controls.Add(Me.cmdGeoTiff)
        Me.Controls.Add(Me.cmdCalibrateMain)
        Me.Controls.Add(Me.cmdData)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.SSTab1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMapsP"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - Map Properties"
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage4.ResumeLayout(False)
        Me._SSTab1_TabPage4.PerformLayout()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        Me._SSTab1_TabPage1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents cmdCalibrateMain As System.Windows.Forms.Button
    Public WithEvents cmdData As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents SSTab1 As System.Windows.Forms.TabControl
    Public WithEvents _SSTab1_TabPage4 As System.Windows.Forms.TabPage
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents txtCellY As System.Windows.Forms.TextBox
    Public WithEvents txtCellX As System.Windows.Forms.TextBox
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents lbRows As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents lbCols As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents txtELon As System.Windows.Forms.TextBox
    Public WithEvents txtSLat As System.Windows.Forms.TextBox
    Public WithEvents txtWLon As System.Windows.Forms.TextBox
    Public WithEvents txtNLat As System.Windows.Forms.TextBox
    Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents cmdSummer As System.Windows.Forms.Button
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents txtBMPSummer As System.Windows.Forms.TextBox
    Public WithEvents txtBMPSpring As System.Windows.Forms.TextBox
    Public WithEvents txtBMPFall As System.Windows.Forms.TextBox
    Public WithEvents txtBMPWinter As System.Windows.Forms.TextBox
    Public WithEvents txtBMPHard As System.Windows.Forms.TextBox
    Public WithEvents txtBMPNight As System.Windows.Forms.TextBox
    Public WithEvents cmdSpring As System.Windows.Forms.Button
    Public WithEvents cmdFall As System.Windows.Forms.Button
    Public WithEvents cmdWinter As System.Windows.Forms.Button
    Public WithEvents cmdHard As System.Windows.Forms.Button
    Public WithEvents cmdNight As System.Windows.Forms.Button
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents cmdGeoTiff As System.Windows.Forms.Button

End Class
