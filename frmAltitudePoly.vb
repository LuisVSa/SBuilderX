Option Strict Off
Option Explicit On

Imports System.Windows.Forms



Friend Class FrmAltitudePoly

    Private Sub CmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Dispose()

    End Sub

    Private Sub FrmLPAltitude_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        LoadForm()

    End Sub

    Private Sub LoadForm()

        Dim N As Integer
        Dim X, Y As Double
        Dim Flag As Boolean = False
        Dim n0, n1, n2 As Integer
        Dim k1, k2, k3 As Double
        Dim lat As Double

        Dim sxy, head As Double

        X = 0
        Y = Polys(POPIndex).GPoints(1).alt

        For N = 1 To Polys(POPIndex).NoOfPoints
            X = X + Polys(POPIndex).GPoints(N).alt
            If Y <> Polys(POPIndex).GPoints(N).alt Then Flag = True
        Next

        txtAlt.Text = X / Polys(POPIndex).NoOfPoints

        If Flag = False Then
            txtHead.Text = 0
            txtSlope.Text = 0
            txtAlt0.Text = Polys(POPIndex).GPoints(1).alt
            txtPt0.Text = 1
            Exit Sub
        End If

        Get3Points(POPIndex, n0, n1, n2, lat)
        GetSlopes(POPIndex, n0, n1, n2, k1, k2, k3)
        GetMaximumSlope(k1, k2, lat, sxy, head)

        txtHead.Text = head
        txtSlope.Text = sxy * 1000
        txtAlt0.Text = Polys(POPIndex).GPoints(n1).alt
        txtPt0.Text = n1

        lbSX.Text = "SlopeX = " & Mid(Str(k1), 1, 13)
        lbSY.Text = "SlopeY = " & Mid(Str(k2), 1, 13)

    End Sub

    Private Sub CmdSlope_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSlope.Click

        Dim N As Integer

        If POPMode = "One" Then
            SetPolyAltitudes(POPIndex)
        Else
            For N = 1 To NoOfPolys
                If Polys(N).Selected And Polys(N).NoOfChilds >= 0 Then
                    SetPolyAltitudes(N)
                End If
            Next
        End If

        LoadForm()

    End Sub

    Private Sub SetPolyAltitudes(ByVal N As Integer)

        Dim Head, lat, lon, sxy As Double
        Dim k1, k2, k3 As Double

        Dim x0, y0, z0 As Double
        Dim P As Integer
        Dim K, J As Integer

        Head = CDbl(txtHead.Text)
        sxy = CDbl(txtSlope.Text)

        lat = 0
        For K = 1 To Polys(N).NoOfPoints
            lat = lat + Polys(N).GPoints(K).lat
        Next
        lat = lat / Polys(N).NoOfPoints

        Head = Head * PI / 180.0#
        sxy = sxy / 1000.0#

        k1 = sxy * System.Math.Sin(Head)
        k2 = sxy * System.Math.Cos(Head)

        k1 = k1 * MetersPerDegLon(lat)
        k2 = k2 * MetersPerDegLat

        P = CInt(txtPt0.Text)

        x0 = Polys(N).GPoints(P).lon
        y0 = Polys(N).GPoints(P).lat
        z0 = CDbl(txtAlt0.Text)

        k3 = z0 - k1 * x0 - k2 * y0

        For K = 1 To Polys(N).NoOfPoints
            lat = Polys(N).GPoints(K).lat
            lon = Polys(N).GPoints(K).lon
            Polys(N).GPoints(K).alt = k1 * lon + k2 * lat + k3
        Next

        For P = 1 To Polys(N).NoOfChilds
            J = Polys(N).Childs(P)
            For K = 1 To Polys(J).NoOfPoints
                lat = Polys(J).GPoints(K).lat
                lon = Polys(J).GPoints(K).lon
                Polys(J).GPoints(K).alt = k1 * lon + k2 * lat + k3
            Next
        Next


    End Sub

    Private Sub CmdAlt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAlt.Click

        Dim N As Integer
        Dim X As Double

        On Error GoTo erro1

        X = CDbl(txtAlt.Text)

        If POPMode = "One" Then
            SetConstantAltitude(POPIndex, X)
        Else
            For N = 1 To NoOfPolys
                If Polys(N).Selected And Polys(N).NoOfChilds >= 0 Then
                    SetConstantAltitude(N, X)
                End If
            Next
        End If

        LoadForm()
        Exit Sub

