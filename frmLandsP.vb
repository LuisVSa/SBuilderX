Friend Class FrmLandsP
    Private EntryLC As Byte


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
            FillLand(N)
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


    Private Sub CmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Dispose()
    End Sub

    Private Sub CmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        DefaultLC = EntryLC
        Dispose()
    End Sub

    Private Sub FrmLandsP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim A As String
        Dim N As Integer
        Dim jpg As String = ".jpg"

        LandWaterDELETE = False
        LandWaterRASTER = False

        'SetLWCIs()

        For N = 1 To NoOfLCs
            List1.Items.Add(LC(N).Caption)
        Next N

        EntryLC = DefaultLC
        N = CInt(DefaultLC)

        lbLand.Text = List1.GetItemText(N - 1)
        lbLand.BackColor = LC(N).Color
        lbLand.ForeColor = InvertColor(LC(N).Color)

        A = My.Application.Info.DirectoryPath & "\tools\bmps\" & LC(N).Texture & jpg

        ImgText.Image = System.Drawing.Image.FromFile(A)
        ImageFileName = A

        'If Not PolyViewer Then lbPolyViewer.Text = vbCrLf & "PolyView not detected!"

    End Sub

    'Public Sub ShowLandProperty(ByVal POPIndex)

    '    Dim N, J, K, C, R As Integer
    '    Dim A As String

    '    N = POPIndex
    '    R = N Mod 512
    '    N = N >> 9
    '    C = N Mod 512
    '    N = N >> 6
    '    K = N Mod 64
    '    N = N >> 7
    '    J = N Mod 128

    '    N = CInt(LLands(C, R, LL_XY(J, K).Pointer) And 127)

    '    DefaultLC = CByte(N)

    '    lbLand.Text = VB6.GetItemString(List1, N - 1)
    '    List1.SelectedIndex = N - 1

    '    lbLand.BackColor = LC(N).Color

    '    lbLand.ForeColor = InvertColor(LC(N).Color)

    '    A = My.Application.Info.DirectoryPath & "\tools\bmps\" & LC(N).Texture & ".jpg"
    '    ImgText.Image = System.Drawing.Image.FromFile(A)



    'End Sub

    Private Sub List1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles List1.SelectedIndexChanged

        Dim N As Integer
        Dim A As String
        Dim jpg As String = ".jpg"

        N = List1.SelectedIndex + 1
        DefaultLC = CByte(N)

        lbLand.Text = List1.GetItemText(N - 1)
        lbLand.BackColor = LC(N).Color

        lbLand.ForeColor = InvertColor(LC(N).Color)

        A = My.Application.Info.DirectoryPath & "\tools\bmps\" & LC(N).Texture & jpg
        ImgText.Image = System.Drawing.Image.FromFile(A)
        ImageFileName = A

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


    Private Sub OptClick_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optClick.CheckedChanged
        SetOptions()
    End Sub

    Private Sub OptRaster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optRaster.CheckedChanged
        SetOptions()
    End Sub

    Private Sub OptDelete_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDelete.CheckedChanged
        SetOptions()
    End Sub

    Private Sub Ck1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ck1.CheckedChanged
        SetSize()
    End Sub

    Private Sub Ck2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ck2.CheckedChanged
        SetSize()
    End Sub

    Private Sub Ck4_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ck4.CheckedChanged
        SetSize()
    End Sub
    Private Sub SetSize()
        If ck1.Checked Then BrushSize = 1
        If ck2.Checked Then BrushSize = 2
        If ck4.Checked Then BrushSize = 4
    End Sub

    Private Sub LbLand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbLand.Click
        ARGBColor = lbLand.BackColor
        If FrmTransparency.ShowDialog = Windows.Forms.DialogResult.OK Then
            lbLand.BackColor = ARGBColor
        End If
    End Sub

    Private Sub ImgText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImgText.Click
        FrmImage.ShowDialog()
    End Sub
End Class