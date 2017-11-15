<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAltitudeLine
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAltitudeLine))
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.txtAlt = New System.Windows.Forms.TextBox
        Me.cmdAlt = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(78, 54)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(57, 25)
        Me.cmdCancel.TabIndex = 56
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'txtAlt
        '
        Me.txtAlt.AcceptsReturn = True
        Me.txtAlt.BackColor = System.Drawing.SystemColors.Window
        Me.txtAlt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAlt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAlt.Location = New System.Drawing.Point(141, 12)
        Me.txtAlt.MaxLength = 0
        Me.txtAlt.Name = "txtAlt"
        Me.txtAlt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAlt.Size = New System.Drawing.Size(69, 20)
        Me.txtAlt.TabIndex = 29
        Me.txtAlt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdAlt
        '
        Me.cmdAlt.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAlt.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAlt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAlt.Location = New System.Drawing.Point(153, 54)
        Me.cmdAlt.Name = "cmdAlt"
        Me.cmdAlt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAlt.Size = New System.Drawing.Size(57, 25)
        Me.cmdAlt.TabIndex = 28
        Me.cmdAlt.Text = "OK"
        Me.cmdAlt.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(126, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Apply altitude to all points"
        '
        'frmAltitudeLine
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(227, 95)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdAlt)
        Me.Controls.Add(Me.txtAlt)
        Me.Controls.Add(Me.cmdCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAltitudeLine"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - Set Line Altitude"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents txtAlt As System.Windows.Forms.TextBox
    Public WithEvents cmdAlt As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
