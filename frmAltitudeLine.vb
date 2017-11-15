Imports System.Windows.Forms

Friend Class FrmAltitudeLine

    Private Sub CmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Dispose()

    End Sub

    Private Sub CmdAlt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAlt.Click

        Dim N, K As Integer
        Dim X As Double

        On Error GoTo erro1

        X = CDbl(txtAlt.Text)

        If POPMode = "One" Then
            For K = 1 To Lines(POPIndex).NoOfPoints
                Lines(POPIndex).GLPoints(K).alt = X
            Next
        Else
            For N = 1 To NoOfLines
                If Lines(N).Selected Then
                    For K = 1 To Lines(N).NoOfPoints
                        Lines(N).GLPoints(K).alt = X
                    Next
                End If
            Next
        End If

        Dispose()
        Exit Sub

erro1:
        MsgBox("Check altitude value!", MsgBoxStyle.Critical)

        Dispose()

    End Sub

    Private Sub FrmAltitudeLine_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim N As Integer
        Dim X As Double

        X = 0
        For N = 1 To Lines(POPIndex).NoOfPoints
            X = X + Lines(POPIndex).GLPoints(N).alt
        Next
        txtAlt.Text = (X / Lines(POPIndex).NoOfPoints).ToString

    End Sub

End Class
