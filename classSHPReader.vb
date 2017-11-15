Public Class SHPReader

    ' Private pointer As Integer      ' to pointer to shape file

    Private pointerXY As Integer
    Private pointerZ As Integer

    Private _shpLen As Integer      ' total number of bytes of the shape file

    Private buffer() As Byte        ' array of bytes to hold the contents of the SHP file
    Private shxbuffer() As Byte     ' array of bytes to hold the contents of the SHX file

    Private RecInd() As Integer     ' RecInd(n) holds the pointer to the nth Record in the SHP file

    Private _Begins() As Integer    ' _Begins(n) = pointer to the first point of nth part

    Private _Points() As Double_XYZ


    Structure Double_XYZ
        Dim X As Double
        Dim Y As Double
        Dim Z As Double
    End Structure

    Public ReadOnly Property Points(ByVal N As Integer) As Double_XYZ
        Get
            Return _Points(N)
        End Get
    End Property

    Private _NumPoints As Integer
    Public ReadOnly Property NumPoints As Integer
        Get
            Return _NumPoints
        End Get
    End Property

    Private _NumParts As Integer
    Public ReadOnly Property NumParts As Integer
        Get
            Return _NumParts
        End Get
    End Property

    Public ReadOnly Property Begins(ByVal N As Integer) As Integer
        Get
            Return _Begins(N)
        End Get
    End Property


    Private _shpType As Integer
    Public ReadOnly Property shpType As Integer
        Get
            Return _shpType
        End Get
    End Property

    Private _xMin As Double
    Public ReadOnly Property xMin As Double
        Get
            Return _xMin
        End Get
    End Property

    Private _xMax As Double
    Public ReadOnly Property xMax As Double
        Get
            Return _xMax
        End Get
    End Property

    Private _yMin As Double
    Public ReadOnly Property yMin As Double
        Get
            Return _yMin
        End Get
    End Property

    Private _yMax As Double
    Public ReadOnly Property yMax As Double
        Get
            Return _yMax
        End Get
    End Property

    Private _recordCount As Integer
    Public ReadOnly Property recordCount As Double
        Get
            Return _recordCount
        End Get
    End Property

    Public Function FileReader(ByVal filename As String) As Boolean

        FileReader = False
        On Error GoTo erro1
        Dim N, M As Integer

        buffer = My.Computer.FileSystem.ReadAllBytes(filename)

        N = BigEndian(BitConverter.ToInt32(buffer, 0))
        If N <> 9994 Then Exit Function

        _shpLen = BigEndian(BitConverter.ToInt32(buffer, 24))
        _shpType = BitConverter.ToInt32(buffer, 32)
        _xMin = BitConverter.ToDouble(buffer, 36)
        _yMin = BitConverter.ToDouble(buffer, 44)
        _xMax = BitConverter.ToDouble(buffer, 52)
        _yMax = BitConverter.ToDouble(buffer, 60)

        ' now go to the SHX file 
        Dim shxfile As String = Path.ChangeExtension(filename, ".shx")
        shxbuffer = My.Computer.FileSystem.ReadAllBytes(shxfile)
        N = shxbuffer.Length - 100    ' total number of bytes - the 100 from the header
        _recordCount = N / 8

        ReDim RecInd(_recordCount)
        For N = 1 To _recordCount
            M = 100 + (N - 1) * 8
            M = BitConverter.ToInt32(shxbuffer, M)
            RecInd(N) = 2 * BigEndian(M)
        Next

        FileReader = True

        Exit Function
erro1:

    End Function

    Public Function GetInfo(ByVal filename As String) As Boolean

        GetInfo = False
        On Error GoTo erro1
        Dim N, M As Integer

        ' read first 100 bytes of SHP file
        Dim header(100) As Byte
        Dim fs As New FileStream(filename, FileMode.Open, FileAccess.Read)
        fs.Read(header, 0, 100)
        fs.Close()

        N = BigEndian(BitConverter.ToInt32(header, 0))
        If N <> 9994 Then Exit Function

        _shpLen = BigEndian(BitConverter.ToInt32(header, 24))
        _shpType = BitConverter.ToInt32(header, 32)
        _xMin = BitConverter.ToDouble(header, 36)
        _yMin = BitConverter.ToDouble(header, 44)
        _xMax = BitConverter.ToDouble(header, 52)
        _yMax = BitConverter.ToDouble(header, 60)

        ' now go to the SHX file 
        Dim shxfile As String = Path.ChangeExtension(filename, ".shx")
        shxbuffer = My.Computer.FileSystem.ReadAllBytes(shxfile)
        N = shxbuffer.Length - 100    ' total number of bytes - the 100 from the header
        _recordCount = N / 8

        ReDim RecInd(_recordCount)
        For N = 1 To _recordCount
            M = 100 + (N - 1) * 8
            M = BitConverter.ToInt32(shxbuffer, M)
            RecInd(N) = 2 * BigEndian(M)
        Next

        GetInfo = True

        Exit Function
erro1:

    End Function

    Public Sub MoveTo(ByVal N As Integer)

        Dim pointer As Integer
        Dim P As Integer
        pointer = RecInd(N) + 44       ' 36 plus 8 from the record header
        _NumParts = BitConverter.ToInt32(buffer, pointer)
        pointer = pointer + 4
        _NumPoints = BitConverter.ToInt32(buffer, pointer)

        ReDim _Begins(_NumParts)
        ReDim _Points(_NumPoints)
        For P = 1 To _NumParts
            pointer = pointer + 4
            _Begins(P) = BitConverter.ToInt32(buffer, pointer)
        Next

        ' leaves the pointer for the first point
        pointerXY = pointer + 4
        pointerZ = pointerXY + (_NumPoints + 1) * 16

        If _shpType = 3 Or shpType = 5 Then
            For P = 1 To _NumPoints
                _Points(P).X = BitConverter.ToDouble(buffer, pointerXY)
                _Points(P).Y = BitConverter.ToDouble(buffer, pointerXY + 8)
                pointerXY = pointerXY + 16
            Next
        Else
            For P = 1 To _NumPoints
                _Points(P).X = BitConverter.ToDouble(buffer, pointerXY)
                _Points(P).Y = BitConverter.ToDouble(buffer, pointerXY + 8)
                _Points(P).Z = BitConverter.ToDouble(buffer, pointerZ)
                pointerXY = pointerXY + 16
                pointerZ = pointerZ + 8
            Next
        End If



    End Sub

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

    Public Sub Close()

        ' does nothing
        ReDim buffer(0)

    End Sub

End Class
