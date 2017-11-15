<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFind
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFind))
        Me.cmdAll = New System.Windows.Forms.Button
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.ckPTypes = New System.Windows.Forms.RadioButton
        Me.ckLTypes = New System.Windows.Forms.RadioButton
        Me.ckPolys = New System.Windows.Forms.RadioButton
        Me.ckLines = New System.Windows.Forms.RadioButton
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdNext = New System.Windows.Forms.Button
        Me.txtName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Frame1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdAll
        '
        Me.cmdAll.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAll.Location = New System.Drawing.Point(268, 87)
        Me.cmdAll.Name = "cmdAll"
        Me.cmdAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAll.Size = New System.Drawing.Size(70, 25)
        Me.cmdAll.TabIndex = 15
        Me.cmdAll.Text = "Find All"
        Me.cmdAll.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.ckPTypes)
        Me.Frame1.Controls.Add(Me.ckLTypes)
        Me.Frame1.Controls.Add(Me.ckPolys)
        Me.Frame1.Controls.Add(Me.ckLines)
        Me.Frame1.Location = New System.Drawing.Point(19, 64)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(211, 87)
        Me.Frame1.TabIndex = 13
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Where to Search"
        '
        'ckPTypes
        '
        Me.ckPTypes.AutoSize = True
        Me.ckPTypes.BackColor = System.Drawing.SystemColors.Control
        Me.ckPTypes.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckPTypes.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckPTypes.Location = New System.Drawing.Point(116, 48)
        Me.ckPTypes.Name = "ckPTypes"
        Me.ckPTypes.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckPTypes.Size = New System.Drawing.Size(75, 17)
        Me.ckPTypes.TabIndex = 8
        Me.ckPTypes.TabStop = True
        Me.ckPTypes.Text = "Poly Guids"
        Me.ckPTypes.UseVisualStyleBackColor = False
        '
        'ckLTypes
        '
        Me.ckLTypes.AutoSize = True
        Me.ckLTypes.BackColor = System.Drawing.SystemColors.Control
        Me.ckLTypes.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckLTypes.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckLTypes.Location = New System.Drawing.Point(116, 24)
        Me.ckLTypes.Name = "ckLTypes"
        Me.ckLTypes.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckLTypes.Size = New System.Drawing.Size(75, 17)
        Me.ckLTypes.TabIndex = 7
        Me.ckLTypes.TabStop = True
        Me.ckLTypes.Text = "Line Guids"
        Me.ckLTypes.UseVisualStyleBackColor = False
        '
        'ckPolys
        '
        Me.ckPolys.AutoSize = True
        Me.ckPolys.BackColor = System.Drawing.SystemColors.Control
        Me.ckPolys.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckPolys.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckPolys.Location = New System.Drawing.Point(18, 48)
        Me.ckPolys.Name = "ckPolys"
        Me.ckPolys.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckPolys.Size = New System.Drawing.Size(81, 17)
        Me.ckPolys.TabIndex = 5
        Me.ckPolys.TabStop = True
        Me.ckPolys.Text = "Poly Names"
        Me.ckPolys.UseVisualStyleBackColor = False
        '
        'ckLines
        '
        Me.ckLines.AutoSize = True
        Me.ckLines.BackColor = System.Drawing.SystemColors.Control
        Me.ckLines.Checked = True
        Me.ckLines.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckLines.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckLines.Location = New System.Drawing.Point(18, 24)
        Me.ckLines.Name = "ckLines"
        Me.ckLines.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckLines.Size = New System.Drawing.Size(81, 17)
        Me.ckLines.TabIndex = 4
        Me.ckLines.TabStop = True
        Me.ckLines.Text = "Line Names"
        Me.ckLines.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(268, 126)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(70, 25)
        Me.cmdCancel.TabIndex = 12
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdNext
        '
        Me.cmdNext.BackColor = System.Drawing.SystemColors.Control
        Me.cmdNext.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdNext.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdNext.Location = New System.Drawing.Point(268, 48)
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdNext.Size = New System.Drawing.Size(70, 25)
        Me.cmdNext.TabIndex = 11
        Me.cmdNext.Text = "Find Next"
        Me.cmdNext.UseVisualStyleBackColor = False
        '
        'txtName
        '
        Me.txtName.AcceptsReturn = True
        Me.txtName.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtName.Location = New System.Drawing.Point(103, 12)
        Me.txtName.MaxLength = 0
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.Size = New System.Drawing.Size(235, 20)
        Me.txtName.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(16, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(85, 16)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Text to Search:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmFind
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(357, 166)
        Me.Controls.Add(Me.cmdAll)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdNext)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFind"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - Find Line or Poly"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents cmdAll As System.Windows.Forms.Button
    Public WithEvents ckPTypes As System.Windows.Forms.RadioButton
    Public WithEvents ckLTypes As System.Windows.Forms.RadioButton
    Public WithEvents ckPolys As System.Windows.Forms.RadioButton
    Public WithEvents ckLines As System.Windows.Forms.RadioButton
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdNext As System.Windows.Forms.Button
    Public WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Frame1 As System.Windows.Forms.GroupBox

End Class
