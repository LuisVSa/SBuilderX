
Option Strict On
Option Explicit On
Module moduleTRIANGLES

    Friend Structure Pts2Tri
        Dim X As Double
        Dim Y As Double
        Dim T As Double ' turn =positive if clockwise
        Dim N As Integer
    End Structure

    Friend Pts2Tris() As Pts2Tri
    Friend NoOfPts2Tris As Integer

    ' this is output for triangles
    Friend Structure Tri
        Dim N1 As Integer
        Dim N2 As Integer
        Dim N3 As Integer
    End Structure

    Friend Tris() As Tri
    Friend NoOfTris As Integer

    Friend Sub MakeTris()

        Dim N1 As Integer

        NoOfTris = 0
        SetTriConcaves()

        Do
            FindTri(N1)
            RemoveTri(N1)
        Loop While NoOfPts2Tris > 3

        NoOfTris = NoOfTris + 1
        ReDim Preserve Tris(NoOfTris)
        Tris(NoOfTris).N1 = Pts2Tris(1).N
        Tris(NoOfTris).N2 = Pts2Tris(2).N
        Tris(NoOfTris).N3 = Pts2Tris(3).N

    End Sub

    Private Sub FindTri(ByRef N1 As Integer)

        Dim N As Integer

        For N = 2 To NoOfPts2Tris
            If Not IsTri(N) Then GoTo next_N
            ' ok on some
            N1 = N
            Exit Sub
next_N:
        Next N

        MsgBox("Error on the triangulation!")

    End Sub

    Private Sub RemoveTri(ByVal N1 As Integer)


        Dim K As Integer

        NoOfTris = NoOfTris + 1
        ReDim Preserve Tris(NoOfTris)
        Tris(NoOfTris).N1 = Pts2Tris(N1 - 1).N
        Tris(NoOfTris).N2 = Pts2Tris(N1).N
        Tris(NoOfTris).N3 = Pts2Tris(N1 + 1).N

        ' fazer o novo Pts2Tri
        NoOfPts2Tris = NoOfPts2Tris - 1
        For K = N1 To NoOfPts2Tris
            Pts2Tris(K) = Pts2Tris(K + 1)
        Next K

        SetTriConcaves()

        Pts2Tris(0) = Pts2Tris(NoOfPts2Tris)
        Pts2Tris(NoOfPts2Tris + 1) = Pts2Tris(1)

    End Sub

    Private Function IsTri(ByVal N1 As Integer) As Boolean

        Dim K As Integer

        IsTri = False
        If Pts2Tris(N1).T < 0 Then Exit Function
        For K = N1 + 2 To NoOfPts2Tris
            'If Pts2Tris(K).T < 0 Then GoTo next_1:
            If IsPtInTri(N1, K) Then Exit Function
next_1:
        Next K

        For K = 1 To N1 - 2
            'If Pts2Tri(K).T < 0 Then GoTo next_2:
            If IsPtInTri(N1, K) Then Exit Function
next_2:
        Next K

        IsTri = True

    End Function

    Private Sub SetTriConcaves()

        Dim N As Integer

        For N = 1 To NoOfPts2Tris
            Pts2Tris(N).T = GetTriTurn(N)
        Next N

    End Sub

    Private Function GetTriTurn(ByVal N As Integer) As Double

        Dim X1, X0, Y0, Y1 As Double

        X0 = CDbl(Pts2Tris(N).X - Pts2Tris(N - 1).X)
        Y0 = CDbl(Pts2Tris(N).Y - Pts2Tris(N - 1).Y)
        X1 = CDbl(Pts2Tris(N + 1).X - Pts2Tris(N).X)
        Y1 = CDbl(Pts2Tris(N + 1).Y - Pts2Tris(N).Y)

        GetTriTurn = Y0 * X1 - X0 * Y1 ' this is when GetTurn returns a double

    End Function

    Private Function IsPtInTri(ByVal N1 As Integer, ByVal K As Integer) As Boolean

        Dim P(4) As Double_XY
        Dim C As Integer

        Dim X1, Y1 As Double
        Dim X0, Y0 As Double
        Dim X, Y As Double

        Dim CP As Double

        P(1).X = Pts2Tris(N1 - 1).X
        P(1).Y = Pts2Tris(N1 - 1).Y

        P(2).X = Pts2Tris(N1).X
        P(2).Y = Pts2Tris(N1).Y

        P(3).X = Pts2Tris(N1 + 1).X
        P(3).Y = Pts2Tris(N1 + 1).Y

        P(4).X = Pts2Tris(N1 - 1).X
        P(4).Y = Pts2Tris(N1 - 1).Y

        X = Pts2Tris(K).X
        Y = Pts2Tris(K).Y

        IsPtInTri = False

        For C = 2 To 4
            Y1 = P(C).Y
            Y0 = P(C - 1).Y
            If Y1 = Y0 Then GoTo next_C
            If Y <= Y1 And Y <= Y0 Then GoTo next_C
            If Y > Y1 And Y > Y0 Then GoTo next_C
            X1 = P(C).X
            X0 = P(C - 1).X
            If X < X1 And X < X0 Then
                IsPtInTri = Not IsPtInTri
                GoTo next_C
            End If
            If X > X1 And X > X0 Then GoTo next_C
            CP = (X1 - X0) * (Y - Y0) / (Y1 - Y0) + X0
            If X < CP Then IsPtInTri = Not IsPtInTri

next_C:
        Next C

    End Function

End Module
