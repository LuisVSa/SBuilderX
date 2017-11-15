Option Strict On
Option Explicit On
Module modulePOPUP

    ' Friend IsPopUP As Boolean  ' goes false when mouse goes down


    Friend POPIndex As Integer
    Friend POPType As String
    Friend POPIndexPT As Integer
    Friend POPMode As String

    Friend XPOP As Integer
    Friend YPOP As Integer

    Friend Sub ProcessPopUp(ByVal X1 As Integer, ByVal Y1 As Integer)

        Dim X, Y As Double
        Dim N, J, K, C, R As Integer
        Dim PT As Point

        Dim B As Byte

        Dim A As String

        XPOP = X1
        YPOP = Y1

        X = LonDispWest * PixelsPerLonDeg + X1   ' longitude in pixels
        Y = LatDispNorth * PixelsPerLatDeg - Y1  ' latitude in pixels

        POPType = ""
        POPIndex = 0
        POPIndexPT = 0
        POPMode = "One"

        HidePopUPMenu()

        If LineON And NoOfLinesSelected > 1 Then
            For N = 1 To NoOfLines
                POPIndex = N
                If Lines(N).Selected Then Exit For
            Next
            FrmLinesP.Close()
            POPMode = "Many"
            POPType = "Line"
            frmStart.NamePopUPMenu.Text = "ON MANY LINES"
            ShowPopUPMenu()
            MakeOnMany = NoOfLinesSelected
            frmStart.JoinAllPopUPMenu.Visible = True
            frmStart.SmoothPopUPMenu.Visible = True
            frmStart.SamplePopUPMenu.Visible = True
            frmStart.SetWidthPopUpMenu.Visible = True
            frmStart.SetAltitudePopUpMenu.Visible = True
            frmStart.DeletePopUPMenu.Visible = False
            If QMIDLevel > 3 Then
                frmStart.SliceQMIDPopUpMenu.Text = "Slice to QMID " & QMIDLevel.ToString
                frmStart.SliceQMIDPopUpMenu.Visible = True
            End If
            Exit Sub
        End If

        Dim OuterFlag As Boolean = False
        Dim HoleFlag As Boolean = False

        If PolyON And NoOfPolysSelected > 1 Then

            ' check if there are possible outers and possible holes
            For N = 1 To NoOfPolys
                If Polys(N).Selected Then
                    POPIndex = N
                    If Polys(N).NoOfChilds < 0 Then OuterFlag = True
                    If Polys(N).NoOfChilds = 0 Then HoleFlag = True
                End If
            Next

            FrmPolysP.Close()
            POPMode = "Many"
            POPType = "Poly"
            MakeOnMany = K
            frmStart.SmoothPopUPMenu.Visible = True
            frmStart.SamplePopUPMenu.Visible = True
            frmStart.NamePopUPMenu.Text = "ON MANY POLYS"
            ShowPopUPMenu()
            frmStart.JoinAllPopUPMenu.Visible = True
            'frmStart.SetWidthPopUpMenu.Visible = True

            If HoleFlag Then frmStart.HolePopUpMenu.Visible = True
            If OuterFlag Then frmStart.OuterPopUpMenu.Visible = True

            frmStart.SetAltitudePopUpMenu.Visible = True
            frmStart.DeletePopUPMenu.Visible = False

            If QMIDLevel > 3 Then
                frmStart.SnapQMIDPopUPMenu.Text = "Snap to QMID " & QMIDLevel.ToString
                frmStart.SnapQMIDPopUPMenu.Visible = True
                If Polys(POPIndex).NoOfChilds >= 0 Then
                    frmStart.SliceQMIDPopUpMenu.Text = "Slice to QMID " & QMIDLevel.ToString
                    frmStart.SliceQMIDPopUpMenu.Visible = True
                    frmStart.FillQMIDPopUpMenu.Text = "Fill to QMID " & QMIDLevel.ToString
                    frmStart.FillQMIDPopUpMenu.Visible = True
                End If
            End If


            Exit Sub
        End If


        If ObjectON And NoOfObjectsSelected > 1 Then
            frmObjectsP.Close()
            frmObjectsP.ShowForAllSelected()
            frmObjectsP.ShowDialog()
            Exit Sub
        End If


        If LandON Then
            POPIndex = IsLandUP(X, Y)
            If POPIndex > -1 Then
                POPType = "Land"
                N = POPIndex
                R = N Mod 512
                N = N >> 9
                C = N Mod 512

                N = N >> 9
                K = N Mod 64
                N = N >> 6
                J = N Mod 128

                B = LLands(C, R, LL_XY(J, K).Pointer)
                B = LC(B).Index
                frmStart.NamePopUPMenu.Text = "LAND CLASS = " & CStr(B)
                ShowPopUPMenu()
                frmStart.Sep3PopUPMenu.Visible = False
                frmStart.PropertiesPopUPMenu.Visible = False
                Exit Sub
            End If
        End If

        If WaterON Then
            POPIndex = IsWaterUP(X, Y)
            If POPIndex > -1 Then
                POPType = "Water"
                N = POPIndex
                R = N Mod 512
                N = N >> 9
                C = N Mod 512
                N = N >> 9
                K = N Mod 64
                N = N >> 6
                J = N Mod 128
                B = WWaters(C, R, WW_XY(J, K).Pointer)
                B = WC(B).Index
                frmStart.NamePopUPMenu.Text = "WATER CLASS = " & CStr(B)
                ShowPopUPMenu()
                frmStart.Sep3PopUPMenu.Visible = False
                frmStart.PropertiesPopUPMenu.Visible = False
                Exit Sub
            End If
        End If


        If ObjectON Then
            POPIndex = IsObjectUP(X, Y)
            If POPIndex > 0 Then
                POPType = "Object"
                frmStart.NamePopUPMenu.Text = "OBJECT # " & CStr(POPIndex)
                ShowPopUPMenu()
                Exit Sub
            End If
        End If


        ' now we are 

        PT = IsPointInLineUP(X, Y)
        If PT.X > 0 Then
            POPType = "PtInLine"
            POPIndex = PT.Y
            POPIndexPT = PT.X
            A = Lines(PT.Y).Name
            If A = "" Then A = "LINE # " & CStr(PT.Y)
            frmStart.NamePopUPMenu.Text = "POINT # " & CStr(PT.X) & " from " & A
            ShowPopUPMenu()
            frmStart.PointFromAircraftPopUpMenu.Visible = True
            Exit Sub
        End If


        PT = IsPointInPolyUP(X, Y)
        If PT.X > 0 Then
            POPType = "PtInPoly"
            POPIndex = PT.Y
            POPIndexPT = PT.X
            A = Polys(PT.Y).Name
            If A = "" Then A = "POLY # " & CStr(PT.Y)
            frmStart.NamePopUPMenu.Text = "POINT # " & CStr(PT.X) & " from " & A
            ShowPopUPMenu()
            frmStart.PointFromAircraftPopUpMenu.Visible = True
            Exit Sub
        End If

        N = IsLineUP(X, Y)
        If N > 0 Then
            POPType = "Line"
            POPIndex = N
            A = Lines(N).Name
            If A <> "" Then A = A & " "
            A = A & "LINE # " & CStr(N)
            frmStart.NamePopUPMenu.Text = A
            ShowPopUPMenu()
            frmStart.SetTransparencyPopUpMenu.Visible = True
            frmStart.SetWidthPopUpMenu.Visible = True
            frmStart.SetAltitudePopUpMenu.Visible = True
            frmStart.MakePolyPopUPMenu.Visible = True
            frmStart.ConvertToPolyPopUpMenu.Visible = True
            frmStart.SmoothPopUPMenu.Visible = True
            frmStart.SamplePopUPMenu.Visible = True
            frmStart.ManualCheckPopUPMenu.Visible = True
            If QMIDLevel > 3 And LineON Then
                frmStart.SliceQMIDPopUpMenu.Text = "Slice to QMID " & QMIDLevel.ToString
                frmStart.SliceQMIDPopUpMenu.Visible = True
            End If
            Exit Sub
        End If

        N = IsPolyUP(X, Y)
        If N > 0 Then
            POPType = "Poly"
            POPIndex = N
            A = Polys(N).Name
            If A <> "" Then A = A & " "
            A = A & "POLY # " & CStr(N)
            frmStart.NamePopUPMenu.Text = A
            ShowPopUPMenu()
            frmStart.SetAltitudePopUpMenu.Visible = True
            frmStart.SetTransparencyPopUpMenu.Visible = True
            frmStart.MakeLinePopUPMenu.Visible = True
            frmStart.SmoothPopUPMenu.Visible = True
            frmStart.SamplePopUPMenu.Visible = True
            frmStart.ManualCheckPopUPMenu.Visible = True

            If QMIDLevel > 3 And PolyON Then
                frmStart.SnapQMIDPopUPMenu.Text = "Snap to QMID " & QMIDLevel.ToString
                frmStart.SnapQMIDPopUPMenu.Visible = True
            End If

            If Polys(POPIndex).NoOfChilds >= 0 Then
                If NoOfPolys > 1 Then frmStart.HolePopUpMenu.Visible = True
                If QMIDLevel > 3 And PolyON Then
                    frmStart.SliceQMIDPopUpMenu.Text = "Slice to QMID " & QMIDLevel.ToString
                    frmStart.SliceQMIDPopUpMenu.Visible = True
                    frmStart.FillQMIDPopUpMenu.Text = "Fill to QMID " & QMIDLevel.ToString
                    frmStart.FillQMIDPopUpMenu.Visible = True
                End If
            Else
                If NoOfPolys > 1 Then frmStart.OuterPopUpMenu.Visible = True
                frmStart.Sep3PopUPMenu.Visible = False
                frmStart.PropertiesPopUPMenu.Visible = False
                frmStart.SetAltitudePopUpMenu.Visible = False
            End If
            Exit Sub
        End If

        POPIndex = IsObjectUP(X, Y)
        If POPIndex > 0 Then
            POPType = "Object"
            frmStart.NamePopUPMenu.Text = "OBJECT # " & CStr(POPIndex)
            ShowPopUPMenu()
            Exit Sub
        End If


        POPIndex = IsExcludeUP(X, Y)
        If POPIndex > 0 Then
            POPType = "Exclude"
            frmStart.NamePopUPMenu.Text = "EXCLUDE # " & CStr(POPIndex)
            ShowPopUPMenu()
            Exit Sub
        End If

        POPIndex = IsLandUP(X, Y)
        If POPIndex > -1 Then
            POPType = "Land"
            N = POPIndex
            R = N Mod 512
            N = N >> 9
            C = N Mod 512
            N = N >> 9
            K = N Mod 64
            N = N >> 6
            J = N Mod 128
            B = LLands(C, R, LL_XY(J, K).Pointer)
            B = LC(B).Index
            frmStart.NamePopUPMenu.Text = "LAND CLASS = " & CStr(B)
            ShowPopUPMenu()
            frmStart.Sep3PopUPMenu.Visible = False
            frmStart.PropertiesPopUPMenu.Visible = False
            Exit Sub
        End If


        POPIndex = IsWaterUP(X, Y)
        If POPIndex > -1 Then
            POPType = "Water"
            N = POPIndex
            R = N Mod 512
            N = N >> 9
            C = N Mod 512
            N = N >> 9
            K = N Mod 64
            N = N >> 6
            J = N Mod 128
            B = WWaters(C, R, WW_XY(J, K).Pointer)
            B = WC(B).Index
            frmStart.NamePopUPMenu.Text = "WATER CLASS = " & CStr(B)
            ShowPopUPMenu()
            frmStart.Sep3PopUPMenu.Visible = False
            frmStart.PropertiesPopUPMenu.Visible = False
            Exit Sub
        End If


        N = IsMapUP(X, Y)
        If N > 0 Then
            POPType = "Map"
            POPIndex = N
            A = Maps(N).Name
            If A <> "" Then A = A & " "
            A = A & "MAP # " & CStr(N)
            frmStart.NamePopUPMenu.Text = A
            ShowPopUPMenu()
            frmStart.CalibratePopUPMenu.Visible = True
            Exit Sub
        End If

        frmStart.NamePopUPMenu.Text = ProjectName
        frmStart.NamePopUPMenu.Visible = True
        frmStart.Sep1PopUPMenu.Visible = True
        frmStart.CenterPopUPMenu.Visible = True
        frmStart.FlyToPopUPMenu.Visible = True
        frmStart.ZoomInPopUPMenu.Visible = True
        frmStart.ZoomOutPopUPMenu.Visible = True
        If frmStart.ShowBackgroundMenuItem.Enabled Then
            If frmStart.ShowBackgroundMenuItem.Checked Then
                frmStart.SaveBackGroundPopUpMenu.Visible = True
                frmStart.TilePathToClipboardPopUpMenu.Visible = True
            End If
        End If
        frmStart.Sep3PopUPMenu.Visible = True
        frmStart.PropertiesPopUPMenu.Visible = True
        POPType = "Project"


    End Sub

    Friend Function IsPointInLineUP(ByVal X As Double, ByVal Y As Double) As Point

        ' If the right mouse is not over a Point returns Zero in X property.
        ' Otherwise return the index of Point and the index of the Line
        ' in the Y property

        Dim X1, Y1 As Double
        Dim N, K As Integer
        Dim retval As Boolean

        IsPointInLineUP.X = 0

        Dim DX As Double = 3 / PixelsPerLonDeg
        Dim DY As Double = 3 / PixelsPerLatDeg

        X1 = X / PixelsPerLonDeg
        Y1 = Y / PixelsPerLatDeg

        If (PointerON Or LineON) And LineVIEW Then

            For N = 1 To NoOfLines

                If X1 < Lines(N).WLON - DX Then GoTo Jump_NextLN
                If X1 > Lines(N).ELON + DX Then GoTo Jump_NextLN
                If Y1 < Lines(N).SLAT - DY Then GoTo Jump_NextLN
                If Y1 > Lines(N).NLAT + DY Then GoTo Jump_NextLN

                K = 1
                retval = False
                Do Until retval = True Or K = Lines(N).NoOfPoints + 1
                    retval = retval Or IsPoint(Lines(N).GLPoints(K).lon, Lines(N).GLPoints(K).lat, X, Y)
                    K = K + 1
                Loop
                If retval Then
                    IsPointInLineUP.X = K - 1
                    IsPointInLineUP.Y = N
                    Exit Function
                End If
