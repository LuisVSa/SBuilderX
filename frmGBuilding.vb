Option Strict On

Imports slimDX
Imports SlimDX.Direct3D9

Public Class FrmGBuilding

    Private FullScreen As Boolean = False
    Private Grid As Single = 10.0!
    Private helpInfo As String = "Left mouse to rotate. Mouse wheel to zoom (rolling) or to pan (pressing). Right mouse to return."

    Private Sub CancelCommand(ByVal sender As Object, ByVal e As System.EventArgs)
        Dispose()
    End Sub


    Private Sub OKCommand(ByVal sender As Object, ByVal e As System.EventArgs)

        bottomTexture = CInt(nUPbottomTexture.Value)
        roofTexture = CInt(nUProofTexture.Value)
        topTexture = CInt(nUPtopTexture.Value)
        windowTexture = CInt(nUPwindowTexture.Value)

        sizeX = nUPsizeX.Value
        sizeZ = nUPsizeZ.Value
        scale_gb = nUPscale.Value

        frmObjectsP.nUPsizeX.Value = CDec(sizeX)
        frmObjectsP.nUPsizeZ.Value = CDec(sizeZ)
        frmObjectsP.nUPscale.Value = CDec(scale_gb)
        If BuildingType = 256 Then frmObjectsP.optGbFlat.Checked = True
        If BuildingType = 257 Then frmObjectsP.optGbPeaked.Checked = True
        If BuildingType = 258 Then frmObjectsP.optGbRidge.Checked = True
        If BuildingType = 259 Then frmObjectsP.optGbSlant.Checked = True
        If BuildingType = 260 Then frmObjectsP.optGbPyramidal.Checked = True
        If BuildingType = 261 Then frmObjectsP.optGbMultiSided.Checked = True

        sizeBottomY = nUPsizeBottomY.Value
        sizeWindowY = nUPsizeWindowY.Value
        sizeTopY = nUPsizeTopY.Value
        sizeRoofY = nUPsizeRoofY.Value

        If BuildingType = 260 Then  ' pyramidal
            sizeTopX = nUPsizeTopX.Value
            sizeTopZ = nUPsizeTopZ.Value
        Else
            sizeTopX = sizeTopX_S
            sizeTopZ = sizeTopZ_S
        End If

        textureIndexBottomX = CInt(256 * nUPtextureIndexBottomX.Value)
        textureIndexBottomZ = CInt(256 * nUPtextureIndexBottomZ.Value)
        textureIndexWindowX = CInt(256 * nUPtextureIndexWindowX.Value)
        textureIndexWindowY = CInt(256 * nUPtextureIndexWindowY.Value)
        textureIndexWindowZ = CInt(256 * nUPtextureIndexWindowZ.Value)

        textureIndexTopX = CInt(256 * nUPtextureIndexTopZ.Value)
        textureIndexTopZ = CInt(256 * nUPtextureIndexTopZ.Value)

        textureIndexRoofX = CInt(256 * nUPtextureIndexRoofX.Value)
        textureIndexRoofY = CInt(256 * nUPtextureIndexRoofY.Value)
        textureIndexRoofZ = CInt(256 * nUPtextureIndexRoofZ.Value)

        gableTexture = CInt(nUPgableTexture.Value)
        textureIndexGableY = CInt(256 * nUPtextureIndexGableY.Value)
        textureIndexGableZ = CInt(256 * nUPtextureIndexGableZ.Value)
        faceTexture = CInt(nUPfaceTexture.Value)
        textureIndexFaceX = CInt(256 * nUPtextureIndexFaceX.Value)
        textureIndexFaceY = CInt(256 * nUPtextureIndexFaceY.Value)
        buildingSides = CInt(nUPbuildingSides.Value)
        smoothing = ckSmoothing.Checked

        Dispose()


    End Sub


    Private Sub FrmGBuilding_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        nUPsizeX.Value = CDec(sizeX)
        nUPsizeZ.Value = CDec(sizeZ)
        nUPscale.Value = CDec(scale_gb)

        nUPbottomTexture.Value = bottomTexture
        nUPwindowTexture.Value = windowTexture
        nUPtopTexture.Value = topTexture
        nUProofTexture.Value = roofTexture

        nUPsizeBottomY.Value = CDec(sizeBottomY)
        nUPsizeWindowY.Value = CDec(sizeWindowY)
        nUPsizeTopY.Value = CDec(sizeTopY)
        nUPsizeRoofY.Value = CDec(sizeRoofY)

        nUPsizeTopX.Value = CDec(sizeTopX)
        nUPsizeTopZ.Value = CDec(sizeTopZ)
        sizeTopX_S = sizeTopX
        sizeTopZ_S = sizeTopZ

        nUPtextureIndexBottomX.Value = CDec(textureIndexBottomX / 256)
        nUPtextureIndexBottomZ.Value = CDec(textureIndexBottomZ / 256)
        nUPtextureIndexWindowX.Value = CDec(textureIndexWindowX / 256)
        nUPtextureIndexWindowY.Value = CDec(textureIndexWindowY / 256)
        nUPtextureIndexWindowZ.Value = CDec(textureIndexWindowZ / 256)

        nUPtextureIndexTopZ.Value = CDec(textureIndexTopX / 256)
        nUPtextureIndexTopZ.Value = CDec(textureIndexTopZ / 256)

        nUPtextureIndexRoofX.Value = CDec(textureIndexRoofX / 256)
        nUPtextureIndexRoofY.Value = CDec(textureIndexRoofY / 256)
        nUPtextureIndexRoofZ.Value = CDec(textureIndexRoofZ / 256)

        nUPgableTexture.Value = gableTexture
        nUPtextureIndexGableY.Value = CDec(textureIndexGableY / 256)
        nUPtextureIndexGableZ.Value = CDec(textureIndexGableZ / 256)
        nUPfaceTexture.Value = faceTexture
        nUPtextureIndexFaceX.Value = CDec(textureIndexFaceX / 256)
        nUPtextureIndexFaceY.Value = CDec(textureIndexFaceY / 256)
        nUPbuildingSides.Value = buildingSides
        ckSmoothing.Checked = smoothing

        optGbFlat.Checked = False
        optGbPeaked.Checked = False
        optGbRidge.Checked = False
        optGbSlant.Checked = False
        optGbPyramidal.Checked = False
        optGbMultiSided.Checked = False

        If BuildingType = 256 Then optGbFlat.Checked = True
        If BuildingType = 257 Then optGbPeaked.Checked = True
        If BuildingType = 258 Then optGbRidge.Checked = True
        If BuildingType = 259 Then optGbSlant.Checked = True
        If BuildingType = 260 Then optGbPyramidal.Checked = True
        If BuildingType = 261 Then optGbMultiSided.Checked = True

        'SetBuildingType()  because false >>> true called it already

        LoadGraphics()

    End Sub

    Private sizeTopX_S As Single   '_S to store 
    Private sizeTopZ_S As Single

    Private Sub SetBuildingType()

        Dim A As String = ""
        If BuildingType = 256 Then A = "Rectangular - FLAT roof"
        If BuildingType = 257 Then A = "Rectangular - PEAKED roof"
        If BuildingType = 258 Then A = "Rectangular - RIDGE roof"
        If BuildingType = 259 Then A = "Rectangular - SLANT roof"
        If BuildingType = 260 Then A = "Pyramidal Building"
        If BuildingType = 261 Then A = "Multi-Sided Building"

        Text = "SBuilderX - Generic Buildings - " & A

        nUPsizeBottomY.Enabled = False
        nUPtextureIndexBottomX.Enabled = False
        nUPtextureIndexBottomZ.Enabled = False

        nUPsizeWindowY.Enabled = False
        nUPtextureIndexWindowX.Enabled = False
        nUPtextureIndexWindowY.Enabled = False
        nUPtextureIndexWindowZ.Enabled = False

        nUPsizeTopX.Enabled = False
        nUPsizeTopY.Enabled = False
        nUPsizeTopZ.Enabled = False
        lbTW.Enabled = False
        lbTD.Enabled = False
        nUPtextureIndexTopX.Enabled = False
        nUPtextureIndexTopZ.Enabled = False

        nUPsizeRoofY.Enabled = False
        nUPtextureIndexRoofX.Enabled = False
        nUPtextureIndexRoofY.Enabled = False
        nUPtextureIndexRoofZ.Enabled = False

        nUPgableTexture.Enabled = False
        nUPtextureIndexGableY.Enabled = False
        nUPtextureIndexGableZ.Enabled = False

        nUPfaceTexture.Enabled = False
        nUPtextureIndexFaceX.Enabled = False
        nUPtextureIndexFaceY.Enabled = False

        frMulti.Enabled = False
        nUPbuildingSides.Enabled = False
        ckSmoothing.Enabled = False

        nUPWX.Value = nUPsizeX.Value
        nUPWZ.Value = nUPsizeZ.Value
        nUPRX.Value = nUPsizeX.Value
        nUPRZ.Value = nUPsizeZ.Value

        lbBZT.Enabled = False
        lbWZT.Enabled = False
        lbTZT.Enabled = False

        lbRYT.Enabled = False

        lbF.Enabled = False
        lbFXT.Enabled = False
        lbFYT.Enabled = False
        lbG.Enabled = False
        lbGYT.Enabled = False
        lbGZT.Enabled = False

        lbRH.Enabled = True
        If BuildingType = 256 Then lbRH.Enabled = False

        If BuildingType = 256 Or BuildingType = 257 _
                   Or BuildingType = 258 Or BuildingType = 259 Then ' Rect flat
            nUPsizeBottomY.Enabled = True
            nUPtextureIndexBottomX.Enabled = True
            nUPtextureIndexBottomZ.Enabled = True
            nUPsizeWindowY.Enabled = True
            nUPtextureIndexWindowX.Enabled = True
            nUPtextureIndexWindowY.Enabled = True
            nUPtextureIndexWindowZ.Enabled = True
            nUPsizeTopY.Enabled = True
            nUPtextureIndexTopX.Enabled = True
            nUPtextureIndexTopZ.Enabled = True
            nUPtextureIndexRoofX.Enabled = True
            nUPtextureIndexRoofZ.Enabled = True
            lbBZT.Enabled = True
            lbWZT.Enabled = True
            lbTZT.Enabled = True
        End If


        If BuildingType = 257 Then  ' Rect peaked
            nUPsizeRoofY.Enabled = True
            nUPtextureIndexRoofY.Enabled = True
            lbRYT.Enabled = True
        End If

        If BuildingType = 258 Then  ' Rect Ridge
            nUPsizeRoofY.Enabled = True
            nUPgableTexture.Enabled = True
            nUPtextureIndexGableY.Enabled = True
            nUPtextureIndexGableZ.Enabled = True
            lbG.Enabled = True
            lbGYT.Enabled = True
            lbGZT.Enabled = True
        End If

        If BuildingType = 259 Then  ' Rect slant
            nUPsizeRoofY.Enabled = True
            nUPgableTexture.Enabled = True
            nUPtextureIndexGableY.Enabled = True
            nUPtextureIndexGableZ.Enabled = True
            nUPfaceTexture.Enabled = True
            nUPtextureIndexFaceX.Enabled = True
            nUPtextureIndexFaceY.Enabled = True
            lbF.Enabled = True
            lbFXT.Enabled = True
            lbFYT.Enabled = True
            lbG.Enabled = True
            lbGYT.Enabled = True
            lbGZT.Enabled = True
        End If

        If BuildingType = 260 Then  ' pyramidal
            nUPsizeTopX.Enabled = True
            nUPsizeTopZ.Enabled = True
            lbTW.Enabled = True
            lbTD.Enabled = True
            nUPsizeBottomY.Enabled = True
            nUPtextureIndexBottomX.Enabled = True
            nUPtextureIndexBottomZ.Enabled = True
            nUPsizeWindowY.Enabled = True
            nUPtextureIndexWindowX.Enabled = True
            nUPtextureIndexWindowY.Enabled = True
            nUPtextureIndexWindowZ.Enabled = True
            nUPsizeTopY.Enabled = True
            nUPtextureIndexTopX.Enabled = True
            nUPtextureIndexTopZ.Enabled = True
            nUPtextureIndexRoofX.Enabled = True
            nUPtextureIndexRoofZ.Enabled = True
            lbBZT.Enabled = True
            lbWZT.Enabled = True
            lbTZT.Enabled = True
            lbRH.Enabled = False
        End If

        If BuildingType = 261 Then ' multi sided
            frMulti.Enabled = True
            nUPbuildingSides.Enabled = True
            ckSmoothing.Enabled = True
            nUPsizeBottomY.Enabled = True
            nUPtextureIndexBottomX.Enabled = True
            nUPsizeWindowY.Enabled = True
            nUPtextureIndexWindowX.Enabled = True
            nUPtextureIndexWindowY.Enabled = True
            nUPsizeTopY.Enabled = True
            nUPtextureIndexTopX.Enabled = True
            nUPsizeRoofY.Enabled = True
            nUPtextureIndexRoofX.Enabled = True
            nUPtextureIndexRoofY.Enabled = True
            nUPtextureIndexRoofZ.Enabled = True
            lbRYT.Enabled = True
        End If

        If BuildingType = 260 Then  ' pyramidal
            nUPsizeTopX.Value = CDec(sizeTopX_S)
            nUPsizeTopZ.Value = CDec(sizeTopZ_S)
            sizTopX = sizeTopX_S
            sizTopZ = sizeTopZ_S
        Else
            nUPsizeTopX.Value = nUPsizeX.Value
            nUPsizeTopZ.Value = nUPsizeZ.Value
            sizTopX = nUPsizeX.Value
            sizTopZ = nUPsizeZ.Value
        End If

        If IsInit Then Exit Sub
        ResetDevice()

    End Sub

    ' Our global variables for this project
    Private renderDevice As Device = Nothing ' Our rendering device
    Private vertexBuffer As VertexBuffer = Nothing
    Private vertexBuffer0 As VertexBuffer = Nothing

    Private Structure VertexPT
        Public Position As Vector3
        Public Texture As Vector2
    End Structure

    Private Structure VertexPC
        Public Position As Vector3
        Public Color As Integer
    End Structure

    Private pause As Boolean = False
    Private fntOut As Direct3D9.Font
    Private sDevInfo As String

    Private modelMatrix As Matrix
    Private modelScale As Single = 1
    Private modelAngleX As Single = 0
    Private modelAngleY As Single = 0
    Private modelPanX As Single = 0
    Private modelPanY As Single = 0

    Private IsInit As Boolean = True

    Private Sub LoadGraphics()

        SetEvents()

        If Not InitializeGraphics() Then ' Initialize Direct3D
            MessageBox.Show("Could not initialize slimDX.Direct3D!")
            Return
        End If
        Show()

        Dim s As New Object
        Dim e As New System.EventArgs
        RebuildBuilding(s, e)

        IsInit = False

        While Created
            Render()
            Application.DoEvents()
        End While

    End Sub

    Private Function InitializeGraphics() As Boolean

        Dim d3D As New Direct3D
        Dim presentParams As New PresentParameters

        Try
            renderDevice = New Device(d3D, 0, DeviceType.Hardware, imgGenB.Handle, CreateFlags.HardwareVertexProcessing, presentParams)
            ResetDevice()
            LoadTextures()
            SetupMatrices()
            fntOut = New Font(renderDevice, New Drawing.Font("Arial", 10, FontStyle.Regular))
            'sDevInfo = Manager.Adapters(0).Information.Description

            sDevInfo = "UNKNOWN ADAPTER"

            pause = False
            Return True
        Catch e As Direct3D9Exception
            Return False
        End Try

    End Function 'InitializeGraphics

    Private Sub ResetDevice()

        CreateVertexBuffer()

        renderDevice.SetRenderState(RenderState.CullMode, False)
        renderDevice.SetRenderState(RenderState.Lighting, False)
        renderDevice.SetRenderState(RenderState.ZEnable, True)

    End Sub
    Private Sub SetEvents()

        AddHandler nUPsizeX.ValueChanged, AddressOf RebuildBuilding
        AddHandler nUPsizeZ.ValueChanged, AddressOf RebuildBuilding

        AddHandler nUPsizeBottomY.ValueChanged, AddressOf RebuildBuilding
        AddHandler nUPsizeWindowY.ValueChanged, AddressOf RebuildBuilding
        AddHandler nUPsizeTopY.ValueChanged, AddressOf RebuildBuilding
        AddHandler nUPsizeRoofY.ValueChanged, AddressOf RebuildBuilding

        AddHandler nUPtextureIndexBottomX.ValueChanged, AddressOf RebuildBuilding
        AddHandler nUPtextureIndexBottomZ.ValueChanged, AddressOf RebuildBuilding

        AddHandler nUPtextureIndexWindowX.ValueChanged, AddressOf RebuildBuilding
        AddHandler nUPtextureIndexWindowY.ValueChanged, AddressOf RebuildBuilding
        AddHandler nUPtextureIndexWindowZ.ValueChanged, AddressOf RebuildBuilding

        AddHandler nUPtextureIndexTopX.ValueChanged, AddressOf RebuildBuilding
        AddHandler nUPtextureIndexTopZ.ValueChanged, AddressOf RebuildBuilding

        AddHandler nUPtextureIndexRoofX.ValueChanged, AddressOf RebuildBuilding
        AddHandler nUPtextureIndexRoofY.ValueChanged, AddressOf RebuildBuilding
        AddHandler nUPtextureIndexRoofZ.ValueChanged, AddressOf RebuildBuilding

        AddHandler nUPtextureIndexGableY.ValueChanged, AddressOf RebuildBuilding
        AddHandler nUPtextureIndexGableZ.ValueChanged, AddressOf RebuildBuilding

        AddHandler nUPtextureIndexFaceX.ValueChanged, AddressOf RebuildBuilding
        AddHandler nUPtextureIndexFaceY.ValueChanged, AddressOf RebuildBuilding

        AddHandler nUPbuildingSides.ValueChanged, AddressOf RebuildBuilding

        AddHandler nUPGrid.ValueChanged, AddressOf RebuildBuilding

        AddHandler nUPbottomTexture.ValueChanged, AddressOf LoadBottomTexture
        AddHandler nUPwindowTexture.ValueChanged, AddressOf LoadWindowTexture
        AddHandler nUPtopTexture.ValueChanged, AddressOf LoadTopTexture
        AddHandler nUProofTexture.ValueChanged, AddressOf LoadRoofTexture
        AddHandler nUPgableTexture.ValueChanged, AddressOf LoadGableTexture
        AddHandler nUPfaceTexture.ValueChanged, AddressOf LoadFaceTexture

        AddHandler cmdCancel.Click, AddressOf CancelCommand
        AddHandler cmdOK.Click, AddressOf OKCommand


    End Sub

    ' these are local
    Private texIndexBottomX As Single
    Private texIndexBottomZ As Single

    Private texIndexWindowX As Single
    Private texIndexWindowZ As Single
    Private texIndexWindowY As Single

    Private texIndexTopX As Single
    Private texIndexTopZ As Single

    Private texIndexRoofX As Single
    Private texIndexRoofY As Single
    Private texIndexRoofZ As Single

    Private texIndexGableY As Single
    Private texIndexGableZ As Single

    Private texIndexFaceX As Single
    Private texIndexFaceY As Single

    Private sizX As Single
    Private sizZ As Single
    Private sizTopX As Single
    Private sizTopZ As Single

    Private sizBottomY As Single
    Private sizWindowY As Single
    Private sizTopY As Single
    Private sizRoofY As Single

    Private sides As Integer

    Private Sub RebuildBuilding(ByVal sender As Object, ByVal e As System.EventArgs)

        Grid = nUPGrid.Value
        sizX = nUPsizeX.Value
        sizZ = nUPsizeZ.Value

        If BuildingType = 260 Then  ' pyramidal
            sizTopX = nUPsizeTopX.Value
            sizTopZ = nUPsizeTopZ.Value
        Else
            sizTopX = nUPsizeX.Value
            sizTopZ = nUPsizeZ.Value
        End If

        nUPWX.Value = nUPsizeX.Value
        nUPWZ.Value = nUPsizeZ.Value
        nUPRX.Value = nUPsizeX.Value
        nUPRZ.Value = nUPsizeZ.Value

        sizBottomY = nUPsizeBottomY.Value
        sizWindowY = nUPsizeWindowY.Value
        sizTopY = nUPsizeTopY.Value
        sizRoofY = nUPsizeRoofY.Value

        texIndexBottomX = nUPtextureIndexBottomX.Value
        texIndexBottomZ = nUPtextureIndexBottomZ.Value

        texIndexWindowX = nUPtextureIndexWindowX.Value
        texIndexWindowY = nUPtextureIndexWindowY.Value
        texIndexWindowZ = nUPtextureIndexWindowZ.Value

        texIndexTopX = nUPtextureIndexTopX.Value
        texIndexTopZ = nUPtextureIndexTopZ.Value

        texIndexRoofX = nUPtextureIndexRoofX.Value
        texIndexRoofY = nUPtextureIndexRoofY.Value
        texIndexRoofZ = nUPtextureIndexRoofZ.Value

        texIndexGableY = nUPtextureIndexGableY.Value
        texIndexGableZ = nUPtextureIndexGableZ.Value

        texIndexFaceX = nUPtextureIndexFaceX.Value
        texIndexFaceY = nUPtextureIndexFaceY.Value

        sides = CInt(nUPbuildingSides.Value)

        ResetDevice()

    End Sub

    Private Sub CreateVertexBuffer()

        Dim N As Integer

        ' never we have 300 vertices; they are i at the end
        Dim v(300) As VertexPT

        Dim i As Integer = 0
        Dim sX0, sX1, y0, y1, sZ0, sZ1, tX, tY, tZ, gY, gZ, fX, fY As Single
        Dim t0 As Single = 0, t1 As Single = 1
        Dim tb As Single = 0, tw As Single = 0

        If nUPbottomTexture.Value > 85 Then tb = 0.5!
        If nUPwindowTexture.Value > 84 Then tw = 0.5!

        Dim rX, rZ As Single

        y0 = -(sizBottomY + sizWindowY + sizTopY) / 2.0F

        If BuildingType = 261 Then  ' multisided
            rX = sizX / 2
            rZ = sizZ / 2
            Dim pi1 As Single = CSng(PI / sides)
            Dim pi2 As Single = pi1 * 2
            ' bottom
            y1 = y0 + sizBottomY
            tX = texIndexBottomX
            If sizBottomY > 0 Then
                For N = 1 To sides
                    sX0 = CSng(rX * Math.Cos((N - 1) * pi2 - pi1))
                    sX1 = CSng(rX * Math.Cos(N * pi2 - pi1))
                    sZ0 = CSng(rZ * Math.Sin((N - 1) * pi2 - pi1))
                    sZ1 = CSng(rZ * Math.Sin(N * pi2 - pi1))
                    v(i + 0) = VPT(sX0, y0, sZ0, t0, t0)
                    v(i + 1) = VPT(sX0, y1, sZ0, t0, t1)
                    v(i + 2) = VPT(sX1, y1, sZ1, tX, t1)
                    v(i + 3) = VPT(sX1, y0, sZ1, tX, t0)
                    i = i + 4
                Next
            End If
            y0 = y1
            y1 = y0 + sizWindowY
            tX = texIndexWindowX
            tY = texIndexWindowY
            If sizWindowY > 0 Then
                For N = 1 To sides
                    sX0 = CSng(rX * Math.Cos((N - 1) * pi2 - pi1))
                    sX1 = CSng(rX * Math.Cos(N * pi2 - pi1))
                    sZ0 = CSng(rZ * Math.Sin((N - 1) * pi2 - pi1))
                    sZ1 = CSng(rZ * Math.Sin(N * pi2 - pi1))
                    v(i + 0) = VPT(sX0, y0, sZ0, t0, t0)
                    v(i + 1) = VPT(sX0, y1, sZ0, t0, tY)
                    v(i + 2) = VPT(sX1, y1, sZ1, tX, tY)
                    v(i + 3) = VPT(sX1, y0, sZ1, tX, t0)
                    i = i + 4
                Next
            End If
            y0 = y1
            y1 = y0 + sizTopY
            tX = texIndexTopX
            If sizTopY > 0 Then
                For N = 1 To sides
                    sX0 = CSng(rX * Math.Cos((N - 1) * pi2 - pi1))
                    sX1 = CSng(rX * Math.Cos(N * pi2 - pi1))
                    sZ0 = CSng(rZ * Math.Sin((N - 1) * pi2 - pi1))
                    sZ1 = CSng(rZ * Math.Sin(N * pi2 - pi1))
                    v(i + 0) = VPT(sX0, y0, sZ0, t0, t0)
                    v(i + 1) = VPT(sX0, y1, sZ0, t0, t1)
                    v(i + 2) = VPT(sX1, y1, sZ1, tX, t1)
                    v(i + 3) = VPT(sX1, y0, sZ1, tX, t0)
                    i = i + 4
                Next
            End If

            ' roof of multised
            y0 = y1
            y1 = y0 + sizRoofY
            tX = texIndexRoofX
            tZ = texIndexRoofZ
            For N = 1 To sides
                sX0 = CSng(rX * Math.Cos((N - 1) * pi2 - pi1))
                sX1 = CSng(rX * Math.Cos(N * pi2 - pi1))
                sZ0 = CSng(rZ * Math.Sin((N - 1) * pi2 - pi1))
                sZ1 = CSng(rZ * Math.Sin(N * pi2 - pi1))
                v(i + 0) = VPT(sX0, y0, sZ0, t0, t0)
                v(i + 1) = VPT(0, y1, 0, tX / 2, tZ)
                v(i + 2) = VPT(sX1, y0, sZ1, tX, t0)
                i = i + 3
            Next


        Else  ' non multisided

            sX0 = sizX / 2
            sX1 = sizX / 2
            sZ0 = sizZ / 2
            sZ1 = sizZ / 2

            ' bottom
            y1 = y0 + sizBottomY
            tX = texIndexBottomX
            tZ = texIndexBottomZ
            If sizBottomY > 0 Then
                v(i + 0) = VPT(-sX0, y0, -sZ0, t0, t0)  ' frontX
                v(i + 1) = VPT(-sX1, y1, -sZ1, t0, t1)
                v(i + 2) = VPT(+sX1, y1, -sZ1, tX, t1)
                v(i + 3) = VPT(+sX0, y0, -sZ0, tX, t0)
                i = i + 4
                v(i + 0) = VPT(+sX0, y0, -sZ0, tb, t0)  ' rightZ
                v(i + 1) = VPT(+sX1, y1, -sZ1, tb, t1)
                v(i + 2) = VPT(+sX1, y1, +sZ1, tZ, t1)
                v(i + 3) = VPT(+sX0, y0, +sZ0, tZ, t0)
                i = i + 4
                v(i + 0) = VPT(+sX0, y0, +sZ0, t0, t0)  ' backX
                v(i + 1) = VPT(+sX1, y1, +sZ1, t0, t1)
                v(i + 2) = VPT(-sX1, y1, +sZ1, tX, t1)
                v(i + 3) = VPT(-sX0, y0, +sZ0, tX, t0)
                i = i + 4
                v(i + 0) = VPT(-sX0, y0, +sZ0, tb, t0)  ' leftZ
                v(i + 1) = VPT(-sX1, y1, +sZ1, tb, t1)
                v(i + 2) = VPT(-sX1, y1, -sZ1, tZ, t1)
                v(i + 3) = VPT(-sX0, y0, -sZ0, tZ, t0)
                i = i + 4
            End If

            ' window
            y0 = y1
            y1 = y0 + sizWindowY
            tX = texIndexWindowX
            tZ = texIndexWindowZ
            tY = texIndexWindowY
            If sizWindowY > 0 Then
                v(i + 0) = VPT(-sX0, y0, -sZ0, tw, t0)  ' frontX
                v(i + 1) = VPT(-sX1, y1, -sZ1, tw, tY)
                v(i + 2) = VPT(+sX1, y1, -sZ1, tX, tY)
                v(i + 3) = VPT(+sX0, y0, -sZ0, tX, t0)
                i = i + 4
                v(i + 0) = VPT(+sX0, y0, -sZ0, t0, t0)
                v(i + 1) = VPT(+sX1, y1, -sZ1, t0, tY)
                v(i + 2) = VPT(+sX1, y1, +sZ1, tZ, tY)
                v(i + 3) = VPT(+sX0, y0, +sZ0, tZ, t0)
                i = i + 4
                v(i + 0) = VPT(+sX0, y0, +sZ0, t0, t0)
                v(i + 1) = VPT(+sX1, y1, +sZ1, t0, tY)
                v(i + 2) = VPT(-sX1, y1, +sZ1, tX, tY)
                v(i + 3) = VPT(-sX0, y0, +sZ0, tX, t0)
                i = i + 4
                v(i + 0) = VPT(-sX0, y0, +sZ0, t0, t0)
                v(i + 1) = VPT(-sX1, y1, +sZ1, t0, tY)
                v(i + 2) = VPT(-sX1, y1, -sZ1, tZ, tY)
                v(i + 3) = VPT(-sX0, y0, -sZ0, tZ, t0)
                i = i + 4
            End If

            'top
            'sX0 = sizX / 2
            sX1 = sizTopX / 2
            'sZ0 = sizZ / 2
            sZ1 = sizTopZ / 2
            y0 = y1
            y1 = y0 + sizTopY
            tX = texIndexTopX
            tZ = texIndexTopZ
            If sizTopY > 0 Then
                v(i + 0) = VPT(-sX0, y0, -sZ0, t0, t0)
                v(i + 1) = VPT(-sX1, y1, -sZ1, t0, t1)
                v(i + 2) = VPT(+sX1, y1, -sZ1, tX, t1)
                v(i + 3) = VPT(+sX0, y0, -sZ0, tX, t0)
                i = i + 4
                v(i + 0) = VPT(+sX0, y0, -sZ0, t0, t0)
                v(i + 1) = VPT(+sX1, y1, -sZ1, t0, t1)
                v(i + 2) = VPT(+sX1, y1, +sZ1, tZ, t1)
                v(i + 3) = VPT(+sX0, y0, +sZ0, tZ, t0)
                i = i + 4
                v(i + 0) = VPT(+sX0, y0, +sZ0, t0, t0)
                v(i + 1) = VPT(+sX1, y1, +sZ1, t0, t1)
                v(i + 2) = VPT(-sX1, y1, +sZ1, tX, t1)
                v(i + 3) = VPT(-sX0, y0, +sZ0, tX, t0)
                i = i + 4
                v(i + 0) = VPT(-sX0, y0, +sZ0, t0, t0)
                v(i + 1) = VPT(-sX1, y1, +sZ1, t0, t1)
                v(i + 2) = VPT(-sX1, y1, -sZ1, tZ, t1)
                v(i + 3) = VPT(-sX0, y0, -sZ0, tZ, t0)
                i = i + 4
            End If

            If BuildingType = 256 Or BuildingType = 260 Then   ' flat or pyramid
                sX0 = sizTopX / 2
                sZ0 = sizTopZ / 2
                y0 = y1
                'y1 = y0 + sizRoofY
                tX = texIndexRoofX
                tZ = texIndexRoofZ
                v(i + 0) = VPT(-sX0, y0, +sZ0, t0, tZ)
                v(i + 1) = VPT(+sX0, y0, +sZ0, tX, tZ)
                v(i + 2) = VPT(+sX0, y0, -sZ0, tX, t0)
                v(i + 3) = VPT(-sX0, y0, -sZ0, t0, t0)
                i = i + 4
            End If

            If BuildingType = 257 Then   ' peaked
                sX0 = sizTopX / 2
                sZ0 = sizTopZ / 2
                y0 = y1
                y1 = y0 + sizRoofY
                tX = texIndexRoofX
                tZ = texIndexRoofZ
                tY = texIndexRoofY
                v(i + 0) = VPT(-sX0, y0, -sZ0, t0, t0)
                v(i + 1) = VPT(0, y1, 0, tX / 2, tY)
                v(i + 2) = VPT(+sX0, y0, -sZ0, tX, t0)
                i = i + 3
                v(i + 0) = VPT(+sX0, y0, -sZ0, t0, t0)
                v(i + 1) = VPT(0, y1, 0, tZ / 2, tY)
                v(i + 2) = VPT(+sX0, y0, +sZ0, tZ, t0)
                i = i + 3
                v(i + 0) = VPT(+sX0, y0, +sZ0, t0, t0)
                v(i + 1) = VPT(0, y1, 0, tX / 2, tY)
                v(i + 2) = VPT(-sX0, y0, +sZ0, tX, t0)
                i = i + 3
                v(i + 0) = VPT(-sX0, y0, +sZ0, t0, t0)
                v(i + 1) = VPT(0, y1, 0, tZ / 2, tY)
                v(i + 2) = VPT(-sX0, y0, -sZ0, tZ, t0)
                i = i + 3
            End If

            If BuildingType = 258 Then   ' ridge
                sX0 = sizTopX / 2
                sZ0 = sizTopZ / 2
                y0 = y1
                y1 = y0 + sizRoofY
                tX = texIndexRoofX
                tZ = texIndexRoofZ
                gY = texIndexGableY
                gZ = texIndexGableZ
                v(i + 0) = VPT(-sX0, y0, -sZ0, t0, t0)
                v(i + 1) = VPT(-sX0, y1, 0, t0, tZ)
                v(i + 2) = VPT(+sX0, y1, 0, tX, tZ)
                v(i + 3) = VPT(+sX0, y0, -sZ0, tX, t0)
                i = i + 4
                v(i + 0) = VPT(+sX0, y0, +sZ0, t0, t0)
                v(i + 1) = VPT(+sX0, y1, 0, t0, tZ)
                v(i + 2) = VPT(-sX0, y1, 0, tX, tZ)
                v(i + 3) = VPT(-sX0, y0, +sZ0, tX, t0)
                i = i + 4
                v(i + 0) = VPT(+sX0, y0, -sZ0, t0, t0)
                v(i + 1) = VPT(+sX0, y1, 0, gZ / 2, gY)
                v(i + 2) = VPT(+sX0, y0, +sZ0, gZ, t0)
                i = i + 3
                v(i + 0) = VPT(-sX0, y0, +sZ0, t0, t0)
                v(i + 1) = VPT(-sX0, y1, 0, gZ / 2, gY)
                v(i + 2) = VPT(-sX0, y0, -sZ0, gZ, t0)
                i = i + 3
            End If

            If BuildingType = 259 Then   ' slant
                sX0 = sizTopX / 2
                sZ0 = sizTopZ / 2
                y0 = y1
                y1 = y0 + sizRoofY
                tX = texIndexRoofX
                tZ = texIndexRoofZ
                gY = texIndexGableY
                gZ = texIndexGableZ
                fX = texIndexFaceX
                fY = texIndexFaceY
                v(i + 0) = VPT(-sX0, y0, -sZ0, t0, t0)
                v(i + 1) = VPT(-sX0, y1, -sZ0, t0, fY)
                v(i + 2) = VPT(+sX0, y1, -sZ0, fX, fY)
                v(i + 3) = VPT(+sX0, y0, -sZ0, fX, t0)
                i = i + 4
                v(i + 0) = VPT(+sX0, y0, +sZ0, t0, t0)
                v(i + 1) = VPT(+sX0, y1, -sZ0, t0, tZ)
                v(i + 2) = VPT(-sX0, y1, -sZ0, tX, tZ)
                v(i + 3) = VPT(-sX0, y0, +sZ0, tX, t0)
                i = i + 4
                v(i + 0) = VPT(+sX0, y0, -sZ0, t0, t0)
                v(i + 1) = VPT(+sX0, y1, -sZ0, t0, gY)
                v(i + 2) = VPT(+sX0, y0, +sZ0, gZ, t0)
                i = i + 3
                v(i + 0) = VPT(-sX0, y0, +sZ0, t0, t0)
                v(i + 1) = VPT(-sX0, y1, -sZ0, gZ, gY)
                v(i + 2) = VPT(-sX0, y0, -sZ0, gZ, t0)
                i = i + 3
            End If

        End If


        'base
        sX0 = CSng(sizX / 2)
        sZ0 = CSng(sizZ / 2)
        y0 = -(sizBottomY + sizWindowY + sizTopY) / 2.0F
        v(i + 0) = VPT(-sX0, y0, +sZ0, t0, t0)    'seen from underneath
        v(i + 1) = VPT(-sX0, y0, -sZ0, t0, t1)
        v(i + 2) = VPT(+sX0, y0, -sZ0, t1, t1)
        v(i + 3) = VPT(+sX0, y0, +sZ0, t1, t0)
        i = i + 4

        ReDim Preserve v(i - 1)

        Dim sizeVector As Integer = System.Runtime.InteropServices.Marshal.SizeOf(GetType(VertexPT))
        ' MsgBox(i.ToString & "  " & sizeVector.ToString) ' size vector should be 20 (in bytes)

        vertexBuffer = New VertexBuffer(renderDevice, sizeVector * i, Usage.WriteOnly, VertexFormat.Position Or VertexFormat.Texture2, Pool.Managed)
        Dim stream As DataStream = vertexBuffer.Lock(0, 0, LockFlags.None)
        stream.WriteRange(v)
        vertexBuffer.Unlock()

        Dim C As Color = Color.White

        ' this time there will be 43 vertices, no need to ReDim!
        Dim v0(43) As VertexPC

        i = -1
        sX0 = -6 * Grid : sZ0 = -5 * Grid : sZ1 = 5 * Grid
        For N = 1 To 11
            i += 1 : sX0 += Grid
            v0(i) = VPC(sX0, y0, sZ0, C)
            i += 1
            v0(i) = VPC(sX0, y0, sZ1, C)
        Next
        sX0 = -5 * Grid : sX1 = 5 * Grid : sZ0 = -6 * Grid
        For N = 1 To 11
            i += 1 : sZ0 += Grid
            v0(i) = VPC(sX0, y0, sZ0, C)
            i += 1
            v0(i) = VPC(sX1, y0, sZ0, C)
        Next

        'ReDim Preserve v0(i - 1)

        Dim red As Integer = Color.Red.ToArgb
        v0(10).Color = red
        v0(11).Color = red
        Dim green As Integer = Color.Green.ToArgb
        v0(32).Color = green
        v0(33).Color = green

        sizeVector = System.Runtime.InteropServices.Marshal.SizeOf(GetType(VertexPC))
        'MsgBox(i.ToString & "  " & sizeVector.ToString)
        vertexBuffer0 = New VertexBuffer(renderDevice, sizeVector * 44, Usage.WriteOnly, VertexFormat.Position Or VertexFormat.Diffuse, Pool.Managed)
        Dim stream0 As DataStream = vertexBuffer0.Lock(0, 0, LockFlags.None)
        stream0.WriteRange(v0)
        vertexBuffer0.Unlock()

    End Sub 'OnCreateVertexBuffer

    Private Function VPT(ByVal x1 As Single, ByVal x2 As Single, ByVal x3 As Single, ByVal x4 As Single, ByVal x5 As Single) As VertexPT

        VPT.Position = New Vector3(x1, x2, x3)
        VPT.Texture = New Vector2(x4, x5)

    End Function

    Private Function VPC(ByVal x1 As Single, ByVal x2 As Single, ByVal x3 As Single, ByVal x4 As Color) As VertexPC

        VPC.Position = New Vector3(x1, x2, x3)
        VPC.Color = x4.ToArgb

    End Function

    Private Sub Render()

        Dim N As Integer

        If pause Then
            Return
        End If

        renderDevice.Clear(ClearFlags.Target Or ClearFlags.ZBuffer, Drawing.Color.SkyBlue, 1.0F, 0)
        renderDevice.BeginScene()

        modelMatrix = Matrix.Scaling(modelScale, modelScale, modelScale)
        modelMatrix = Matrix.Multiply(modelMatrix, Matrix.RotationX(CSng(modelAngleX * (Math.PI / 180))))
        modelMatrix = Matrix.Multiply(modelMatrix, Matrix.RotationY(CSng(modelAngleY * (Math.PI / 180))))
        modelMatrix = Matrix.Multiply(modelMatrix, Matrix.Translation(modelPanX, modelPanY, 0))

        renderDevice.SetTransform(TransformState.World, modelMatrix)
        renderDevice.SetStreamSource(0, vertexBuffer, 0, 20)
        renderDevice.VertexFormat = VertexFormat.Position Or VertexFormat.Texture2

        Dim i As Integer = 0

        If BuildingType = 261 Then  ' multisided
            If sizBottomY > 0 Then
                renderDevice.SetTexture(0, textureB)
                For N = 1 To sides
                    renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 0, 2)
                    i = i + 4
                Next
            End If
            If sizWindowY > 0 Then
                renderDevice.SetTexture(0, textureW)
                For N = 1 To sides
                    renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 0, 2)
                    i = i + 4
                Next
            End If
            If sizTopY > 0 Then
                renderDevice.SetTexture(0, textureT)
                For N = 1 To sides
                    renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 0, 2)
                    i = i + 4
                Next
            End If

            renderDevice.SetTexture(0, textureR)
            For N = 1 To sides
                renderDevice.DrawPrimitives(PrimitiveType.TriangleList, i + 0, 1)
                i = i + 3
            Next


        Else  ' non multisided

            If sizBottomY > 0 Then
                renderDevice.SetTexture(0, textureB)
                renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 0, 2)
                renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 4, 2)
                renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 8, 2)
                renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 12, 2)
                i = i + 16
            End If

            If sizWindowY > 0 Then
                renderDevice.SetTexture(0, textureW)
                renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 0, 2)
                renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 4, 2)
                renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 8, 2)
                renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 12, 2)
                i = i + 16
            End If

            If sizTopY > 0 Then
                renderDevice.SetTexture(0, textureT)
                renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 0, 2)
                renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 4, 2)
                renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 8, 2)
                renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 12, 2)
                i = i + 16
            End If

            If BuildingType = 256 Or BuildingType = 260 Then
                ' flat roof
                renderDevice.SetTexture(0, textureR)
                renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 0, 2)
                i = i + 4
            End If

            If BuildingType = 257 Then
                ' peak roof
                renderDevice.SetTexture(0, textureR)
                renderDevice.DrawPrimitives(PrimitiveType.TriangleList, i + 0, 4)
                i = i + 12
            End If
            If BuildingType = 258 Then
                ' ridg roof
                renderDevice.SetTexture(0, textureR)
                renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 0, 2)
                i = i + 4
                renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 0, 2)
                i = i + 4
                renderDevice.SetTexture(0, textureG)
                renderDevice.DrawPrimitives(PrimitiveType.TriangleList, i + 0, 1)
                i = i + 3
                renderDevice.DrawPrimitives(PrimitiveType.TriangleList, i + 0, 1)
                i = i + 3
            End If

            If BuildingType = 259 Then
                ' slant roof
                renderDevice.SetTexture(0, textureF)
                renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 0, 2)
                i = i + 4
                renderDevice.SetTexture(0, textureR)
                renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 0, 2)
                i = i + 4
                renderDevice.SetTexture(0, textureG)
                renderDevice.DrawPrimitives(PrimitiveType.TriangleList, i + 0, 1)
                i = i + 3
                renderDevice.DrawPrimitives(PrimitiveType.TriangleList, i + 0, 1)
                i = i + 3
            End If
        End If

        'base
        renderDevice.SetTexture(0, texture0)
        renderDevice.DrawPrimitives(PrimitiveType.TriangleFan, i + 0, 2)
        i = i + 4

        ' size of VectorPC is 16
        renderDevice.SetStreamSource(0, vertexBuffer0, 0, 16)
        renderDevice.VertexFormat = VertexFormat.Position Or VertexFormat.Diffuse
        renderDevice.SetTexture(0, Nothing)
        renderDevice.SetTextureStageState(0, TextureStage.ColorOperation, 4)

        renderDevice.DrawPrimitives(PrimitiveType.LineList, 0, 22)

        'fntOut.DrawString(Nothing, sDevInfo, 5, 5, Color.DarkBlue)
        If FullScreen Then
            fntOut.DrawString(Nothing, helpInfo, 5, 5, Color.DarkBlue)
        End If

        renderDevice.EndScene()
        renderDevice.Present()

    End Sub 'Render



    Private Sub SetupMatrices()

        modelScale = sizeX
        If sizeZ > modelScale Then modelScale = sizeZ

        modelScale = 5.0F / modelScale
        renderDevice.SetTransform(TransformState.World, Matrix.Scaling(modelScale, modelScale, modelScale))
        renderDevice.SetTransform(TransformState.View, Matrix.LookAtLH(New Vector3(0, 0, -10), New Vector3(0, 0, 0), New Vector3(0, 1, 0)))
        renderDevice.SetTransform(TransformState.Projection, Matrix.PerspectiveFovLH(Math.PI / 4, 4 / 3, 1, 100))

    End Sub 'SetupMatrices


    Private texFolder As String = Application.StartupPath + "\Tools\GenB\"
    Private textureB As Texture = Nothing
    Private Sub LoadBottomTexture(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim textB As String = Trim(CStr(nUPbottomTexture.Value))
        Dim textBfile As String = texFolder & "bottom" & textB & ".jpg"
        Try
            textureB = Texture.FromFile(renderDevice, textBfile)
        Catch ex As Exception
            textureB = Texture.FromFile(renderDevice, texFolder & "bottom00.jpg")
        End Try
        RebuildBuilding(sender, e)
    End Sub
    Private textureW As Texture = Nothing
    Private Sub LoadWindowTexture(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim textW As String = Trim(CStr(nUPwindowTexture.Value))
        Dim textWfile As String = texFolder & "window" & textW & ".jpg"
        Try
            textureW = Texture.FromFile(renderDevice, textWfile)
        Catch ex As Exception
            textureW = Texture.FromFile(renderDevice, texFolder & "window00.jpg")
        End Try
        RebuildBuilding(sender, e)
    End Sub
    Private textureT As Texture = Nothing
    Private Sub LoadTopTexture(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim textT As String = Trim(CStr(nUPtopTexture.Value))
        Dim textTfile As String = texFolder & "top" & textT & ".jpg"
        Try
            textureT = Texture.FromFile(renderDevice, textTfile)
        Catch ex As Exception
            textureT = Texture.FromFile(renderDevice, texFolder & "top00.jpg")
        End Try
        RebuildBuilding(sender, e)
    End Sub
    Private textureR As Texture = Nothing
    Private Sub LoadRoofTexture(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim textR As String = Trim(CStr(nUProofTexture.Value))
        Dim textRfile As String = texFolder & "roof" & textR & ".jpg"
        Try
            textureR = Texture.FromFile(renderDevice, textRfile)
        Catch ex As Exception
            textureR = Texture.FromFile(renderDevice, texFolder & "roof00.jpg")
        End Try
        RebuildBuilding(sender, e)
    End Sub

    Private textureF As Texture = Nothing
    Private Sub LoadFaceTexture(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim textFfile As String
        Dim T As Integer = CInt(nUPfaceTexture.Value)
        If T >= 1000 Then
            textFfile = texFolder & "window" & Trim(CStr(T - 1000)) & ".jpg"
        Else
            textFfile = texFolder & "face" & Trim(CStr(T)) & ".jpg"
        End If

        Try
            textureF = Texture.FromFile(renderDevice, textFfile)
        Catch ex As Exception
            textureF = Texture.FromFile(renderDevice, texFolder & "face00.jpg")
        End Try
        RebuildBuilding(sender, e)

    End Sub

    Private textureG As Texture = Nothing
    Private Sub LoadGableTexture(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim textGfile As String
        Dim T As Integer = CInt(nUPgableTexture.Value)
        If T >= 1000 Then
            textGfile = texFolder & "window" & Trim(CStr(T - 1000)) & ".jpg"
        Else
            textGfile = texFolder & "gable" & Trim(CStr(T)) & ".jpg"
        End If

        Try
            textureG = Texture.FromFile(renderDevice, textGfile)
        Catch ex As Exception
            textureG = Texture.FromFile(renderDevice, texFolder & "gable00.jpg")
        End Try
        RebuildBuilding(sender, e)
    End Sub

    Private texture0 As Texture = Nothing
    Private Sub LoadTextures()

        texture0 = Texture.FromFile(renderDevice, texFolder & "base00.jpg")
        Dim textB As String = Trim(CStr(nUPbottomTexture.Value))
        Dim textBfile As String = texFolder & "bottom" & textB & ".jpg"
        Try
            textureB = Texture.FromFile(renderDevice, textBfile)
        Catch ex As Exception
            textureB = Texture.FromFile(renderDevice, texFolder & "bottom00.jpg")
        End Try
        Dim textW As String = Trim(CStr(nUPwindowTexture.Value))
        Dim textWfile As String = texFolder & "window" & textW & ".jpg"
        Try
            textureW = Texture.FromFile(renderDevice, textWfile)
        Catch ex As Exception
            textureW = Texture.FromFile(renderDevice, texFolder & "window00.jpg")
        End Try
        Dim textT As String = Trim(CStr(nUPtopTexture.Value))
        Dim textTfile As String = texFolder & "top" & textT & ".jpg"
        Try
            textureT = Texture.FromFile(renderDevice, textTfile)
        Catch ex As Exception
            textureT = Texture.FromFile(renderDevice, texFolder & "top00.jpg")
        End Try
        Dim textR As String = Trim(CStr(nUProofTexture.Value))
        Dim textRfile As String = texFolder & "roof" & textR & ".jpg"
        Try
            textureR = Texture.FromFile(renderDevice, textRfile)
        Catch ex As Exception
            textureR = Texture.FromFile(renderDevice, texFolder & "roof00.jpg")
        End Try


        Dim textFfile As String
        Dim T As Integer = CInt(nUPfaceTexture.Value)
        If T >= 1000 Then
            textFfile = texFolder & "window" & Trim(CStr(T - 1000)) & ".jpg"
        Else
            textFfile = texFolder & "face" & Trim(CStr(T)) & ".jpg"
        End If

        Try
            textureF = Texture.FromFile(renderDevice, textFfile)
        Catch ex As Exception
            textureF = Texture.FromFile(renderDevice, texFolder & "face00.jpg")
        End Try

        Dim textGfile As String
        T = CInt(nUPgableTexture.Value)
        If T >= 1000 Then
            textGfile = texFolder & "window" & Trim(CStr(T - 1000)) & ".jpg"
        Else
            textGfile = texFolder & "gable" & Trim(CStr(T)) & ".jpg"
        End If

        Try
            textureG = Texture.FromFile(renderDevice, textGfile)
        Catch ex As Exception
            textureG = Texture.FromFile(renderDevice, texFolder & "gable00.jpg")
        End Try

    End Sub



    Private PanX As Integer
    Private PanY As Integer
    Private MouseX As Integer
    Private MouseY As Integer
    Private AngleX As Single
    Private AngleY As Single
    Private modelMove As Boolean = False
    Private modelPan As Boolean = False

    Private ImgLoc As New Point(323, 238)
    Private ImgSiz As New Size(286, 252)

    Private Sub ImgGenB_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles imgGenB.MouseDown

        Dim Button As Integer = e.Button \ &H100000
        If Button = 1 Then
            MouseX = e.X
            MouseY = e.Y
            AngleX = modelAngleX
            AngleY = modelAngleY
            modelMove = True
        ElseIf Button = 4 Then
            MouseX = e.X
            MouseY = e.Y
            PanX = CInt(modelPanX)
            PanY = CInt(modelPanY)
            modelPan = True
        ElseIf Button = 2 Then
            Hide()
            If FullScreen Then
                imgGenB.Location = ImgLoc
                imgGenB.Size = ImgSiz
            Else
                imgGenB.Location = New Point(0, 0)
                imgGenB.Size = New Size(Size.Width, Size.Height)
            End If
            FullScreen = Not FullScreen
            IsInit = True
            renderDevice.Dispose()
            LoadGraphics()

        End If
    End Sub

    Private Sub ImgGenB_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles imgGenB.MouseHover
        imgGenB.Focus()
    End Sub

    Private Sub ImgGenB_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles imgGenB.MouseMove

        If modelMove = True Then
            modelAngleY = AngleY + CSng(MouseX - e.X) * 2
            modelAngleX = AngleX - CSng(e.Y - MouseY) * 2
        End If

        If modelPan Then
            modelPanY = PanY + CSng(MouseY - e.Y) / 30
            modelPanX = PanX + CSng(e.X - MouseX) / 30
            If modelPanY > 3 Then modelPanY = 3
            If modelPanY < -3 Then modelPanY = -3
            If modelPanX > 3 Then modelPanX = 3
            If modelPanX < -3 Then modelPanX = -3
        End If

    End Sub
    Private Sub ImgGenB_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles imgGenB.MouseUp
        modelMove = False
        modelPan = False
    End Sub

    Private Sub ImgGenB_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles imgGenB.MouseWheel
        If e.Delta > 0 Then
            modelScale = modelScale * 1.5!
        End If
        If e.Delta < 0 Then
            modelScale = modelScale / 1.5!
        End If
    End Sub

    Private Sub CmdCancel_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.MouseHover
        cmdCancel.Focus()
    End Sub

    Private Sub OptGbFlat_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optGbFlat.CheckedChanged
        If optGbFlat.Checked Then
            BuildingType = 256
            SetBuildingType()
        End If
    End Sub

    Private Sub OptGbPeaked_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optGbPeaked.CheckedChanged
        If optGbPeaked.Checked Then
            BuildingType = 257
            SetBuildingType()
        End If
    End Sub

    Private Sub OptGbRidge_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optGbRidge.CheckedChanged
        If optGbRidge.Checked Then
            BuildingType = 258
            SetBuildingType()
        End If
    End Sub


    Private Sub OptGbSlant_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optGbSlant.CheckedChanged
        If optGbSlant.Checked Then
            BuildingType = 259
            SetBuildingType()
        End If
    End Sub


    Private Sub OptGbPyramidal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optGbPyramidal.CheckedChanged
        If optGbPyramidal.Checked Then
            BuildingType = 260
            SetBuildingType()
        End If
    End Sub

    Private Sub OptGbMultiSided_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optGbMultiSided.CheckedChanged
        If optGbMultiSided.Checked Then
            BuildingType = 261
            SetBuildingType()
        End If
    End Sub


End Class