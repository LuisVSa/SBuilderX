Public Class frmArcGisMap

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click

        ArcGisMapsType = "Imagery"
        If buttonMap.Checked Then ArcGisMapsType = "Street_Map"
        If buttonTopo.Checked Then ArcGisMapsType = "Topo_Map"
        DialogResult = System.Windows.Forms.DialogResult.OK
        Dispose()

    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click

        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Dispose()

    End Sub

End Class