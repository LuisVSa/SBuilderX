
Option Strict Off
Option Explicit On
Imports System.Runtime.Serialization
Imports System.Reflection
Imports system.Xml

Module moduleFILE_IO

    Private Structure LegacyPoly
        Dim LClass As Integer
        Dim Guid As String
    End Structure
    Private LegacyPolys() As LegacyPoly
    Private NoOfLegacyPolys As Integer

    Private Structure LegacyLine
        Dim Legacy As Integer
        Dim Guid As String
        Dim Type As String
    End Structure
    Private LegacyLines() As LegacyLine
    Private NoOfLegacyLines As Integer

    Friend SBPDir As String
    Friend SBXDir As String
    Friend SURDir As String
    Friend OBJDir As String
    Friend RAWDir As String
    Friend LIBDir As String
    Friend TEXDir As String
    Friend BMPDir As String
    Friend SHPDir As String
    Friend KMLDir As String

    Friend BackUpFileName As String
    Friend BackUpSeconds As Integer
    Friend BackUpFileCounter As Integer

    Friend Const SettingsKey As String = "Settings" ' INI File Key constant.
    Friend RecentFiles(4) As String


    Friend Sub GetSettings()

        Dim IniSettings As New Collection()

        On Error GoTo erro

        '[Main]
        IniSettings.Add("-9.136076", "LonIniCenter")
        IniSettings.Add("38.7813203", "LatIniCenter")
        IniSettings.Add("False", "DecimalDegrees")
        IniSettings.Add("True", "MeasuringMeters")
        IniSettings.Add(My.Application.Info.DirectoryPath & "\Scenery", "BGLFolder")
        IniSettings.Add("False", "OriginalTerrainCFG")
        IniSettings.Add("FSX", "NameOfSim")
        IniSettings.Add(My.Application.Info.DirectoryPath & "\Tools", "SimPath")

        ' [Edit]
        IniSettings.Add("False", "BackUpON")
        IniSettings.Add("60", "BackUpSeconds")
        IniSettings.Add("True", "AskDelete")
        IniSettings.Add("True", "ShowDonation")
        IniSettings.Add("True", "BorderON")
        IniSettings.Add("True", "ShowLabels")
        IniSettings.Add(ArgbFromColor(Color.Green), "SelectedPointColor")
        IniSettings.Add(ArgbFromColor(Color.Red), "UnselectedPointColor")

        ' [Misc]
        IniSettings.Add("False", "AutoLinePolyJoin")
        IniSettings.Add("True", "DisplayJoin")
        IniSettings.Add("True", "DirJoin")
        IniSettings.Add("True", "NameJoin")
        IniSettings.Add("True", "NoEndsSmooth")
        IniSettings.Add("True", "CornerSmooth")
        IniSettings.Add("10", "SampleDistance")
        IniSettings.Add("50", "SmoothDistance")
        IniSettings.Add("11", "DefaultLC")
        IniSettings.Add("12", "DefaultWC")
        'IniSettings.Add("DemoAppId01082013GAL", "Here_app_id")
        'IniSettings.Add("AJKnXv84fjrb0KIHawS0Tg", "Here_app_code")
        IniSettings.Add("", "GoogleMapsAPI")
        IniSettings.Add("False", "MakeSlopeXY")

        ' [Grid]
        IniSettings.Add(ArgbFromColor(Color.Green), "GridColor")
        IniSettings.Add(ArgbFromColor(Color.Red), "GridLODColor")
        IniSettings.Add("1", "GridWidth")
        IniSettings.Add("True", "ZoomOnQMID")
        IniSettings.Add("False", "CenterOnMouseWheel")

        ' [Aircraft]
        IniSettings.Add("5000", "ShowAircraftPeriod")
        IniSettings.Add("0.5", "AircraftAltitudeOffset")
        IniSettings.Add("-20M", "ExtraExtrusionAltitude")

        '[Objects]
        IniSettings.Add(My.Application.Info.DirectoryPath & "\Rwy12", "Rwy12Path")
        IniSettings.Add(My.Application.Info.DirectoryPath & "\API", "MacroAPIPath")
        IniSettings.Add(My.Application.Info.DirectoryPath & "\ASD", "MacroASDPath")
        IniSettings.Add(My.Application.Info.DirectoryPath & "\LibObjects", "LibObjectsPath")

        '[Lines]
        IniSettings.Add("0", "DefaultLineAltitude")
        IniSettings.Add("50", "DefaultLineWidth")
        IniSettings.Add("1", "LinePenWidth")
        IniSettings.Add(ArgbFromColor(Color.Green), "SelectedLineColor")
        IniSettings.Add(ArgbFromColor(Color.Blue), "DefaultLineColor")
        IniSettings.Add("False", "MakeClosedLineFromPoly")

        '[Polys]
        IniSettings.Add("0", "DefaultPolyAltitude")
        IniSettings.Add("1", "PolyPenWidth")
        IniSettings.Add("True", "PolyFILL")
        IniSettings.Add("80FFFF00", "DefaultPolyColor")
        IniSettings.Add(ArgbFromColor(Color.Black), "PolyColorBorder")

        '[Shapes]
        IniSettings.Add("{89ABCDEF-EDCB-A987-6543-210FEDCBA000}", "ShapeLineGuid")
        IniSettings.Add("0", "ShapeLineAltitude")
        IniSettings.Add("50", "ShapeLineWidth")
        IniSettings.Add(ArgbFromColor(Color.Blue), "ShapeLineColor")
        IniSettings.Add("{9ABCDEF0-FEDC-BA98-7654-3210FEDCB000}", "ShapePolyGuid")
        IniSettings.Add("0", "ShapePolyAltitude")
        IniSettings.Add(ArgbFromColor(Color.Pink), "ShapePolyColor")
        IniSettings.Add("True", "AddToCells")

        '[Tiles]
        IniSettings.Add("", "ActiveTileFolder")
        IniSettings.Add("True", "ReprojectMercatorTiles")
        IniSettings.Add("July,August,September", "SummerVariations")
        IniSettings.Add("April,May,June", "SpringVariations")
        IniSettings.Add("October,November", "FallVariations")
        IniSettings.Add("December,February,March", "WinterVariations")
        IniSettings.Add("January", "HardWinterVariations")
        IniSettings.Add("85", "CompressionQuality")

        '[BLN]
        IniSettings.Add(",", "BLNSeparator")
        If BLNSeparator = "tab" Then BLNSeparator = Chr(9)
        IniSettings.Add("{9ABCDEF0-FEDC-BA98-7654-3210FEDCB000}", "BLNPolyGuid")
        IniSettings.Add(ArgbFromColor(Color.Red), "BLNPolyColor")
        IniSettings.Add("{89ABCDEF-EDCB-A987-6543-210FEDCBA000}", "BLNLineGuid")
        IniSettings.Add(ArgbFromColor(Color.Red), "BLNLineColor")
        IniSettings.Add("True", "BLNIsPolyAlt")
        IniSettings.Add("True", "BLNIsLineAlt")
        IniSettings.Add("35", "BLNStartWidth")
        IniSettings.Add("35", "BLNEndWidth")
        IniSettings.Add("True", "BLNLineFromPoly")
        IniSettings.Add("True", "BLNExportAltitudes")

        '[RecentDirs]
        IniSettings.Add("", "SBPDir")
        IniSettings.Add("", "SBXDir")
        IniSettings.Add("", "SHPDir")
        IniSettings.Add("", "SURDir")
        IniSettings.Add("", "TEXDir")
        IniSettings.Add("", "BMPDir")
        IniSettings.Add("", "OBJDir")
        IniSettings.Add("", "RAWDir")
        IniSettings.Add("", "LIBDir")
        IniSettings.Add("", "KMLDir")

        IniSettings.Add("", "RecentFile1")
        IniSettings.Add("", "RecentFile2")
        IniSettings.Add("", "RecentFile3")
        IniSettings.Add("", "RecentFile4")

        Dim NF As Integer = FreeFile()
        Dim myLine, A, B As String
        Dim N As Integer

        If File.Exists(AppIni) Then

            ' added this October 2017
            File.Copy(AppIni, AppPath & "\SBuilderX_backup.ini", True)

            FileOpen(NF, AppIni, OpenMode.Input)
            While Not EOF(NF)
                myLine = LineInput(NF)
                N = InStr(myLine, "=")
                If N > 0 Then
                    A = myLine.Substring(0, N - 1)
                    If IniSettings.Contains(A) Then
                        B = myLine.Substring(N)
                        IniSettings.Remove(A)
                        IniSettings.Add(B, A)
                    End If
                End If
            End While
            FileClose(NF)

        End If

        '[Main]
        LonIniCenter = Val(IniSettings.Item("LonIniCenter"))
        LatIniCenter = Val(IniSettings.Item("LatIniCenter"))
        DecimalDegrees = CBool(IniSettings.Item("DecimalDegrees"))
        MeasuringMeters = CBool(IniSettings.Item("MeasuringMeters"))
        BGLFolder = IniSettings.Item("BGLFolder")
        OriginalTerrainCFG = CBool(IniSettings.Item("OriginalTerrainCFG"))
        NameOfSim = CStr(IniSettings.Item("NameOfSim"))
        SimPath = CStr(IniSettings.Item("SimPath"))

        '[Edit]
        BackUpON = CBool(IniSettings.Item("BackUpON"))
        BackUpSeconds = Val(IniSettings.Item("BackUpSeconds"))
        BorderON = CBool(IniSettings.Item("BorderON"))
        AskDelete = CBool(IniSettings.Item("AskDelete"))
        ShowDonation = CBool(IniSettings.Item("ShowDonation"))
        ShowLabels = CBool(IniSettings.Item("ShowLabels"))
        SelectedPointColor = ColorFromArgb(IniSettings.Item("SelectedPointColor"))
        UnselectedPointColor = ColorFromArgb(IniSettings.Item("UnselectedPointColor"))

        '[Misc]
        AutoLinePolyJoin = CBool(IniSettings.Item("AutoLinePolyJoin"))
        DisplayJoin = CBool(IniSettings.Item("DisplayJoin"))
        DirJoin = CBool(IniSettings.Item("DirJoin"))
        NameJoin = CBool(IniSettings.Item("NameJoin"))
        NoEndsSmooth = CBool(IniSettings.Item("NoEndsSmooth"))
        CornerSmooth = CBool(IniSettings.Item("CornerSmooth"))
        SampleDistance = Val(IniSettings.Item("SampleDistance"))
        SmoothDistance = Val(IniSettings.Item("SmoothDistance"))
        DefaultLC = CByte(IniSettings.Item("DefaultLC"))
        DefaultWC = CByte(IniSettings.Item("DefaultWC"))
        'Here_app_id = CStr(IniSettings.Item("Here_app_id"))
        'Here_app_code = CStr(IniSettings.Item("Here_app_code"))
        GoogleMapsAPI = CStr(IniSettings.Item("GoogleMapsAPI"))
        MakeSlopeXY = CBool(IniSettings.Item("MakeSlopeXY"))

        '[Grid]
        GridColor = ColorFromArgb(IniSettings.Item("GridColor"))
        GridLODColor = ColorFromArgb(IniSettings.Item("GridLODColor"))
        GridWidth = Val(IniSettings.Item("GridWidth"))
        ZoomOnQMID = CBool(IniSettings.Item("ZoomOnQMID"))
        If GridWidth > 2 Then GridWidth = 2
        CenterOnMouseWheel = CBool(IniSettings.Item("CenterOnMouseWheel"))

        '[Aircraft]
        ShowAircraftPeriod = Val(IniSettings.Item("ShowAircraftPeriod"))
        AircraftAltitudeOffset = Val(IniSettings.Item("AircraftAltitudeOffset"))
        ExtraExtrusionAltitude = CStr(IniSettings.Item("ExtraExtrusionAltitude"))

        '[Objects]
        Rwy12Path = CStr(IniSettings.Item("Rwy12Path"))
        MacroAPIPath = CStr(IniSettings.Item("MacroAPIPath"))
        MacroASDPath = CStr(IniSettings.Item("MacroASDPath"))
        LibObjectsPath = CStr(IniSettings.Item("LibObjectsPath"))

        '[Lines]
        DefaultLineAltitude = Val(IniSettings.Item("DefaultLineAltitude"))
        DefaultLineWidth = Val(IniSettings.Item("DefaultLineWidth"))
        LinePenWidth = Val(IniSettings.Item("LinePenWidth"))
        If LinePenWidth > 2 Then LinePenWidth = 2
        SelectedLineColor = ColorFromArgb(IniSettings.Item("SelectedLineColor"))
        DefaultLineColor = ColorFromArgb(IniSettings.Item("DefaultLineColor"))
        MakeClosedLineFromPoly = CBool(IniSettings.Item("MakeClosedLineFromPoly"))

        '[Polys]
        DefaultPolyAltitude = Val(IniSettings.Item("DefaultPolyAltitude"))
        PolyPenWidth = Val(IniSettings.Item("PolyPenWidth"))
        If PolyPenWidth > 2 Then PolyPenWidth = 2
        PolyFILL = CBool(IniSettings.Item("PolyFILL"))
        DefaultPolyColor = ColorFromArgb(IniSettings.Item("DefaultPolyColor"))
        PolyColorBorder = ColorFromArgb(IniSettings.Item("PolyColorBorder"))

        '[Shapes]
        ShapeLineGuid = CStr(IniSettings.Item("ShapeLineGuid"))
        ShapeLineAltitude = Val(IniSettings.Item("ShapeLineAltitude"))
        ShapeLineWidth = Val(IniSettings.Item("ShapeLineWidth"))
        ShapeLineColor = ColorFromArgb(IniSettings.Item("ShapeLineColor"))
        ShapePolyGuid = CStr(IniSettings.Item("ShapePolyGuid"))
        ShapePolyAltitude = Val(IniSettings.Item("ShapePolyAltitude"))
        ShapePolyColor = ColorFromArgb(IniSettings.Item("ShapePolyColor"))
        AddToCells = CBool(IniSettings.Item("AddToCells"))

        '[Tiles]
        ActiveTileFolder = CStr(IniSettings.Item("ActiveTileFolder"))
        ReprojectMercatorTiles = CBool(IniSettings.Item("ReprojectMercatorTiles"))
        SummerVariations = CStr(IniSettings.Item("SummerVariations"))
        SpringVariations = CStr(IniSettings.Item("SpringVariations"))
        FallVariations = CStr(IniSettings.Item("FallVariations"))
        WinterVariations = CStr(IniSettings.Item("WinterVariations"))
        HardWinterVariations = CStr(IniSettings.Item("HardWinterVariations"))
        CompressionQuality = CInt(IniSettings.Item("CompressionQuality"))

        '[BLN]
        BLNSeparator = CStr(IniSettings.Item("BLNSeparator"))
        If BLNSeparator = "tab" Then BLNSeparator = Chr(9)
        BLNPolyGuid = CStr(IniSettings.Item("BLNPolyGuid"))
        BLNPolyColor = ColorFromArgb(IniSettings.Item("BLNPolyColor"))
        BLNLineGuid = CStr(IniSettings.Item("BLNLineGuid"))
        BLNLineColor = ColorFromArgb(IniSettings.Item("BLNLineColor"))
        BLNIsPolyAlt = CBool(IniSettings.Item("BLNIsPolyAlt"))
        BLNIsLineAlt = CBool(IniSettings.Item("BLNIsLineAlt"))
        BLNStartWidth = Val(IniSettings.Item("BLNStartWidth"))
        BLNEndWidth = Val(IniSettings.Item("BLNEndWidth"))
        BLNLineFromPoly = CBool(IniSettings.Item("BLNLineFromPoly"))
        BLNExportAltitudes = CBool(IniSettings.Item("BLNExportAltitudes"))

        '[RecentDirs]
        SHPDir = CStr(IniSettings.Item("SHPDir"))
        SBPDir = CStr(IniSettings.Item("SBPDir"))
        SBXDir = CStr(IniSettings.Item("SBXDir"))
        SURDir = CStr(IniSettings.Item("SURDir"))
        TEXDir = CStr(IniSettings.Item("TEXDir"))
        BMPDir = CStr(IniSettings.Item("BMPDir"))
        OBJDir = CStr(IniSettings.Item("OBJDir"))
        RAWDir = CStr(IniSettings.Item("RAWDir"))
        LIBDir = CStr(IniSettings.Item("LIBDir"))
        KMLDir = CStr(IniSettings.Item("KMLDir"))

        '[RecentFiles]
        RecentFiles(1) = CStr(IniSettings.Item("RecentFile1"))
        RecentFiles(2) = CStr(IniSettings.Item("RecentFile2"))
        RecentFiles(3) = CStr(IniSettings.Item("RecentFile3"))
        RecentFiles(4) = CStr(IniSettings.Item("RecentFile4"))

        'PRINT INI FILE
        '**************
        WriteSettings()

        Exit Sub
