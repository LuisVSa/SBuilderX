Imports System.Windows.Forms

Public Class FrmExtrusions

    Private Sub FrmExtrusions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        cmbComplexity.SelectedIndex = FrmLinesP.Complexity

        txtProfileGuid.Text = FrmLinesP.ProfileGuid
        txtMaterialGuid.Text = FrmLinesP.MaterialGuid
        txtPylonGuid.Text = FrmLinesP.PylonGuid
        txtWidth.Text = FrmLinesP.ExtrusionWidth.ToString
        txtProbability.Text = FrmLinesP.ExtrusionProbability.ToString
        ckSuppress.Checked = False
        If FrmLinesP.SuppressPlatform Then ckSuppress.Checked = True
        If Lines(POPIndex).NoOfPoints < 4 Then
            boxHeight.Enabled = False
            Exit Sub
        End If
        txtHeight.Text = Str(GetHeight)

    End Sub
    Private Function GetHeight() As Double

        Dim NP As Integer = Lines(POPIndex).NoOfPoints
        Dim L(NP) As Double
        L(1) = 0

        For K As Integer = 2 To NP
            L(K) = L(K - 1) + GetDistanceP1P2(POPIndex, K - 1, K)
        Next
        Dim W As Double = L(NP) / 2

        If L(2) > W Then L(2) = 2 * W - L(2)
        If L(3) > W Then L(3) = 2 * W - L(3)

        Dim a, b, c, d, x, gh As Double  'now invert a square 2 x 2 matrix

        x = L(2)
        a = x * x
        b = a * x
        x = L(3)
        c = x * x
        d = c * x

        a = a * d - b * c
        b = -b / a
        a = d / a

        c = a * Lines(POPIndex).GLPoints(2).alt + b * Lines(POPIndex).GLPoints(3).alt
        gh = Math.Round(W * W * c / 3, 2)

        ' now the other exterme
        If L(NP - 1) > W Then L(NP - 1) = 2 * W - L(NP - 1)
        If L(NP - 2) > W Then L(NP - 2) = 2 * W - L(NP - 2)

        x = L(NP - 1)
        a = x * x
        b = a * x
        x = L(NP - 2)
        c = x * x
        d = c * x

        a = a * d - b * c
        b = -b / a
        a = d / a

        c = a * Lines(POPIndex).GLPoints(NP - 1).alt + b * Lines(POPIndex).GLPoints(NP - 2).alt
        GetHeight = Math.Round(W * W * c / 3, 2)

        If GetHeight <> gh Then GetHeight = 0
        If GetHeight < 0 Then GetHeight = 0

    End Function

    Private Function GetDistanceP1P2(ByVal line As Integer, ByVal p1 As Integer, ByVal p2 As Integer) As Double

        Dim lat1 As Double = Lines(line).GLPoints(p1).lat
        Dim lat2 As Double = Lines(line).GLPoints(p2).lat
        Dim lon1 As Double = Lines(line).GLPoints(p1).lon
        Dim lon2 As Double = Lines(line).GLPoints(p2).lon
        Dim lat As Double = (lat1 + lat2) / 2
        Dim DX As Double = (lon1 - lon2) * MetersPerDegLon(lat)   ' in meters pos or neg
        Dim DY As Double = (lat1 - lat2) * MetersPerDegLat
        DX = DX * DX + DY * DY
        GetDistanceP1P2 = System.Math.Sqrt(DX)

    End Function


    Private Sub CmdSetHeight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetHeight.Click

        Dim K, NP As Integer

        NP = Lines(POPIndex).NoOfPoints
        Dim L(NP) As Double
        L(1) = 0
        For K = 2 To NP
            L(K) = L(K - 1) + GetDistanceP1P2(POPIndex, K - 1, K)
        Next
        Dim W As Double = L(NP) / 2

        Dim H As Double = Val(txtHeight.Text)
        Dim W3 As Double = W * W * W
        Dim K1 As Double = 6 * H / W3
        Dim K2 As Double = -K1 / 3
        K1 = K1 * W / 2

        Dim X2, X3 As Double
        Lines(POPIndex).GLPoints(1).alt = 0
        For K = 2 To NP
            If L(K) > W Then L(K) = 2 * W - L(K)
            X2 = L(K)
            X3 = X2 * X2 * X2
            X2 = X2 * X2
            Lines(POPIndex).GLPoints(K).alt = K1 * X2 + K2 * X3
        Next

    End Sub


    Private Sub CmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        FrmLinesP.Complexity = cmbComplexity.SelectedIndex

        FrmLinesP.ProfileGuid = txtProfileGuid.Text
        FrmLinesP.MaterialGuid = txtMaterialGuid.Text
        FrmLinesP.PylonGuid = txtPylonGuid.Text
        FrmLinesP.ExtrusionWidth = CDbl(txtWidth.Text)
        FrmLinesP.ExtrusionProbability = CDbl(txtProbability.Text)
        FrmLinesP.SuppressPlatform = False
        If ckSuppress.Checked Then FrmLinesP.SuppressPlatform = True

        Dispose()

    End Sub

    Private Sub CmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Dispose()

    End Sub

End Class
