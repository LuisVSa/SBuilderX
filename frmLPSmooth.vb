Option Strict Off
Option Explicit On

Imports System.Windows.Forms

Friend Class FrmLPSmooth

    Private backupLine() As GLine
    Private backupPoly() As GPoly
    Private SquareSmoothDistance As Double

    Private Sub FrmLPSmooth_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim N, K As Integer

        chNoEnds.Checked = False
        If NoEndsSmooth Then chNoEnds.Checked = True

        chCorner.Checked = False
        If CornerSmooth Then chCorner.Checked = True

        txtDistance.Text = CStr(SmoothDistance)

        If POPType = "Line" Then
            If MakeOnMany = 1 Then
                ReDim backupLine(0)
                backupLine(0) = Lines(POPIndex)
            Else
                ReDim backupLine(MakeOnMany - 1)
                K = 0
                For N = 1 To NoOfLines
                    If Lines(N).Selected Then
                        backupLine(K) = Lines(N)
                        K = K + 1
                    End If
                Next
            End If
        End If

        If POPType = "Poly" Then
            If MakeOnMany = 1 Then
                ReDim backupPoly(0)
                backupPoly(0) = Polys(POPIndex)
            Else
                ReDim backupPoly(MakeOnMany - 1)
                K = 0
                For N = 1 To NoOfPolys
                    If Polys(N).Selected Then
                        backupPoly(K) = Polys(N)
                        K = K + 1
                    End If
                Next
            End If
        End If

        SquareSmoothDistance = SmoothDistance * SmoothDistance

    End Sub


    Private Sub CmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Dim N, K As Integer

        If POPType = "Line" Then
            If MakeOnMany = 1 Then
                Lines(POPIndex) = backupLine(0)
            Else
                K = 0
                For N = 1 To NoOfLines
                    If Lines(N).Selected Then
                        Lines(N) = backupLine(K)
                        K = K + 1
                    End If
                Next
            End If
        End If

        If POPType = "Poly" Then
            If MakeOnMany = 1 Then
                Polys(POPIndex) = backupPoly(0)
            Else
                K = 0
                For N = 1 To NoOfPolys
                    If Polys(N).Selected Then
                        Polys(N) = backupPoly(K)
                        K = K + 1
                    End If
                Next
            End If
        End If

        RebuildDisplay()
        Dispose()

    End Sub

    Private Sub CmdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        NoEndsSmooth = chNoEnds.Checked
        CornerSmooth = chCorner.Checked
        SmoothDistance = CDbl(txtDistance.Text)

        WriteIniValue(AppIni, "Main", "NoEndsSmooth", CStr(NoEndsSmooth))
        WriteIniValue(AppIni, "Main", "CornerSmooth", CStr(CornerSmooth))
        WriteIniValue(AppIni, "Main", "SmoothDistance", Str(SmoothDistance))

        'WriteSettings()
        Dispose()

    End Sub


    Private Sub CmdSmooth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSmooth.Click

        Dim N As Integer

        If POPType = "Line" Then
            If MakeOnMany = 1 Then
                SmoothMyLine(POPIndex)
            Else
                For N = 1 To NoOfLines
                    If Lines(N).Selected Then
                        SmoothMyLine(N)
                    End If
                Next
            End If
        End If

        If POPType = "Poly" Then
            If MakeOnMany = 1 Then
                SmoothMyPoly(POPIndex)
            Else
                For N = 1 To NoOfPolys
                    If Polys(N).Selected Then
                        SmoothMyPoly(N)
                    End If
                Next
            End If
        End If

        RebuildDisplay()

    End Sub

    Private Sub SmoothMyLine(ByVal L As Integer)

        Dim K, N, M, NP As Integer
        Dim myLine As GLine

        NP = Lines(L).NoOfPoints
        If NP < 3 Then Exit Sub

        myLine = Lines(L)
        myLine.NoOfPoints = 3 * NP
        ReDim myLine.GLPoints(3 * NP)

        myLine.GLPoints(1) = Lines(L).GLPoints(1)

        N = 2
        M = 2
        Do
            K = SmoothThisLine(myLine, N, M, L)
            M = M + K
            N = N + 1
        Loop While N < NP
        myLine.GLPoints(M) = Lines(L).GLPoints(N)

        myLine.NoOfPoints = M
        ReDim Preserve myLine.GLPoints(M)

        Lines(L) = myLine
        AddLatLonToLine(L)

    End Sub

    Private Sub SmoothMyPoly(ByVal L As Integer)

        Dim K, N, M, NP As Integer
        Dim myPoly As GPoly

        NP = Polys(L).NoOfPoints
        If NP < 3 Then Exit Sub

        myPoly = Polys(L)
        myPoly.NoOfPoints = 3 * NP
        ReDim myPoly.GPoints(3 * NP)

        'myPoly.GPoints(1) = Polys(L).GPoints(1)

        N = 1
        M = 1
        Do
            K = SmoothThisPoly(myPoly, N, M, L)
            M = M + K
            N = N + 1
        Loop While N <= NP

        'myPoly.GPoints(M) = Polys(L).GPoints(N)

        myPoly.NoOfPoints = M - 1
        ReDim Preserve myPoly.GPoints(M - 1)

        Polys(L) = myPoly
        AddLatLonToPoly(L)

    End Sub


    Private Sub TxtDistance_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDistance.TextChanged

        SmoothDistance = Val(txtDistance.Text)
        SquareSmoothDistance = SmoothDistance * SmoothDistance

    End Sub

    Private Sub ChCorner_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chCorner.CheckedChanged

        CornerSmooth = chCorner.Checked

    End Sub

    Private Sub ChNoEnds_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chNoEnds.CheckedChanged

        NoEndsSmooth = chNoEnds.Checked

    End Sub

    Private Function SmoothThisLine(ByRef AuxLine As GLine, ByVal N As Integer, ByVal M As Integer, ByVal Ln As Integer) As Integer

        ' N is the point in Ln
        ' M is the point in AuxLine
        ' returns 1 2 or 3

        Dim X1, X2, X, Y, Y2 As Double
        Dim XN, YN As Double
        Dim V1X, V1Y As Double
        Dim V2X, V2Y As Double
        Dim VX, VY As Double
        Dim Near1, Near2 As Boolean
        Dim XY As Double_XY

        On Error GoTo erro1

        ' at leat one point will be added
        SmoothThisLine = 1

        'X1 = GetDistance(N - 1, N - 1, Near1, Ln)
        'X2 = GetDistance(N, N, Near2, Ln)

        XY = Get2DistancesLine(N, Near1, Near2, Ln)
        If NoEndsSmooth Then
            If N = 2 Then Near1 = True
            If N = Lines(Ln).NoOfPoints - 1 Then Near2 = True
        End If

        X1 = XY.X
        X2 = XY.Y

        If Near1 And Near2 Then
            AuxLine.GLPoints(M) = Lines(Ln).GLPoints(N)
            Exit Function
        End If

        XN = Lines(Ln).GLPoints(N).lon
        YN = Lines(Ln).GLPoints(N).lat

        V1X = XN - Lines(Ln).GLPoints(N - 1).lon
        V1Y = YN - Lines(Ln).GLPoints(N - 1).lat
        V1X = V1X / X1
        V1Y = V1Y / X1

        V2X = Lines(Ln).GLPoints(N + 1).lon - XN
        V2Y = Lines(Ln).GLPoints(N + 1).lat - YN
        V2X = V2X / X2
        V2Y = V2Y / X2

        If Not Near1 Then
            VX = 3 * V1X + V2X
            VY = 3 * V1Y + V2Y
            X = VX * VX
            X = X + VY * VY
            X = System.Math.Sqrt(X) * 3 / X1
            VX = VX / X
            VY = VY / X
            AuxLine.GLPoints(M).lon = XN - VX
            AuxLine.GLPoints(M).lat = YN - VY
            AuxLine.GLPoints(M).alt = (2 * Lines(Ln).GLPoints(N).alt + Lines(Ln).GLPoints(N - 1).alt) / 3
            AuxLine.GLPoints(M).wid = (2 * Lines(Ln).GLPoints(N).wid + Lines(Ln).GLPoints(N - 1).wid) / 3
            M = M + 1
            SmoothThisLine = 2
        End If
        AuxLine.GLPoints(M) = Lines(Ln).GLPoints(N)

        If Not Near2 Then
            M = M + 1
            VX = 3 * V2X + V1X
            VY = 3 * V2Y + V1Y
            X = VX * VX
            X = X + VY * VY
            X = System.Math.Sqrt(X) * 3 / X2
            VX = VX / X
            VY = VY / X
            AuxLine.GLPoints(M).lon = XN + VX
            AuxLine.GLPoints(M).lat = YN + VY
            AuxLine.GLPoints(M).alt = (2 * Lines(Ln).GLPoints(N).alt + Lines(Ln).GLPoints(N + 1).alt) / 3
            AuxLine.GLPoints(M).wid = (2 * Lines(Ln).GLPoints(N).wid + Lines(Ln).GLPoints(N + 1).wid) / 3
            SmoothThisLine = SmoothThisLine + 1
        End If

        Exit Function
