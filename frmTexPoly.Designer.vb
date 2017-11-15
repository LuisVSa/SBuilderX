<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTexPoly
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTexPoly))
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.cmdLL = New System.Windows.Forms.Button
        Me.cmdRR = New System.Windows.Forms.Button
        Me.txtPY = New System.Windows.Forms.TextBox
        Me.cmdDD = New System.Windows.Forms.Button
        Me.cmdUU = New System.Windows.Forms.Button
        Me.txtPX = New System.Windows.Forms.TextBox
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdReset = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.imgText = New System.Windows.Forms.PictureBox
        Me.Timer1 = New System.Timers.Timer
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Frame1.SuspendLayout()
        CType(Me.imgText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Timer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.Label3)
        Me.Frame1.Controls.Add(Me.Label2)
        Me.Frame1.Controls.Add(Me.cmdLL)
        Me.Frame1.Controls.Add(Me.cmdRR)
        Me.Frame1.Controls.Add(Me.txtPY)
        Me.Frame1.Controls.Add(Me.cmdDD)
        Me.Frame1.Controls.Add(Me.cmdUU)
        Me.Frame1.Controls.Add(Me.txtPX)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(554, 180)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(86, 172)
        Me.Frame1.TabIndex = 23
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Point # 1"
        '
        'cmdLL
        '
        Me.cmdLL.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLL.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLL.Image = CType(resources.GetObject("cmdLL.Image"), System.Drawing.Image)
        Me.cmdLL.Location = New System.Drawing.Point(13, 51)
        Me.cmdLL.Name = "cmdLL"
        Me.cmdLL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLL.Size = New System.Drawing.Size(25, 25)
        Me.cmdLL.TabIndex = 9
        Me.cmdLL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdLL.UseVisualStyleBackColor = False
        '
        'cmdRR
        '
        Me.cmdRR.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRR.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRR.Image = CType(resources.GetObject("cmdRR.Image"), System.Drawing.Image)
        Me.cmdRR.Location = New System.Drawing.Point(48, 51)
        Me.cmdRR.Name = "cmdRR"
        Me.cmdRR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRR.Size = New System.Drawing.Size(25, 25)
        Me.cmdRR.TabIndex = 8
        Me.cmdRR.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdRR.UseVisualStyleBackColor = False
        '
        'txtPY
        '
        Me.txtPY.AcceptsReturn = True
        Me.txtPY.BackColor = System.Drawing.SystemColors.Window
        Me.txtPY.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPY.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPY.Location = New System.Drawing.Point(41, 100)
        Me.txtPY.MaxLength = 0
        Me.txtPY.Name = "txtPY"
        Me.txtPY.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPY.Size = New System.Drawing.Size(32, 20)
        Me.txtPY.TabIndex = 7
        Me.txtPY.Text = "255"
        Me.txtPY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdDD
        '
        Me.cmdDD.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDD.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDD.Image = CType(resources.GetObject("cmdDD.Image"), System.Drawing.Image)
        Me.cmdDD.Location = New System.Drawing.Point(13, 126)
        Me.cmdDD.Name = "cmdDD"
        Me.cmdDD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDD.Size = New System.Drawing.Size(25, 25)
        Me.cmdDD.TabIndex = 6
        Me.cmdDD.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdDD.UseVisualStyleBackColor = False
        '
        'cmdUU
        '
        Me.cmdUU.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUU.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUU.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUU.Image = CType(resources.GetObject("cmdUU.Image"), System.Drawing.Image)
        Me.cmdUU.Location = New System.Drawing.Point(48, 126)
        Me.cmdUU.Name = "cmdUU"
        Me.cmdUU.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUU.Size = New System.Drawing.Size(25, 25)
        Me.cmdUU.TabIndex = 5
        Me.cmdUU.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdUU.UseVisualStyleBackColor = False
        '
        'txtPX
        '
        Me.txtPX.AcceptsReturn = True
        Me.txtPX.BackColor = System.Drawing.SystemColors.Window
        Me.txtPX.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPX.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPX.Location = New System.Drawing.Point(41, 25)
        Me.txtPX.MaxLength = 0
        Me.txtPX.Name = "txtPX"
        Me.txtPX.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPX.Size = New System.Drawing.Size(32, 20)
        Me.txtPX.TabIndex = 4
        Me.txtPX.Text = "0"
        Me.txtPX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(566, 473)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(61, 25)
        Me.cmdOK.TabIndex = 21
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(566, 428)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(61, 25)
        Me.cmdCancel.TabIndex = 20
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdReset
        '
        Me.cmdReset.BackColor = System.Drawing.SystemColors.Control
        Me.cmdReset.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdReset.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReset.Location = New System.Drawing.Point(566, 383)
        Me.cmdReset.Name = "cmdReset"
        Me.cmdReset.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdReset.Size = New System.Drawing.Size(61, 25)
        Me.cmdReset.TabIndex = 19
        Me.cmdReset.Text = "Reset"
        Me.cmdReset.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(551, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(89, 126)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Adjust the tie points by selecting and moving with the mouse or by using the arro" & _
            "ws comands below."
        '
        'imgText
        '
        Me.imgText.Location = New System.Drawing.Point(12, 12)
        Me.imgText.Name = "imgText"
        Me.imgText.Size = New System.Drawing.Size(512, 512)
        Me.imgText.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgText.TabIndex = 24
        Me.imgText.TabStop = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.SynchronizingObject = Me
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(17, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "X:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 103)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(17, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Y:"
        '
        'frmTexPoly
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(668, 535)
        Me.Controls.Add(Me.imgText)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdReset)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmTexPoly"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.imgText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Timer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdLL As System.Windows.Forms.Button
    Public WithEvents cmdRR As System.Windows.Forms.Button
    Public WithEvents txtPY As System.Windows.Forms.TextBox
    Public WithEvents cmdDD As System.Windows.Forms.Button
    Public WithEvents cmdUU As System.Windows.Forms.Button
    Public WithEvents txtPX As System.Windows.Forms.TextBox
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdReset As System.Windows.Forms.Button
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents imgText As System.Windows.Forms.PictureBox
    Friend WithEvents Timer1 As System.Timers.Timer
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
