<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGoogleMap
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGoogleMap))
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.buttonSat = New System.Windows.Forms.RadioButton()
        Me.buttonMap = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(205, 65)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(58, 25)
        Me.cmdOK.TabIndex = 4
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(130, 65)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(58, 25)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'buttonSat
        '
        Me.buttonSat.AutoSize = True
        Me.buttonSat.Checked = True
        Me.buttonSat.Location = New System.Drawing.Point(17, 13)
        Me.buttonSat.Name = "buttonSat"
        Me.buttonSat.Size = New System.Drawing.Size(62, 17)
        Me.buttonSat.TabIndex = 6
        Me.buttonSat.TabStop = True
        Me.buttonSat.Text = "Satellite"
        Me.buttonSat.UseVisualStyleBackColor = True
        '
        'buttonMap
        '
        Me.buttonMap.AutoSize = True
        Me.buttonMap.Location = New System.Drawing.Point(17, 39)
        Me.buttonMap.Name = "buttonMap"
        Me.buttonMap.Size = New System.Drawing.Size(72, 17)
        Me.buttonMap.TabIndex = 7
        Me.buttonMap.Text = "RoadMap"
        Me.buttonMap.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(110, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(175, 44)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "An image of 1280 x 1280 pixels at the present SBuilderX Zoom level will be added " &
    "to your workspace"
        '
        'frmGoogleMap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 107)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.buttonMap)
        Me.Controls.Add(Me.buttonSat)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGoogleMap"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - Add Google Map"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents buttonSat As System.Windows.Forms.RadioButton
    Friend WithEvents buttonMap As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