erro:
        A = "Error in reading/writing the INI file! If you can not" & vbCrLf
        A = A & "determine the cause of this error, delete the INI file" & vbCrLf
        A = A & "and SBuilderX will recreate it! SBuilderX will stop now!"
        MsgBox(A, MsgBoxStyle.Critical)

        End

    End Sub

    Friend Sub WriteSettings()

        Dim NF As Integer = FreeFile()

        FileOpen(NF, AppIni, OpenMode.Output)

        PrintLine(NF, "[Recent Files]")
        PrintLine(NF, "RecentFile1=" & RecentFiles(1))
        PrintLine(NF, "RecentFile2=" & RecentFiles(2))
        PrintLine(NF, "RecentFile3=" & RecentFiles(3))
        PrintLine(NF, "RecentFile4=" & RecentFiles(4))

        PrintLine(NF)
        PrintLine(NF, "[RecentDirs]")
        PrintLine(NF, "SBPDir=" & SBPDir)
        PrintLine(NF, "SBXDir=" & SBXDir)
        PrintLine(NF, "SHPDir=" & SHPDir)
        PrintLine(NF, "SURDir=" & SURDir)
        PrintLine(NF, "TEXDir=" & TEXDir)
        PrintLine(NF, "BMPDir=" & BMPDir)
        PrintLine(NF, "OBJDir=" & OBJDir)
        PrintLine(NF, "RAWDir=" & RAWDir)
        PrintLine(NF, "LIBDir=" & LIBDir)
        PrintLine(NF, "KMLDir=" & KMLDir)

        PrintLine(NF)
        PrintLine(NF, "[Main]")
        PrintLine(NF, "LonIniCenter=" & Str(LonIniCenter))
        PrintLine(NF, "LatIniCenter=" & Str(LatIniCenter))
        PrintLine(NF, "DecimalDegrees=" & DecimalDegrees.ToString)
        PrintLine(NF, "MeasuringMeters=" & MeasuringMeters.ToString)
        PrintLine(NF, "BGLFolder=" & BGLFolder)
        PrintLine(NF, "OriginalTerrainCFG=" & OriginalTerrainCFG.ToString)
        ' after October 2017
        PrintLine(NF, "NameOfSim=" & NameOfSim)
        PrintLine(NF, "SimPath=" & SimPath)


        PrintLine(NF)
        PrintLine(NF, "[Edit]")
        PrintLine(NF, "BackUpON=" & BackUpON.ToString)
        PrintLine(NF, "BackUpSeconds=" & BackUpSeconds.ToString)
        PrintLine(NF, "AskDelete=" & AskDelete.ToString)
        PrintLine(NF, "BorderON=" & BorderON.ToString)
        PrintLine(NF, "ShowDonation=" & ShowDonation.ToString)
        PrintLine(NF, "ShowLabels=" & ShowLabels.ToString)
        PrintLine(NF, "SelectedPointColor=" & ArgbFromColor(SelectedPointColor))
        PrintLine(NF, "UnselectedPointColor=" & ArgbFromColor(UnselectedPointColor))

        PrintLine(NF)
        PrintLine(NF, "[Miscelaneous]")
        PrintLine(NF, "AutoLinePolyJoin=" & AutoLinePolyJoin.ToString)
        PrintLine(NF, "DisplayJoin=" & DisplayJoin.ToString)
        PrintLine(NF, "DirJoin=" & DirJoin.ToString)
        PrintLine(NF, "NameJoin=" & NameJoin.ToString)
        PrintLine(NF, "NoEndsSmooth=" & NoEndsSmooth.ToString)
        PrintLine(NF, "CornerSmooth=" & CornerSmooth.ToString)
        PrintLine(NF, "SampleDistance=" & Str(SampleDistance))
        PrintLine(NF, "SmoothDistance=" & Str(SmoothDistance))
        PrintLine(NF, "DefaultLC=" & DefaultLC.ToString)
        PrintLine(NF, "DefaultWC=" & DefaultWC.ToString)
        'PrintLine(NF, "Here_app_id=" & Here_app_id)
        'PrintLine(NF, "Here_app_code=" & Here_app_code)
        PrintLine(NF, "GoogleMapsAPI=" & GoogleMapsAPI)
        PrintLine(NF, "MakeSlopeXY=" & MakeSlopeXY.tostring)

        PrintLine(NF)
        PrintLine(NF, "[Grids]")
        PrintLine(NF, "GridColor=" & ArgbFromColor(GridColor))
        PrintLine(NF, "GridLODColor=" & ArgbFromColor(GridLODColor))
        PrintLine(NF, "GridWidth=" & Str(GridWidth))
        PrintLine(NF, "ZoomOnQMID=" & ZoomOnQMID.ToString)
        PrintLine(NF, "CenterOnMouseWheel=" & CenterOnMouseWheel.ToString)

        PrintLine(NF)
        PrintLine(NF, "[Aircraft]")
        PrintLine(NF, "ShowAircraftPeriod=" & ShowAircraftPeriod.ToString)
        PrintLine(NF, "AircraftAltitudeOffset=" & Str(AircraftAltitudeOffset))
        PrintLine(NF, "ExtraExtrusionAltitude=" & ExtraExtrusionAltitude)

        PrintLine(NF)
        PrintLine(NF, "[Objects]")
        PrintLine(NF, "Rwy12Path=" & Rwy12Path)
        PrintLine(NF, "MacroAPIPath=" & MacroAPIPath)
        PrintLine(NF, "MacroASDPath=" & MacroASDPath)
        PrintLine(NF, "LibObjectsPath=" & LibObjectsPath)

        PrintLine(NF)
        PrintLine(NF, "[Lines]")
        PrintLine(NF, "DefaultLineAltitude=" & Str(DefaultLineAltitude))
        PrintLine(NF, "DefaultLineWidth=" & Str(DefaultLineWidth))
        PrintLine(NF, "LinePenWidth=" & Str(LinePenWidth))
        PrintLine(NF, "SelectedLineColor=" & ArgbFromColor(SelectedLineColor))
        PrintLine(NF, "DefaultLineColor=" & ArgbFromColor(DefaultLineColor))
        PrintLine(NF, "MakeClosedLineFromPoly=" & MakeClosedLineFromPoly.ToString)

        PrintLine(NF)
        PrintLine(NF, "[Polys]")
        PrintLine(NF, "DefaultPolyAltitude=" & Str(DefaultPolyAltitude))
        PrintLine(NF, "PolyPenWidth=" & Str(PolyPenWidth))
        PrintLine(NF, "PolyFILL=" & PolyFILL.ToString)
        PrintLine(NF, "DefaultPolyColor=" & ArgbFromColor(DefaultPolyColor))
        PrintLine(NF, "PolyColorBorder=" & ArgbFromColor(PolyColorBorder))

        PrintLine(NF)
        PrintLine(NF, "[Shapes]")
        PrintLine(NF, "ShapeLineGuid=" & ShapeLineGuid)
        PrintLine(NF, "ShapeLineAltitude=" & Str(ShapeLineAltitude))
        PrintLine(NF, "ShapeLineWidth=" & Str(ShapeLineWidth))
        PrintLine(NF, "ShapeLineColor=" & ArgbFromColor(ShapeLineColor))
        PrintLine(NF, "ShapePolyGuid=" & ShapePolyGuid)
        PrintLine(NF, "ShapePolyAltitude=" & Str(ShapePolyAltitude))
        PrintLine(NF, "ShapePolyColor=" & ArgbFromColor(ShapePolyColor))
        PrintLine(NF, "AddToCells=" & AddToCells.ToString)

        PrintLine(NF)
        PrintLine(NF, "[Tiles]")
        PrintLine(NF, "ActiveTileFolder=" & ActiveTileFolder)
        PrintLine(NF, "ReprojectMercatorTiles=" & ReprojectMercatorTiles.ToString)
        PrintLine(NF, "SummerVariations=" & SummerVariations)
        PrintLine(NF, "SpringVariations=" & SpringVariations)
        PrintLine(NF, "FallVariations=" & FallVariations)
        PrintLine(NF, "WinterVariations=" & WinterVariations)
        PrintLine(NF, "HardWinterVariations=" & HardWinterVariations)
        PrintLine(NF, "CompressionQuality=" & CompressionQuality.ToString)

        PrintLine(NF)
        PrintLine(NF, "[BLN]")
        If BLNSeparator = Chr(9) Then
            PrintLine(NF, "BLNSeparator=tab")
        Else
            PrintLine(NF, "BLNSeparator=" & BLNSeparator)
        End If
        PrintLine(NF, "BLNPolyGuid=" & BLNPolyGuid)
        PrintLine(NF, "BLNPolyColor=" & ArgbFromColor(BLNPolyColor))
        PrintLine(NF, "BLNLineGuid=" & BLNLineGuid)
        PrintLine(NF, "BLNLineColor=" & ArgbFromColor(BLNLineColor))
        PrintLine(NF, "BLNIsPolyAlt=" & BLNIsPolyAlt.ToString)
        PrintLine(NF, "BLNIsLineAlt=" & BLNIsLineAlt.ToString)
        PrintLine(NF, "BLNStartWidth=" & Str(BLNStartWidth))
        PrintLine(NF, "BLNEndWidth=" & Str(BLNEndWidth))
        PrintLine(NF, "BLNLineFromPoly=" & BLNLineFromPoly.ToString)
        PrintLine(NF, "BLNExportAltitudes=" & BLNExportAltitudes.ToString)

        PrintLine(NF)
        FileClose(NF)

    End Sub

    Sub SetRecentFiles()

        RecentFiles(1) = GetString("Recent Files", "RecentFile1", "")
        If RecentFiles(1) = "" Then Exit Sub
        FrmStart.RecentFileSeparatorMenuItem.Visible = True
        FrmStart.RecentFile1MenuItem.Visible = True
        FrmStart.RecentFile1MenuItem.Text = TrimFile(RecentFiles(1), 35)

        RecentFiles(2) = GetString("Recent Files", "RecentFile2", "")
        If RecentFiles(2) = "" Then Exit Sub
        FrmStart.RecentFile2MenuItem.Visible = True
        FrmStart.RecentFile2MenuItem.Text = TrimFile(RecentFiles(2), 35)

        RecentFiles(3) = GetString("Recent Files", "RecentFile3", "")
        If RecentFiles(3) = "" Then Exit Sub
        FrmStart.RecentFile3MenuItem.Visible = True
        FrmStart.RecentFile3MenuItem.Text = TrimFile(RecentFiles(3), 35)

        RecentFiles(4) = GetString("Recent Files", "RecentFile4", "")
        If RecentFiles(4) = "" Then Exit Sub
        FrmStart.RecentFile4MenuItem.Visible = True
        FrmStart.RecentFile4MenuItem.Text = TrimFile(RecentFiles(4), 35)

    End Sub


    Private Function GetString(ByVal A As String, ByVal B As String, ByVal C As String) As String

        Dim D, B1 As String

        B1 = B
        D = Trim(ReadIniValue(AppIni, A, B1))

        If D = "" Then
            WriteIniValue(AppIni, A, B, C)
            GetString = C
        Else
            GetString = D
        End If

    End Function


    Friend Sub WriteRecentFiles(ByVal OpenFileName As String)

        Dim I As Integer
        Dim strFile, KEY As String

        ' Copy RecentFile1 to RecentFile2, and so on.
        For I = 3 To 1 Step -1
            KEY = "RecentFile" & I
            strFile = ReadIniValue(AppIni, "Recent Files", KEY)
            If strFile <> "" Then
                KEY = "RecentFile" & (I + 1)
                WriteIniValue(AppIni, "Recent Files", KEY, strFile)
            End If
        Next I
        ' Write the open file to first recent file.
        WriteIniValue(AppIni, "Recent Files", "RecentFile1", OpenFileName)

    End Sub


    Friend Function ReadIniValue(ByVal INIpath As String, ByVal KEY As String, ByRef Variable As String) As String

        Dim NF As Integer
        Dim Temp As String
        Dim LcaseTemp As String
        Dim ReadyToRead As Boolean