erro1:

    End Function

    Private Function Get2DistancesLine(ByVal N As Integer, ByRef Near1 As Boolean, ByRef Near2 As Boolean, ByVal Ln As Integer) As Double_XY

        Dim X, X1, Y1, Y As Double
        Dim MPD As Double

        Dim VX1, VY1 As Double
        Dim VX2, VY2 As Double

        ' N is the point to check and Ln is the line to check

        ' computes on .X the distance between N and N-1
        ' if distance is less then specified then returns Near1=true

        ' computes on .Y the distance between N and N+1
        ' if distance is less then specified then returns Near2=true

        Near1 = True
        Near2 = True

        On Error GoTo erro1

        MPD = MetersPerDegLon(LatDispCenter)

        VX1 = Lines(Ln).GLPoints(N).lon - Lines(Ln).GLPoints(N - 1).lon
        X = VX1 * MPD
        X1 = VX1 * VX1
        X = X * X
        VY1 = Lines(Ln).GLPoints(N).lat - Lines(Ln).GLPoints(N - 1).lat
        Y = VY1 * MetersPerDegLat
        Y1 = VY1 * VY1
        Y = Y * Y
        X = X + Y
        Y = X1 + Y1
        Y = System.Math.Sqrt(Y)
        Get2DistancesLine.X = Y
        VX1 = VX1 / Y
        VY1 = VY1 / Y
        If X > SquareSmoothDistance Then Near1 = False

        VX2 = Lines(Ln).GLPoints(N + 1).lon - Lines(Ln).GLPoints(N).lon
        X = VX2 * MPD

        X1 = VX2 * VX2
        X = X * X

        VY2 = Lines(Ln).GLPoints(N + 1).lat - Lines(Ln).GLPoints(N).lat
        Y = VY2 * MetersPerDegLat

        Y1 = VY2 * VY2
        Y = Y * Y
        X = X + Y
        Y = X1 + Y1
        Y = System.Math.Sqrt(Y)
        Get2DistancesLine.Y = Y
        VX2 = VX2 / Y
        VY2 = VY2 / Y

        If X > SquareSmoothDistance Then Near2 = False

        ' now get cos(v1 v2) in X

        X = VX1 * VX2 + VY1 * VY2

        If X > 0.995 Then
            Near1 = True
            Near2 = True
        End If

        If CornerSmooth Then
            If X < -0.25 Then
                Near1 = True
                Near2 = True
            End If
        End If

        Exit Function
