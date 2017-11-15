Imports System.IO

Public Class frmSCREEN

    Private Screen As New Bitmap(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height)
    Private Screensize As New Size(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height)

    Private MouseIsDown As Boolean = False
    Private X0 As Integer = 0
    Private Y0 As Integer = 0
    Private X1 As Integer = 0
    Private Y1 As Integer = 0
    Private DX As Integer = 0, DY As Integer = 0
    Private PX, PY As Integer

    Private _myCapture As Bitmap

    Friend ReadOnly Property MyCapture() As Bitmap
        Get
            Return _myCapture
        End Get
    End Property

    Private Sub FrmSCREEN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Dim KeyCode As Keys = e.KeyCode

        If KeyCode = Keys.Escape Then DialogResult = System.Windows.Forms.DialogResult.Cancel

        If KeyCode = Keys.Space Then
            If DX < 5 Then Exit Sub
            If DY < 5 Then Exit Sub
            UpDateDisplay()
            Dim myScreen As New Bitmap(DX, DY)
            Dim GS As Graphics = Graphics.FromImage(myScreen)
            GS.CopyFromScreen(New Point(PX, PY), New Point, New Size(DX, DY)) 'Takes a screen shot of the screen
            GS.Dispose()
            _myCapture = myScreen
            DialogResult = System.Windows.Forms.DialogResult.OK
        End If

    End Sub


    Private Sub FrmSCREEN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim A As String = "  You are in screen capture mode! Select a region with the mouse and press" & vbCrLf
        A = A & " <Esc> (or right click) to cancel or <Space> to capture the selected region."

        Dim GS As Graphics = Graphics.FromImage(Screen)
        Dim drawFont As New Font("Arial", 10)

        GS.CopyFromScreen(New Point, New Point, Screensize) 'Takes a screen shot of the screen
        GS.FillRectangle(Brushes.Beige, New Rectangle(0, 0, 460, 45))
        GS.DrawString(A, drawFont, Brushes.Black, 0, 5)
        drawFont.Dispose()
        GS.Dispose()

        WindowState = FormWindowState.Maximized
        FormBorderStyle = Windows.Forms.FormBorderStyle.None
        BackgroundImage = Screen

        Cursor = New System.Windows.Forms.Cursor(AppPath & "\Tools\Cursors\grab.cur")

    End Sub

    Private Sub FrmSCREEN_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown

        MouseIsDown = False
        Dim Button As Integer = e.Button \ &H100000

        If Button = 2 Then DialogResult = System.Windows.Forms.DialogResult.Cancel

        If Button = 1 Then
            X0 = e.X
            Y0 = e.Y
            MouseIsDown = True
        End If

    End Sub

    Private Sub FrmSCREEN_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove

        If Not MouseIsDown Then Exit Sub

        X1 = e.X
        Y1 = e.Y
        DrawSelectBox(X1, Y1)

    End Sub

    Private Sub FrmSCREEN_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp

        'If Not MouseIsDown Then Exit Sub
        MouseIsDown = False

    End Sub


    Private Sub DrawSelectBox(ByVal X As Integer, ByVal Y As Integer)

        UpDateDisplay()

        Dim p As New System.Drawing.Pen(Color.Red)
        Dim g As System.Drawing.Graphics
        p.DashStyle = Drawing2D.DashStyle.Dash
        g = CreateGraphics()
        DX = X - X0
        DY = Y - Y0
        PX = X0
        PY = Y0
        If X < X0 Then
            DX = X0 - X
            PX = X
        End If
        If Y < Y0 Then
            DY = Y0 - Y
            PY = Y
        End If
        g.DrawRectangle(p, New Rectangle(PX, PY, DX, DY))
        p.Dispose()
        g.Dispose()

    End Sub

    Private Sub UpDateDisplay()
        Dim gr As Graphics = CreateGraphics()
        gr.DrawImageUnscaled(Screen, New Point(0, 0))   'copy buffer to display
        gr.Dispose()
    End Sub


End Class
