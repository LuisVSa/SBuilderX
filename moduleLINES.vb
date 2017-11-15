Option Strict On
Option Explicit On
Imports System.xml
Imports System.text
'Imports VB = Microsoft.VisualBasic
Module moduleLINES

    <Serializable()> Friend Structure GLine
        Dim Name As String
        Dim Type As String
        Dim Guid As String
        Dim Color As Color
        Dim Selected As Boolean
        Dim NoOfPoints As Integer
        Dim GLPoints() As GLPoint
        Dim NLAT As Double ' not saved
        Dim SLAT As Double ' not saved
        Dim WLON As Double ' not saved
        Dim ELON As Double ' not saved
        Dim OnScreen As Boolean

    End Structure

    Friend Structure LineType
        Dim Name As String
        Dim Type As String
        Dim Color As Color
        Dim Guid As String
        Dim Texture As String
        Dim TerrainIndex As Integer
    End Structure

    Friend LineTypes() As LineType
    Friend NoOfLineTypes As Integer

    Friend Structure ExtrusionType
        Dim Name As String
        Dim Color As Color
        Dim Width As Double
        Dim Profile As String
        Dim Material As String
        Dim Pylon As String
    End Structure
    Friend NoOfExtrusionTypes As Integer
    Friend ExtrusionTypes() As ExtrusionType

    Friend Lines() As GLine
    Friend NoOfLines As Integer = 0
    Friend NoOfLinesSelected As Integer = 0
    Friend NewLine As GLine

    Friend DefaultLineAltitude As Double
    Friend DefaultLineWidth As Double
    Friend ExtraExtrusionAltitude As String

    ' these are read from Lines.TXT
    Friend DefaultLineNoneGuid As String
    Friend DefaultLineFS9Guid As String

    Private H_NLat As Double ' header borders
    Private H_SLat As Double ' header borders
    Private H_WLon As Double ' header borders
    Private H_ELon As Double ' header borders

    Friend BreakLineON As Boolean
    Friend DecreaseWithdON As Boolean
    Friend IncreaseWithdON As Boolean

    Friend LineON As Boolean
    Friend LineVIEW As Boolean

    Friend LineTURN As Boolean
    Private LineToTurn As Integer
    Private PointToTurn As Integer

    Friend CheckLine As Integer
    Friend CheckLinePt As Integer

    Friend AutoLinePolyJoin As Boolean
    Friend DisplayJoin As Boolean 'If true 3 pixels if false D21
    Friend DirJoin As Boolean ' if true ignore direction
    Friend NameJoin As Boolean ' if true ignore names in joining

    'Friend JoinCellLines As Boolean ' if true join lines that cross cells on VTP appending

    Friend SelectedLineColor As Color
    Friend DefaultLineColor As Color
    Friend LinePenWidth As Integer

    ' Friend DefaultWidth As Double

    Friend PtLineCounter As Integer
    Friend AuxLatLine As Double
    Friend AuxLonLine As Double

    Private PTS(0 To 3) As Point    ' used to draw the polygons that define a line

    Friend Sub LineInsertMode(ByVal Button As Short, ByVal Shift As Short, ByVal X As Integer, ByVal Y As Integer)

        ' enters here when the mouse goes down in line mode

        If Button = 1 Then

            If IsCenterDisplay(X, Y) Then
                X = X - DisplayCenterX
                Y = Y - DisplayCenterY
                SetDispCenter(X, Y)
                X = DisplayCenterX
                Y = DisplayCenterY
                RebuildDisplay()
            End If

            If PtLineCounter > 0 Then   ' start line creation begun earlier
                BuildLine(X, Y)
            Else     ' the first click
                frmStart.CopyMenuItem.Enabled = False
                frmStart.DeleteMenuItem.Enabled = False
                ' SHIFT IS DOWN!
                If Shift = 1 Then 'was a point selected?
                    'SomeSelected = SomeSelected Or IsPointInLine(X, Y)
                    If IsPointInLine(X, Y) Then  ' was a point selected?
                        SomeSelected = True
                        RebuildDisplay()    ' start the movement of the point and the rest
                        SetDelay(200)
                        MoveON = True
                        FirstMOVE = True
                        AuxXInt = X
                        AuxYInt = Y
                        Exit Sub
                    End If   ' is a line selected?
                    'SomeSelected = SomeSelected Or IsLineSelected(LonDispWest * PixelsPerLonDeg + X, LatDispNorth * PixelsPerLatDeg - Y)
                    If IsLineSelected(LonDispWest * PixelsPerLonDeg + X, LatDispNorth * PixelsPerLatDeg - Y) Then
                        SomeSelected = True
                        RebuildDisplay()
                        SetDelay(200)
                        frmStart.CopyMenuItem.Enabled = True
                        frmStart.DeleteMenuItem.Enabled = True
                        MoveON = True
                        FirstMOVE = True
                        AuxXInt = X
                        AuxYInt = Y
                    End If
                    Exit Sub
                End If
                ' SHIFT IS NOT DOWN!
                UnSelectAll()
                SomeSelected = SomeSelected Or IsPointInLine(X, Y)
                If SomeSelected Then  ' was a point selected?
                    If DeleteON Then
                        DeleteSelectedPointsInLines()
                        RebuildDisplay()
                        Exit Sub
                    End If
                    If BreakLineON Then
                        BreakLines()
                        RebuildDisplay()
                        Exit Sub
                    End If
                    If DecreaseWithdON Then
                        DecreaseWidth()
                        RebuildDisplay()
                        Exit Sub
                    End If
                    If IncreaseWithdON Then
                        IncreaseWidth()
                        RebuildDisplay()
                        Exit Sub
                    End If
                    RebuildDisplay()    ' start the movement of the point and the rest
                    SetDelay(200)
                    MoveON = True
                    FirstMOVE = True
                    AuxXInt = X
                    AuxYInt = Y
                    Exit Sub
                Else  ' is InsertON true and we clicked on a segment of a line?
                    SomeSelected = SomeSelected Or IsInsertPointInLine(X, Y)
                    If SomeSelected Then
                        RebuildDisplay()
                        SetDelay(200)
                        MoveON = True
                        FirstMOVE = True
                        AuxXInt = X
                        AuxYInt = Y
                        Exit Sub ' new
                    ElseIf InsertON Then    ' InsertON is true and we did not click a segment, so do nothing
                        Exit Sub
                    Else                    ' did we selected a line?
                        SomeSelected = SomeSelected Or IsLineSelected(LonDispWest * PixelsPerLonDeg + X, LatDispNorth * PixelsPerLatDeg - Y)
                        If SomeSelected Then
                            RebuildDisplay()
                            SetDelay(200)
                            frmStart.CopyMenuItem.Enabled = True
                            frmStart.DeleteMenuItem.Enabled = True
                            MoveON = True
                            FirstMOVE = True
                            AuxXInt = X
                            AuxYInt = Y
                            Exit Sub ' new
                        ElseIf IsLineObjectTurn(X, Y) Then   ' is turnobject
                            'RebuildDisplay()
                            SetDelay(200)
                            LineTURN = True
                            frmStart.SetMouseIcon()
                            Exit Sub
                        Else   'no!!! so this is the start of the creation of a new line
                            If BackUpON Then BackUp()
                            RebuildDisplay()
                            BuildLine(X, Y)
                        End If
                    End If
                End If
            End If
        End If

        If Button = 2 Then
            EndLine(X, Y)
            RebuildDisplay()
        End If

    End Sub

    Friend Sub BuildLine(ByVal X As Integer, ByRef Y As Integer)

        ' this looks to the number of clicks (in PtLineCounter)
        ' if PtLineCounter = 0 it is the first click
        ' if PtLineCounter = 2 it is the second click and
        '      a new line is created
        ' if PtLineCounter > 2 a new point is added to the new line

        Dim Lat, Lon As Double

        Lat = LatDispNorth - CDbl(Y / PixelsPerLatDeg)
        Lon = LonDispWest + CDbl(X / PixelsPerLonDeg)

        If PtLineCounter = 0 Then
            SelectAllLines(False)
            NoOfLinesSelected = 0
            AuxLatLine = Lat
            AuxLonLine = Lon
            PtLineCounter = 2
            Exit Sub
        End If

        If PtLineCounter = 2 Then
            PtLineCounter = 3
            ReDim NewLine.GLPoints(2)
            NewLine.NoOfPoints = 2
            NewLine.Color = DefaultLineColor
            NewLine.Selected = False
            NewLine.GLPoints(1).lat = AuxLatLine
            NewLine.GLPoints(1).lon = AuxLonLine
            NewLine.GLPoints(1).alt = DefaultLineAltitude
            NewLine.GLPoints(1).wid = DefaultLineWidth

            NewLine.GLPoints(1).Selected = False
            AuxLatLine = Lat
            AuxLonLine = Lon
            NewLine.GLPoints(2).lat = Lat
            NewLine.GLPoints(2).lon = Lon
            NewLine.GLPoints(2).alt = DefaultLineAltitude
            NewLine.GLPoints(2).wid = DefaultLineWidth
            NewLine.GLPoints(2).Selected = False
            Exit Sub
        End If

        ' PtLineCounter is > 2

        AuxLatLine = Lat
        AuxLonLine = Lon
        ReDim Preserve NewLine.GLPoints(PtLineCounter)
        NewLine.GLPoints(PtLineCounter).lat = Lat
        NewLine.GLPoints(PtLineCounter).lon = Lon
        NewLine.GLPoints(PtLineCounter).alt = DefaultLineAltitude
        NewLine.GLPoints(PtLineCounter).wid = DefaultLineWidth
        NewLine.GLPoints(PtLineCounter).Selected = False
        NewLine.NoOfPoints = PtLineCounter

        PtLineCounter = PtLineCounter + 1

    End Sub

    Friend Sub EndLine(ByVal X As Integer, ByVal Y As Integer)

        If PtLineCounter = 0 Then
            ProcessPopUp(X, Y)
            Exit Sub
        End If

        If PtLineCounter = 2 Then
            PtLineCounter = 0
            Exit Sub
        End If

        ' ok, create the line!
        PtLineCounter = 0

        NoOfLines = NoOfLines + 1
        ReDim Preserve Lines(NoOfLines)
        Lines(NoOfLines) = NewLine
        Dirty = True

        If (NoOfLines > 1) And AutoLinePolyJoin Then
            If TryThisLineJoin(NoOfLines) Then
                Beep()
                AddLatLonToLine(NoOfLines)
                Exit Sub
            End If
        End If
        Lines(NoOfLines).Name = Str(Lines(NoOfLines).NoOfPoints) & "_Pts_Line_of_Type_None"
        Lines(NoOfLines).Guid = DefaultLineNoneGuid
        Lines(NoOfLines).Type = "NONE"
        AddLatLonToLine(NoOfLines)
        HidePopUPMenu()

    End Sub

    Private Function IsLineObjectTurn(ByVal X As Integer, ByVal Y As Integer) As Boolean

        ' on entry X Y contain distance from NW corner display in pixels

        Dim N, K As Integer
        
        IsLineObjectTurn = False

        For N = 1 To NoOfLines
            If Mid(Lines(N).Type, 1, 3) = "OBJ" Then
                SetLenWidFromObject(N)
                For K = 1 To Lines(N).NoOfPoints
                    SetHeadingsFromObject(N, K)
                    If HDX > X + 5 Then GoTo next_k
                    If HDX < X - 5 Then GoTo next_k
                    If HDY < Y - 5 Then GoTo next_k
                    If HDY > Y + 5 Then GoTo next_k
                    LineToTurn = N
                    PointToTurn = K
                    Lines(N).GLPoints(K).Selected = True
                    IsLineObjectTurn = True
                    Exit Function
