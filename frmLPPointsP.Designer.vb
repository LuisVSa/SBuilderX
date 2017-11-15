<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLPPointsP
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLPPointsP))
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.Frame2 = New System.Windows.Forms.GroupBox
        Me.lbAltitude = New System.Windows.Forms.Label
        Me.txtAltitude = New System.Windows.Forms.TextBox
        Me.txtLat = New System.Windows.Forms.TextBox
        Me.txtLon = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lbWidth = New System.Windows.Forms.Label
        Me.lbPt = New System.Windows.Forms.Label
        Me.txtWidth = New System.Windows.Forms.TextBox
        Me.Frame2.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(179, 133)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(70, 25)
        Me.cmdOK.TabIndex = 13
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(179, 87)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(70, 25)
        Me.cmdCancel.TabIndex = 12
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.lbAltitude)
        Me.Frame2.Controls.Add(Me.txtAltitude)
        Me.Frame2.Controls.Add(Me.txtLat)
        Me.Frame2.Controls.Add(Me.txtLon)
        Me.Frame2.Controls.Add(Me.Label2)
        Me.Frame2.Controls.Add(Me.Label1)
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(12, 12)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(146, 146)
        Me.Frame2.TabIndex = 11
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Geographic Coordinates"
        '
        'lbAltitude
        '
        Me.lbAltitude.AutoSize = True
        Me.lbAltitude.Location = New System.Drawing.Point(15, 98)
        Me.lbAltitude.Name = "lbAltitude"
        Me.lbAltitude.Size = New System.Drawing.Size(42, 13)
        Me.lbAltitude.TabIndex = 16
        Me.lbAltitude.Text = "Altitude"
        '
        'txtAltitude
        '
        Me.txtAltitude.AcceptsReturn = True
        Me.txtAltitude.BackColor = System.Drawing.SystemColors.Window
        Me.txtAltitude.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAltitude.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAltitude.Location = New System.Drawing.Point(15, 113)
        Me.txtAltitude.MaxLength = 0
        Me.txtAltitude.Name = "txtAltitude"
        Me.txtAltitude.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAltitude.Size = New System.Drawing.Size(108, 20)
        Me.txtAltitude.TabIndex = 7
        Me.txtAltitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtLat
        '
        Me.txtLat.AcceptsReturn = True
        Me.txtLat.BackColor = System.Drawing.SystemColors.Window
        Me.txtLat.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLat.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLat.Location = New System.Drawing.Point(15, 36)
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
        Me.txtLon.Location = New System.Drawing.Point(15, 75)
        Me.txtLon.MaxLength = 0
        Me.txtLon.Name = "txtLon"
        Me.txtLon.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLon.Size = New System.Drawing.Size(108, 20)
        Me.txtLon.TabIndex = 1
        Me.txtLon.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(15, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Longitude"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(12, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Latitude"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lbWidth
        '
        Me.lbWidth.AutoSize = True
        Me.lbWidth.BackColor = System.Drawing.SystemColors.Control
        Me.lbWidth.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbWidth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbWidth.Location = New System.Drawing.Point(178, 32)
        Me.lbWidth.Name = "lbWidth"
        Me.lbWidth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbWidth.Size = New System.Drawing.Size(35, 13)
        Me.lbWidth.TabIndex = 8
        Me.lbWidth.Text = "Width"
        Me.lbWidth.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lbPt
        '
        Me.lbPt.BackColor = System.Drawing.SystemColors.Control
        Me.lbPt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbPt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbPt.Location = New System.Drawing.Point(178, 12)
        Me.lbPt.Name = "lbPt"
        Me.lbPt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbPt.Size = New System.Drawing.Size(46, 19)
        Me.lbPt.TabIndex = 15
        Me.lbPt.Text = "PT #"
        '
        'txtWidth
        '
        Me.txtWidth.AcceptsReturn = True
        Me.txtWidth.BackColor = System.Drawing.SystemColors.Window
        Me.txtWidth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWidth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWidth.Location = New System.Drawing.Point(179, 48)
        Me.txtWidth.MaxLength = 0
        Me.txtWidth.Name = "txtWidth"
        Me.txtWidth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWidth.Size = New System.Drawing.Size(70, 20)
        Me.txtWidth.TabIndex = 16
        Me.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frmLPPointsP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(269, 175)
        Me.Controls.Add(Me.txtWidth)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.lbWidth)
        Me.Controls.Add(Me.lbPt)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLPPointsP"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents txtAltitude As System.Windows.Forms.TextBox
    Public WithEvents txtLat As System.Windows.Forms.TextBox
    Public WithEvents txtLon As System.Windows.Forms.TextBox
    Public WithEvents lbWidth As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lbPt As System.Windows.Forms.Label
    Friend WithEvents lbAltitude As System.Windows.Forms.Label
    Public WithEvents txtWidth As System.Windows.Forms.TextBox

End Class
