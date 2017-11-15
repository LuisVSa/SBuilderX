<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLPSmooth
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLPSmooth))
        Me.chNoEnds = New System.Windows.Forms.CheckBox
        Me.chCorner = New System.Windows.Forms.CheckBox
        Me.txtDistance = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdSmooth = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'chNoEnds
        '
        Me.chNoEnds.Location = New System.Drawing.Point(25, 85)
        Me.chNoEnds.Name = "chNoEnds"
        Me.chNoEnds.Size = New System.Drawing.Size(168, 44)
        Me.chNoEnds.TabIndex = 3
        Me.chNoEnds.Text = "Do not smooth start/end segments (Lines only)"
        Me.chNoEnds.UseVisualStyleBackColor = True
        '
        'chCorner
        '
        Me.chCorner.AutoSize = True
        Me.chCorner.Location = New System.Drawing.Point(25, 129)
        Me.chCorner.Name = "chCorner"
        Me.chCorner.Size = New System.Drawing.Size(158, 17)
        Me.chCorner.TabIndex = 4
        Me.chCorner.Text = "Do not smooth sharp angles"
        Me.chCorner.UseVisualStyleBackColor = True
        '
        'txtDistance
        '
        Me.txtDistance.Location = New System.Drawing.Point(25, 152)
        Me.txtDistance.Name = "txtDistance"
        Me.txtDistance.Size = New System.Drawing.Size(70, 20)
        Me.txtDistance.TabIndex = 5
        Me.txtDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(22, 175)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(147, 30)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Minimum Distance in meters between adjacent points"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(25, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(225, 77)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "By smoothing (or interpolating) a Line or Poly, you introduce more points except " & _
            "when they become closer than the Minimum Distance shown below. Each time you pre" & _
            "ss ""Smooth"" more points are inserted."
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(190, 174)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(61, 23)
        Me.cmdOK.TabIndex = 9
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(190, 98)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(60, 25)
        Me.cmdCancel.TabIndex = 10
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdSmooth
        '
        Me.cmdSmooth.Location = New System.Drawing.Point(191, 135)
        Me.cmdSmooth.Name = "cmdSmooth"
        Me.cmdSmooth.Size = New System.Drawing.Size(60, 25)
        Me.cmdSmooth.TabIndex = 11
        Me.cmdSmooth.Text = "Smooth"
        Me.cmdSmooth.UseVisualStyleBackColor = True
        '
        'frmLPSmooth
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(272, 210)
        Me.Controls.Add(Me.cmdSmooth)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtDistance)
        Me.Controls.Add(Me.chCorner)
        Me.Controls.Add(Me.chNoEnds)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLPSmooth"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - Line or Poly Smoothing"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chNoEnds As System.Windows.Forms.CheckBox
    Friend WithEvents chCorner As System.Windows.Forms.CheckBox
    Friend WithEvents txtDistance As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdSmooth As System.Windows.Forms.Button

End Class
