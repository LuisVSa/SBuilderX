Imports System.Windows.Forms

Friend Class FrmSurfer


    Private Sub FrmSurfer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        txtStartW.Text = BLNStartWidth.ToString
        txtEndW.Text = BLNEndWidth.ToString

        txtLineGuid.Text = BLNLineGuid
        txtPolyGuid.Text = BLNPolyGuid

        lbLineColor.BackColor = BLNLineColor
        lbPolyColor.BackColor = BLNPolyColor

        ckAutoLine.Checked = BLNLineFromPoly
        ckLineAltitude.Checked = BLNIsLineAlt
        ckPolyAltitude.Checked = BLNIsPolyAlt

    End Sub

    Private Sub CmdContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdContinue.Click

        Dim A, B As String
        Dim W1, W2 As Double

        Try
            W1 = CDbl(txtStartW.Text)
            W2 = CDbl(txtEndW.Text)
        Catch ex As Exception
            MsgBox("Check width values!", MsgBoxStyle.Critical)
            Exit Sub
        End Try

        BLNStartWidth = W1
        BLNEndWidth = W2

        BLNLineGuid = txtLineGuid.Text
        BLNPolyGuid = txtPolyGuid.Text

        BLNLineType = GetLineTypeFromGuid(BLNLineGuid)
        BLNPolyType = GetPolyTypeFromGuid(BLNPolyGuid)

        BLNLineColor = lbLineColor.BackColor
        BLNPolyColor = lbPolyColor.BackColor

        BLNLineFromPoly = ckAutoLine.Checked
        BLNIsLineAlt = ckLineAltitude.Checked
        BLNIsPolyAlt = ckPolyAltitude.Checked

        Hide()

        'WriteBLNSettings()
        WriteSettings()

        A = "Surfer File (*.BLN)|*.BLN"
        B = "SBuilderX: Append Surfer File"

        A = FileNameToOpen(A, B, "SUR")

        If A = "" Then
            Dispose()
            Exit Sub
        End If

        AppendSurfer(A)
        Dirty = False

        Dispose()

    End Sub

    Private Sub CmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Dispose()

    End Sub

    Private Sub CkAutoLine_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ckAutoLine.CheckStateChanged

        If ckAutoLine.CheckState = 1 Then
            BLNLineFromPoly = True
        Else
            BLNLineFromPoly = False
        End If

    End Sub

    Private Sub CkPolyAltitude_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckPolyAltitude.CheckStateChanged

        If ckPolyAltitude.CheckState = 1 Then
            BLNIsPolyAlt = True
        Else
            BLNIsPolyAlt = False
        End If

    End Sub

    Private Sub CkLineAltitude_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckLineAltitude.CheckStateChanged

        If ckLineAltitude.CheckState = 1 Then
            BLNIsLineAlt = True
        Else
            BLNIsLineAlt = False
        End If

    End Sub

    Private Function GetLineTypeFromGuid(ByVal guid As String) As String

        GetLineTypeFromGuid = ""

        Dim K As Integer

        For K = 1 To NoOfLineTypes
            If LineTypes(K).Guid = guid Then
                GetLineTypeFromGuid = LineTypes(K).Type
                Exit For
            End If
        Next

    End Function

    Private Function GetPolyTypeFromGuid(ByVal guid As String) As String

        GetPolyTypeFromGuid = ""

        Dim K As Integer

        For K = 1 To NoOfPolyTypes
            If PolyTypes(K).Guid = guid Then
                GetPolyTypeFromGuid = PolyTypes(K).Type
                Exit For
            End If
        Next

    End Function


    Private Sub TxtLineGuid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLineGuid.Click

        POPMode = "SUR"
        FrmLinesP.ShowDialog()
        txtLineGuid.Text = BLNLineGuid

    End Sub

    Private Sub TxtPolyGuid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPolyGuid.Click

        POPMode = "SUR"
        FrmPolysP.ShowDialog()
        txtPolyGuid.Text = BLNPolyGuid

    End Sub

    Private Sub LbLineColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbLineColor.Click

        'frmStart.ColorDialog1.Color = lbLineColor.BackColor
        '' Update the color if the user clicks OK 
        'If frmStart.ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
        '    lbLineColor.BackColor = frmStart.ColorDialog1.Color
        'End If

        ARGBColor = lbLineColor.BackColor
        If FrmTransparency.ShowDialog = Windows.Forms.DialogResult.OK Then
            lbLineColor.BackColor = ARGBColor
        End If

    End Sub

    Private Sub LbPolyColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbPolyColor.Click

        'frmStart.ColorDialog1.Color = lbPolyColor.BackColor
        '' Update the color if the user clicks OK 
        'If frmStart.ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
        '    lbPolyColor.BackColor = frmStart.ColorDialog1.Color
        'End If

        ARGBColor = lbPolyColor.BackColor
        If FrmTransparency.ShowDialog = Windows.Forms.DialogResult.OK Then
            lbPolyColor.BackColor = ARGBColor
        End If

    End Sub


End Class
