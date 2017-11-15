<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmArcGisMap
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmArcGisMap))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.buttonMap = New System.Windows.Forms.RadioButton()
        Me.buttonSat = New System.Windows.Forms.RadioButton()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.buttonTopo = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(110, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(175, 44)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "An image of 4096 x 4096 pixels at the present SBuilderX Zoom level will be added " &
    "to your workspace"
        '
        'buttonMap
        '
        Me.buttonMap.AutoSize = True
        Me.buttonMap.Location = New System.Drawing.Point(17, 36)
        Me.buttonMap.Name = "buttonMap"
        Me.buttonMap.Size = New System.Drawing.Size(72, 17)
        Me.buttonMap.TabIndex = 12
        Me.buttonMap.Text = "RoadMap"
        Me.buttonMap.UseVisualStyleBackColor = True
        '
        'buttonSat
        '
        Me.buttonSat.AutoSize = True
        Me.buttonSat.Checked = True
        Me.buttonSat.Location = New System.Drawing.Point(17, 13)
        Me.buttonSat.Name = "buttonSat"
        Me.buttonSat.Size = New System.Drawing.Size(62, 17)
        Me.buttonSat.TabIndex = 11
        Me.buttonSat.TabStop = True
        Me.buttonSat.Text = "Satellite"
        Me.buttonSat.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(130, 65)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(58, 25)
        Me.cmdCancel.TabIndex = 10
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(205, 65)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(58, 25)
        Me.cmdOK.TabIndex = 9
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'buttonTopo
        '
        Me.buttonTopo.AutoSize = True
        Me.buttonTopo.Location = New System.Drawing.Point(17, 59)
        Me.buttonTopo.Name = "buttonTopo"
        Me.buttonTopo.Size = New System.Drawing.Size(74, 17)
        Me.buttonTopo.TabIndex = 12
        Me.buttonTopo.Text = "Topo Map"
        Me.buttonTopo.UseVisualStyleBackColor = True
        '
        'frmArcGisMap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 107)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.buttonTopo)
        Me.Controls.Add(Me.buttonMap)
        Me.Controls.Add(Me.buttonSat)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmArcGisMap"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - Add ArcGis Map"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label2 As Label
    Friend WithEvents buttonMap As RadioButton
    Friend WithEvents buttonSat As RadioButton
    Friend WithEvents cmdCancel As Button
    Friend WithEvents cmdOK As Button
    Friend WithEvents buttonTopo As RadioButton
End Class
