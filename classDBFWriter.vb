Public Class DBFWriter

    Private nFields As Int16 = 0
    Private fileIsOpened As Boolean = False
    Private headerDone As Boolean = False
    Private nBytesInRecord As Int16
    Private NoOfRecords As Integer = 0
    Private NoOfBytesInHeader As Int16

    Private fs As FileStream
    Private bw As BinaryWriter

    Private Const shp_CHARACTER As Byte = 67   ' C
    Private Const shp_NUMERIC As Byte = 78     ' N
    Private Const shp_FLOAT As Byte = 70    ' F

    Structure Field
        Dim Name As String
        Dim Type As Byte
        Dim Lenght As Byte
        Dim DecCount As Byte
    End Structure

    Private Fields(128) As Field
    Private FieldStart(128) As Integer      ' points to the start of the field in a record

    Public Function FileWriter(ByVal filename As String, ByVal nRecords As Integer) As Boolean

        On Error GoTo erro

        FileWriter = False
        headerDone = False
        fileIsOpened = False
        nBytesInRecord = 0
        NoOfRecords = 0

        Dim DBFile As String = Path.ChangeExtension(filename, ".DBF")
        If File.Exists(DBFile) Then File.Delete(DBFile)
        fs = New FileStream(DBFile, FileMode.Create)
        bw = New BinaryWriter(fs)

        Dim today As Date = Now
        bw.Write(CByte(3))
        bw.Write(CByte((today.Year) - 1900))
        bw.Write(CByte(today.Month))
        bw.Write(CByte(today.Day))
        bw.Write(nRecords)           ' byte 4 - 7

        ' accounts for the leading space on each record note that
        ' FieldStart(2) = FieldStart(1) + Fields(1).Lenght and so on ...
        FieldStart(1) = 1

        NoOfRecords = nRecords
        FileWriter = True
        fileIsOpened = True
        Exit Function
erro:
        MsgBox("Could not create database file!", MsgBoxStyle.Exclamation)
        fileIsOpened = False
        bw.Close()
        fs.Close()
        Exit Function

    End Function

    Public Function CreateField(ByVal name As String, ByVal type As Byte, len As Byte, ByVal dec As Byte) As Boolean

        Dim N As Integer

        CreateField = False
        On Error GoTo erro

        If Not fileIsOpened Then Exit Function
        If headerDone Then Exit Function
        If type <> shp_CHARACTER Then
            If type <> shp_FLOAT Then
                If type <> shp_NUMERIC Then Exit Function
            End If
        End If

        ' validate arguments
        If type = shp_CHARACTER Then dec = 0
        If len = 0 Then Exit Function
        If (len - dec) < 1 Then Exit Function

        ' now start the writing
        Dim ptr As Integer = (nFields + 1) * 32
        name = Trim(name)           ' remove any trailing or leading spaces
        N = name.Length
        If N > 11 Then
            name = Left(name, 11) : N = 11
        End If
        bw.Seek(ptr, SeekOrigin.Begin)
        Dim b() As Byte      ' binary.writer(string) also rights the len of string :-(
        b = System.Text.Encoding.UTF8.GetBytes(name)   ' so transform string into array of bytes
        bw.Write(b)
        bw.Seek(11 - N, SeekOrigin.Current)
        bw.Write(type)            ' byte 11
        bw.Seek(4, SeekOrigin.Current)
        bw.Write(len)             ' byte 16
        bw.Write(dec)             ' byte 17

        nFields = nFields + 1            ' array Fields(0) and StartField(0) is ignored!
        Fields(nFields).Name = name
        Fields(nFields).Type = type
        Fields(nFields).Lenght = len
        Fields(nFields).DecCount = dec

        FieldStart(nFields + 1) = FieldStart(nFields) + len

        nBytesInRecord = nBytesInRecord + len

        CreateField = True

        Exit Function

erro:
        MsgBox("Could not create database field: " & name & "!", MsgBoxStyle.Exclamation)
        CreateField = False

    End Function

    Public Function AppendFields() As Boolean

        AppendFields = False
        headerDone = False
        If nFields = 0 Then Exit Function

        nBytesInRecord = nBytesInRecord + 1      ' there is a leading space on each record!

        On Error GoTo erro

        ' point to bytes 8-9 
        bw.Seek(8, SeekOrigin.Begin)
        ' calculate number of bytes in header
        NoOfBytesInHeader = 32 * (nFields + 1) + 1   '  add one for the terminator 13
        bw.Write(NoOfBytesInHeader)
        bw.Write(nBytesInRecord)
        bw.Seek(NoOfBytesInHeader - 1, SeekOrigin.Begin)
        bw.Write(CByte(13))  ' add the terminator for the header
        ' fill the table with spaces
        Dim mySpaces As String = Space(NoOfRecords * nBytesInRecord)
        Dim b() As Byte      ' binary.writer(string) also rights the len of string :-(
        b = System.Text.Encoding.UTF8.GetBytes(mySpaces)   ' so transform string into array of bytes
        bw.Write(b)
        bw.Write(CByte(26))    ' 26 is 1A the terminator for the file

        ReDim Preserve FieldStart(nFields)  ' it will eliminate the last field as (field + 1) will not appear
        ReDim Preserve Fields(nFields)
        AppendFields = True
        headerDone = True
        Exit Function
erro:
        MsgBox("Could not append database fields!", MsgBoxStyle.Exclamation)
        AppendFields = False
        headerDone = False
    End Function

    Public Function AddRecord(ByVal record As Integer, ByVal field As Integer, ByVal value As String) As Boolean

        ' when calling and for numeric or floating types 
        ' value should be passed as a string! No conversion made here!

        AddRecord = False
        On Error GoTo erro

        Dim b() As Byte   ' to strip the lenght at the start of the string

        If Not headerDone Then Exit Function
        If field > nFields Then Exit Function

        Dim myLen As Integer = CInt(Fields(field).Lenght)

        Dim ptr As Integer = NoOfBytesInHeader + (nBytesInRecord) * (record - 1) + FieldStart(field)
        bw.Seek(ptr, SeekOrigin.Begin)

        value = Trim(value)
        value = Left(value, myLen)   ' just to make sure the string can go to the allocated space
        b = System.Text.Encoding.UTF8.GetBytes(value)   ' transform string into array of bytes
        bw.Write(b)

        AddRecord = True
        Exit Function
erro:
    End Function

    Public Sub Close()

        bw.Close()
        fs.Close()

    End Sub

End Class
