Option Strict Off
Option Explicit On

Imports System.Windows.Forms

Friend Class FrmPolysP

    Private ThisPolyType As Integer
    Private ThisPoly As Integer
    Private IsInit As Boolean = True
    Private NoOfParents As Integer
    Private ThisParent As Integer


    Private Sub FrmPolysP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ParticularExcludeGUID = "{00000000-0000-0000-0000-111111111111}"
        ReDim Polys(0).GPoints(Polys(POPIndex).NoOfPoints)
        Polys(0) = Polys(POPIndex)
        If Polys(0).Type = "XXX" Then
            If Polys(0).Guid <> "" Then
                ParticularExcludeGUID = Polys(0).Guid
            End If
        End If

        ThisPoly = POPIndex
        Dim N As Integer
        NoOfParents = 0
        For N = 1 To NoOfPolys
            If Polys(N).NoOfChilds >= 0 Then NoOfParents = NoOfParents + 1
            If N = ThisPoly Then ThisParent = NoOfParents
        Next


        lbOrder.Text = "Poly #" & CStr(ThisParent) & " out of " & CStr(NoOfParents)
        If POPMode = "One" Then
            If IsTexPoly(ThisPoly) Then
                EnableTexPolys(True)
                EnableVecPolys(False)
                optTexture.Checked = True
                ShowTexturePoly()
                TabControl1.SelectedIndex = 2
            Else
                EnableTexPolys(False)
                EnableVecPolys(True)
                optVector.Checked = True
                ShowVectorPoly()
                TabControl1.SelectedIndex = 1
            End If
        Else
            BoxOrder.Enabled = False
            EnableTexPolys(False)
            optVector.Checked = True
            optTexture.Enabled = False
            EnableVecPolys(True)
            ShowVectorPoly()
            TabControl1.SelectedIndex = 1
        End If

        IsInit = False

        LoadForm()

    End Sub


    Private Sub ShowVectorPoly()

        List1.Items.Clear()

        Dim N, LT, K As Integer
        Dim A As String

        If POPMode = "Many" Or POPMode = "SHP" Or POPMode = "SUR" Then  ' many to show
            lbName.Enabled = False
            txtName.Enabled = False
            ckThisColor.BackColor = DefaultPolyColor
            ckThisColor.ForeColor = InvertColor(DefaultPolyColor)
        Else
            lbName.Enabled = True
            txtName.Enabled = True
            txtName.Text = Polys(0).Name
            ckThisColor.BackColor = Polys(0).Color
            ckThisColor.ForeColor = InvertColor(Polys(0).Color)
        End If

        LT = 0
        If POPMode = "SHP" Then
            A = ShapePolyGuid
            cmdSmooth.Enabled = False
            cmdSample.Enabled = False
        ElseIf POPMode = "SUR" Then
            A = BLNPolyGuid
            cmdSmooth.Enabled = False
            cmdSample.Enabled = False
        Else
            A = Polys(0).Guid
        End If

        If Not Polys(0).Type = "XXX" Then
            K = 0
            For N = 1 To NoOfPolyTypes
                K = K + 1
                List1.Items.Add(PolyTypes(N).Name)
                If PolyTypes(N).Guid = A Then LT = K
            Next N

        Else
            K = 0
            For N = 1 To NoOfPolyTypes
                K = K + 1
                List1.Items.Add(PolyTypes(N).Name)
                If PolyTypes(N).Type = "XXX" Then LT = K
            Next N
        End If

        List1.SelectedIndex = LT - 1

    End Sub

    Private Sub ShowTexturePoly()

        Dim a, b As String
        Dim N As Integer

        a = Polys(POPIndex).Type

        N = InStr(1, a, "//")
        a = Mid(a, N + 2)

        N = InStr(1, a, "//")
        b = Mid(a, 1, N - 1)
        ShowPolyTex(b)

        a = Mid(a, N + 2)
        N = InStr(1, a, "//")
        b = Mid(a, 1, N - 1)
        txtTexPri.Text = CStr(CInt(b))

        a = Mid(a, N + 2)
        N = InStr(1, a, "//")
        b = Mid(a, 1, N - 1)
        txtTexTileX.Text = CStr(CInt(b))

        a = Mid(a, N + 2)
        N = InStr(1, a, "//")
        b = Mid(a, 1, N - 1)
        txtTexTileY.Text = CStr(CInt(b))

        a = Mid(a, N + 2)
        N = InStr(1, a, "//")
        b = Mid(a, 1, N - 1)
        txtV1.Text = CStr(CInt(b))

        a = Mid(a, N + 2)
        N = InStr(1, a, "//")
        ckNight.CheckState = CInt(Mid(a, 1, N - 1))

        a = Mid(a, N + 2)

        lbPolyColor.BackColor = Polys(POPIndex).Color
        lbPolyColor.ForeColor = InvertColor(Polys(POPIndex).Color)

        PolyTexString = a


    End Sub

    Private Sub ShowPolyTex(ByVal Tex As String)

        If Tex = "" Then Exit Sub

        Try

            ChDrive(My.Application.Info.DirectoryPath)
            ChDir(My.Application.Info.DirectoryPath & "\Tools\")

            Dim ImageTool As String
            Dim BmpPath As String = AppPath & "\Tools\Work\temp.bmp"

            txtTexName.Text = Tex
            Dim TexPath As String = AppPath & "\Texture\" & Tex
            If My.Computer.FileSystem.FileExists(TexPath) Then
                ImageTool = "imagetool -nogui -nomip -32 Work\temp.bmp"
                File.Copy(TexPath, BmpPath, True)
                ExecCmd(ImageTool)
            Else
                FileCopy(AppPath & "\Tools\BMPs\none.jpg", BmpPath)
            End If

            Dim bmp As Image = System.Drawing.Image.FromFile(BmpPath)
            Dim cpy As New Bitmap(bmp)
            bmp.Dispose()
            imgTex.Image = cpy

        Catch ex As Exception
            MsgBox("There is a problem with the display of this image!", MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub CmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Dim N As Integer


        If POPMode = "Many" Then 'many to set
            Dirty = True
            For N = 1 To NoOfPolys
                If Polys(N).Selected And Not IsTexPoly(N) Then
                    SetThisPolyProperties(N)
                End If
            Next
            RebuildDisplay()
        ElseIf POPMode = "SHP" Then
            ShapePolyGuid = PolyTypes(ThisPolyType).Guid
        ElseIf POPMode = "SUR" Then
            BLNPolyGuid = PolyTypes(ThisPolyType).Guid
        Else
            Dirty = True
            Polys(POPIndex).Name = txtName.Text
            SetThisPolyProperties(POPIndex)
            RebuildDisplay()
        End If

        POPIndex = 0
        Dispose()

    End Sub

    Private Sub SetThisPolyProperties(ByVal N As Integer)

        Dim M As Integer
        Dim A As String

        If optTexture.Checked Then

            On Error GoTo erro

            A = "TEX//" & CStr(txtTexName.Text) & "//"

            If txtTexPri.Text = "" Then txtTexPri.Text = "4"
            If txtTexTileX.Text = "" Then txtTexTileX.Text = "1"
            If txtTexTileY.Text = "" Then txtTexTileY.Text = "1"
            If txtV1.Text = "" Then txtV1.Text = "5000"


            A = A & CStr(CInt(txtTexPri.Text)) & "//"
            A = A & CStr(CInt(txtTexTileX.Text)) & "//"
            A = A & CStr(CInt(txtTexTileY.Text)) & "//"
            A = A & CStr(CInt(txtV1.Text)) & "//"
            A = A & CStr(ckNight.CheckState) & "//"

            Polys(N).Type = A
            MakePolyTexString(N, False)
            Polys(N).Type = A & PolyTexString

            Polys(N).Color = lbPolyColor.BackColor

            Exit Sub
erro:
            MsgBox("Check your entries!", MsgBoxStyle.Critical)
            Exit Sub

        Else

            If ckThisColor.Checked Then
                Polys(N).Color = ckThisColor.BackColor
            Else
                Polys(N).Color = lbTexture.BackColor
            End If

            If PolyTypes(ThisPolyType).Type = "XXX" Then
                Polys(N).Guid = ParticularExcludeGUID
            Else
                Polys(N).Guid = PolyTypes(ThisPolyType).Guid
            End If

            If PolyTypes(ThisPolyType).Guid <> DefaultPolyFS9Guid Then
                Polys(N).Type = PolyTypes(ThisPolyType).Type
            End If

            If Polys(N).NoOfChilds > 0 Then
                For M = 1 To Polys(N).NoOfChilds
                    CopyProperties(N, Polys(N).Childs(M))
                Next
            End If

            Polys(N).Name = CheckVectorPolyName(N)

        End If

    End Sub

    Private Function CheckVectorPolyName(ByVal N As Integer) As String

        On Error GoTo erro1
        CheckVectorPolyName = Polys(N).Name
        If CheckVectorPolyName = "" Then CheckVectorPolyName = Str(Polys(N).NoOfPoints) & "_Pts_"
        Dim K As Integer = InStr(CheckVectorPolyName, "_")
        If K = 0 Then Exit Function
        Dim A As String = CheckVectorPolyName.Substring(K - 1, 5)
        If A = "_Pts_" Then
            CheckVectorPolyName = CheckVectorPolyName.Substring(0, K + 4) & PolyTypes(ThisPolyType).Name
        End If

erro1:

    End Function

    Private Sub CopyProperties(ByVal source As Integer, ByVal dest As Integer)

        Polys(dest).Color = Polys(source).Color
        Polys(dest).Guid = Polys(source).Guid
        Polys(dest).Type = Polys(source).Type


    End Sub

    Private Sub CmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        DialogResult = System.Windows.Forms.DialogResult.Cancel
        POPIndex = 0
        Dispose()

    End Sub


    Private Sub List1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles List1.MouseDown

        Dim Button As Short = e.Button \ &H100000

        If Button = 2 Then
            Dim LT As Integer
            LT = List1.SelectedIndex + 1
            My.Computer.Clipboard.SetText(PolyTypes(LT).Guid)
        End If

    End Sub


    Private Sub List1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles List1.SelectedIndexChanged

        Dim LT As Integer
        LT = List1.SelectedIndex + 1
        ThisPolyType = LT
        ShowVectorImage(LT)
        CheckIfXXX(LT)

    End Sub

    Private Sub ShowVectorImage(ByVal LT As Integer)

        Dim A As String
        Dim jpg As String = ".jpg"

        On Error GoTo erro1

        ' lbTexture.Text = VB6.GetItemString(List1, LT - 1)
        lbTexture.Text = List1.GetItemText(LT - 1)
        lbTexture.BackColor = PolyTypes(LT).Color
        lbTexture.ForeColor = InvertColor(lbTexture.BackColor)

        A = My.Application.Info.DirectoryPath & "\Tools\Bmps\" & PolyTypes(LT).Texture & jpg
        imgTexture.Image = System.Drawing.Image.FromFile(A)
        ImageFileName = A
        Exit Sub
erro1:
        MsgBox(A)

    End Sub



    Private Sub CheckIfXXX(ByVal K As Integer)

        If PolyTypes(K).Type = "XXX" Then
            lbExcludeCaption.Enabled = True
            lbExclude.Enabled = True
            lbExclude.Text = GetNameFromGuid(ParticularExcludeGUID)

        Else

            lbExcludeCaption.Enabled = False
            lbExclude.Enabled = False
            lbExclude.Text = ""
        End If


    End Sub

    Private Sub CmdDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDetail.Click

        If PolyTypes(ThisPolyType).TerrainIndex < 0 Then
            MsgBox("This type is not described in Terrain.cfg!")
            Exit Sub
        End If

        Dim TerrainFile, A, B, C, Key As String
        Dim N, Marker As Integer
        Dim F1 As Boolean

        If IsFSX Then
            TerrainFile = FSPath & "Terrain.cfg"
            C = "Description from FSX/Terrain.cfg"
        Else
            TerrainFile = AppPath & "\Tools\Terrain.cfg"
            C = "Description from Tools/Terrain.cfg"
        End If


        Key = "[Texture." & Trim(CStr(PolyTypes(ThisPolyType).TerrainIndex)) & "]"

        FileOpen(2, TerrainFile, OpenMode.Input)
        N = LOF(2)
        Marker = 0
        F1 = False
        B = ""
        Do While Marker < N
            A = LineInput(2)
            Marker = Marker + Len(A) + 2
            A = Trim(A)
            If F1 Then
                If A = "" Then Exit Do
                B = B & A & vbCrLf
            End If
            If Not F1 Then
                If A = Key Then F1 = True
            End If
        Loop
        MsgBox(B, MsgBoxStyle.Information, C)
        FileClose()

    End Sub

    Private Sub LbExclude_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbExclude.Click

        'ParticularExcludeGUID = lbExclude.Text
        If FrmTerrainExclude.ShowDialog = Windows.Forms.DialogResult.OK Then
            lbExclude.Text = GetNameFromGuid(ParticularExcludeGUID)
        End If

    End Sub


    Private Sub ImgTexture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgTexture.Click

        FrmImage.ShowDialog()

    End Sub


    Private Function GetNameFromGuid(ByVal GUID As String) As String

        Dim N As Integer

        GetNameFromGuid = "Nothing to Exclude"

        For N = PolyInit To NoOfPolyTypes
            If PolyTypes(N).Guid = GUID Then
                GetNameFromGuid = PolyTypes(N).Name
                Exit Function
            End If
        Next

        For N = LineInit To NoOfLineTypes
            If LineTypes(N).Guid = GUID Then
                GetNameFromGuid = LineTypes(N).Name
                Exit Function
            End If
        Next

    End Function


    Private Sub CmdColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdColor.Click

        ARGBColor = ckThisColor.BackColor
        If FrmTransparency.ShowDialog = Windows.Forms.DialogResult.OK Then
            ckThisColor.BackColor = ARGBColor
        End If
        ckThisColor.ForeColor = InvertColor(ARGBColor)

    End Sub


    Private Sub CmdColor2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdColor2.Click

        ARGBColor = lbPolyColor.BackColor
        If FrmTransparency.ShowDialog = Windows.Forms.DialogResult.OK Then
            lbPolyColor.BackColor = ARGBColor
        End If
        lbPolyColor.ForeColor = InvertColor(ARGBColor)


    End Sub

    Private Sub CmdUP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUP.Click

        If ThisParent = NoOfParents Then Exit Sub
        BackUp()

        Dim P1 As GPoly
        Dim NP As Integer = NextParent()
        Dim J, N As Integer

        P1 = Polys(NP)
        Polys(NP) = Polys(ThisPoly)
        Polys(ThisPoly) = P1

        For N = 1 To Polys(ThisPoly).NoOfChilds
            J = Polys(ThisPoly).Childs(N)
            Polys(J).NoOfChilds = -ThisPoly
        Next

        For N = 1 To Polys(NP).NoOfChilds
            J = Polys(NP).Childs(N)
            Polys(J).NoOfChilds = -NP
        Next

        ThisPoly = NP
        ThisParent = ThisParent + 1

        lbOrder.Text = "Poly #" & CStr(ThisParent) & " out of " & CStr(NoOfParents)
        RebuildDisplay()

    End Sub

    Private Function NextParent() As Integer

        NextParent = ThisPoly
        Do
            NextParent = NextParent + 1
            If Polys(NextParent).NoOfChilds >= 0 Then Exit Do
        Loop

    End Function

    Private Function PreParent() As Integer

        PreParent = ThisPoly
        Do
            PreParent = PreParent - 1
            If Polys(PreParent).NoOfChilds >= 0 Then Exit Do
        Loop


    End Function

    Private Sub CmdDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDown.Click

        If ThisParent = 1 Then Exit Sub
        BackUp()

        Dim P1 As GPoly
        Dim PP As Integer = PreParent()
        Dim J, N As Integer

        P1 = Polys(PP)
        Polys(PP) = Polys(ThisPoly)
        Polys(ThisPoly) = P1

        For N = 1 To Polys(ThisPoly).NoOfChilds
            J = Polys(ThisPoly).Childs(N)
            Polys(J).NoOfChilds = -ThisPoly
        Next

        For N = 1 To Polys(PP).NoOfChilds
            J = Polys(PP).Childs(N)
            Polys(J).NoOfChilds = -PP
        Next

        ThisPoly = PP
        ThisParent = ThisParent - 1

        lbOrder.Text = "Poly #" & CStr(ThisParent) & " out of " & CStr(NoOfParents)
        RebuildDisplay()

    End Sub

    Private Sub CmdTop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTop.Click

        If ThisParent = NoOfParents Then Exit Sub
        BackUp()

        Dim P1 As GPoly

        Dim J, K, N, NP As Integer

        P1 = Polys(ThisPoly)
        N = ThisPoly
        Do
            NP = NextParent()
            Polys(N) = Polys(NP)
            For K = 1 To Polys(N).NoOfChilds
                J = Polys(N).Childs(K)
                Polys(J).NoOfChilds = -N
            Next
            ThisParent = ThisParent + 1
            ThisPoly = NP
            If ThisParent = NoOfParents Then Exit Do
            N = NP
        Loop

        Polys(NP) = P1
        For K = 1 To Polys(NP).NoOfChilds
            J = Polys(NP).Childs(K)
            Polys(J).NoOfChilds = -NP
        Next

        lbOrder.Text = "Poly #" & CStr(ThisParent) & " out of " & CStr(NoOfParents)
        RebuildDisplay()

    End Sub

    Private Sub CmdBottom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBottom.Click

        'Dim P1 As GPoly
        'Dim N As Integer

        'If ThisPoly = 1 Then Exit Sub
        'BackUp()
        'P1 = Polys(ThisPoly)
        'For N = ThisPoly To 2 Step -1
        '    Polys(N) = Polys(N - 1)
        'Next
        'Polys(1) = P1
        'ThisPoly = 1

        'lbOrder.Text = "Poly #" & CStr(ThisPoly) & " out of " & CStr(NoOfParents)
        'RebuildDisplay()

        If ThisParent = 1 Then Exit Sub
        BackUp()

        Dim P1 As GPoly

        Dim J, K, N, PP As Integer

        P1 = Polys(ThisPoly)
        N = ThisPoly
        Do
            PP = PreParent()
            Polys(N) = Polys(PP)
            For K = 1 To Polys(N).NoOfChilds
                J = Polys(N).Childs(K)
                Polys(J).NoOfChilds = -N
            Next
            ThisParent = ThisParent - 1
            ThisPoly = PP
            If ThisParent = 1 Then Exit Do
            N = PP
        Loop

        Polys(PP) = P1
        For K = 1 To Polys(PP).NoOfChilds
            J = Polys(PP).Childs(K)
            Polys(J).NoOfChilds = -PP
        Next

        lbOrder.Text = "Poly #" & CStr(ThisParent) & " out of " & CStr(NoOfParents)
        RebuildDisplay()

    End Sub

    Private Sub EnableTexPolys(ByVal Flag As Boolean)


        imgTex.Visible = Flag
        cmdColor2.Enabled = Flag
        lbPolyColor.Enabled = Flag
        txtTexName.Enabled = Flag
        cmdTex.Enabled = Flag
        lbTexName.Enabled = Flag
        ckNight.Enabled = Flag
        boxTiling.Enabled = Flag
        LbV1.Enabled = Flag
        txtV1.Enabled = Flag
        txtTexPri.Enabled = Flag
        lbTexPri.Enabled = Flag
        lbTex1.Enabled = Flag


    End Sub

    Private Sub EnableVecPolys(ByVal Flag As Boolean)

        lbTexture.Enabled = Flag
        cmdColor.Enabled = Flag
        ckThisColor.Enabled = Flag
        Label1.Enabled = Flag
        Label2.Enabled = Flag
        Label3.Enabled = Flag
        lbExcludeCaption.Enabled = Flag
        cmdDetail.Enabled = Flag
        imgTexture.Visible = Flag
        lbExclude.Enabled = Flag
        List1.Enabled = Flag


    End Sub

    Private Sub CmdType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdType.Click
        If optVector.Checked Then TabControl1.SelectedIndex = 1
        If optTexture.Checked Then TabControl1.SelectedIndex = 2
    End Sub

    Private Sub OptVector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optVector.CheckedChanged

        If sender.Checked Then
            If IsInit Then Exit Sub
            EnableVecPolys(True)
            EnableTexPolys(False)
            ShowVectorPoly()
            TabControl1.SelectedIndex = 1
        End If

    End Sub

    Private Sub OptTexture_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTexture.CheckedChanged

        If sender.Checked Then
            If IsInit Then Exit Sub
            EnableVecPolys(False)
            EnableTexPolys(True)
            TabControl1.SelectedIndex = 2
        End If

    End Sub
    Private Sub LoadForm()

        Dim N As Integer
        Dim X, Y As Double
        Dim Flag As Boolean = False
        Dim n0, n1, n2 As Integer
        Dim k1, k2, k3 As Double
        Dim lat As Double

        Dim sxy, head As Double

        'added March 2009 Scott Smart 
        If POPMode = "SHP" Or POPMode = "SUR" Then
            boxSlope.Enabled = False
            boxAltitude.Enabled = False
            Exit Sub
        End If

        X = 0
        Y = Polys(POPIndex).GPoints(1).alt

        For N = 1 To Polys(POPIndex).NoOfPoints
            X = X + Polys(POPIndex).GPoints(N).alt
            If Y <> Polys(POPIndex).GPoints(N).alt Then Flag = True
        Next

        txtAlt.Text = X / Polys(POPIndex).NoOfPoints

        If Flag = False Then
            txtHead.Text = 0
            txtSlope.Text = 0
            txtAlt0.Text = Polys(POPIndex).GPoints(1).alt
            txtPt0.Text = 1
            Exit Sub
        End If

        Get3Points(POPIndex, n0, n1, n2, lat)
        GetSlopes(POPIndex, n0, n1, n2, k1, k2, k3)
        GetMaximumSlope(k1, k2, lat, sxy, head)

        txtHead.Text = head
        txtSlope.Text = sxy * 1000
        txtAlt0.Text = Polys(POPIndex).GPoints(n1).alt
        txtPt0.Text = n1

        lbSX.Text = "SlopeX = " & Mid(Str(k1), 1, 13)
        lbSY.Text = "SlopeY = " & Mid(Str(k2), 1, 13)

    End Sub

    Private Sub CmdSlope_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSlope.Click

        Dim N As Integer

        On Error GoTo erro

        If POPMode = "One" Then
            SetPolyAltitudes(POPIndex)
        Else
            For N = 1 To NoOfPolys
                If Polys(N).Selected And Polys(N).NoOfChilds >= 0 Then
                    SetPolyAltitudes(N)
                End If
            Next
        End If

        LoadForm()

        Exit Sub
erro:
        MsgBox("Check your entries!", MsgBoxStyle.Critical)

    End Sub

    Private Sub SetPolyAltitudes(ByVal N As Integer)

        Dim Head, lat, lon, sxy As Double
        Dim k1, k2, k3 As Double

        Dim x0, y0, z0 As Double
        Dim P As Integer
        Dim K, J As Integer

        Head = CDbl(txtHead.Text)
        sxy = CDbl(txtSlope.Text)

        lat = 0
        For K = 1 To Polys(N).NoOfPoints
            lat = lat + Polys(N).GPoints(K).lat
        Next
        lat = lat / Polys(N).NoOfPoints

        Head = Head * PI / 180.0#
        sxy = sxy / 1000.0#

        k1 = sxy * System.Math.Sin(Head)
        k2 = sxy * System.Math.Cos(Head)

        k1 = k1 * MetersPerDegLon(lat)
        k2 = k2 * MetersPerDegLat

        P = CInt(txtPt0.Text)

        x0 = Polys(N).GPoints(P).lon
        y0 = Polys(N).GPoints(P).lat
        z0 = CDbl(txtAlt0.Text)

        k3 = z0 - k1 * x0 - k2 * y0

        For K = 1 To Polys(N).NoOfPoints
            lat = Polys(N).GPoints(K).lat
            lon = Polys(N).GPoints(K).lon
            Polys(N).GPoints(K).alt = k1 * lon + k2 * lat + k3
        Next

        For P = 1 To Polys(N).NoOfChilds
            J = Polys(N).Childs(P)
            For K = 1 To Polys(J).NoOfPoints
                lat = Polys(J).GPoints(K).lat
                lon = Polys(J).GPoints(K).lon
                Polys(J).GPoints(K).alt = k1 * lon + k2 * lat + k3
            Next
        Next


    End Sub

    Private Sub CmdAlt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAlt.Click

        Dim N As Integer
        Dim X As Double

        On Error GoTo erro1

        X = CDbl(txtAlt.Text)

        If POPMode = "One" Then
            SetConstantAltitude(POPIndex, X)
        Else
            For N = 1 To NoOfPolys
                If Polys(N).Selected And Polys(N).NoOfChilds >= 0 Then
                    SetConstantAltitude(N, X)
                End If
            Next
        End If

        LoadForm()
        Exit Sub

erro1:
        MsgBox("Check altitude value!", MsgBoxStyle.Critical)

    End Sub

    Private Sub SetConstantAltitude(ByVal N As Integer, ByVal H As Double)

        Dim K, P, J As Integer

        For K = 1 To Polys(N).NoOfPoints
            Polys(N).GPoints(K).alt = H
        Next

        For P = 1 To Polys(N).NoOfChilds
            J = Polys(N).Childs(P)
            For K = 1 To Polys(J).NoOfPoints
                Polys(J).GPoints(K).alt = H
            Next
        Next

    End Sub


    Private Sub CmdHelpSlope_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHelpSlope.Click

        Dim A As String

        A = "In this mode you specify: (i) the altitude and index of one" & vbCrLf
        A = A & "point (ii) the heading of maximum slope and (iii) the maximum" & vbCrLf
        A = A & "slope expressed as the altitude increase in meters per one" & vbCrLf
        A = A & "thousand meters of horizontal shift."

        MsgBox(A, MsgBoxStyle.Information)

    End Sub

    'Private Sub Get3Points(ByVal N As Integer, ByRef N1 As Integer, ByRef N2 As Integer, ByRef N3 As Integer, ByRef lat As Double)

    '    Dim NP, J, K As Integer
    '    Dim X1, X2, DX, DY, D As Double


    '    NP = Polys(N).NoOfPoints
    '    N1 = 1
    '    N2 = 1
    '    X1 = Polys(N).GPoints(1).lon
    '    X2 = Polys(N).GPoints(1).lon

    '    lat = 0

    '    For J = 1 To NP
    '        lat = lat + Polys(N).GPoints(J).lat
    '        If Polys(N).GPoints(J).lon < X1 Then
    '            N1 = J
    '            X1 = Polys(N).GPoints(J).lon
    '        End If
    '        If Polys(N).GPoints(J).lon > X2 Then
    '            N2 = J
    '            X2 = Polys(N).GPoints(J).lon
    '        End If
    '    Next

    '    lat = lat / NP

    '    X1 = (Polys(N).GPoints(N1).lon - Polys(N).GPoints(N2).lon)
    '    X1 = X1 * X1
    '    X2 = (Polys(N).GPoints(N1).lat - Polys(N).GPoints(N2).lat)
    '    X2 = X2 * X2
    '    D = System.Math.Sqrt(X1 + X2)
    '    For K = 1 To NP
    '        If K <> N1 And K <> N2 Then
    '            X1 = (Polys(N).GPoints(N1).lon - Polys(N).GPoints(K).lon)
    '            X1 = X1 * X1
    '            X2 = (Polys(N).GPoints(N1).lat - Polys(N).GPoints(K).lat)
    '            X2 = X2 * X2
    '            DX = System.Math.Sqrt(X1 + X2)
    '            X1 = (Polys(N).GPoints(N2).lon - Polys(N).GPoints(K).lon)
    '            X1 = X1 * X1
    '            X2 = (Polys(N).GPoints(N2).lat - Polys(N).GPoints(K).lat)
    '            X2 = X2 * X2
    '            DY = System.Math.Sqrt(X1 + X2)
    '            DX = DX + DY
    '            If DX > D Then
    '                D = DX
    '                N3 = K
    '            End If
    '        End If
    '    Next

    'End Sub

    'Private Sub GetSlopes(ByVal N As Integer, ByVal N0 As Integer, ByVal N1 As Integer, ByVal N2 As Integer, ByRef K1 As Double, ByRef K2 As Double, ByRef K3 As Double)

    '    Dim z00, z01, z02 As Double
    '    Dim x00, x01, x02 As Double
    '    Dim y00, y01, y02 As Double
    '    Dim a1, a2 As Double

    '    x01 = Polys(N).GPoints(N0).lon - Polys(N).GPoints(N1).lon
    '    x02 = Polys(N).GPoints(N0).lon - Polys(N).GPoints(N2).lon
    '    y01 = Polys(N).GPoints(N0).lat - Polys(N).GPoints(N1).lat
    '    y02 = Polys(N).GPoints(N0).lat - Polys(N).GPoints(N2).lat
    '    z01 = Polys(N).GPoints(N0).alt - Polys(N).GPoints(N1).alt
    '    z02 = Polys(N).GPoints(N0).alt - Polys(N).GPoints(N2).alt

    '    If y01 = 0 Then
    '        If x01 = 0 Then
    '            K1 = 0
    '        Else
    '            K1 = z01 / x01
    '        End If
    '    Else
    '        a1 = y02 / y01
    '        z00 = z01 * a1 - z02
    '        x00 = x01 * a1 - x02
    '        K1 = z00 / x00
    '    End If


    '    If x01 = 0 Then
    '        If y01 = 0 Then
    '            K2 = 0
    '        Else
    '            K2 = z01 / y01
    '        End If
    '    Else
    '        a2 = x02 / x01
    '        z00 = z01 * a2 - z02
    '        y00 = y01 * a2 - y02
    '        K2 = z00 / y00
    '    End If

    '    K3 = Polys(N).GPoints(N0).alt - K1 * Polys(N).GPoints(N0).lon - K2 * Polys(N).GPoints(N0).lat

    'End Sub

    Private Sub GetMaximumSlope(ByVal k1 As Double, ByVal k2 As Double, ByVal lat As Double, ByRef sxy As Double, ByRef head As Double)

        Dim sx, sy As Double

        sxy = 0
        head = 0

        If k1 = 0 And k2 = 0 Then Exit Sub

        sx = k1 / MetersPerDegLon(lat)
        sy = k2 / MetersPerDegLat

        sxy = System.Math.Sqrt(sx * sx + sy * sy)

        head = 0

        If sx = 0 Then
            If sy > 0 Then
                head = 0
                Exit Sub
            Else
                head = 180
                Exit Sub
            End If
        End If

        If sy = 0 Then
            If sx > 0 Then
                head = 90
                Exit Sub
            Else
                head = 270
                Exit Sub
            End If
        End If

        If sx > 0 Then
            head = 90 - System.Math.Atan(sy / sx) * 180 / PI
            Exit Sub
        Else
            head = 270 - System.Math.Atan(sy / sx) * 180 / PI
            Exit Sub
        End If

    End Sub


    Private Function IsTexPoly(ByVal N As Integer) As Boolean

        IsTexPoly = False
        If Mid(Polys(N).Type, 1, 3) = "TEX" Then IsTexPoly = True

    End Function



    Private Sub CmdSample_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSample.Click
        frmLPSample.ShowDialog()
    End Sub

    Private Sub CmdSmooth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSmooth.Click
        FrmLPSmooth.ShowDialog()
    End Sub

    Private Sub ImgTex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgTex.Click

        Dim Flag As Boolean

        'On Error GoTo erro

        Flag = False
        If txtTexName.Text = "" Then Flag = True
        If txtTexName.Text = "na.bmp" Then Flag = True
        If POPMode <> "One" Then Flag = True

        If Flag Then Exit Sub

        PolyTex = txtTexName.Text
        PolyTexIndex = POPIndex
        MakePolyTexString(ThisPoly, False)

        frmTexPoly.ShowDialog()

        Exit Sub

erro:


    End Sub

    Private Sub CmdTex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTex.Click

        Dim A, B As String

        Dim Tex As String = ""

        A = "FSX Texture File (*.BMP)|*.BMP"
        B = "SBuilderX: Open Texture File"

        A = FileNameToOpen(A, B, "TEX")

        If A = "" Then
            Exit Sub
        End If

        Tex = Path.GetFileName(A)
        Dim TexPath As String = AppPath & "\Texture\" & Tex
        If File.Exists(TexPath) Then
            If TexPath <> A Then

                B = "This file already exists in the ../SBuilder/Texture" & vbCrLf
                B = B & "folder and it will be overwriten! Do you want to continue?"
                If MsgBox(B, MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            End If

        End If

        Try
            If TexPath <> A Then
                File.Delete(TexPath)
                File.Copy(A, TexPath, True)
            End If
        Catch ex As Exception
            MsgBox("The file could not be loaded!", MsgBoxStyle.Exclamation)
            Exit Sub
        End Try

        ShowPolyTex(Tex)

    End Sub


    Private Sub TabPage3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage3.Click

        If Not (POPMode = "Many" Or POPMode = "SHP" Or POPMode = "SUR") Then  ' many to show
            optTexture.Checked = True
        End If


    End Sub

    Private Sub TabPage2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage2.Click


        If Not (POPMode = "Many" Or POPMode = "SHP" Or POPMode = "SUR") Then  ' many to show
            optVector.Checked = True
        End If


    End Sub

End Class

