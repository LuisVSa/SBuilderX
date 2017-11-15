Option Strict Off
Option Explicit On
Module moduleMACROS

    Friend Structure MacroObject
        Dim File As String ' file name
        Dim Name As String
    End Structure

    Friend Structure MacroCategory
        Dim Name As String
        Dim NOB As Integer
        Dim MacroObjects() As MacroObject
    End Structure

    Friend MacroCategories() As MacroCategory
    Friend NoOfMacroCategories As Integer

    Friend MacroBitmap As String
    Friend MacroLength As Single
    Friend MacroWidth As Single
    Friend MacroScale As Single
    Friend MacroRange As Integer
    Friend MacroRotation As Single
    Friend MacroElevation As Single
    Friend MacroDensity As Integer
    Friend MacroVisibility As Single

    Friend MacroV2Value As Single

    Friend MacroP6Name As String
    Friend MacroP7Name As String
    Friend MacroP8Name As String
    Friend MacroP9Name As String
    Friend MacroPAName As String
    Friend MacroPBName As String
    Friend MacroPCName As String
    Friend MacroPDName As String

    Friend MacroP6Value As String
    Friend MacroP7Value As String
    Friend MacroP8Value As String
    Friend MacroP9Value As String
    Friend MacroPAValue As String
    Friend MacroPBValue As String
    Friend MacroPCValue As String
    Friend MacroPDValue As String


    Friend MacroID As String

    Friend MacroString As String


    Friend MacroAPIPath As String
    Friend MacroASDPath As String
    Friend MacroAPIIsOn As Boolean
    Friend MacroASDIsOn As Boolean

    Friend Sub ShowAPI(ByVal C As Integer, ByVal M As Integer)

        Dim B, A, FileName As String
        Dim J, N As Integer

        FileName = MacroCategories(C).MacroObjects(M).File
        FileName = MacroAPIPath & "\" & FileName

        MacroBitmap = My.Application.Info.DirectoryPath & "\tools\bmps\na.bmp"
        MacroLength = 50
        MacroWidth = 50
        MacroScale = 1
        MacroRange = 30

        On Error GoTo erro1

        FileOpen(2, FileName, OpenMode.Input)

        For N = 1 To 6
            A = LineInput(2)
            A = Trim(A)

            B = Mid(A, 1, 10)
            If B = ";DEFAULTSC" Then
                A = Mid(A, 15)
                A = Replace(A, ",", ".")
                MacroScale = CSng(A)
            End If

            If B = ";DEFAULTRA" Then
                J = InStr(15, A, " ")
                If J = 0 Then
                    MacroRange = Int(CSng(Mid(A, 15)) / 1000)
                Else
                    MacroRange = Int(CSng(Mid(A, 15, J - 15)) / 1000)
                End If
            End If

            If B = ";MACRODESC" Then

                A = Mid(A, 12)
                J = InStr(1, A, " by ")
                If J = 0 Then GoTo next_N

                B = Mid(A, J + 4)
                A = Mid(A, 1, J - 1)

                J = InStrRev(A, " ")
                If J = 0 Then GoTo next_N
                MacroWidth = CSng(Mid(A, J + 1))

                J = InStr(B, " ")
                If J = 0 Then GoTo next_N
                MacroLength = CSng(Mid(B, 1, J - 1))

            End If
next_N:
        Next N

        FileClose()

        N = Len(FileName)
        FileName = Mid(FileName, 1, N - 3)

        A = FileName & "bmp"
        ImageFileNameTrue = A   ' added in 313
        If File.Exists(A) Then
            MacroBitmap = A
            Exit Sub
        Else
            A = FileName & "jpg"
            ImageFileNameTrue = A
            If File.Exists(A) Then
                MacroBitmap = A
                Exit Sub
            End If
        End If

        Exit Sub

