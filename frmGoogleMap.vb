Imports System.Windows.Forms

Friend Class frmGoogleMap

    Private Sub CmdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        GoogleMapsType = "satellite"
        If buttonMap.Checked Then GoogleMapsType = "roadmap"
        DialogResult = System.Windows.Forms.DialogResult.OK
        Dispose()

    End Sub

    Private Sub CmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Dispose()
    End Sub

End Class
