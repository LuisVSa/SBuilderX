<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRAW
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmRAW))
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdOK = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.optWater = New System.Windows.Forms.RadioButton
        Me.optLand = New System.Windows.Forms.RadioButton
        Me.txtJ = New System.Windows.Forms.TextBox
        Me.txtK = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(248, 25)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(56, 26)
        Me.cmdCancel.TabIndex = 0
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(248, 68)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(56, 26)
        Me.cmdOK.TabIndex = 0
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optWater)
        Me.GroupBox1.Controls.Add(Me.optLand)
        Me.GroupBox1.Location = New System.Drawing.Point(21, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(79, 79)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Class Type"
        '
        'optWater
        '
        Me.optWater.AutoSize = True
        Me.optWater.Location = New System.Drawing.Point(15, 47)
        Me.optWater.Name = "optWater"
        Me.optWater.Size = New System.Drawing.Size(54, 17)
        Me.optWater.TabIndex = 0
        Me.optWater.Text = "Water"
        Me.optWater.UseVisualStyleBackColor = True
        '
        'optLand
        '
        Me.optLand.AutoSize = True
        Me.optLand.Checked = True
        Me.optLand.Location = New System.Drawing.Point(15, 24)
        Me.optLand.Name = "optLand"
        Me.optLand.Size = New System.Drawing.Size(49, 17)
        Me.optLand.TabIndex = 0
        Me.optLand.TabStop = True
        Me.optLand.Text = "Land"
        Me.optLand.UseVisualStyleBackColor = True
        '
        'txtJ
        '
        Me.txtJ.Location = New System.Drawing.Point(124, 25)
        Me.txtJ.Name = "txtJ"
        Me.txtJ.Size = New System.Drawing.Size(33, 20)
        Me.txtJ.TabIndex = 2
        Me.txtJ.Text = "95"
        Me.txtJ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtK
        '
        Me.txtK.Location = New System.Drawing.Point(124, 68)
        Me.txtK.Name = "txtK"
        Me.txtK.Size = New System.Drawing.Size(33, 20)
        Me.txtK.TabIndex = 2
        Me.txtK.Text = "63"
        Me.txtK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(163, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 31)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "W/E QMID (0 up to 95)"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(163, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 27)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "N/S QMID (0 up to 63)"
        '
        'frmRAW
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(326, 108)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtK)
        Me.Controls.Add(Me.txtJ)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRAW"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - Raw file parameters"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optWater As System.Windows.Forms.RadioButton
    Friend WithEvents optLand As System.Windows.Forms.RadioButton
    Friend WithEvents txtJ As System.Windows.Forms.TextBox
    Friend WithEvents txtK As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
