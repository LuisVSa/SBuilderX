Public Class FrmObjectFolders

    Private IniAPIPath As String
    Private IniRwy12Path As String
    Private IniASDPath As String
    Private IniLibObjectsPath As String

    Private Sub FrmObjectFolders_Load(sender As Object, e As EventArgs) Handles Me.Load

        txtRwy12Folder.Text = Rwy12Path
        txtASDFolder.Text = MacroASDPath
        txtAPIFolder.Text = MacroAPIPath
        txtLibObjectsFolder.Text = LibObjectsPath

        IniRwy12Path = Rwy12Path
        IniASDPath = MacroASDPath
        IniAPIPath = MacroAPIPath
        IniLibObjectsPath = LibObjectsPath

    End Sub

    Private Sub CmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click

        Dispose()

    End Sub

    Private Sub CmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click

        MacroASDPath = txtASDFolder.Text
        MacroAPIPath = txtAPIFolder.Text
        Rwy12Path = txtRwy12Folder.Text
        LibObjectsPath = txtLibObjectsFolder.Text

        ' WriteObjectsSettings()

        If IniLibObjectsPath <> LibObjectsPath Then SetLibObjects()
        If IniRwy12Path <> Rwy12Path Then SetRwy12Objects()
        If IniASDPath <> MacroASDPath Or IniAPIPath <> MacroAPIPath Then SetMacroObjects()

        WriteSettings()
        Dispose()

    End Sub

    Private Sub CmdLibObjects_Click(sender As Object, e As EventArgs) Handles cmdLibObjects.Click

        Dim A As String = "Choose the Folder that contains Library Objects."
        FrmStart.FolderBrowserDialog1.Description = A
        If FrmStart.FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtLibObjectsFolder.Text = FrmStart.FolderBrowserDialog1.SelectedPath
        End If

    End Sub

    Private Sub CmdRyw12_Click(sender As Object, e As EventArgs) Handles cmdRyw12.Click

        Dim A As String = "Choose the Folder that contains Rwy12 Objects."
        FrmStart.FolderBrowserDialog1.Description = A
        If FrmStart.FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtRwy12Folder.Text = FrmStart.FolderBrowserDialog1.SelectedPath
        End If

    End Sub

    Private Sub CmdAPI_Click(sender As Object, e As EventArgs) Handles cmdAPI.Click

        Dim A As String = "Choose the Folder that contains API macros."
        FrmStart.FolderBrowserDialog1.Description = A
        If FrmStart.FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtAPIFolder.Text = FrmStart.FolderBrowserDialog1.SelectedPath
        End If

    End Sub

    Private Sub CmdASD_Click(sender As Object, e As EventArgs) Handles cmdASD.Click

        Dim A As String = "Choose the Folder that contains ASD macros."
        FrmStart.FolderBrowserDialog1.Description = A
        If FrmStart.FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtASDFolder.Text = FrmStart.FolderBrowserDialog1.SelectedPath
        End If

    End Sub

End Class