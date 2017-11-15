
Public Class ClipPoly

    Private Structure PathPt
        Dim X As Double
        Dim Y As Double
        Dim T As Integer   ' =0 is start  =1 is normal  =2 is end 
        Dim IO As Integer  ' =0 is out    =1 is in      =2 is coming  =3 is leaving
    End Structure
    Private Path() As PathPt
    Private NoOfPathPts As Integer = 0

    Private Structure QuadPt
        Implements IComparable
        Dim X As Double
        Dim Y As Double
        Dim I As Integer   ' points to Path
        Dim D As Double    ' distance from NW corner - used to sort
        Dim T As Integer   ' =1 the corners!  =2  enters the quad   =3 leaves the quad =0 already used
        Private Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Dim tmp As QuadPt = DirectCast(obj, QuadPt)
            Return D.CompareTo(obj.D)
        End Function
    End Structure

    Private QPts() As QuadPt
    Private NoOfQPts As Integer = 0

    'Private Structure Slice  ' in main module
    '    Dim N As Long
    '    Dim NC As Integer
    '    Dim C() As Integer
    '    Dim P() As XYDouble
    'End Structure

    'Public Slices() As Slice
    'Public NoOfSlices As Integer

    Private NoOfChilds As Integer = 0
    Private PolyIndex As Integer

    Friend Sub SetPoly(ByVal N As Integer)

        Dim I, J, K, M As Integer

        NoOfChilds = Polys(N).NoOfChilds
        'Childs = Polys(N).Childs
        PolyIndex = N

        Dim NP As Integer = Polys(N).NoOfPoints
        For J = 1 To NoOfChilds
            M = Polys(N).Childs(J)
            NP = NP + Polys(M).NoOfPoints + 1
        Next J

        NoOfPathPts = NP
        ReDim Path(NP)
        For J = 1 To Polys(N).NoOfPoints
            Path(J).X = Polys(N).GPoints(J).lon
            Path(J).Y = Polys(N).GPoints(J).lat
            Path(J).T = 1
        Next
        Path(J - 1).T = 2
        J = J - 1
        Path(0).X = Path(J).X
        Path(0).Y = Path(J).Y
        Path(0).T = 0

        If NoOfChilds > 0 Then
            For I = 1 To NoOfChilds
                M = Polys(N).Childs(I)
                For K = 1 To Polys(M).NoOfPoints
                    J = J + 1
                    Path(J).X = Polys(M).GPoints(K).lon
                    Path(J).Y = Polys(M).GPoints(K).lat
                    Path(J).T = 1
                    If K = 1 Then Path(J).T = 0
                Next K
                J = J + 1
                Path(J).X = Polys(M).GPoints(1).lon
                Path(J).Y = Polys(M).GPoints(1).lat
                Path(J).T = 2
            Next I
        End If

    End Sub

    Private QMID As Integer
    Private NLat, SLat, WLon, ELon As Double  ' borders
    Private CLat, CLon, DLat, DLon As Double  ' center and delta
    Private AllInside As Boolean
    Private NoneInside As Boolean
    Private NoOfInside As Integer

    Friend Sub SetQuad(ByVal Q As Integer, ByVal lat As Double, ByVal lon As Double)

        ' set QMID, NLat, SLat, WLon, ELon, DLat, DLon giving the QMID
        ' and a lat/lon point inside the quad

        QMID = Q
        Q = CInt(2 ^ (Q - 2))
        DLat = CDbl(90 / Q)  ' delta lat: the height of the quad
        DLon = CDbl(120 / Q)  ' delta lon: the width of the quad

        WLon = Int((lon + 180) / DLon) * DLon - 180
        ELon = WLon + DLon
        NLat = 90 - Int((90 - lat) / DLat) * DLat
        SLat = NLat - DLat
        CLat = (NLat + SLat) / 2
        CLon = (WLon + ELon) / 2

        ' check how many times the path enters this quad
        ' and set a number of variables

        NoOfInside = 0
        NoneInside = True
        AllInside = True

        Dim PresentIsIn, PresentIsOut As Boolean
        Dim PreviousIsIn, PreviousIsOut As Boolean

        Dim N, NS As Integer  ' NS start of a subpath
        For N = 0 To NoOfPathPts

            PresentIsIn = IsPtInQuad(N)
            PresentIsOut = Not PresentIsIn

            If Path(N).T = 0 Then               ' point is the start of a sub poly (hole)

                NS = N                          ' sign the index in NS
                PreviousIsIn = PresentIsIn      ' and set previous in/out as all the following
                PreviousIsOut = PresentIsOut    ' elseifs are skipped

            ElseIf Path(N).T > 0 Then           ' normal or ending point of subpath

                If PresentIsIn Then
                    NoneInside = False
                    NoOfInside = NoOfInside + 1  ' this can be revised later as it can be: either a "false" belong
                    Path(N).IO = 1               ' or an entry or leaving point
                Else
                    AllInside = False
                    Path(N).IO = 0
                End If

                If PreviousIsOut And PresentIsIn Then
                    Path(N).IO = 2                ' this is an entry point
                End If

                If PreviousIsIn And PresentIsOut Then

                    If Path(N - 1).IO = 2 Then
                        Path(N - 1).IO = 0           ' can not enter and leave at same time 
                        NoOfInside = NoOfInside - 1  ' excepts if it touches the border
                    ElseIf Path(N - 1).IO = 1 Then
                        Path(N - 1).IO = 3           ' a leaving point
                    End If

                    If N - NS > 1 Then
                        If Path(N - 2).IO = 2 Then
                            ' check if n-2 and n-1 are on the same edge; if yes then n-1=n-2=0
                            If CheckSameSide(N - 2, N - 1) Then
                                Path(N - 2).IO = 0
                                Path(N - 1).IO = 0
                                NoOfInside = NoOfInside - 2
                            End If
                        End If
                    End If

                End If

                PreviousIsIn = PresentIsIn
                PreviousIsOut = PresentIsOut

            ElseIf Path(N).T = 2 Then             ' it is an end of path and was already treated

                ' NS + 1 
                PresentIsIn = IsPtInQuad(NS + 1)  ' so re-check points 1 and 2 of path
                PresentIsOut = Not PresentIsIn

                If PreviousIsIn And PresentIsOut Then

                    If Path(N).IO = 2 Then
                        Path(N).IO = 0            ' a false entry 
                        NoOfInside = NoOfInside - 1
                    ElseIf Path(N).IO = 1 Then
                        Path(N).IO = 3
                    End If

                    If Path(N - 1).IO = 2 Then
                        If CheckSameSide(N - 1, N) Then
                            Path(N).IO = 0
                            Path(N - 1).IO = 0
                            NoOfInside = NoOfInside - 2
                        End If
                    End If

                End If

                PreviousIsIn = PresentIsIn
                PreviousIsOut = PresentIsOut
                ' NS + 2
                PresentIsIn = IsPtInQuad(NS + 2)
                PresentIsOut = Not PresentIsIn

                If PreviousIsIn And PresentIsOut Then

                    If Path(NS + 1).IO = 2 Then
                        Path(NS + 1).IO = 0 ' a false entry 
                        NoOfInside = NoOfInside - 1
                    ElseIf Path(NS + 1).IO = 1 Then
                        Path(NS + 1).IO = 3
                    End If

                    If Path(N).IO = 2 Then
                        If CheckSameSide(N, NS + 1) Then
                            Path(NS + 1).IO = 0
                            Path(N).IO = 0
                            NoOfInside = NoOfInside - 2
                        End If
                    End If

                End If

            End If

        Next

        NoOfQPts = 4
        For N = 1 To NoOfPathPts
            ' Debug.Print("Pt#" & N & "   Type=" & Path(N).T.ToString & "   IO=" & Path(N).IO.ToString)
            If Path(N).IO = 2 Then NoOfQPts = NoOfQPts + 1
            If Path(N).IO = 3 Then NoOfQPts = NoOfQPts + 1
        Next
        'Debug.Print(NoOfInside)

        ReDim QPts(NoOfQPts)
        MakeQuadCorners()
        Q = 5
        For N = 1 To NoOfPathPts
            If Path(N).IO = 2 Then
                MakeQuadPoint(2, N, Q)
                Q = Q + 1
            End If
            If Path(N).IO = 3 Then
                MakeQuadPoint(3, N, Q)
                Q = Q + 1
            End If
        Next

        Array.Sort(QPts, 1, NoOfQPts)

    End Sub

    Private Sub MakeQuadPoint(ByVal T As Integer, ByVal N As Integer, ByVal Q As Integer)

        QPts(Q).T = T
        QPts(Q).X = Path(N).X
        QPts(Q).Y = Path(N).Y
        QPts(Q).I = N

        If Path(N).Y = NLat Then
            QPts(Q).D = (Path(N).X - WLon) / DLon
            Exit Sub
        End If

        If Path(N).Y = SLat Then
            QPts(Q).D = 2 + (ELon - Path(N).X) / DLon
            Exit Sub
        End If

        If Path(N).X = ELon Then
            QPts(Q).D = 1 + (NLat - Path(N).Y) / DLat
            Exit Sub
        End If

        If Path(N).X = WLon Then
            QPts(Q).D = 3 + (Path(N).Y - SLat) / DLat
            Exit Sub
        End If

    End Sub

    Private Sub MakeQuadCorners()

        QPts(1).X = WLon
        QPts(1).Y = NLat
        QPts(1).T = 1
        QPts(1).D = 0

        QPts(2).X = ELon
        QPts(2).Y = NLat
        QPts(2).T = 1
        QPts(2).D = 1

        QPts(3).X = ELon
        QPts(3).Y = SLat
        QPts(3).T = 1
        QPts(3).D = 2

        QPts(4).X = WLon
        QPts(4).Y = SLat
        QPts(4).T = 1
        QPts(4).D = 3

    End Sub

    Private Function CheckSameSide(ByVal J As Integer, ByVal K As Integer) As Boolean

        CheckSameSide = True
        If Path(J).X = WLon Then
            If Path(K).X = WLon Then Exit Function
        End If
        If Path(J).X = ELon Then
            If Path(K).X = ELon Then Exit Function
        End If
        If Path(J).Y = NLat Then
            If Path(K).Y = NLat Then Exit Function
        End If
        If Path(J).Y = SLat Then
            If Path(K).Y = SLat Then Exit Function
        End If
        CheckSameSide = False

    End Function

    Friend Sub InsertLatCrossing(ByVal LA As Double)

        Dim N As Integer
        Dim D As Double
        Dim Y1, Y0 As Double
        Dim X1, X0 As Double

        N = 1
        Do
            If Not Path(N).T = 0 Then
                Y0 = Path(N - 1).Y
                Y1 = Path(N).Y
                If (Y0 < LA And Y1 > LA) Or (Y0 > LA And Y1 < LA) Then
                    X0 = Path(N - 1).X
                    X1 = Path(N).X
                    D = (X1 - X0) / (Y1 - Y0)
                    X0 = D * (LA - Y1)
                    X1 = X1 + X0
                    InsertPointInPath(X1, LA, N)
                    N = N + 1
                End If
            End If
            N = N + 1
        Loop While N <= NoOfPathPts

    End Sub

    Friend Sub InsertLonCrossing(ByVal LO As Double)

        Dim N As Integer
        Dim D As Double
        Dim Y1, Y0 As Double
        Dim X1, X0 As Double

        N = 1
        Do
            If Not Path(N).T = 0 Then
                X0 = Path(N - 1).X
                X1 = Path(N).X
                If (X0 < LO And X1 > LO) Or (X0 > LO And X1 < LO) Then
                    Y0 = Path(N - 1).Y
                    Y1 = Path(N).Y
                    D = (Y1 - Y0) / (X1 - X0)
                    Y0 = D * (X1 - LO)
                    Y1 = Y1 - Y0
                    InsertPointInPath(LO, Y1, N)
                    N = N + 1
                End If
            End If
            N = N + 1
        Loop While N <= NoOfPathPts

    End Sub

    Friend Sub InsertPointInPath(ByVal X As Double, ByVal Y As Double, ByVal Point As Integer)

        Dim K As Integer

        NoOfPathPts = NoOfPathPts + 1
        ReDim Preserve Path(NoOfPathPts)

        For K = NoOfPathPts - 1 To Point Step -1
            Path(K + 1) = Path(K)
        Next K

        Path(Point).Y = Y
        Path(Point).X = X
        Path(Point).T = 1

    End Sub

    Private Function IsPtInQuad(ByVal N As Integer) As Boolean

        ' checks if point N of Path is inside the borders of quad
        ' if exactly on the border, returns true

        IsPtInQuad = False
        If Path(N).Y > NLat Then Exit Function
        If Path(N).Y < SLat Then Exit Function
        If Path(N).X < WLon Then Exit Function
        If Path(N).X > ELon Then Exit Function
        IsPtInQuad = True

    End Function

    Private Function IsLLInQuad(ByVal lat As Double, ByVal lon As Double) As Boolean

        ' checks if (lon, lat) is inside the borders of quad
        ' if exactly on the border, returns true

        IsLLInQuad = False
        If lat > NLat Then Exit Function
        If lat < SLat Then Exit Function
        If lon < WLon Then Exit Function
        If lon > ELon Then Exit Function
        IsLLInQuad = True

    End Function

    Private Function IsPtInPath(ByVal Y As Double, ByVal X As Double) As Boolean

        ' checks point of longitude=x and latitude=y is inside the path

        Dim N As Integer
        Dim X1 As Double, Y1 As Double
        Dim X0 As Double, Y0 As Double
        Dim CP As Double  ' to make cross product

        ' test = going to infinitum_north and counting the jumps across borders
        IsPtInPath = False

        For N = 1 To NoOfPathPts
            If Path(N).T = 0 Then GoTo next_N
            Y1 = Path(N).Y
            Y0 = Path(N - 1).Y
            If Y1 = Y0 Then GoTo next_N
            If Y <= Y1 And Y <= Y0 Then GoTo next_N
            If Y > Y1 And Y > Y0 Then GoTo next_N
            X1 = Path(N).X
            X0 = Path(N - 1).X
            If X < X1 And X < X0 Then
                IsPtInPath = Not IsPtInPath
                GoTo next_N
            End If
            If X > X1 And X > X0 Then GoTo next_N
            CP = (X1 - X0) * (Y - Y0) / (Y1 - Y0) + X0
            If X < CP Then IsPtInPath = Not IsPtInPath