next_k:
                Next K
            End If
        Next N

    End Function

    Friend Sub TurnLine(ByVal X As Integer, ByVal Y As Integer)


        SetLenWidFromObject(LineToTurn)
        SetHeadingsFromObject(LineToTurn, PointToTurn)

        Dim Y0, X0, Head As Single
        'X0 = (Objects(ObjectID).lon - LonDispWest) * PixelsPerLonDeg
        'Y0 = (LatDispNorth - Objects(ObjectID).lat) * PixelsPerLatDeg

        X0 = X - P0X
        Y0 = P0Y - Y

        Head = 0
        If Y0 > 0 Then
            Head = CSng(System.Math.Atan(X0 / Y0) * 180 / PI)
        ElseIf Y0 < 0 Then
            Head = CSng(180 + System.Math.Atan(X0 / Y0) * 180 / PI)
        Else
            If X0 > 0 Then
                Head = 90
            Else
                Head = 270
            End If
        End If

        If Head < 0 Then Head = Head + 360

        Lines(LineToTurn).GLPoints(PointToTurn).wid = Head
        RebuildDisplay()

    End Sub

    Friend Function TryThisLineJoin(ByVal LJ As Integer) As Boolean

        Dim K, N, NP As Integer
        Dim X1J, Y1J As Double
        Dim XNJ, YNJ As Double
        Dim X1, Y1 As Double
        Dim XN, YN As Double
        Dim DX, DY As Double

        Dim DeltaLat, DeltaLon As Double

        If DisplayJoin Then
            DeltaLon = 3 / PixelsPerLonDeg
            DeltaLat = 3 / PixelsPerLatDeg
        Else
            DeltaLon = D255Lon
            DeltaLat = D255Lat
        End If

        TryThisLineJoin = False

        K = Lines(LJ).NoOfPoints
        X1J = Lines(LJ).GLPoints(1).lon
        Y1J = Lines(LJ).GLPoints(1).lat
        XNJ = Lines(LJ).GLPoints(K).lon
        YNJ = Lines(LJ).GLPoints(K).lat

        For N = 1 To NoOfLines
            If N = LJ Then GoTo NextN
            If NameJoin Then If Lines(N).Name <> Lines(LJ).Name Then GoTo NextN
            If Lines(N).Type <> Lines(LJ).Type Then GoTo NextN
            NP = Lines(N).NoOfPoints
            X1 = Lines(N).GLPoints(1).lon
            Y1 = Lines(N).GLPoints(1).lat
            XN = Lines(N).GLPoints(NP).lon
            YN = Lines(N).GLPoints(NP).lat
Nextx0:
            DX = System.Math.Abs(X1 - XNJ)
            If DX > DeltaLon Then GoTo Next1
            DY = System.Math.Abs(Y1 - YNJ)
            If DY > DeltaLat Then GoTo Next1
            JoinLines(LJ, N, 1, 1)
            TryThisLineJoin = True
            Exit Function
Next1:
            DX = System.Math.Abs(XN - X1J)
            If DX > DeltaLon Then GoTo Next2
            DY = System.Math.Abs(YN - Y1J)
            If DY > DeltaLat Then GoTo Next2
            JoinLines(N, LJ, 1, 1)
            TryThisLineJoin = True
            Exit Function
Next2:
            If DirJoin Then GoTo NextN

            DX = System.Math.Abs(XN - XNJ)
            If DX > DeltaLon Then GoTo Next3
            DY = System.Math.Abs(YN - YNJ)
            If DY > DeltaLat Then GoTo Next3
            JoinLines(LJ, N, 1, -1)
            TryThisLineJoin = True
            Exit Function
Next3:
            DX = System.Math.Abs(X1 - X1J)
            If DX > DeltaLon Then GoTo NextN
            DY = System.Math.Abs(Y1 - Y1J)
            If DY > DeltaLat Then GoTo NextN
            JoinLines(LJ, N, -1, 1)
            TryThisLineJoin = True
            Exit Function
NextN:
        Next N


    End Function
    Friend Sub JoinLines(ByVal N1 As Integer, ByVal N2 As Integer, ByVal F1 As Integer, ByVal F2 As Integer)

        Dim P2, P1, N As Integer

        P1 = Lines(N1).NoOfPoints
        P2 = Lines(N2).NoOfPoints

        If F1 = -1 Then ReverseLine(N1)
        If F2 = -1 Then ReverseLine(N2)

        Lines(N1).NoOfPoints = P1 + P2 - 1
        ReDim Preserve Lines(N1).GLPoints(Lines(N1).NoOfPoints)

        For N = 2 To P2
            Lines(N1).GLPoints(N + P1 - 1).wid = Lines(N2).GLPoints(N).wid
            Lines(N1).GLPoints(N + P1 - 1).alt = Lines(N2).GLPoints(N).alt
            Lines(N1).GLPoints(N + P1 - 1).lat = Lines(N2).GLPoints(N).lat
            Lines(N1).GLPoints(N + P1 - 1).lon = Lines(N2).GLPoints(N).lon
        Next N

        Lines(N1).Selected = True
        AddLatLonToLine(N1)
        DeleteLine(N2)

    End Sub

    Friend Sub TryAllLineJoin()

        Dim N, K As Integer
        Dim Done(NoOfLines) As Boolean

jump_here:
        For N = 1 To NoOfLines
            If Done(N) Then GoTo next_n
            If N > NoOfLines - 1 Then Exit Sub
            If Lines(N).Selected Then
                For K = N + 1 To NoOfLines
                    If K > NoOfLines Then Exit For
                    If Lines(K).Selected Then
                        If Try2LineJoin(N, K) Then GoTo jump_here
                    End If
                Next
            End If
            Done(N) = True
