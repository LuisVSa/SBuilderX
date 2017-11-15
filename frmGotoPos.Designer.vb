<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGotoPos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGotoPos))
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtLat = New System.Windows.Forms.TextBox()
        Me.txtLon = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtU = New System.Windows.Forms.TextBox()
        Me.txtV = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtL = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmdCheck = New System.Windows.Forms.Button()
        Me.Frame2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(316, 108)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(56, 25)
        Me.cmdOK.TabIndex = 13
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(316, 67)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(56, 25)
        Me.cmdCancel.TabIndex = 12
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Frame2.Controls.Add(Me.Label2)
        Me.Frame2.Controls.Add(Me.txtLat)
        Me.Frame2.Controls.Add(Me.txtLon)
        Me.Frame2.Controls.Add(Me.Label1)
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(21, 12)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(146, 132)
        Me.Frame2.TabIndex = 11
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Geographic"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(15, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Longitude"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtLat
        '
        Me.txtLat.AcceptsReturn = True
        Me.txtLat.BackColor = System.Drawing.SystemColors.Window
        Me.txtLat.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLat.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLat.Location = New System.Drawing.Point(18, 41)
        Me.txtLat.MaxLength = 0
        Me.txtLat.Name = "txtLat"
        Me.txtLat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLat.Size = New System.Drawing.Size(108, 20)
        Me.txtLat.TabIndex = 2
        Me.txtLat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtLon
        '
        Me.txtLon.AcceptsReturn = True
        Me.txtLon.BackColor = System.Drawing.SystemColors.Window
        Me.txtLon.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLon.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLon.Location = New System.Drawing.Point(18, 93)
        Me.txtLon.MaxLength = 0
        Me.txtLon.Name = "txtLon"
        Me.txtLon.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLon.Size = New System.Drawing.Size(108, 20)
        Me.txtLon.TabIndex = 1
        Me.txtLon.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(15, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Latitude"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtU
        '
        Me.txtU.AcceptsReturn = True
        Me.txtU.BackColor = System.Drawing.SystemColors.Window
        Me.txtU.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtU.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtU.Location = New System.Drawing.Point(37, 28)
        Me.txtU.MaxLength = 0
        Me.txtU.Name = "txtU"
        Me.txtU.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtU.Size = New System.Drawing.Size(53, 20)
        Me.txtU.TabIndex = 2
        Me.txtU.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtV
        '
        Me.txtV.AcceptsReturn = True
        Me.txtV.BackColor = System.Drawing.SystemColors.Window
        Me.txtV.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtV.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtV.Location = New System.Drawing.Point(37, 63)
        Me.txtV.MaxLength = 0
        Me.txtV.Name = "txtV"
        Me.txtV.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtV.Size = New System.Drawing.Size(53, 20)
        Me.txtV.TabIndex = 1
        Me.txtV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(17, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(14, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "V"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(16, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(15, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "U"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtU)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtV)
        Me.GroupBox1.Location = New System.Drawing.Point(185, 45)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(108, 99)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "(U, V)"
        '
        'txtL
        '
        Me.txtL.AcceptsReturn = True
        Me.txtL.BackColor = System.Drawing.SystemColors.Window
        Me.txtL.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtL.Location = New System.Drawing.Point(237, 12)
        Me.txtL.MaxLength = 0
        Me.txtL.Name = "txtL"
        Me.txtL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtL.Size = New System.Drawing.Size(38, 20)
        Me.txtL.TabIndex = 15
        Me.txtL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(202, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(29, 13)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "LOD"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdCheck
        '
        Me.cmdCheck.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCheck.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCheck.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCheck.Location = New System.Drawing.Point(316, 25)
        Me.cmdCheck.Name = "cmdCheck"
        Me.cmdCheck.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCheck.Size = New System.Drawing.Size(56, 25)
        Me.cmdCheck.TabIndex = 15
        Me.cmdCheck.Text = "Check"
        Me.cmdCheck.UseVisualStyleBackColor = False
        '
        'frmGotoPos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(392, 165)
        Me.Controls.Add(Me.txtL)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmdCheck)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.Frame2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGotoPos"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents txtLat As System.Windows.Forms.TextBox
    Public WithEvents txtLon As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents txtU As System.Windows.Forms.TextBox
    Public WithEvents txtV As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents txtL As System.Windows.Forms.TextBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents cmdCheck As System.Windows.Forms.Button

End Class
