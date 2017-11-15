Public Class DBFReader

    Public Enum FieldType
        FTString
        FTInteger
        FTDouble
        FTLogical
        FTInvalid
    End Enum

    Structure Field
        Dim Name As String
        Dim Type As FieldType
        Dim Lenght As Integer
        Dim DecCount As Integer
    End Structure

    Private Fields(128) As Field

    Private NoOfRecords As Integer
    Public ReadOnly Property RecordCount As Integer
        Get
            Return NoOfRecords
        End Get
    End Property

    Private NoOfFields As Integer

    Public ReadOnly Property FieldCount As Integer
        Get
            Return NoOfFields
        End Get
    End Property

    Public ReadOnly Property FieldInfo(ByVal N As Integer) As Field
        Get
            Return Fields(N)
        End Get
    End Property

    Private BB(,) As Byte           ' each records is a row
    Private SF(128) As Integer      ' points to the start of the field in a record
    Private GR() As Boolean         ' if true it is a GoodRecord (not deleted flag)

    Public Function Attribute(ByVal R As Integer, ByVal F As Integer) As String

        Attribute = ""

        If R >= RecordCount Then Exit Function
        If F >= FieldCount Then Exit Function
        If Not GR(R) Then Exit Function

        Dim B As Byte
        Dim N, J, K As Integer
        J = SF(F)
        K = J + Fields(F).Lenght - 1
        For N = J To K
            B = BB(R, N)
            If B <> 32 Then
                Attribute = Attribute & Convert.ToChar(B)
            End If
        Next

    End Function


    Public Function FileReader(ByVal filename As String) As Boolean

        FileReader = False

        Dim myFile As String = Path.ChangeExtension(filename, ".dbf")
        Dim fs As New FileStream(myFile, FileMode.Open, FileAccess.Read)
        Dim reader As New BinaryReader(fs)

        Dim LenHeader, LenRecord As Integer

        Dim B As Byte
        Dim S As String
        Dim C, R As Integer

        C = 0 ' to count the size of the header
        Try
            If reader.ReadByte <> 3 Then Exit Function
            reader.ReadBytes(3)
            NoOfRecords = CInt(reader.ReadUInt32)
            ReDim GR(NoOfRecords - 1)
            LenHeader = reader.ReadUInt16
            LenRecord = reader.ReadUInt16
            reader.ReadBytes(20)
            NoOfFields = 0
            SF(NoOfFields) = 1
            C = 32 ' 32 bytes read
            Do
                B = reader.ReadByte
                C = C + 1
                If B = 13 Then Exit Do
                S = Convert.ToChar(B) & reader.ReadChars(10)
                Fields(NoOfFields).Name = Left(S, InStr(S, vbNullChar) - 1)
                S = reader.ReadChar
                If S = "C" Then
                    Fields(NoOfFields).Type = FieldType.FTString
                ElseIf S = "N" Then
                    Fields(NoOfFields).Type = FieldType.FTInteger
                    'ElseIf S = "O" Then
                ElseIf S = "F" Then       ' was O and I changed to F in November 2017! Needs testing!
                    Fields(NoOfFields).Type = FieldType.FTDouble
                ElseIf S = "L" Then
                    Fields(NoOfFields).Type = FieldType.FTLogical
                Else
                    Fields(NoOfFields).Type = FieldType.FTInvalid
                End If
                reader.ReadBytes(4)
                B = reader.ReadByte
                Fields(NoOfFields).Lenght = B
                SF(NoOfFields + 1) = B + SF(NoOfFields)
                Fields(NoOfFields).DecCount = reader.ReadByte
                reader.ReadBytes(14)
                NoOfFields = NoOfFields + 1
                C = C + 31
            Loop
            ' check if there is something before the records
            ' and if so advance the pointer
            If (LenHeader - C) > 0 Then
                reader.ReadBytes(LenHeader - C)
            End If

            ReDim Preserve Fields(NoOfFields - 1)
            ReDim Preserve SF(NoOfFields - 1)

            ReDim BB(RecordCount - 1, LenRecord - 1)

            For R = 0 To RecordCount - 1
                For C = 0 To LenRecord - 1
                    BB(R, C) = reader.ReadByte
                Next
                If BB(R, 0) = 32 Then
                    GR(R) = True
                Else
                    GR(R) = False
                End If
            Next

            FileReader = True
        Catch ex As Exception
            MsgBox(ex)
        End Try

        ' should I reaaly comment the following? October 2017
        ' reader.Close()
        fs.Close()

    End Function

    Public Sub Close()
        ReDim BB(0, 0)
        ReDim SF(0)
        ReDim Fields(0)
        ReDim GR(0)
    End Sub

End Class