next_N:
        Next N

    End Function

    Friend Sub Clip2Quad()

        Dim N, K As Integer
        Dim lat, lon As Double

        If AllInside Then  ' if all points inside the quad, path into slices and exit
            AddAllToSlices()
            Exit Sub
        End If

        If NoneInside Then
            If IsPtInPath(CLat, CLon) = False Then  ' center of quad inside poly?
                Exit Sub   ' the quad has nothing to do with the poly
            Else
                AddFullSlice() ' no points because the whole quad is inside poly
                Exit Sub
            End If
        End If

        ' we get here, meaning that there are points inside and points outside
        If NoOfQPts = 4 Then     ' no crossings meaning there is a hole inside
            AddFullSlice()
            For N = 1 To NoOfChilds
                lat = Polys(Polys(PolyIndex).Childs(N)).GPoints(1).lat
                lon = Polys(Polys(PolyIndex).Childs(N)).GPoints(1).lon
                If IsLLInQuad(lat, lon) Then
                    'Slices(1).NC = Slices(1).NC + 1
                    'ReDim Preserve Slices(1).C(Slices(1).NC)
                    'Slices(1).C(Slices(1).NC) = Slices(1).NC + 1
                    AddHoleSlice(N, 1)
                End If
            Next
            Exit Sub
        End If

        Dim Rest As Integer    ' Remaining points in QPts
        Rest = NoOfQPts - 4
        Dim M As Integer       ' index to Quad Points
        Dim Mi As Integer      ' initial M
        Dim I As Integer       ' index to Path Points
        Dim C As Integer = 0   ' to count Consumed "NoOfInside" points
        Dim S As Slice
        ReDim S.P(NoOfPathPts) ' just in case!

        Do
            M = 1
            K = 1
            ' get the index of an unused entry point  
            Do While QPts(M).T <> 2
                M = IncrementM(M)
            Loop
            Mi = M
            ' starting the creation of polygon
            Do
                I = QPts(M).I   ' start with this I
                QPts(M).T = 0   ' mark as used
                Rest = Rest - 1
                Do
                    S.P(K).X = Path(I).X
                    S.P(K).Y = Path(I).Y
                    C = C + 1
                    K = K + 1
                    I = IncrementI(I)
                Loop Until Path(I).IO = 3
                S.P(K).X = Path(I).X
                S.P(K).Y = Path(I).Y
                C = C + 1
                K = K + 1

                M = GetQPtFromPath(I)
                QPts(M).T = 0    ' used
                Rest = Rest - 1

                M = IncrementM(M)
                Do While QPts(M).T = 1
                    S.P(K).X = QPts(M).X
                    S.P(K).Y = QPts(M).Y
                    M = IncrementM(M)
                    K = K + 1
                Loop
            Loop Until M = Mi
            NoOfSlices = NoOfSlices + 1
            ReDim Preserve Slices(NoOfSlices)
            Slices(NoOfSlices).N = K - 1
            ReDim Slices(NoOfSlices).P(Slices(NoOfSlices).N)
            Slices(NoOfSlices).NC = 0
            For K = 1 To Slices(NoOfSlices).N
                Slices(NoOfSlices).P(K).X = S.P(K).X
                Slices(NoOfSlices).P(K).Y = S.P(K).Y
            Next
            K = K - 1
            Slices(NoOfSlices).P(0).X = S.P(K).X
            Slices(NoOfSlices).P(0).Y = S.P(K).Y
        Loop While Rest > 1

        Rest = NoOfInside - C   ' Rest is now used for unused Inside points
        Dim NOfS As Integer = NoOfSlices
        For C = 1 To NoOfChilds
            If Polys(Polys(PolyIndex).Childs(C)).NoOfPoints <= Rest Then
                For N = 1 To NOfS
                    If ChildInSlice(C, N) Then
                        ' add slice
                        AddHoleSlice(C, N)
                        Rest = Rest - Polys(Polys(PolyIndex).Childs(C)).NoOfPoints
                        Exit For
                    End If
                Next N
                If Rest < 3 Then Exit For
            End If
        Next C

    End Sub

    Private Function ChildInSlice(ByVal C As Integer, ByVal N As Integer) As Boolean

        ChildInSlice = False
        Dim K As Integer
        Dim lat, lon As Double
        For K = 1 To Polys(Polys(PolyIndex).Childs(C)).NoOfPoints
            lat = Polys(Polys(PolyIndex).Childs(C)).GPoints(K).lat
            lon = Polys(Polys(PolyIndex).Childs(C)).GPoints(K).lon
            If Not IsPtInSlice(lat, lon, N) Then Exit Function
        Next
        ChildInSlice = True

    End Function


    Private Function IsPtInSlice(ByVal Y As Double, ByVal X As Double, ByVal I As Integer) As Boolean

        ' checks point of longitude=x and latitude=y is inside slice I

        Dim N As Integer
        Dim X1 As Double, Y1 As Double
        Dim X0 As Double, Y0 As Double
        Dim CP As Double  ' to make cross product

        ' test = going to infinitum_north and counting the jumps across borders
        IsPtInSlice = False

        For N = 1 To Slices(I).N
            'If Path(N).T = 0 Then GoTo next_N
            Y1 = Slices(I).P(N).Y
            Y0 = Slices(I).P(N - 1).Y
            If Y1 = Y0 Then GoTo next_N
            If Y <= Y1 And Y <= Y0 Then GoTo next_N
            If Y > Y1 And Y > Y0 Then GoTo next_N
            X1 = Slices(I).P(N).X
            X0 = Slices(I).P(N - 1).X
            If X < X1 And X < X0 Then
                IsPtInSlice = Not IsPtInSlice
                GoTo next_N
            End If
            If X > X1 And X > X0 Then GoTo next_N
            CP = (X1 - X0) * (Y - Y0) / (Y1 - Y0) + X0
            If X < CP Then IsPtInSlice = Not IsPtInSlice
