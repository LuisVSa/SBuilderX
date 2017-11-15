<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPolysP
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPolysP))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.BoxOrder = New System.Windows.Forms.GroupBox()
        Me.cmdBottom = New System.Windows.Forms.Button()
        Me.cmdTop = New System.Windows.Forms.Button()
        Me.cmdDown = New System.Windows.Forms.Button()
        Me.cmdUP = New System.Windows.Forms.Button()
        Me.lbOrder = New System.Windows.Forms.Label()
        Me.boxType = New System.Windows.Forms.GroupBox()
        Me.cmdType = New System.Windows.Forms.Button()
        Me.optTexture = New System.Windows.Forms.RadioButton()
        Me.optVector = New System.Windows.Forms.RadioButton()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lbName = New System.Windows.Forms.Label()
        Me.boxAltitude = New System.Windows.Forms.GroupBox()
        Me.txtAlt = New System.Windows.Forms.TextBox()
        Me.cmdAlt = New System.Windows.Forms.Button()
        Me.boxSlope = New System.Windows.Forms.GroupBox()
        Me.txtHead = New System.Windows.Forms.TextBox()
        Me.txtAlt0 = New System.Windows.Forms.TextBox()
        Me.cmdHelpSlope = New System.Windows.Forms.Button()
        Me.cmdSlope = New System.Windows.Forms.Button()
        Me.txtSlope = New System.Windows.Forms.TextBox()
        Me.lbSY = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lbSX = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPt0 = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.lbTexture = New System.Windows.Forms.Label()
        Me.cmdColor = New System.Windows.Forms.Button()
        Me.ckThisColor = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbExclude = New System.Windows.Forms.Label()
        Me.lbExcludeCaption = New System.Windows.Forms.Label()
        Me.cmdDetail = New System.Windows.Forms.Button()
        Me.imgTexture = New System.Windows.Forms.PictureBox()
        Me.List1 = New System.Windows.Forms.ListBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.lbPolyColor = New System.Windows.Forms.Label()
        Me.cmdColor2 = New System.Windows.Forms.Button()
        Me.boxTiling = New System.Windows.Forms.GroupBox()
        Me.lbTex4 = New System.Windows.Forms.Label()
        Me.txtTexTileX = New System.Windows.Forms.TextBox()
        Me.lbTex5 = New System.Windows.Forms.Label()
        Me.txtTexTileY = New System.Windows.Forms.TextBox()
        Me.lbTexName = New System.Windows.Forms.Label()
        Me.imgTex = New System.Windows.Forms.PictureBox()
        Me.lbTex1 = New System.Windows.Forms.Label()
        Me.lbTexPri = New System.Windows.Forms.Label()
        Me.LbV1 = New System.Windows.Forms.Label()
        Me.txtTexName = New System.Windows.Forms.TextBox()
        Me.txtTexPri = New System.Windows.Forms.TextBox()
        Me.cmdTex = New System.Windows.Forms.Button()
        Me.txtV1 = New System.Windows.Forms.TextBox()
        Me.ckNight = New System.Windows.Forms.CheckBox()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdSmooth = New System.Windows.Forms.Button()
        Me.cmdSample = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.BoxOrder.SuspendLayout()
        Me.boxType.SuspendLayout()
        Me.boxAltitude.SuspendLayout()
        Me.boxSlope.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.imgTexture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.boxTiling.SuspendLayout()
        CType(Me.imgTex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(17, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(469, 272)
        Me.TabControl1.TabIndex = 34
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.BoxOrder)
        Me.TabPage1.Controls.Add(Me.boxType)
        Me.TabPage1.Controls.Add(Me.txtName)
        Me.TabPage1.Controls.Add(Me.lbName)
        Me.TabPage1.Controls.Add(Me.boxAltitude)
        Me.TabPage1.Controls.Add(Me.boxSlope)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(461, 246)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'BoxOrder
        '
        Me.BoxOrder.Controls.Add(Me.cmdBottom)
        Me.BoxOrder.Controls.Add(Me.cmdTop)
        Me.BoxOrder.Controls.Add(Me.cmdDown)
        Me.BoxOrder.Controls.Add(Me.cmdUP)
        Me.BoxOrder.Controls.Add(Me.lbOrder)
        Me.BoxOrder.Location = New System.Drawing.Point(18, 66)
        Me.BoxOrder.Name = "BoxOrder"
        Me.BoxOrder.Size = New System.Drawing.Size(171, 78)
        Me.BoxOrder.TabIndex = 66
        Me.BoxOrder.TabStop = False
        Me.BoxOrder.Text = "Drawing order"
        '
        'cmdBottom
        '
        Me.cmdBottom.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBottom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBottom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBottom.Image = CType(resources.GetObject("cmdBottom.Image"), System.Drawing.Image)
        Me.cmdBottom.Location = New System.Drawing.Point(136, 41)
        Me.cmdBottom.Name = "cmdBottom"
        Me.cmdBottom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBottom.Size = New System.Drawing.Size(25, 25)
        Me.cmdBottom.TabIndex = 18
        Me.cmdBottom.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdBottom.UseVisualStyleBackColor = False
        '
        'cmdTop
        '
        Me.cmdTop.BackColor = System.Drawing.SystemColors.Control
        Me.cmdTop.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTop.Image = CType(resources.GetObject("cmdTop.Image"), System.Drawing.Image)
        Me.cmdTop.Location = New System.Drawing.Point(96, 41)
        Me.cmdTop.Name = "cmdTop"
        Me.cmdTop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTop.Size = New System.Drawing.Size(25, 25)
        Me.cmdTop.TabIndex = 17
        Me.cmdTop.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdTop.UseVisualStyleBackColor = False
        '
        'cmdDown
        '
        Me.cmdDown.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDown.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDown.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDown.Image = CType(resources.GetObject("cmdDown.Image"), System.Drawing.Image)
        Me.cmdDown.Location = New System.Drawing.Point(57, 41)
        Me.cmdDown.Name = "cmdDown"
        Me.cmdDown.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDown.Size = New System.Drawing.Size(25, 25)
        Me.cmdDown.TabIndex = 16
        Me.cmdDown.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdDown.UseVisualStyleBackColor = False
        '
        'cmdUP
        '
        Me.cmdUP.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUP.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUP.Image = CType(resources.GetObject("cmdUP.Image"), System.Drawing.Image)
        Me.cmdUP.Location = New System.Drawing.Point(18, 41)
        Me.cmdUP.Name = "cmdUP"
        Me.cmdUP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUP.Size = New System.Drawing.Size(25, 25)
        Me.cmdUP.TabIndex = 2
        Me.cmdUP.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdUP.UseVisualStyleBackColor = False
        '
        'lbOrder
        '
        Me.lbOrder.AutoSize = True
        Me.lbOrder.BackColor = System.Drawing.Color.Transparent
        Me.lbOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbOrder.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbOrder.Location = New System.Drawing.Point(42, 19)
        Me.lbOrder.Name = "lbOrder"
        Me.lbOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbOrder.Size = New System.Drawing.Size(41, 13)
        Me.lbOrder.TabIndex = 34
        Me.lbOrder.Text = "lbOrder"
        '
        'boxType
        '
        Me.boxType.Controls.Add(Me.cmdType)
        Me.boxType.Controls.Add(Me.optTexture)
        Me.boxType.Controls.Add(Me.optVector)
        Me.boxType.Location = New System.Drawing.Point(18, 160)
        Me.boxType.Name = "boxType"
        Me.boxType.Size = New System.Drawing.Size(171, 73)
        Me.boxType.TabIndex = 64
        Me.boxType.TabStop = False
        Me.boxType.Text = "Polygon type"
        '
        'cmdType
        '
        Me.cmdType.Location = New System.Drawing.Point(112, 25)
        Me.cmdType.Name = "cmdType"
        Me.cmdType.Size = New System.Drawing.Size(47, 24)
        Me.cmdType.TabIndex = 2
        Me.cmdType.Text = ">>>"
        Me.cmdType.UseVisualStyleBackColor = True
        '
        'optTexture
        '
        Me.optTexture.AutoSize = True
        Me.optTexture.Location = New System.Drawing.Point(17, 43)
        Me.optTexture.Name = "optTexture"
        Me.optTexture.Size = New System.Drawing.Size(90, 17)
        Me.optTexture.TabIndex = 1
        Me.optTexture.TabStop = True
        Me.optTexture.Text = "Textured Poly"
        Me.optTexture.UseVisualStyleBackColor = True
        '
        'optVector
        '
        Me.optVector.AutoSize = True
        Me.optVector.Location = New System.Drawing.Point(17, 18)
        Me.optVector.Name = "optVector"
        Me.optVector.Size = New System.Drawing.Size(79, 17)
        Me.optVector.TabIndex = 0
        Me.optVector.TabStop = True
        Me.optVector.Text = "Vector Poly"
        Me.optVector.UseVisualStyleBackColor = True
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(18, 30)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(232, 20)
        Me.txtName.TabIndex = 61
        '
        'lbName
        '
        Me.lbName.AutoSize = True
        Me.lbName.Location = New System.Drawing.Point(15, 14)
        Me.lbName.Name = "lbName"
        Me.lbName.Size = New System.Drawing.Size(35, 13)
        Me.lbName.TabIndex = 62
        Me.lbName.Text = "Name"
        Me.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'boxAltitude
        '
        Me.boxAltitude.Controls.Add(Me.txtAlt)
        Me.boxAltitude.Controls.Add(Me.cmdAlt)
        Me.boxAltitude.Location = New System.Drawing.Point(271, 14)
        Me.boxAltitude.Name = "boxAltitude"
        Me.boxAltitude.Size = New System.Drawing.Size(171, 58)
        Me.boxAltitude.TabIndex = 57
        Me.boxAltitude.TabStop = False
        Me.boxAltitude.Text = "Constant altitude"
        '
        'txtAlt
        '
        Me.txtAlt.BackColor = System.Drawing.SystemColors.Window
        Me.txtAlt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAlt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAlt.Location = New System.Drawing.Point(16, 24)
        Me.txtAlt.MaxLength = 0
        Me.txtAlt.Name = "txtAlt"
        Me.txtAlt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAlt.Size = New System.Drawing.Size(70, 20)
        Me.txtAlt.TabIndex = 29
        Me.txtAlt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdAlt
        '
        Me.cmdAlt.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAlt.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAlt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAlt.Location = New System.Drawing.Point(110, 19)
        Me.cmdAlt.Name = "cmdAlt"
        Me.cmdAlt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAlt.Size = New System.Drawing.Size(44, 25)
        Me.cmdAlt.TabIndex = 28
        Me.cmdAlt.Text = "Set"
        Me.cmdAlt.UseVisualStyleBackColor = False
        '
        'boxSlope
        '
        Me.boxSlope.Controls.Add(Me.txtHead)
        Me.boxSlope.Controls.Add(Me.txtAlt0)
        Me.boxSlope.Controls.Add(Me.cmdHelpSlope)
        Me.boxSlope.Controls.Add(Me.cmdSlope)
        Me.boxSlope.Controls.Add(Me.txtSlope)
        Me.boxSlope.Controls.Add(Me.lbSY)
        Me.boxSlope.Controls.Add(Me.Label8)
        Me.boxSlope.Controls.Add(Me.lbSX)
        Me.boxSlope.Controls.Add(Me.Label6)
        Me.boxSlope.Controls.Add(Me.Label4)
        Me.boxSlope.Controls.Add(Me.Label5)
        Me.boxSlope.Controls.Add(Me.txtPt0)
        Me.boxSlope.Location = New System.Drawing.Point(209, 88)
        Me.boxSlope.Name = "boxSlope"
        Me.boxSlope.Size = New System.Drawing.Size(233, 145)
        Me.boxSlope.TabIndex = 56
        Me.boxSlope.TabStop = False
        Me.boxSlope.Text = "Variable Altitude"
        '
        'txtHead
        '
        Me.txtHead.BackColor = System.Drawing.SystemColors.Window
        Me.txtHead.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHead.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtHead.Location = New System.Drawing.Point(77, 62)
        Me.txtHead.MaxLength = 0
        Me.txtHead.Name = "txtHead"
        Me.txtHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHead.Size = New System.Drawing.Size(71, 20)
        Me.txtHead.TabIndex = 63
        Me.txtHead.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtAlt0
        '
        Me.txtAlt0.BackColor = System.Drawing.SystemColors.Window
        Me.txtAlt0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAlt0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAlt0.Location = New System.Drawing.Point(18, 36)
        Me.txtAlt0.MaxLength = 0
        Me.txtAlt0.Name = "txtAlt0"
        Me.txtAlt0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAlt0.Size = New System.Drawing.Size(90, 20)
        Me.txtAlt0.TabIndex = 62
        Me.txtAlt0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdHelpSlope
        '
        Me.cmdHelpSlope.BackColor = System.Drawing.SystemColors.Control
        Me.cmdHelpSlope.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdHelpSlope.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHelpSlope.Location = New System.Drawing.Point(171, 36)
        Me.cmdHelpSlope.Name = "cmdHelpSlope"
        Me.cmdHelpSlope.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdHelpSlope.Size = New System.Drawing.Size(44, 25)
        Me.cmdHelpSlope.TabIndex = 61
        Me.cmdHelpSlope.Text = "Help"
        Me.cmdHelpSlope.UseVisualStyleBackColor = False
        '
        'cmdSlope
        '
        Me.cmdSlope.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSlope.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSlope.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSlope.Location = New System.Drawing.Point(171, 83)
        Me.cmdSlope.Name = "cmdSlope"
        Me.cmdSlope.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSlope.Size = New System.Drawing.Size(44, 25)
        Me.cmdSlope.TabIndex = 60
        Me.cmdSlope.Text = "Set"
        Me.cmdSlope.UseVisualStyleBackColor = False
        '
        'txtSlope
        '
        Me.txtSlope.BackColor = System.Drawing.SystemColors.Window
        Me.txtSlope.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSlope.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSlope.Location = New System.Drawing.Point(77, 88)
        Me.txtSlope.MaxLength = 0
        Me.txtSlope.Name = "txtSlope"
        Me.txtSlope.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSlope.Size = New System.Drawing.Size(71, 20)
        Me.txtSlope.TabIndex = 58
        Me.txtSlope.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbSY
        '
        Me.lbSY.AutoSize = True
        Me.lbSY.Location = New System.Drawing.Point(136, 119)
        Me.lbSY.Name = "lbSY"
        Me.lbSY.Size = New System.Drawing.Size(59, 13)
        Me.lbSY.TabIndex = 59
        Me.lbSY.Text = "SlopeY = 0"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(40, 91)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(34, 13)
        Me.Label8.TabIndex = 59
        Me.Label8.Text = "Slope"
        '
        'lbSX
        '
        Me.lbSX.AutoSize = True
        Me.lbSX.Location = New System.Drawing.Point(15, 119)
        Me.lbSX.Name = "lbSX"
        Me.lbSX.Size = New System.Drawing.Size(59, 13)
        Me.lbSX.TabIndex = 58
        Me.lbSX.Text = "SlopeX = 0"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(24, 65)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 13)
        Me.Label6.TabIndex = 57
        Me.Label6.Text = "Heading"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(111, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(27, 13)
        Me.Label4.TabIndex = 56
        Me.Label4.Text = "Pt #"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(59, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 13)
        Me.Label5.TabIndex = 53
        Me.Label5.Text = "Altitude of"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPt0
        '
        Me.txtPt0.BackColor = System.Drawing.SystemColors.Window
        Me.txtPt0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPt0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPt0.Location = New System.Drawing.Point(114, 36)
        Me.txtPt0.MaxLength = 0
        Me.txtPt0.Name = "txtPt0"
        Me.txtPt0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPt0.Size = New System.Drawing.Size(34, 20)
        Me.txtPt0.TabIndex = 52
        Me.txtPt0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.lbTexture)
        Me.TabPage2.Controls.Add(Me.cmdColor)
        Me.TabPage2.Controls.Add(Me.ckThisColor)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.lbExclude)
        Me.TabPage2.Controls.Add(Me.lbExcludeCaption)
        Me.TabPage2.Controls.Add(Me.cmdDetail)
        Me.TabPage2.Controls.Add(Me.imgTexture)
        Me.TabPage2.Controls.Add(Me.List1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(461, 246)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Vector Polys"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'lbTexture
        '
        Me.lbTexture.BackColor = System.Drawing.SystemColors.Control
        Me.lbTexture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbTexture.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbTexture.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbTexture.Location = New System.Drawing.Point(330, 24)
        Me.lbTexture.Name = "lbTexture"
        Me.lbTexture.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbTexture.Size = New System.Drawing.Size(44, 20)
        Me.lbTexture.TabIndex = 66
        Me.lbTexture.Text = "Label1"
        Me.lbTexture.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdColor
        '
        Me.cmdColor.Location = New System.Drawing.Point(276, 19)
        Me.cmdColor.Name = "cmdColor"
        Me.cmdColor.Size = New System.Drawing.Size(44, 25)
        Me.cmdColor.TabIndex = 65
        Me.cmdColor.Text = "Color"
        Me.cmdColor.UseVisualStyleBackColor = True
        '
        'ckThisColor
        '
        Me.ckThisColor.BackColor = System.Drawing.Color.Maroon
        Me.ckThisColor.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ckThisColor.Location = New System.Drawing.Point(380, 14)
        Me.ckThisColor.Name = "ckThisColor"
        Me.ckThisColor.Size = New System.Drawing.Size(66, 30)
        Me.ckThisColor.TabIndex = 64
        Me.ckThisColor.Text = "Use this Color"
        Me.ckThisColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ckThisColor.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(327, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 59
        Me.Label1.Text = "Type"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(223, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(192, 13)
        Me.Label3.TabIndex = 58
        Me.Label3.Text = "Right click to copy GUID into Clipboard"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(128, 13)
        Me.Label2.TabIndex = 57
        Me.Label2.Text = "Click to get larger window"
        '
        'lbExclude
        '
        Me.lbExclude.BackColor = System.Drawing.SystemColors.Control
        Me.lbExclude.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbExclude.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbExclude.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbExclude.Location = New System.Drawing.Point(226, 211)
        Me.lbExclude.Name = "lbExclude"
        Me.lbExclude.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbExclude.Size = New System.Drawing.Size(220, 20)
        Me.lbExclude.TabIndex = 56
        Me.lbExclude.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbExcludeCaption
        '
        Me.lbExcludeCaption.AutoSize = True
        Me.lbExcludeCaption.BackColor = System.Drawing.Color.Transparent
        Me.lbExcludeCaption.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbExcludeCaption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbExcludeCaption.Location = New System.Drawing.Point(223, 198)
        Me.lbExcludeCaption.Name = "lbExcludeCaption"
        Me.lbExcludeCaption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbExcludeCaption.Size = New System.Drawing.Size(175, 13)
        Me.lbExcludeCaption.TabIndex = 55
        Me.lbExcludeCaption.Text = "Terrain to Exclude (click to change)"
        Me.lbExcludeCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdDetail
        '
        Me.cmdDetail.Location = New System.Drawing.Point(226, 19)
        Me.cmdDetail.Name = "cmdDetail"
        Me.cmdDetail.Size = New System.Drawing.Size(44, 25)
        Me.cmdDetail.TabIndex = 54
        Me.cmdDetail.Text = "Info"
        Me.cmdDetail.UseVisualStyleBackColor = True
        '
        'imgTexture
        '
        Me.imgTexture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imgTexture.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgTexture.Location = New System.Drawing.Point(10, 30)
        Me.imgTexture.Name = "imgTexture"
        Me.imgTexture.Size = New System.Drawing.Size(200, 200)
        Me.imgTexture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgTexture.TabIndex = 50
        Me.imgTexture.TabStop = False
        '
        'List1
        '
        Me.List1.BackColor = System.Drawing.SystemColors.Window
        Me.List1.Cursor = System.Windows.Forms.Cursors.Default
        Me.List1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.List1.Location = New System.Drawing.Point(226, 61)
        Me.List1.Name = "List1"
        Me.List1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.List1.Size = New System.Drawing.Size(220, 134)
        Me.List1.TabIndex = 48
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.Transparent
        Me.TabPage3.Controls.Add(Me.lbPolyColor)
        Me.TabPage3.Controls.Add(Me.cmdColor2)
        Me.TabPage3.Controls.Add(Me.boxTiling)
        Me.TabPage3.Controls.Add(Me.lbTexName)
        Me.TabPage3.Controls.Add(Me.imgTex)
        Me.TabPage3.Controls.Add(Me.lbTex1)
        Me.TabPage3.Controls.Add(Me.lbTexPri)
        Me.TabPage3.Controls.Add(Me.LbV1)
        Me.TabPage3.Controls.Add(Me.txtTexName)
        Me.TabPage3.Controls.Add(Me.txtTexPri)
        Me.TabPage3.Controls.Add(Me.cmdTex)
        Me.TabPage3.Controls.Add(Me.txtV1)
        Me.TabPage3.Controls.Add(Me.ckNight)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(461, 246)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Textured Polys"
        '
        'lbPolyColor
        '
        Me.lbPolyColor.BackColor = System.Drawing.Color.Silver
        Me.lbPolyColor.Location = New System.Drawing.Point(355, 21)
        Me.lbPolyColor.Name = "lbPolyColor"
        Me.lbPolyColor.Size = New System.Drawing.Size(91, 20)
        Me.lbPolyColor.TabIndex = 89
        Me.lbPolyColor.Text = "Polygon Color"
        Me.lbPolyColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmdColor2
        '
        Me.cmdColor2.Location = New System.Drawing.Point(296, 19)
        Me.cmdColor2.Name = "cmdColor2"
        Me.cmdColor2.Size = New System.Drawing.Size(44, 25)
        Me.cmdColor2.TabIndex = 87
        Me.cmdColor2.Text = "Color"
        Me.cmdColor2.UseVisualStyleBackColor = True
        '
        'boxTiling
        '
        Me.boxTiling.Controls.Add(Me.lbTex4)
        Me.boxTiling.Controls.Add(Me.txtTexTileX)
        Me.boxTiling.Controls.Add(Me.lbTex5)
        Me.boxTiling.Controls.Add(Me.txtTexTileY)
        Me.boxTiling.Location = New System.Drawing.Point(226, 108)
        Me.boxTiling.Name = "boxTiling"
        Me.boxTiling.Size = New System.Drawing.Size(147, 82)
        Me.boxTiling.TabIndex = 86
        Me.boxTiling.TabStop = False
        Me.boxTiling.Text = "Texture Tiling"
        '
        'lbTex4
        '
        Me.lbTex4.AutoSize = True
        Me.lbTex4.BackColor = System.Drawing.Color.Transparent
        Me.lbTex4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbTex4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbTex4.Location = New System.Drawing.Point(40, 25)
        Me.lbTex4.Name = "lbTex4"
        Me.lbTex4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbTex4.Size = New System.Drawing.Size(100, 13)
        Me.lbTex4.TabIndex = 80
        Me.lbTex4.Text = "Horizontal Repetion"
        '
        'txtTexTileX
        '
        Me.txtTexTileX.AcceptsReturn = True
        Me.txtTexTileX.BackColor = System.Drawing.SystemColors.Window
        Me.txtTexTileX.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTexTileX.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTexTileX.Location = New System.Drawing.Point(13, 22)
        Me.txtTexTileX.MaxLength = 0
        Me.txtTexTileX.Name = "txtTexTileX"
        Me.txtTexTileX.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTexTileX.Size = New System.Drawing.Size(22, 20)
        Me.txtTexTileX.TabIndex = 74
        Me.txtTexTileX.Text = "1"
        Me.txtTexTileX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbTex5
        '
        Me.lbTex5.AutoSize = True
        Me.lbTex5.BackColor = System.Drawing.Color.Transparent
        Me.lbTex5.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbTex5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbTex5.Location = New System.Drawing.Point(40, 53)
        Me.lbTex5.Name = "lbTex5"
        Me.lbTex5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbTex5.Size = New System.Drawing.Size(93, 13)
        Me.lbTex5.TabIndex = 81
        Me.lbTex5.Text = "Vertical Repetition"
        '
        'txtTexTileY
        '
        Me.txtTexTileY.AcceptsReturn = True
        Me.txtTexTileY.BackColor = System.Drawing.SystemColors.Window
        Me.txtTexTileY.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTexTileY.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTexTileY.Location = New System.Drawing.Point(13, 50)
        Me.txtTexTileY.MaxLength = 0
        Me.txtTexTileY.Name = "txtTexTileY"
        Me.txtTexTileY.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTexTileY.Size = New System.Drawing.Size(22, 20)
        Me.txtTexTileY.TabIndex = 73
        Me.txtTexTileY.Text = "1"
        Me.txtTexTileY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbTexName
        '
        Me.lbTexName.AutoSize = True
        Me.lbTexName.Location = New System.Drawing.Point(223, 51)
        Me.lbTexName.Name = "lbTexName"
        Me.lbTexName.Size = New System.Drawing.Size(74, 13)
        Me.lbTexName.TabIndex = 85
        Me.lbTexName.Text = "Texture Name"
        '
        'imgTex
        '
        Me.imgTex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imgTex.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgTex.Location = New System.Drawing.Point(10, 30)
        Me.imgTex.Name = "imgTex"
        Me.imgTex.Size = New System.Drawing.Size(200, 200)
        Me.imgTex.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgTex.TabIndex = 71
        Me.imgTex.TabStop = False
        '
        'lbTex1
        '
        Me.lbTex1.AutoSize = True
        Me.lbTex1.BackColor = System.Drawing.Color.Transparent
        Me.lbTex1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbTex1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbTex1.Location = New System.Drawing.Point(7, 12)
        Me.lbTex1.Name = "lbTex1"
        Me.lbTex1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbTex1.Size = New System.Drawing.Size(172, 13)
        Me.lbTex1.TabIndex = 77
        Me.lbTex1.Text = "Click on the picture to adjust points"
        '
        'lbTexPri
        '
        Me.lbTexPri.AutoSize = True
        Me.lbTexPri.BackColor = System.Drawing.Color.Transparent
        Me.lbTexPri.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbTexPri.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbTexPri.Location = New System.Drawing.Point(369, 213)
        Me.lbTexPri.Name = "lbTexPri"
        Me.lbTexPri.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbTexPri.Size = New System.Drawing.Size(41, 13)
        Me.lbTexPri.TabIndex = 79
        Me.lbTexPri.Text = "Priority:"
        '
        'LbV1
        '
        Me.LbV1.AutoSize = True
        Me.LbV1.BackColor = System.Drawing.Color.Transparent
        Me.LbV1.Cursor = System.Windows.Forms.Cursors.Default
        Me.LbV1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LbV1.Location = New System.Drawing.Point(234, 213)
        Me.LbV1.Name = "LbV1"
        Me.LbV1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LbV1.Size = New System.Drawing.Size(63, 13)
        Me.LbV1.TabIndex = 83
        Me.LbV1.Text = "Visibility (m):"
        Me.LbV1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTexName
        '
        Me.txtTexName.AcceptsReturn = True
        Me.txtTexName.BackColor = System.Drawing.SystemColors.Window
        Me.txtTexName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTexName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTexName.Location = New System.Drawing.Point(226, 67)
        Me.txtTexName.MaxLength = 0
        Me.txtTexName.Name = "txtTexName"
        Me.txtTexName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTexName.Size = New System.Drawing.Size(147, 20)
        Me.txtTexName.TabIndex = 72
        Me.txtTexName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtTexPri
        '
        Me.txtTexPri.AcceptsReturn = True
        Me.txtTexPri.BackColor = System.Drawing.SystemColors.Window
        Me.txtTexPri.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTexPri.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTexPri.Location = New System.Drawing.Point(416, 210)
        Me.txtTexPri.MaxLength = 0
        Me.txtTexPri.Name = "txtTexPri"
        Me.txtTexPri.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTexPri.Size = New System.Drawing.Size(25, 20)
        Me.txtTexPri.TabIndex = 75
        Me.txtTexPri.Text = "4"
        Me.txtTexPri.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdTex
        '
        Me.cmdTex.BackColor = System.Drawing.SystemColors.Control
        Me.cmdTex.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTex.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTex.Location = New System.Drawing.Point(402, 62)
        Me.cmdTex.Name = "cmdTex"
        Me.cmdTex.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTex.Size = New System.Drawing.Size(44, 25)
        Me.cmdTex.TabIndex = 76
        Me.cmdTex.Text = "..."
        Me.cmdTex.UseVisualStyleBackColor = False
        '
        'txtV1
        '
        Me.txtV1.AcceptsReturn = True
        Me.txtV1.BackColor = System.Drawing.SystemColors.Window
        Me.txtV1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtV1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtV1.Location = New System.Drawing.Point(303, 210)
        Me.txtV1.MaxLength = 0
        Me.txtV1.Name = "txtV1"
        Me.txtV1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtV1.Size = New System.Drawing.Size(52, 20)
        Me.txtV1.TabIndex = 82
        Me.txtV1.Text = "5000"
        Me.txtV1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ckNight
        '
        Me.ckNight.BackColor = System.Drawing.Color.Transparent
        Me.ckNight.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckNight.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckNight.Location = New System.Drawing.Point(387, 135)
        Me.ckNight.Name = "ckNight"
        Me.ckNight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckNight.Size = New System.Drawing.Size(54, 39)
        Me.ckNight.TabIndex = 84
        Me.ckNight.Text = "Night Map"
        Me.ckNight.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(344, 290)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(62, 25)
        Me.cmdCancel.TabIndex = 21
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(420, 290)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(62, 25)
        Me.cmdOK.TabIndex = 20
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdSmooth
        '
        Me.cmdSmooth.Location = New System.Drawing.Point(17, 290)
        Me.cmdSmooth.Name = "cmdSmooth"
        Me.cmdSmooth.Size = New System.Drawing.Size(62, 25)
        Me.cmdSmooth.TabIndex = 35
        Me.cmdSmooth.Text = "Smooth"
        Me.cmdSmooth.UseVisualStyleBackColor = True
        '
        'cmdSample
        '
        Me.cmdSample.Location = New System.Drawing.Point(98, 290)
        Me.cmdSample.Name = "cmdSample"
        Me.cmdSample.Size = New System.Drawing.Size(62, 25)
        Me.cmdSample.TabIndex = 36
        Me.cmdSample.Text = "Sample"
        Me.cmdSample.UseVisualStyleBackColor = True
        '
        'FrmPolysP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(502, 327)
        Me.Controls.Add(Me.cmdSample)
        Me.Controls.Add(Me.cmdSmooth)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.cmdOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPolysP"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - Polygon Properties"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.BoxOrder.ResumeLayout(False)
        Me.BoxOrder.PerformLayout()
        Me.boxType.ResumeLayout(False)
        Me.boxType.PerformLayout()
        Me.boxAltitude.ResumeLayout(False)
        Me.boxAltitude.PerformLayout()
        Me.boxSlope.ResumeLayout(False)
        Me.boxSlope.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.imgTexture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.boxTiling.ResumeLayout(False)
        Me.boxTiling.PerformLayout()
        CType(Me.imgTex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lbExclude As System.Windows.Forms.Label
    Public WithEvents lbExcludeCaption As System.Windows.Forms.Label
    Friend WithEvents cmdDetail As System.Windows.Forms.Button
    Public WithEvents imgTexture As System.Windows.Forms.PictureBox
    Public WithEvents List1 As System.Windows.Forms.ListBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents boxSlope As System.Windows.Forms.GroupBox
    Public WithEvents txtHead As System.Windows.Forms.TextBox
    Public WithEvents txtAlt0 As System.Windows.Forms.TextBox
    Public WithEvents cmdHelpSlope As System.Windows.Forms.Button
    Public WithEvents cmdSlope As System.Windows.Forms.Button
    Public WithEvents txtSlope As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents txtPt0 As System.Windows.Forms.TextBox
    Friend WithEvents boxAltitude As System.Windows.Forms.GroupBox
    Public WithEvents txtAlt As System.Windows.Forms.TextBox
    Public WithEvents cmdAlt As System.Windows.Forms.Button
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lbName As System.Windows.Forms.Label
    Friend WithEvents lbSY As System.Windows.Forms.Label
    Friend WithEvents lbSX As System.Windows.Forms.Label
    Friend WithEvents boxType As System.Windows.Forms.GroupBox
    Public WithEvents lbOrder As System.Windows.Forms.Label
    Friend WithEvents optTexture As System.Windows.Forms.RadioButton
    Friend WithEvents optVector As System.Windows.Forms.RadioButton
    Friend WithEvents cmdType As System.Windows.Forms.Button
    Friend WithEvents BoxOrder As System.Windows.Forms.GroupBox
    Public WithEvents cmdUP As System.Windows.Forms.Button
    Public WithEvents cmdBottom As System.Windows.Forms.Button
    Public WithEvents cmdTop As System.Windows.Forms.Button
    Public WithEvents cmdDown As System.Windows.Forms.Button
    Friend WithEvents cmdSmooth As System.Windows.Forms.Button
    Friend WithEvents cmdSample As System.Windows.Forms.Button
    Public WithEvents imgTex As System.Windows.Forms.PictureBox
    Public WithEvents lbTex1 As System.Windows.Forms.Label
    Public WithEvents lbTexPri As System.Windows.Forms.Label
    Public WithEvents lbTex4 As System.Windows.Forms.Label
    Public WithEvents lbTex5 As System.Windows.Forms.Label
    Public WithEvents LbV1 As System.Windows.Forms.Label
    Public WithEvents txtTexName As System.Windows.Forms.TextBox
    Public WithEvents txtTexTileY As System.Windows.Forms.TextBox
    Public WithEvents txtTexTileX As System.Windows.Forms.TextBox
    Public WithEvents txtTexPri As System.Windows.Forms.TextBox
    Public WithEvents cmdTex As System.Windows.Forms.Button
    Public WithEvents txtV1 As System.Windows.Forms.TextBox
    Public WithEvents ckNight As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbTexName As System.Windows.Forms.Label
    Friend WithEvents boxTiling As System.Windows.Forms.GroupBox
    Friend WithEvents cmdColor As System.Windows.Forms.Button
    Friend WithEvents ckThisColor As System.Windows.Forms.CheckBox
    Friend WithEvents cmdColor2 As System.Windows.Forms.Button
    Friend WithEvents lbPolyColor As System.Windows.Forms.Label
    Public WithEvents lbTexture As System.Windows.Forms.Label

End Class
