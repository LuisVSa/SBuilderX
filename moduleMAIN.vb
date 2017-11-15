Option Strict On
Option Explicit On

Module moduleMAIN

    Friend PolyInit As Integer = 15   ' see these! exclude excludes?
    Friend LineInit As Integer = 4   ' see these! exclude excludes?

    'Friend Structure Lod5
    '    Dim Lod5X As Integer
    '    Dim Lod5Y As Integer
    '    Dim X As Integer
    '    Dim Y As Integer
    'End Structure

    Friend Structure Double_XY
        Dim X As Double
        Dim Y As Double
    End Structure
    Friend Structure Double_XYZ
        Dim X As Double
        Dim Y As Double
        Dim Z As Double
    End Structure

    Friend Structure Double_XYZW   ' used to fragment lines
        Dim X As Double
        Dim Y As Double
        Dim Z As Double
        Dim W As Double
    End Structure

    Friend Structure Slice   ' for polys
        Dim N As Integer     ' do not remember why used Long, so changed to Integer may Dick can test with his data
        Dim NC As Integer
        Dim C() As Integer
        Dim P() As Double_XY
    End Structure

    Friend Slices() As Slice
    Friend NoOfSlices As Integer

    Friend Structure Fragment  ' for lines
        Dim N As Integer       ' do not remember why used Long, so changed to Integer may Dick can test with his data
        Dim NC As Integer
        Dim C() As Integer
        Dim P() As Double_XYZW
    End Structure

    Friend Fragments() As Fragment
    Friend NoOfFragments As Integer

    Friend AppTitle As String = "SBuilderX"
    Friend ProjectName As String = ""

    Friend AppPath As String = My.Application.Info.DirectoryPath ' INI File App constant.
    Friend AppIni As String = My.Application.Info.DirectoryPath & "\SBuilderX.ini" ' INI File App constant.

    ' for Registration
    Friend ShowDonation As Boolean

    Friend IsFSX As Boolean   ' if terrain.cfg detected
    Friend SDKPath As String
    Friend SDKTerrain As String
    Friend SDKBglComp As String
    Friend OriginalTerrainCFG As Boolean
    Friend IsFSXTerrain As Boolean
    Friend IsFSXBGLComp As Boolean

    Friend MakeSlopeXY As Boolean ' if False SlopeX and SlopeY will be set to 0 before passing SHP to shp2vec

    Friend IsFSXPlugins As Boolean
    Friend SDKPlugins As String

    Friend FSPath As String
    Friend FSTextureFolder As String

    ' added October 2017
    Friend NameOfSim As String    ' exists in the INI
    Friend SimExe As String       ' deduced from NameOfSim
    Friend SimPath As String      ' exists in the INI
    Friend IgnoreFSX As Boolean   ' deduced from the NameOfSim

    Friend ShowLabels As Boolean

    Friend ImageFileName As String
    ' used by frmImage
    Friend ImageFileNameTrue As String ' used by the screen capture

    Friend Season As String

    ' indicate mode ON/OFF

    Friend PointerON As Boolean = False
    Friend ZoomON As Boolean = False

    ' used in the Select menu bar
    Friend AllSELECT As Boolean
    Friend SomeSelected As Boolean
    Friend NoOfPointsSelected As Integer = 0

    ' used by Lines and Polys
    Friend NoEndsSmooth As Boolean
    Friend CornerSmooth As Boolean
    Friend SampleDistance As Double
    Friend SmoothDistance As Double

    Friend MakeOnMany As Integer = 1

    'used in View menu bar
    Friend AllVIEW As Boolean
    Friend ViewON As Boolean = False
    Friend AircraftVIEW As Boolean
    Friend ShowAircraftPeriod As Integer  ' FSUIPC

    Friend DELAY As Boolean = False
    Friend WAIT As Boolean

    'used by the display routines

    Friend PanON As Boolean = False ' a PAN movement is under way
    Friend SelectON As Boolean = False ' the mouse is drawing a selection box
    Friend MoveON As Boolean = False  ' an element (point, line, ...) is moving
    Friend FirstMOVE As Boolean = False

    Friend LatDispNorth As Double ' latitude of display north border
    Friend LatDispSouth As Double ' latitude of display south border
    Friend LonDispWest As Double  ' longitude of display west border
    Friend LonDispEast As Double  ' longitude of display east border

    Friend LatDispCenter As Double ' latitude of display center point
    Friend LonDispCenter As Double ' longitude of display center point

    Friend CenterOnMouseWheel As Boolean  ' to be set through INI used in the mouse wheel Zoom

    Friend LatIniCenter As Double = 40
    Friend LonIniCenter As Double = -10

    Friend PixelsPerLatDeg As Double ' latitude resolution of display window
    Friend PixelsPerLonDeg As Double ' longitude resolution of display window
    Friend PixelsPerMeter As Double  ' longitude resolution of display window

    Friend DecimalDegrees As Boolean

    ' ON when inserting points in Lines or Polys
    Friend InsertON As Boolean

    Friend AuxXInt As Integer
    Friend AuxYInt As Integer

    Friend QMIDLevel As Integer
    Friend GridColor As Color
    Friend GridLODColor As Color
    Friend GridWidth As Integer
    Friend ZoomOnQMID As Boolean
    Friend LODLevel As Integer

    Friend BGLProjectFolder As String

    Friend AircraftAltitudeOffset As Double


    Friend BGLFolder As String ' from the INI

    Friend Zoom As Integer = 1   'changed from Double to Integer in October 2017
    Friend ARGBColor As Color   ' for the return of the transparency form

    Friend DeleteON As Boolean

    Friend AskDelete As Boolean
    Friend Dirty As Boolean
    Friend WorkFile As String

    Friend DisplayWidth As Integer ' value in pixels
    Friend DisplayHeight As Integer ' value in pixels
    Friend DisplayCenterX As Integer ' value in pixels
    Friend DisplayCenterY As Integer ' value in pixels

    Friend BitmapBuffer As Bitmap

    Friend Sub RebuildDisplay()

        ' builds the buffer
        ' copies the buffer to the display

        If WAIT Then Exit Sub
        WAIT = True

        frmStart.BuildBitmapBuffer()
        frmStart.UpdateDisplay()

        WAIT = False

    End Sub


    Friend Sub SetDispCenter(ByVal X As Integer, ByVal Y As Integer)

        ' sets center and limits of the display
        ' call SetDispCenter(0,0) to set display limits only
        ' it resets the zoom (to adjust Pixels per Long Degree)
        ' X and Y bring the desired shift in pixels

        Dim LatSouthLimit As Double
        Dim LatNorthLimit As Double

        Dim NewLatDispCenter As Double

        Dim NewLonDispEast As Double
        Dim NewLonDispWest As Double

        Dim DX, DY As Double  ' shift 

        DY = 90.0 / (2.0 ^ Zoom)
        LatNorthLimit = 90.0 - DY
        LatSouthLimit = -90.0 + DY

        DY = -CDbl(Y / PixelsPerLatDeg)
        NewLatDispCenter = LatDispCenter + DY
        If NewLatDispCenter > LatNorthLimit Then
            LatDispCenter = LatNorthLimit
        ElseIf NewLatDispCenter < LatSouthLimit Then
            LatDispCenter = LatSouthLimit
        Else
            LatDispCenter = NewLatDispCenter
        End If

        ResetZoom()

        DX = CDbl(X / PixelsPerLonDeg)

        NewLonDispEast = LonDispEast + DX
        NewLonDispWest = LonDispWest + DX
        If NewLonDispEast > 180 Then
            DX = DX - (NewLonDispEast - 180)
        End If
        If NewLonDispWest < -180 Then
            DX = DX - (NewLonDispWest + 180)
        End If
        LonDispCenter = LonDispCenter + DX

        LatDispNorth = LatDispCenter + CDbl(DisplayCenterY / PixelsPerLatDeg)
        LatDispSouth = LatDispCenter - CDbl(DisplayCenterY / PixelsPerLatDeg)

        LonDispWest = LonDispCenter - CDbl(DisplayCenterX / PixelsPerLonDeg)
        LonDispEast = LonDispCenter + CDbl(DisplayCenterX / PixelsPerLonDeg)

        If LonDispWest < -180 Then
            DX = 180 + LonDispWest
            LonDispWest = LonDispWest - DX
            LonDispEast = LonDispEast - DX
            LonDispCenter = LonDispCenter - DX
        End If

        If LonDispEast > 180 Then
            DX = LonDispEast - 180
            LonDispEast = LonDispEast - DX
            LonDispEast = LonDispEast - DX
            LonDispCenter = LonDispCenter - DX
        End If

    End Sub


    Friend Function IsCenterDisplay(ByVal X As Integer, ByVal Y As Integer) As Boolean

        Dim X1, X2, Y1, Y2, YY As Integer


        IsCenterDisplay = True

        X1 = CInt(DisplayWidth * 0.05)
        If X < X1 Then Exit Function

        X2 = CInt(DisplayWidth * 0.95)
        If X > X2 Then Exit Function

        YY = DisplayHeight - frmStart.MenuStrip.Height - frmStart.StatusStrip.Height - frmStart.ToolStrip.Height
        YY = CInt(0.05 * YY)

        Y1 = YY + frmStart.MenuStrip.Height + frmStart.ToolStrip.Height
        If Y < Y1 Then Exit Function

        Y2 = DisplayHeight - YY - frmStart.StatusStrip.Height
        If Y > Y2 Then Exit Function

        IsCenterDisplay = False

    End Function

    Friend Sub SetDelay(ByVal N As Integer)

        If frmStart.Timer1.Enabled Then Exit Sub ' if delay is on then ignore

        If N > 0 Then
            frmStart.Timer1.Interval = N
            DELAY = True
            frmStart.Timer1.Enabled = True
            Exit Sub
        End If

        If N = 0 Then
            frmStart.Timer1.Enabled = False
            DELAY = False
        End If

    End Sub


    Friend MeasuringMeters As Boolean

    Friend Const MetersPerDegLat As Double = 111330.0#

    Friend Const D0Lon As Double = 360.0# / 3.0#
    Friend Const D1Lon As Double = D0Lon / 2.0#
    Friend Const D2Lon As Double = D1Lon / 2.0#
    Friend Const D3Lon As Double = D2Lon / 2.0#
    Friend Const D4Lon As Double = D3Lon / 2.0#
    Friend Const D5Lon As Double = D4Lon / 2.0#
    Friend Const D6Lon As Double = D5Lon / 2.0#
    Friend Const D7Lon As Double = D6Lon / 2.0#
    Friend Const D8Lon As Double = D7Lon / 2.0#
    Friend Const D9Lon As Double = D8Lon / 2.0#
    Friend Const D10Lon As Double = D9Lon / 2.0#
    Friend Const D11Lon As Double = D10Lon / 2.0#
    Friend Const D12Lon As Double = D11Lon / 2.0#
    Friend Const D13Lon As Double = D12Lon / 2.0#
    Friend Const D14Lon As Double = D13Lon / 2.0#

    Friend Const D16Lon As Double = D13Lon / 8.0#

    Friend Const D0Lat As Double = 180.0# / 2.0#
    Friend Const D1Lat As Double = D0Lat / 2.0#
    Friend Const D2Lat As Double = D1Lat / 2.0#
    Friend Const D3Lat As Double = D2Lat / 2.0#
    Friend Const D4Lat As Double = D3Lat / 2.0#
    Friend Const D5Lat As Double = D4Lat / 2.0#
    Friend Const D6Lat As Double = D5Lat / 2.0#
    Friend Const D7Lat As Double = D6Lat / 2.0#
    Friend Const D8Lat As Double = D7Lat / 2.0#
    Friend Const D9Lat As Double = D8Lat / 2.0#
    Friend Const D10Lat As Double = D9Lat / 2.0#
    Friend Const D11Lat As Double = D10Lat / 2.0#
    Friend Const D12Lat As Double = D11Lat / 2.0#
    Friend Const D13Lat As Double = D12Lat / 2.0#
    Friend Const D14Lat As Double = D13Lat / 2.0#
    Friend Const D16Lat As Double = D13Lat / 8.0#

    Friend Const D255Lon As Double = D13Lon / 255.0#
    Friend Const D255Lat As Double = D13Lat / 255.0#

    Friend Const D510Lon As Double = D255Lon / 2.0#
    Friend Const D510Lat As Double = D255Lat / 2.0#


    ' this is a minimum (not zero though!)
    Friend Const MinValue As Double = 0.0000000000000005#

    ' the following are used in photo scenery
    Friend Const D21Lon As Double = 360.0# / 768.0# / 32.0# / 256.0#
    Friend Const D21Lat As Double = 180.0# / 512.0# / 32.0# / 256.0#
    Friend Const D22Lon As Double = D21Lon / 2.0#
    Friend Const D22Lat As Double = D21Lat / 2.0#



    'Friend Const D5Lon  As Double = 360 / 96
    'Friend Const D5Lat As Double = 180 / 64
    Friend Const PI As Double = 3.14159265358979

    ' to go to other modules

    Friend Sub UnSelectAll()

        If SomeSelected = False Then
            NoOfPointsSelected = 0
            NoOfLinesSelected = 0
            NoOfPolysSelected = 0
            NoOfObjectsSelected = 0

            NoOfLandsSelected = 0
            NoOfWatersSelected = 0
            NoOfMapsSelected = 0
            NoOfExcludesSelected = 0

            frmStart.SelectAllMenuItem.Checked = False
            AllSELECT = False
            Exit Sub
        End If

        SelectAllMaps(False)
        SelectAllLands(False)
        SelectAllLines(False)
        SelectAllPolys(False)
        SelectAllWaters(False)
        SelectAllObjects(False)
        SelectAllExcludes(False)
        AllSELECT = False

        SomeSelected = False
        NoOfPointsSelected = 0
        NoOfLinesSelected = 0
        NoOfPolysSelected = 0
        NoOfObjectsSelected = 0

        NoOfLandsSelected = 0
        NoOfWatersSelected = 0
        NoOfMapsSelected = 0
        NoOfExcludesSelected = 0

        frmStart.SelectAllMenuItem.Checked = False

    End Sub

    Friend Sub SelectAll()

        NoOfPointsSelected = 0
        NoOfLinesSelected = 0
        NoOfPolysSelected = 0
        NoOfObjectsSelected = 0

        NoOfLandsSelected = 0
        NoOfWatersSelected = 0
        NoOfMapsSelected = 0
        NoOfExcludesSelected = 0

        SelectAllMaps(True)
        SelectAllLines(True)
        SelectAllPolys(True)
        SelectAllWaters(True)
        SelectAllLands(True)
        SelectAllObjects(True)
        SelectAllExcludes(True)

        SomeSelected = True

    End Sub

   
    Friend Sub SelectBoxed(ByVal X As Integer, ByVal Y As Integer)

        Dim X0, Y0 As Double
        Dim X1, Y1 As Double

        X0 = LonDispWest + AuxXInt / PixelsPerLonDeg
        Y0 = LatDispNorth - AuxYInt / PixelsPerLatDeg
        X1 = LonDispWest + X / PixelsPerLonDeg
        Y1 = LatDispNorth - Y / PixelsPerLatDeg

        ' point 0 will be NW and point 1 SE
        If X0 > X1 Then
            X1 = X0 + X1
            X0 = X1 - X0
            X1 = X1 - X0
        End If

        If Y1 > Y0 Then
            Y1 = Y0 + Y1
            Y0 = Y1 - Y0
            Y1 = Y1 - Y0
        End If

        If LineON Then
            SelectLinesInBox(X0, Y0, X1, Y1)
            Exit Sub
        End If

        If PolyON Then
            SelectPolysInBox(X0, Y0, X1, Y1)
            Exit Sub
        End If

        If ObjectON Then
            SelectObjectsInBox(X0, Y0, X1, Y1)
            Exit Sub
        End If


        If MapVIEW Then SelectMapsInBox(X0, Y0, X1, Y1)
        If LineVIEW Then SelectLinesInBox(X0, Y0, X1, Y1)
        If PolyVIEW Then SelectPolysInBox(X0, Y0, X1, Y1)
        If ExcludeVIEW Then SelectExcludesInBox(X0, Y0, X1, Y1)
        'If PhotoVIEW Then SelectPhotosInBox(X0, Y0, X1, Y1)
        If LandVIEW Then SelectLandsInBox(X0, Y0, X1, Y1)
        If WaterVIEW Then SelectWatersInBox(X0, Y0, X1, Y1)
        'If MeshVIEW Then SelectMeshesInBox(X0, Y0, X1, Y1)
        If ObjectVIEW Then SelectObjectsInBox(X0, Y0, X1, Y1)


    End Sub
    Friend Function InvertColor(ByVal Col As Color) As Color

        Dim A As Integer
        Dim R As Integer = 0
        Dim G As Integer = 0
        Dim B As Integer = 0
        Dim I As Integer

        A = Col.A
        R = Col.R
        G = Col.G
        B = Col.B

        I = R + G + B

        If I < 384 Then
            R = 255
            G = 255
            B = 255
        Else
            R = 0
            G = 0
            B = 0
        End If

        InvertColor = Color.FromArgb(A, R, G, B)

    End Function

    'Friend Function LL2Lod5(ByVal X As Double, ByVal Y As Double) As Lod5

    '    X = X + 180
    '    Y = 90 - Y

    '    X = X / D5Lon
    '    LL2Lod5.Lod5X = CInt(Fix(X))

    '    Y = Y / D5Lat
    '    LL2Lod5.Lod5Y = CInt(Fix(Y))

    '    LL2Lod5.X = CInt(Fix((X - LL2Lod5.Lod5X) * D5Lon / D13Lon))
    '    LL2Lod5.Y = CInt(Fix((Y - LL2Lod5.Lod5Y) * D5Lat / D13Lat))


    'End Function

    Friend Sub ResetZoom()

        If Zoom > GlobeOrTiles Then
            PixelsPerLatDeg = (2 ^ Zoom) * 256 / 180.0
        Else
            PixelsPerLatDeg = (2 ^ Zoom) * DisplayHeight / 180.0
        End If

        PixelsPerMeter = PixelsPerLatDeg / 111330.0#
        PixelsPerLonDeg = Math.Cos(LatDispCenter * PI / 180.0#) * PixelsPerLatDeg

    End Sub

    Friend Function MetersPerDegLon(ByVal lat As Double) As Double

        MetersPerDegLon = Math.Cos(lat * PI / 180.0#) * 111330.0#

    End Function

    Friend Function Str2Lat(ByVal lat As String) As Double

        Dim a As String
        Dim N, M As Integer
        Dim Neg As Boolean

        On Error GoTo error1

        If DecimalDegrees Then

            Str2Lat = CDbl(lat)

        Else
            lat = Replace(lat, "s", "S")
            lat = Replace(lat, "n", "N")

            Neg = False
            N = InStr(lat, "S")
            If N > 0 Then
                Neg = True
                lat = Replace(lat, "S", "")
            End If

            N = InStr(lat, "-")
            If N > 0 Then
                Neg = True
                lat = Replace(lat, "-", "")
            End If

            N = InStr(lat, "N")
            If N > 0 Then lat = Replace(lat, "N", "")

            lat = Replace(lat, ":", " ")
            lat = Replace(lat, Chr(176), " ")
            lat = Replace(lat, "'", " ")
            'lat = Replace(lat, "''", " ")
            lat = Replace(lat, "''", "")
            lat = Replace(lat, "   ", " ")
            lat = Replace(lat, "  ", " ")
            lat = Trim(lat)

            N = InStr(lat, " ")
            If N = 0 Then
                Str2Lat = CDbl(lat)
                If Neg Then Str2Lat = -1 * Str2Lat
                Exit Function
            End If
            a = Mid(lat, 1, N - 1)
            Str2Lat = CDbl(a)

            M = InStr(N + 1, lat, " ")
            If M = 0 Then
                a = Mid(lat, N + 1)
                Str2Lat = Str2Lat + CDbl(a) / 60
                If Neg Then Str2Lat = -1 * Str2Lat
                Exit Function
            End If
            a = Mid(lat, N + 1, M - N - 1)
            Str2Lat = Str2Lat + CDbl(a) / 60

            a = Mid(lat, M + 1)
            Str2Lat = Str2Lat + CDbl(a) / 3600

            If Neg Then Str2Lat = -1 * Str2Lat

        End If

        Exit Function


error1:
        Str2Lat = 100

    End Function

    Friend Function Str2Lon(ByVal lon As String) As Double

        Dim a As String
        Dim N, M As Integer
        Dim Neg As Boolean

        On Error GoTo error1

        If DecimalDegrees Then
            Str2Lon = CDbl(lon)
        Else

            lon = Replace(lon, "e", "E")
            lon = Replace(lon, "w", "W")

            Neg = False
            N = InStr(lon, "W")
            If N > 0 Then
                Neg = True
                lon = Replace(lon, "W", "")
            End If

            N = InStr(lon, "-")
            If N > 0 Then
                Neg = True
                lon = Replace(lon, "-", "")
            End If


            N = InStr(lon, "E")
            If N > 0 Then lon = Replace(lon, "E", "")

            lon = Replace(lon, ":", " ")
            lon = Replace(lon, Chr(176), " ")
            lon = Replace(lon, "'", " ")
            'lon = Replace(lon, "''", " ")
            lon = Replace(lon, "''", "'")
            lon = Replace(lon, "   ", " ")
            lon = Replace(lon, "  ", " ")
            lon = Trim(lon)

            N = InStr(lon, " ")

            If N = 0 Then
                Str2Lon = CDbl(lon)
                If Neg Then Str2Lon = -1 * Str2Lon
                Exit Function
            End If

            a = Mid(lon, 1, N - 1)
            Str2Lon = CDbl(a)

            M = InStr(N + 1, lon, " ")

            If M = 0 Then
                a = Mid(lon, N + 1)
                Str2Lon = Str2Lon + CDbl(a) / 60
                If Neg Then Str2Lon = -1 * Str2Lon
                Exit Function
            End If

            a = Mid(lon, N + 1, M - N - 1)
            Str2Lon = Str2Lon + CDbl(a) / 60

            a = Mid(lon, M + 1)
            Str2Lon = Str2Lon + CDbl(a) / 3600

            If Neg Then Str2Lon = -1 * Str2Lon

        End If

        Exit Function

error1:
        Str2Lon = 200

    End Function

    Friend Function Lon2Str(ByVal lon As Double) As String

        Dim a As String
        Dim X As Double
        Dim N As Integer

        If DecimalDegrees Then
            Lon2Str = Format(lon, "00.0000000")
        Else
            If lon < 0 Then
                a = "W"
                lon = -1 * lon
            Else
                a = "E"
            End If
            N = CInt(Int(lon))
            Lon2Str = CStr(N)
            X = (lon - CDbl(N)) * 60
            N = CInt(Int(X))
            Lon2Str = Lon2Str & Chr(176) & " " & Format(N, "00")
            X = (X - CDbl(N)) * 60
            X = System.Math.Round(X, 4)
            Lon2Str = Lon2Str & "' " & Format(X, "00.0000") & "'' " & a
        End If

    End Function

    Friend Function Lat2Str(ByVal lat As Double) As String

        Dim b As String
        Dim X As Double
        Dim N As Integer


        If DecimalDegrees Then
            Lat2Str = Format(lat, "00.0000000")
        Else
            If lat < 0 Then
                b = "S"
                lat = -1 * lat
            Else
                b = "N"
            End If
            N = CInt(Int(lat))
            Lat2Str = CStr(N)
            X = (lat - CDbl(N)) * 60
            N = CInt(Int(X))
            Lat2Str = Lat2Str & Chr(176) & " " & Format(N, "00")
            X = (X - CDbl(N)) * 60
            X = System.Math.Round(X, 4)
            Lat2Str = Lat2Str & "' " & Format(X, "00.0000") & "'' " & b
        End If

    End Function


    Friend Function IsSelection(ByVal X As Integer, ByVal Y As Integer) As Boolean

        ' on entry X Y contain distance from origin of display in pixels

        Dim XD, YD As Double

        IsSelection = True

        If IsPointInLine(X, Y) Then Exit Function
        If IsPointInPoly(X, Y) Then Exit Function ' changed from double to integer

        XD = LonDispWest * PixelsPerLonDeg + X
        YD = LatDispNorth * PixelsPerLatDeg - Y

        If IsObjectSelected(XD, YD) Then Exit Function
        If IsLineSelected(XD, YD) Then Exit Function
        If IsPolySelected(XD, YD) Then Exit Function
        If IsMapSelected(XD, YD) Then Exit Function
        If IsExcludeSelected(XD, YD) Then Exit Function

        IsSelection = False

    End Function

    Friend Function IsPtInDisplay(ByVal x As Double, ByVal y As Double) As Boolean

        IsPtInDisplay = False
        If x < LonDispWest Then Exit Function
        If x > LonDispEast Then Exit Function
        If y > LatDispNorth Then Exit Function
        If y < LatDispSouth Then Exit Function
        IsPtInDisplay = True

    End Function

    Friend Function IsSegInDisplay(ByVal X1 As Double, ByVal Y1 As Double, ByVal X2 As Double, ByVal Y2 As Double) As Boolean

        Dim M, DX, DY, B As Double


        IsSegInDisplay = False
        If Y1 > LatDispNorth Then If Y2 > LatDispNorth Then Exit Function
        If Y1 < LatDispSouth Then If Y2 < LatDispSouth Then Exit Function
        If X1 < LonDispWest Then If X2 < LonDispWest Then Exit Function
        If X1 > LonDispEast Then If X2 > LonDispEast Then Exit Function

        DX = X2 - X1
        DY = Y2 - Y1

        If System.Math.Abs(DX) > System.Math.Abs(DY) Then
            M = DY / DX
            B = Y1 - M * X1
            Y1 = M * LonDispWest + B
            Y2 = M * LonDispEast + B
            If Y1 > LatDispNorth Then If Y2 > LatDispNorth Then Exit Function
            If Y1 < LatDispSouth Then If Y2 < LatDispSouth Then Exit Function
            IsSegInDisplay = True
            Exit Function
        Else
            M = DX / DY
            B = X1 - M * Y1
            X1 = M * LatDispSouth + B
            X2 = M * LatDispNorth + B
            If X1 < LonDispWest Then If X2 < LonDispWest Then Exit Function
            If X1 > LonDispEast Then If X2 > LonDispEast Then Exit Function
            IsSegInDisplay = True
            Exit Function
        End If

    End Function

    Friend Sub MoveSelected(ByVal X1 As Integer, ByVal Y1 As Integer)

        Dim X, Y As Double

        X = CDbl(X1 / PixelsPerLonDeg)
        Y = CDbl(Y1 / PixelsPerLatDeg)

        If LineON Then
            MoveSelectedLines(X, Y)
            Exit Sub
        End If

        If PolyON Then
            MoveSelectedPolys(X, Y)
            Exit Sub
        End If

        If ObjectON Then
            MoveSelectedObjects(X, Y)
            Exit Sub
        End If

        MoveSelectedMaps(X, Y)
        MoveSelectedExcludes(X, Y)
        MoveSelectedLines(X, Y)
        MoveSelectedPolys(X, Y)
        MoveSelectedObjects(X, Y)

    End Sub

    Friend Sub DeleteSelected()

        Dim N As Integer

        DeleteSelectedLines()
        DeleteSelectedPointsInLines()

        DeleteSelectedPolys()
        DeleteSelectedPointsInPolys()

        For N = NoOfMaps To 1 Step -1
            If Maps(N).Selected Then DeleteMap(N)
        Next N

        For N = NoOfObjects To 1 Step -1
            If Objects(N).Selected Then DeleteThisObject(N)
        Next N

        For N = NoOfExcludes To 1 Step -1
            If Excludes(N).Selected Then DeleteExclude(N)
        Next N

        DeleteSelectedLands()
        DeleteSelectedWaters()

        SomeSelected = False
        NoOfPointsSelected = 0
        NoOfLinesSelected = 0
        NoOfPolysSelected = 0
        NoOfLandsSelected = 0
        NoOfWatersSelected = 0
        NoOfObjectsSelected = 0
        NoOfMapsSelected = 0

        Dirty = True

    End Sub

    Friend Sub CheckFolders()

        If BGLProjectFolder = "" Then
            BGLProjectFolder = BGLFolder
        End If
        Dim A As String

        Try
            If Directory.Exists(BGLProjectFolder) = False Then Directory.CreateDirectory(BGLProjectFolder)
        Catch ex As Exception
            A = "Could not create " & vbCrLf & BGLProjectFolder
            MsgBox(A, MsgBoxStyle.Exclamation)
            BGLProjectFolder = BGLFolder
        End Try

    End Sub

    Friend Function LLFromJKCR(ByVal J As Integer, ByVal K As Integer, ByVal C As Integer, ByVal R As Integer) As Double_XY

        LLFromJKCR.X = J * D5Lon + C * D13Lon - 180
        LLFromJKCR.Y = 90 - K * D5Lat - R * D13Lat

    End Function

    Friend Function JKCRFromLL(ByVal X As Double, ByVal Y As Double, ByVal NW As Boolean) As JKCR

        Dim J, K As Integer

        If NW Then
            Y = Y - D14Lat
            X = X + D14Lon
        Else
            Y = Y + D14Lat
            X = X - D14Lon
        End If

        X = X + 180
        X = X / D5Lon
        J = CInt(Fix(X))
        JKCRFromLL.J = J

        Y = 90 - Y
        Y = Y / D5Lat
        K = CInt(Fix(Y))
        JKCRFromLL.K = K

        JKCRFromLL.C = CInt(Math.Round((X - J) * D5Lon / D13Lon))
        JKCRFromLL.R = CInt(Math.Round((Y - K) * D5Lat / D13Lat))


    End Function

    Friend Function ExecCmd(ByVal str As String) As Integer

        Dim myCommand As String
        Dim myArgs As String
        Dim N As Integer = InStr(str, " ")

        ExecCmd = 0

        Try
            If N = 0 Then
                Dim myProcess As New Process
                myProcess = Process.Start(str)
                myProcess.WaitForExit()
                ExecCmd = myProcess.ExitCode
                myProcess.Dispose()
            Else
                myCommand = Mid(str, 1, N - 1)
                myArgs = Mid(str, N + 1)
                Dim myProcess As New Process
                myProcess = Process.Start(myCommand, myArgs)
                myProcess.WaitForExit()
                ExecCmd = myProcess.ExitCode
                myProcess.Dispose()
            End If
        Catch ex As Exception
            myCommand = "Error in processing the following command:" & vbCrLf
            myCommand = myCommand & str
            MsgBox(str, MsgBoxStyle.Critical)
        End Try

    End Function


End Module


