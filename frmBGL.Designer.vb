<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBGL
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBGL))
        Me.ckStartFSX = New System.Windows.Forms.CheckBox
        Me.ckCopyBGLs = New System.Windows.Forms.CheckBox
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdCompile = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.ckLinesOfObjects = New System.Windows.Forms.CheckBox
        Me.ckTexPolys = New System.Windows.Forms.CheckBox
        Me.ckExcludes = New System.Windows.Forms.CheckBox
        Me.ckObjects = New System.Windows.Forms.CheckBox
        Me.ckExtrusions = New System.Windows.Forms.CheckBox
        Me.ckTexLines = New System.Windows.Forms.CheckBox
        Me.ckPhoto = New System.Windows.Forms.CheckBox
        Me.lbNoSelection = New System.Windows.Forms.Label
        Me.ckWater = New System.Windows.Forms.CheckBox
        Me.ckLand = New System.Windows.Forms.CheckBox
        Me.ckVector = New System.Windows.Forms.CheckBox
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ckStartFSX
        '
        Me.ckStartFSX.AutoSize = True
        Me.ckStartFSX.BackColor = System.Drawing.SystemColors.Control
        Me.ckStartFSX.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ckStartFSX.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckStartFSX.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckStartFSX.Location = New System.Drawing.Point(280, 35)
        Me.ckStartFSX.Name = "ckStartFSX"
        Me.ckStartFSX.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ckStartFSX.Size = New System.Drawing.Size(71, 17)
        Me.ckStartFSX.TabIndex = 23
        Me.ckStartFSX.Text = "Start FSX"
        Me.ckStartFSX.UseVisualStyleBackColor = False
        '
        'ckCopyBGLs
        '
        Me.ckCopyBGLs.BackColor = System.Drawing.SystemColors.Control
        Me.ckCopyBGLs.Cursor = System.Windows.Forms.Cursors.Default
        Me.ckCopyBGLs.Enabled = False
        Me.ckCopyBGLs.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ckCopyBGLs.Location = New System.Drawing.Point(280, 58)
        Me.ckCopyBGLs.Name = "ckCopyBGLs"
        Me.ckCopyBGLs.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ckCopyBGLs.Size = New System.Drawing.Size(97, 34)
        Me.ckCopyBGLs.TabIndex = 22
        Me.ckCopyBGLs.Text = "Copy BGL files to BGL folder"
        Me.ckCopyBGLs.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(300, 135)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(64, 25)
        Me.cmdCancel.TabIndex = 21
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdCompile
        '
        Me.cmdCompile.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCompile.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCompile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCompile.Location = New System.Drawing.Point(300, 181)
        Me.cmdCompile.Name = "cmdCompile"
        Me.cmdCompile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCompile.Size = New System.Drawing.Size(64, 25)
        Me.cmdCompile.TabIndex = 20
        Me.cmdCompile.Text = "Compile"
        Me.cmdCompile.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ckLinesOfObjects)
        Me.GroupBox1.Controls.Add(Me.ckTexPolys)
        Me.GroupBox1.Controls.Add(Me.ckExcludes)
        Me.GroupBox1.Controls.Add(Me.ckObjects)
        Me.GroupBox1.Controls.Add(Me.ckExtrusions)
        Me.GroupBox1.Controls.Add(Me.ckTexLines)
        Me.GroupBox1.Controls.Add(Me.ckPhoto)
        Me.GroupBox1.Controls.Add(Me.lbNoSelection)
        Me.GroupBox1.Controls.Add(Me.ckWater)
        Me.GroupBox1.Controls.Add(Me.ckLand)
        Me.GroupBox1.Controls.Add(Me.ckVector)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(247, 216)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Type of Scenery"
        '
        'ckLinesOfObjects
        '
        Me.ckLinesOfObjects.AutoSize = True
        Me.ckLinesOfObjects.Location = New System.Drawing.Point(132, 103)
        Me.ckLinesOfObjects.Name = "ckLinesOfObjects"
        Me.ckLinesOfObjects.Size = New System.Drawing.Size(102, 17)
        Me.ckLinesOfObjects.TabIndex = 8
        Me.ckLinesOfObjects.Text = "Lines of Objects"
        Me.ckLinesOfObjects.UseVisualStyleBackColor = True
        '
        'ckTexPolys
        '
        Me.ckTexPolys.AutoSize = True
        Me.ckTexPolys.Enabled = False
        Me.ckTexPolys.Location = New System.Drawing.Point(132, 28)
        Me.ckTexPolys.Name = "ckTexPolys"
        Me.ckTexPolys.Size = New System.Drawing.Size(96, 17)
        Me.ckTexPolys.TabIndex = 7
        Me.ckTexPolys.Text = "Textured Polys"
        Me.ckTexPolys.UseVisualStyleBackColor = True
        '
        'ckExcludes
        '
        Me.ckExcludes.Enabled = False
        Me.ckExcludes.Location = New System.Drawing.Point(16, 122)
        Me.ckExcludes.Name = "ckExcludes"
        Me.ckExcludes.Size = New System.Drawing.Size(108, 34)
        Me.ckExcludes.TabIndex = 6
        Me.ckExcludes.Text = "Exclusion Rectangles"
        Me.ckExcludes.UseVisualStyleBackColor = True
        '
        'ckObjects
        '
        Me.ckObjects.AutoSize = True
        Me.ckObjects.Enabled = False
        Me.ckObjects.Location = New System.Drawing.Point(132, 131)
        Me.ckObjects.Name = "ckObjects"
        Me.ckObjects.Size = New System.Drawing.Size(62, 17)
        Me.ckObjects.TabIndex = 5
        Me.ckObjects.Text = "Objects"
        Me.ckObjects.UseVisualStyleBackColor = True
        '
        'ckExtrusions
        '
        Me.ckExtrusions.AutoSize = True
        Me.ckExtrusions.Enabled = False
        Me.ckExtrusions.Location = New System.Drawing.Point(132, 78)
        Me.ckExtrusions.Name = "ckExtrusions"
        Me.ckExtrusions.Size = New System.Drawing.Size(107, 17)
        Me.ckExtrusions.TabIndex = 4
        Me.ckExtrusions.Text = "Extrusion Bridges"
        Me.ckExtrusions.UseVisualStyleBackColor = True
        '
        'ckTexLines
        '
        Me.ckTexLines.AutoSize = True
        Me.ckTexLines.Enabled = False
        Me.ckTexLines.Location = New System.Drawing.Point(132, 53)
        Me.ckTexLines.Name = "ckTexLines"
        Me.ckTexLines.Size = New System.Drawing.Size(96, 17)
        Me.ckTexLines.TabIndex = 4
        Me.ckTexLines.Text = "Textured Lines"
        Me.ckTexLines.UseVisualStyleBackColor = True
        '
        'ckPhoto
        '
        Me.ckPhoto.AutoSize = True
        Me.ckPhoto.Enabled = False
        Me.ckPhoto.Location = New System.Drawing.Point(16, 103)
        Me.ckPhoto.Name = "ckPhoto"
        Me.ckPhoto.Size = New System.Drawing.Size(96, 17)
        Me.ckPhoto.TabIndex = 4
        Me.ckPhoto.Text = "Photo Scenery"
        Me.ckPhoto.UseVisualStyleBackColor = True
        '
        'lbNoSelection
        '
        Me.lbNoSelection.Location = New System.Drawing.Point(13, 163)
        Me.lbNoSelection.Name = "lbNoSelection"
        Me.lbNoSelection.Size = New System.Drawing.Size(227, 42)
        Me.lbNoSelection.TabIndex = 3
        Me.lbNoSelection.Text = "There is nothing to compile because no items have been selected! Go back and sele" & _
            "ct the items to compile!"
        '
        'ckWater
        '
        Me.ckWater.AutoSize = True
        Me.ckWater.Enabled = False
        Me.ckWater.Location = New System.Drawing.Point(16, 78)
        Me.ckWater.Name = "ckWater"
        Me.ckWater.Size = New System.Drawing.Size(83, 17)
        Me.ckWater.TabIndex = 2
        Me.ckWater.Text = "Water Class"
        Me.ckWater.UseVisualStyleBackColor = True
        '
        'ckLand
        '
        Me.ckLand.AutoSize = True
        Me.ckLand.Enabled = False
        Me.ckLand.Location = New System.Drawing.Point(16, 53)
        Me.ckLand.Name = "ckLand"
        Me.ckLand.Size = New System.Drawing.Size(78, 17)
        Me.ckLand.TabIndex = 1
        Me.ckLand.Text = "Land Class"
        Me.ckLand.UseVisualStyleBackColor = True
        '
        'ckVector
        '
        Me.ckVector.AutoSize = True
        Me.ckVector.Enabled = False
        Me.ckVector.Location = New System.Drawing.Point(16, 28)
        Me.ckVector.Name = "ckVector"
        Me.ckVector.Size = New System.Drawing.Size(93, 17)
        Me.ckVector.TabIndex = 0
        Me.ckVector.Text = "Terrain Vector"
        Me.ckVector.UseVisualStyleBackColor = True
        '
        'frmBGL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(388, 247)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ckStartFSX)
        Me.Controls.Add(Me.ckCopyBGLs)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdCompile)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBGL"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - BGL Compilation"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents ckStartFSX As System.Windows.Forms.CheckBox
    Public WithEvents ckCopyBGLs As System.Windows.Forms.CheckBox
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdCompile As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ckVector As System.Windows.Forms.CheckBox
    Friend WithEvents ckWater As System.Windows.Forms.CheckBox
    Friend WithEvents ckLand As System.Windows.Forms.CheckBox
    Friend WithEvents lbNoSelection As System.Windows.Forms.Label
    Friend WithEvents ckPhoto As System.Windows.Forms.CheckBox
    Friend WithEvents ckObjects As System.Windows.Forms.CheckBox
    Friend WithEvents ckExcludes As System.Windows.Forms.CheckBox
    Friend WithEvents ckTexPolys As System.Windows.Forms.CheckBox
    Friend WithEvents ckTexLines As System.Windows.Forms.CheckBox
    Friend WithEvents ckExtrusions As System.Windows.Forms.CheckBox
    Friend WithEvents ckLinesOfObjects As System.Windows.Forms.CheckBox

End Class
