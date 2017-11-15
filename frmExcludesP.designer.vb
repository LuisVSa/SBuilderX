<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmExcludesP
#Region "Windows Form Designer generated code "
    <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
    Public WithEvents txtSouth As System.Windows.Forms.TextBox
	Public WithEvents txtEast As System.Windows.Forms.TextBox
	Public WithEvents txtNorth As System.Windows.Forms.TextBox
	Public WithEvents txtWest As System.Windows.Forms.TextBox
	Public WithEvents ckWind As System.Windows.Forms.CheckBox
	Public WithEvents ckTrigger As System.Windows.Forms.CheckBox
	Public WithEvents ckTaxi As System.Windows.Forms.CheckBox
	Public WithEvents ckLibrary As System.Windows.Forms.CheckBox
	Public WithEvents ckGenBuilds As System.Windows.Forms.CheckBox
	Public WithEvents ckEffects As System.Windows.Forms.CheckBox
	Public WithEvents ckBeacons As System.Windows.Forms.CheckBox
	Public WithEvents ckAll As System.Windows.Forms.CheckBox
	Public WithEvents cmdCancel As System.Windows.Forms.Button
	Public WithEvents cmdOK As System.Windows.Forms.Button
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExcludesP))
        Me.txtSouth = New System.Windows.Forms.TextBox
        Me.txtEast = New System.Windows.Forms.TextBox
        Me.txtNorth = New System.Windows.Forms.TextBox
        Me.txtWest = New System.Windows.Forms.TextBox
        Me.ckWind = New System.Windows.Forms.CheckBox
        Me.ckTrigger = New System.Windows.Forms.CheckBox
        Me.ckTaxi = New System.Windows.Forms.CheckBox
        Me.ckLibrary = New System.Windows.Forms.CheckBox
        Me.ckGenBuilds = New System.Windows.Forms.CheckBox
        Me.ckEffects = New System.Windows.Forms.CheckBox
        Me.ckBeacons = New System.Windows.Forms.CheckBox
        Me.ckAll = New System.Windows.Forms.CheckBox
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdOK = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.ckBridges = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'txtSouth
        '
        Me.txtSouth.AcceptsReturn = True
        Me.txtSouth.BackColor = System.Drawing.SystemColors.Window
        Me.txtSouth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSouth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSouth.Location = New System.Drawing.Point(182, 143)
        Me.txtSouth.MaxLength = 0
        Me.txtSouth.Name = "txtSouth"
        Me.txtSouth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSouth.Size = New System.Drawing.Size(110, 20)
        Me.txtSouth.TabIndex = 15
        Me.txtSouth.Text = "Text1"
        Me.txtSouth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtEast
        '
        Me.txtEast.AcceptsReturn = True
        Me.txtEast.BackColor = System.Drawing.SystemColors.Window
        Me.txtEast.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEast.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEast.Location = New System.Drawing.Point(182, 191)
        Me.txtEast.MaxLength = 0
        Me.txtEast.Name = "txtEast"
        Me.txtEast.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEast.Size = New System.Drawing.Size(110, 20)
        Me.txtEast.TabIndex = 14
        Me.txtEast.Text = "Text1"
        Me.txtEast.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtNorth
        '
        Me.txtNorth.AcceptsReturn = True
        Me.txtNorth.BackColor = System.Drawing.SystemColors.Window
        Me.txtNorth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNorth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNorth.Location = New System.Drawing.Point(27, 40)
        Me.txtNorth.MaxLength = 0
        Me.txtNorth.Name = "txtNorth"
        Me.txtNorth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNorth.Size = New System.Drawing.Size(110, 20)
        Me.txtNorth.TabIndex = 11
        Me.txtNorth.Text = "Text1"
        Me.txtNorth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtWest
        '
        Me.txtWest.AcceptsReturn = True
        Me.txtWest.BackColor = System.Drawing.SystemColors.Window
        Me.txtWest.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWest.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWest.Location = New System.Drawing.Point(27, 88)
        Me.txtWest.MaxLength = 0
        Me.txtWest.Name = "txtWest"
        Me.txtWest.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWest.Size = New System.Drawing.Size(110, 20)
        Me.txtWest.TabIndex = 10
        Me.txtWest.Text = "Text1"
        Me.txtWest.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ckWind
        '
        Me.ckWind.AutoSize = True
        Me.ckWind.BackColor = System.Drawing.SystemColors.Control
        Me.ckWind.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckWind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckWind.Location = New System.Drawing.Point(182, 90)
        Me.ckWind.Name = "ckWind"
        Me.ckWind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckWind.Size = New System.Drawing.Size(113, 17)
        Me.ckWind.TabIndex = 9
        Me.ckWind.Text = "Windsock Objects"
        Me.ckWind.UseVisualStyleBackColor = False
        '
        'ckTrigger
        '
        Me.ckTrigger.AutoSize = True
        Me.ckTrigger.BackColor = System.Drawing.SystemColors.Control
        Me.ckTrigger.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckTrigger.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckTrigger.Location = New System.Drawing.Point(182, 64)
        Me.ckTrigger.Name = "ckTrigger"
        Me.ckTrigger.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckTrigger.Size = New System.Drawing.Size(98, 17)
        Me.ckTrigger.TabIndex = 8
        Me.ckTrigger.Text = "Trigger Objects"
        Me.ckTrigger.UseVisualStyleBackColor = False
        '
        'ckTaxi
        '
        Me.ckTaxi.AutoSize = True
        Me.ckTaxi.BackColor = System.Drawing.SystemColors.Control
        Me.ckTaxi.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckTaxi.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckTaxi.Location = New System.Drawing.Point(182, 38)
        Me.ckTaxi.Name = "ckTaxi"
        Me.ckTaxi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckTaxi.Size = New System.Drawing.Size(125, 17)
        Me.ckTaxi.TabIndex = 7
        Me.ckTaxi.Text = "TaxiwaySign Objects"
        Me.ckTaxi.UseVisualStyleBackColor = False
        '
        'ckLibrary
        '
        Me.ckLibrary.AutoSize = True
        Me.ckLibrary.BackColor = System.Drawing.SystemColors.Control
        Me.ckLibrary.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckLibrary.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckLibrary.Location = New System.Drawing.Point(182, 12)
        Me.ckLibrary.Name = "ckLibrary"
        Me.ckLibrary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckLibrary.Size = New System.Drawing.Size(96, 17)
        Me.ckLibrary.TabIndex = 6
        Me.ckLibrary.Text = "Library Objects"
        Me.ckLibrary.UseVisualStyleBackColor = False
        '
        'ckGenBuilds
        '
        Me.ckGenBuilds.AutoSize = True
        Me.ckGenBuilds.BackColor = System.Drawing.SystemColors.Control
        Me.ckGenBuilds.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckGenBuilds.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckGenBuilds.Location = New System.Drawing.Point(30, 205)
        Me.ckGenBuilds.Name = "ckGenBuilds"
        Me.ckGenBuilds.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckGenBuilds.Size = New System.Drawing.Size(108, 17)
        Me.ckGenBuilds.TabIndex = 5
        Me.ckGenBuilds.Text = "Generic Buildings"
        Me.ckGenBuilds.UseVisualStyleBackColor = False
        '
        'ckEffects
        '
        Me.ckEffects.AutoSize = True
        Me.ckEffects.BackColor = System.Drawing.SystemColors.Control
        Me.ckEffects.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckEffects.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckEffects.Location = New System.Drawing.Point(30, 179)
        Me.ckEffects.Name = "ckEffects"
        Me.ckEffects.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckEffects.Size = New System.Drawing.Size(93, 17)
        Me.ckEffects.TabIndex = 4
        Me.ckEffects.Text = "Effect Objects"
        Me.ckEffects.UseVisualStyleBackColor = False
        '
        'ckBeacons
        '
        Me.ckBeacons.AutoSize = True
        Me.ckBeacons.BackColor = System.Drawing.SystemColors.Control
        Me.ckBeacons.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckBeacons.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckBeacons.Location = New System.Drawing.Point(30, 153)
        Me.ckBeacons.Name = "ckBeacons"
        Me.ckBeacons.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckBeacons.Size = New System.Drawing.Size(102, 17)
        Me.ckBeacons.TabIndex = 3
        Me.ckBeacons.Text = "Beacon Objects"
        Me.ckBeacons.UseVisualStyleBackColor = False
        '
        'ckAll
        '
        Me.ckAll.AutoSize = True
        Me.ckAll.BackColor = System.Drawing.SystemColors.Control
        Me.ckAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckAll.Location = New System.Drawing.Point(30, 127)
        Me.ckAll.Name = "ckAll"
        Me.ckAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckAll.Size = New System.Drawing.Size(76, 17)
        Me.ckAll.TabIndex = 2
        Me.ckAll.Text = "All Objects"
        Me.ckAll.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(182, 233)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(49, 25)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(243, 233)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(49, 25)
        Me.cmdOK.TabIndex = 0
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(182, 176)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(109, 22)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "East Boundary :"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(182, 128)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(109, 22)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "South Boundary :"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(27, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(112, 22)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "West Boundary :"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(27, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(106, 22)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "North Boundary :"
        '
        'ckBridges
        '
        Me.ckBridges.AutoSize = True
        Me.ckBridges.BackColor = System.Drawing.SystemColors.Control
        Me.ckBridges.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckBridges.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckBridges.Location = New System.Drawing.Point(30, 231)
        Me.ckBridges.Name = "ckBridges"
        Me.ckBridges.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckBridges.Size = New System.Drawing.Size(107, 17)
        Me.ckBridges.TabIndex = 18
        Me.ckBridges.Text = "Extrusion Bridges"
        Me.ckBridges.UseVisualStyleBackColor = False
        '
        'frmExcludesP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(315, 273)
        Me.Controls.Add(Me.ckBridges)
        Me.Controls.Add(Me.txtSouth)
        Me.Controls.Add(Me.txtEast)
        Me.Controls.Add(Me.txtNorth)
        Me.Controls.Add(Me.txtWest)
        Me.Controls.Add(Me.ckWind)
        Me.Controls.Add(Me.ckTrigger)
        Me.Controls.Add(Me.ckTaxi)
        Me.Controls.Add(Me.ckLibrary)
        Me.Controls.Add(Me.ckGenBuilds)
        Me.Controls.Add(Me.ckEffects)
        Me.Controls.Add(Me.ckBeacons)
        Me.Controls.Add(Me.ckAll)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExcludesP"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - Exclusion Rectangle Properties"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents ckBridges As System.Windows.Forms.CheckBox
#End Region 
End Class