erro1:
        MsgBox("Check altitude value!", MsgBoxStyle.Critical)

    End Sub

    Private Sub SetConstantAltitude(ByVal N As Integer, ByVal H As Double)

        Dim K, P, J As Integer

        For K = 1 To Polys(N).NoOfPoints
            Polys(N).GPoints(K).alt = H
        Next

        For P = 1 To Polys(N).NoOfChilds
            J = Polys(N).Childs(P)
            For K = 1 To Polys(J).NoOfPoints
                Polys(J).GPoints(K).alt = H
            Next
        Next

    End Sub


    Private Sub CmdHelpSlope_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHelpSlope.Click

        Dim A As String

        A = "In this mode you specify: (i) the altitude and index of one" & vbCrLf
        A = A & "point (ii) the heading of maximum slope and (iii) the maximum" & vbCrLf
        A = A & "slope expressed as the altitude increase in meters per one" & vbCrLf
        A = A & "thousand meters of horizontal shift."

        MsgBox(A, MsgBoxStyle.Information)

    End Sub

    Private Sub Get3Points(ByVal N As Integer, ByRef N1 As Integer, ByRef N2 As Integer, ByRef N3 As Integer, ByRef lat As Double)

        Dim NP, J, K As Integer
        Dim X1, X2, DX, DY, D As Double


        NP = Polys(N).NoOfPoints
        N1 = 1
        N2 = 1
        X1 = Polys(N).GPoints(1).lon
        X2 = Polys(N).GPoints(1).lon

        lat = 0

        For J = 1 To NP
            lat = lat + Polys(N).GPoints(J).lat
            If Polys(N).GPoints(J).lon < X1 Then
                N1 = J
                X1 = Polys(N).GPoints(J).lon
            End If
            If Polys(N).GPoints(J).lon > X2 Then
                N2 = J
                X2 = Polys(N).GPoints(J).lon
            End If
        Next

        lat = lat / NP

        X1 = (Polys(N).GPoints(N1).lon - Polys(N).GPoints(N2).lon)
        X1 = X1 * X1
        X2 = (Polys(N).GPoints(N1).lat - Polys(N).GPoints(N2).lat)
        X2 = X2 * X2
        D = System.Math.Sqrt(X1 + X2)
        For K = 1 To NP
            If K <> N1 And K <> N2 Then
                X1 = (Polys(N).GPoints(N1).lon - Polys(N).GPoints(K).lon)
                X1 = X1 * X1
                X2 = (Polys(N).GPoints(N1).lat - Polys(N).GPoints(K).lat)
                X2 = X2 * X2
                DX = System.Math.Sqrt(X1 + X2)
                X1 = (Polys(N).GPoints(N2).lon - Polys(N).GPoints(K).lon)
                X1 = X1 * X1
                X2 = (Polys(N).GPoints(N2).lat - Polys(N).GPoints(K).lat)
                X2 = X2 * X2
                DY = System.Math.Sqrt(X1 + X2)
                DX = DX + DY
                If DX > D Then
                    D = DX
                    N3 = K
                End If
            End If
        Next

    End Sub

    Private Sub GetSlopes(ByVal N As Integer, ByVal N0 As Integer, ByVal N1 As Integer, ByVal N2 As Integer, ByRef K1 As Double, ByRef K2 As Double, ByRef K3 As Double)

        Dim z00, z01, z02 As Double
        Dim x00, x01, x02 As Double
        Dim y00, y01, y02 As Double
        Dim a1, a2 As Double

        x01 = Polys(N).GPoints(N0).lon - Polys(N).GPoints(N1).lon
        x02 = Polys(N).GPoints(N0).lon - Polys(N).GPoints(N2).lon
        y01 = Polys(N).GPoints(N0).lat - Polys(N).GPoints(N1).lat
        y02 = Polys(N).GPoints(N0).lat - Polys(N).GPoints(N2).lat
        z01 = Polys(N).GPoints(N0).alt - Polys(N).GPoints(N1).alt
        z02 = Polys(N).GPoints(N0).alt - Polys(N).GPoints(N2).alt

        If y01 = 0 Then
            If x01 = 0 Then
                K1 = 0
            Else
                K1 = z01 / x01
            End If
        Else
            a1 = y02 / y01
            z00 = z01 * a1 - z02
            x00 = x01 * a1 - x02
            K1 = z00 / x00
        End If


        If x01 = 0 Then
            If y01 = 0 Then
                K2 = 0
            Else
                K2 = z01 / y01
            End If
        Else
            a2 = x02 / x01
            z00 = z01 * a2 - z02
            y00 = y01 * a2 - y02
            K2 = z00 / y00
        End If

        K3 = Polys(N).GPoints(N0).alt - K1 * Polys(N).GPoints(N0).lon - K2 * Polys(N).GPoints(N0).lat

    End Sub

    Private Sub GetMaximumSlope(ByVal k1 As Double, ByVal k2 As Double, ByVal lat As Double, ByRef sxy As Double, ByRef head As Double)

        Dim sx, sy As Double

        sxy = 0
        head = 0

        If k1 = 0 And k2 = 0 Then Exit Sub

        sx = k1 / MetersPerDegLon(lat)
        sy = k2 / MetersPerDegLat

        sxy = System.Math.Sqrt(sx * sx + sy * sy)

        head = 0

        If sx = 0 Then
            If sy > 0 Then
                head = 0
                Exit Sub
            Else
                head = 180
                Exit Sub
            End If
        End If

        If sy = 0 Then
            If sx > 0 Then
                head = 90
                Exit Sub
            Else
                head = 270
                Exit Sub
            End If
        End If

        If sx > 0 Then
            head = 90 - System.Math.Atan(sy / sx) * 180 / PI
            Exit Sub
        Else
            head = 270 - System.Math.Atan(sy / sx) * 180 / PI
            Exit Sub
        End If

    End Sub



End Class