AssignVariables:
        NF = FreeFile()
        ReadIniValue = ""
        KEY = "[" & LCase(KEY) & "]"
        Variable = LCase(Variable)

EnsureFileExists:
        FileOpen(NF, INIpath, OpenMode.Binary)
        FileClose(NF)
        SetAttr(INIpath, FileAttribute.Archive)

LoadFile:
        FileOpen(NF, INIpath, OpenMode.Input)
        While Not EOF(NF)
            Temp = LineInput(NF)
            LcaseTemp = LCase(Temp)
            If InStr(LcaseTemp, "[") <> 0 Then ReadyToRead = False
            If LcaseTemp = KEY Then ReadyToRead = True
            If InStr(LcaseTemp, "[") = 0 And ReadyToRead Then
                If InStr(LcaseTemp, Variable & "=") = 1 Then
                    ReadIniValue = Mid(Temp, 1 + Len(Variable & "="))
                    FileClose(NF)
                    Exit Function
                End If
            End If
        End While
        FileClose(NF)
    End Function


    Friend Sub WriteIniValue(ByRef INIpath As String, ByRef PutKey As String, ByRef PutVariable As String, ByRef PutValue As String)

        Dim Temp As String
        Dim LcaseTemp As String
        Dim ReadKey As String
        Dim ReadVariable As String
        Dim LOKEY As Integer
        Dim HIKEY As Integer
        Dim KEYLEN As Integer
        Dim VAR As Integer
        Dim VARENDOFLINE As Integer
        Dim NF As Integer

AssignVariables:
        NF = FreeFile()
        ReadKey = vbCrLf & "[" & LCase(PutKey) & "]" & Chr(13)
        KEYLEN = Len(ReadKey)
        ReadVariable = Chr(10) & LCase(PutVariable) & "="

EnsureFileExists:
        FileOpen(NF, INIpath, OpenMode.Binary)
        FileClose(NF)
        SetAttr(INIpath, FileAttribute.Archive)

LoadFile:
        FileOpen(NF, INIpath, OpenMode.Input)
        Temp = InputString(NF, CInt(LOF(NF)))
        Temp = vbCrLf & Temp & "[]"
        FileClose(NF)
        LcaseTemp = LCase(Temp)

LogicMenu:
        LOKEY = InStr(LcaseTemp, ReadKey)
        If LOKEY = 0 Then GoTo AddKey
        HIKEY = InStr(LOKEY + KEYLEN, LcaseTemp, "[")
        VAR = InStr(LOKEY, LcaseTemp, ReadVariable)
        If VAR > HIKEY Or VAR < LOKEY Then GoTo AddVariable
        GoTo RenewVariable

AddKey:
        Temp = Left(Temp, Len(Temp) - 2)
        Temp = Temp & vbCrLf & vbCrLf & "[" & PutKey & "]" & vbCrLf & PutVariable & "=" & PutValue
        GoTo TrimFinalString

AddVariable:
        Temp = Left(Temp, Len(Temp) - 2)
        Temp = Left(Temp, LOKEY + KEYLEN) & PutVariable & "=" & PutValue & vbCrLf & Mid(Temp, LOKEY + KEYLEN + 1)
        GoTo TrimFinalString

RenewVariable:
        Temp = Left(Temp, Len(Temp) - 2)
        VARENDOFLINE = InStr(VAR, Temp, Chr(13))
        Temp = Left(Temp, VAR) & PutVariable & "=" & PutValue & Mid(Temp, VARENDOFLINE)
        GoTo TrimFinalString

TrimFinalString:
        Temp = Mid(Temp, 2)
        Do Until InStr(Temp, vbCrLf & vbCrLf & vbCrLf) = 0
            Temp = Replace(Temp, vbCrLf & vbCrLf & vbCrLf, vbCrLf & vbCrLf)
        Loop

        Do Until Right(Temp, 1) > Chr(13)
            Temp = Left(Temp, Len(Temp) - 1)
        Loop

        Do Until Left(Temp, 1) > Chr(13)
            Temp = Mid(Temp, 2)
        Loop

