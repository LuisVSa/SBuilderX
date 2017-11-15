<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGBuilding
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        vertexBuffer.Dispose()
        vertexBuffer0.Dispose()
        renderDevice.Dispose()
        fntOut.Dispose()
        MyBase.Dispose(disposing)

    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGBuilding))
        Me.nUPsizeBottomY = New System.Windows.Forms.NumericUpDown
        Me.nUPsizeWindowY = New System.Windows.Forms.NumericUpDown
        Me.nUPsizeTopY = New System.Windows.Forms.NumericUpDown
        Me.nUPsizeRoofY = New System.Windows.Forms.NumericUpDown
        Me.nUPsizeTopX = New System.Windows.Forms.NumericUpDown
        Me.nUPsizeTopZ = New System.Windows.Forms.NumericUpDown
        Me.nUPtextureIndexBottomX = New System.Windows.Forms.NumericUpDown
        Me.nUPtextureIndexBottomZ = New System.Windows.Forms.NumericUpDown
        Me.nUPtextureIndexWindowX = New System.Windows.Forms.NumericUpDown
        Me.nUPtextureIndexWindowY = New System.Windows.Forms.NumericUpDown
        Me.nUPtextureIndexWindowZ = New System.Windows.Forms.NumericUpDown
        Me.nUPtextureIndexTopX = New System.Windows.Forms.NumericUpDown
        Me.nUPtextureIndexTopZ = New System.Windows.Forms.NumericUpDown
        Me.nUPtextureIndexRoofX = New System.Windows.Forms.NumericUpDown
        Me.nUPtextureIndexRoofY = New System.Windows.Forms.NumericUpDown
        Me.nUPtextureIndexRoofZ = New System.Windows.Forms.NumericUpDown
        Me.nUPbuildingSides = New System.Windows.Forms.NumericUpDown
        Me.ckSmoothing = New System.Windows.Forms.CheckBox
        Me.lbSides = New System.Windows.Forms.Label
        Me.lbG = New System.Windows.Forms.Label
        Me.lbF = New System.Windows.Forms.Label
        Me.nUPgableTexture = New System.Windows.Forms.NumericUpDown
        Me.nUPfaceTexture = New System.Windows.Forms.NumericUpDown
        Me.nUPtextureIndexGableY = New System.Windows.Forms.NumericUpDown
        Me.nUPtextureIndexFaceX = New System.Windows.Forms.NumericUpDown
        Me.nUPtextureIndexGableZ = New System.Windows.Forms.NumericUpDown
        Me.nUPtextureIndexFaceY = New System.Windows.Forms.NumericUpDown
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdOK = New System.Windows.Forms.Button
        Me.lbgb3 = New System.Windows.Forms.Label
        Me.lbBW = New System.Windows.Forms.Label
        Me.lbBD = New System.Windows.Forms.Label
        Me.nUPsizeX = New System.Windows.Forms.NumericUpDown
        Me.nUPsizeZ = New System.Windows.Forms.NumericUpDown
        Me.nUPscale = New System.Windows.Forms.NumericUpDown
        Me.nUPtopTexture = New System.Windows.Forms.NumericUpDown
        Me.nUPbottomTexture = New System.Windows.Forms.NumericUpDown
        Me.nUProofTexture = New System.Windows.Forms.NumericUpDown
        Me.nUPwindowTexture = New System.Windows.Forms.NumericUpDown
        Me.imgGenB = New System.Windows.Forms.PictureBox
        Me.frGenB = New System.Windows.Forms.GroupBox
        Me.optGbMultiSided = New System.Windows.Forms.RadioButton
        Me.optGbPyramidal = New System.Windows.Forms.RadioButton
        Me.optGbSlant = New System.Windows.Forms.RadioButton
        Me.optGbRidge = New System.Windows.Forms.RadioButton
        Me.optGbPeaked = New System.Windows.Forms.RadioButton
        Me.optGbFlat = New System.Windows.Forms.RadioButton
        Me.frBottom = New System.Windows.Forms.GroupBox
        Me.lbBH = New System.Windows.Forms.Label
        Me.lbBT = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.lbBZT = New System.Windows.Forms.Label
        Me.lbRH = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.lbWZT = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.frWindow = New System.Windows.Forms.GroupBox
        Me.nUPWZ = New System.Windows.Forms.NumericUpDown
        Me.nUPWX = New System.Windows.Forms.NumericUpDown
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lbGYT = New System.Windows.Forms.Label
        Me.Label43 = New System.Windows.Forms.Label
        Me.Label44 = New System.Windows.Forms.Label
        Me.frTop = New System.Windows.Forms.GroupBox
        Me.Label45 = New System.Windows.Forms.Label
        Me.lbTZT = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lbTD = New System.Windows.Forms.Label
        Me.lbTW = New System.Windows.Forms.Label
        Me.lbGZT = New System.Windows.Forms.Label
        Me.frRoof = New System.Windows.Forms.GroupBox
        Me.nUPRZ = New System.Windows.Forms.NumericUpDown
        Me.nUPRX = New System.Windows.Forms.NumericUpDown
        Me.lbFYT = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lbFXT = New System.Windows.Forms.Label
        Me.lbRYT = New System.Windows.Forms.Label
        Me.frMulti = New System.Windows.Forms.GroupBox
        Me.nUPGrid = New System.Windows.Forms.NumericUpDown
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        CType(Me.nUPsizeBottomY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPsizeWindowY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPsizeTopY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPsizeRoofY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPsizeTopX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPsizeTopZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPtextureIndexBottomX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPtextureIndexBottomZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPtextureIndexWindowX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPtextureIndexWindowY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPtextureIndexWindowZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPtextureIndexTopX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPtextureIndexTopZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPtextureIndexRoofX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPtextureIndexRoofY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPtextureIndexRoofZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPbuildingSides, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPgableTexture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPfaceTexture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPtextureIndexGableY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPtextureIndexFaceX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPtextureIndexGableZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPtextureIndexFaceY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPsizeX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPsizeZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPscale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPtopTexture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPbottomTexture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUProofTexture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPwindowTexture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgGenB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.frGenB.SuspendLayout()
        Me.frBottom.SuspendLayout()
        Me.frWindow.SuspendLayout()
        CType(Me.nUPWZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPWX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.frTop.SuspendLayout()
        Me.frRoof.SuspendLayout()
        CType(Me.nUPRZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nUPRX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.frMulti.SuspendLayout()
        CType(Me.nUPGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'nUPsizeBottomY
        '
        Me.nUPsizeBottomY.BackColor = System.Drawing.SystemColors.Window
        Me.nUPsizeBottomY.Enabled = False
        Me.nUPsizeBottomY.Location = New System.Drawing.Point(143, 37)
        Me.nUPsizeBottomY.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nUPsizeBottomY.Name = "nUPsizeBottomY"
        Me.nUPsizeBottomY.Size = New System.Drawing.Size(53, 20)
        Me.nUPsizeBottomY.TabIndex = 57
        Me.nUPsizeBottomY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPsizeBottomY.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nUPsizeWindowY
        '
        Me.nUPsizeWindowY.BackColor = System.Drawing.SystemColors.Window
        Me.nUPsizeWindowY.Enabled = False
        Me.nUPsizeWindowY.Location = New System.Drawing.Point(143, 36)
        Me.nUPsizeWindowY.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nUPsizeWindowY.Name = "nUPsizeWindowY"
        Me.nUPsizeWindowY.Size = New System.Drawing.Size(55, 20)
        Me.nUPsizeWindowY.TabIndex = 57
        Me.nUPsizeWindowY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPsizeWindowY.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nUPsizeTopY
        '
        Me.nUPsizeTopY.BackColor = System.Drawing.SystemColors.Window
        Me.nUPsizeTopY.Enabled = False
        Me.nUPsizeTopY.Location = New System.Drawing.Point(142, 37)
        Me.nUPsizeTopY.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nUPsizeTopY.Name = "nUPsizeTopY"
        Me.nUPsizeTopY.Size = New System.Drawing.Size(55, 20)
        Me.nUPsizeTopY.TabIndex = 57
        Me.nUPsizeTopY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPsizeTopY.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nUPsizeRoofY
        '
        Me.nUPsizeRoofY.BackColor = System.Drawing.SystemColors.Window
        Me.nUPsizeRoofY.Enabled = False
        Me.nUPsizeRoofY.Location = New System.Drawing.Point(143, 37)
        Me.nUPsizeRoofY.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nUPsizeRoofY.Name = "nUPsizeRoofY"
        Me.nUPsizeRoofY.Size = New System.Drawing.Size(55, 20)
        Me.nUPsizeRoofY.TabIndex = 57
        Me.nUPsizeRoofY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPsizeRoofY.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nUPsizeTopX
        '
        Me.nUPsizeTopX.BackColor = System.Drawing.SystemColors.Window
        Me.nUPsizeTopX.DecimalPlaces = 2
        Me.nUPsizeTopX.Enabled = False
        Me.nUPsizeTopX.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nUPsizeTopX.Location = New System.Drawing.Point(74, 37)
        Me.nUPsizeTopX.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nUPsizeTopX.Name = "nUPsizeTopX"
        Me.nUPsizeTopX.Size = New System.Drawing.Size(55, 20)
        Me.nUPsizeTopX.TabIndex = 57
        Me.nUPsizeTopX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPsizeTopX.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nUPsizeTopZ
        '
        Me.nUPsizeTopZ.BackColor = System.Drawing.SystemColors.Window
        Me.nUPsizeTopZ.DecimalPlaces = 2
        Me.nUPsizeTopZ.Enabled = False
        Me.nUPsizeTopZ.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nUPsizeTopZ.Location = New System.Drawing.Point(210, 37)
        Me.nUPsizeTopZ.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nUPsizeTopZ.Name = "nUPsizeTopZ"
        Me.nUPsizeTopZ.Size = New System.Drawing.Size(55, 20)
        Me.nUPsizeTopZ.TabIndex = 57
        Me.nUPsizeTopZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPsizeTopZ.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nUPtextureIndexBottomX
        '
        Me.nUPtextureIndexBottomX.BackColor = System.Drawing.SystemColors.Window
        Me.nUPtextureIndexBottomX.DecimalPlaces = 3
        Me.nUPtextureIndexBottomX.Enabled = False
        Me.nUPtextureIndexBottomX.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.nUPtextureIndexBottomX.Location = New System.Drawing.Point(73, 78)
        Me.nUPtextureIndexBottomX.Maximum = New Decimal(New Integer() {9999, 0, 0, 131072})
        Me.nUPtextureIndexBottomX.Name = "nUPtextureIndexBottomX"
        Me.nUPtextureIndexBottomX.Size = New System.Drawing.Size(55, 20)
        Me.nUPtextureIndexBottomX.TabIndex = 59
        Me.nUPtextureIndexBottomX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPtextureIndexBottomX.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nUPtextureIndexBottomZ
        '
        Me.nUPtextureIndexBottomZ.BackColor = System.Drawing.SystemColors.Window
        Me.nUPtextureIndexBottomZ.DecimalPlaces = 3
        Me.nUPtextureIndexBottomZ.Enabled = False
        Me.nUPtextureIndexBottomZ.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.nUPtextureIndexBottomZ.Location = New System.Drawing.Point(210, 78)
        Me.nUPtextureIndexBottomZ.Maximum = New Decimal(New Integer() {9999, 0, 0, 131072})
        Me.nUPtextureIndexBottomZ.Name = "nUPtextureIndexBottomZ"
        Me.nUPtextureIndexBottomZ.Size = New System.Drawing.Size(55, 20)
        Me.nUPtextureIndexBottomZ.TabIndex = 59
        Me.nUPtextureIndexBottomZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPtextureIndexBottomZ.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nUPtextureIndexWindowX
        '
        Me.nUPtextureIndexWindowX.BackColor = System.Drawing.SystemColors.Window
        Me.nUPtextureIndexWindowX.DecimalPlaces = 3
        Me.nUPtextureIndexWindowX.Enabled = False
        Me.nUPtextureIndexWindowX.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.nUPtextureIndexWindowX.Location = New System.Drawing.Point(73, 78)
        Me.nUPtextureIndexWindowX.Maximum = New Decimal(New Integer() {9999, 0, 0, 131072})
        Me.nUPtextureIndexWindowX.Name = "nUPtextureIndexWindowX"
        Me.nUPtextureIndexWindowX.Size = New System.Drawing.Size(55, 20)
        Me.nUPtextureIndexWindowX.TabIndex = 59
        Me.nUPtextureIndexWindowX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPtextureIndexWindowX.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nUPtextureIndexWindowY
        '
        Me.nUPtextureIndexWindowY.BackColor = System.Drawing.SystemColors.Window
        Me.nUPtextureIndexWindowY.DecimalPlaces = 3
        Me.nUPtextureIndexWindowY.Enabled = False
        Me.nUPtextureIndexWindowY.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.nUPtextureIndexWindowY.Location = New System.Drawing.Point(143, 78)
        Me.nUPtextureIndexWindowY.Maximum = New Decimal(New Integer() {9999, 0, 0, 131072})
        Me.nUPtextureIndexWindowY.Name = "nUPtextureIndexWindowY"
        Me.nUPtextureIndexWindowY.Size = New System.Drawing.Size(55, 20)
        Me.nUPtextureIndexWindowY.TabIndex = 59
        Me.nUPtextureIndexWindowY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPtextureIndexWindowY.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nUPtextureIndexWindowZ
        '
        Me.nUPtextureIndexWindowZ.BackColor = System.Drawing.SystemColors.Window
        Me.nUPtextureIndexWindowZ.DecimalPlaces = 3
        Me.nUPtextureIndexWindowZ.Enabled = False
        Me.nUPtextureIndexWindowZ.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.nUPtextureIndexWindowZ.Location = New System.Drawing.Point(214, 78)
        Me.nUPtextureIndexWindowZ.Maximum = New Decimal(New Integer() {9999, 0, 0, 131072})
        Me.nUPtextureIndexWindowZ.Name = "nUPtextureIndexWindowZ"
        Me.nUPtextureIndexWindowZ.Size = New System.Drawing.Size(55, 20)
        Me.nUPtextureIndexWindowZ.TabIndex = 59
        Me.nUPtextureIndexWindowZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPtextureIndexWindowZ.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nUPtextureIndexTopX
        '
        Me.nUPtextureIndexTopX.BackColor = System.Drawing.SystemColors.Window
        Me.nUPtextureIndexTopX.DecimalPlaces = 3
        Me.nUPtextureIndexTopX.Enabled = False
        Me.nUPtextureIndexTopX.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.nUPtextureIndexTopX.Location = New System.Drawing.Point(74, 78)
        Me.nUPtextureIndexTopX.Maximum = New Decimal(New Integer() {9999, 0, 0, 131072})
        Me.nUPtextureIndexTopX.Name = "nUPtextureIndexTopX"
        Me.nUPtextureIndexTopX.Size = New System.Drawing.Size(55, 20)
        Me.nUPtextureIndexTopX.TabIndex = 59
        Me.nUPtextureIndexTopX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPtextureIndexTopX.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nUPtextureIndexTopZ
        '
        Me.nUPtextureIndexTopZ.BackColor = System.Drawing.SystemColors.Window
        Me.nUPtextureIndexTopZ.DecimalPlaces = 3
        Me.nUPtextureIndexTopZ.Enabled = False
        Me.nUPtextureIndexTopZ.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.nUPtextureIndexTopZ.Location = New System.Drawing.Point(212, 78)
        Me.nUPtextureIndexTopZ.Maximum = New Decimal(New Integer() {9999, 0, 0, 131072})
        Me.nUPtextureIndexTopZ.Name = "nUPtextureIndexTopZ"
        Me.nUPtextureIndexTopZ.Size = New System.Drawing.Size(55, 20)
        Me.nUPtextureIndexTopZ.TabIndex = 59
        Me.nUPtextureIndexTopZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPtextureIndexTopZ.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nUPtextureIndexRoofX
        '
        Me.nUPtextureIndexRoofX.BackColor = System.Drawing.SystemColors.Window
        Me.nUPtextureIndexRoofX.DecimalPlaces = 3
        Me.nUPtextureIndexRoofX.Enabled = False
        Me.nUPtextureIndexRoofX.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.nUPtextureIndexRoofX.Location = New System.Drawing.Point(75, 78)
        Me.nUPtextureIndexRoofX.Maximum = New Decimal(New Integer() {9999, 0, 0, 131072})
        Me.nUPtextureIndexRoofX.Name = "nUPtextureIndexRoofX"
        Me.nUPtextureIndexRoofX.Size = New System.Drawing.Size(55, 20)
        Me.nUPtextureIndexRoofX.TabIndex = 59
        Me.nUPtextureIndexRoofX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPtextureIndexRoofX.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nUPtextureIndexRoofY
        '
        Me.nUPtextureIndexRoofY.BackColor = System.Drawing.SystemColors.Window
        Me.nUPtextureIndexRoofY.DecimalPlaces = 3
        Me.nUPtextureIndexRoofY.Enabled = False
        Me.nUPtextureIndexRoofY.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.nUPtextureIndexRoofY.Location = New System.Drawing.Point(143, 78)
        Me.nUPtextureIndexRoofY.Maximum = New Decimal(New Integer() {9999, 0, 0, 131072})
        Me.nUPtextureIndexRoofY.Name = "nUPtextureIndexRoofY"
        Me.nUPtextureIndexRoofY.Size = New System.Drawing.Size(55, 20)
        Me.nUPtextureIndexRoofY.TabIndex = 59
        Me.nUPtextureIndexRoofY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPtextureIndexRoofY.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nUPtextureIndexRoofZ
        '
        Me.nUPtextureIndexRoofZ.BackColor = System.Drawing.SystemColors.Window
        Me.nUPtextureIndexRoofZ.DecimalPlaces = 3
        Me.nUPtextureIndexRoofZ.Enabled = False
        Me.nUPtextureIndexRoofZ.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.nUPtextureIndexRoofZ.Location = New System.Drawing.Point(212, 78)
        Me.nUPtextureIndexRoofZ.Maximum = New Decimal(New Integer() {9999, 0, 0, 131072})
        Me.nUPtextureIndexRoofZ.Name = "nUPtextureIndexRoofZ"
        Me.nUPtextureIndexRoofZ.Size = New System.Drawing.Size(55, 20)
        Me.nUPtextureIndexRoofZ.TabIndex = 59
        Me.nUPtextureIndexRoofZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPtextureIndexRoofZ.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nUPbuildingSides
        '
        Me.nUPbuildingSides.BackColor = System.Drawing.SystemColors.Window
        Me.nUPbuildingSides.Enabled = False
        Me.nUPbuildingSides.Location = New System.Drawing.Point(138, 22)
        Me.nUPbuildingSides.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.nUPbuildingSides.Minimum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.nUPbuildingSides.Name = "nUPbuildingSides"
        Me.nUPbuildingSides.Size = New System.Drawing.Size(40, 20)
        Me.nUPbuildingSides.TabIndex = 59
        Me.nUPbuildingSides.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPbuildingSides.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'ckSmoothing
        '
        Me.ckSmoothing.AutoSize = True
        Me.ckSmoothing.Enabled = False
        Me.ckSmoothing.Location = New System.Drawing.Point(12, 25)
        Me.ckSmoothing.Name = "ckSmoothing"
        Me.ckSmoothing.Size = New System.Drawing.Size(76, 17)
        Me.ckSmoothing.TabIndex = 60
        Me.ckSmoothing.Text = "Smoothing"
        Me.ckSmoothing.UseVisualStyleBackColor = True
        '
        'lbSides
        '
        Me.lbSides.AutoSize = True
        Me.lbSides.Location = New System.Drawing.Point(99, 27)
        Me.lbSides.Name = "lbSides"
        Me.lbSides.Size = New System.Drawing.Size(33, 13)
        Me.lbSides.TabIndex = 58
        Me.lbSides.Text = "Sides"
        '
        'lbG
        '
        Me.lbG.AutoSize = True
        Me.lbG.Location = New System.Drawing.Point(14, 104)
        Me.lbG.Name = "lbG"
        Me.lbG.Size = New System.Drawing.Size(35, 13)
        Me.lbG.TabIndex = 58
        Me.lbG.Text = "Gable"
        '
        'lbF
        '
        Me.lbF.AutoSize = True
        Me.lbF.Location = New System.Drawing.Point(13, 146)
        Me.lbF.Name = "lbF"
        Me.lbF.Size = New System.Drawing.Size(31, 13)
        Me.lbF.TabIndex = 58
        Me.lbF.Text = "Face"
        '
        'nUPgableTexture
        '
        Me.nUPgableTexture.BackColor = System.Drawing.SystemColors.Window
        Me.nUPgableTexture.Enabled = False
        Me.nUPgableTexture.Location = New System.Drawing.Point(17, 120)
        Me.nUPgableTexture.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nUPgableTexture.Name = "nUPgableTexture"
        Me.nUPgableTexture.Size = New System.Drawing.Size(46, 20)
        Me.nUPgableTexture.TabIndex = 59
        Me.nUPgableTexture.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPgableTexture.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nUPfaceTexture
        '
        Me.nUPfaceTexture.BackColor = System.Drawing.SystemColors.Window
        Me.nUPfaceTexture.Enabled = False
        Me.nUPfaceTexture.Location = New System.Drawing.Point(17, 161)
        Me.nUPfaceTexture.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nUPfaceTexture.Name = "nUPfaceTexture"
        Me.nUPfaceTexture.Size = New System.Drawing.Size(46, 20)
        Me.nUPfaceTexture.TabIndex = 59
        Me.nUPfaceTexture.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPfaceTexture.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nUPtextureIndexGableY
        '
        Me.nUPtextureIndexGableY.BackColor = System.Drawing.SystemColors.Window
        Me.nUPtextureIndexGableY.DecimalPlaces = 3
        Me.nUPtextureIndexGableY.Enabled = False
        Me.nUPtextureIndexGableY.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.nUPtextureIndexGableY.Location = New System.Drawing.Point(143, 120)
        Me.nUPtextureIndexGableY.Maximum = New Decimal(New Integer() {9999, 0, 0, 131072})
        Me.nUPtextureIndexGableY.Name = "nUPtextureIndexGableY"
        Me.nUPtextureIndexGableY.Size = New System.Drawing.Size(55, 20)
        Me.nUPtextureIndexGableY.TabIndex = 59
        Me.nUPtextureIndexGableY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPtextureIndexGableY.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nUPtextureIndexFaceX
        '
        Me.nUPtextureIndexFaceX.BackColor = System.Drawing.SystemColors.Window
        Me.nUPtextureIndexFaceX.DecimalPlaces = 3
        Me.nUPtextureIndexFaceX.Enabled = False
        Me.nUPtextureIndexFaceX.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.nUPtextureIndexFaceX.Location = New System.Drawing.Point(75, 162)
        Me.nUPtextureIndexFaceX.Maximum = New Decimal(New Integer() {9999, 0, 0, 131072})
        Me.nUPtextureIndexFaceX.Name = "nUPtextureIndexFaceX"
        Me.nUPtextureIndexFaceX.Size = New System.Drawing.Size(55, 20)
        Me.nUPtextureIndexFaceX.TabIndex = 59
        Me.nUPtextureIndexFaceX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPtextureIndexFaceX.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nUPtextureIndexGableZ
        '
        Me.nUPtextureIndexGableZ.BackColor = System.Drawing.SystemColors.Window
        Me.nUPtextureIndexGableZ.DecimalPlaces = 3
        Me.nUPtextureIndexGableZ.Enabled = False
        Me.nUPtextureIndexGableZ.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.nUPtextureIndexGableZ.Location = New System.Drawing.Point(212, 122)
        Me.nUPtextureIndexGableZ.Maximum = New Decimal(New Integer() {9999, 0, 0, 131072})
        Me.nUPtextureIndexGableZ.Name = "nUPtextureIndexGableZ"
        Me.nUPtextureIndexGableZ.Size = New System.Drawing.Size(55, 20)
        Me.nUPtextureIndexGableZ.TabIndex = 59
        Me.nUPtextureIndexGableZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPtextureIndexGableZ.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nUPtextureIndexFaceY
        '
        Me.nUPtextureIndexFaceY.BackColor = System.Drawing.SystemColors.Window
        Me.nUPtextureIndexFaceY.DecimalPlaces = 3
        Me.nUPtextureIndexFaceY.Enabled = False
        Me.nUPtextureIndexFaceY.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.nUPtextureIndexFaceY.Location = New System.Drawing.Point(143, 162)
        Me.nUPtextureIndexFaceY.Maximum = New Decimal(New Integer() {9999, 0, 0, 131072})
        Me.nUPtextureIndexFaceY.Name = "nUPtextureIndexFaceY"
        Me.nUPtextureIndexFaceY.Size = New System.Drawing.Size(55, 20)
        Me.nUPtextureIndexFaceY.TabIndex = 59
        Me.nUPtextureIndexFaceY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPtextureIndexFaceY.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(465, 505)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(59, 23)
        Me.cmdCancel.TabIndex = 61
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(549, 505)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(59, 23)
        Me.cmdOK.TabIndex = 61
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'lbgb3
        '
        Me.lbgb3.BackColor = System.Drawing.Color.Transparent
        Me.lbgb3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbgb3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbgb3.Location = New System.Drawing.Point(23, 99)
        Me.lbgb3.Name = "lbgb3"
        Me.lbgb3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbgb3.Size = New System.Drawing.Size(37, 18)
        Me.lbgb3.TabIndex = 64
        Me.lbgb3.Text = "Scale"
        Me.lbgb3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbBW
        '
        Me.lbBW.AutoSize = True
        Me.lbBW.Location = New System.Drawing.Point(64, 22)
        Me.lbBW.Name = "lbBW"
        Me.lbBW.Size = New System.Drawing.Size(45, 13)
        Me.lbBW.TabIndex = 66
        Me.lbBW.Text = "Width X"
        '
        'lbBD
        '
        Me.lbBD.AutoSize = True
        Me.lbBD.Location = New System.Drawing.Point(207, 22)
        Me.lbBD.Name = "lbBD"
        Me.lbBD.Size = New System.Drawing.Size(43, 13)
        Me.lbBD.TabIndex = 67
        Me.lbBD.Text = "Deep Z"
        '
        'nUPsizeX
        '
        Me.nUPsizeX.BackColor = System.Drawing.SystemColors.Window
        Me.nUPsizeX.DecimalPlaces = 2
        Me.nUPsizeX.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nUPsizeX.Location = New System.Drawing.Point(73, 36)
        Me.nUPsizeX.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nUPsizeX.Name = "nUPsizeX"
        Me.nUPsizeX.Size = New System.Drawing.Size(55, 20)
        Me.nUPsizeX.TabIndex = 68
        Me.nUPsizeX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPsizeX.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nUPsizeZ
        '
        Me.nUPsizeZ.BackColor = System.Drawing.SystemColors.Window
        Me.nUPsizeZ.DecimalPlaces = 2
        Me.nUPsizeZ.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nUPsizeZ.Location = New System.Drawing.Point(209, 38)
        Me.nUPsizeZ.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nUPsizeZ.Name = "nUPsizeZ"
        Me.nUPsizeZ.Size = New System.Drawing.Size(55, 20)
        Me.nUPsizeZ.TabIndex = 69
        Me.nUPsizeZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPsizeZ.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nUPscale
        '
        Me.nUPscale.BackColor = System.Drawing.SystemColors.Window
        Me.nUPscale.DecimalPlaces = 2
        Me.nUPscale.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.nUPscale.Location = New System.Drawing.Point(26, 120)
        Me.nUPscale.Name = "nUPscale"
        Me.nUPscale.Size = New System.Drawing.Size(50, 20)
        Me.nUPscale.TabIndex = 70
        Me.nUPscale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPscale.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nUPtopTexture
        '
        Me.nUPtopTexture.BackColor = System.Drawing.SystemColors.Window
        Me.nUPtopTexture.Location = New System.Drawing.Point(15, 37)
        Me.nUPtopTexture.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nUPtopTexture.Name = "nUPtopTexture"
        Me.nUPtopTexture.Size = New System.Drawing.Size(46, 20)
        Me.nUPtopTexture.TabIndex = 53
        Me.nUPtopTexture.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPtopTexture.Value = New Decimal(New Integer() {8, 0, 0, 0})
        '
        'nUPbottomTexture
        '
        Me.nUPbottomTexture.BackColor = System.Drawing.SystemColors.Window
        Me.nUPbottomTexture.Location = New System.Drawing.Point(14, 36)
        Me.nUPbottomTexture.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nUPbottomTexture.Name = "nUPbottomTexture"
        Me.nUPbottomTexture.Size = New System.Drawing.Size(46, 20)
        Me.nUPbottomTexture.TabIndex = 53
        Me.nUPbottomTexture.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPbottomTexture.Value = New Decimal(New Integer() {8, 0, 0, 0})
        '
        'nUProofTexture
        '
        Me.nUProofTexture.BackColor = System.Drawing.SystemColors.Window
        Me.nUProofTexture.Location = New System.Drawing.Point(16, 37)
        Me.nUProofTexture.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nUProofTexture.Name = "nUProofTexture"
        Me.nUProofTexture.Size = New System.Drawing.Size(46, 20)
        Me.nUProofTexture.TabIndex = 53
        Me.nUProofTexture.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUProofTexture.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'nUPwindowTexture
        '
        Me.nUPwindowTexture.BackColor = System.Drawing.SystemColors.Window
        Me.nUPwindowTexture.Location = New System.Drawing.Point(12, 36)
        Me.nUPwindowTexture.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nUPwindowTexture.Name = "nUPwindowTexture"
        Me.nUPwindowTexture.Size = New System.Drawing.Size(46, 20)
        Me.nUPwindowTexture.TabIndex = 53
        Me.nUPwindowTexture.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPwindowTexture.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'imgGenB
        '
        Me.imgGenB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imgGenB.Location = New System.Drawing.Point(323, 238)
        Me.imgGenB.Name = "imgGenB"
        Me.imgGenB.Size = New System.Drawing.Size(286, 252)
        Me.imgGenB.TabIndex = 72
        Me.imgGenB.TabStop = False
        '
        'frGenB
        '
        Me.frGenB.BackColor = System.Drawing.Color.Transparent
        Me.frGenB.Controls.Add(Me.optGbMultiSided)
        Me.frGenB.Controls.Add(Me.optGbPyramidal)
        Me.frGenB.Controls.Add(Me.optGbSlant)
        Me.frGenB.Controls.Add(Me.optGbRidge)
        Me.frGenB.Controls.Add(Me.optGbPeaked)
        Me.frGenB.Controls.Add(Me.optGbFlat)
        Me.frGenB.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.frGenB.Location = New System.Drawing.Point(22, 14)
        Me.frGenB.Name = "frGenB"
        Me.frGenB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frGenB.Size = New System.Drawing.Size(282, 73)
        Me.frGenB.TabIndex = 74
        Me.frGenB.TabStop = False
        Me.frGenB.Text = "Building Type"
        '
        'optGbMultiSided
        '
        Me.optGbMultiSided.AutoSize = True
        Me.optGbMultiSided.ForeColor = System.Drawing.Color.Black
        Me.optGbMultiSided.Location = New System.Drawing.Point(183, 42)
        Me.optGbMultiSided.Name = "optGbMultiSided"
        Me.optGbMultiSided.Size = New System.Drawing.Size(74, 17)
        Me.optGbMultiSided.TabIndex = 0
        Me.optGbMultiSided.Text = "MultiSided"
        Me.optGbMultiSided.UseVisualStyleBackColor = True
        '
        'optGbPyramidal
        '
        Me.optGbPyramidal.AutoSize = True
        Me.optGbPyramidal.ForeColor = System.Drawing.Color.Black
        Me.optGbPyramidal.Location = New System.Drawing.Point(183, 19)
        Me.optGbPyramidal.Name = "optGbPyramidal"
        Me.optGbPyramidal.Size = New System.Drawing.Size(70, 17)
        Me.optGbPyramidal.TabIndex = 0
        Me.optGbPyramidal.Text = "Pyramidal"
        Me.optGbPyramidal.UseVisualStyleBackColor = True
        '
        'optGbSlant
        '
        Me.optGbSlant.AutoSize = True
        Me.optGbSlant.ForeColor = System.Drawing.Color.Black
        Me.optGbSlant.Location = New System.Drawing.Point(109, 44)
        Me.optGbSlant.Name = "optGbSlant"
        Me.optGbSlant.Size = New System.Drawing.Size(49, 17)
        Me.optGbSlant.TabIndex = 0
        Me.optGbSlant.Text = "Slant"
        Me.optGbSlant.UseVisualStyleBackColor = True
        '
        'optGbRidge
        '
        Me.optGbRidge.AutoSize = True
        Me.optGbRidge.ForeColor = System.Drawing.Color.Black
        Me.optGbRidge.Location = New System.Drawing.Point(109, 21)
        Me.optGbRidge.Name = "optGbRidge"
        Me.optGbRidge.Size = New System.Drawing.Size(53, 17)
        Me.optGbRidge.TabIndex = 0
        Me.optGbRidge.Text = "Ridge"
        Me.optGbRidge.UseVisualStyleBackColor = True
        '
        'optGbPeaked
        '
        Me.optGbPeaked.AutoSize = True
        Me.optGbPeaked.ForeColor = System.Drawing.Color.Black
        Me.optGbPeaked.Location = New System.Drawing.Point(25, 44)
        Me.optGbPeaked.Name = "optGbPeaked"
        Me.optGbPeaked.Size = New System.Drawing.Size(62, 17)
        Me.optGbPeaked.TabIndex = 0
        Me.optGbPeaked.Text = "Peaked"
        Me.optGbPeaked.UseVisualStyleBackColor = True
        '
        'optGbFlat
        '
        Me.optGbFlat.AutoSize = True
        Me.optGbFlat.ForeColor = System.Drawing.Color.Black
        Me.optGbFlat.Location = New System.Drawing.Point(25, 21)
        Me.optGbFlat.Name = "optGbFlat"
        Me.optGbFlat.Size = New System.Drawing.Size(42, 17)
        Me.optGbFlat.TabIndex = 0
        Me.optGbFlat.Text = "Flat"
        Me.optGbFlat.UseVisualStyleBackColor = True
        '
        'frBottom
        '
        Me.frBottom.Controls.Add(Me.nUPsizeBottomY)
        Me.frBottom.Controls.Add(Me.lbBH)
        Me.frBottom.Controls.Add(Me.nUPbottomTexture)
        Me.frBottom.Controls.Add(Me.lbBT)
        Me.frBottom.Controls.Add(Me.nUPtextureIndexBottomX)
        Me.frBottom.Controls.Add(Me.Label40)
        Me.frBottom.Controls.Add(Me.lbBZT)
        Me.frBottom.Controls.Add(Me.nUPtextureIndexBottomZ)
        Me.frBottom.Controls.Add(Me.nUPsizeX)
        Me.frBottom.Controls.Add(Me.nUPsizeZ)
        Me.frBottom.Controls.Add(Me.lbBD)
        Me.frBottom.Controls.Add(Me.lbBW)
        Me.frBottom.Location = New System.Drawing.Point(22, 160)
        Me.frBottom.Name = "frBottom"
        Me.frBottom.Size = New System.Drawing.Size(283, 113)
        Me.frBottom.TabIndex = 75
        Me.frBottom.TabStop = False
        Me.frBottom.Text = "Bottom Section"
        '
        'lbBH
        '
        Me.lbBH.AutoSize = True
        Me.lbBH.Location = New System.Drawing.Point(137, 21)
        Me.lbBH.Name = "lbBH"
        Me.lbBH.Size = New System.Drawing.Size(48, 13)
        Me.lbBH.TabIndex = 76
        Me.lbBH.Text = "Height Y"
        '
        'lbBT
        '
        Me.lbBT.AutoSize = True
        Me.lbBT.Location = New System.Drawing.Point(11, 20)
        Me.lbBT.Name = "lbBT"
        Me.lbBT.Size = New System.Drawing.Size(43, 13)
        Me.lbBT.TabIndex = 76
        Me.lbBT.Text = "Texture"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(67, 62)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(42, 13)
        Me.Label40.TabIndex = 76
        Me.Label40.Text = "X Tiling"
        '
        'lbBZT
        '
        Me.lbBZT.AutoSize = True
        Me.lbBZT.Location = New System.Drawing.Point(207, 62)
        Me.lbBZT.Name = "lbBZT"
        Me.lbBZT.Size = New System.Drawing.Size(42, 13)
        Me.lbBZT.TabIndex = 76
        Me.lbBZT.Text = "Z Tiling"
        '
        'lbRH
        '
        Me.lbRH.AutoSize = True
        Me.lbRH.Location = New System.Drawing.Point(140, 21)
        Me.lbRH.Name = "lbRH"
        Me.lbRH.Size = New System.Drawing.Size(48, 13)
        Me.lbRH.TabIndex = 76
        Me.lbRH.Text = "Height Y"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(137, 20)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(48, 13)
        Me.Label31.TabIndex = 76
        Me.Label31.Text = "Height Y"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(140, 21)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(48, 13)
        Me.Label32.TabIndex = 76
        Me.Label32.Text = "Height Y"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(139, 61)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(42, 13)
        Me.Label34.TabIndex = 76
        Me.Label34.Text = "Y Tiling"
        '
        'lbWZT
        '
        Me.lbWZT.AutoSize = True
        Me.lbWZT.Location = New System.Drawing.Point(211, 62)
        Me.lbWZT.Name = "lbWZT"
        Me.lbWZT.Size = New System.Drawing.Size(42, 13)
        Me.lbWZT.TabIndex = 76
        Me.lbWZT.Text = "Z Tiling"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(67, 62)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(42, 13)
        Me.Label37.TabIndex = 76
        Me.Label37.Text = "X Tiling"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(11, 20)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(43, 13)
        Me.Label38.TabIndex = 76
        Me.Label38.Text = "Texture"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(14, 21)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(43, 13)
        Me.Label39.TabIndex = 76
        Me.Label39.Text = "Texture"
        '
        'frWindow
        '
        Me.frWindow.Controls.Add(Me.nUPwindowTexture)
        Me.frWindow.Controls.Add(Me.Label38)
        Me.frWindow.Controls.Add(Me.Label31)
        Me.frWindow.Controls.Add(Me.nUPsizeWindowY)
        Me.frWindow.Controls.Add(Me.lbWZT)
        Me.frWindow.Controls.Add(Me.nUPtextureIndexWindowX)
        Me.frWindow.Controls.Add(Me.Label34)
        Me.frWindow.Controls.Add(Me.Label37)
        Me.frWindow.Controls.Add(Me.nUPWZ)
        Me.frWindow.Controls.Add(Me.nUPWX)
        Me.frWindow.Controls.Add(Me.Label5)
        Me.frWindow.Controls.Add(Me.nUPtextureIndexWindowZ)
        Me.frWindow.Controls.Add(Me.nUPtextureIndexWindowY)
        Me.frWindow.Controls.Add(Me.Label1)
        Me.frWindow.Location = New System.Drawing.Point(22, 289)
        Me.frWindow.Name = "frWindow"
        Me.frWindow.Size = New System.Drawing.Size(283, 113)
        Me.frWindow.TabIndex = 77
        Me.frWindow.TabStop = False
        Me.frWindow.Text = "Window Section"
        '
        'nUPWZ
        '
        Me.nUPWZ.BackColor = System.Drawing.SystemColors.Window
        Me.nUPWZ.DecimalPlaces = 2
        Me.nUPWZ.Enabled = False
        Me.nUPWZ.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nUPWZ.Location = New System.Drawing.Point(213, 36)
        Me.nUPWZ.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nUPWZ.Name = "nUPWZ"
        Me.nUPWZ.Size = New System.Drawing.Size(55, 20)
        Me.nUPWZ.TabIndex = 68
        Me.nUPWZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPWZ.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nUPWX
        '
        Me.nUPWX.BackColor = System.Drawing.SystemColors.Window
        Me.nUPWX.DecimalPlaces = 2
        Me.nUPWX.Enabled = False
        Me.nUPWX.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nUPWX.Location = New System.Drawing.Point(73, 36)
        Me.nUPWX.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nUPWX.Name = "nUPWX"
        Me.nUPWX.Size = New System.Drawing.Size(55, 20)
        Me.nUPWX.TabIndex = 68
        Me.nUPWX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPWX.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Enabled = False
        Me.Label5.Location = New System.Drawing.Point(213, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 13)
        Me.Label5.TabIndex = 67
        Me.Label5.Text = "Deep Z"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Enabled = False
        Me.Label1.Location = New System.Drawing.Point(67, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 66
        Me.Label1.Text = "Width X"
        '
        'lbGYT
        '
        Me.lbGYT.AutoSize = True
        Me.lbGYT.Location = New System.Drawing.Point(146, 104)
        Me.lbGYT.Name = "lbGYT"
        Me.lbGYT.Size = New System.Drawing.Size(42, 13)
        Me.lbGYT.TabIndex = 76
        Me.lbGYT.Text = "Y Tiling"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(72, 62)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(42, 13)
        Me.Label43.TabIndex = 76
        Me.Label43.Text = "X Tiling"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(213, 62)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(42, 13)
        Me.Label44.TabIndex = 76
        Me.Label44.Text = "Z Tiling"
        '
        'frTop
        '
        Me.frTop.Controls.Add(Me.nUPtopTexture)
        Me.frTop.Controls.Add(Me.nUPsizeTopY)
        Me.frTop.Controls.Add(Me.Label45)
        Me.frTop.Controls.Add(Me.Label32)
        Me.frTop.Controls.Add(Me.nUPtextureIndexTopX)
        Me.frTop.Controls.Add(Me.nUPtextureIndexTopZ)
        Me.frTop.Controls.Add(Me.lbTZT)
        Me.frTop.Controls.Add(Me.Label8)
        Me.frTop.Controls.Add(Me.lbTD)
        Me.frTop.Controls.Add(Me.nUPsizeTopZ)
        Me.frTop.Controls.Add(Me.nUPsizeTopX)
        Me.frTop.Controls.Add(Me.lbTW)
        Me.frTop.Location = New System.Drawing.Point(19, 415)
        Me.frTop.Name = "frTop"
        Me.frTop.Size = New System.Drawing.Size(285, 113)
        Me.frTop.TabIndex = 79
        Me.frTop.TabStop = False
        Me.frTop.Text = "Top Section"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(12, 21)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(43, 13)
        Me.Label45.TabIndex = 76
        Me.Label45.Text = "Texture"
        '
        'lbTZT
        '
        Me.lbTZT.AutoSize = True
        Me.lbTZT.Location = New System.Drawing.Point(211, 62)
        Me.lbTZT.Name = "lbTZT"
        Me.lbTZT.Size = New System.Drawing.Size(42, 13)
        Me.lbTZT.TabIndex = 76
        Me.lbTZT.Text = "Z Tiling"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(70, 62)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(42, 13)
        Me.Label8.TabIndex = 76
        Me.Label8.Text = "X Tiling"
        '
        'lbTD
        '
        Me.lbTD.AutoSize = True
        Me.lbTD.Location = New System.Drawing.Point(210, 22)
        Me.lbTD.Name = "lbTD"
        Me.lbTD.Size = New System.Drawing.Size(43, 13)
        Me.lbTD.TabIndex = 67
        Me.lbTD.Text = "Deep Z"
        '
        'lbTW
        '
        Me.lbTW.AutoSize = True
        Me.lbTW.Location = New System.Drawing.Point(70, 22)
        Me.lbTW.Name = "lbTW"
        Me.lbTW.Size = New System.Drawing.Size(45, 13)
        Me.lbTW.TabIndex = 66
        Me.lbTW.Text = "Width X"
        '
        'lbGZT
        '
        Me.lbGZT.AutoSize = True
        Me.lbGZT.Location = New System.Drawing.Point(212, 106)
        Me.lbGZT.Name = "lbGZT"
        Me.lbGZT.Size = New System.Drawing.Size(42, 13)
        Me.lbGZT.TabIndex = 76
        Me.lbGZT.Text = "Z Tiling"
        '
        'frRoof
        '
        Me.frRoof.Controls.Add(Me.nUProofTexture)
        Me.frRoof.Controls.Add(Me.Label39)
        Me.frRoof.Controls.Add(Me.nUPtextureIndexRoofX)
        Me.frRoof.Controls.Add(Me.nUPtextureIndexRoofZ)
        Me.frRoof.Controls.Add(Me.Label44)
        Me.frRoof.Controls.Add(Me.Label43)
        Me.frRoof.Controls.Add(Me.lbGZT)
        Me.frRoof.Controls.Add(Me.nUPsizeRoofY)
        Me.frRoof.Controls.Add(Me.nUPRZ)
        Me.frRoof.Controls.Add(Me.nUPRX)
        Me.frRoof.Controls.Add(Me.lbFYT)
        Me.frRoof.Controls.Add(Me.Label2)
        Me.frRoof.Controls.Add(Me.lbGYT)
        Me.frRoof.Controls.Add(Me.Label3)
        Me.frRoof.Controls.Add(Me.lbFXT)
        Me.frRoof.Controls.Add(Me.lbRH)
        Me.frRoof.Controls.Add(Me.lbRYT)
        Me.frRoof.Controls.Add(Me.nUPtextureIndexRoofY)
        Me.frRoof.Controls.Add(Me.nUPgableTexture)
        Me.frRoof.Controls.Add(Me.lbG)
        Me.frRoof.Controls.Add(Me.nUPfaceTexture)
        Me.frRoof.Controls.Add(Me.lbF)
        Me.frRoof.Controls.Add(Me.nUPtextureIndexFaceY)
        Me.frRoof.Controls.Add(Me.nUPtextureIndexFaceX)
        Me.frRoof.Controls.Add(Me.nUPtextureIndexGableY)
        Me.frRoof.Controls.Add(Me.nUPtextureIndexGableZ)
        Me.frRoof.Location = New System.Drawing.Point(323, 14)
        Me.frRoof.Name = "frRoof"
        Me.frRoof.Size = New System.Drawing.Size(285, 196)
        Me.frRoof.TabIndex = 80
        Me.frRoof.TabStop = False
        Me.frRoof.Text = "Roof Section"
        '
        'nUPRZ
        '
        Me.nUPRZ.BackColor = System.Drawing.SystemColors.Window
        Me.nUPRZ.DecimalPlaces = 2
        Me.nUPRZ.Enabled = False
        Me.nUPRZ.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nUPRZ.Location = New System.Drawing.Point(211, 37)
        Me.nUPRZ.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nUPRZ.Name = "nUPRZ"
        Me.nUPRZ.Size = New System.Drawing.Size(55, 20)
        Me.nUPRZ.TabIndex = 68
        Me.nUPRZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPRZ.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nUPRX
        '
        Me.nUPRX.BackColor = System.Drawing.SystemColors.Window
        Me.nUPRX.DecimalPlaces = 2
        Me.nUPRX.Enabled = False
        Me.nUPRX.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nUPRX.Location = New System.Drawing.Point(75, 37)
        Me.nUPRX.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nUPRX.Name = "nUPRX"
        Me.nUPRX.Size = New System.Drawing.Size(55, 20)
        Me.nUPRX.TabIndex = 68
        Me.nUPRX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPRX.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'lbFYT
        '
        Me.lbFYT.AutoSize = True
        Me.lbFYT.Location = New System.Drawing.Point(144, 146)
        Me.lbFYT.Name = "lbFYT"
        Me.lbFYT.Size = New System.Drawing.Size(42, 13)
        Me.lbFYT.TabIndex = 76
        Me.lbFYT.Text = "Y Tiling"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Enabled = False
        Me.Label2.Location = New System.Drawing.Point(211, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 67
        Me.Label2.Text = "Deep Z"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Enabled = False
        Me.Label3.Location = New System.Drawing.Point(70, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 66
        Me.Label3.Text = "Width X"
        '
        'lbFXT
        '
        Me.lbFXT.AutoSize = True
        Me.lbFXT.Location = New System.Drawing.Point(72, 146)
        Me.lbFXT.Name = "lbFXT"
        Me.lbFXT.Size = New System.Drawing.Size(42, 13)
        Me.lbFXT.TabIndex = 76
        Me.lbFXT.Text = "X Tiling"
        '
        'lbRYT
        '
        Me.lbRYT.AutoSize = True
        Me.lbRYT.Location = New System.Drawing.Point(140, 62)
        Me.lbRYT.Name = "lbRYT"
        Me.lbRYT.Size = New System.Drawing.Size(42, 13)
        Me.lbRYT.TabIndex = 76
        Me.lbRYT.Text = "Y Tiling"
        '
        'frMulti
        '
        Me.frMulti.Controls.Add(Me.ckSmoothing)
        Me.frMulti.Controls.Add(Me.nUPbuildingSides)
        Me.frMulti.Controls.Add(Me.lbSides)
        Me.frMulti.Location = New System.Drawing.Point(107, 99)
        Me.frMulti.Name = "frMulti"
        Me.frMulti.Size = New System.Drawing.Size(197, 55)
        Me.frMulti.TabIndex = 81
        Me.frMulti.TabStop = False
        Me.frMulti.Text = "MultiSided Building"
        '
        'nUPGrid
        '
        Me.nUPGrid.BackColor = System.Drawing.SystemColors.Window
        Me.nUPGrid.DecimalPlaces = 1
        Me.nUPGrid.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.nUPGrid.Location = New System.Drawing.Point(381, 505)
        Me.nUPGrid.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nUPGrid.Name = "nUPGrid"
        Me.nUPGrid.Size = New System.Drawing.Size(53, 20)
        Me.nUPGrid.TabIndex = 82
        Me.nUPGrid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nUPGrid.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(317, 507)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 16)
        Me.Label4.TabIndex = 83
        Me.Label4.Text = "Grid meters"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(322, 222)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(269, 13)
        Me.Label6.TabIndex = 84
        Me.Label6.Text = "Right mouse to enlarge. Wheel and left mouse to move."
        '
        'frmGBuilding
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(628, 547)
        Me.ControlBox = False
        Me.Controls.Add(Me.imgGenB)
        Me.Controls.Add(Me.frMulti)
        Me.Controls.Add(Me.lbgb3)
        Me.Controls.Add(Me.frRoof)
        Me.Controls.Add(Me.frTop)
        Me.Controls.Add(Me.frWindow)
        Me.Controls.Add(Me.frBottom)
        Me.Controls.Add(Me.frGenB)
        Me.Controls.Add(Me.nUPscale)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.nUPGrid)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label6)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGBuilding"
        Me.Text = "SBuilderX - Generic Buildings"
        CType(Me.nUPsizeBottomY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPsizeWindowY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPsizeTopY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPsizeRoofY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPsizeTopX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPsizeTopZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPtextureIndexBottomX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPtextureIndexBottomZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPtextureIndexWindowX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPtextureIndexWindowY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPtextureIndexWindowZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPtextureIndexTopX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPtextureIndexTopZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPtextureIndexRoofX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPtextureIndexRoofY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPtextureIndexRoofZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPbuildingSides, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPgableTexture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPfaceTexture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPtextureIndexGableY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPtextureIndexFaceX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPtextureIndexGableZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPtextureIndexFaceY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPsizeX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPsizeZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPscale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPtopTexture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPbottomTexture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUProofTexture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPwindowTexture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgGenB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.frGenB.ResumeLayout(False)
        Me.frGenB.PerformLayout()
        Me.frBottom.ResumeLayout(False)
        Me.frBottom.PerformLayout()
        Me.frWindow.ResumeLayout(False)
        Me.frWindow.PerformLayout()
        CType(Me.nUPWZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPWX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.frTop.ResumeLayout(False)
        Me.frTop.PerformLayout()
        Me.frRoof.ResumeLayout(False)
        Me.frRoof.PerformLayout()
        CType(Me.nUPRZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nUPRX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.frMulti.ResumeLayout(False)
        Me.frMulti.PerformLayout()
        CType(Me.nUPGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents nUPsizeBottomY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPsizeWindowY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPsizeTopY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPsizeRoofY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPsizeTopX As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPsizeTopZ As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPtextureIndexBottomX As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPtextureIndexBottomZ As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPtextureIndexWindowX As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPtextureIndexWindowY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPtextureIndexWindowZ As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPtextureIndexTopX As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPtextureIndexTopZ As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPtextureIndexRoofX As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPtextureIndexRoofY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPtextureIndexRoofZ As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPbuildingSides As System.Windows.Forms.NumericUpDown
    Friend WithEvents ckSmoothing As System.Windows.Forms.CheckBox
    Friend WithEvents lbSides As System.Windows.Forms.Label
    Friend WithEvents lbG As System.Windows.Forms.Label
    Friend WithEvents lbF As System.Windows.Forms.Label
    Friend WithEvents nUPgableTexture As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPfaceTexture As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPtextureIndexGableY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPtextureIndexFaceX As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPtextureIndexGableZ As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPtextureIndexFaceY As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents lbgb3 As System.Windows.Forms.Label
    Friend WithEvents lbBW As System.Windows.Forms.Label
    Friend WithEvents lbBD As System.Windows.Forms.Label
    Friend WithEvents nUPsizeX As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPsizeZ As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPscale As System.Windows.Forms.NumericUpDown
    Private WithEvents nUPtopTexture As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPbottomTexture As System.Windows.Forms.NumericUpDown
    Private WithEvents nUProofTexture As System.Windows.Forms.NumericUpDown
    Private WithEvents nUPwindowTexture As System.Windows.Forms.NumericUpDown
    Friend WithEvents imgGenB As System.Windows.Forms.PictureBox
    Public WithEvents frGenB As System.Windows.Forms.GroupBox
    Friend WithEvents optGbMultiSided As System.Windows.Forms.RadioButton
    Friend WithEvents optGbPyramidal As System.Windows.Forms.RadioButton
    Friend WithEvents optGbSlant As System.Windows.Forms.RadioButton
    Friend WithEvents optGbRidge As System.Windows.Forms.RadioButton
    Friend WithEvents optGbPeaked As System.Windows.Forms.RadioButton
    Friend WithEvents optGbFlat As System.Windows.Forms.RadioButton
    Friend WithEvents frBottom As System.Windows.Forms.GroupBox
    Friend WithEvents lbRH As System.Windows.Forms.Label
    Friend WithEvents lbBH As System.Windows.Forms.Label
    Friend WithEvents lbBT As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents lbWZT As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents lbBZT As System.Windows.Forms.Label
    Friend WithEvents frWindow As System.Windows.Forms.GroupBox
    Friend WithEvents lbGYT As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents frTop As System.Windows.Forms.GroupBox
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lbTZT As System.Windows.Forms.Label
    Friend WithEvents lbGZT As System.Windows.Forms.Label
    Friend WithEvents lbTW As System.Windows.Forms.Label
    Friend WithEvents lbTD As System.Windows.Forms.Label
    Friend WithEvents frRoof As System.Windows.Forms.GroupBox
    Friend WithEvents lbRYT As System.Windows.Forms.Label
    Friend WithEvents lbFXT As System.Windows.Forms.Label
    Friend WithEvents lbFYT As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents nUPWZ As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPWX As System.Windows.Forms.NumericUpDown
    Friend WithEvents frMulti As System.Windows.Forms.GroupBox
    Friend WithEvents nUPRZ As System.Windows.Forms.NumericUpDown
    Friend WithEvents nUPRX As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents nUPGrid As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label

    Private Sub FrmGBuilding_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        vertexBuffer.Dispose()
    End Sub
End Class