Jump_NextLN:
            Next N
        End If

    End Function
    Friend Function IsPointInPolyUP(ByVal X As Double, ByVal Y As Double) As Point

        ' If the right mouse is not over a Point returns Zero in X property.
        ' Otherwise return the index of Point and the index of the Poly
        ' in the Y property

        Dim X1, Y1 As Double
        Dim N, K As Integer
        Dim retval As Boolean

        IsPointInPolyUP.X = 0

        X1 = X / PixelsPerLonDeg
        Y1 = Y / PixelsPerLatDeg

        If (PointerON Or PolyON) And PolyVIEW Then
            For N = 1 To NoOfPolys
                K = 1
                retval = False
                Do Until retval = True Or K = Polys(N).NoOfPoints + 1
                    retval = retval Or IsPoint(Polys(N).GPoints(K).lon, Polys(N).GPoints(K).lat, X, Y)
                    K = K + 1
                Loop
                If retval Then
                    IsPointInPolyUP.X = K - 1
                    IsPointInPolyUP.Y = N
                    Exit Function
                End If
            Next N
        End If

    End Function

    Friend Function IsLineUP(ByVal X As Double, ByVal Y As Double) As Integer

        ' If the right mouse is not over a Line returns Zero.
        ' Otherwise return the index of Line

        Dim N, K As Integer
        Dim retval As Boolean
        Dim WLON, NLAT, SLAT, ELON As Double

        IsLineUP = 0
        If Not ((PointerON Or LineON) And LineVIEW) Then Exit Function


        WLON = (X + 5) / PixelsPerLonDeg
        ELON = (X - 5) / PixelsPerLonDeg
        NLAT = (Y - 5) / PixelsPerLatDeg
        SLAT = (Y + 5) / PixelsPerLatDeg

        For N = 1 To NoOfLines

            retval = False

            If WLON < Lines(N).WLON Then GoTo Jump_NextLN
            If ELON > Lines(N).ELON Then GoTo Jump_NextLN
            If SLAT < Lines(N).SLAT Then GoTo Jump_NextLN
            If NLAT > Lines(N).NLAT Then GoTo Jump_NextLN

            K = 2
            retval = False
            Do Until retval = True Or K = Lines(N).NoOfPoints + 1
                retval = retval Or IsPointInSegment(Lines(N).GLPoints(K - 1).lon, Lines(N).GLPoints(K - 1).lat, Lines(N).GLPoints(K).lon, Lines(N).GLPoints(K).lat, X, Y)
                K = K + 1
            Loop

            If retval Then
                IsLineUP = N
                Exit Function
            End If
