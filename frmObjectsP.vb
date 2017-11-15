Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports FSUIPC
Friend Class FrmObjectsP



    'Dim LatAircraft As Offset(Of Long) = New FSUIPC.Offset(Of Long)(&H560)
    'Dim LonAircraft As Offset(Of Long) = New FSUIPC.Offset(Of Long)(&H568)

    'Dim AltAircraft As Offset(Of Long) = New FSUIPC.Offset(Of Long)(&H568)
    'Dim PitchAircraft As Offset(Of Long) = New FSUIPC.Offset(Of Long)(&H568)
    'Dim BankAircraft As Offset(Of Long) = New FSUIPC.Offset(Of Long)(&H568)
    'Dim HeadingAircraft As Offset(Of Long) = New FSUIPC.Offset(Of Long)(&H568)

    Private Mode As String ' One or Many or RGN
    Private IsReady As Boolean 'to sign that combox can start!
    Private IsInit As Boolean = True ' to sign that opt controls should do nothing
    'Private ListObj1 As Integer
    'Private ListObj2 As Integer

    Private Sub CmbLibCat_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmbLibCat.SelectedIndexChanged

        Dim K As Integer
        Dim a, LibCat As String

        If Not IsReady Then Exit Sub

        'first remove all listings
        lstLib.Items.Clear()

        ' get the category index
        K = cmbLibCat.SelectedIndex + 1

        Dim g
        For Each g In LibCategories(K).Objs
            lstLib.Items.Add(g.Name)
        Next

        If LibCategories(K).Objs.Count = 0 Then
            txtLibWidth.Text = ""
            txtLibLength.Text = ""
            txtLibScale.Text = ""
            txtLibName.Text = ""
            cmdUpDefault.Enabled = False
            txtLibID.Text = ""
            txtComment.Text = ""
            a = My.Application.Info.DirectoryPath & "\tools\bmps\na.jpg"
            imgLib.Image = System.Drawing.Image.FromFile(a)
            ImageFileName = a
            Exit Sub
        End If


        LibCat = LibCategories(K).Name
        Dim myLibObj As LibObject = LibCategories(K).Objs(0)
        lstLib.SelectedIndex = 0
        txtLibID.Text = myLibObj.ID
        txtLibWidth.Text = CStr(myLibObj.Width)
        txtLibLength.Text = CStr(myLibObj.Length)
        txtLibScale.Text = CStr(myLibObj.Scaling)
        txtLibName.Text = myLibObj.Name
        cmdUpDefault.Enabled = False

        ' after 205
        txtComment.Text = myLibObj.Name

        ObjLibType = CInt(myLibObj.Type)
        If ObjLibType = 0 Then
            labelFS.Text = "Old FS8 Library Object"
        ElseIf ObjLibType = 1 Then
            labelFS.Text = "Old FS9 Library Object"
        ElseIf ObjLibType = 2 Then
            labelFS.Text = "New FSX Library Object"
        End If

        cmdUpDefault.Enabled = False

        a = LibObjectsPath & "\" & LibCat & "\" & txtLibID.Text & ".jpg"
        ImageFileNameTrue = a
        If Not File.Exists(a) Then
            a = My.Application.Info.DirectoryPath & "\tools\bmps\na.jpg"
        End If

        Dim fs As New System.IO.FileStream(a, IO.FileMode.Open, IO.FileAccess.Read)
        'imgLib.Image = System.Drawing.Image.FromFile(a)
        imgLib.Image = Image.FromStream(fs)
        fs.Close()

        ImageFileName = a

    End Sub


    Private Sub CmbMacroCat_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmbMacroCat.SelectedIndexChanged

        Dim N, K As Integer

        If Not IsReady Then Exit Sub

        ' first remove all listings
        N = lstMacro.Items.Count
        For K = N To 1 Step -1
            lstMacro.Items.RemoveAt(K - 1)
        Next K

        ' get the category index
        K = cmbMacroCat.SelectedIndex + 1

        For N = 1 To MacroCategories(K).NOB
            lstMacro.Items.Add(MacroCategories(K).MacroObjects(N).Name)
        Next N

        lstMacro.SelectedIndex = 0

        ShowMacro(K, 1)
        MacroID = MacroCategories(K).MacroObjects(1).File

        IsReady = True

    End Sub

    Private Sub CmbRwy12Cat_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmbRwy12Cat.SelectedIndexChanged

        Dim N, K As Integer
        Dim a As String

        If Not IsReady Then Exit Sub

        ' first remove all listings
        N = lstRwy12.Items.Count
        For K = N To 1 Step -1
            lstRwy12.Items.RemoveAt(K - 1)
        Next K

        ' get the category index
        K = cmbRwy12Cat.SelectedIndex + 1

        For N = 1 To Rwy12Categories(K).NOB
            lstRwy12.Items.Add(Rwy12Categories(K).Rwy12Objects(N).Name)
        Next N

        lstRwy12.SelectedIndex = 0
        txtRwy12ID.Text = Rwy12Categories(K).Rwy12Objects(1).ID

        txtRwy12Width.Text = CStr(100)
        txtRwy12Length.Text = CStr(100)
        txtRwy12Scale.Text = CStr(1)

        a = Rwy12Path & "\img\" & Rwy12Categories(K).Rwy12Objects(1).Texture
        ImageFileNameTrue = a
        If Not File.Exists(a) Then
            a = My.Application.Info.DirectoryPath & "\tools\bmps\na.jpg"
        End If

        Dim fs As New System.IO.FileStream(a, IO.FileMode.Open, IO.FileAccess.Read)
        'imgRwy12.Image = System.Drawing.Image.FromFile(a)
        imgRwy12.Image = Image.FromStream(fs)
        fs.Close()

        ImageFileName = a

    End Sub

    Private Sub CmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click

        Dispose()

    End Sub

    Private Sub CmdExpand_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExpand.Click

        FrmExtraMacro.ShowDialog()

    End Sub

    Private Sub CmdMacroEdit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdMacroEdit.Click

        Dim N, K As Integer
        Dim a, b As String

        'On Error Resume Next

        N = lstMacro.SelectedIndex + 1
        K = cmbMacroCat.SelectedIndex + 1
        b = MacroCategories(K).MacroObjects(N).File
        a = VB.Right(b, 3)

        If a = "API" Then a = MacroAPIPath & "\"
        If a = "SCM" Then a = MacroASDPath & "\"

        ChDir(a)

        b = a & b

        a = "notepad.exe " & b
        N = ExecCmd(a)

    End Sub


    Private Sub SetObjectProperties(ByVal N As Integer)

        Dim Macro As String

        On Error GoTo erro
        ' insert here checking of values

        Objects(N).Width = 10
        Objects(N).Length = 10
        Objects(N).Heading = CSng(txtHeading.Text)
        Objects(N).Pitch = CSng(txtPitch.Text)
        Objects(N).Bank = CSng(txtBank.Text)
        Objects(N).BiasX = CSng(txtBiasX.Text)
        Objects(N).BiasY = CSng(txtBiasY.Text)
        Objects(N).BiasZ = CSng(txtBiasZ.Text)
        Objects(N).Altitude = CSng(txtAltitude.Text)
        Objects(N).AGL = ckAGL.CheckState
        If opVSparse.Checked Then Objects(N).Complexity = 0
        If opSparse.Checked Then Objects(N).Complexity = 1
        If opNormal.Checked Then Objects(N).Complexity = 2
        If opDense.Checked Then Objects(N).Complexity = 3
        If opVDense.Checked Then Objects(N).Complexity = 4
        If opEDense.Checked Then Objects(N).Complexity = 5

        If optLib.Checked Then
            If ObjLibType = 0 Then
                Objects(N).Type = 1
            ElseIf ObjLibType = 1 Then
                Objects(N).Type = 2
            ElseIf ObjLibType = 2 Then
                Objects(N).Type = 0
            End If

            ObjLibID = txtLibID.Text
            ObjLibScale = CSng(txtLibScale.Text)
            ObjLibV1 = CSng(txtV1.Text)
            ObjLibV2 = CSng(txtV2.Text)
            Objects(N).Width = CSng(txtLibWidth.Text)
            Objects(N).Length = CSng(txtLibLength.Text)
        End If

        If optRwy12.Checked Then
            Objects(N).Type = 3
            ObjLibID = txtRwy12ID.Text
            ObjLibScale = CSng(txtRwy12Scale.Text)
            Objects(N).Width = CSng(txtRwy12Width.Text)
            Objects(N).Length = CSng(txtRwy12Length.Text)
        End If

        Macro = ""
        If optMacro.Checked Then
            If VB.Right(MacroID, 3) = "API" Then Macro = "API"
            If VB.Right(MacroID, 3) = "SCM" Then Macro = "ASD"
        End If

        If Macro = "API" Then
            Objects(N).Type = 4
            Objects(N).Width = CSng(txtMacroWidth.Text)
            Objects(N).Length = CSng(txtMacroLength.Text)
            MacroScale = CSng(txtMacroScale.Text)
            MacroRange = CInt(txtMacroRange.Text)
            MacroVisibility = CSng(txtV1.Text)
            MacroV2Value = CSng(txtV2.Text)
            MacroP6Value = txtP6.Text
            MacroP7Value = txtP7.Text
            MacroP8Value = txtP8.Text
            MacroP9Value = txtP9.Text
        End If

        If Macro = "ASD" Then
            Objects(N).Type = 5
            Objects(N).Width = CSng(txtMacroWidth.Text)
            Objects(N).Length = CSng(txtMacroLength.Text)
            MacroScale = CSng(txtMacroScale.Text)
            MacroRange = CInt(txtMacroRange.Text)
            MacroVisibility = CSng(txtV1.Text)
            MacroP6Value = txtP6.Text
            MacroP7Value = txtP7.Text
            MacroP8Value = txtP8.Text
            If lbP9.Visible Then MacroP9Value = txtP9.Text

        End If

        If optTaxiwaySign.Checked Then
            Objects(N).Type = 8
            ObjTaxSize = CInt(combTaxiwaySize.Text)
            ObjTaxJust = combTaxiwayJustification.Text
            ObjTaxLabel = txtTaxiwayText.Text
        End If

        If optEffect.Checked Then
            Objects(N).Type = 16
            ObjEffName = txtEffectName.Text
            ObjEffParameters = txtEffectParameters.Text
        End If

        If optBeacon.Checked Then
            Objects(N).Type = 32
            ObjBeaCivil = 0
            ObjBeaMil = 0
            ObjBeaAirport = 0
            ObjBeaHeliport = 0
            ObjBeaSeaBase = 0
            If optBeaconCivilian.Checked Then ObjBeaCivil = 1
            If optBeaconMil.Checked Then ObjBeaMil = 1
            If optBeaconAirport.Checked Then ObjBeaAirport = 1
            If optBeaconSeaBase.Checked Then ObjBeaSeaBase = 1
            If optBeaconHeliport.Checked Then ObjBeaHeliport = 1
        End If

        If optWindsock.Checked Then
            Objects(N).Type = 64
            ObjWinLight = 0
            If ckWindsockLight.CheckState = 1 Then ObjWinLight = 1
            ObjWinLength = CSng(txtWindsockLength.Text)
            ObjWinHeight = CSng(txtWindsockHeight.Text)
            ObjWindPoleColor = CInt(System.Drawing.ColorTranslator.ToOle(txtWindsockHeight.BackColor))
            ObjWindSockColor = CInt(System.Drawing.ColorTranslator.ToOle(txtWindsockLength.BackColor))
        End If

        If optMDL.Checked Then
            Objects(N).Width = CSng(txtMDLWidth.Text)
            Objects(N).Length = CSng(txtMDLLength.Text)
            ObjMDLFile = txtMDLFile.Text
            ObjMDLGuid = labelMDLguid.Text
            ObjMDLScale = Val(txtMDLscale.Text)
            ObjMDLName = labelMDLName.Text
            If Mid(ObjMDLGuid, 1, 1) = "{" Then
                Objects(N).Type = 128  ' FSX type
            Else
                Objects(N).Type = 129  ' FS9 type
            End If

        End If

        If optGenB.Checked Then

            Objects(N).Width = CSng(nUPsizeX.Value)
            Objects(N).Length = CSng(nUPsizeZ.Value)

            If optGbFlat.Checked Then Objects(N).Type = 256
            If optGbPeaked.Checked Then Objects(N).Type = 257
            If optGbRidge.Checked Then Objects(N).Type = 258
            If optGbSlant.Checked Then Objects(N).Type = 259
            If optGbPyramidal.Checked Then Objects(N).Type = 260
            If optGbMultiSided.Checked Then Objects(N).Type = 261

            scale_gb = nUPscale.Value

        End If

        Exit Sub
