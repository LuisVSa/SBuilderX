<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProjectP
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmProjectP))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lbPolys = New System.Windows.Forms.Label()
        Me.lbLines = New System.Windows.Forms.Label()
        Me.lbWaters = New System.Windows.Forms.Label()
        Me.lbMaps = New System.Windows.Forms.Label()
        Me.lbExcludes = New System.Windows.Forms.Label()
        Me.lbLands = New System.Windows.Forms.Label()
        Me.lbObjects = New System.Windows.Forms.Label()
        Me.ckBGLFolder = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtBGLFolder = New System.Windows.Forms.TextBox()
        Me.cmdBGL = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbClassItem = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lstClassItems = New System.Windows.Forms.ListBox()
        Me.cmdClassIndexDelete = New System.Windows.Forms.Button()
        Me.cmdClassIndexEdit = New System.Windows.Forms.Button()
        Me.cmdClassIndexAdd = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(334, 247)
        Me.TabControl1.TabIndex = 40
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.Transparent
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.ckBGLFolder)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.txtName)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.txtBGLFolder)
        Me.TabPage1.Controls.Add(Me.cmdBGL)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(326, 221)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.lbPolys)
        Me.GroupBox1.Controls.Add(Me.lbLines)
        Me.GroupBox1.Controls.Add(Me.lbWaters)
        Me.GroupBox1.Controls.Add(Me.lbMaps)
        Me.GroupBox1.Controls.Add(Me.lbExcludes)
        Me.GroupBox1.Controls.Add(Me.lbLands)
        Me.GroupBox1.Controls.Add(Me.lbObjects)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 110)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(294, 95)
        Me.GroupBox1.TabIndex = 58
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Existing Items"
        '
        'lbPolys
        '
        Me.lbPolys.BackColor = System.Drawing.Color.Transparent
        Me.lbPolys.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbPolys.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbPolys.Location = New System.Drawing.Point(107, 25)
        Me.lbPolys.Name = "lbPolys"
        Me.lbPolys.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbPolys.Size = New System.Drawing.Size(95, 15)
        Me.lbPolys.TabIndex = 32
        Me.lbPolys.Text = "Polys = 1"
        Me.lbPolys.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbLines
        '
        Me.lbLines.BackColor = System.Drawing.Color.Transparent
        Me.lbLines.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbLines.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbLines.Location = New System.Drawing.Point(6, 25)
        Me.lbLines.Name = "lbLines"
        Me.lbLines.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbLines.Size = New System.Drawing.Size(95, 15)
        Me.lbLines.TabIndex = 34
        Me.lbLines.Text = "Lines = 1"
        Me.lbLines.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbWaters
        '
        Me.lbWaters.BackColor = System.Drawing.Color.Transparent
        Me.lbWaters.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbWaters.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbWaters.Location = New System.Drawing.Point(107, 45)
        Me.lbWaters.Name = "lbWaters"
        Me.lbWaters.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbWaters.Size = New System.Drawing.Size(95, 15)
        Me.lbWaters.TabIndex = 35
        Me.lbWaters.Text = "Waters = 1"
        Me.lbWaters.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbMaps
        '
        Me.lbMaps.BackColor = System.Drawing.Color.Transparent
        Me.lbMaps.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbMaps.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbMaps.Location = New System.Drawing.Point(6, 65)
        Me.lbMaps.Name = "lbMaps"
        Me.lbMaps.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbMaps.Size = New System.Drawing.Size(95, 15)
        Me.lbMaps.TabIndex = 38
        Me.lbMaps.Text = "Maps = 1"
        Me.lbMaps.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbExcludes
        '
        Me.lbExcludes.BackColor = System.Drawing.Color.Transparent
        Me.lbExcludes.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbExcludes.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbExcludes.Location = New System.Drawing.Point(208, 25)
        Me.lbExcludes.Name = "lbExcludes"
        Me.lbExcludes.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbExcludes.Size = New System.Drawing.Size(80, 15)
        Me.lbExcludes.TabIndex = 39
        Me.lbExcludes.Text = "Excludes = 1"
        Me.lbExcludes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbLands
        '
        Me.lbLands.BackColor = System.Drawing.Color.Transparent
        Me.lbLands.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbLands.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbLands.Location = New System.Drawing.Point(6, 45)
        Me.lbLands.Name = "lbLands"
        Me.lbLands.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbLands.Size = New System.Drawing.Size(95, 15)
        Me.lbLands.TabIndex = 36
        Me.lbLands.Text = "Lands = 1"
        Me.lbLands.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbObjects
        '
        Me.lbObjects.BackColor = System.Drawing.Color.Transparent
        Me.lbObjects.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbObjects.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbObjects.Location = New System.Drawing.Point(107, 65)
        Me.lbObjects.Name = "lbObjects"
        Me.lbObjects.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbObjects.Size = New System.Drawing.Size(95, 15)
        Me.lbObjects.TabIndex = 33
        Me.lbObjects.Text = "Objects = 1"
        Me.lbObjects.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ckBGLFolder
        '
        Me.ckBGLFolder.AutoSize = True
        Me.ckBGLFolder.BackColor = System.Drawing.Color.Transparent
        Me.ckBGLFolder.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ckBGLFolder.Location = New System.Drawing.Point(125, 83)
        Me.ckBGLFolder.Name = "ckBGLFolder"
        Me.ckBGLFolder.Size = New System.Drawing.Size(168, 17)
        Me.ckBGLFolder.TabIndex = 57
        Me.ckBGLFolder.Text = "Use this folder in new Projects"
        Me.ckBGLFolder.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(79, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(76, 17)
        Me.Label1.TabIndex = 53
        Me.Label1.Text = "Project Name :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtName
        '
        Me.txtName.AcceptsReturn = True
        Me.txtName.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtName.Location = New System.Drawing.Point(161, 19)
        Me.txtName.MaxLength = 0
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.Size = New System.Drawing.Size(139, 20)
        Me.txtName.TabIndex = 54
        Me.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(12, 36)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(115, 16)
        Me.Label7.TabIndex = 51
        Me.Label7.Text = "BGL Folder"
        '
        'txtBGLFolder
        '
        Me.txtBGLFolder.AcceptsReturn = True
        Me.txtBGLFolder.BackColor = System.Drawing.SystemColors.Window
        Me.txtBGLFolder.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBGLFolder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBGLFolder.Location = New System.Drawing.Point(15, 55)
        Me.txtBGLFolder.MaxLength = 0
        Me.txtBGLFolder.Name = "txtBGLFolder"
        Me.txtBGLFolder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBGLFolder.Size = New System.Drawing.Size(240, 20)
        Me.txtBGLFolder.TabIndex = 49
        Me.txtBGLFolder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmdBGL
        '
        Me.cmdBGL.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.cmdBGL.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBGL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBGL.Location = New System.Drawing.Point(272, 55)
        Me.cmdBGL.Name = "cmdBGL"
        Me.cmdBGL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBGL.Size = New System.Drawing.Size(31, 22)
        Me.cmdBGL.TabIndex = 50
        Me.cmdBGL.Text = "..."
        Me.cmdBGL.UseVisualStyleBackColor = False
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.Transparent
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.lbClassItem)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.lstClassItems)
        Me.TabPage2.Controls.Add(Me.cmdClassIndexDelete)
        Me.TabPage2.Controls.Add(Me.cmdClassIndexEdit)
        Me.TabPage2.Controls.Add(Me.cmdClassIndexAdd)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(326, 221)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Class Scenery"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(6, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(314, 61)
        Me.Label2.TabIndex = 78
        Me.Label2.Text = resources.GetString("Label2.Text")
        '
        'lbClassItem
        '
        Me.lbClassItem.BackColor = System.Drawing.Color.White
        Me.lbClassItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbClassItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbClassItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbClassItem.Location = New System.Drawing.Point(15, 98)
        Me.lbClassItem.Name = "lbClassItem"
        Me.lbClassItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbClassItem.Size = New System.Drawing.Size(210, 20)
        Me.lbClassItem.TabIndex = 74
        Me.lbClassItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(12, 82)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(178, 13)
        Me.Label5.TabIndex = 73
        Me.Label5.Text = "Click to set Color (letf or right mouse)"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(12, 123)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(130, 13)
        Me.Label6.TabIndex = 72
        Me.Label6.Text = "Land or Water Definitions:"
        '
        'lstClassItems
        '
        Me.lstClassItems.BackColor = System.Drawing.SystemColors.Window
        Me.lstClassItems.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstClassItems.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstClassItems.Location = New System.Drawing.Point(15, 139)
        Me.lstClassItems.Name = "lstClassItems"
        Me.lstClassItems.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstClassItems.Size = New System.Drawing.Size(210, 69)
        Me.lstClassItems.TabIndex = 71
        '
        'cmdClassIndexDelete
        '
        Me.cmdClassIndexDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClassIndexDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClassIndexDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClassIndexDelete.Location = New System.Drawing.Point(253, 98)
        Me.cmdClassIndexDelete.Name = "cmdClassIndexDelete"
        Me.cmdClassIndexDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClassIndexDelete.Size = New System.Drawing.Size(51, 25)
        Me.cmdClassIndexDelete.TabIndex = 76
        Me.cmdClassIndexDelete.Text = "Delete"
        Me.cmdClassIndexDelete.UseVisualStyleBackColor = False
        '
        'cmdClassIndexEdit
        '
        Me.cmdClassIndexEdit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClassIndexEdit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClassIndexEdit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClassIndexEdit.Location = New System.Drawing.Point(253, 142)
        Me.cmdClassIndexEdit.Name = "cmdClassIndexEdit"
        Me.cmdClassIndexEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClassIndexEdit.Size = New System.Drawing.Size(51, 25)
        Me.cmdClassIndexEdit.TabIndex = 75
        Me.cmdClassIndexEdit.Text = "Edit"
        Me.cmdClassIndexEdit.UseVisualStyleBackColor = False
        '
        'cmdClassIndexAdd
        '
        Me.cmdClassIndexAdd.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClassIndexAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClassIndexAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClassIndexAdd.Location = New System.Drawing.Point(253, 183)
        Me.cmdClassIndexAdd.Name = "cmdClassIndexAdd"
        Me.cmdClassIndexAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClassIndexAdd.Size = New System.Drawing.Size(51, 25)
        Me.cmdClassIndexAdd.TabIndex = 77
        Me.cmdClassIndexAdd.Text = "Add"
        Me.cmdClassIndexAdd.UseVisualStyleBackColor = False
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(269, 266)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(77, 24)
        Me.cmdOK.TabIndex = 58
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(164, 266)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(77, 25)
        Me.cmdCancel.TabIndex = 57
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'FrmProjectP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(357, 301)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmProjectP"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - Project Properties"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents ckBGLFolder As System.Windows.Forms.CheckBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents txtBGLFolder As System.Windows.Forms.TextBox
    Public WithEvents cmdBGL As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdClassIndexAdd As System.Windows.Forms.Button
    Public WithEvents cmdClassIndexDelete As System.Windows.Forms.Button
    Public WithEvents cmdClassIndexEdit As System.Windows.Forms.Button
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents lbClassItem As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents lstClassItems As System.Windows.Forms.ListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents lbExcludes As System.Windows.Forms.Label
    Public WithEvents lbMaps As System.Windows.Forms.Label
    Public WithEvents lbLands As System.Windows.Forms.Label
    Public WithEvents lbWaters As System.Windows.Forms.Label
    Public WithEvents lbLines As System.Windows.Forms.Label
    Public WithEvents lbObjects As System.Windows.Forms.Label
    Public WithEvents lbPolys As System.Windows.Forms.Label

End Class
