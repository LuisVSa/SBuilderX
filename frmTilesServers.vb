Public Class frmTilesServers

    Private Sub frmTilesServers_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim A As String = "TileServer plug-ins that conform to the  "
        A = A & "SBuilderX TileServer Interface can generate "
        A = A & "image backgrounds. You should comply with "
        A = A & "any copyrights that such images may hold."

        Label1.Text = A

        If NoOfServerTypes > 0 Then
            ListMapServers.Enabled = True
            ckMapServer.Enabled = True
        Else
            ckMapServer.Text = "No Map Server detected!"
            ListMapServers.Enabled = False
            ckMapServer.Enabled = False
            Exit Sub
        End If

        Dim N, K As Integer
        K = 0
        For N = 1 To NoOfServerTypes
            ListMapServers.Items.Add(ServerTypes(N).Name)
            If ServerTypes(N).Name = ActiveTileFolder Then K = N
        Next

        If K > 0 Then
            ckMapServer.Checked = True
            ListMapServers.SelectedIndex = K - 1
            ckMapServer.Text = "Use " & ServerTypes(K).Name & " ?"
        Else
            ListMapServers.SelectedIndex = 0
            ckMapServer.Checked = False
            ActiveTileFolder = ""
            'WriteTilesSettings()
            'WriteSettings()
        End If

    End Sub

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click

        Dim K As Integer
        ActiveTileFolder = ""
        TileServer = Nothing
        TilesDownloading.Clear()
        TilesFailed.Clear()
        TilesToCome = 0

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(AppPath & "\Tiles", FileIO.SearchOption.SearchTopLevelOnly, "L*")
            My.Computer.FileSystem.DeleteFile(foundFile)
        Next

        If ListMapServers.Enabled Then
            K = ListMapServers.SelectedIndex + 1
            If ckMapServer.Checked Then ActiveTileFolder = ServerTypes(K).Name
        End If

        If ActiveTileFolder <> "" Then
            FrmStart.ShowBackgroundMenuItem.Enabled = True
            TileServer = Activator.CreateInstance(ServerTypes(K))
            MaximumZoom = TileServer.MaximumZoom
        Else
            TileVIEW = False
            FrmStart.ShowBackgroundMenuItem.Checked = False
            FrmStart.FromBackgroundMapMenuItem.Enabled = False
            FrmStart.ShowBackgroundMenuItem.Enabled = False
        End If

        FrmStart.MakeBackground()
        RebuildDisplay()

        WriteSettings()
        Dispose()

    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click

        Dispose()

    End Sub

    Private Sub ListMapServers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListMapServers.SelectedIndexChanged

        Dim LT As Integer
        LT = ListMapServers.SelectedIndex + 1
        ckMapServer.Text = "Use " & ServerTypes(LT).Name & " ?"

    End Sub

End Class