next_N:
        Next N

    End Function


    Private Function IncrementI(ByVal I As Integer) As Integer

        If Path(I).T = 2 Then
            Do
                I = I - 1
            Loop While Path(I).T = 1
        End If
        IncrementI = I + 1

    End Function


    Private Function IncrementM(ByVal M As Integer) As Integer

        IncrementM = M + 1
        If IncrementM > NoOfQPts Then IncrementM = 1

    End Function


    Private Function GetQPtFromPath(ByVal I As Integer) As Integer

        Dim N As Integer
        GetQPtFromPath = 0
        For N = 1 To NoOfQPts
            If QPts(N).I = I Then
                GetQPtFromPath = N
                Exit Function
            End If
        Next

    End Function


    Private Sub AddFullSlice()

        ' create a slice with the 4 corners of the quad
        NoOfSlices = 1
        ReDim Slices(1)
        Slices(1).N = 4
        Slices(1).NC = 0
        ReDim Slices(1).P(4)
        Slices(1).P(1).X = WLon
        Slices(1).P(1).Y = NLat
        Slices(1).P(2).X = ELon
        Slices(1).P(2).Y = NLat
        Slices(1).P(3).X = ELon
        Slices(1).P(3).Y = SLat
        Slices(1).P(4).X = WLon
        Slices(1).P(4).Y = SLat

    End Sub

    Private Sub AddHoleSlice(ByVal C As Integer, ByVal P As Integer)

        ' add poly hole C to parent slice P

        Dim K As Integer
        Dim M As Integer = Polys(PolyIndex).Childs(C)

        Slices(P).NC = Slices(P).NC + 1
        ReDim Preserve Slices(P).C(Slices(P).NC)
        'Slices(P).C(Slices(P).NC) = Slices(P).NC + 1  ' aqui

        NoOfSlices = NoOfSlices + 1
        ReDim Preserve Slices(NoOfSlices)
        Slices(P).C(Slices(P).NC) = NoOfSlices ' aqui

        Slices(NoOfSlices).N = Polys(M).NoOfPoints
        ReDim Slices(NoOfSlices).P(Slices(NoOfSlices).N)
        Slices(NoOfSlices).NC = -P

        For K = 1 To Polys(M).NoOfPoints
            Slices(NoOfSlices).P(K).X = Polys(M).GPoints(K).lon
            Slices(NoOfSlices).P(K).Y = Polys(M).GPoints(K).lat
        Next

    End Sub

    Private Sub AddAllToSlices()

        Dim J, K, N, C As Integer
        Dim M As Integer = Polys(PolyIndex).NoOfChilds

        NoOfSlices = Polys(PolyIndex).NoOfChilds + 1
        ReDim Slices(NoOfSlices)

        Slices(1).N = Polys(PolyIndex).NoOfPoints
        ReDim Slices(1).P(Slices(1).N)
        Slices(NoOfSlices).NC = -1
        For K = 1 To Polys(PolyIndex).NoOfPoints
            Slices(1).P(K).X = Polys(PolyIndex).GPoints(K).lon
            Slices(1).P(K).Y = Polys(PolyIndex).GPoints(K).lat
        Next

        Slices(1).NC = M
        If M > 0 Then
            ReDim Slices(1).C(M)
            For K = 1 To M
                Slices(1).C(K) = K + 1
                'Next
                'For K = 1 To M
                C = Polys(PolyIndex).Childs(K)
                N = Polys(C).NoOfPoints
                Slices(K + 1).N = N
                ReDim Slices(K + 1).P(N)
                Slices(K + 1).NC = -1
                For J = 1 To N
                    Slices(K + 1).P(J).X = Polys(C).GPoints(J).lon
                    Slices(K + 1).P(J).Y = Polys(C).GPoints(J).lat
                Next
            Next
        End If

    End Sub

    Friend Sub Fill2Quad()

        If NoneInside Then
            If IsPtInPath(CLat, CLon) = False Then  ' center of quad inside poly?
                Exit Sub   ' the quad has nothing to do with the poly
            Else
                AddFullSlice() ' no points because the whole quad is inside poly
                Exit Sub
            End If
        Else
            AddFullSlice() ' one point at least so fill the quad
            Exit Sub
        End If

    End Sub

End Class