Jump_NextLN:
        Next N


    End Function

    Friend Function IsPolyUP(ByVal X As Double, ByVal Y As Double) As Integer

        ' If the right mouse is not over a Poly returns Zero.
        ' Otherwise return the index of Poly

        Dim N, K As Integer
        Dim retval As Boolean

        IsPolyUP = 0
        If (PointerON Or PolyON) And PolyVIEW Then
            For N = 1 To NoOfPolys
                K = 2
                retval = False
                Do Until retval = True Or K = Polys(N).NoOfPoints + 1
                    retval = retval Or IsPointInSegment(Polys(N).GPoints(K - 1).lon, Polys(N).GPoints(K - 1).lat, Polys(N).GPoints(K).lon, Polys(N).GPoints(K).lat, X, Y)
                    K = K + 1
                Loop
                If retval Then
                    IsPolyUP = N
                    Exit Function
                End If
                retval = IsPointInSegment(Polys(N).GPoints(K - 1).lon, Polys(N).GPoints(K - 1).lat, Polys(N).GPoints(1).lon, Polys(N).GPoints(1).lat, X, Y)
                If retval Then
                    IsPolyUP = N
                    Exit Function
                End If
            Next N
        End If

    End Function




    Friend Function IsExcludeUP(ByVal X As Double, ByVal Y As Double) As Integer

        Dim N As Integer
        Dim retval As Boolean

        IsExcludeUP = 0
        If (PointerON Or ExcludeON) And ExcludeVIEW Then
            For N = 1 To NoOfExcludes
                retval = IsPtInExclude(N, X, Y)
                If retval Then
                    IsExcludeUP = N
                    Exit Function
                End If
            Next N
        End If

    End Function

    Friend Function IsObjectUP(ByVal X As Double, ByVal Y As Double) As Integer

        Dim N As Integer
        Dim X1, Y1 As Double

        IsObjectUP = 0
        If (PointerON Or ObjectON) And ObjectVIEW Then
            For N = 1 To NoOfObjects
                X1 = Objects(N).lon * PixelsPerLonDeg
                Y1 = Objects(N).lat * PixelsPerLatDeg
                If X1 > X + 4 Then GoTo next_N
                If X1 < X - 4 Then GoTo next_N
                If Y1 < Y - 4 Then GoTo next_N
                If Y1 > Y + 4 Then GoTo next_N
                IsObjectUP = N
                Exit Function
