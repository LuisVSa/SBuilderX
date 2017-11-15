Friend Class FrmWatersP
    Private EntryWC As Byte
    Private Sub OptClick_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optClick.CheckedChanged
        SetOptions()
    End Sub

    Private Sub OptRaster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optRaster.CheckedChanged
        SetOptions()
    End Sub

    Private Sub OptDelete_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDelete.CheckedChanged
        SetOptions()
    End Sub
    Private Sub SetOptions()

        If optClick.Checked Then
            LandWaterDELETE = False
            LandWaterRASTER = False
        End If

        If optRaster.Checked Then
            LandWaterDELETE = False
            LandWaterRASTER = True
        End If

        If optDelete.Checked Then
            LandWaterDELETE = True
            LandWaterRASTER = False
        End If

    End Sub

    Private Sub Ck1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ck1.CheckedChanged
        SetSize()
    End Sub

    Private Sub Ck2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ck2.CheckedChanged
        SetSize()
    End Sub

    Private Sub Ck4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ck4.CheckedChanged
        SetSize()
    End Sub
    Private Sub SetSize()
        If ck1.Checked Then BrushSize = 1
        If ck2.Checked Then BrushSize = 2
        If ck4.Checked Then BrushSize = 4
    End Sub

    Private Sub CmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        FrmProjectP.Show()
        FrmProjectP.TabControl1.SelectTab(1)
    End Sub

    Private Sub CmdAuto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAuto.Click

        Dim N As Integer
        Dim A As String
        Dim Flag As Boolean = False

        If NoOfMaps = 0 Then Exit Sub

        WAIT = True
        Cursor = System.Windows.Forms.Cursors.WaitCursor

        For N = 1 To NoOfMaps
            A = Mid(Maps(N).Name, 1, 5)
            A = UCase(A)
            If A <> "CLASS" Then GoTo NextMap
            FillWater(N)
            Flag = True
NextMap:
        Next N

        Cursor = System.Windows.Forms.Cursors.Arrow
        WAIT = False
        Dirty = True
        RebuildDisplay()

        If Flag Then
            MsgBox("Class Maps have been processed!", MsgBoxStyle.Information)
        Else
            MsgBox("No Class Map was found!", MsgBoxStyle.Information)
        End If


    End Sub

    Private Sub CmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        DefaultWC = EntryWC
        Dispose()
    End Sub

    Private Sub CmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Dispose()
    End Sub

    Private Sub FrmWatersP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim A As String
        Dim N As Integer
        Dim jpg As String = ".jpg"

        LandWaterDELETE = False
        LandWaterRASTER = False

        'SetLWCIs()

        For N = 1 To NoOfWCs
            List1.Items.Add(WC(N).Caption)
        Next N

        EntryWC = DefaultWC
        N = CInt(DefaultWC)

        ' lbWater.Text = VB6.GetItemString(List1, N - 1)
        lbWater.Text = List1.GetItemText(N - 1)
        lbWater.BackColor = WC(N).Color
        lbWater.ForeColor = InvertColor(WC(N).Color)

        A = My.Application.Info.DirectoryPath & "\tools\bmps\" & WC(N).Texture & jpg

        ImgText.Image = System.Drawing.Image.FromFile(A)
        ImageFileName = A

    End Sub

    Private Sub List1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles List1.SelectedIndexChanged

        Dim N As Integer
        Dim A As String
        Dim jpg As String = ".jpg"

        N = List1.SelectedIndex + 1
        DefaultWC = CByte(N)

        ' lbWater.Text = VB6.GetItemString(List1, N - 1)
        lbWater.Text = List1.GetItemText(N - 1)
        lbWater.BackColor = WC(N).Color

        lbWater.ForeColor = InvertColor(WC(N).Color)

        A = My.Application.Info.DirectoryPath & "\tools\bmps\" & WC(N).Texture & jpg
        ImgText.Image = System.Drawing.Image.FromFile(A)
        ImageFileName = A

    End Sub

    Private Sub LbWater_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbWater.Click
        ARGBColor = lbWater.BackColor
        If FrmTransparency.ShowDialog = Windows.Forms.DialogResult.OK Then
            lbWater.BackColor = ARGBColor
        End If
    End Sub

    Private Sub ImgText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImgText.Click
        FrmImage.ShowDialog()
    End Sub


End Class