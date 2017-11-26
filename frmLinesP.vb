Option Strict Off
Option Explicit On

Imports System.Windows.Forms

Friend Class FrmLinesP

    Private ThisLineType As Integer
    Private ThisExtrusionType As Integer

    'Private ThisObjectType As Integer
    Private ObjWidth As Single
    Private ObjLength As Single

    Friend ProfileGuid As String
    Friend MaterialGuid As String
    Friend PylonGuid As String
    Friend ExtrusionWidth As Double
    Friend ExtrusionProbability As Double
    Friend SuppressPlatform As Boolean
    Friend Complexity As Integer  ' for extrusions

    Private Init As Boolean = True ' controls call to LoadList

    Private Sub SetThisVectorLineProperties(ByVal N As Integer)

        If ckThisColor.Checked Then
            Lines(N).Color = ckThisColor.BackColor
        Else
            Lines(N).Color = labelVectorTexture.BackColor
        End If

        Lines(N).Guid = LineTypes(ThisLineType).Guid

        If LineTypes(ThisLineType).Guid <> DefaultLineFS9Guid Then
            Dim A As String
            A = Mid(LineTypes(ThisLineType).Type, 1, 3)
            If A = "FWX" Then
                Lines(N).Type = "FWX" & cbLanes.SelectedItem & cbDir.SelectedItem
            Else
                Lines(N).Type = LineTypes(ThisLineType).Type
            End If
        End If

        Lines(N).Name = CheckVectorLineName(N)

    End Sub

    Private Sub SetThisExtrusionLineProperties(ByVal N As Integer)

        On Error GoTo erro

        Lines(N).Color = ckThisColor.BackColor
        Lines(N).Name = CheckExtrusionLineName(N)

        Lines(N).Guid = labelProfile.Text
        Lines(N).Type = "EXT|" & MaterialGuid & "|" & PylonGuid & "|" _
                        & Complexity.ToString & "|" & Str(ExtrusionWidth) & "|" _
                        & Str(ExtrusionProbability) & "|" & SuppressPlatform.ToString
        Dim K As Integer

        For K = 1 To Lines(N).NoOfPoints
            Lines(N).GLPoints(K).wid = ExtrusionWidth
        Next

        Exit Sub
erro:
        MsgBox("Check your Extrusion Line parameters!", MsgBoxStyle.Critical)

        Exit Sub

    End Sub


    Private Sub SetThisTexturedLineProperties(ByVal N As Integer)

        On Error GoTo erro

        Dim Complex As Integer = cmbComplex.SelectedIndex

        Dim X As String = "Standing"
        If optLying.Checked Then X = "Lying"

        If txtTexName.Text = "" Then
            MsgBox("You need to specify a texture for the line!", MsgBoxStyle.Exclamation)
        End If

        Dim A As String = "TEX|" & X & "|"
        A = A & CStr(txtTexName.Text) & "|"

        If txtTexPri.Text = "" Then txtTexPri.Text = "4"
        If txtV1.Text = "" Then txtV1.Text = "15000"

        A = A & CStr(CInt(txtTexPri.Text)) & "|"
        A = A & CStr(CInt(txtV1.Text)) & "|"
        A = A & Complex.ToString & "|"

        A = A & CStr(ckNight.Checked) & "|"

        X = "Tile"
        If optStretch.Checked Then X = "Stretch"
        If optFull.Checked Then X = "FullTile"

        A = A & X

        Lines(N).Type = A

        Lines(N).Color = ckThisColor.BackColor

        Lines(N).Name = CheckTexturedLineName(N)

        Exit Sub

erro:
        MsgBox("Check your Textured Line parameters!", MsgBoxStyle.Critical)

        Exit Sub

    End Sub

    Private Sub SetThisLineOfObjectsProperties(ByVal N As Integer)

        On Error GoTo erro

        If lstLib.SelectedIndex = -1 Then Exit Sub ' because unknown

        Dim Complexity As Integer = cmbComplexity.SelectedIndex

        Lines(N).Color = ckThisColor.BackColor
        Lines(N).Name = CheckLineOfObjectsName(N)

        Lines(N).Guid = labelLibID.Text
        Lines(N).Type = "OBJ|" & Trim(Str(ObjWidth)) & "|" & Trim(Str(ObjLength)) & "|" & Complexity.ToString
        Exit Sub

