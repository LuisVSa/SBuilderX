<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLWidth
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLWidth))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtWidth = New System.Windows.Forms.TextBox
        Me.cmdWidth = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmdReverse = New System.Windows.Forms.Button
        Me.cmdWidth12 = New System.Windows.Forms.Button
        Me.txtWidth1 = New System.Windows.Forms.TextBox
        Me.txtWidth2 = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdWinding = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtWidth)
        Me.GroupBox1.Controls.Add(Me.cmdWidth)
        Me.GroupBox1.Location = New System.Drawing.Point(216, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(103, 115)
        Me.GroupBox1.TabIndex = 32
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Constant width"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Width"
        '
        'txtWidth
        '
        Me.txtWidth.AcceptsReturn = True
        Me.txtWidth.BackColor = System.Drawing.SystemColors.Window
        Me.txtWidth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWidth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWidth.Location = New System.Drawing.Point(23, 38)
        Me.txtWidth.MaxLength = 0
        Me.txtWidth.Name = "txtWidth"
        Me.txtWidth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWidth.Size = New System.Drawing.Size(57, 20)
        Me.txtWidth.TabIndex = 29
        Me.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdWidth
        '
        Me.cmdWidth.BackColor = System.Drawing.SystemColors.Control
        Me.cmdWidth.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdWidth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdWidth.Location = New System.Drawing.Point(23, 75)
        Me.cmdWidth.Name = "cmdWidth"
        Me.cmdWidth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdWidth.Size = New System.Drawing.Size(57, 25)
        Me.cmdWidth.TabIndex = 28
        Me.cmdWidth.Text = "Set"
        Me.cmdWidth.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdReverse)
        Me.GroupBox2.Controls.Add(Me.cmdWidth12)
        Me.GroupBox2.Controls.Add(Me.txtWidth1)
        Me.GroupBox2.Controls.Add(Me.txtWidth2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(189, 115)
        Me.GroupBox2.TabIndex = 33
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Progressive width"
        '
        'cmdReverse
        '
        Me.cmdReverse.BackColor = System.Drawing.SystemColors.Control
        Me.cmdReverse.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdReverse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReverse.Location = New System.Drawing.Point(24, 75)
        Me.cmdReverse.Name = "cmdReverse"
        Me.cmdReverse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdReverse.Size = New System.Drawing.Size(57, 25)
        Me.cmdReverse.TabIndex = 42
        Me.cmdReverse.Text = "Change"
        Me.cmdReverse.UseVisualStyleBackColor = False
        '
        'cmdWidth12
        '
        Me.cmdWidth12.BackColor = System.Drawing.SystemColors.Control
        Me.cmdWidth12.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdWidth12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdWidth12.Location = New System.Drawing.Point(103, 75)
        Me.cmdWidth12.Name = "cmdWidth12"
        Me.cmdWidth12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdWidth12.Size = New System.Drawing.Size(57, 25)
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
        Me.txtWidth1.Location = New System.Drawing.Point(24, 40)
        Me.txtWidth1.MaxLength = 0
        Me.txtWidth1.Name = "txtWidth1"
        Me.txtWidth1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWidth1.Size = New System.Drawing.Size(57, 20)
        Me.txtWidth1.TabIndex = 38
        Me.txtWidth1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtWidth2
        '
        Me.txtWidth2.AcceptsReturn = True
        Me.txtWidth2.BackColor = System.Drawing.SystemColors.Window
        Me.txtWidth2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWidth2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWidth2.Location = New System.Drawing.Point(103, 40)
        Me.txtWidth2.MaxLength = 0
        Me.txtWidth2.Name = "txtWidth2"
        Me.txtWidth2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWidth2.Size = New System.Drawing.Size(57, 20)
        Me.txtWidth2.TabIndex = 37
        Me.txtWidth2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(21, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 41
        Me.Label1.Text = "First Point"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(100, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "Last Point"
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(335, 87)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(63, 25)
        Me.cmdCancel.TabIndex = 35
        Me.cmdCancel.Text = "Close"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdWinding
        '
        Me.cmdWinding.Location = New System.Drawing.Point(335, 47)
        Me.cmdWinding.Name = "cmdWinding"
        Me.cmdWinding.Size = New System.Drawing.Size(63, 25)
        Me.cmdWinding.TabIndex = 36
        Me.cmdWinding.Text = "Reverse"
        Me.cmdWinding.UseVisualStyleBackColor = True
        '
        'frmLWidth
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(410, 143)
        Me.Controls.Add(Me.cmdWinding)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLWidth"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - Set Line Width"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents txtWidth As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents cmdReverse As System.Windows.Forms.Button
    Public WithEvents txtWidth1 As System.Windows.Forms.TextBox
    Public WithEvents txtWidth2 As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdWidth As System.Windows.Forms.Button
    Public WithEvents cmdWidth12 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdWinding As System.Windows.Forms.Button

End Class