next_n:
        Next

    End Sub

    Private Function Try2LineJoin(ByVal N1 As Integer, ByVal N2 As Integer) As Boolean

        ' N1 and N2 are the lines to join!

        Dim N As Integer
        Dim X1, XN, YN, Y1 As Double
        Dim D1, X, D, DN As Double

        Dim DNN, D11 As Double

        Try2LineJoin = False

        If NameJoin Then
            If Lines(N1).Name <> Lines(N2).Name Then Exit Function
        End If

        N = Lines(N1).NoOfPoints
        XN = Lines(N1).GLPoints(N).lon
        YN = Lines(N1).GLPoints(N).lat
        X1 = Lines(N1).GLPoints(N - 1).lon
        Y1 = Lines(N1).GLPoints(N - 1).lat
        DN = XN - X1
        DN = DN * DN
        X = YN - Y1
        X = X * X
        DN = DN + X
        DN = System.Math.Sqrt(DN)

        XN = Lines(N2).GLPoints(1).lon
        YN = Lines(N2).GLPoints(1).lat
        X1 = Lines(N2).GLPoints(2).lon
        Y1 = Lines(N2).GLPoints(2).lat
        D1 = XN - X1
        D1 = D1 * D1
        X = YN - Y1
        X = X * X
        D1 = D1 + X
        D1 = System.Math.Sqrt(D1)

        XN = Lines(N1).GLPoints(N).lon
        YN = Lines(N1).GLPoints(N).lat
        X1 = Lines(N2).GLPoints(1).lon
        Y1 = Lines(N2).GLPoints(1).lat
        D = XN - X1
        D = D * D
        X = YN - Y1
        X = X * X
        D = D + X
        D = System.Math.Sqrt(D)

        If D < D1 Or D < DN Then
            JoinLines(N1, N2, 1, 1)
            Try2LineJoin = True
            Beep()
            Exit Function
        End If

        N = Lines(N2).NoOfPoints
        XN = Lines(N2).GLPoints(N).lon
        YN = Lines(N2).GLPoints(N).lat
        X1 = Lines(N2).GLPoints(N - 1).lon
        Y1 = Lines(N2).GLPoints(N - 1).lat
        DN = XN - X1
        DN = DN * DN
        X = YN - Y1
        X = X * X
        DN = DN + X
        DN = System.Math.Sqrt(DN)

        XN = Lines(N1).GLPoints(1).lon
        YN = Lines(N1).GLPoints(1).lat
        X1 = Lines(N1).GLPoints(2).lon
        Y1 = Lines(N1).GLPoints(2).lat
        D1 = XN - X1
        D1 = D1 * D1
        X = YN - Y1
        X = X * X
        D1 = D1 + X
        D1 = System.Math.Sqrt(D1)

        XN = Lines(N2).GLPoints(N).lon
        YN = Lines(N2).GLPoints(N).lat
        X1 = Lines(N1).GLPoints(1).lon
        Y1 = Lines(N1).GLPoints(1).lat
        D = XN - X1
        D = D * D
        X = YN - Y1
        X = X * X
        D = D + X
        D = System.Math.Sqrt(D)

        If D < D1 Or D < DN Then
            JoinLines(N2, N1, 1, 1)
            Try2LineJoin = True
            Beep()
            Exit Function
        End If

        If DirJoin Then Exit Function

        Dim NP1 As Integer = Lines(N1).NoOfPoints
        Dim NP2 As Integer = Lines(N2).NoOfPoints

        ' NP1 & NP2
        XN = Lines(N1).GLPoints(NP1).lon
        YN = Lines(N1).GLPoints(NP1).lat
        X1 = Lines(N2).GLPoints(NP2).lon
        Y1 = Lines(N2).GLPoints(NP2).lat
        D = XN - X1
        D = D * D
        X = YN - Y1
        X = X * X
        D = D + X
        DNN = System.Math.Sqrt(D)

        ' 1 & 1
        XN = Lines(N1).GLPoints(1).lon
        YN = Lines(N1).GLPoints(1).lat
        X1 = Lines(N2).GLPoints(1).lon
        Y1 = Lines(N2).GLPoints(1).lat
        D = XN - X1
        D = D * D
        X = YN - Y1
        X = X * X
        D = D + X
        D11 = System.Math.Sqrt(D)

        If DNN < D11 Then

            XN = Lines(N1).GLPoints(NP1).lon
            YN = Lines(N1).GLPoints(NP1).lat
            X1 = Lines(N1).GLPoints(NP1 - 1).lon
            Y1 = Lines(N1).GLPoints(NP1 - 1).lat
            DN = XN - X1
            DN = DN * DN
            X = YN - Y1
            X = X * X
            DN = DN + X
            DN = System.Math.Sqrt(DN)

            XN = Lines(N2).GLPoints(NP2).lon
            YN = Lines(N2).GLPoints(NP2).lat
            X1 = Lines(N2).GLPoints(NP2 - 1).lon
            Y1 = Lines(N2).GLPoints(NP2 - 1).lat
            D1 = XN - X1
            D1 = D1 * D1
            X = YN - Y1
            X = X * X
            D1 = D1 + X
            D1 = System.Math.Sqrt(D1)

            If DNN < D1 Or D < DN Then
                JoinLines(N1, N2, 1, -1)
                Try2LineJoin = True
                Beep()
                Exit Function
            End If

        Else
            ' 1 and 1
            N = Lines(N1).NoOfPoints
            XN = Lines(N1).GLPoints(1).lon
            YN = Lines(N1).GLPoints(1).lat
            X1 = Lines(N1).GLPoints(2).lon
            Y1 = Lines(N1).GLPoints(2).lat
            DN = XN - X1
            DN = DN * DN
            X = YN - Y1
            X = X * X
            DN = DN + X
            DN = System.Math.Sqrt(DN)

            XN = Lines(N2).GLPoints(1).lon
            YN = Lines(N2).GLPoints(1).lat
            X1 = Lines(N2).GLPoints(2).lon
            Y1 = Lines(N2).GLPoints(2).lat
            D1 = XN - X1
            D1 = D1 * D1
            X = YN - Y1
            X = X * X
            D1 = D1 + X
            D1 = System.Math.Sqrt(D1)

            If D11 < D1 Or D < DN Then
                JoinLines(N1, N2, -1, 1)
                Try2LineJoin = True
                Beep()
                Exit Function
            End If

        End If

    End Function


    Friend Sub CheckLineJoins()

        Dim N, N1 As Integer
        Dim Flag As Boolean

        BackUp()
        SkipBackUp = True

        N1 = 1
        Flag = False
Return_back:
        N = N1
        Do While N <= NoOfLines
            If Lines(N).Selected Then
                If TryThisLineJoin(N) Then
                    Beep()
                    Flag = True
                    GoTo Return_back
                End If
            End If
            N = N + 1
            N1 = N
        Loop

        SkipBackUp = False

        If Flag Then RebuildDisplay()

        For N = 1 To NoOfLines
            If Lines(N).GLPoints(1).Selected Then
                If TryThisLineJoin(N) Then Exit Sub
            End If
            If Lines(N).GLPoints(Lines(N).NoOfPoints).Selected Then
                If TryThisLineJoin(N) Then Exit Sub
            End If
        Next N

    End Sub
    Private Sub BreakLines()

        Dim L, N, K, p As Integer

        'BackUp

        For N = 1 To NoOfLines
            If Lines(N).Selected Then GoTo NextN
            p = Lines(N).NoOfPoints
            For K = 2 To p - 1
                If Lines(N).GLPoints(K).Selected Then
                    NoOfLines = NoOfLines + 1
                    ReDim Preserve Lines(NoOfLines)
                    'Lines(NoOfLines).NoOfPoints = p - K
                    Lines(NoOfLines).NoOfPoints = p - K + 1
                    ReDim Lines(NoOfLines).GLPoints(Lines(NoOfLines).NoOfPoints)
                    'For L = K + 1 To p
                    For L = K To p
                        'Lines(NoOfLines).GLPoints(L - K).alt = Lines(N).GLPoints(L).alt
                        'Lines(NoOfLines).GLPoints(L - K).lat = Lines(N).GLPoints(L).lat
                        'Lines(NoOfLines).GLPoints(L - K).lon = Lines(N).GLPoints(L).lon
                        Lines(NoOfLines).GLPoints(L - K + 1).wid = Lines(N).GLPoints(L).wid
                        Lines(NoOfLines).GLPoints(L - K + 1).alt = Lines(N).GLPoints(L).alt
                        Lines(NoOfLines).GLPoints(L - K + 1).lat = Lines(N).GLPoints(L).lat
                        Lines(NoOfLines).GLPoints(L - K + 1).lon = Lines(N).GLPoints(L).lon

                    Next L
                    Lines(NoOfLines).Color = Lines(N).Color
                    Lines(NoOfLines).Name = Lines(N).Name
                    Lines(NoOfLines).Type = Lines(N).Type
                    Lines(NoOfLines).Guid = Lines(N).Guid
                    'Lines(NoOfLines).Selected = Lines(N).Selected
                    Lines(NoOfLines).Selected = True
                    'Lines(N).NoOfPoints = K - 1
                    Lines(N).NoOfPoints = K
                    ReDim Preserve Lines(N).GLPoints(Lines(N).NoOfPoints)
                    AddLatLonToLine(NoOfLines)
                    AddLatLonToLine(N)
                    Beep()
                    Exit Sub
                End If
            Next K
NextN:
        Next N



    End Sub
    Private Sub IncreaseWidth()

        Dim N, K As Integer
        Dim w As Double

        'BackUp

        For N = 1 To NoOfLines
            If Lines(N).Selected Then GoTo NextN
            For K = 1 To Lines(N).NoOfPoints
                If Lines(N).GLPoints(K).Selected Then
                    w = 1.5 * Lines(N).GLPoints(K).wid
                    If w > 255 Then w = 255
                    Lines(N).GLPoints(K).wid = w
                    Exit Sub
                End If
            Next K
NextN:
        Next N


    End Sub

    Private Sub DecreaseWidth()

        Dim N, K As Integer
        Dim w As Double

        'BackUp

        For N = 1 To NoOfLines
            If Lines(N).Selected Then GoTo NextN
            For K = 1 To Lines(N).NoOfPoints
                If Lines(N).GLPoints(K).Selected Then
                    w = Lines(N).GLPoints(K).wid / 1.5
                    If w < 1 Then w = 1
                    Lines(N).GLPoints(K).wid = w
                    Exit Sub
                End If
            Next K
NextN:
        Next N

    End Sub


    Friend Sub SelectAllLines(ByRef Flag As Boolean)

        Dim N, K As Integer

        If Not LineVIEW Then Exit Sub

        If Flag Then
            frmStart.SelectAllLinesMenuItem.Checked = True
        Else
            frmStart.SelectAllLinesMenuItem.Checked = False
        End If

        For N = 1 To NoOfLines
            If Flag Then
                If Not Lines(N).Selected Then NoOfLinesSelected = NoOfLinesSelected + 1
                SomeSelected = True
            Else
                If Lines(N).Selected Then NoOfLinesSelected = NoOfLinesSelected - 1
            End If

            For K = 1 To Lines(N).NoOfPoints
                Lines(N).GLPoints(K).Selected = False
            Next K
            Lines(N).Selected = Flag
        Next N

    End Sub

    Friend Sub SelectInvertLines()

        Dim N, K As Integer
        Dim Flag As Boolean

        If Not LineVIEW Then Exit Sub

        For N = 1 To NoOfLines
            If Lines(N).Selected Then
                NoOfLinesSelected = NoOfLinesSelected - 1
                Lines(N).Selected = False
                ' unselect points ?
                GoTo Jump_Next
            Else
                Flag = False
                For K = 1 To Lines(N).NoOfPoints
                    If Lines(N).GLPoints(K).Selected Then
                        Flag = True
                        Exit For
                    End If
                Next K
                If Flag Then GoTo Jump_Next
                NoOfLinesSelected = NoOfLinesSelected + 1
                SomeSelected = True
                Lines(N).Selected = True
            End If