Friend Class ClipLine

    'Friend Structure Fragment  ' for lines
    '    Dim N As Long
    '    Dim NC As Integer
    '    Dim C() As Integer
    '    Dim P() As Double_XYZW
    'End Structure

    'Friend Fragments() As Fragment
    'Friend NoOfFragments As Integer

    Private Path() As Double_XYZW
    Private NoOfPathPts As Integer = 0


    Friend Sub SetLine(ByVal N As Integer)

        ' Fill Path with X Y data from Line N
        Dim J As Integer

        NoOfPathPts = Lines(N).NoOfPoints - 1
        ReDim Path(NoOfPathPts)
        For J = 0 To NoOfPathPts
            Path(J).X = Lines(N).GLPoints(J + 1).lon
            Path(J).Y = Lines(N).GLPoints(J + 1).lat
            Path(J).Z = Lines(N).GLPoints(J + 1).alt
            Path(J).W = Lines(N).GLPoints(J + 1).wid
        Next

    End Sub
    Private QMID As Integer
    Private NLat, SLat, WLon, ELon As Double  ' borders
    Private CLat, CLon, DLat, DLon As Double  ' center and delta
    Friend Sub SetQuad(ByVal Q As Integer, ByVal lat As Double, ByVal lon As Double)

        ' set QMID, NLat, SLat, WLon, ELon, DLat, DLon giving the QMID
        ' and a lat/lon point inside the quad

        QMID = Q
        Q = CInt(2 ^ (Q - 2))
        DLat = CDbl(90 / Q)  ' delta lat: the height of the quad
        DLon = CDbl(120 / Q)  ' delta lon: the width of the quad

        WLon = Int((lon + 180) / DLon) * DLon - 180
        ELon = WLon + DLon
        NLat = 90 - Int((90 - lat) / DLat) * DLat
        SLat = NLat - DLat
        CLat = (NLat + SLat) / 2
        CLon = (WLon + ELon) / 2

        ' check how many times the polygon line enters this quad
        ' and set a number of variables

        NoOfInside = 0
        'NoOfEntries = 0
        FirstIn = 0
        NoneInside = True
        AllInside = True

        Dim PreviousIsOut As Boolean = Not IsPtInQuad(0)
        Dim PresentIsIn As Boolean
        Dim N As Integer

        If PreviousIsOut Then AllInside = False

        For N = 1 To NoOfPathPts
            PresentIsIn = IsPtInQuad(N)
            If PresentIsIn Then
                NoneInside = False
                NoOfInside = NoOfInside + 1
            Else
                AllInside = False
            End If
            If PreviousIsOut And PresentIsIn Then
                If FirstIn = 0 Then FirstIn = N
            End If
            PreviousIsOut = Not PresentIsIn
        Next

    End Sub

    Private AllInside As Boolean
    Private NoneInside As Boolean
    Private NoOfInside As Integer
    Private FirstIn As Integer

    Friend Sub InsertLatCrossing(ByVal LA As Double)

        Dim N As Integer
        Dim D As Double
        Dim Y1, Y0 As Double
        Dim X1, X0 As Double

        N = 1
        Do
            Y0 = Path(N - 1).Y
            Y1 = Path(N).Y
            If (Y0 < LA And Y1 > LA) Or (Y0 > LA And Y1 < LA) Then
                X0 = Path(N - 1).X
                X1 = Path(N).X
                D = (X1 - X0) / (Y1 - Y0)
                X0 = D * (LA - Y1)
                X1 = X1 + X0
                InsertPointInPath(X1, LA, N)
                N = N + 1
            End If
            N = N + 1
        Loop While N <= NoOfPathPts

    End Sub

    Friend Sub InsertLonCrossing(ByVal LO As Double)

        Dim N As Integer
        Dim D As Double
        Dim Y1, Y0 As Double
        Dim X1, X0 As Double

        N = 1
        Do
            X0 = Path(N - 1).X
            X1 = Path(N).X
            If (X0 < LO And X1 > LO) Or (X0 > LO And X1 < LO) Then
                Y0 = Path(N - 1).Y
                Y1 = Path(N).Y
                D = (Y1 - Y0) / (X1 - X0)
                Y0 = D * (X1 - LO)
                Y1 = Y1 - Y0
                InsertPointInPath(LO, Y1, N)
                N = N + 1
            End If
            N = N + 1
        Loop While N <= NoOfPathPts

    End Sub

    Friend Sub InsertPointInPath(ByVal X As Double, ByVal Y As Double, ByVal Point As Integer)

        Dim K As Integer

        NoOfPathPts = NoOfPathPts + 1
        ReDim Preserve Path(NoOfPathPts)

        For K = NoOfPathPts - 1 To Point Step -1
            Path(K + 1) = Path(K)
        Next K

        Path(Point).Y = Y
        Path(Point).X = X

        Dim Z, W As Double
        ZWFromXY(Z, W, X, Y, Point - 1, Point + 1)
        Path(Point).Z = Z
        Path(Point).W = W

    End Sub

    Private Sub ZWFromXY(ByRef Z As Double, ByRef W As Double, _
                            ByVal x As Double, ByVal y As Double, _
                            ByVal P0 As Integer, ByVal P1 As Integer)

        Dim x0, y0, z0, w0, x1, y1, z1, w1, dx, dy As Double

        x0 = Path(P0).X
        y0 = Path(P0).Y
        z0 = Path(P0).Z
        w0 = Path(P0).W

        x1 = Path(P1).X
        y1 = Path(P1).Y
        z1 = Path(P1).Z
        w1 = Path(P1).W

        dx = x1 - x0
        dy = y1 - y0
        If dy > dx Then
            y = (y1 - y) / dy
            x = 1 - y
            Z = y * z0 + x * z1
            W = y * w0 + x * w1
        Else
            x = (x1 - x) / dx
            y = 1 - x
            Z = x * z0 + y * z1
            W = x * w0 + y * w1
        End If

    End Sub

    Private Function IsPtInQuad(ByVal N As Integer) As Boolean

        ' checks if point N of Path is inside the borders of quad
        ' if exactly on the border, returns true

        IsPtInQuad = False
        If Path(N).Y > NLat Then Exit Function
        If Path(N).Y < SLat Then Exit Function
        If Path(N).X < WLon Then Exit Function
        If Path(N).X > ELon Then Exit Function
        IsPtInQuad = True

    End Function

    Friend Sub Fragment2Quad()

        Dim N As Integer

        If NoneInside Then Exit Sub

        If AllInside Then  ' if all points inside the quad, make a fragment with them and exit
            NoOfFragments = NoOfFragments + 1
            ReDim Preserve Fragments(NoOfFragments)
            Fragments(NoOfFragments).N = NoOfPathPts + 1
            'Fragments(NoOfFragments).N = NoOfPathPts

            ReDim Fragments(NoOfFragments).P(NoOfPathPts + 1)
            For N = 1 To NoOfPathPts + 1
                Fragments(NoOfFragments).P(N).X = Path(N - 1).X
                Fragments(NoOfFragments).P(N).Y = Path(N - 1).Y
                Fragments(NoOfFragments).P(N).Z = Path(N - 1).Z
                Fragments(NoOfFragments).P(N).W = Path(N - 1).W
            Next N
            Exit Sub
        End If

        ' we get here, meaning that there are points inside and points outside
        ' and there will be one fragment at least

        Dim PreviousIsIn As Boolean = False
        Dim PreviousIsOut As Boolean = True
        Dim PresentIsOut As Boolean
        Dim PresentIsIn As Boolean

        Dim K As Integer

        K = 1 ' index to fragments()
        For N = FirstIn To NoOfPathPts
            PresentIsIn = IsPtInQuad(N)
            PresentIsOut = Not PresentIsIn
            If PreviousIsOut And PresentIsIn Then
                NoOfFragments = NoOfFragments + 1
                ReDim Preserve Fragments(NoOfFragments)
                ReDim Fragments(NoOfFragments).P(NoOfPathPts)
            End If
            If PresentIsIn Then
                Fragments(NoOfFragments).P(K).X = Path(N).X
                Fragments(NoOfFragments).P(K).Y = Path(N).Y
                Fragments(NoOfFragments).P(K).Z = Path(N).Z
                Fragments(NoOfFragments).P(K).W = Path(N).W
                K = K + 1
            End If
            If PreviousIsIn And PresentIsOut Then
                ReDim Preserve Fragments(NoOfFragments).P(K - 1)
                Fragments(NoOfFragments).N = K - 1
                K = 1
            End If
            PreviousIsOut = PresentIsOut
            PreviousIsIn = PresentIsIn
        Next

        For N = 0 To FirstIn - 1
            PresentIsIn = IsPtInQuad(N)
            PresentIsOut = Not PresentIsIn
            If PreviousIsOut And PresentIsIn Then
                NoOfFragments = NoOfFragments + 1
                ReDim Preserve Fragments(NoOfFragments)
                ReDim Fragments(NoOfFragments).P(NoOfPathPts)
            End If
            If PresentIsIn Then
                Fragments(NoOfFragments).P(K).X = Path(N).X
                Fragments(NoOfFragments).P(K).Y = Path(N).Y
                Fragments(NoOfFragments).P(K).Z = Path(N).Z
                Fragments(NoOfFragments).P(K).W = Path(N).W
                K = K + 1
            End If
            If PreviousIsIn And PresentIsOut Then
                ReDim Preserve Fragments(NoOfFragments).P(K - 1)
                Fragments(NoOfFragments).N = K - 1
                K = 1
            End If
            PreviousIsOut = PresentIsOut
            PreviousIsIn = PresentIsIn
        Next

    End Sub

End Class


'ORIGINAL FOLLOWS!


'Public Class SliceFill

'    Private WP() As Double_XYZW ' the working polygon
'    Private QQ() As Double_XY ' points inside the quad
'    Private NP() As Double_XY
'    Private IO() As Double

'    'Private Structure XYDouble
'    '    Dim X As Double
'    '    Dim Y As Double
'    'End Structure

'    'Private Structure Slice
'    '    Dim N As Long
'    '    Dim P() As XYDouble
'    'End Structure

'    'Public Slices() As Slice
'    'Public NoOfSlices As Integer


'    ' HOW IT WORKS
'    ' ************

'    ' SetPoly  < sets the poligon that will be sliced
'    ' SetQuad  < sets the quad 
'    ' SlicePoly  < gets the slices of the set poly on the set quad

'    Private NoOfChilds As Integer

'    Public Sub SetPoly(ByVal N As Integer)