next_N:
            Next N
        End If


    End Function


    'Friend Function IsPhotoUP(ByVal X As Double, ByVal Y As Double) As Integer

    'Dim N As Integer
    'Dim retval As Boolean

    'IsPhotoUP = 0
    'If (PointerON Or PhotoON) And PhotoVIEW Then
    '    For N = 1 To NoOfPhotos
    '        retval = IsPointInsidePhoto(N, X, Y)
    '        If retval Then
    '            IsPhotoUP = N
    '            Exit Function
    '        End If
    '    Next N
    'End If

    'End Function

    'Friend Function IsMeshUP(ByVal X As Double, ByVal Y As Double) As Integer

    '    Dim N As Integer
    '    Dim retval As Boolean

    '    IsMeshUP = 0
    '    If (PointerON Or MeshON) And MeshVIEW Then
    '        For N = 1 To NoOfMeshes
    '            retval = IsPointInsideMesh(N, X, Y)
    '            If retval Then
    '                IsMeshUP = N
    '                Exit Function
    '            End If
    '        Next N
    '    End If

    'End Function

    Friend Function IsLandUP(ByVal X As Double, ByVal Y As Double) As Integer

        Dim J, K, C, R, P As Integer
        Dim PT As JKCR

        X = X / PixelsPerLonDeg
        Y = Y / PixelsPerLatDeg

        IsLandUP = -1
        If (PointerON Or LandON) And LandVIEW Then
            PT = JKCRFromLL(X - D14Lon, Y + D14Lat, True)
            J = PT.J
            K = PT.K
            C = PT.C
            R = PT.R
            If LL_XY(J, K).NoOfLWs = 0 Then Exit Function
            P = LL_XY(J, K).Pointer
            If LLands(C, R, P) = 0 Then Exit Function
            C = C << 9
            K = K << 18
            J = J << 24
            IsLandUP = R + C + J + K
        End If

    End Function


    Friend Function IsWaterUP(ByVal X As Double, ByVal Y As Double) As Integer

        Dim J, K, C, R, P As Integer
        Dim PT As JKCR

        X = X / PixelsPerLonDeg
        Y = Y / PixelsPerLatDeg

        IsWaterUP = -1
        If (PointerON Or WaterON) And WaterVIEW Then
            PT = JKCRFromLL(X - D14Lon, Y + D14Lat, True)
            J = PT.J
            K = PT.K
            C = PT.C
            R = PT.R
            If WW_XY(J, K).NoOfLWs = 0 Then Exit Function
            P = WW_XY(J, K).Pointer
            If WWaters(C, R, P) = 0 Then Exit Function
            C = C << 9
            K = K << 18
            J = J << 24
            IsWaterUP = R + C + J + K
        End If


    End Function


    Friend Function IsMapUP(ByVal X As Double, ByVal Y As Double) As Integer

        Dim N As Integer
        Dim retval As Boolean

        IsMapUP = 0
        If PointerON And MapVIEW Then
            For N = 1 To NoOfMaps
                retval = IsPointInMap(N, X, Y)
                If retval Then
                    IsMapUP = N
                    Exit Function
                End If
            Next N
        End If
    End Function




    Private Sub ShowPopUPMenu()

        frmStart.NamePopUPMenu.Visible = True
        frmStart.Sep1PopUPMenu.Visible = True
        frmStart.CenterPopUPMenu.Visible = True
        frmStart.FlyToPopUPMenu.Visible = True
        frmStart.ZoomInPopUPMenu.Visible = True
        frmStart.ZoomOutPopUPMenu.Visible = True
        frmStart.Sep2PopUPMenu.Visible = True
        frmStart.DeletePopUPMenu.Visible = True

        'If frmStart.ShowBackgroundMenuItem.Enabled Then
        '    If frmStart.ShowBackgroundMenuItem.Checked Then
        '        frmStart.SaveBackGroundPopUpMenu.Visible = True
        '    End If
        'End If

        frmStart.Sep3PopUPMenu.Visible = True
        frmStart.PropertiesPopUPMenu.Visible = True


    End Sub


    Friend Sub HidePopUPMenu()

        frmStart.NamePopUPMenu.Visible = False
        frmStart.Sep1PopUPMenu.Visible = False
        frmStart.CenterPopUPMenu.Visible = False
        frmStart.FlyToPopUPMenu.Visible = False
        frmStart.PointFromAircraftPopUpMenu.Visible = False
        frmStart.ZoomInPopUPMenu.Visible = False
        frmStart.ZoomOutPopUPMenu.Visible = False
        frmStart.Sep2PopUPMenu.Visible = False
        frmStart.DeletePopUPMenu.Visible = False
        frmStart.SaveBackGroundPopUpMenu.Visible = False
        frmStart.TilePathToClipboardPopUpMenu.Visible = False
        frmStart.JoinAllPopUPMenu.Visible = False
        frmStart.SetWidthPopUpMenu.Visible = False
        frmStart.SetAltitudePopUpMenu.Visible = False
        frmStart.SetTransparencyPopUpMenu.Visible = False

        frmStart.MakeLinePopUPMenu.Visible = False
        frmStart.MakePolyPopUPMenu.Visible = False
        frmStart.ConvertToPolyPopUpMenu.Visible = False
        frmStart.SmoothPopUPMenu.Visible = False
        frmStart.SamplePopUPMenu.Visible = False
        frmStart.ManualCheckPopUPMenu.Visible = False
        frmStart.HolePopUpMenu.Visible = False
        frmStart.OuterPopUpMenu.Visible = False
        frmStart.CalibratePopUPMenu.Visible = False
        frmStart.Sep3PopUPMenu.Visible = False
        frmStart.PropertiesPopUPMenu.Visible = False

        frmStart.SnapQMIDPopUPMenu.Visible = False
        frmStart.FillQMIDPopUpMenu.Visible = False
        frmStart.SliceQMIDPopUpMenu.Visible = False

    End Sub




End Module