Jump_Next:
        Next N

    End Sub


    Friend Sub SelectLinesInBox(ByVal X0 As Double, ByVal Y0 As Double, ByVal X1 As Double, ByVal Y1 As Double)

        Dim N As Integer

        For N = 1 To NoOfLines
            If Lines(N).ELON < X1 Then
                If Lines(N).WLON > X0 Then
                    If Lines(N).SLAT > Y1 Then
                        If Lines(N).NLAT < Y0 Then
                            If Not Lines(N).Selected Then NoOfLinesSelected = NoOfLinesSelected + 1
                            SomeSelected = True
                            Lines(N).Selected = True
                        End If
                    End If
                End If
            End If
        Next N

    End Sub

    Friend Sub DisplayLines(ByVal gr As Graphics)

        Dim X1, X0, Y0, Y1 As Double

        Dim N, K, NP As Integer

        Dim Flag As Boolean

        Dim BR As Single()
        ReDim BR(3)
        BR(0) = 0.0!
        BR(1) = 0.2!
        BR(2) = 0.8!
        BR(3) = 1.0!

        Dim PX0, PY0, PX1, PY1, L0, L1 As Integer

        Dim PointOnDisplay() As Boolean

        Dim SkipSegment As Boolean

        Dim UY, UX, U As Double
        Dim DX, DY As Integer

        Dim myPen As New System.Drawing.Pen(Color.Red)
        Dim myBrush As New System.Drawing.SolidBrush(Color.Red)

        Dim P1, P2 As Integer  ' to draw the points
        P1 = 2
        If LinePenWidth = 2 Then P1 = 3
        P2 = 2 * P1

        For N = 1 To NoOfLines

            If Not MoveON Then Lines(N).OnScreen = False

            If Lines(N).NLAT < LatDispSouth Then GoTo skip_this_one
            If Lines(N).SLAT > LatDispNorth Then GoTo skip_this_one
            If Lines(N).WLON > LonDispEast Then GoTo skip_this_one
            If Lines(N).ELON < LonDispWest Then GoTo skip_this_one

            NP = Lines(N).NoOfPoints

            Dim IsExtrusion As Boolean = False
            Dim IsObjects As Boolean = False

            myPen.Width = LinePenWidth
            myPen.DashStyle = Drawing2D.DashStyle.Solid
            If Not Lines(N).Type = Nothing Then
                If Lines(N).Type.Substring(0, 3) = "EXT" Then IsExtrusion = True
                If Lines(N).Type.Substring(0, 3) = "OBJ" Then
                    IsObjects = True
                    'myPen.DashStyle = Drawing2D.DashStyle.Dash
                    SetLenWidFromObject(N)
                End If
            End If

            Flag = False
            If Lines(N).GLPoints(1).Selected Then Flag = True ' only the first the others below

            If Lines(N).Selected Then
                myPen.Color = SelectedLineColor
                myBrush.Color = SelectedLineColor
                Flag = True
            Else
                myPen.Color = Lines(N).Color
                myBrush.Color = Lines(N).Color
            End If

            ReDim PointOnDisplay(NP)

            X1 = Lines(N).GLPoints(1).lon
            Y1 = Lines(N).GLPoints(1).lat
            PX1 = CInt((X1 - LonDispWest) * PixelsPerLonDeg)
            PY1 = CInt((LatDispNorth - Y1) * PixelsPerLatDeg)
            L1 = CInt(Lines(N).GLPoints(1).wid * PixelsPerMeter / 2)
            If L1 < LinePenWidth Then L1 = LinePenWidth

            For K = 2 To NP
                Flag = Flag Or Lines(N).GLPoints(K).Selected ' was before on another for next
                PointOnDisplay(K - 1) = False
                SkipSegment = False
                X0 = X1
                Y0 = Y1
                X1 = Lines(N).GLPoints(K).lon
                Y1 = Lines(N).GLPoints(K).lat
                If IsPtInDisplay(X0, Y0) Then
                    PointOnDisplay(K - 1) = True
                Else
                    If Not IsPtInDisplay(X1, Y1) Then
                        If Not IsSegInDisplay(X0, Y0, X1, Y1) Then
                            SkipSegment = True
                        End If
                    End If
                End If
                PX0 = PX1
                PY0 = PY1
                L0 = L1
                PX1 = CInt((X1 - LonDispWest) * PixelsPerLonDeg)
                PY1 = CInt((LatDispNorth - Y1) * PixelsPerLatDeg)
                L1 = CInt(Lines(N).GLPoints(K).wid * PixelsPerMeter / 2)
                If L1 < LinePenWidth Then L1 = LinePenWidth
                If SkipSegment Then GoTo jump_next_segment
                If (L0 = LinePenWidth And L1 = LinePenWidth) Or IsObjects Then
                    gr.DrawLine(myPen, PX0, PY0, PX1, PY1)
                ElseIf IsExtrusion Then
                    myPen.CompoundArray = BR
                    myPen.Width = L0 + L1
                    gr.DrawLine(myPen, PX0, PY0, PX1, PY1)
                Else
                    UX = CDbl(PX1 - PX0)
                    UY = CDbl(PY1 - PY0)
                    U = UX * UX + UY * UY
                    U = System.Math.Sqrt(U)
                    If U < MinValue Then U = MinValue
                    UX = UX / U
                    UY = UY / U
                    DX = CInt(L0 * UX)
                    DY = CInt(L0 * UY)
                    PTS(0).X = PX0 - DY
                    PTS(0).Y = PY0 + DX
                    PTS(1).X = PX0 + DY
                    PTS(1).Y = PY0 - DX
                    If PointOnDisplay(K - 1) Then
                        If K > 2 Then
                            gr.FillPolygon(myBrush, PTS)   ' the junction
                        End If
                    End If

                    DX = CInt(L1 * UX)
                    DY = CInt(L1 * UY)
                    PTS(2).X = PX1 + DY
                    PTS(2).Y = PY1 - DX
                    PTS(3).X = PX1 - DY
                    PTS(3).Y = PY1 + DX
                    gr.FillPolygon(myBrush, PTS)
                End If

                Lines(N).OnScreen = True
jump_next_segment:
            Next K
            ' check if last point is outside the display
            If IsPtInDisplay(Lines(N).GLPoints(NP).lon, Lines(N).GLPoints(NP).lat) Then PointOnDisplay(NP) = True

            ' now draw selected points
            If IsExtrusion Then myPen.Width = LinePenWidth

            For K = 1 To NP
                If PointOnDisplay(K) Then
                    If Flag Or LineON Then
                        If IsObjects Then
                            SetCornersFromObject(N, K)

                            If Lines(N).GLPoints(K).Selected Then
                                myBrush.Color = Color.SpringGreen
                                myPen.Color = Color.Green
                            Else
                                myBrush.Color = Color.SkyBlue
                                myPen.Color = Color.Black
                            End If

                            gr.FillRectangle(myBrush, P0X - 3, P0Y - 3, 6, 6)
                            gr.DrawRectangle(myPen, P0X - 3, P0Y - 3, 6, 6)

                            If (WID + LEN) * PixelsPerMeter > 10 Then
                                myPen.DashStyle = Drawing2D.DashStyle.Dash
                                gr.DrawLine(myPen, P1X, P1Y, P2X, P2Y)
                                gr.DrawLine(myPen, P2X, P2Y, P3X, P3Y)
                                gr.DrawLine(myPen, P3X, P3Y, P4X, P4Y)
                                gr.DrawLine(myPen, P4X, P4Y, P1X, P1Y)
                                gr.DrawLine(myPen, P0X, P0Y, HDX, HDY)

                                If LineON Then
                                    myPen.DashStyle = Drawing2D.DashStyle.Solid
                                    gr.DrawRectangle(myPen, P1X - 2, P1Y - 2, 4, 4)
                                    gr.DrawRectangle(myPen, P2X - 2, P2Y - 2, 4, 4)
                                    gr.DrawRectangle(myPen, P3X - 2, P3Y - 3, 4, 4)
                                    gr.DrawRectangle(myPen, P4X - 2, P4Y - 2, 4, 4)
                                    gr.DrawRectangle(myPen, HDX - 2, HDY - 2, 4, 4)
                                End If
                            End If

                        Else
                            PX0 = CInt((Lines(N).GLPoints(K).lon - LonDispWest) * PixelsPerLonDeg)
                            PY0 = CInt((LatDispNorth - Lines(N).GLPoints(K).lat) * PixelsPerLatDeg)
                            myPen.Color = UnselectedPointColor
                            myBrush.Color = UnselectedPointColor
                            If Lines(N).GLPoints(K).Selected Then
                                myPen.Color = SelectedPointColor
                                myBrush.Color = SelectedPointColor
                            End If
                            If K = NP Then
                                gr.DrawRectangle(myPen, PX0 - 3, PY0 - 3, 6, 6)
                            Else
                                gr.FillRectangle(myBrush, PX0 - P1, PY0 - P1, P2, P2)
                            End If
                        End If

                    End If

                End If
            Next K

