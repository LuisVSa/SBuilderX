Imports System.Windows.Forms

Friend Class FrmFind

    Private LineInd As Integer
    Private PolyInd As Integer
    Private LatFind As Double
    Private LonFind As Double
    Public Search As String

    Private Sub CmdAll_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAll.Click


        Search = Trim(UCase(txtName.Text))

        If ckLines.Checked Then
            If FindLines(1) = 0 Then
                MsgBox("No Line found !")
                Exit Sub
            End If
        End If

        If ckLTypes.Checked Then
            If FindLTypes(1) = 0 Then
                MsgBox("No Line found !")
                Exit Sub
            End If
        End If

        If ckPolys.Checked Then
            If FindPolys(1) = 0 Then
                MsgBox("No Polygon found !")
                Exit Sub
            End If
        End If

        If ckPTypes.Checked Then
            If FindPTypes(1) = 0 Then
                MsgBox("No Polygon found !")
                Exit Sub
            End If
        End If

        Beep()

        LonDispCenter = LonFind
        LatDispCenter = LatFind
        SetDispCenter(0, 0)
        RebuildDisplay()

    End Sub

    Private Sub CmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        Dispose()
    End Sub

    Private Sub CmdNext_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdNext.Click


        Search = Trim(UCase(txtName.Text))

        If ckLines.Checked Then
            If FindLines(0) = 0 Then
                MsgBox("Line not found !")
                Exit Sub
            End If
        End If

        If ckLTypes.Checked Then
            If FindLTypes(0) = 0 Then
                MsgBox("Line not found !")
                Exit Sub
            End If
        End If

        If ckPolys.Checked Then
            If FindPolys(0) = 0 Then
                MsgBox("Polygon not found !")
                Exit Sub
            End If
        End If

        If ckPTypes.Checked Then
            If FindPTypes(0) = 0 Then
                MsgBox("Polygon not found !")
                Exit Sub
            End If
        End If

        Beep()

        LonDispCenter = LonFind
        LatDispCenter = LatFind
        SetDispCenter(0, 0)
        RebuildDisplay()

    End Sub

    Private Sub FrmFind_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        txtName.Text = Search

        LineInd = 0
        PolyInd = 0

    End Sub

    Private Function FindLines(ByVal All As Integer) As Integer

        Dim N As Integer
        Dim Flag As Boolean

        On Error GoTo erro1

        FindLines = 0

        If All = 1 Then LineInd = 0

        LineInd = LineInd + 1
        If LineInd = NoOfLines Then LineInd = 1

        For N = LineInd To NoOfLines

            If Search = "" Then
                Flag = False
                If Lines(N).Name = "" Then Flag = True
            Else
                Flag = InStr(1, UCase(Lines(N).Name), Search) > 0
            End If

            If Flag Then
                FindLines = N
                LineInd = N
                LatFind = (Lines(N).NLAT + Lines(N).SLAT) / 2
                LonFind = (Lines(N).ELON + Lines(N).WLON) / 2
                Lines(N).Selected = True
                NoOfLinesSelected = NoOfLinesSelected + 1
                If All = 0 Then Exit Function
            End If
        Next N
        If All = 1 Then Exit Function

        For N = 1 To LineInd

            If Search = "" Then
                Flag = False
                If Lines(N).Name = "" Then Flag = True
            Else
                Flag = InStr(1, UCase(Lines(N).Name), Search) > 0
            End If

            If Flag Then
                FindLines = N
                LineInd = N
                LatFind = (Lines(N).NLAT + Lines(N).SLAT) / 2
                LonFind = (Lines(N).ELON + Lines(N).WLON) / 2
                Lines(N).Selected = True
                NoOfLinesSelected = NoOfLinesSelected + 1
                Exit Function
            End If
        Next N

        Exit Function
