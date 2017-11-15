Public Class FrmFSXSettings

    Dim SDKBglCompX As String

    Private Sub FrmFSXSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim FSPathX As String = ""
        Dim SDKTerrainX As String = ""
        Dim SDKBglCompX As String = ""
        Dim SDKPluginsX As String = ""

        If FSPath <> "" Then FSPathX = FSPath.Substring(0, FSPath.Length - 1)
        If SDKTerrain <> "" Then SDKTerrainX = SDKTerrain.Substring(0, SDKTerrain.Length - 1)
        If SDKBglComp <> "" Then SDKBglCompX = SDKBglComp.Substring(0, SDKBglComp.Length - 1)
        If SDKPlugins <> "" Then SDKPluginsX = SDKPlugins.Substring(0, SDKPlugins.Length - 1)

        Dim ToolsFolder As String = AppPath & "\Tools\"
        If My.Computer.FileSystem.FileExists(ToolsFolder & "shp2vec.exe") _
            And My.Computer.FileSystem.FileExists(ToolsFolder & "resample.exe") _
            And My.Computer.FileSystem.FileExists(ToolsFolder & "imagetool.exe") Then
            SDKTerrainX = AppPath & "\Tools"
        End If
        If My.Computer.FileSystem.FileExists(ToolsFolder & "bglcomp.exe") _
            And My.Computer.FileSystem.FileExists(ToolsFolder & "bglcomp.xsd") Then
            SDKBglCompX = AppPath & "\Tools"
        End If
        If My.Computer.FileSystem.FileExists(ToolsFolder & "XToMdl.exe") _
            And My.Computer.FileSystem.FileExists(ToolsFolder & "Managed_CrashTree.dll") _
            And My.Computer.FileSystem.FileExists(ToolsFolder & "Managed_Lookup_Keyword.dll") Then
            SDKPluginsX = AppPath & "\Tools"
        End If

        txtNameOfSim.Text = NameOfSim
        txtFSPath.Text = FSPathX
        txtTerrain.Text = SDKTerrainX
        txtBGLComp.Text = SDKBglCompX
        txtPlugins.Text = SDKPluginsX
        If IsFSX = False And IgnoreFSX = False Then txtFSPath.ForeColor = Color.Red
        If IsFSXBGLComp = False Then txtBGLComp.ForeColor = Color.Red
        If IsFSXTerrain = False Then txtTerrain.ForeColor = Color.Red
        If IsFSXPlugins = False Then txtPlugins.ForeColor = Color.Red

    End Sub


    Private Sub CmdFSX_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFSX.Click

        FolderBrowserDialog1.ShowNewFolderButton = False
        FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
        FolderBrowserDialog1.SelectedPath = txtFSPath.Text
        FolderBrowserDialog1.Description = "Set the Full Path to Flight Simulator"
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtFSPath.Text = FolderBrowserDialog1.SelectedPath
            If My.Computer.FileSystem.FileExists(txtFSPath.Text & "\terrain.cfg") Then
                txtFSPath.ForeColor = Color.Black
            Else
                txtFSPath.ForeColor = Color.Red
            End If
        End If

    End Sub

    Private Sub CmdTerrain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTerrain.Click

        Dim ToolsFolder As String = AppPath & "\Tools\"
        Dim TerrainFolder As String
        Dim B, C As String

        FolderBrowserDialog1.ShowNewFolderButton = False
        FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
        FolderBrowserDialog1.SelectedPath = txtTerrain.Text
        FolderBrowserDialog1.Description = "Point to the location of Shp2Vec.exe, Resample.exe and ImageTool.exe"
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then

            IsFSXTerrain = False
            TerrainFolder = FolderBrowserDialog1.SelectedPath & "\"

            If My.Computer.FileSystem.FileExists(ToolsFolder & "shp2vec.exe") _
            And My.Computer.FileSystem.FileExists(ToolsFolder & "resample.exe") _
            And My.Computer.FileSystem.FileExists(ToolsFolder & "imagetool.exe") Then
                IsFSXTerrain = True
            ElseIf My.Computer.FileSystem.FileExists(TerrainFolder & "shp2vec.exe") _
            And My.Computer.FileSystem.FileExists(TerrainFolder & "resample.exe") _
            And My.Computer.FileSystem.FileExists(TerrainFolder & "imagetool.exe") Then
                B = TerrainFolder & "shp2vec.exe"
                C = ToolsFolder & "shp2vec.exe"
                File.Copy(B, C, True)
                B = TerrainFolder & "resample.exe"
                C = ToolsFolder & "resample.exe"
                File.Copy(B, C, True)
                B = TerrainFolder & "imagetool.exe"
                C = ToolsFolder & "imagetool.exe"
                File.Copy(B, C, True)
                IsFSXTerrain = True
            End If

            If IsFSXTerrain Then
                txtTerrain.ForeColor = Color.Black
                txtTerrain.Text = AppPath & "\Tools"
            Else
                txtTerrain.ForeColor = Color.Red
            End If
        End If

    End Sub

    Private Sub CmdBGLComp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBGLComp.Click

        Dim ToolsFolder As String = AppPath & "\Tools\"
        Dim BGLCompFolder As String
        Dim B, C As String

        FolderBrowserDialog1.ShowNewFolderButton = False
        FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
        FolderBrowserDialog1.SelectedPath = txtBGLComp.Text
        FolderBrowserDialog1.Description = "Point to the location of BglComp.exe and BglComp.xsd"
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then

            IsFSXBGLComp = False
            BGLCompFolder = FolderBrowserDialog1.SelectedPath & "\"

            If My.Computer.FileSystem.FileExists(ToolsFolder & "bglcomp.exe") _
            And My.Computer.FileSystem.FileExists(ToolsFolder & "bglcomp.xsd") Then
                IsFSXBGLComp = True
            ElseIf My.Computer.FileSystem.FileExists(BGLCompFolder & "bglcomp.exe") _
            And My.Computer.FileSystem.FileExists(BGLCompFolder & "bglcomp.xsd") Then
                B = BGLCompFolder & "bglcomp.exe"
                C = ToolsFolder & "bglcomp.exe"
                File.Copy(B, C, True)
                B = BGLCompFolder & "bglcomp.xsd"
                C = ToolsFolder & "bglcomp.xsd"
                File.Copy(B, C, True)
                IsFSXBGLComp = True
            End If

            If IsFSXBGLComp Then
                txtBGLComp.ForeColor = Color.Black
                txtBGLComp.Text = AppPath & "\Tools"
            Else
                txtBGLComp.ForeColor = Color.Red
            End If
        End If

    End Sub

    Private Sub CmdPlugins_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPlugins.Click

        Dim ToolsFolder As String = AppPath & "\Tools\"
        Dim PluginsFolder As String
        Dim B, C As String

        FolderBrowserDialog1.ShowNewFolderButton = False
        FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer
        FolderBrowserDialog1.SelectedPath = txtPlugins.Text
        FolderBrowserDialog1.Description = "Point to the location of XToMdl.exe"
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then

            IsFSXPlugins = False
            PluginsFolder = FolderBrowserDialog1.SelectedPath & "\"

            If My.Computer.FileSystem.FileExists(ToolsFolder & "XToMdl.exe") _
            And My.Computer.FileSystem.FileExists(ToolsFolder & "Managed_CrashTree.dll") _
            And My.Computer.FileSystem.FileExists(ToolsFolder & "Managed_Lookup_Keyword.dll") Then
                IsFSXPlugins = True
            ElseIf My.Computer.FileSystem.FileExists(PluginsFolder & "XToMdl.exe") _
            And My.Computer.FileSystem.FileExists(PluginsFolder & "Managed_CrashTree.dll") _
            And My.Computer.FileSystem.FileExists(PluginsFolder & "Managed_Lookup_Keyword.dll") Then
                B = PluginsFolder & "XToMdl.exe"
                C = ToolsFolder & "XToMdl.exe"
                File.Copy(B, C, True)
                B = PluginsFolder & "Managed_CrashTree.dll"
                C = ToolsFolder & "Managed_CrashTree.dll"
                File.Copy(B, C, True)
                B = PluginsFolder & "Managed_Lookup_Keyword.dll"
                C = ToolsFolder & "Managed_Lookup_Keyword.dll"
                File.Copy(B, C, True)
                IsFSXPlugins = True
            End If

            If IsFSXPlugins Then
                txtPlugins.ForeColor = Color.Black
                txtPlugins.Text = AppPath & "\Tools"
            Else
                txtPlugins.ForeColor = Color.Red
            End If
        End If

    End Sub

    Private Sub CmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Dim B, C As String

        NameOfSim = Trim(txtNameOfSim.Text)
        SimPath = Trim(txtFSPath.Text)

        ' FSX
        FSPath = txtFSPath.Text & "\"
        IsFSX = False
        FSTextureFolder = FSPath & "Scenery\World\Texture\"
        If My.Computer.FileSystem.FileExists(FSPath & "terrain.cfg") Then IsFSX = True

        ' Terrain
        Dim TerrainFolder As String = txtTerrain.Text & "\"
        IsFSXTerrain = False
        Dim ToolsFolder As String = AppPath & "\Tools\"
        If My.Computer.FileSystem.FileExists(ToolsFolder & "shp2vec.exe") _
            And My.Computer.FileSystem.FileExists(ToolsFolder & "resample.exe") _
            And My.Computer.FileSystem.FileExists(ToolsFolder & "imagetool.exe") Then
            IsFSXTerrain = True
        ElseIf My.Computer.FileSystem.FileExists(TerrainFolder & "shp2vec.exe") _
            And My.Computer.FileSystem.FileExists(TerrainFolder & "resample.exe") _
            And My.Computer.FileSystem.FileExists(TerrainFolder & "imagetool.exe") Then
            B = TerrainFolder & "shp2vec.exe"
            C = ToolsFolder & "shp2vec.exe"
            File.Copy(B, C, True)
            B = TerrainFolder & "resample.exe"
            C = ToolsFolder & "resample.exe"
            File.Copy(B, C, True)
            B = TerrainFolder & "imagetool.exe"
            C = ToolsFolder & "imagetool.exe"
            File.Copy(B, C, True)
            IsFSXTerrain = True
        End If

        ' Plugins
        Dim PluginsFolder As String = txtPlugins.Text & "\"
        IsFSXPlugins = False
        If My.Computer.FileSystem.FileExists(ToolsFolder & "XToMdl.exe") _
            And My.Computer.FileSystem.FileExists(ToolsFolder & "Managed_CrashTree.dll") _
            And My.Computer.FileSystem.FileExists(ToolsFolder & "Managed_Lookup_Keyword.dll") Then
            IsFSXPlugins = True
        ElseIf My.Computer.FileSystem.FileExists(PluginsFolder & "XToMdl.exe") _
            And My.Computer.FileSystem.FileExists(PluginsFolder & "Managed_CrashTree.dll") _
            And My.Computer.FileSystem.FileExists(PluginsFolder & "Managed_Lookup_Keyword.dll") Then
            B = PluginsFolder & "XToMdl.exe"
            C = ToolsFolder & "XToMdl.exe"
            File.Copy(B, C, True)
            B = PluginsFolder & "Managed_CrashTree.dll"
            C = ToolsFolder & "Managed_CrashTree.dll"
            File.Copy(B, C, True)
            B = PluginsFolder & "Managed_Lookup_Keyword.dll"
            C = ToolsFolder & "Managed_Lookup_Keyword.dll"
            File.Copy(B, C, True)
            IsFSXPlugins = True
        End If

        Dim BGLCompFolder As String = txtBGLComp.Text & "\"
        IsFSXBGLComp = False
        If My.Computer.FileSystem.FileExists(ToolsFolder & "bglcomp.exe") _
            And My.Computer.FileSystem.FileExists(ToolsFolder & "bglcomp.xsd") Then
            IsFSXBGLComp = True
        ElseIf My.Computer.FileSystem.FileExists(BGLCompFolder & "bglcomp.exe") _
            And My.Computer.FileSystem.FileExists(BGLCompFolder & "bglcomp.xsd") Then
            B = BGLCompFolder & "bglcomp.exe"
            C = ToolsFolder & "bglcomp.exe"
            File.Copy(B, C, True)
            B = BGLCompFolder & "bglcomp.xsd"
            C = ToolsFolder & "bglcomp.xsd"
            File.Copy(B, C, True)
            IsFSXBGLComp = True
        End If

        WriteSettings()
        Dispose()

    End Sub

End Class