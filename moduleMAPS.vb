Option Strict Off
Option Explicit On

Imports System.net
Imports System.io
Imports System.Xml

Module moduleMAPS

    <Serializable()> Friend Structure Map
        Dim Name As String
        Dim Selected As Boolean
        Dim COLS As Integer ' for the selected map can be one of 0 1 or 2 / not exported!
        Dim ROWS As Integer
        Dim NLAT As Double
        Dim SLAT As Double
        Dim WLON As Double
        Dim ELON As Double
        Dim BMPSu As String
        Dim BMPWi As String
        Dim BMPFa As String
        Dim BMPSp As String
        Dim BMPHw As String
        Dim BMPLm As String
    End Structure

    Friend ImgMaps() As Image

    Friend NoOfMaps As Integer = 0
    Friend NoOfMapsSelected As Integer = 0
    Friend Maps() As Map

    'Friend NokiaZoom As Integer
    'Friend NokiaType As Integer
    'Friend Here_app_id As String
    'Friend Here_app_code As String

    Friend GoogleMapsType As String
    Friend GoogleMapsAPI As String

    Friend ArcGisMapsType As String

    Friend ShowSimpleMaps As Boolean = False
    Friend MapVIEW As Boolean = False
    Friend BorderON As Boolean

    Friend CalibratePT1 As Boolean
    Friend CalibratePT2 As Boolean
    Friend CalibratePixel As Boolean = False

    Private Const two_pi As Double = 2.0 * PI
    Private Const pi_360 As Double = PI / 360.0
    Private Const pi_180 As Double = PI / 180.0
    Private Const pi_4 As Double = PI / 4.0
    Private Const pi_2 As Double = PI / 2.0
    Private Const x256_180 As Double = 256 / 180
    Private Const x256_pi As Double = 256 / PI

    Friend Sub AddNewMap()

        Dim DataFile As String
        Dim East, North, South, West As Double
        Dim Flag As Boolean

        Dim Name As String = "MAP" & Format(NoOfMaps, "00")

        Dim A As String
        A = "Windows Bitmap Format (*.BMP)|*.bmp"
        A = A & "|Jpeg File Interchange Format (*.JPG)|*.jpg"
        A = A & "|Tag Image File Format (*.TIF)|*.tif"
        A = A & "|Graphics Interchange Format (*.GIF)|*.gif"
        A = A & "|Portable Network Graphics (*.PNG)|*.png"

        Dim B As String = "SBuilderX: Open Image File"

        Dim myFile As String = FileNameToOpen(A, B, "BMP")
        Dim geoTiff As Boolean
        Dim Cols, Rows As Integer

        frmStart.Cursor = Cursors.AppStarting

        If myFile <> "" Then
            If Season <> "Summer" Then
                Season = "Summer"
                SetBitmapSeason()
            End If
            NoOfMaps = NoOfMaps + 1
            ReDim Preserve ImgMaps(NoOfMaps)
            Flag = GetImageParameters(myFile, Cols, Rows, North, South, West, East, geoTiff)
            If Flag = False Then
                NoOfMaps = NoOfMaps - 1
                MsgBox("The image has a wrong file format!", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Else
            Exit Sub
        End If

        ReDim Preserve Maps(NoOfMaps)

        Flag = False
        If geoTiff Then Flag = True

        ' as per Dick
        ' DataFile = Path.GetFileNameWithoutExtension(myFile) & ".TXT"
        DataFile = Path.ChangeExtension(myFile, "TXT")

        If File.Exists(DataFile) Then
            Flag = True
            North = ReadIniDouble(DataFile, "GEOGRAPHIC", "North")
            South = ReadIniDouble(DataFile, "GEOGRAPHIC", "South")
            West = ReadIniDouble(DataFile, "GEOGRAPHIC", "West")
            East = ReadIniDouble(DataFile, "GEOGRAPHIC", "East")
            Name = ReadIniValue(DataFile, "GEOGRAPHIC", "Name")
        End If

        If South >= North Or West >= East Then Flag = False

        BackUp()

        If Flag Then
            Maps(NoOfMaps).NLAT = North
            Maps(NoOfMaps).SLAT = South
            Maps(NoOfMaps).ELON = East
            Maps(NoOfMaps).WLON = West
        Else
            Maps(NoOfMaps).NLAT = LatDispCenter + 0.1
            Maps(NoOfMaps).SLAT = LatDispCenter - 0.1
            Maps(NoOfMaps).ELON = LonDispCenter + 0.1
            Maps(NoOfMaps).WLON = LonDispCenter - 0.1
        End If

        Maps(NoOfMaps).Name = Name
        Maps(NoOfMaps).Selected = False

        Maps(NoOfMaps).BMPSu = myFile
        Maps(NoOfMaps).BMPSp = myFile
        Maps(NoOfMaps).BMPFa = myFile
        Maps(NoOfMaps).BMPWi = myFile
        Maps(NoOfMaps).BMPHw = myFile
        Maps(NoOfMaps).BMPLm = myFile

        Maps(NoOfMaps).COLS = Cols
        Maps(NoOfMaps).ROWS = Rows

        LonDispCenter = (Maps(NoOfMaps).WLON + Maps(NoOfMaps).ELON) / 2
        LatDispCenter = (Maps(NoOfMaps).NLAT + Maps(NoOfMaps).SLAT) / 2

        frmStart.SummerMapMenuItem.Checked = True
        frmStart.ViewMapsMenuItem.Enabled = True

        ViewON = True
        Zoom = 8
        ResetZoom()
        SetDispCenter(0, 0)
        RebuildDisplay()
        Dirty = True

        frmStart.Cursor = Cursors.Default

        If Flag = False Then
            MsgBox("You may need to calibrate this bitmap!", MsgBoxStyle.OkOnly)
            POPIndex = NoOfMaps
            FrmMapsP.ShowDialog()
        End If

    End Sub

    'Friend Sub AddHereNokiaMap()

    '    Dim myfile As String

    '    If My.Computer.Network.IsAvailable = False Then
    '        MsgBox("There is no Internet connection!", MsgBoxStyle.Information)
    '        Exit Sub
    '    End If

    '    If Here_app_id = "DemoAppId01082013GAL" Then
    '        myfile = "You are using demo application Id and Code keys! In order to" & vbCrLf
    '        myfile = myfile & "avoid this message, please go to the Here/Nokia web site:" & vbCrLf & vbCrLf
    '        myfile = myfile & "http://developer.here.com/rest-apis" & vbCrLf & vbCrLf
    '        myfile = myfile & "and apply for your own keys. Then, open the SBuilderX.ini" & vbCrLf
    '        myfile = myfile & "file and replace the demo keys with your own keys!"
    '        MsgBox(myfile, MsgBoxStyle.Information)
    '    End If

    '    If FrmGoogleMaps.ShowDialog = Windows.Forms.DialogResult.Cancel Then
    '        Exit Sub
    '    End If

    '    frmStart.Cursor = Cursors.WaitCursor

    '    Dim NokiaSize As Integer = 2048

    '    Dim index As Integer
    '    NoOfMaps = NoOfMaps + 1
    '    ReDim Preserve ImgMaps(NoOfMaps)

    '    Dim makeurl As String
    '    makeurl = "http://image.maps.cit.api.here.com/mia/1.6/mapview?app_id=DemoAppId01082013GAL&app_code=AJKnXv84fjrb0KIHawS0Tg"
    '    makeurl = makeurl & "&nocp=1&nodot=1&c=" & Trim(Str(LatDispCenter)) & "," & Trim(Str(LonDispCenter))
    '    makeurl = makeurl & "&f=0&t=" & NokiaType & "&h=" & NokiaSize & "&w=" & NokiaSize & "&z=" & NokiaZoom
    '    Debug.Print(makeurl)

    '    Try
    '        Dim req As Net.HttpWebRequest = DirectCast(Net.HttpWebRequest.Create(makeurl), Net.HttpWebRequest)
    '        Dim res As Net.HttpWebResponse = DirectCast(req.GetResponse, Net.HttpWebResponse)
    '        index = res.ContentType.IndexOf("image")
    '        ImgMaps(NoOfMaps) = Image.FromStream(res.GetResponseStream)
    '        res.Close()
    '    Catch ex As Exception
    '        MsgBox("Could not get a Here Nokia image!", MsgBoxStyle.Critical)
    '        frmStart.Cursor = Cursors.Default
    '        NoOfMaps = NoOfMaps - 1
    '        Exit Sub
    '    End Try

    '    ReDim Preserve Maps(NoOfMaps)

    '    Maps(NoOfMaps).Name = "MAP" & Format(NoOfMaps, "00")
    '    Maps(NoOfMaps).Selected = False

    '    Dim LA, LO As String

    '    LA = Format(Val(LatDispCenter), "00.000000")
    '    LO = Format(Val(LonDispCenter), "000.000000")

    '    myfile = AppPath & "\Tools\Work\Nokia_" & LA & "_" & LO & "_" & Zoom & ".png"
    '    Try
    '        If Not (ImgMaps(NoOfMaps) Is Nothing) Then
    '            ImgMaps(NoOfMaps).Save(myfile)
    '        End If
    '    Catch ex As Exception
    '        MsgBox("There was a problem saving the image!", MsgBoxStyle.Exclamation)
    '    End Try

    '    Maps(NoOfMaps).BMPSu = myfile
    '    Maps(NoOfMaps).BMPSp = myfile
    '    Maps(NoOfMaps).BMPFa = myfile
    '    Maps(NoOfMaps).BMPWi = myfile
    '    Maps(NoOfMaps).BMPHw = myfile
    '    Maps(NoOfMaps).BMPLm = myfile

    '    Maps(NoOfMaps).COLS = NokiaSize
    '    Maps(NoOfMaps).ROWS = NokiaSize

    '    Dim CLat, CLon As Integer
    '    CLon = XMPixFromLon(LonDispCenter, (NokiaZoom - 1))
    '    CLat = YMPixFromLat(LatDispCenter, (NokiaZoom - 1))

    '    Dim LN, LS, LW, LE As Double

    '    LN = LatFromYMPix(CLat + NokiaSize / 2, (NokiaZoom - 1))
    '    LS = LatFromYMPix(CLat - NokiaSize / 2, (NokiaZoom - 1))
    '    LW = LonFromXMPix(CLon - NokiaSize / 2, (NokiaZoom - 1))
    '    LE = LonFromXMPix(CLon + NokiaSize / 2, (NokiaZoom - 1))

    '    If NokiaZoom < 8 Then
    '        FileMercator2LatLon(LN, LS, myfile, myfile)
    '        ImgMaps(NoOfMaps) = Image.FromFile(myfile)
    '    End If

    '    Maps(NoOfMaps).NLAT = LN
    '    Maps(NoOfMaps).SLAT = LS
    '    Maps(NoOfMaps).ELON = LE
    '    Maps(NoOfMaps).WLON = LW

    '    frmStart.SummerMapMenuItem.Checked = True
    '    frmStart.ViewMapsMenuItem.Enabled = True

    '    MapVIEW = True
    '    ViewON = True
    '    Zoom = NokiaZoom - 1
    '    ResetZoom()
    '    SetDispCenter(0, 0)
    '    RebuildDisplay()
    '    Dirty = True
    '    frmStart.Cursor = Cursors.Default
    '    frmStart.StatusZoom.Text = "Zoom = " & CStr(Zoom)

    'End Sub

    Friend Sub AddArcGisMap()

        Dim myfile As String

        If My.Computer.Network.IsAvailable = False Then
            MsgBox("There is no Internet connection!", MsgBoxStyle.Information)
            Exit Sub
        End If

        If frmArcGisMap.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        FrmStart.Cursor = Cursors.WaitCursor
        Dim index As Integer
        NoOfMaps = NoOfMaps + 1
        ReDim Preserve ImgMaps(NoOfMaps)

        Dim CLat, CLon As Integer
        CLon = XMPixFromLon(LonDispCenter, Zoom)
        CLat = YMPixFromLat(LatDispCenter, Zoom)

        Dim msize As Integer = 4096
        Dim LN, LS, LW, LE As Double

        LN = LatFromYMPix(CLat + msize / 2, Zoom)
        LS = LatFromYMPix(CLat - msize / 2, Zoom)
        LW = LonFromXMPix(CLon - msize / 2, Zoom)
        LE = LonFromXMPix(CLon + msize / 2, Zoom)

        Dim makeurl As String
        makeurl = "https://services.arcgisonline.com/arcgis/rest/services/World_" & ArcGisMapsType
        makeurl = makeurl & "/MapServer/export?bbox=" & Trim(Str(LW)) & "," & Trim(Str(LS))
        makeurl = makeurl & "," & Trim(Str(LE)) & "," & Trim(Str(LN))
        makeurl = makeurl & "&bboxSR=4326&size=" & msize & "," & msize & "&f=image"

        'MsgBox(makeurl)

        Try
            Dim req As Net.HttpWebRequest = DirectCast(Net.HttpWebRequest.Create(makeurl), Net.HttpWebRequest)
            Dim res As Net.HttpWebResponse = DirectCast(req.GetResponse, Net.HttpWebResponse)
            index = res.ContentType.IndexOf("image")
            ImgMaps(NoOfMaps) = Image.FromStream(res.GetResponseStream)
            res.Close()
        Catch ex As Exception
            ' MsgBox("Could not get a Arc Gis Map image!", MsgBoxStyle.Critical)
            MsgBox("Could not get an image at Zoom=" & Zoom & "!", MsgBoxStyle.Critical)
            FrmStart.Cursor = Cursors.Default
            NoOfMaps = NoOfMaps - 1
            Exit Sub
        End Try

        ReDim Preserve Maps(NoOfMaps)

        Maps(NoOfMaps).Name = "MAP" & Format(NoOfMaps, "00")
        Maps(NoOfMaps).Selected = False

        Dim LA, LO As String

        LA = Format(Val(LatDispCenter), "00.000000")
        LO = Format(Val(LonDispCenter), "000.000000")

        myfile = AppPath & "\Tools\Work\ARCG_" & LA & "_" & LO & "_" & Zoom & ".png"
        Try
            If Not (ImgMaps(NoOfMaps) Is Nothing) Then
                ImgMaps(NoOfMaps).Save(myfile)
            End If
        Catch ex As Exception
            MsgBox("There was a problem saving the image!", MsgBoxStyle.Exclamation)
        End Try

        Maps(NoOfMaps).BMPSu = myfile
        Maps(NoOfMaps).BMPSp = myfile
        Maps(NoOfMaps).BMPFa = myfile
        Maps(NoOfMaps).BMPWi = myfile
        Maps(NoOfMaps).BMPHw = myfile
        Maps(NoOfMaps).BMPLm = myfile

        Maps(NoOfMaps).COLS = msize
        Maps(NoOfMaps).ROWS = msize

        Maps(NoOfMaps).NLAT = LN
        Maps(NoOfMaps).SLAT = LS
        Maps(NoOfMaps).ELON = LE
        Maps(NoOfMaps).WLON = LW

        FrmStart.SummerMapMenuItem.Checked = True
        FrmStart.ViewMapsMenuItem.Enabled = True

        MapVIEW = True
        ViewON = True
        SetDispCenter(0, 0)
        RebuildDisplay()
        Dirty = True
        FrmStart.Cursor = Cursors.Default
        FrmStart.StatusZoom.Text = "Zoom = " & CStr(Zoom)

    End Sub

    Friend Sub AddGoogleMap()

        Dim myfile As String

        If My.Computer.Network.IsAvailable = False Then
            MsgBox("There is no Internet connection!", MsgBoxStyle.Information)
            Exit Sub
        End If

        If GoogleMapsAPI = "" Then
            myfile = "You need to have a valid and active Google Static Maps API Key! Once you" & vbCrLf
            myfile = myfile & "get your_google_api_key, open SBuilderX.ini file and edit the following line" & vbCrLf
            myfile = myfile & "GoogleMapsAPI=your_google_api_key!" & vbCrLf & vbCrLf
            myfile = myfile & "Do you want to learn how to get a Google API key?"
            Dim A As MsgBoxResult = MsgBox(myfile, MsgBoxStyle.YesNo, "Google Maps API is missing")
            If A = MsgBoxResult.Yes Then
                myfile = "https://developers.google.com/maps/documentation/static-maps/get-api-key"
                Process.Start(myfile)
            End If
            Exit Sub
        End If

        If frmGoogleMap.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        FrmStart.Cursor = Cursors.WaitCursor

        Dim index As Integer
        NoOfMaps = NoOfMaps + 1
        ReDim Preserve ImgMaps(NoOfMaps)

        Dim makeurl As String
        makeurl = "https://maps.googleapis.com/maps/api/staticmap?center="
        makeurl = makeurl & Trim(Str(LatDispCenter)) & "," & Trim(Str(LonDispCenter))

        Dim msize As Integer = 640

        makeurl = makeurl & "&size=" & msize & "x" & msize & "&zoom=" & (Zoom) & "&scale=2"
        makeurl = makeurl & "&format=png&maptype=" & GoogleMapsType
        makeurl = makeurl & "&key=" & GoogleMapsAPI
        Debug.Print(makeurl)

        Try
            Dim req As Net.HttpWebRequest = DirectCast(Net.HttpWebRequest.Create(makeurl), Net.HttpWebRequest)
            Dim res As Net.HttpWebResponse = DirectCast(req.GetResponse, Net.HttpWebResponse)
            index = res.ContentType.IndexOf("image")
            ImgMaps(NoOfMaps) = Image.FromStream(res.GetResponseStream)
            res.Close()
        Catch ex As Exception
            MsgBox("Could not get an image at Zoom=" & Zoom & "!", MsgBoxStyle.Critical)
            FrmStart.Cursor = Cursors.Default
            NoOfMaps = NoOfMaps - 1
            Exit Sub
        End Try

        ReDim Preserve Maps(NoOfMaps)

        Maps(NoOfMaps).Name = "MAP" & Format(NoOfMaps, "00")
        Maps(NoOfMaps).Selected = False

        Dim LA, LO As String

        LA = Format(Val(LatDispCenter), "00.000000")
        LO = Format(Val(LonDispCenter), "000.000000")

        myfile = AppPath & "\Tools\Work\GMAP_" & LA & "_" & LO & "_" & Zoom & ".png"
        Try
            If Not (ImgMaps(NoOfMaps) Is Nothing) Then
                ImgMaps(NoOfMaps).Save(myfile)
            End If
        Catch ex As Exception
            MsgBox("There was a problem saving the image!", MsgBoxStyle.Exclamation)
        End Try

        Maps(NoOfMaps).BMPSu = myfile
        Maps(NoOfMaps).BMPSp = myfile
        Maps(NoOfMaps).BMPFa = myfile
        Maps(NoOfMaps).BMPWi = myfile
        Maps(NoOfMaps).BMPHw = myfile
        Maps(NoOfMaps).BMPLm = myfile

        Maps(NoOfMaps).COLS = msize * 2
        Maps(NoOfMaps).ROWS = msize * 2

        Dim CLat, CLon As Integer
        CLon = XMPixFromLon(LonDispCenter, Zoom)
        CLat = YMPixFromLat(LatDispCenter, Zoom)

        Dim LN, LS, LW, LE As Double

        LN = LatFromYMPix(CLat + msize, Zoom)
        LS = LatFromYMPix(CLat - msize, Zoom)
        LW = LonFromXMPix(CLon - msize, Zoom)
        LE = LonFromXMPix(CLon + msize, Zoom)

        If Zoom < 8 Then          ' I do not remember how it works - October 2017
            FileMercator2LatLon(LN, LS, myfile, myfile)
            ImgMaps(NoOfMaps) = Image.FromFile(myfile)
        End If

        Maps(NoOfMaps).NLAT = LN
        Maps(NoOfMaps).SLAT = LS
        Maps(NoOfMaps).ELON = LE
        Maps(NoOfMaps).WLON = LW

        FrmStart.SummerMapMenuItem.Checked = True
        FrmStart.ViewMapsMenuItem.Enabled = True

        MapVIEW = True
        ViewON = True
        SetDispCenter(0, 0)
        RebuildDisplay()
        Dirty = True
        FrmStart.Cursor = Cursors.Default
        FrmStart.StatusZoom.Text = "Zoom = " & CStr(Zoom)

    End Sub

    Private Sub FileMercator2LatLon(ByVal North As Double, ByVal South As Double, ByVal inputfile As String, ByVal outfile As String)

        Dim N, K As Integer
        Dim lat, dlat As Double
        Dim merc, dmerc As Double
        Dim north_m, south_m As Double

        Dim img As Image = Image.FromFile(inputfile)
        Dim imgformat As ImageFormat = img.RawFormat
        Dim rows As Integer = img.Height
        Dim cols As Integer = img.Width

        Dim bmp As ImageFormat = ImageFormat.Bmp
        Dim ms1 As New MemoryStream
        img.Save(ms1, bmp)

        Dim inp() As Byte = ms1.GetBuffer
        ms1.Close()
        img.Dispose()

        Dim out() As Byte = inp.Clone

        Dim Base, LineWidth, pointN, pointK As Integer
        Dim BytesPerPixel As Integer

        Base = BitConverter.ToInt32(inp, 10)
        BytesPerPixel = CInt(BitConverter.ToInt16(inp, 28) / 8)
        LineWidth = cols * BytesPerPixel
        ' make LineWidth a multiple of 4
        N = Int((LineWidth - 1) / 4)
        LineWidth = (N + 1) * 4

        dlat = (North - South) / (rows - 1)
        north_m = YMercFromLat(North)
        south_m = YMercFromLat(South)
        dmerc = (north_m - south_m) / (rows - 1)
        lat = South + dlat

        For N = 1 To rows - 2
            merc = YMercFromLat(lat)
            merc = merc - south_m
            merc = merc / dmerc
            K = CInt(merc)   ' round
            If N <> K Then
                pointN = Base + (rows - N - 1) * LineWidth
                pointK = Base + (rows - K - 1) * LineWidth
                Array.Copy(inp, pointK, out, pointN, LineWidth)
            End If
            lat = lat + dlat
        Next

        Dim ms2 As New MemoryStream(out)
        img = Image.FromStream(ms2)
        img.Save(outfile, imgformat)
        ms2.Close()
        img.Dispose()

    End Sub

    Private Function YMercFromLat(ByVal lat As Double) As Double

        lat = lat * pi_360
        lat = lat + pi_4
        lat = Math.Tan(lat)
        YMercFromLat = Math.Log(lat)

    End Function

    Private Function XMercFromLon(ByVal lon As Double) As Double

        XMercFromLon = PI + lon * pi_180

    End Function

    Private Function XMPixFromLon(ByVal lon As Double, ByVal Z As Integer) As Integer

        XMPixFromLon = lon * x256_180 * (2 ^ Z)

    End Function

    Private Function YMPixFromLat(ByVal lat As Double, ByVal Z As Integer) As Integer

        lat = lat * pi_360  ' lat=lat/2  <<< equivalent
        lat = lat + pi_4
        lat = Math.Tan(lat)
        lat = Math.Log(lat)
        YMPixFromLat = lat * x256_pi * (2 ^ Z)

    End Function

    Private Function LonFromXMPix(ByVal X As Long, ByVal Z As Integer) As Double

        LonFromXMPix = X / (x256_180 * (2 ^ Z))

    End Function

    Private Function LatFromYMPix(ByVal Y As Long, ByVal Z As Integer) As Double

        Dim X As Double

        X = Y / (x256_pi * (2 ^ Z))
        X = Math.Exp(X)
        X = 2 * Math.Atan(X)
        LatFromYMPix = (X - pi_2) / pi_180

    End Function

    Private Function ReadIniDouble(ByVal File As String, ByVal KEY As String, ByVal Value As String) As Double

        On Error GoTo erro
        'ReadIniDouble = CDbl(ReadIniValue(File, KEY, Value))
        ReadIniDouble = Val(ReadIniValue(File, KEY, Value))
        Exit Function
erro:
        ReadIniDouble = 0

    End Function


    Friend Sub SetBitmapSeason()

        Dim R As Integer
        Dim N As Integer
        Dim A As String


        frmStart.Cursor = Cursors.WaitCursor

        frmStart.SummerMapMenuItem.Checked = False
        frmStart.WinterMapMenuItem.Checked = False
        frmStart.HardWinterMapMenuItem.Checked = False
        frmStart.SpringMapMenuItem.Checked = False
        frmStart.FallMapMenuItem.Checked = False
        frmStart.NightMapMenuItem.Checked = False

        If NoOfMaps = 0 Then Season = ""

        If Season = "" Then
            frmStart.ViewMapsMenuItem.Enabled = False
            frmStart.SetMouseIcon()
            Exit Sub
        Else
            frmStart.ViewMapsMenuItem.Enabled = True
        End If

        On Error GoTo error1

        Select Case Season
            Case Is = "Summer"
                frmStart.SummerMapMenuItem.Checked = True
                For N = 1 To NoOfMaps
                    A = Maps(N).BMPSu
                    ImgMaps(N) = Image.FromFile(A)
                Next N
            Case Is = "Winter"
                frmStart.WinterMapMenuItem.Checked = True
                For N = 1 To NoOfMaps
                    A = Maps(N).BMPWi
                    ImgMaps(N) = Image.FromFile(A)
                Next N
            Case Is = "HardWinter"
                frmStart.HardWinterMapMenuItem.Checked = True
                For N = 1 To NoOfMaps
                    A = Maps(N).BMPHw
                    ImgMaps(N) = Image.FromFile(A)
                Next N
            Case Is = "Spring"
                frmStart.SpringMapMenuItem.Checked = True
                For N = 1 To NoOfMaps
                    A = Maps(N).BMPSp
                    ImgMaps(N) = Image.FromFile(A)
                Next N
            Case Is = "Fall"
                frmStart.FallMapMenuItem.Checked = True
                For N = 1 To NoOfMaps
                    A = Maps(N).BMPFa
                    ImgMaps(N) = Image.FromFile(A)
                Next N
            Case Is = "Night"
                frmStart.NightMapMenuItem.Checked = True
                For N = 1 To NoOfMaps
                    A = Maps(N).BMPLm
                    ImgMaps(N) = Image.FromFile(A)
                Next N

        End Select

        frmStart.SetMouseIcon()
        Exit Sub

error1:
        CheckMaps()
        frmStart.SetMouseIcon()

    End Sub

    Friend Sub CheckMaps()

        Dim N As Integer
        Dim A As String
        N = 0

        SkipBackUp = True
        Do
            N = N + 1
            If N > NoOfMaps Then Exit Do
            If Not File.Exists(Maps(N).BMPSu) Then
                A = "The the following map:" & vbCrLf & vbCrLf
                A = A & Maps(N).BMPSu & vbCrLf & vbCrLf
                A = A & "has a problem and will be deleted!"
                MsgBox(A, MsgBoxStyle.Exclamation)
                DeleteMap(N)
                N = N - 1
            Else
                If Not File.Exists(Maps(N).BMPSp) Then Maps(N).BMPSp = Maps(N).BMPSu
                If Not File.Exists(Maps(N).BMPFa) Then Maps(N).BMPFa = Maps(N).BMPSu
                If Not File.Exists(Maps(N).BMPWi) Then Maps(N).BMPWi = Maps(N).BMPSu
                If Not File.Exists(Maps(N).BMPHw) Then Maps(N).BMPHw = Maps(N).BMPSu
                If Not File.Exists(Maps(N).BMPLm) Then Maps(N).BMPLm = Maps(N).BMPSu
            End If
        Loop

        SkipBackUp = False

    End Sub

    Friend Sub DisplayMaps(ByVal g As Graphics)

        Try

            Dim N As Integer
            Dim MapPixelsPerDegree As Double

            Dim screen As New Rectangle()
            Dim source As New RectangleF()
            Dim units As GraphicsUnit = GraphicsUnit.Pixel

            Dim mypen As New System.Drawing.Pen(Color.Black)
            Dim mybrush As New System.Drawing.SolidBrush(UnselectedPointColor)

            For N = 1 To NoOfMaps

                If Maps(N).NLAT < LatDispSouth Then GoTo JumpHere
                If Maps(N).SLAT > LatDispNorth Then GoTo JumpHere
                If Maps(N).WLON > LonDispEast Then GoTo JumpHere
                If Maps(N).ELON < LonDispWest Then GoTo JumpHere

                If Maps(N).WLON > LonDispWest Then
                    If Maps(N).ELON > LonDispEast Then
                        MapPixelsPerDegree = Maps(N).COLS / (Maps(N).ELON - Maps(N).WLON)
                        source.X = 0
                        source.Width = CSng((LonDispEast - Maps(N).WLON) * MapPixelsPerDegree)
                        screen.X = CInt((Maps(N).WLON - LonDispWest) * PixelsPerLonDeg)
                        screen.Width = DisplayWidth - screen.X
                    Else
                        source.X = 0
                        source.Width = Maps(N).COLS
                        screen.X = CInt((Maps(N).WLON - LonDispWest) * PixelsPerLonDeg)
                        screen.Width = CInt((Maps(N).ELON - Maps(N).WLON) * PixelsPerLonDeg)
                    End If
                Else
                    If Maps(N).ELON > LonDispEast Then
                        MapPixelsPerDegree = Maps(N).COLS / (Maps(N).ELON - Maps(N).WLON)
                        source.X = CSng((LonDispWest - Maps(N).WLON) * MapPixelsPerDegree)
                        source.Width = CSng((LonDispEast - LonDispWest) * MapPixelsPerDegree)
                        screen.X = 0
                        screen.Width = DisplayWidth
                    Else
                        MapPixelsPerDegree = Maps(N).COLS / (Maps(N).ELON - Maps(N).WLON)
                        source.X = CSng((LonDispWest - Maps(N).WLON) * MapPixelsPerDegree)
                        source.Width = CSng((Maps(N).ELON - LonDispWest) * MapPixelsPerDegree)
                        screen.X = 0
                        screen.Width = CInt((Maps(N).ELON - LonDispWest) * PixelsPerLonDeg)
                    End If
                End If


                If Maps(N).SLAT > LatDispSouth Then
                    If Maps(N).NLAT > LatDispNorth Then
                        MapPixelsPerDegree = Maps(N).ROWS / (Maps(N).NLAT - Maps(N).SLAT)
                        source.Y = CSng((Maps(N).NLAT - LatDispNorth) * MapPixelsPerDegree)
                        source.Height = CSng((LatDispNorth - Maps(N).SLAT) * MapPixelsPerDegree)
                        screen.Y = 0
                        screen.Height = CInt((LatDispNorth - Maps(N).SLAT) * PixelsPerLatDeg)
                    Else
                        source.Y = 0
                        source.Height = Maps(N).ROWS
                        screen.Y = CInt((LatDispNorth - Maps(N).NLAT) * PixelsPerLatDeg)
                        screen.Height = CInt((Maps(N).NLAT - Maps(N).SLAT) * PixelsPerLatDeg)
                    End If
                Else
                    If Maps(N).NLAT > LatDispNorth Then
                        MapPixelsPerDegree = Maps(N).ROWS / (Maps(N).NLAT - Maps(N).SLAT)
                        source.Y = CInt((Maps(N).NLAT - LatDispNorth) * MapPixelsPerDegree)
                        source.Height = CInt((LatDispNorth - LatDispSouth) * MapPixelsPerDegree)
                        screen.Y = 0
                        screen.Height = DisplayHeight
                    Else
                        MapPixelsPerDegree = Maps(N).ROWS / (Maps(N).NLAT - Maps(N).SLAT)
                        source.Y = 0
                        source.Height = CInt((Maps(N).NLAT - LatDispSouth) * MapPixelsPerDegree)
                        screen.Y = CInt((LatDispNorth - Maps(N).NLAT) * PixelsPerLatDeg)
                        screen.Height = CInt((Maps(N).NLAT - LatDispSouth) * PixelsPerLatDeg)
                    End If
                End If

                If ShowSimpleMaps Then
                    g.FillRectangle(mybrush, screen.X, screen.Y, screen.Width, screen.Height)
                Else
                    g.DrawImage(ImgMaps(N), screen, source, units)
                End If

                If Maps(N).Selected Then
                    mypen.Width = 2
                    mypen.Color = Color.Green
                    g.DrawRectangle(mypen, New Rectangle(screen.X, screen.Y, screen.Width, screen.Height))
                    g.FillRectangle(mybrush, screen.X - 2, screen.Y - 2, 4, 4)
                    g.FillRectangle(mybrush, screen.X + screen.Width - 2, screen.Y - 2, 4, 4)
                    g.FillRectangle(mybrush, screen.X - 2, screen.Y + screen.Height - 2, 4, 4)
                    g.FillRectangle(mybrush, screen.X + screen.Width - 2, screen.Y + screen.Height - 2, 4, 4)

                ElseIf BorderON Then
                    mypen.Color = Color.Black
                    mypen.Width = 1
                    g.DrawRectangle(mypen, New Rectangle(screen.X, screen.Y, screen.Width, screen.Height))
                End If

JumpHere:
            Next

            mypen.Dispose()
            mybrush.Dispose()

        Catch ex As Exception
            CheckMaps()
            'MsgBox("Can not display maps! Map View was turned OFF!", MsgBoxStyle.Exclamation)
            'MapVIEW = False
            'frmStart.ViewAllMapsMenuItem.Checked = False
            MsgBox("Can not display maps! Simple Map View turned ON!", MsgBoxStyle.Exclamation)
            ShowSimpleMaps = True
        End Try


    End Sub

    Friend Sub DeleteMap(ByVal N As Integer)

        Dim K As Integer

        If Not SkipBackUp Then BackUp()
        If Maps(N).Selected Then NoOfMapsSelected = NoOfMapsSelected - 1
        If N < NoOfMaps Then
            For K = N To NoOfMaps - 1
                Maps(K).Name = Maps(K + 1).Name
                Maps(K).Selected = Maps(K + 1).Selected
                Maps(K).BMPSu = Maps(K + 1).BMPSu
                Maps(K).BMPSp = Maps(K + 1).BMPSp
                Maps(K).BMPFa = Maps(K + 1).BMPFa
                Maps(K).BMPWi = Maps(K + 1).BMPWi
                Maps(K).BMPHw = Maps(K + 1).BMPHw
                Maps(K).BMPLm = Maps(K + 1).BMPLm

                Maps(K).COLS = Maps(K + 1).COLS
                Maps(K).ROWS = Maps(K + 1).ROWS
                Maps(K).NLAT = Maps(K + 1).NLAT
                Maps(K).SLAT = Maps(K + 1).SLAT
                Maps(K).ELON = Maps(K + 1).ELON
                Maps(K).WLON = Maps(K + 1).WLON

                ImgMaps(K) = ImgMaps(K + 1)

            Next K
        End If

        If Not ImgMaps(NoOfMaps) Is Nothing Then ImgMaps(NoOfMaps).Dispose()
        NoOfMaps = NoOfMaps - 1
        SetBitmapSeason()

        Dirty = True

    End Sub

    Friend Function IsMapSelected(ByVal X As Double, ByVal Y As Double) As Boolean

        Dim N As Integer
        Dim retval As Boolean

        IsMapSelected = False
        If Not MapVIEW Then Exit Function

        For N = 1 To NoOfMaps
            retval = IsPointInMap(N, X, Y)
            If retval Then
                If Maps(N).Selected = False Then NoOfMapsSelected = NoOfMapsSelected + 1
                Maps(N).Selected = True
                SomeSelected = True
                IsMapSelected = True
                Exit Function
            End If
        Next N

    End Function


    Friend Sub MoveSelectedMaps(ByVal X As Double, ByVal Y As Double)

        Dim N As Integer

        For N = 1 To NoOfMaps
            If Maps(N).Selected Then
                Maps(N).NLAT = Maps(N).NLAT - Y
                Maps(N).SLAT = Maps(N).SLAT - Y
                Maps(N).WLON = Maps(N).WLON + X
                Maps(N).ELON = Maps(N).ELON + X
            End If
        Next N


    End Sub

    Friend Function IsPointInMap(ByVal N As Integer, ByVal X As Double, ByVal Y As Double) As Boolean

        Dim X2, X1, Y1, Y2 As Double

        X1 = Maps(N).WLON * PixelsPerLonDeg
        X2 = Maps(N).ELON * PixelsPerLonDeg

        Y1 = Maps(N).NLAT * PixelsPerLatDeg
        Y2 = Maps(N).SLAT * PixelsPerLatDeg

        IsPointInMap = False

        If X > X2 + 3 Then Exit Function
        If X < X1 - 3 Then Exit Function
        If Y < Y2 - 3 Then Exit Function
        If Y > Y1 + 3 Then Exit Function

        If X < X2 - 3 Then
            If X > X1 + 3 Then
                If Y > Y2 + 3 Then
                    If Y < Y1 - 3 Then Exit Function
                End If
            End If
        End If

        IsPointInMap = True

    End Function

    Friend Function IsPointInsideMap(ByVal N As Integer, ByVal X As Double, ByVal Y As Double) As Boolean

        Dim X2, X1, Y1, Y2 As Double

        X1 = Maps(N).WLON * PixelsPerLonDeg
        X2 = Maps(N).ELON * PixelsPerLonDeg

        Y1 = Maps(N).NLAT * PixelsPerLatDeg
        Y2 = Maps(N).SLAT * PixelsPerLatDeg

        IsPointInsideMap = False

        If X > X2 Then Exit Function
        If X < X1 Then Exit Function
        If Y < Y2 Then Exit Function
        If Y > Y1 Then Exit Function

        IsPointInsideMap = True

    End Function

    Friend Sub SelectMapsInBox(ByVal X0 As Double, ByVal Y0 As Double, ByVal X1 As Double, ByVal Y1 As Double)

        Dim N As Integer

        If Not MapVIEW Then Exit Sub

        For N = 1 To NoOfMaps
            If Maps(N).ELON < X1 Then
                If Maps(N).WLON > X0 Then
                    If Maps(N).SLAT > Y1 Then
                        If Maps(N).NLAT < Y0 Then
                            If Not Maps(N).Selected Then NoOfMapsSelected = NoOfMapsSelected + 1
                            SomeSelected = True
                            Maps(N).Selected = True
                        End If
                    End If
                End If
            End If
        Next N

    End Sub
    Friend Sub SelectAllMaps(ByRef Flag As Boolean)

        Dim N As Integer

        If Not MapVIEW Then Exit Sub

        If Flag Then
            frmStart.SelectAllMapsMenuItem.Checked = True
        Else
            frmStart.SelectAllMapsMenuItem.Checked = False
        End If

        For N = 1 To NoOfMaps
            If Flag Then
                If Not Maps(N).Selected Then NoOfMapsSelected = NoOfMapsSelected + 1
                SomeSelected = True
            Else
                If Maps(N).Selected Then NoOfMapsSelected = NoOfMapsSelected - 1
            End If
            Maps(N).Selected = Flag
        Next N

    End Sub

    Private Function GetImageParameters(ByVal myFile As String, ByRef Cols As Integer, ByRef Rows As Integer, _
                                        ByRef North As Double, ByRef South As Double, ByRef West As Double, _
                                        ByRef East As Double, ByRef geoTiff As Boolean) As Boolean

        On Error GoTo erro1
        GetImageParameters = False

        Dim BB(1) As Byte
        Dim IsTiff As Boolean = False
        Dim Swap As Boolean = False



        'FileOpen(3, myFile, OpenMode.Binary)
        'FileGet(3, BB)
        'FileClose(3)
        ' adding a second map gave a crash so I replace with this

        BB = File.ReadAllBytes(myFile)

        If BB(0) = 77 And BB(1) = 77 Then
            Swap = True
        End If

        Dim Flag As Boolean
        Dim tiff As ImageFormat = ImageFormat.Tiff
        Dim jpeg As ImageFormat = ImageFormat.Jpeg
        Dim bmp As ImageFormat = ImageFormat.Bmp
        Dim png As ImageFormat = ImageFormat.Png
        Dim gif As ImageFormat = ImageFormat.Gif

        ImgMaps(NoOfMaps) = New Bitmap(myFile)
        On Error GoTo erro2

        Flag = True
        If ImgMaps(NoOfMaps).RawFormat.Equals(tiff) Then
            Flag = False
            IsTiff = True
        End If

        If ImgMaps(NoOfMaps).RawFormat.Equals(jpeg) Then Flag = False
        If ImgMaps(NoOfMaps).RawFormat.Equals(bmp) Then Flag = False
        If ImgMaps(NoOfMaps).RawFormat.Equals(png) Then Flag = False
        If ImgMaps(NoOfMaps).RawFormat.Equals(gif) Then Flag = False

        If Flag Then
            If Not ImgMaps(NoOfMaps) Is Nothing Then ImgMaps(NoOfMaps).Dispose()
            Exit Function
        End If

        Rows = ImgMaps(NoOfMaps).Height
        Cols = ImgMaps(NoOfMaps).Width

        GetImageParameters = True
        geoTiff = False
        If Not IsTiff Then Exit Function
        On Error GoTo erro1

        'Get the GeoTiff PropertyItems property from image.
        Dim N As Integer
        Dim bytes() As Byte
        Dim piScale As PropertyItem = ImgMaps(NoOfMaps).GetPropertyItem(33550)
        Dim piTiePoint As PropertyItem = ImgMaps(NoOfMaps).GetPropertyItem(33922)
        Dim piGeoDir As PropertyItem = ImgMaps(NoOfMaps).GetPropertyItem(34735)

        ' check the GeoKey for valid geographic projection and WGS84 datum
        N = piGeoDir.Value.Length
        ReDim bytes(N)
        bytes = piGeoDir.Value

        ' MsgBox(N)

        If Not IsGCS_WGS84(bytes) Then

            Exit Function
        End If

        Dim X As Double     ' tie point pixel column
        Dim Y As Double     ' tie point pixel row
        Dim LX As Double    ' tie point longitude
        Dim LY As Double    ' tie point latitude
        N = piTiePoint.Value.Length
        ReDim bytes(N)
        bytes = piTiePoint.Value
        If Swap Then ByteReversal(bytes)
        X = BitConverter.ToDouble(bytes, 0)
        Y = BitConverter.ToDouble(bytes, 8)
        LX = BitConverter.ToDouble(bytes, 24)
        LY = BitConverter.ToDouble(bytes, 32)

        Dim SX As Double    ' ScaleX
        Dim SY As Double    ' scaleY
        Dim SX2, SY2 As Double
        N = piScale.Value.Length
        ReDim bytes(N)
        bytes = piScale.Value
        If Swap Then ByteReversal(bytes)
        SX = BitConverter.ToDouble(bytes, 0)
        SY = BitConverter.ToDouble(bytes, 8)
        SX2 = SX / 2
        SY2 = SY / 2

        West = LX - X * SX - SX2
        East = LX + (Cols - X - 1) * SX + SX2
        North = LY + Y * SY + SY2
        South = LY - (Rows - Y - 1) * SY - SY2

        If West < -180 Or West > 180 Then Exit Function
        If East < -180 Or East > 180 Then Exit Function
        If West > East Then Exit Function

        If North > 90 Or North < -90 Then Exit Function
        If South > 90 Or South < -90 Then Exit Function
        If North < South Then Exit Function

        geoTiff = True
erro1:
        Exit Function
erro2:

        If Not ImgMaps(NoOfMaps) Is Nothing Then ImgMaps(NoOfMaps).Dispose()


    End Function

    Private Function IsGCS_WGS84(ByVal arr() As Byte) As Boolean

        IsGCS_WGS84 = False

        Dim N, J As Long
        Dim F1, F2 As Boolean
        Dim key, value As Short

        F1 = False
        F2 = False

        N = arr.Length
        N = 8 * Int(N / 8) - 1

        For J = 8 To N Step 8
            key = BitConverter.ToUInt16(arr, J)
            If key = 1024 Then
                value = BitConverter.ToUInt16(arr, J + 6)
                If value = 2 Then F1 = True
            End If
            If key = 2048 Then
                value = BitConverter.ToUInt16(arr, J + 6)
                If value = 4326 Then F2 = True
            End If
        Next

        If F1 And F2 Then IsGCS_WGS84 = True

    End Function

    Private Sub ByteReversal(ByRef arr() As Byte)

        Dim N, J As Integer
        Dim B0 As Byte

        N = arr.Length
        N = 8 * Int(N / 8) - 1
        For J = 0 To N Step 8
            B0 = arr(J)
            arr(J) = arr(J + 7)
            arr(J + 7) = B0
            B0 = arr(J + 1)
            arr(J + 1) = arr(J + 6)
            arr(J + 6) = B0
            B0 = arr(J + 2)
            arr(J + 2) = arr(J + 5)
            arr(J + 5) = B0
            B0 = arr(J + 3)
            arr(J + 3) = arr(J + 4)
            arr(J + 4) = B0
        Next

    End Sub


    Friend Function MakeGeoKeyDirTag() As Byte()

        ' 1     1    0    4
        '1024   0    1    2
        '1025   0    1    1
        '2048   0    1    4326   ' Geographic WGS84
        '2054   0    1    9102   ' Angular_Degree

        ReDim MakeGeoKeyDirTag(39)
        Dim X0, X1, X2, X4 As Byte()
        Dim X1024, X1025, X2048, X2054, X4326, X9102 As Byte()

        X0 = BitConverter.GetBytes(Convert.ToUInt16(0))
        X1 = BitConverter.GetBytes(Convert.ToUInt16(1))
        X2 = BitConverter.GetBytes(Convert.ToUInt16(2))
        X4 = BitConverter.GetBytes(Convert.ToUInt16(4))
        X1024 = BitConverter.GetBytes(Convert.ToUInt16(1024))
        X1025 = BitConverter.GetBytes(Convert.ToUInt16(1025))
        X2048 = BitConverter.GetBytes(Convert.ToUInt16(2048))
        X4326 = BitConverter.GetBytes(Convert.ToUInt16(4326))
        X2054 = BitConverter.GetBytes(Convert.ToUInt16(2054))
        X9102 = BitConverter.GetBytes(Convert.ToUInt16(9102))

        ' 1     1    0    4
        X1.CopyTo(MakeGeoKeyDirTag, 0)
        X1.CopyTo(MakeGeoKeyDirTag, 2)
        X0.CopyTo(MakeGeoKeyDirTag, 4)
        X4.CopyTo(MakeGeoKeyDirTag, 6)
        '1024   0    1    2
        X1024.CopyTo(MakeGeoKeyDirTag, 8)
        X0.CopyTo(MakeGeoKeyDirTag, 10)
        X1.CopyTo(MakeGeoKeyDirTag, 12)
        X2.CopyTo(MakeGeoKeyDirTag, 14)
        '1025   0    1    1
        X1025.CopyTo(MakeGeoKeyDirTag, 16)
        X0.CopyTo(MakeGeoKeyDirTag, 18)
        X1.CopyTo(MakeGeoKeyDirTag, 20)
        X1.CopyTo(MakeGeoKeyDirTag, 22)
        '2048   0    1    4326
        X2048.CopyTo(MakeGeoKeyDirTag, 24)
        X0.CopyTo(MakeGeoKeyDirTag, 26)
        X1.CopyTo(MakeGeoKeyDirTag, 28)
        X4326.CopyTo(MakeGeoKeyDirTag, 30)
        '2054   0    1    9102   ' Angular_Degree
        X2054.CopyTo(MakeGeoKeyDirTag, 32)
        X0.CopyTo(MakeGeoKeyDirTag, 34)
        X1.CopyTo(MakeGeoKeyDirTag, 36)
        X9102.CopyTo(MakeGeoKeyDirTag, 38)

    End Function


    Friend Function MakeTiePointTag(ByVal LX As Double, ByVal LY As Double) As Byte()

        ReDim MakeTiePointTag(47)

        Dim X0, XLX, XLY As Byte()

        X0 = BitConverter.GetBytes(Convert.ToDouble(0))
        XLX = BitConverter.GetBytes(Convert.ToDouble(LX))
        XLY = BitConverter.GetBytes(Convert.ToDouble(LY))

        ' X Y Z LX LY LZ
        ' 0 0 0 LX LY 0
        X0.CopyTo(MakeTiePointTag, 0)
        X0.CopyTo(MakeTiePointTag, 8)
        X0.CopyTo(MakeTiePointTag, 16)
        XLX.CopyTo(MakeTiePointTag, 24)
        XLY.CopyTo(MakeTiePointTag, 32)
        X0.CopyTo(MakeTiePointTag, 40)

    End Function

    Friend Function MakePixelScaleTag(ByVal SX As Double, ByVal SY As Double) As Byte()

        ReDim MakePixelScaleTag(23)

        Dim X0, XSX, XSY As Byte()

        X0 = BitConverter.GetBytes(Convert.ToDouble(0))
        XSX = BitConverter.GetBytes(Convert.ToDouble(SX))
        XSY = BitConverter.GetBytes(Convert.ToDouble(SY))

        ' SX SY SZ
        ' SX SY 0
        XSX.CopyTo(MakePixelScaleTag, 0)
        XSY.CopyTo(MakePixelScaleTag, 8)
        X0.CopyTo(MakePixelScaleTag, 16)

    End Function

End Module