erro1:

    End Function

    Private Function SmoothThisPoly(ByRef AuxPoly As GPoly, ByVal N As Integer, ByVal M As Integer, ByVal Pl As Integer) As Integer

        ' N is the point in Pl
        ' M is the point in AuxPoly
        ' returns 1 2 or 3

        Dim X1, X2, X, Y, Y2 As Double
        Dim XN, YN As Double
        Dim V1X, V1Y As Double
        Dim V2X, V2Y As Double
        Dim VX, VY As Double
        Dim Near1, Near2 As Boolean
        Dim XY As Double_XY
        Dim NP As Integer

        On Error GoTo erro1
        SmoothThisPoly = 1

        XY = Get2DistancesPoly(N, Near1, Near2, Pl)
        X1 = XY.X
        X2 = XY.Y

        If Near1 And Near2 Then
            AuxPoly.GPoints(M) = Polys(Pl).GPoints(N)
            Exit Function
        End If

        XN = Polys(Pl).GPoints(N).lon
        YN = Polys(Pl).GPoints(N).lat
        NP = Polys(Pl).NoOfPoints

        If N = 1 Then
            V1X = XN - Polys(Pl).GPoints(NP).lon
            V1Y = YN - Polys(Pl).GPoints(NP).lat
        Else
            V1X = XN - Polys(Pl).GPoints(N - 1).lon
            V1Y = YN - Polys(Pl).GPoints(N - 1).lat
        End If

        V1X = V1X / X1
        V1Y = V1Y / X1

        If N = NP Then
            V2X = Polys(Pl).GPoints(1).lon - XN
            V2Y = Polys(Pl).GPoints(1).lat - YN
        Else
            V2X = Polys(Pl).GPoints(N + 1).lon - XN
            V2Y = Polys(Pl).GPoints(N + 1).lat - YN
        End If

        V2X = V2X / X2
        V2Y = V2Y / X2

        If Not Near1 Then
            VX = 3 * V1X + V2X
            VY = 3 * V1Y + V2Y
            X = VX * VX
            X = X + VY * VY
            X = System.Math.Sqrt(X) * 3 / X1
            VX = VX / X
            VY = VY / X
            AuxPoly.GPoints(M).lon = XN - VX
            AuxPoly.GPoints(M).lat = YN - VY
            M = M + 1
            SmoothThisPoly = 2
        End If

        AuxPoly.GPoints(M) = Polys(Pl).GPoints(N)

        If Not Near2 Then
            M = M + 1
            VX = 3 * V2X + V1X
            VY = 3 * V2Y + V1Y
            X = VX * VX
            X = X + VY * VY
            X = System.Math.Sqrt(X) * 3 / X2
            VX = VX / X
            VY = VY / X
            AuxPoly.GPoints(M).lon = XN + VX
            AuxPoly.GPoints(M).lat = YN + VY
            SmoothThisPoly = SmoothThisPoly + 1
        End If

        Exit Function

