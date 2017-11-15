<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmExtrusions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExtrusions))
        Me.txtProfileGuid = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtMaterialGuid = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtPylonGuid = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.ckSuppress = New System.Windows.Forms.CheckBox
        Me.txtWidth = New System.Windows.Forms.TextBox
        Me.txtProbability = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdOK = New System.Windows.Forms.Button
        Me.boxHeight = New System.Windows.Forms.GroupBox
        Me.cmdSetHeight = New System.Windows.Forms.Button
        Me.txtHeight = New System.Windows.Forms.TextBox
        Me.cmbComplexity = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.boxHeight.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtProfileGuid
        '
        Me.txtProfileGuid.Location = New System.Drawing.Point(21, 32)
        Me.txtProfileGuid.Name = "txtProfileGuid"
        Me.txtProfileGuid.Size = New System.Drawing.Size(234, 20)
        Me.txtProfileGuid.TabIndex = 97
        Me.txtProfileGuid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 13)
        Me.Label1.TabIndex = 98
        Me.Label1.Text = "Extrusion Profile Guid"
        '
        'txtMaterialGuid
        '
        Me.txtMaterialGuid.Location = New System.Drawing.Point(21, 70)
        Me.txtMaterialGuid.Name = "txtMaterialGuid"
        Me.txtMaterialGuid.Size = New System.Drawing.Size(234, 20)
        Me.txtMaterialGuid.TabIndex = 97
        Me.txtMaterialGuid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 98
        Me.Label2.Text = "Material Guid"
        '
        'txtPylonGuid
        '
        Me.txtPylonGuid.Location = New System.Drawing.Point(21, 109)
        Me.txtPylonGuid.Name = "txtPylonGuid"
        Me.txtPylonGuid.Size = New System.Drawing.Size(234, 20)
        Me.txtPylonGuid.TabIndex = 97
        Me.txtPylonGuid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 98
        Me.Label3.Text = "Pylon Guid"
        '
        'ckSuppress
        '
        Me.ckSuppress.AutoSize = True
        Me.ckSuppress.Location = New System.Drawing.Point(279, 112)
        Me.ckSuppress.Name = "ckSuppress"
        Me.ckSuppress.Size = New System.Drawing.Size(111, 17)
        Me.ckSuppress.TabIndex = 99
        Me.ckSuppress.Text = "Suppress Platform"
        Me.ckSuppress.UseVisualStyleBackColor = True
        '
        'txtWidth
        '
        Me.txtWidth.Location = New System.Drawing.Point(279, 32)
        Me.txtWidth.Name = "txtWidth"
        Me.txtWidth.Size = New System.Drawing.Size(29, 20)
        Me.txtWidth.TabIndex = 100
        Me.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtProbability
        '
        Me.txtProbability.Location = New System.Drawing.Point(279, 70)
        Me.txtProbability.Name = "txtProbability"
        Me.txtProbability.Size = New System.Drawing.Size(29, 20)
        Me.txtProbability.TabIndex = 100
        Me.txtProbability.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(314, 73)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 98
        Me.Label4.Text = "Probability"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(314, 35)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 13)
        Me.Label5.TabIndex = 98
        Me.Label5.Text = "Width"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(333, 146)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(57, 26)
        Me.cmdCancel.TabIndex = 101
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(413, 146)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(57, 26)
        Me.cmdOK.TabIndex = 101
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'boxHeight
        '
        Me.boxHeight.Controls.Add(Me.cmdSetHeight)
        Me.boxHeight.Controls.Add(Me.txtHeight)
        Me.boxHeight.Location = New System.Drawing.Point(406, 32)
        Me.boxHeight.Name = "boxHeight"
        Me.boxHeight.Size = New System.Drawing.Size(64, 97)
        Me.boxHeight.TabIndex = 102
        Me.boxHeight.TabStop = False
        Me.boxHeight.Text = "Height"
        '
        'cmdSetHeight
        '
        Me.cmdSetHeight.Location = New System.Drawing.Point(17, 55)
        Me.cmdSetHeight.Name = "cmdSetHeight"
        Me.cmdSetHeight.Size = New System.Drawing.Size(33, 24)
        Me.cmdSetHeight.TabIndex = 94
        Me.cmdSetHeight.Text = "Set"
        Me.cmdSetHeight.UseVisualStyleBackColor = True
        '
        'txtHeight
        '
        Me.txtHeight.AcceptsReturn = True
        Me.txtHeight.BackColor = System.Drawing.SystemColors.Window
        Me.txtHeight.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHeight.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtHeight.Location = New System.Drawing.Point(17, 22)
        Me.txtHeight.MaxLength = 0
        Me.txtHeight.Name = "txtHeight"
        Me.txtHeight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHeight.Size = New System.Drawing.Size(33, 20)
        Me.txtHeight.TabIndex = 92
        Me.txtHeight.Text = "10"
        Me.txtHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmbComplexity
        '
        Me.cmbComplexity.FormattingEnabled = True
        Me.cmbComplexity.Items.AddRange(New Object() {"Very Sparse", "Sparse", "Normal", "Dense", "Very Dense", "Extra Dense"})
        Me.cmbComplexity.Location = New System.Drawing.Point(134, 151)
        Me.cmbComplexity.Name = "cmbComplexity"
        Me.cmbComplexity.Size = New System.Drawing.Size(121, 21)
        Me.cmbComplexity.TabIndex = 104
        Me.cmbComplexity.Text = "Normal"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(39, 154)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 13)
        Me.Label6.TabIndex = 105
        Me.Label6.Text = "Image Complexity"
        '
        'frmExtrusions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(489, 191)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmbComplexity)
        Me.Controls.Add(Me.boxHeight)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.txtProbability)
        Me.Controls.Add(Me.txtWidth)
        Me.Controls.Add(Me.ckSuppress)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtMaterialGuid)
        Me.Controls.Add(Me.txtPylonGuid)
        Me.Controls.Add(Me.txtProfileGuid)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExtrusions"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - Extrusion Bridges Properties"
        Me.boxHeight.ResumeLayout(False)
        Me.boxHeight.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtProfileGuid As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtMaterialGuid As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPylonGuid As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ckSuppress As System.Windows.Forms.CheckBox
    Friend WithEvents txtWidth As System.Windows.Forms.TextBox
    Friend WithEvents txtProbability As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents boxHeight As System.Windows.Forms.GroupBox
    Friend WithEvents cmdSetHeight As System.Windows.Forms.Button
    Public WithEvents txtHeight As System.Windows.Forms.TextBox
    Friend WithEvents cmbComplexity As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label

End Class