erro:
        MsgBox("Check your Line of Objects parameters!", MsgBoxStyle.Critical)

        Exit Sub

    End Sub

    Private Function CheckLineOfObjectsName(ByVal N) As String

        CheckLineOfObjectsName = Lines(N).Name
        If CheckLineOfObjectsName = "" Then CheckLineOfObjectsName = Str(Lines(N).NoOfPoints) & "_Pts_"
        Dim K As Integer = InStr(CheckLineOfObjectsName, "_")
        If K = 0 Then Exit Function
        Dim A As String = CheckLineOfObjectsName.Substring(K - 1, 5)
        If A = "_Pts_" Then
            CheckLineOfObjectsName = CheckLineOfObjectsName.Substring(0, K + 4) & labelLibID.Text
        End If

    End Function

    Private Function CheckVectorLineName(ByVal N As Integer) As String

        On Error GoTo erro1
        CheckVectorLineName = Lines(N).Name
        If CheckVectorLineName = "" Then CheckVectorLineName = Str(Lines(N).NoOfPoints) & "_Pts_"
        Dim K As Integer = InStr(CheckVectorLineName, "_")
        If K = 0 Then Exit Function
        Dim A As String = CheckVectorLineName.Substring(K - 1, 5)
        If A = "_Pts_" Then
            CheckVectorLineName = CheckVectorLineName.Substring(0, K + 4) & LineTypes(ThisLineType).Name
        End If