skip_this_one:
        Next N


        myBrush.Dispose()
        myPen.Dispose()

    End Sub
    Private HDX As Single   ' these are for the object of a line that is being processed
    Private HDY As Single
    Private P0Y As Single
    Private P0X As Single
    Private P1Y As Single
    Private P1X As Single
    Private P2Y As Single
    Private P2X As Single
    Private P3Y As Single
    Private P3X As Single
    Private P4Y As Single
    Private P4X As Single
    Private WID As Single
    Private LEN As Single

    Private Sub SetLenWidFromObject(ByVal N As Integer)

        ' N is the line which is already knnow to be of the OBJ type
        Dim A As String

        A = Lines(N).Type.Substring(4)
        N = InStr(A, "|")
        WID = CSng(Val(A.Substring(0, N - 1))) / 2

        A = A.Substring(N)
        N = InStr(A, "|")
        LEN = CSng(Val(A.Substring(0, N - 1))) / 2

    End Sub

    Private Sub SetCornersFromObject(ByVal N As Integer, ByVal K As Integer)

        ' N is line and K is the point
        Dim C, teta, S As Single

        teta = CSng(Lines(N).GLPoints(K).wid * PI / 180)
        C = CSng(System.Math.Cos(teta) * PixelsPerMeter)
        S = CSng(System.Math.Sin(teta) * PixelsPerMeter)

        P1X = -WID : P1Y = LEN
        RotateXY(P1X, P1Y, C, S)

        P2X = WID : P2Y = LEN
        RotateXY(P2X, P2Y, C, S)

        P3X = WID : P3Y = -LEN
        RotateXY(P3X, P3Y, C, S)

        P4X = -WID : P4Y = -LEN
        RotateXY(P4X, P4Y, C, S)

        HDX = 0 : HDY = LEN
        RotateXY(HDX, HDY, C, S)

        P0X = CSng((Lines(N).GLPoints(K).lon - LonDispWest) * PixelsPerLonDeg)
        P0Y = CSng((LatDispNorth - Lines(N).GLPoints(K).lat) * PixelsPerLatDeg)

        P1X = P1X + P0X
        P2X = P2X + P0X
        P3X = P3X + P0X
        P4X = P4X + P0X
        HDX = HDX + P0X

        P1Y = -P1Y + P0Y
        P2Y = -P2Y + P0Y
        P3Y = -P3Y + P0Y
        P4Y = -P4Y + P0Y
        HDY = -HDY + P0Y

    End Sub

    Private Sub SetHeadingsFromObject(ByVal N As Integer, ByVal K As Integer)

        ' N is line and K is the point
        Dim C, teta, S As Single

        teta = CSng(Lines(N).GLPoints(K).wid * PI / 180)
        C = CSng(System.Math.Cos(teta) * PixelsPerMeter)
        S = CSng(System.Math.Sin(teta) * PixelsPerMeter)
        HDX = 0 : HDY = LEN
        RotateXY(HDX, HDY, C, S)

        P0X = CSng((Lines(N).GLPoints(K).lon - LonDispWest) * PixelsPerLonDeg)
        P0Y = CSng((LatDispNorth - Lines(N).GLPoints(K).lat) * PixelsPerLatDeg)

        HDX = HDX + P0X
        HDY = -HDY + P0Y

    End Sub
 
    Private Sub RotateXY(ByRef X As Single, ByRef Y As Single, ByVal Cos As Single, ByVal Sin As Single)

        Dim a As Single
        a = X
        X = Cos * X + Sin * Y
        Y = Cos * Y - Sin * a

    End Sub

    Friend Sub MakeBGLObjLines(ByVal CopyBGLs As Boolean)

        Dim FSNew, FSXml As Boolean
        Dim N, K, J As Integer
        Dim BGLFile1 As String = ""
        Dim BGLFile2 As String = ""
        Dim File1 As String = ""
        Dim File2 As String = ""
        Dim A, B As String
        FSNew = False
        FSXml = False
        Dim Complexity As Integer

        Dim Latitude, Longitude, Heading, Altitude As String ' used for FS9 - SCASM

        For N = 1 To NoOfLines
            If Lines(N).Selected Then
                If Mid(Lines(N).Type, 1, 3) = "OBJ" Then
                    If Mid(Lines(N).Guid, 1, 1) = "{" Then
                        FSXml = True
                    Else
                        FSNew = True
                    End If
                End If
            End If
        Next N

        If FSXml Then
            File2 = ProjectName & "_LOBX"
            File2 = Replace(File2, " ", "_")
            A = My.Application.Info.DirectoryPath & "\tools\work\" & File2 & ".xml"
            Dim settings As XmlWriterSettings = New XmlWriterSettings With {
                .Indent = True,
                .Encoding = Encoding.GetEncoding(28591),
                .NewLineOnAttributes = True
            }

            Dim writer As XmlWriter = XmlWriter.Create(a, settings)
            writer.WriteStartDocument()
            writer.WriteComment("Created by SBuilderX on " & Now)
            writer.WriteStartElement("FSData")
            writer.WriteAttributeString("version", "9.0")
            writer.WriteAttributeString("xmlns", "xsi", Nothing, "http://www.w3.org/2001/XMLSchema-instance")
            writer.WriteAttributeString("noNamespaceSchemaLocation", "http://www.w3.org/2001/XMLSchema-instance", "bglcomp.xsd")

            writer.WriteComment("FSX Line(s) of Library Objects")

            For N = 1 To NoOfLines
                If Lines(N).Selected Then
                    If Mid(Lines(N).Type, 1, 3) = "OBJ" Then
                        If Mid(Lines(N).Guid, 1, 1) = "{" Then
                            writer.WriteComment("Line_of_Objects_#" & CStr(N))
                            A = Lines(N).Type.Substring(4)
                            J = InStr(A, "|")
                            A = A.Substring(J)
                            J = InStr(A, "|")
                            A = A.Substring(J)
                            Complexity = CInt(A)
                            For K = 1 To Lines(N).NoOfPoints
                                writer.WriteStartElement("SceneryObject")
                                writer.WriteAttributeString("lat", Trim(Str(Lines(N).GLPoints(K).lat)))
                                writer.WriteAttributeString("lon", Trim(Str(Lines(N).GLPoints(K).lon)))
                                writer.WriteAttributeString("alt", Trim(Str(Lines(N).GLPoints(K).alt)))
                                writer.WriteAttributeString("altitudeIsAgl", "TRUE")
                                writer.WriteAttributeString("pitch", "0")
                                writer.WriteAttributeString("bank", "0")
                                writer.WriteAttributeString("heading", Trim(Str(Lines(N).GLPoints(K).wid)))
                                writer.WriteAttributeString("imageComplexity", GetComplex(Complexity))
                                writer.WriteStartElement("LibraryObject")
                                writer.WriteAttributeString("name", Lines(N).Guid)
                                writer.WriteAttributeString("scale", "1.0")
                                writer.WriteEndElement()
                                writer.WriteFullEndElement()
                            Next K
                        End If
                    End If
                End If
            Next N


            writer.WriteFullEndElement()
            writer.Close()

            ' delete BGL File2
            BGLFile2 = My.Application.Info.DirectoryPath & "\tools\work\" & File2 & ".BGL"
            If File.Exists(BGLFile2) Then File.Delete(BGLFile2)

            ChDrive(My.Application.Info.DirectoryPath)
            ChDir(My.Application.Info.DirectoryPath & "\tools\")

            A = My.Application.Info.DirectoryPath & "\tools\bglcomp.exe"
            B = "work\" & File2 & ".xml"

            Dim myProcess As New Process
            myProcess = Process.Start(A, B)
            myProcess.WaitForExit()
            myProcess.Dispose()

            If Not File.Exists(BGLFile2) Then
                A = "BGLComp could not produce the file" & vbCrLf & BGLFile2 & vbCrLf
                A = A & "Try to compile the file ..\tools\" & B & " in a MSDOS window" & vbCrLf
                A = A & "to read the error report!"
                MsgBox(A, MsgBoxStyle.Critical)
            End If

        End If

        If FSNew Then
            File1 = ProjectName & "_LOB1"
            File1 = Replace(File1, " ", "_")
            FileOpen(3, My.Application.Info.DirectoryPath & "\tools\work\" & File1 & ".scm", OpenMode.Output)
            A = "Header( 0x201 )"
            PrintLine(3, A)
            PrintLine(3)
            A = "; FS9 Line(s) of Library Objects"
            For N = 1 To NoOfLines
                If Lines(N).Selected Then
                    If Mid(Lines(N).Type, 1, 3) = "OBJ" Then
                        If Mid(Lines(N).Guid, 1, 1) <> "{" Then
                            ObjLibID = Lines(N).Guid
                            A = Lines(N).Type.Substring(4)
                            J = InStr(A, "|")
                            A = A.Substring(J)
                            J = InStr(A, "|")
                            A = A.Substring(J)
                            Complexity = CInt(A)
                            For K = 1 To Lines(N).NoOfPoints
                                Latitude = Format(Lines(N).GLPoints(K).lat, "0.00000000")
                                Longitude = Format(Lines(N).GLPoints(K).lon, "0.00000000")
                                Altitude = Format(Lines(N).GLPoints(K).alt, "0.000")
                                Heading = Format(Lines(N).GLPoints(K).wid, "0.000")
                                Latitude = Replace(Latitude, ",", ".")
                                Longitude = Replace(Longitude, ",", ".")
                                Altitude = Replace(Altitude, ",", ".")
                                Heading = Replace(Heading, ",", ".")
                                A = "; Line_of_Objects_#" & CStr(N) & vbCrLf
                                A = A & "LibraryObject( " & Latitude & " " & Longitude & " " & Altitude & " 1"
                                PrintLine(3, A)
                                A = "               0 0 " & Heading & " " & CStr(Complexity) & " " & FixLibID(ObjLibID) & " 1 )"
                                PrintLine(3, A)
                            Next K
                        End If
                    End If
                End If
            Next N

            PrintLine(3)

            FileClose(3)

            ' delete BGL File1
            BGLFile1 = My.Application.Info.DirectoryPath & "\tools\work\" & File1 & ".BGL"
            If File.Exists(BGLFile1) Then File.Delete(BGLFile1)

            ChDrive(My.Application.Info.DirectoryPath)
            ChDir(My.Application.Info.DirectoryPath & "\tools\")

            A = "scasm work\" & File1 & ".scm -s -l"
            N = ExecCmd(A)

            If N > 0 Then
                A = "There was a compilation error in this project!" & vbCrLf
                A = A & "Do you want to read a SCASM report?"
                N = MsgBox(A, MsgBoxStyle.OkCancel)
                If N = 1 Then
                    A = "notepad SCAERROR.LOG"
                    N = ExecCmd(A)
                End If
                Exit Sub
            End If
        End If

        If Not CopyBGLs Then Exit Sub

        Dim BGLFileTarget As String
        ' copy BGL files
        Try
            If FSNew Then
                BGLFileTarget = BGLProjectFolder & "\" & File1 & ".BGL"
                If File.Exists(BGLFile1) Then File.Copy(BGLFile1, BGLFileTarget, True)
            End If
            If FSXml Then
                BGLFileTarget = BGLProjectFolder & "\" & File2 & ".BGL"
                If File.Exists(BGLFile2) Then File.Copy(BGLFile2, BGLFileTarget, True)
            End If
        Catch ex As Exception
            MsgBox("Copying BGL files failed! Try to close FSX.", MsgBoxStyle.Information)
        End Try

    End Sub

    Private Function FixLibID(ByVal ID As String) As String

        FixLibID = UCase(Mid(ID, 1, 8)) & " "
        FixLibID = FixLibID & UCase(Mid(ID, 9, 8)) & " "
        FixLibID = FixLibID & UCase(Mid(ID, 17, 8)) & " "
        FixLibID = FixLibID & UCase(Mid(ID, 25, 8))

    End Function

    Friend Sub MakeBGLExtrusions(ByVal CopyBGLs As Boolean)

        Dim N, K As Integer

        Dim g As Guid

        Dim lat0 As String = ""
        Dim lon0 As String = ""
        Dim alt0 As String = ""
        Dim latN As String = ""
        Dim lonN As String = ""
        Dim altN As String = ""

        Dim myFile As String = ProjectName & "_EXT"
        myFile = Replace(myFile, " ", "_")
        Dim a As String = My.Application.Info.DirectoryPath & "\tools\work\" & myFile & ".xml"

        Dim settings As XmlWriterSettings = New XmlWriterSettings With {
            .Indent = True,
            .Encoding = Encoding.GetEncoding(28591),
            .NewLineOnAttributes = True
        }

        Dim writer As XmlWriter = XmlWriter.Create(A, settings)
        writer.WriteStartDocument()
        writer.WriteComment("Created by SBuilderX on " & Now)
        writer.WriteStartElement("FSData")
        writer.WriteAttributeString("version", "9.0")
        writer.WriteAttributeString("xmlns", "xsi", Nothing, "http://www.w3.org/2001/XMLSchema-instance")
        writer.WriteAttributeString("noNamespaceSchemaLocation", "http://www.w3.org/2001/XMLSchema-instance", "bglcomp.xsd")

        writer.WriteComment("FSX Extrusion Bridges")
        For N = 1 To NoOfLines
            If Lines(N).Selected Then
                If Mid(Lines(N).Type, 1, 3) = "EXT" Then
                    GetExtrusionLineParameters(N)
                    writer.WriteComment("Extrusion Line " & CStr(N))
                    writer.WriteStartElement("ExtrusionBridge")
                    g = Guid.NewGuid
                    writer.WriteAttributeString("instanceId", g.ToString("B"))
                    writer.WriteAttributeString("imageComplexity", GetComplex(ExtrusionComplexity))
                    writer.WriteAttributeString("probability", Trim(Str(ExtrusionProbability)))
                    writer.WriteAttributeString("suppressPlatform", Trim(Str(SuppressPlatform).ToUpper))
                    writer.WriteAttributeString("roadWidth", Trim(Str(ExtrusionWidth)))
                    writer.WriteAttributeString("extrusionProfile", Lines(N).Guid)
                    writer.WriteAttributeString("materialSet", MaterialGuid)

                    writer.WriteStartElement("AltitudeSampleLocationList")
                    writer.WriteStartElement("AltitudeSampleLocation")
                    writer.WriteAttributeString("lat", Trim(Str(Lines(N).GLPoints(1).lat)))
                    writer.WriteAttributeString("lon", Trim(Str(Lines(N).GLPoints(1).lon)))
                    writer.WriteEndElement()
                    writer.WriteStartElement("AltitudeSampleLocation")
                    writer.WriteAttributeString("lat", Trim(Str(Lines(N).GLPoints(Lines(N).NoOfPoints).lat)))
                    writer.WriteAttributeString("lon", Trim(Str(Lines(N).GLPoints(Lines(N).NoOfPoints).lon)))
                    writer.WriteEndElement()
                    writer.WriteFullEndElement()

                    writer.WriteStartElement("PolylinePointList")

                    SetExtrusionExtremes(N, lat0, lon0, latN, lonN)

                    writer.WriteStartElement("PolylinePoint")
                    writer.WriteAttributeString("latitude", lat0)
                    writer.WriteAttributeString("longitude", lon0)
                    writer.WriteAttributeString("altitude", ExtraExtrusionAltitude)
                    writer.WriteEndElement()

                    For K = 1 To Lines(N).NoOfPoints
                        writer.WriteStartElement("PolylinePoint")
                        writer.WriteAttributeString("latitude", Trim(Str(Lines(N).GLPoints(K).lat)))
                        writer.WriteAttributeString("longitude", Trim(Str(Lines(N).GLPoints(K).lon)))
                        writer.WriteAttributeString("altitude", Trim(Str(Lines(N).GLPoints(K).alt)) & "M")
                        writer.WriteEndElement()
                    Next

                    writer.WriteStartElement("PolylinePoint")
                    writer.WriteAttributeString("latitude", latN)
                    writer.WriteAttributeString("longitude", lonN)
                    writer.WriteAttributeString("altitude", ExtraExtrusionAltitude)
                    writer.WriteEndElement()

                    writer.WriteFullEndElement()

                    writer.WriteStartElement("PolylineObjectPlacementList")
                    writer.WriteStartElement("PolylineObjectPlacement")
                    writer.WriteAttributeString("id", PylonGuid)
                    writer.WriteEndElement()
                    writer.WriteFullEndElement()

                    writer.WriteFullEndElement()  ' ExtrusionBridge
                End If
            End If
        Next

        writer.WriteFullEndElement() ' FSData
        writer.Close()

        ' delete BGL File
        Dim BGLFile As String = My.Application.Info.DirectoryPath & "\tools\work\" & myFile & ".BGL"
        If File.Exists(BGLFile) Then File.Delete(BGLFile)

        ChDrive(My.Application.Info.DirectoryPath)
        ChDir(My.Application.Info.DirectoryPath & "\tools\")

        a = My.Application.Info.DirectoryPath & "\tools\bglcomp.exe"
        Dim b As String = "work\" & myFile & ".xml"

        Dim myProcess As New Process
        myProcess = Process.Start(a, b)
        myProcess.WaitForExit()
        myProcess.Dispose()

        If Not File.Exists(BGLFile) Then
            a = "BGLComp could not produce the file" & vbCrLf & BGLFile & vbCrLf
            a = a & "Try to compile the file ..\tools\" & b & " in a MSDOS window" & vbCrLf
            a = a & "to read the error report!"
            MsgBox(a, MsgBoxStyle.Critical)
        End If

        If Not CopyBGLs Then Exit Sub

        Dim BGLFileTarget As String
        ' copy BGL files
        Try
            BGLFileTarget = BGLProjectFolder & "\" & myFile & ".BGL"
            If File.Exists(BGLFile) Then File.Copy(BGLFile, BGLFileTarget, True)
        Catch ex As Exception
            MsgBox("Copying BGL files failed!", MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Private Sub SetExtrusionExtremes(ByVal N As Integer, _
                                    ByRef lat0 As String, ByRef lon0 As String, _
                                    ByRef latN As String, ByRef lonN As String)

        Dim D As Double
        Dim NP As Integer = Lines(N).NoOfPoints

        D = Lines(N).GLPoints(2).lat - Lines(N).GLPoints(1).lat
        lat0 = Str(Lines(N).GLPoints(1).lat - D / 2)
        D = Lines(N).GLPoints(2).lon - Lines(N).GLPoints(1).lon
        lon0 = Str(Lines(N).GLPoints(1).lon - D / 2)

        D = Lines(N).GLPoints(NP - 1).lat - Lines(N).GLPoints(NP).lat
        latN = Str(Lines(N).GLPoints(NP).lat - D / 2)
        D = Lines(N).GLPoints(NP - 1).lon - Lines(N).GLPoints(NP).lon
        lonN = Str(Lines(N).GLPoints(NP).lon - D / 2)

    End Sub

    Private MaterialGuid As String
    Private PylonGuid As String
    Private ExtrusionComplexity As Integer
    Private ExtrusionWidth As Double
    Private ExtrusionProbability As Double
    Private SuppressPlatform As Boolean

    Private Sub GetExtrusionLineParameters(ByVal N As Integer)

        Dim A As String
        Dim J As Integer

        A = Lines(N).Type.Substring(4)
        J = InStr(A, "|")
        MaterialGuid = A.Substring(0, J - 1)

        A = A.Substring(J)
        J = InStr(A, "|")
        PylonGuid = A.Substring(0, J - 1)

        A = A.Substring(J)
        J = InStr(A, "|")
        ExtrusionComplexity = CInt(A.Substring(0, J - 1))

        A = A.Substring(J)
        J = InStr(A, "|")
        ExtrusionWidth = Val(A.Substring(0, J - 1))

        A = A.Substring(J)
        J = InStr(A, "|")
        ExtrusionProbability = Val(A.Substring(0, J - 1))

        A = A.Substring(J)
        SuppressPlatform = CBool(A)

    End Sub

    Private Function GetComplex(ByVal X As Integer) As String

        GetComplex = ""
        If X = 0 Then GetComplex = "VERY_SPARSE"
        If X = 1 Then GetComplex = "SPARSE"
        If X = 2 Then GetComplex = "NORMAL"
        If X = 3 Then GetComplex = "DENSE"
        If X = 4 Then GetComplex = "VERY_DENSE"
        If X = 5 Then GetComplex = "EXTREMELY_DENSE"

    End Function



    Friend Sub MakePolyFromLine(ByVal N As Integer)

        Dim P, M As Integer
        Dim UX, UY, U, K As Double


        Dim NP As Integer = Lines(N).NoOfPoints
        Dim Line(NP) As Double_XY  ' line in meter coordinates

        For P = 1 To NP
            Line(P).X = Lines(N).GLPoints(P).lon * MetersPerDegLon(LatDispCenter)
            Line(P).Y = Lines(N).GLPoints(P).lat * MetersPerDegLat
        Next

        Dim D1(NP) As Double_XY
        Dim D2(NP) As Double_XY

        Dim W As Double
        For P = 1 To NP - 1
            UX = Line(P + 1).X - Line(P).X
            UY = Line(P + 1).Y - Line(P).Y
            U = UX * UX + UY * UY
            U = System.Math.Sqrt(U)
            If U < MinValue Then U = MinValue
            UX = UX / U
            UY = UY / U
            W = Lines(N).GLPoints(P).wid / 2
            D2(P).X = UX * W
            D2(P).Y = UY * W
            W = Lines(N).GLPoints(P + 1).wid / 2
            D1(P + 1).X = UX * W
            D1(P + 1).Y = UY * W
        Next

        Dim PR1(NP) As Double_XY   ' right side
        Dim PR2(NP) As Double_XY
        Dim PR(NP) As Double_XY
        Dim PL1(NP) As Double_XY   ' left side
        Dim PL2(NP) As Double_XY
        Dim PL(NP) As Double_XY

        PL(1).X = Line(1).X - D2(1).Y
        PL(1).Y = Line(1).Y + D2(1).X
        PR(1).X = Line(1).X + D2(1).Y
        PR(1).Y = Line(1).Y - D2(1).X

        PL(NP).X = Line(NP).X - D1(NP).Y
        PL(NP).Y = Line(NP).Y + D1(NP).X
        PR(NP).X = Line(NP).X + D1(NP).Y
        PR(NP).Y = Line(NP).Y - D1(NP).X

        For P = 2 To NP - 1
            PL1(P).X = Line(P).X - D1(P).Y
            PL1(P).Y = Line(P).Y + D1(P).X
            PR1(P).X = Line(P).X + D1(P).Y
            PR1(P).Y = Line(P).Y - D1(P).X

            PL2(P).X = Line(P).X - D2(P).Y
            PL2(P).Y = Line(P).Y + D2(P).X
            PR2(P).X = Line(P).X + D2(P).Y
            PR2(P).Y = Line(P).Y - D2(P).X

            K = PL2(P).X * D1(P).Y
            K = K - PL1(P).X * D1(P).Y
            K = K + PL1(P).Y * D1(P).X
            K = K - PL2(P).Y * D1(P).X
            U = D2(P).Y * D1(P).X - D2(P).X * D1(P).Y
            If Not K = 0 Then K = K / U
            PL(P).X = PL2(P).X + K * D2(P).X
            PL(P).Y = PL2(P).Y + K * D2(P).Y

            K = PR2(P).X * D1(P).Y
            K = K - PR1(P).X * D1(P).Y
            K = K + PR1(P).Y * D1(P).X
            K = K - PR2(P).Y * D1(P).X
            U = D2(P).Y * D1(P).X - D2(P).X * D1(P).Y
            If Not K = 0 Then K = K / U
            PR(P).X = PR2(P).X + K * D2(P).X
            PR(P).Y = PR2(P).Y + K * D2(P).Y

        Next

        NoOfPolys = NoOfPolys + 1
        ReDim Preserve Polys(NoOfPolys)
        Polys(NoOfPolys).NoOfPoints = 2 * NP
        Polys(NoOfPolys).Name = Str(Polys(NoOfPolys).NoOfPoints) & "_Pts_Polygon_of_Type_None"
        Polys(NoOfPolys).Guid = DefaultPolyNoneGuid
        Polys(NoOfPolys).NoOfChilds = 0
        Polys(NoOfPolys).Color = DefaultPolyColor

        ReDim Polys(NoOfPolys).GPoints(2 * NP)

        M = 1
        For P = 1 To NP
            Polys(NoOfPolys).GPoints(M).lat = PL(P).Y / MetersPerDegLat
            Polys(NoOfPolys).GPoints(M).lon = PL(P).X / MetersPerDegLon(LatDispCenter)
            Polys(NoOfPolys).GPoints(M).alt = Lines(N).GLPoints(P).alt
            M = M + 1
        Next

        For P = NP To 1 Step -1
            Polys(NoOfPolys).GPoints(M).lat = PR(P).Y / MetersPerDegLat
            Polys(NoOfPolys).GPoints(M).lon = PR(P).X / MetersPerDegLon(LatDispCenter)
            Polys(NoOfPolys).GPoints(M).alt = Lines(N).GLPoints(P).alt
            M = M + 1
        Next

        AddLatLonToPoly(NoOfPolys)
        Dirty = True

    End Sub

    Friend Sub DeleteSelectedLines()

        Dim N, L As Integer

        If NoOfLinesSelected < 5 Then

            For N = NoOfLines To 1 Step -1
                If Lines(N).Selected Then DeleteLine(N)
            Next N

        Else

            N = 0
            L = 0
            Do
                N = N + 1
                If N > NoOfLines Then Exit Do
                If Not Lines(N).Selected Then
                    L = L + 1
                    Lines(L) = Lines(N)
                End If
            Loop


            If L > 0 Then
                ReDim Preserve Lines(L)
            Else
                ReDim Preserve Lines(1)
            End If
            NoOfLines = L

        End If

    End Sub


    Friend Sub DeleteSelectedPointsInLines()

        Dim N, K As Integer

        For N = NoOfLines To 1 Step -1
            For K = Lines(N).NoOfPoints To 1 Step -1
                If Lines(N).GLPoints(K).Selected Then DeletePointInLine(N, K)
            Next K
        Next N

    End Sub

    Friend Sub DeletePointInLine(ByVal Ln As Integer, ByVal PT As Integer)

        Dim p As Integer

        If Lines(Ln).NoOfPoints < 3 Then
            DeleteLine(Ln)
            Dirty = True
            Exit Sub
        End If

        If Not SkipBackUp Then BackUp()

        If PT < Lines(Ln).NoOfPoints Then
            For p = PT To Lines(Ln).NoOfPoints - 1
                Lines(Ln).GLPoints(p).lat = Lines(Ln).GLPoints(p + 1).lat
                Lines(Ln).GLPoints(p).lon = Lines(Ln).GLPoints(p + 1).lon
                Lines(Ln).GLPoints(p).alt = Lines(Ln).GLPoints(p + 1).alt
                Lines(Ln).GLPoints(p).wid = Lines(Ln).GLPoints(p + 1).wid
                Lines(Ln).GLPoints(p).Selected = Lines(Ln).GLPoints(p + 1).Selected
            Next p
        End If

        ReDim Preserve Lines(Ln).GLPoints(Lines(Ln).NoOfPoints - 1)
        Lines(Ln).NoOfPoints = Lines(Ln).NoOfPoints - 1
        Dirty = True

    End Sub

    Friend Sub DeleteLine(ByVal N As Integer)

        Dim K As Integer
        If Lines(N).Selected Then NoOfLinesSelected = NoOfLinesSelected - 1

        If Not SkipBackUp Then BackUp()

        If N < NoOfLines Then
            For K = N To NoOfLines - 1
                Lines(K) = Lines(K + 1)
            Next K
        End If

        If NoOfLines > 1 Then
            ReDim Preserve Lines(NoOfLines - 1)
        End If

        NoOfLines = NoOfLines - 1
        Dirty = True

    End Sub
    Friend Function IsInsertPointInLine(ByVal X1 As Integer, ByVal Y1 As Integer) As Boolean

        'returns true if point was inserted
        'on entry X1 Y1 contain distance from the origin of display in pixels

        Dim retval As Boolean
        Dim N, K As Integer
        Dim X, Y As Double

        IsInsertPointInLine = False
        If InsertON = False Then Exit Function
        If Not LineVIEW Then Exit Function

        X = LonDispWest * PixelsPerLonDeg + X1
        Y = LatDispNorth * PixelsPerLatDeg - Y1

        ' check if we are over a Line
        For N = 1 To NoOfLines
            K = 2
            Do Until retval = True Or K = Lines(N).NoOfPoints + 1
                retval = retval Or IsPointInSegment(Lines(N).GLPoints(K - 1).lon, Lines(N).GLPoints(K - 1).lat, Lines(N).GLPoints(K).lon, Lines(N).GLPoints(K).lat, X, Y)
                K = K + 1
            Loop
            If retval Then
                IsInsertPointInLine = True
                InsertPointInLine(X, Y, N, K - 1)
                Exit Function
            End If
        Next N
    End Function

    Friend Function InsertPointInLine(ByVal X As Double, ByVal Y As Double, ByVal Line As Integer, ByVal Point As Integer) As Integer

        ' returns 0 if insert fails or 1 if it inserts

        Dim N, K As Integer
        Dim X1, Y1 As Double

        X1 = X / PixelsPerLonDeg
        Y1 = Y / PixelsPerLatDeg

        InsertPointInLine = 0

        N = Lines(Line).NoOfPoints
        For K = 1 To N
            If System.Math.Abs(Lines(Line).GLPoints(K).lat - Y1) < D22Lat Then
                If System.Math.Abs(Lines(Line).GLPoints(K).lon - X1) < D22Lon Then
                    Exit Function
                End If
            End If
        Next K
        If BackUpON Then BackUp()

        N = Lines(Line).NoOfPoints + 1
        Lines(Line).NoOfPoints = N

        ReDim Preserve Lines(Line).GLPoints(N)

        For K = N - 1 To Point Step -1
            Lines(Line).GLPoints(K + 1).lat = Lines(Line).GLPoints(K).lat
            Lines(Line).GLPoints(K + 1).lon = Lines(Line).GLPoints(K).lon
            Lines(Line).GLPoints(K + 1).alt = Lines(Line).GLPoints(K).alt
            Lines(Line).GLPoints(K + 1).wid = Lines(Line).GLPoints(K).wid
        Next K

        Lines(Line).GLPoints(Point).lat = Y1
        Lines(Line).GLPoints(Point).lon = X1
        'Lines(Line).GLPoints(Point).alt = 0.5 * (Lines(Line).GLPoints(Point - 1).alt + Lines(Line).GLPoints(Point + 1).alt)
        'Lines(Line).GLPoints(Point).wid = 0.5 * (Lines(Line).GLPoints(Point - 1).wid + Lines(Line).GLPoints(Point + 1).wid)

        Dim Z, W As Double
        ZWFromXYZ(Z, W, X1, Y1, Line, Point - 1, Point + 1)
        Lines(Line).GLPoints(Point).alt = Z
        Lines(Line).GLPoints(Point).wid = W

        Lines(Line).GLPoints(Point).Selected = True

        InsertPointInLine = 1

    End Function

    Friend Function IsPointInLine(ByVal X1 As Integer, ByVal Y1 As Integer) As Boolean

        'THIS HAS CHANGED - on entry X Y contain distance from center display in pixels
        ' NOW Sept2006 - on entry X Y contain distance from the origin of the display in pixels

        Dim N, K As Integer
        Dim retval As Boolean
        Dim WLON, NLAT, SLAT, ELON As Double
        Dim X, Y As Double

        IsPointInLine = False
        If Not LineVIEW Then Exit Function

        WLON = LonDispWest + (X1 + 5) / PixelsPerLonDeg
        ELON = LonDispWest + (X1 - 5) / PixelsPerLonDeg
        NLAT = LatDispNorth - (Y1 + 5) / PixelsPerLatDeg
        SLAT = LatDispNorth - (Y1 - 5) / PixelsPerLatDeg

        X = LonDispWest * PixelsPerLonDeg + X1  ' longitude in pixels
        Y = LatDispNorth * PixelsPerLatDeg - Y1 ' latitude in pixels

        For N = 1 To NoOfLines

            If Lines(N).Selected Then GoTo Jump_Next ' mantain??? if line is selected the points aare not selected

            If WLON < Lines(N).WLON Then GoTo Jump_Next
            If ELON > Lines(N).ELON Then GoTo Jump_Next
            If SLAT < Lines(N).SLAT Then GoTo Jump_Next
            If NLAT > Lines(N).NLAT Then GoTo Jump_Next

            K = 1
            retval = False
            Do Until retval = True Or K = Lines(N).NoOfPoints + 1
                retval = retval Or IsPoint(Lines(N).GLPoints(K).lon, Lines(N).GLPoints(K).lat, X, Y)
                K = K + 1
            Loop
            'If retval Then
            '    If Lines(N).Selected = False Then NoOfLinesSelected = NoOfLinesSelected + 1
            '    Lines(N).GLPoints(K - 1).Selected = True
            '    Lines(N).Selected = False
            '    IsPointInLine = True
            '    Exit Function
            'End If
            ' mudado em 28 de setembro1006

            If retval Then
                If Lines(N).GLPoints(K - 1).Selected = False Then NoOfPointsSelected = NoOfPointsSelected + 1
                Lines(N).GLPoints(K - 1).Selected = True
                IsPointInLine = True
                Exit Function
            End If

Jump_Next:
        Next N

    End Function

    Friend Function IsLineSelected(ByVal X As Double, ByVal Y As Double) As Boolean

        ' on entry X Y contain distance from center of earth in pixels

        Dim N, K As Integer
        Dim retval As Boolean
        Dim WLON, NLAT, SLAT, ELON As Double

        IsLineSelected = False
        If Not LineVIEW Then Exit Function

        WLON = (X + 5) / PixelsPerLonDeg
        ELON = (X - 5) / PixelsPerLonDeg
        NLAT = (Y - 5) / PixelsPerLatDeg
        SLAT = (Y + 5) / PixelsPerLatDeg

        For N = 1 To NoOfLines

            retval = False

            If Lines(N).Selected Then GoTo Jump_Next
            If WLON < Lines(N).WLON Then GoTo Jump_Next
            If ELON > Lines(N).ELON Then GoTo Jump_Next
            If SLAT < Lines(N).SLAT Then GoTo Jump_Next
            If NLAT > Lines(N).NLAT Then GoTo Jump_Next

            K = 2
            Do Until retval = True Or K = Lines(N).NoOfPoints + 1
                retval = retval Or IsPointInSegment(Lines(N).GLPoints(K - 1).lon, Lines(N).GLPoints(K - 1).lat, Lines(N).GLPoints(K).lon, Lines(N).GLPoints(K).lat, X, Y)
                K = K + 1
            Loop
            If retval Then
                If Lines(N).Selected = False Then NoOfLinesSelected = NoOfLinesSelected + 1
                Lines(N).Selected = True
                For K = 1 To Lines(N).NoOfPoints
                    If Lines(N).GLPoints(K).Selected Then NoOfPointsSelected = NoOfPointsSelected - 1
                    Lines(N).GLPoints(K).Selected = False
                Next K
                IsLineSelected = True
                SomeSelected = True
                Exit Function
            End If
Jump_Next:
        Next N

    End Function

    Friend Sub AddLatLonToLine(ByVal N As Integer)

        Dim K As Integer
        Dim EL, SL, NL, WL, x As Double

        NL = -90 : SL = 90 : EL = -180 : WL = 180

        For K = 1 To Lines(N).NoOfPoints
            x = Lines(N).GLPoints(K).lat
            If x < SL Then SL = x
            If x > NL Then NL = x
            x = Lines(N).GLPoints(K).lon
            If x > EL Then EL = x
            If x < WL Then WL = x
        Next K
        Lines(N).ELON = EL
        Lines(N).WLON = WL
        Lines(N).NLAT = NL
        Lines(N).SLAT = SL

    End Sub


    Friend Sub MoveSelectedLines(ByVal X As Double, ByVal Y As Double)

        Dim N, K As Integer
        Dim Flag As Boolean

        For N = 1 To NoOfLines
            If Lines(N).OnScreen Then
                If Lines(N).Selected Then
                    For K = 1 To Lines(N).NoOfPoints
                        Lines(N).GLPoints(K).lat = Lines(N).GLPoints(K).lat - Y
                        Lines(N).GLPoints(K).lon = Lines(N).GLPoints(K).lon + X
                    Next K
                    AddLatLonToLine(N)
                Else 'If Not LineON Then ' ??? that was not permitting point movements in Line Mode
                    Flag = False
                    For K = 1 To Lines(N).NoOfPoints
                        If Lines(N).GLPoints(K).Selected Then
                            Lines(N).GLPoints(K).lat = Lines(N).GLPoints(K).lat - Y
                            Lines(N).GLPoints(K).lon = Lines(N).GLPoints(K).lon + X
                            Flag = True
                        End If
                    Next K
                    If Flag Then AddLatLonToLine(N)
                End If
            End If
        Next N
        Dirty = True

    End Sub

    Friend Sub CheckLineMove(ByVal UP As Boolean)

        Dim L As Integer

        Lines(CheckLine).GLPoints(CheckLinePt).Selected = False

        If UP Then
            L = CheckLine + 1
            If L = NoOfLines + 1 Then L = 1
        Else
            L = CheckLine - 1
            If L = 0 Then L = NoOfLines
        End If

        CheckLine = L
        CheckLinePt = 1

        RefreshCheckLinePt()

    End Sub

    Friend Sub CheckLinePtMove(ByVal UP As Boolean)

        Dim NP, p As Integer

        NP = Lines(CheckLine).NoOfPoints

        Lines(CheckLine).GLPoints(CheckLinePt).Selected = False

        If UP Then
            p = CheckLinePt + 1
            If p > NP Then p = 1
        Else
            p = CheckLinePt - 1
            If p = 0 Then p = NP
        End If

        CheckLinePt = p
        RefreshCheckLinePt()

    End Sub

    Friend Sub DeleteCheckLinePt()

        If Lines(CheckLine).NoOfPoints < 3 Then
            CheckLine = 0
            Exit Sub
        End If

        DeletePointInLine(CheckLine, CheckLinePt)

        If CheckLinePt > Lines(CheckLine).NoOfPoints Then
            CheckLinePt = Lines(CheckLine).NoOfPoints
        End If

        RefreshCheckLinePt()

    End Sub

    Private Sub RefreshCheckLinePt()

        Lines(CheckLine).GLPoints(CheckLinePt).Selected = True
        LatDispCenter = Lines(CheckLine).GLPoints(CheckLinePt).lat
        LonDispCenter = Lines(CheckLine).GLPoints(CheckLinePt).lon
        SetDispCenter(0, 0)
        RebuildDisplay()

    End Sub


    Friend Function IsMouseOnLine(ByVal X1 As Integer, ByVal Y1 As Integer) As Integer

        ' returns the first line that has the mouse over it! if none returns 0
        ' on entry X Y contains mouse distance from origin of display in pixels

        Dim N, K As Integer
        Dim retval As Boolean
        Dim X, Y As Double

        IsMouseOnLine = 0
        If LineVIEW = False Then Exit Function

        X = LonDispWest * PixelsPerLonDeg + X1
        Y = LatDispNorth * PixelsPerLatDeg - Y1

        For N = 1 To NoOfLines
            If Lines(N).OnScreen = False Then GoTo Jump_Next
            K = 2
            retval = False
            Do Until retval = True Or K = Lines(N).NoOfPoints + 1
                retval = retval Or IsPointInSegment(Lines(N).GLPoints(K - 1).lon, Lines(N).GLPoints(K - 1).lat, Lines(N).GLPoints(K).lon, Lines(N).GLPoints(K).lat, X, Y)
                K = K + 1
            Loop
            If retval Then
                IsMouseOnLine = N
                Exit Function
            End If
Jump_Next:
        Next N

    End Function

    Private Sub ReverseLine(ByVal N As Integer)

        Dim x As GLPoint
        Dim M, NP, K As Integer

        NP = Lines(N).NoOfPoints
        M = CInt(Int(NP / 2))

        NP = NP + 1
        For K = 1 To M
            x = Lines(N).GLPoints(NP - K)
            Lines(N).GLPoints(NP - K) = Lines(N).GLPoints(K)
            Lines(N).GLPoints(K) = x
        Next K

    End Sub

    Private Sub ZWFromXYZ(ByRef Z As Double, ByRef W As Double, _
                         ByVal x As Double, ByVal y As Double, _
                         ByVal L As Integer, ByVal P0 As Integer, ByVal P1 As Integer)

        Dim x0, y0, z0, w0, x1, y1, z1, w1, dx, dy As Double

        x0 = Lines(L).GLPoints(P0).lon
        y0 = Lines(L).GLPoints(P0).lat
        z0 = Lines(L).GLPoints(P0).alt
        w0 = Lines(L).GLPoints(P0).wid

        x1 = Lines(L).GLPoints(P1).lon
        y1 = Lines(L).GLPoints(P1).lat
        z1 = Lines(L).GLPoints(P1).alt
        w1 = Lines(L).GLPoints(P1).wid

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



End Module