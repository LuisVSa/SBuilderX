<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFSXSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFSXSettings))
        Me.cmdFSX = New System.Windows.Forms.Button()
        Me.txtFSPath = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdTerrain = New System.Windows.Forms.Button()
        Me.txtTerrain = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdBGLComp = New System.Windows.Forms.Button()
        Me.txtBGLComp = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.cmdPlugins = New System.Windows.Forms.Button()
        Me.txtPlugins = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtNameOfSim = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'cmdFSX
        '
        Me.cmdFSX.Location = New System.Drawing.Point(514, 33)
        Me.cmdFSX.Name = "cmdFSX"
        Me.cmdFSX.Size = New System.Drawing.Size(31, 23)
        Me.cmdFSX.TabIndex = 0
        Me.cmdFSX.Text = "..."
        Me.cmdFSX.UseVisualStyleBackColor = True
        '
        'txtFSPath
        '
        Me.txtFSPath.BackColor = System.Drawing.Color.White
        Me.txtFSPath.Location = New System.Drawing.Point(22, 35)
        Me.txtFSPath.Name = "txtFSPath"
        Me.txtFSPath.ReadOnly = True
        Me.txtFSPath.Size = New System.Drawing.Size(476, 20)
        Me.txtFSPath.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(134, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Full Path to Flight Simulator"
        '
        'cmdTerrain
        '
        Me.cmdTerrain.Location = New System.Drawing.Point(460, 165)
        Me.cmdTerrain.Name = "cmdTerrain"
        Me.cmdTerrain.Size = New System.Drawing.Size(85, 23)
        Me.cmdTerrain.TabIndex = 0
        Me.cmdTerrain.Text = "Copy From ..."
        Me.cmdTerrain.UseVisualStyleBackColor = True
        '
        'txtTerrain
        '
        Me.txtTerrain.BackColor = System.Drawing.SystemColors.Control
        Me.txtTerrain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTerrain.Location = New System.Drawing.Point(22, 168)
        Me.txtTerrain.Name = "txtTerrain"
        Me.txtTerrain.ReadOnly = True
        Me.txtTerrain.Size = New System.Drawing.Size(418, 20)
        Me.txtTerrain.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 151)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(238, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Shp2Vec.exe, Resample.exe and ImageTool.exe"
        '
        'cmdBGLComp
        '
        Me.cmdBGLComp.Location = New System.Drawing.Point(460, 206)
        Me.cmdBGLComp.Name = "cmdBGLComp"
        Me.cmdBGLComp.Size = New System.Drawing.Size(85, 23)
        Me.cmdBGLComp.TabIndex = 0
        Me.cmdBGLComp.Text = "Copy From ..."
        Me.cmdBGLComp.UseVisualStyleBackColor = True
        '
        'txtBGLComp
        '
        Me.txtBGLComp.BackColor = System.Drawing.SystemColors.Control
        Me.txtBGLComp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBGLComp.Location = New System.Drawing.Point(22, 208)
        Me.txtBGLComp.Name = "txtBGLComp"
        Me.txtBGLComp.ReadOnly = True
        Me.txtBGLComp.Size = New System.Drawing.Size(418, 20)
        Me.txtBGLComp.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 192)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(154, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "BglComp.exe and BglComp.xsd"
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(460, 295)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(85, 24)
        Me.cmdOK.TabIndex = 3
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(19, 295)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(430, 36)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Paths shown in RED do not seem correct. If you press OK, without correcting this " &
    "situation, SBuilderX can not generate some BGL files."
        '
        'cmdPlugins
        '
        Me.cmdPlugins.Location = New System.Drawing.Point(460, 252)
        Me.cmdPlugins.Name = "cmdPlugins"
        Me.cmdPlugins.Size = New System.Drawing.Size(85, 23)
        Me.cmdPlugins.TabIndex = 0
        Me.cmdPlugins.Text = "Copy From ..."
        Me.cmdPlugins.UseVisualStyleBackColor = True
        '
        'txtPlugins
        '
        Me.txtPlugins.BackColor = System.Drawing.SystemColors.Control
        Me.txtPlugins.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPlugins.Location = New System.Drawing.Point(22, 254)
        Me.txtPlugins.Name = "txtPlugins"
        Me.txtPlugins.ReadOnly = True
        Me.txtPlugins.Size = New System.Drawing.Size(418, 20)
        Me.txtPlugins.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 238)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(354, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "XToMdl.exe, Managed_CrashTree.dll and Managed_Lookup_Keyword.dll"
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Location = New System.Drawing.Point(19, 92)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(526, 2)
        Me.Label6.TabIndex = 6
        Me.Label6.UseWaitCursor = True
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(19, 111)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(526, 40)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = resources.GetString("Label7.Text")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(137, 64)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(305, 13)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Name of the Simulator. For Microsoft FSX you should enter FSX"
        '
        'txtNameOfSim
        '
        Me.txtNameOfSim.Location = New System.Drawing.Point(22, 61)
        Me.txtNameOfSim.Name = "txtNameOfSim"
        Me.txtNameOfSim.Size = New System.Drawing.Size(109, 20)
        Me.txtNameOfSim.TabIndex = 10
        '
        'FrmFSXSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(563, 332)
        Me.Controls.Add(Me.txtNameOfSim)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPlugins)
        Me.Controls.Add(Me.txtBGLComp)
        Me.Controls.Add(Me.cmdPlugins)
        Me.Controls.Add(Me.txtTerrain)
        Me.Controls.Add(Me.cmdBGLComp)
        Me.Controls.Add(Me.cmdTerrain)
        Me.Controls.Add(Me.txtFSPath)
        Me.Controls.Add(Me.cmdFSX)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFSXSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - FSX Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdFSX As System.Windows.Forms.Button
    Friend WithEvents txtFSPath As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdTerrain As System.Windows.Forms.Button
    Friend WithEvents txtTerrain As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdBGLComp As System.Windows.Forms.Button
    Friend WithEvents txtBGLComp As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents cmdPlugins As System.Windows.Forms.Button
    Friend WithEvents txtPlugins As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents txtNameOfSim As TextBox
End Class
