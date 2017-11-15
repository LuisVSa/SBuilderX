<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAltitudePoly
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAltitudePoly))
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtAlt = New System.Windows.Forms.TextBox
        Me.cmdAlt = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.txtHead = New System.Windows.Forms.TextBox
        Me.txtAlt0 = New System.Windows.Forms.TextBox
        Me.cmdHelpSlope = New System.Windows.Forms.Button
        Me.cmdSlope = New System.Windows.Forms.Button
        Me.txtSlope = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtPt0 = New System.Windows.Forms.TextBox
        Me.lbSX = New System.Windows.Forms.Label
        Me.lbSY = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(396, 99)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(57, 25)
        Me.cmdCancel.TabIndex = 54
        Me.cmdCancel.Text = "Close"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtAlt)
        Me.GroupBox1.Controls.Add(Me.cmdAlt)
        Me.GroupBox1.Location = New System.Drawing.Point(264, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(205, 59)
        Me.GroupBox1.TabIndex = 51
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Apply constant altitude to Points"
        '
        'txtAlt
        '
        Me.txtAlt.BackColor = System.Drawing.SystemColors.Window
        Me.txtAlt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAlt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAlt.Location = New System.Drawing.Point(23, 27)
        Me.txtAlt.MaxLength = 0
        Me.txtAlt.Name = "txtAlt"
        Me.txtAlt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAlt.Size = New System.Drawing.Size(85, 20)
        Me.txtAlt.TabIndex = 29
        Me.txtAlt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdAlt
        '
        Me.cmdAlt.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAlt.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAlt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAlt.Location = New System.Drawing.Point(132, 24)
        Me.cmdAlt.Name = "cmdAlt"
        Me.cmdAlt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAlt.Size = New System.Drawing.Size(57, 25)
        Me.cmdAlt.TabIndex = 28
        Me.cmdAlt.Text = "Set"
        Me.cmdAlt.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtHead)
        Me.GroupBox3.Controls.Add(Me.txtAlt0)
        Me.GroupBox3.Controls.Add(Me.cmdHelpSlope)
        Me.GroupBox3.Controls.Add(Me.cmdSlope)
        Me.GroupBox3.Controls.Add(Me.txtSlope)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.txtPt0)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(234, 126)
        Me.GroupBox3.TabIndex = 55
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Set 1 point and maximum slope"
        '
        'txtHead
        '
        Me.txtHead.BackColor = System.Drawing.SystemColors.Window
        Me.txtHead.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHead.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtHead.Location = New System.Drawing.Point(68, 66)
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
        Me.txtAlt0.Location = New System.Drawing.Point(18, 40)
        Me.txtAlt0.MaxLength = 0
        Me.txtAlt0.Name = "txtAlt0"
        Me.txtAlt0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAlt0.Size = New System.Drawing.Size(81, 20)
        Me.txtAlt0.TabIndex = 62
        Me.txtAlt0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdHelpSlope
        '
        Me.cmdHelpSlope.BackColor = System.Drawing.SystemColors.Control
        Me.cmdHelpSlope.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdHelpSlope.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHelpSlope.Location = New System.Drawing.Point(161, 40)
        Me.cmdHelpSlope.Name = "cmdHelpSlope"
        Me.cmdHelpSlope.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdHelpSlope.Size = New System.Drawing.Size(57, 25)
        Me.cmdHelpSlope.TabIndex = 61
        Me.cmdHelpSlope.Text = "Help"
        Me.cmdHelpSlope.UseVisualStyleBackColor = False
        '
        'cmdSlope
        '
        Me.cmdSlope.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSlope.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSlope.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSlope.Location = New System.Drawing.Point(161, 87)
        Me.cmdSlope.Name = "cmdSlope"
        Me.cmdSlope.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSlope.Size = New System.Drawing.Size(57, 25)
        Me.cmdSlope.TabIndex = 60
        Me.cmdSlope.Text = "Set"
        Me.cmdSlope.UseVisualStyleBackColor = False
        '
        'txtSlope
        '
        Me.txtSlope.BackColor = System.Drawing.SystemColors.Window
        Me.txtSlope.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSlope.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSlope.Location = New System.Drawing.Point(68, 92)
        Me.txtSlope.MaxLength = 0
        Me.txtSlope.Name = "txtSlope"
        Me.txtSlope.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSlope.Size = New System.Drawing.Size(71, 20)
        Me.txtSlope.TabIndex = 58
        Me.txtSlope.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(28, 95)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(34, 13)
        Me.Label8.TabIndex = 59
        Me.Label8.Text = "Slope"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 68)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 13)
        Me.Label6.TabIndex = 57
        Me.Label6.Text = "Heading"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(102, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 56
        Me.Label2.Text = "Pt #"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 53
        Me.Label1.Text = "Altitude"
        '
        'txtPt0
        '
        Me.txtPt0.BackColor = System.Drawing.SystemColors.Window
        Me.txtPt0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPt0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPt0.Location = New System.Drawing.Point(105, 40)
        Me.txtPt0.MaxLength = 0
        Me.txtPt0.Name = "txtPt0"
        Me.txtPt0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPt0.Size = New System.Drawing.Size(34, 20)
        Me.txtPt0.TabIndex = 52
        Me.txtPt0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbSX
        '
        Me.lbSX.AutoSize = True
        Me.lbSX.Location = New System.Drawing.Point(261, 99)
        Me.lbSX.Name = "lbSX"
        Me.lbSX.Size = New System.Drawing.Size(59, 13)
        Me.lbSX.TabIndex = 56
        Me.lbSX.Text = "SlopeX = 0"
        '
        'lbSY
        '
        Me.lbSY.AutoSize = True
        Me.lbSY.Location = New System.Drawing.Point(261, 115)
        Me.lbSY.Name = "lbSY"
        Me.lbSY.Size = New System.Drawing.Size(59, 13)
        Me.lbSY.TabIndex = 57
        Me.lbSY.Text = "SlopeY = 0"
        '
        'frmAltitudePoly
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(485, 151)
        Me.Controls.Add(Me.lbSY)
        Me.Controls.Add(Me.lbSX)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAltitudePoly"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - Set Polygon Altitude"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents txtAlt As System.Windows.Forms.TextBox
    Public WithEvents cmdAlt As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtPt0 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents txtSlope As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents cmdHelpSlope As System.Windows.Forms.Button
    Public WithEvents cmdSlope As System.Windows.Forms.Button
    Friend WithEvents lbSX As System.Windows.Forms.Label
    Friend WithEvents lbSY As System.Windows.Forms.Label
    Public WithEvents txtHead As System.Windows.Forms.TextBox
    Public WithEvents txtAlt0 As System.Windows.Forms.TextBox

End Class