OutputAmendedINIFile:
        FileOpen(NF, INIpath, OpenMode.Output)
        PrintLine(NF, Temp)
        FileClose(NF)

    End Sub

    Private Function TrimFile(ByVal str_file As String, ByVal N As Integer) As String

        Dim K1, K, K2 As Integer

        TrimFile = str_file

        K = Len(str_file)
        If K <= N Then Exit Function

        K1 = InStr(4, str_file, "\")
        K2 = InStrRev(str_file, "\")

        If K1 + K - K2 > N Then
            TrimFile = Mid(str_file, 1, 3) & ".." & Mid(str_file, K2)
            Exit Function
        End If

        TrimFile = Mid(str_file, 1, K1) & ".." & Mid(str_file, K2)

    End Function

    Function FileNameToOpen(ByVal s_filter As String, ByVal s_title As String, ByVal s_dir As String) As String


        Dim TheDir As String

        On Error Resume Next

        TheDir = ""

        FrmStart.OpenFileDialog1.Filter = s_filter & "|All Files|*.*"

        If s_dir = "SBX" Then TheDir = SBXDir
        If s_dir = "SBP" Then TheDir = SBPDir
        If s_dir = "SHP" Then TheDir = SHPDir
        If s_dir = "SUR" Then TheDir = SURDir
        If s_dir = "TEX" Then TheDir = TEXDir
        If s_dir = "OBJ" Then TheDir = OBJDir
        If s_dir = "RAW" Then TheDir = RAWDir
        If s_dir = "LIB" Then TheDir = LIBDir
        If s_dir = "BMP" Then TheDir = BMPDir
        If s_dir = "MDL" Then TheDir = AppPath & "\Mdls"

        'MsgBox A$ & "  " & B$ & "  " & TheDir

        FrmStart.OpenFileDialog1.InitialDirectory = TheDir
        FrmStart.OpenFileDialog1.FileName = ""
        FrmStart.OpenFileDialog1.Title = s_title

        FileNameToOpen = ""

        Dim cDir As String

        If FrmStart.OpenFileDialog1.ShowDialog = DialogResult.OK Then 'user did not press cancel
            FileNameToOpen = FrmStart.OpenFileDialog1.FileName
            FrmStart.OpenFileDialog1.Filter = ""

            cDir = Path.GetDirectoryName(FileNameToOpen)

            If s_dir = "SBX" Then SBXDir = cDir
            If s_dir = "SBP" Then SBPDir = cDir
            If s_dir = "SHP" Then SHPDir = cDir
            If s_dir = "SUR" Then SURDir = cDir
            If s_dir = "TEX" Then TEXDir = cDir
            If s_dir = "OBJ" Then OBJDir = cDir
            If s_dir = "RAW" Then RAWDir = cDir
            If s_dir = "LIB" Then LIBDir = cDir
            If s_dir = "BMP" Then BMPDir = cDir

            WriteSettings()

        End If

        FrmStart.OpenFileDialog1.Dispose()

    End Function


    Function FileNameToSave(ByVal s_filter As String, ByVal s_title As String, ByVal s_dir As String) As String

        Dim TheDir, TheFile As String
        Dim N As Integer
        Dim A As String

        On Error Resume Next

        TheFile = Path.GetFileNameWithoutExtension(WorkFile)
        TheDir = ""

        If s_dir = "SBX" Then TheDir = SBXDir
        If s_dir = "SBP" Then TheDir = SBPDir
        If s_dir = "SHP" Then TheDir = SHPDir
        If s_dir = "SUR" Then TheDir = SURDir
        If s_dir = "TEX" Then TheDir = TEXDir
        If s_dir = "OBJ" Then TheDir = OBJDir
        If s_dir = "RAW" Then TheDir = RAWDir
        If s_dir = "LIB" Then TheDir = LIBDir
        If s_dir = "BMP" Then TheDir = BMPDir

        FrmStart.SaveFileDialog1.Filter = s_filter + "|All Files|*.*"
        FrmStart.SaveFileDialog1.InitialDirectory = TheDir
        FrmStart.SaveFileDialog1.FileName = TheFile  ' or A$ ?
        FrmStart.SaveFileDialog1.Title = s_title

        FileNameToSave = ""

        Dim cDir As String

        If FrmStart.SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then      'User cancelled dialog

            FileNameToSave = FrmStart.SaveFileDialog1.FileName
            cDir = Path.GetDirectoryName(FileNameToSave)

            If s_dir = "SBX" Then SBXDir = cDir
            If s_dir = "SBP" Then SBPDir = cDir
            If s_dir = "SHP" Then SHPDir = cDir
            If s_dir = "SUR" Then SURDir = cDir
            If s_dir = "TEX" Then TEXDir = cDir
            If s_dir = "OBJ" Then OBJDir = cDir
            If s_dir = "RAW" Then RAWDir = cDir
            If s_dir = "LIB" Then LIBDir = cDir
            If s_dir = "BMP" Then BMPDir = cDir

            WriteSettings()

        End If

        frmStart.SaveFileDialog1.Dispose()

    End Function


    Private Sub UpdateFileMenu(ByVal FileName As String)

        Dim retval As Boolean

        ' check if FileName is already on the list.
        retval = OnRecentFilesList(FileName)
        If Not retval Then
            WriteRecentFiles(FileName)
            SetRecentFiles()
        End If

    End Sub

    Private Function OnRecentFilesList(ByVal FileName As String) As Boolean

        Dim I As Integer

        OnRecentFilesList = False
        For I = 1 To 4
            If RecentFiles(I) = FileName Then
                OnRecentFilesList = True
                Exit Function
            End If
        Next I

    End Function

    Friend Sub SetFileBackUp(ByVal filename As String)

        If BackUpON = False Then Exit Sub

        Dim ext As String = UCase(Path.GetExtension(filename))

        BackUpFileName = Path.GetDirectoryName(filename)
        BackUpFileName = BackUpFileName & "\" & Path.GetFileNameWithoutExtension(filename) & "_BAK_"
        If ext = ".SBP" Then File.Copy(filename, BackUpFileName & "00.SBP", True)
        frmStart.Timer3.Interval = BackUpSeconds * 1000
        frmStart.Timer3.Enabled = True
        BackUpFileCounter = 0

    End Sub

    Friend Sub OpenFile(ByVal filename As String)

        Dim Lixo As Integer

        If Not My.Computer.FileSystem.FileExists(filename) Then
            MsgBox("This file was not found!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        On Error GoTo erro

        Dim oFile As New FileStream(filename, FileMode.Open)
        Dim BFormatter As New BinaryFormatter With {
            .Binder = New SbuilderBinder
        }

        Dim Version As String

        Version = BFormatter.Deserialize(oFile)

        SetFileBackUp(filename)

        ProjectName = BFormatter.Deserialize(oFile)
        BGLProjectFolder = BFormatter.Deserialize(oFile)
        CheckFolders()
        Dim readZoom As Double = BFormatter.Deserialize(oFile)
        Zoom = CType(readZoom, Integer)
        LatDispCenter = BFormatter.Deserialize(oFile)
        LonDispCenter = BFormatter.Deserialize(oFile)

        QMIDLevel = BFormatter.Deserialize(oFile)

        NoOfMaps = BFormatter.Deserialize(oFile)
        NoOfLands = BFormatter.Deserialize(oFile)
        Lixo = BFormatter.Deserialize(oFile)
        NoOfLines = BFormatter.Deserialize(oFile)
        NoOfPolys = BFormatter.Deserialize(oFile)
        NoOfWaters = BFormatter.Deserialize(oFile)
        NoOfObjects = BFormatter.Deserialize(oFile)
        NoOfExcludes = BFormatter.Deserialize(oFile)
        Lixo = BFormatter.Deserialize(oFile)


        NoOfLWCIs = 0
        If Not (Version = "SB301") Then NoOfLWCIs = BFormatter.Deserialize(oFile)
        If NoOfMaps > 0 Then Maps = BFormatter.Deserialize(oFile)
        If NoOfLands > 0 Then
            NoOfLLXYs = BFormatter.Deserialize(oFile)
            LL_XY = BFormatter.Deserialize(oFile)
            LLands = BFormatter.Deserialize(oFile)
        End If

        If NoOfLines > 0 Then Lines = BFormatter.Deserialize(oFile)
        If NoOfPolys > 0 Then Polys = BFormatter.Deserialize(oFile)

        If NoOfWaters > 0 Then
            NoOfWWXYs = BFormatter.Deserialize(oFile)
            WW_XY = BFormatter.Deserialize(oFile)
            WWaters = BFormatter.Deserialize(oFile)
        End If
        If NoOfObjects > 0 Then Objects = BFormatter.Deserialize(oFile)
        If NoOfExcludes > 0 Then Excludes = BFormatter.Deserialize(oFile)

        If NoOfLWCIs > 0 Then LWCIs = BFormatter.Deserialize(oFile)

        oFile.Close()

        UpdateFileMenu(filename)

        Exit Sub
erro:
        oFile.Close()
        MsgBox("This file is not a SBuilderX 3.XX project!", MsgBoxStyle.Exclamation)
        NoOfMaps = 0
        NoOfLands = 0
        NoOfLines = 0
        NoOfPolys = 0
        NoOfWaters = 0
        NoOfObjects = 0
        NoOfExcludes = 0
        NoOfLWCIs = 0

    End Sub


    Friend Sub SaveFile(ByVal filename As String)

        Dim sFile As New FileStream(filename, FileMode.Create)
        Dim BFormatter As New BinaryFormatter

        BFormatter.Serialize(sFile, "SB314")
        BFormatter.Serialize(sFile, ProjectName)
        BFormatter.Serialize(sFile, BGLProjectFolder)
        Dim savedZoom As Double = CType(Zoom, Double)
        BFormatter.Serialize(sFile, savedZoom)
        BFormatter.Serialize(sFile, LatDispCenter)
        BFormatter.Serialize(sFile, LonDispCenter)

        BFormatter.Serialize(sFile, QMIDLevel)

        BFormatter.Serialize(sFile, NoOfMaps)
        BFormatter.Serialize(sFile, NoOfLands)
        BFormatter.Serialize(sFile, 0)
        BFormatter.Serialize(sFile, NoOfLines)
        BFormatter.Serialize(sFile, NoOfPolys)
        BFormatter.Serialize(sFile, NoOfWaters)
        BFormatter.Serialize(sFile, NoOfObjects)
        BFormatter.Serialize(sFile, NoOfExcludes)
        BFormatter.Serialize(sFile, 0)
        BFormatter.Serialize(sFile, NoOfLWCIs) ' inserted in SB302


        If NoOfMaps > 0 Then BFormatter.Serialize(sFile, Maps)
        If NoOfLands > 0 Then
            BFormatter.Serialize(sFile, NoOfLLXYs)
            BFormatter.Serialize(sFile, LL_XY)
            BFormatter.Serialize(sFile, LLands)
        End If

        If NoOfLines > 0 Then BFormatter.Serialize(sFile, Lines)
        If NoOfPolys > 0 Then BFormatter.Serialize(sFile, Polys)
        If NoOfWaters > 0 Then
            BFormatter.Serialize(sFile, NoOfWWXYs)
            BFormatter.Serialize(sFile, WW_XY)
            BFormatter.Serialize(sFile, WWaters)
        End If
        If NoOfObjects > 0 Then BFormatter.Serialize(sFile, Objects)
        If NoOfExcludes > 0 Then BFormatter.Serialize(sFile, Excludes)

        If NoOfLWCIs > 0 Then BFormatter.Serialize(sFile, LWCIs) ' in sb302

        sFile.Close()


    End Sub

    Friend Sub ExportSBX(ByVal FileName As String)

        Dim M, N, FN As Integer
        Dim KEY As String

        frmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor

        FN = FreeFile()

        FileOpen(FN, FileName, OpenMode.Output)

        PrintLine(FN, "[Main]")
        PrintLine(FN, "CopyRight=" & "PTSIM SB314")

        PrintLine(FN, "Name=" & ProjectName)

        PrintLine(FN, "NoOfMaps=" & NoOfMaps)
        PrintLine(FN, "NoOfLines=" & NoOfLines)
        PrintLine(FN, "NoOfPolys=" & NoOfPolys)

        PrintLine(FN, "NoOfLC_LOD5s=" & NoOfLLXYs)
        PrintLine(FN, "NoOfWC_LOD5s=" & NoOfWWXYs)

        PrintLine(FN, "NoOfObjects=" & NoOfObjects)
        PrintLine(FN, "NoOfExcludes=" & NoOfExcludes)
        PrintLine(FN, "NoOfLWCIs=" & NoOfLWCIs)

        PrintLine(FN, "BGLProjectFolder=" & BGLProjectFolder)

        PrintLine(FN, "LatDispCenter=" & Str(LatDispCenter))
        PrintLine(FN, "LonDispCenter=" & Str(LonDispCenter))

        PrintLine(FN, "Zoom=" & Zoom)

        For N = 1 To NoOfMaps

            PrintLine(FN)
            KEY = "[Map." & Trim(Str(N)) & "]"
            PrintLine(FN, KEY)
            PrintLine(FN, "Name=" & Maps(N).Name)
            PrintLine(FN, "BMPSu=" & Maps(N).BMPSu)
            PrintLine(FN, "BMPSp=" & Maps(N).BMPSp)
            PrintLine(FN, "BMPFa=" & Maps(N).BMPFa)
            PrintLine(FN, "BMPWi=" & Maps(N).BMPWi)
            PrintLine(FN, "BMPHw=" & Maps(N).BMPHw)
            PrintLine(FN, "BMPLm=" & Maps(N).BMPLm)

            PrintLine(FN, "Cols=" & Str(Maps(N).COLS))
            PrintLine(FN, "Rows=" & Str(Maps(N).ROWS))
            PrintLine(FN, "NLat=" & Str(Maps(N).NLAT))
            PrintLine(FN, "SLat=" & Str(Maps(N).SLAT))
            PrintLine(FN, "ELon=" & Str(Maps(N).ELON))
            PrintLine(FN, "WLon=" & Str(Maps(N).WLON))

        Next N

        For N = 1 To NoOfLines
            PrintLine(FN)
            KEY = "[Line." & Trim(Str(N)) & "]"
            PrintLine(FN, KEY)
            PrintLine(FN, "Name=" & Lines(N).Name)
            PrintLine(FN, "Type=" & Lines(N).Type)
            PrintLine(FN, "Guid=" & Lines(N).Guid)
            PrintLine(FN, "Color=" & ArgbFromColor(Lines(N).Color))
            PrintLine(FN, "NoOfPoints=" & CStr(Lines(N).NoOfPoints))
            For M = 1 To Lines(N).NoOfPoints
                PrintLine(FN, "Lat" & Trim(Str(M)) & "=" & Str(Lines(N).GLPoints(M).lat))
                PrintLine(FN, "Lon" & Trim(Str(M)) & "=" & Str(Lines(N).GLPoints(M).lon))
                PrintLine(FN, "Alt" & M & "=" & Str(Lines(N).GLPoints(M).alt))
                PrintLine(FN, "Wid" & M & "=" & Str(Lines(N).GLPoints(M).wid))
            Next M
        Next N

        For N = 1 To NoOfPolys
            PrintLine(FN)
            KEY = "[Poly." & Trim(Str(N)) & "]"
            PrintLine(FN, KEY)
            PrintLine(FN, "Name=" & Polys(N).Name)
            PrintLine(FN, "Type=" & Polys(N).Type)
            PrintLine(FN, "Guid=" & Polys(N).Guid)
            PrintLine(FN, "Color=" & ArgbFromColor(Polys(N).Color))
            PrintLine(FN, "NoOfChilds=" & CStr(Polys(N).NoOfChilds))
            For M = 1 To Polys(N).NoOfChilds
                PrintLine(FN, "Child" & Trim(Str(M)) & "=" & Str(Polys(N).Childs(M)))
            Next M
            PrintLine(FN, "NoOfPoints=" & CStr(Polys(N).NoOfPoints))
            For M = 1 To Polys(N).NoOfPoints
                PrintLine(FN, "Lat" & Trim(Str(M)) & "=" & Str(Polys(N).GPoints(M).lat))
                PrintLine(FN, "Lon" & Trim(Str(M)) & "=" & Str(Polys(N).GPoints(M).lon))
                PrintLine(FN, "Alt" & Trim(Str(M)) & "=" & Str(Polys(N).GPoints(M).alt))
            Next M
        Next N

        Dim J, K, C, R, P As Integer
        Dim A As String

        N = 1
        If NoOfLLXYs > 0 Then
            For K = 0 To 63
                For J = 0 To 95
                    If LL_XY(J, K).NoOfLWs > 0 Then
                        PrintLine(FN)
                        A = "[LC_LOD5." & N & "]"
                        PrintLine(FN, A)
                        A = "U=" & Format(J, "00")
                        PrintLine(FN, A)
                        A = "V=" & Format(K, "00")
                        PrintLine(FN, A)
                        PrintLine(FN, "NoOfLands=" & LL_XY(J, K).NoOfLWs)
                        P = LL_XY(J, K).Pointer
                        M = LL_XY(J, K).NoOfLWs
                        For R = 0 To 256
                            For C = 0 To 256
                                If LLands(C, R, P) > 0 Then
                                    If Not LLands(C, R, P) = 254 Then
                                        PrintLine(FN, "C" & Format(C, "000") & "R" & Format(R, "000=") & LC(LLands(C, R, P)).Index)
                                    End If
                                End If
                            Next C
                        Next R
                        N = N + 1
                    End If
                Next J
            Next K
        End If

        N = 1
        If NoOfWWXYs > 0 Then
            For K = 0 To 63
                For J = 0 To 95
                    If WW_XY(J, K).NoOfLWs > 0 Then
                        PrintLine(FN)
                        A = "[WC_LOD5." & N & "]"
                        PrintLine(FN, A)
                        A = "U=" & Format(J, "00")
                        PrintLine(FN, A)
                        A = "V=" & Format(K, "00")
                        PrintLine(FN, A)
                        PrintLine(FN, "NoOfWaters=" & WW_XY(J, K).NoOfLWs)
                        P = WW_XY(J, K).Pointer
                        M = WW_XY(J, K).NoOfLWs
                        For R = 0 To 256
                            For C = 0 To 256
                                If WWaters(C, R, P) > 0 Then
                                    If Not WWaters(C, R, P) = 254 Then
                                        PrintLine(FN, "C" & Format(C, "000") & "R" & Format(R, "000=") & WC(WWaters(C, R, P)).Index)
                                    End If
                                End If
                            Next C
                        Next R
                        N = N + 1
                    End If
                Next J
            Next K
        End If

        For N = 1 To NoOfExcludes
            PrintLine(FN)
            KEY = "[Exclude." & Trim(Str(N)) & "]"
            PrintLine(FN, KEY)
            ' before November 2017 these were CStr()
            PrintLine(FN, "Flag=" & Str(Excludes(N).Flag))
            PrintLine(FN, "NLat=" & Str(Excludes(N).NLAT))
            PrintLine(FN, "SLat=" & Str(Excludes(N).SLAT))
            PrintLine(FN, "ELon=" & Str(Excludes(N).ELON))
            PrintLine(FN, "WLon=" & Str(Excludes(N).WLON))
        Next N

        For N = 1 To NoOfObjects
            PrintLine(FN)
            KEY = "[Object." & Trim(Str(N)) & "]"
            PrintLine(FN, KEY)

            PrintLine(FN, "Type=" & CStr(Objects(N).Type))
            PrintLine(FN, "Description=" & Objects(N).Description)
            PrintLine(FN, "Width=" & Str(Objects(N).Width))
            PrintLine(FN, "Length=" & Str(Objects(N).Length))
            PrintLine(FN, "Heading=" & Str(Objects(N).Heading))
            PrintLine(FN, "Pitch=" & Str(Objects(N).Pitch))
            PrintLine(FN, "Bank=" & Str(Objects(N).Bank))
            PrintLine(FN, "BiasX=" & Str(Objects(N).BiasX))
            PrintLine(FN, "BiasY=" & Str(Objects(N).BiasY))
            PrintLine(FN, "BiasZ=" & Str(Objects(N).BiasZ))
            PrintLine(FN, "Lat=" & Str(Objects(N).lat))
            PrintLine(FN, "Lon=" & Str(Objects(N).lon))
            PrintLine(FN, "Altitude=" & Str(Objects(N).Altitude))
            PrintLine(FN, "AGL=" & Str(Objects(N).AGL))
            PrintLine(FN, "Complexity=" & Str(Objects(N).Complexity))
        Next N

        For N = 1 To NoOfLWCIs
            PrintLine(FN)
            KEY = "[LWCI." & Trim(Str(N)) & "]"
            PrintLine(FN, KEY)
            PrintLine(FN, "Text=" & LWCIs(N).Text)
            PrintLine(FN, "Color=" & ArgbFromColor(LWCIs(N).Color))
        Next N

        FileClose(FN)

        frmStart.SetMouseIcon()

    End Sub

    Friend Sub ImportSBX(ByVal Filename As String)

        Dim M, N As Integer
        Dim I, L As Integer
        Dim J, K, C, R, P As Integer
        Dim KEY As String
        Dim EL, SL, NL, WL, X As Double
        Dim A, Version As String

        KEY = ReadIniValue(Filename, "Main", "CopyRight")
        KEY = Left(KEY, 11)

        If KEY = "PTSIM" Then
            ImportSBX205(Filename, "")
            Exit Sub
        End If

        If KEY = "PTSIM SB205" Then
            ImportSBX205(Filename, "SB205")
            Exit Sub
        End If

        If KEY = "PTSIM SB301" Then
            Version = "SB301"
        ElseIf KEY = "PTSIM SB302" Then
            Version = "SB302"
        ElseIf KEY = "PTSIM SB303" Then
            Version = "SB303"
        ElseIf KEY = "PTSIM SB310" Then
            Version = "SB310"
        ElseIf KEY = "PTSIM SB313" Then
            Version = "SB313"
        ElseIf KEY = "PTSIM SB314" Then
            Version = "SB314"
        Else
            MsgBox("Not a Valid SBX SBuilderX File!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        SetFileBackUp(Filename)

        frmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor

        ProjectName = ReadIniValue(Filename, "Main", "Name")
        NoOfMaps = ReadIniInteger(Filename, "Main", "NoOfMaps")
        NoOfLines = ReadIniInteger(Filename, "Main", "NoOfLines")
        NoOfPolys = ReadIniInteger(Filename, "Main", "NoOfPolys")

        NoOfLLXYs = 0
        NoOfWWXYs = 0
        If Version = "SB314" Then
            NoOfLLXYs = ReadIniInteger(Filename, "Main", "NoOfLC_LOD5s")
            NoOfWWXYs = ReadIniInteger(Filename, "Main", "NoOfWC_LOD5s")
        End If

        NoOfObjects = ReadIniInteger(Filename, "Main", "NoOfObjects")
        NoOfExcludes = ReadIniInteger(Filename, "Main", "NoOfExcludes")
        NoOfLWCIs = 0
        If Not Version = "SB301" Then NoOfLWCIs = ReadIniInteger(Filename, "Main", "NoOfLWCIs")

        BGLProjectFolder = ReadIniValue(Filename, "Main", "BGLProjectFolder")
        CheckFolders()

        LatDispCenter = ReadIniDouble(Filename, "Main", "LatDispCenter")
        LonDispCenter = ReadIniDouble(Filename, "Main", "LonDispCenter")

        Zoom = ReadIniDouble(Filename, "Main", "Zoom")

        If NoOfMaps > 0 Then ReDim Maps(NoOfMaps)
        If NoOfLines > 0 Then ReDim Lines(NoOfLines)
        If NoOfPolys > 0 Then ReDim Polys(NoOfPolys)
        If NoOfObjects > 0 Then ReDim Objects(NoOfObjects)
        If NoOfExcludes > 0 Then ReDim Excludes(NoOfExcludes)
        If NoOfLWCIs > 0 Then ReDim LWCIs(NoOfLWCIs)

        For N = 1 To NoOfMaps
            KEY = "Map." & Trim(Str(N))

            Maps(N).Name = ReadIniValue(Filename, KEY, "Name")
            Maps(N).BMPSu = ReadIniValue(Filename, KEY, "BMPSu")
            Maps(N).BMPSp = ReadIniValue(Filename, KEY, "BMPSp")
            Maps(N).BMPFa = ReadIniValue(Filename, KEY, "BMPFa")
            Maps(N).BMPWi = ReadIniValue(Filename, KEY, "BMPWi")
            Maps(N).BMPHw = ReadIniValue(Filename, KEY, "BMPHw")
            Maps(N).BMPLm = ReadIniValue(Filename, KEY, "BMPLm")

            Maps(N).COLS = ReadIniInteger(Filename, KEY, "Cols")
            Maps(N).ROWS = ReadIniInteger(Filename, KEY, "Rows")
            Maps(N).NLAT = ReadIniDouble(Filename, KEY, "NLat")
            Maps(N).SLAT = ReadIniDouble(Filename, KEY, "SLat")
            Maps(N).ELON = ReadIniDouble(Filename, KEY, "ELon")
            Maps(N).WLON = ReadIniDouble(Filename, KEY, "WLon")

        Next N

        FileOpen(5, Filename, OpenMode.Input)

        For N = 1 To NoOfLines
            KEY = "[Line." & Trim(Str(N)) & "]"
            GoToThisKey((KEY))
            Lines(N).Name = Mid(LineInput(5), 6)
            Lines(N).Type = Mid(LineInput(5), 6)
            Lines(N).Guid = Mid(LineInput(5), 6)
            If Version = "SB301" Then
                Lines(N).Color = Color.FromArgb(CInt(Mid(LineInput(5), 7)))
            Else
                Lines(N).Color = ColorFromArgb(Mid(LineInput(5), 7))
            End If
            Lines(N).NoOfPoints = CInt(Mid(LineInput(5), 12))
            If Lines(N).Name = "" Then Lines(N).Name = CStr(Lines(N).NoOfPoints) & "_Pts_Imported_Line"
            ReDim Lines(N).GLPoints(Lines(N).NoOfPoints)
            J = 6
            NL = -90 : SL = 90 : EL = -180 : WL = 180
            For M = 1 To Lines(N).NoOfPoints
                If M > 9 Then J = 7
                If M > 99 Then J = 8
                If M > 999 Then J = 9
                If M > 9999 Then J = 10
                If M > 99999 Then J = 11
                If M > 999999 Then J = 12
                X = Val(Mid(LineInput(5), J))
                Lines(N).GLPoints(M).lat = X
                If X < SL Then SL = X
                If X > NL Then NL = X
                X = Val(Mid(LineInput(5), J))
                Lines(N).GLPoints(M).lon = X
                If X > EL Then EL = X
                If X < WL Then WL = X
                Lines(N).GLPoints(M).alt = Val(Mid(LineInput(5), J))
                Lines(N).GLPoints(M).wid = Val(Mid(LineInput(5), J))
            Next M
            Lines(N).ELON = EL
            Lines(N).WLON = WL
            Lines(N).NLAT = NL
            Lines(N).SLAT = SL
        Next N

        For N = 1 To NoOfPolys
            KEY = "[Poly." & Trim(Str(N)) & "]"
            GoToThisKey((KEY))
            Polys(N).Name = Mid(LineInput(5), 6)
            Polys(N).Type = Mid(LineInput(5), 6)
            Polys(N).Guid = Mid(LineInput(5), 6)
            If Version = "SB301" Then
                Polys(N).Color = Color.FromArgb(CInt(Mid(LineInput(5), 7)))
            Else
                Polys(N).Color = ColorFromArgb(Mid(LineInput(5), 7))
            End If
            J = CInt(Mid(LineInput(5), 12))
            Polys(N).NoOfChilds = J
            If J > 0 Then
                ReDim Polys(N).Childs(J)
            Else
                ReDim Polys(N).Childs(0)
            End If
            J = 8
            For M = 1 To Polys(N).NoOfChilds
                If M > 9 Then J = 9
                If M > 99 Then J = 10
                If M > 999 Then J = 11
                If M > 9999 Then J = 12
                If M > 99999 Then J = 13
                If M > 999999 Then J = 14
                Polys(N).Childs(M) = Val(Mid(LineInput(5), J))
            Next
            Polys(N).NoOfPoints = CInt(Mid(LineInput(5), 12))
            If Polys(N).Name = "" Then Polys(N).Name = CStr(Polys(N).NoOfPoints) & "_Pts_Imported_Polygon"
            ReDim Polys(N).GPoints(Polys(N).NoOfPoints)
            J = 6
            NL = -90 : SL = 90 : EL = -180 : WL = 180
            For M = 1 To Polys(N).NoOfPoints
                If M > 9 Then J = 7
                If M > 99 Then J = 8
                If M > 999 Then J = 9
                If M > 9999 Then J = 10
                If M > 99999 Then J = 11
                If M > 999999 Then J = 12
                X = Val(Mid(LineInput(5), J))
                Polys(N).GPoints(M).lat = X
                If X < SL Then SL = X
                If X > NL Then NL = X
                X = Val(Mid(LineInput(5), J))
                Polys(N).GPoints(M).lon = X
                If X > EL Then EL = X
                If X < WL Then WL = X
                Polys(N).GPoints(M).alt = Val(Mid(LineInput(5), J))

            Next M
            Polys(N).ELON = EL
            Polys(N).WLON = WL
            Polys(N).NLAT = NL
            Polys(N).SLAT = SL
        Next N

        If NoOfLLXYs > 0 Then
            N = 0
            ReDim LLands(256, 256, NoOfLLXYs - 1)
            For P = 1 To NoOfLLXYs
                KEY = "[LC_LOD5." & P & "]"
                GoToThisKey((KEY))
                J = Val(Mid(LineInput(5), 3))
                K = Val(Mid(LineInput(5), 3))
                L = CInt(Mid(LineInput(5), 11))
                LL_XY(J, K).Pointer = P - 1
                LL_XY(J, K).NoOfLWs = L
                For M = 1 To L
                    A = LineInput(5)
                    C = Val(Mid(A, 2, 3))
                    R = Val(Mid(A, 6, 3))
                    I = Val(Mid(A, 10))
                    LLands(C, R, P - 1) = ILC(I)
                    N = N + 1
                Next M
            Next P
            NoOfLands = N
        End If

        If NoOfWWXYs > 0 Then
            N = 0
            ReDim WWaters(256, 256, NoOfWWXYs - 1)
            For P = 1 To NoOfWWXYs
                KEY = "[WC_LOD5." & P & "]"
                GoToThisKey((KEY))
                J = Val(Mid(LineInput(5), 3))
                K = Val(Mid(LineInput(5), 3))
                L = CInt(Mid(LineInput(5), 12))
                WW_XY(J, K).Pointer = P - 1
                WW_XY(J, K).NoOfLWs = L
                For M = 1 To L
                    A = LineInput(5)
                    C = Val(Mid(A, 2, 3))
                    R = Val(Mid(A, 6, 3))
                    I = Val(Mid(A, 10))
                    WWaters(C, R, P - 1) = IWC(I)
                    N = N + 1
                Next M
            Next P
            NoOfWaters = N
        End If
 
        For N = 1 To NoOfExcludes
            KEY = "[Exclude." & Trim(Str(N)) & "]"
            GoToThisKey((KEY))
            Excludes(N).Flag = CInt(Mid(LineInput(5), 6))
            Excludes(N).NLAT = Val(Mid(LineInput(5), 6))
            Excludes(N).SLAT = Val(Mid(LineInput(5), 6))
            Excludes(N).ELON = Val(Mid(LineInput(5), 6))
            Excludes(N).WLON = Val(Mid(LineInput(5), 6))
        Next N


        For N = 1 To NoOfObjects
            KEY = "[Object." & Trim(Str(N)) & "]"
            GoToThisKey((KEY))

            Objects(N).Type = CInt(Mid(LineInput(5), 6))
            Objects(N).Description = Mid(LineInput(5), 13)
            Objects(N).Width = CSng(Val(Mid(LineInput(5), 7)))
            Objects(N).Length = CSng(Val(Mid(LineInput(5), 8)))
            Objects(N).Heading = CSng(Val(Mid(LineInput(5), 9)))
            Objects(N).Pitch = CSng(Val(Mid(LineInput(5), 7)))
            Objects(N).Bank = CSng(Val(Mid(LineInput(5), 6)))
            Objects(N).BiasX = CSng(Val(Mid(LineInput(5), 7)))
            Objects(N).BiasY = CSng(Val(Mid(LineInput(5), 7)))
            Objects(N).BiasZ = CSng(Val(Mid(LineInput(5), 7)))
            Objects(N).lat = Val(Mid(LineInput(5), 5))
            Objects(N).lon = Val(Mid(LineInput(5), 5))
            Objects(N).Altitude = CSng(Val(Mid(LineInput(5), 10)))
            Objects(N).AGL = CInt(Mid(LineInput(5), 5))
            Objects(N).Complexity = CInt(Mid(LineInput(5), 12))
            AddLatLonToObjects(N)

        Next N

        For N = 1 To NoOfLWCIs
            KEY = "[LWCI." & Trim(Str(N)) & "]"
            GoToThisKey((KEY))
            LWCIs(N).Text = Mid(LineInput(5), 6)
            LWCIs(N).Color = ColorFromArgb(Mid(LineInput(5), 7))
        Next N

        SetLWCIs()

        FileClose(5)

    End Sub

    Private Sub ImportSBX205(ByVal Filename As String, ByVal Version As String)

        Dim M, N, J As Integer
        Dim KEY As String
        Dim EL, SL, NL, WL, X As Double
        Dim C As Color
        Dim R, G, B As Integer
        Dim SB205 As Boolean = False

        If Version = "SB205" Then SB205 = True

        frmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor

        ProjectName = ReadIniValue(Filename, "Main", "Name")
        NoOfMaps = ReadIniInteger(Filename, "Main", "NoOfMaps")
        NoOfLines = ReadIniInteger(Filename, "Main", "NoOfLines")
        NoOfPolys = ReadIniInteger(Filename, "Main", "NoOfPolys")
        NoOfObjects = ReadIniInteger(Filename, "Main", "NoOfObjects")
        NoOfExcludes = ReadIniInteger(Filename, "Main", "NoOfExcludes")

        BGLProjectFolder = ReadIniValue(Filename, "Main", "BGLProjectFolder")
        CheckFolders()

        LatDispCenter = ReadIniDouble(Filename, "Main", "LatDispCenter")
        LonDispCenter = ReadIniDouble(Filename, "Main", "LonDispCenter")

        Dim myZoom As Double = ReadIniDouble(Filename, "Main", "Zoom")
        Zoom = Math.Log(myZoom, 2) + 9

        If NoOfMaps > 0 Then ReDim Maps(NoOfMaps)

        If NoOfLines > 0 Then
            SetLegacyLines()
            ReDim Lines(NoOfLines)
        End If

        If NoOfPolys > 0 Then
            SetLegacyPolys()
            ReDim Polys(NoOfPolys)
        End If

        If NoOfObjects > 0 Then ReDim Objects(NoOfObjects)
        If NoOfExcludes > 0 Then ReDim Excludes(NoOfExcludes)

        For N = 1 To NoOfMaps
            KEY = "Map." & Trim(Str(N))

            Maps(N).Name = ReadIniValue(Filename, KEY, "Name")
            Maps(N).BMPSu = ReadIniValue(Filename, KEY, "BMPSu")
            Maps(N).BMPSp = ReadIniValue(Filename, KEY, "BMPSp")
            Maps(N).BMPFa = ReadIniValue(Filename, KEY, "BMPFa")
            Maps(N).BMPWi = ReadIniValue(Filename, KEY, "BMPWi")
            Maps(N).BMPHw = ReadIniValue(Filename, KEY, "BMPHw")
            Maps(N).BMPLm = ReadIniValue(Filename, KEY, "BMPLm")

            Maps(N).COLS = ReadIniInteger(Filename, KEY, "COLS0")
            Maps(N).ROWS = ReadIniInteger(Filename, KEY, "ROWS0")
            Maps(N).NLAT = ReadIniDouble(Filename, KEY, "NLat0")
            Maps(N).SLAT = ReadIniDouble(Filename, KEY, "SLat0")
            Maps(N).ELON = ReadIniDouble(Filename, KEY, "ELon0")
            Maps(N).WLON = ReadIniDouble(Filename, KEY, "WLon0")

        Next N


        FileOpen(5, Filename, OpenMode.Input)

        For N = 1 To NoOfLines
            KEY = "[Line." & Trim(Str(N)) & "]"
            GoToThisKey(KEY)
            Lines(N).Name = Mid(LineInput(5), 6)

            KEY = Mid(LineInput(5), 6)
            'Lines(N).Type = Mid(LineInput(5), 6)
            'Lines(N).Guid = Mid(LineInput(5), 6)
            ConvertOldLineType(N, KEY)
            C = Color.FromArgb(CInt(Mid(LineInput(5), 7)))
            R = C.R
            G = C.G
            B = C.B
            Lines(N).Color = Color.FromArgb(B, G, R)
            Lines(N).NoOfPoints = CInt(Mid(LineInput(5), 12))
            If Lines(N).Name = "" Then Lines(N).Name = CStr(Lines(N).NoOfPoints) & "_Pts_Imported_Line"
            ReDim Lines(N).GLPoints(Lines(N).NoOfPoints)
            J = 6
            NL = -90 : SL = 90 : EL = -180 : WL = 180
            For M = 1 To Lines(N).NoOfPoints
                If M > 9 Then J = 7
                If M > 99 Then J = 8
                If M > 999 Then J = 9
                If M > 9999 Then J = 10
                If M > 99999 Then J = 11
                If M > 999999 Then J = 12
                X = Val(Mid(LineInput(5), J))   ' latitude
                Lines(N).GLPoints(M).lat = X
                If X < SL Then SL = X
                If X > NL Then NL = X
                X = Val(Mid(LineInput(5), J))  ' longitude
                Lines(N).GLPoints(M).lon = X
                If X > EL Then EL = X
                If X < WL Then WL = X
                Lines(N).GLPoints(M).alt = 0
                Lines(N).GLPoints(M).wid = Val(Mid(LineInput(5), J))
            Next M
            Lines(N).ELON = EL
            Lines(N).WLON = WL
            Lines(N).NLAT = NL
            Lines(N).SLAT = SL
        Next N


        For N = 1 To NoOfPolys
            KEY = "[Poly." & Trim(Str(N)) & "]"
            GoToThisKey(KEY)
            Polys(N).Name = Mid(LineInput(5), 6)

            KEY = Mid(LineInput(5), 6)
            'Polys(N).Type = Mid(LineInput(5), 6)
            'Polys(N).Guid = Mid(LineInput(5), 6)
            ConvertOldPolyType(N, KEY)

            C = Color.FromArgb(CInt(Mid(LineInput(5), 7)))
            R = C.R
            G = C.G
            B = C.B
            Polys(N).Color = Color.FromArgb(B, G, R)
            Polys(N).NoOfChilds = 0
            ' ReDim Polys(N).Childs(Polys(N).NoOfChilds)
            Polys(N).NoOfPoints = CInt(Mid(LineInput(5), 12))
            If Polys(N).Name = "" Then Polys(N).Name = CStr(Polys(N).NoOfPoints) & "_Pts_Imported_Polygon"
            ReDim Polys(N).GPoints(Polys(N).NoOfPoints)
            J = 6
            NL = -90 : SL = 90 : EL = -180 : WL = 180
            For M = 1 To Polys(N).NoOfPoints
                If M > 9 Then J = 7
                If M > 99 Then J = 8
                If M > 999 Then J = 9
                If M > 9999 Then J = 10
                If M > 99999 Then J = 11
                If M > 999999 Then J = 12
                X = Val(Mid(LineInput(5), J))
                Polys(N).GPoints(M).lat = X
                If X < SL Then SL = X
                If X > NL Then NL = X
                X = Val(Mid(LineInput(5), J))
                Polys(N).GPoints(M).lon = X
                If X > EL Then EL = X
                If X < WL Then WL = X
                ' *******!!!!
                If SB205 Then
                    Polys(N).GPoints(M).alt = Val(Mid(LineInput(5), J))
                Else
                    Polys(N).GPoints(M).alt = 0
                End If
            Next M
            Polys(N).ELON = EL
            Polys(N).WLON = WL
            Polys(N).NLAT = NL
            Polys(N).SLAT = SL
        Next N

        For N = 1 To NoOfExcludes
            KEY = "[Exclude." & Trim(Str(N)) & "]"
            GoToThisKey(KEY)

            Excludes(N).Flag = CInt(Mid(LineInput(5), 6))
            Excludes(N).NLAT = Val(Mid(LineInput(5), 6))
            Excludes(N).SLAT = Val(Mid(LineInput(5), 6))
            Excludes(N).ELON = Val(Mid(LineInput(5), 6))
            Excludes(N).WLON = Val(Mid(LineInput(5), 6))

        Next N


        For N = 1 To NoOfObjects
            KEY = "[Object." & Trim(Str(N)) & "]"
            GoToThisKey(KEY)

            Objects(N).Type = CInt(Mid(LineInput(5), 6))
            Objects(N).Description = Mid(LineInput(5), 13)
            Objects(N).Width = CSng(Val(Mid(LineInput(5), 7)))
            Objects(N).Length = CSng(Val(Mid(LineInput(5), 8)))
            Objects(N).Heading = CSng(Val(Mid(LineInput(5), 9)))
            Objects(N).Pitch = CSng(Val(Mid(LineInput(5), 7)))
            Objects(N).Bank = CSng(Val(Mid(LineInput(5), 6)))
            Objects(N).BiasX = CSng(Val(Mid(LineInput(5), 7)))
            Objects(N).BiasY = CSng(Val(Mid(LineInput(5), 7)))
            Objects(N).BiasZ = CSng(Val(Mid(LineInput(5), 7)))
            Objects(N).lat = Val(Mid(LineInput(5), 5))
            Objects(N).lon = Val(Mid(LineInput(5), 5))
            Objects(N).Altitude = CSng(Val(Mid(LineInput(5), 10)))
            Objects(N).AGL = CInt(Mid(LineInput(5), 5))
            Objects(N).Complexity = CInt(Mid(LineInput(5), 12))
            AddLatLonToObjects(N)

        Next N

        ReDim LegacyPolys(1)
        ReDim LegacyLines(1)

        FileClose(5)

    End Sub


    Friend Sub AppendSBX(ByVal Filename As String)

        Dim K, N, M, J, L As Integer
        Dim KEY As String

        Dim NoOfMapsX, NoOfLinesX As Integer
        Dim NoOfObjectsX, NoOfPolysX, NoOfExcludesX As Integer
        Dim NoOfLWCIsX As Integer

        Dim NoOfLLXYsX, NoOfWWXYsX As Integer

        Dim EL, SL, NL, WL, X As Double

        KEY = ReadIniValue(Filename, "Main", "CopyRight")
        KEY = Left(KEY, 11)

        If KEY = "PTSIM SB205" Or KEY = "PTSIM" Then
            MsgBox("You can not Append this file! Try to Import it!", MsgBoxStyle.Information)
            Exit Sub
        End If

        Dim Version As String

        If KEY = "PTSIM SB301" Then
            Version = "SB301"
        ElseIf KEY = "PTSIM SB302" Then
            Version = "SB302"
        ElseIf KEY = "PTSIM SB303" Then
            Version = "SB303"
        ElseIf KEY = "PTSIM SB310" Then
            Version = "SB310"
        ElseIf KEY = "PTSIM SB313" Then
            Version = "SB313"
        ElseIf KEY = "PTSIM SB314" Then
            Version = "SB314"
        Else
            MsgBox("Not a Valid SBX SBuilderX File!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        frmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor

        BackUp()

        NoOfMapsX = ReadIniInteger(Filename, "Main", "NoOfMaps")
        NoOfLinesX = ReadIniInteger(Filename, "Main", "NoOfLines")
        NoOfPolysX = ReadIniInteger(Filename, "Main", "NoOfPolys")
        NoOfObjectsX = ReadIniInteger(Filename, "Main", "NoOfObjects")
        NoOfExcludesX = ReadIniInteger(Filename, "Main", "NoOfExcludes")

        NoOfLWCIsX = 0
        If Not Version = "SB301" Then NoOfLWCIsX = ReadIniInteger(Filename, "Main", "NoOfLWCIs")

        NoOfLLXYsX = 0
        NoOfWWXYsX = 0
        If Version = "SB314" Then
            NoOfLLXYsX = ReadIniInteger(Filename, "Main", "NoOfLC_LOD5s")
            NoOfWWXYsX = ReadIniInteger(Filename, "Main", "NoOfWC_LOD5s")
        End If


        If NoOfMapsX > 0 Then ReDim Preserve Maps(NoOfMaps + NoOfMapsX)
        If NoOfLinesX > 0 Then ReDim Preserve Lines(NoOfLines + NoOfLinesX)
        If NoOfPolysX > 0 Then ReDim Preserve Polys(NoOfPolys + NoOfPolysX)
        If NoOfObjectsX > 0 Then ReDim Preserve Objects(NoOfObjects + NoOfObjectsX)
        If NoOfExcludesX > 0 Then ReDim Preserve Excludes(NoOfExcludes + NoOfExcludesX)
        If NoOfLWCIsX > 0 Then ReDim Preserve LWCIs(NoOfLWCIs + NoOfLWCIsX)

        For K = 1 To NoOfMapsX
            N = NoOfMaps + K
            KEY = "Map." & Trim(Str(K))
            Maps(N).Name = ReadIniValue(Filename, KEY, "Name")
            Maps(N).BMPSu = ReadIniValue(Filename, KEY, "BMPSu")
            Maps(N).BMPSp = ReadIniValue(Filename, KEY, "BMPSp")
            Maps(N).BMPFa = ReadIniValue(Filename, KEY, "BMPFa")
            Maps(N).BMPWi = ReadIniValue(Filename, KEY, "BMPWi")
            Maps(N).BMPHw = ReadIniValue(Filename, KEY, "BMPHw")
            Maps(N).BMPLm = ReadIniValue(Filename, KEY, "BMPLm")

            Maps(N).COLS = ReadIniInteger(Filename, KEY, "Cols")
            Maps(N).ROWS = ReadIniInteger(Filename, KEY, "Rows")
            Maps(N).NLAT = ReadIniDouble(Filename, KEY, "NLat")
            Maps(N).SLAT = ReadIniDouble(Filename, KEY, "SLat")
            Maps(N).ELON = ReadIniDouble(Filename, KEY, "ELon")
            Maps(N).WLON = ReadIniDouble(Filename, KEY, "WLon")
        Next K

        FileOpen(5, Filename, OpenMode.Input)

        For K = 1 To NoOfLinesX
            N = K + NoOfLines
            KEY = "[Line." & Trim(Str(K)) & "]"
            GoToThisKey((KEY))
            Lines(N).Name = Mid(LineInput(5), 6)
            Lines(N).Type = Mid(LineInput(5), 6)
            Lines(N).Guid = Mid(LineInput(5), 6)
            If Version = "SB301" Then
                Lines(N).Color = Color.FromArgb(CInt(Mid(LineInput(5), 7)))
            Else
                Lines(N).Color = ColorFromArgb(Mid(LineInput(5), 7))
            End If
            Lines(N).NoOfPoints = CInt(Mid(LineInput(5), 12))
            If Lines(N).Name = "" Then Lines(N).Name = CStr(Lines(N).NoOfPoints) & "_Pts_Imported_Line"
            ReDim Lines(N).GLPoints(Lines(N).NoOfPoints)
            J = 6
            NL = -90 : SL = 90 : EL = -180 : WL = 180
            For M = 1 To Lines(N).NoOfPoints
                If M > 9 Then J = 7
                If M > 99 Then J = 8
                If M > 999 Then J = 9
                If M > 9999 Then J = 10
                If M > 99999 Then J = 11
                If M > 999999 Then J = 12
                X = Val(Mid(LineInput(5), J))
                Lines(N).GLPoints(M).lat = X
                If X < SL Then SL = X
                If X > NL Then NL = X
                X = Val(Mid(LineInput(5), J))
                Lines(N).GLPoints(M).lon = X
                If X > EL Then EL = X
                If X < WL Then WL = X
                Lines(N).GLPoints(M).alt = Val(Mid(LineInput(5), J))
                Lines(N).GLPoints(M).wid = Val(Mid(LineInput(5), J))
            Next M
            Lines(N).ELON = EL
            Lines(N).WLON = WL
            Lines(N).NLAT = NL
            Lines(N).SLAT = SL
        Next K

        For K = 1 To NoOfPolysX
            N = K + NoOfPolys
            KEY = "[Poly." & Trim(Str(K)) & "]"
            GoToThisKey((KEY))
            Polys(N).Name = Mid(LineInput(5), 6)
            Polys(N).Type = Mid(LineInput(5), 6)
            Polys(N).Guid = Mid(LineInput(5), 6)
            If Version = "SB301" Then
                Polys(N).Color = Color.FromArgb(CInt(Mid(LineInput(5), 7)))
            Else
                Polys(N).Color = ColorFromArgb(Mid(LineInput(5), 7))
            End If
            L = CInt(Mid(LineInput(5), 12))
            If L > 0 Then
                Polys(N).NoOfChilds = L
                ReDim Polys(N).Childs(L)
            ElseIf L = 0 Then
                Polys(N).NoOfChilds = 0
                ReDim Polys(N).Childs(0)
            Else
                Polys(N).NoOfChilds = L - NoOfPolys
                ReDim Polys(N).Childs(0)
            End If
            J = 8
            For M = 1 To Polys(N).NoOfChilds
                If M > 9 Then J = 9
                If M > 99 Then J = 10
                If M > 999 Then J = 11
                If M > 9999 Then J = 12
                If M > 99999 Then J = 13
                If M > 999999 Then J = 14
                L = Val(Mid(LineInput(5), J))
                Polys(N).Childs(M) = L + NoOfPolys
            Next
            Polys(N).NoOfPoints = CInt(Mid(LineInput(5), 12))
            If Polys(N).Name = "" Then Polys(N).Name = CStr(Polys(N).NoOfPoints) & "_Pts_Imported_Polygon"
            ReDim Polys(N).GPoints(Polys(N).NoOfPoints)
            J = 6
            NL = -90 : SL = 90 : EL = -180 : WL = 180
            For M = 1 To Polys(N).NoOfPoints
                If M > 9 Then J = 7
                If M > 99 Then J = 8
                If M > 999 Then J = 9
                If M > 9999 Then J = 10
                If M > 99999 Then J = 11
                If M > 999999 Then J = 12
                X = Val(Mid(LineInput(5), J))
                Polys(N).GPoints(M).lat = X
                If X < SL Then SL = X
                If X > NL Then NL = X
                X = Val(Mid(LineInput(5), J))
                Polys(N).GPoints(M).lon = X
                If X > EL Then EL = X
                If X < WL Then WL = X
                Polys(N).GPoints(M).alt = Val(Mid(LineInput(5), J))
            Next M
            Polys(N).ELON = EL
            Polys(N).WLON = WL
            Polys(N).NLAT = NL
            Polys(N).SLAT = SL
        Next K

        Dim P, PP, C, R, I As Integer
        Dim A As String
        Dim D As Integer ' to count duplicates
        Dim Flag As Boolean
        Dim NN, LL As Integer

        NN = NoOfLands
        If NoOfLLXYsX Then
            D = 0
            ReDim Preserve LLands(256, 256, NoOfLLXYs + NoOfLLXYsX - 1)
            For P = 1 To NoOfLLXYsX
                KEY = "[LC_LOD5." & P & "]"
                GoToThisKey((KEY))
                J = Val(Mid(LineInput(5), 3))
                K = Val(Mid(LineInput(5), 3))
                L = CInt(Mid(LineInput(5), 11))
                N = LL_XY(J, K).NoOfLWs
                PP = NoOfLLXYs + P - 1
                Flag = False
                If N > 0 Then
                    D = D + 1
                    Flag = True
                    PP = LL_XY(J, K).Pointer
                Else
                    LL_XY(J, K).Pointer = PP
                End If
                LL = 0
                For M = 1 To L
                    A = LineInput(5)
                    C = Val(Mid(A, 2, 3))
                    R = Val(Mid(A, 6, 3))
                    I = Val(Mid(A, 10))
                    If Flag Then
                        If LLands(C, R, PP) > 0 Then
                            NN = NN - 1
                            LL = LL + 1
                        End If
                    End If
                    LLands(C, R, PP) = ILC(I)
                    NN = NN + 1
                Next M
                LL_XY(J, K).NoOfLWs = N + L - LL
            Next P
            NoOfLands = NN
            NoOfLLXYs = NoOfLLXYs + NoOfLLXYsX - D
            If D > 0 Then
                ReDim Preserve LLands(256, 256, NoOfLLXYs + NoOfLLXYsX - 1 - D)
            End If
        End If


        NN = NoOfWaters
        If NoOfWWXYsX Then
            D = 0
            ReDim Preserve WWaters(256, 256, NoOfWWXYs + NoOfWWXYsX - 1)
            For P = 1 To NoOfWWXYsX
                KEY = "[WC_LOD5." & P & "]"
                GoToThisKey((KEY))
                J = Val(Mid(LineInput(5), 3))
                K = Val(Mid(LineInput(5), 3))
                L = CInt(Mid(LineInput(5), 12))
                N = WW_XY(J, K).NoOfLWs
                PP = NoOfWWXYs + P - 1
                Flag = False
                If N > 0 Then
                    D = D + 1
                    Flag = True
                    PP = WW_XY(J, K).Pointer
                Else
                    WW_XY(J, K).Pointer = PP
                End If
                LL = 0
                For M = 1 To L
                    A = LineInput(5)
                    C = Val(Mid(A, 2, 3))
                    R = Val(Mid(A, 6, 3))
                    I = Val(Mid(A, 10))
                    If Flag Then
                        If WWaters(C, R, PP) > 0 Then
                            NN = NN - 1
                            LL = LL + 1
                        End If
                    End If
                    WWaters(C, R, PP) = IWC(I)
                    NN = NN + 1
                Next M
                WW_XY(J, K).NoOfLWs = N + L - LL
            Next P
            NoOfWaters = NN
            NoOfWWXYs = NoOfWWXYs + NoOfWWXYsX - D
            If D > 0 Then
                ReDim Preserve WWaters(256, 256, NoOfWWXYs + NoOfWWXYsX - 1 - D)
            End If
        End If


        For K = 1 To NoOfExcludesX
            N = K + NoOfExcludes
            KEY = "[Exclude." & Trim(Str(K)) & "]"
            GoToThisKey((KEY))

            Excludes(N).Flag = CInt(Mid(LineInput(5), 6))
            Excludes(N).NLAT = Val(Mid(LineInput(5), 6))
            Excludes(N).SLAT = Val(Mid(LineInput(5), 6))
            Excludes(N).ELON = Val(Mid(LineInput(5), 6))
            Excludes(N).WLON = Val(Mid(LineInput(5), 6))

        Next K

        For K = 1 To NoOfObjectsX
            N = K + NoOfObjects
            KEY = "[Object." & Trim(Str(K)) & "]"
            GoToThisKey((KEY))

            Objects(N).Type = CInt(Mid(LineInput(5), 6))
            Objects(N).Description = Mid(LineInput(5), 13)
            Objects(N).Width = CSng(Val(Mid(LineInput(5), 7)))
            Objects(N).Length = CSng(Val(Mid(LineInput(5), 8)))
            Objects(N).Heading = CSng(Val(Mid(LineInput(5), 9)))
            Objects(N).Pitch = CSng(Val(Mid(LineInput(5), 7)))
            Objects(N).Bank = CSng(Val(Mid(LineInput(5), 6)))
            Objects(N).BiasX = CSng(Val(Mid(LineInput(5), 7)))
            Objects(N).BiasY = CSng(Val(Mid(LineInput(5), 7)))
            Objects(N).BiasZ = CSng(Val(Mid(LineInput(5), 7)))
            Objects(N).lat = Val(Mid(LineInput(5), 5))
            Objects(N).lon = Val(Mid(LineInput(5), 5))
            Objects(N).Altitude = CSng(Val(Mid(LineInput(5), 10)))
            Objects(N).AGL = CInt(Mid(LineInput(5), 5))
            Objects(N).Complexity = CInt(Mid(LineInput(5), 12))
            AddLatLonToObjects(N)

        Next K

        For K = 1 To NoOfLWCIsX
            N = K + NoOfLWCIs
            KEY = "[LWCI." & Trim(Str(K)) & "]"
            GoToThisKey((KEY))
            LWCIs(N).Text = Mid(LineInput(5), 6)
            LWCIs(N).Color = ColorFromArgb(Mid(LineInput(5), 7))
        Next K

        FileClose(5)

        NoOfMaps = NoOfMaps + NoOfMapsX
        NoOfLines = NoOfLines + NoOfLinesX
        NoOfPolys = NoOfPolys + NoOfPolysX
        NoOfObjects = NoOfObjects + NoOfObjectsX
        NoOfExcludes = NoOfExcludes + NoOfExcludesX
        NoOfLWCIs = NoOfLWCIs + NoOfLWCIsX
        SetLWCIs()

        For K = 1 To NoOfLWCIs
            MsgBox(LWCIs(2).Color.ToString & " " & LWCIs(2).IsLand.ToString & " Text= " & LWCIs(2).Text)
        Next

        frmStart.SetMouseIcon()

    End Sub


    Private Sub GoToThisKey(ByVal KEY As String)

        Dim A As String
        Dim NK, NA As Integer

        NK = Len(KEY)

        Do
            A = LineInput(5)
            NA = Len(A)
            If NA = NK Then If A = KEY Then Exit Do
        Loop

    End Sub


    Private Function ReadIniInteger(ByVal File As String, ByVal KEY As String, ByVal Value As String) As Integer

        On Error GoTo erro
        ReadIniInteger = CInt(ReadIniValue(File, KEY, Value))
        Exit Function
erro:
        ReadIniInteger = 0

    End Function

    Private Function ReadIniDouble(ByVal File As String, ByVal KEY As String, ByVal Value As String) As Double

        On Error GoTo erro
        ReadIniDouble = Val(ReadIniValue(File, KEY, Value))
        Exit Function
erro:
        ReadIniDouble = 0

    End Function

    Private Sub SetLWCIs()

        Dim J As Integer
        Dim A, B As String
        Dim IsLand As Boolean
        Dim N2, N, N1 As Integer
        Dim P0, P1, P2, P3 As Byte

        'On Error GoTo erro

        For J = 1 To NoOfLWCIs
            A = Trim(LWCIs(J).Text) & " "

            N1 = 1
            N2 = InStr(1, A, " ")
            B = UCase(Mid(A, 1, N2 - N1))

            If B = "LAND" Then
                IsLand = True
            ElseIf B = "WATER" Then
                IsLand = False
            Else
                GoTo erro
            End If

            N1 = N2 + 1
            N2 = InStr(N1, A, " ")
            P0 = CByte(Mid(A, N1, N2 - N1))
            If IsLand Then
                For N = 1 To NoOfLCs
                    If P0 = LC(N).Index Then Exit For ' found N or the class
                Next N
                If N > NoOfLCs Then GoTo erro
            Else
                For N = 1 To NoOfWCs
                    If P0 = WC(N).Index Then Exit For ' found N
                Next N
                If N > NoOfWCs Then GoTo erro
            End If
            P1 = CByte(N) ' N goes to P1

            N1 = N2 + 1
            N2 = InStr(N1, A, " ")
            P0 = CByte(Mid(A, N1, N2 - N1))
            If IsLand Then
                For N = 1 To NoOfLCs
                    If P0 = LC(N).Index Then Exit For
                Next N
                If N > NoOfLCs Then GoTo erro
            Else
                For N = 1 To NoOfWCs
                    If P0 = WC(N).Index Then Exit For
                Next N
                If N > NoOfWCs Then GoTo erro
            End If
            P2 = CByte(N)

            N1 = N2 + 1
            N2 = InStr(N1, A, " ")
            P0 = CByte(Mid(A, N1, N2 - N1))
            If IsLand Then
                For N = 1 To NoOfLCs
                    If P0 = LC(N).Index Then Exit For
                Next N
                If N > NoOfLCs Then GoTo erro
            Else
                For N = 1 To NoOfWCs
                    If P0 = WC(N).Index Then Exit For
                Next N
                If N > NoOfWCs Then GoTo erro
            End If
            P3 = CByte(N)

            LWCIs(J).IsLand = IsLand
            LWCIs(J).Class1 = P1
            LWCIs(J).Class2 = P2
            LWCIs(J).Class3 = P3

        Next J

        Exit Sub

erro:
        MsgBox("Land/Water Class indexes could not be read!", MsgBoxStyle.Exclamation)

    End Sub
    Private Sub SetLegacyPolys()

        Dim A, B, C, File As String
        Dim Marker, N, K, J As Integer

        ReDim LegacyPolys(200)

        File = My.Application.Info.DirectoryPath & "\tools\Polys.txt"
        FileOpen(2, File, OpenMode.Input)
        N = LOF(2)
        Marker = 0
        K = 1
        Do While Marker < N
            A = LineInput(2)
            Marker = Marker + Len(A) + 2
            B = UCase(Trim(Mid(A, 1, 4)))

            If B = "GUID" Then
                C = Trim(Mid(A, 6))
                LegacyPolys(K).Guid = C
            End If

            If B = "TYPE" Then
                C = Trim(Mid(A, 6, 3))
                If C = "LCP" Then
                    If Len(A) > 8 Then
                        Try
                            J = CInt(Trim(Mid(A, 9)))
                        Catch ex As Exception
                            J = 0
                        End Try
                        If J > 0 Then
                            LegacyPolys(K).LClass = J
                            K = K + 1
                        End If
                    End If
                End If
            End If
        Loop

        FileClose()

        NoOfLegacyPolys = K - 1
        ReDim Preserve LegacyPolys(NoOfLegacyPolys)

    End Sub

    Private Sub SetLegacyLines()

        Dim A, B, C, File As String
        Dim Marker, N, K, J As Integer

        ReDim LegacyLines(250)

        File = My.Application.Info.DirectoryPath & "\tools\Lines.txt"
        FileOpen(2, File, OpenMode.Input)
        N = LOF(2)
        Marker = 0
        K = 1
        Do While Marker < N
            A = LineInput(2)
            Marker = Marker + Len(A) + 2
            B = UCase(Trim(Mid(A, 1, 4)))

            If B = "GUID" Then
                C = Trim(Mid(A, 6))
                LegacyLines(K).Guid = C
            End If

            If B = "TYPE" Then
                If Len(A) > 8 Then
                    Try
                        J = CInt(Trim(Mid(A, 9)))
                    Catch ex As Exception
                        J = 0
                    End Try
                    If J > 0 Then
                        LegacyLines(K).Legacy = J
                        LegacyLines(K).Type = Trim(Mid(A, 6, 3))
                        K = K + 1
                    End If
                End If
            End If
        Loop

        FileClose()

        NoOfLegacyLines = K - 1
        ReDim Preserve LegacyLines(NoOfLegacyLines)

    End Sub

    Private Sub ConvertOldPolyType(ByVal N As Integer, ByVal Key As String)

        Dim A, B As String
        A = Trim(Key)

        'set to none
        Polys(N).Type = ""
        Polys(N).Guid = "{00000000-0000-0000-0000-111111111111}"
        If A = "" Then Exit Sub

        ' set to old
        Polys(N).Guid = "{00000000-0000-0000-0000-222222222222}"
        On Error GoTo erro1

        Dim J, K As Integer
        Dim Flag As Boolean
        B = Mid(A, 1, 3)

        If B = "VTP" Then
            J = InStr(A, "//")
            A = Mid(A, J + 2)
            J = InStr(A, "//")
            A = Mid(A, J + 2)
            J = InStr(A, "//")
            A = Mid(A, 1, J - 1)
            J = CInt(A)
            Flag = False
            For K = 1 To NoOfLegacyPolys
                If J = LegacyPolys(K).LClass Then
                    Flag = True
                    Exit For
                End If
            Next
            If Flag Then
                Polys(N).Guid = LegacyPolys(K).Guid
                Polys(N).Type = "LCP"
            End If
        End If

        If B = "TEX" Then
            Polys(N).Type = A
            Exit Sub
        End If

        If B = "LWM" Then
            J = InStr(A, "//")
            A = Mid(A, J + 2)
            ' Water//455//455//1//1//
            J = InStr(A, "//")
            B = Mid(A, 1, J - 1)
            A = Mid(A, J + 2)
            ' 455//455//1//1//

            If B = "Water" Then
                J = InStr(A, "//")
                B = Mid(A, 1, J - 1)
                K = CInt(B)
                If K = -9999 Then
                    Polys(N).Guid = "{5835459A-4B8B-41F2-ADC1-DEE721573B28}"
                    Polys(N).Type = "HPX"
                Else
                    Polys(N).Guid = "{F4775962-DA14-4BF6-9C70-672420752870}"
                    Polys(N).Type = "HPX"
                End If
            End If

            If B = "Land" Then
                J = InStr(A, "//")
                B = Mid(A, 1, J - 1)
                K = CInt(B)
                If K = -9999 Then
                    Polys(N).Guid = "{3EC48E64-5522-449C-96C6-96F8CEBDBDE2}"
                    Polys(N).Type = "HPX"
                Else
                    Polys(N).Guid = "{D242B77F-7308-4685-9B6C-89AF1CD43D13}"
                    Polys(N).Type = "HPX"
                End If
            End If

            If B = "Flatten" Then
                Polys(N).Guid = "{18580A63-FC8F-4A02-A622-8A1E073E627B}"
                Polys(N).Type = "FLX"
            End If

        End If

        Exit Sub

erro1:
        Polys(N).Name = Polys(N).Name & "_not converted from SB205"

    End Sub

    Private Sub ConvertOldLineType(ByVal N As Integer, ByVal Key As String)

        'Type=VTP//7//1032//

        Dim A, B As String
        A = Trim(Key)

        ' set to none
        Lines(N).Type = ""
        Lines(N).Guid = "{00000000-0000-0000-0000-333333333333}"
        If A = "" Then Exit Sub

        On Error GoTo erro1
        ' set to old
        Lines(N).Guid = "{00000000-0000-0000-0000-444444444444}"

        Dim J, K As Integer
        Dim Flag As Boolean
        B = Mid(A, 1, 3)

        If B = "VTP" Then
            J = InStr(A, "//")
            A = Mid(A, J + 2)
            J = InStr(A, "//")
            A = Mid(A, J + 2)
            J = InStr(A, "//")
            A = Mid(A, 1, J - 1)
            J = CInt(A)
            Flag = False
            For K = 1 To NoOfLegacyLines
                If J = LegacyLines(K).Legacy Then
                    Flag = True
                    Exit For
                End If
            Next
            If Flag Then
                Lines(N).Guid = LegacyLines(K).Guid
                Lines(N).Type = LegacyLines(K).Type
            End If
        End If

        Exit Sub

erro1:

        Lines(N).Name = Lines(N).Name & "_not converted from SB205"

    End Sub

    Private Function ArgbFromColor(ByVal myColor As Color) As String

        Dim A, R, G, B As String

        A = Hex(myColor.A)
        If Len(A) = 1 Then A = "0" & A

        R = Hex(myColor.R)
        If Len(R) = 1 Then R = "0" & R

        G = Hex(myColor.G)
        If Len(G) = 1 Then G = "0" & G

        B = Hex(myColor.B)
        If Len(B) = 1 Then B = "0" & B

        ArgbFromColor = A & R & G & B

    End Function

    Private Function ColorFromArgb(ByVal argb As String) As Color

        ColorFromArgb = Color.FromArgb(Convert.ToInt32(argb, 16))

    End Function

End Module

NotInheritable Class SbuilderBinder
    Inherits SerializationBinder
    Public Overrides Function BindToType(ByVal assemblyName As String, ByVal typeName As String) As Type

        Dim typeToDeserialize As Type = Nothing
        typeName = typeName.Replace("WindowsApplication1", "SBuilderX")
        typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName))
        Return typeToDeserialize

    End Function


End Class


