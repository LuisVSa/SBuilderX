Option Strict Off
Option Explicit On

Imports System.Windows.Forms

Friend Class FrmLWidth

    Private Sub CmdWidth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdWidth.Click

        Dim N, K As Integer
        Dim X As Double

        On Error GoTo erro1

        X = CDbl(txtWidth.Text)

        If POPMode = "One" Then
            For K = 1 To Lines(POPIndex).NoOfPoints
                Lines(POPIndex).GLPoints(K).wid = X
            Next
        Else
            For N = 1 To NoOfLines
                If Lines(N).Selected Then
                    For K = 1 To Lines(N).NoOfPoints
                        Lines(N).GLPoints(K).wid = X
                    Next
                End If
            Next
        End If

        RebuildDisplay()

        Close()
        Exit Sub

erro1:
        MsgBox("Check width value!", MsgBoxStyle.Critical)


    End Sub

    Private Sub CmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Dispose()

    End Sub

    Private Sub FrmLWidth_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        txtWidth1.Text = CStr(Lines(POPIndex).GLPoints(1).wid)
        txtWidth2.Text = CStr(Lines(POPIndex).GLPoints(Lines(POPIndex).NoOfPoints).wid)

        Dim W As Double
        Dim K As Integer

        W = 0
        For K = 1 To Lines(POPIndex).NoOfPoints
            W = W + Lines(POPIndex).GLPoints(K).wid
        Next

        txtWidth.Text = CStr(W / Lines(POPIndex).NoOfPoints)

    End Sub



    Private Sub CmdWidth12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWidth12.Click

        Dim N, K As Integer
        Dim W1, W21, DW, W As Double

        On Error GoTo erro1

        W1 = CDbl(txtWidth1.Text)
        W21 = CDbl(txtWidth2.Text)
        W21 = W21 - W1

        If POPMode = "One" Then
            W = W1
            DW = W21 / (Lines(POPIndex).NoOfPoints - 1)
            For K = 1 To Lines(POPIndex).NoOfPoints
                Lines(POPIndex).GLPoints(K).wid = W
                W = W + DW
            Next
        Else
            For N = 1 To NoOfLines
                If Lines(N).Selected Then
                    W = W1
                    DW = W21 / (Lines(N).NoOfPoints - 1)
                    For K = 1 To Lines(N).NoOfPoints
                        Lines(N).GLPoints(K).wid = W
                        W = W + DW
                    Next
                End If
            Next
        End If

        RebuildDisplay()

        Exit Sub

erro1:
        MsgBox("Check width values!", MsgBoxStyle.Critical)

    End Sub

    Private Sub CmdReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReverse.Click

        Dim N As Integer
        Dim A As String

        A = txtWidth1.Text
        txtWidth1.Text = txtWidth2.Text
        txtWidth2.Text = A

        If POPMode = "One" Then
            ReverseLine(POPIndex)
        Else
            For N = 1 To NoOfLines
                If Lines(N).Selected Then ReverseLine(N)
            Next
        End If

        RebuildDisplay()

    End Sub


    Private Sub ReverseLine(ByVal N As Integer)

        Dim myLine As GLine
        Dim K, NP As Integer

        NP = Lines(N).NoOfPoints
        myLine.NoOfPoints = NP

        ReDim myLine.GLPoints(NP)

        For K = 1 To NP
            'myLine.GLPoints(K).lon = Lines(N).GLPoints(K).lon
            'myLine.GLPoints(K).lat = Lines(N).GLPoints(K).lat
            'myLine.GLPoints(K).alt = Lines(N).GLPoints(K).alt
            myLine.GLPoints(K).wid = Lines(N).GLPoints(K).wid
        Next
        For K = 1 To NP
            'Lines(N).GLPoints(K).lon = myLine.GLPoints(NP + 1 - K).lon
            'Lines(N).GLPoints(K).lat = myLine.GLPoints(NP + 1 - K).lat
            'Lines(N).GLPoints(K).alt = myLine.GLPoints(NP + 1 - K).alt
            Lines(N).GLPoints(K).wid = myLine.GLPoints(NP + 1 - K).wid
        Next


    End Sub



    Private Sub CmdWinding_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWinding.Click

        Dim N As Integer

        If POPMode = "One" Then
            ChangeWinding(POPIndex)
        Else
            For N = 1 To NoOfLines
                If Lines(N).Selected Then ChangeWinding(N)
            Next
        End If

        RebuildDisplay()


    End Sub

    Private Sub ChangeWinding(ByVal N As Integer)

        Dim myLine As GLine
        Dim K, NP As Integer

        NP = Lines(N).NoOfPoints
        myLine.NoOfPoints = NP

        ReDim myLine.GLPoints(NP)

        For K = 1 To NP
            myLine.GLPoints(K).lon = Lines(N).GLPoints(K).lon
            myLine.GLPoints(K).lat = Lines(N).GLPoints(K).lat
            myLine.GLPoints(K).alt = Lines(N).GLPoints(K).alt
            myLine.GLPoints(K).wid = Lines(N).GLPoints(K).wid
        Next

        For K = 1 To NP
            Lines(N).GLPoints(K).lon = myLine.GLPoints(NP + 1 - K).lon
            Lines(N).GLPoints(K).lat = myLine.GLPoints(NP + 1 - K).lat
            Lines(N).GLPoints(K).alt = myLine.GLPoints(NP + 1 - K).alt
            Lines(N).GLPoints(K).wid = myLine.GLPoints(NP + 1 - K).wid
        Next

    End Sub



End Class