erro1:
    End Function

    Private Function Get2DistancesPoly(ByVal N As Integer, ByRef Near1 As Boolean, ByRef Near2 As Boolean, ByVal Pl As Integer) As Double_XY

        Dim X, X1, Y1, Y As Double
        Dim MPD As Double

        Dim VX1, VY1 As Double
        Dim VX2, VY2 As Double

        Dim K, M As Integer

        ' N is the point to check and Pl is the poly to check

        ' computes on .X the distance between N and N-1
        ' if distance is less then specified then returns Near1=true

        ' computes on .Y the distance between N and N+1
        ' if distance is less then specified then returns Near2=true

        Near1 = True
        Near2 = True

        M = N - 1
        K = N + 1

        If N = Polys(Pl).NoOfPoints Then K = 1
        If N = 1 Then M = Polys(Pl).NoOfPoints

        On Error GoTo erro1

        MPD = MetersPerDegLon(LatDispCenter)

        VX1 = Polys(Pl).GPoints(N).lon - Polys(Pl).GPoints(M).lon
        X = VX1 * MPD
        X1 = VX1 * VX1
        X = X * X
        VY1 = Polys(Pl).GPoints(N).lat - Polys(Pl).GPoints(M).lat
        Y = VY1 * MetersPerDegLat
        Y1 = VY1 * VY1
        Y = Y * Y
        X = X + Y
        Y = X1 + Y1
        Y = System.Math.Sqrt(Y)
        Get2DistancesPoly.X = Y
        VX1 = VX1 / Y
        VY1 = VY1 / Y
        If X > SquareSmoothDistance Then Near1 = False

        VX2 = Polys(Pl).GPoints(K).lon - Polys(Pl).GPoints(N).lon
        X = VX2 * MPD

        X1 = VX2 * VX2
        X = X * X

        VY2 = Polys(Pl).GPoints(K).lat - Polys(Pl).GPoints(N).lat
        Y = VY2 * MetersPerDegLat

        Y1 = VY2 * VY2
        Y = Y * Y
        X = X + Y
        Y = X1 + Y1
        Y = System.Math.Sqrt(Y)
        Get2DistancesPoly.Y = Y
        VX2 = VX2 / Y
        VY2 = VY2 / Y

        If X > SquareSmoothDistance Then Near2 = False

        ' now get cos(v1 v2) in X

        X = VX1 * VX2 + VY1 * VY2

        If X > 0.995 Then
            Near1 = True
            Near2 = True
        End If

        If CornerSmooth Then
            If X < -0.25 Then
                Near1 = True
                Near2 = True
            End If
        End If

        Exit Function
erro1:

    End Function



End Class