erro:
        MsgBox("Could not set object properties!", MsgBoxStyle.Exclamation)

    End Sub

    Private Sub CmdOK_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click

        If optLib.Checked Then
            If lstLib.SelectedIndex = -1 Then
                Close()   ' unknow so do nothing
                Exit Sub
            End If
        End If

        Dim N As Integer
        Dirty = True
        If Mode = "One" Then
            N = POPIndex
            If N > NoOfObjects Then
                ReDim Preserve Objects(N)
                NoOfObjects = NoOfObjects + 1
                ' added October 2017
                BackUp()
            End If
            SetObjectProperties(N)
            'IniObjType = Objects(N).Type
            Objects(N).lat = Str2Lat(txtLat.Text)
            Objects(N).lon = Str2Lon(txtLon.Text)
            ObjComment = txtComment.Text
            ObjComment = Replace(ObjComment, " ", "_")
            Objects(N).Description = MakeDescription(N)
            AddLatLonToObjects(N)
            'ElseIf Mode = "RGN" Then 
            '	SetRGNProperties()
            '	RGNPointType1 = MakeRGNPointType1
            '	RGNPointType2 = MakeRGNPointType2
        ElseIf Mode = "Many" Then
            For N = 1 To NoOfObjects
                If Objects(N).Selected Then
                    'ObjComment = GetObjectName(N)
                    ObjComment = txtComment.Text
                    SetObjectProperties(N)
                    Objects(N).Description = MakeDescription(N)
                    AddLatLonToObjects(N)
                End If
            Next N
        End If

        RebuildDisplay()

        ' should I realy comment the following? October 2017
        'Close()
        Dispose()

    End Sub


    Private Sub CmdOption_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOption.Click

        If optLib.Checked Then SSTab1.SelectedIndex = 1
        If optGenB.Checked Then SSTab1.SelectedIndex = 2
        If optMacro.Checked Then SSTab1.SelectedIndex = 3
        If optRwy12.Checked Then SSTab1.SelectedIndex = 4
        If optBeacon.Checked Then SSTab1.SelectedIndex = 5
        If optTaxiwaySign.Checked Then SSTab1.SelectedIndex = 5
        If optEffect.Checked Then SSTab1.SelectedIndex = 5
        If optWindsock.Checked Then SSTab1.SelectedIndex = 5
        If optMDL.Checked Then SSTab1.SelectedIndex = 5

    End Sub
    Private Sub CmdOrder_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOrder.Click

        If NoOfObjects < 2 Then
            MsgBox("At least 2 objects must exist!", MsgBoxStyle.Information)
            Exit Sub
        End If

        ObjOrder = False

        FrmOrder.ShowDialog()

        If ObjOrder Then Dispose()

    End Sub

    Private Sub CmdPosFs_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPosFs.Click

        Dim Bank, Altitude, Pitch, Heading As Single
        Dim Lat, Lon As Double

        Try
            If AircraftVIEW Then   ' this on May 25 2009
                FSUIPCConnection.Close()
                frmStart.UpdateAircraft(0)
                frmStart.ShowAircraftMenuItem.Checked = False
                AircraftVIEW = False
            End If
            FSUIPCConnection.Open()
            FSUIPCConnection.Process()
            Lat = CDbl(frmStart.LatAircraft.Value)
            Lat = Lat * 90.0 / (10001750.0 * 65536.0 * 65536.0)
            Lon = CDbl(frmStart.LonAircraft.Value)
            Lon = Lon * 360.0 / (65536.0 * 65536.0 * 65536.0 * 65536.0)
            Altitude = CSng(frmStart.Alt1Aircraft.Value)
            Altitude = Altitude + CSng(frmStart.Alt2Aircraft.Value) / (65536.0! * 65536.0!)
            Altitude = Altitude - AircraftAltitudeOffset
            Bank = CSng(frmStart.BankAircraft.Value) * 360.0! / (65536.0! * 65536.0!)
            Pitch = CSng(frmStart.PitchAircraft.Value) * 360.0! / (65536.0! * 65536.0!)
            Heading = CSng(frmStart.HeadingAircraft.Value) * 360.0! / (65536.0! * 65536.0!)
            If Heading < 0 Then Heading = Heading + 360
            FSUIPCConnection.Close()
        Catch ex As Exception
            FSUIPCConnection.Close()
            MsgBox("Error communicating with FSUIPC!", MsgBoxStyle.Information)
            Exit Sub
        End Try

        txtLon.Text = Lon2Str(Lon)
        txtLat.Text = Lat2Str(Lat)
        txtAltitude.Text = Format(Altitude, "0.000")
        txtBank.Text = Format(Bank, "0.000")
        txtPitch.Text = Format(Pitch, "0.000")
        txtHeading.Text = Format(Heading, "0.000")

    End Sub

    Private Sub CmdTaxiwayHelp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdTaxiwayHelp.Click

        FrmTaxSign.ShowDialog()

    End Sub


    Public Sub ShowForAllSelected()

        Mode = "Many"
        lbComment.Enabled = False
        txtComment.Enabled = False
        cmdOrder.Enabled = False
        txtLat.Enabled = False
        txtLon.Enabled = False
        Label1.Enabled = False
        Label2.Enabled = False
        cmdPosFs.Enabled = False

        ' optLib_CheckedChanged(optLib, New System.EventArgs())

        If Not LibObjectsIsOn Then optLib.Enabled = False
        If Not Rwy12IsOn Then optRwy12.Enabled = False
        If MacroAPIIsOn = False And MacroASDIsOn = False Then optMacro.Enabled = False

        If LibObjectsIsOn Then FillLibList()
        If Rwy12IsOn Then FillRwy12List()
        If MacroAPIIsOn Then FillMacroList()


    End Sub


    Public Sub ShowObjectProperties(ByVal N As Integer)

        Dim K As Integer
        Dim a As String

        IsReady = False

        Mode = "One"
        SSTab1.SelectedIndex = 0
        'cmdOrder.Enabled = True

        If N > NoOfObjects Then
            cmdOrder.Enabled = False
        End If

        If Not LibObjectsIsOn Then optLib.Enabled = False
        If Not Rwy12IsOn Then optRwy12.Enabled = False
        If MacroAPIIsOn = False And MacroASDIsOn = False Then optMacro.Enabled = False

        DisableAllTypes()

        If N > NoOfObjects Then   ' is a new object place with mouse
            If ObjMYes Then
                txtHeading.Text = CStr(ObjMHead)
                ObjMYes = False
            End If
            txtLat.Text = Lat2Str(LatObj)
            txtLon.Text = Lon2Str(LonObj)

            If LibObjectsIsOn Then
                optLib.Checked = True
                SetEnableLib(True)
                FillLibList()
                SSTab1.SelectedIndex = 1
            Else
                optLib.Checked = False
                SSTab1.SelectedIndex = 0
            End If
            'ShowIniObject()


        Else  ' it is an existing object >> show the properties 

            optLib.Checked = False
            optMacro.Checked = False
            optRwy12.Checked = False
            optTaxiwaySign.Checked = False
            optEffect.Checked = False
            optBeacon.Checked = False
            optWindsock.Checked = False
            optMDL.Checked = False
            optGenB.Checked = False


            If Objects(N).Type = 0 Then
                optLib.Checked = True
                SetEnableLib(True)
                a = Objects(N).Description
                K = InStr(1, a, "|")
                ObjLibID = Mid(a, 1, K - 1)
                FillLibList()
                ObjLibType = 2
                AnalyseLibObject(N)
                txtLibID.Text = ObjLibID
                txtLibScale.Text = CStr(ObjLibScale)
                txtLibWidth.Text = CStr(Objects(N).Width)
                txtLibLength.Text = CStr(Objects(N).Length)
                'cmdUpDefault.Enabled = True
                txtV1.Text = "0"
                txtV2.Text = "0"
                SSTab1.SelectedIndex = 1
            End If

            If Objects(N).Type = 1 Then
                optLib.Checked = True
                SetEnableLib(True)
                a = Objects(N).Description
                K = InStr(1, a, "|")
                ObjLibID = Mid(a, 1, K - 1)
                FillLibList()
                ObjLibType = 0
                AnalyseLibObject(N)
                txtLibID.Text = ObjLibID
                txtLibScale.Text = CStr(ObjLibScale)
                txtLibWidth.Text = CStr(Objects(N).Width)
                txtLibLength.Text = CStr(Objects(N).Length)
                'cmdUpDefault.Enabled = True
                txtV1.Text = CStr(ObjLibV1)
                txtV2.Text = CStr(ObjLibV2)
                SSTab1.SelectedIndex = 1
            End If

            If Objects(N).Type = 2 Then
                optLib.Checked = True
                SetEnableLib(True)
                a = Objects(N).Description
                K = InStr(1, a, "|")
                ObjLibID = Mid(a, 1, K - 1)
                FillLibList()
                ObjLibType = 1
                AnalyseLibObject(N)
                txtLibID.Text = ObjLibID
                txtLibScale.Text = CStr(ObjLibScale)
                txtLibWidth.Text = CStr(Objects(N).Width)
                txtLibLength.Text = CStr(Objects(N).Length)
                'cmdUpDefault.Enabled = True
                txtV1.Text = "0"
                txtV2.Text = "0"
                SSTab1.SelectedIndex = 1
            End If

            If Objects(N).Type = 3 Then
                If Rwy12IsOn Then
                    optRwy12.Checked = True
                    SetEnableRwy12(True)
                    a = Objects(N).Description
                    K = InStr(1, a, "|")
                    ObjLibID = Mid(a, 1, K - 1)
                    FillRwy12List()
                    ObjLibType = 1
                    AnalyseLibObject(N)
                    txtRwy12ID.Text = ObjLibID
                    txtRwy12Scale.Text = CStr(ObjLibScale)
                    txtRwy12Width.Text = CStr(Objects(N).Width)
                    txtRwy12Length.Text = CStr(Objects(N).Length)
                    SSTab1.SelectedIndex = 4
                Else
                    MsgBox("You can not edit this object as a Rwy12 object!", MsgBoxStyle.Exclamation)
                    optLib.Checked = True
                    SetEnableLib(True)
                    a = Objects(N).Description
                    K = InStr(1, a, "|")
                    ObjLibID = Mid(a, 1, K - 1)
                    FillLibList()
                    ObjLibType = 1
                    AnalyseLibObject(N)
                    txtLibID.Text = ObjLibID
                    txtLibScale.Text = CStr(ObjLibScale)
                    txtLibWidth.Text = CStr(Objects(N).Width)
                    txtLibLength.Text = CStr(Objects(N).Length)
                    'cmdUpDefault.Enabled = True
                    SSTab1.SelectedIndex = 1
                End If
            End If

            If Objects(N).Type = 4 Then
                If MacroAPIIsOn Then
                    optMacro.Checked = True
                    SetEnableMacro(True)
                    a = Objects(N).Description
                    K = InStr(1, a, "|")
                    MacroID = Mid(a, 1, K - 1)
                    FillMacroList() ' uses MacroID
                    AnalyseAPIMacro(N)
                    txtMacroScale.Text = CStr(MacroScale)
                    txtMacroRange.Text = CStr(MacroRange)
                    txtMacroWidth.Text = CStr(Objects(N).Width)
                    txtMacroLength.Text = CStr(Objects(N).Length)
                    txtV1.Text = CStr(MacroVisibility)
                    txtV2.Text = CStr(MacroV2Value)
                    txtP6.Text = MacroP6Value
                    txtP7.Text = MacroP7Value
                    txtP8.Text = MacroP8Value
                    txtP9.Text = MacroP9Value
                    SSTab1.SelectedIndex = 3
                Else
                    MsgBox("You can not edit this object as an API macro!", MsgBoxStyle.Exclamation)
                End If
            End If

            If Objects(N).Type = 5 Then
                If MacroASDIsOn Then
                    optMacro.Checked = True
                    SetEnableMacro(True)
                    a = Objects(N).Description
                    K = InStr(1, a, "|")
                    MacroID = Mid(a, 1, K - 1)
                    FillMacroList() ' uses MacroID
                    AnalyseASDMacro(N)
                    txtMacroScale.Text = CStr(MacroScale)
                    txtMacroRange.Text = CStr(MacroRange)
                    txtMacroWidth.Text = CStr(Objects(N).Width)
                    txtMacroLength.Text = CStr(Objects(N).Length)
                    txtV1.Text = CStr(MacroVisibility)
                    txtP6.Text = MacroP6Value
                    txtP7.Text = MacroP7Value
                    txtP8.Text = MacroP8Value
                    txtP9.Text = MacroP9Value
                    SSTab1.SelectedIndex = 3
                Else
                    MsgBox("You can not edit this object as an ASD macro!", MsgBoxStyle.Exclamation)
                End If

            End If

            If Objects(N).Type = 8 Then
                optTaxiwaySign.Checked = True
                SetEnableTaxiway(True)
                AnalyseTaxiwayObject(N)
                txtTaxiwayText.Text = ObjTaxLabel
                combTaxiwaySize.Text = CStr(ObjTaxSize)
                combTaxiwayJustification.Text = ObjTaxJust
                SSTab1.SelectedIndex = 5
            End If

            If Objects(N).Type = 16 Then
                optEffect.Checked = True
                SetEnableEffect(True)
                AnalyseEffectObject(N)
                txtEffectName.Text = ObjEffName
                txtEffectParameters.Text = ObjEffParameters
                SSTab1.SelectedIndex = 5
            End If

            If Objects(N).Type = 32 Then
                optBeacon.Checked = True
                SetEnableBeacon(True)
                AnalyseBeaconObject(N)

                optBeaconCivilian.Checked = False
                optBeaconMil.Checked = False
                optBeaconAirport.Checked = False
                optBeaconSeaBase.Checked = False
                optBeaconHeliport.Checked = False

                If ObjBeaCivil = 1 Then optBeaconCivilian.Checked = True
                If ObjBeaMil = 1 Then optBeaconMil.Checked = True
                If ObjBeaAirport = 1 Then optBeaconAirport.Checked = True
                If ObjBeaSeaBase = 1 Then optBeaconSeaBase.Checked = True
                If ObjBeaHeliport = 1 Then optBeaconHeliport.Checked = True

                SSTab1.SelectedIndex = 5

            End If

            If Objects(N).Type = 64 Then
                optWindsock.Checked = True
                SetEnableWindSock(True)
                AnalyseWindsockObject(N)

                ckWindsockLight.CheckState = ObjWinLight
                txtWindsockHeight.Text = CStr(ObjWinHeight)
                txtWindsockLength.Text = CStr(ObjWinLength)

                txtWindsockHeight.BackColor = System.Drawing.ColorTranslator.FromOle(ObjWindPoleColor)
                txtWindsockLength.BackColor = System.Drawing.ColorTranslator.FromOle(ObjWindSockColor)

                txtWindsockHeight.ForeColor = InvertColor(txtWindsockHeight.BackColor)
                txtWindsockLength.ForeColor = InvertColor(txtWindsockLength.BackColor)
                SSTab1.SelectedIndex = 5

            End If

            If Objects(N).Type = 128 Or Objects(N).Type = 129 Then
                optMDL.Checked = True
                SetEnableMDL(True)
                AnalyseMDLObject(N)
                txtMDLWidth.Text = CStr(Objects(N).Width)
                txtMDLLength.Text = CStr(Objects(N).Length)
                txtMDLFile.Text = ObjMDLFile
                labelMDLName.Text = ObjMDLName
                labelMDLguid.Text = ObjMDLGuid
                txtMDLscale.Text = CStr(ObjMDLScale)
                SSTab1.SelectedIndex = 5
            End If


            If Objects(N).Type > 255 Then
                optGenB.Checked = True
                If Objects(N).Type = 256 Then optGbFlat.Checked = True
                If Objects(N).Type = 257 Then optGbPeaked.Checked = True
                If Objects(N).Type = 258 Then optGbRidge.Checked = True
                If Objects(N).Type = 259 Then optGbSlant.Checked = True
                If Objects(N).Type = 260 Then optGbPyramidal.Checked = True
                If Objects(N).Type = 261 Then optGbMultiSided.Checked = True
                SetEnableGenB(True)
                FillGenBList()
                AnalyseGenBObject(N)
                sizeX = Objects(N).Width
                sizeZ = Objects(N).Length
                nUPsizeX.Value = sizeX
                nUPsizeZ.Value = sizeZ
                nUPscale.Value = scale_gb
                SSTab1.SelectedIndex = 2
            End If

            txtLat.Text = Lat2Str(Objects(N).lat)
            txtLon.Text = Lon2Str(Objects(N).lon)
            txtHeading.Text = CStr(Objects(N).Heading)
            txtPitch.Text = CStr(Objects(N).Pitch)
            txtBank.Text = CStr(Objects(N).Bank)
            txtBiasX.Text = CStr(Objects(N).BiasX)
            txtBiasY.Text = CStr(Objects(N).BiasY)
            txtBiasZ.Text = CStr(Objects(N).BiasZ)
            txtAltitude.Text = CStr(Objects(N).Altitude)
            If Objects(N).Complexity = 0 Then opVSparse.Checked = True
            If Objects(N).Complexity = 1 Then opSparse.Checked = True
            If Objects(N).Complexity = 2 Then opNormal.Checked = True
            If Objects(N).Complexity = 3 Then opDense.Checked = True
            If Objects(N).Complexity = 4 Then opVDense.Checked = True
            If Objects(N).Complexity = 5 Then opEDense.Checked = True
            ckAGL.CheckState = Objects(N).AGL

            txtComment.Text = ObjComment

        End If
        IsInit = False

        IsReady = True

    End Sub
    Private Sub DisableAllTypes()

        SetEnableBeacon(False)
        SetEnableTaxiway(False)
        SetEnableEffect(False)
        SetEnableLib(False)
        SetEnableWindSock(False)
        SetEnableMacro(False)
        SetEnableRwy12(False)
        SetEnableMDL(False)
        SetEnableGenB(False)

    End Sub

    Private Sub SetEnableRwy12(ByVal Flag As Boolean)

        'frRwy12.Enabled = Flag

        txtRwy12Width.Enabled = Flag
        txtRwy12Length.Enabled = Flag
        txtRwy12Scale.Enabled = Flag
        LabelRwy12Length.Enabled = Flag
        LabelRwy12Width.Enabled = Flag
        LabelRwy12Scale.Enabled = Flag
        labelRwy12Mouse.Enabled = Flag

        LabelRwy121.Enabled = Flag
        LabelRwy12Cat.Enabled = Flag
        cmbRwy12Cat.Enabled = Flag

        imgRwy12.Visible = Flag
        lstRwy12.Enabled = Flag
        txtRwy12ID.Enabled = Flag

    End Sub

    Private Sub SetEnableMacro(ByVal Flag As Boolean)

        'frMacro.Enabled = Flag
        'frMacro1.Enabled = Flag

        lbP6.Enabled = Flag
        lbP7.Enabled = Flag
        lbP8.Enabled = Flag
        lbP9.Enabled = Flag

        txtP6.Enabled = Flag
        txtP7.Enabled = Flag
        txtP8.Enabled = Flag
        txtP9.Enabled = Flag

        txtMacroWidth.Enabled = Flag
        txtMacroLength.Enabled = Flag
        txtMacroRange.Enabled = Flag
        txtMacroScale.Enabled = Flag
        LabelMacroLength.Enabled = Flag
        LabelMacroWidth.Enabled = Flag
        LabelMacroScale.Enabled = Flag
        LabelMacroRange.Enabled = Flag
        LabelMacro1.Enabled = Flag
        labelMacroMouse.Enabled = Flag

        LabelMacroCat.Enabled = Flag
        cmbMacroCat.Enabled = Flag

        imgMacro.Visible = Flag
        lstMacro.Enabled = Flag

        cmdExpand.Visible = False

        cmdMacroEdit.Enabled = Flag

    End Sub


    Private Sub SetEnableBeacon(ByVal Flag As Boolean)

        'frBeaconType.Enabled = Flag
        'frBeaconBase.Enabled = Flag
        optBeaconCivilian.Enabled = Flag
        optBeaconMil.Enabled = Flag
        optBeaconAirport.Enabled = Flag
        optBeaconHeliport.Enabled = Flag
        optBeaconSeaBase.Enabled = Flag

    End Sub

    Private Sub SetEnableWindSock(ByVal Flag As Boolean)

        'frWindsock.Enabled = Flag
        txtWindsockLength.Enabled = Flag
        txtWindsockHeight.Enabled = Flag
        ckWindsockLight.Enabled = Flag
        LabelWin1.Enabled = Flag
        LabelWin2.Enabled = Flag
        LabelWin3.Enabled = Flag

    End Sub

    Private Sub SetEnableTaxiway(ByVal Flag As Boolean)

        'frTaxiway.Enabled = Flag
        txtTaxiwayText.Enabled = Flag
        combTaxiwaySize.Enabled = Flag
        combTaxiwayJustification.Enabled = Flag
        LabelTax1.Enabled = Flag
        LabelTax2.Enabled = Flag
        LabelTax3.Enabled = Flag
        cmdTaxiwayHelp.Enabled = Flag

    End Sub

    Private Sub SetEnableEffect(ByVal Flag As Boolean)

        'frEffect.Enabled = Flag
        txtEffectName.Enabled = Flag
        txtEffectParameters.Enabled = Flag
        LabelEffect1.Enabled = Flag
        LabelEffect2.Enabled = Flag

    End Sub

    Private Sub SetEnableMDL(ByVal Flag As Boolean)

        'frMDL.Enabled = Flag

        txtMDLWidth.Enabled = Flag
        txtMDLLength.Enabled = Flag
        txtMDLFile.Enabled = Flag
        LabelMDL1.Enabled = Flag
        LabelMDL2.Enabled = Flag
        LabelMDL3.Enabled = Flag
        labelMDLguid.Enabled = Flag
        labelMDLName.Enabled = Flag
        cmdMDL.Enabled = Flag

    End Sub

    Private Sub SetEnableGenB(ByVal Flag As Boolean)

        frGenB.Enabled = Flag
        'frGenBTextures.Enabled = Flag

        nUPsizeX.Enabled = Flag
        nUPsizeZ.Enabled = Flag
        nUPscale.Enabled = Flag
        lbgb1.Enabled = Flag
        lbgb2.Enabled = Flag
        lbgb3.Enabled = Flag
        cmdGbLoad.Enabled = Flag
        cmdGbStore.Enabled = Flag
        cmdGBDelete.Enabled = Flag
        'cmdGBFind.Enabled = Flag
        cmdGbDetail.Enabled = Flag
        lstGenB.Enabled = Flag
        imgGenB.Visible = Flag
        Label14.Enabled = Flag
        Label15.Enabled = Flag

    End Sub

    Private Sub SetEnableLib(ByVal Flag As Boolean)

        frLib.Enabled = Flag

        txtLibWidth.Enabled = Flag
        txtLibLength.Enabled = Flag
        txtLibScale.Enabled = Flag
        txtLibName.Enabled = Flag
        LabelLib1.Enabled = Flag
        labelLib2.Enabled = Flag
        LabelLib3.Enabled = Flag
        LabelLibName.Enabled = Flag
        labelLibMouse.Enabled = Flag
        labelFS.Enabled = Flag

        'cmdUpDefault.Enabled = Flag
        LabelCat.Enabled = Flag
        cmbLibCat.Enabled = Flag

        imgLib.Visible = Flag
        lstLib.Enabled = Flag
        txtLibID.Enabled = Flag

    End Sub

    Private Sub LstLib_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstLib.SelectedIndexChanged

        Dim N, K As Integer
        Dim a, LibCat As String

        If Not IsReady Then Exit Sub

        ' get the category index
        K = cmbLibCat.SelectedIndex + 1
        LibCat = LibCategories(K).Name

        N = lstLib.SelectedIndex
        Dim myLibObj As LibObject = LibCategories(K).Objs(N)

        txtLibID.Text = myLibObj.ID
        txtLibWidth.Text = CStr(myLibObj.Width)
        txtLibLength.Text = CStr(myLibObj.Length)
        txtLibScale.Text = CStr(myLibObj.Scaling)
        txtLibName.Text = myLibObj.Name
        cmdUpDefault.Enabled = False

        ObjLibType = CInt(myLibObj.Type)

        ' after 205
        txtComment.Text = myLibObj.Name

        If ObjLibType = 0 Then
            labelFS.Text = "Old FS8 Library Object"
        ElseIf ObjLibType = 1 Then
            labelFS.Text = "Old FS9 Library Object"
        ElseIf ObjLibType = 2 Then
            labelFS.Text = "New FSX Library Object"
        End If

        'cmdUpDefault.Enabled = False


        a = LibObjectsPath & "\" & LibCat & "\" & txtLibID.Text & ".jpg"
        ImageFileNameTrue = a
        If Not File.Exists(a) Then
            a = My.Application.Info.DirectoryPath & "\tools\bmps\na.jpg"
        End If

        Dim fs As New System.IO.FileStream(a, IO.FileMode.Open, IO.FileAccess.Read)
        'imgLib.Image = System.Drawing.Image.FromFile(a)
        imgLib.Image = Image.FromStream(fs)
        fs.Close()

        ImageFileName = a

    End Sub

    Private Sub LstMacro_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstMacro.SelectedIndexChanged

        Dim N, K As Integer

        If Not IsReady Then Exit Sub

        'On Error Resume Next

        N = lstMacro.SelectedIndex + 1
        K = cmbMacroCat.SelectedIndex + 1

        ShowMacro(K, N)
        MacroID = MacroCategories(K).MacroObjects(N).File

        IsReady = True

    End Sub

    Private Sub LstRwy12_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstRwy12.SelectedIndexChanged

        If Not IsReady Then Exit Sub

        Dim N, K As Integer
        Dim a As String

        N = lstRwy12.SelectedIndex + 1
        K = cmbRwy12Cat.SelectedIndex + 1

        txtRwy12ID.Text = Rwy12Categories(K).Rwy12Objects(N).ID

        ' after 205
        txtComment.Text = Rwy12Categories(K).Rwy12Objects(N).Name

        a = Rwy12Path & "\img\" & Rwy12Categories(K).Rwy12Objects(N).Texture
        ImageFileNameTrue = a
        If Not File.Exists(a) Then a = My.Application.Info.DirectoryPath & "\tools\bmps\na.jpg"

        Dim fs As New System.IO.FileStream(a, IO.FileMode.Open, IO.FileAccess.Read)
        'imgRwy12.Image = System.Drawing.Image.FromFile(a)
        imgRwy12.Image = Image.FromStream(fs)
        fs.Close()

        ImageFileName = a

        IsReady = True

    End Sub


    Private Sub OptMacro_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optMacro.CheckedChanged
        If eventSender.Checked Then

            If IsInit Then Exit Sub

            DisableAllTypes()
            If MacroAPIIsOn = False And MacroASDIsOn = False Then Exit Sub
            SetEnableMacro(True)
            MacroID = ""
            FillMacroList()
            SSTab1.SelectedIndex = 3

        End If
    End Sub


    Private Sub OptEffect_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optEffect.CheckedChanged
        If eventSender.Checked Then

            If IsInit Then Exit Sub

            DisableAllTypes()
            SetEnableEffect(True)
            SSTab1.SelectedIndex = 5
            ' after 205
            txtComment.Text = "Effect object"

        End If
    End Sub

    Private Sub OptLib_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optLib.CheckedChanged

        If eventSender.Checked Then

            If IsInit Then Exit Sub

            DisableAllTypes()
            SetEnableLib(True)
            ObjLibID = ""
            FillLibList()
            SSTab1.SelectedIndex = 1

        End If
    End Sub

    Private Sub OptRwy12_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optRwy12.CheckedChanged
        If eventSender.Checked Then

            If IsInit Then Exit Sub

            DisableAllTypes()
            If Not Rwy12IsOn Then Exit Sub
            SetEnableRwy12(True)
            ObjLibID = ""
            FillRwy12List()
            SSTab1.SelectedIndex = 4

        End If
    End Sub


    Private Sub OptBeacon_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optBeacon.CheckedChanged
        If eventSender.Checked Then

            If IsInit Then Exit Sub

            DisableAllTypes()
            SetEnableBeacon(True)
            SSTab1.SelectedIndex = 5
            txtComment.Text = "Beacon object"
        End If
    End Sub


    Private Sub OptTaxiwaySign_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optTaxiwaySign.CheckedChanged

        If eventSender.Checked Then

            If IsInit Then Exit Sub
            DisableAllTypes()
            SetEnableTaxiway(True)
            SSTab1.SelectedIndex = 5
            ' after 205
            txtComment.Text = "Taxiway Sign"
        End If

    End Sub

    Private Sub OptWindSock_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optWindsock.CheckedChanged
        If eventSender.Checked Then

            If IsInit Then Exit Sub

            DisableAllTypes()
            SetEnableWindSock(True)
            SSTab1.SelectedIndex = 5
            ' after 205
            txtComment.Text = "WindSock object"

        End If
    End Sub


    Private Sub OptMDL_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optMDL.CheckedChanged

        If eventSender.Checked Then

            If IsInit Then Exit Sub

            DisableAllTypes()
            SetEnableMDL(True)
            SSTab1.SelectedIndex = 5
            ' after 205
            txtComment.Text = "MDL Model object"

        End If

    End Sub

    Private Sub OptGenB_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optGenB.CheckedChanged

        If eventSender.Checked Then

            If IsInit Then Exit Sub

            DisableAllTypes()
            SetEnableGenB(True)
            FillGenBList()
            SSTab1.SelectedIndex = 2
            ' after 205
            txtComment.Text = "Generic_Building_Object"

        End If

    End Sub


    Private Sub TxtWindsockHeight_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWindsockHeight.DoubleClick

        ARGBColor = txtWindsockHeight.BackColor
        If FrmTransparency.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtWindsockHeight.BackColor = ARGBColor
        End If
        txtWindsockHeight.ForeColor = InvertColor(ARGBColor)

    End Sub

    Private Sub TxtWindsockLength_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWindsockLength.DoubleClick

        ARGBColor = txtWindsockLength.BackColor
        If FrmTransparency.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtWindsockLength.BackColor = ARGBColor
        End If
        txtWindsockLength.ForeColor = InvertColor(ARGBColor)


    End Sub

    Private Sub FillLibList()

        ' uses the ObjLibID and fills the category, the member list and
        ' selects the object
        ' if ObjLibID is nothing or not found then uses 1st obj in 1st cat

        Dim N, K As Integer
        Dim a As String
        Dim LibCat As String = ""
        Dim g

        Dim myLibObj As LibObject = Nothing

        If NoOfLibCategories = 0 Then Exit Sub

        IsReady = False

        'first remove
        lstLib.Items.Clear()
        cmbLibCat.Items.Clear()

        For K = 1 To NoOfLibCategories
            cmbLibCat.Items.Add(LibCategories(K).Name)
        Next K

        Dim Found As Boolean = False
        If ObjLibID Is Nothing Then
            'Debug.Print("ObjLibID Is Nothing")
            myLibObj = LibCategories(1).Objs(0)
            ObjLibID = LibCategories(1).Objs(0).ID
            Found = True  ' it is a new object not an unknown one!
            N = 0
            K = 1
        Else
            For K = 1 To NoOfLibCategories
                N = 0
                For Each g In LibCategories(K).Objs
                    If g.ID = ObjLibID Then
                        myLibObj = g
                        Found = True
                        Exit For
                    End If
                    N = N + 1
                Next
                If Found Then Exit For
            Next
        End If

        If Not Found Then
            'Debug.Print("ObjLibID Not Found")
            myLibObj = LibCategories(1).Objs(0)
            N = 0
            K = 1
        End If

        ' select category
        LibCat = LibCategories(K).Name
        cmbLibCat.SelectedIndex = K - 1

        ' fill objects and set selected
        For Each g In LibCategories(K).Objs
            lstLib.Items.Add(g.Name)
        Next

        'txtLibID.Text = myLibObj.ID   ' changed when unknown was introduced
        txtLibID.Text = ObjLibID
        txtLibWidth.Text = CStr(myLibObj.Width)
        txtLibLength.Text = CStr(myLibObj.Length)
        txtLibScale.Text = CStr(myLibObj.Scaling)
        txtLibName.Text = myLibObj.Name
        cmdUpDefault.Enabled = False

        ' after 205
        txtComment.Text = myLibObj.Name

        ObjLibType = CInt(myLibObj.Type)

        If ObjLibType = 0 Then
            labelFS.Text = "Old FS8 Library Object"
        ElseIf ObjLibType = 1 Then
            labelFS.Text = "Old FS9 Library Object"
        ElseIf ObjLibType = 2 Then
            labelFS.Text = "New FSX Library Object"
        End If

        If Not Found Then
            a = My.Application.Info.DirectoryPath & "\tools\bmps\unknown.jpg"
        Else
            lstLib.SelectedIndex = N
            a = LibObjectsPath & "\" & LibCat & "\" & txtLibID.Text & ".jpg"
            ImageFileNameTrue = a
            If Not File.Exists(a) Then
                a = My.Application.Info.DirectoryPath & "\tools\bmps\na.jpg"
            End If
        End If

        Dim fs As New System.IO.FileStream(a, IO.FileMode.Open, IO.FileAccess.Read)
        imgLib.Image = Image.FromStream(fs)
        fs.Close()

        ImageFileName = a

        IsReady = True

    End Sub

    Private Sub FillRwy12List()

        ' uses the ObjLibID and fills the category, the member list and
        ' selects the object
        ' if ObjLibID not found then uses the first one in objects.txt

        Dim K, N, J As Integer
        Dim a As String
        Dim Flag As Boolean

        'first remove

        IsReady = False

        ' first remove all categories
        N = cmbRwy12Cat.Items.Count

        For K = N To 1 Step -1
            cmbRwy12Cat.Items.RemoveAt(K - 1)
        Next K

        ' first remove all objects
        N = lstRwy12.Items.Count
        For K = N To 1 Step -1
            lstRwy12.Items.RemoveAt(K - 1)
        Next K

        For K = 1 To NoOfRwy12Categories
            cmbRwy12Cat.Items.Add(Rwy12Categories(K).Name)
        Next K

        Flag = True
        For K = 1 To NoOfRwy12Categories
            For N = 1 To Rwy12Categories(K).NOB
                If Rwy12Categories(K).Rwy12Objects(N).ID = ObjLibID Then
                    Flag = False
                    GoTo HERE
                End If
            Next N
        Next K

