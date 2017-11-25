<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLinesP
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLinesP))
        Me.imgVector = New System.Windows.Forms.PictureBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.ckThisColor = New System.Windows.Forms.CheckBox()
        Me.cmdColor = New System.Windows.Forms.Button()
        Me.cmdWinding = New System.Windows.Forms.Button()
        Me.labelReverse = New System.Windows.Forms.Label()
        Me.boxType = New System.Windows.Forms.GroupBox()
        Me.optObjects = New System.Windows.Forms.RadioButton()
        Me.cmdType = New System.Windows.Forms.Button()
        Me.optExtrusion = New System.Windows.Forms.RadioButton()
        Me.optTexture = New System.Windows.Forms.RadioButton()
        Me.optVector = New System.Windows.Forms.RadioButton()
        Me.boxAltitude = New System.Windows.Forms.GroupBox()
        Me.txtAlt = New System.Windows.Forms.TextBox()
        Me.cmdAlt = New System.Windows.Forms.Button()
        Me.boxProgressive = New System.Windows.Forms.GroupBox()
        Me.cmdReverse = New System.Windows.Forms.Button()
        Me.cmdWidth12 = New System.Windows.Forms.Button()
        Me.txtWidth1 = New System.Windows.Forms.TextBox()
        Me.txtWidth2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.boxWidth = New System.Windows.Forms.GroupBox()
        Me.txtWidth = New System.Windows.Forms.TextBox()
        Me.cmdWidth = New System.Windows.Forms.Button()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.labelName = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.labelDir = New System.Windows.Forms.Label()
        Me.cbLanes = New System.Windows.Forms.ComboBox()
        Me.labelLanes = New System.Windows.Forms.Label()
        Me.cbDir = New System.Windows.Forms.ComboBox()
        Me.cmdDetail = New System.Windows.Forms.Button()
        Me.labelVectorTexture = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.listVector = New System.Windows.Forms.ListBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.boxTexType = New System.Windows.Forms.GroupBox()
        Me.txtV1 = New System.Windows.Forms.TextBox()
        Me.txtTexPri = New System.Windows.Forms.TextBox()
        Me.cmbComplex = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.optStanding = New System.Windows.Forms.RadioButton()
        Me.optLying = New System.Windows.Forms.RadioButton()
        Me.lbTexPri = New System.Windows.Forms.Label()
        Me.LbV1 = New System.Windows.Forms.Label()
        Me.imgTex = New System.Windows.Forms.PictureBox()
        Me.boxTexTexture = New System.Windows.Forms.GroupBox()
        Me.optTile = New System.Windows.Forms.RadioButton()
        Me.optStretch = New System.Windows.Forms.RadioButton()
        Me.cmdTex = New System.Windows.Forms.Button()
        Me.txtTexName = New System.Windows.Forms.TextBox()
        Me.ckNight = New System.Windows.Forms.CheckBox()
        Me.optFull = New System.Windows.Forms.RadioButton()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.cmdExtrusionProperties = New System.Windows.Forms.Button()
        Me.labelProfile = New System.Windows.Forms.Label()
        Me.lbguid = New System.Windows.Forms.Label()
        Me.lbExt1 = New System.Windows.Forms.Label()
        Me.listExtrusion = New System.Windows.Forms.ListBox()
        Me.imgExtrusion = New System.Windows.Forms.PictureBox()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.boxHeading = New System.Windows.Forms.GroupBox()
        Me.ckRandom = New System.Windows.Forms.CheckBox()
        Me.txtHeading = New System.Windows.Forms.TextBox()
        Me.cmdHeading = New System.Windows.Forms.Button()
        Me.cmbComplexity = New System.Windows.Forms.ComboBox()
        Me.labelComplexity = New System.Windows.Forms.Label()
        Me.labelCat = New System.Windows.Forms.Label()
        Me.imgLib = New System.Windows.Forms.PictureBox()
        Me.lstLib = New System.Windows.Forms.ListBox()
        Me.labelLibID = New System.Windows.Forms.Label()
        Me.cmbLibCat = New System.Windows.Forms.ComboBox()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdSample = New System.Windows.Forms.Button()
        Me.cmdSmooth = New System.Windows.Forms.Button()
        CType(Me.imgVector, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.boxType.SuspendLayout()
        Me.boxAltitude.SuspendLayout()
        Me.boxProgressive.SuspendLayout()
        Me.boxWidth.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.boxTexType.SuspendLayout()
        CType(Me.imgTex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.boxTexTexture.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.imgExtrusion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        Me.boxHeading.SuspendLayout()
        CType(Me.imgLib, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'imgVector
        '
        Me.imgVector.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imgVector.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgVector.Location = New System.Drawing.Point(15, 29)
        Me.imgVector.Name = "imgVector"
        Me.imgVector.Size = New System.Drawing.Size(200, 200)
        Me.imgVector.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgVector.TabIndex = 7
        Me.imgVector.TabStop = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(488, 267)
        Me.TabControl1.TabIndex = 25
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.ckThisColor)
        Me.TabPage1.Controls.Add(Me.cmdColor)
        Me.TabPage1.Controls.Add(Me.cmdWinding)
        Me.TabPage1.Controls.Add(Me.labelReverse)
        Me.TabPage1.Controls.Add(Me.boxType)
        Me.TabPage1.Controls.Add(Me.boxAltitude)
        Me.TabPage1.Controls.Add(Me.boxProgressive)
        Me.TabPage1.Controls.Add(Me.boxWidth)
        Me.TabPage1.Controls.Add(Me.txtName)
        Me.TabPage1.Controls.Add(Me.labelName)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(480, 241)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'ckThisColor
        '
        Me.ckThisColor.AllowDrop = True
        Me.ckThisColor.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.ckThisColor.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ckThisColor.Location = New System.Drawing.Point(344, 16)
        Me.ckThisColor.Name = "ckThisColor"
        Me.ckThisColor.Size = New System.Drawing.Size(118, 32)
        Me.ckThisColor.TabIndex = 40
        Me.ckThisColor.Text = "Also use this Color if Vector Line?"
        Me.ckThisColor.UseVisualStyleBackColor = False
        '
        'cmdColor
        '
        Me.cmdColor.Location = New System.Drawing.Point(298, 19)
        Me.cmdColor.Name = "cmdColor"
        Me.cmdColor.Size = New System.Drawing.Size(40, 25)
        Me.cmdColor.TabIndex = 39
        Me.cmdColor.Text = "Color"
        Me.cmdColor.UseVisualStyleBackColor = True
        '
        'cmdWinding
        '
        Me.cmdWinding.Location = New System.Drawing.Point(134, 83)
        Me.cmdWinding.Name = "cmdWinding"
        Me.cmdWinding.Size = New System.Drawing.Size(63, 25)
        Me.cmdWinding.TabIndex = 40
        Me.cmdWinding.Text = "Reverse"
        Me.cmdWinding.UseVisualStyleBackColor = True
        '
        'labelReverse
        '
        Me.labelReverse.Location = New System.Drawing.Point(14, 63)
        Me.labelReverse.Name = "labelReverse"
        Me.labelReverse.Size = New System.Drawing.Size(114, 57)
        Me.labelReverse.TabIndex = 66
        Me.labelReverse.Text = "On a shoreline with waves, Reverse will place the waves on the other side."
        '
        'boxType
        '
        Me.boxType.Controls.Add(Me.optObjects)
        Me.boxType.Controls.Add(Me.cmdType)
        Me.boxType.Controls.Add(Me.optExtrusion)
        Me.boxType.Controls.Add(Me.optTexture)
        Me.boxType.Controls.Add(Me.optVector)
        Me.boxType.Location = New System.Drawing.Point(15, 123)
        Me.boxType.Name = "boxType"
        Me.boxType.Size = New System.Drawing.Size(182, 97)
        Me.boxType.TabIndex = 65
        Me.boxType.TabStop = False
        Me.boxType.Text = "Line type"
        '
        'optObjects
        '
        Me.optObjects.Location = New System.Drawing.Point(109, 9)
        Me.optObjects.Name = "optObjects"
        Me.optObjects.Size = New System.Drawing.Size(67, 37)
        Me.optObjects.TabIndex = 1
        Me.optObjects.Text = "Line of Objects"
        Me.optObjects.UseVisualStyleBackColor = True
        '
        'cmdType
        '
        Me.cmdType.Location = New System.Drawing.Point(135, 59)
        Me.cmdType.Name = "cmdType"
        Me.cmdType.Size = New System.Drawing.Size(34, 24)
        Me.cmdType.TabIndex = 2
        Me.cmdType.Text = ">>>"
        Me.cmdType.UseVisualStyleBackColor = True
        '
        'optExtrusion
        '
        Me.optExtrusion.AutoSize = True
        Me.optExtrusion.Location = New System.Drawing.Point(13, 42)
        Me.optExtrusion.Name = "optExtrusion"
        Me.optExtrusion.Size = New System.Drawing.Size(101, 17)
        Me.optExtrusion.TabIndex = 1
        Me.optExtrusion.Text = "Extrusion Bridge"
        Me.optExtrusion.UseVisualStyleBackColor = True
        '
        'optTexture
        '
        Me.optTexture.AutoSize = True
        Me.optTexture.Location = New System.Drawing.Point(13, 63)
        Me.optTexture.Name = "optTexture"
        Me.optTexture.Size = New System.Drawing.Size(90, 17)
        Me.optTexture.TabIndex = 1
        Me.optTexture.Text = "Textured Line"
        Me.optTexture.UseVisualStyleBackColor = True
        '
        'optVector
        '
        Me.optVector.AutoSize = True
        Me.optVector.Checked = True
        Me.optVector.Location = New System.Drawing.Point(13, 19)
        Me.optVector.Name = "optVector"
        Me.optVector.Size = New System.Drawing.Size(79, 17)
        Me.optVector.TabIndex = 0
        Me.optVector.TabStop = True
        Me.optVector.Text = "Vector Line"
        Me.optVector.UseVisualStyleBackColor = True
        '
        'boxAltitude
        '
        Me.boxAltitude.Controls.Add(Me.txtAlt)
        Me.boxAltitude.Controls.Add(Me.cmdAlt)
        Me.boxAltitude.Location = New System.Drawing.Point(344, 152)
        Me.boxAltitude.Name = "boxAltitude"
        Me.boxAltitude.Size = New System.Drawing.Size(118, 68)
        Me.boxAltitude.TabIndex = 58
        Me.boxAltitude.TabStop = False
        Me.boxAltitude.Text = "Altitude"
        '
        'txtAlt
        '
        Me.txtAlt.BackColor = System.Drawing.SystemColors.Window
        Me.txtAlt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAlt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAlt.Location = New System.Drawing.Point(12, 26)
        Me.txtAlt.MaxLength = 0
        Me.txtAlt.Name = "txtAlt"
        Me.txtAlt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAlt.Size = New System.Drawing.Size(50, 20)
        Me.txtAlt.TabIndex = 29
        Me.txtAlt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdAlt
        '
        Me.cmdAlt.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAlt.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAlt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAlt.Location = New System.Drawing.Point(68, 23)
        Me.cmdAlt.Name = "cmdAlt"
        Me.cmdAlt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAlt.Size = New System.Drawing.Size(35, 25)
        Me.cmdAlt.TabIndex = 28
        Me.cmdAlt.Text = "Set"
        Me.cmdAlt.UseVisualStyleBackColor = False
        '
        'boxProgressive
        '
        Me.boxProgressive.Controls.Add(Me.cmdReverse)
        Me.boxProgressive.Controls.Add(Me.cmdWidth12)
        Me.boxProgressive.Controls.Add(Me.txtWidth1)
        Me.boxProgressive.Controls.Add(Me.txtWidth2)
        Me.boxProgressive.Controls.Add(Me.Label1)
        Me.boxProgressive.Controls.Add(Me.Label2)
        Me.boxProgressive.Location = New System.Drawing.Point(212, 63)
        Me.boxProgressive.Name = "boxProgressive"
        Me.boxProgressive.Size = New System.Drawing.Size(250, 77)
        Me.boxProgressive.TabIndex = 38
        Me.boxProgressive.TabStop = False
        Me.boxProgressive.Text = "Progressive width"
        '
        'cmdReverse
        '
        Me.cmdReverse.BackColor = System.Drawing.SystemColors.Control
        Me.cmdReverse.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdReverse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReverse.Location = New System.Drawing.Point(138, 40)
        Me.cmdReverse.Name = "cmdReverse"
        Me.cmdReverse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdReverse.Size = New System.Drawing.Size(50, 25)
        Me.cmdReverse.TabIndex = 42
        Me.cmdReverse.Text = "Switch"
        Me.cmdReverse.UseVisualStyleBackColor = False
        '
        'cmdWidth12
        '
        Me.cmdWidth12.BackColor = System.Drawing.SystemColors.Control
        Me.cmdWidth12.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdWidth12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdWidth12.Location = New System.Drawing.Point(200, 40)
        Me.cmdWidth12.Name = "cmdWidth12"
        Me.cmdWidth12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdWidth12.Size = New System.Drawing.Size(35, 25)
        Me.cmdWidth12.TabIndex = 39
        Me.cmdWidth12.Text = "Set"
        Me.cmdWidth12.UseVisualStyleBackColor = False
        '
        'txtWidth1
        '
        Me.txtWidth1.AcceptsReturn = True
        Me.txtWidth1.BackColor = System.Drawing.SystemColors.Window
        Me.txtWidth1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWidth1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWidth1.Location = New System.Drawing.Point(10, 43)
        Me.txtWidth1.MaxLength = 0
        Me.txtWidth1.Name = "txtWidth1"
        Me.txtWidth1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWidth1.Size = New System.Drawing.Size(53, 20)
        Me.txtWidth1.TabIndex = 38
        Me.txtWidth1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtWidth2
        '
        Me.txtWidth2.AcceptsReturn = True
        Me.txtWidth2.BackColor = System.Drawing.SystemColors.Window
        Me.txtWidth2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWidth2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWidth2.Location = New System.Drawing.Point(73, 43)
        Me.txtWidth2.MaxLength = 0
        Me.txtWidth2.Name = "txtWidth2"
        Me.txtWidth2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWidth2.Size = New System.Drawing.Size(53, 20)
        Me.txtWidth2.TabIndex = 37
        Me.txtWidth2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(10, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 41
        Me.Label1.Text = "Start"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(70, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "End"
        '
        'boxWidth
        '
        Me.boxWidth.Controls.Add(Me.txtWidth)
        Me.boxWidth.Controls.Add(Me.cmdWidth)
        Me.boxWidth.Location = New System.Drawing.Point(212, 152)
        Me.boxWidth.Name = "boxWidth"
        Me.boxWidth.Size = New System.Drawing.Size(118, 68)
        Me.boxWidth.TabIndex = 37
        Me.boxWidth.TabStop = False
        Me.boxWidth.Text = "Width"
        '
        'txtWidth
        '
        Me.txtWidth.AcceptsReturn = True
        Me.txtWidth.BackColor = System.Drawing.SystemColors.Window
        Me.txtWidth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWidth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWidth.Location = New System.Drawing.Point(9, 29)
        Me.txtWidth.MaxLength = 0
        Me.txtWidth.Name = "txtWidth"
        Me.txtWidth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWidth.Size = New System.Drawing.Size(53, 20)
        Me.txtWidth.TabIndex = 29
        Me.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdWidth
        '
        Me.cmdWidth.BackColor = System.Drawing.SystemColors.Control
        Me.cmdWidth.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdWidth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdWidth.Location = New System.Drawing.Point(68, 26)
        Me.cmdWidth.Name = "cmdWidth"
        Me.cmdWidth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdWidth.Size = New System.Drawing.Size(35, 25)
        Me.cmdWidth.TabIndex = 28
        Me.cmdWidth.Text = "Set"
        Me.cmdWidth.UseVisualStyleBackColor = False
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(62, 22)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(201, 20)
        Me.txtName.TabIndex = 32
        '
        'labelName
        '
        Me.labelName.AutoSize = True
        Me.labelName.Location = New System.Drawing.Point(21, 25)
        Me.labelName.Name = "labelName"
        Me.labelName.Size = New System.Drawing.Size(35, 13)
        Me.labelName.TabIndex = 33
        Me.labelName.Text = "Name"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.labelDir)
        Me.TabPage2.Controls.Add(Me.cbLanes)
        Me.TabPage2.Controls.Add(Me.labelLanes)
        Me.TabPage2.Controls.Add(Me.cbDir)
        Me.TabPage2.Controls.Add(Me.cmdDetail)
        Me.TabPage2.Controls.Add(Me.labelVectorTexture)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.listVector)
        Me.TabPage2.Controls.Add(Me.imgVector)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(480, 241)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Vector Lines"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'labelDir
        '
        Me.labelDir.AutoSize = True
        Me.labelDir.Location = New System.Drawing.Point(320, 18)
        Me.labelDir.Name = "labelDir"
        Me.labelDir.Size = New System.Drawing.Size(49, 13)
        Me.labelDir.TabIndex = 19
        Me.labelDir.Text = "Direction"
        '
        'cbLanes
        '
        Me.cbLanes.FormattingEnabled = True
        Me.cbLanes.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9"})
        Me.cbLanes.Location = New System.Drawing.Point(273, 34)
        Me.cbLanes.Name = "cbLanes"
        Me.cbLanes.Size = New System.Drawing.Size(44, 21)
        Me.cbLanes.TabIndex = 20
        Me.cbLanes.Text = "2"
        '
        'labelLanes
        '
        Me.labelLanes.Location = New System.Drawing.Point(235, 30)
        Me.labelLanes.Name = "labelLanes"
        Me.labelLanes.Size = New System.Drawing.Size(41, 27)
        Me.labelLanes.TabIndex = 18
        Me.labelLanes.Text = "Traffic Lanes"
        '
        'cbDir
        '
        Me.cbDir.FormattingEnabled = True
        Me.cbDir.Items.AddRange(New Object() {"F", "T"})
        Me.cbDir.Location = New System.Drawing.Point(323, 34)
        Me.cbDir.Name = "cbDir"
        Me.cbDir.Size = New System.Drawing.Size(46, 21)
        Me.cbDir.TabIndex = 17
        Me.cbDir.Text = "F"
        '
        'cmdDetail
        '
        Me.cmdDetail.Location = New System.Drawing.Point(414, 30)
        Me.cmdDetail.Name = "cmdDetail"
        Me.cmdDetail.Size = New System.Drawing.Size(43, 25)
        Me.cmdDetail.TabIndex = 34
        Me.cmdDetail.Text = "Info"
        Me.cmdDetail.UseVisualStyleBackColor = True
        '
        'labelVectorTexture
        '
        Me.labelVectorTexture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.labelVectorTexture.Location = New System.Drawing.Point(377, 33)
        Me.labelVectorTexture.Name = "labelVectorTexture"
        Me.labelVectorTexture.Size = New System.Drawing.Size(31, 21)
        Me.labelVectorTexture.TabIndex = 33
        Me.labelVectorTexture.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(235, 64)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(192, 13)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Right click to copy GUID into Clipboard"
        '
        'listVector
        '
        Me.listVector.BackColor = System.Drawing.SystemColors.Window
        Me.listVector.Cursor = System.Windows.Forms.Cursors.Default
        Me.listVector.ForeColor = System.Drawing.SystemColors.WindowText
        Me.listVector.Location = New System.Drawing.Point(234, 80)
        Me.listVector.Name = "listVector"
        Me.listVector.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.listVector.Size = New System.Drawing.Size(223, 147)
        Me.listVector.TabIndex = 27
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.boxTexType)
        Me.TabPage3.Controls.Add(Me.imgTex)
        Me.TabPage3.Controls.Add(Me.boxTexTexture)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(480, 241)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Textured Lines"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'boxTexType
        '
        Me.boxTexType.Controls.Add(Me.txtV1)
        Me.boxTexType.Controls.Add(Me.txtTexPri)
        Me.boxTexType.Controls.Add(Me.cmbComplex)
        Me.boxTexType.Controls.Add(Me.Label3)
        Me.boxTexType.Controls.Add(Me.optStanding)
        Me.boxTexType.Controls.Add(Me.optLying)
        Me.boxTexType.Controls.Add(Me.lbTexPri)
        Me.boxTexType.Controls.Add(Me.LbV1)
        Me.boxTexType.Location = New System.Drawing.Point(228, 140)
        Me.boxTexType.Name = "boxTexType"
        Me.boxTexType.Size = New System.Drawing.Size(234, 86)
        Me.boxTexType.TabIndex = 99
        Me.boxTexType.TabStop = False
        Me.boxTexType.Text = "Type"
        '
        'txtV1
        '
        Me.txtV1.AcceptsReturn = True
        Me.txtV1.BackColor = System.Drawing.SystemColors.Window
        Me.txtV1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtV1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtV1.Location = New System.Drawing.Point(124, 22)
        Me.txtV1.MaxLength = 0
        Me.txtV1.Name = "txtV1"
        Me.txtV1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtV1.Size = New System.Drawing.Size(39, 20)
        Me.txtV1.TabIndex = 94
        Me.txtV1.Text = "25000"
        Me.txtV1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtTexPri
        '
        Me.txtTexPri.AcceptsReturn = True
        Me.txtTexPri.BackColor = System.Drawing.SystemColors.Window
        Me.txtTexPri.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTexPri.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTexPri.Location = New System.Drawing.Point(190, 21)
        Me.txtTexPri.MaxLength = 0
        Me.txtTexPri.Name = "txtTexPri"
        Me.txtTexPri.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTexPri.Size = New System.Drawing.Size(25, 20)
        Me.txtTexPri.TabIndex = 91
        Me.txtTexPri.Text = "4"
        Me.txtTexPri.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmbComplex
        '
        Me.cmbComplex.FormattingEnabled = True
        Me.cmbComplex.Items.AddRange(New Object() {"Very Sparse", "Sparse", "Normal", "Dense", "Very Dense", "Extra Dense"})
        Me.cmbComplex.Location = New System.Drawing.Point(124, 52)
        Me.cmbComplex.Name = "cmbComplex"
        Me.cmbComplex.Size = New System.Drawing.Size(91, 21)
        Me.cmbComplex.TabIndex = 107
        Me.cmbComplex.Text = "Normal"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(88, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 106
        Me.Label3.Text = "Compl."
        '
        'optStanding
        '
        Me.optStanding.AutoSize = True
        Me.optStanding.Location = New System.Drawing.Point(15, 54)
        Me.optStanding.Name = "optStanding"
        Me.optStanding.Size = New System.Drawing.Size(67, 17)
        Me.optStanding.TabIndex = 97
        Me.optStanding.Text = "Standing"
        Me.optStanding.UseVisualStyleBackColor = True
        '
        'optLying
        '
        Me.optLying.AutoSize = True
        Me.optLying.Checked = True
        Me.optLying.Location = New System.Drawing.Point(15, 24)
        Me.optLying.Name = "optLying"
        Me.optLying.Size = New System.Drawing.Size(50, 17)
        Me.optLying.TabIndex = 96
        Me.optLying.TabStop = True
        Me.optLying.Text = "Lying"
        Me.optLying.UseVisualStyleBackColor = True
        '
        'lbTexPri
        '
        Me.lbTexPri.AutoSize = True
        Me.lbTexPri.BackColor = System.Drawing.Color.Transparent
        Me.lbTexPri.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbTexPri.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbTexPri.Location = New System.Drawing.Point(171, 27)
        Me.lbTexPri.Name = "lbTexPri"
        Me.lbTexPri.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbTexPri.Size = New System.Drawing.Size(22, 13)
        Me.lbTexPri.TabIndex = 93
        Me.lbTexPri.Text = "Pri."
        '
        'LbV1
        '
        Me.LbV1.AutoSize = True
        Me.LbV1.BackColor = System.Drawing.Color.Transparent
        Me.LbV1.Cursor = System.Windows.Forms.Cursors.Default
        Me.LbV1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LbV1.Location = New System.Drawing.Point(101, 27)
        Me.LbV1.Name = "LbV1"
        Me.LbV1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LbV1.Size = New System.Drawing.Size(24, 13)
        Me.LbV1.TabIndex = 95
        Me.LbV1.Text = "Vis."
        Me.LbV1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'imgTex
        '
        Me.imgTex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imgTex.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgTex.ErrorImage = Nothing
        Me.imgTex.InitialImage = Nothing
        Me.imgTex.Location = New System.Drawing.Point(18, 22)
        Me.imgTex.Name = "imgTex"
        Me.imgTex.Size = New System.Drawing.Size(444, 111)
        Me.imgTex.TabIndex = 8
        Me.imgTex.TabStop = False
        '
        'boxTexTexture
        '
        Me.boxTexTexture.Controls.Add(Me.optTile)
        Me.boxTexTexture.Controls.Add(Me.optStretch)
        Me.boxTexTexture.Controls.Add(Me.cmdTex)
        Me.boxTexTexture.Controls.Add(Me.txtTexName)
        Me.boxTexTexture.Controls.Add(Me.ckNight)
        Me.boxTexTexture.Controls.Add(Me.optFull)
        Me.boxTexTexture.Location = New System.Drawing.Point(18, 139)
        Me.boxTexTexture.Name = "boxTexTexture"
        Me.boxTexTexture.Size = New System.Drawing.Size(194, 87)
        Me.boxTexTexture.TabIndex = 98
        Me.boxTexTexture.TabStop = False
        Me.boxTexTexture.Text = "Texture"
        '
        'optTile
        '
        Me.optTile.AutoSize = True
        Me.optTile.Checked = True
        Me.optTile.Location = New System.Drawing.Point(12, 18)
        Me.optTile.Name = "optTile"
        Me.optTile.Size = New System.Drawing.Size(42, 17)
        Me.optTile.TabIndex = 98
        Me.optTile.TabStop = True
        Me.optTile.Text = "Tile"
        Me.optTile.UseVisualStyleBackColor = True
        '
        'optStretch
        '
        Me.optStretch.AutoSize = True
        Me.optStretch.Location = New System.Drawing.Point(12, 64)
        Me.optStretch.Name = "optStretch"
        Me.optStretch.Size = New System.Drawing.Size(59, 17)
        Me.optStretch.TabIndex = 97
        Me.optStretch.Text = "Stretch"
        Me.optStretch.UseVisualStyleBackColor = True
        '
        'cmdTex
        '
        Me.cmdTex.BackColor = System.Drawing.SystemColors.Control
        Me.cmdTex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.cmdTex.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTex.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTex.Location = New System.Drawing.Point(151, 16)
        Me.cmdTex.Name = "cmdTex"
        Me.cmdTex.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTex.Size = New System.Drawing.Size(32, 25)
        Me.cmdTex.TabIndex = 92
        Me.cmdTex.Text = "..."
        Me.cmdTex.UseVisualStyleBackColor = False
        '
        'txtTexName
        '
        Me.txtTexName.AcceptsReturn = True
        Me.txtTexName.BackColor = System.Drawing.SystemColors.Window
        Me.txtTexName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTexName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTexName.Location = New System.Drawing.Point(60, 18)
        Me.txtTexName.MaxLength = 0
        Me.txtTexName.Name = "txtTexName"
        Me.txtTexName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTexName.Size = New System.Drawing.Size(85, 20)
        Me.txtTexName.TabIndex = 90
        Me.txtTexName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ckNight
        '
        Me.ckNight.BackColor = System.Drawing.Color.Transparent
        Me.ckNight.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ckNight.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckNight.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckNight.Location = New System.Drawing.Point(125, 54)
        Me.ckNight.Name = "ckNight"
        Me.ckNight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckNight.Size = New System.Drawing.Size(58, 21)
        Me.ckNight.TabIndex = 96
        Me.ckNight.Text = "Night"
        Me.ckNight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ckNight.UseVisualStyleBackColor = False
        '
        'optFull
        '
        Me.optFull.AutoSize = True
        Me.optFull.Location = New System.Drawing.Point(12, 41)
        Me.optFull.Name = "optFull"
        Me.optFull.Size = New System.Drawing.Size(89, 17)
        Me.optFull.TabIndex = 97
        Me.optFull.Text = "Complete Tile"
        Me.optFull.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.cmdExtrusionProperties)
        Me.TabPage4.Controls.Add(Me.labelProfile)
        Me.TabPage4.Controls.Add(Me.lbguid)
        Me.TabPage4.Controls.Add(Me.lbExt1)
        Me.TabPage4.Controls.Add(Me.listExtrusion)
        Me.TabPage4.Controls.Add(Me.imgExtrusion)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(480, 241)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Extrusion Bridges"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'cmdExtrusionProperties
        '
        Me.cmdExtrusionProperties.Location = New System.Drawing.Point(235, 29)
        Me.cmdExtrusionProperties.Name = "cmdExtrusionProperties"
        Me.cmdExtrusionProperties.Size = New System.Drawing.Size(68, 25)
        Me.cmdExtrusionProperties.TabIndex = 96
        Me.cmdExtrusionProperties.Text = "Properties"
        Me.cmdExtrusionProperties.UseVisualStyleBackColor = True
        '
        'labelProfile
        '
        Me.labelProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.labelProfile.Location = New System.Drawing.Point(235, 73)
        Me.labelProfile.Name = "labelProfile"
        Me.labelProfile.Size = New System.Drawing.Size(223, 19)
        Me.labelProfile.TabIndex = 95
        Me.labelProfile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbguid
        '
        Me.lbguid.AutoSize = True
        Me.lbguid.Location = New System.Drawing.Point(355, 60)
        Me.lbguid.Name = "lbguid"
        Me.lbguid.Size = New System.Drawing.Size(107, 13)
        Me.lbguid.TabIndex = 35
        Me.lbguid.Text = "Extrusion Profile Guid"
        '
        'lbExt1
        '
        Me.lbExt1.AutoSize = True
        Me.lbExt1.Location = New System.Drawing.Point(12, 16)
        Me.lbExt1.Name = "lbExt1"
        Me.lbExt1.Size = New System.Drawing.Size(99, 13)
        Me.lbExt1.TabIndex = 35
        Me.lbExt1.Text = "Click to see a detail"
        '
        'listExtrusion
        '
        Me.listExtrusion.BackColor = System.Drawing.SystemColors.Window
        Me.listExtrusion.Cursor = System.Windows.Forms.Cursors.Default
        Me.listExtrusion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.listExtrusion.Location = New System.Drawing.Point(235, 95)
        Me.listExtrusion.Name = "listExtrusion"
        Me.listExtrusion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.listExtrusion.Size = New System.Drawing.Size(223, 134)
        Me.listExtrusion.TabIndex = 34
        '
        'imgExtrusion
        '
        Me.imgExtrusion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imgExtrusion.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgExtrusion.Location = New System.Drawing.Point(15, 29)
        Me.imgExtrusion.Name = "imgExtrusion"
        Me.imgExtrusion.Size = New System.Drawing.Size(200, 200)
        Me.imgExtrusion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgExtrusion.TabIndex = 8
        Me.imgExtrusion.TabStop = False
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.boxHeading)
        Me.TabPage5.Controls.Add(Me.cmbComplexity)
        Me.TabPage5.Controls.Add(Me.labelComplexity)
        Me.TabPage5.Controls.Add(Me.labelCat)
        Me.TabPage5.Controls.Add(Me.imgLib)
        Me.TabPage5.Controls.Add(Me.lstLib)
        Me.TabPage5.Controls.Add(Me.labelLibID)
        Me.TabPage5.Controls.Add(Me.cmbLibCat)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(480, 241)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Line of Objects"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'boxHeading
        '
        Me.boxHeading.Controls.Add(Me.ckRandom)
        Me.boxHeading.Controls.Add(Me.txtHeading)
        Me.boxHeading.Controls.Add(Me.cmdHeading)
        Me.boxHeading.Location = New System.Drawing.Point(235, 17)
        Me.boxHeading.Name = "boxHeading"
        Me.boxHeading.Size = New System.Drawing.Size(224, 51)
        Me.boxHeading.TabIndex = 106
        Me.boxHeading.TabStop = False
        Me.boxHeading.Text = "Heading"
        '
        'ckRandom
        '
        Me.ckRandom.AutoSize = True
        Me.ckRandom.Location = New System.Drawing.Point(16, 21)
        Me.ckRandom.Name = "ckRandom"
        Me.ckRandom.Size = New System.Drawing.Size(66, 17)
        Me.ckRandom.TabIndex = 2
        Me.ckRandom.Text = "Random"
        Me.ckRandom.UseVisualStyleBackColor = True
        '
        'txtHeading
        '
        Me.txtHeading.Location = New System.Drawing.Point(102, 18)
        Me.txtHeading.Name = "txtHeading"
        Me.txtHeading.Size = New System.Drawing.Size(51, 20)
        Me.txtHeading.TabIndex = 1
        Me.txtHeading.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdHeading
        '
        Me.cmdHeading.Location = New System.Drawing.Point(166, 16)
        Me.cmdHeading.Name = "cmdHeading"
        Me.cmdHeading.Size = New System.Drawing.Size(40, 23)
        Me.cmdHeading.TabIndex = 0
        Me.cmdHeading.Text = "Set"
        Me.cmdHeading.UseVisualStyleBackColor = True
        '
        'cmbComplexity
        '
        Me.cmbComplexity.FormattingEnabled = True
        Me.cmbComplexity.Items.AddRange(New Object() {"Very Sparse", "Sparse", "Normal", "Dense", "Very Dense", "Extra Dense"})
        Me.cmbComplexity.Location = New System.Drawing.Point(235, 91)
        Me.cmbComplexity.Name = "cmbComplexity"
        Me.cmbComplexity.Size = New System.Drawing.Size(75, 21)
        Me.cmbComplexity.TabIndex = 105
        Me.cmbComplexity.Text = "Normal"
        '
        'labelComplexity
        '
        Me.labelComplexity.AutoSize = True
        Me.labelComplexity.BackColor = System.Drawing.Color.Transparent
        Me.labelComplexity.Cursor = System.Windows.Forms.Cursors.Default
        Me.labelComplexity.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labelComplexity.Location = New System.Drawing.Point(232, 75)
        Me.labelComplexity.Name = "labelComplexity"
        Me.labelComplexity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labelComplexity.Size = New System.Drawing.Size(57, 13)
        Me.labelComplexity.TabIndex = 103
        Me.labelComplexity.Text = "Complexity"
        '
        'labelCat
        '
        Me.labelCat.AutoSize = True
        Me.labelCat.BackColor = System.Drawing.Color.Transparent
        Me.labelCat.Cursor = System.Windows.Forms.Cursors.Default
        Me.labelCat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labelCat.Location = New System.Drawing.Point(320, 75)
        Me.labelCat.Name = "labelCat"
        Me.labelCat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labelCat.Size = New System.Drawing.Size(83, 13)
        Me.labelCat.TabIndex = 103
        Me.labelCat.Text = "Object Category"
        '
        'imgLib
        '
        Me.imgLib.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imgLib.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgLib.Location = New System.Drawing.Point(15, 29)
        Me.imgLib.Name = "imgLib"
        Me.imgLib.Size = New System.Drawing.Size(200, 200)
        Me.imgLib.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgLib.TabIndex = 9
        Me.imgLib.TabStop = False
        '
        'lstLib
        '
        Me.lstLib.BackColor = System.Drawing.SystemColors.Window
        Me.lstLib.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstLib.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstLib.Location = New System.Drawing.Point(235, 134)
        Me.lstLib.Name = "lstLib"
        Me.lstLib.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstLib.Size = New System.Drawing.Size(224, 95)
        Me.lstLib.TabIndex = 100
        '
        'labelLibID
        '
        Me.labelLibID.BackColor = System.Drawing.Color.Transparent
        Me.labelLibID.Cursor = System.Windows.Forms.Cursors.Default
        Me.labelLibID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labelLibID.Location = New System.Drawing.Point(235, 115)
        Me.labelLibID.Name = "labelLibID"
        Me.labelLibID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labelLibID.Size = New System.Drawing.Size(227, 16)
        Me.labelLibID.TabIndex = 104
        Me.labelLibID.Text = "GUID"
        Me.labelLibID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbLibCat
        '
        Me.cmbLibCat.BackColor = System.Drawing.SystemColors.Window
        Me.cmbLibCat.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbLibCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLibCat.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbLibCat.Location = New System.Drawing.Point(323, 91)
        Me.cmbLibCat.Name = "cmbLibCat"
        Me.cmbLibCat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbLibCat.Size = New System.Drawing.Size(136, 21)
        Me.cmbLibCat.TabIndex = 102
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(348, 295)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(66, 25)
        Me.cmdCancel.TabIndex = 26
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(430, 295)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(66, 25)
        Me.cmdOK.TabIndex = 25
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdSample
        '
        Me.cmdSample.Location = New System.Drawing.Point(125, 295)
        Me.cmdSample.Name = "cmdSample"
        Me.cmdSample.Size = New System.Drawing.Size(62, 25)
        Me.cmdSample.TabIndex = 38
        Me.cmdSample.Text = "Sample"
        Me.cmdSample.UseVisualStyleBackColor = True
        '
        'cmdSmooth
        '
        Me.cmdSmooth.Location = New System.Drawing.Point(46, 295)
        Me.cmdSmooth.Name = "cmdSmooth"
        Me.cmdSmooth.Size = New System.Drawing.Size(62, 25)
        Me.cmdSmooth.TabIndex = 37
        Me.cmdSmooth.Text = "Smooth"
        Me.cmdSmooth.UseVisualStyleBackColor = True
        '
        'FrmLinesP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(515, 334)
        Me.Controls.Add(Me.cmdSample)
        Me.Controls.Add(Me.cmdSmooth)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmLinesP"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - Line Properties"
        CType(Me.imgVector, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.boxType.ResumeLayout(False)
        Me.boxType.PerformLayout()
        Me.boxAltitude.ResumeLayout(False)
        Me.boxAltitude.PerformLayout()
        Me.boxProgressive.ResumeLayout(False)
        Me.boxProgressive.PerformLayout()
        Me.boxWidth.ResumeLayout(False)
        Me.boxWidth.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.boxTexType.ResumeLayout(False)
        Me.boxTexType.PerformLayout()
        CType(Me.imgTex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.boxTexTexture.ResumeLayout(False)
        Me.boxTexTexture.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        CType(Me.imgExtrusion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        Me.boxHeading.ResumeLayout(False)
        Me.boxHeading.PerformLayout()
        CType(Me.imgLib, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents imgVector As System.Windows.Forms.PictureBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents cmdDetail As System.Windows.Forms.Button
    Friend WithEvents labelVectorTexture As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbLanes As System.Windows.Forms.ComboBox
    Friend WithEvents labelDir As System.Windows.Forms.Label
    Friend WithEvents labelLanes As System.Windows.Forms.Label
    Friend WithEvents cbDir As System.Windows.Forms.ComboBox
    Public WithEvents listVector As System.Windows.Forms.ListBox
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdSample As System.Windows.Forms.Button
    Friend WithEvents cmdSmooth As System.Windows.Forms.Button
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents labelName As System.Windows.Forms.Label
    Friend WithEvents cmdWinding As System.Windows.Forms.Button
    Friend WithEvents boxProgressive As System.Windows.Forms.GroupBox
    Public WithEvents cmdReverse As System.Windows.Forms.Button
    Public WithEvents cmdWidth12 As System.Windows.Forms.Button
    Public WithEvents txtWidth1 As System.Windows.Forms.TextBox
    Public WithEvents txtWidth2 As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents boxWidth As System.Windows.Forms.GroupBox
    Public WithEvents txtWidth As System.Windows.Forms.TextBox
    Public WithEvents cmdWidth As System.Windows.Forms.Button
    Friend WithEvents boxAltitude As System.Windows.Forms.GroupBox
    Public WithEvents txtAlt As System.Windows.Forms.TextBox
    Public WithEvents cmdAlt As System.Windows.Forms.Button
    Friend WithEvents boxType As System.Windows.Forms.GroupBox
    Friend WithEvents cmdType As System.Windows.Forms.Button
    Friend WithEvents optExtrusion As System.Windows.Forms.RadioButton
    Friend WithEvents optTexture As System.Windows.Forms.RadioButton
    Friend WithEvents optVector As System.Windows.Forms.RadioButton
    Friend WithEvents labelReverse As System.Windows.Forms.Label
    Public WithEvents imgExtrusion As System.Windows.Forms.PictureBox
    Public WithEvents imgTex As System.Windows.Forms.PictureBox
    Public WithEvents lbTexPri As System.Windows.Forms.Label
    Public WithEvents LbV1 As System.Windows.Forms.Label
    Public WithEvents txtTexName As System.Windows.Forms.TextBox
    Public WithEvents txtTexPri As System.Windows.Forms.TextBox
    Public WithEvents cmdTex As System.Windows.Forms.Button
    Public WithEvents txtV1 As System.Windows.Forms.TextBox
    Public WithEvents ckNight As System.Windows.Forms.CheckBox
    Friend WithEvents lbExt1 As System.Windows.Forms.Label
    Public WithEvents listExtrusion As System.Windows.Forms.ListBox
    Friend WithEvents lbguid As System.Windows.Forms.Label
    Friend WithEvents labelProfile As System.Windows.Forms.Label
    Friend WithEvents cmdExtrusionProperties As System.Windows.Forms.Button
    Friend WithEvents optObjects As System.Windows.Forms.RadioButton
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Public WithEvents imgLib As System.Windows.Forms.PictureBox
    Public WithEvents labelCat As System.Windows.Forms.Label
    Public WithEvents lstLib As System.Windows.Forms.ListBox
    Public WithEvents labelLibID As System.Windows.Forms.Label
    Public WithEvents cmbLibCat As System.Windows.Forms.ComboBox
    Friend WithEvents cmbComplexity As System.Windows.Forms.ComboBox
    Friend WithEvents boxHeading As System.Windows.Forms.GroupBox
    Friend WithEvents txtHeading As System.Windows.Forms.TextBox
    Friend WithEvents cmdHeading As System.Windows.Forms.Button
    Public WithEvents labelComplexity As System.Windows.Forms.Label
    Friend WithEvents ckThisColor As System.Windows.Forms.CheckBox
    Friend WithEvents cmdColor As System.Windows.Forms.Button
    Friend WithEvents ckRandom As System.Windows.Forms.CheckBox
    Friend WithEvents boxTexTexture As System.Windows.Forms.GroupBox
    Friend WithEvents optTile As System.Windows.Forms.RadioButton
    Friend WithEvents optStretch As System.Windows.Forms.RadioButton
    Friend WithEvents boxTexType As System.Windows.Forms.GroupBox
    Friend WithEvents optLying As System.Windows.Forms.RadioButton
    Friend WithEvents optStanding As System.Windows.Forms.RadioButton
    Friend WithEvents cmbComplex As System.Windows.Forms.ComboBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents optFull As System.Windows.Forms.RadioButton

End Class