'        ' Fill WP with X Y data from Polygon N
'        Dim J As Integer

'        NoOfWPPts = Polys(N).NoOfPoints
'        ReDim WP(NoOfWPPts)
'        For J = 1 To NoOfWPPts
'            WP(J).X = Polys(N).GPoints(J).lon
'            WP(J).Y = Polys(N).GPoints(J).lat
'        Next
'        WP(0).X = WP(J - 1).X
'        WP(0).Y = WP(J - 1).Y

'    End Sub

'    Public Sub SetLine(ByVal N As Integer)

'        ' Fill WP with X Y data from Polygon N
'        Dim J As Integer

'        NoOfWPPts = Lines(N).NoOfPoints - 1
'        ReDim WP(NoOfWPPts)
'        For J = 0 To NoOfWPPts
'            WP(J).X = Lines(N).GLPoints(J + 1).lon
'            WP(J).Y = Lines(N).GLPoints(J + 1).lat
'            WP(J).Z = Lines(N).GLPoints(J + 1).alt
'            WP(J).W = Lines(N).GLPoints(J + 1).wid
'        Next

'    End Sub


'    Private QMID As Integer
'    Private NLat, SLat, WLon, ELon As Double  ' borders
'    Private CLat, CLon, DLat, DLon As Double  ' center and delta
'    Public Sub SetQuad(ByVal Q As Integer, ByVal lat As Double, ByVal lon As Double)

'        ' set QMID, NLat, SLat, WLon, ELon, DLat, DLon giving the QMID
'        ' and a lat/lon point inside the quad

'        QMID = Q
'        Q = 2 ^ (Q - 2)
'        DLat = CDbl(90 / Q)  ' delta lat: the height of the quad
'        DLon = CDbl(120 / Q)  ' delta lon: the width of the quad

'        WLon = Int((lon + 180) / DLon) * DLon - 180
'        ELon = WLon + DLon
'        NLat = 90 - Int((90 - lat) / DLat) * DLat
'        SLat = NLat - DLat
'        CLat = (NLat + SLat) / 2
'        CLon = (WLon + ELon) / 2

'        ' check how many times the polygon line enters this quad
'        ' and set a number of variables

'        NoOfInside = 0
'        'NoOfEntries = 0
'        FirstIn = 0
'        NoneInside = True
'        AllInside = True

'        Dim PreviousIsOut As Boolean = Not IsPtInQuad(0)
'        Dim PresentIsIn As Boolean
'        Dim N As Integer

'        If PreviousIsOut Then AllInside = False

'        For N = 1 To NoOfWPPts
'            PresentIsIn = IsPtInQuad(N)
'            If PresentIsIn Then
'                NoneInside = False
'                NoOfInside = NoOfInside + 1
'            Else
'                AllInside = False
'            End If
'            If PreviousIsOut And PresentIsIn Then
'                If FirstIn = 0 Then FirstIn = N
'            End If
'            PreviousIsOut = Not PresentIsIn
'        Next

'    End Sub

'    Private AllInside As Boolean
'    Private NoneInside As Boolean
'    Private NoOfInside As Integer
'    Private NoOfWPPts As Integer
'    Private NoOfQQPts As Integer
'    Private FirstIn As Integer

'    Public Sub InsertLatCrossing(ByVal LA As Double)

'        Dim N As Integer
'        Dim D As Double
'        Dim Y1, Y0 As Double
'        Dim X1, X0 As Double

'        N = 1
'        Do
'            Y0 = WP(N - 1).Y
'            Y1 = WP(N).Y
'            If (Y0 < LA And Y1 > LA) Or (Y0 > LA And Y1 < LA) Then
'                X0 = WP(N - 1).X
'                X1 = WP(N).X
'                D = (X1 - X0) / (Y1 - Y0)
'                'X = D * (lat  - Y1)
'                X0 = D * (LA - Y1)
'                X1 = X1 + X0
'                InsertPointInWP(X1, LA, N)
'                N = N + 1
'            End If
'            N = N + 1
'        Loop While N <= NoOfWPPts


'    End Sub


'    Public Sub InsertLonCrossing(ByVal LO As Double)

'        Dim N As Integer
'        Dim D As Double
'        Dim Y1, Y0 As Double
'        Dim X1, X0 As Double

'        N = 1
'        Do
'            X0 = WP(N - 1).X
'            X1 = WP(N).X
'            If (X0 < LO And X1 > LO) Or (X0 > LO And X1 < LO) Then
'                Y0 = WP(N - 1).Y
'                Y1 = WP(N).Y
'                D = (Y1 - Y0) / (X1 - X0)
'                'Y = D * (X1 - LO)
'                Y0 = D * (X1 - LO)
'                Y1 = Y1 - Y0
'                InsertPointInWP(LO, Y1, N)
'                N = N + 1
'            End If
'            N = N + 1
'        Loop While N <= NoOfWPPts


'    End Sub

'    Friend Sub InsertPointInWP(ByVal X As Double, ByVal Y As Double, ByVal Point As Integer)

'        Dim K As Integer

'        NoOfWPPts = NoOfWPPts + 1
'        ReDim Preserve WP(NoOfWPPts)

'        'If Point = 1 Then Point = NoOfWPPts  ' never happens

'        For K = NoOfWPPts - 1 To Point Step -1
'            WP(K + 1) = WP(K)
'        Next K

'        WP(Point).Y = Y
'        WP(Point).X = X

'        Dim Z, W As Double
'        ZWFromXY(Z, W, X, Y, Point - 1, Point + 1)
'        WP(Point).Z = Z
'        WP(Point).W = W

'        'WP(0).X = WP(NoOfWPPts).X  ' never happens
'        'WP(0).Y = WP(NoOfWPPts).Y

'    End Sub

'    Private Sub ZWFromXY(ByRef Z As Double, ByRef W As Double, _
'                         ByVal x As Double, ByVal y As Double, _
'                         ByVal P0 As Integer, ByVal P1 As Integer)

'        Dim x0, y0, z0, w0, x1, y1, z1, w1, dx, dy As Double

'        x0 = WP(P0).X
'        y0 = WP(P0).Y
'        z0 = WP(P0).Z
'        w0 = WP(P0).W

'        x1 = WP(P1).X
'        y1 = WP(P1).Y
'        z1 = WP(P1).Z
'        w1 = WP(P1).W

'        dx = x1 - x0
'        dy = y1 - y0
'        If dy > dx Then
'            y = (y1 - y) / dy
'            x = 1 - y
'            Z = y * z0 + x * z1
'            W = y * w0 + x * w1
'        Else
'            x = (x1 - x) / dx
'            y = 1 - x
'            Z = x * z0 + y * z1
'            W = x * w0 + y * w1
'        End If

'    End Sub

'    Public Sub FillPoly()

'        If NoneInside Then
'            If IsPtInPoly(CLat, CLon) = False Then  ' center of quad inside poly?
'                Exit Sub   ' the quad has nothing to do with the poly
'            Else
'                AddFullSlice() ' no points because the whole quad is inside poly
'                Exit Sub
'            End If
'        Else
'            AddFullSlice() ' one point at least so fill the quad
'            Exit Sub
'        End If

'    End Sub

'    Public Sub SliceLine()

'        Dim N As Integer

'        If NoneInside Then Exit Sub

'        If AllInside Then  ' if all points inside the quad, make a slice with them and exit
'            NoOfSlices = NoOfSlices + 1
'            ReDim Preserve Slices(NoOfSlices)
'            Slices(NoOfSlices).N = NoOfWPPts + 1
'            ' Slices(NoOfSlices).N = NoOfWPPts

'            ReDim Slices(NoOfSlices).P(NoOfWPPts + 1)
'            For N = 1 To NoOfWPPts + 1
'                Slices(NoOfSlices).P(N).X = WP(N - 1).X
'                Slices(NoOfSlices).P(N).Y = WP(N - 1).Y
'                Slices(NoOfSlices).P(N).Z = WP(N - 1).Z
'                Slices(NoOfSlices).P(N).W = WP(N - 1).W
'            Next N
'            Exit Sub
'        End If

'        ' we get here, meaning that there are points inside and points outside
'        ' and there will be one slice at least

'        Dim PreviousIsIn As Boolean = False
'        Dim PreviousIsOut As Boolean = True
'        Dim PresentIsOut As Boolean
'        Dim PresentIsIn As Boolean

'        Dim K As Integer

