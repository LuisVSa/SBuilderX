Public Class FrmBackground

    Private Sub CmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Dispose()
    End Sub

    Private Sub CmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        DialogResult = System.Windows.Forms.DialogResult.OK
        Dispose()
    End Sub

    Private Sub CmdCompile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCompile.Click


        DialogResult = System.Windows.Forms.DialogResult.Cancel
        If Zoom < 5 Then
            MsgBox("Zoom is too low for photo scenery!", MsgBoxStyle.Exclamation)
            Dispose()
            Exit Sub  ' added this. It is right? in October 2017
        End If

        frmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor

        MakeBglPhotoBackground(ckCopyBGLs.Checked)

        'If ckStartFSX.CheckState = 1 Then Shell(FSPath & "fsx.exe", 1)  'it looks Win10 does not like it
        'If ckStartFSX.CheckState = 1 Then Process.Start(FSPath & "fsx.exe")
        If ckStartFSX.CheckState = 1 Then Process.Start(FSPath & SimExe)

        FrmStart.SetMouseIcon()
        Dispose()

    End Sub
End Class