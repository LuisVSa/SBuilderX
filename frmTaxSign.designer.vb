<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmTaxSign
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
	Public WithEvents txtMessage As System.Windows.Forms.TextBox
	Public WithEvents cmdOK As System.Windows.Forms.Button
	Public WithEvents cmdCancel As System.Windows.Forms.Button
	Public WithEvents cmdINFO As System.Windows.Forms.Button
	Public WithEvents cmdRUN As System.Windows.Forms.Button
	Public WithEvents cmdDIR As System.Windows.Forms.Button
	Public WithEvents cmdLOC As System.Windows.Forms.Button
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents cmdDIV As System.Windows.Forms.Button
	Public WithEvents cmdXX As System.Windows.Forms.Button
	Public WithEvents cmdHOLD As System.Windows.Forms.Button
	Public WithEvents cmdILS As System.Windows.Forms.Button
	Public WithEvents cmdDD As System.Windows.Forms.Button
	Public WithEvents cmdDL As System.Windows.Forms.Button
	Public WithEvents cmdLL As System.Windows.Forms.Button
	Public WithEvents cmdUL As System.Windows.Forms.Button
	Public WithEvents cmdDR As System.Windows.Forms.Button
	Public WithEvents cmdRR As System.Windows.Forms.Button
	Public WithEvents cmdUR As System.Windows.Forms.Button
	Public WithEvents cmdUP As System.Windows.Forms.Button
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTaxSign))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtMessage = New System.Windows.Forms.TextBox
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.Frame2 = New System.Windows.Forms.GroupBox
        Me.cmdINFO = New System.Windows.Forms.Button
        Me.cmdRUN = New System.Windows.Forms.Button
        Me.cmdDIR = New System.Windows.Forms.Button
        Me.cmdLOC = New System.Windows.Forms.Button
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.cmdDIV = New System.Windows.Forms.Button
        Me.cmdXX = New System.Windows.Forms.Button
        Me.cmdHOLD = New System.Windows.Forms.Button
        Me.cmdILS = New System.Windows.Forms.Button
        Me.cmdDD = New System.Windows.Forms.Button
        Me.cmdDL = New System.Windows.Forms.Button
        Me.cmdLL = New System.Windows.Forms.Button
        Me.cmdUL = New System.Windows.Forms.Button
        Me.cmdDR = New System.Windows.Forms.Button
        Me.cmdRR = New System.Windows.Forms.Button
        Me.cmdUR = New System.Windows.Forms.Button
        Me.cmdUP = New System.Windows.Forms.Button
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtMessage
        '
        Me.txtMessage.AcceptsReturn = True
        Me.txtMessage.BackColor = System.Drawing.SystemColors.Window
        Me.txtMessage.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMessage.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMessage.Location = New System.Drawing.Point(204, 96)
        Me.txtMessage.MaxLength = 0
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMessage.Size = New System.Drawing.Size(133, 20)
        Me.txtMessage.TabIndex = 11
        Me.txtMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(282, 132)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(55, 25)
        Me.cmdOK.TabIndex = 10
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(204, 132)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(55, 25)
        Me.cmdCancel.TabIndex = 9
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.cmdINFO)
        Me.Frame2.Controls.Add(Me.cmdRUN)
        Me.Frame2.Controls.Add(Me.cmdDIR)
        Me.Frame2.Controls.Add(Me.cmdLOC)
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Frame2.Location = New System.Drawing.Point(192, 18)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(157, 61)
        Me.Frame2.TabIndex = 8
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Change Type"
        '
        'cmdINFO
        '
        Me.cmdINFO.BackColor = System.Drawing.SystemColors.Control
        Me.cmdINFO.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdINFO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdINFO.Image = CType(resources.GetObject("cmdINFO.Image"), System.Drawing.Image)
        Me.cmdINFO.Location = New System.Drawing.Point(120, 24)
        Me.cmdINFO.Name = "cmdINFO"
        Me.cmdINFO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdINFO.Size = New System.Drawing.Size(25, 25)
        Me.cmdINFO.TabIndex = 20
        Me.cmdINFO.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdINFO.UseVisualStyleBackColor = False
        '
        'cmdRUN
        '
        Me.cmdRUN.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRUN.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRUN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRUN.Image = CType(resources.GetObject("cmdRUN.Image"), System.Drawing.Image)
        Me.cmdRUN.Location = New System.Drawing.Point(84, 24)
        Me.cmdRUN.Name = "cmdRUN"
        Me.cmdRUN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRUN.Size = New System.Drawing.Size(25, 25)
        Me.cmdRUN.TabIndex = 14
        Me.cmdRUN.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdRUN.UseVisualStyleBackColor = False
        '
        'cmdDIR
        '
        Me.cmdDIR.BackColor = System.Drawing.Color.White
        Me.cmdDIR.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDIR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDIR.Image = CType(resources.GetObject("cmdDIR.Image"), System.Drawing.Image)
        Me.cmdDIR.Location = New System.Drawing.Point(48, 24)
        Me.cmdDIR.Name = "cmdDIR"
        Me.cmdDIR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDIR.Size = New System.Drawing.Size(25, 25)
        Me.cmdDIR.TabIndex = 13
        Me.cmdDIR.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdDIR.UseVisualStyleBackColor = False
        '
        'cmdLOC
        '
        Me.cmdLOC.BackColor = System.Drawing.Color.White
        Me.cmdLOC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLOC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLOC.Image = CType(resources.GetObject("cmdLOC.Image"), System.Drawing.Image)
        Me.cmdLOC.Location = New System.Drawing.Point(12, 24)
        Me.cmdLOC.Name = "cmdLOC"
        Me.cmdLOC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLOC.Size = New System.Drawing.Size(25, 25)
        Me.cmdLOC.TabIndex = 12
        Me.cmdLOC.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdLOC.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cmdDIV)
        Me.Frame1.Controls.Add(Me.cmdXX)
        Me.Frame1.Controls.Add(Me.cmdHOLD)
        Me.Frame1.Controls.Add(Me.cmdILS)
        Me.Frame1.Controls.Add(Me.cmdDD)
        Me.Frame1.Controls.Add(Me.cmdDL)
        Me.Frame1.Controls.Add(Me.cmdLL)
        Me.Frame1.Controls.Add(Me.cmdUL)
        Me.Frame1.Controls.Add(Me.cmdDR)
        Me.Frame1.Controls.Add(Me.cmdRR)
        Me.Frame1.Controls.Add(Me.cmdUR)
        Me.Frame1.Controls.Add(Me.cmdUP)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Frame1.Location = New System.Drawing.Point(12, 18)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(157, 139)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Special symbols"
        '
        'cmdDIV
        '
        Me.cmdDIV.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDIV.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDIV.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDIV.Image = CType(resources.GetObject("cmdDIV.Image"), System.Drawing.Image)
        Me.cmdDIV.Location = New System.Drawing.Point(120, 99)
        Me.cmdDIV.Name = "cmdDIV"
        Me.cmdDIV.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDIV.Size = New System.Drawing.Size(25, 25)
        Me.cmdDIV.TabIndex = 19
        Me.cmdDIV.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdDIV.UseVisualStyleBackColor = False
        '
        'cmdXX
        '
        Me.cmdXX.BackColor = System.Drawing.SystemColors.Control
        Me.cmdXX.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdXX.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdXX.Image = CType(resources.GetObject("cmdXX.Image"), System.Drawing.Image)
        Me.cmdXX.Location = New System.Drawing.Point(12, 99)
        Me.cmdXX.Name = "cmdXX"
        Me.cmdXX.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdXX.Size = New System.Drawing.Size(25, 25)
        Me.cmdXX.TabIndex = 18
        Me.cmdXX.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdXX.UseVisualStyleBackColor = False
        '
        'cmdHOLD
        '
        Me.cmdHOLD.BackColor = System.Drawing.SystemColors.Control
        Me.cmdHOLD.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdHOLD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdHOLD.Image = CType(resources.GetObject("cmdHOLD.Image"), System.Drawing.Image)
        Me.cmdHOLD.Location = New System.Drawing.Point(48, 99)
        Me.cmdHOLD.Name = "cmdHOLD"
        Me.cmdHOLD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdHOLD.Size = New System.Drawing.Size(25, 25)
        Me.cmdHOLD.TabIndex = 17
        Me.cmdHOLD.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdHOLD.UseVisualStyleBackColor = False
        '
        'cmdILS
        '
        Me.cmdILS.BackColor = System.Drawing.SystemColors.Control
        Me.cmdILS.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdILS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdILS.Image = CType(resources.GetObject("cmdILS.Image"), System.Drawing.Image)
        Me.cmdILS.Location = New System.Drawing.Point(84, 99)
        Me.cmdILS.Name = "cmdILS"
        Me.cmdILS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdILS.Size = New System.Drawing.Size(25, 25)
        Me.cmdILS.TabIndex = 16
        Me.cmdILS.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdILS.UseVisualStyleBackColor = False
        '
        'cmdDD
        '
        Me.cmdDD.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDD.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDD.Image = CType(resources.GetObject("cmdDD.Image"), System.Drawing.Image)
        Me.cmdDD.Location = New System.Drawing.Point(120, 60)
        Me.cmdDD.Name = "cmdDD"
        Me.cmdDD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDD.Size = New System.Drawing.Size(25, 25)
        Me.cmdDD.TabIndex = 15
        Me.cmdDD.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdDD.UseVisualStyleBackColor = False
        '
        'cmdDL
        '
        Me.cmdDL.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDL.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDL.Image = CType(resources.GetObject("cmdDL.Image"), System.Drawing.Image)
        Me.cmdDL.Location = New System.Drawing.Point(12, 24)
        Me.cmdDL.Name = "cmdDL"
        Me.cmdDL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDL.Size = New System.Drawing.Size(25, 25)
        Me.cmdDL.TabIndex = 7
        Me.cmdDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdDL.UseVisualStyleBackColor = False
        '
        'cmdLL
        '
        Me.cmdLL.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLL.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLL.Image = CType(resources.GetObject("cmdLL.Image"), System.Drawing.Image)
        Me.cmdLL.Location = New System.Drawing.Point(43, 24)
        Me.cmdLL.Name = "cmdLL"
        Me.cmdLL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLL.Size = New System.Drawing.Size(25, 25)
        Me.cmdLL.TabIndex = 6
        Me.cmdLL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdLL.UseVisualStyleBackColor = False
        '
        'cmdUL
        '
        Me.cmdUL.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUL.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUL.Image = CType(resources.GetObject("cmdUL.Image"), System.Drawing.Image)
        Me.cmdUL.Location = New System.Drawing.Point(84, 24)
        Me.cmdUL.Name = "cmdUL"
        Me.cmdUL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUL.Size = New System.Drawing.Size(25, 25)
        Me.cmdUL.TabIndex = 5
        Me.cmdUL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdUL.UseVisualStyleBackColor = False
        '
        'cmdDR
        '
        Me.cmdDR.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDR.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDR.Image = CType(resources.GetObject("cmdDR.Image"), System.Drawing.Image)
        Me.cmdDR.Location = New System.Drawing.Point(84, 60)
        Me.cmdDR.Name = "cmdDR"
        Me.cmdDR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDR.Size = New System.Drawing.Size(25, 25)
        Me.cmdDR.TabIndex = 4
        Me.cmdDR.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdDR.UseVisualStyleBackColor = False
        '
        'cmdRR
        '
        Me.cmdRR.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRR.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRR.Image = CType(resources.GetObject("cmdRR.Image"), System.Drawing.Image)
        Me.cmdRR.Location = New System.Drawing.Point(48, 60)
        Me.cmdRR.Name = "cmdRR"
        Me.cmdRR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRR.Size = New System.Drawing.Size(25, 25)
        Me.cmdRR.TabIndex = 3
        Me.cmdRR.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdRR.UseVisualStyleBackColor = False
        '
        'cmdUR
        '
        Me.cmdUR.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUR.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUR.Image = CType(resources.GetObject("cmdUR.Image"), System.Drawing.Image)
        Me.cmdUR.Location = New System.Drawing.Point(12, 60)
        Me.cmdUR.Name = "cmdUR"
        Me.cmdUR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUR.Size = New System.Drawing.Size(25, 25)
        Me.cmdUR.TabIndex = 2
        Me.cmdUR.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdUR.UseVisualStyleBackColor = False
        '
        'cmdUP
        '
        Me.cmdUP.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUP.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUP.Image = CType(resources.GetObject("cmdUP.Image"), System.Drawing.Image)
        Me.cmdUP.Location = New System.Drawing.Point(120, 24)
        Me.cmdUP.Name = "cmdUP"
        Me.cmdUP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUP.Size = New System.Drawing.Size(25, 25)
        Me.cmdUP.TabIndex = 1
        Me.cmdUP.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdUP.UseVisualStyleBackColor = False
        '
        'frmTaxSign
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(364, 169)
        Me.Controls.Add(Me.txtMessage)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTaxSign"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "SBuilderX - Build Sign Message"
        Me.Frame2.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
End Class