'        K = 1 ' index to Slices()
'        For N = FirstIn To NoOfWPPts
'            PresentIsIn = IsPtInQuad(N)
'            PresentIsOut = Not PresentIsIn
'            If PreviousIsOut And PresentIsIn Then
'                NoOfSlices = NoOfSlices + 1
'                ReDim Preserve Slices(NoOfSlices)
'                ReDim Slices(NoOfSlices).P(NoOfWPPts)
'            End If
'            If PresentIsIn Then
'                Slices(NoOfSlices).P(K).X = WP(N).X
'                Slices(NoOfSlices).P(K).Y = WP(N).Y
'                Slices(NoOfSlices).P(K).Z = WP(N).Z
'                Slices(NoOfSlices).P(K).W = WP(N).W
'                K = K + 1
'            End If
'            If PreviousIsIn And PresentIsOut Then
'                ReDim Preserve Slices(NoOfSlices).P(K - 1)
'                Slices(NoOfSlices).N = K - 1
'                K = 1
'            End If
'            PreviousIsOut = PresentIsOut
'            PreviousIsIn = PresentIsIn
'        Next

'        For N = 0 To FirstIn - 1
'            PresentIsIn = IsPtInQuad(N)
'            PresentIsOut = Not PresentIsIn
'            If PreviousIsOut And PresentIsIn Then
'                NoOfSlices = NoOfSlices + 1
'                ReDim Preserve Slices(NoOfSlices)
'                ReDim Slices(NoOfSlices).P(NoOfWPPts)
'            End If
'            If PresentIsIn Then
'                Slices(NoOfSlices).P(K).X = WP(N).X
'                Slices(NoOfSlices).P(K).Y = WP(N).Y
'                Slices(NoOfSlices).P(K).Z = WP(N).Z
'                Slices(NoOfSlices).P(K).W = WP(N).W
'                K = K + 1
'            End If
'            If PreviousIsIn And PresentIsOut Then
'                ReDim Preserve Slices(NoOfSlices).P(K - 1)
'                Slices(NoOfSlices).N = K - 1
'                K = 1
'            End If
'            PreviousIsOut = PresentIsOut
'            PreviousIsIn = PresentIsIn
'        Next

'    End Sub


'    Public Sub SlicePoly()

'        Dim N As Integer

'        If AllInside Then  ' if all points inside the quad, make a slice with them and exit
'            NoOfSlices = NoOfSlices + 1
'            ReDim Preserve Slices(NoOfSlices)
'            Slices(NoOfSlices).N = NoOfWPPts
'            ReDim Slices(NoOfSlices).P(NoOfWPPts)
'            For N = 1 To NoOfWPPts
'                Slices(NoOfSlices).P(N).X = WP(N).X
'                Slices(NoOfSlices).P(N).Y = WP(N).Y
'            Next N
'            Exit Sub
'        End If

'        If NoneInside Then
'            If IsPtInPoly(CLat, CLon) = False Then  ' center of quad inside poly?
'                Exit Sub   ' the quad has nothing to do with the poly
'            Else
'                AddFullSlice() ' no points because the whole quad is inside poly
'                Exit Sub
'            End If
'        End If

'        ' we get here, meaning that there are points inside and points outside
'        ' prepare a new Poly called QQ to hold points that belong to the quad
'        ' for each entry there will be a inner border point and an outer
'        ' point that will be added to QQ

'        NoOfQQPts = NoOfInside
'        ReDim QQ(NoOfQQPts)
'        ReDim IO(NoOfQQPts)

'        SetQQPoly()  ' construct QQ() adding entry and exit points at the quad borders

'        N = NoOfQQPts
'        Do Until N < 2
'            N = SliceFromQQ()
'        Loop

'    End Sub

'    Private Function IsPtInQuad(ByVal N As Integer) As Boolean

'        'checks if point N of polygon WP is inside the borders of quad
'        ' if on the border it is true

'        IsPtInQuad = False
'        If WP(N).Y > NLat Then Exit Function
'        If WP(N).Y < SLat Then Exit Function
'        If WP(N).X < WLon Then Exit Function
'        If WP(N).X > ELon Then Exit Function
'        IsPtInQuad = True

'    End Function

'    Private Sub AddFullSlice()

'        ' create a slice with the 4 corners of the quad
'        NoOfSlices = NoOfSlices + 1
'        ReDim Preserve Slices(NoOfSlices)
'        Slices(NoOfSlices).N = 4
'        ReDim Slices(NoOfSlices).P(4)
'        Slices(NoOfSlices).P(1).X = WLon
'        Slices(NoOfSlices).P(1).Y = NLat
'        Slices(NoOfSlices).P(2).X = ELon
'        Slices(NoOfSlices).P(2).Y = NLat
'        Slices(NoOfSlices).P(3).X = ELon
'        Slices(NoOfSlices).P(3).Y = SLat
'        Slices(NoOfSlices).P(4).X = WLon
'        Slices(NoOfSlices).P(4).Y = SLat

'    End Sub

'    Private Sub SetQQPoly()

'        ' check how many times the polygon line exits the quad

'        Dim PreviousIsIn As Boolean = False
'        Dim PreviousIsOut As Boolean = True
'        Dim PresentIsOut As Boolean
'        Dim PresentIsIn As Boolean

'        Dim N, K As Integer

'        K = 1 ' index to QQ()
'        For N = FirstIn To NoOfWPPts
'            PresentIsIn = IsPtInQuad(N)
'            PresentIsOut = Not PresentIsIn
'            If PresentIsIn Then
'                QQ(K).X = WP(N).X
'                QQ(K).Y = WP(N).Y
'                IO(K) = -1
'            End If
'            If PreviousIsOut And PresentIsIn Then
'                SetIOPoint(K, N)
'            End If
'            If PreviousIsIn And PresentIsOut Then
'                SetIOPoint(K - 1, N - 1)
'            End If
'            If PresentIsIn Then K = K + 1
'            PreviousIsOut = PresentIsOut
'            PreviousIsIn = PresentIsIn
'        Next

'        For N = 1 To FirstIn - 1
'            PresentIsIn = IsPtInQuad(N)
'            PresentIsOut = Not PresentIsIn
'            If PresentIsIn Then
'                QQ(K).X = WP(N).X
'                QQ(K).Y = WP(N).Y
'                IO(K) = -1
'            End If
'            If PreviousIsOut And PresentIsIn Then
'                SetIOPoint(K, N)
'            End If
'            If PreviousIsIn And PresentIsOut Then
'                SetIOPoint(K - 1, N - 1)
'            End If
'            If PresentIsIn Then K = K + 1
'            PreviousIsOut = PresentIsOut
'            PreviousIsIn = PresentIsIn
'        Next

'    End Sub

'    'Private Sub SetEntryPoint(ByVal K As Integer, ByVal N As Integer)

'    '    SetQuadTetas(N)

'    '    Dim DY As Double = WP(N - 1).Y - WP(N).Y
'    '    Dim DX As Double = WP(N - 1).X - WP(N).X
'    '    Dim T As Double = Teta2(DY, DX)

'    '    DY = -DY
'    '    DX = -DX

'    '    Dim M, A, B As Double   ' y = m x + b     m=dy/dx or  dx/dy   b=a/dx   or b=-a/dy

'    '    If T > TetaNE And T <= TetaNW Then   ' the side is north = 1
'    '        QQ(K).Y = NLat
'    '        M = DX / DY
'    '        A = WP(N - 1).X * WP(N).Y - WP(N - 1).Y * WP(N).X
'    '        B = A / DY
'    '        QQ(K).X = M * NLat + B
'    '        IO(K) = (QQ(K).X - WLon) / DLon
'    '    ElseIf T > TetaNW And T <= TetaSW Then ' the side is west = 4
'    '        QQ(K).X = WLon
'    '        M = DY / DX
'    '        A = WP(N - 1).Y * WP(N).X - WP(N - 1).X * WP(N).Y
'    '        B = A / DX
'    '        QQ(K).Y = M * WLon + B
'    '        IO(K) = 3 + (QQ(K).Y - SLat) / DLat
'    '    ElseIf T > TetaSW And T <= TetaSE Then ' the side is south = 3
'    '        QQ(K).Y = SLat
'    '        M = DX / DY
'    '        A = WP(N - 1).X * WP(N).Y - WP(N - 1).Y * WP(N).X
'    '        B = A / DY
'    '        QQ(K).X = M * SLat + B
'    '        IO(K) = 2 + (ELon - QQ(K).X) / DLon
'    '    Else ' the side is east = 2
'    '        QQ(K).X = ELon
'    '        M = DY / DX
'    '        A = WP(N - 1).Y * WP(N).X - WP(N - 1).X * WP(N).Y
'    '        B = A / DX
'    '        QQ(K).Y = M * ELon + B
'    '        IO(K) = 1 + (NLat - QQ(K).Y) / DLat
'    '    End If

'    'End Sub

'    'Private Sub SetEntryPoint(ByVal K As Integer, ByVal N As Integer)

'    '    Dim Y As Double = WP(N).Y
'    '    Dim X As Double = WP(N).X

'    '    QQ(K).Y = Y
'    '    QQ(K).X = X

