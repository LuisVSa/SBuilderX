

Imports TileServer
Imports System.Reflection
Imports System.io
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms

Module moduleTILES

    Friend SummerVariations As String
    Friend SpringVariations As String
    Friend FallVariations As String
    Friend WinterVariations As String
    Friend HardWinterVariations As String
    Friend CompressionQuality As Integer

    Friend ReprojectMercatorTiles As Boolean
    Friend TilesToCome As Integer = 0
    Friend TileServer As TileServer.IServer
    Friend ServerTypes() As Type
    Friend NoOfServerTypes As Integer
    Friend MaximumZoom As Integer

    Friend TileVIEW As Boolean
    Friend ActiveTileFolder As String
    Friend TileFolder As String

    Friend TimeToUpdate As Boolean = False ' when to make the background goes true when last tile comes


    Friend Structure MapTile
        Dim COLS As Integer
        Dim ROWS As Integer
        Dim NLAT As Double
        Dim SLAT As Double
        Dim WLON As Double
        Dim ELON As Double
    End Structure

    Friend MapBackground As MapTile  ' final
    Friend MapBackground0 As MapTile ' temp
    Friend TilesDownloading As New ArrayList
    Friend TilesFailed As New ArrayList

    Friend Structure TileHandlerState
        Dim handler As Object
        Dim tile As String
        Dim dir As String
    End Structure

    Private Const pi_360 As Double = PI / 360.0
    Private Const pi_180 As Double = PI / 180.0
    Private Const pi_4 As Double = PI / 4.0
    Private Const pi_2 As Double = PI / 2.0

    Friend GlobeOrTiles As Integer = 2

    Friend ImageBackground As Bitmap = New Bitmap(2, 2)    ' final
    Friend ImageBackground0 As Bitmap = New Bitmap(2, 2)   ' temporary

    Friend ImageGlobe As Bitmap = CType(Image.FromFile(AppPath & "\Tiles\globe.jpg"), Bitmap)
    Friend najpg As Bitmap = CType(Image.FromFile(AppPath & "\Tiles\na.jpg"), Bitmap)
    ' Friend blankjpg As Bitmap = Image.FromFile(AppPath & "\Tiles\blank.jpg")      ' was like this in October 2017
    Friend blankjpg As Bitmap = CType(Image.FromFile(AppPath & "\Tiles\blank.jpg"), Bitmap)

    Friend Delegate Function DownloadTileHandler(ByVal X As Integer, ByVal Y As Integer, ByVal S As Integer, ByVal file As String) As Boolean
    Friend myDownloadTileCallback As AsyncCallback = AddressOf frmStart.DownloadTileCallback

    Friend Sub SetServerTypes()

        ' this only runs when SB starts

        Dim myAssembly As Assembly
        Dim myFolder As String = My.Application.Info.DirectoryPath

        Dim myFiles As String()
        Dim myTypes As Type()
        Dim myType As Type
        ReDim ServerTypes(30)

        Dim K As Integer

        myFiles = Directory.GetFiles(myFolder & "\Tiles", "*.dll")

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(AppPath & "\Tiles", FileIO.SearchOption.SearchTopLevelOnly, "L*")
            MsgBox(foundFile)
            My.Computer.FileSystem.DeleteFile(foundFile)
        Next

        Dim Dll, DllBase As String

        ' changed the app.config as per Dick and remove the following line
        ' AppDomain.CurrentDomain.AppendPrivatePath("Tiles")

        K = 0
        For Each Dll In myFiles
            DllBase = Path.GetFileNameWithoutExtension(Dll)
            myAssembly = Assembly.Load(DllBase)
            myTypes = myAssembly.GetTypes
            For Each myType In myTypes
                If myType.GetInterface("TileServer.IServer") IsNot Nothing Then
                    If Not myType.IsAbstract Then
                        K = K + 1
                        ServerTypes(K) = myType
                    End If
                End If
            Next
        Next

        NoOfServerTypes = K

        ReDim Preserve ServerTypes(NoOfServerTypes)

        If ActiveTileFolder = "" Then Exit Sub

        Dim Flag As Boolean = True
        For K = 1 To NoOfServerTypes
            If ActiveTileFolder = ServerTypes(K).Name Then
                TileServer = Activator.CreateInstance(ServerTypes(K))
                MaximumZoom = TileServer.MaximumZoom
                Flag = False
                Exit For
            End If
        Next

        If Flag Then
            ActiveTileFolder = ""
            WriteSettings()
        End If


    End Sub

   
    Friend Sub TileHasArrived(ByVal N As Integer)

        If N > 0 Then
            Dim XY As Point
            XY.X = 0
            XY.Y = DisplayHeight - 35
            frmStart.lbTilesRemaining.Location = XY
            frmStart.lbTilesRemaining.Text = "(" & N & " tiles remainning) downloading from " & TileServer.ServerName
            frmStart.lbTilesRemaining.Visible = True
            frmStart.lbTilesRemaining.Refresh()
        Else
            frmStart.lbTilesRemaining.Visible = False
            If TimeToUpdate Then
                frmStart.MakeBackground()
                'ImageBackground = ImageBackground0
                'MapBackground = MapBackground0
                RebuildDisplay()
            End If
        End If

    End Sub

    Friend Function XTilesFromLon(ByVal lon As Double, ByVal Z As Double) As Integer

        Dim dXY As Double
        dXY = PI / (2 ^ Z)
        lon = PI + lon * pi_180
        XTilesFromLon = CInt(Int(lon / dXY))

    End Function
    Friend Function YTilesFromLat(ByVal lat As Double, ByVal Z As Double) As Integer

        Dim dXY As Double
        dXY = PI / (2 ^ Z)
        lat = lat * pi_360  ' lat=lat/2  <<< equivalent
        lat = lat + pi_4
        lat = Math.Tan(lat)
        lat = Math.Log(lat)
        lat = PI - lat
        YTilesFromLat = CInt(Int(lat / dXY))

    End Function

    'Private Function YMercFromLat(ByVal lat As Double) As Double

    '    lat = lat * pi_360
    '    lat = lat + pi_4
    '    lat = Math.Tan(lat)
    '    YMercFromLat = Math.Log(lat)

    'End Function

    'Private Function XMercFromLon(ByVal lon As Double) As Double

    '    XMercFromLon = PI + lon * pi_180

    'End Function


    'Friend Sub ImageMercator2LatLon(ByRef out As Bitmap, ByVal North As Double, ByVal South As Double, ByVal outfile As String)


    '    Dim N, K, X, rows, cols As Integer
    '    Dim lat, dlat As Double
    '    Dim merc, dmerc As Double
    '    Dim myformat As ImageFormat

    '    Dim north_m, south_m As Double

    '    Dim imgst1 As New MemoryStream
    '    Dim imgst2 As New MemoryStream

    '    Dim bmp As ImageFormat = ImageFormat.Bmp

    '    rows = out.Height
    '    cols = out.Width
    '    myformat = out.RawFormat

    '    Dim base, pointN, pointK As Integer


    '    ' make cols multiple of 4
    '    N = Int((cols - 1) / 4)
    '    cols = (N + 1) * 4

    '    Dim bufferline(cols - 1) As Byte

    '    out.Save(imgst1, bmp)
    '    imgst2 = imgst1

    '    imgst1.Position = 10
    '    X = imgst1.Read(bufferline, 0, 4)

    '    base = BitConverter.ToInt32(bufferline, 0)

    '    dlat = (North - South) / (rows - 1)
    '    north_m = YMercFromLat(North)
    '    south_m = YMercFromLat(South)
    '    dmerc = (north_m - south_m) / (rows - 1)

    '    lat = South + dlat
    '    For N = 1 To rows - 2
    '        merc = YMercFromLat(lat)
    '        merc = merc - south_m
    '        merc = merc / dmerc
    '        K = CInt(merc)   ' round
    '        If N <> K Then
    '            '    For K = 0 To cols - 1
    '            '        out.SetPixel(K, N, inp.GetPixel(K, N1))
    '            '    Next
    '            pointN = base + (rows - N - 1) * cols
    '            pointK = base + (rows - K - 1) * cols
    '            imgst1.Position = pointN
    '            imgst2.Position = pointK
    '            X = imgst1.Read(bufferline, 0, cols)
    '            imgst2.Write(bufferline, 0, cols)
    '        End If
    '        lat = lat + dlat
    '    Next

    '    out = Image.FromStream(imgst2)
    '    out.Save(outfile, myformat)
    '    imgst1.Close()
    '    imgst2.Close()

    'End Sub

    'Private Sub FileMercator2LatLon(ByVal North As Double, ByVal South As Double, ByVal inputfile As String, ByVal outfile As String)


    '    Dim N, K As Integer
    '    Dim lat, dlat As Double
    '    Dim merc, dmerc As Double
    '    Dim north_m, south_m As Double

    '    Dim img As Image = Image.FromFile(inputfile)
    '    Dim imgformat As ImageFormat = img.RawFormat
    '    Dim rows As Integer = img.Height
    '    Dim cols As Integer = img.Width

    '    Dim bmp As ImageFormat = ImageFormat.Bmp
    '    Dim ms1 As New MemoryStream
    '    img.Save(ms1, bmp)

    '    Dim inp() As Byte = ms1.GetBuffer
    '    ms1.Close()
    '    img.Dispose()

    '    Dim out() As Byte = inp.Clone

    '    Dim Base, LineWidth, pointN, pointK As Integer
    '    Dim BytesPerPixel As Integer

    '    Base = BitConverter.ToInt32(inp, 10)
    '    BytesPerPixel = CInt(BitConverter.ToInt16(inp, 28) / 8)
    '    LineWidth = cols * BytesPerPixel
    '    ' make LineWidth a multiple of 4
    '    N = Int((LineWidth - 1) / 4)
    '    LineWidth = (N + 1) * 4


    '    dlat = (North - South) / (rows - 1)
    '    north_m = YMercFromLat(North)
    '    south_m = YMercFromLat(South)
    '    dmerc = (north_m - south_m) / (rows - 1)
    '    lat = South + dlat

    '    For N = 1 To rows - 2
    '        merc = YMercFromLat(lat)
    '        merc = merc - south_m
    '        merc = merc / dmerc
    '        K = CInt(merc)   ' round
    '        If N <> K Then
    '            pointN = Base + (rows - N - 1) * LineWidth
    '            pointK = Base + (rows - K - 1) * LineWidth
    '            Array.Copy(inp, pointK, out, pointN, LineWidth)
    '        End If
    '        lat = lat + dlat
    '    Next

    '    Dim ms2 As New MemoryStream(out)
    '    img = Image.FromStream(ms2)
    '    img.Save(outfile, imgformat)
    '    ms2.Close()
    '    img.Dispose()


    'End Sub


    Friend Sub PixelHeightFromY(ByVal Y As Integer, ByRef H() As Integer, ByVal N As Integer, ByVal Z As Integer)

        Dim R As Integer
        Dim NS As Double
        Dim X(N) As Double

        For R = 0 To N
            X(R) = LatFromYMerc(Y + R, Z)
        Next
        ' Debug.Print(vbCrLf)

        NS = (256 * N) / (X(0) - X(N))
        For R = 0 To N - 1
            H(R) = CInt((X(R) - X(R + 1)) * NS)
            ' Debug.Print(H(R))
        Next

    End Sub

    Friend Function LatFromYMerc(ByVal Y As Integer, ByVal Z As Integer) As Double

        Dim dXY, lat As Double
        dXY = PI / (2 ^ Z)
        lat = Y * dXY
        lat = PI - lat
        lat = Math.Exp(lat)
        lat = 2 * Math.Atan(lat)
        LatFromYMerc = (lat - pi_2) / pi_180

    End Function

    Friend Function LonFromXMerc(ByVal X As Integer, ByVal Z As Integer) As Double

        Dim dXY As Double
        dXY = PI / (2 ^ Z)
        LonFromXMerc = (X * dXY - PI) / pi_180

    End Function

    Friend Function TileDirFromXYZ(ByVal X As Integer, ByVal Y As Integer, ByVal Z As Integer) As String

        TileDirFromXYZ = ""
        If Z < 4 Then Exit Function

        Dim N As Integer
        Dim Limit As Integer

        For N = Z To 4 Step -1
            Limit = CInt(2 ^ N)
            If X < Limit Then
                If Y < Limit Then
                    TileDirFromXYZ = TileDirFromXYZ & "\0"
                Else
                    TileDirFromXYZ = TileDirFromXYZ & "\2"
                    Y = Y - Limit
                End If
            Else
                If Y < Limit Then
                    TileDirFromXYZ = TileDirFromXYZ & "\1"
                Else
                    TileDirFromXYZ = TileDirFromXYZ & "\3"
                    Y = Y - Limit
                End If
                X = X - Limit
            End If
        Next

    End Function

    Friend Sub DisplayTiles(ByVal g As Graphics)

        Dim MapPixelsPerDegree As Double

        Dim screen As New Rectangle()
        Dim source As New RectangleF()
        Dim units As GraphicsUnit = GraphicsUnit.Pixel

        If MapBackground.NLAT < LatDispSouth Then Exit Sub
        If MapBackground.SLAT > LatDispNorth Then Exit Sub
        If MapBackground.WLON > LonDispEast Then Exit Sub
        If MapBackground.ELON < LonDispWest Then Exit Sub

        If MapBackground.WLON > LonDispWest Then
            If MapBackground.ELON > LonDispEast Then
                MapPixelsPerDegree = MapBackground.COLS / (MapBackground.ELON - MapBackground.WLON)
                source.X = 0
                source.Width = CSng((LonDispEast - MapBackground.WLON) * MapPixelsPerDegree)
                screen.X = CInt((MapBackground.WLON - LonDispWest) * PixelsPerLonDeg)
                screen.Width = DisplayWidth - screen.X
            Else
                source.X = 0
                source.Width = MapBackground.COLS
                screen.X = CInt((MapBackground.WLON - LonDispWest) * PixelsPerLonDeg)
                screen.Width = CInt((MapBackground.ELON - MapBackground.WLON) * PixelsPerLonDeg)
            End If
        Else
            If MapBackground.ELON > LonDispEast Then
                MapPixelsPerDegree = MapBackground.COLS / (MapBackground.ELON - MapBackground.WLON)
                source.X = CSng((LonDispWest - MapBackground.WLON) * MapPixelsPerDegree)
                source.Width = CSng((LonDispEast - LonDispWest) * MapPixelsPerDegree)
                screen.X = 0
                screen.Width = DisplayWidth
            Else
                MapPixelsPerDegree = MapBackground.COLS / (MapBackground.ELON - MapBackground.WLON)
                source.X = CSng((LonDispWest - MapBackground.WLON) * MapPixelsPerDegree)
                source.Width = CSng((MapBackground.ELON - LonDispWest) * MapPixelsPerDegree)
                screen.X = 0
                screen.Width = CInt((MapBackground.ELON - LonDispWest) * PixelsPerLonDeg)
            End If
        End If

        If MapBackground.SLAT > LatDispSouth Then
            If MapBackground.NLAT > LatDispNorth Then
                MapPixelsPerDegree = MapBackground.ROWS / (MapBackground.NLAT - MapBackground.SLAT)
                source.Y = CSng((MapBackground.NLAT - LatDispNorth) * MapPixelsPerDegree)
                source.Height = CSng((LatDispNorth - MapBackground.SLAT) * MapPixelsPerDegree)
                screen.Y = 0
                screen.Height = CInt((LatDispNorth - MapBackground.SLAT) * PixelsPerLatDeg)
            Else
                source.Y = 0
                source.Height = MapBackground.ROWS
                screen.Y = CInt((LatDispNorth - MapBackground.NLAT) * PixelsPerLatDeg)
                screen.Height = CInt((MapBackground.NLAT - MapBackground.SLAT) * PixelsPerLatDeg)
            End If
        Else
            If MapBackground.NLAT > LatDispNorth Then
                MapPixelsPerDegree = MapBackground.ROWS / (MapBackground.NLAT - MapBackground.SLAT)
                source.Y = CInt((MapBackground.NLAT - LatDispNorth) * MapPixelsPerDegree)
                source.Height = CInt((LatDispNorth - LatDispSouth) * MapPixelsPerDegree)
                screen.Y = 0
                screen.Height = DisplayHeight
            Else
                MapPixelsPerDegree = MapBackground.ROWS / (MapBackground.NLAT - MapBackground.SLAT)
                source.Y = 0
                source.Height = CInt((MapBackground.NLAT - LatDispSouth) * MapPixelsPerDegree)
                screen.Y = CInt((LatDispNorth - MapBackground.NLAT) * PixelsPerLatDeg)
                screen.Height = CInt((MapBackground.NLAT - LatDispSouth) * PixelsPerLatDeg)
            End If
        End If

        If Zoom > GlobeOrTiles Then
            If ImageBackground IsNot Nothing Then g.DrawImage(ImageBackground, screen, source, units)
        Else
            g.DrawImage(ImageGlobe, screen, source, units)
        End If

        'If TilesToCome > 0 Then
        '    Dim XY As Point
        '    XY.X = 0
        '    XY.Y = DisplayHeight - 35
        '    frmStart.lbTilesRemaining.Location = XY
        '    frmStart.lbTilesRemaining.Text = "(" & TilesToCome & " tiles remainning) downloading from " & TileServer.ServerName
        '    frmStart.lbTilesRemaining.Visible = True
        'Else
        '    frmStart.lbTilesRemaining.Visible = False
        'End If

    End Sub

    Friend Sub SaveBackground(ByVal Filename As String)

        Dim GeoTiff As Image
        Dim Stream As New MemoryStream
        ImageBackground.Save(Stream, ImageFormat.Tiff)
        GeoTiff = Image.FromStream(Stream)

        Dim propItem As PropertyItem
        propItem = GeoTiff.GetPropertyItem(256)
        propItem.Len = 40
        propItem.Type = 3
        propItem.Value = MakeGeoKeyDirTag()
        propItem.Id = 34735
        GeoTiff.SetPropertyItem(propItem)

        Dim North As Double = MapBackground.NLAT
        Dim South As Double = MapBackground.SLAT
        Dim East As Double = MapBackground.ELON
        Dim West As Double = MapBackground.WLON
        Dim Cols As Integer = MapBackground.COLS
        Dim Rows As Integer = MapBackground.ROWS

        Dim LX, SX As Double
        Dim LY, SY As Double

        SX = (East - West) / Cols
        SY = (North - South) / Rows
        LX = West + SX / 2
        LY = North + SY / 2

        propItem.Id = 33922
        propItem.Len = 48
        propItem.Type = 10
        propItem.Value = MakeTiePointTag(LX, LY)
        GeoTiff.SetPropertyItem(propItem)
        propItem.Len = 24
        propItem.Id = 33550
        propItem.Value = MakePixelScaleTag(SX, SY)
        GeoTiff.SetPropertyItem(propItem)

        GeoTiff.Save(Filename, ImageFormat.Tiff)
        Stream.Dispose()

        Dim buffer() As Byte
        buffer = File.ReadAllBytes(Filename)

        Dim N As Integer = buffer.GetUpperBound(0)
        Dim Problem As Boolean = False
        If buffer(N - 25) = 10 Then
            buffer(N - 25) = 12
        Else
            Problem = True
        End If

        If buffer(N - 37) = 10 Then
            buffer(N - 37) = 12
        Else
            Problem = True
        End If

        If Problem = True Then MsgBox("Geotiff file may have some problems!")

        File.WriteAllBytes(Filename, buffer)
        ReDim buffer(0)


        Dim Name As String = "Photo_L" & CStr(Zoom)
        Dim X As Integer = XTilesFromLon(LonDispCenter, Zoom)
        Name = Name & "X" & CStr(X - 3) & "X" & CStr(X + 3)
        X = YTilesFromLat(LatDispCenter, Zoom)
        Name = Name & "Y" & CStr(X - 2) & "Y" & CStr(X + 2)

        SaveDataFile(Filename, North, South, West, East, Name)

    End Sub


    Friend Sub SaveDataFile(ByVal myfile As String, ByVal NLat As Double, ByVal SLat As Double, ByVal WLon As Double, ByVal ELon As Double, ByVal str As String)

        Dim A, DataFile, DataPath, FullFile As String

        DataFile = Path.GetFileNameWithoutExtension(myfile) & ".TXT"
        DataPath = Path.GetDirectoryName(myfile)

        FullFile = DataPath & "\" & DataFile

        If File.Exists(FullFile) Then
            A = "The data file:" & vbCrLf & vbCrLf & DataFile & vbCrLf & vbCrLf
            A = A & "already exists! Overwrite?"
            If MsgBox(A, MsgBoxStyle.YesNo Or MsgBoxStyle.Question) = 7 Then Exit Sub
        End If

        FileOpen(3, FullFile, OpenMode.Output)
        A = "[GEOGRAPHIC]"
        PrintLine(3, A)
        A = "Name=" & str
        PrintLine(3, A)
        A = "North=" & CStr(NLat)
        PrintLine(3, A)
        A = "South=" & CStr(SLat)
        PrintLine(3, A)
        A = "West=" & CStr(WLon)
        PrintLine(3, A)
        A = "East=" & CStr(ELon)
        PrintLine(3, A)
        PrintLine(3)

        FileClose(3)

    End Sub

    Friend Sub MakeBglPhoto(ByVal CopyBGLs As Boolean)

        Dim N As Integer
        Dim A As String

        For N = 1 To NoOfMaps
            If Maps(N).Selected Then
                A = UCase(Mid(Maps(N).Name, 1, 5))
                If A = "PHOTO" Then
                    A = UCase(Path.GetExtension(Maps(N).BMPSu))
                    If A = ".BMP" Then
                        MakeThisBglPhoto(N, CopyBGLs)
                    End If
                End If
            End If
        Next

    End Sub

    Private Sub MakeThisBglPhoto(ByVal N As Integer, ByVal CopyBGLs As Boolean)

        Dim K As Integer
        Dim NoOfSources As Integer
        Dim SourceFiles(6) As String
        Dim Variations(6) As String
        Dim All As Boolean
        Dim Day As Boolean

        SourceFiles(1) = Maps(N).BMPSu
        SourceFiles(2) = Maps(N).BMPSp
        SourceFiles(3) = Maps(N).BMPFa
        SourceFiles(4) = Maps(N).BMPWi
        SourceFiles(5) = Maps(N).BMPHw
        SourceFiles(6) = Maps(N).BMPLm

        Variations(1) = SummerVariations
        Variations(2) = SpringVariations
        Variations(3) = FallVariations
        Variations(4) = WinterVariations
        Variations(5) = HardWinterVariations
        Variations(6) = "Night"

        All = True
        For K = 2 To 6
            If SourceFiles(K) <> SourceFiles(1) Then
                All = False
                Exit For
            End If
        Next
        If All Then
            MakeThisBglPhotoAll(N, CopyBGLs)
            Exit Sub
        End If

        Day = True
        For K = 2 To 5
            If SourceFiles(K) <> SourceFiles(1) Then
                Day = False
                Exit For
            End If
        Next
        If Day Then
            MakeThisBglPhotoDay(N, CopyBGLs)
            Exit Sub
        End If

        For K = 2 To 5
            If SourceFiles(K) = SourceFiles(1) Then
                Variations(1) = Variations(1) & "," & Variations(K)
                SourceFiles(K) = ""
            End If
        Next

        If SourceFiles(2) <> "" Then
            For K = 3 To 5
                If SourceFiles(K) = SourceFiles(2) Then
                    Variations(2) = Variations(2) & "," & Variations(K)
                    SourceFiles(K) = ""
                End If
            Next
        End If

        If SourceFiles(3) <> "" Then
            For K = 4 To 5
                If SourceFiles(K) = SourceFiles(3) Then
                    Variations(3) = Variations(3) & "," & Variations(K)
                    SourceFiles(K) = ""
                End If
            Next
        End If

        If SourceFiles(4) <> "" Then
            If SourceFiles(5) = SourceFiles(4) Then
                Variations(4) = Variations(4) & "," & Variations(5)
                SourceFiles(5) = ""
            End If
        End If

        NoOfSources = 1
        For K = 2 To 5
            If SourceFiles(K) <> "" Then
                NoOfSources = NoOfSources + 1
                SourceFiles(NoOfSources) = SourceFiles(K)
                Variations(NoOfSources) = Variations(K)
            End If
        Next

        If SourceFiles(1) <> SourceFiles(6) Then
            NoOfSources = NoOfSources + 1
            SourceFiles(NoOfSources) = SourceFiles(6)
            Variations(NoOfSources) = Variations(6)
        End If

        ' now make INF

        Dim InfFile, BGLFile, BGLFileTarget As String
        Dim BaseName As String = Maps(N).Name
        Dim SourceFile As String
        InfFile = My.Application.Info.DirectoryPath & "\tools\work\" & BaseName & ".INF"
        BGLFile = My.Application.Info.DirectoryPath & "\tools\work\" & BaseName & ".BGL"

        Dim IsBlend As Boolean = False
        Dim IsWater As Boolean = False
        Dim BlendName As String = Path.GetFileNameWithoutExtension(Maps(N).BMPSu) & "_B.TIF"
        Dim WaterName As String = Path.GetFileNameWithoutExtension(Maps(N).BMPSu) & "_W.TIF"

        If File.Exists(My.Application.Info.DirectoryPath & "\tools\work\" & BlendName) Then
            IsBlend = True
        End If

        If File.Exists(My.Application.Info.DirectoryPath & "\tools\work\" & WaterName) Then
            IsWater = True
        End If

        Dim ExtraNoOfSources As Integer = 0
        If IsBlend Then ExtraNoOfSources = 1
        If IsWater Then ExtraNoOfSources = ExtraNoOfSources + 1

        If File.Exists(BGLFile) Then File.Delete(BGLFile)

        Dim Command As String = "resample" & " work\" & BaseName & ".INF"
        FileOpen(3, InfFile, OpenMode.Output)
        PrintLine(3, "[Source]")
        PrintLine(3, "   Type = MultiSource")
        PrintLine(3, "   NumberOfSources = " & CStr(NoOfSources + ExtraNoOfSources))
        For K = 1 To NoOfSources
            SourceFile = Path.GetFileName(SourceFiles(K))
            PrintLine(3)
            PrintLine(3, "[Source" & CStr(K) & "]")
            PrintLine(3, "   Type = BMP")
            PrintLine(3, "   Layer = Imagery")
            PrintLine(3, "   SourceDir = " & Chr(34) & "." & Chr(34))
            PrintLine(3, "   SourceFile = " & Chr(34) & SourceFile & Chr(34))
            PrintLine(3, "   Variation = " & Variations(K))

            If IsBlend Then PrintLine(3, "   Channel_BlendMask = " & CStr(NoOfSources + 1) & ".0")
            If IsWater And ExtraNoOfSources = 1 Then PrintLine(3, "   Channel_LandWaterMask = " & CStr(NoOfSources + 1) & ".0")
            If IsWater And ExtraNoOfSources = 2 Then PrintLine(3, "   Channel_LandWaterMask = " & CStr(NoOfSources + 2) & ".0")

            PrintLine(3, "   NullValue = 255,255,255")
            PrintLine(3, "   SamplingMethod = Gaussian")
            PrintLine(3, "   ulyMap = " & Str(Maps(N).NLAT))
            PrintLine(3, "   ulxMap = " & Str(Maps(N).WLON))
            PrintLine(3, "   xDim = " & Str((Maps(N).ELON - Maps(N).WLON) / Maps(N).COLS))
            PrintLine(3, "   yDim = " & Str((Maps(N).NLAT - Maps(N).SLAT) / Maps(N).ROWS))
        Next

        If IsBlend Then
            PrintLine(3)
            PrintLine(3, "[Source" & CStr(NoOfSources + 1) & "]")
            PrintLine(3, "   Type = TIFF")
            PrintLine(3, "   Layer = None")
            PrintLine(3, "   SourceDir = " & Chr(34) & "." & Chr(34))
            PrintLine(3, "   SourceFile = " & Chr(34) & BlendName & Chr(34))
            PrintLine(3, "   SamplingMethod = Gaussian")
            PrintLine(3, "   ulyMap = " & Str(Maps(N).NLAT))
            PrintLine(3, "   ulxMap = " & Str(Maps(N).WLON))
            PrintLine(3, "   xDim = " & Str((Maps(N).ELON - Maps(N).WLON) / Maps(N).COLS))
            PrintLine(3, "   yDim = " & Str((Maps(N).NLAT - Maps(N).SLAT) / Maps(N).ROWS))
        End If

        If IsWater Then
            PrintLine(3)
            If ExtraNoOfSources = 1 Then PrintLine(3, "[Source" & CStr(NoOfSources + 1) & "]")
            If ExtraNoOfSources = 2 Then PrintLine(3, "[Source" & CStr(NoOfSources + 2) & "]")
            PrintLine(3, "   Type = TIFF")
            PrintLine(3, "   Layer = None")
            PrintLine(3, "   SourceDir = " & Chr(34) & "." & Chr(34))
            PrintLine(3, "   SourceFile = " & Chr(34) & WaterName & Chr(34))
            PrintLine(3, "   SamplingMethod = Gaussian")
            PrintLine(3, "   ulyMap = " & Str(Maps(N).NLAT))
            PrintLine(3, "   ulxMap = " & Str(Maps(N).WLON))
            PrintLine(3, "   xDim = " & Str((Maps(N).ELON - Maps(N).WLON) / Maps(N).COLS))
            PrintLine(3, "   yDim = " & Str((Maps(N).NLAT - Maps(N).SLAT) / Maps(N).ROWS))
        End If

        PrintLine(3)
        PrintLine(3, "[Destination]")
        PrintLine(3, "   DestDir = " & Chr(34) & "." & Chr(34))
        PrintLine(3, "   DestBaseFileName = " & Chr(34) & BaseName & Chr(34))
        PrintLine(3, "   DestFileType = BGL")
        PrintLine(3, "   LOD = Auto")
        PrintLine(3, "   UseSourceDimensions = 1")
        PrintLine(3, "   CompressionQuality = " & CompressionQuality.ToString)
        FileClose(3)

        ChDrive(My.Application.Info.DirectoryPath)
        ChDir(My.Application.Info.DirectoryPath & "\tools\")

        ExecCmd(Command)

        If Not CopyBGLs Then Exit Sub

        ' copy BGL files
        Try
            BGLFileTarget = BGLProjectFolder & "\" & BaseName & ".BGL"
            If File.Exists(BGLFile) Then File.Copy(BGLFile, BGLFileTarget, True)
        Catch ex As Exception
            MsgBox("Copying BGL files failed! Try to close FSX.", MsgBoxStyle.Exclamation)
        End Try

    End Sub


    Private Sub MakeThisBglPhotoAll(ByVal N As Integer, ByVal CopyBGLs As Boolean)

        Dim InfFile, BGLFile, BGLFileTarget As String
        Dim BaseName As String = Maps(N).Name
        Dim SourceName As String = Path.GetFileName(Maps(N).BMPSu)

        InfFile = My.Application.Info.DirectoryPath & "\tools\work\" & BaseName & ".INF"
        BGLFile = My.Application.Info.DirectoryPath & "\tools\work\" & BaseName & ".BGL"

        Dim IsBlend As Boolean = False
        Dim IsWater As Boolean = False
        Dim BlendName As String = Path.GetFileNameWithoutExtension(Maps(N).BMPSu) & "_B.TIF"
        Dim WaterName As String = Path.GetFileNameWithoutExtension(Maps(N).BMPSu) & "_W.TIF"

        If File.Exists(My.Application.Info.DirectoryPath & "\tools\work\" & BlendName) Then
            IsBlend = True
        End If

        If File.Exists(My.Application.Info.DirectoryPath & "\tools\work\" & WaterName) Then
            IsWater = True
        End If

        Dim NoOfSources As Integer = 1
        If IsBlend Then NoOfSources = NoOfSources + 1
        If IsWater Then NoOfSources = NoOfSources + 1

        If File.Exists(BGLFile) Then File.Delete(BGLFile)

        Dim Command As String = "resample" & " work\" & BaseName & ".INF"
        FileOpen(3, InfFile, OpenMode.Output)

        PrintLine(3, "[Source]")
        If NoOfSources = 2 Then
            PrintLine(3, "   Type = MultiSource")
            PrintLine(3, "   NumberOfSources = 2")
            PrintLine(3, "[Source1]")
        End If

        If NoOfSources = 3 Then
            PrintLine(3, "   Type = MultiSource")
            PrintLine(3, "   NumberOfSources = 3")
            PrintLine(3, "[Source1]")
        End If

        PrintLine(3, "   Type = BMP")
        PrintLine(3, "   Layer = Imagery")
        PrintLine(3, "   SourceDir = " & Chr(34) & "." & Chr(34))
        PrintLine(3, "   SourceFile = " & Chr(34) & SourceName & Chr(34))
        PrintLine(3, "   Variation = All")

        If IsBlend Then PrintLine(3, "   Channel_BlendMask = 2.0")
        If IsWater And NoOfSources = 2 Then PrintLine(3, "   Channel_LandWaterMask = 2.0")
        If IsWater And NoOfSources = 3 Then PrintLine(3, "   Channel_LandWaterMask = 3.0")

        PrintLine(3, "   NullValue = 255,255,255")
        PrintLine(3, "   SamplingMethod = Gaussian")
        PrintLine(3, "   ulyMap = " & Str(Maps(N).NLAT))
        PrintLine(3, "   ulxMap = " & Str(Maps(N).WLON))
        PrintLine(3, "   xDim = " & Str((Maps(N).ELON - Maps(N).WLON) / Maps(N).COLS))
        PrintLine(3, "   yDim = " & Str((Maps(N).NLAT - Maps(N).SLAT) / Maps(N).ROWS))

        If IsBlend Then
            PrintLine(3)
            PrintLine(3, "[Source2]")
            PrintLine(3, "   Type = TIFF")
            PrintLine(3, "   Layer = None")
            PrintLine(3, "   SourceDir = " & Chr(34) & "." & Chr(34))
            PrintLine(3, "   SourceFile = " & Chr(34) & BlendName & Chr(34))
            PrintLine(3, "   SamplingMethod = Gaussian")
            PrintLine(3, "   ulyMap = " & Str(Maps(N).NLAT))
            PrintLine(3, "   ulxMap = " & Str(Maps(N).WLON))
            PrintLine(3, "   xDim = " & Str((Maps(N).ELON - Maps(N).WLON) / Maps(N).COLS))
            PrintLine(3, "   yDim = " & Str((Maps(N).NLAT - Maps(N).SLAT) / Maps(N).ROWS))
        End If

        If IsWater Then
            PrintLine(3)
            If NoOfSources = 2 Then PrintLine(3, "[Source2]")
            If NoOfSources = 3 Then PrintLine(3, "[Source3]")
            PrintLine(3, "   Type = TIFF")
            PrintLine(3, "   Layer = None")
            PrintLine(3, "   SourceDir = " & Chr(34) & "." & Chr(34))
            PrintLine(3, "   SourceFile = " & Chr(34) & WaterName & Chr(34))
            PrintLine(3, "   SamplingMethod = Gaussian")
            PrintLine(3, "   ulyMap = " & Str(Maps(N).NLAT))
            PrintLine(3, "   ulxMap = " & Str(Maps(N).WLON))
            PrintLine(3, "   xDim = " & Str((Maps(N).ELON - Maps(N).WLON) / Maps(N).COLS))
            PrintLine(3, "   yDim = " & Str((Maps(N).NLAT - Maps(N).SLAT) / Maps(N).ROWS))
        End If

        PrintLine(3)
        PrintLine(3, "[Destination]")
        PrintLine(3, "   DestDir = " & Chr(34) & "." & Chr(34))
        PrintLine(3, "   DestBaseFileName = " & Chr(34) & BaseName & Chr(34))
        PrintLine(3, "   DestFileType = BGL")
        PrintLine(3, "   LOD = Auto")
        PrintLine(3, "   UseSourceDimensions = 1")
        PrintLine(3, "   CompressionQuality = " & CompressionQuality.ToString)
        FileClose(3)

        ChDrive(My.Application.Info.DirectoryPath)
        ChDir(My.Application.Info.DirectoryPath & "\tools\")

        ExecCmd(Command)

        If Not CopyBGLs Then Exit Sub

        ' copy BGL files
        Try
            BGLFileTarget = BGLProjectFolder & "\" & BaseName & ".BGL"
            If File.Exists(BGLFile) Then File.Copy(BGLFile, BGLFileTarget, True)
        Catch ex As Exception
            MsgBox("Copying BGL files failed! Try to close FSX.", MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Friend Sub MakeBglPhotoBackground(ByVal CopyBGLs As Boolean)

        Dim BaseName As String = "L" & CStr(Zoom)
        Dim X As Integer = XTilesFromLon(LonDispCenter, Zoom)
        BaseName = BaseName & "X" & CStr(X - 3) & "X" & CStr(X + 3)
        X = YTilesFromLat(LatDispCenter, Zoom)
        BaseName = BaseName & "Y" & CStr(X - 2) & "Y" & CStr(X + 2)

        Dim BMPSourceName As String = BaseName & ".BMP"

        ' save the background as bitmap file
        Dim BMPFile As String = My.Application.Info.DirectoryPath & "\tools\work\" & BaseName & ".BMP"
        ImageBackground.Save(BMPFile, ImageFormat.Bmp)

        Dim InfFile As String = My.Application.Info.DirectoryPath & "\tools\work\" & BaseName & ".INF"
        Dim BGLFile As String = My.Application.Info.DirectoryPath & "\tools\work\" & BaseName & ".BGL"
        If File.Exists(BGLFile) Then File.Delete(BGLFile)

        Dim BlendFile As String = My.Application.Info.DirectoryPath & "\tools\work\blendmask.tif"
        If Not File.Exists(BlendFile) Then
            File.Copy(My.Application.Info.DirectoryPath & "\tools\bmps\blendmask.tif", BlendFile)
        End If

        Dim Command As String = "resample" & " work\" & BaseName & ".INF"
        FileOpen(3, InfFile, OpenMode.Output)

        PrintLine(3, "[Source]")
        PrintLine(3, "   Type = MultiSource")
        PrintLine(3, "   NumberOfSources = 2")

        PrintLine(3, "[Source1]")
        PrintLine(3, "   Type = BMP")
        PrintLine(3, "   Layer = Imagery")
        PrintLine(3, "   SourceDir = " & Chr(34) & "." & Chr(34))
        PrintLine(3, "   SourceFile = " & Chr(34) & BMPSourceName & Chr(34))
        PrintLine(3, "   Variation = All")
        PrintLine(3, "   Channel_BlendMask = 2.0")
        PrintLine(3, "   NullValue = 255,255,255")
        PrintLine(3, "   SamplingMethod = Gaussian")
        PrintLine(3, "   ulyMap = " & Str(MapBackground.NLAT))
        PrintLine(3, "   ulxMap = " & Str(MapBackground.WLON))
        PrintLine(3, "   xDim = " & Str((MapBackground.ELON - MapBackground.WLON) / MapBackground.COLS))
        PrintLine(3, "   yDim = " & Str((MapBackground.NLAT - MapBackground.SLAT) / MapBackground.ROWS))
        PrintLine(3)

        PrintLine(3, "[Source2]")
        PrintLine(3, "   Type = TIFF")
        PrintLine(3, "   Layer = None")
        PrintLine(3, "   SourceDir = " & Chr(34) & "." & Chr(34))
        PrintLine(3, "   SourceFile = " & Chr(34) & "blendmask.tif" & Chr(34))
        PrintLine(3, "   SamplingMethod = Gaussian")
        PrintLine(3, "   ulyMap = " & Str(MapBackground.NLAT))
        PrintLine(3, "   ulxMap = " & Str(MapBackground.WLON))
        PrintLine(3, "   xDim = " & Str((MapBackground.ELON - MapBackground.WLON) / MapBackground.COLS))
        PrintLine(3, "   yDim = " & Str((MapBackground.NLAT - MapBackground.SLAT) / MapBackground.ROWS))
        PrintLine(3)

        PrintLine(3, "[Destination]")
        PrintLine(3, "   DestDir = " & Chr(34) & "." & Chr(34))
        PrintLine(3, "   DestBaseFileName = " & Chr(34) & BaseName & Chr(34))
        PrintLine(3, "   DestFileType = BGL")
        PrintLine(3, "   LOD = Auto")
        PrintLine(3, "   UseSourceDimensions = 1")
        PrintLine(3, "   CompressionQuality = " & CompressionQuality.ToString)
        FileClose(3)

        ChDrive(My.Application.Info.DirectoryPath)
        ChDir(My.Application.Info.DirectoryPath & "\tools\")

        ExecCmd(Command)

        If Not CopyBGLs Then Exit Sub
        Dim BGLFileTarget As String
        ' copy BGL files
        Try
            BGLFileTarget = BGLProjectFolder & "\" & BaseName & ".BGL"
            If File.Exists(BGLFile) Then File.Copy(BGLFile, BGLFileTarget, True)
        Catch ex As Exception
            MsgBox("Copying BGL files failed! Try to close FSX.", MsgBoxStyle.Exclamation)
        End Try

    End Sub



    Private Sub MakeThisBglPhotoDay(ByVal N As Integer, ByVal CopyBGLs As Boolean)

        Dim InfFile, BGLFile, BGLFileTarget As String
        Dim BaseName As String = Maps(N).Name
        Dim SourceDay As String = Path.GetFileName(Maps(N).BMPSu)
        Dim SourceNight As String = Path.GetFileName(Maps(N).BMPLm)
        InfFile = My.Application.Info.DirectoryPath & "\tools\work\" & BaseName & ".INF"
        BGLFile = My.Application.Info.DirectoryPath & "\tools\work\" & BaseName & ".BGL"

        Dim IsBlend As Boolean = False
        Dim IsWater As Boolean = False
        Dim BlendName As String = Path.GetFileNameWithoutExtension(Maps(N).BMPSu) & "_B.TIF"
        Dim WaterName As String = Path.GetFileNameWithoutExtension(Maps(N).BMPSu) & "_W.TIF"

        If File.Exists(My.Application.Info.DirectoryPath & "\tools\work\" & BlendName) Then
            IsBlend = True
        End If

        If File.Exists(My.Application.Info.DirectoryPath & "\tools\work\" & WaterName) Then
            IsWater = True
        End If

        Dim NoOfSources As Integer = 2
        If IsBlend Then NoOfSources = NoOfSources + 1
        If IsWater Then NoOfSources = NoOfSources + 1

        If File.Exists(BGLFile) Then File.Delete(BGLFile)

        Dim Command As String = "resample" & " work\" & BaseName & ".INF"
        FileOpen(3, InfFile, OpenMode.Output)

        PrintLine(3, "[Source]")
        PrintLine(3, "   Type = MultiSource")
        If NoOfSources = 2 Then PrintLine(3, "   NumberOfSources = 2")
        If NoOfSources = 3 Then PrintLine(3, "   NumberOfSources = 3")
        If NoOfSources = 4 Then PrintLine(3, "   NumberOfSources = 4")

        PrintLine(3)
        PrintLine(3, "[Source1]")
        PrintLine(3, "   Type = BMP")
        PrintLine(3, "   Layer = Imagery")
        PrintLine(3, "   SourceDir = " & Chr(34) & "." & Chr(34))
        PrintLine(3, "   SourceFile = " & Chr(34) & SourceDay & Chr(34))
        PrintLine(3, "   Variation = Day")

        If IsBlend Then PrintLine(3, "   Channel_BlendMask = 3.0")
        If IsWater And NoOfSources = 3 Then PrintLine(3, "   Channel_LandWaterMask = 3.0")
        If IsWater And NoOfSources = 4 Then PrintLine(3, "   Channel_LandWaterMask = 4.0")

        PrintLine(3, "   NullValue = 255,255,255")
        PrintLine(3, "   SamplingMethod = Gaussian")
        PrintLine(3, "   ulyMap = " & Str(Maps(N).NLAT))
        PrintLine(3, "   ulxMap = " & Str(Maps(N).WLON))
        PrintLine(3, "   xDim = " & Str((Maps(N).ELON - Maps(N).WLON) / Maps(N).COLS))
        PrintLine(3, "   yDim = " & Str((Maps(N).NLAT - Maps(N).SLAT) / Maps(N).ROWS))
        PrintLine(3)
        PrintLine(3, "[Source2]")
        PrintLine(3, "   Type = BMP")
        PrintLine(3, "   Layer = Imagery")
        PrintLine(3, "   SourceDir = " & Chr(34) & "." & Chr(34))
        PrintLine(3, "   SourceFile = " & Chr(34) & SourceNight & Chr(34))
        PrintLine(3, "   Variation = Night")

        If IsBlend Then PrintLine(3, "   Channel_BlendMask = 3.0")
        If IsWater And NoOfSources = 3 Then PrintLine(3, "   Channel_LandWaterMask = 3.0")
        If IsWater And NoOfSources = 4 Then PrintLine(3, "   Channel_LandWaterMask = 4.0")

        PrintLine(3, "   NullValue = 255,255,255")
        PrintLine(3, "   SamplingMethod = Gaussian")
        PrintLine(3, "   ulyMap = " & Str(Maps(N).NLAT))
        PrintLine(3, "   ulxMap = " & Str(Maps(N).WLON))
        PrintLine(3, "   xDim = " & Str((Maps(N).ELON - Maps(N).WLON) / Maps(N).COLS))
        PrintLine(3, "   yDim = " & Str((Maps(N).NLAT - Maps(N).SLAT) / Maps(N).ROWS))

        If IsBlend Then
            PrintLine(3)
            PrintLine(3, "[Source3]")
            PrintLine(3, "   Type = TIFF")
            PrintLine(3, "   Layer = None")
            PrintLine(3, "   SourceDir = " & Chr(34) & "." & Chr(34))
            PrintLine(3, "   SourceFile = " & Chr(34) & BlendName & Chr(34))
            PrintLine(3, "   SamplingMethod = Gaussian")
            PrintLine(3, "   ulyMap = " & Str(Maps(N).NLAT))
            PrintLine(3, "   ulxMap = " & Str(Maps(N).WLON))
            PrintLine(3, "   xDim = " & Str((Maps(N).ELON - Maps(N).WLON) / Maps(N).COLS))
            PrintLine(3, "   yDim = " & Str((Maps(N).NLAT - Maps(N).SLAT) / Maps(N).ROWS))
        End If

        If IsWater Then
            PrintLine(3)
            If NoOfSources = 3 Then PrintLine(3, "[Source3]")
            If NoOfSources = 4 Then PrintLine(3, "[Source4]")
            PrintLine(3, "   Type = TIFF")
            PrintLine(3, "   Layer = None")
            PrintLine(3, "   SourceDir = " & Chr(34) & "." & Chr(34))
            PrintLine(3, "   SourceFile = " & Chr(34) & WaterName & Chr(34))
            PrintLine(3, "   SamplingMethod = Gaussian")
            PrintLine(3, "   ulyMap = " & Str(Maps(N).NLAT))
            PrintLine(3, "   ulxMap = " & Str(Maps(N).WLON))
            PrintLine(3, "   xDim = " & Str((Maps(N).ELON - Maps(N).WLON) / Maps(N).COLS))
            PrintLine(3, "   yDim = " & Str((Maps(N).NLAT - Maps(N).SLAT) / Maps(N).ROWS))
        End If

        PrintLine(3)
        PrintLine(3, "[Destination]")
        PrintLine(3, "   DestDir = " & Chr(34) & "." & Chr(34))
        PrintLine(3, "   DestBaseFileName = " & Chr(34) & BaseName & Chr(34))
        PrintLine(3, "   DestFileType = BGL")
        PrintLine(3, "   LOD = Auto")
        PrintLine(3, "   UseSourceDimensions = 1")
        PrintLine(3, "   CompressionQuality = " & CompressionQuality.ToString)
        FileClose(3)

        ChDrive(My.Application.Info.DirectoryPath)
        ChDir(My.Application.Info.DirectoryPath & "\tools\")

        ExecCmd(Command)

        If Not CopyBGLs Then Exit Sub

        ' copy BGL files
        Try
            BGLFileTarget = BGLProjectFolder & "\" & BaseName & ".BGL"
            If File.Exists(BGLFile) Then File.Copy(BGLFile, BGLFileTarget, True)
        Catch ex As Exception
            MsgBox("Copying BGL files failed! Try to close FSX.", MsgBoxStyle.Exclamation)
        End Try


    End Sub

End Module