erro1:

    End Function

    Private Function CheckExtrusionLineName(ByVal N As Integer) As String

        CheckExtrusionLineName = Lines(N).Name
        If CheckExtrusionLineName = "" Then CheckExtrusionLineName = Str(Lines(N).NoOfPoints) & "_Pts_"
        Dim K As Integer = InStr(CheckExtrusionLineName, "_")
        If K = 0 Then Exit Function
        Dim A As String = CheckExtrusionLineName.Substring(K - 1, 5)
        If A = "_Pts_" Then
            CheckExtrusionLineName = CheckExtrusionLineName.Substring(0, K + 4) & ExtrusionTypes(ThisExtrusionType).Name
        End If

    End Function

    Private Function CheckTexturedLineName(ByVal N As Integer) As String

        CheckTexturedLineName = Lines(N).Name
        If CheckTexturedLineName = "" Then CheckTexturedLineName = Str(Lines(N).NoOfPoints) & "_Pts_"
        Dim K As Integer = InStr(CheckTexturedLineName, "_")
        If K = 0 Then Exit Function
        Dim A As String = CheckTexturedLineName.Substring(K - 1, 5)
        If A = "_Pts_" Then
            CheckTexturedLineName = CheckTexturedLineName.Substring(0, K + 4) & "Line_" & txtTexName.Text
        End If

    End Function


    Private Sub FrmLinesP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        LoadForm() ' only used here

    End Sub

    Private Sub LoadForm()

        Dim Guid As String

        If POPMode = "SHP" Then
            Guid = ShapeLineGuid
            LoadListVector(Guid)
            cmdSmooth.Enabled = False
            cmdSample.Enabled = False
            EnableVectorLine(True)
            ckThisColor.Enabled = False
            cmdColor.Enabled = False
            EnableName(False)
            EnableReverse(False)
            EnableWidthAndAltitude(False)
            boxType.Enabled = False
            EnableExtrusionLine(False)
            EnableTexturedLine(False)
            EnableLineOfObjects(False)
            TabControl1.SelectedIndex = 1
            Init = False
            Exit Sub
        End If

        If POPMode = "SUR" Then
            Guid = BLNLineGuid
            LoadListVector(Guid)
            cmdSmooth.Enabled = False
            cmdSample.Enabled = False
            EnableVectorLine(True)
            ckThisColor.Enabled = False
            cmdColor.Enabled = False
            EnableName(False)
            EnableReverse(False)
            EnableWidthAndAltitude(False)
            boxType.Enabled = False
            EnableExtrusionLine(False)
            EnableTexturedLine(False)
            EnableLineOfObjects(False)
            TabControl1.SelectedIndex = 1
            Init = False
            Exit Sub
        End If

        If POPMode = "Many" Then  ' many to show
            EnableName(False)
            ckThisColor.BackColor = DefaultLineColor
            ckThisColor.ForeColor = InvertColor(DefaultLineColor)
            Guid = Lines(POPIndex).Guid
            LoadListVector(Guid)
            EnableVectorLine(True)
            EnableWidthAndAltitude(True)
            boxType.Enabled = False
            EnableExtrusionLine(False)
            EnableTexturedLine(False)
            EnableLineOfObjects(False)
            Init = False
            Exit Sub
        End If

        ' or just one to show
        EnableName(True)
        txtName.Text = Lines(POPIndex).Name
        ckThisColor.BackColor = Lines(POPIndex).Color
        ckThisColor.ForeColor = InvertColor(Lines(POPIndex).Color)

        Guid = Lines(POPIndex).Guid
        LoadWidthAndAltitude()
        LoadListVector(Guid)
        LoadListExtrusion(Guid)
        LoadListObjects(Guid)
        Init = False   ' before the opts

        Try
            Dim A As String = Lines(POPIndex).Type.Substring(0, 3).ToUpper
            If A = "EXT" Then
                optExtrusion.Checked = True
                GetExtrusionLine(POPIndex)
            ElseIf A = "TEX" Then
                optTexture.Checked = True
                GetTexturedLine(POPIndex)
            ElseIf A = "OBJ" Then
                optObjects.Checked = True
                GetLineOfObjects(POPIndex)
            Else
                optVector.Checked = False
                optVector.Checked = True
            End If
        Catch ex As Exception   ' line has no type yet
            optVector.Checked = False
            optVector.Checked = True
        End Try

    End Sub

    Private Sub LoadListVector(ByVal Guid As String)

        Dim K, N, LT As Integer

        listVector.Items.Clear()

        LT = 1
        K = 0
        For N = 1 To NoOfLineTypes
            K = K + 1
            listVector.Items.Add(LineTypes(N).Name)
            If LineTypes(N).Guid = Guid Then LT = K
        Next N

        listVector.SelectedIndex = LT - 1

        ShowThisVectorType(LT)
        CheckIfFWX(LT)
        ThisLineType = LT

    End Sub

    Private Sub LoadListExtrusion(ByVal guid As String)

        Dim K, N, LT As Integer

        listExtrusion.Items.Clear()

        LT = 1
        K = 0
        For N = 1 To NoOfExtrusionTypes
            K = K + 1
            listExtrusion.Items.Add(ExtrusionTypes(N).Name)
            If ExtrusionTypes(N).Profile = guid Then LT = K
        Next N
        listExtrusion.SelectedIndex = LT - 1

        labelProfile.Text = ExtrusionTypes(LT).Profile
        ShowThisExtrusionType(LT)
        ThisExtrusionType = LT

    End Sub

    Private Sub LoadListObjects(ByVal Guid As String)

        Dim N, K As Integer
        Dim a As String
        Dim LibCat As String = ""
        Dim g

        If NoOfLibCategories = 0 Then Exit Sub

        labelLibID.Text = Guid

        'first remove
        lstLib.Items.Clear()
        cmbLibCat.Items.Clear()

        For K = 1 To NoOfLibCategories
            cmbLibCat.Items.Add(LibCategories(K).Name)
        Next K

        Dim Flag As Boolean = False
        For K = 1 To NoOfLibCategories
            N = 0
            For Each g In LibCategories(K).Objs
                If g.ID = Guid Then
                    Flag = True
                    Exit For
                End If
                N = N + 1
            Next
            If Flag Then Exit For
        Next

        labelLibID.Text = Guid

        If Flag = False Then K = 1
        'N = 0

        'End If

        ' select category
        LibCat = LibCategories(K).Name
        cmbLibCat.SelectedIndex = K - 1
        ' fill objects and set selected
        For Each g In LibCategories(K).Objs
            lstLib.Items.Add(g.Name)
        Next

        If Flag = False Then
            a = My.Application.Info.DirectoryPath & "\tools\bmps\unknown.jpg"  ' no index selected!
        Else
            lstLib.SelectedIndex = N
            a = LibObjectsPath & "\" & LibCat & "\" & labelLibID.Text & ".jpg"
            ImageFileNameTrue = a
            If Not File.Exists(a) Then
                a = My.Application.Info.DirectoryPath & "\tools\bmps\none.jpg"
            End If
        End If

        Dim fs As New System.IO.FileStream(a, IO.FileMode.Open, IO.FileAccess.Read)
        imgLib.Image = Image.FromStream(fs)
        fs.Close()

        ImageFileName = a

    End Sub

    Private Sub GetTexturedLine(ByVal K As Integer)

        Dim A, B As String
        Dim N As Integer

        A = Lines(K).Type
        N = InStr(1, A, "|")
        A = Mid(A, N + 1)
        ' TEX| was removed

        N = InStr(1, A, "|")
        B = Mid(A, 1, 1)
        If B = "S" Then
            optStanding.Checked = True
            optLying.Checked = False
        ElseIf B = "L" Then
            optStanding.Checked = False
            optLying.Checked = True
        End If

        A = Mid(A, N + 1)
        N = InStr(1, A, "|")
        B = Mid(A, 1, N - 1)
        ShowLineTex(B)

        A = Mid(A, N + 1)
        N = InStr(1, A, "|")
        B = Mid(A, 1, N - 1)
        txtTexPri.Text = CStr(CInt(B))

        A = Mid(A, N + 1)
        N = InStr(1, A, "|")
        B = Mid(A, 1, N - 1)
        txtV1.Text = CStr(CInt(B))

        A = Mid(A, N + 1)
        N = InStr(1, A, "|")
        B = Mid(A, 1, N - 1)
        cmbComplex.SelectedIndex = CInt(B)

        A = Mid(A, N + 1)
        N = InStr(1, A, "|")
        B = Mid(A, 1, N - 1)
        ckNight.Checked = CBool(B)

        A = Mid(A, N + 1, 1)

        If A = "T" Then
            optTile.Checked = True
            optStretch.Checked = False
            optFull.Checked = False
        ElseIf A = "S" Then
            optTile.Checked = False
            optStretch.Checked = True
            optFull.Checked = False
        ElseIf A = "F" Then
            optTile.Checked = False
            optStretch.Checked = False
            optFull.Checked = True
        End If

        ckThisColor.BackColor = Lines(K).Color
        ckThisColor.ForeColor = InvertColor(Lines(K).Color)

    End Sub

    Private Sub LoadWidthAndAltitude()

        Dim N As Integer
        Dim X As Double

        'altitude
        X = 0
        For N = 1 To Lines(POPIndex).NoOfPoints
            X = X + Lines(POPIndex).GLPoints(N).alt
        Next
        txtAlt.Text = X / Lines(POPIndex).NoOfPoints

        ' progressive width
        txtWidth1.Text = CStr(Lines(POPIndex).GLPoints(1).wid)
        txtWidth2.Text = CStr(Lines(POPIndex).GLPoints(Lines(POPIndex).NoOfPoints).wid)

        Dim W As Double

        W = 0
        For N = 1 To Lines(POPIndex).NoOfPoints
            W = W + Lines(POPIndex).GLPoints(N).wid
        Next

        txtWidth.Text = CStr(W / Lines(POPIndex).NoOfPoints)

    End Sub


    Private Sub ShowThisVectorType(ByVal LT As Integer)

        Dim A As String
        Dim jpg As String = ".jpg"

        On Error GoTo erro1

        labelVectorTexture.BackColor = LineTypes(LT).Color
        labelVectorTexture.ForeColor = InvertColor(labelVectorTexture.BackColor)
        ' labelVectorTexture.Text = VB6.GetItemString(listVector, LT - 1)
        labelVectorTexture.Text = listVector.GetItemText(LT - 1)
        A = My.Application.Info.DirectoryPath & "\Tools\Bmps\" & LineTypes(LT).Texture & jpg
        imgVector.Image = System.Drawing.Image.FromFile(A)
        ImageFileName = A
        Exit Sub