erro1:
        MsgBox("Error on Show API routine!", MsgBoxStyle.Exclamation)
        FileClose()

    End Sub

    Friend Sub ShowASD(ByVal C As Integer, ByVal M As Integer)

        Dim B, A, Name, FileName As String
        Dim J, N As Integer

        FileName = MacroCategories(C).MacroObjects(M).File
        FileName = MacroASDPath & "\" & FileName

        MacroBitmap = My.Application.Info.DirectoryPath & "\tools\bmps\na.bmp"
        MacroLength = 50
        MacroWidth = 50
        MacroScale = 1
        MacroRange = 30
        MacroRotation = 0
        MacroElevation = 0
        MacroDensity = 2
        MacroVisibility = 0
        MacroP6Name = ""
        MacroP7Name = ""
        MacroP8Name = ""
        MacroP9Name = ""
        MacroPAName = ""
        MacroPBName = ""
        MacroPCName = ""
        MacroPDName = ""

        On Error GoTo erro1

        FileOpen(2, FileName, OpenMode.Input)

        A = LineInput(2)
        B = ""
        Do
            A = LineInput(2)
            N = InStr(1, A, "\")
            If N > 0 Then B = B & Mid(A, 2, N - 2) & ","
            If N = 0 Then
                B = B & Mid(A, 2) & ","
                Exit Do
            End If
        Loop
        FileClose()
        B = Replace(B, ", ", ",")
        B = Replace(B, " ,", ",")
        B = Replace(B, ",,", ",")
        B = Replace(B, ",,", ",")
        MacroString = Replace(B, "= ", "=")

        Name = GetMacroValue("Name")
        A = GetMacroValue("Type")
        A = GetMacroValue("Latitude")
        A = GetMacroValue("Longitude")
        A = GetMacroValue("Autoscale")
        A = GetMacroValue("autoscale")

        A = GetMacroValue("FixedLength")
        If A <> "" Then MacroLength = CSng(A)

        A = GetMacroValue("FixedWidth")
        If A <> "" Then MacroWidth = CSng(A)

        A = GetMacroValue("Length")
        If A <> "" Then MacroLength = CSng(A)

        A = GetMacroValue("Width")
        If A <> "" Then MacroWidth = CSng(A)

        A = GetMacroValue("Range")
        If A <> "" Then MacroRange = CInt(A)

        A = GetMacroValue("Scale")
        If A <> "" Then MacroScale = CSng(A)

        A = GetMacroValue("Rotation")
        If A <> "" Then MacroRotation = CSng(A)

        A = GetMacroValue("Elevation")
        If A <> "" Then MacroElevation = CSng(A)

        A = GetMacroValue("Visibility")
        If A <> "" Then MacroVisibility = CSng(A)

        A = GetMacroValue("Density")
        If A <> "" Then MacroDensity = CInt(A)

        A = GetMacroValue("Bitmap")
        If A <> "" Then
            A = MacroASDPath & "\" & A
            If File.Exists(A) Then
                ImageFileNameTrue = A
                MacroBitmap = A
            Else
                ImageFileNameTrue = MacroASDPath & "\" & Name & ".jpg"
            End If
        End If

        If Len(MacroString) < 2 Then Exit Sub
        A = GetMacroParameter

        If A <> "" Then
            MacroP6Name = A
            MacroP6Value = GetMacroValue(A)
        Else
            Exit Sub
        End If

        If Len(MacroString) < 2 Then Exit Sub
        A = GetMacroParameter
        If A <> "" Then
            MacroP7Name = A
            MacroP7Value = GetMacroValue(A)
        Else
            Exit Sub
        End If

        If Len(MacroString) < 2 Then Exit Sub
        A = GetMacroParameter
        If A <> "" Then
            MacroP8Name = A
            MacroP8Value = GetMacroValue(A)
        Else
            Exit Sub
        End If


        If Len(MacroString) < 2 Then Exit Sub
        A = GetMacroParameter
        If A <> "" Then
            MacroP9Name = A
            MacroP9Value = GetMacroValue(A)
        Else
            Exit Sub
        End If

        If Len(MacroString) < 2 Then Exit Sub
        A = GetMacroParameter
        If A <> "" Then
            MacroPAName = A
            MacroPAValue = GetMacroValue(A)
        Else
            Exit Sub
        End If

        If Len(MacroString) < 2 Then Exit Sub
        A = GetMacroParameter
        If A <> "" Then
            MacroPBName = A
            MacroPBValue = GetMacroValue(A)
        Else
            Exit Sub
        End If

        If Len(MacroString) < 2 Then Exit Sub
        A = GetMacroParameter
        If A <> "" Then
            MacroPCName = A
            MacroPCValue = GetMacroValue(A)
        Else
            Exit Sub
        End If

        If Len(MacroString) < 2 Then Exit Sub
        A = GetMacroParameter
        If A <> "" Then
            MacroPDName = A
            MacroPDValue = GetMacroValue(A)
        Else
            Exit Sub
        End If


erro1:
        FileClose()
        MsgBox("Error on Show ASD routine!", MsgBoxStyle.Exclamation)

    End Sub

    Friend Sub AnalyseASDMacro(ByVal N As Integer)

        Dim A As String = ""
        Dim M1, M2 As Integer

        MacroP6Name = ""
        MacroP7Name = ""
        MacroP8Name = ""
        MacroP9Name = ""
        MacroPAName = ""
        MacroPBName = ""
        MacroPCName = ""
        MacroPDName = ""

        ' If N = 0 Then A = RGNPointType1
        If N > 0 Then A = Objects(N).Description

        M1 = 1
        M2 = InStr(M1, A, "|")
        MacroID = Mid(A, M1, M2 - M1)

        M1 = M2 + 1
        M2 = InStr(M1, A, "|")
        MacroString = Mid(A, M1, M2 - M1)
        ObjComment = Mid(A, M2 + 1)

        A = GetMacroValue("Range")
        If A <> "" Then MacroRange = CInt(A)

        A = GetMacroValue("Scale")
        If A <> "" Then MacroScale = CSng(A)

        A = GetMacroValue("V1")
        If A <> "" Then MacroVisibility = CSng(A)

        If Len(MacroString) < 3 Then Exit Sub
        A = GetMacroParameter
        If A <> "" Then
            MacroP6Name = A
            MacroP6Value = GetMacroValue(A)
        Else
            Exit Sub
        End If

        If Len(MacroString) < 3 Then Exit Sub
        A = GetMacroParameter
        If A <> "" Then
            MacroP7Name = A
            MacroP7Value = GetMacroValue(A)
        Else
            Exit Sub
        End If

        If Len(MacroString) < 3 Then Exit Sub
        A = GetMacroParameter
        If A <> "" Then
            MacroP8Name = A
            MacroP8Value = GetMacroValue(A)
        Else
            Exit Sub
        End If

        If Len(MacroString) < 3 Then Exit Sub
        A = GetMacroParameter
        If A <> "" Then
            MacroP9Name = A
            MacroP9Value = GetMacroValue(A)
        Else
            Exit Sub
        End If

        If Len(MacroString) < 3 Then Exit Sub
        A = GetMacroParameter
        If A <> "" Then
            MacroPAName = A
            MacroPAValue = GetMacroValue(A)
        Else
            Exit Sub
        End If

        If Len(MacroString) < 3 Then Exit Sub
        A = GetMacroParameter
        If A <> "" Then
            MacroPBName = A
            MacroPBValue = GetMacroValue(A)
        Else
            Exit Sub
        End If

        If Len(MacroString) < 3 Then Exit Sub
        A = GetMacroParameter
        If A <> "" Then
            MacroPCName = A
            MacroPCValue = GetMacroValue(A)
        Else
            Exit Sub
        End If

        If Len(MacroString) < 3 Then Exit Sub
        A = GetMacroParameter
        If A <> "" Then
            MacroPDName = A
            MacroPDValue = GetMacroValue(A)
        Else
            Exit Sub
        End If


    End Sub

    Friend Sub SetMacroObjects()

        Dim A, File As String

        CheckAPIMacro()
        CheckASDMacro()

        If MacroAPIIsOn = False And MacroASDIsOn = False Then Exit Sub

        NoOfMacroCategories = 0
        ReDim MacroCategories(1)

        ' dot net - does Dir work?
        If MacroAPIIsOn Then
            File = MacroAPIPath & "\*.api"
            A = Dir(File)
            Do
                If A = "" Then Exit Do
                AddMacroAPIFile(A)
                A = Dir()
            Loop
        End If

        If MacroASDIsOn Then
            File = MacroASDPath & "\*.scm"
            A = Dir(File)
            Do
                If A = "" Then Exit Do
                AddMacroASDFile(A)
                A = Dir()
            Loop
        End If


    End Sub
    Private Sub AddMacroAPIFile(ByVal File As String)

        Dim B, A, FileName As String
        Dim M, C, J, N As Integer

        FileName = MacroAPIPath & "\" & File

        FileOpen(2, FileName, OpenMode.Input)

        ' line number 1
        A = LineInput(2)
        A = Replace(A, Chr(9), "")
        A = Trim(A)

        B = Mid(A, 1, 9)
        If B = ";CATEGORY" Then
            B = Trim(Mid(A, 11))
        Else
            B = "API - General"
        End If

        AddCatMacro(C, M, B) ' by ref

        MacroCategories(C).MacroObjects(M).File = UCase(File)
        MacroCategories(C).MacroObjects(M).Name = UCase(File)

        For N = 1 To 5
            A = Trim(A)
            B = Mid(A, 1, 10)
            If B = ";MACRODESC" Then
                A = Trim(Mid(A, 12))
                J = InStr(1, A, " by ")
                If J > 0 Then
                    A = Mid(A, 1, J - 1)
                    J = InStrRev(A, " ")
                    If J = 0 Then Exit For
                    A = Mid(A, 1, J - 1)
                End If
                If A <> "" Then MacroCategories(C).MacroObjects(M).Name = A
            End If
            A = LineInput(2)
        Next N

        FileClose()

    End Sub
    Private Sub AddCatMacro(ByRef Cat As Integer, ByRef Macro As Integer, ByVal Name As String)

        Dim N As Integer
        Dim NewCat As Boolean

        If NoOfMacroCategories = 0 Then
            Cat = 1
            Macro = 1
            NoOfMacroCategories = 1
            MacroCategories(1).NOB = 1
            MacroCategories(1).Name = Name
            ReDim MacroCategories(1).MacroObjects(1)
            Exit Sub
        End If

        NewCat = True
        For N = 1 To NoOfMacroCategories
            If MacroCategories(N).Name = Name Then
                NewCat = False
                Exit For
            End If
        Next N

        If NewCat Then
            NoOfMacroCategories = NoOfMacroCategories + 1
            ReDim Preserve MacroCategories(NoOfMacroCategories)
            MacroCategories(NoOfMacroCategories).Name = Name
            MacroCategories(NoOfMacroCategories).NOB = 1
            ReDim MacroCategories(NoOfMacroCategories).MacroObjects(1)
            Cat = NoOfMacroCategories
            Macro = 1
        Else
            Cat = N
            Macro = MacroCategories(N).NOB + 1
            MacroCategories(N).NOB = Macro
            ReDim Preserve MacroCategories(N).MacroObjects(Macro)
        End If

    End Sub

    Private Sub AddMacroASDFile(ByVal File As String)

        Dim B, A, FileName As String
        Dim N1, N2 As Integer
        Dim C, M As Integer

        On Error GoTo erro1

        FileName = MacroASDPath & "\" & File

        FileOpen(2, FileName, OpenMode.Input)

        ' line number 1
        A = LineInput(2)
        A = Trim(A)
        If A <> ";ASDesign Compatible Macro" Then GoTo erro1

        A = LineInput(2)
        A = A & ","
        A = Replace(A, "\", ",")
        A = Replace(A, ",,", ",")
        A = Replace(A, " ,", ",")
        N1 = InStr(1, A, "Type=")
        If N1 = 0 Then GoTo erro1
        N2 = InStr(N1, A, ",")
        B = Mid(A, N1 + 5, N2 - N1 - 5)

        If B = "Misc." Then
            B = "ASD - General"
        End If

        B = Trim(B)
        AddCatMacro(C, M, B) ' by ref

        MacroCategories(C).MacroObjects(M).File = UCase(File)
        MacroCategories(C).MacroObjects(M).Name = UCase(File)

        N1 = InStr(1, A, "Name=")
        If N1 = 0 Then GoTo erro1
        N2 = InStr(N1, A, ",")
        MacroCategories(C).MacroObjects(M).Name = Mid(A, N1 + 5, N2 - N1 - 5)

        FileClose()

        Exit Sub

erro1:

        FileClose()

    End Sub

    Private Sub CheckAPIMacro()

        MacroAPIIsOn = False
        If Dir(MacroAPIPath & "\*.api") <> "" Then MacroAPIIsOn = True

    End Sub

    Private Sub CheckASDMacro()

        MacroASDIsOn = False
        If Dir(MacroASDPath & "\*.scm") <> "" Then MacroASDIsOn = True

    End Sub

    Friend Sub AnalyseAPIMacro(ByVal N As Integer)

        Dim A As String = ""
        Dim M1, M2 As Integer

        ' If N = 0 Then A = RGNPointType1
        If N > 0 Then A = Objects(N).Description

        M1 = 1
        M2 = InStr(M1, A, "|")
        MacroID = Mid(A, M1, M2 - M1)

        M1 = M2 + 1
        M2 = InStr(M1, A, "|")
        MacroString = Mid(A, M1, M2 - M1)
        ObjComment = Mid(A, M2 + 1)

        A = GetMacroValue("Range")
        If A <> "" Then MacroRange = CInt(A)

        A = GetMacroValue("Scale")
        If A <> "" Then MacroScale = CSng(A)

        A = GetMacroValue("P6")
        If A <> "" Then MacroP6Value = A

        A = GetMacroValue("P7")
        If A <> "" Then MacroP7Value = A

        A = GetMacroValue("P8")
        If A <> "" Then MacroP8Value = A

        A = GetMacroValue("P9")
        If A <> "" Then MacroP9Value = A

        A = GetMacroValue("V1")
        If A <> "" Then MacroVisibility = CSng(A)

        A = GetMacroValue("V2")
        If A <> "" Then MacroV2Value = CSng(A)

    End Sub


    Friend Function GetMacroValue(ByVal str1 As String) As String

        Dim A As String
        Dim J, N, J1, K As Integer

        GetMacroValue = ""
        N = Len(str1)
        J1 = InStr(1, MacroString, str1)
        If J1 = 0 Then Exit Function
        GetMacroValue = "0"
        K = InStr(J1, MacroString, ",")
        J = J1 + N + 1
        If K <= J Then
            A = Mid(MacroString, J1, K - J1 + 1)
            MacroString = Replace(MacroString, A, "")
            Exit Function
        End If
        GetMacroValue = Mid(MacroString, J, K - J)
        A = Mid(MacroString, J1, K - J1 + 1)
        MacroString = Replace(MacroString, A, "")

    End Function

    Friend Function GetMacroParameter() As String

        Dim J, N As Integer

        GetMacroParameter = ""
        N = InStr(1, MacroString, "=")
        J = InStr(1, MacroString, ",")
        If N < J And N > 0 Then J = N
        If J = 0 Then Exit Function
        GetMacroParameter = Mid(MacroString, 1, J - 1)

    End Function
End Module