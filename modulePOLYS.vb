Option Strict On
Option Explicit On

Imports System.xml
Imports System.text
Imports VB = Microsoft.VisualBasic

Module modulePOLYS

    Friend PtPolyCounter As Integer
    Friend AuxLatPoly As Double ' also used in make bgl tex polys
    Friend AuxLonPoly As Double ' also used in make bgl tex polys
    Private AuxZPoly As Double ' used in make bgl tex polys

    'Private Const DJoinLon As Double = 360# / 768# / 32# / 256# / 10#
    'Private Const DJoinLat  As Double = 180# / 512# / 32# / 256# / 10#

    <Serializable()> Friend Structure GPoly
        Dim Name As String
        Dim Type As String
        Dim Guid As String
        Dim Color As Color
        Dim Selected As Boolean
        Dim NoOfChilds As Integer
        Dim Childs() As Integer
        Dim NoOfPoints As Integer
        Dim GPoints() As GPoint
        Dim NLAT As Double ' not saved
        Dim SLAT As Double ' not saved
        Dim WLON As Double ' not saved
        Dim ELON As Double ' not saved
        Dim OnScreen As Boolean
    End Structure

    Friend NoOfPolys As Integer = 0
    Friend NoOfPolysSelected As Integer = 0

    Friend Structure PolyType
        Dim Name As String
        Dim Type As String
        Dim Color As Color
        Dim Guid As String
        Dim Texture As String
        Dim TerrainIndex As Integer
    End Structure

    Friend PolyTypes() As PolyType
    Friend NoOfPolyTypes As Integer

    Friend PTS() As Point

    Friend Polys() As GPoly
    Friend NewPoly As GPoly

    Friend DefaultPolyAltitude As Double

    Friend ParticularExcludeGUID As String

    ' these are read from Polys.TXT
    Friend DefaultPolyNoneGuid As String
    Friend DefaultPolyFS9Guid As String
    Friend DefaultPolyGPSGuid As String

    'Friend PolyTexPath As String
    Friend PolyTex As String
    Friend PolyTexIndex As Integer
    Friend PolyTexString As String

    '  Private OtherN1 As Integer
    '  Private OtherN2 As Integer
    '  Private OtherN3 As Integer
    '  Private OtherN4 As Integer

    'Friend HideOtherPolys As Boolean

    Friend MakeClosedLineFromPoly As Boolean

    Friend CheckPoly As Integer
    Friend CheckPolyPt As Integer

    Friend PolyON As Boolean
    Friend PolyVIEW As Boolean
    Friend PolyFILL As Boolean

    Friend DefaultPolyColor As Color
    Friend PolyColorBorder As Color
    Friend PolyPenWidth As Integer

    'Friend PolyTexPath As String


    'Friend PolyFlattenColor As Integer
    'Friend PolyWaterColor As Integer
    'Friend PolyLandColor As Integer
    'Friend PolyExcludeColor As Integer

    Friend Sub PolyInsertMode(ByRef Button As Short, ByRef Shift As Short, ByVal X As Integer, ByVal Y As Integer)

        If Button = 1 Then

            If IsCenterDisplay(X, Y) Then
                X = X - DisplayCenterX
                Y = Y - DisplayCenterY
                SetDispCenter(X, Y)
                X = DisplayCenterX
                Y = DisplayCenterY
                RebuildDisplay()
            End If

            If PtPolyCounter > 0 Then   ' start Poly creation begun earlier
                BuildPoly(X, Y)
            Else     ' the first click
                frmStart.CopyMenuItem.Enabled = False
                frmStart.DeleteMenuItem.Enabled = False
                ' SHIFT IS DOWN!
                If Shift = 1 Then
                    If IsPointInPoly(X, Y) Then  ' did we add a point to the selection
                        SomeSelected = True
                        RebuildDisplay()    ' start the movement of the point and the rest
                        SetDelay(200)
                        MoveON = True
                        FirstMOVE = True
                        AuxXInt = X
                        AuxYInt = Y
                        Exit Sub
                    End If  ' did we add a poly to the selection
                    If IsPolySelected(LonDispWest * PixelsPerLonDeg + X, LatDispNorth * PixelsPerLatDeg - Y) Then
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
                SomeSelected = SomeSelected Or IsPointInPoly(X, Y)
                If SomeSelected Then  ' was a point selected?
                    If DeleteON Then
                        DeleteSelectedPointsInPolys()
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
                Else  ' is InsertON true and we clicked on a segment of a Poly?
                    SomeSelected = SomeSelected Or IsInsertPointInPoly(X, Y)
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
                    Else                    ' did we selected a Poly?
                        SomeSelected = SomeSelected Or IsPolySelected(LonDispWest * PixelsPerLonDeg + X, LatDispNorth * PixelsPerLatDeg - Y)
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
                        Else   'no!!! so this is the start of the creation of a new Poly
                            BackUp()
                            RebuildDisplay()
                            BuildPoly(X, Y)
                        End If
                    End If
                End If
            End If
        End If

        If Button = 2 Then
            EndPoly(X, Y)
            RebuildDisplay()
        End If

    End Sub


    Friend Sub BuildPoly(ByVal X As Integer, ByRef Y As Integer)

        ' this looks to the number of clicks (in PtPolyCounter)
        ' if PtPolyCounter = 0 it is the first click
        ' if PtPolyCounter => 2 a new point is added to PTS()

        Dim Lat, Lon As Double

        Lat = LatDispNorth - CDbl(Y / PixelsPerLatDeg)
        Lon = LonDispWest + CDbl(X / PixelsPerLonDeg)

        If PtPolyCounter = 0 Then
            SelectAllPolys(False)
            NoOfPolysSelected = 0
            AuxLatPoly = Lat
            AuxLonPoly = Lon
            PtPolyCounter = 2
            Exit Sub
        End If

        If PtPolyCounter = 2 Then

            PtPolyCounter = 3

            ReDim NewPoly.GPoints(2)

            NewPoly.NoOfPoints = 2
            NewPoly.Color = DefaultPolyColor
            NewPoly.Selected = False

            NewPoly.GPoints(1).lat = AuxLatPoly
            NewPoly.GPoints(1).lon = AuxLonPoly
            NewPoly.GPoints(1).alt = DefaultPolyAltitude
            NewPoly.GPoints(1).Selected = False


            NewPoly.GPoints(2).lat = Lat
            NewPoly.GPoints(2).lon = Lon
            NewPoly.GPoints(2).alt = DefaultPolyAltitude
            NewPoly.GPoints(2).Selected = False

            AuxLatPoly = Lat
            AuxLonPoly = Lon

            Exit Sub
        End If

        ' PtPolyCounter is > 2

        AuxLatPoly = Lat
        AuxLonPoly = Lon
        ReDim Preserve NewPoly.GPoints(PtPolyCounter)
        NewPoly.GPoints(PtPolyCounter).lat = Lat
        NewPoly.GPoints(PtPolyCounter).lon = Lon
        NewPoly.GPoints(PtPolyCounter).alt = DefaultPolyAltitude
        NewPoly.GPoints(PtPolyCounter).Selected = False
        NewPoly.NoOfPoints = PtPolyCounter

        PtPolyCounter = PtPolyCounter + 1

    End Sub

    Friend Sub EndPoly(ByVal X As Integer, ByVal Y As Integer)

        If PtPolyCounter = 0 Then
            ProcessPopUp(X, Y)
            Exit Sub
        End If

        If PtPolyCounter = 2 Then
            PtPolyCounter = 0
            Exit Sub
        End If

        ' ok, create the Poly!
        PtPolyCounter = 0

        NoOfPolys = NoOfPolys + 1
        ReDim Preserve Polys(NoOfPolys)


        Polys(NoOfPolys) = NewPoly
        Polys(NoOfPolys).Name = Str(Polys(NoOfPolys).NoOfPoints) & "_Pts_Polygon_of_Type_None"
        Polys(NoOfPolys).Guid = DefaultPolyNoneGuid
        Polys(NoOfPolys).NoOfChilds = 0
        Dirty = True

        If (NoOfPolys > 1) And AutoLinePolyJoin Then
            If TryThisPolyJoin(NoOfPolys) Then
                Beep()
                AddLatLonToPoly(NoOfPolys)
                Exit Sub
            End If
        End If

        AddLatLonToPoly(NoOfPolys)
        HidePopUPMenu()

    End Sub

    Friend Sub AddLatLonToPoly(ByVal N As Integer)

        Dim K As Integer
        Dim EL, SL, NL, WL, x As Double

        NL = -90 : SL = 90 : EL = -180 : WL = 180

        For K = 1 To Polys(N).NoOfPoints
            x = Polys(N).GPoints(K).lat
            If x < SL Then SL = x
            If x > NL Then NL = x
            x = Polys(N).GPoints(K).lon
            If x > EL Then EL = x
            If x < WL Then WL = x
        Next K
        Polys(N).ELON = EL
        Polys(N).WLON = WL
        Polys(N).NLAT = NL
        Polys(N).SLAT = SL

    End Sub

    Friend Sub TryParentPoly(ByVal X1 As Integer, ByVal Y1 As Integer)

        ' this is called upon mouse down when SelectParent is ON.
        ' it uses POPIndex which points to the polygon that we want 
        ' to make a child. In April 2014, if there are also selected polygons 
        ' in Poly mode we will try to make childs of the chosed
        ' parent polygon

        Dim N, K As Integer
        Dim X, Y As Double

        X = LonDispWest * PixelsPerLonDeg + X1   ' longitude in pixels
        Y = LatDispNorth * PixelsPerLatDeg - Y1  ' latitude in pixels

        N = IsPolyUP(X, Y)  ' N parent
        If N = 0 Then
            MsgBox("You need to click on the border of a polygon!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Polys(N).NoOfChilds < 0 Then
            MsgBox("The selected polygon is a hole!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        ' OK we clicked on a good parent polygon  pointed by N

        Dim NLat, SLat, WLon, ELon As Double
        NLat = Polys(N).NLAT
        SLat = Polys(N).SLAT
        ELon = Polys(N).ELON
        WLon = Polys(N).WLON

        Dim Flag As Boolean = False

        If PointerON Then ' we just make a single hole

            K = POPIndex  ' K  child
            If N = K Then
                MsgBox("A polygon can not be an hole of itself!", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            If NLat < Polys(POPIndex).NLAT Then Flag = True
            If SLat > Polys(POPIndex).SLAT Then Flag = True
            If ELon < Polys(POPIndex).ELON Then Flag = True
            If WLon > Polys(POPIndex).WLON Then Flag = True
            If Flag Then
                MsgBox("The selected polygon can not contain the hole!", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            MakePolyClockWise(N)
            MakePolyAntiClockWise(K)
            Polys(K).Guid = Polys(N).Guid
            Polys(K).Type = Polys(N).Type
            Polys(K).Color = Polys(N).Color
            Polys(K).NoOfChilds = -N
            ReDim Preserve Polys(N).Childs(Polys(N).NoOfChilds + 1)
            Polys(N).NoOfChilds = Polys(N).NoOfChilds + 1
            Polys(N).Childs(Polys(N).NoOfChilds) = K
            RebuildDisplay()
            Exit Sub
        End If

        ' OK we will do bulk
        ' make sure POPIndex is selected and the parent N deselected
        If Polys(POPIndex).Selected = False Then Polys(POPIndex).Selected = True
        If Polys(N).Selected = True Then Polys(N).Selected = False

        Dim OK(NoOfPolys) As Boolean ' if OK then polygon can be a child

        Dim NoOfOKs As Integer = 0
        For K = 1 To NoOfPolys
            OK(K) = False
            If Polys(K).Selected Then ' it is selected then is a candidate
                If Polys(K).NoOfChilds = 0 Then  ' it is not a hole nor it has childs
                    Flag = True
                    If NLat < Polys(K).NLAT Then Flag = False
                    If SLat > Polys(K).SLAT Then Flag = False
                    If ELon < Polys(K).ELON Then Flag = False
                    If WLon > Polys(K).WLON Then Flag = False
                    If Flag Then
                        MakePolyAntiClockWise(K)
                        Polys(K).Guid = Polys(N).Guid
                        Polys(K).Type = Polys(N).Type
                        Polys(K).Color = Polys(N).Color
                        Polys(K).NoOfChilds = -N
                        OK(K) = True
                        NoOfOKs = NoOfOKs + 1
                    End If
                End If
            End If
        Next

        MakePolyClockWise(N)

        ReDim Preserve Polys(N).Childs(Polys(N).NoOfChilds + NoOfOKs)

        For K = 1 To NoOfPolys
            If OK(K) Then
                Polys(N).NoOfChilds = Polys(N).NoOfChilds + 1
                Polys(N).Childs(Polys(N).NoOfChilds) = K
            End If
        Next

        RebuildDisplay()

    End Sub


    Friend Sub SelectAllPolys(ByRef Flag As Boolean)

        Dim N, K As Integer
        If Not PolyVIEW Then Exit Sub

        If Flag Then
            frmStart.SelectAllPolysMenuItem.Checked = True
        Else
            frmStart.SelectAllPolysMenuItem.Checked = False
        End If

        For N = 1 To NoOfPolys
            If Flag Then
                If Not Polys(N).Selected Then NoOfPolysSelected = NoOfPolysSelected + 1
                SomeSelected = True
            Else
                If Polys(N).Selected Then NoOfPolysSelected = NoOfPolysSelected - 1
            End If

            For K = 1 To Polys(N).NoOfPoints
                Polys(N).GPoints(K).Selected = False
            Next K

            Polys(N).Selected = Flag
        Next N

    End Sub

    Friend Sub SelectInvertPolys()

        Dim N, K As Integer
        Dim Flag As Boolean

        If Not PolyVIEW Then Exit Sub

        For N = 1 To NoOfPolys
            If Polys(N).Selected Then
                NoOfPolysSelected = NoOfPolysSelected - 1
                Polys(N).Selected = False
                ' unselect points ?
                GoTo Jump_Next
            Else
                Flag = False
                For K = 1 To Polys(N).NoOfPoints
                    If Polys(N).GPoints(K).Selected Then
                        Flag = True
                        Exit For
                    End If
                Next K
                If Flag Then GoTo Jump_Next
                NoOfPolysSelected = NoOfPolysSelected + 1
                SomeSelected = True
                Polys(N).Selected = True
            End If
Jump_Next:
        Next N

    End Sub


    Friend Sub SelectPolysInBox(ByVal X0 As Double, ByVal Y0 As Double, ByVal X1 As Double, ByVal Y1 As Double)

        Dim N As Integer

        For N = 1 To NoOfPolys
            If Polys(N).ELON < X1 Then
                If Polys(N).WLON > X0 Then
                    If Polys(N).SLAT > Y1 Then
                        If Polys(N).NLAT < Y0 Then
                            If Not Polys(N).Selected Then NoOfPolysSelected = NoOfPolysSelected + 1
                            SomeSelected = True
                            Polys(N).Selected = True
                        End If
                    End If
                End If
            End If
        Next N


    End Sub

    '    Friend Sub DisplayPolys(ByVal gr As Graphics)

    '        Dim X, Y As Integer
    '        Dim Flag As Boolean

    '        Dim K, N, NP As Integer
    '        Dim A As String

    '        Dim myPen As New System.Drawing.Pen(PolyColorBorder)
    '        Dim myBrush As New System.Drawing.SolidBrush(Color.Yellow)

    '        For N = 1 To NoOfPolys

    '            If Not MoveON Then Polys(N).OnScreen = False

    '            If Polys(N).NLAT < LatDispSouth Then GoTo skip_this_one
    '            If Polys(N).SLAT > LatDispNorth Then GoTo skip_this_one
    '            If Polys(N).WLON > LonDispEast Then GoTo skip_this_one
    '            If Polys(N).ELON < LonDispWest Then GoTo skip_this_one

    '            Polys(N).OnScreen = True

    '            If HideOtherPolys Then
    '                a = Left(Polys(N).Type, 1)
    '                If a = "O" Then GoTo skip_this_one
    '            End If

    '            NP = Polys(N).NoOfPoints

    '            Flag = False
    '            ReDim PTS(NP - 1)
    '            For K = 1 To NP
    '                PTS(K - 1).X = CInt((Polys(N).GPoints(K).lon - LonDispWest) * PixelsPerLonDeg)
    '                PTS(K - 1).Y = CInt((LatDispNorth - Polys(N).GPoints(K).lat) * PixelsPerLatDeg)
    '                Flag = Flag Or Polys(N).GPoints(K).Selected
    '            Next K

    '            If PolyFILL Then
    '                myBrush.Color = Polys(N).Color
    '                gr.FillPolygon(myBrush, PTS)
    '            End If
    '            gr.DrawPolygon(myPen, PTS)


    '            If Polys(N).Selected Then
    '                myBrush.Color = SelectedLineColor
    '                For K = 1 To NP
    '                    X = CInt((Polys(N).GPoints(K).lon - LonDispWest) * PixelsPerLonDeg)
    '                    Y = CInt((LatDispNorth - Polys(N).GPoints(K).lat) * PixelsPerLatDeg)
    '                    gr.FillRectangle(myBrush, X - 2, Y - 2, 4, 4)
    '                Next K

    '            ElseIf PolyON Or Flag Then
    '                For K = 1 To NP
    '                    myBrush.Color = UnselectedPointColor
    '                    If Polys(N).GPoints(K).Selected Then myBrush.Color = SelectedLineColor
    '                    X = CInt((Polys(N).GPoints(K).lon - LonDispWest) * PixelsPerLonDeg)
    '                    Y = CInt((LatDispNorth - Polys(N).GPoints(K).lat) * PixelsPerLatDeg)
    '                    gr.FillRectangle(myBrush, X - 2, Y - 2, 4, 4)
    '                Next K

    '            End If

    'skip_this_one:
    '        Next N

    '        myPen.Dispose()
    '        myBrush.Dispose()



    '    End Sub

    Friend Sub DisplayPolys(ByVal gr As Graphics)

        Dim X, Y As Integer
        Dim Flag As Boolean

        Dim J, K, N, M, NC, NP As Integer

        Dim myPen As New System.Drawing.Pen(PolyColorBorder, PolyPenWidth)
        Dim myBrush As New System.Drawing.SolidBrush(Color.Yellow)
        Dim path As New System.Drawing.Drawing2D.GraphicsPath

        Dim P1, P2 As Integer  ' to draw the points
        P1 = 2
        If PolyPenWidth = 2 Then P1 = 3
        P2 = 2 * P1

        For N = 1 To NoOfPolys

            If Not MoveON Then Polys(N).OnScreen = False

            If Polys(N).NLAT < LatDispSouth Then GoTo skip_this_one
            If Polys(N).SLAT > LatDispNorth Then GoTo skip_this_one
            If Polys(N).WLON > LonDispEast Then GoTo skip_this_one
            If Polys(N).ELON < LonDispWest Then GoTo skip_this_one

            Polys(N).OnScreen = True
            If Polys(N).NoOfChilds < 0 Then GoTo skip_fill

            'Dim A As String
            'If HideOtherPolys Then
            '    a = Left(Polys(N).Type, 1)
            '    If a = "O" Then GoTo skip_this_one
            'End If

            NP = Polys(N).NoOfPoints
            ReDim PTS(NP - 1)
            For K = 1 To NP
                PTS(K - 1).X = CInt((Polys(N).GPoints(K).lon - LonDispWest) * PixelsPerLonDeg)
                PTS(K - 1).Y = CInt((LatDispNorth - Polys(N).GPoints(K).lat) * PixelsPerLatDeg)
            Next K

            path.Reset()
            path.AddLines(PTS)
            path.CloseFigure()

            If Polys(N).NoOfChilds > 0 Then
                For J = 1 To Polys(N).NoOfChilds
                    M = Polys(N).Childs(J)
                    Polys(M).OnScreen = True
                    NC = Polys(M).NoOfPoints
                    ReDim PTS(NC - 1)
                    For K = 1 To NC
                        PTS(K - 1).X = CInt((Polys(M).GPoints(K).lon - LonDispWest) * PixelsPerLonDeg)
                        PTS(K - 1).Y = CInt((LatDispNorth - Polys(M).GPoints(K).lat) * PixelsPerLatDeg)
                    Next K
                    path.AddLines(PTS)
                    path.CloseFigure()
                Next J
            End If
            If PolyFILL Then
                myBrush.Color = Polys(N).Color
                gr.FillPath(myBrush, path)
            End If

            gr.DrawPath(myPen, path)

skip_fill:

            NP = Polys(N).NoOfPoints
            If Polys(N).Selected Then
                myBrush.Color = SelectedLineColor
                For K = 1 To NP
                    X = CInt((Polys(N).GPoints(K).lon - LonDispWest) * PixelsPerLonDeg)
                    Y = CInt((LatDispNorth - Polys(N).GPoints(K).lat) * PixelsPerLatDeg)
                    gr.FillRectangle(myBrush, X - P1, Y - P1, P2, P2)
                Next K
            Else
                Flag = False
                For K = 1 To NP
                    If Polys(N).GPoints(K).Selected Then
                        Flag = True
                        Exit For
                    End If
                Next
                If PolyON Or Flag Then
                    For K = 1 To NP
                        myBrush.Color = UnselectedPointColor
                        If Polys(N).GPoints(K).Selected Then myBrush.Color = SelectedLineColor
                        X = CInt((Polys(N).GPoints(K).lon - LonDispWest) * PixelsPerLonDeg)
                        Y = CInt((LatDispNorth - Polys(N).GPoints(K).lat) * PixelsPerLatDeg)
                        gr.FillRectangle(myBrush, X - P1, Y - P1, P2, P2)
                    Next K
                End If
            End If

skip_this_one:
        Next N


        gr.DrawPath(myPen, path)


        myPen.Dispose()
        myBrush.Dispose()
        path.Dispose()



    End Sub


    Friend Sub DeletePointInPoly(ByVal PL As Integer, ByVal PT As Integer)

        Dim P As Integer ' when they come here they have at least 3 points

        If Not SkipBackUp Then BackUp()

        If PT < Polys(Pl).NoOfPoints Then
            For P = PT To Polys(Pl).NoOfPoints - 1
                Polys(Pl).GPoints(P) = Polys(Pl).GPoints(P + 1)
            Next P
        End If

        ReDim Preserve Polys(Pl).GPoints(Polys(Pl).NoOfPoints - 1)
        Polys(Pl).NoOfPoints = Polys(Pl).NoOfPoints - 1
        Dirty = True

    End Sub

    Friend Sub DeletePoly(ByVal N As Integer)

        Dim K, J, P, C, D As Integer
        If Polys(N).Selected Then NoOfPolysSelected = NoOfPolysSelected - 1

        If Not SkipBackUp Then BackUp()

        'check all that are childs and set the index of parent
        For K = 1 To NoOfPolys
            If Polys(K).NoOfChilds < 0 Then
                P = -Polys(K).NoOfChilds
                If P = N Then
                    Polys(K).NoOfChilds = 0
                ElseIf P > N Then
                    Polys(K).NoOfChilds = Polys(K).NoOfChilds + 1
                End If
            End If
        Next

        'check all that are parents and set the index of childs
        For K = 1 To NoOfPolys
            If Polys(K).NoOfChilds > 0 Then
                D = 0
                For J = 1 To Polys(K).NoOfChilds
                    C = Polys(K).Childs(J)
                    If C = N Then
                        D = J
                    ElseIf C > N Then
                        Polys(K).Childs(J) = Polys(K).Childs(J) - 1
                    End If
                Next
                If D > 0 Then DeleteThisChild(K, D)
            End If
        Next

        If N < NoOfPolys Then
            For K = N To NoOfPolys - 1
                Polys(K) = Polys(K + 1)
            Next K
        End If

        If NoOfPolys > 1 Then
            ReDim Preserve Polys(NoOfPolys - 1)
        End If

        NoOfPolys = NoOfPolys - 1
        Dirty = True

    End Sub
    Friend Sub DeleteThisChild(ByVal P As Integer, ByVal C As Integer)

        Dim N As Integer

        If C < Polys(P).NoOfChilds Then
            For N = C To Polys(P).NoOfChilds - 1
                Polys(P).Childs(N) = Polys(P).Childs(N + 1)
            Next
        End If

        If Polys(P).NoOfChilds > 1 Then
            ReDim Preserve Polys(P).Childs(Polys(P).NoOfChilds - 1)
        End If

        Polys(P).NoOfChilds = Polys(P).NoOfChilds - 1

    End Sub

    Friend Sub DeleteParentAndChilds(ByVal N As Integer)

        Dim K As Integer
        Polys(N).Guid = "Delete!"
        For K = 1 To Polys(N).NoOfChilds
            Polys(Polys(N).Childs(K)).Guid = "Delete!"
        Next
        For K = NoOfPolys To 1 Step -1
            If Polys(K).Guid = "Delete!" Then
                DeletePoly(K)
            End If
        Next K

    End Sub

    Friend Sub DeleteSelectedPolys()

        Dim N As Integer

        For N = NoOfPolys To 1 Step -1
            If Polys(N).Selected Then
                DeletePoly(N)
            End If
        Next N

    End Sub

    Friend Sub DeleteSelectedPointsInPolys()

        Dim N, K As Integer

        For N = NoOfPolys To 1 Step -1
            For K = Polys(N).NoOfPoints To 1 Step -1
                If Polys(N).GPoints(K).Selected Then
                    If Polys(N).NoOfPoints < 3 Then
                        DeletePoly(N)
                        Dirty = True
                    Else
                        DeletePointInPoly(N, K)
                    End If
                End If
            Next K
        Next N

    End Sub

    Friend Function IsInsertPointInPoly(ByVal X1 As Integer, ByVal Y1 As Integer) As Boolean

        'returns true if point was inserted

        Dim retval As Boolean
        Dim N, K As Integer
        Dim X, Y As Double

        IsInsertPointInPoly = False
        If InsertON = False Then Exit Function
        If Not PolyVIEW Then Exit Function

        X = LonDispWest * PixelsPerLonDeg + X1
        Y = LatDispNorth * PixelsPerLatDeg - Y1

        ' check if we are over a Line
        For N = 1 To NoOfPolys
            K = Polys(N).NoOfPoints
            retval = IsPointInSegment(Polys(N).GPoints(1).lon, Polys(N).GPoints(1).lat, Polys(N).GPoints(K).lon, Polys(N).GPoints(K).lat, X, Y)
            K = 2
            Do Until retval = True Or K = Polys(N).NoOfPoints + 1
                retval = retval Or IsPointInSegment(Polys(N).GPoints(K - 1).lon, Polys(N).GPoints(K - 1).lat, Polys(N).GPoints(K).lon, Polys(N).GPoints(K).lat, X, Y)
                K = K + 1
            Loop
            If retval Then
                IsInsertPointInPoly = True
                InsertPointInPoly(X, Y, N, K - 1)
                Exit Function
            End If
        Next N

    End Function

    Friend Function InsertPointInPoly(ByVal X As Double, ByVal Y As Double, ByVal Poly As Integer, ByVal Point As Integer) As Integer

        ' returns 0 if insert fails or 1 if it inserts

        Dim N, K As Integer
        Dim X1, Y1 As Double

        X1 = X / PixelsPerLonDeg
        Y1 = Y / PixelsPerLatDeg

        InsertPointInPoly = 0

        N = Polys(Poly).NoOfPoints
        For K = 1 To N
            If System.Math.Abs(Polys(Poly).GPoints(K).lat - Y1) < D22Lat Then
                If System.Math.Abs(Polys(Poly).GPoints(K).lon - X1) < D22Lon Then
                    Exit Function
                End If
            End If
        Next K

        If BackUpON Then BackUp()

        N = Polys(Poly).NoOfPoints + 1
        Polys(Poly).NoOfPoints = N
        ReDim Preserve Polys(Poly).GPoints(N)

        If Point = 1 Then Point = N

        For K = N - 1 To Point Step -1
            Polys(Poly).GPoints(K + 1) = Polys(Poly).GPoints(K)
        Next K

        Polys(Poly).GPoints(Point).lat = Y1
        Polys(Poly).GPoints(Point).lon = X1
        Polys(Poly).GPoints(Point).Selected = True

        InsertPointInPoly = 1

        Dim N0, N2 As Integer
        Dim Z0, Z2 As Double
        Dim X0, X2 As Double
        Dim Y0, Y2 As Double
        Dim XX As Double

        If Point = N Then
            N0 = Point - 1
            N2 = 1
        Else
            N0 = Point - 1
            N2 = Point + 1
        End If

        Z0 = Polys(Poly).GPoints(N0).alt
        Z2 = Polys(Poly).GPoints(N2).alt

        If Z2 = Z0 Then
            Polys(Poly).GPoints(Point).alt = Z0
            Exit Function
        End If

        X0 = Polys(Poly).GPoints(N0).lon
        X2 = Polys(Poly).GPoints(N2).lon

        If X0 <> X2 Then
            Y0 = X1 - X0
            Y2 = X2 - X1
            XX = X2 - X0
            Y0 = 1 - (Y0 / XX)
            Y2 = 1 - (Y2 / XX)
            Polys(Poly).GPoints(Point).alt = Z0 * Y0 + Z2 * Y2
            Exit Function
        End If

        Y0 = Polys(Poly).GPoints(N0).lat
        Y2 = Polys(Poly).GPoints(N2).lat

        If Y0 = Y2 Then ' just in case!
            Polys(Poly).GPoints(Point).alt = Z0
            Exit Function
        End If

        ' last try!
        X0 = Y1 - Y0
        X2 = Y2 - Y1
        XX = Y2 - Y0
        X0 = 1 - (X0 / XX)
        X2 = 1 - (X2 / XX)
        Polys(Poly).GPoints(Point).alt = Z0 * X0 + Z2 * X2

    End Function


    Friend Function IsPointInPoly(ByVal X1 As Integer, ByVal Y1 As Integer) As Boolean


        'THIS HAS CHANGED - on entry X Y contain distance from center display in pixels
        ' NOW Sept2006 - on entry X Y contain distance from the origin of the display in pixels

        Dim N, K As Integer
        Dim retval As Boolean
        Dim WLON, NLAT, SLAT, ELON As Double
        Dim X, Y As Double

        IsPointInPoly = False
        If Not PolyVIEW Then Exit Function

        WLON = LonDispWest + (X1 + 5) / PixelsPerLonDeg
        ELON = LonDispWest + (X1 - 5) / PixelsPerLonDeg
        NLAT = LatDispNorth - (Y1 + 5) / PixelsPerLatDeg
        SLAT = LatDispNorth - (Y1 - 5) / PixelsPerLatDeg

        X = LonDispWest * PixelsPerLonDeg + X1  ' longitude in pixels
        Y = LatDispNorth * PixelsPerLatDeg - Y1 ' latitude in pixels

        For N = 1 To NoOfPolys

            If Polys(N).Selected Then GoTo Jump_Next
            If WLON < Polys(N).WLON Then GoTo Jump_Next
            If ELON > Polys(N).ELON Then GoTo Jump_Next
            If SLAT < Polys(N).SLAT Then GoTo Jump_Next
            If NLAT > Polys(N).NLAT Then GoTo Jump_Next

            K = 1
            retval = False
            Do Until retval = True Or K = Polys(N).NoOfPoints + 1
                retval = retval Or IsPoint(Polys(N).GPoints(K).lon, Polys(N).GPoints(K).lat, X, Y)
                K = K + 1
            Loop
            If retval Then
                If Polys(N).GPoints(K - 1).Selected = False Then NoOfPointsSelected = NoOfPointsSelected + 1
                Polys(N).GPoints(K - 1).Selected = True
                IsPointInPoly = True
                Exit Function
            End If
Jump_Next:
        Next N

    End Function

    Friend Function IsMouseOnPoly(ByVal X1 As Integer, ByVal Y1 As Integer, ByRef Pt As Integer) As Integer

        ' returns the first Poly that has the mouse over it! if none returns 0
        ' on entry X Y contains mouse distance from origin of display in pixels
        ' if poly was found then Pt returns 0 if not over a vertex or the index of a vertex

        Dim N, K As Integer
        Dim retval As Boolean
        Dim X, Y As Double

        Pt = 0
        IsMouseOnPoly = 0
        If PolyVIEW = False Then Exit Function

        X = LonDispWest * PixelsPerLonDeg + X1
        Y = LatDispNorth * PixelsPerLatDeg - Y1

        For N = 1 To NoOfPolys
            If Polys(N).OnScreen = False Then GoTo Jump_Next
            K = 2
            retval = False
            Do Until retval = True Or K = Polys(N).NoOfPoints + 1
                retval = retval Or IsPointInSegment(Polys(N).GPoints(K - 1).lon, Polys(N).GPoints(K - 1).lat, Polys(N).GPoints(K).lon, Polys(N).GPoints(K).lat, X, Y)
                K = K + 1
            Loop
            If retval Then
                IsMouseOnPoly = N
                If IsPoint(Polys(N).GPoints(K - 2).lon, Polys(N).GPoints(K - 2).lat, X, Y) Then
                    Pt = K - 2
                    Exit Function
                End If
                If IsPoint(Polys(N).GPoints(K - 1).lon, Polys(N).GPoints(K - 1).lat, X, Y) Then
                    Pt = K - 1
                    Exit Function
                End If
                Exit Function
            End If
Jump_Next:
        Next N

    End Function

    


    Friend Function IsPolySelected(ByVal X As Double, ByVal Y As Double) As Boolean

        ' X and Y come with pixels from earth center

        Dim N, K As Integer
        Dim retval As Boolean
        Dim WLON, NLAT, SLAT, ELON As Double

        IsPolySelected = False
        If Not PolyVIEW Then Exit Function

        WLON = (X + 5) / PixelsPerLonDeg
        ELON = (X - 5) / PixelsPerLonDeg
        NLAT = (Y - 5) / PixelsPerLatDeg
        SLAT = (Y + 5) / PixelsPerLatDeg

        For N = 1 To NoOfPolys

            If WLON < Polys(N).WLON Then GoTo Jump_Next
            If ELON > Polys(N).ELON Then GoTo Jump_Next
            If SLAT < Polys(N).SLAT Then GoTo Jump_Next
            If NLAT > Polys(N).NLAT Then GoTo Jump_Next

            K = Polys(N).NoOfPoints
            retval = IsPointInSegment(Polys(N).GPoints(1).lon, Polys(N).GPoints(1).lat, Polys(N).GPoints(K).lon, Polys(N).GPoints(K).lat, X, Y)
            K = 2
            Do Until retval = True Or K = Polys(N).NoOfPoints + 1
                retval = retval Or IsPointInSegment(Polys(N).GPoints(K - 1).lon, Polys(N).GPoints(K - 1).lat, Polys(N).GPoints(K).lon, Polys(N).GPoints(K).lat, X, Y)
                K = K + 1
            Loop
            If retval Then
                If Polys(N).Selected = False Then NoOfPolysSelected = NoOfPolysSelected + 1
                Polys(N).Selected = True
                For K = 1 To Polys(N).NoOfPoints
                    If Polys(N).GPoints(K).Selected Then NoOfPointsSelected = NoOfPointsSelected - 1
                    Polys(N).GPoints(K).Selected = False
                Next K
                IsPolySelected = True
                Exit Function
            End If
Jump_Next:
        Next N

    End Function

    Friend Sub MoveSelectedPolys(ByVal X As Double, ByVal Y As Double)

        Dim N, K, J, L As Integer
        Dim Flag As Boolean = False
        Dim X1, Y1 As Double

        For N = 1 To NoOfPolys
            If Polys(N).OnScreen Then

                If Polys(N).Selected Then
                    If Polys(N).NoOfChilds >= 0 Then ' has not children
                        For K = 1 To Polys(N).NoOfPoints
                            Polys(N).GPoints(K).lat = Polys(N).GPoints(K).lat - Y
                            Polys(N).GPoints(K).lon = Polys(N).GPoints(K).lon + X
                        Next K
                        AddLatLonToPolyxy(N, X, Y)
                        ' childs move with parents except if parent is selected
                        If Polys(N).NoOfChilds > 0 Then
                            For K = 1 To Polys(N).NoOfChilds
                                J = Polys(N).Childs(K)
                                For L = 1 To Polys(J).NoOfPoints
                                    Polys(J).GPoints(L).lat = Polys(J).GPoints(L).lat - Y
                                    Polys(J).GPoints(L).lon = Polys(J).GPoints(L).lon + X
                                Next
                                AddLatLonToPolyxy(J, X, Y)
                            Next
                        End If
                    Else ' if selected and child - test if it remains inside parent
                        J = -Polys(N).NoOfChilds ' Jth is the parent of child N
                        If Not Polys(J).Selected Then  ' parent not selected then move child
                            If Polys(J).NLAT > Polys(N).NLAT - Y Then
                                If Polys(J).SLAT < Polys(N).SLAT - Y Then
                                    If Polys(J).WLON < Polys(N).WLON + X Then
                                        If Polys(J).ELON > Polys(N).ELON + X Then
                                            ' ok it can be moved
                                            For L = 1 To Polys(N).NoOfPoints
                                                Polys(N).GPoints(L).lat = Polys(N).GPoints(L).lat - Y
                                                Polys(N).GPoints(L).lon = Polys(N).GPoints(L).lon + X
                                            Next
                                            AddLatLonToPolyxy(N, X, Y)
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If

                Else ' if not selected check points movement

                    If Polys(N).NoOfChilds >= 0 Then  ' is not children so move freely
                        Flag = False
                        For K = 1 To Polys(N).NoOfPoints
                            If Polys(N).GPoints(K).Selected Then
                                Polys(N).GPoints(K).lat = Polys(N).GPoints(K).lat - Y
                                Polys(N).GPoints(K).lon = Polys(N).GPoints(K).lon + X
                                Flag = True
                            End If
                        Next K
                        If Flag Then AddLatLonToPoly(N)

                    Else  ' is child
                        J = -Polys(N).NoOfChilds ' Jth is the parent of child N
                        If Not Polys(J).Selected Then
                            Flag = False
                            For K = 1 To Polys(N).NoOfPoints
                                If Polys(N).GPoints(K).Selected Then
                                    Y1 = Polys(N).GPoints(K).lat - Y
                                    X1 = Polys(N).GPoints(K).lon + X
                                    If Y1 < Polys(J).NLAT Then
                                        If Y1 > Polys(J).SLAT Then
                                            Polys(N).GPoints(K).lat = Y1
                                            Flag = True
                                        End If
                                    End If
                                    If X1 < Polys(J).ELON Then
                                        If X1 > Polys(J).WLON Then
                                            Polys(N).GPoints(K).lon = X1
                                            Flag = True
                                        End If
                                    End If
                                End If
                            Next K
                            If Flag Then AddLatLonToPoly(N)
                        End If
                    End If
                End If
            End If
        Next N

        Dirty = True

    End Sub
    Private Sub AddLatLonToPolyXY(ByVal N As Integer, ByVal X As Double, ByVal Y As Double)

        Polys(N).NLAT = Polys(N).NLAT - Y
        Polys(N).SLAT = Polys(N).SLAT - Y
        Polys(N).ELON = Polys(N).ELON + X
        Polys(N).WLON = Polys(N).WLON + X

    End Sub
    Friend Sub CheckPolyJoins()

        Dim N, N1 As Integer

        BackUp()
        SkipBackUp = True

        N1 = 1
Return_back:
        N = N1
        Do While N <= NoOfPolys
            If Polys(N).Selected Then
                If TryThisPolyJoin(N) Then GoTo Return_back
            End If
            N = N + 1
            N1 = N
        Loop

        SkipBackUp = False

        'comentei e descomentei pois não percebi pq!!!
        For N = 1 To NoOfPolys
            If Polys(N).GPoints(1).Selected Then
                If TryThisPolyJoin(N) Then Exit Sub
            End If
            If Polys(N).GPoints(Polys(N).NoOfPoints).Selected Then
                If TryThisPolyJoin(N) Then Exit Sub
            End If
        Next N

    End Sub

    Friend Sub TryAllPolyJoin()

        Dim N, K As Integer

        Dim Done(NoOfPolys) As Boolean

        For N = 1 To NoOfPolys
            MakePolyClockWise(N)
        Next
jump_here:
        For N = 1 To NoOfPolys
            If Done(N) Then GoTo next_n
            If N > NoOfPolys - 1 Then GoTo end_here
            If Polys(N).Selected Then
                For K = N + 1 To NoOfPolys
                    If K > NoOfPolys Then Exit For
                    If Polys(K).Selected Then
                        If Try2PolyJoin(N, K) Then
                            GoTo jump_here
                        End If
                        If Try2PolyJoin(K, N) Then
                            GoTo jump_here
                        End If
                    End If
                Next
            End If
            Done(N) = True
next_n:
        Next

        ReDim Done(0)

end_here:
        For N = 1 To NoOfPolys
            If Polys(N).Selected Then
                CleanPoly(N)
            End If
        Next

    End Sub

    Private Function Try2PolyJoin(ByVal N1 As Integer, ByVal N2 As Integer) As Boolean

        Dim M, N, NX As Integer
        Dim K1, K, K2 As Integer
        Dim N1T, N2T As Integer
        Dim N2B, N1B, N1A, N2A, NT As Integer
        Dim Flag As Integer
        Dim Common As Boolean
        Dim Pts() As GPoint
        Dim X1 As Double_XY

        Dim DeltaLat, DeltaLon As Double
        Try2PolyJoin = False
        If NameJoin Then
            If Polys(N1).Name <> Polys(N2).Name Then Exit Function
        End If


        If DisplayJoin Then
            DeltaLon = 3 / PixelsPerLonDeg
            DeltaLat = 3 / PixelsPerLatDeg
        Else
            DeltaLon = D255Lon
            DeltaLat = D255Lat
        End If



        N1T = Polys(N1).NoOfPoints
        N2T = Polys(N2).NoOfPoints

        For N = 1 To N1T
            Common = False
            X1.Y = Polys(N1).GPoints(N).lat
            X1.X = Polys(N1).GPoints(N).lon
            For M = 1 To N2T
                If System.Math.Abs(X1.Y - Polys(N2).GPoints(M).lat) < DeltaLat Then
                    If System.Math.Abs(X1.X - Polys(N2).GPoints(M).lon) < DeltaLon Then
                        Common = True
                        Exit For
                    End If
                End If
            Next M
            If Not Common Then Exit For
        Next N

        N1A = N  ' 1st point in N1 that is not commom to some point in N2

        If N1A > N1T Then
            ' MsgBox("Polygons can not be joined!", 16)
            Exit Function
        End If

        If N1A > 1 Then CyclePoly(N1, N1A)

        ' now Point 1 is not on the common part!
        ' ======================================

        Flag = 0
Return_Here:
        Flag = Flag + 1
        For N = 1 To N1T
            For M = 1 To N2T
                If System.Math.Abs(Polys(N1).GPoints(N).lat - Polys(N2).GPoints(M).lat) < DeltaLat Then
                    If System.Math.Abs(Polys(N1).GPoints(N).lon - Polys(N2).GPoints(M).lon) < DeltaLon Then GoTo jump_here
                End If
            Next M
        Next N
jump_here:

        N1A = N   ' 1st point in N1 that is common
        N2A = M   ' M is common to N

        If N1A >= N1T Then
            'MsgBox("Polygons can not be joined!", 16)
            Exit Function
        End If

        If N1A = 1 Then
            'MsgBox("Fatal error in polygon " & N1 & "!", MsgBoxStyle.Critical)
            Exit Function
        End If

        K1 = N1A
        K2 = N2A
        Do
            K1 = K1 + 1
            K2 = K2 - 1
            If K1 > N1T Then
                K1 = 1
            End If
            If K2 < 1 Then
                K2 = N2T
            End If
            If System.Math.Abs(Polys(N1).GPoints(K1).lat - Polys(N2).GPoints(K2).lat) > DeltaLat Then
                N1B = K1 - 1
                N2B = K2 + 1
                Exit Do
            End If
            If System.Math.Abs(Polys(N1).GPoints(K1).lon - Polys(N2).GPoints(K2).lon) > DeltaLon Then
                N1B = K1 - 1
                N2B = K2 + 1
                Exit Do
            End If
        Loop

        If N1B = 0 Then N1B = N1T
        If N2B = N2T + 1 Then N2B = 1

        If Flag = 1 Then
            If Not N2A = N2T Then
                CyclePoly(N2, N2A + 1)
                GoTo Return_Here
            End If
        End If

        If Flag = 2 And Not N2A = N2T Then Exit Function

        NX = N1B - N1A
        NX = 2 * NX
        NT = N1T + N2T - NX

        ReDim Pts(N1T)

        For K = 1 To N1T
            Pts(K).lat = Polys(N1).GPoints(K).lat
            Pts(K).lon = Polys(N1).GPoints(K).lon
            Pts(K).alt = Polys(N1).GPoints(K).alt
        Next K

        Polys(N1).NoOfPoints = NT
        ReDim Polys(N1).GPoints(NT)

        K = 1

        For M = 1 To N1A
            Polys(N1).GPoints(K).lat = Pts(M).lat
            Polys(N1).GPoints(K).lon = Pts(M).lon
            Polys(N1).GPoints(K).alt = Pts(M).alt
            K = K + 1
        Next M

        For M = 1 To N2B - 1
            Polys(N1).GPoints(K).lat = Polys(N2).GPoints(M).lat
            Polys(N1).GPoints(K).lon = Polys(N2).GPoints(M).lon
            Polys(N1).GPoints(K).alt = Polys(N2).GPoints(M).alt
            K = K + 1
        Next M

        For M = N1B To N1T
            Polys(N1).GPoints(K).lat = Pts(M).lat
            Polys(N1).GPoints(K).lon = Pts(M).lon
            Polys(N1).GPoints(K).alt = Pts(M).alt
            K = K + 1
        Next M

        AddLatLonToPoly(N1)
        DeletePoly(N2)
        Beep()
        Try2PolyJoin = True

    End Function

    Private Sub CleanPoly(ByVal N As Integer)

        Dim PI_180 As Double = PI / 180.0
        Dim K, NP As Integer
        Dim D, UX, UY, VX, VY, X1, X2, X3, Y1, Y2, Y3 As Double
jump_here:
        NP = Polys(N).NoOfPoints - 1
        For K = 2 To NP
            X1 = Polys(N).GPoints(K - 1).lon
            X2 = Polys(N).GPoints(K).lon
            X3 = Polys(N).GPoints(K + 1).lon
            Y1 = Polys(N).GPoints(K - 1).lat
            Y2 = Polys(N).GPoints(K).lat
            Y3 = Polys(N).GPoints(K + 1).lat
            UX = X2 - X1
            UY = Y2 - Y1
            VX = X3 - X2
            VY = Y3 - Y2
            D = UX * VY - UY * VX
            UX = UX * UX
            UY = UY * UY
            UX = UX + UY
            UX = Math.Sqrt(UX)
            VX = VX * VX
            VY = VY * VY
            VX = VX + VY
            VX = Math.Sqrt(VX)
            D = D / (UX * VX)
            D = Math.Abs(D)
            If D < PI_180 Then
                If Polys(N).NoOfPoints < 3 Then
                    DeletePoly(N)
                    Dirty = True
                Else
                    DeletePointInPoly(N, K)
                End If
                GoTo jump_here
            End If
        Next


    End Sub

    Friend Function TryThisPolyJoin(ByVal N1 As Integer) As Boolean

        Dim N As Integer

        TryThisPolyJoin = False

        For N = 1 To N1 - 1
            If JoinPolys(N, N1) Then
                TryThisPolyJoin = True
                RebuildDisplay()
                Beep()
                Exit Function
            End If
        Next N

        For N = N1 + 1 To NoOfPolys
            If JoinPolys(N, N1) Then
                TryThisPolyJoin = True
                RebuildDisplay()
                Beep()
                Exit Function
            End If
        Next N


    End Function

    Private Function JoinPolys(ByVal N1 As Integer, ByVal N2 As Integer) As Boolean

        '        ' different from JoinLines
        '        ' JoinPolys returns True if the Polys are joined!

        Dim M, N, NX As Integer
        Dim K1, K, K2 As Integer
        Dim N1T, N2T As Integer
        Dim N2B, N1B, N1A, N2A, NT As Integer
        Dim Flag As Integer
        Dim Common As Boolean
        Dim Lat, Lon As Double
        Dim DeltaLat, DeltaLon As Double

        Dim myPoly As GPoly

        JoinPolys = False

        If Polys(N1).Name <> Polys(N2).Name Then Exit Function
        If Polys(N1).Type <> Polys(N2).Type Then Exit Function
        If Polys(N1).Guid <> Polys(N2).Guid Then Exit Function

        On Error GoTo erro1

        If DisplayJoin Then
            DeltaLon = 3 / PixelsPerLonDeg
            DeltaLat = 3 / PixelsPerLatDeg
        Else
            DeltaLon = D255Lon
            DeltaLat = D255Lat
        End If

        N1T = Polys(N1).NoOfPoints
        N2T = Polys(N2).NoOfPoints

        MakePolyClockWise(N1)
        MakePolyClockWise(N2)


        ' both polys are now clockwise!
        ' ============================

        ' this gets (in N) the index of the first point in poly N1
        ' that does not coincide with any point in poly N2
        For N = 1 To N1T
            Common = False
            Lat = Polys(N1).GPoints(N).lat
            Lon = Polys(N1).GPoints(N).lon
            For M = 1 To N2T
                If System.Math.Abs(Lat - Polys(N2).GPoints(M).lat) < DeltaLat Then
                    If System.Math.Abs(Lon - Polys(N2).GPoints(M).lon) < DeltaLon Then
                        Common = True
                        Exit For
                    End If
                End If
            Next M
            If Not Common Then Exit For
        Next N

        N1A = N

        If N1A > N1T Then Exit Function

        If N1A > 1 Then CyclePoly(N1, N1A)

        ' now Point 1 is not on the common part!
        ' ======================================

        Flag = 0
Return_Here:
        Flag = Flag + 1
        For N = 1 To N1T
            For M = 1 To N2T
                If System.Math.Abs(Polys(N1).GPoints(N).lat - Polys(N2).GPoints(M).lat) < DeltaLat Then
                    If System.Math.Abs(Polys(N1).GPoints(N).lon - Polys(N2).GPoints(M).lon) < DeltaLon Then GoTo jump_here
                End If
            Next M
        Next N
jump_here:

        N1A = N
        N2A = M

        If N1A >= N1T Then Exit Function

        If N1A = 1 Then Exit Function

        K1 = N1A
        K2 = N2A
        Do
            K1 = K1 + 1
            K2 = K2 - 1
            If K1 > N1T Then
                K1 = 1
            End If
            If K2 < 1 Then
                K2 = N2T
            End If
            If System.Math.Abs(Polys(N1).GPoints(K1).lat - Polys(N2).GPoints(K2).lat) > DeltaLat Then
                N1B = K1 - 1
                N2B = K2 + 1
                Exit Do
            End If
            If System.Math.Abs(Polys(N1).GPoints(K1).lon - Polys(N2).GPoints(K2).lon) > DeltaLon Then
                N1B = K1 - 1
                N2B = K2 + 1
                Exit Do
            End If
        Loop

        If N1B = 0 Then N1B = N1T

        If N2B >= N2A Or N2B = 1 Then
            If Flag > 2 Then Exit Function
            CyclePoly(N2, N2A + 1)
            GoTo Return_Here
        End If

        NX = N1B - N1A
        NX = 2 * NX
        NT = N1T + N2T - NX

        'now copy poly N1 in myPoly
        ReDim myPoly.GPoints(N1T)
        For K = 1 To N1T
            myPoly.GPoints(K).lat = Polys(N1).GPoints(K).lat
            myPoly.GPoints(K).lon = Polys(N1).GPoints(K).lon
            myPoly.GPoints(K).alt = Polys(N1).GPoints(K).alt
        Next

        Polys(N1).NoOfPoints = NT
        ReDim Polys(N1).GPoints(NT)
        K = 1

        For M = 1 To N1A
            Polys(N1).GPoints(K).lat = myPoly.GPoints(M).lat
            Polys(N1).GPoints(K).lon = myPoly.GPoints(M).lon
            Polys(N1).GPoints(K).alt = myPoly.GPoints(M).alt
            K = K + 1
        Next M

        For M = N2A + 1 To N2T
            Polys(N1).GPoints(K).lat = Polys(N2).GPoints(M).lat
            Polys(N1).GPoints(K).lon = Polys(N2).GPoints(M).lon
            Polys(N1).GPoints(K).alt = Polys(N2).GPoints(M).alt
            K = K + 1
        Next M

        For M = 1 To N2B
            Polys(N1).GPoints(K).lat = Polys(N2).GPoints(M).lat
            Polys(N1).GPoints(K).lon = Polys(N2).GPoints(M).lon
            Polys(N1).GPoints(K).alt = Polys(N2).GPoints(M).alt
            K = K + 1
        Next M

        For M = N1B + 1 To N1T
            Polys(N1).GPoints(K).lat = myPoly.GPoints(M).lat
            Polys(N1).GPoints(K).lon = myPoly.GPoints(M).lon
            Polys(N1).GPoints(K).alt = myPoly.GPoints(M).alt
            K = K + 1
        Next M

        Polys(N1).Selected = True
        AddLatLonToPoly(N1)
        DeletePoly(N2)

        JoinPolys = True
        Dirty = True
        Exit Function
erro1:

    End Function

    Private Sub CyclePoly(ByVal N As Integer, ByVal N1 As Integer)

        Dim M, K, NP As Integer

        Dim myPoly As GPoly

        NP = Polys(N).NoOfPoints
        ReDim myPoly.GPoints(NP)

        For K = 1 To NP
            myPoly.GPoints(K).lat = Polys(N).GPoints(K).lat
            myPoly.GPoints(K).lon = Polys(N).GPoints(K).lon
            myPoly.GPoints(K).alt = Polys(N).GPoints(K).alt
        Next

        M = 1
        For K = N1 To NP
            Polys(N).GPoints(M).lat = myPoly.GPoints(K).lat
            Polys(N).GPoints(M).lon = myPoly.GPoints(K).lon
            Polys(N).GPoints(M).alt = myPoly.GPoints(K).alt
            M = M + 1
        Next K

        For K = 1 To N1 - 1
            Polys(N).GPoints(M).lat = myPoly.GPoints(K).lat
            Polys(N).GPoints(M).lon = myPoly.GPoints(K).lon
            Polys(N).GPoints(M).alt = myPoly.GPoints(K).alt
            M = M + 1
        Next K

    End Sub

    Friend Function MakePolyClockWise(ByVal P As Integer) As Boolean

        ' returns true if poly is modified

        Dim N, M, L, NP As Integer
        Dim Lat, LatN As Double
        Dim DX1, X1, Y1, DY1 As Double
        Dim DX2, X2, Y2, DY2 As Double
        Dim Y3, X3, CP As Double
        Dim M1, M3 As Integer
        Dim PT As GPoint

        MakePolyClockWise = False


        NP = Polys(P).NoOfPoints

        ' get southest point in M (if 2 then the right most)
        Lat = Polys(P).GPoints(1).lat
        M = 1
        For N = 2 To NP
            LatN = Polys(P).GPoints(N).lat
            If LatN <= Lat Then
                If LatN < Lat Then
                    M = N
                    Lat = LatN
                Else
                    If Polys(P).GPoints(N).lon > Polys(P).GPoints(M).lon Then
                        M = N
                        Lat = LatN
                    End If
                End If
            End If
        Next N

        ' form the vectors M-1>M  and M>M+1  (1>2 2>3)
        X2 = Polys(P).GPoints(M).lon
        Y2 = Polys(P).GPoints(M).lat
        If M = 1 Then
            M1 = NP
        Else
            M1 = M - 1
        End If
        X1 = Polys(P).GPoints(M1).lon
        Y1 = Polys(P).GPoints(M1).lat

        If M = NP Then
            M3 = 1
        Else
            M3 = M + 1
        End If
        X3 = Polys(P).GPoints(M3).lon
        Y3 = Polys(P).GPoints(M3).lat

        ' vector 1>2 in 1 and 2>3 in 2 then cross product in x3
        DX1 = X2 - X1
        DY1 = Y2 - Y1
        DX2 = X3 - X2
        DY2 = Y3 - Y2

        CP = DX1 * DY2 - DY1 * DX2

        If CP < 0 Then Exit Function ' is already clock wise

        L = 0
        'If CP = 0 Then      ' could be clockwise Horst email on 3 October
        Do While CP = 0 And L < 4
            If Y3 > Y1 Then
                X2 = X1
                Y2 = Y1
                M1 = M1 - 1
                If M1 = 0 Then M1 = NP
                X1 = Polys(P).GPoints(M1).lon
                Y1 = Polys(P).GPoints(M1).lat
            ElseIf Y1 > Y3 Then
                X2 = X3
                Y2 = Y3
                M3 = M3 + 1
                If M3 > NP Then M3 = 1
                X3 = Polys(P).GPoints(M3).lon
                Y3 = Polys(P).GPoints(M3).lat
            Else ' =
                X2 = X1
                Y2 = Y1
                M1 = M1 - 1
                If M1 = 0 Then M1 = NP
                X1 = Polys(P).GPoints(M1).lon
                Y1 = Polys(P).GPoints(M1).lat
                M3 = M3 + 1
                If M3 > NP Then M3 = 1
                X3 = Polys(P).GPoints(M3).lon
                Y3 = Polys(P).GPoints(M3).lat
            End If

            L = L + 1    ' this is a way to come out of the loop

            ' recalculate CP cross product
            DX1 = X2 - X1
            DY1 = Y2 - Y1
            DX2 = X3 - X2
            DY2 = Y3 - Y2
            CP = DX1 * DY2 - DY1 * DX2
            If CP < 0 Then Exit Function ' is already clock wise
        Loop

        L = NP + 1
        M = CInt(Int(L / 2))

        For N = 1 To M
            PT = Polys(P).GPoints(N)
            Polys(P).GPoints(N) = Polys(P).GPoints(L - N)
            Polys(P).GPoints(L - N) = PT
        Next N

        MakePolyClockWise = True

    End Function

    Friend Sub MakePolyAntiClockWise(ByVal P As Integer)

        Dim N, M, L, NP As Integer
        Dim Lat, LatN As Double
        Dim DX1, X1, Y1, DY1 As Double
        Dim DX2, X2, Y2, DY2 As Double
        Dim Y3, X3, CP As Double
        Dim M1, M3 As Integer
        Dim PT As GPoint

        NP = Polys(P).NoOfPoints

        ' get southest point in M (if 2 then the right most)
        Lat = Polys(P).GPoints(1).lat
        M = 1
        For N = 2 To NP
            LatN = Polys(P).GPoints(N).lat
            If LatN <= Lat Then
                If LatN < Lat Then
                    M = N
                    Lat = LatN
                Else
                    If Polys(P).GPoints(N).lon > Polys(P).GPoints(M).lon Then
                        M = N
                        Lat = LatN
                    End If
                End If
            End If
        Next N

        ' form the vectors M-1>M  and M>M+1  (1>2 2>3)
        X2 = Polys(P).GPoints(M).lon
        Y2 = Polys(P).GPoints(M).lat
        If M = 1 Then
            M1 = NP
        Else
            M1 = M - 1
        End If
        X1 = Polys(P).GPoints(M1).lon
        Y1 = Polys(P).GPoints(M1).lat

        If M = NP Then
            M3 = 1
        Else
            M3 = M + 1
        End If
        X3 = Polys(P).GPoints(M3).lon
        Y3 = Polys(P).GPoints(M3).lat

        ' vector 1>2 in 1 and 2>3 in 2 then cross product in x3
        DX1 = X2 - X1
        DY1 = Y2 - Y1
        DX2 = X3 - X2
        DY2 = Y3 - Y2

        CP = DX1 * DY2 - DY1 * DX2

        If CP > 0 Then Exit Sub ' is already anticlock wise

        L = 0
        'If CP = 0 Then      ' could be clockwise Horst email on 3 October
        Do While CP = 0 And L < 4
            If Y3 > Y1 Then
                X2 = X1
                Y2 = Y1
                M1 = M1 - 1
                If M1 = 0 Then M1 = NP
                X1 = Polys(P).GPoints(M1).lon
                Y1 = Polys(P).GPoints(M1).lat
            ElseIf Y1 > Y3 Then
                X2 = X3
                Y2 = Y3
                M3 = M3 + 1
                If M3 > NP Then M3 = 1
                X3 = Polys(P).GPoints(M3).lon
                Y3 = Polys(P).GPoints(M3).lat
            Else ' =
                X2 = X1
                Y2 = Y1
                M1 = M1 - 1
                If M1 = 0 Then M1 = NP
                X1 = Polys(P).GPoints(M1).lon
                Y1 = Polys(P).GPoints(M1).lat
                M3 = M3 + 1
                If M3 > NP Then M3 = 1
                X3 = Polys(P).GPoints(M3).lon
                Y3 = Polys(P).GPoints(M3).lat
            End If

            L = L + 1 ' this is a way to come out of the loop

            ' recalculate CP cross product
            DX1 = X2 - X1
            DY1 = Y2 - Y1
            DX2 = X3 - X2
            DY2 = Y3 - Y2
            CP = DX1 * DY2 - DY1 * DX2
            If CP > 0 Then Exit Sub ' is already anticlock wise
        Loop

        L = NP + 1
        M = CInt(Int(L / 2))

        For N = 1 To M
            PT = Polys(P).GPoints(N)
            Polys(P).GPoints(N) = Polys(P).GPoints(L - N)
            Polys(P).GPoints(L - N) = PT
        Next N

    End Sub

    Friend Sub CheckPolyMove(ByVal UP As Boolean)

        Dim L As Integer

        Polys(CheckPoly).GPoints(CheckPolyPt).Selected = False

        If UP Then
            L = CheckPoly + 1
            If L = NoOfPolys + 1 Then L = 1
        Else
            L = CheckPoly - 1
            If L = 0 Then L = NoOfPolys
        End If

        CheckPoly = L
        CheckPolyPt = 1

        RefreshCheckPolyPt()

    End Sub

    Friend Sub CheckPolyPtMove(ByVal UP As Boolean)

        Dim NP, P As Integer

        NP = Polys(CheckPoly).NoOfPoints

        Polys(CheckPoly).GPoints(CheckPolyPt).Selected = False

        If UP Then
            P = CheckPolyPt + 1
            If P > NP Then P = 1
        Else
            P = CheckPolyPt - 1
            If P = 0 Then P = NP
        End If

        CheckPolyPt = P
        RefreshCheckPolyPt()

    End Sub

    Friend Sub DeleteCheckPolyPt()

        If Polys(CheckPoly).NoOfPoints < 4 Then
            CheckPoly = 0
            Exit Sub
        End If

        DeletePointInPoly(CheckPoly, CheckPolyPt)

        If CheckPolyPt > Polys(CheckPoly).NoOfPoints Then
            CheckPolyPt = Polys(CheckPoly).NoOfPoints
        End If

        RefreshCheckPolyPt()

    End Sub

    Private Sub RefreshCheckPolyPt()

        Polys(CheckPoly).GPoints(CheckPolyPt).Selected = True
        LatDispCenter = Polys(CheckPoly).GPoints(CheckPolyPt).lat
        LonDispCenter = Polys(CheckPoly).GPoints(CheckPolyPt).lon
        SetDispCenter(0, 0)
        RebuildDisplay()

    End Sub

    Friend Sub MakePolyTexString(ByVal P As Integer, ByVal Reset_Renamed As Boolean)

        Dim N, NP As Integer
        Dim x, y As Double
        Dim K, J As Integer

        Dim LatMin, LonMin As Double
        Dim LatMax, LonMax As Double

        NP = Polys(P).NoOfPoints

        ' first checks if the number of points is less or equal than the ones
        ' on the list in PolyTexString

        ' but if Reset = True then forces Reset

        If Reset_Renamed Then GoTo reset_string
        J = 1
        For N = 1 To NP
            K = InStr(J, PolyTexString, "//")
            If K = 0 Then GoTo reset_string
            J = K + 2
        Next N

        Exit Sub

reset_string:

        PolyTexString = ""

        LatMin = 90 : LatMax = -90 : LonMin = 180 : LonMax = -180

        For N = 1 To NP
            x = Polys(P).GPoints(N).lat
            If x > LatMax Then LatMax = x
            If x < LatMin Then LatMin = x
            x = Polys(P).GPoints(N).lon
            If x > LonMax Then LonMax = x
            If x < LonMin Then LonMin = x
        Next N

        LatMax = LatMax - LatMin
        LonMax = LonMax - LonMin
        x = LonMax / LatMax
        If x >= 1 Then x = 1
        x = x * 256.0# / LonMax
        y = LatMax / LonMax
        If y >= 1 Then y = 1
        y = y * 256.0# / LatMax

        For N = 1 To NP
            K = CInt(x * (Polys(P).GPoints(N).lon - LonMin))
            PolyTexString = PolyTexString & CStr(K) & ","
            K = CInt(y * (Polys(P).GPoints(N).lat - LatMin))
            PolyTexString = PolyTexString & CStr(K) & "//"
        Next N

    End Sub

    Friend Sub MakeBGLTexLines(ByVal CopyBGLs As Boolean)

        Dim N As Integer
        Dim A, B As String

        Dim IsStanding As Boolean = False
        Dim IsLying As Boolean = False

        Dim H_NLat As Double = -90
        Dim H_SLat As Double = 90
        Dim H_WLon As Double = 180
        Dim H_ELon As Double = -180

        For N = 1 To NoOfLines
            If Lines(N).Selected Then
                A = Lines(N).Type
                If A = "" Then GoTo next_N0
                B = Mid(A, 1, 5)
                If B = "TEX|S" Then
                    IsStanding = True

                ElseIf B = "TEX|L" Then
                    IsLying = True
                    If Lines(N).ELON > H_ELon Then H_ELon = Lines(N).ELON
                    If Lines(N).WLON < H_WLon Then H_WLon = Lines(N).WLON
                    If Lines(N).NLAT > H_NLat Then H_NLat = Lines(N).NLAT
                    If Lines(N).SLAT < H_SLat Then H_SLat = Lines(N).SLAT
                End If
            End If
next_N0:
        Next N

        If IsLying Then MakeBGLLyingLines(CopyBGLs, H_NLat, H_SLat, H_WLon, H_ELon)
        If IsStanding Then MakeBglStandingLines(CopyBGLs)

    End Sub

    Private X_lat As Double
    Private X_lon As Double
    Private Sub MakeBglStandingLines(ByVal CopyBGLs As Boolean)

        Dim N As Integer
        Dim A, B, template, header, material, st As String
        Dim L() As Boolean    ' to indicate lines of this type
        ReDim L(NoOfLines)

        Dim Complex As Integer = 0
        Dim Night, Tiled As Integer

        Dim myFileBase As String = ProjectName & "_TESL"
        myFileBase = Replace(myFileBase, " ", "_")

        Dim myFile As String = My.Application.Info.DirectoryPath & "\tools\work\" & myFileBase
        Dim myFileXN As String
        Dim myFileXLM As String = myFile & ".xml"

        Dim settings As XmlWriterSettings = New XmlWriterSettings()
        settings.Indent = True
        settings.Encoding = Encoding.GetEncoding(28591)
        settings.NewLineOnAttributes = True

        Dim writer As XmlWriter = XmlWriter.Create(myFileXLM, settings)
        writer.WriteStartDocument()
        writer.WriteComment("Created by SBuilderX on " & Now)
        writer.WriteStartElement("FSData")
        writer.WriteAttributeString("version", "9.0")
        writer.WriteAttributeString("xmlns", "xsi", Nothing, "http://www.w3.org/2001/XMLSchema-instance")
        writer.WriteAttributeString("noNamespaceSchemaLocation", "http://www.w3.org/2001/XMLSchema-instance", "bglcomp.xsd")
        writer.WriteComment("Standing Textured Lines FSX Models")

        header = "xof 0302txt 0032" & vbCrLf & vbCrLf & "// Direct3D X file created by SBuilderX on " & Now.ToString & vbCrLf & vbCrLf
        template = My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & "\tools\x_templates.txt")
        material = My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & "\tools\x_material.txt")

        For N = 1 To NoOfLines
            L(N) = False
            If Lines(N).Selected Then
                A = Lines(N).Type
                If A = "" Then GoTo next_N
                B = Mid(A, 1, 5)
                If B <> "TEX|S" Then GoTo next_N
                ' Ok, go and get Tiled, Night Texture ....
                L(N) = True
                Set_Tex_T_N_C(Tiled, Night, Complex, N)
                Dim G As Guid = Guid.NewGuid  ' create a random Guid
                Dim myGuid As String = G.ToString("B")
                X_lat = (Lines(N).NLAT + Lines(N).SLAT) / 2
                X_lon = (Lines(N).ELON + Lines(N).WLON) / 2
                writer.WriteComment("Line # " & N.ToString)
                writer.WriteStartElement("SceneryObject")
                writer.WriteAttributeString("lat", Trim(Str(X_lat)))
                writer.WriteAttributeString("lon", Trim(Str(X_lon)))
                writer.WriteAttributeString("alt", "0")
                writer.WriteAttributeString("altitudeIsAgl", Get_Is__AGL(N))
                writer.WriteAttributeString("pitch", "0")
                writer.WriteAttributeString("bank", "0")
                writer.WriteAttributeString("heading", "0")
                writer.WriteAttributeString("imageComplexity", GetComplex(Complex))
                writer.WriteStartElement("LibraryObject")
                writer.WriteAttributeString("name", myGuid)
                writer.WriteAttributeString("scale", "1.0")
                writer.WriteEndElement()
                writer.WriteFullEndElement()
                writer.WriteStartElement("ModelData")
                writer.WriteAttributeString("sourceFile", myFileBase & N.ToString & ".mdl")
                writer.WriteEndElement()

                myFileXN = myFile & N.ToString & ".X"
                My.Computer.FileSystem.WriteAllText(myFileXN, header, False, Encoding.ASCII)
                My.Computer.FileSystem.WriteAllText(myFileXN, template, True)
                My.Computer.FileSystem.WriteAllText(myFileXN, Make_X_Header(), True)
                
                st = "GuidToName {" & vbCrLf
                st = st & "   """ & myGuid & """;" & vbCrLf
                st = st & "   ""StandingTexLine"";" & vbCrLf & "}" & vbCrLf & vbCrLf
                My.Computer.FileSystem.WriteAllText(myFileXN, st, True)
                My.Computer.FileSystem.WriteAllText(myFileXN, Make_X_Frame(), True)
                My.Computer.FileSystem.WriteAllText(myFileXN, Make_X_Mesh(N), True)
                My.Computer.FileSystem.WriteAllText(myFileXN, Make_X_MaterialStart(N, Night), True)
                My.Computer.FileSystem.WriteAllText(myFileXN, material, True)
                My.Computer.FileSystem.WriteAllText(myFileXN, Make_X_MaterialEnd(Night), True)
                My.Computer.FileSystem.WriteAllText(myFileXN, Make_X_MeshNormals(N), True)
                My.Computer.FileSystem.WriteAllText(myFileXN, Make_X_MeshTextureCoords(N, Tiled), True)

            End If
next_N:
        Next N

        writer.WriteFullEndElement()
        writer.Close()

        ChDrive(My.Application.Info.DirectoryPath)
        ChDir(My.Application.Info.DirectoryPath & "\tools\")

        ' make the mdls from the X's
        For N = 1 To NoOfLines
            If L(N) Then
                A = "XToMdl work\" & myFileBase & N.ToString & ".X"
                ExecCmd(A)
            End If
        Next

        ' delete BGL File
        Dim BGLFile As String = My.Application.Info.DirectoryPath & "\tools\work\" & myFileBase & ".BGL"
        If File.Exists(BGLFile) Then File.Delete(BGLFile)

        A = My.Application.Info.DirectoryPath & "\tools\bglcomp.exe"
        B = "work\" & myFileBase & ".xml"

        Dim myProcess As New Process
        myProcess = Process.Start(A, B)
        myProcess.WaitForExit()
        myProcess.Dispose()

        If Not File.Exists(BGLFile) Then
            A = "BGLComp could not produce the file" & vbCrLf & BGLFile & vbCrLf
            A = A & "Try to compile the file ..\tools\" & B & " in a MSDOS window" & vbCrLf
            A = A & "to read the error report!"
            MsgBox(A, MsgBoxStyle.Critical)
        End If

        If Not CopyBGLs Then Exit Sub

        Try
            A = My.Application.Info.DirectoryPath & "\tools\work\" & myFileBase & ".BGL"
            If File.Exists(A) Then
                File.Copy(A, BGLProjectFolder & "\" & myFileBase & ".BGL", True)
            End If
        Catch ex As Exception
            MsgBox("Copying BGL files failed! Try to close FSX.", MsgBoxStyle.Exclamation)
        End Try


    End Sub

    Private Function Get_Is__AGL(ByVal N As Integer) As String

        Get_Is__AGL = "TRUE"
        Dim K As Integer
        For K = 1 To Lines(N).NoOfPoints
            If Lines(N).GLPoints(K).alt <> 0 Then
                Get_Is__AGL = "FALSE"
                Exit Function
            End If
        Next

    End Function

    Private Function Make_X_Header() As String

        Dim A As New StringBuilder
        A.AppendLine("Header {")
        A.AppendLine("   1;")
        A.AppendLine("   0;")
        A.AppendLine("   1;")
        A.AppendLine("}").AppendLine()
        Return A.ToString

    End Function


    Private Function Make_X_Frame() As String

        Dim A As New StringBuilder(200)
        A.AppendLine("Frame masterfrm {")
        A.AppendLine("FrameTransformMatrix {")
        A.AppendLine("   1.0, 0.0, 0.0, 0.0,")
        A.AppendLine("   0.0, 0.0, 1.0, 0.0,")
        A.AppendLine("   0.0, 1.0, 0.0, 0.0,")
        A.AppendLine("   0.0, 0.0, 0.0, 1.0;")
        A.AppendLine("}").AppendLine()
        Return A.ToString

    End Function

    Private Function Make_X_MaterialStart(ByVal L As Integer, ByVal Night As Integer) As String

        Dim P As Integer = 4 * (Lines(L).NoOfPoints - 1)

        Dim N As Integer = InStr(PolyTex, ".")
        Dim base As String = Mid(PolyTex, 1, N - 1)
        Dim ext As String = Mid(PolyTex, N)

        Dim A As New StringBuilder(500)

        A.AppendLine("MeshMaterialList {")
        A.AppendLine("1;")
        A.AppendLine("" & P.ToString & ";")
        For N = 2 To P
            A.AppendLine("0,")
        Next
        A.AppendLine("0;")
        A.AppendLine("Material mat2 {")
        A.AppendLine("  1.000; 1.000; 1.000; 1.000;;")
        A.AppendLine("  0.000000;")
        A.AppendLine("  0.000; 0.000; 0.000;;")
        A.AppendLine("  0.000; 0.000; 0.000;;")
        A.AppendLine("  TextureFileName {")
        A.AppendLine("    """ & base & ext & """;" & vbCrLf & "  }")
        A.AppendLine("  DiffuseTextureFileName  {")
        A.AppendLine("    """ & base & ext & """;" & vbCrLf & "  }")
        If Night = 1 Then
            A.AppendLine("  EmissiveTextureFileName  {")
            A.AppendLine("    """ & base & "_lm" & ext & """;" & vbCrLf & "  }")
        End If

        Return A.ToString

    End Function

    Private Function Make_X_MaterialEnd(ByVal Night As Integer) As String

        Dim N As Integer = InStr(PolyTex, ".")
        Dim base As String = Mid(PolyTex, 1, N - 1)
        Dim ext As String = Mid(PolyTex, N)

        Dim A As New StringBuilder(200)
        A.AppendLine("    DiffuseTextureFileName  {")
        A.AppendLine("      """ & base & ext & """;" & vbCrLf & "    }")
        If Night = 1 Then
            A.AppendLine("    EmissiveTextureFileName  {")
            A.AppendLine("      """ & base & "_lm" & ext & """;" & vbCrLf & "    }")
        End If
        A.AppendLine("  }" & vbCrLf & "}" & vbCrLf & "}" & vbCrLf)
        Return A.ToString

    End Function
    Private X_MetPerDegLon As Double
    Private X_MetPerDegLat As Double
    Private Function Make_X_Mesh(ByVal N As Integer) As String

        Dim K, P As Integer
        X_MetPerDegLon = MetersPerDegLon(X_lat)
        X_MetPerDegLat = MetersPerDegLat
        Dim X As String = ""
        Dim Y As String = ""
        Dim ZL As String = ""
        Dim ZH As String = ""

        Dim A As New StringBuilder(32000)
        Dim B As String = "Mesh Part2 {" & vbCrLf
        B = B & CStr(8 * (Lines(N).NoOfPoints - 1)) & ";" & vbCrLf

        Make_X_XYZ(X, Y, ZL, ZH, N, 1)
        A.AppendLine(X & "; " & Y & "; " & ZH & ";,")
        A.AppendLine(X & "; " & Y & "; " & ZL & ";,")
        For K = 2 To Lines(N).NoOfPoints - 1
            Make_X_XYZ(X, Y, ZL, ZH, N, K)
            A.AppendLine(X & "; " & Y & "; " & ZL & ";,")
            A.AppendLine(X & "; " & Y & "; " & ZH & ";,")
            A.AppendLine(X & "; " & Y & "; " & ZH & ";,")
            A.AppendLine(X & "; " & Y & "; " & ZL & ";,")
        Next
        Make_X_XYZ(X, Y, ZL, ZH, N, Lines(N).NoOfPoints)
        A.AppendLine(X & "; " & Y & "; " & ZL & ";,")
        A.Append(X & "; " & Y & "; " & ZH)
        A.AppendLine(";," & vbCrLf & A.ToString & ";;" & vbCrLf)
        A.Insert(0, B)

        P = 0

        A.AppendLine(CStr(4 * (Lines(N).NoOfPoints - 1)) & ";")

        For K = 1 To Lines(N).NoOfPoints - 1
            A.AppendLine("3; " & CStr(P) & ", " & CStr(P + 2) & ", " & CStr(P + 1) & ";,")
            A.AppendLine("3; " & CStr(P) & ", " & CStr(P + 3) & ", " & CStr(P + 2) & ";,")
            P = P + 4
        Next

        For K = 1 To Lines(N).NoOfPoints - 2
            A.AppendLine("3; " & CStr(P) & ", " & CStr(P + 1) & ", " & CStr(P + 2) & ";,")
            A.AppendLine("3; " & CStr(P) & ", " & CStr(P + 2) & ", " & CStr(P + 3) & ";,")
            P = P + 4
        Next
        A.AppendLine("3; " & CStr(P) & ", " & CStr(P + 1) & ", " & CStr(P + 2) & ";,")
        A.AppendLine("3; " & CStr(P) & ", " & CStr(P + 2) & ", " & CStr(P + 3) & ";;" & vbCrLf)

        Return A.ToString


    End Function

    Private Function Make_X_MeshNormals(ByVal N As Integer) As String

        Dim X1 As String = ""
        Dim Z1 As String = ""
        Dim X2 As String = ""
        Dim Z2 As String = ""
        Dim K, P As Integer
        Dim B As String = ""

        Dim A1 As New StringBuilder(48000)
        A1.AppendLine("MeshNormals {")
        A1.AppendLine(CStr(8 * (Lines(N).NoOfPoints - 1)) & ";")
        Dim A2 As New StringBuilder(16000)

        For K = 1 To Lines(N).NoOfPoints - 2
            Make_X_XZN(X1, Z1, X2, Z2, N, K)
            B = X1 & "; " & Z1 & "; " & "0.000;,"
            A1.AppendLine(B).AppendLine(B).AppendLine(B).AppendLine(B)
            B = X2 & "; " & Z2 & "; " & "0.000;,"
            A2.AppendLine(B).AppendLine(B).AppendLine(B).AppendLine(B)
        Next
        Make_X_XZN(X1, Z1, X2, Z2, N, Lines(N).NoOfPoints - 1)
        B = X1 & "; " & Z1 & "; " & "0.000;,"
        A1.AppendLine(B).AppendLine(B).AppendLine(B).AppendLine(B)
        B = X2 & "; " & Z2 & "; " & "0.000;,"
        A2.AppendLine(B).AppendLine(B).AppendLine(B)
        A2.AppendLine(X2 & "; " & Z2 & "; " & "0.000;;")

        A1.AppendLine(A2.ToString)
        P = 0

        A1.AppendLine(CStr(4 * (Lines(N).NoOfPoints - 1)) & ";")

        For K = 1 To Lines(N).NoOfPoints - 1
            A1.AppendLine("3; " & CStr(P) & ", " & CStr(P + 2) & ", " & CStr(P + 1) & ";,")
            A1.AppendLine("3; " & CStr(P) & ", " & CStr(P + 3) & ", " & CStr(P + 2) & ";,")
            P = P + 4
        Next

        For K = 1 To Lines(N).NoOfPoints - 2
            A1.AppendLine("3; " & CStr(P) & ", " & CStr(P + 1) & ", " & CStr(P + 2) & ";,")
            A1.AppendLine("3; " & CStr(P) & ", " & CStr(P + 2) & ", " & CStr(P + 3) & ";,")
            P = P + 4
        Next
        A1.AppendLine("3; " & CStr(P) & ", " & CStr(P + 1) & ", " & CStr(P + 2) & ";,")
        A1.AppendLine("3; " & CStr(P) & ", " & CStr(P + 2) & ", " & CStr(P + 3) & ";;" & vbCrLf & "}" & vbCrLf)

        Return A1.ToString

    End Function

    Private Function Make_X_MeshTextureCoords(ByVal N As Integer, ByVal Tiled As Integer) As String

        Dim K As Integer
        Dim NP As Integer = Lines(N).NoOfPoints
        Dim TU() As Single
        TU = Make_TU_Tiling(N, Tiled)

        Dim A1 As New StringBuilder(48000)
        A1.AppendLine("MeshTextureCoords {")
        A1.AppendLine(CStr(8 * (Lines(N).NoOfPoints - 1)) & ";")
        Dim A2 As New StringBuilder(16000)

        For K = 1 To Lines(N).NoOfPoints - 2
            A1.AppendLine(Format_TU(TU(K)) & "; 0.000;,")
            A1.AppendLine(Format_TU(TU(K)) & "; 1.000;,")
            A1.AppendLine(Format_TU(TU(K + 1)) & "; 1.000;,")
            A1.AppendLine(Format_TU(TU(K + 1)) & "; 0.000;,")
            A2.AppendLine(Format_TU(TU(K)) & "; 0.000;,")
            A2.AppendLine(Format_TU(TU(K)) & "; 1.000;,")
            A2.AppendLine(Format_TU(TU(K + 1)) & "; 1.000;,")
            A2.AppendLine(Format_TU(TU(K + 1)) & "; 0.000;,")
        Next
        A1.AppendLine(Format_TU(TU(K)) & "; 0.000;,")
        A1.AppendLine(Format_TU(TU(K)) & "; 1.000;,")
        A1.AppendLine(Format_TU(TU(K + 1)) & "; 1.000;,")
        A1.AppendLine(Format_TU(TU(K + 1)) & "; 0.000;,")
        A2.AppendLine(Format_TU(TU(K)) & "; 0.000;,")
        A2.AppendLine(Format_TU(TU(K)) & "; 1.000;,")
        A2.AppendLine(Format_TU(TU(K + 1)) & "; 1.000;,")
        A2.AppendLine(Format_TU(TU(K + 1)) & "; 0.000;;")  ' ;;
        A2.AppendLine("}").AppendLine("}").AppendLine("}")
        A1.AppendLine(A2.ToString)

        Return A1.ToString


    End Function

    Private Function Format_TU(ByVal X As Single) As String

        Format_TU = Format(X, "0.000")
        Format_TU = Replace(Format_TU, ",", ".")

    End Function

    Private Sub Make_X_XYZ(ByRef X As String, ByRef Y As String, ByRef H0 As String, ByRef H1 As String, ByVal N As Integer, ByVal K As Integer)

        Dim V As Double = (Lines(N).GLPoints(K).lon - X_lon) * X_MetPerDegLon
        X = Format(V, "0.000")
        X = Replace(X, ",", ".")

        V = (Lines(N).GLPoints(K).lat - X_lat) * X_MetPerDegLat
        Y = Format(V, "0.000")
        Y = Replace(Y, ",", ".")

        V = Lines(N).GLPoints(K).alt
        H0 = Format(V, "0.000")
        H0 = Replace(H0, ",", ".")

        V = V + Lines(N).GLPoints(K).wid
        H1 = Format(V, "0.000")
        H1 = Replace(H1, ",", ".")

    End Sub

    Private Sub Make_X_XZN(ByRef X1 As String, ByRef Z1 As String, ByRef X2 As String, ByRef Z2 As String, ByVal N As Integer, ByVal K As Integer)

        Dim U As Double = Lines(N).GLPoints(K + 1).lon - Lines(N).GLPoints(K).lon
        Dim V As Double = Lines(N).GLPoints(K + 1).lat - Lines(N).GLPoints(K).lat
        Dim X As Double = U * U + V * V
        X = Math.Sqrt(X)
        U = U / X   ' normalize rotate clockwise
        V = V / X

        X = U
        U = V   ' rotate clockwise
        V = -X

        X1 = Format(U, "0.000")
        X1 = Replace(X1, ",", ".")

        Z1 = Format(V, "0.000")
        Z1 = Replace(Z1, ",", ".")

        X2 = Format(-U, "0.000")
        X2 = Replace(X2, ",", ".")

        Z2 = Format(-V, "0.000")
        Z2 = Replace(Z2, ",", ".")

    End Sub

    Private Sub Set_Tex_T_N_C(ByRef Tiled As Integer, ByRef Night As Integer, ByRef Complex As Integer, ByVal Line As Integer)

        Dim A, B As String
        Dim M As Integer

        ' sets global variable PolyTex

        A = Mid(Lines(Line).Type, 14)   ' TEX|Standing|   is removed
        M = InStr(1, A, "|")
        PolyTex = Mid(A, 1, M - 1)
        A = Mid(A, M + 1)
        M = InStr(1, A, "|")
        A = Mid(A, M + 1)
        M = InStr(1, A, "|")
        A = Mid(A, M + 1)
        M = InStr(1, A, "|")
        B = Mid(A, 1, M - 1)
        Complex = CInt(B)
        A = Mid(A, M + 1)
        M = InStr(1, A, "|")
        B = Mid(A, 1, M - 1)
        Night = 0
        If CBool(B) Then Night = 1
        A = Mid(A, M + 1, 1)
        Tiled = 0
        If A = "T" Then Tiled = 1
        If A = "F" Then Tiled = 2

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

    Private Sub MakeBglLyingLines(ByVal CopyBGLs As Boolean, ByVal H_NLat As Double, _
                       ByVal H_SLat As Double, ByVal H_WLon As Double, ByVal H_ELon As Double)


        Dim myFile As String
        Dim A, B As String
        Dim M As Integer
        Dim N As Integer

        Dim Priority, Visibility, Complex As String
        Dim Night, Tiled As Integer

        Dim LZL As Double_XYZ

        Dim Longitude, Latitude, Altitude As String

        myFile = ProjectName & "_TELL"
        myFile = Replace(myFile, " ", "_")
        FileOpen(3, My.Application.Info.DirectoryPath & "\tools\work\" + myFile + ".scm", OpenMode.Output)

        A = "Header( 1 "
        A = A & Str(Int(H_NLat + 1.5)) & " "
        A = A & Str(Int(H_SLat - 0.5)) & " "
        A = A & Str(Int(H_ELon + 1.5)) & " "
        A = A & Str(Int(H_WLon - 0.5)) & " )"
        PrintLine(3, A)
        A = "LatRange( "
        A = A & Str(Int(H_SLat - 0.5)) & " "
        A = A & Str(Int(H_NLat + 1.5)) & " )"
        PrintLine(3, A)
        PrintLine(3)

        For N = 1 To NoOfLines
            If Lines(N).Selected Then
                A = Lines(N).Type
                If A = "" Then GoTo next_N
                B = Mid(A, 1, 5)
                If B <> "TEX|L" Then GoTo next_N
                A = Mid(A, 11)   ' TEX|Lying|   is removed
                M = InStr(1, A, "|")
                PolyTex = Mid(A, 1, M - 1)
                A = Mid(A, M + 1)
                M = InStr(1, A, "|")
                B = Mid(A, 1, M - 1)
                Priority = CStr(CInt(B))
                A = Mid(A, M + 1)
                M = InStr(1, A, "|")
                B = Mid(A, 1, M - 1)
                Visibility = CStr(CInt(B))

                A = Mid(A, M + 1)
                M = InStr(1, A, "|")
                B = Mid(A, 1, M - 1)
                Complex = CStr(CInt(B))

                A = Mid(A, M + 1)
                M = InStr(1, A, "|")
                B = Mid(A, 1, M - 1)

                Night = 0
                If CBool(B) Then Night = 1
                A = Mid(A, M + 1, 1)

                Tiled = 0
                If A = "T" Then Tiled = 1

                MakePoly_0_FromLine(N)
                LZL = GetLatZLonTexPoly(0)
                AuxLatPoly = LZL.Y
                Latitude = Format(AuxLatPoly, "0.00000000")
                AuxLonPoly = LZL.X
                Longitude = Format(AuxLonPoly, "0.00000000")
                AuxZPoly = LZL.Z
                Altitude = Format(AuxZPoly, "0.00000000")

                Latitude = Replace(Latitude, ",", ".")
                Longitude = Replace(Longitude, ",", ".")
                Altitude = Replace(Altitude, ",", ".")
                Visibility = Replace(Visibility, ",", ".")

                A = "; Textured Line #" & Str(N) & vbCrLf & vbCrLf
                A = A & "Area( 5 "
                A = A & Latitude & " " & Longitude & " 50 )"
                PrintLine(3, A)

                A = "IfVarRange( : 346 " & Complex & " 5 )"
                PrintLine(3, A)

                A = "LayerCall( :lcall " & Priority & " )"
                PrintLine(3, A)
                A = "Jump( : )" & vbCrLf & ":lcall"
                PrintLine(3, A)

                A = "RefPoint( 2 :return 1 " & Latitude & " " & Longitude
                A = A & " E= " & Altitude & " v1= " & Visibility & " v2= " & GetV2(0) & " )"
                PrintLine(3, A)

                A = "BGLVersion( 0800 )"
                PrintLine(3, A)

                If PolyTex <> "" Then
                    A = FillTextureList(Night)
                    PrintLine(3, A)
                End If

                A = FillMaterialList(0)
                PrintLine(3, A)

                A = FillVextexList_0(N, Tiled)
                PrintLine(3, A)

                If PolyTex <> "" Then
                    A = "SetMaterial( 0 0 )"
                Else
                    A = "SetMaterial( 0 -1 )"
                End If
                PrintLine(3, A)

                A = FillDrawTriList_0(N)
                PrintLine(3, A)
                A = "EndVersion"
                PrintLine(3, A)

                A = ":return" & vbCrLf & "Return" & vbCrLf & "EndA" & vbCrLf
                PrintLine(3, A)

            End If
next_N:
        Next N

        FileClose(3)

        ' delete BGL file
        A = My.Application.Info.DirectoryPath & "\tools\work\" + myFile + ".BGL"
        If File.Exists(A) Then File.Delete(A)

        ChDrive(My.Application.Info.DirectoryPath)
        ChDir(My.Application.Info.DirectoryPath & "\tools\")

        A = "scasm work\" + myFile + ".scm -s -l"
        N = ExecCmd(A)

        If N > 0 Then
            A = "There was a compilation error in the textured" & vbCrLf
            A = A & " lines! Do you want to read a SCASM report?"
            N = MsgBox(A, MsgBoxStyle.OkCancel)
            If N = 1 Then
                A = "notepad SCAERROR.LOG"
                N = ExecCmd(A)
            End If
            Exit Sub
        End If

        If Not CopyBGLs Then Exit Sub

        Try
            A = My.Application.Info.DirectoryPath & "\tools\work\" & myFile & ".BGL"
            If File.Exists(A) Then
                File.Copy(A, BGLProjectFolder & "\" & myFile & ".BGL", True)
            End If
        Catch ex As Exception
            MsgBox("Copying BGL files failed! Try to close FSX.", MsgBoxStyle.Exclamation)
        End Try

    End Sub


    Private Sub MakePoly_0_FromLine(ByVal N As Integer)

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

        Polys(0).NoOfPoints = 2 * NP
        Polys(0).Name = "0"
        Polys(0).Guid = "0"
        Polys(0).NoOfChilds = 0
        Polys(0).Color = DefaultPolyColor

        ReDim Polys(0).GPoints(2 * NP)

        M = 1
        For P = 1 To NP
            Polys(0).GPoints(M).lat = PL(P).Y / MetersPerDegLat
            Polys(0).GPoints(M).lon = PL(P).X / MetersPerDegLon(LatDispCenter)
            Polys(0).GPoints(M).alt = Lines(N).GLPoints(P).alt
            M = M + 1
        Next

        For P = NP To 1 Step -1
            Polys(0).GPoints(M).lat = PR(P).Y / MetersPerDegLat
            Polys(0).GPoints(M).lon = PR(P).X / MetersPerDegLon(LatDispCenter)
            Polys(0).GPoints(M).alt = Lines(N).GLPoints(P).alt
            M = M + 1
        Next

    End Sub

    Private Function FillVextexList_0(ByVal N As Integer, ByVal Tiled As Integer) As String

        'Dim K, J As Integer
        'Dim NP As Integer = Lines(N).NoOfPoints
        'Dim TU(NP) As Single
        'Dim X, Y, Z As Double

        'If Tiled = 0 Then

        '    For K = 1 To NP
        '        TU(K) = CSng(K - 1)
        '    Next

        'End If

        'If Tiled = 1 Then

        '    Dim L(NP) As Double
        '    L(1) = 0
        '    Dim W As Double = Lines(N).GLPoints(1).wid
        '    For K = 2 To NP
        '        L(K) = L(K - 1) + GetDistanceP1P2(N, K - 1, K)
        '        W = W + Lines(N).GLPoints(K).wid
        '    Next
        '    W = W / NP

        '    Dim R As Double = GetImageRatio()

        '    TU(1) = 0
        '    For K = 2 To NP
        '        X = L(K) - L(K - 1)
        '        X = X / W
        '        X = X / R
        '        TU(K) = CSng(X) + TU(K - 1)
        '    Next

        'End If


        Dim K, J As Integer
        Dim NP As Integer = Lines(N).NoOfPoints
        Dim X, Y, Z As Double
        Dim TU() As Single
        TU = Make_TU_Tiling(N, Tiled)

        Dim A As String
        FillVextexList_0 = "VertexList( 0 " & vbCrLf

        For K = 1 To NP
            ' the left side point
            A = ""
            X = (Polys(0).GPoints(K).lon - AuxLonPoly) * MetersPerDegLon(AuxLatPoly)
            If X < 0 Then A = Format(X, "0000.0000") & " "
            If X >= 0 Then A = Format(X, " 0000.0000") & " "
            Z = CDbl(Polys(0).GPoints(K).alt) - AuxZPoly
            A = A & Format(Z, "0000.0000000") & " "
            Y = (Polys(0).GPoints(K).lat - AuxLatPoly) * MetersPerDegLat
            If Y < 0 Then A = A & Format(Y, "0000.0000") & " 0.0 1.0 0.0 "
            If Y >= 0 Then A = A & Format(Y, " 0000.0000") & " 0.0 1.0 0.0 "
            A = A & Format(TU(K), "00.000") & " " & Format(1, "00.000")
            A = Replace(A, ",", ".")
            A = A & " ; vertex #" & Format(2 * K - 2, "000")
            FillVextexList_0 = FillVextexList_0 & "      " & A & vbCrLf
            ' the right side point
            A = ""
            J = 2 * NP + 1 - K
            X = (Polys(0).GPoints(J).lon - AuxLonPoly) * MetersPerDegLon(AuxLatPoly)
            If X < 0 Then A = Format(X, "0000.0000") & " "
            If X >= 0 Then A = Format(X, " 0000.0000") & " "
            Z = CDbl(Polys(0).GPoints(J).alt) - AuxZPoly
            A = A & Format(Z, "0000.0000000") & " "
            Y = (Polys(0).GPoints(J).lat - AuxLatPoly) * MetersPerDegLat
            If Y < 0 Then A = A & Format(Y, "0000.0000") & " 0.0 1.0 0.0 "
            If Y >= 0 Then A = A & Format(Y, " 0000.0000") & " 0.0 1.0 0.0 "
            A = A & Format(TU(K), "00.000") & " " & Format(0, "00.000")
            A = Replace(A, ",", ".")
            A = A & " ; vertex #" & Format(2 * K - 1, "000")
            FillVextexList_0 = FillVextexList_0 & "      " & A & vbCrLf
        Next K

        FillVextexList_0 = FillVextexList_0 & "       )"

    End Function

    Private Function Make_TU_Tiling(ByVal N As Integer, ByVal Tiled As Integer) As Single()

        Dim K As Integer
        Dim NP As Integer = Lines(N).NoOfPoints
        Dim TU(NP) As Single
        Dim X As Double

        If Tiled = 0 Then

            For K = 1 To NP
                TU(K) = CSng(K - 1)
            Next

        End If

        If Tiled > 0 Then

            Dim L(NP) As Double
            L(1) = 0
            Dim W As Double = Lines(N).GLPoints(1).wid
            For K = 2 To NP
                L(K) = L(K - 1) + GetDistanceP1P2(N, K - 1, K)
                W = W + Lines(N).GLPoints(K).wid
            Next
            W = W / NP

            Dim R As Double = GetImageRatio()

            TU(1) = 0
            For K = 2 To NP
                X = L(K) - L(K - 1)
                X = X / W
                X = X / R
                TU(K) = CSng(X) + TU(K - 1)
            Next

        End If

        If Tiled = 2 Then
            For K = 2 To NP
                TU(K) = Int(TU(K) + 0.5!)
                If TU(K) = TU(K - 1) Then TU(K) = TU(K) + 1
            Next
        End If

        Return TU

    End Function

    Private Function GetImageRatio() As Double


        Try
            ChDrive(My.Application.Info.DirectoryPath)
            ChDir(My.Application.Info.DirectoryPath & "\Tools\")

            Dim BmpPath As String = AppPath & "\Tools\Work\temp.bmp"
            Dim TexPath As String = AppPath & "\Texture\" & PolyTex
            Dim ImageTool As String = "imagetool -nowarning -nogui -nomip -8 Work\temp.bmp"
            File.Copy(TexPath, BmpPath, True)
            ExecCmd(ImageTool)

            Dim bmp As Image = System.Drawing.Image.FromFile(BmpPath)

            GetImageRatio = bmp.Width / bmp.Height
            bmp.Dispose()

        Catch ex As Exception
            GetImageRatio = 1
            MsgBox("There is a problem with " & PolyTex, MsgBoxStyle.Critical)
        End Try

    End Function

    Private Function FillDrawTriList_0(ByVal N As Integer) As String


        Dim NP As Integer = Lines(N).NoOfPoints
        Dim K, J As Integer

        FillDrawTriList_0 = "DrawTriList( 0 " & vbCrLf & "       "

        For K = 2 To NP
            J = 2 * K
            FillDrawTriList_0 = FillDrawTriList_0 & Format(J - 2, "000") & " "
            FillDrawTriList_0 = FillDrawTriList_0 & Format(J - 4, "000") & " "
            FillDrawTriList_0 = FillDrawTriList_0 & Format(J - 3, "000") & vbCrLf & "       "
            FillDrawTriList_0 = FillDrawTriList_0 & Format(J - 3, "000") & " "
            FillDrawTriList_0 = FillDrawTriList_0 & Format(J - 1, "000") & " "
            FillDrawTriList_0 = FillDrawTriList_0 & Format(J - 2, "000") & vbCrLf & "       "
        Next K

        FillDrawTriList_0 = FillDrawTriList_0 & ")"

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



    Friend Sub MakeBGLTexPolys(ByVal CopyBGLs As Boolean)

        Dim myFile As String
        Dim b As String
        Dim M As Integer
        Dim a As String
        Dim N As Integer

        Dim Priority, Visibility As String
        Dim TileY, TileX, Night As Integer

        Dim LZL As Double_XYZ

        Dim Longitude, Latitude, Altitude As String

        Dim H_NLat As Double = -90
        Dim H_SLat As Double = 90
        Dim H_WLon As Double = 180
        Dim H_ELon As Double = -180

        For N = 1 To NoOfPolys
            If Polys(N).Selected Then
                a = Polys(N).Type
                If a = "" Then GoTo next_N0
                b = Mid(a, 1, 3)
                If b = "TEX" Then
                    If Polys(N).ELON > H_ELon Then H_ELon = Polys(N).ELON
                    If Polys(N).WLON < H_WLon Then H_WLon = Polys(N).WLON
                    If Polys(N).NLAT > H_NLat Then H_NLat = Polys(N).NLAT
                    If Polys(N).SLAT < H_SLat Then H_SLat = Polys(N).SLAT
                End If
            End If
next_N0:
        Next N

        myFile = ProjectName & "_TEXP"
        myFile = Replace(myFile, " ", "_")
        FileOpen(3, My.Application.Info.DirectoryPath & "\tools\work\" + myFile + ".scm", OpenMode.Output)

        a = "Header( 1 "
        a = a & Str(Int(H_NLat + 1.5)) & " "
        a = a & Str(Int(H_SLat - 0.5)) & " "
        a = a & Str(Int(H_ELon + 1.5)) & " "
        a = a & Str(Int(H_WLon - 0.5)) & " )"
        PrintLine(3, a)
        a = "LatRange( "
        a = a & Str(Int(H_SLat - 0.5)) & " "
        a = a & Str(Int(H_NLat + 1.5)) & " )"
        PrintLine(3, a)
        PrintLine(3)


        For N = 1 To NoOfPolys
            If Polys(N).Selected Then
                a = Polys(N).Type
                If a = "" Then GoTo next_N
                b = Mid(a, 1, 3)
                If b <> "TEX" Then GoTo next_N
                M = InStr(1, a, "//")
                b = Mid(a, 1, M - 1)
                ' b = "TEX"

                a = Mid(a, M + 2)
                M = InStr(1, a, "//")
                PolyTex = Mid(a, 1, M - 1)

                a = Mid(a, M + 2)
                M = InStr(1, a, "//")
                Priority = Mid(a, 1, M - 1)

                a = Mid(a, M + 2)
                M = InStr(1, a, "//")
                TileX = CInt(Mid(a, 1, M - 1))

                a = Mid(a, M + 2)
                M = InStr(1, a, "//")
                TileY = CInt(Mid(a, 1, M - 1))

                a = Mid(a, M + 2)
                M = InStr(1, a, "//")
                Visibility = Mid(a, 1, M - 1)

                a = Mid(a, M + 2)
                M = InStr(1, a, "//")
                Night = CInt(Mid(a, 1, M - 1))

                PolyTexString = Mid(a, M + 2)
                MakePolyTexString(N, False)

                LZL = GetLatZLonTexPoly(N)
                AuxLatPoly = LZL.Y
                Latitude = Format(AuxLatPoly, "0.00000000")
                AuxLonPoly = LZL.X
                Longitude = Format(AuxLonPoly, "0.00000000")
                AuxZPoly = LZL.Z
                Altitude = Format(AuxZPoly, "0.00000000")

                Latitude = Replace(Latitude, ",", ".")
                Longitude = Replace(Longitude, ",", ".")
                Altitude = Replace(Altitude, ",", ".")
                Visibility = Replace(Visibility, ",", ".")

                a = "; Textured Poly #" & Str(N) & vbCrLf & vbCrLf
                a = a & "Area( 5 "
                a = a & Latitude & " " & Longitude & " 50 )"
                PrintLine(3, a)
                a = "LayerCall( :lcall " & Priority & " )"
                PrintLine(3, a)
                a = "Jump( : )" & vbCrLf & ":lcall"
                PrintLine(3, a)

                a = "RefPoint( 2 :return 1 " & Latitude & " " & Longitude
                a = a & " E= " & Altitude & " v1= " & Visibility & " v2= " & GetV2(N) & " )"
                PrintLine(3, a)

                a = "BGLVersion( 0800 )"
                PrintLine(3, a)

                If PolyTex <> "" Then
                    a = FillTextureList(Night)
                    PrintLine(3, a)
                End If

                a = FillMaterialList(N)
                PrintLine(3, a)

                a = FillVextexList(N, TileX, TileY)
                PrintLine(3, a)

                If PolyTex <> "" Then
                    a = "SetMaterial( 0 0 )"
                Else
                    a = "SetMaterial( 0 -1 )"
                End If
                PrintLine(3, a)

                a = FillDrawTriList(N)
                PrintLine(3, a)
                a = "EndVersion"
                PrintLine(3, a)

                a = ":return" & vbCrLf & "Return" & vbCrLf & "EndA" & vbCrLf
                PrintLine(3, a)


            End If
next_N:
        Next N

        FileClose(3)

        ' delete BGL file
        a = My.Application.Info.DirectoryPath & "\tools\work\" + myFile + ".BGL"
        If File.Exists(a) Then File.Delete(a)

        ChDrive(My.Application.Info.DirectoryPath)
        ChDir(My.Application.Info.DirectoryPath & "\tools\")

        a = "scasm work\" + myFile + ".scm -s -l"
        N = ExecCmd(a)

        If N > 0 Then
            a = "There was a compilation error in the textured" & vbCrLf
            a = a & " polygons! Do you want to read a SCASM report?"
            N = MsgBox(a, MsgBoxStyle.OkCancel)
            If N = 1 Then
                a = "notepad SCAERROR.LOG"
                N = ExecCmd(a)
            End If
            Exit Sub
        End If

        If Not CopyBGLs Then Exit Sub

        Try
            a = My.Application.Info.DirectoryPath & "\tools\work\" & myFile & ".BGL"
            If File.Exists(a) Then
                File.Copy(a, BGLProjectFolder & "\" & myFile & ".BGL", True)
            End If
        Catch ex As Exception
            MsgBox("Copying BGL files failed!", MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Private Function GetLatZLonTexPoly(ByVal P As Integer) As Double_XYZ

        Dim N, NP As Integer
        Dim Z, X, Y As Double

        X = 0
        Y = 0
        Z = 0
        NP = Polys(P).NoOfPoints

        For N = 1 To NP
            X = X + Polys(P).GPoints(N).lon
            Y = Y + Polys(P).GPoints(N).lat
            Z = Z + Polys(P).GPoints(N).alt
        Next N

        GetLatZLonTexPoly.X = X / NP
        GetLatZLonTexPoly.Y = Y / NP
        GetLatZLonTexPoly.Z = Z / NP

    End Function

    Private Function GetV2(ByVal P As Integer) As String

        Dim lat As Double
        Dim N, NP As Integer
        Dim DZ, DX, X, DY As Double

        DX = 0
        DY = 0
        DZ = 0
        NP = Polys(P).NoOfPoints

        For N = 1 To NP
            X = System.Math.Abs(AuxLonPoly - Polys(P).GPoints(N).lon) * MetersPerDegLon(lat)
            If X > DX Then DX = X
            X = System.Math.Abs(AuxLatPoly - Polys(P).GPoints(N).lat) * MetersPerDegLat
            If X > DY Then DY = X
            X = System.Math.Abs(AuxZPoly - Polys(P).GPoints(N).alt)
            If X > DZ Then DZ = X
        Next N

        X = DX * DX + DY * DY + DZ * DZ
        X = System.Math.Sqrt(X)
        X = Int(X + 20)
        GetV2 = CStr(X)

    End Function

    Private Function FillTextureList(ByVal Nite As Integer) As String

        Dim a, File As String
        Dim K As Integer

        a = "TextureList( 0 " & vbCrLf

        K = Len(PolyTex)
        K = K - 4
        File = Mid(PolyTex, 1, K)

        a = a & "       6 FF 255 255 255 0 50.000000 " & File & ".bmp" & vbCrLf
        If Nite = 1 Then
            a = a & "     256 FF 255 255 255 0 50.000000 " & File & "_lm.bmp" & vbCrLf
        End If

        FillTextureList = a & "       )"

    End Function

    Private Function FillMaterialList(ByVal P As Integer) As String

        Dim C As Color
        Dim a As String

        C = Polys(P).Color

        FillMaterialList = "MaterialList( 0   "

        a = Format(C.R / 255, "0.000") & " "
        a = a & Format(C.G / 255, "0.000") & " "
        a = a & Format(C.B / 255, "0.000") & " 1.00   "

        a = Replace(a, ",", ".")

        FillMaterialList = FillMaterialList & a & a & "0.00 0.00 0.00 1.00   0.00 0.00 0.00 1.00   0.00 )"

    End Function

    Private Function FillVextexList(ByVal P As Integer, ByVal TileX As Integer, ByVal TileY As Integer) As String


        Dim N, K, M, L, NP As Integer
        Dim a, b As String
        Dim PT As Point
        Dim Z, X, Y As Double
        Dim TX, TY As Single

        Dim LP() As Point

        ' copy TexPoints to LP
        a = PolyTexString
        NP = Polys(P).NoOfPoints
        ReDim LP(NP)
        For M = 1 To NP
            K = InStr(1, a, "//")
            b = Mid(a, 1, K - 1)
            a = Mid(a, K + 2)
            K = InStr(1, b, ",")
            LP(M).X = CInt(Mid(b, 1, K - 1))
            LP(M).Y = CInt(Mid(b, K + 1))
        Next M

        If MakePolyClockWise(P) Then
            L = NP + 1
            M = CInt(Int(L / 2))
            For N = 1 To M
                PT = LP(N)
                LP(N) = LP(L - N)
                LP(L - N) = PT
            Next N
        End If

        FillVextexList = "VertexList( 0 " & vbCrLf

        For N = 1 To NP

            X = (Polys(P).GPoints(N).lon - AuxLonPoly) * MetersPerDegLon(AuxLatPoly)
            If X < 0 Then a = Format(X, "0000.0000") & " "
            If X >= 0 Then a = Format(X, " 0000.0000") & " "

            Z = CDbl(Polys(P).GPoints(N).alt) - AuxZPoly
            a = a & Format(Z, "0000.0000000") & " "

            Y = (Polys(P).GPoints(N).lat - AuxLatPoly) * MetersPerDegLat
            If Y < 0 Then a = a & Format(Y, "0000.0000") & " 0.0 1.0 0.0 "
            If Y >= 0 Then a = a & Format(Y, " 0000.0000") & " 0.0 1.0 0.0 "

            TX = CSng((LP(N).X * TileX) / 256)
            TY = CSng((LP(N).Y * TileY) / 256)
            a = a & Format(TX, "00.000") & " " & Format(TY, "00.000")

            a = Replace(a, ",", ".")

            a = a & " ; vertex #" & Format(N - 1, "000")
            FillVextexList = FillVextexList & "      " & a & vbCrLf

        Next N

        FillVextexList = FillVextexList & "       )"


    End Function

    Private Function FillDrawTriList(ByVal P As Integer) As String

        Dim N, S, K As Integer

        S = Polys(P).NoOfPoints
        NoOfPts2Tris = S
        ReDim Pts2Tris(S + 1)

        For N = 1 To S
            Pts2Tris(N).X = Polys(P).GPoints(N).lon
            Pts2Tris(N).Y = Polys(P).GPoints(N).lat
            Pts2Tris(N).N = N - 1
        Next N

        Pts2Tris(0) = Pts2Tris(S)
        Pts2Tris(S + 1) = Pts2Tris(1)

        MakeTris() ' make the triangles!

        FillDrawTriList = "DrawTriList( 0 " & vbCrLf & "       "

        For N = 1 To NoOfTris
            K = Tris(N).N3
            FillDrawTriList = FillDrawTriList & Format(K, "000") & " "
            K = Tris(N).N2
            FillDrawTriList = FillDrawTriList & Format(K, "000") & " "
            K = Tris(N).N1
            FillDrawTriList = FillDrawTriList & Format(K, "000") & vbCrLf & "       "
        Next N

        FillDrawTriList = FillDrawTriList & ")"

    End Function

    Friend Sub Get3Points(ByVal N As Integer, ByRef N1 As Integer, ByRef N2 As Integer, ByRef N3 As Integer, ByRef lat As Double)

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

    Friend Sub GetSlopes(ByVal N As Integer, ByVal N0 As Integer, ByVal N1 As Integer, ByVal N2 As Integer, ByRef K1 As Double, ByRef K2 As Double, ByRef K3 As Double)

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

    Private DeltaLat As Double
    Private DeltaLon As Double

    Public Sub SetDeltaLatLon()

        DeltaLat = 360 / (2 ^ QMIDLevel)
        DeltaLon = 480 / (2 ^ QMIDLevel)

    End Sub

    Public Sub SnapPolys()

        SetDeltaLatLon()

        Dim N As Integer
        N = 1
        Do While N <= NoOfPolys
            If Polys(N).Selected Then
                SnapThisPoly(N)
            End If
            N = N + 1
        Loop

    End Sub

    Public Sub SnapThisPoly(ByVal P As Integer)

        Dim NP, N, J, int As Integer
        Dim Lat, Lon As Double

        NP = Polys(P).NoOfPoints
        N = 1

        Do While N <= NP
            Lat = 90 - Polys(P).GPoints(N).lat
            Lon = 180 + Polys(P).GPoints(N).lon
            int = Convert.ToInt32(Lat / DeltaLat)
            Lat = 90 - int * DeltaLat
            int = Convert.ToInt32(Lon / DeltaLon)
            Lon = int * DeltaLon - 180
            Polys(P).GPoints(N).lat = Lat
            Polys(P).GPoints(N).lon = Lon
            For J = 1 To N - 1
                If Polys(P).GPoints(J).lat = Lat Then
                    If Polys(P).GPoints(J).lon = Lon Then
                        If Polys(P).NoOfPoints < 3 Then
                            DeletePoly(P)
                            Dirty = True
                            Exit Sub
                        Else
                            DeletePointInPoly(P, N)
                        End If
                        NP = NP - 1
                        N = N - 1
                    End If
                End If
            Next
            N = N + 1
        Loop

    End Sub

End Module

