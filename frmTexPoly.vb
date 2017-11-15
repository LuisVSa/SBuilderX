


Public Class frmTexPoly

    Private Pts() As Point
    Private NoOfPts As Integer
    Private SelPt As Integer
    Private MoveON As Boolean
    Private MouseDownDone As Boolean = False
    Private MoveDir As Integer '1 east  2 south  3 west 4 north
    Private imgBuffer As Bitmap = New Bitmap(512, 512)
    Private AuxX As Integer
    Private AuxY As Integer
    Private IsInit As Boolean = True

    Private Sub FrmTexPoly_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Me.Load

        Dim a As String

        a = "SBuilderX - Set Tying Points for Texture "
        a = a & UCase(PolyTex)
        Text = a


        Dim BmpPath As String = AppPath & "\Tools\Work\temp.bmp"
        Dim bmp As Image = System.Drawing.Image.FromFile(BmpPath)
        Dim cpy As New Bitmap(bmp)
        bmp.Dispose()
        imgBuffer = cpy

        String2Pts()
        ShowCoordinates()

        IsInit = False

    End Sub

    Private Sub String2Pts()

        Dim N, K As Integer
        Dim a, b As String

        NoOfPts = Polys(PolyTexIndex).NoOfPoints

        ReDim Pts(NoOfPts)

        a = PolyTexString

        SelPt = 1

        For N = 1 To NoOfPts
            K = InStr(1, a, "//")
            b = Mid(a, 1, K - 1)
            a = Mid(a, K + 2)
            K = InStr(1, b, ",")
            Pts(N).X = 2 * CInt(Mid(b, 1, K - 1))
            Pts(N).Y = 512 - 2 * CInt(Mid(b, K + 1))
        Next N

    End Sub

    Private Sub CmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Dispose()

    End Sub

    Private Sub DoDD()

        Dim N As Integer

        N = CInt(txtPY.Text)
        N = N - 1
        If N < 0 Then N = 0

        Pts(SelPt).Y = T2PY(N)
        DisplayPoly()

        ShowCoordinates()

    End Sub

    Private Sub CmdDD_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles cmdDD.MouseDown

        DoDD()
        MoveDir = 2
        Timer1.Interval = 512
        Timer1.Enabled = True

    End Sub

    Private Sub CmdDD_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles cmdDD.MouseUp

        Timer1.Enabled = False

    End Sub

    Private Sub DoLL()

        Dim N As Integer

        N = CInt(txtPX.Text)
        N = N - 1
        If N < 0 Then N = 0

        Pts(SelPt).X = T2PX(N)
        DisplayPoly()

        ShowCoordinates()

    End Sub

    Private Sub CmdLL_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles cmdLL.MouseDown

        DoLL()
        MoveDir = 3
        Timer1.Interval = 512
        Timer1.Enabled = True

    End Sub

    Private Sub CmdLL_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles cmdLL.MouseUp

        Timer1.Enabled = False

    End Sub

    Private Sub CmdOK_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click


        Dim N As Integer

        PolyTexString = ""

        For N = 1 To NoOfPts

            PolyTexString = PolyTexString & CStr(Pt2X(N)) & ","
            PolyTexString = PolyTexString & CStr(Pt2Y(N)) & "//"

        Next N

        Dispose()


    End Sub

    Private Sub CmdReset_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdReset.Click

        MakePolyTexString(PolyTexIndex, True)
        String2Pts()
        DisplayPoly()
        ShowCoordinates()

    End Sub

    Private Sub DoRR()

        Dim N As Integer

        N = CInt(txtPX.Text)
        N = N + 1
        If N > 256 Then N = 256

        Pts(SelPt).X = T2PX(N)
        DisplayPoly()

        ShowCoordinates()

    End Sub


    Private Sub DoUU()

        Dim N As Integer

        N = CInt(txtPY.Text)
        N = N + 1
        If N > 256 Then N = 256

        Pts(SelPt).Y = T2PY(N)
        DisplayPoly()

        ShowCoordinates()

    End Sub

    Private Sub CmdRR_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles cmdRR.MouseDown

        DoRR()
        MoveDir = 1
        Timer1.Interval = 512
        Timer1.Enabled = True

    End Sub


    Private Sub CmdRR_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles cmdRR.MouseUp

        Timer1.Enabled = False

    End Sub

    Private Sub CmdUU_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles cmdUU.MouseDown

        DoUU()
        MoveDir = 4
        Timer1.Interval = 512
        Timer1.Enabled = True

    End Sub

    Private Sub CmdUU_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles cmdUU.MouseUp

        Timer1.Enabled = False

    End Sub

    Private Sub DisplayPoly()


        Dim N As Integer
        Dim g As Graphics = imgText.CreateGraphics
        Dim myPen As New System.Drawing.Pen(Color.Black)
        Dim myBrush As New System.Drawing.SolidBrush(Color.White)

        g.DrawImage(imgBuffer, 0, 0, 512, 512)
        myPen.Color = Color.Black
        For N = 1 To NoOfPts - 1
            g.DrawLine(myPen, Pts(N), Pts(N + 1))
        Next
        g.DrawLine(myPen, Pts(NoOfPts), Pts(1))

        For N = 1 To NoOfPts
            If N = SelPt Then
                myPen.Color = Color.Green
            Else
                myPen.Color = Color.Black
            End If
            g.FillRectangle(myBrush, Pts(N).X - 3, Pts(N).Y - 3, 6, 6)
            g.DrawRectangle(myPen, Pts(N).X - 3, Pts(N).Y - 3, 6, 6)
        Next

        myBrush.Dispose()
        myPen.Dispose()
        g.Dispose()

    End Sub

    '    'UPGRADE_WARNING: Event txtPX.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
    '    Private Sub TxtPX_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPX.TextChanged

    '        Dim N, N0, N1 As Integer

    '        If NoChange Then Exit Sub

    '        N0 = Pts(SelPt).x

    '        On Error GoTo erro

    '        N = CInt(txtPX.Text)
    '        If N > 256 Then N = 256
    '        If N < 0 Then N = 0

    '        Pts(SelPt).X = T2PX(N)
    '        DisplayPoly()

    '        NoChange = True
    '        ShowCoordinates()
    '        NoChange = False

    '        Exit Sub

    'erro:
    '        Pts(SelPt).x = N0
    '        txtPX.Text = CStr(Pt2X(SelPt))
    '        NoChange = False

    '    End Sub

    'UPGRADE_WARNING: Event txtPY.TextChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
    '    Private Sub TxtPY_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPY.TextChanged

    '        Dim N, N0, N1 As Integer

    '        If NoChange Then Exit Sub

    '        N0 = Pts(SelPt).Y

    '        On Error GoTo erro

    '        N = CInt(txtPY.Text)
    '        If N > 256 Then N = 256
    '        If N < 0 Then N = 0

    '        Pts(SelPt).Y = T2PY(N)
    '        DisplayPoly()

    '        NoChange = True
    '        ShowCoordinates()
    '        NoChange = False

    '        Exit Sub

    'erro:
    '        Pts(SelPt).Y = N0
    '        txtPY.Text = CStr(Pt2Y(SelPt))
    '        NoChange = False

    '    End Sub

    Private Sub ShowCoordinates()

        IsInit = True
        txtPY.Text = CStr(Pt2Y(SelPt))
        txtPX.Text = CStr(Pt2X(SelPt))
        Frame1.Text = "Point # " & CStr(SelPt)
        IsInit = False

    End Sub

    Private Function T2PX(ByVal T As Integer) As Integer

        T2PX = 2 * T

    End Function
    Private Function T2PY(ByVal T As Integer) As Integer

        T2PY = 512 - 2 * T

    End Function
    Private Function Pt2X(ByVal PT As Integer) As Integer

        Pt2X = CInt(Pts(PT).X / 2)

    End Function

    Private Function Pt2Y(ByVal PT As Integer) As Integer

        Pt2Y = CInt(512 - Pts(PT).Y / 2)

    End Function

    Private Sub Timer1_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles Timer1.Elapsed

        Dim N As Double

        N = Timer1.Interval
        N = N / 2
        If N < 8 Then N = 8

        If MoveDir = 1 Then DoRR()
        If MoveDir = 2 Then DoDD()
        If MoveDir = 3 Then DoLL()
        If MoveDir = 4 Then DoUU()

        Timer1.Interval = N

    End Sub



    Private Sub FrmTexPoly_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        DisplayPoly()
    End Sub

    Private Sub ImgText_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles imgText.MouseDown

        Dim x As Integer = e.X
        Dim y As Integer = e.Y

        Dim PY, PX, N As Integer

        If x < 0 Or x > 512 Then Exit Sub
        If y < 0 Or y > 512 Then Exit Sub

        If MouseDownDone Then Exit Sub

        MoveON = False
        MouseDownDone = True

        PX = x
        PY = y

        For N = 1 To NoOfPts
            If System.Math.Abs(PX - Pts(N).X) > 3 Then GoTo next_N
            If System.Math.Abs(PY - Pts(N).Y) > 3 Then GoTo next_N
            MoveON = True
            SelPt = N
            DisplayPoly()
            ShowCoordinates()
            Exit Sub
