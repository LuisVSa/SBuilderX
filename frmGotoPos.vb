Option Strict Off
Option Explicit On

Imports System.Windows.Forms

Friend Class FrmGotoPos

    Friend CheckGeo As Boolean
    Friend X0, Y0 As Double
    Friend L0, U0, V0 As Integer

    Private Sub FrmGotoPos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Text = "SBuilderX - Goto this position"

        X0 = LonDispCenter
        Y0 = LatDispCenter
        L0 = Zoom

        FillValues(X0, Y0, L0, U0, V0)

    End Sub

    Private Sub FillValues(ByVal X As Double, ByVal Y As Double, ByVal L As Integer, ByVal U As Integer, ByVal V As Integer)

        X0 = X
        Y0 = Y
        L0 = L
        U0 = U
        V0 = V

        txtLon.Text = Lon2Str(X)
        txtLat.Text = Lat2Str(Y)
        txtU.Text = U
        txtV.Text = V
        txtL.Text = L

    End Sub

    Private Sub ValidateGeo()

        Dim X, Y As Double
        Dim Good As Boolean = True
        Dim U, V, L As Integer

        Dim PU, PV As Integer

        On Error GoTo erro1

        L = Fix(txtL.Text)
        If L < 0 Then Good = False
        If L > 25 Then Good = False

        Y = Str2Lat(txtLat.Text)
        If Y > 90 Then Good = False
        If Y < -90 Then Good = False

        X = Str2Lon(txtLon.Text)
        If X > 180 Then Good = False
        If X < -180 Then Good = False

        If Good Then
            PU = 3 * Math.Pow(2, L)
            PV = 2 * Math.Pow(2, L)
            U = CInt(Int(PU * (X + 180) / 360))
            V = CInt(Int(PV * (90 - Y) / 180))
            FillValues(X, Y, L, U, V)
        Else
            FillValues(X0, Y0, L0, U0, V0)
            MsgBox("Check your entry!", MsgBoxStyle.Critical)
        End If

        Exit Sub

erro1:
        FillValues(X0, Y0, L0, U0, V0)
        MsgBox("Check your entry!", MsgBoxStyle.Critical)

    End Sub

    Private Sub ValidateUV()

        Dim X, Y As Double
        Dim Good As Boolean = True
        Dim PU, PV As Integer
        Dim L, U, V As Integer

        On Error GoTo erro1

        L = Fix(txtL.Text)
        U = Fix(txtU.Text)
        V = Fix(txtV.Text)

        If L < 0 Then Good = False
        If L > 27 Then Good = False

        PU = 3 * Math.Pow(2, L)
        PV = 2 * Math.Pow(2, L)

        If U > PU - 1 Then Good = False
        If U < 0 Then Good = False
        If V > PV - 1 Then Good = False
        If V < 0 Then Good = False

        If Good Then
            X = (360 * (U + 0.5) / PU) - 180
            Y = 90 - 180 * (V + 0.5) / PV
            FillValues(X, Y, L, U, V)
        Else
            FillValues(X0, Y0, L0, U0, V0)
            MsgBox("Check your entry!", MsgBoxStyle.Critical)
        End If

        Exit Sub

erro1:
        FillValues(X0, Y0, L0, U0, V0)
        MsgBox("Check your entry!", MsgBoxStyle.Critical)

    End Sub

    Private Sub CmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click

        Dispose()

    End Sub

    Private Sub CmdCheck_Click(sender As Object, e As EventArgs) Handles cmdCheck.Click

        If CheckGeo Then
            ValidateGeo()
        Else
            ValidateUV()
        End If
    End Sub

    Private Sub CmdOK_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click

        LonDispCenter = Str2Lon(txtLon.Text)
        LatDispCenter = Str2Lat(txtLat.Text)
        SetDispCenter(0, 0)
        RebuildDisplay()
        Dispose()

    End Sub

    Private Sub TxtLat_LostFocus(sender As Object, e As EventArgs) Handles txtLat.LostFocus
        CheckGeo = True
    End Sub

    Private Sub TxtLon_LostFocus(sender As Object, e As EventArgs) Handles txtLon.LostFocus
        CheckGeo = True
    End Sub

    Private Sub TxtU_LostFocus(sender As Object, e As EventArgs) Handles txtU.LostFocus
        CheckGeo = False
    End Sub

    Private Sub TxtV_LostFocus(sender As Object, e As EventArgs) Handles txtV.LostFocus
        CheckGeo = False
    End Sub
    Private Sub TxtL_LostFocus(sender As Object, e As EventArgs) Handles txtL.LostFocus
        CheckGeo = False
    End Sub


End Class
