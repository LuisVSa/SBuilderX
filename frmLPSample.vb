Imports System.Windows.Forms

Friend Class FrmLPSample

    Private backupLine() As GLine
    Private backupPoly() As GPoly
    Private SquareSampleDistance As Double

    Private Sub FrmLPSample_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim N, K As Integer

        txtDistance.Text = CStr(SampleDistance)

        If POPType = "Line" Then
            If MakeOnMany = 1 Then
                ReDim backupLine(0)
                StoreLine(POPIndex, K)
            Else
                ReDim backupLine(MakeOnMany - 1)
                K = 0
                For N = 1 To NoOfLines
                    If Lines(N).Selected Then
                        StoreLine(N, K)
                        K = K + 1
                    End If
                Next
            End If
        End If

        If POPType = "Poly" Then
            If MakeOnMany = 1 Then
                ReDim backupPoly(0)
                StorePoly(POPIndex, 0)
            Else
                ReDim backupPoly(MakeOnMany - 1)
                K = 0
                For N = 1 To NoOfPolys
                    If Polys(N).Selected Then
                        StorePoly(N, K)
                        K = K + 1
                    End If
                Next
            End If
        End If

        SquareSampleDistance = SampleDistance * SampleDistance

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

    Private Sub CmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        SampleDistance = CDbl(txtDistance.Text)
        WriteIniValue(AppIni, "Main", "SampleDistance", Str(SampleDistance))
        Dispose()

    End Sub

    Private Sub TxtDistance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDistance.Click

        SampleDistance = CDbl(txtDistance.Text)
        SquareSampleDistance = SampleDistance * SampleDistance

    End Sub

    Private Sub CmdSample_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSample.Click

        Dim N As Integer

        If POPType = "Line" Then
            If MakeOnMany = 1 Then
                SampleMyLine(POPIndex)
            Else
                For N = 1 To NoOfLines
                    If Lines(N).Selected Then
                        SampleMyLine(N)
                    End If
                Next
            End If
        End If

        If POPType = "Poly" Then
            If MakeOnMany = 1 Then
                SampleMyPoly(POPIndex)
            Else
                For N = 1 To NoOfPolys
                    If Polys(N).Selected Then
                        SampleMyPoly(N)
                    End If
                Next
            End If
        End If

        RebuildDisplay()

    End Sub
    Private Sub SampleMyLine(ByVal Ln As Integer)

        Dim K, N, M As Integer
        Dim Near As Boolean

        Dim auxLine As GLine

        If Lines(Ln).NoOfPoints = 2 Then Exit Sub

        auxLine = Lines(Ln)

        K = 1
        N = 2
        M = 1
        Do
            GetDistanceLine(N, M, Near, Ln)
            If Near Then
                N = N + 1
            Else
                K = K + 1
                auxLine.GLPoints(K) = Lines(Ln).GLPoints(N)
                M = N
                N = N + 1
            End If
        Loop While N < Lines(Ln).NoOfPoints

        K = K + 1
        auxLine.GLPoints(K) = Lines(Ln).GLPoints(N)

        auxLine.NoOfPoints = K
        ReDim Preserve auxLine.GLPoints(K)

        Lines(Ln) = auxLine

    End Sub

    Private Function GetDistanceLine(ByVal N As Integer, ByVal M As Integer, ByRef Near As Boolean, ByVal Ln As Integer) As Double

        Dim X, X1, Y1, Y As Double

        ' N is the point to check
        ' based on the the distance between N+1 (the next) and the previous M (N-1 could have been deleted!)
        ' returns the distance in degrees
        ' if distance is less then specified then returns Near=true

        X1 = Lines(Ln).GLPoints(N + 1).lon - Lines(Ln).GLPoints(M).lon
        X = X1 * MetersPerDegLon(LatDispCenter)

        X1 = X1 * X1
        X = X * X

        Y1 = Lines(Ln).GLPoints(N + 1).lat - Lines(Ln).GLPoints(M).lat
        Y = Y1 * MetersPerDegLat

        Y1 = Y1 * Y1
        Y = Y * Y
        X = X + Y
        Y = X1 + Y1
        GetDistanceLine = System.Math.Sqrt(Y)
        Near = True
        If X > SquareSampleDistance Then Near = False

    End Function

    Private Sub SampleMyPoly(ByVal Pl As Integer)

        Dim K, N, M As Integer
        Dim Near As Boolean

        Dim auxPoly As GPoly

        auxPoly = Polys(Pl)

        K = 1
        N = 2
        M = 1
        Do
            GetDistancePoly(N, M, Near, Pl)
            If Near Then
                N = N + 1
            Else
                K = K + 1
                auxPoly.GPoints(K) = Polys(Pl).GPoints(N)
                M = N
                N = N + 1
            End If
        Loop While N < Polys(Pl).NoOfPoints

        K = K + 1
        auxPoly.GPoints(K) = Polys(Pl).GPoints(N)

        auxPoly.NoOfPoints = K
        ReDim Preserve auxPoly.GPoints(K)

        If K < 3 Then
            MsgBox("Poly # " & Str(Pl) & " has 2 points!", MsgBoxStyle.Critical)
            'Exit Sub
        End If

        Polys(Pl) = auxPoly

    End Sub

    Private Function GetDistancePoly(ByVal N As Integer, ByVal M As Integer, ByRef Near As Boolean, ByVal Pl As Integer) As Double

        Dim X, X1, Y1, y As Double

        ' N is the point to check
        ' based on the the distance between N+1 (the next) and the previous M (N-1 could have been deleted!)
        ' returns the distance in degrees
        ' if distance is less then specified then returns Near=true

        If N = Polys(Pl).NoOfPoints Then N = 0
        If M = 0 Then M = Polys(Pl).NoOfPoints

        X1 = Polys(Pl).GPoints(N + 1).lon - Polys(Pl).GPoints(M).lon
        Y1 = Polys(Pl).GPoints(N + 1).lat - Polys(Pl).GPoints(M).lat

        X = X1 * MetersPerDegLon(LatDispCenter)
        X1 = X1 * X1
        X = X * X
        y = Y1 * MetersPerDegLat
        Y1 = Y1 * Y1
        y = y * y
        X = X + y
        y = X1 + Y1
        GetDistancePoly = System.Math.Sqrt(y)
        Near = True
        If X > SquareSampleDistance Then Near = False

    End Function


    Private Sub StorePoly(ByVal N As Integer, ByVal K As Integer)

        Dim J, L As Integer

        backupPoly(K) = Polys(N)
        J = Polys(N).NoOfPoints
        ReDim backupPoly(K).GPoints(J)
        backupPoly(K).NoOfPoints = J
        For L = 1 To J
            backupPoly(K).GPoints(L) = Polys(N).GPoints(L)
        Next

    End Sub

    Private Sub StoreLine(ByVal N As Integer, ByVal K As Integer)

        Dim J, L As Integer

        backupLine(K) = Lines(N)
        J = Lines(N).NoOfPoints
        ReDim backupLine(K).GLPoints(J)
        backupLine(K).NoOfPoints = J
        For L = 1 To J
            backupLine(K).GLPoints(L) = Lines(N).GLPoints(L)
        Next

    End Sub

End Class
