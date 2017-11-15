<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSHPPoly
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSHPPoly))
        Me.cmdHelp = New System.Windows.Forms.Button
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtGUID = New System.Windows.Forms.TextBox
        Me.cmbGUID = New System.Windows.Forms.ComboBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lbColor = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtAltitude = New System.Windows.Forms.TextBox
        Me.cmbAltitude = New System.Windows.Forms.ComboBox
        Me.cmbColor = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.cmbName = New System.Windows.Forms.ComboBox
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdHelp
        '
        Me.cmdHelp.Location = New System.Drawing.Point(344, 163)
        Me.cmdHelp.Name = "cmdHelp"
        Me.cmdHelp.Size = New System.Drawing.Size(65, 26)
        Me.cmdHelp.TabIndex = 17
        Me.cmdHelp.Text = "Help"
        Me.cmdHelp.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(511, 163)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(65, 26)
        Me.cmdOK.TabIndex = 16
        Me.cmdOK.Text = "Continue"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(429, 163)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(65, 26)
        Me.cmdCancel.TabIndex = 15
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.txtGUID)
        Me.GroupBox4.Controls.Add(Me.cmbGUID)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 119)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(269, 97)
        Me.GroupBox4.TabIndex = 14
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "GUID of Imported Polys"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(11, 18)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(148, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Click to set the GUID (or type)"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(46, 66)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(91, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Get from this Field"
        '
        'txtGUID
        '
        Me.txtGUID.BackColor = System.Drawing.Color.White
        Me.txtGUID.Location = New System.Drawing.Point(14, 34)
        Me.txtGUID.Name = "txtGUID"
        Me.txtGUID.ReadOnly = True
        Me.txtGUID.Size = New System.Drawing.Size(239, 20)
        Me.txtGUID.TabIndex = 6
        Me.txtGUID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmbGUID
        '
        Me.cmbGUID.FormattingEnabled = True
        Me.cmbGUID.Items.AddRange(New Object() {"Use GUID above"})
        Me.cmbGUID.Location = New System.Drawing.Point(143, 63)
        Me.cmbGUID.Name = "cmbGUID"
        Me.cmbGUID.Size = New System.Drawing.Size(110, 21)
        Me.cmbGUID.TabIndex = 5
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.lbColor)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.txtAltitude)
        Me.GroupBox3.Controls.Add(Me.cmbAltitude)
        Me.GroupBox3.Controls.Add(Me.cmbColor)
        Me.GroupBox3.Location = New System.Drawing.Point(304, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(298, 122)
        Me.GroupBox3.TabIndex = 13
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Altitude and Color of Imported Polys"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(95, 26)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(55, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Use these"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(166, 26)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(106, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Get from these Fields"
        '
        'lbColor
        '
        Me.lbColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lbColor.Location = New System.Drawing.Point(128, 80)
        Me.lbColor.Name = "lbColor"
        Me.lbColor.Size = New System.Drawing.Size(20, 21)
        Me.lbColor.TabIndex = 15
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Color (click to change)"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Altitude (meters)"
        '
        'txtAltitude
        '
        Me.txtAltitude.Location = New System.Drawing.Point(98, 44)
        Me.txtAltitude.Name = "txtAltitude"
        Me.txtAltitude.Size = New System.Drawing.Size(52, 20)
        Me.txtAltitude.TabIndex = 12
        Me.txtAltitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmbAltitude
        '
        Me.cmbAltitude.FormattingEnabled = True
        Me.cmbAltitude.Items.AddRange(New Object() {"Value on the left"})
        Me.cmbAltitude.Location = New System.Drawing.Point(169, 43)
        Me.cmbAltitude.Name = "cmbAltitude"
        Me.cmbAltitude.Size = New System.Drawing.Size(111, 21)
        Me.cmbAltitude.TabIndex = 11
        '
        'cmbColor
        '
        Me.cmbColor.FormattingEnabled = True
        Me.cmbColor.Items.AddRange(New Object() {"Color on the left"})
        Me.cmbColor.Location = New System.Drawing.Point(167, 80)
        Me.cmbColor.Name = "cmbColor"
        Me.cmbColor.Size = New System.Drawing.Size(111, 21)
        Me.cmbColor.TabIndex = 8
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtName)
        Me.GroupBox1.Controls.Add(Me.cmbName)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(269, 82)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Name or Label of Imported Polys"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(46, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Get from this Field"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(14, 19)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(239, 20)
        Me.txtName.TabIndex = 6
        Me.txtName.Text = "Polygon imported from a shape file"
        Me.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmbName
        '
        Me.cmbName.FormattingEnabled = True
        Me.cmbName.Items.AddRange(New Object() {"Use text above"})
        Me.cmbName.Location = New System.Drawing.Point(143, 48)
        Me.cmbName.Name = "cmbName"
        Me.cmbName.Size = New System.Drawing.Size(110, 21)
        Me.cmbName.TabIndex = 5
        '
        'frmSHPPoly
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(616, 232)
        Me.Controls.Add(Me.cmdHelp)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSHPPoly"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - Appending a Polygon ShapeFile"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdHelp As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtGUID As System.Windows.Forms.TextBox
    Friend WithEvents cmbGUID As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lbColor As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtAltitude As System.Windows.Forms.TextBox
    Friend WithEvents cmbAltitude As System.Windows.Forms.ComboBox
    Friend WithEvents cmbColor As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents cmbName As System.Windows.Forms.ComboBox

End Class
