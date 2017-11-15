
Public Class MDLReader

    Private TypeValue As Integer
    Public ReadOnly Property Type() As Integer
        Get
            Return TypeValue

        End Get
    End Property

    Private GuidValue As String
    Public ReadOnly Property Guid() As String
        Get
            Return GuidValue
        End Get
    End Property

    Private NameValue As String
    Public ReadOnly Property Name() As String
        Get
            NameValue = NameValue
            Return NameValue
        End Get
    End Property

    Private BBoxValue As MDLBBox
    Public ReadOnly Property BBox() As MDLBBox
        Get
            Return BBoxValue
        End Get
    End Property

    Structure MDLBBox
        Dim Xmin As Single
        Dim Xmax As Single
        Dim Ymin As Single
        Dim Ymax As Single
        Dim Zmin As Single
        Dim Zmax As Single
    End Structure


    Public ReadOnly Property Width() As Single
        Get
            Return CSng(Math.Round(BBoxValue.Xmax - BBoxValue.Xmin, 2))
        End Get
    End Property

    Public ReadOnly Property Lenght() As Single
        Get
            Dim X As Double = CDbl(BBoxValue.Zmax - BBoxValue.Zmin)
            Return CSng(Math.Round(X, 2))
        End Get
    End Property

    Public Function Read(ByVal reader As BinaryReader) As Boolean

        Read = False
        Dim GuidOK As Boolean = False
        Dim NameOK As Boolean = False
        Dim BoxOK As Boolean = False
        Dim N As Integer
        Dim R As String

        Dim B As Byte()
        ReDim B(16)
        NameValue = ""

        Try

            If reader.ReadChars(4) <> "RIFF" Then Exit Function

            reader.ReadBytes(4)
            R = reader.ReadChars(4)
            If R = "MDLX" Then
                TypeValue = 2
            ElseIf R = "MDL9" Then
                TypeValue = 1
            Else
                Exit Function
            End If

            R = reader.ReadChars(4)
            If R <> "MDLH" Then Exit Function

            N = CInt(reader.ReadUInt32())
            reader.ReadBytes(N)

            Do
                R = reader.ReadChars(4)
                If R = "MDLG" Then  ' only happens in FSX
                    reader.ReadBytes(4)
                    B = reader.ReadBytes(16)
                    Dim G As New Guid(B)
                    GuidValue = G.ToString("B")
                    GuidOK = True
                ElseIf R = "MDLN" Then ' only happens in FSX
                    N = CInt(reader.ReadUInt32())
                    NameValue = reader.ReadChars(N)
                    NameValue = NameValue.Substring(0, N - 1)
                    NameOK = True
                ElseIf R = "BBOX" Then
                    N = CInt(reader.ReadUInt32())
                    BBoxValue.Xmin = reader.ReadSingle
                    BBoxValue.Ymin = reader.ReadSingle
                    BBoxValue.Zmin = reader.ReadSingle
                    BBoxValue.Xmax = reader.ReadSingle
                    BBoxValue.Ymax = reader.ReadSingle
                    BBoxValue.Zmax = reader.ReadSingle
                    reader.ReadBytes(N - 24)
                    BoxOK = True
                    If TypeValue = 1 Then Exit Do
                Else
                    N = CInt(reader.ReadUInt32())
                    reader.ReadBytes(N)
                End If
                If GuidOK And NameOK And BoxOK Then Exit Do
            Loop

            Read = True

        Catch ex As Exception

        End Try

    End Function

End Class