erro1:
        MsgBox(A)

    End Sub


    Private Sub ShowThisExtrusionType(ByVal LT)

        Dim A As String
        Dim jpg As String = ".jpg"
        Dim closejpg As String = "_close.jpg"
        Dim C As Color

        On Error GoTo erro1
        C = ExtrusionTypes(LT).Color

        labelProfile.BackColor = C
        C = InvertColor(C)
        labelProfile.ForeColor = C

        labelProfile.Text = ExtrusionTypes(LT).Profile
        ProfileGuid = ExtrusionTypes(LT).Profile
        MaterialGuid = ExtrusionTypes(LT).Material
        PylonGuid = ExtrusionTypes(LT).Pylon
        ExtrusionWidth = Val(ExtrusionTypes(LT).Width)
        ExtrusionProbability = 0.5
        SuppressPlatform = True
        Complexity = 2

        A = My.Application.Info.DirectoryPath & "\Tools\Bmps\" & ExtrusionTypes(LT).Name & jpg
        imgExtrusion.Image = System.Drawing.Image.FromFile(A)
        ImageFileName = My.Application.Info.DirectoryPath & "\Tools\Bmps\" & ExtrusionTypes(LT).Name & closejpg
        Exit Sub
erro1:
        MsgBox("Image for Extrusion Bridge could not be used!")

    End Sub

    Private Sub GetExtrusionLine(ByVal N As Integer)

        Dim A As String
        Dim J As Integer

        A = Lines(N).Type.Substring(4)
        J = InStr(A, "|")
        MaterialGuid = A.Substring(0, J - 1)

        A = A.Substring(J)
        J = InStr(A, "|")
        PylonGuid = A.Substring(0, J - 1)

        A = A.Substring(J)
        J = InStr(A, "|")
        Complexity = CInt(A.Substring(0, J - 1))

        A = A.Substring(J)
        J = InStr(A, "|")
        ExtrusionWidth = Val(A.Substring(0, J - 1))

        A = A.Substring(J)
        J = InStr(A, "|")
        ExtrusionProbability = Val(A.Substring(0, J - 1))

        A = A.Substring(J)
        SuppressPlatform = CBool(A)

        ckThisColor.BackColor = Lines(N).Color
        ckThisColor.ForeColor = InvertColor(Lines(N).Color)

    End Sub

    Private Sub GetLineOfObjects(ByVal N As Integer)


        Dim A As String
        Dim J As Integer

        A = Lines(N).Type.Substring(4)
        J = InStr(A, "|")
        ObjWidth = Val(A.Substring(0, J - 1))

        A = A.Substring(J)
        J = InStr(A, "|")
        ObjLength = Val(A.Substring(0, J - 1))

        A = A.Substring(J)

        cmbComplexity.SelectedIndex = CInt(A)

        Dim X As Double = 0
        For J = 1 To Lines(N).NoOfPoints
            X = X + Lines(N).GLPoints(J).wid
        Next

        txtHeading.Text = Format(X / Lines(N).NoOfPoints, "0.00")

        ckThisColor.BackColor = Lines(N).Color
        ckThisColor.ForeColor = InvertColor(Lines(N).Color)

    End Sub

    Private Sub CheckIfFWX(ByVal K As Integer)

        Dim A, B As String
        A = Mid(LineTypes(K).Type, 1, 3)
        If A = "FWX" Then
            EnableTraffic(True)
            B = Mid(Lines(POPIndex).Type, 1, 3)
            If B = "FWX" Then
                cbLanes.Text = Mid(Lines(POPIndex).Type, 4, 1)
                cbDir.Text = Mid(Lines(POPIndex).Type, 5, 1)
            End If
        Else
            EnableTraffic(False)
        End If

    End Sub

    Private Sub EnableVectorLine(ByVal Flag As Boolean)

        Label5.Enabled = Flag
        labelVectorTexture.Enabled = Flag
        listVector.Enabled = Flag
        imgVector.Enabled = Flag
        cmdDetail.Enabled = Flag
        EnableReverse(True)

    End Sub

    Private Sub EnableExtrusionLine(ByVal Flag As Boolean)

        lbguid.Enabled = Flag
        labelProfile.Enabled = Flag
        listExtrusion.Enabled = Flag
        imgExtrusion.Enabled = Flag
        lbExt1.Enabled = Flag
        boxWidth.Enabled = Flag
        EnableReverse(False)
        cmdExtrusionProperties.Enabled = Flag

    End Sub

    Private Sub EnableTexturedLine(ByVal Flag As Boolean)

        'txtTexName.Enabled = Flag
        'cmdTex.Enabled = Flag
        'ckNight.Enabled = Flag

        boxTexType.Enabled = Flag
        boxTexTexture.Enabled = Flag
        imgTex.Enabled = Flag


        'LbV1.Enabled = Flag
        'lbTexPri.Enabled = Flag
        'txtV1.Enabled = Flag
        'txtTexPri.Enabled = Flag
        'EnableReverse(False)

    End Sub

    Private Sub EnableLineOfObjects(ByVal Flag As Boolean)

        labelComplexity.Enabled = Flag
        labelLibID.Enabled = Flag
        labelCat.Enabled = Flag
        boxHeading.Enabled = Flag
        cmbComplexity.Enabled = Flag
        cmbLibCat.Enabled = Flag
        imgLib.Enabled = Flag
        lstLib.Enabled = Flag
        EnableReverse(False)

    End Sub

    Private Sub EnableWidthAndAltitude(ByVal flag As Boolean)

        boxWidth.Enabled = flag
        boxProgressive.Enabled = flag
        boxAltitude.Enabled = flag

    End Sub

    Private Sub EnableReverse(ByVal flag As Boolean)

        cmdWinding.Enabled = flag
        labelReverse.Enabled = flag

    End Sub

    Private Sub EnableName(ByVal flag As Boolean)

        labelName.Enabled = flag
        txtName.Enabled = flag

    End Sub

    Private Sub EnableTraffic(ByVal Flag As Boolean)

        labelLanes.Enabled = Flag
        cbLanes.Enabled = Flag
        labelDir.Enabled = Flag
        cbDir.Enabled = Flag

    End Sub

    Private Sub CmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Dim N As Integer

        If POPMode = "Many" Then 'many to set
            Dirty = True
            For N = 1 To NoOfLines
                If Lines(N).Selected Then
                    SetThisLineProperties(N)
                End If
            Next
            RebuildDisplay()
        ElseIf POPMode = "SHP" Then
            ShapeLineGuid = LineTypes(ThisLineType).Guid
        ElseIf POPMode = "SUR" Then
            BLNLineGuid = LineTypes(ThisLineType).Guid
        Else
            Dirty = True
            Lines(POPIndex).Name = txtName.Text
            SetThisLineProperties(POPIndex)
            RebuildDisplay()
        End If

        ' this could be used when appending FWX lines
        DefaultNoOfLanes = CByte(cbLanes.SelectedItem)
        DefaultTrafficDir = cbDir.SelectedItem

        Init = True
        POPIndex = 0
        Dispose()

    End Sub

    Private Sub SetThisLineProperties(ByVal N)

        If optVector.Checked Then
            SetThisVectorLineProperties(N)
        ElseIf optExtrusion.Checked Then
            SetThisExtrusionLineProperties(N)
        ElseIf optTexture.Checked Then
            SetThisTexturedLineProperties(N)
        ElseIf optObjects.Checked Then
            SetThisLineOfObjectsProperties(N)
        End If

    End Sub

    Private Sub CmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Init = True
        POPIndex = 0
        Dispose()

    End Sub

    Private Sub CmdSample_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSample.Click
        frmLPSample.ShowDialog()
    End Sub

    Private Sub CmdSmooth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSmooth.Click
        FrmLPSmooth.ShowDialog()
    End Sub

    Private Sub CmdColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdColor.Click

        ARGBColor = ckThisColor.BackColor
        If FrmTransparency.ShowDialog = Windows.Forms.DialogResult.OK Then
            ckThisColor.BackColor = ARGBColor
        End If
        ckThisColor.ForeColor = InvertColor(ARGBColor)

    End Sub


    Private Sub CmdDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDetail.Click

        If LineTypes(ThisLineType).TerrainIndex < 0 Then
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

        Key = "[Texture." & Trim(CStr(LineTypes(ThisLineType).TerrainIndex)) & "]"

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

    Private Sub List1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles listVector.MouseDown

        Dim Button As Short = e.Button \ &H100000

        If Button = 2 Then
            Dim LT As Integer
            LT = listVector.SelectedIndex + 1
            My.Computer.Clipboard.SetText(LineTypes(LT).Guid)
        End If

    End Sub

    Private Sub List1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listVector.SelectedIndexChanged

        If Init Then Exit Sub
        Dim LT As Integer

        LT = listVector.SelectedIndex + 1
        ShowThisVectorType(LT)
        CheckIfFWX(LT)
        ThisLineType = LT

    End Sub


    Private Sub CmdWidth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWidth.Click

        Dim N, K As Integer
        Dim X As Double

        On Error GoTo erro1

        X = Val(txtWidth.Text)

        If POPMode = "One" Then
            For K = 1 To Lines(POPIndex).NoOfPoints
                Lines(POPIndex).GLPoints(K).wid = X
            Next
        Else
            For N = 1 To NoOfLines
                If Lines(N).Selected Then
                    For K = 1 To Lines(N).NoOfPoints
                        Lines(N).GLPoints(K).wid = X
                    Next
                End If
            Next
        End If

        RebuildDisplay()

        Exit Sub

