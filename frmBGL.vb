Option Strict Off
Option Explicit On

Friend Class FrmBGL

    Private Init As Boolean = True

    Private CopyBGLs As Boolean = False
    Private FWX As Boolean = False
    Private STX As Boolean = False
    Private RDX As Boolean = False
    Private HLX As Boolean = False
    Private RRX As Boolean = False
    Private UTX As Boolean = False
    Private EXX As Boolean = False
    Private FLX As Boolean = False
    Private PKX As Boolean = False
    Private HPX As Boolean = False
    Private HGX As Boolean = False


    Private Sub CmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click

        Dispose()

    End Sub

    Private Sub CmdCompile_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCompile.Click


        'If FSXTools = False Then
        If IsFSXTerrain = False Or IsFSXBGLComp = False Then
            MsgBox("SDK Tools are not present in ..\SBuilder\Tools folder!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        FrmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor

        If ckCopyBGLs.CheckState = 1 Then CopyBGLs = True

        If ckVector.Checked Then MakeBGLVector()
        If ckLand.CheckState = 1 Then MakeBglLand(CopyBGLs)
        If ckWater.CheckState = 1 Then MakeBglWater(CopyBGLs)
        If ckPhoto.CheckState = 1 Then MakeBglPhoto(CopyBGLs)
        If ckObjects.CheckState = 1 Then MakeBGLObjects(CopyBGLs)
        If ckExcludes.CheckState = 1 Then MakeBGLExcludes(CopyBGLs)
        If ckTexPolys.CheckState = 1 Then MakeBGLTexPolys(CopyBGLs)
        If ckExtrusions.CheckState = 1 Then MakeBGLExtrusions(CopyBGLs)
        If ckTexLines.CheckState = 1 Then MakeBGLTexLines(CopyBGLs)
        If ckLinesOfObjects.CheckState = 1 Then MakeBGLObjLines(CopyBGLs)

        'If ckStartFSX.CheckState = 1 Then Shell(FSPath & "fsx.exe", 1)  'it looks Win10 does not like it
        'If ckStartFSX.CheckState = 1 Then Process.Start(FSPath & "fsx.exe")
        If ckStartFSX.CheckState = 1 Then Process.Start(FSPath & SimExe)

        FrmStart.SetMouseIcon()
        Dispose()

    End Sub

    Private Sub FrmBGL_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim N As Integer
        Dim A As String

        Dim Flag1 As Boolean = False
        Dim Flag2 As Boolean = False

        Init = True

        ckLand.Enabled = False
        ckVector.Enabled = False
        ckWater.Enabled = False
        ckPhoto.Enabled = False
        ckObjects.Enabled = False
        ckExcludes.Enabled = False
        ckTexPolys.Enabled = False
        ckExtrusions.Enabled = False
        ckTexLines.Enabled = False
        ckLinesOfObjects.Enabled = False

        ckCopyBGLs.Enabled = False

        ckLand.Checked = False
        ckVector.Checked = False
        ckWater.Checked = False
        ckPhoto.Checked = False
        ckObjects.Checked = False
        ckExcludes.Checked = False
        ckTexPolys.Checked = False
        ckExtrusions.Checked = False
        ckTexLines.Checked = False
        ckLinesOfObjects.Checked = False

        EXX = False
        PKX = False
        HPX = False
        HGX = False

        FLX = False
        STX = False
        FWX = False
        RDX = False
        HLX = False
        RRX = False
        UTX = False

        For N = 1 To NoOfPolys
            If Polys(N).Selected Then
                A = Mid(Polys(N).Type, 1, 3)
                If A = "XXX" Then EXX = True
                If A = "EXX" Then EXX = True
                If A = "LCP" Then PKX = True
                If A = "HPX" Then HPX = True
                If A = "HGX" Then HGX = True
                If A = "FLX" Then FLX = True
                If A = "TEX" Then
                    ckTexPolys.Enabled = True
                    lbNoSelection.Visible = False
                    ckTexPolys.Checked = True
                    ckCopyBGLs.Enabled = True
                End If

            End If
        Next N

        For N = 1 To NoOfLines
            If Lines(N).Selected Then
                A = Mid(Lines(N).Type, 1, 3)
                If A = "STX" Then STX = True
                If A = "FWX" Then FWX = True
                If A = "RDX" Then RDX = True
                If A = "HLX" Then HLX = True
                If A = "RRX" Then RRX = True
                If A = "UTX" Then UTX = True
                If A = "TEX" Then
                    ckTexLines.Enabled = True
                    lbNoSelection.Visible = False
                    ckTexLines.Checked = True
                    ckCopyBGLs.Enabled = True
                End If
                If A = "EXT" Then
                    ckExtrusions.Enabled = True
                    lbNoSelection.Visible = False
                    ckExtrusions.Checked = True
                    ckCopyBGLs.Enabled = True
                End If
                If A = "OBJ" Then
                    ckLinesOfObjects.Enabled = True
                    lbNoSelection.Visible = False
                    ckLinesOfObjects.Checked = True
                    ckCopyBGLs.Enabled = True
                End If
            End If
        Next N

        If EXX Or PKX Or HPX Or HGX Or FLX Or STX Or FWX Or RDX Or HLX Or RRX Or UTX Then
            ckVector.Enabled = True
            lbNoSelection.Visible = False
            ckVector.Checked = True
            ckCopyBGLs.Enabled = True
        End If

        If NoOfLandsSelected > 0 Then
            ckLand.Enabled = True
            lbNoSelection.Visible = False
            ckLand.Checked = True
            ckCopyBGLs.Enabled = True
        End If

        If NoOfWatersSelected > 0 Then
            ckWater.Enabled = True
            lbNoSelection.Visible = False
            ckWater.Checked = True
            ckCopyBGLs.Enabled = True
        End If

        Flag1 = False
        For N = 1 To NoOfMaps
            If Maps(N).Selected Then
                A = UCase(Mid(Maps(N).Name, 1, 5))
                If A = "PHOTO" Then
                    A = UCase(Path.GetExtension(Maps(N).BMPSu))
                    If A = ".BMP" Then
                        Flag1 = True
                        Exit For
                    End If
                End If
            End If
        Next
        If Flag1 Then
            ckPhoto.Enabled = True
            lbNoSelection.Visible = False
            ckPhoto.Checked = True
            ckCopyBGLs.Enabled = True
        End If

        Flag1 = False
        For N = 1 To NoOfObjects
            If Objects(N).Selected Then
                Flag1 = True
                Exit For
            End If
        Next
        If Flag1 Then
            ckObjects.Enabled = True
            lbNoSelection.Visible = False
            ckObjects.Checked = True
            ckCopyBGLs.Enabled = True
        End If

        Flag1 = False
        For N = 1 To NoOfExcludes
            If Excludes(N).Selected Then
                Flag1 = True
                Exit For
            End If
        Next
        If Flag1 Then
            ckExcludes.Enabled = True
            lbNoSelection.Visible = False
            ckExcludes.Checked = True
            ckCopyBGLs.Enabled = True
        End If

        Init = False

    End Sub

    Private Sub MakeBGLVector()

        Dim sourcebase, destbase As String
        Dim source, dest, shapefile As String
        Dim shpfiles As String()
        Dim ProjectNameNoSpaces As String

        ProjectNameNoSpaces = Replace(ProjectName, " ", "_")

        shpfiles = Directory.GetFiles(AppPath & "\Tools\Shapes")

        For Each shapefile In shpfiles
            File.Delete(shapefile)
        Next

        sourcebase = AppPath & "\Tools\"
        destbase = AppPath & "\Tools\Shapes\"

        ChDrive(My.Application.Info.DirectoryPath)
        ChDir(My.Application.Info.DirectoryPath & "\Tools\Shapes")

        If EXX Then
            source = sourcebase & "EXX.xml"
            shapefile = "EXX_" & ProjectNameNoSpaces
            dest = destbase & shapefile & ".xml"
            File.Copy(source, dest)
            shapefile = shapefile & ".shp"
            MakeSHPPolys(shapefile, "EXX")
        End If

        If PKX Then
            source = sourcebase & "PKX.xml"
            shapefile = "PKX_" & ProjectNameNoSpaces
            dest = destbase & shapefile & ".xml"
            File.Copy(source, dest)
            shapefile = shapefile & ".shp"
            MakeSHPPolys(shapefile, "LCP")
        End If

        If HPX Then
            source = sourcebase & "HPX.xml"
            shapefile = "HPX_" & ProjectNameNoSpaces
            dest = destbase & shapefile & ".xml"
            File.Copy(source, dest)
            shapefile = shapefile & ".shp"
            MakeSHPPolys(shapefile, "HPX")
        End If


        If HGX Then
            source = sourcebase & "HGX.xml"
            shapefile = "HGX_" & ProjectNameNoSpaces
            dest = destbase & shapefile & ".xml"
            File.Copy(source, dest)
            shapefile = shapefile & ".shp"
            MakeSHPPolys(shapefile, "HGX")
        End If

        If FLX Then
            source = sourcebase & "FLX.xml"
            shapefile = "FLX_" & ProjectNameNoSpaces
            dest = destbase & shapefile & ".xml"
            File.Copy(source, dest)
            shapefile = shapefile & ".shp"
            MakeSHPPolys(shapefile, "FLX")
        End If

        If STX Then
            source = sourcebase & "STX.xml"
            shapefile = "STX_" & ProjectNameNoSpaces
            dest = destbase & shapefile & ".xml"
            File.Copy(source, dest)
            shapefile = shapefile & ".shp"
            MakeSHPLines(shapefile, "STX")
        End If

        If FWX Then
            source = sourcebase & "FWX.xml"
            shapefile = "FWX_" & ProjectNameNoSpaces
            dest = destbase & shapefile & ".xml"
            File.Copy(source, dest)
            shapefile = shapefile & ".shp"
            MakeSHPLines(shapefile, "FWX")
        End If

        If RDX Then
            source = sourcebase & "RDX.xml"
            shapefile = "RDX_" & ProjectNameNoSpaces
            dest = destbase & shapefile & ".xml"
            File.Copy(source, dest)
            shapefile = shapefile & ".shp"
            MakeSHPLines(shapefile, "RDX")
        End If

        If HLX Then
            source = sourcebase & "HLX.xml"
            shapefile = "HLX_" & ProjectNameNoSpaces
            dest = destbase & shapefile & ".xml"
            File.Copy(source, dest)
            shapefile = shapefile & ".shp"
            MakeSHPLines(shapefile, "HLX")
        End If

        If RRX Then
            source = sourcebase & "RRX.xml"
            shapefile = "RRX_" & ProjectNameNoSpaces
            dest = destbase & shapefile & ".xml"
            File.Copy(source, dest)
            shapefile = shapefile & ".shp"
            MakeSHPLines(shapefile, "RRX")
        End If

        If UTX Then
            source = sourcebase & "UTX.xml"
            shapefile = "UTX_" & ProjectNameNoSpaces
            dest = destbase & shapefile & ".xml"
            File.Copy(source, dest)
            shapefile = shapefile & ".shp"
            MakeSHPLines(shapefile, "UTX")
        End If

        ChDir(My.Application.Info.DirectoryPath & "\Tools")

        Dim myCommand As String

        myCommand = "shp2vec Shapes _" & ProjectNameNoSpaces
        If AddToCells Then myCommand = myCommand & " -ADDTOCELLS"
        ExecCmd(myCommand)

        If Not CopyBGLs Then Exit Sub

        Try
            source = destbase & "CVX_" & ProjectNameNoSpaces & ".BGL"
            If File.Exists(source) Then
                dest = BGLProjectFolder & "\CVX_" & ProjectNameNoSpaces & ".BGL"
                File.Copy(source, dest, True)
            End If
        Catch ex As Exception
            MsgBox("Copying BGL files failed! Try to close FSX.", MsgBoxStyle.Information)
        End Try

    End Sub

    Private Sub CkVector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckVector.CheckedChanged
        SetChecks()
    End Sub

    Private Sub CkLand_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckLand.CheckedChanged
        SetChecks()
    End Sub

    Private Sub CkWater_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckWater.CheckedChanged
        SetChecks()
    End Sub

    Private Sub CkPhoto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckPhoto.CheckedChanged, ckTexLines.CheckedChanged, ckExtrusions.CheckedChanged
        SetChecks()
    End Sub

    Private Sub SetChecks()

        If Init Then Exit Sub

        ckCopyBGLs.Enabled = False
        If ckVector.Checked Or ckLand.Checked Or ckWater.Checked Or ckPhoto.Checked _
           Or ckTexPolys.Checked Or ckObjects.Checked Or ckExcludes.Checked Then
            ckCopyBGLs.Enabled = True
        End If

    End Sub


    Private Sub CkTexPolys_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckTexPolys.CheckedChanged
        SetChecks()
    End Sub

    Private Sub CkObjects_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckObjects.CheckedChanged
        SetChecks()
    End Sub

    Private Sub CkExcludes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckExcludes.CheckedChanged
        SetChecks()
    End Sub

    Private Sub CkLinesOfObjects_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckLinesOfObjects.CheckedChanged
        SetChecks()
    End Sub
End Class