next_N:
        Next N


    End Sub


    Private Sub ImgText_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles imgText.MouseMove


        If MoveON = False Then Exit Sub

        Dim x As Integer = e.X
        Dim y As Integer = e.Y

        If x = AuxX And y = AuxY Then Exit Sub

        If y < 0 Then y = 0
        If y > 511 Then y = 511

        If x < 0 Then x = 0
        If x > 511 Then x = 511

        Pts(SelPt).X = x
        Pts(SelPt).Y = y

        AuxX = x
        AuxY = y

        DisplayPoly()
        ShowCoordinates()


    End Sub


    Private Sub ImgText_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles imgText.MouseUp
        MouseDownDone = False
        MoveON = False
    End Sub

    Private Sub TxtPX_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPX.TextChanged

        If IsInit Then Exit Sub

        Dim N, N0, N1 As Integer

        N0 = Pts(SelPt).X

        On Error GoTo erro

        N = CInt(txtPX.Text)
        If N > 256 Then N = 256
        If N < 0 Then N = 0

        Pts(SelPt).X = T2PX(N)
        DisplayPoly()

        ShowCoordinates()

        Exit Sub

erro:
        Pts(SelPt).X = N0
        txtPX.Text = CStr(Pt2X(SelPt))


    End Sub

    Private Sub TxtPY_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPY.TextChanged

        If IsInit Then Exit Sub

        Dim N, N0, N1 As Integer

        N0 = Pts(SelPt).Y

        On Error GoTo erro

        N = CInt(txtPY.Text)
        If N > 256 Then N = 256
        If N < 0 Then N = 0

        Pts(SelPt).Y = T2PY(N)
        DisplayPoly()

        ShowCoordinates()

        Exit Sub

erro:
        Pts(SelPt).Y = N0
        txtPY.Text = CStr(Pt2Y(SelPt))

    End Sub
End Class