erro1:
        MsgBox("Check width value!", MsgBoxStyle.Critical)

    End Sub

    Private Sub CmdAlt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAlt.Click

        Dim N, K As Integer
        Dim X As Double

        On Error GoTo erro1

        X = Val(txtAlt.Text)

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

        Exit Sub

erro1:
        MsgBox("Check altitude value!", MsgBoxStyle.Critical)


    End Sub

    Private Sub CmdWidth12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWidth12.Click

        Dim N, K As Integer
        Dim W1, W21, DW, W As Double

        On Error GoTo erro1

        W1 = Val(txtWidth1.Text)
        W21 = Val(txtWidth2.Text)
        W21 = W21 - W1

        If POPMode = "One" Then
            W = W1
            DW = W21 / (Lines(POPIndex).NoOfPoints - 1)
            For K = 1 To Lines(POPIndex).NoOfPoints
                Lines(POPIndex).GLPoints(K).wid = W
                W = W + DW
            Next
        Else
            For N = 1 To NoOfLines
                If Lines(N).Selected Then
                    W = W1
                    DW = W21 / (Lines(N).NoOfPoints - 1)
                    For K = 1 To Lines(N).NoOfPoints
                        Lines(N).GLPoints(K).wid = W
                        W = W + DW
                    Next
                End If
            Next
        End If

        RebuildDisplay()

        Exit Sub