'    '    If Y = NLat Then   ' the side is north = 1
'    '        IO(K) = (X - WLon) / DLon
'    '        Exit Sub
'    '    End If

'    '    If X = WLon Then ' the side is west = 4
'    '        IO(K) = 3 + (Y - SLat) / DLat
'    '        Exit Sub
'    '    End If

'    '    If Y = SLat Then ' the side is south = 3
'    '        IO(K) = 2 + (ELon - X) / DLon
'    '        Exit Sub
'    '    End If

'    '    If X = WLon Then ' the side is east = 2
'    '        IO(K) = 1 + (NLat - Y) / DLat
'    '        Exit Sub
'    '    End If

'    'End Sub

'    'Private Sub SetExitPoint(ByVal K As Integer, ByVal N As Integer)

'    '    SetQuadTetas(N - 1)

'    '    Dim DY As Double = WP(N).Y - WP(N - 1).Y
'    '    Dim DX As Double = WP(N).X - WP(N - 1).X
'    '    Dim T As Double = Teta2(DY, DX)

'    '    Dim M, A, B As Double   ' y = m x + b     m=dy/dx or  dx/dy   b=a/dx   or -a/dy

'    '    If T > TetaNE And T <= TetaNW Then   ' the side is north = 1
'    '        QQ(K).Y = NLat
'    '        M = DX / DY
'    '        A = WP(N - 1).X * WP(N).Y - WP(N - 1).Y * WP(N).X
'    '        B = A / DY
'    '        QQ(K).X = M * NLat + B
'    '        IO(K) = (QQ(K).X - WLon) / DLon
'    '    ElseIf T > TetaNW And T <= TetaSW Then ' the side is west = 4
'    '        QQ(K).X = WLon
'    '        M = DY / DX
'    '        A = WP(N - 1).Y * WP(N).X - WP(N - 1).X * WP(N).Y
'    '        B = A / DX
'    '        QQ(K).Y = M * WLon + B
'    '        IO(K) = 3 + (QQ(K).Y - SLat) / DLat
'    '    ElseIf T > TetaSW And T <= TetaSE Then ' the side is south = 3
'    '        QQ(K).Y = SLat
'    '        M = DX / DY
'    '        A = WP(N - 1).X * WP(N).Y - WP(N - 1).Y * WP(N).X
'    '        B = A / DY
'    '        QQ(K).X = M * SLat + B
'    '        IO(K) = 2 + (ELon - QQ(K).X) / DLon
'    '    Else ' the side is east = 2
'    '        QQ(K).X = ELon
'    '        M = DY / DX
'    '        A = WP(N - 1).Y * WP(N).X - WP(N - 1).X * WP(N).Y
'    '        B = A / DX
'    '        QQ(K).Y = M * ELon + B
'    '        IO(K) = 1 + (NLat - QQ(K).Y) / DLat
'    '    End If

'    'End Sub

'    Private Sub SetIOPoint(ByVal K As Integer, ByVal N As Integer)

'        Dim Y As Double = WP(N).Y
'        Dim X As Double = WP(N).X

'        If Y = NLat Then   ' the side is north = 1
'            IO(K) = (X - WLon) / DLon
'            Exit Sub
'        End If

'        If X = WLon Then ' the side is west = 4
'            IO(K) = 3 + (Y - SLat) / DLat
'            Exit Sub
'        End If

'        If Y = SLat Then ' the side is south = 3
'            IO(K) = 2 + (ELon - X) / DLon
'            Exit Sub
'        End If

'        If X = ELon Then ' the side is east = 2
'            IO(K) = 1 + (NLat - Y) / DLat
'            Exit Sub
'        End If


'    End Sub


'    'Private Sub SetQuadTetas(ByVal N As Integer)

'    '    ' set TetaNW TetaNE TetaSE TetaSW - the argument of the
'    '    ' corners of quad using WP(N) as the origin

'    '    TetaNW = Teta2(NLat - WP(N).Y, WLon - WP(N).X)
'    '    TetaNE = Teta2(NLat - WP(N).Y, ELon - WP(N).X)
'    '    TetaSE = Teta2(SLat - WP(N).Y, ELon - WP(N).X)
'    '    TetaSW = Teta2(SLat - WP(N).Y, WLon - WP(N).X)

'    'End Sub

'    Private Function Teta2(ByVal DY As Double, ByVal DX As Double) As Double

'        If DY = 0 Then
'            If DX < 0 Then
'                Teta2 = PI
'                Exit Function
'            End If
'        End If

'        If DX = 0 Then  'dy = 0 is impossible to get here
'            If DY > 0 Then Teta2 = PI / 2
'            If DY < 0 Then Teta2 = 3 * PI / 2
'            Exit Function
'        End If

'        Teta2 = Math.Atan2(DY, DX)
'        If Teta2 < 0 Then Teta2 = Teta2 + 2 * PI

'    End Function

'    Private Function IsPtInPoly(ByVal y As Double, ByVal x As Double) As Boolean

'        ' checks point of longitude=x and latitude=y is inside WP

'        Dim N, S As Integer
'        Dim X1 As Double, Y1 As Double
'        Dim X0 As Double, Y0 As Double
'        Dim CP As Double  ' to make cross product

'        IsPtInPoly = False

'        S = NoOfWPPts

'        For N = 2 To S
'            Y1 = WP(N).Y
'            Y0 = WP(N - 1).Y
'            If Y1 = Y0 Then GoTo next_N
'            If y <= Y1 And y <= Y0 Then GoTo next_N
'            If y > Y1 And y > Y0 Then GoTo next_N
'            X1 = WP(N).X
'            X0 = WP(N - 1).X
'            If x < X1 And x < X0 Then
'                IsPtInPoly = Not IsPtInPoly
'                GoTo next_N
'            End If
'            If x > X1 And x > X0 Then GoTo next_N
'            CP = (X1 - X0) * (y - Y0) / (Y1 - Y0) + X0
'            If x < CP Then IsPtInPoly = Not IsPtInPoly

'next_N:
'        Next N

'        Y1 = WP(1).Y
'        Y0 = WP(S).Y

'        If Y0 = Y1 Then Exit Function
'        If y <= Y1 And y <= Y0 Then Exit Function
'        If y > Y1 And y > Y0 Then Exit Function
'        X1 = WP(1).X
'        X0 = WP(S).X
'        If x < X1 And x < X0 Then
'            IsPtInPoly = Not IsPtInPoly
'            Exit Function
'        End If
'        If x > X1 And x > X0 Then Exit Function

'        CP = (X1 - X0) * (y - Y0) / (Y1 - Y0) + X0
'        If x < CP Then IsPtInPoly = Not IsPtInPoly

'    End Function


'    Private Function SliceFromQQ() As Integer

'        Dim N, M, K, C As Integer

'        Dim SKPQQ() As Double_XY  ' SKP = skip poly 
'        Dim SKPIO() As Double
'        Dim AP As Double_XY

'        Dim K1, K2 As Integer
'        Dim L1, L2 As Double

'        ' examine points in QQ and copy to NP those points
'        ' that form a good Slice poly. Insert corners if needed
'        ' when finish construct QQ with unused or left points

'        ' construt NP <<< new poly

'        ReDim NP(NoOfQQPts + 15)   'Taburet error 7 in December 2004  ????
'        ReDim SKPQQ(NoOfQQPts)
'        ReDim SKPIO(NoOfQQPts)

'        ' get side of first point (N=0 W=3 S=2 E=1) and initiate NP
'        L1 = IO(1)
'        NP(1) = QQ(1)

'        M = 2 ' M points to NP
'        N = 2 ' N points to QQ
'        K = 1 ' K points to SKPQQ

'        ' go until you get a border. Keep points
'        Do
'            NP(M) = QQ(N)
'            M = M + 1
'            N = N + 1
'        Loop While IO(N - 1) = -1
'        ' here N and M point to next free indexes

'        L2 = IO(N - 1)

'Return_Here:
'        If N >= NoOfQQPts Then GoTo all_done

'        ' now we turn clockwise
'        ' if next point not on "clockwise rest of L2 border" skip set that follows
'        ' otherwise keep in
'        ' it is not like this!
'        ' actually the next point could be in rest of the same border but a future point
'        ' could be more close! in April 18 2004

'        ' parameters L2 and M are called by reference
'        If TakeNext(L1, L2, N, M) = False Then  ' skip points
'            Do
'                SKPQQ(K) = QQ(N)
'                SKPIO(K) = IO(N)
'                N = N + 1
'                K = K + 1
'            Loop While IO(N) = -1
'            SKPQQ(K) = QQ(N)
'            SKPIO(K) = IO(N)
'            K = K + 1
'            N = N + 1
'        Else                                    ' keep points
'            NP(M) = QQ(N)
'            M = M + 1
'            N = N + 1
'            Do
'                NP(M) = QQ(N)
'                M = M + 1
'                N = N + 1
'            Loop While IO(N - 1) = -1
'            L2 = IO(N - 1)
'        End If

