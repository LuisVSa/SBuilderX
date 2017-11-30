Option Strict Off
Option Explicit On
' This module was written on October 2017 to replace the old one.
' Now it uses 64-bit ArcShapeFileNetDLLx64.dll
' in November 2017 it uses no external DLL

Module moduleSHAPE

    Friend FieldNames() As String
    Friend NoOfFields As Integer
    Friend IsZ As Boolean

    Friend ShapeLineGuid As String
    Friend ShapeLineName As String
    Friend ShapeLineAltitude As Double
    Friend ShapeLineWidth As Double
    Friend ShapeLineColor As Color

    Friend ShapeLineGuidField As Integer
    Friend ShapeLineNameField As Integer
    Friend ShapeLineAltitudeField As Integer
    Friend ShapeLineWidthField As Integer
    Friend ShapeLineColorField As Integer

    Friend ShapePolyGuid As String
    Friend ShapePolyName As String
    Friend ShapePolyAltitude As Double
    Friend ShapePolyColor As Color

    Friend ShapePolyGuidField As Integer
    Friend ShapePolyNameField As Integer
    Friend ShapePolyAltitudeField As Integer
    Friend ShapePolyColorField As Integer

    Friend ShapeLineCancel As Boolean = False
    Friend ShapePolyCancel As Boolean = False

    Friend AddToCells As Boolean

    ' these are used by appending FWX lines
    ' will be changed to whatever is set when
    ' we close the Properties page of a line
    Friend DefaultNoOfLanes As Byte = 2
    Friend DefaultTrafficDir As String = "F"

    ' used by classDBFWriter
    Private Const shp_CHARACTER As Byte = 67   ' C
    Private Const shp_NUMERIC As Byte = 78     ' N
    Private Const shp_FLOAT As Byte = 70    ' F



    Friend Sub AppendSHPFile(ByVal filename As String)

        Dim SHP As New SHPReader

        Dim shpType As Integer
        Dim NoOfItems As Integer = 0
        Dim xMin, xMax, yMin, yMax As Double

        Dim IsValid As Boolean = False
        Dim IsLine As Boolean = False

        IsZ = False

        On Error GoTo erro1

        ' open shape file
        SHP.GetInfo(filename)

        ' get shape info
        shpType = SHP.shpType
        NoOfItems = SHP.recordCount
        xMin = SHP.xMin
        xMax = SHP.xMax
        yMin = SHP.yMin
        yMax = SHP.yMax

        SHP.Close()

        'ShpPolyLine
        If shpType = 3 Then     ' line with Z
            IsValid = True
            IsZ = False
            IsLine = True
        End If

        'ShpPolygon
        If shpType = 5 Then     ' polygon without Z
            IsValid = True
            IsZ = False
            IsLine = False
        End If

        'ShpPolyLineZ 
        If shpType = 13 Then    ' line with Z
            IsValid = True
            IsZ = True
            IsLine = True
        End If

        'ShpPolygonZ
        If shpType = 15 Then    ' poly with Z
            IsValid = True
            IsZ = True
            IsLine = False
        End If

        If xMin < -180 Then IsValid = False
        If xMax > 180 Then IsValid = False
        If yMin < -90 Then IsValid = False
        If yMax > 90 Then IsValid = False

        If Not IsValid Then
            GoTo erro1
        End If


        If IsLine Then
            AppendSHPLines(filename, NoOfItems, IsZ)
        Else
            AppendSHPPolys(filename, NoOfItems, IsZ)
        End If

        LonDispCenter = (xMin + xMax) / 2
        LatDispCenter = (yMin + yMax) / 2

        ViewON = True
        Zoom = 8
        ResetZoom()
        SetDispCenter(0, 0)
        Dirty = True

        RebuildDisplay()
        Exit Sub

