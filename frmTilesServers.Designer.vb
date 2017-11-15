<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTilesServers
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTilesServers))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ckMapServer = New System.Windows.Forms.CheckBox()
        Me.ListMapServers = New System.Windows.Forms.ListBox()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(209, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(131, 108)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Label1"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(109, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Available Tile Servers"
        '
        'ckMapServer
        '
        Me.ckMapServer.AutoSize = True
        Me.ckMapServer.Enabled = False
        Me.ckMapServer.Location = New System.Drawing.Point(15, 151)
        Me.ckMapServer.Name = "ckMapServer"
        Me.ckMapServer.Size = New System.Drawing.Size(15, 14)
        Me.ckMapServer.TabIndex = 5
        Me.ckMapServer.UseVisualStyleBackColor = True
        '
        'ListMapServers
        '
        Me.ListMapServers.Enabled = False
        Me.ListMapServers.FormattingEnabled = True
        Me.ListMapServers.Location = New System.Drawing.Point(15, 25)
        Me.ListMapServers.Name = "ListMapServers"
        Me.ListMapServers.Size = New System.Drawing.Size(182, 108)
        Me.ListMapServers.TabIndex = 4
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(286, 145)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(54, 25)
        Me.cmdOK.TabIndex = 28
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(212, 145)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(54, 25)
        Me.cmdCancel.TabIndex = 27
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'frmTilesServers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(359, 181)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ckMapServer)
        Me.Controls.Add(Me.ListMapServers)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTilesServers"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - Tile Servers"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents ckMapServer As CheckBox
    Friend WithEvents ListMapServers As ListBox
    Friend WithEvents cmdOK As Button
    Friend WithEvents cmdCancel As Button
End Class