HERE:
        If Flag Then
            K = 1
            N = 1
        End If

        cmbRwy12Cat.SelectedIndex = K - 1

        For J = 1 To Rwy12Categories(K).NOB
            lstRwy12.Items.Add(Rwy12Categories(K).Rwy12Objects(J).Name)
        Next J

        lstRwy12.SelectedIndex = N - 1
        txtRwy12ID.Text = Rwy12Categories(K).Rwy12Objects(N).ID
        txtRwy12Width.Text = CStr(100)
        txtRwy12Length.Text = CStr(100)
        txtRwy12Scale.Text = CStr(1)

        a = Rwy12Path & "\img\" & Rwy12Categories(K).Rwy12Objects(N).Texture
        ImageFileNameTrue = a
        If Not File.Exists(a) Then a = My.Application.Info.DirectoryPath & "\tools\bmps\na.jpg"

        Dim fs As New System.IO.FileStream(a, IO.FileMode.Open, IO.FileAccess.Read)
        'imgRwy12.Image = System.Drawing.Image.FromFile(a)
        imgRwy12.Image = Image.FromStream(fs)
        fs.Close()

        ImageFileName = a

        IsReady = True

    End Sub


    Private Sub FillMacroList()

        ' uses the MacroID and fills the category, the member list and
        ' selects the object
        ' if MacroID not found then uses the first one in objects.txt

        Dim K, N, J As Integer
        Dim Flag As Boolean
        'first remove

        IsReady = False

        ' first remove all categories
        N = cmbMacroCat.Items.Count

        For K = N To 1 Step -1
            cmbMacroCat.Items.RemoveAt(K - 1)
        Next K

        ' first remove all objects
        N = lstMacro.Items.Count
        For K = N To 1 Step -1
            lstMacro.Items.RemoveAt(K - 1)
        Next K

        For K = 1 To NoOfMacroCategories
            cmbMacroCat.Items.Add(MacroCategories(K).Name)
        Next K

        Flag = True
        For K = 1 To NoOfMacroCategories
            For N = 1 To MacroCategories(K).NOB
                If MacroCategories(K).MacroObjects(N).File = MacroID Then
                    Flag = False
                    GoTo HERE
                End If
            Next N
        Next K