erro1:
        MsgBox("SBuilderX can not append this Shapefile!", MsgBoxStyle.Exclamation)

    End Sub

    Private Sub AppendSHPLines(ByVal filename As String, ByVal NoOfItems As Integer, ByVal IsZ As Boolean)

        Dim myLines() As GLine
        Dim myWidths() As Double
        Dim myAltitudes() As Double

        Dim A As String
        Dim recCount As Integer
        Dim N, NV, K As Integer

        Dim FieldTypes() As DBFReader.FieldType

        ' open the dbase file
        Dim DBF As New DBFReader

        If Not DBF.FileReader(filename) Then
            MsgBox("SBuilderX can not read the database Shapefile!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        recCount = DBF.RecordCount
        NoOfFields = DBF.FieldCount

        If recCount <> NoOfItems Then
            MsgBox("The number of records in database is different from the number of shapes!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        ReDim FieldNames(NoOfFields - 1)
        ReDim FieldTypes(NoOfFields - 1)

        Dim TypeField As Integer = -1  ' added because of Luis Feliz - type for traffic lines was lost!
        ' If there is a field with name = "Type" then the type of each line
        ' is taken from the records of this field. TypeField < 0 means that the
        ' type is obtained from the Guid

        ' these are to catch the case of FWX lines
        Dim IsLanes As Boolean = False
        Dim IsDirT As Boolean = False
        Dim LanesField As Integer
        Dim DirTField As Integer
        Dim myLanes(1) As Byte
        Dim myDirT(1) As String

        For N = 0 To NoOfFields - 1
            FieldTypes(N) = DBF.FieldInfo(N).Type
            FieldNames(N) = DBF.FieldInfo(N).Name
            If FieldNames(N) = "Type" Then TypeField = N
            If FieldNames(N) = "NumberOfLa" Then
                IsLanes = True
                LanesField = N
            End If
            If FieldNames(N) = "TrafficDir" Then
                IsDirT = True
                DirTField = N
            End If
        Next N

        If IsLanes Or IsDirT Then ShapeLineGuid = "{54B91ED8-BC02-41B7-8C3B-2B8449FF85EC}"

        FrmSHPLine.ShowDialog()
        ' *********************

        If ShapeLineCancel Then
            DBF.Close()
            Exit Sub
        End If

        If ShapeLineNameField > 0 Then
            If FieldTypes(ShapeLineNameField - 1) <> DBFReader.FieldType.FTString Then
                A = "Field " & FieldNames(ShapeLineNameField - 1) & " is not a string and will be ignored!"
                ShapeLineNameField = 0
                MsgBox(A, MsgBoxStyle.Exclamation)
            End If
        End If

        If ShapeLineGuidField > 0 Then
            If FieldTypes(ShapeLineGuidField - 1) <> DBFReader.FieldType.FTString Then
                A = "Field " & FieldNames(ShapeLineGuidField - 1) & " is not a string and will be ignored!"
                ShapeLineGuidField = 0
                MsgBox(A, MsgBoxStyle.Exclamation)
            End If
        End If

        If ShapeLineWidthField > 0 Then
            If FieldTypes(ShapeLineWidthField - 1) <> DBFReader.FieldType.FTDouble Then
                A = "Field " & FieldNames(ShapeLineWidthField - 1) & " is not a double precision number and will be ignored!"
                ShapeLineWidthField = 0
                MsgBox(A, MsgBoxStyle.Exclamation)
            End If
        End If

        If IsZ Then
            If ShapeLineAltitudeField > 1 Then
                If FieldTypes(ShapeLineAltitudeField - 2) <> DBFReader.FieldType.FTDouble Then
                    A = "Field " & FieldNames(ShapeLineAltitudeField - 2) & " is not a double precision number and will be ignored!"
                    ShapeLineAltitudeField = 0
                    MsgBox(A, MsgBoxStyle.Exclamation)
                End If
            End If
        Else
            If ShapeLineAltitudeField > 0 Then
                If FieldTypes(ShapeLineAltitudeField - 1) <> DBFReader.FieldType.FTDouble Then
                    A = "Field " & FieldNames(ShapeLineAltitudeField - 1) & " is not a double precision number and will be ignored!"
                    ShapeLineAltitudeField = 0
                    MsgBox(A, MsgBoxStyle.Exclamation)
                End If
            End If
        End If

        If ShapeLineAltitudeField > 1 Then
            If FieldTypes(ShapeLineAltitudeField - 2) <> DBFReader.FieldType.FTDouble Then
                A = "Field " & FieldNames(ShapeLineAltitudeField - 2) & " is not a double precision number and will be ignored!"
                ShapeLineAltitudeField = 0
                MsgBox(A, MsgBoxStyle.Exclamation)
            End If
        End If

        If ShapeLineColorField > 0 Then
            If FieldTypes(ShapeLineColorField - 1) <> DBFReader.FieldType.FTInteger Then
                A = "Field " & FieldNames(ShapeLineColorField - 1) & " is not an integer number and will be ignored!"
                ShapeLineColorField = 0
                MsgBox(A, MsgBoxStyle.Exclamation)
            End If
        End If

        ReDim myLines(NoOfItems - 1)
        ReDim myWidths(NoOfItems - 1)
        ReDim myAltitudes(NoOfItems - 1)

        If IsLanes Then ReDim myLanes(NoOfItems - 1)
        If IsDirT Then ReDim myDirT(NoOfItems - 1)

        For N = 0 To NoOfItems - 1
            If ShapeLineNameField = 0 Then
                myLines(N).Name = ShapeLineName
            Else
                myLines(N).Name = DBF.Attribute(N, ShapeLineNameField - 1)
            End If

            If ShapeLineGuidField = 0 Then
                myLines(N).Guid = ShapeLineGuid
            Else
                A = DBF.Attribute(N, ShapeLineGuidField - 1)
                If A <> "" Then
                    myLines(N).Guid = A
                Else
                    myLines(N).Guid = ShapeLineGuid
                End If
            End If

            If IsLanes Then
                A = DBF.Attribute(N, LanesField)
                If A <> "" Then myLanes(N) = A Else myLanes(N) = DefaultNoOfLanes
            End If

            If IsDirT Then
                A = DBF.Attribute(N, DirTField)
                If A <> "" Then myDirT(N) = A Else myDirT(N) = DefaultTrafficDir
            End If

            If TypeField >= 0 Then
                myLines(N).Type = DBF.Attribute(N, TypeField)
                If myLines(N).Type = "" Then
                    myLines(N).Type = GetLineTypeFromGuid(myLines(N).Guid)
                End If
            Else
                If Not IsLanes And Not IsDirT Then
                    myLines(N).Type = GetLineTypeFromGuid(myLines(N).Guid)
                Else  ' form the type here without looking to Guid
                    A = "FWX"
                    If IsLanes Then A = A & myLanes(N) Else A = A & DefaultNoOfLanes
                    If IsDirT Then A = A & myDirT(N) Else A = A & DefaultTrafficDir
                    myLines(N).Type = A
                End If
            End If

            If ShapeLineColorField = 0 Then
                myLines(N).Color = ShapeLineColor
            Else
                A = DBF.Attribute(N, ShapeLineColorField - 1)
                If A <> "" Then
                    myLines(N).Color = Color.FromArgb(CInt(A))
                Else
                    myLines(N).Color = ShapeLineColor
                End If
            End If

            If ShapeLineWidthField = 0 Then
                myWidths(N) = ShapeLineWidth
            Else
                A = DBF.Attribute(N, ShapeLineWidthField - 1)
                If A <> "" Then
                    myWidths(N) = A
                Else
                    myWidths(N) = ShapeLineWidth
                End If
            End If

            If IsZ Then
                If ShapeLineAltitudeField = 0 Then
                    myAltitudes(N) = ShapeLineAltitude
                ElseIf ShapeLineAltitudeField > 1 Then
                    A = DBF.Attribute(N, ShapeLineAltitudeField - 2)
                    If A <> "" Then
                        myAltitudes(N) = A
                    Else
                        myAltitudes(N) = ShapeLineAltitude
                    End If
                End If
            Else
                If ShapeLineAltitudeField = 0 Then
                    myAltitudes(N) = ShapeLineAltitude
                ElseIf ShapeLineAltitudeField > 0 Then
                    A = DBF.Attribute(N, ShapeLineAltitudeField - 1)
                    If A <> "" Then
                        myAltitudes(N) = A
                    Else
                        myAltitudes(N) = ShapeLineAltitude
                    End If
                End If
            End If

        Next

        DBF.Close()

        FrmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor
        On Error GoTo erro1

        ' open shape file
        Dim SHP As New SHPReader
        SHP.FileReader(filename)

        For N = 1 To NoOfItems

            SHP.MoveTo(N)  ' set the current record
            NV = SHP.NumPoints
            myLines(N - 1).NoOfPoints = NV
            ReDim myLines(N - 1).GLPoints(NV)

            For K = 1 To NV
                myLines(N - 1).GLPoints(K).lon = SHP.Points(K).X
                myLines(N - 1).GLPoints(K).lat = SHP.Points(K).Y
                If IsZ And ShapeLineAltitudeField = 1 Then
                    myLines(N - 1).GLPoints(K).alt = SHP.Points(K).Z
                Else
                    myLines(N - 1).GLPoints(K).alt = myAltitudes(N - 1)
                End If
                myLines(N - 1).GLPoints(K).wid = myWidths(N - 1)
            Next

        Next

        'close the shape file
        SHP.Close()

        ReDim Preserve Lines(NoOfLines + NoOfItems)

        For N = 1 To NoOfItems
            Lines(NoOfLines + N) = myLines(N - 1)
            AddLatLonToLine(NoOfLines + N)
        Next
        NoOfLines = NoOfLines + NoOfItems

        FrmStart.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
erro1:
        MsgBox("SBuilderX can not Append " & filename & " !", MsgBoxStyle.Exclamation)
        FrmStart.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Private Function GetLineTypeFromGuid(ByVal guid As String) As String

        GetLineTypeFromGuid = ""

        Dim K As Integer

        For K = 1 To NoOfLineTypes
            If LineTypes(K).Guid = guid Then
                GetLineTypeFromGuid = LineTypes(K).Type
                If GetLineTypeFromGuid = "FWX" Then
                    GetLineTypeFromGuid = GetLineTypeFromGuid & DefaultNoOfLanes.ToString & DefaultTrafficDir
                End If
                Exit For
            End If
        Next

    End Function

    Private Sub AppendSHPPolys(ByVal filename As String, ByVal NoOfItems As Integer, ByVal IsZ As Boolean)

        Dim myAltitudes() As Double
        Dim myColors() As Color
        Dim myNames() As String
        Dim myGuids() As String
        Dim myTypes() As String

        Dim A As String

        Dim recCount As Integer
        Dim N, N1, N2, NV, K, J, JK, I, M, NP As Integer

        Dim FieldTypes() As DBFReader.FieldType

        ' open the dbase file
        Dim DBF As New DBFReader

        If Not DBF.FileReader(filename) Then
            MsgBox("SBuilderX can not read the database Shapefile!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        recCount = DBF.RecordCount
        NoOfFields = DBF.FieldCount

        If recCount <> NoOfItems Then
            MsgBox("The number of records in database is different from the number of shapes!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        ReDim FieldNames(NoOfFields - 1)
        ReDim FieldTypes(NoOfFields - 1)


        Dim TypeField As Integer = -1  ' added because of Luis Feliz - type for traffic lines was lost!

        For N = 0 To NoOfFields - 1
            FieldTypes(N) = DBF.FieldInfo(N).Type
            FieldNames(N) = DBF.FieldInfo(N).Name
            If FieldNames(N) = "Type" Then TypeField = N
        Next N

        frmSHPPoly.ShowDialog()
        ' *********************

        If ShapePolyCancel Then
            DBF.Close()
            Exit Sub
        End If

        If ShapePolyNameField > 0 Then
            If FieldTypes(ShapePolyNameField - 1) <> DBFReader.FieldType.FTString Then
                A = "Field """ & FieldNames(ShapePolyNameField - 1) & """ is not a string and will be ignored!"
                ShapePolyNameField = 0
                MsgBox(A, MsgBoxStyle.Exclamation)
            End If
        End If

        If ShapePolyGuidField > 0 Then
            If FieldTypes(ShapePolyGuidField - 1) <> DBFReader.FieldType.FTString Then
                A = "Field """ & FieldNames(ShapePolyGuidField - 1) & """ is not a string and will be ignored!"
                ShapePolyGuidField = 0
                MsgBox(A, MsgBoxStyle.Exclamation)
            End If
        End If

        If IsZ Then   ' after scott
            If ShapePolyAltitudeField > 1 Then
                If FieldTypes(ShapePolyAltitudeField - 2) <> DBFReader.FieldType.FTDouble Then
                    A = "Field """ & FieldNames(ShapePolyAltitudeField - 2) & """ is not a double precision number and will be ignored!"
                    ShapePolyAltitudeField = 0
                    MsgBox(A, MsgBoxStyle.Exclamation)
                End If
            End If
        Else
            If ShapePolyAltitudeField > 0 Then
                If FieldTypes(ShapePolyAltitudeField - 1) <> DBFReader.FieldType.FTDouble Then
                    A = "Field """ & FieldNames(ShapePolyAltitudeField - 1) & """ is not a double precision number and will be ignored!"
                    ShapePolyAltitudeField = 0
                    MsgBox(A, MsgBoxStyle.Exclamation)
                End If
            End If

        End If

        If ShapePolyColorField > 0 Then
            If FieldTypes(ShapePolyColorField - 1) <> DBFReader.FieldType.FTInteger Then
                A = "Field """ & FieldNames(ShapePolyColorField - 1) & """ is not a integer number and will be ignored!"
                ShapePolyColorField = 0
                MsgBox(A, MsgBoxStyle.Exclamation)
            End If
        End If

        ReDim myAltitudes(NoOfItems - 1)
        ReDim myColors(NoOfItems - 1)
        ReDim myNames(NoOfItems - 1)
        ReDim myGuids(NoOfItems - 1)
        ReDim myTypes(NoOfItems - 1)

        For N = 0 To NoOfItems - 1
            If ShapePolyNameField = 0 Then
                myNames(N) = ShapePolyName
            Else
                myNames(N) = DBF.Attribute(N, ShapePolyNameField - 1)
            End If

            If ShapePolyGuidField = 0 Then
                myGuids(N) = ShapePolyGuid
            Else
                myGuids(N) = DBF.Attribute(N, ShapePolyGuidField - 1)
                If myGuids(N) = "" Then
                    myGuids(N) = ShapePolyGuid
                End If
            End If

            If TypeField >= 0 Then
                myTypes(N) = DBF.Attribute(N, TypeField)
                If myTypes(N) = "" Then
                    myTypes(N) = GetPolyTypeFromGuid(myGuids(N))
                End If
            Else
                myTypes(N) = GetPolyTypeFromGuid(myGuids(N))
            End If

            If ShapePolyColorField = 0 Then
                myColors(N) = ShapePolyColor
            Else
                A = DBF.Attribute(N, ShapePolyColorField - 1)
                If A <> "" Then  ''''
                    myColors(N) = Color.FromArgb(CInt(A))
                Else
                    myColors(N) = ShapePolyColor
                End If
            End If

            If IsZ Then     ' after scott
                If ShapePolyAltitudeField = 0 Then
                    myAltitudes(N) = ShapePolyAltitude
                ElseIf ShapePolyAltitudeField > 1 Then
                    A = DBF.Attribute(N, ShapePolyAltitudeField - 2)
                    If A <> "" Then
                        myAltitudes(N) = A
                    Else
                        myAltitudes(N) = ShapePolyAltitude
                    End If
                End If
            Else
                If ShapePolyAltitudeField = 0 Then
                    myAltitudes(N) = ShapePolyAltitude
                ElseIf ShapePolyAltitudeField > 0 Then
                    A = DBF.Attribute(N, ShapePolyAltitudeField - 1)
                    If A <> "" Then
                        myAltitudes(N) = A
                    Else
                        myAltitudes(N) = ShapePolyAltitude
                    End If
                End If
            End If
        Next

        ' close the dbase
        DBF.Close()

        FrmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor
        On Error GoTo erro1

        ' open shape file
        Dim SHP As New SHPReader
        SHP.FileReader(filename)

        'K is to count number of polygons
        K = NoOfPolys
        ReDim Preserve Polys(K + 100)   ' allow 100 polygons to be added!
        Dim P() As Integer  'to store the index of the starting vertice of the part

        For N = 1 To NoOfItems     ' NoOfItems taken from the DBF file also means parent polys!

            SHP.MoveTo(N)  ' set the current record
            NV = SHP.NumPoints
            NP = SHP.NumParts

            ReDim P(NP - 1)

            If NP > 1 Then
                For J = 1 To NP
                    P(J - 1) = SHP.Begins(J)
                Next
            End If

            For J = 1 To NP
                K = K + 1
                Polys(K).Name = myNames(N - 1)
                Polys(K).Color = myColors(N - 1)
                Polys(K).Guid = myGuids(N - 1)
                Polys(K).Type = myTypes(N - 1)
                If NP = 1 Then
                    ReDim Polys(K).Childs(0)
                    Polys(K).NoOfChilds = 0
                    N1 = 1
                    N2 = NV - 1
                Else
                    If J = 1 Then   ' first is a father
                        ReDim Polys(K).Childs(NP - 1)
                        Polys(K).NoOfChilds = NP - 1
                        For I = 1 To NP - 1
                            Polys(K).Childs(I) = K + I
                        Next
                        'N1 = 1
                        'N2 = P(1) - 2
                        ' to correct the triangle anomalities 
                        N1 = 1
                        N2 = P(1) - 1
                        JK = K
                    ElseIf J = NP Then  ' the last child
                        ReDim Polys(K).Childs(0)
                        Polys(K).NoOfChilds = -JK
                        'N1 = P(J - 1)
                        'N2 = NV - 1
                        ' to correct the triangle anomalities 
                        N1 = P(J - 1) + 1
                        N2 = NV - 1
                    Else ' others childs if more than one
                        ReDim Polys(K).Childs(0)
                        Polys(K).NoOfChilds = -JK
                        'N1 = P(J - 1)
                        'N2 = P(J) - 2
                        ' to correct the triangle anomalities 
                        N1 = P(J - 1) + 1
                        N2 = P(J) - 1
                    End If
                End If

                ReDim Polys(K).GPoints(N2 - N1 + 1)
                Polys(K).NoOfPoints = N2 - N1 + 1
                For I = N1 To N2
                    M = I - N1 + 1
                    Polys(K).GPoints(M).lon = SHP.Points(I).X
                    Polys(K).GPoints(M).lat = SHP.Points(I).Y
                    If IsZ And ShapePolyAltitudeField = 1 Then
                        Polys(K).GPoints(M).alt = SHP.Points(I).Z
                    Else
                        Polys(K).GPoints(M).alt = myAltitudes(N - 1)
                    End If
                Next

                AddLatLonToPoly(K)

                If K = NoOfPolys + 100 Then
                    ReDim Preserve Polys(NoOfPolys + 1000)
                End If
                If K = NoOfPolys + 1000 Then
                    ReDim Preserve Polys(NoOfPolys + 10000)
                End If
                If K = NoOfPolys + 10000 Then
                    ReDim Preserve Polys(NoOfPolys + 100000)
                End If
                If K = NoOfPolys + 100000 Then
                    ReDim Preserve Polys(NoOfPolys + 1000000)
                End If
            Next
        Next

        'close the shape file
        SHP.Close()

        'K is total and final number of polys
        ReDim Preserve Polys(K)
        NoOfPolys = K

        FrmStart.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
erro1:
        MsgBox("SBuilderX can not Append " & filename & " !", MsgBoxStyle.Exclamation)
        FrmStart.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Private Function GetPolyTypeFromGuid(ByVal guid As String) As String

        GetPolyTypeFromGuid = ""

        Dim K As Integer

        For K = 1 To NoOfPolyTypes
            If PolyTypes(K).Guid = guid Then
                GetPolyTypeFromGuid = PolyTypes(K).Type
                Exit For
            End If
        Next

    End Function

    Friend Sub ExportSHPLines(ByVal filename As String)

        Dim N As Integer

        Dim DBF As New DBFWriter

        FrmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor
        On Error GoTo erro1

        If Not DBF.FileWriter(filename, NoOfLines) Then Exit Sub

        ' add the fields
        If Not DBF.CreateField("Name", shp_CHARACTER, 20, 0) Then Exit Sub
        If Not DBF.CreateField("Type", shp_CHARACTER, 50, 0) Then Exit Sub
        If Not DBF.CreateField("Color", shp_NUMERIC, 11, 0) Then Exit Sub
        If Not DBF.CreateField("Guid", shp_CHARACTER, 38, 0) Then Exit Sub

        ' append the fields
        If Not DBF.AppendFields() Then Exit Sub

        ' populate the fields
        For N = 1 To NoOfLines
            DBF.AddRecord(N, 1, Lines(N).Name)
            DBF.AddRecord(N, 2, Lines(N).Type)
            DBF.AddRecord(N, 3, Lines(N).Color.ToArgb)
            DBF.AddRecord(N, 4, Lines(N).Guid)
        Next

        ' close the DB file
        DBF.Close()

        ' now create the SHP and the SHX files
        CreateShpAndShxFilesFromLines(filename, "ALL")

        FrmStart.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
erro1:
        MsgBox("SBuilderX can not Export " & filename & " !", MsgBoxStyle.Exclamation)
        FrmStart.Cursor = System.Windows.Forms.Cursors.Default

    End Sub


    Friend Sub ExportSHPPolys(ByVal filename As String)

        Dim N, K As Integer

        FrmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor
        On Error GoTo erro1

        ' to be used by SHX creation and DBFile creation
        Dim nRecords As Integer = NumberOfRecordsInPolys()

        Dim DBF As New DBFWriter

        If Not DBF.FileWriter(filename, nRecords) Then Exit Sub

        ' add the fields
        If Not DBF.CreateField("Name", shp_CHARACTER, 20, 0) Then Exit Sub
        If Not DBF.CreateField("Type", shp_CHARACTER, 50, 0) Then Exit Sub
        If Not DBF.CreateField("Color", shp_NUMERIC, 11, 0) Then Exit Sub
        If Not DBF.CreateField("Guid", shp_CHARACTER, 38, 0) Then Exit Sub

        ' append the fields
        If Not DBF.AppendFields() Then Exit Sub

        ' populate the fields
        K = 0
        For N = 1 To NoOfPolys
            If Polys(N).NoOfChilds >= 0 Then
                K = K + 1
                DBF.AddRecord(K, 1, Polys(N).Name)
                DBF.AddRecord(K, 2, Polys(N).Type)
                DBF.AddRecord(K, 3, Polys(N).Color.ToArgb)
                DBF.AddRecord(K, 4, Polys(N).Guid)
            End If
        Next

        ' close the DB file
        DBF.Close()

        ' now create the SHP and the SHX files
        CreateShpAndShxFilesFromPolys(filename, "ALL")

        FrmStart.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
erro1:
        MsgBox("SBuilderX can not Export " & filename & " !", MsgBoxStyle.Exclamation)
        FrmStart.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Private Function CreateShpAndShxFilesFromPolys(ByVal filename As String, ByVal type As String) As Boolean

        ' type can be ALL (used when exporting shape files) or
        ' EXX XXX LCP HPX HGX FLX  (when creating shape files for shp2vec)

        CreateShpAndShxFilesFromPolys = False

        FrmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor
        On Error GoTo erro1

        Dim N, M, J, K, REC, np As Integer

        Dim Xmin, Xmax As Double
        Dim Ymin, Ymax As Double
        Dim Zmin, Zmax As Double

        Dim X, Y, Z As Double

        Dim XXmin, XXmax As Double
        Dim YYmin, YYmax As Double
        Dim ZZmin, ZZmax As Double

        Dim nParts, nPoints As Integer

        Dim recLen As Integer = 0
        Dim ptrBegin As Integer = 0
        Dim ptrMid As Integer = 0
        Dim ptrEnd As Integer = 0

        Dim nRecords As Integer
        If type = "ALL" Then
            nRecords = NumberOfRecordsInPolys()
        Else
            nRecords = NumberOfRecordsInSelectedPolys(type)
        End If

        Dim RecOffset() As Integer
        ReDim RecOffset(nRecords)
        Dim RecLenght() As Integer
        ReDim RecLenght(nRecords)

        Dim ShapeType As Integer = 15   ' polygonZ

        Dim fs As New FileStream(filename, FileMode.Create)
        Dim bw As New BinaryWriter(fs)

        bw.Write(BigEndian(9994I))   ' write fixed initial number
        bw.Seek(96, SeekOrigin.Current)

        REC = 0
        XXmin = 180 : XXmax = -180 : YYmin = 90 : YYmax = -90
        ZZmin = 100000 : ZZmax = -100000
        Dim P() As Integer   ' point to the 1st index of a part in the sequence of points
        For N = 1 To NoOfPolys
            If Polys(N).NoOfChilds >= 0 Then
                If type = "ALL" Or type = Mid(Polys(N).Type, 1, 3) Then
                    ' new record 
                    REC = REC + 1
                    ptrBegin = CInt(fs.Position)
                    RecOffset(REC) = ptrBegin / 2
                    nParts = Polys(N).NoOfChilds + 1   ' the childs plus the parent
                    ReDim P(nParts)
                    P(1) = 0     ' always zero!
                    ' find nPoints for the record and build P()
                    nPoints = Polys(N).NoOfPoints + 1
                    For M = 2 To nParts
                        P(M) = nPoints
                        np = Polys(Polys(N).Childs(M - 1)).NoOfPoints + 1
                        nPoints = nPoints + np
                    Next
                    ' advance 4 + 4 + 4 + 4 x 8 = 44 ( recNum recLen ShapeType and box )
                    ' plus 4 + 4 (nParts nPoints) + nParts * 4 (for P() array )
                    ' total = 52 + 4 * nParts
                    bw.Seek((52 + 4 * nParts), SeekOrigin.Current)
                    ' the XXXXXXXXXXXXXXXX and YYYYYYYYYYYYYYYYYYYY
                    Xmin = 180 : Xmax = -180 : Ymin = 90 : Ymax = -90
                    For J = 1 To Polys(N).NoOfPoints
                        X = Polys(N).GPoints(J).lon
                        If X > Xmax Then Xmax = X
                        If X < Xmin Then Xmin = X
                        bw.Write(X)
                        Y = Polys(N).GPoints(J).lat
                        If Y > Ymax Then Ymax = Y
                        If Y < Ymin Then Ymin = Y
                        bw.Write(Y)
                    Next
                    X = Polys(N).GPoints(1).lon
                    bw.Write(X)
                    Y = Polys(N).GPoints(1).lat
                    bw.Write(Y)
                    For M = 2 To nParts
                        K = Polys(N).Childs(M - 1)
                        For J = 1 To Polys(K).NoOfPoints
                            X = Polys(K).GPoints(J).lon
                            If X > Xmax Then Xmax = X
                            If X < Xmin Then Xmin = X
                            bw.Write(X)
                            Y = Polys(K).GPoints(J).lat
                            If Y > Ymax Then Ymax = Y
                            If Y < Ymin Then Ymin = Y
                            bw.Write(Y)
                        Next
                        X = Polys(K).GPoints(1).lon
                        bw.Write(X)
                        Y = Polys(K).GPoints(1).lat
                        bw.Write(Y)
                    Next
                    ' now the ZZZZZZZZZZZZZZZZZ
                    ptrMid = CInt(fs.Position)          ' need to return here!
                    bw.Seek(16, SeekOrigin.Current)     ' leave space for Zmin & Zmax
                    Zmin = 100000 : Zmax = -100000
                    For J = 1 To Polys(N).NoOfPoints
                        Z = Polys(N).GPoints(J).alt
                        If Z > Zmax Then Zmax = Z
                        If Z < Zmin Then Zmin = Z
                        bw.Write(Z)
                    Next
                    Z = Polys(N).GPoints(1).alt
                    bw.Write(Z)
                    For M = 2 To nParts
                        K = Polys(N).Childs(M - 1)
                        For J = 1 To Polys(K).NoOfPoints
                            Z = Polys(K).GPoints(J).alt
                            If Z > Zmax Then Zmax = Z
                            If Z < Zmin Then Zmin = Z
                            bw.Write(Z)
                        Next
                        Z = Polys(K).GPoints(1).alt
                        bw.Write(Z)
                    Next
                    If Xmin < XXmin Then XXmin = Xmin
                    If Ymin < YYmin Then YYmin = Ymin
                    If Zmin < ZZmin Then ZZmin = Zmin
                    If Xmax > XXmax Then XXmax = Xmax
                    If Ymax > YYmax Then YYmax = Ymax
                    If Zmax > ZZmax Then ZZmax = Zmax

                    bw.Seek((nPoints + 1) * 8, SeekOrigin.Current)     ' advance Mmin Mmax and nPoints M points
                    bw.Write(0R)    ' go back 8 and write a double = O to terminate

                    ptrEnd = CInt(fs.Position)
                    recLen = (ptrEnd - ptrBegin) / 2    ' get the record lenght
                    recLen = recLen - 4 ' contents lenght of record is less by 8 bytes
                    RecLenght(REC) = recLen
                    ' go back and fill the record header
                    bw.Seek(ptrBegin, SeekOrigin.Begin)
                    bw.Write(BigEndian(REC))
                    bw.Write(BigEndian(recLen))
                    bw.Write(ShapeType)
                    bw.Write(Xmin)
                    bw.Write(Ymin)
                    bw.Write(Xmax)
                    bw.Write(Ymax)
                    bw.Write(nParts)
                    bw.Write(nPoints)    ' total number of points
                    bw.Write(P(1))       ' the index for the first point of the first part 
                    For M = 2 To nParts
                        bw.Write(P(M))   ' the index for the first point of part M
                    Next
                    ' do not forget to fill Zmin & Zmax
                    bw.Seek(ptrMid, SeekOrigin.Begin)
                    bw.Write(Zmin)
                    bw.Write(Zmax)
                    ' and reposition the pointer for next record
                    bw.Seek(ptrEnd, SeekOrigin.Begin)
                End If
            End If
        Next
        recLen = CInt(fs.Length)    ' recLen is free
        recLen = recLen / 2
        ' now complete  header
        bw.Seek(24, SeekOrigin.Begin)
        bw.Write(BigEndian(recLen))
        bw.Write(1000I)       ' version
        bw.Write(ShapeType)   ' shape type
        bw.Write(XXmin)
        bw.Write(YYmin)
        bw.Write(XXmax)
        bw.Write(YYmax)
        bw.Write(ZZmin)
        bw.Write(ZZmax)
        bw.Write(0R)     ' Mmin
        bw.Write(0R)     ' Mmax
        bw.Close()
        fs.Close()

        ' now create the SHX file!
        Dim SHXFile As String = Path.ChangeExtension(filename, ".SHX")
        If File.Exists(SHXFile) Then File.Delete(SHXFile)
        fs = New FileStream(SHXFile, FileMode.Create)
        bw = New BinaryWriter(fs)
        bw.Write(BigEndian(9994I))   ' write fixed initial number
        bw.Seek(20, SeekOrigin.Current)
        bw.Write(BigEndian(50 + nRecords * 4)) ' lenght here
        bw.Write(1000I)       ' version
        bw.Write(ShapeType)   ' shape type
        bw.Write(XXmin)
        bw.Write(YYmin)
        bw.Write(XXmax)
        bw.Write(YYmax)
        bw.Write(ZZmin)
        bw.Write(ZZmax)
        bw.Write(0R)     ' Mmin
        bw.Write(0R)     ' Mmax
        'bw.Seek(16, SeekOrigin.Current)  ' skip Mmin and Mmax
        For N = 1 To nRecords
            bw.Write(BigEndian(RecOffset(N)))
            bw.Write(BigEndian(RecLenght(N)))
        Next
        bw.Close()
        fs.Close()

        CreateShpAndShxFilesFromPolys = True

        FrmStart.Cursor = System.Windows.Forms.Cursors.Default
        Exit Function
erro1:
        MsgBox("SBuilderX can not Export " & filename & " !", MsgBoxStyle.Exclamation)
        FrmStart.Cursor = System.Windows.Forms.Cursors.Default

    End Function

    Private Function CreateShpAndShxFilesFromLines(ByVal filename As String, ByVal type As String) As Boolean

        ' type can be ALL for exporting shape files or 
        '  STX FWX RDX HLX RRX UTX for shp2vec

        CreateShpAndShxFilesFromLines = False
        FrmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor
        On Error GoTo erro1

        Dim N, M, J, K, REC, np As Integer

        Dim Xmin, Xmax As Double
        Dim Ymin, Ymax As Double
        Dim Zmin, Zmax As Double

        Dim X, Y, Z As Double

        Dim XXmin, XXmax As Double
        Dim YYmin, YYmax As Double
        Dim ZZmin, ZZmax As Double

        Dim nPoints As Integer

        Dim recLen As Integer = 0
        Dim ptrBegin As Integer = 0
        Dim ptrMid As Integer = 0
        Dim ptrEnd As Integer = 0

        Dim nRecords As Integer

        If type = "ALL" Then
            nRecords = NoOfLines
        Else
            nRecords = NumberOfRecordsInSelectedLines(type)
        End If

        ' these are for the SHX file
        Dim RecOffset() As Integer
        ReDim RecOffset(nRecords)
        Dim RecLenght() As Integer
        ReDim RecLenght(nRecords)

        Dim ShapeType As Integer = 13   ' polylineZ

        Dim fs As New FileStream(filename, FileMode.Create)
        Dim bw As New BinaryWriter(fs)

        bw.Write(BigEndian(9994I))   ' write fixed initial number
        bw.Seek(96, SeekOrigin.Current)

        XXmin = 180 : XXmax = -180 : YYmin = 90 : YYmax = -90
        ZZmin = 100000 : ZZmax = -100000

        REC = 0
        For N = 1 To NoOfLines
            If type = "ALL" Or type = Mid(Lines(N).Type, 1, 3) Then
                ' new record 
                REC = REC + 1
                ptrBegin = CInt(fs.Position)
                RecOffset(REC) = ptrBegin / 2
                nPoints = Lines(N).NoOfPoints
                ' advance 4 + 4 + 4 + 4 x 8 = 44 ( recNum recLen ShapeType and box )
                ' plus 4 + 4 (nParts nPoints) + 4  for P(1) ; P(1)=0 
                ' total = 52 + 4 
                bw.Seek((56), SeekOrigin.Current)
                ' the XXXXXXXXXXXXXXXX and YYYYYYYYYYYYYYYYYYYY
                Xmin = 180 : Xmax = -180 : Ymin = 90 : Ymax = -90
                For J = 1 To nPoints
                    X = Lines(N).GLPoints(J).lon
                    If X > Xmax Then Xmax = X
                    If X < Xmin Then Xmin = X
                    bw.Write(X)
                    Y = Lines(N).GLPoints(J).lat
                    If Y > Ymax Then Ymax = Y
                    If Y < Ymin Then Ymin = Y
                    bw.Write(Y)
                Next
                ' now the ZZZZZZZZZZZZZZZZZ
                ptrMid = CInt(fs.Position)          ' need to return here!
                bw.Seek(16, SeekOrigin.Current)     ' leave space for Zmin & Zmax
                Zmin = 100000 : Zmax = -100000
                For J = 1 To nPoints
                    Z = Lines(N).GLPoints(J).alt
                    If Z > Zmax Then Zmax = Z
                    If Z < Zmin Then Zmin = Z
                    bw.Write(Z)
                Next
                If Xmin < XXmin Then XXmin = Xmin
                If Ymin < YYmin Then YYmin = Ymin
                If Zmin < ZZmin Then ZZmin = Zmin
                If Xmax > XXmax Then XXmax = Xmax
                If Ymax > YYmax Then YYmax = Ymax
                If Zmax > ZZmax Then ZZmax = Zmax
                bw.Seek((nPoints + 1) * 8, SeekOrigin.Current)     ' advance Mmin Mmax and nPoints M points
                bw.Write(0R)    ' go back 8 and write a double = O to terminate
                ptrEnd = CInt(fs.Position)
                recLen = (ptrEnd - ptrBegin) / 2    ' get the record lenght
                recLen = recLen - 4 ' contents lenght of record is less by 8 bytes
                RecLenght(REC) = recLen
                ' go back and fill the record header
                bw.Seek(ptrBegin, SeekOrigin.Begin)
                bw.Write(BigEndian(REC))
                bw.Write(BigEndian(recLen))
                bw.Write(ShapeType)
                bw.Write(Xmin)
                bw.Write(Ymin)
                bw.Write(Xmax)
                bw.Write(Ymax)
                bw.Write(1I)         ' 1 part !
                bw.Write(nPoints)    ' total number of points
                bw.Write(0I)         ' the index for the first point of the first part P(1)=0
                ' do not forget to fill Zmin & Zmax
                bw.Seek(ptrMid, SeekOrigin.Begin)
                bw.Write(Zmin)
                bw.Write(Zmax)
                ' and reposition the pointer for next record
                bw.Seek(ptrEnd, SeekOrigin.Begin)
            End If
        Next
        recLen = CInt(fs.Length)    ' recLen is free
        recLen = recLen / 2
        ' now complete  header
        bw.Seek(24, SeekOrigin.Begin)
        bw.Write(BigEndian(recLen))
        bw.Write(1000I)       ' version
        bw.Write(ShapeType)   ' shape type
        bw.Write(XXmin)
        bw.Write(YYmin)
        bw.Write(XXmax)
        bw.Write(YYmax)
        bw.Write(ZZmin)
        bw.Write(ZZmax)
        bw.Write(0R)
        bw.Write(0R)
        bw.Close()
        fs.Close()

        ' now create the SHX file!
        Dim SHXFile As String = Path.ChangeExtension(filename, ".SHX")
        If File.Exists(SHXFile) Then File.Delete(SHXFile)
        fs = New FileStream(SHXFile, FileMode.Create)
        bw = New BinaryWriter(fs)
        bw.Write(BigEndian(9994I))   ' write fixed initial number
        bw.Seek(20, SeekOrigin.Current)
        bw.Write(BigEndian(50 + nRecords * 4)) ' lenght here
        bw.Write(1000I)       ' version
        bw.Write(ShapeType)   ' shape type
        bw.Write(XXmin)
        bw.Write(YYmin)
        bw.Write(XXmax)
        bw.Write(YYmax)
        bw.Write(ZZmin)
        bw.Write(ZZmax)
        bw.Write(0R)     ' Mmin
        bw.Write(0R)     ' Mmax
        'bw.Seek(16, SeekOrigin.Current)  ' skip Mmin and Mmax
        For N = 1 To nRecords
            bw.Write(BigEndian(RecOffset(N)))
            bw.Write(BigEndian(RecLenght(N)))
        Next
        bw.Close()
        fs.Close()

        CreateShpAndShxFilesFromLines = True

        FrmStart.Cursor = System.Windows.Forms.Cursors.Default
        Exit Function
erro1:
        MsgBox("SBuilderX can not Export " & filename & " !", MsgBoxStyle.Exclamation)
        FrmStart.Cursor = System.Windows.Forms.Cursors.Default

    End Function

    Private Function BigEndian(ByVal n As Integer) As Integer

        ' returns the big endian representation of a Int32 number
        Dim BB As Byte
        Dim B() As Byte = BitConverter.GetBytes(n)
        BB = B(0)
        B(0) = B(3)
        B(3) = BB
        BB = B(1)
        B(1) = B(2)
        B(2) = BB
        BigEndian = BitConverter.ToInt32(B, 0)

    End Function

    Private Function NumberOfRecordsInSelectedLines(ByVal type As String) As Integer

        Dim N As Integer

        NumberOfRecordsInSelectedLines = 0

        For N = 1 To NoOfLines
            If Lines(N).Selected Then
                If Mid(Lines(N).Type, 1, 3) = type Then
                    NumberOfRecordsInSelectedLines = NumberOfRecordsInSelectedLines + 1
                End If
            End If
        Next

    End Function
    Private Function NumberOfRecordsInPolys() As Integer

        Dim N As Integer

        NumberOfRecordsInPolys = 0

        For N = 1 To NoOfPolys
            If Polys(N).NoOfChilds >= 0 Then
                NumberOfRecordsInPolys = NumberOfRecordsInPolys + 1
            End If
        Next

    End Function

    Private Function NumberOfRecordsInSelectedPolys(ByVal type As String) As Integer

        Dim N As Integer

        NumberOfRecordsInSelectedPolys = 0

        For N = 1 To NoOfPolys
            If Polys(N).Selected Then
                If Polys(N).NoOfChilds >= 0 Then
                    If Mid(Polys(N).Type, 1, 3) = type Then
                        NumberOfRecordsInSelectedPolys = NumberOfRecordsInSelectedPolys + 1
                    End If
                End If
            End If
        Next

    End Function


    Friend Sub MakeSHPPolys(ByVal filename As String, ByVal type As String)

        Dim N, K As Integer
        Dim Uiid, UiidTrail As String

        Dim thisType As String

        ' slopes
        Dim SY As Double = 0
        Dim SX As Double = 0

        ' to be used by SHX creation and DBFile creation
        Dim nRecords As Integer = NumberOfRecordsInSelectedPolys(type)

        Dim DBF As New DBFWriter

        If Not DBF.FileWriter(filename, nRecords) Then Exit Sub

        ' add the fields

        ' add fields
        If Not DBF.CreateField("Uuid", shp_CHARACTER, 38, 0) Then Exit Sub

        If type <> "HGX" Then
            If Not DBF.CreateField("Guid", shp_CHARACTER, 38, 0) Then Exit Sub
        End If
        If type = "HPX" Then
            If Not DBF.CreateField("SlopeX", shp_FLOAT, 13, 3) Then Exit Sub
            If Not DBF.CreateField("SlopeY", shp_FLOAT, 13, 3) Then Exit Sub
        End If

        ' append the fields
        If Not DBF.AppendFields() Then Exit Sub
        
        ' UiidTrail = "-0000-0000-0000-000000000000}"
        Dim myGuid As Guid
        myGuid = Guid.NewGuid
        UiidTrail = Right(myGuid.ToString("B"), 29).ToUpper

        ' populate the fields
        K = 0
        For N = 1 To NoOfPolys
            If Polys(N).Selected Then
                If Polys(N).NoOfChilds >= 0 Then
                    thisType = Mid(Polys(N).Type, 1, 3)
                    If thisType = type Or (thisType = "XXX" And type = "EXX") Then
                        K = K + 1
                        Uiid = Format(K, "{00000000") & UiidTrail
                        DBF.AddRecord(K, 1, Uiid)
                        If type <> "HGX" Then
                            DBF.AddRecord(K, 2, Polys(N).Guid)
                        End If
                        If type = "HPX" Then
                            If MakeSlopeXY Then
                                GetHPXSlopes(N, SX, SY)
                            End If
                            DBF.AddRecord(K, 3, SX.ToString("F3", Globalization.CultureInfo.InvariantCulture))
                            DBF.AddRecord(K, 4, SY.ToString("F3", Globalization.CultureInfo.InvariantCulture))
                        End If
                    End If
                End If
            End If
        Next

        ' close the DB file
        DBF.Close()

        ' now create the SHP and the SHX files
        CreateShpAndShxFilesFromPolys(filename, type)   ' only some polys will be exported as shp and shx

    End Sub

    Private Sub GetHPXSlopes(ByVal N As Integer, ByRef SX As Double, ByRef SY As Double)

        Dim Flag As Boolean
        Dim K As Integer
        Dim X As Double

        Flag = True
        X = Polys(N).GPoints(1).alt
        For K = 2 To Polys(N).NoOfPoints
            If X <> Polys(N).GPoints(K).alt Then
                Flag = False
                Exit For
            End If
        Next

        If Flag Then
            SX = 0
            SY = 0
            Exit Sub
        End If

        Dim NP, J As Integer
        Dim N1, N2, N3 As Integer
        Dim X1, X2, DX, DY, D As Double

        NP = Polys(N).NoOfPoints
        N1 = 1
        N2 = 1
        X1 = Polys(N).GPoints(1).lon
        X2 = Polys(N).GPoints(1).lon

        For J = 1 To NP
            If Polys(N).GPoints(J).lon < X1 Then
                N1 = J
                X1 = Polys(N).GPoints(J).lon
            End If
            If Polys(N).GPoints(J).lon > X2 Then
                N2 = J
                X2 = Polys(N).GPoints(J).lon
            End If
        Next

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

        Dim z00, z01, z02 As Double
        Dim x00, x01, x02 As Double
        Dim y00, y01, y02 As Double
        Dim a1, a2 As Double

        x01 = Polys(N).GPoints(N1).lon - Polys(N).GPoints(N2).lon
        x02 = Polys(N).GPoints(N1).lon - Polys(N).GPoints(N3).lon
        y01 = Polys(N).GPoints(N1).lat - Polys(N).GPoints(N2).lat
        y02 = Polys(N).GPoints(N1).lat - Polys(N).GPoints(N3).lat
        z01 = Polys(N).GPoints(N1).alt - Polys(N).GPoints(N2).alt
        z02 = Polys(N).GPoints(N1).alt - Polys(N).GPoints(N3).alt

        If y01 = 0 Then
            If x01 = 0 Then
                SX = 0
            Else
                SX = z01 / x01
            End If
        Else
            a1 = y02 / y01
            z00 = z01 * a1 - z02
            x00 = x01 * a1 - x02
            SX = z00 / x00
        End If


        If x01 = 0 Then
            If y01 = 0 Then
                SY = 0
            Else
                SY = z01 / y01
            End If
        Else
            a2 = x02 / x01
            z00 = z01 * a2 - z02
            y00 = y01 * a2 - y02
            SY = z00 / y00
        End If

    End Sub


    Friend Sub MakeSHPLines(ByVal filename As String, ByVal type As String)

        Dim Uiid, UiidTrail As String

        Dim Lanes As Byte    ' the SDK says it it is a UInt8
        Dim DirT As String
        Dim N, K As Integer

        ' to be used by SHX creation and DBFile creation
        Dim nRecords As Integer = NumberOfRecordsInSelectedLines(type)

        Dim DBF As New DBFWriter

        'If Not DBF.FileWriter(filename, NoOfLines) Then Exit Sub
        If Not DBF.FileWriter(filename, nRecords) Then Exit Sub

        ' add the fields
        If Not DBF.CreateField("Uuid", shp_CHARACTER, 38, 0) Then Exit Sub
        If type <> "FWX" Then        ' create Guid except for FWX  November 25 2017
            If Not DBF.CreateField("Guid", shp_CHARACTER, 38, 0) Then Exit Sub
        End If
        If type = "FWX" Then
            ' by Dick on November 24, 2017
            'If Not DBF.CreateField("NumberOfLanes", shp_NUMERIC, 1, 0) Then Exit Sub
            'If Not DBF.CreateField("TrafficDirection", shp_CHARACTER, 1, 0) Then Exit Sub
            If Not DBF.CreateField("NumberOfLa", shp_NUMERIC, 1, 0) Then Exit Sub
            If Not DBF.CreateField("TrafficDir", shp_CHARACTER, 1, 0) Then Exit Sub
        End If

        ' append the fields
        If Not DBF.AppendFields() Then Exit Sub

        ' UiidTrail = "-0000-0000-0000-000000000000}"
        Dim myGuid As Guid
        myGuid = Guid.NewGuid
        UiidTrail = Right(myGuid.ToString("B"), 29).ToUpper

        ' populate the fields
        K = 1
        For N = 1 To NoOfLines
            If Lines(N).Selected Then
                If Mid(Lines(N).Type, 1, 3) = type Then
                    Uiid = Format(K, "{00000000") & UiidTrail
                    DBF.AddRecord(K, 1, Uiid)
                    ' DBF.AddRecord(K, 2, Lines(N).Guid)
                    If type <> "FWX" Then
                        DBF.AddRecord(K, 2, Lines(N).Guid)
                    End If
                    If type = "FWX" Then
                        'Lanes = CByte(Mid(Lines(N).Type, 4, 1))   ' change from Cint() to CByte() in November 2017
                        'DirT = Mid(Lines(N).Type, 5, 1)
                        'DBF.AddRecord(K, 3, Lanes)
                        'DBF.AddRecord(K, 4, DirT)
                        ' after Dick proposal on November 24/25 2017
                        'If Mid(Lines(N).Type, 4, 1) = "" Then Lanes = 2 Else Lanes = CByte(Mid(Lines(N).Type, 4, 1))   ' change from Cint() to CByte() in November 2017
                        'If Mid(Lines(N).Type, 5, 1) = "" Then DirT = "F" Else DirT = Mid(Lines(N).Type, 5, 1)
                        ' then changed by Luis
                        If Mid(Lines(N).Type, 4, 1) = "" Then Lanes = DefaultNoOfLanes Else Lanes = CByte(Mid(Lines(N).Type, 4, 1))   ' change from Cint() to CByte() in November 2017
                        If Mid(Lines(N).Type, 5, 1) = "" Then DirT = DefaultTrafficDir Else DirT = Mid(Lines(N).Type, 5, 1)
                        DBF.AddRecord(K, 2, Lanes)
                        DBF.AddRecord(K, 3, DirT)
                    End If
                    K = K + 1
                End If
            End If
        Next

        ' close the DB file
        DBF.Close()

        ' now create the SHP and the SHX files
        CreateShpAndShxFilesFromLines(filename, type)   ' only selected lines will be exported as shp and shx

    End Sub

End Module