erro1:


    End Function

    Private Function FindLTypes(ByVal All As Integer) As Integer

        Dim N As Integer

        On Error GoTo erro1

        FindLTypes = 0

        If All = 1 Then LineInd = 0

        LineInd = LineInd + 1
        If LineInd = NoOfLines Then LineInd = 1

        For N = LineInd To NoOfLines
            If InStr(1, UCase(Lines(N).Guid), Search) > 0 Then
                FindLTypes = N
                LineInd = N
                LatFind = (Lines(N).NLAT + Lines(N).SLAT) / 2
                LonFind = (Lines(N).ELON + Lines(N).WLON) / 2
                Lines(N).Selected = True
                NoOfLinesSelected = NoOfLinesSelected + 1
                If All = 0 Then Exit Function
            End If
        Next N
        If All = 1 Then Exit Function

        For N = 1 To LineInd
            If InStr(1, UCase(Lines(N).Guid), Search) > 0 Then
                FindLTypes = N
                LineInd = N
                LatFind = (Lines(N).NLAT + Lines(N).SLAT) / 2
                LonFind = (Lines(N).ELON + Lines(N).WLON) / 2
                Lines(N).Selected = True
                NoOfLinesSelected = NoOfLinesSelected + 1
                Exit Function
            End If
        Next N

        Exit Function
erro1:


    End Function

    Private Function FindPolys(ByVal All As Integer) As Integer

        Dim N As Integer
        Dim Flag As Boolean

        On Error GoTo erro1


        FindPolys = 0

        If All = 1 Then PolyInd = 0

        PolyInd = PolyInd + 1
        If PolyInd = NoOfPolys Then PolyInd = 1

        For N = PolyInd To NoOfPolys

            If Search = "" Then
                Flag = False
                If Polys(N).Name = "" Then Flag = True
            Else
                Flag = InStr(1, UCase(Polys(N).Name), Search) > 0
            End If

            If Flag Then
                FindPolys = N
                PolyInd = N
                LatFind = (Polys(N).NLAT + Polys(N).SLAT) / 2
                LonFind = (Polys(N).ELON + Polys(N).WLON) / 2
                Polys(N).Selected = True
                NoOfPolysSelected = NoOfPolysSelected + 1
                If All = 0 Then Exit Function
            End If
        Next N
        If All = 1 Then Exit Function

        For N = 1 To PolyInd

            If Search = "" Then
                Flag = False
                If Polys(N).Name = "" Then Flag = True
            Else
                Flag = InStr(1, UCase(Polys(N).Name), Search) > 0
            End If

            If Flag Then
                FindPolys = N
                PolyInd = N
                LatFind = (Polys(N).NLAT + Polys(N).SLAT) / 2
                LonFind = (Polys(N).ELON + Polys(N).WLON) / 2
                Polys(N).Selected = True
                NoOfPolysSelected = NoOfPolysSelected + 1
                Exit Function
            End If
        Next N

        Exit Function
erro1:


    End Function

    Private Function FindPTypes(ByVal All As Integer) As Integer

        Dim N As Integer

        On Error GoTo erro1

        FindPTypes = 0

        If All = 1 Then PolyInd = 0

        PolyInd = PolyInd + 1
        If PolyInd = NoOfPolys Then PolyInd = 1

        For N = PolyInd To NoOfPolys
            If InStr(1, UCase(Polys(N).Guid), Search) > 0 Then
                FindPTypes = N
                PolyInd = N
                LatFind = (Polys(N).NLAT + Polys(N).SLAT) / 2
                LonFind = (Polys(N).ELON + Polys(N).WLON) / 2
                Polys(N).Selected = True
                NoOfPolysSelected = NoOfPolysSelected + 1
                If All = 0 Then Exit Function
            End If
        Next N
        If All = 1 Then Exit Function

        For N = 1 To PolyInd
            If InStr(1, UCase(Polys(N).Guid), Search) > 0 Then
                FindPTypes = N
                PolyInd = N
                LatFind = (Polys(N).NLAT + Polys(N).SLAT) / 2
                LonFind = (Polys(N).ELON + Polys(N).WLON) / 2
                Polys(N).Selected = True
                NoOfPolysSelected = NoOfPolysSelected + 1
                Exit Function
            End If
        Next N

        Exit Function
erro1:

    End Function

End Class
