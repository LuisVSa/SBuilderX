Option Strict Off
Option Explicit On
Friend Class FrmTaxSign
    Inherits System.Windows.Forms.Form

    Private EntState As Integer = 0

    Private Sub CmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click

        Dispose()

    End Sub

    Private Sub CmdDIR_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDIR.Click

        Dim A As String

        EntState = 1
        A = txtMessage.Text

        A = A & "d[]"
        txtMessage.Text = A
        txtMessage.Focus()

        System.Windows.Forms.SendKeys.Send("{RIGHT}{LEFT}")

    End Sub

    Private Sub CmdINFO_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdINFO.Click

        Dim A As String

        EntState = 1
        A = txtMessage.Text

        A = A & "i[]"
        txtMessage.Text = A
        txtMessage.Focus()

        System.Windows.Forms.SendKeys.Send("{RIGHT}{LEFT}")

    End Sub


    Private Sub CmdDL_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDL.Click

        Dim A As String
        Dim N As Integer

        If EntState = 0 Then
            MsgBox("Select a type!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        N = Len(txtMessage.Text)

        If N < 3 Then
            MsgBox("Check your typing!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        A = Mid(txtMessage.Text, 1, N - 1)

        A = A & "/]"
        txtMessage.Text = A

        txtMessage.Focus()

        System.Windows.Forms.SendKeys.Send("{RIGHT}{LEFT}")

    End Sub

    Private Sub CmdDR_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDR.Click

        Dim A As String
        Dim N As Integer

        If EntState = 0 Then
            MsgBox("Select a type!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        N = Len(txtMessage.Text)

        If N < 3 Then
            MsgBox("Check your typing!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        A = Mid(txtMessage.Text, 1, N - 1)

        A = A & "\]"
        txtMessage.Text = A

        txtMessage.Focus()

        System.Windows.Forms.SendKeys.Send("{RIGHT}{LEFT}")

    End Sub

    Private Sub CmdLL_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLL.Click

        Dim A As String
        Dim N As Integer

        If EntState = 0 Then
            MsgBox("Select a type!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        N = Len(txtMessage.Text)

        If N < 3 Then
            MsgBox("Check your typing!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        A = Mid(txtMessage.Text, 1, N - 1)

        A = A & "<]"
        txtMessage.Text = A

        txtMessage.Focus()
        System.Windows.Forms.SendKeys.Send("{RIGHT}{LEFT}")

    End Sub

    Private Sub CmdLOC_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLOC.Click

        Dim A As String

        EntState = 1
        A = txtMessage.Text

        A = A & "l[]"
        txtMessage.Text = A

        txtMessage.Focus()
        System.Windows.Forms.SendKeys.Send("{RIGHT}{LEFT}")

    End Sub

    Private Sub CmdOK_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click

        frmObjectsP.txtTaxiwayText.Text = txtMessage.Text
        Dispose()

    End Sub

    Private Sub CmdRR_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRR.Click

        Dim A As String
        Dim N As Integer

        If EntState = 0 Then
            MsgBox("Select a type!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        N = Len(txtMessage.Text)

        If N < 3 Then
            MsgBox("Check your typing!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        A = Mid(txtMessage.Text, 1, N - 1)

        A = A & ">]"
        txtMessage.Text = A

        txtMessage.Focus()

        System.Windows.Forms.SendKeys.Send("{RIGHT}{LEFT}")

    End Sub

    Private Sub CmdRUN_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRUN.Click

        Dim A As String

        EntState = 1
        A = txtMessage.Text

        A = A & "m[]"
        txtMessage.Text = A

        txtMessage.Focus()
        System.Windows.Forms.SendKeys.Send("{RIGHT}{LEFT}")

    End Sub

    Private Sub CmdUL_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUL.Click

        Dim A As String
        Dim N As Integer

        If EntState = 0 Then
            MsgBox("Select a type!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        N = Len(txtMessage.Text)

        If N < 3 Then
            MsgBox("Check your typing!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        A = Mid(txtMessage.Text, 1, N - 1)

        A = A & "`]"
        txtMessage.Text = A

        txtMessage.Focus()

        System.Windows.Forms.SendKeys.Send("{RIGHT}{LEFT}")

    End Sub

    Private Sub CmdUp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUP.Click

        Dim A As String
        Dim N As Integer

        If EntState = 0 Then
            MsgBox("Select a type!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        N = Len(txtMessage.Text)

        If N < 3 Then
            MsgBox("Check your typing!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        A = Mid(txtMessage.Text, 1, N - 1)

        A = A & "^]"
        txtMessage.Text = A

        txtMessage.Focus()

        System.Windows.Forms.SendKeys.Send("{RIGHT}{LEFT}")

    End Sub

    Private Sub CmdUR_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUR.Click

        Dim A As String
        Dim N As Integer

        If EntState = 0 Then
            MsgBox("Select a type!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        N = Len(txtMessage.Text)

        If N < 3 Then
            MsgBox("Check your typing!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        A = Mid(txtMessage.Text, 1, N - 1)

        A = A & "']"
        txtMessage.Text = A

        txtMessage.Focus()

        System.Windows.Forms.SendKeys.Send("{RIGHT}{LEFT}")

    End Sub

    Private Sub CmdDD_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDD.Click

        Dim A As String
        Dim N As Integer

        If EntState = 0 Then
            MsgBox("Select a type!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        N = Len(txtMessage.Text)

        If N < 3 Then
            MsgBox("Check your typing!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        A = Mid(txtMessage.Text, 1, N - 1)

        A = A & "v]"
        txtMessage.Text = A

        txtMessage.Focus()
        System.Windows.Forms.SendKeys.Send("{RIGHT}{LEFT}")

    End Sub
    Private Sub CmdILS_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdILS.Click

        Dim A As String
        Dim N As Integer

        If EntState = 0 Then
            MsgBox("Select a type!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        N = Len(txtMessage.Text)

        If N < 3 Then
            MsgBox("Check your typing!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        A = Mid(txtMessage.Text, 1, N - 1)

        A = A & "=]"
        txtMessage.Text = A

        txtMessage.Focus()

        System.Windows.Forms.SendKeys.Send("{RIGHT}{LEFT}")

    End Sub
    Private Sub CmdHOLD_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdHOLD.Click

        Dim A As String
        Dim N As Integer

        If EntState = 0 Then
            MsgBox("Select a type!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        N = Len(txtMessage.Text)

        If N < 3 Then
            MsgBox("Check your typing!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        A = Mid(txtMessage.Text, 1, N - 1)

        A = A & "#]"
        txtMessage.Text = A

        txtMessage.Focus()

        System.Windows.Forms.SendKeys.Send("{RIGHT}{LEFT}")

    End Sub

    Private Sub CmdXX_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdXX.Click

        Dim A As String
        Dim N As Integer

        If EntState = 0 Then
            MsgBox("Select a type!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        N = Len(txtMessage.Text)

        If N < 3 Then
            MsgBox("Check your typing!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        A = Mid(txtMessage.Text, 1, N - 1)

        A = A & "x]"
        txtMessage.Text = A

        txtMessage.Focus()

        System.Windows.Forms.SendKeys.Send("{RIGHT}{LEFT}")

    End Sub

    Private Sub CmdDIV_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDIV.Click

        Dim A As String
        Dim N As Integer

        If EntState = 0 Then
            MsgBox("Select a type!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        N = Len(txtMessage.Text)

        If N < 3 Then
            MsgBox("Check your typing!", 16)
            txtMessage.Text = ""
            Exit Sub
        End If

        A = Mid(txtMessage.Text, 1, N - 1)

        A = A & "|]"
        txtMessage.Text = A

        txtMessage.Focus()
        System.Windows.Forms.SendKeys.Send("{RIGHT}{LEFT}")

    End Sub


    Private Sub TxtMessage_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMessage.KeyDown

        If EntState = 0 Then
            MsgBox("Select a type!", 16)
            txtMessage.Text = ""
        End If

    End Sub

End Class