'        GoTo Return_Here

'all_done:

'        'K1 = Int(L1 / 255)   ' October 8
'        'K2 = Int(L2 / 255)

'        K1 = Int(L1)
'        K2 = Int(L2)

'        C = K1 - K2
'        If C < 0 Then C = C + 4
'        If C = 0 Then
'            If L2 >= L1 Then C = 4
'            If L2 = L1 Then ' added in September 2004
'                If NPIsClock(M - 1, K2) Then C = 0
'            End If
'        End If

'        Dim NoOfNPPts = M - 1 + C
'        'NP.N = M - 1 + C
'        ReDim Preserve NP(NoOfNPPts)
'        For N = 1 To C
'            AP = Corner(L2, N)
'            NP(M).X = AP.X
'            NP(M).Y = AP.Y
'            M = M + 1
'        Next N

'        N = NoOfSlices + 1
'        NoOfSlices = N
'        ReDim Preserve Slices(N)
'        Slices(N).N = NoOfNPPts
'        ReDim Slices(N).P(NoOfNPPts)
'        For M = 1 To NoOfNPPts
'            Slices(N).P(M).X = NP(M).X
'            Slices(N).P(M).Y = NP(M).Y
'        Next M

'skip_this:
'        'SliceFromQQ = K 
'        'If K > 1 Then
'        '    ReDim Preserve SKPQQ(K - 1)
'        '    QQ = SKPQQ
'        'End If

'        NoOfQQPts = K - 1
'        SliceFromQQ = NoOfQQPts
'        If NoOfQQPts > 1 Then
'            ReDim QQ(NoOfQQPts)
'            ReDim IO(NoOfQQPts)
'            For K = 1 To NoOfQQPts
'                QQ(K) = SKPQQ(K)
'                IO(K) = SKPIO(K)
'            Next
'        End If



'    End Function

'    Private Function TakeNext(ByVal L1 As Double, ByRef L2 As Double, ByVal N As Integer, ByRef M As Integer) As Boolean

'        ' if true means that the next segment of QQ starting at N should be included in NP
'        ' it starts looking to the clockwise side of L2 until the clockwise next corner.

'        Dim K, P, PX As Double
'        Dim C As Integer, Flag As Boolean

'Re_Do:
'        ' If L2 = 1020 Then L2 = 0
'        If L2 = 4 Then L2 = 0
'        TakeNext = False

'        ' changed 8 October
'        'K = Int((L2) / 255)
'        'If K = 0 Then K = 255
'        'If K = 1 Then K = 510
'        'If K = 2 Then K = 765
'        'If K = 3 Then K = 1020

'        K = 1
'        If L2 >= 1 Then K = 2
'        If L2 >= 2 Then K = 3
'        If L2 >= 3 Then K = 4



'        'P = GetBPSide(BP.P(N))
'        P = IO(N)

'        ' if L1 and L2 are on the same edge with L1 clockwise of L2
'        ' then if and P not between L1 and L2 >>> skip next
'        If L1 > L2 And L1 < K Then If P > L1 Or P < L2 Then Exit Function

'        ' if next segment starts to the clockwise of L2 and before the corner
'        ' and if there is no one nearer then take it

'        If P >= L2 And P <= K Then

'            ' added April 18
'            'For C = N + 1 To BP.N
'            '    If BP.P(C).IO = 1 Then
'            '        PX = GetBPSide(BP.P(C))
'            '        If PX >= L2 And PX < P Then Exit Function
'            '    End If
'            'Next C
'            For C = N + 1 To NoOfQQPts
'                If IO(C) >= 0 Then
'                    PX = IO(C)              ' DEVE SER ENTRY?
'                    If PX >= L2 And PX < P Then Exit Function
'                End If
'            Next C
'            ' end of April 18

'            TakeNext = True
'            Exit Function
'        End If

'        ' so P is not on the clockwise rest of the edge! so check if there are more intersections
'        Flag = False
'        For C = N + 1 To NoOfQQPts
'            If IO(C) >= 0 Then
'                PX = IO(C)
'                If PX > L2 And PX <= K Then Flag = True 'september 2004 was If px >= L2 And px <= K Then Flag = True
'            End If
'        Next C

'        ' if there is no more intersects then add corner and increment L2
'        If Flag Then
'            Exit Function
'        End If

'        Dim AP As Double_XY = Corner(L2, 1)
'        NP(M).X = AP.X
'        NP(M).Y = AP.Y
'        M = M + 1

'        L2 = K
'        GoTo Re_Do

'    End Function

'    Private Function Corner(ByVal L2 As Double, ByVal N As Integer) As Double_XY

'        ' return the coordinates of the "next" (if N=1) "next next" (if N=2) clockwise
'        ' corner that follows L2

'        Dim K As Integer

'        'K = (Int(L2 / 255) + N) Mod 4
'        K = (Int(L2) + N) Mod 4

'        If K = 0 Then   ' corner 0 = NW
'            Corner.X = WLon
'            Corner.Y = NLat
'            Exit Function
'        End If
'        If K = 1 Then   ' corner 1 = NE
'            Corner.X = ELon
'            Corner.Y = NLat
'            Exit Function
'        End If
'        If K = 2 Then   ' corner 2 = SE
'            Corner.X = ELon
'            Corner.Y = SLat
'            Exit Function
'        End If
'        If K = 3 Then   ' corner 3 = SW
'            Corner.X = WLon
'            Corner.Y = SLat
'            Exit Function
'        End If

'    End Function

'    Private Function NPIsClock(ByVal S As Integer, ByVal L As Integer) As Boolean

'        Dim N, M As Integer
'        Dim lat As Double, LatN As Double
'        Dim X1 As Double, Y1 As Double
'        Dim X2 As Double, Y2 As Double
'        Dim X3 As Double, Y3 As Double

'        If L <> 2 Then  ' not 2 points in south !!!

'            ' get southest point in M (if 2 then the right most)
'            lat = NP(1).Y
'            M = 1
'            For N = 2 To S
'                LatN = NP(N).Y
'                If LatN >= lat Then
'                    If LatN > lat Then
'                        M = N
'                        lat = LatN
'                    Else
'                        If NP(N).X > NP(M).X Then
'                            M = N
'                            lat = LatN
'                        End If
'                    End If
'                End If
'            Next N

'            ' form the vectors M-1>M  and M>M+1  (1>2 2>3)
'            X2 = NP(M).X
'            Y2 = 255 - NP(M).Y
'            If M = 1 Then
'                X1 = NP(S).X
'                Y1 = NP(S).Y
'            Else
'                X1 = NP(M - 1).X
'                Y1 = NP(M - 1).Y
'            End If
'            If M = S Then
'                X3 = NP(1).X
'                Y3 = NP(1).Y
'            Else
'                X3 = NP(M + 1).X
'                Y3 = NP(M + 1).Y
'            End If

'            ' vector 1>2 in 1 and 2>3 in 2 then cross product in x3
'            X1 = X2 - X1
'            Y1 = Y2 - Y1
'            X2 = X3 - X2
'            Y2 = Y3 - Y2

'            X3 = X1 * Y2 - Y1 * X2

'            NPIsClock = False

'            If X3 < 0 Then NPIsClock = True ' is  clock wise

'            Exit Function

'        Else

'            ' get eastest point in M (if 2 then the north most)
'            lat = NP(1).X
'            M = 1
'            For N = 2 To S
'                LatN = NP(N).X
'                If LatN >= lat Then
'                    If LatN > lat Then
'                        M = N
'                        lat = LatN
'                    Else
'                        If NP(N).Y < NP(M).Y Then
'                            M = N
'                            lat = LatN
'                        End If
'                    End If
'                End If
'            Next N

'            ' form the vectors M-1>M  and M>M+1  (1>2 2>3)
'            X2 = NP(M).X
'            Y2 = NP(M).Y
'            If M = 1 Then
'                X1 = NP(S).X
'                Y1 = NP(S).Y
'            Else
'                X1 = NP(M - 1).X
'                Y1 = NP(M - 1).Y
'            End If
'            If M = S Then
'                X3 = NP(1).X
'                Y3 = NP(1).Y
'            Else
'                X3 = NP(M + 1).X
'                Y3 = NP(M + 1).Y
'            End If

'            ' vector 1>2 in 1 and 2>3 in 2 then cross product in x3
'            X1 = X2 - X1
'            Y1 = Y2 - Y1
'            X2 = X3 - X2
'            Y2 = Y3 - Y2

'            X3 = X1 * Y2 - Y1 * X2

'            NPIsClock = False

'            If X3 < 0 Then NPIsClock = True ' is  clock wise

'            Exit Function

'        End If

'    End Function

'End Class