HERE:
        If Flag Then
            K = 1
            N = 1
        End If

        cmbMacroCat.SelectedIndex = K - 1

        For J = 1 To MacroCategories(K).NOB
            lstMacro.Items.Add(MacroCategories(K).MacroObjects(J).Name)
        Next J

        lstMacro.SelectedIndex = N - 1

        ShowMacro(K, N)
        MacroID = MacroCategories(K).MacroObjects(N).File

        IsReady = True

    End Sub

    Private Sub ShowMacro(ByVal C As Integer, ByVal M As Integer)

        Dim a As String

        a = VB.Right(MacroCategories(C).MacroObjects(M).File, 3)

        ' after 205
        txtComment.Text = MacroCategories(C).MacroObjects(M).Name

        lbP6.Visible = False
        lbP7.Visible = False
        lbP8.Visible = False
        lbP9.Visible = False

        txtP6.Visible = False
        txtP7.Visible = False
        txtP8.Visible = False
        txtP9.Visible = False
        cmdExpand.Visible = False

        If a = "API" Then
            LabelMacro1.Text = "API Macro Object"
            ShowAPI(C, M)
            txtMacroScale.Text = CStr(MacroScale)
            txtMacroRange.Text = CStr(MacroRange)
            txtMacroWidth.Text = CStr(MacroWidth)
            txtMacroLength.Text = CStr(MacroLength)

            Dim fs As New System.IO.FileStream(MacroBitmap, IO.FileMode.Open, IO.FileAccess.Read)
            imgMacro.Image = System.Drawing.Image.FromFile(MacroBitmap)
            imgMacro.Image = Image.FromStream(fs)
            fs.Close()

            ImageFileName = MacroBitmap

            lbP6.Visible = True
            lbP7.Visible = True
            lbP8.Visible = True
            lbP9.Visible = True

            txtP6.Visible = True
            txtP7.Visible = True
            txtP8.Visible = True
            txtP9.Visible = True

            lbP6.Text = "Parameter 6"
            lbP7.Text = "Parameter 7"
            lbP8.Text = "Parameter 8"
            lbP9.Text = "Parameter 9"

            txtP6.Text = "0"
            txtP7.Text = "0"
            txtP8.Text = "0"
            txtP9.Text = "0"

        End If

        If a = "SCM" Then

            LabelMacro1.Text = "ASD Macro Object"
            ShowASD(C, M)

            txtMacroScale.Text = CStr(MacroScale)
            txtMacroRange.Text = CStr(MacroRange)
            txtMacroWidth.Text = CStr(MacroWidth)
            txtMacroLength.Text = CStr(MacroLength)

            Dim fs As New System.IO.FileStream(MacroBitmap, IO.FileMode.Open, IO.FileAccess.Read)
            imgMacro.Image = System.Drawing.Image.FromFile(MacroBitmap)
            imgMacro.Image = Image.FromStream(fs)
            fs.Close()

            ImageFileName = MacroBitmap

            If MacroRotation <> 0 Then
                txtHeading.Text = CStr(MacroRotation)
            End If
            txtAltitude.Text = CStr(MacroElevation)
            If MacroElevation = 0 Then
                ckAGL.CheckState = System.Windows.Forms.CheckState.Checked
            Else
                ckAGL.CheckState = System.Windows.Forms.CheckState.Unchecked
            End If

            If MacroDensity = 0 Then opVSparse.Checked = True
            If MacroDensity = 1 Then opSparse.Checked = True
            If MacroDensity = 2 Then opNormal.Checked = True
            If MacroDensity = 3 Then opDense.Checked = True
            If MacroDensity = 4 Then opVDense.Checked = True

            txtV1.Text = CStr(MacroVisibility)

            If MacroP6Name <> "" Then
                lbP6.Visible = True
                txtP6.Visible = True
                lbP6.Text = MacroP6Name
                txtP6.Text = MacroP6Value
                lbP6.Text = MacroP6Name
                txtP6.Text = MacroP6Value
            End If

            If MacroP7Name <> "" Then
                lbP7.Visible = True
                txtP7.Visible = True
                lbP7.Text = MacroP7Name
                txtP7.Text = MacroP7Value
                lbP7.Text = MacroP7Name
                txtP7.Text = MacroP7Value
            End If

            If MacroP8Name <> "" Then
                lbP8.Visible = True
                txtP8.Visible = True
                lbP8.Text = MacroP8Name
                txtP8.Text = MacroP8Value
                lbP8.Text = MacroP8Name
                txtP8.Text = MacroP8Value
            End If

            If MacroP9Name = "" Then
                Exit Sub
            Else
                If MacroPAName = "" Then
                    lbP9.Visible = True
                    txtP9.Visible = True
                    lbP9.Text = MacroP9Name
                    txtP9.Text = MacroP9Value
                    lbP9.Text = MacroP9Name
                    txtP9.Text = MacroP9Value
                Else
                    cmdExpand.Visible = True
                End If
            End If
        End If

    End Sub


    Private Sub CmdMDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMDL.Click

        Dim A, B, mdlFile As String
        Dim MDLDir As String = AppPath & "\Mdls"

        A = "Object Model file (*.MDL)|*.mdl"
        B = "SBuilderX: Select Object Model file"

        A = FileNameToOpen(A, B, "MDL")

        If A = "" Then
            Exit Sub
        End If

        mdlFile = Path.GetFileName(A)
        B = MDLDir & "\" & mdlFile

        If Path.GetDirectoryName(A) <> MDLDir Then
            File.Copy(A, B)
        End If

        Dim fs As New FileStream(B, FileMode.Open, FileAccess.Read)
        Dim reader As New BinaryReader(fs)

        Dim mdl As New MDLReader
        If mdl.Read(reader) Then
            txtMDLFile.Text = mdlFile
            txtMDLWidth.Text = mdl.Width
            txtMDLLength.Text = mdl.Lenght
            If mdl.Type = 1 Then  ' it is FS9
                Dim G As Guid = Guid.NewGuid() ' create one!
                labelMDLName.Text = Path.GetFileNameWithoutExtension(mdlFile)
                labelMDLguid.Text = G.ToString("N")   ' present the guid in FS9 format
            Else  ' it is FSX
                labelMDLName.Text = mdl.Name
                labelMDLguid.Text = mdl.Guid
            End If
        Else
            MsgBox("Not a valid MDL")
        End If

        ' should I realy comment the following? October 2017
        'reader.Close()
        fs.Dispose()

    End Sub

    Private Sub FrmDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frMDL.Click
        'mdl
        optMDL.Checked = True
    End Sub

    Private Sub FrTaxiway_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frTaxiway.Click
        'taxiwaysign
        optTaxiwaySign.Checked = True
    End Sub

    'Private Sub _SSTab1_TabPage4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _SSTab1_TabPage4.Click
    '    'rwy12
    '    If Rwy12IsOn = False Then Exit Sub
    '    optRwy12.Checked = True
    'End Sub

    Private Sub FrRwy12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frRwy12.Click
        'rwy12
        If Rwy12IsOn = False Then Exit Sub
        optRwy12.Checked = True
    End Sub
    'Private Sub _SSTab1_TabPage3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _SSTab1_TabPage3.Click
    '    'macro
    '    optMacro.Checked = True
    'End Sub

    Private Sub Frmacro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frMacro.Click
        'macro
        If MacroAPIIsOn = False And MacroASDIsOn = False Then Exit Sub
        optMacro.Checked = True
    End Sub

    Private Sub Frmacro1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frMacro1.Click
        'macro
        If MacroAPIIsOn = False And MacroASDIsOn = False Then Exit Sub
        optMacro.Checked = True
    End Sub

    'Private Sub _SSTab1_TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _SSTab1_TabPage1.Click
    '    'lib
    '    If LibObjectsIsOn = False Then Exit Sub
    '    optLib.Checked = True
    'End Sub

    'Private Sub _SSTab1_TabPage2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _SSTab1_TabPage2.Click
    '    'genBuild

    '    optGenB.Checked = True
    'End Sub

    Private Sub FrWindsock_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles frWindsock.Click
        'windsock
        optWindsock.Checked = True
    End Sub

    Private Sub FrBeaconBase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frBeaconBase.Click
        'beacon
        optBeacon.Checked = True
    End Sub

    Private Sub FrBeaconType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frBeaconType.Click
        'beacon
        optBeacon.Checked = True
    End Sub

    Private Sub FrEffect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frEffect.Click
        'effect
        optEffect.Checked = True
    End Sub


    Private Sub TxtLibName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLibName.TextChanged

        cmdUpDefault.Enabled = True

    End Sub


    Private Sub TxtLibLength_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLibLength.TextChanged

        cmdUpDefault.Enabled = True

    End Sub

    Private Sub TxtLibWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLibWidth.TextChanged

        cmdUpDefault.Enabled = True

    End Sub

    Private Sub TxtLibScale_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLibScale.TextChanged

        cmdUpDefault.Enabled = True

    End Sub


    Private Sub CmdUpDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpDefault.Click

        Dim myObj As New LibObject

        Dim K As Integer = cmbLibCat.SelectedIndex + 1
        If K < 1 Then Exit Sub
        Dim N As Integer = lstLib.SelectedIndex
        If N < 0 Then Exit Sub
        myObj.ID = LibCategories(K).Objs(N).ID
        myObj.Name = txtLibName.Text
        myObj.Type = LibCategories(K).Objs(N).Type
        myObj.Width = Val(txtLibWidth.Text)
        myObj.Length = Val(txtLibLength.Text)
        myObj.Scaling = Val(txtLibScale.Text)
        LibCategories(K).Objs(N) = myObj
        lstLib.Items.Clear()
        Dim g
        For Each g In LibCategories(K).Objs
            lstLib.Items.Add(g.name)
        Next
        lstLib.SelectedIndex = N

        Dim A As String
        Dim stream As FileStream
        Dim fileWriter As System.IO.StreamWriter
        stream = New FileStream(LibObjectsPath & "\" & LibCategories(K).Name & ".txt", FileMode.Create)
        fileWriter = New System.IO.StreamWriter(stream)
        fileWriter.WriteLine("[" & LibCategories(K).Name & "]")
        For Each g In LibCategories(K).Objs
            A = g.ID & " " & g.Type & " " & g.Width & " " & g.Length & " " & g.Scaling & " " & g.Name
            fileWriter.WriteLine(A)
        Next

        'should I realy comment the following? October 2017
        'fileWriter.Close()
        stream.Close()

        cmdUpDefault.Enabled = False

    End Sub


    Private Sub CapturePopUPMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CapturePopUpMenuItem.Click

        Hide()
        frmStart.Hide()
        If frmSCREEN.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                If optLib.Checked Then
                    imgLib.Image = frmSCREEN.MyCapture
                    imgLib.Image.Save(ImageFileNameTrue, Imaging.ImageFormat.Jpeg)  'saves the screen shot
                End If
                If optMacro.Checked Then
                    imgMacro.Image = frmSCREEN.MyCapture
                    imgMacro.Image.Save(ImageFileNameTrue, Imaging.ImageFormat.Jpeg)  'saves the screen shot
                End If
                If optRwy12.Checked Then
                    imgRwy12.Image = frmSCREEN.MyCapture
                    imgRwy12.Image.Save(ImageFileNameTrue, Imaging.ImageFormat.Jpeg)  'saves the screen shot
                End If

                If optGenB.Checked Then
                    imgGenB.Image = frmSCREEN.MyCapture
                    imgGenB.Image.Save(ImageFileNameTrue, Imaging.ImageFormat.Jpeg)  'saves the screen shot
                End If

            Catch ex As Exception
                MsgBox("Could not save the image!", MsgBoxStyle.Information)
            End Try
        End If
        frmSCREEN.Dispose()
        Show()
        frmStart.Show()

    End Sub


    Private Sub ImgRwy12_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles imgRwy12.MouseDown

        Dim Button As Short = e.Button \ &H100000
        Dim N As Integer = InStrRev(ImageFileName, "\")
        Dim A As String = UCase(Mid(ImageFileName, N + 1))
        If Button = 1 Then
            If A <> "NA.JPG" Then FrmImage.ShowDialog()
        ElseIf Button = 2 Then
            If A = "NA.JPG" Then
                EnlargePopUpMenuItem.Visible = False
                DeletePopUpMenuItem.Visible = False
            Else
                EnlargePopUpMenuItem.Visible = True
                DeletePopUpMenuItem.Visible = True
            End If
            ' frmImage.ShowDialog()
            TitlePopUpMenuItem.Text = "RWY12 THUMBNAIL"
            PurgePopUPMenuItem.Visible = False
        End If

    End Sub

    Private Sub ImgMacro_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles imgMacro.MouseDown

        Dim Button As Short = e.Button \ &H100000
        Dim N As Integer = InStrRev(ImageFileName, "\")
        Dim A As String = UCase(Mid(ImageFileName, N + 1))
        If Button = 1 Then
            If A <> "NA.JPG" Then FrmImage.ShowDialog()
        ElseIf Button = 2 Then
            If A = "NA.JPG" Then
                EnlargePopUpMenuItem.Visible = False
                DeletePopUpMenuItem.Visible = False
            Else
                EnlargePopUpMenuItem.Visible = True
                DeletePopUpMenuItem.Visible = True
            End If
            ' frmImage.ShowDialog()
            TitlePopUpMenuItem.Text = "MACRO THUMBNAIL"
            PurgePopUPMenuItem.Visible = False
        End If

    End Sub


    Private Sub ImgLib_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles imgLib.MouseDown

        Dim Button As Short = e.Button \ &H100000
        Dim N As Integer = InStrRev(ImageFileName, "\")
        Dim A As String = UCase(Mid(ImageFileName, N + 1))
        If A = "UNKNOWN.JPG" Then
            ShowPopUpMenu(False)
            Exit Sub
        End If
        ShowPopUpMenu(True)
        If Button = 1 Then
            If A <> "NA.JPG" Then FrmImage.ShowDialog()
        ElseIf Button = 2 Then
            If A = "NA.JPG" Then
                EnlargePopUpMenuItem.Visible = False
                DeletePopUpMenuItem.Visible = False
            Else
                EnlargePopUpMenuItem.Visible = True
                DeletePopUpMenuItem.Visible = True
            End If
            ' frmImage.ShowDialog()
            TitlePopUpMenuItem.Text = "LIB OBJECT THUMBNAIL"
            PurgePopUPMenuItem.Visible = True
        End If

    End Sub
    Private Sub ShowPopUpMenu(ByVal flag As Boolean)

        EnlargePopUpMenuItem.Visible = flag
        DeletePopUpMenuItem.Visible = flag
        PurgePopUPMenuItem.Visible = flag
        TitlePopUpMenuItem.Visible = flag
        CapturePopUpMenuItem.Visible = flag
        FromFilePopUpMenuItem.Visible = flag
        ToolStripSeparator1.Visible = flag

    End Sub

    Private Sub EnlargePopUpMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnlargePopUpMenuItem.Click
        FrmImage.ShowDialog()
    End Sub

    Private Sub DeletePopUpMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeletePopUpMenuItem.Click

        Try

            Dim na As String = My.Application.Info.DirectoryPath & "\tools\bmps\na.jpg"
            If optLib.Checked Then
                imgLib.Image = System.Drawing.Image.FromFile(na)
            End If
            If optMacro.Checked Then
                imgMacro.Image = System.Drawing.Image.FromFile(na)
            End If
            If optRwy12.Checked Then
                imgRwy12.Image = System.Drawing.Image.FromFile(na)
            End If
            If optGenB.Checked Then
                imgGenB.Image = System.Drawing.Image.FromFile(na)
            End If
            ImageFileName = na

            File.Delete(ImageFileNameTrue)

        Catch ex As Exception

            Dim a As String = "Could not delete the file:" & vbCrLf
            a = a & ImageFileNameTrue
            MsgBox(a, MsgBoxStyle.Information)

        End Try

    End Sub

    Private Sub FromFilePopUpMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FromFilePopUpMenuItem.Click

        Dim a, b, FileName As String
        FileName = ""

        Try

            a = "JPEG Files (*.JPG)|*.JPG"
            b = "SBuilderX - Find a JPEG Thumbnail"
            FileName = FileNameToOpen(a, b, "")
            If FileName = "" Then Exit Sub

            If My.Computer.FileSystem.FileExists(FileName) Then

                If optLib.Checked Then
                    imgLib.Image = System.Drawing.Image.FromFile(FileName)
                End If
                If optMacro.Checked Then
                    imgMacro.Image = System.Drawing.Image.FromFile(FileName)
                End If
                If optRwy12.Checked Then
                    imgRwy12.Image = System.Drawing.Image.FromFile(FileName)
                End If
                If optGenB.Checked Then
                    imgGenB.Image = System.Drawing.Image.FromFile(FileName)
                End If

                My.Computer.FileSystem.CopyFile(FileName, ImageFileNameTrue)
                ImageFileName = FileName
                ImageFileNameTrue = FileName

            End If

        Catch ex As Exception

            a = "The file can not be used as a thumbnail" & vbCrLf
            MsgBox(a, MsgBoxStyle.Information)

        End Try

    End Sub

    Private Sub PurgePopUPMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurgePopUPMenuItem.Click

        Dim A As String = "Do you want to delete unused Thumbnail Jpegs" & vbCrLf
        A = A & "found in the directory for this category of objects ?"

        If MsgBox(A, MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
            Exit Sub
        End If

        Dim K As Integer
        Dim fileName As String = ""
        Dim foundFile As String = ""
        Dim FilesToCheck As New ArrayList

        If optLib.Checked Then
            ' get the category index
            K = cmbLibCat.SelectedIndex + 1
            Dim g
            For Each g In LibCategories(K).Objs
                FilesToCheck.Add(g.id)
            Next
            For Each foundFile In My.Computer.FileSystem.GetFiles _
                (LibObjectsPath & "\" & LibCategories(K).Name)
                fileName = Path.GetFileNameWithoutExtension(foundFile)
                If Not FilesToCheck.Contains(fileName) Then
                    If Path.GetExtension(foundFile).ToUpper = ".JPG" Then
                        My.Computer.FileSystem.DeleteFile(foundFile)
                    End If
                End If
            Next
        End If

        If optGenB.Checked Then

            For Each foundFile In My.Computer.FileSystem.GetFiles(AppPath & "\GenBuildings")
                fileName = Path.GetFileNameWithoutExtension(foundFile)
                If Not lstGenB.Items.Contains(fileName) Then
                    If Path.GetExtension(foundFile).ToUpper = ".JPG" Then
                        My.Computer.FileSystem.DeleteFile(foundFile)
                    End If
                End If
            Next
        End If

    End Sub



    Private Sub CmdGbDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGbDetail.Click

        If optGbFlat.Checked Then BuildingType = 256
        If optGbPeaked.Checked Then BuildingType = 257
        If optGbRidge.Checked Then BuildingType = 258
        If optGbSlant.Checked Then BuildingType = 259
        If optGbPyramidal.Checked Then BuildingType = 260
        If optGbMultiSided.Checked Then BuildingType = 261
        sizeX = nUPsizeX.Value
        sizeZ = nUPsizeZ.Value
        scale_gb = nUPscale.Value
        FrmGBuilding.ShowDialog()

    End Sub


    Private Sub FillGenBList()

        If NoOfGenBObjects < 1 Then Exit Sub
        IsReady = False
        nUPsizeX.Value = sizeX
        nUPsizeZ.Value = sizeZ
        nUPscale.Value = scale_gb
        'nUPbottomTexture.Value = bottomTexture
        'nUPwindowTexture.Value = windowTexture
        'nUPtopTexture.Value = topTexture
        'nUProofTexture.Value = roofTexture

        lstGenB.Items.Clear()
        Dim K As Integer

        For K = 1 To NoOfGenBObjects
            lstGenB.Items.Add(GenBObjects(K).name)
        Next K
        IsReady = True
        If NoOfGenBObjects > 0 Then lstGenB.SelectedIndex = 0

    End Sub


    Private Sub CmdGbStore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGbStore.Click

        IsReady = False

        Dim type As Integer

        If optGbFlat.Checked Then type = 256
        If optGbPeaked.Checked Then type = 257
        If optGbRidge.Checked Then type = 258
        If optGbSlant.Checked Then type = 259
        If optGbPyramidal.Checked Then type = 260
        If optGbMultiSided.Checked Then type = 261

        Dim width As String = Trim(Str(nUPsizeX.Value))
        Dim lenght As String = Trim(Str(nUPsizeZ.Value))
        Dim scale As String = Trim(Str(nUPscale.Value))

        'bottomTexture = nUPbottomTexture.Value
        'roofTexture = nUProofTexture.Value
        'topTexture = nUPtopTexture.Value
        'windowTexture = nUPwindowTexture.Value

        Dim textures As String = MakeGBTextures()
        Dim indexes As String = MakeGBIndexes(type)

        Dim name As String = txtComment.Text
        name = Replace(name, " ", "_")
        If name = "" Then name = "Generic_Building_No_Name"

        Dim myLine As String = CStr(type) & " " & width & " " & lenght & " " & scale _
                               & " " & textures & " " & indexes & " " & name & vbCrLf

        NoOfGenBObjects = NoOfGenBObjects + 1
        ReDim Preserve GenBObjects(NoOfGenBObjects)

        Dim K As Integer = lstGenB.SelectedIndex + 1
        If K = 0 Then
            K = K + 1 ' in case there none in the list
        End If

        Dim N As Integer
        For N = NoOfGenBObjects To K + 1 Step -1
            GenBObjects(N) = GenBObjects(N - 1)
        Next N

        GenBObjects(K).type = type
        GenBObjects(K).sizeX = Val(width)
        GenBObjects(K).sizeZ = Val(lenght)
        GenBObjects(K).scale = Val(scale)
        GenBObjects(K).textures = textures
        GenBObjects(K).indexes = indexes
        GenBObjects(K).name = name

        lstGenB.Items.Clear()
        For N = 1 To NoOfGenBObjects
            lstGenB.Items.Add(GenBObjects(N).name)
        Next N
        If NoOfGenBObjects > 0 Then lstGenB.SelectedIndex = K - 1

        Dim newText As String = ""
        N = 0
        Dim myFile As String = AppPath & "\GenBuildings\GenBuildings.txt"
        Dim stream As FileStream = New FileStream(myFile, FileMode.Open)
        Dim fileReader As System.IO.StreamReader = New System.IO.StreamReader(stream)
        Dim line As String = ""
        Do Until fileReader.EndOfStream
            line = fileReader.ReadLine()
            N = N + 1
            If N = K Then newText = newText & myLine
            newText = newText & line & vbCrLf
        Loop

        'should I realy comment the following? October 2017
        'fileReader.Close()
        stream.Close()

        If N = 0 Then newText = myLine ' in case the file was empty
        My.Computer.FileSystem.WriteAllText(myFile, newText, False)

        IsReady = True

    End Sub

    Private Sub CmdGBDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGBDelete.Click

        If NoOfGenBObjects = 0 Then Exit Sub
        IsReady = False

        Dim K As Integer = lstGenB.SelectedIndex + 1
        Dim N As Integer

        For N = K + 1 To NoOfGenBObjects
            GenBObjects(N - 1) = GenBObjects(N)
        Next N

        NoOfGenBObjects = NoOfGenBObjects - 1
        ' ReDim Preserve GenBObjects(NoOfGenBObjects)

        lstGenB.Items.Clear()
        For N = 1 To NoOfGenBObjects
            lstGenB.Items.Add(GenBObjects(N).name)
        Next N

        If K > NoOfGenBObjects Then
            If NoOfGenBObjects > 0 Then lstGenB.SelectedIndex = K - 2
        Else
            If NoOfGenBObjects > 0 Then lstGenB.SelectedIndex = K - 1
        End If

        Dim newText As String = ""
        N = 0
        Dim myFile As String = AppPath & "\GenBuildings\GenBuildings.txt"
        Dim stream As FileStream = New FileStream(myFile, FileMode.Open)
        Dim fileReader As System.IO.StreamReader = New System.IO.StreamReader(stream)
        Dim line As String = ""
        Do Until fileReader.EndOfStream
            line = fileReader.ReadLine()
            N = N + 1
            If Not N = K Then
                newText = newText & line & vbCrLf
            End If
        Loop

        'should I realy comment the following? October 2017
        'fileReader.Close()
        stream.Close()

        My.Computer.FileSystem.WriteAllText(myFile, newText, False)

        IsReady = True

    End Sub

    Private Sub CmdGbLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGbLoad.Click

        If NoOfGenBObjects = 0 Then Exit Sub

        Dim K As Integer = lstGenB.SelectedIndex + 1
        Dim type As Integer = GenBObjects(K).type
        sizeX = GenBObjects(K).sizeX
        sizeZ = GenBObjects(K).sizeZ
        scale_gb = GenBObjects(K).scale
        Dim textures As String = GenBObjects(K).textures
        Dim indexes As String = GenBObjects(K).indexes
        Dim name As String = GenBObjects(K).name

        Objects(0).Type = type
        Objects(0).Description = Trim(Str(scale_gb)) & "|" & textures & "|" & indexes & "|" & name
        AnalyseGenBObject(0)

        ObjComment = name
        txtComment.Text = name

        nUPsizeX.Value = sizeX
        nUPsizeZ.Value = sizeZ
        nUPscale.Value = scale_gb
        'nUPbottomTexture.Value = bottomTexture
        'nUPwindowTexture.Value = windowTexture
        'nUPtopTexture.Value = topTexture
        'nUProofTexture.Value = roofTexture

        If type = 256 Then optGbFlat.Checked = True
        If type = 257 Then optGbPeaked.Checked = True
        If type = 258 Then optGbRidge.Checked = True
        If type = 259 Then optGbSlant.Checked = True
        If type = 260 Then optGbPyramidal.Checked = True
        If type = 261 Then optGbMultiSided.Checked = True

    End Sub


    Private Sub LstGenB_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstGenB.SelectedIndexChanged

        If Not IsReady Then Exit Sub
        Dim K As Integer = lstGenB.SelectedIndex + 1

        Dim a As String = AppPath & "\GenBuildings\" & GenBObjects(K).name & ".jpg"
        ImageFileNameTrue = a
        If Not File.Exists(a) Then
            a = My.Application.Info.DirectoryPath & "\tools\bmps\na.jpg"
        End If

        Dim fs As New System.IO.FileStream(a, IO.FileMode.Open, IO.FileAccess.Read)
        'imgGenB.Image = System.Drawing.Image.FromFile(a)
        imgGenB.Image = Image.FromStream(fs)
        fs.Close()

        ImageFileName = a

    End Sub

    Private Sub ImgGenB_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles imgGenB.MouseDown

        Dim Button As Short = e.Button \ &H100000
        Dim N As Integer = InStrRev(ImageFileName, "\")
        Dim A As String = UCase(Mid(ImageFileName, N + 1))
        If Button = 1 Then
            If A <> "NA.JPG" Then FrmImage.ShowDialog()
        ElseIf Button = 2 Then
            If A = "NA.JPG" Then
                EnlargePopUpMenuItem.Visible = False
                DeletePopUpMenuItem.Visible = False
            Else
                EnlargePopUpMenuItem.Visible = True
                DeletePopUpMenuItem.Visible = True
            End If
            ' frmImage.ShowDialog()
            TitlePopUpMenuItem.Text = "GEN BUILDING THUMBNAIL"
            PurgePopUPMenuItem.Visible = True
        End If

    End Sub


    'Private Sub CmdGBFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Dim A As String = CStr(bottomTexture) & "|"
    '    A = A & CStr(roofTexture) & "|"
    '    A = A & CStr(topTexture) & "|"
    '    A = A & CStr(windowTexture)

    '    Dim type As Integer

    '    If optGbFlat.Checked Then type = 256
    '    If optGbPeaked.Checked Then type = 257
    '    If optGbRidge.Checked Then type = 258
    '    If optGbSlant.Checked Then type = 259
    '    If optGbPyramidal.Checked Then type = 260
    '    If optGbMultiSided.Checked Then type = 261

    '    Dim N As Integer
    '    For N = 1 To NoOfGenBObjects
    '        If type = GenBObjects(N).type Then
    '            If A = GenBObjects(N).textures Then
    '                lstGenB.SelectedIndex = N - 1
    '                Exit Sub
    '            End If
    '        End If
    '    Next N

    '    A = "No Pre-Stored Generic Building with the " & vbCrLf
    '    A = A & "shown type and textures is available!"
    '    MsgBox(A, MsgBoxStyle.Information)

    'End Sub

    Private Sub SSTab1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SSTab1.SelectedIndexChanged

        ' June,8, 2014, to show tab pages filled when we
        ' click on the tab

        Dim N As Integer = SSTab1.SelectedIndex

        If N = 1 Then
            If LibObjectsIsOn = False Then Exit Sub
            optLib.Checked = True
        End If

        If N = 2 Then
            optGenB.Checked = True
        End If

        If N = 3 Then
            optMacro.Checked = True
        End If

        If N = 4 Then
            If Rwy12IsOn = False Then Exit Sub
            optRwy12.Checked = True
        End If

    End Sub
End Class