erro1:
        MsgBox("Check width values!", MsgBoxStyle.Critical)

    End Sub

    Private Sub CmdReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReverse.Click

        Dim N As Integer
        Dim A As String

        A = txtWidth1.Text
        txtWidth1.Text = txtWidth2.Text
        txtWidth2.Text = A

        If POPMode = "One" Then
            ReverseLine(POPIndex)
        Else
            For N = 1 To NoOfLines
                If Lines(N).Selected Then ReverseLine(N)
            Next
        End If

        RebuildDisplay()

    End Sub

    Private Sub ReverseLine(ByVal N As Integer)

        Dim myLine As GLine
        Dim K, NP As Integer

        NP = Lines(N).NoOfPoints
        myLine.NoOfPoints = NP

        ReDim myLine.GLPoints(NP)

        For K = 1 To NP
            'myLine.GLPoints(K).lon = Lines(N).GLPoints(K).lon
            'myLine.GLPoints(K).lat = Lines(N).GLPoints(K).lat
            'myLine.GLPoints(K).alt = Lines(N).GLPoints(K).alt
            myLine.GLPoints(K).wid = Lines(N).GLPoints(K).wid
        Next
        For K = 1 To NP
            'Lines(N).GLPoints(K).lon = myLine.GLPoints(NP + 1 - K).lon
            'Lines(N).GLPoints(K).lat = myLine.GLPoints(NP + 1 - K).lat
            'Lines(N).GLPoints(K).alt = myLine.GLPoints(NP + 1 - K).alt
            Lines(N).GLPoints(K).wid = myLine.GLPoints(NP + 1 - K).wid
        Next

    End Sub

    Private Sub CmdWinding_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWinding.Click

        Dim N As Integer

        If POPMode = "One" Then
            ChangeWinding(POPIndex)
        Else
            For N = 1 To NoOfLines
                If Lines(N).Selected Then ChangeWinding(N)
            Next
        End If

        RebuildDisplay()

    End Sub

    Private Sub ChangeWinding(ByVal N As Integer)

        Dim myLine As GLine
        Dim K, NP As Integer

        NP = Lines(N).NoOfPoints
        myLine.NoOfPoints = NP

        ReDim myLine.GLPoints(NP)

        For K = 1 To NP
            myLine.GLPoints(K).lon = Lines(N).GLPoints(K).lon
            myLine.GLPoints(K).lat = Lines(N).GLPoints(K).lat
            myLine.GLPoints(K).alt = Lines(N).GLPoints(K).alt
            myLine.GLPoints(K).wid = Lines(N).GLPoints(K).wid
        Next

        For K = 1 To NP
            Lines(N).GLPoints(K).lon = myLine.GLPoints(NP + 1 - K).lon
            Lines(N).GLPoints(K).lat = myLine.GLPoints(NP + 1 - K).lat
            Lines(N).GLPoints(K).alt = myLine.GLPoints(NP + 1 - K).alt
            Lines(N).GLPoints(K).wid = myLine.GLPoints(NP + 1 - K).wid
        Next

    End Sub

    Private Sub ListExtrusion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles listExtrusion.SelectedIndexChanged

        If Init Then Exit Sub
        Dim LT As Integer

        LT = listExtrusion.SelectedIndex + 1
        If Not ThisExtrusionType = LT Then
            ThisExtrusionType = LT
            ShowThisExtrusionType(LT)
        End If


    End Sub

    Private Sub ImgExtrusion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles imgExtrusion.Click
        FrmImage.ShowDialog()
    End Sub


    Private Sub OptVector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optVector.CheckedChanged

        If sender.Checked Then
            If Init Then Exit Sub
            EnableTexturedLine(False)
            EnableExtrusionLine(False)
            EnableLineOfObjects(False)
            EnableWidthAndAltitude(True)
            EnableVectorLine(True)
            TabControl1.SelectedIndex = 1
        End If

    End Sub

    Private Sub OptTexture_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTexture.CheckedChanged

        If sender.Checked Then
            If Init Then Exit Sub
            EnableVectorLine(False)
            EnableExtrusionLine(False)
            EnableLineOfObjects(False)
            EnableWidthAndAltitude(True)
            EnableTexturedLine(True)
            TabControl1.SelectedIndex = 2
        End If

    End Sub

    Private Sub OptExtrusion_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optExtrusion.CheckedChanged

        If sender.Checked Then
            If Init Then Exit Sub
            EnableVectorLine(False)
            EnableTexturedLine(False)
            EnableLineOfObjects(False)
            EnableWidthAndAltitude(False)
            boxAltitude.Enabled = True
            EnableExtrusionLine(True)
            TabControl1.SelectedIndex = 3
        End If

    End Sub

    Private Sub OptObjects_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optObjects.CheckedChanged

        If sender.Checked Then
            If Init Then Exit Sub
            EnableVectorLine(False)
            EnableTexturedLine(False)
            EnableExtrusionLine(False)
            EnableLineOfObjects(True)
            EnableWidthAndAltitude(False)
            boxAltitude.Enabled = True
            TabControl1.SelectedIndex = 4

        End If

    End Sub

    Private Sub CmdType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdType.Click
        If optVector.Checked Then TabControl1.SelectedIndex = 1
        If optTexture.Checked Then TabControl1.SelectedIndex = 2
        If optExtrusion.Checked Then TabControl1.SelectedIndex = 3
        If optObjects.Checked Then TabControl1.SelectedIndex = 4
    End Sub

    Private Sub CmdExtrusionProperties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExtrusionProperties.Click
        FrmExtrusions.ShowDialog()
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

                B = "This file already exists in the ../SBuilderX/Texture" & vbCrLf
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

        ShowLineTex(Tex)

    End Sub

    Private Sub ShowLineTex(ByVal Tex As String)

        If Tex = "" Then Exit Sub

        Try

            ChDrive(My.Application.Info.DirectoryPath)
            ChDir(My.Application.Info.DirectoryPath & "\Tools\")

            Dim ImageTool As String
            'Dim BmpPath As String = AppPath & "\Tools\Work\temp.bmp"
            Dim BmpPath As String = AppPath & "\Tools\Work\" & Tex

            txtTexName.Text = Tex
            Dim TexPath As String = AppPath & "\Texture\" & Tex

            If My.Computer.FileSystem.FileExists(TexPath) Then
                'ImageTool = "imagetool -nogui -nomip -32 Work\temp.bmp"
                ImageTool = "imagetool -nogui -nomip -32 Work\" & Tex
                File.Copy(TexPath, BmpPath, True)
                ExecCmd(ImageTool)
            Else
                FileCopy(AppPath & "\Tools\BMPs\none.jpg", BmpPath)
            End If

            Dim bmp As Image = System.Drawing.Image.FromFile(BmpPath)

            Dim width As Integer = bmp.Width
            Dim height As Integer = imgTex.Height
            Dim thumb As New Bitmap(width, height)
            Dim g As Graphics = Graphics.FromImage(thumb)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.DrawImage(bmp, New Rectangle(0, 0, width, height), New Rectangle(0, 0, bmp.Width,
                            bmp.Height), GraphicsUnit.Pixel)
            g.Dispose()
            bmp.Dispose()
            imgTex.BackgroundImage = thumb

        Catch ex As Exception
            MsgBox("There is a problem with the display of this image!", MsgBoxStyle.Critical)
        End Try

    End Sub



    Private Sub TabPage2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage2.Click
        optVector.Checked = True
    End Sub

    Private Sub TabPage3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage3.Click
        If POPMode = "SHP" Or POPMode = "SUR" Or POPMode = "Many" Then Exit Sub
        optTexture.Checked = True
    End Sub

    Private Sub TabPage4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage4.Click
        If POPMode = "SHP" Or POPMode = "SUR" Or POPMode = "Many" Then Exit Sub
        optExtrusion.Checked = True
    End Sub

    Private Sub TabPage5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage5.Click
        If POPMode = "SHP" Or POPMode = "SUR" Or POPMode = "Many" Then Exit Sub
        optObjects.Checked = True
    End Sub

    Private Sub OptTile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTile.CheckedChanged

        If optTile.Checked Or optFull.Checked Then
            imgTex.BackgroundImageLayout = ImageLayout.Tile
        Else
            imgTex.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub

    Private Sub OptStretch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optStretch.CheckedChanged

        If optTile.Checked Or optFull.Checked Then
            imgTex.BackgroundImageLayout = ImageLayout.Tile
        Else
            imgTex.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub

    Private Sub OptLying_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optLying.CheckedChanged

        If optLying.Checked Then
            lbTexPri.Enabled = True
            txtTexPri.Enabled = True
            LbV1.Enabled = True
            txtV1.Enabled = True
        Else
            lbTexPri.Enabled = False
            txtTexPri.Enabled = False
            LbV1.Enabled = False
            txtV1.Enabled = False
        End If

    End Sub

    Private Sub LstLib_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstLib.SelectedIndexChanged

        If Init Then Exit Sub

        Dim N, K As Integer
        Dim a, LibCat As String

        ' get the category index
        K = cmbLibCat.SelectedIndex + 1
        LibCat = LibCategories(K).Name

        N = lstLib.SelectedIndex

        Dim myLibObj As LibObject = LibCategories(K).Objs(N)

        labelLibID.Text = myLibObj.ID
        ObjWidth = myLibObj.Width
        ObjLength = myLibObj.Length

        a = LibObjectsPath & "\" & LibCat & "\" & labelLibID.Text & ".jpg"
        ImageFileNameTrue = a
        If Not File.Exists(a) Then
            a = My.Application.Info.DirectoryPath & "\tools\bmps\none.jpg"
        End If

        Dim fs As New System.IO.FileStream(a, IO.FileMode.Open, IO.FileAccess.Read)
        imgLib.Image = Image.FromStream(fs)
        fs.Close()

        ImageFileName = a

    End Sub

    Private Sub CmbLibCat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLibCat.SelectedIndexChanged

        If Init Then Exit Sub

        Dim K As Integer
        Dim a, LibCat As String

        'first remove all listings
        lstLib.Items.Clear()

        ' get the category index
        K = cmbLibCat.SelectedIndex + 1

        Dim g
        For Each g In LibCategories(K).Objs
            lstLib.Items.Add(g.Name)
        Next

        If LibCategories(K).Objs.Count = 0 Then
            labelLibID.Text = ""
            a = My.Application.Info.DirectoryPath & "\tools\bmps\none.jpg"
            imgLib.Image = System.Drawing.Image.FromFile(a)
            ImageFileName = a
            Exit Sub
        End If


        LibCat = LibCategories(K).Name
        Dim myLibObj As LibObject = LibCategories(K).Objs(0)
        lstLib.SelectedIndex = 0
        labelLibID.Text = myLibObj.ID
        ObjWidth = myLibObj.Width
        ObjLength = myLibObj.Length

        a = LibObjectsPath & "\" & LibCat & "\" & labelLibID.Text & ".jpg"
        ImageFileNameTrue = a
        If Not File.Exists(a) Then
            a = My.Application.Info.DirectoryPath & "\tools\bmps\none.jpg"
        End If

        Dim fs As New System.IO.FileStream(a, IO.FileMode.Open, IO.FileAccess.Read)
        imgLib.Image = Image.FromStream(fs)
        fs.Close()

        ImageFileName = a

    End Sub


    Private Sub CkRandom_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckRandom.CheckedChanged

        If ckRandom.CheckState = CheckState.Checked Then
            txtHeading.Enabled = False
        Else
            txtHeading.Enabled = True
        End If

    End Sub

    Private Sub CmdHeading_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHeading.Click

        Dim W As Double = Val(txtHeading.Text)
        Dim K As Integer

        If ckRandom.Checked Then Randomize()

        For K = 1 To Lines(POPIndex).NoOfPoints
            If ckRandom.Checked = False Then
                Lines(POPIndex).GLPoints(K).wid = W
            Else
                Lines(POPIndex).GLPoints(K).wid = 360 * Rnd()
            End If
        Next

        RebuildDisplay()

    End Sub

End Class

