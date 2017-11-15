<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLibrary
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLibrary))
        Me.lstBGL = New System.Windows.Forms.ListBox
        Me.cmbLibCat = New System.Windows.Forms.ComboBox
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.lstLib = New System.Windows.Forms.ListBox
        Me.labelFS = New System.Windows.Forms.Label
        Me.frLib = New System.Windows.Forms.GroupBox
        Me.txtLibName = New System.Windows.Forms.TextBox
        Me.txtLibWidth = New System.Windows.Forms.TextBox
        Me.txtLibLength = New System.Windows.Forms.TextBox
        Me.cmdUpdate = New System.Windows.Forms.Button
        Me.txtLibScale = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.LabelLib2 = New System.Windows.Forms.Label
        Me.LabelLib1 = New System.Windows.Forms.Label
        Me.LabelLib3 = New System.Windows.Forms.Label
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdOK = New System.Windows.Forms.Button
        Me.txtBGLFile = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.labelFSTemp = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmdNewCat = New System.Windows.Forms.Button
        Me.txtLibID = New System.Windows.Forms.TextBox
        Me.txtBGLID = New System.Windows.Forms.TextBox
        Me.cmdBGL = New System.Windows.Forms.Button
        Me.cmdDown = New System.Windows.Forms.Button
        Me.cmdUP = New System.Windows.Forms.Button
        Me.imgLib = New System.Windows.Forms.PictureBox
        Me.cmdRemove = New System.Windows.Forms.Button
        Me.LabelNoBGLs = New System.Windows.Forms.Label
        Me.LabelNoLIBs = New System.Windows.Forms.Label
        Me.cmdAZ = New System.Windows.Forms.Button
        Me.cmdEditCat = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.frLib.SuspendLayout()
        CType(Me.imgLib, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lstBGL
        '
        Me.lstBGL.FormattingEnabled = True
        Me.lstBGL.Location = New System.Drawing.Point(504, 54)
        Me.lstBGL.Name = "lstBGL"
        Me.lstBGL.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstBGL.Size = New System.Drawing.Size(224, 147)
        Me.lstBGL.TabIndex = 3
        '
        'cmbLibCat
        '
        Me.cmbLibCat.FormattingEnabled = True
        Me.cmbLibCat.Location = New System.Drawing.Point(235, 27)
        Me.cmbLibCat.Name = "cmbLibCat"
        Me.cmbLibCat.Size = New System.Drawing.Size(224, 21)
        Me.cmbLibCat.TabIndex = 4
        '
        'cmdAdd
        '
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.Location = New System.Drawing.Point(465, 178)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(33, 23)
        Me.cmdAdd.TabIndex = 5
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'lstLib
        '
        Me.lstLib.FormattingEnabled = True
        Me.lstLib.Location = New System.Drawing.Point(235, 54)
        Me.lstLib.Name = "lstLib"
        Me.lstLib.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstLib.Size = New System.Drawing.Size(224, 147)
        Me.lstLib.TabIndex = 6
        '
        'labelFS
        '
        Me.labelFS.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.labelFS.AutoSize = True
        Me.labelFS.BackColor = System.Drawing.Color.Transparent
        Me.labelFS.Cursor = System.Windows.Forms.Cursors.Default
        Me.labelFS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labelFS.Location = New System.Drawing.Point(306, 211)
        Me.labelFS.Name = "labelFS"
        Me.labelFS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labelFS.Size = New System.Drawing.Size(153, 13)
        Me.labelFS.TabIndex = 103
        Me.labelFS.Text = "Categorized FS9 Library Object"
        Me.labelFS.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frLib
        '
        Me.frLib.BackColor = System.Drawing.Color.Transparent
        Me.frLib.Controls.Add(Me.txtLibName)
        Me.frLib.Controls.Add(Me.txtLibWidth)
        Me.frLib.Controls.Add(Me.txtLibLength)
        Me.frLib.Controls.Add(Me.cmdUpdate)
        Me.frLib.Controls.Add(Me.txtLibScale)
        Me.frLib.Controls.Add(Me.Label2)
        Me.frLib.Controls.Add(Me.LabelLib2)
        Me.frLib.Controls.Add(Me.LabelLib1)
        Me.frLib.Controls.Add(Me.LabelLib3)
        Me.frLib.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.frLib.Location = New System.Drawing.Point(12, 11)
        Me.frLib.Name = "frLib"
        Me.frLib.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frLib.Size = New System.Drawing.Size(208, 91)
        Me.frLib.TabIndex = 102
        Me.frLib.TabStop = False
        Me.frLib.Text = "Change Name Scale and Footprint"
        '
        'txtLibName
        '
        Me.txtLibName.AcceptsReturn = True
        Me.txtLibName.BackColor = System.Drawing.SystemColors.Window
        Me.txtLibName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLibName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLibName.Location = New System.Drawing.Point(54, 63)
        Me.txtLibName.MaxLength = 0
        Me.txtLibName.Name = "txtLibName"
        Me.txtLibName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLibName.Size = New System.Drawing.Size(140, 20)
        Me.txtLibName.TabIndex = 106
        Me.txtLibName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtLibWidth
        '
        Me.txtLibWidth.AcceptsReturn = True
        Me.txtLibWidth.BackColor = System.Drawing.SystemColors.Window
        Me.txtLibWidth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLibWidth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLibWidth.Location = New System.Drawing.Point(70, 32)
        Me.txtLibWidth.MaxLength = 0
        Me.txtLibWidth.Name = "txtLibWidth"
        Me.txtLibWidth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLibWidth.Size = New System.Drawing.Size(44, 20)
        Me.txtLibWidth.TabIndex = 37
        Me.txtLibWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtLibLength
        '
        Me.txtLibLength.AcceptsReturn = True
        Me.txtLibLength.BackColor = System.Drawing.SystemColors.Window
        Me.txtLibLength.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLibLength.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLibLength.Location = New System.Drawing.Point(16, 32)
        Me.txtLibLength.MaxLength = 0
        Me.txtLibLength.Name = "txtLibLength"
        Me.txtLibLength.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLibLength.Size = New System.Drawing.Size(45, 20)
        Me.txtLibLength.TabIndex = 36
        Me.txtLibLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdUpdate
        '
        Me.cmdUpdate.Enabled = False
        Me.cmdUpdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdate.Location = New System.Drawing.Point(163, 29)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(31, 23)
        Me.cmdUpdate.TabIndex = 105
        Me.cmdUpdate.Text = "OK"
        Me.cmdUpdate.UseVisualStyleBackColor = True
        '
        'txtLibScale
        '
        Me.txtLibScale.AcceptsReturn = True
        Me.txtLibScale.BackColor = System.Drawing.SystemColors.Window
        Me.txtLibScale.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLibScale.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLibScale.Location = New System.Drawing.Point(123, 32)
        Me.txtLibScale.MaxLength = 0
        Me.txtLibScale.Name = "txtLibScale"
        Me.txtLibScale.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLibScale.Size = New System.Drawing.Size(29, 20)
        Me.txtLibScale.TabIndex = 34
        Me.txtLibScale.Text = "1"
        Me.txtLibScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(13, 66)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(51, 19)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "Name:"
        '
        'LabelLib2
        '
        Me.LabelLib2.BackColor = System.Drawing.Color.Transparent
        Me.LabelLib2.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelLib2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelLib2.Location = New System.Drawing.Point(13, 17)
        Me.LabelLib2.Name = "LabelLib2"
        Me.LabelLib2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelLib2.Size = New System.Drawing.Size(51, 19)
        Me.LabelLib2.TabIndex = 39
        Me.LabelLib2.Text = "Length:"
        '
        'LabelLib1
        '
        Me.LabelLib1.BackColor = System.Drawing.Color.Transparent
        Me.LabelLib1.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelLib1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelLib1.Location = New System.Drawing.Point(67, 17)
        Me.LabelLib1.Name = "LabelLib1"
        Me.LabelLib1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelLib1.Size = New System.Drawing.Size(38, 18)
        Me.LabelLib1.TabIndex = 38
        Me.LabelLib1.Text = "Width:"
        '
        'LabelLib3
        '
        Me.LabelLib3.BackColor = System.Drawing.Color.Transparent
        Me.LabelLib3.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelLib3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelLib3.Location = New System.Drawing.Point(120, 17)
        Me.LabelLib3.Name = "LabelLib3"
        Me.LabelLib3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelLib3.Size = New System.Drawing.Size(37, 16)
        Me.LabelLib3.TabIndex = 35
        Me.LabelLib3.Text = "Scale:"
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(600, 262)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(55, 23)
        Me.cmdCancel.TabIndex = 104
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(673, 262)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(55, 23)
        Me.cmdOK.TabIndex = 105
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'txtBGLFile
        '
        Me.txtBGLFile.AcceptsReturn = True
        Me.txtBGLFile.BackColor = System.Drawing.SystemColors.Window
        Me.txtBGLFile.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBGLFile.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBGLFile.Location = New System.Drawing.Point(504, 27)
        Me.txtBGLFile.MaxLength = 0
        Me.txtBGLFile.Name = "txtBGLFile"
        Me.txtBGLFile.ReadOnly = True
        Me.txtBGLFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBGLFile.Size = New System.Drawing.Size(181, 20)
        Me.txtBGLFile.TabIndex = 109
        Me.txtBGLFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(501, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(179, 13)
        Me.Label1.TabIndex = 103
        Me.Label1.Text = "Get Temporary Objects from BGL file"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'labelFSTemp
        '
        Me.labelFSTemp.AutoSize = True
        Me.labelFSTemp.BackColor = System.Drawing.Color.Transparent
        Me.labelFSTemp.Cursor = System.Windows.Forms.Cursors.Default
        Me.labelFSTemp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labelFSTemp.Location = New System.Drawing.Point(581, 211)
        Me.labelFSTemp.Name = "labelFSTemp"
        Me.labelFSTemp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labelFSTemp.Size = New System.Drawing.Size(147, 13)
        Me.labelFSTemp.TabIndex = 103
        Me.labelFSTemp.Text = "Temporary FS9 Library Object"
        Me.labelFSTemp.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(363, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(96, 13)
        Me.Label3.TabIndex = 103
        Me.Label3.Text = "Existing Categories"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdNewCat
        '
        Me.cmdNewCat.Location = New System.Drawing.Point(405, 262)
        Me.cmdNewCat.Name = "cmdNewCat"
        Me.cmdNewCat.Size = New System.Drawing.Size(54, 23)
        Me.cmdNewCat.TabIndex = 5
        Me.cmdNewCat.Text = "New"
        Me.cmdNewCat.UseVisualStyleBackColor = True
        '
        'txtLibID
        '
        Me.txtLibID.AcceptsReturn = True
        Me.txtLibID.BackColor = System.Drawing.SystemColors.Window
        Me.txtLibID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLibID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLibID.Location = New System.Drawing.Point(235, 227)
        Me.txtLibID.MaxLength = 0
        Me.txtLibID.Name = "txtLibID"
        Me.txtLibID.ReadOnly = True
        Me.txtLibID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLibID.Size = New System.Drawing.Size(224, 20)
        Me.txtLibID.TabIndex = 110
        Me.txtLibID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtBGLID
        '
        Me.txtBGLID.AcceptsReturn = True
        Me.txtBGLID.BackColor = System.Drawing.SystemColors.Window
        Me.txtBGLID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBGLID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBGLID.Location = New System.Drawing.Point(504, 227)
        Me.txtBGLID.MaxLength = 0
        Me.txtBGLID.Name = "txtBGLID"
        Me.txtBGLID.ReadOnly = True
        Me.txtBGLID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBGLID.Size = New System.Drawing.Size(224, 20)
        Me.txtBGLID.TabIndex = 110
        Me.txtBGLID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdBGL
        '
        Me.cmdBGL.Location = New System.Drawing.Point(691, 25)
        Me.cmdBGL.Name = "cmdBGL"
        Me.cmdBGL.Size = New System.Drawing.Size(37, 23)
        Me.cmdBGL.TabIndex = 5
        Me.cmdBGL.Text = "..."
        Me.cmdBGL.UseVisualStyleBackColor = True
        '
        'cmdDown
        '
        Me.cmdDown.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDown.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDown.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDown.Image = CType(resources.GetObject("cmdDown.Image"), System.Drawing.Image)
        Me.cmdDown.Location = New System.Drawing.Point(465, 116)
        Me.cmdDown.Name = "cmdDown"
        Me.cmdDown.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDown.Size = New System.Drawing.Size(25, 25)
        Me.cmdDown.TabIndex = 111
        Me.cmdDown.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdDown.UseVisualStyleBackColor = False
        '
        'cmdUP
        '
        Me.cmdUP.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUP.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUP.Image = CType(resources.GetObject("cmdUP.Image"), System.Drawing.Image)
        Me.cmdUP.Location = New System.Drawing.Point(465, 85)
        Me.cmdUP.Name = "cmdUP"
        Me.cmdUP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUP.Size = New System.Drawing.Size(25, 25)
        Me.cmdUP.TabIndex = 111
        Me.cmdUP.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdUP.UseVisualStyleBackColor = False
        '
        'imgLib
        '
        Me.imgLib.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgLib.Location = New System.Drawing.Point(12, 116)
        Me.imgLib.Name = "imgLib"
        Me.imgLib.Size = New System.Drawing.Size(208, 169)
        Me.imgLib.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgLib.TabIndex = 100
        Me.imgLib.TabStop = False
        '
        'cmdRemove
        '
        Me.cmdRemove.Image = CType(resources.GetObject("cmdRemove.Image"), System.Drawing.Image)
        Me.cmdRemove.Location = New System.Drawing.Point(465, 147)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(33, 23)
        Me.cmdRemove.TabIndex = 5
        Me.cmdRemove.UseVisualStyleBackColor = True
        '
        'LabelNoBGLs
        '
        Me.LabelNoBGLs.AutoSize = True
        Me.LabelNoBGLs.Location = New System.Drawing.Point(501, 206)
        Me.LabelNoBGLs.Name = "LabelNoBGLs"
        Me.LabelNoBGLs.Size = New System.Drawing.Size(33, 13)
        Me.LabelNoBGLs.TabIndex = 112
        Me.LabelNoBGLs.Text = "BGLs"
        '
        'LabelNoLIBs
        '
        Me.LabelNoLIBs.AutoSize = True
        Me.LabelNoLIBs.Location = New System.Drawing.Point(232, 206)
        Me.LabelNoLIBs.Name = "LabelNoLIBs"
        Me.LabelNoLIBs.Size = New System.Drawing.Size(28, 13)
        Me.LabelNoLIBs.TabIndex = 112
        Me.LabelNoLIBs.Text = "LIBs"
        '
        'cmdAZ
        '
        Me.cmdAZ.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAZ.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAZ.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAZ.Image = CType(resources.GetObject("cmdAZ.Image"), System.Drawing.Image)
        Me.cmdAZ.Location = New System.Drawing.Point(465, 54)
        Me.cmdAZ.Name = "cmdAZ"
        Me.cmdAZ.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAZ.Size = New System.Drawing.Size(25, 25)
        Me.cmdAZ.TabIndex = 111
        Me.cmdAZ.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdAZ.UseVisualStyleBackColor = False
        '
        'cmdEditCat
        '
        Me.cmdEditCat.Location = New System.Drawing.Point(345, 262)
        Me.cmdEditCat.Name = "cmdEditCat"
        Me.cmdEditCat.Size = New System.Drawing.Size(54, 23)
        Me.cmdEditCat.TabIndex = 5
        Me.cmdEditCat.Text = "Name"
        Me.cmdEditCat.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(214, 255)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(128, 30)
        Me.Label4.TabIndex = 113
        Me.Label4.Text = "Change the Name or create a New category"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmLibrary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(744, 299)
        Me.Controls.Add(Me.LabelNoLIBs)
        Me.Controls.Add(Me.LabelNoBGLs)
        Me.Controls.Add(Me.cmdDown)
        Me.Controls.Add(Me.cmdAZ)
        Me.Controls.Add(Me.cmdUP)
        Me.Controls.Add(Me.txtBGLID)
        Me.Controls.Add(Me.txtLibID)
        Me.Controls.Add(Me.txtBGLFile)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.imgLib)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.labelFSTemp)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.labelFS)
        Me.Controls.Add(Me.frLib)
        Me.Controls.Add(Me.lstLib)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.cmdBGL)
        Me.Controls.Add(Me.cmdEditCat)
        Me.Controls.Add(Me.cmdNewCat)
        Me.Controls.Add(Me.cmdRemove)
        Me.Controls.Add(Me.cmbLibCat)
        Me.Controls.Add(Me.lstBGL)
        Me.Controls.Add(Me.Label4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLibrary"
        Me.Text = "SBuilderX - Object Library Manager"
        Me.frLib.ResumeLayout(False)
        Me.frLib.PerformLayout()
        CType(Me.imgLib, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstBGL As System.Windows.Forms.ListBox
    Friend WithEvents cmbLibCat As System.Windows.Forms.ComboBox
    Friend WithEvents cmdRemove As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents lstLib As System.Windows.Forms.ListBox
    Public WithEvents imgLib As System.Windows.Forms.PictureBox
    Public WithEvents labelFS As System.Windows.Forms.Label
    Public WithEvents frLib As System.Windows.Forms.GroupBox
    Public WithEvents txtLibWidth As System.Windows.Forms.TextBox
    Public WithEvents txtLibLength As System.Windows.Forms.TextBox
    Public WithEvents txtLibScale As System.Windows.Forms.TextBox
    Public WithEvents LabelLib1 As System.Windows.Forms.Label
    Public WithEvents LabelLib3 As System.Windows.Forms.Label
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents txtBGLFile As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents labelFSTemp As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdNewCat As System.Windows.Forms.Button
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Public WithEvents txtLibID As System.Windows.Forms.TextBox
    Public WithEvents txtBGLID As System.Windows.Forms.TextBox
    Friend WithEvents cmdBGL As System.Windows.Forms.Button
    Public WithEvents cmdUP As System.Windows.Forms.Button
    Public WithEvents cmdDown As System.Windows.Forms.Button
    Friend WithEvents LabelNoBGLs As System.Windows.Forms.Label
    Friend WithEvents LabelNoLIBs As System.Windows.Forms.Label
    Public WithEvents cmdAZ As System.Windows.Forms.Button
    Public WithEvents txtLibName As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents LabelLib2 As System.Windows.Forms.Label
    Friend WithEvents cmdEditCat As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
