<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTerrainExclude
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTerrainExclude))
        Me.List = New System.Windows.Forms.ListBox
        Me.cmdDetail = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.OK_Button = New System.Windows.Forms.Button
        Me.lbTex = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'List
        '
        Me.List.FormattingEnabled = True
        Me.List.Location = New System.Drawing.Point(10, 42)
        Me.List.Name = "List"
        Me.List.Size = New System.Drawing.Size(265, 82)
        Me.List.TabIndex = 0
        '
        'cmdDetail
        '
        Me.cmdDetail.Location = New System.Drawing.Point(294, 12)
        Me.cmdDetail.Name = "cmdDetail"
        Me.cmdDetail.Size = New System.Drawing.Size(75, 25)
        Me.cmdDetail.TabIndex = 25
        Me.cmdDetail.Text = "Info"
        Me.cmdDetail.UseVisualStyleBackColor = True
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Location = New System.Drawing.Point(294, 54)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(75, 25)
        Me.Cancel_Button.TabIndex = 24
        Me.Cancel_Button.Text = "Cancel"
        Me.Cancel_Button.UseVisualStyleBackColor = True
        '
        'OK_Button
        '
        Me.OK_Button.Location = New System.Drawing.Point(294, 99)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(75, 25)
        Me.OK_Button.TabIndex = 23
        Me.OK_Button.Text = "OK"
        Me.OK_Button.UseVisualStyleBackColor = True
        '
        'lbTex
        '
        Me.lbTex.BackColor = System.Drawing.SystemColors.Control
        Me.lbTex.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbTex.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbTex.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbTex.Location = New System.Drawing.Point(12, 9)
        Me.lbTex.Name = "lbTex"
        Me.lbTex.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbTex.Size = New System.Drawing.Size(263, 19)
        Me.lbTex.TabIndex = 26
        Me.lbTex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmTerrainExclude
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(381, 141)
        Me.Controls.Add(Me.lbTex)
        Me.Controls.Add(Me.cmdDetail)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.List)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTerrainExclude"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - Terrain Type to Exclude"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents List As System.Windows.Forms.ListBox
    Friend WithEvents cmdDetail As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Public WithEvents lbTex As System.Windows.Forms.Label

End Class
