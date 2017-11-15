Option Strict Off
Option Explicit On

Imports System.Windows.Forms

Friend Class FrmCalibrate

    Private ThisMap As Integer

    Private Pt1Lat As Double
    Private Pt2Lat As Double
    Private Pt1Lon As Double
    Private Pt2Lon As Double

    Private NLAT As Double
    Private SLAT As Double
    Private WLON As Double
    Private ELON As Double
    Private ROWS As Integer
    Private COLS As Integer

    Private FlagNW As Boolean
    Private FlagSE As Boolean

    Private ROW1 As Double
    Private ROW2 As Double
    Private COL1 As Double
    Private COL2 As Double

    Private DoBackUp As Boolean

    Private Sub CmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click

        Dispose()

    End Sub

    Private Sub CmdCalibrate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCalibrate.Click

        Dim WL, NL, SL, EL As Double
        Dim LatDegPerPx, LonDegPerPx As Double
        Dim CnLat, CnLon As Double
        Dim PxLat, PxLon As Double

        If Not ValidateLatLon() Then
            MsgBox("Check Latitude & Longitude Values")
            Exit Sub
        End If

        If System.Math.Abs(ROW2 - ROW1) < 2 Then
            If Not optLon.Checked Then
                MsgBox("Check Y Pixel Values")
                Exit Sub
            End If
        End If

        If System.Math.Abs(COL2 - COL1) < 2 Then
            If Not optLat.Checked Then
                MsgBox("Check X Pixel Values")
                Exit Sub
            End If
        End If

        LatDegPerPx = (Pt1Lat - Pt2Lat) / (ROW2 - ROW1)
        LonDegPerPx = (Pt2Lon - Pt1Lon) / (COL2 - COL1)

        CnLat = (Pt1Lat + Pt2Lat) / 2
        CnLon = (Pt1Lon + Pt2Lon) / 2
        PxLat = CDbl((ROW1 + ROW2) / 2)
        PxLon = CDbl((COL2 + COL1) / 2)

        ResetZoom()

        If optLat.Checked Then
            LonDegPerPx = LatDegPerPx * PixelsPerLatDeg / PixelsPerLonDeg
        End If

        If optLon.Checked Then
            LatDegPerPx = LonDegPerPx * PixelsPerLonDeg / PixelsPerLatDeg
        End If

        NL = CnLat + PxLat * LatDegPerPx
        SL = CnLat - (ROWS - PxLat) * LatDegPerPx

        WL = CnLon - PxLon * LonDegPerPx
        EL = CnLon + (COLS - PxLon) * LonDegPerPx

        BackUp()

        Maps(ThisMap).NLAT = NL
        Maps(ThisMap).SLAT = SL
        Maps(ThisMap).ELON = EL
        Maps(ThisMap).WLON = WL

        SetBitmapSeason()
        RebuildDisplay()

        Dispose()

    End Sub

    Private Sub CmdHelp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdHelp.Click

        Dim A As String

        A = "In order to calibrate a map, the geographic and the bitmap locations"
        A = A & vbCrLf & "of 2 points are needed. In particular you need to know:"
        A = A & vbCrLf
        A = A & vbCrLf & "- lat/lon and X/Y of point P1"
        A = A & vbCrLf & "- lat/lon and X/Y of point P2."
        A = A & vbCrLf
        A = A & vbCrLf & "You can manually enter the lat/lon values of a known"
        A = A & vbCrLf & "point in the lat/lon boxes or you can press the ""Change"""
        A = A & vbCrLf & "command button on the left side of each frame. In this"
        A = A & vbCrLf & "case the lat/lon values of the clicked point will be"
        A = A & vbCrLf & "copied into the lat/lon boxes."
        A = A & vbCrLf
        A = A & vbCrLf & "You also need to enter the X/Y bitmap pixel locations of"
        A = A & vbCrLf & "the 2 calibrating points. You can either enter these"
        A = A & vbCrLf & "values directly into the corresponding boxes or you can"
        A = A & vbCrLf & "press the ""Change"" command button on the right side of the"
        A = A & vbCrLf & "frames. In this case the clicked the pixel will be used in"
        A = A & vbCrLf & "the setting of the X/Y values."


        MsgBox(A, MsgBoxStyle.Information)

    End Sub

    Private Sub CmdLL1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLL1.Click


        CalibratePT1 = True
        CalibratePT2 = False
        CalibratePixel = False
        Hide()

    End Sub

    Private Sub CmdLL2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLL2.Click

        CalibratePT1 = False
        CalibratePT2 = True
        CalibratePixel = False
        Hide()

    End Sub

    Private Sub CmdPP1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPP1.Click


        CalibratePT1 = True
        CalibratePT2 = False
        CalibratePixel = True
        Hide()

    End Sub

    Private Sub CmdPP2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPP2.Click

        CalibratePT1 = False
        CalibratePT2 = True
        CalibratePixel = True
        Hide()

    End Sub



    Private Function ValidateLatLon() As Boolean

        ValidateLatLon = False

        Pt1Lat = Str2Lat(txtLat1.Text)
        Pt2Lat = Str2Lat(txtLat2.Text)
        Pt2Lon = Str2Lon(txtLon2.Text)
        Pt1Lon = Str2Lon(txtLon1.Text)

        If Pt1Lat > 90 Then Exit Function
        If Pt1Lat < -90 Then Exit Function
        If Pt2Lat > 90 Then Exit Function
        If Pt2Lat < -90 Then Exit Function

        If Pt1Lon > 180 Then Exit Function
        If Pt1Lon < -180 Then Exit Function
        If Pt2Lon > 180 Then Exit Function
        If Pt2Lon < -180 Then Exit Function

        ValidateLatLon = True

    End Function

    Public Sub ReturnCalPT1(ByVal Button As Short, ByVal X As Integer, ByVal Y As Integer)

        Dim X1, Y1 As Double
        Dim X0, Y0 As Double

        frmStart.SetMouseIcon()

        CalibratePT2 = False
        CalibratePT1 = False

        X0 = LonDispWest + X / PixelsPerLonDeg
        Y0 = LatDispNorth - Y / PixelsPerLatDeg



        If Button = 2 Then Exit Sub

        If Not CalibratePixel Then
            txtLat1.Text = Lat2Str(Y0)
            txtLon1.Text = Lon2Str(X0)
            Exit Sub
        End If

        X1 = LonDispWest * PixelsPerLonDeg + X
        Y1 = LatDispNorth * PixelsPerLatDeg - Y

        If Not IsPointInsideMap(ThisMap, X1, Y1) Then
            MsgBox("Point is not on the map!")
            Exit Sub
        End If

        Y1 = NLAT - SLAT
        X1 = ELON - WLON

        Y0 = NLAT - Y0
        X0 = X0 - WLON

        Y0 = Y0 / Y1
        X0 = X0 / X1

        ROW1 = Y0 * ROWS
        COL1 = X0 * COLS

        txtPX1.Text = CStr(System.Math.Round(COL1))
        txtPY1.Text = CStr(System.Math.Round(ROW1))

    End Sub

    Public Sub ReturnCalPT2(ByVal Button As Short, ByVal X As Integer, ByVal Y As Integer)

        Dim X1, Y1 As Double
        Dim X0, Y0 As Double

        frmStart.SetMouseIcon()

        CalibratePT2 = False
        CalibratePT1 = False

        X0 = LonDispWest + X / PixelsPerLonDeg
        Y0 = LatDispNorth - Y / PixelsPerLatDeg

        If Button = 2 Then Exit Sub

        If Not CalibratePixel Then
            txtLat2.Text = Lat2Str(Y0)
            txtLon2.Text = Lon2Str(X0)
            Exit Sub
        End If

        X1 = LonDispWest * PixelsPerLonDeg + X
        Y1 = LatDispNorth * PixelsPerLatDeg - Y

        If Not IsPointInsideMap(ThisMap, X1, Y1) Then
            MsgBox("Point is not on the map!")
            Exit Sub
        End If

        Y1 = NLAT - SLAT
        X1 = ELON - WLON

        Y0 = NLAT - Y0
        X0 = X0 - WLON

        Y0 = Y0 / Y1
        X0 = X0 / X1

        ROW2 = Y0 * ROWS
        COL2 = X0 * COLS

        txtPX2.Text = CStr(System.Math.Round(COL2))
        txtPY2.Text = CStr(System.Math.Round(ROW2))

    End Sub


    Private Sub FrmCalibrate_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        FrmMapsP.Hide()
        DoBackUp = False

        ThisMap = POPIndex

        NLAT = Maps(ThisMap).NLAT
        SLAT = Maps(ThisMap).SLAT
        ELON = Maps(ThisMap).ELON
        WLON = Maps(ThisMap).WLON
        ROWS = Maps(ThisMap).ROWS
        COLS = Maps(ThisMap).COLS

        ROW1 = 0
        COL1 = 0
        ROW2 = ROWS
        COL2 = COLS

        txtLat1.Text = Lat2Str(NLAT)
        txtLat2.Text = Lat2Str(SLAT)
        txtLon2.Text = Lon2Str(ELON)
        txtLon1.Text = Lon2Str(WLON)

        txtPX1.Text = CStr(COL1)
        txtPY1.Text = CStr(ROW1)
        txtPX2.Text = CStr(COL2)
        txtPY2.Text = CStr(ROW2)

    End Sub




End Class
