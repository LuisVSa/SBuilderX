<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmOrder
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents cmdOK As System.Windows.Forms.Button
	Public WithEvents cmdCancel As System.Windows.Forms.Button
	Public WithEvents lstObject As System.Windows.Forms.ListBox
	Public WithEvents cmdDOWN As System.Windows.Forms.Button
	Public WithEvents cmdUP As System.Windows.Forms.Button
	Public WithEvents Label1 As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmOrder))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.lstObject = New System.Windows.Forms.ListBox
        Me.cmdDOWN = New System.Windows.Forms.Button
        Me.cmdUP = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(435, 177)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(61, 25)
        Me.cmdOK.TabIndex = 4
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(435, 136)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(61, 25)
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'lstObject
        '
        Me.lstObject.BackColor = System.Drawing.SystemColors.Window
        Me.lstObject.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstObject.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstObject.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstObject.ItemHeight = 15
        Me.lstObject.Location = New System.Drawing.Point(12, 33)
        Me.lstObject.Name = "lstObject"
        Me.lstObject.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstObject.Size = New System.Drawing.Size(400, 169)
        Me.lstObject.TabIndex = 2
        '
        'cmdDOWN
        '
        Me.cmdDOWN.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDOWN.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDOWN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDOWN.Image = CType(resources.GetObject("cmdDOWN.Image"), System.Drawing.Image)
        Me.cmdDOWN.Location = New System.Drawing.Point(450, 78)
        Me.cmdDOWN.Name = "cmdDOWN"
        Me.cmdDOWN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDOWN.Size = New System.Drawing.Size(28, 28)
        Me.cmdDOWN.TabIndex = 1
        Me.cmdDOWN.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdDOWN.UseVisualStyleBackColor = False
        '
        'cmdUP
        '
        Me.cmdUP.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUP.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUP.Image = CType(resources.GetObject("cmdUP.Image"), System.Drawing.Image)
        Me.cmdUP.Location = New System.Drawing.Point(450, 39)
        Me.cmdUP.Name = "cmdUP"
        Me.cmdUP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUP.Size = New System.Drawing.Size(28, 28)
        Me.cmdUP.TabIndex = 0
        Me.cmdUP.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdUP.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(364, 19)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Select the object and use the arrows to change the order"
        '
        'frmOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(513, 215)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.lstObject)
        Me.Controls.Add(Me.cmdDOWN)
        Me.Controls.Add(Me.cmdUP)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOrder"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SBuilderX - Object drawing order"
        Me.ResumeLayout(False)

    End Sub
#End Region 
End Class