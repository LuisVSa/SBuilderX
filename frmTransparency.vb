Option Strict On
Option Explicit On

Imports System.Windows.Forms


Friend Class FrmTransparency

    Private Init As Boolean = True
    Private Alpha As Integer
    Private Red As Integer
    Private Green As Integer
    Private Blue As Integer


    Private Sub CmdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        ARGBColor = Color.FromArgb(Alpha, Red, Green, Blue)
        DialogResult = System.Windows.Forms.DialogResult.OK
        Dispose()

    End Sub

    Private Sub CmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Dispose()

    End Sub



    Private Sub FrmTransparency_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim myColor As Color

        'If POPType = "Line" Then myColor = Lines(POPIndex).Color
        'If POPType = "Poly" Then myColor = Polys(POPIndex).Color

        Alpha = ARGBColor.A
        Red = ARGBColor.R
        Green = ARGBColor.G
        Blue = ARGBColor.B

        UpDateValues()

    End Sub




    Private Sub FrmTransparency_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

        If Init Then Exit Sub

        Dim mycolor As Color = Color.FromArgb(Alpha, Red, Green, Blue)
        Dim mybrush1 As New System.Drawing.SolidBrush(mycolor)
        Dim mybrush2 As New System.Drawing.SolidBrush(Color.White)
        Dim mybrush3 As New System.Drawing.SolidBrush(Color.Black)
        Dim myPen As New System.Drawing.Pen(Color.Black)
        Dim drawFont As New System.Drawing.Font("Arial", 46)

        Dim g As Graphics
        g = e.Graphics

        g.FillRectangle(mybrush2, New Rectangle(15, 20, 110, 80))
        g.DrawString("OK", drawFont, mybrush3, 18, 28)
        g.FillRectangle(mybrush1, New Rectangle(15, 20, 110, 80))
        g.DrawRectangle(myPen, New Rectangle(15, 20, 110, 80))

        mybrush1.Dispose()
        mybrush2.Dispose()
        mybrush3.Dispose()
        drawFont.Dispose()
        myPen.Dispose()

    End Sub

    Private Sub BarA_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BarA.ValueChanged

        If Init Then Exit Sub
        Alpha = BarA.Value
        UpDateValues()

    End Sub

    Private Sub BarR_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BarR.ValueChanged

        If Init Then Exit Sub
        Red = BarR.Value
        UpDateValues()

    End Sub


    Private Sub BarG_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BarG.ValueChanged

        If Init Then Exit Sub
        Green = BarG.Value
        UpDateValues()

    End Sub


    Private Sub BarB_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BarB.ValueChanged

        If Init Then Exit Sub
        Blue = BarB.Value
        UpDateValues()

    End Sub

    Private Sub TxtA_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtA.TextChanged

        Dim EC As Integer = Alpha
        If Init Then Exit Sub
        Try
            Alpha = CInt(txtA.Text)
            If Alpha < 0 Then Alpha = 0
            If Alpha > 255 Then Alpha = 255
            UpDateValues()
        Catch ex As Exception
            Alpha = EC
            txtA.Text = EC.ToString
        End Try


    End Sub

    Private Sub TxtR_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtR.TextChanged

        Dim EC As Integer = Red
        If Init Then Exit Sub
        Try
            Red = CInt(txtR.Text)
            If Red < 0 Then Red = 0
            If Red > 255 Then Red = 255
            UpDateValues()
        Catch ex As Exception
            Red = EC
            txtR.Text = EC.ToString
        End Try

    End Sub

    Private Sub TxtG_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtG.TextChanged

        If Init Then Exit Sub
        Dim EC As Integer = Green
        Try
            Green = CInt(txtG.Text)
            If Green < 0 Then Green = 0
            If Green > 255 Then Green = 255
            UpDateValues()
        Catch ex As Exception
            Green = EC
            txtG.Text = EC.ToString
        End Try

    End Sub

    Private Sub TxtB_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtB.TextChanged

        If Init Then Exit Sub
        Dim EC As Integer = Blue
        Try
            Blue = CInt(txtB.Text)
            If Blue < 0 Then Blue = 0
            If Blue > 255 Then Blue = 255
            UpDateValues()
        Catch ex As Exception
            Blue = EC
            txtB.Text = EC.ToString
        End Try

    End Sub

    Private Sub UpDateValues()

        Init = True

        BarA.Value = Alpha
        BarR.Value = Red
        BarG.Value = Green
        BarB.Value = Blue

        txtA.Text = Alpha.ToString
        txtR.Text = Red.ToString
        txtG.Text = Green.ToString
        txtB.Text = Blue.ToString

        Init = False
        Refresh()

    End Sub


    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        SetThisColor(1)
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
        SetThisColor(2)
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        SetThisColor(3)
    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click
        SetThisColor(4)
    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click
        SetThisColor(5)
    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click
        SetThisColor(6)
    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click
        SetThisColor(7)
    End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click
        SetThisColor(8)
    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click
        SetThisColor(9)
    End Sub


    Private Sub Label10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label10.Click
        SetThisColor(10)
    End Sub

    Private Sub Label11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label11.Click
        SetThisColor(11)
    End Sub

    Private Sub Label12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label12.Click
        SetThisColor(12)
    End Sub

    Private Sub Label13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label13.Click
        SetThisColor(13)
    End Sub

    Private Sub Label14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label14.Click
        SetThisColor(14)
    End Sub

    Private Sub Label15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label15.Click
        SetThisColor(15)
    End Sub

    Private Sub Label16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label16.Click
        SetThisColor(16)
    End Sub

    Private Sub Label17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label17.Click
        SetThisColor(17)
    End Sub

    Private Sub Label18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label18.Click
        SetThisColor(18)
    End Sub

    Private Sub Label19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label19.Click
        SetThisColor(19)
    End Sub

    Private Sub Label20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label20.Click
        SetThisColor(20)
    End Sub

    Private Sub Label21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label21.Click
        SetThisColor(21)
    End Sub

    Private Sub Label22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label22.Click
        SetThisColor(22)
    End Sub

    Private Sub Label23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label23.Click
        SetThisColor(23)
    End Sub

    Private Sub Label24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label24.Click
        SetThisColor(24)
    End Sub

    Private Sub Label25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label25.Click
        SetThisColor(25)
    End Sub

    Private Sub Label26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label26.Click
        SetThisColor(26)
    End Sub

    Private Sub Label27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label27.Click
        SetThisColor(27)
    End Sub


    Sub SetThisColor(ByVal N As Integer)

        Dim myColor As Color

        Label1.BorderStyle = BorderStyle.None
        Label2.BorderStyle = BorderStyle.None
        Label3.BorderStyle = BorderStyle.None
        Label4.BorderStyle = BorderStyle.None
        Label5.BorderStyle = BorderStyle.None
        Label6.BorderStyle = BorderStyle.None
        Label7.BorderStyle = BorderStyle.None
        Label8.BorderStyle = BorderStyle.None
        Label9.BorderStyle = BorderStyle.None

        Label10.BorderStyle = BorderStyle.None
        Label11.BorderStyle = BorderStyle.None
        Label12.BorderStyle = BorderStyle.None
        Label13.BorderStyle = BorderStyle.None
        Label14.BorderStyle = BorderStyle.None
        Label15.BorderStyle = BorderStyle.None
        Label16.BorderStyle = BorderStyle.None
        Label17.BorderStyle = BorderStyle.None
        Label18.BorderStyle = BorderStyle.None
        Label19.BorderStyle = BorderStyle.None

        Label20.BorderStyle = BorderStyle.None
        Label21.BorderStyle = BorderStyle.None
        Label22.BorderStyle = BorderStyle.None
        Label23.BorderStyle = BorderStyle.None
        Label24.BorderStyle = BorderStyle.None
        Label25.BorderStyle = BorderStyle.None
        Label26.BorderStyle = BorderStyle.None
        Label27.BorderStyle = BorderStyle.None

        Select Case N
            Case 1
                Label1.BorderStyle = BorderStyle.Fixed3D
                myColor = Label1.BackColor
            Case 2
                Label2.BorderStyle = BorderStyle.Fixed3D
                myColor = Label2.BackColor
            Case 3
                Label3.BorderStyle = BorderStyle.Fixed3D
                myColor = Label3.BackColor
            Case 4
                Label4.BorderStyle = BorderStyle.Fixed3D
                myColor = Label4.BackColor
            Case 5
                Label5.BorderStyle = BorderStyle.Fixed3D
                myColor = Label5.BackColor
            Case 6
                Label6.BorderStyle = BorderStyle.Fixed3D
                myColor = Label6.BackColor
            Case 7
                Label7.BorderStyle = BorderStyle.Fixed3D
                myColor = Label7.BackColor
            Case 8
                Label8.BorderStyle = BorderStyle.Fixed3D
                myColor = Label8.BackColor
            Case 9
                Label9.BorderStyle = BorderStyle.Fixed3D
                myColor = Label9.BackColor
            Case 10
                Label10.BorderStyle = BorderStyle.Fixed3D
                myColor = Label10.BackColor
            Case 11
                Label11.BorderStyle = BorderStyle.Fixed3D
                myColor = Label11.BackColor
            Case 12
                Label12.BorderStyle = BorderStyle.Fixed3D
                myColor = Label12.BackColor
            Case 13
                Label13.BorderStyle = BorderStyle.Fixed3D
                myColor = Label13.BackColor
            Case 14
                Label14.BorderStyle = BorderStyle.Fixed3D
                myColor = Label14.BackColor
            Case 15
                Label15.BorderStyle = BorderStyle.Fixed3D
                myColor = Label15.BackColor
            Case 16
                Label16.BorderStyle = BorderStyle.Fixed3D
                myColor = Label16.BackColor
            Case 17
                Label17.BorderStyle = BorderStyle.Fixed3D
                myColor = Label17.BackColor
            Case 18
                Label18.BorderStyle = BorderStyle.Fixed3D
                myColor = Label18.BackColor
            Case 19
                Label19.BorderStyle = BorderStyle.Fixed3D
                myColor = Label19.BackColor
            Case 20
                Label20.BorderStyle = BorderStyle.Fixed3D
                myColor = Label20.BackColor
            Case 21
                Label21.BorderStyle = BorderStyle.Fixed3D
                myColor = Label21.BackColor
            Case 22
                Label22.BorderStyle = BorderStyle.Fixed3D
                myColor = Label22.BackColor
            Case 23
                Label23.BorderStyle = BorderStyle.Fixed3D
                myColor = Label23.BackColor
            Case 24
                Label24.BorderStyle = BorderStyle.Fixed3D
                myColor = Label24.BackColor
            Case 25
                Label25.BorderStyle = BorderStyle.Fixed3D
                myColor = Label25.BackColor
            Case 26
                Label26.BorderStyle = BorderStyle.Fixed3D
                myColor = Label26.BackColor
            Case 27
                Label27.BorderStyle = BorderStyle.Fixed3D
                myColor = Label27.BackColor
        End Select

        Red = myColor.R
        Green = myColor.G
        Blue = myColor.B
        UpDateValues()

    End Sub

End Class
