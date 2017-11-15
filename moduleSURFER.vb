Module moduleSURFER

    Friend BLNSeparator As String
    Friend BLNExportAltitudes As Boolean

    Friend BLNLineFromPoly As Boolean
    Friend BLNPolyType As String
    Friend BLNPolyGuid As String
    Friend BLNPolyColor As Color
    Friend BLNLineType As String
    Friend BLNLineGuid As String
    Friend BLNLineColor As Color
    Friend BLNIsPolyAlt As Boolean ' true means use altitudes from file
    Friend BLNIsLineAlt As Boolean ' true means use altitudes from file
    Friend BLNStartWidth As Double
    Friend BLNEndWidth As Double

    Friend Sub ExportSurfer(ByRef FileName As String)

        Dim M, N, FN As Integer
        Dim A As String

        frmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor

        FN = FreeFile()

        FileOpen(FN, FileName, OpenMode.Output)

        For N = 1 To NoOfLines
            PrintLine(FN, CStr(Lines(N).NoOfPoints) & BLNSeparator & Trim(Lines(N).Name))
            For M = 1 To Lines(N).NoOfPoints
                A = Trim(Str(Lines(N).GLPoints(M).lon)) & BLNSeparator
                A = A & Trim(Str(Lines(N).GLPoints(M).lat))
                If BLNExportAltitudes Then A = A & BLNSeparator & Trim(Str(Lines(N).GLPoints(M).alt))
                PrintLine(FN, A)
            Next M
        Next N

        For N = 1 To NoOfPolys
            PrintLine(FN, CStr(Polys(N).NoOfPoints + 1) & BLNSeparator & Trim(Polys(N).Name))
            For M = 1 To Polys(N).NoOfPoints
                A = Trim(Str(Polys(N).GPoints(M).lon)) & BLNSeparator
                A = A & Trim(Str(Polys(N).GPoints(M).lat))
                If BLNExportAltitudes Then A = A & BLNSeparator & Trim(Str(Polys(N).GPoints(M).alt))
                PrintLine(FN, A)
            Next M
            A = Trim(Str(Polys(N).GPoints(1).lon)) & BLNSeparator
            A = A & Trim(Str(Polys(N).GPoints(1).lat))
            If BLNExportAltitudes Then A = A & BLNSeparator & Trim(Str(Polys(N).GPoints(1).alt))
            PrintLine(FN, A)
        Next N

        FileClose(FN)
        frmStart.SetMouseIcon()

    End Sub

    Friend Sub AppendSurfer(ByRef FileName As String)

        Dim K1, N, M, K2 As Integer
        Dim NoL As Integer  ' to count Lines (November 2017)
        Dim NoP As Integer  ' to count Polys (November 2017)
        Dim A, B As String
        Dim myLine As GLine
        Dim Marker As Integer
        Dim PolyFlag, Has3Par As Boolean
        Dim LenOfFile As Long

        Dim d, X0 As Double
        Dim EL, SL, NL, WL, X As Double

        frmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor
        On Error GoTo erro1

        FileOpen(2, FileName, OpenMode.Input)
        LenOfFile = LOF(2)
        Marker = 0

        myLine.Name = ""

        NoP = NoOfPolys                   ' added November 2017 
        ReDim Preserve Polys(NoP + 100)   ' create space for more 100 polys

        NoL = NoOfLines                   ' added November 2017 
        ReDim Preserve Lines(NoL + 100)   ' create space for more 100 lines

        Do While Marker < LenOfFile
            A = LineInput(2)
            Marker = Marker + Len(A) + 2
            K1 = InStr(A, BLNSeparator)
            If K1 > 0 Then
                N = CInt(Left(A, K1 - 1))
                B = Mid(A, K1 + 1, 2) ' added in April 2005, 15th
                If B = "0" & BLNSeparator Or B = "1" & BLNSeparator Then
                    myLine.Name = Mid(A, K1 + 3)
                Else
                    myLine.Name = Mid(A, K1 + 1)
                End If
            Else
                N = CInt(A)
            End If

            ReDim myLine.GLPoints(N)
            For M = 1 To N
                A = LineInput(2)
                Marker = Marker + Len(A) + 2
                K1 = InStr(A, BLNSeparator)
                myLine.GLPoints(M).lon = Val(Left(A, K1 - 1))

                ' commented on November 2017
                'K2 = InStr(K1 + 1, A, BLNSeparator)
                'If K2 = 0 Then
                '    Has3Par = False
                '    myLine.GLPoints(M).lat = Val(Mid(A, K1 + 1))
                'Else
                '    Has3Par = True
                '    myLine.GLPoints(M).lat = Val(Mid(A, K1 + 1, K2 - K1 - 1))
                '    myLine.GLPoints(M).alt = Val(Mid(A, K2 + 1))
                'End If

                ' replaced with the following
                If M = 1 Then
                    K2 = InStr(K1 + 1, A, BLNSeparator)
                    If K2 = 0 Then
                        Has3Par = False
                        myLine.GLPoints(M).lat = Val(Mid(A, K1 + 1))
                    Else
                        Has3Par = True
                        myLine.GLPoints(M).lat = Val(Mid(A, K1 + 1, K2 - K1 - 1))
                        myLine.GLPoints(M).alt = Val(Mid(A, K2 + 1))
                    End If
                Else
                    If Has3Par Then
                        K2 = InStr(K1 + 1, A, BLNSeparator)
                        myLine.GLPoints(M).lat = Val(Mid(A, K1 + 1, K2 - K1 - 1))
                        myLine.GLPoints(M).alt = Val(Mid(A, K2 + 1))
                    Else
                        myLine.GLPoints(M).lat = Val(Mid(A, K1 + 1))
                    End If
                End If
                ' end of replacement
            Next M
            PolyFlag = False
            If myLine.GLPoints(1).lat = myLine.GLPoints(N).lat Then
                If myLine.GLPoints(1).lon = myLine.GLPoints(N).lon Then
                    PolyFlag = True
                End If
            End If

            If PolyFlag Then
                NoP = NoP + 1
                'ReDim Preserve Polys(NoOfPolys)
                If myLine.Name = "" Then
                    Polys(NoP).Name = Str(N - 1) & "_Pts_BLN_Imported_Polygon"
                Else
                    Polys(NoP).Name = myLine.Name
                End If
                Polys(NoP).Color = BLNPolyColor
                Polys(NoP).Type = BLNPolyType
                Polys(NoP).Guid = BLNPolyGuid
                Polys(NoP).NoOfPoints = N - 1
                ReDim Polys(NoP).GPoints(N - 1)
                NL = -90 : SL = 90 : EL = -180 : WL = 180
                For M = 1 To N - 1
                    'X = myLine.GLPoints(M).lat
                    'Polys(NoP).GPoints(M).lat = X
                    'If X < SL Then SL = X
                    'If X > NL Then NL = X
                    '  replace with the following in November 2017
                    Polys(NoP).GPoints(M).lat = myLine.GLPoints(M).lat
                    If myLine.GLPoints(M).lat < SL Then SL = myLine.GLPoints(M).lat
                    If myLine.GLPoints(M).lat > NL Then NL = myLine.GLPoints(M).lat
                    'X = myLine.GLPoints(M).lon
                    'Polys(NoP).GPoints(M).lon = X
                    'If X > EL Then EL = X
                    'If X < WL Then WL = X
                    '  replace with the following in November 2017
                    Polys(NoP).GPoints(M).lon = myLine.GLPoints(M).lon
                    If myLine.GLPoints(M).lon > EL Then EL = myLine.GLPoints(M).lon
                    If myLine.GLPoints(M).lon < WL Then WL = myLine.GLPoints(M).lon

                    If Has3Par Then
                        If BLNIsPolyAlt Then
                            Polys(NoP).GPoints(M).alt = myLine.GLPoints(M).alt
                        Else
                            Polys(NoP).GPoints(M).alt = DefaultPolyAltitude
                        End If
                    Else
                        Polys(NoP).GPoints(M).alt = DefaultPolyAltitude
                    End If
                Next M
                Polys(NoP).ELON = EL
                Polys(NoP).WLON = WL
                Polys(NoP).NLAT = NL
                Polys(NoP).SLAT = SL

                If NoP = (NoOfPolys + 100) Then
                    NoOfPolys = NoP
                    ReDim Preserve Polys(NoP + 100)
                End If

                If BLNLineFromPoly Then
                    NoL = NoL + 1
                    'ReDim Preserve Lines(NoOfLines)
                    If myLine.Name = "" Then
                        Lines(NoL).Name = CStr(N) & "_Pts_BLN_Imported_Line"
                    Else
                        Lines(NoL).Name = myLine.Name
                    End If
                    Lines(NoL).Color = BLNLineColor
                    Lines(NoL).Type = BLNLineType
                    Lines(NoL).Guid = BLNLineGuid
                    Lines(NoL).NoOfPoints = N
                    ReDim Lines(NoL).GLPoints(N)
                    d = (BLNEndWidth - BLNStartWidth) / (N - 1)
                    X0 = BLNStartWidth
                    For M = 1 To N
                        Lines(NoL).GLPoints(M).lat = myLine.GLPoints(M).lat
                        Lines(NoL).GLPoints(M).lon = myLine.GLPoints(M).lon
                        Lines(NoL).GLPoints(M).wid = X0
                        X0 = X0 + d
                        If Has3Par Then
                            If BLNIsLineAlt Then
                                Lines(NoL).GLPoints(M).alt = myLine.GLPoints(M).alt
                            Else
                                Lines(NoL).GLPoints(M).alt = DefaultLineAltitude
                            End If
                        Else
                            Lines(NoL).GLPoints(M).alt = DefaultLineAltitude
                        End If
                    Next M
                    Lines(NoL).ELON = EL
                    Lines(NoL).WLON = WL
                    Lines(NoL).NLAT = NL
                    Lines(NoL).SLAT = SL
                    If NoL = (NoOfLines + 100) Then
                        NoOfLines = NoL
                        ReDim Preserve Lines(NoL + 100)
                    End If
                End If
            Else
                NoL = NoL + 1
                'ReDim Preserve Lines(NoOfLines)
                If myLine.Name = "" Then
                    Lines(NoL).Name = CStr(N) & "_Pts_BLN_Imported_Line"
                Else
                    Lines(NoL).Name = myLine.Name
                End If
                Lines(NoL).Color = BLNLineColor
                Lines(NoL).Type = BLNLineType
                Lines(NoL).Guid = BLNLineGuid
                Lines(NoL).NoOfPoints = N
                ReDim Lines(NoL).GLPoints(N)
                NL = -90 : SL = 90 : EL = -180 : WL = 180
                d = (BLNEndWidth - BLNStartWidth) / (N - 1)
                X0 = BLNStartWidth
                For M = 1 To N
                    'X = myLine.GLPoints(M).lat
                    'Lines(NoOfLines).GLPoints(M).lat = X
                    'If X < SL Then SL = X
                    'If X > NL Then NL = X
                    'X = myLine.GLPoints(M).lon
                    'Lines(NoOfLines).GLPoints(M).lon = X
                    'If X > EL Then EL = X
                    'If X < WL Then WL = X
                    ' replaced by the following in November 2017
                    X = myLine.GLPoints(M).lat
                    Lines(NoL).GLPoints(M).lat = myLine.GLPoints(M).lat
                    If myLine.GLPoints(M).lat < SL Then SL = myLine.GLPoints(M).lat
                    If myLine.GLPoints(M).lat > NL Then NL = myLine.GLPoints(M).lat
                    Lines(NoL).GLPoints(M).lon = myLine.GLPoints(M).lon
                    If myLine.GLPoints(M).lon > EL Then EL = myLine.GLPoints(M).lon
                    If myLine.GLPoints(M).lon < WL Then WL = myLine.GLPoints(M).lon
                    Lines(NoL).GLPoints(M).wid = X0
                    X0 = X0 + d
                    If Has3Par Then
                        If BLNIsLineAlt Then
                            Lines(NoL).GLPoints(M).alt = myLine.GLPoints(M).alt
                        Else
                            Lines(NoL).GLPoints(M).alt = DefaultLineAltitude
                        End If
                    Else
                        Lines(NoL).GLPoints(M).alt = DefaultLineAltitude
                    End If
                Next M
                Lines(NoL).ELON = EL
                Lines(NoL).WLON = WL
                Lines(NoL).NLAT = NL
                Lines(NoL).SLAT = SL
                If NoL = (NoOfLines + 100) Then
                    NoOfLines = NoL
                    ReDim Preserve Lines(NoL + 100)
                End If
            End If
        Loop

        ' NoL and NoP are the final number of lines and polys
        NoOfLines = NoL
        NoOfPolys = NoP
        ReDim Preserve Lines(NoL)
        ReDim Preserve Polys(NoP)

        FileClose(2)
        frmStart.SetMouseIcon()

        Exit Sub

erro1:
        FileClose(2)
        A = "There has been a fatal error! Please" & vbCrLf
        A = A & "verify the BLN separator in INI file!"
        MsgBox(A, MsgBoxStyle.Critical)

        frmStart.SetMouseIcon()

    End Sub


End Module
