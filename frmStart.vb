Option Strict Off
Option Explicit On
Imports Microsoft.Win32
Imports FSUIPC
Imports ScruffyDuck.Flightsim.Scenery.SceneryFile
Imports ScruffyDuck.Flightsim.Enumerations

Friend Class FrmStart

    Friend LatAircraft As Offset(Of Long) = New FSUIPC.Offset(Of Long)(&H560)
    Friend LonAircraft As Offset(Of Long) = New FSUIPC.Offset(Of Long)(&H568)

    Friend Alt1Aircraft As Offset(Of Integer) = New FSUIPC.Offset(Of Integer)(&H574)   ' units
    Friend Alt2Aircraft As Offset(Of Integer) = New FSUIPC.Offset(Of Integer)(&H570)   ' fraction

    Friend PitchAircraft As Offset(Of Integer) = New FSUIPC.Offset(Of Integer)(&H578)
    Friend BankAircraft As Offset(Of Integer) = New FSUIPC.Offset(Of Integer)(&H57C)
    Friend HeadingAircraft As Offset(Of Integer) = New FSUIPC.Offset(Of Integer)(&H580)

    Private AircraftLatitude As Double
    Private AircraftLongitude As Double

    Private SelectParent As Boolean = False
    Private KeyPanON As Boolean ' used for Pen
    Private KeyZoomRollON As Boolean
    Private ZoomRollON As Boolean
    Private KeyRightMouse As Boolean
    Private SelectWindow As Boolean
    Private MeasureTool As Boolean
    Private MeasureON As Boolean
    Private CursorPath As String = My.Application.Info.DirectoryPath & "\Tools\Cursors\"
    Private LatitudeDelta As Double
    Private LongitudeDelta As Double
    Private BackColorGray As Boolean = False


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)

        'If InitON Then Exit Sub

        If BitmapBuffer Is Nothing Then

            DisplayWidth = ClientSize.Width
            DisplayHeight = ClientSize.Height

            ' put this because when minimized there were crashes
            If DisplayWidth = 0 Then DisplayWidth = 10
            If DisplayHeight = 0 Then DisplayHeight = 10

            If DisplayWidth > 2 * DisplayHeight Then
                Width = 2 * DisplayHeight + 8
                DisplayWidth = 2 * DisplayHeight
            End If

            DisplayCenterX = CInt(DisplayWidth / 2)
            DisplayCenterY = CInt(DisplayHeight / 2)
            BitmapBuffer = New Bitmap(DisplayWidth, DisplayHeight)
            SetDispCenter(0, 0)
            BuildBitmapBuffer()
            e.Graphics.DrawImageUnscaled(BitmapBuffer, 0, 0)

        Else
            e.Graphics.DrawImageUnscaled(BitmapBuffer, 0, 0)
        End If

    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)

        If Not (BitmapBuffer Is Nothing) Then
            BitmapBuffer.Dispose()
            BitmapBuffer = Nothing
            Invalidate()
        End If

        MyBase.OnSizeChanged(e)

    End Sub

    Public Sub BuildBitmapBuffer()

        If Not BitmapBuffer Is Nothing Then

            Dim g As Graphics = Graphics.FromImage(BitmapBuffer)

            If BackColorGray Then
                g.Clear(Color.Gray)
            Else
                g.Clear(Color.White)
            End If

            If TileVIEW Then DisplayTiles(g)
            If MapVIEW Then DisplayMaps(g)
            If WaterVIEW Then DisplayWaters(g)
            If LandVIEW Then DisplayLands(g)
            If PolyVIEW Then DisplayPolys(g)
            If LineVIEW Then DisplayLines(g)
            If ObjectVIEW Then DisplayObjects(g)
            If ExcludeVIEW Then DisplayExcludes(g)
            DisplayGrids(g)
            DisplayScale()
            g.Dispose()

        End If

    End Sub

    Public Sub UpdateDisplay()

        ' copies the buffer to the display
        ' draws grids if necessary

        If Not BitmapBuffer Is Nothing Then

            Dim gr As Graphics = CreateGraphics()
            gr.DrawImageUnscaled(BitmapBuffer, 0, 0)   'copy buffer to display

            If CheckLine > 0 Then DisplayCheckLine(gr)
            If CheckPoly > 0 Then DisplayCheckPoly(gr)
            If AircraftVIEW Then DisplayAircraft(gr)

            gr.Dispose()

        End If

    End Sub

    Private Sub DisplayScale()

        Dim X As Integer = 0
        Dim A As String = ""
        GetScaleValues(X, A)
        lbScale.ForeColor = Color.Black
        If TileVIEW Then lbScale.ForeColor = Color.White

        Dim XY As Point
        XY.X = DisplayWidth - X - 10
        XY.Y = DisplayHeight - 35
        lbScaleBar.Location = XY
        lbScaleBar.Width = X
        XY.Y = DisplayHeight - 50
        X = Len(A)
        'XY.X = DisplayWidth - 65
        XY.X = DisplayWidth - X * 8 - 10
        lbScale.Location = XY
        lbScale.Text = A

    End Sub

    Private Sub GetScaleValues(ByRef X As Integer, ByRef A As String)

        Select Case Zoom
            Case 25
                X = 83
                A = "20 cm"
            Case 24
                X = 106
                A = "50 cm"
            Case 23
                X = 106
                A = "1 m"
            Case 22
                X = 107
                A = "2 m"
            Case 21
                X = 107
                A = "4 m"
            Case 20
                X = 107
                A = "8 m"
            Case 19
                X = 134
                A = "20 m"
            Case 18
                X = 100
                A = "30 m"
            Case 17
                X = 100
                A = "60 m"
            Case 16
                X = 84
                A = "100 m"
            Case 15
                X = 84
                A = "200 m"
            Case 14
                X = 105
                A = "500 m"
            Case 13
                X = 105
                A = "1 km"
            Case 12
                X = 105
                A = "2 km"
            Case 11
                X = 105
                A = "4 km"
            Case 10
                X = 105
                A = "8 km"
            Case 9
                X = 131
                A = "20 km"
            Case 8
                X = 98
                A = "30 km"
            Case 7
                X = 98
                A = "60 km"
            Case 6
                X = 82
                A = "100 km"
            Case 5
                X = 82
                A = "200 km"
            Case 4
                X = 102
                A = "500 km"
            Case 3
                X = 102
                A = "1000 km"
            Case 2
                A = "1000 km"
                X = 1000000 * PixelsPerMeter
            Case 1
                A = "2000 km"
                X = 2000000 * PixelsPerMeter
            Case 0
                A = "4000 km"
                X = 4000000 * PixelsPerMeter
        End Select


    End Sub


    Public Sub DisplayGrids(ByVal gr As Graphics)

        If QMIDLevel > 0 Then
            DrawThisGrid(QMIDLevel, gr)
        End If

        If LODLevel = -1 Then Exit Sub
        DrawThisGrid(-LODLevel, gr)   ' negative grid means it is a LOG grid!

    End Sub

    Private Sub DrawThisGrid(ByVal G As Integer, ByVal gr As Graphics)

        Dim myColor As Color = GridColor
        If G < 1 Then
            myColor = GridLODColor
            G = 2 - G
        End If

        Dim p As New Pen(myColor) With {
            .Width = GridWidth,
            .DashStyle = Drawing2D.DashStyle.Dash
        }

        G = G - 2 ' QMID=LOD+2

        Dim LA1, LA, LA2 As Integer
        Dim LO1, LO, LO2 As Integer
        Dim PX, PY As Integer

        Dim LatNorth, LatSouth, LonWest, LonEast As Double

        LatNorth = LatDispNorth
        LatSouth = LatDispSouth
        LonWest = LonDispWest
        LonEast = LonDispEast

        Dim LatDelta, LonDelta As Double
        Dim LatDeltaPix, LonDeltaPix As Double

        LatDelta = 90 / (2 ^ G)
        LatDeltaPix = LatDelta * PixelsPerLatDeg
        If LatDeltaPix < 8 Then
            p.Dispose()
            Exit Sub
        End If

        LonDelta = 120 / (2 ^ G)
        LonDeltaPix = LonDelta * PixelsPerLonDeg
        If LonDeltaPix < 8 Then
            p.Dispose()
            Exit Sub
        End If

        LA1 = Int(LatSouth / LatDelta) + 1
        LA2 = Int(LatNorth / LatDelta)

        LO1 = Int(LonWest / LonDelta) + 1
        LO2 = Int(LonEast / LonDelta)

        LatDelta = (LA1 * LatDelta - LatDispSouth) * PixelsPerLatDeg
        If G = 0 Then
            LonDelta = (LO1 * LonDelta - LonDispWest - 60) * PixelsPerLonDeg
            LO2 = LO2 + 1
        Else
            LonDelta = (LO1 * LonDelta - LonDispWest) * PixelsPerLonDeg
        End If

        Dim X1, X2, Y1, Y2 As Integer

        X1 = 0
        X2 = DisplayWidth
        Y1 = 0
        Y2 = DisplayHeight

        For LA = LA1 To LA2
            PY = DisplayHeight - CInt(LatDelta)
            gr.DrawLine(p, X1, PY, X2, PY)
            LatDelta = LatDelta + LatDeltaPix
        Next LA

        For LO = LO1 To LO2
            PX = CInt(LonDelta)
            gr.DrawLine(p, PX, Y1, PX, Y2)
            LonDelta = LonDelta + LonDeltaPix
        Next LO

        p.Dispose()

    End Sub

    Private Sub DisplayCheckLine(ByVal g As Graphics)

        Dim A As String

        A = "Line #" + Trim(Str(CheckLine)) + vbCrLf + "Point #" + Trim(Str(CheckLinePt))
        Dim drawFont As New Font("MS Reference Sans Serif", 8)
        g.DrawString(A, drawFont, Brushes.Black, DisplayCenterX + 6, DisplayCenterY - 12)
        drawFont.Dispose()

    End Sub


    Private Sub DisplayCheckPoly(ByVal g As Graphics)

        Dim A As String

        A = "Poly #" + Trim(Str(CheckPoly)) + vbCrLf + "Point #" + Trim(Str(CheckPolyPt))
        Dim drawFont As New Font("MS Reference Sans Serif", 8)
        g.DrawString(A, drawFont, Brushes.Black, DisplayCenterX + 6, DisplayCenterY - 12)
        drawFont.Dispose()

    End Sub

    Private Sub ZoomInOut(ByVal Button As Short)

        If Button = 1 Then
            If Zoom > 24 Then Exit Sub
            Zoom = Zoom + 1
        End If

        If Button = 2 Then
            If Zoom < 1 Then Exit Sub
            Zoom = Zoom - 1
        End If

        ResetZoom()

        MakeBackground()

        StatusZoom.Text = "Zoom = " & CStr(Zoom)

    End Sub


    Private Sub ResetLevelGrid(ByVal LOD As Boolean)

        NoGridMenuItem.Checked = QMIDLevel = 0
        Level2MenuItem.Checked = QMIDLevel = 2
        Level3MenuItem.Checked = QMIDLevel = 3
        Level4MenuItem.Checked = QMIDLevel = 4
        Level5MenuItem.Checked = QMIDLevel = 5
        Level6MenuItem.Checked = QMIDLevel = 6
        Level7MenuItem.Checked = QMIDLevel = 7
        Level8MenuItem.Checked = QMIDLevel = 8
        Level9MenuItem.Checked = QMIDLevel = 9
        Level10MenuItem.Checked = QMIDLevel = 10
        Level11MenuItem.Checked = QMIDLevel = 11
        Level12MenuItem.Checked = QMIDLevel = 12
        Level13MenuItem.Checked = QMIDLevel = 13
        Level14MenuItem.Checked = QMIDLevel = 14
        Level15MenuItem.Checked = QMIDLevel = 15
        Level16MenuItem.Checked = QMIDLevel = 16
        Level17MenuItem.Checked = QMIDLevel = 17
        Level18MenuItem.Checked = QMIDLevel = 18
        Level19MenuItem.Checked = QMIDLevel = 19
        Level20MenuItem.Checked = QMIDLevel = 20
        Level21MenuItem.Checked = QMIDLevel = 21
        Level22MenuItem.Checked = QMIDLevel = 22
        Level23MenuItem.Checked = QMIDLevel = 23
        Level24MenuItem.Checked = QMIDLevel = 24
        Level25MenuItem.Checked = QMIDLevel = 25
        Level26MenuItem.Checked = QMIDLevel = 26
        Level27MenuItem.Checked = QMIDLevel = 27
        Level28MenuItem.Checked = QMIDLevel = 28
        Level29MenuItem.Checked = QMIDLevel = 29

        NoLODMenuItem.Checked = LODLevel = -1
        LOD0MenuItem.Checked = LODLevel = 0
        LOD1MenuItem.Checked = LODLevel = 1
        LOD2MenuItem.Checked = LODLevel = 2
        LOD3MenuItem.Checked = LODLevel = 3
        LOD4MenuItem.Checked = LODLevel = 4
        LOD5MenuItem.Checked = LODLevel = 5
        LOD6MenuItem.Checked = LODLevel = 6
        LOD7MenuItem.Checked = LODLevel = 7
        LOD8MenuItem.Checked = LODLevel = 8
        LOD9MenuItem.Checked = LODLevel = 9
        LOD10MenuItem.Checked = LODLevel = 10
        LOD11MenuItem.Checked = LODLevel = 11
        LOD12MenuItem.Checked = LODLevel = 12
        LOD13MenuItem.Checked = LODLevel = 13
        LOD14MenuItem.Checked = LODLevel = 14
        LOD15MenuItem.Checked = LODLevel = 15
        LOD16MenuItem.Checked = LODLevel = 16
        LOD17MenuItem.Checked = LODLevel = 17
        LOD18MenuItem.Checked = LODLevel = 18
        LOD19MenuItem.Checked = LODLevel = 19
        LOD20MenuItem.Checked = LODLevel = 20
        LOD21MenuItem.Checked = LODLevel = 21
        LOD22MenuItem.Checked = LODLevel = 22
        LOD23MenuItem.Checked = LODLevel = 23
        LOD24MenuItem.Checked = LODLevel = 24
        LOD25MenuItem.Checked = LODLevel = 25
        LOD26MenuItem.Checked = LODLevel = 26
        LOD27MenuItem.Checked = LODLevel = 27


        SnapToQMIDMenuItem.Enabled = False
        If QMIDLevel > 0 Then SnapToQMIDMenuItem.Enabled = True

        If LOD Then Exit Sub

        If ZoomOnQMID Then

            If QMIDLevel = 0 Then
                Exit Sub
            ElseIf QMIDLevel < 3 Then
                Zoom = 0
            Else
                Zoom = QMIDLevel - 2
            End If

            If LandON Or WaterON Then Zoom = Zoom - 3

            SetDispCenter(0, 0)
            StatusZoom.Text = "Zoom = " & CStr(Zoom)

        End If

    End Sub



    Private Sub MakeAllOff()

        If LandON Then FrmLandsP.Dispose()
        If WaterON Then frmWatersP.Dispose()

        CheckPoly = 0
        CheckLine = 0

        PasteON = False
        MoveON = False
        InsertON = False
        SelectON = False

        PointerON = False
        ZoomON = False
        LandON = False
        LineON = False
        PolyON = False
        WaterON = False
        ObjectON = False
        ExcludeON = False
        DrawExclude = False

        ObjMYes = False
        MeasureTool = False

        MeasureToolMenuItem.Checked = False

        PasteON = False

        PtPolyCounter = 0
        PtLineCounter = 0

        MeasureToolMenuItem.Enabled = False
        CopyMenuItem.Enabled = False
        PasteMenuItem.Enabled = False
        DeleteMenuItem.Enabled = False

    End Sub

    Private Sub DisableSeveralMenuItems(ByVal Flag As Boolean)

        SaveAsMenuItem.Enabled = Not Flag
        ExportMenuItem.Enabled = Not Flag
        ExportSBXMenuItem.Enabled = Not Flag
        ExportBLNMenuItem.Enabled = Not Flag
        ExportSHPMenuItem.Enabled = Not Flag
        AppendMenuItem.Enabled = Not Flag
        AppendSHPMenuItem.Enabled = Not Flag
        AppendBLNMenuItem.Enabled = Not Flag
        AppendObjMenuItem.Enabled = Not Flag
        AppendRAWMenuItem.Enabled = Not Flag
        AppendVTPMenuItem.Enabled = Not Flag
        PropertiesMenuItem.Enabled = Not Flag
        BGLMenuItem.Enabled = Not Flag

        ShowBackgroundMenuItem.Enabled = Not Flag
        'PreferencesMenuItem.Enabled = Not Flag
        TileServerMenuItem.Enabled = Not Flag
        EditINIFileMenuItem.Enabled = Not Flag
        ObjectFoldersMenuItem.Enabled = Not Flag
        FSXSettingsMenuItem.Enabled = Not Flag


        ' inserted in May 21 2007
        InvertSelectionMenuItem.Enabled = Not Flag
        SelectAllMenuItem.Enabled = Not Flag

        LODGridMenuItem.Enabled = Not Flag
        QMIDGridMenuItem.Enabled = Not Flag
        SnapToQMIDMenuItem.Enabled = Not Flag
        MeasureToolMenuItem.Enabled = Not Flag
        ObjLibManagerMenuItem.Enabled = Not Flag
        ShowAircraftMenuItem.Enabled = Not Flag
        FlyAircraftToMenuItem.Enabled = Not Flag
        GoToPositionMenuItem.Enabled = Not Flag
        BGLToolStripButton.Enabled = Not Flag
        FindMenuItem.Enabled = Not Flag

    End Sub

    Private Sub DisableOtherItems()

        ShowAircraftMenuItem.Checked = False
        SaveMenuItem.Enabled = False
        UndoMenuItem.Enabled = False
        RedoMenuItem.Enabled = False
        CopyMenuItem.Enabled = False
        PasteMenuItem.Enabled = False
        DeleteMenuItem.Enabled = False
        ViewMapsMenuItem.Enabled = False

        UndoToolStripButton.Enabled = False
        RedoToolStripButton.Enabled = False
        SaveToolStripButton.Enabled = False

    End Sub


    Private Sub UncheckToolButtons()

        PointerMenuItem.Checked = False
        ZoomMenuItem.Checked = False
        AddMapMenuItem.Checked = False
        LandMenuItem.Checked = False
        WaterMenuItem.Checked = False
        LineMenuItem.Checked = False
        PolyMenuItem.Checked = False
        ObjectMenuItem.Checked = False
        ExcludeMenuItem.Checked = False
        MeasureToolMenuItem.Enabled = False

        PointerToolStripButton.Checked = False
        ZoomToolStripButton.Checked = False
        MeshToolStripButton.Checked = False
        LandToolStripButton.Checked = False
        WaterToolStripButton.Checked = False
        PhotoToolStripButton.Checked = False
        LineToolStripButton.Checked = False
        PolyToolStripButton.Checked = False
        ObjectToolStripButton.Checked = False
        ExcludeToolStripButton.Checked = False

    End Sub

    Private Sub DisableToolButtons(ByVal Flag As Boolean)

        PointerMenuItem.Enabled = Not Flag
        ZoomMenuItem.Enabled = Not Flag
        AddMapMenuItem.Enabled = Not Flag
        LandMenuItem.Enabled = Not Flag
        WaterMenuItem.Enabled = Not Flag
        LineMenuItem.Enabled = Not Flag
        PolyMenuItem.Enabled = Not Flag
        ObjectMenuItem.Enabled = Not Flag
        ExcludeMenuItem.Enabled = Not Flag
        MeasureToolMenuItem.Enabled = Not Flag

        PointerToolStripButton.Enabled = Not Flag
        ZoomToolStripButton.Enabled = Not Flag
        MeshToolStripButton.Enabled = Not Flag
        LandToolStripButton.Enabled = Not Flag
        WaterToolStripButton.Enabled = Not Flag
        PhotoToolStripButton.Enabled = Not Flag
        LineToolStripButton.Enabled = Not Flag
        PolyToolStripButton.Enabled = Not Flag
        ObjectToolStripButton.Enabled = Not Flag
        ExcludeToolStripButton.Enabled = Not Flag

    End Sub

    Private Sub UncheckViews()

        MeasureToolMenuItem.Checked = False
        ViewAllMapsMenuItem.Checked = False
        ViewAllLandsMenuItem.Checked = False
        ViewAllWatersMenuItem.Checked = False
        ViewAllLinesMenuItem.Checked = False
        ViewAllPolysMenuItem.Checked = False
        ViewAllObjectsMenuItem.Checked = False
        ViewAllExcludesMenuItem.Checked = False
        ViewAllMenuItem.Checked = False
        ViewMapsMenuItem.Checked = False

    End Sub

    Private Sub DisableViews(ByVal Flag As Boolean)

        MeasureToolMenuItem.Enabled = Not Flag
        ViewAllMapsMenuItem.Enabled = Not Flag
        ViewAllLandsMenuItem.Enabled = Not Flag
        ViewAllWatersMenuItem.Enabled = Not Flag
        ViewAllLinesMenuItem.Enabled = Not Flag
        ViewAllPolysMenuItem.Enabled = Not Flag
        ViewAllObjectsMenuItem.Enabled = Not Flag
        ViewAllExcludesMenuItem.Enabled = Not Flag
        ViewAllMenuItem.Enabled = Not Flag

    End Sub

    Private Sub UncheckSelections()

        SelectAllMapsMenuItem.Checked = False

        SelectAllLandsMenuItem.Checked = False
        SelectAllWatersMenuItem.Checked = False

        SelectAllLinesMenuItem.Checked = False
        SelectAllPolysMenuItem.Checked = False
        SelectAllObjectsMenuItem.Checked = False
        SelectAllExcludesMenuItem.Checked = False
        SelectAllMenuItem.Checked = False

    End Sub

    Private Sub DisableSelections(ByVal Flag As Boolean)

        If Flag Then
            SomeSelected = True
            UnSelectAll()
        End If

        SelectAllMapsMenuItem.Enabled = Not Flag
        SelectAllLandsMenuItem.Enabled = Not Flag

        SelectAllLinesMenuItem.Enabled = Not Flag
        SelectAllPolysMenuItem.Enabled = Not Flag
        SelectAllWatersMenuItem.Enabled = Not Flag
        SelectAllObjectsMenuItem.Enabled = Not Flag
        SelectAllExcludesMenuItem.Enabled = Not Flag

        SelectAllMenuItem.Enabled = Not Flag
        InvertSelectionMenuItem.Enabled = Not Flag

    End Sub

    Private Sub DeleteAll()

        NoOfMaps = 0
        NoOfLands = 0
        NoOfLines = 0
        NoOfPolys = 0
        NoOfWaters = 0
        NoOfObjects = 0
        NoOfExcludes = 0
        NoOfLWCIs = 0

        ReDim Maps(1)
        ReDim LLands(256, 256, 0)
        ReDim LL_XY(95, 63)
        ReDim WWaters(256, 256, 0)
        ReDim WW_XY(95, 63)
        ReDim Lines(1)
        ReDim Polys(1)
        ReDim Objects(1)
        ReDim Excludes(1)
        ReDim LWCIs(1)


    End Sub

    Private Sub UnSelectAll()

        If SomeSelected = False Then
            NoOfPointsSelected = 0
            NoOfLinesSelected = 0
            NoOfPolysSelected = 0
            NoOfLandsSelected = 0
            NoOfWatersSelected = 0
            NoOfMapsSelected = 0
            NoOfObjectsSelected = 0
            SelectAllMenuItem.Checked = False
            AllSELECT = False
            Exit Sub
        End If

        SelectAllMaps(False)
        SelectAllLands(False)
        SelectAllLines(False)
        SelectAllPolys(False)
        SelectAllWaters(False)
        SelectAllObjects(False)
        SelectAllExcludes(False)

        AllSELECT = False
        SomeSelected = False
        NoOfPointsSelected = 0
        NoOfLinesSelected = 0
        NoOfPolysSelected = 0
        NoOfLandsSelected = 0
        NoOfWatersSelected = 0
        NoOfMapsSelected = 0
        NoOfObjectsSelected = 0
        NoOfExcludesSelected = 0

        SelectAllMenuItem.Checked = False

    End Sub


    Private Sub FrmStart_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        FrmLandsP.Dispose()
        frmWatersP.Dispose()
        FSUIPCConnection.Close()

    End Sub

    Private Sub FrmStart_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Dim Cancel As Boolean = e.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = e.CloseReason

        If Not Dirty Then
            Cancel = False
            Exit Sub
        End If

        If UnloadMode > 1 Then
            Cancel = False
            Exit Sub
        End If

        Dim A As String = "You did not save your data! Do you really want to exit ?"

        If MsgBox(A, MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Cancel = True
        Else
            Cancel = False
        End If

        e.Cancel = Cancel

    End Sub


    Public Sub FrmStart_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Dim KeyCode As Short = e.KeyCode
        Dim Shift As Short = e.KeyData \ &H10000

        Dim X, N As Integer

        InsertON = False

        N = NoOfPointsSelected
        N = N + NoOfLinesSelected
        N = N + NoOfPolysSelected
        N = N + NoOfLandsSelected
        N = N + NoOfWatersSelected
        N = N + NoOfMapsSelected
        N = N + NoOfObjectsSelected
        N = N + NoOfExcludesSelected

        If KeyCode = Keys.Delete And CheckLine > 0 Then DeleteCheckLinePt()
        If KeyCode = Keys.Delete And CheckPoly > 0 Then DeleteCheckPolyPt()
        If KeyCode = Keys.Delete And (N > 0) Then
            If AskDelete Then
                X = MsgBox("Delete " & Str(N) & " item(s) ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, AppTitle)
            Else
                X = MsgBoxResult.Yes
            End If
            If X = MsgBoxResult.Yes Then
                BackUp()
                SkipBackUp = True
                DeleteSelected()
                SkipBackUp = False
                RebuildDisplay()
                Exit Sub
            Else
                Exit Sub
            End If
        End If

        If KeyCode = Keys.Escape Then

            If SelectParent = True Then
                SelectParent = False
            End If

            If PasteON Then
                PasteON = False
                PasteMenuItem.Checked = False
            End If

            MeasureTool = False
            MeasureON = False
            MeasureToolMenuItem.Checked = False
            TextBoxMeasure.Visible = False
            SetMouseIcon()

            CheckLine = 0
            CheckPoly = 0
            If LineON Then
                If PtLineCounter > 0 Then
                    LineInsertMode(2, 0, 0, 0)
                End If
            End If
            If PolyON Then
                If PtPolyCounter > 0 Then
                    PolyInsertMode(2, 0, 0, 0)
                End If
            End If
            Exit Sub
        End If

        If KeyCode = Keys.M Then
            MeasureToolMenuItem_Click(MeasureToolMenuItem, New System.EventArgs())
            Exit Sub
        End If

        If KeyCode = Keys.I Then
            InsertON = True
            Exit Sub
        End If

        If KeyCode = Keys.D Then
            DeleteON = True
            Exit Sub
        End If

        If KeyCode = Keys.B Then
            BreakLineON = True
            Exit Sub
        End If

        If KeyCode = Keys.P Then
            If Shift = 4 Then ' ALT P
                PolyFILL = Not PolyFILL
                RebuildDisplay()
            Else
                KeyPanON = True
                Exit Sub
            End If
        End If

        If KeyCode = Keys.Z Then
            KeyZoomRollON = True
            Exit Sub
        End If

        If KeyCode = Keys.ControlKey Then
            SelectWindow = True
            Exit Sub
        End If

        If KeyCode = Keys.Subtract Then
            DecreaseWithdON = True
            Exit Sub
        End If

        If KeyCode = Keys.Add Then
            IncreaseWithdON = True
            Exit Sub
        End If

        If CheckLine > 0 Then
            If KeyCode = Keys.Right Then CheckLineMove(True)
            If KeyCode = Keys.Left Then CheckLineMove(False)
            If KeyCode = Keys.Up Then CheckLinePtMove(True)
            If KeyCode = Keys.Down Then CheckLinePtMove(False)
            Exit Sub
        End If

        If CheckPoly > 0 Then
            If KeyCode = Keys.Right Then CheckPolyMove(True)
            If KeyCode = Keys.Left Then CheckPolyMove(False)
            If KeyCode = Keys.Up Then CheckPolyPtMove(True)
            If KeyCode = Keys.Down Then CheckPolyPtMove(False)
            Exit Sub
        End If

        If LineON And KeyCode = Keys.Space Then

            If Not SelectWindow Then
                LineInsertMode(1, Shift, AuxXInt, AuxYInt)
            End If
            Exit Sub
        End If

    End Sub


    Private Sub FrmStart_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp

        Dim KeyCode As Short = e.KeyCode
        Dim Shift As Short = e.KeyData \ &H10000

        If KeyCode = Keys.I Then InsertON = False
        If KeyCode = Keys.Insert Then InsertON = True
        If KeyCode = Keys.D Then DeleteON = False
        If KeyCode = Keys.B Then BreakLineON = False
        If KeyCode = Keys.P Then KeyPanON = False
        If KeyCode = Keys.Z Then KeyZoomRollON = False
        If KeyCode = Keys.ControlKey Then SelectWindow = False
        If KeyCode = Keys.R Then KeyRightMouse = True
        If KeyCode = Keys.Subtract Then DecreaseWithdON = False
        If KeyCode = Keys.Add Then IncreaseWithdON = False

    End Sub



    Private Sub FrmStart_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        FormLoad()

    End Sub


    Public Sub SetMouseIcon()

        On Error GoTo erro

        If PanON Then
            Cursor = System.Windows.Forms.Cursors.Hand
            Exit Sub
        End If

        If MeasureTool Then
            Cursor = New System.Windows.Forms.Cursor(CursorPath & "measure.cur")
            Exit Sub
        End If

        If MoveON Then
            Cursor = New System.Windows.Forms.Cursor(CursorPath & "move.cur")
            Exit Sub
        End If

        If SelectON Then
            Cursor = System.Windows.Forms.Cursors.Cross
            Exit Sub
        End If

        If PointerON Then
            Cursor = System.Windows.Forms.Cursors.Arrow
            Exit Sub
        End If

        If ZoomON Then
            Cursor = New System.Windows.Forms.Cursor(CursorPath & "zoom.cur")
            Exit Sub
        End If


        If LineON Then
            If PasteON Then
                Cursor = New System.Windows.Forms.Cursor(CursorPath & "paste.cur")
                Exit Sub
            ElseIf LineTURN Then
                Cursor = New System.Windows.Forms.Cursor(CursorPath & "turn.cur")
                Exit Sub
            Else
                Cursor = New System.Windows.Forms.Cursor(CursorPath & "line.cur")
                Exit Sub
            End If
        End If

        If PolyON Then
            If PasteON Then
                Cursor = New System.Windows.Forms.Cursor(CursorPath & "paste.cur")
                Exit Sub
            Else
                Cursor = New System.Windows.Forms.Cursor(CursorPath & "poly.cur")
                Exit Sub
            End If
        End If

        If WaterON Then
            Cursor = New System.Windows.Forms.Cursor(CursorPath & "water.cur")
            Exit Sub
        End If

        If ObjectON Then
            If ObjectSIZE Then
                Cursor = New System.Windows.Forms.Cursor(CursorPath & "size.cur")
                Exit Sub
            ElseIf ObjectTURN Then
                Cursor = New System.Windows.Forms.Cursor(CursorPath & "turn.cur")
                Exit Sub
            ElseIf PasteON Then
                Cursor = New System.Windows.Forms.Cursor(CursorPath & "paste.cur")
                Exit Sub
            Else
                Cursor = New System.Windows.Forms.Cursor(CursorPath & "object.cur")
                Exit Sub
            End If
        End If

        If ExcludeON Then
            If ExcludeSizeIndex > 0 Then
                Cursor = New System.Windows.Forms.Cursor(CursorPath & "size.cur")
            Else
                Cursor = New System.Windows.Forms.Cursor(CursorPath & "exclude.cur")
            End If
            Exit Sub
        End If


        If LandON Then
            Cursor = New System.Windows.Forms.Cursor(CursorPath & "land.cur")
            Exit Sub
        End If

        Exit Sub
erro:
        MsgBox("Error - display cursor", MsgBoxStyle.Critical)

    End Sub

    Private Sub FormLoad()

        ToolStrip.Cursor = Cursors.Arrow
        MenuStrip.Cursor = Cursors.Arrow

        If BitmapBuffer Is Nothing Then
            DisplayWidth = ClientSize.Width
            DisplayHeight = ClientSize.Height
            DisplayCenterX = CInt(DisplayWidth / 2)
            DisplayCenterY = CInt(DisplayHeight / 2)
        End If

        UncheckToolButtons()
        UncheckViews()
        UncheckSelections()

        DisableToolButtons(True)
        DisableViews(True)
        DisableSelections(True)
        DisableSeveralMenuItems(True)
        DisableOtherItems()
        HidePopUPMenu()

        ' read INI file
        ReDim RecentFiles(4)
        GetSettings()
        SetRecentFiles()


        'This is the local to make choices for different sims! At this moment only
        ' FSX is implemented. NameOftheSim is read from the INI file and it is only
        ' used here. IgnoreFSX is also used on FSXSettings and this should be looked
        ' uponr when additional sims are added. Also look a bit further down for the
        ' SetBMPTextures()


        ' in October 2017
        IgnoreFSX = True
        If NameOfSim.ToUpper = "FSX" Then
            SimExe = "fsx.exe"
            IgnoreFSX = False
        Else
            SimExe = NameOfSim  ' while there is no implementation you can use NameOfSim as the EXE
        End If

        If IgnoreFSX Then
            IsFSX = False
            FSPath = SimPath & "\"
            CheckFSXTools()         ' should be looked at when different sims implemented
            If Not IsFSXTerrain Or Not IsFSXBGLComp Or Not IsFSXPlugins Then
                FrmFSXSettings.ShowDialog()
            End If
        Else
            ' before October 2017
            CheckFS10()     ' will set IsFSX true if found on register
            CheckFSXTools()
            If Not IsFSX Or Not IsFSXTerrain Or Not IsFSXBGLComp Or Not IsFSXPlugins Then
                FrmFSXSettings.ShowDialog()
            End If
        End If

        CheckTerrainCFG()

        If Not My.Computer.FileSystem.DirectoryExists(BGLFolder) Then
            Directory.CreateDirectory(BGLFolder)
        End If

        If Not My.Computer.FileSystem.DirectoryExists(AppPath & "\Scenery") Then
            Directory.CreateDirectory(My.Application.Info.DirectoryPath & "\Scenery")
        End If

        If Not My.Computer.FileSystem.DirectoryExists(AppPath & "\Texture") Then
            Directory.CreateDirectory(My.Application.Info.DirectoryPath & "\Texture")
        End If

        If Not My.Computer.FileSystem.DirectoryExists(AppPath & "\Tiles") Then
            Directory.CreateDirectory(My.Application.Info.DirectoryPath & "\Tiles")
        End If

        If Not My.Computer.FileSystem.DirectoryExists(AppPath & "\LibObjects") Then
            Directory.CreateDirectory(My.Application.Info.DirectoryPath & "\LibObjects")
        End If

        If Not My.Computer.FileSystem.DirectoryExists(AppPath & "\LibObjects\NewJpegs") Then
            Directory.CreateDirectory(My.Application.Info.DirectoryPath & "\LibObjects\NewJpegs")
        End If

        If Not My.Computer.FileSystem.DirectoryExists(AppPath & "\LibObjects\BackUps") Then
            Directory.CreateDirectory(My.Application.Info.DirectoryPath & "\LibObjects\BackUps")
        End If

        If Not My.Computer.FileSystem.DirectoryExists(AppPath & "\GenBuildings") Then
            Directory.CreateDirectory(My.Application.Info.DirectoryPath & "\GenBuildings")
        End If

        If Not My.Computer.FileSystem.DirectoryExists(AppPath & "\Mdls") Then
            Directory.CreateDirectory(My.Application.Info.DirectoryPath & "\Mdls")
        End If

        If Not My.Computer.FileSystem.DirectoryExists(AppPath & "\ASD") Then
            MkDir(My.Application.Info.DirectoryPath & "\ASD")
        End If

        If Not My.Computer.FileSystem.DirectoryExists(AppPath & "\API") Then
            MkDir(My.Application.Info.DirectoryPath & "\API")
        End If

        If Not My.Computer.FileSystem.DirectoryExists(AppPath & "\Rwy12") Then
            MkDir(My.Application.Info.DirectoryPath & "\Rwy12")
        End If

        If Not My.Computer.FileSystem.DirectoryExists(AppPath & "\Rwy12\img") Then
            MkDir(My.Application.Info.DirectoryPath & "\Rwy12\img")
        End If

        If Not My.Computer.FileSystem.DirectoryExists(AppPath & "\Tools\Work") Then
            Directory.CreateDirectory(My.Application.Info.DirectoryPath & "\Tools\Work")
        End If

        If Not My.Computer.FileSystem.DirectoryExists(AppPath & "\Tools\Shapes") Then
            MkDir(My.Application.Info.DirectoryPath & "\Tools\Shapes")
        End If

        If Not My.Computer.FileSystem.DirectoryExists(AppPath & "\Tools\Bmps") Then
            MkDir(My.Application.Info.DirectoryPath & "\Tools\Bmps")
        End If

        If Not My.Computer.FileSystem.DirectoryExists(AppPath & "\Help") Then
            MkDir(My.Application.Info.DirectoryPath & "\Help")
        End If

        SetPolyTypes()
        SetLineTypes()
        SetLandTypes()
        SetWaterTypes()
        SetServerTypes()
        SetExtrusionsTypes()

        If IsFSX Then  ' added October 2017
            SetBmpTextures()
        End If

        SetGenBObjects()

        SetLibObjects()
        SetRwy12Objects()
        SetMacroObjects()

        ProjectName = "PROJECT"
        BGLProjectFolder = BGLFolder

        DeleteAll()  ' this avoids the error on Loading of Generic Buildings (June, 7, 2014)

        Text = AppTitle & "  " & UCase(ProjectName)
        StatusStrip.Visible = True
        lbScale.Visible = True
        lbScaleBar.Visible = True

        Cursor = System.Windows.Forms.Cursors.Arrow

        Zoom = 8
        LatDispCenter = LatIniCenter
        LonDispCenter = LonIniCenter
        ResetZoom()

        AircraftVIEW = False

        If BackUpON Then BackUpInit()

        UncheckToolButtons()
        UncheckViews()
        UncheckSelections()
        DisableSelections(True)
        DisableToolButtons(False)
        DisableViews(False)
        DisableSeveralMenuItems(False)
        DisableOtherItems()
        ResetLevelGrid(True)

        StatusZoom.Text = "Zoom = " & CStr(Zoom)
        PointerMenuItem.Checked = True
        PointerToolStripButton.Checked = True
        MeasureToolMenuItem.Enabled = True
        PointerON = True
        Cursor = System.Windows.Forms.Cursors.Arrow

        TileVIEW = False
        ShowBackgroundMenuItem.Checked = False
        FromBackgroundMapMenuItem.Enabled = False
        If ActiveTileFolder <> "" Then
            ShowBackgroundMenuItem.Enabled = True
        Else
            ShowBackgroundMenuItem.Enabled = False
        End If

        Dirty = False
        QMIDLevel = 0
        LODLevel = -1
        ViewON = True

        BackColorGray = False
        SetDispCenter(0, 0)
        BuildBitmapBuffer()

    End Sub

    Private Sub NewProject()

        Dim N As Integer

        If Dirty Then

            Dim A As String = "You did not save your data! Continue ?"
            If MsgBox(A, MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Exit Sub
            End If

        End If

        ShowSimpleMaps = False

        For N = 1 To NoOfMaps
            If Not ImgMaps(N) Is Nothing Then ImgMaps(N).Dispose()
        Next N

        StatusStrip.Visible = False
        lbScale.Visible = False
        lbScaleBar.Visible = False

        DeleteAll()
        ViewON = True
        MakeAllOff()
        QMIDLevel = 0
        LODLevel = -1

        BackColorGray = True
        RebuildDisplay()

        WorkFile = ""
        ProjectName = "PROJECT"
        BGLProjectFolder = BGLFolder
        lbDonation.Visible = False

        FrmProjectP.ShowDialog()

        Text = AppTitle & "  " & UCase(ProjectName)

        BackColorGray = False
        StatusStrip.Visible = True
        lbScale.Visible = True
        lbScaleBar.Visible = True

        Dirty = False

        'GetSettings()  is it necessary? suppose no!
        Cursor = System.Windows.Forms.Cursors.Arrow

        Zoom = 8
        LatDispCenter = LatIniCenter
        LonDispCenter = LonIniCenter
        ResetZoom()

        AircraftVIEW = False

        If BackUpON Then BackUpInit()

        UncheckToolButtons()
        UncheckViews()
        UncheckSelections()
        DisableSelections(True)
        DisableToolButtons(False)
        DisableViews(False)
        DisableSeveralMenuItems(False)
        DisableOtherItems()
        ResetLevelGrid(True)

        StatusZoom.Text = "Zoom = " & CStr(Zoom)
        PointerMenuItem.Checked = True
        PointerToolStripButton.Checked = True
        MeasureToolMenuItem.Enabled = True
        PointerON = True
        Cursor = System.Windows.Forms.Cursors.Arrow

        TileVIEW = False
        ShowBackgroundMenuItem.Checked = False
        FromBackgroundMapMenuItem.Enabled = False
        If ActiveTileFolder <> "" Then
            ShowBackgroundMenuItem.Enabled = True
        Else
            ShowBackgroundMenuItem.Enabled = False
        End If

        SetDispCenter(0, 0)
        BuildBitmapBuffer()

    End Sub

    Private Sub CheckFS10()

        Dim C As String

        IsFSX = False
        'FSPath = ""
        'FSPath = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Microsoft Games\Flight Simulator\10.0", "SetupPath", Nothing)
        FSPath = Get_FS_Path("FSX")

        If FSPath = "" Then
            MsgBox("FSX could not be found in this computer!", 16, AppTitle)
        Else
            FSTextureFolder = FSPath & "Scenery\World\Texture\"
            If Not My.Computer.FileSystem.FileExists(FSPath & "terrain.cfg") Then
                C = "The Windows Registry indicates that FSX is in the folder:" & vbCrLf & vbCrLf
                C = C & FSPath & vbCrLf & vbCrLf
                C = C & "but SBuilderX could not find it there!"
                MsgBox(C, 16, AppTitle)
            Else
                IsFSX = True
            End If
        End If

    End Sub

    Private Sub CheckFSXTools()

        Dim B, C As String

        Dim ToolsFolder As String = AppPath & "\Tools\"
        IsFSXTerrain = False
        IsFSXBGLComp = False
        IsFSXPlugins = False

        'SDKPath = ""
        'SDKPath = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Microsoft Games\Flight Simulator X SDK", "SetupPath", Nothing)
        SDKPath = Get_FS_Path("SDK")

        If SDKPath <> "" Then
            SDKTerrain = SDKPath & "SDK\Environment Kit\Terrain SDK\"
            SDKBglComp = SDKPath & "SDK\Environment Kit\BGL Compiler SDK\"
            'SDKPlugins = SDKPath & "SDK\Environment Kit\Modeling SDK\FSX_GmaxGamePack\Plugins\"
            SDKPlugins = SDKPath & "SDK\Environment Kit\Modeling SDK\"
        End If

        If My.Computer.FileSystem.FileExists(ToolsFolder & "shp2vec.exe") _
            And My.Computer.FileSystem.FileExists(ToolsFolder & "resample.exe") _
            And My.Computer.FileSystem.FileExists(ToolsFolder & "imagetool.exe") Then
            IsFSXTerrain = True
        End If

        If My.Computer.FileSystem.FileExists(ToolsFolder & "bglcomp.exe") _
            And My.Computer.FileSystem.FileExists(ToolsFolder & "bglcomp.xsd") Then
            IsFSXBGLComp = True
        End If

        If My.Computer.FileSystem.FileExists(ToolsFolder & "XToMdl.exe") _
                   And My.Computer.FileSystem.FileExists(ToolsFolder & "Managed_CrashTree.dll") _
                   And My.Computer.FileSystem.FileExists(ToolsFolder & "Managed_Lookup_Keyword.dll") Then
            IsFSXPlugins = True
        End If


        If IsFSXTerrain And IsFSXBGLComp And IsFSXPlugins Then Exit Sub

        ' if it got here something is wrong
        If SDKPath <> "" Then

            If IsFSXTerrain = False Then
                'SDKTerrain = SDKPath & "SDK\Environment Kit\Terrain SDK\"
                If My.Computer.FileSystem.FileExists(SDKTerrain & "shp2vec.exe") _
                    And My.Computer.FileSystem.FileExists(SDKTerrain & "resample.exe") _
                    And My.Computer.FileSystem.FileExists(SDKTerrain & "imagetool.exe") Then
                    B = SDKTerrain & "shp2vec.exe"
                    C = ToolsFolder & "shp2vec.exe"
                    File.Copy(B, C, True)
                    B = SDKTerrain & "resample.exe"
                    C = ToolsFolder & "resample.exe"
                    File.Copy(B, C, True)
                    B = SDKTerrain & "imagetool.exe"
                    C = ToolsFolder & "imagetool.exe"
                    File.Copy(B, C, True)
                    IsFSXTerrain = True
                Else
                    C = "Shp2Vec, ImageTool & Resample do not exist in the ..\SBuilder\Tools"
                    C = C & vbCrLf & " folder nor in the Terrain SDK folder! Some BGL files can not be generated!"
                    MsgBox(C, 16, AppTitle)
                End If
            End If

            If IsFSXBGLComp = False Then
                ' SDKBglComp = SDKPath & "SDK\Environment Kit\BGL Compiler SDK\"
                If My.Computer.FileSystem.FileExists(SDKBglComp & "bglcomp.exe") _
                    And My.Computer.FileSystem.FileExists(SDKBglComp & "bglcomp.xsd") Then
                    B = SDKBglComp & "bglcomp.exe"
                    C = ToolsFolder & "bglcomp.exe"
                    File.Copy(B, C, True)
                    B = SDKBglComp & "bglcomp.xsd"
                    C = ToolsFolder & "bglcomp.xsd"
                    File.Copy(B, C, True)
                    IsFSXBGLComp = True
                Else
                    C = "BGLComp does not exist in the ..\SBuilder\Tools folder nor in"
                    C = C & vbCrLf & "the BGL Compiler SDK folder! Some BGL files can not be generated!"
                    MsgBox(C, 16, AppTitle)
                End If
            End If

            If IsFSXPlugins = False Then
                'SDKPlugins = SDKPath & "SDK\Environment Kit\Modeling SDK\FSX_GmaxGamePack\Plugins\"
                If My.Computer.FileSystem.FileExists(SDKPlugins & "XToMdl.exe") _
                    And My.Computer.FileSystem.FileExists(SDKPlugins & "Managed_CrashTree.dll") _
                    And My.Computer.FileSystem.FileExists(SDKPlugins & "Managed_Lookup_Keyword.dll") Then
                    B = SDKPlugins & "XToMdl.exe"
                    C = ToolsFolder & "XToMdl.exe"
                    File.Copy(B, C, True)
                    B = SDKPlugins & "Managed_CrashTree.dll"
                    C = ToolsFolder & "Managed_CrashTree.dll"
                    File.Copy(B, C, True)
                    B = SDKPlugins & "Managed_Lookup_Keyword.dll"
                    C = ToolsFolder & "Managed_Lookup_Keyword.dll"
                    File.Copy(B, C, True)
                    IsFSXPlugins = True
                Else
                    C = "XToMdl, Managed_CrashTree & Managed_Lookup_Keyword do not exist in the ..\SBuilder\Tools"
                    C = C & vbCrLf & " folder nor in the FSX_GmaxGamePack\Plugins SDK folder! Some BGL files can not be generated!"
                    MsgBox(C, 16, AppTitle)
                End If
            End If

        Else
            C = "Some SDK tools are missing in the ..\SBuilder\Tools folder and the FSX SDK"
            C = C & vbCrLf & "could not be found in this computer! Some BGL files can not be generated!"
            MsgBox(C, 16, AppTitle)

        End If

    End Sub

    Private Function Get_FS_Path(ByVal sKey As String) As String

        ' Get_FS_Path("FSX") returns the full path to FSX if found,
        ' Get_FS_Path("SDK") returns the full path to the FSX SDK if found,
        ' otherwise returns an empty string

        Dim regKey As RegistryKey
        Dim GetPath As String = Nothing
        Dim s As String = ""
        Dim sVista As String = ""

        If sKey = "FSX" Then
            sKey = "flight simulator\10.0"
        ElseIf sKey = "SDK" Then
            sKey = "Flight Simulator X SDK"
        Else
            Return ""
        End If

        Try
            Do
                s = "Software\" & sVista & "Microsoft\microsoft games\" & sKey
                regKey = My.Computer.Registry.LocalMachine.OpenSubKey(s)
                If regKey Is Nothing Then regKey = My.Computer.Registry.CurrentUser.OpenSubKey(s)
                If regKey Is Nothing Then regKey = My.Computer.Registry.Users.OpenSubKey(s)
                If regKey IsNot Nothing Then
                    GetPath = regKey.GetValue("SetupPath")
                    If (GetPath IsNot Nothing) AndAlso (Not GetPath.EndsWith("\")) Then
                        GetPath = GetPath & "\"
                    End If
                    regKey.Close()
                End If

                If (GetPath IsNot Nothing) Or (sVista <> "") Then Exit Do
                sVista = "Wow6432Node\"
            Loop

            If GetPath Is Nothing Then GetPath = ""
            Return GetPath

        Catch sE As Security.SecurityException

            MsgBox("You do not have sufficient privileges to run SBuilderX.  Please log on as administrator.")
            Return ""

        End Try

    End Function


    Private Sub CheckTerrainCFG()

        If Not IsFSX Then Exit Sub
        If OriginalTerrainCFG Then Exit Sub

        Dim TerrainFile As String = FSPath & "terrain.cfg"
        Dim A As String

        Dim IsOK As Boolean = True
        FileOpen(2, TerrainFile, OpenMode.Input)
        A = LineInput(2)
        FileClose()

        If A <> "//------------------------------------------------------------------------" Then IsOK = False

        If IsOK Then Exit Sub

        A = "The modified FSX terrain.cfg file authored by Richard Ludowise" & vbCrLf
        A = A & "and Luis Féliz-Tirado was not detected in your system! For better" & vbCrLf
        A = A & "results you should install this file. SBuilderX can make a backup" & vbCrLf
        A = A & "of the original terrain.cfg and install the modified file for you." & vbCrLf & vbCrLf
        A = A & "Do you want to install the modified terrain.cfg?" & vbCrLf

        If MsgBox(A, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            A = "Enter a filename to store the original terrain.cfg."
            Dim BackUpFile As String = InputBox(A, , "terrain_original.cfg")
            If BackUpFile = "" Then Exit Sub
            BackUpFile = FSPath & BackUpFile
            File.Copy(TerrainFile, BackUpFile, True)
            Dim ModTerrainFile As String = AppPath & "\Tools\terrain.cfg"
            File.Copy(ModTerrainFile, TerrainFile, True)
        Else
            A = "If you want to keep your present terrain.cfg and" & vbCrLf
            A = A & "force SBuilderX to ignore this test, answer YES!"
            If MsgBox(A, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                OriginalTerrainCFG = True
                WriteSettings()
            End If
        End If

    End Sub

    Private Sub NewMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NewMenuItem.Click

        NewProject()

    End Sub

    Private Sub OpenMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenMenuItem.Click

        Dim b As String

        Dim a, FileName As String

        lbDonation.Visible = False

        If Dirty Then
            'MsgBox("You did not save your data! Continue ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2)
            'If MsgBoxResult.Yes Then ' User chose Yes.
            'Else ' User chose No.
            '    Exit Sub
            'End If

            a = "You did not save your data! Continue ?"
            If MsgBox(a, MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Exit Sub
            End If

        End If

        a = "SBuilderX (*.SBP)|*.SBP"
        b = "SBuilderX - Open Project"

        FileName = FileNameToOpen(a, b, "SBP")

        If FileName = "" Then Exit Sub

        FileOpenHeader()
        OpenFile(FileName)
        WorkFile = FileName
        FileOpenTrailer()


    End Sub

    Private Sub FileOpenHeader()

        Dim N As Integer
        DisableSelections(True)
        For N = 1 To NoOfMaps
            ImgMaps(N).Dispose()
        Next N

        ReDim Maps(0)
        ReDim ImgMaps(0)
        ReDim LLands(256, 256, 0)
        ReDim LL_XY(95, 63)

        ReDim Lines(0)
        ReDim Polys(0)

        ReDim WWaters(256, 256, 0)
        ReDim WW_XY(95, 63)

        ReDim LWCIs(0)
        ReDim Objects(0)
        ReDim Excludes(0)


    End Sub
    Private Sub FileOpenTrailer()


        MakeAllOff()
        ResetZoom()
        SetDispCenter(0, 0)

        ShowSimpleMaps = False

        If NoOfMaps > 0 Then
            ReDim ImgMaps(NoOfMaps)
            CheckMaps()
        End If

        Text = AppTitle & "  " & UCase(ProjectName)

        StatusStrip.Visible = True
        Dirty = False
        If BackUpON Then BackUpInit()

        AircraftVIEW = False

        UncheckToolButtons()
        UncheckViews()
        UncheckSelections()

        DisableToolButtons(False)
        DisableViews(False)
        DisableSeveralMenuItems(False)
        DisableOtherItems()
        LODLevel = -1
        QMIDLevel = 0
        ResetLevelGrid(True)


        StatusZoom.Text = "Zoom = " & CStr(Zoom)
        PointerMenuItem.Checked = True
        PointerToolStripButton.Checked = True
        MeasureToolMenuItem.Enabled = True
        PointerON = True
        Cursor = System.Windows.Forms.Cursors.Arrow

        If WorkFile <> "" Then
            SaveMenuItem.Enabled = True
            SaveToolStripButton.Enabled = True
        End If

        DisplayWidth = ClientSize.Width
        DisplayHeight = ClientSize.Height
        BitmapBuffer = New Bitmap(DisplayWidth, DisplayHeight)

        BackColor = Color.White

        Season = "Summer"
        SetBitmapSeason()

        TileVIEW = False
        ShowBackgroundMenuItem.Checked = False
        If ActiveTileFolder <> "" Then
            ShowBackgroundMenuItem.Enabled = True
        Else
            ShowBackgroundMenuItem.Enabled = False
        End If

        ViewON = True
        AllVIEW = False
        ViewAllMenuItem_Click(Nothing, Nothing)
        SomeSelected = True
        Dirty = False

        lbDonation.Visible = False

    End Sub


    Private Sub BGLMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BGLMenuItem.Click

        FrmBGL.ShowDialog()

    End Sub

    Private Sub CopyMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CopyMenuItem.Click
        EditCopy()
    End Sub

    Private Sub SaveAsMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveAsMenuItem.Click

        Dim N As Integer

        Dim a, b As String

        a = "SBuilderX (*.SBP)|*.SBP"
        b = "SBuilderX - Save Project As"

        a = FileNameToSave(a, b, "SBP")
        If a = "" Then Exit Sub

        WorkFile = a
        SaveFile(a)
        SetFileBackUp(a)
        Dirty = False

        N = InStrRev(a, "\") + 1
        a = Mid(a, N)
        Text = AppTitle & "  " & UCase(ProjectName)

        SaveMenuItem.Enabled = True
        SaveToolStripButton.Enabled = True

    End Sub

    Private Sub SaveMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveMenuItem.Click


        SaveFile(WorkFile)
        Dirty = False

    End Sub

    Private Sub RedoMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedoMenuItem.Click
        Redo()
        RebuildDisplay()
    End Sub

    Private Sub UndoMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UndoMenuItem.Click
        Undo()
        RebuildDisplay()
    End Sub

    Private Sub LineMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LineMenuItem.Click
        LineToolStripButton_Click(LineToolStripButton, New System.EventArgs())
    End Sub

    Private Sub ObjectMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ObjectMenuItem.Click
        ObjectToolStripButton_Click(ObjectToolStripButton, New System.EventArgs())
    End Sub

    Private Sub PolyMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PolyMenuItem.Click
        PolyToolStripButton_Click(PolyToolStripButton, New System.EventArgs())
    End Sub

    Private Sub WaterMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles WaterMenuItem.Click
        WaterToolStripButton_Click(WaterToolStripButton, New System.EventArgs())
    End Sub

    Private Sub ZoomMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZoomMenuItem.Click
        ZoomToolStripButton_Click(ZoomToolStripButton, New System.EventArgs())
    End Sub

    Private Sub CutMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        EditCut()
    End Sub

    Private Sub PasteMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PasteMenuItem.Click

        If PasteMenuItem.Checked Then
            PasteMenuItem.Checked = False
            PasteON = False
        Else
            PasteMenuItem.Checked = True
            PasteON = True
        End If
        SetMouseIcon()

    End Sub

    Private Sub MeshMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        MeshToolStripButton_Click(MeshToolStripButton, New System.EventArgs())
    End Sub

    Private Sub ViewAllMapsMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ViewAllMapsMenuItem.Click

        If MapVIEW Then
            ViewAllMapsMenuItem.Checked = False
            SelectAllMaps(False)
            SelectAllMapsMenuItem.Enabled = False
        Else
            ViewAllMapsMenuItem.Checked = True
            SelectAllMapsMenuItem.Enabled = True
        End If

        MapVIEW = Not MapVIEW

        If NoOfMaps > 0 Then RebuildDisplay()

    End Sub




    Private Sub ViewAllWatersMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ViewAllWatersMenuItem.Click

        If WaterON Then
            SelectAllWatersMenuItem.Enabled = True
            ViewAllWatersMenuItem.Checked = True
            WaterVIEW = True
            Exit Sub
        End If

        If WaterVIEW Then
            ViewAllWatersMenuItem.Checked = False
            SelectAllWaters(False)
            SelectAllWatersMenuItem.Enabled = False
        Else
            ViewAllWatersMenuItem.Checked = True
            SelectAllWatersMenuItem.Enabled = True
        End If

        WaterVIEW = Not WaterVIEW

        If NoOfWaters > 0 Then RebuildDisplay()

    End Sub


    Private Sub ViewAllLinesMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ViewAllLinesMenuItem.Click

        If LineON Then
            SelectAllLinesMenuItem.Enabled = True
            ViewAllLinesMenuItem.Checked = True
            LineVIEW = True
            Exit Sub
        End If

        If LineVIEW Then
            ViewAllLinesMenuItem.Checked = False
            SelectAllLines(False)
            SelectAllLinesMenuItem.Enabled = False
        Else
            ViewAllLinesMenuItem.Checked = True
            SelectAllLinesMenuItem.Enabled = True
        End If

        LineVIEW = Not LineVIEW

        If NoOfLines > 0 Then RebuildDisplay()

    End Sub

    Private Sub ViewAllPolysMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ViewAllPolysMenuItem.Click

        If PolyON Then
            SelectAllPolysMenuItem.Enabled = True
            ViewAllPolysMenuItem.Checked = True
            PolyVIEW = True
            Exit Sub
        End If

        If PolyVIEW Then
            ViewAllPolysMenuItem.Checked = False
            SelectAllPolys(False)
            SelectAllPolysMenuItem.Enabled = False
        Else
            ViewAllPolysMenuItem.Checked = True
            SelectAllPolysMenuItem.Enabled = True
        End If

        PolyVIEW = Not PolyVIEW

        If NoOfPolys > 0 Then RebuildDisplay()

    End Sub

    Private Sub ViewAllExcludesMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ViewAllExcludesMenuItem.Click

        If ExcludeON Then
            SelectAllExcludesMenuItem.Enabled = True
            ViewAllExcludesMenuItem.Checked = True
            ExcludeVIEW = True
            Exit Sub
        End If

        If ExcludeVIEW Then
            ViewAllExcludesMenuItem.Checked = False
            SelectAllExcludes(False)
            SelectAllExcludesMenuItem.Enabled = False
        Else
            ViewAllExcludesMenuItem.Checked = True
            SelectAllExcludesMenuItem.Enabled = True
        End If

        ExcludeVIEW = Not ExcludeVIEW

        If NoOfExcludes > 0 Then RebuildDisplay()

    End Sub

    Private Sub ViewAllLandsMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ViewAllLandsMenuItem.Click

        If LandON Then
            SelectAllLandsMenuItem.Enabled = True
            ViewAllLandsMenuItem.Checked = True
            LandVIEW = True
            Exit Sub
        End If

        If LandVIEW Then
            ViewAllLandsMenuItem.Checked = False
            SelectAllLands(False)
            SelectAllLandsMenuItem.Enabled = False
        Else
            ViewAllLandsMenuItem.Checked = True
            SelectAllLandsMenuItem.Enabled = True
        End If

        LandVIEW = Not LandVIEW

        If NoOfLands > 0 Then RebuildDisplay()

    End Sub

    Private Sub ViewAllObjectsMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ViewAllObjectsMenuItem.Click

        If ObjectON Then
            SelectAllObjectsMenuItem.Enabled = True
            ViewAllObjectsMenuItem.Checked = True
            ObjectVIEW = True
            Exit Sub
        End If

        If ObjectVIEW Then
            ViewAllObjectsMenuItem.Checked = False
            SelectAllObjects(False)
            SelectAllObjectsMenuItem.Enabled = False
        Else
            ViewAllObjectsMenuItem.Checked = True
            SelectAllObjectsMenuItem.Enabled = True
        End If

        ObjectVIEW = Not ObjectVIEW

        If NoOfObjects > 0 Then RebuildDisplay()

    End Sub


    Private Sub WaterToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles WaterToolStripButton.Click

        MakeAllOff()
        UncheckToolButtons()
        WaterMenuItem.Checked = True
        WaterToolStripButton.Checked = True
        WaterON = True
        SetMouseIcon()
        ViewAllWatersMenuItem_Click(ViewAllWatersMenuItem, New System.EventArgs())
        UncheckSelections()
        DisableSelections(True)
        SelectAllWatersMenuItem.Enabled = True

        'Level15MenuItem.Checked = True
        QMIDLevel = 15
        ResetLevelGrid(True)
        POPIndex = -1
        FrmWatersP.Show()
        'Zoom = 10
        'SetDispCenter(0, 0)
        'Me.StatusZoom.Text = "Zoom = " & CStr(Zoom)
        RebuildDisplay()

    End Sub

    Private Sub BGLToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BGLToolStripButton.Click
        BGLMenuItem_Click(BGLMenuItem, New System.EventArgs())
    End Sub

    Private Sub CopyToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        CopyMenuItem_Click(CopyMenuItem, New System.EventArgs())
    End Sub


    Private Sub ExcludeToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExcludeToolStripButton.Click

        MakeAllOff()
        UncheckToolButtons()
        ExcludeMenuItem.Checked = True
        ExcludeToolStripButton.Checked = True
        ExcludeON = True
        SetMouseIcon()
        ViewAllExcludesMenuItem_Click(ViewAllExcludesMenuItem, New System.EventArgs())
        UncheckSelections()
        DisableSelections(True)
        SelectAllExcludesMenuItem.Enabled = True
        RebuildDisplay()

    End Sub

    Private Sub LandToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LandToolStripButton.Click

        MakeAllOff()
        UncheckToolButtons()
        LandMenuItem.Checked = True
        LandToolStripButton.Checked = True
        LandON = True
        SetMouseIcon()
        ViewAllLandsMenuItem_Click(ViewAllLandsMenuItem, New System.EventArgs())
        UncheckSelections()
        DisableSelections(True)
        SelectAllLandsMenuItem.Enabled = True

        'Level15MenuItem.Checked = True
        QMIDLevel = 15
        ResetLevelGrid(True)
        POPIndex = -1
        FrmLandsP.Show()
        'Zoom = 10
        'SetDispCenter(0, 0)
        'Me.StatusZoom.Text = "Zoom = " & CStr(Zoom)
        RebuildDisplay()

    End Sub

    Private Sub LineToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LineToolStripButton.Click

        MakeAllOff()
        UncheckToolButtons()
        LineMenuItem.Checked = True
        LineToolStripButton.Checked = True
        MeasureToolMenuItem.Enabled = True
        LineON = True
        SetMouseIcon()
        ViewAllLinesMenuItem_Click(ViewAllLinesMenuItem, New System.EventArgs())
        UncheckSelections()
        DisableSelections(True)
        SelectAllLinesMenuItem.Enabled = True
        RebuildDisplay()

    End Sub

    Private Sub MeshToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MeshToolStripButton.Click

        'Dim A As String

        'MakeAllOff()
        'UncheckToolButtons()
        'MeshMenuItem.Checked = True
        'MeshToolStripButton.Checked = True
        'MeshON = True
        'SetMouseIcon()
        'ViewAllMeshesMenuItem_Click(ViewAllMeshesMenuItem, New System.EventArgs())
        'UncheckSelections()
        'DisableSelections(True)
        'SelectAllMeshesMenuItem.Enabled = True
        'ResetLevelGrid()
        'Level15MenuItem.Checked = True
        'QMIDLevel = 15
        ''frmMeshesP.Show()
        'Zoom = 8
        'SetDispCenter(0, 0)
        'Me.StatusZoom.Text = "Zoom = " & CStr(Zoom)
        'If Not ViewAllMeshesMenuItem.Checked Then ViewAllMeshesMenuItem_Click(ViewAllMeshesMenuItem, New System.EventArgs())
        'RebuildDisplay()
        'A = "You should unset the 8-bit/meter option" & vbCrLf
        'A = A & "in the Preferences menu!"
        'If Mesh8 Then MsgBox(A, MsgBoxStyle.Information)

    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click
        NewMenuItem_Click(NewMenuItem, New System.EventArgs())
    End Sub

    Private Sub ObjectToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ObjectToolStripButton.Click

        MakeAllOff()
        UncheckToolButtons()
        ObjectMenuItem.Checked = True
        ObjectToolStripButton.Checked = True
        MeasureToolMenuItem.Enabled = True
        ObjectON = True
        SetMouseIcon()
        ViewAllObjectsMenuItem_Click(ViewAllObjectsMenuItem, New System.EventArgs())
        UncheckSelections()
        DisableSelections(True)
        SelectAllObjectsMenuItem.Enabled = True
        RebuildDisplay()

    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        OpenMenuItem_Click(OpenMenuItem, New System.EventArgs())
    End Sub

    Private Sub PasteToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PasteMenuItem_Click(PasteMenuItem, New System.EventArgs())
    End Sub

    Private Sub PointerToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PointerToolStripButton.Click

        MakeAllOff()
        UncheckToolButtons()
        PointerMenuItem.Checked = True
        PointerToolStripButton.Checked = True
        MeasureToolMenuItem.Enabled = True
        PointerON = True
        SetMouseIcon()
        UncheckSelections()
        DisableSelections(False)
        RebuildDisplay()

    End Sub

    Private Sub PointerMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PointerMenuItem.Click
        PointerToolStripButton_Click(PointerToolStripButton, New System.EventArgs())
    End Sub

    Private Sub PolyToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PolyToolStripButton.Click

        MakeAllOff()
        UncheckToolButtons()
        PolyMenuItem.Checked = True
        PolyToolStripButton.Checked = True
        MeasureToolMenuItem.Enabled = True
        PolyON = True
        SetMouseIcon()
        ViewAllPolysMenuItem_Click(ViewAllPolysMenuItem, New System.EventArgs())
        UncheckSelections()
        DisableSelections(True)
        SelectAllPolysMenuItem.Enabled = True
        RebuildDisplay()

    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        SaveMenuItem_Click(SaveMenuItem, New System.EventArgs())
    End Sub

    Private Sub UndoToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UndoToolStripButton.Click
        UndoMenuItem_Click(UndoMenuItem, New System.EventArgs())
    End Sub

    Private Sub RedoToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedoToolStripButton.Click
        RedoMenuItem_Click(RedoMenuItem, New System.EventArgs())
    End Sub

    Private Sub ZoomToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZoomToolStripButton.Click

        MakeAllOff()
        UncheckToolButtons()
        ZoomMenuItem.Checked = True
        ZoomToolStripButton.Checked = True
        ZoomON = True
        SetMouseIcon()
        UncheckSelections()
        DisableSelections(False)
        RebuildDisplay()

    End Sub


    Private Sub ExcludeMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExcludeMenuItem.Click
        ExcludeToolStripButton_Click(ExcludeToolStripButton, New System.EventArgs())
    End Sub

    Private Sub LandMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LandMenuItem.Click
        LandToolStripButton_Click(LandToolStripButton, New System.EventArgs())
    End Sub

    Private Sub ViewAllMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ViewAllMenuItem.Click

        If AllVIEW Then
            ' hide all and unselect all

            UnSelectAll()
            AllSELECT = False

            MapVIEW = False

            If Not LandON Then LandVIEW = False
            If Not LineON Then LineVIEW = False
            If Not PolyON Then PolyVIEW = False
            If Not WaterON Then WaterVIEW = False
            If Not ObjectON Then ObjectVIEW = False
            If Not ExcludeON Then ExcludeVIEW = False


            ViewAllMapsMenuItem.Checked = False
            If Not LandON Then ViewAllLandsMenuItem.Checked = False
            If Not LineON Then ViewAllLinesMenuItem.Checked = False
            If Not PolyON Then ViewAllPolysMenuItem.Checked = False
            If Not WaterON Then ViewAllWatersMenuItem.Checked = False
            If Not ObjectON Then ViewAllObjectsMenuItem.Checked = False
            If Not ExcludeON Then ViewAllExcludesMenuItem.Checked = False

            ViewAllMenuItem.Checked = False

            SelectAllMapsMenuItem.Enabled = False
            If Not LandON Then SelectAllLandsMenuItem.Enabled = False
            If Not LineON Then SelectAllLinesMenuItem.Enabled = False
            If Not PolyON Then SelectAllPolysMenuItem.Enabled = False
            If Not WaterON Then SelectAllWatersMenuItem.Enabled = False
            If Not ObjectON Then SelectAllObjectsMenuItem.Enabled = False
            If Not ExcludeON Then SelectAllExcludesMenuItem.Enabled = False


            SelectAllMenuItem.Enabled = False

            SelectAllMapsMenuItem.Checked = False
            If Not LandON Then SelectAllLandsMenuItem.Checked = False
            If Not LineON Then SelectAllLinesMenuItem.Checked = False
            If Not PolyON Then SelectAllPolysMenuItem.Checked = False
            If Not WaterON Then SelectAllWatersMenuItem.Checked = False
            If Not ObjectON Then SelectAllObjectsMenuItem.Checked = False
            If Not ExcludeON Then SelectAllExcludesMenuItem.Checked = False

            SelectAllMenuItem.Checked = False

        Else
            'show all
            MapVIEW = True
            LandVIEW = True
            LineVIEW = True
            PolyVIEW = True
            WaterVIEW = True
            ObjectVIEW = True
            ExcludeVIEW = True

            ViewAllMapsMenuItem.Checked = True
            ViewAllLandsMenuItem.Checked = True
            ViewAllLinesMenuItem.Checked = True
            ViewAllPolysMenuItem.Checked = True
            ViewAllWatersMenuItem.Checked = True
            ViewAllObjectsMenuItem.Checked = True
            ViewAllExcludesMenuItem.Checked = True
            ViewAllMenuItem.Checked = True

            If ZoomON Or PointerON Then
                SelectAllMapsMenuItem.Enabled = True
                SelectAllLandsMenuItem.Enabled = True
                SelectAllLinesMenuItem.Enabled = True
                SelectAllPolysMenuItem.Enabled = True
                SelectAllWatersMenuItem.Enabled = True
                SelectAllObjectsMenuItem.Enabled = True
                SelectAllExcludesMenuItem.Enabled = True
                SelectAllMenuItem.Enabled = True
            End If
        End If

        RebuildDisplay()

        AllVIEW = Not AllVIEW

    End Sub

    Private Sub SelectAllExcludesMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectAllExcludesMenuItem.Click

        If SelectAllExcludesMenuItem.Checked Then
            SelectAllExcludes(False)
        Else
            SelectAllExcludes(True)
        End If

        RebuildDisplay()

    End Sub

    Private Sub SelectAllLandsMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectAllLandsMenuItem.Click

        If SelectAllLandsMenuItem.Checked Then
            SelectAllLands(False)
        Else
            SelectAllLands(True)
        End If

        RebuildDisplay()

    End Sub

    Private Sub SelectAllLinesMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectAllLinesMenuItem.Click

        If SelectAllLinesMenuItem.Checked Then
            SelectAllLines(False)
        Else
            SelectAllLines(True)
        End If

        RebuildDisplay()

    End Sub

    Private Sub SelectAllMapsMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectAllMapsMenuItem.Click

        If SelectAllMapsMenuItem.Checked Then
            SelectAllMaps(False)
        Else
            SelectAllMaps(True)
        End If

        RebuildDisplay()

    End Sub


    Private Sub SelectAllObjectsMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectAllObjectsMenuItem.Click

        If SelectAllObjectsMenuItem.Checked Then
            SelectAllObjects(False)
        Else
            SelectAllObjects(True)
        End If

        RebuildDisplay()

    End Sub


    Private Sub SelectAllPolysMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectAllPolysMenuItem.Click

        If SelectAllPolysMenuItem.Checked Then
            SelectAllPolys(False)
        Else
            SelectAllPolys(True)
        End If

        RebuildDisplay()

    End Sub

    Private Sub SelectAllWatersMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectAllWatersMenuItem.Click

        If SelectAllWatersMenuItem.Checked Then
            SelectAllWaters(False)
        Else
            SelectAllWaters(True)
        End If

        RebuildDisplay()

    End Sub

    Private Sub SelectAllMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectAllMenuItem.Click

        If AllSELECT Then
            ' unselect all

            SelectAllLands(False)
            SelectAllLines(False)
            SelectAllPolys(False)
            SelectAllWaters(False)
            SelectAllObjects(False)
            SelectAllExcludes(False)
            SelectAllMaps(False)

            SelectAllMenuItem.Checked = False

        Else
            ' select all
            SelectAllLands(True)
            SelectAllLines(True)
            SelectAllPolys(True)
            SelectAllWaters(True)
            SelectAllObjects(True)
            SelectAllExcludes(True)
            SelectAllMaps(True)

            SelectAllMenuItem.Checked = True

        End If

        RebuildDisplay()

        AllSELECT = Not AllSELECT

    End Sub

    Private Sub ShowToolbarMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowToolbarMenuItem.Click

        If ShowToolbarMenuItem.Checked Then
            ShowToolbarMenuItem.Checked = False
            ToolStrip.Visible = False
        Else
            ShowToolbarMenuItem.Checked = True
            ToolStrip.Visible = True
        End If

        ViewMenuItem.ShowDropDown()

    End Sub

    Private Sub FrmStart_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown

        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim X As Integer = e.X
        Dim Y As Integer = e.Y

        If Not ViewON Then Exit Sub
        If WAIT Then Exit Sub

        If KeyRightMouse And Button = 1 Then
            Button = 2
            KeyRightMouse = False
        End If

        If Button = 2 And InsertON Then
            InsertON = False
            Exit Sub
        End If

        If Button = 2 And PasteON Then
            PasteON = False
            PasteMenuItem.Checked = False
            SetMouseIcon()
            Exit Sub
        End If

        If Button = 2 And MeasureTool Then
            MeasureTool = False
            MeasureON = False
            MeasureToolMenuItem.Checked = False
            TextBoxMeasure.Visible = False
            SetMouseIcon()
            Exit Sub
        End If

        ' we are in zoom mode
        If ZoomON Then
            If Not (Button = 1 Or Button = 2) Then Exit Sub
            SetDispCenter(X - DisplayCenterX, Y - DisplayCenterY)
            ZoomInOut((Button))
            SetDispCenter(0, 0)
            RebuildDisplay()
            HidePopUPMenu()
            Exit Sub
        End If

        If KeyZoomRollON Then
            AuxXInt = X
            AuxYInt = Y
            ZoomRollON = True
            Exit Sub
        End If

        ' starting of a pan movement
        If Button = 4 Or KeyPanON Then
            Cursor = New System.Windows.Forms.Cursor(CursorPath & "pan.cur")
            AuxXInt = X
            AuxYInt = Y
            PanON = True
            Exit Sub
        End If

        CheckLine = 0
        CheckPoly = 0

        If Button = 1 And PasteON Then
            EditPasteXY(X, Y)
            Exit Sub
        End If


        If CalibratePT1 Then
            frmCalibrate.ReturnCalPT1(Button, X, Y)
            frmCalibrate.Show()
            Exit Sub
        End If


        If CalibratePT2 Then
            frmCalibrate.ReturnCalPT2(Button, X, Y)
            frmCalibrate.Show()
            Exit Sub
        End If

        If LColPickON Then
            ColorFromMap(X, Y)
            LColPickON = False
            Exit Sub
        End If

        ' start of a selection or a movement or a measurement in Pointer mode
        If Button = 1 And PointerON Then

            If SelectParent Then
                TryParentPoly(X, Y)
                SelectParent = False
                Exit Sub
            End If

            If Shift = 0 Then UnSelectAll()

            If IsSelection(X, Y) Then ' movement
                SomeSelected = True
                RebuildDisplay()
                SetDelay(200)
                MoveON = True
                FirstMOVE = True
                AuxXInt = X
                AuxYInt = Y
            ElseIf MeasureTool Then
                If MeasureON Then
                    MeasureON = False
                Else
                    AuxXInt = X
                    AuxYInt = Y
                    MeasureON = True
                    Exit Sub
                End If
            Else ' selection
                SelectON = True
                SetMouseIcon()
                AuxXInt = X
                AuxYInt = Y
            End If
            Exit Sub
        End If

        If Button = 2 And PointerON Then
            SelectParent = False
            ProcessPopUp(X, Y)
            Exit Sub
        End If


        ' we are in line insert mode
        If LineON Then
            If Not (SelectWindow Or MeasureTool) Then  ' is the start of new line
                LineInsertMode(Button, Shift, X, Y)
                Exit Sub
            ElseIf SelectWindow Then  ' is a selection
                If Shift = 2 Then UnSelectAll()
                SelectON = True
                SetMouseIcon()
                AuxXInt = X
                AuxYInt = Y
                Exit Sub
            ElseIf Button = 1 Then  ' =2 then the routine goes on to exit on a commom point
                If MeasureON Then
                    MeasureON = False
                Else
                    AuxXInt = X
                    AuxYInt = Y
                    MeasureON = True
                    Exit Sub
                End If
            End If
        End If

        ' we are in poly insert mode
        If PolyON Then
            If SelectParent Then
                If Button = 1 Then
                    TryParentPoly(X, Y)
                    SelectParent = False
                ElseIf Button = 2 Then
                    SelectParent = False
                    ProcessPopUp(X, Y)
                End If
                Exit Sub
            End If
            If Not (SelectWindow Or MeasureTool) Then  ' is the start of new poly
                PolyInsertMode(Button, Shift, X, Y)
                Exit Sub
            ElseIf SelectWindow Then  ' is a selection
                If Shift = 2 Then UnSelectAll()
                SelectON = True
                SetMouseIcon()
                AuxXInt = X
                AuxYInt = Y
                Exit Sub
            ElseIf Button = 1 Then  ' =2 then the routine goes on to exit on a commom point
                If MeasureON Then
                    MeasureON = False
                Else
                    AuxXInt = X
                    AuxYInt = Y
                    MeasureON = True
                    Exit Sub
                End If
            End If
        End If

        ' we are in object insert mode
        If ObjectON Then
            If Not (SelectWindow Or MeasureTool) Then  ' is the start of new object
                ObjectInsertMode(Button, Shift, X, Y)
                Exit Sub
            ElseIf SelectWindow Then  ' is a selection
                If Shift = 2 Then UnSelectAll()
                SelectON = True
                AuxXInt = X
                AuxYInt = Y
                Exit Sub
            ElseIf Button = 1 Then  ' =2 then the routine goes on to exit on a commom point
                If MeasureON Then
                    MeasureON = False
                Else
                    AuxXInt = X
                    AuxYInt = Y
                    MeasureON = True
                    Exit Sub
                End If
            End If
        End If


        ' we are in exclude insert mode
        If ExcludeON Then
            ExcludeInsertMode(Button, Shift, X, Y)
            Exit Sub
        End If

        'If PhotoON Then
        '    'PhotoInsertMode(Button, XD, YD)
        '    Exit Sub
        'End If

        'If MeshON Then
        '    'MeshInsertMode(Button, XD, YD)
        '    Exit Sub
        'End If

        If LandON Then
            If LandWaterDELETE And Button = 1 Then
                DeleteLand(X, Y)
                Dirty = True
                RebuildDisplay()
                Exit Sub
            End If
            If LandWaterRASTER And Button = 1 Then LandWaterRasON = True
            LandInsertMode(Button, X, Y)
            Exit Sub
        End If

        If WaterON Then
            If LandWaterDELETE And Button = 1 Then
                DeleteWater(X, Y)
                Dirty = True
                RebuildDisplay()
                Exit Sub
            End If
            If LandWaterRASTER And Button = 1 Then LandWaterRasON = True
            WaterInsertMode(Button, X, Y)
            Exit Sub
        End If

    End Sub


    Private Sub FrmStart_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove

        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim X As Integer = e.X
        Dim Y As Integer = e.Y

        Dim N, M As Integer
        If WAIT Then Exit Sub

        ShowLatLon(X, Y)

        If KeyRightMouse And Button = 1 Then Button = 2

        ' doing the actual pan
        If PanON Then
            SetDispCenter(AuxXInt - X, AuxYInt - Y)
            AuxXInt = X
            AuxYInt = Y
            RebuildDisplay()
            If PtLineCounter > 0 Then DrawLineSegment(AuxXInt - X, AuxYInt - Y)
            If PtPolyCounter > 0 Then DrawPolySegment(AuxXInt - X, AuxYInt - Y)
            Exit Sub
        End If

        If KeyPanON Then Exit Sub

        'doing a rolled zoom = pressed Z and using a non-wheel mouse!
        If ZoomRollON Then
            If Y - AuxYInt > 50 Then
                ZoomInOut(1)
                AuxYInt = Y
            End If
            If AuxYInt - Y > 50 Then
                ZoomInOut(2)
                AuxYInt = Y
            End If
            SetDispCenter(0, 0)
            RebuildDisplay()
            Exit Sub
        End If

        If KeyZoomRollON Then Exit Sub

        If SelectParent Then
            DrawParentSelectLabel(X, Y)
            Exit Sub
        End If

        ' doing the actual selecting moving box
        If SelectON Then
            DrawSelectBox(X, Y)
            Exit Sub
        End If

        If MeasureTool Then
            If MeasureON Then
                ShowMeasure(X, Y)
            End If
            Exit Sub
        End If

        ' doing the actual MOVE 60 ms after the mouse click
        If MoveON Then
            If FirstMOVE Then
                BackUp()
                FirstMOVE = False
                SetMouseIcon()
            End If
            If DELAY Then Exit Sub
            X = X - AuxXInt
            Y = Y - AuxYInt
            AuxXInt = AuxXInt + X
            AuxYInt = AuxYInt + Y
            MoveSelected(X, Y)
            RebuildDisplay()
            Exit Sub
        End If


        If CalibratePT1 Or CalibratePT2 Then
            ' Me.Cursor = System.Windows.Forms.Cursors.Cross
            Cursor = New System.Windows.Forms.Cursor(CursorPath & "calib.cur")
            Exit Sub
        End If

        If PointerON Then
            If ShowLabels Then
                N = IsMouseOnLine(X, Y)
                If N > 0 Then
                    DrawLineLabel(X, Y, N)
                    Exit Sub
                End If
                N = IsMouseOnPoly(X, Y, M)
                If N > 0 Then
                    DrawPolyLabel(X, Y, N, 0)
                    Exit Sub
                End If
                N = IsMouseOnObject(X, Y)
                If N > 0 Then
                    DrawObjectLabel(X, Y, N)
                    Exit Sub
                End If
                UpdateDisplay()
            End If
            Exit Sub
        End If

        If LineON Then
            If LineTURN Then
                TurnLine(X, Y)
                Exit Sub
            End If
            If PtLineCounter > 0 Then
                AuxXInt = X
                AuxYInt = Y
                DrawLineSegment(X, Y)
            ElseIf ShowLabels Then
                N = IsMouseOnLine(X, Y)
                If N > 0 Then
                    DrawLineLabel(X, Y, N)
                Else
                    UpdateDisplay()
                End If
            End If
            Exit Sub
        End If

        If PolyON Then
            If PtPolyCounter > 0 Then
                DrawPolySegment(X, Y)
            ElseIf ShowLabels Then
                N = IsMouseOnPoly(X, Y, M)
                If N > 0 Then
                    DrawPolyLabel(X, Y, N, M)
                Else
                    UpdateDisplay()
                End If
            End If
            Exit Sub
        End If

        If ObjectTURN Then
            TurnObject(X, Y)
            Exit Sub
        End If

        If ObjectSIZE Then
            SizeObject(X, Y)
            Exit Sub
        End If

        If ExcludeSizeIndex > 0 Then
            SizeExclude(X, Y, ExcludeSizeIndex)
            Exit Sub
        End If

        If DrawExclude Then
            DrawExcludeBox(X, Y)
            Exit Sub
        End If

        If LandON And LandWaterRasON Then
            LandRasterMode(X, Y)
            Exit Sub
        End If

        If WaterON And LandWaterRasON Then
            WaterRasterMode(X, Y)
            Exit Sub
        End If

    End Sub

    Private Sub DrawSelectBox(ByVal X As Integer, ByVal Y As Integer)

        Dim DX, DY As Integer
        Dim PX, PY As Integer

        Dim p As New System.Drawing.Pen(Color.Red)
        Dim g As System.Drawing.Graphics
        p.DashStyle = Drawing2D.DashStyle.Dash
        g = CreateGraphics()
        If Not BitmapBuffer Is Nothing Then g.DrawImageUnscaled(BitmapBuffer, 0, 0)
        DX = X - AuxXInt
        DY = Y - AuxYInt
        PX = AuxXInt
        PY = AuxYInt
        If X < AuxXInt Then
            DX = AuxXInt - X
            PX = X
        End If
        If Y < AuxYInt Then
            DY = AuxYInt - Y
            PY = Y
        End If
        g.DrawRectangle(p, New Rectangle(PX, PY, DX, DY))

        p.Dispose()
        g.Dispose()

    End Sub

    Private Sub DrawExcludeBox(ByVal X As Integer, ByVal Y As Integer)

        Dim DX, DY As Integer
        Dim PX, PY As Integer

        Dim p As New System.Drawing.Pen(Color.Black)
        Dim g As System.Drawing.Graphics
        p.Width = 2
        g = CreateGraphics()
        If Not BitmapBuffer Is Nothing Then g.DrawImageUnscaled(BitmapBuffer, 0, 0)
        DX = X - AuxXInt
        DY = Y - AuxYInt
        PX = AuxXInt
        PY = AuxYInt
        If X < AuxXInt Then
            DX = AuxXInt - X
            PX = X
        End If
        If Y < AuxYInt Then
            DY = AuxYInt - Y
            PY = Y
        End If
        g.DrawRectangle(p, New Rectangle(PX, PY, DX, DY))

        p.Dispose()
        g.Dispose()

    End Sub
    Private Sub ShowLatLon(ByVal X1 As Integer, ByVal Y1 As Integer)

        Dim Lon, Lat As Double

        Dim X, Y As Double
        Dim J, K As Integer
        Dim A As String


        Lon = CDbl(LonDispWest + X1 / PixelsPerLonDeg)
        Lat = CDbl(LatDispNorth - Y1 / PixelsPerLatDeg)

        StatusLat.Text = "   Latitude = " & Lat2Str(Lat)
        StatusLon.Text = "   Longitude = " & Lon2Str(Lon)


        If TileVIEW Then
            Dim BaseName As String = "L" & CStr(Zoom)
            J = XTilesFromLon(Lon, Zoom)
            BaseName = BaseName & "X" & CStr(J)
            J = YTilesFromLat(Lat, Zoom)
            BaseName = BaseName & "Y" & CStr(J)
            StatusTile.Text = "Tile = " & BaseName
        Else
            StatusTile.Text = ""
        End If


        X = Lon + 180
        Y = 90 - Lat

        J = Int(X / 30)
        K = Int(Y / 22.5)
        A = "Dir = " & Format(J, "00") & Format(K, "00") & "   File = "
        J = Int(X / 3.75)
        K = Int(Y / 2.825)
        A = A & Format(J, "00") & Format(K, "00")
        StatusDir.Text = A

        StatusQMID.Text = ""
        If QMIDLevel > 1 Then
            SetLatLonDeltas()
            J = Int(X / LongitudeDelta)
            K = Int(Y / LatitudeDelta)
            A = Format(J, "00") & Format(K, "00")
            StatusQMID.Text = "QMID (L = " & QMIDLevel & "  U = " & Format(J, "00") & "  V = " & Format(K, "00") & ") "
        End If

        StatusStrip.Refresh()

    End Sub

    Private Sub FrmStart_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp

        Dim Button As Short = e.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim X As Integer = e.X
        Dim Y As Integer = e.Y

        SetDelay(0)

        If Button = 2 Then InsertON = False

        If PanON Then

            SetDispCenter(AuxXInt - X, AuxYInt - Y)
            AuxXInt = X
            AuxYInt = Y
            MakeBackground()
            RebuildDisplay()

            PanON = False
            SetMouseIcon()
            Exit Sub
        End If

        If ZoomRollON Then
            ZoomRollON = False
            Exit Sub
        End If

        If SelectON Then
            SelectBoxed(X, Y)
            RebuildDisplay()
            SelectON = False
            SetMouseIcon()
            Exit Sub
        End If

        If DrawExclude Then
            DrawExcludeBox(X, Y)
            DrawExclude = False
            FormExclude(X, Y)
            Exit Sub
        End If

        If MoveON Then
            MoveON = False
            FirstMOVE = False
            If LineON And AutoLinePolyJoin Then CheckLineJoins()
            If PolyON And AutoLinePolyJoin Then CheckPolyJoins()
            SetMouseIcon()
            Exit Sub
        End If

        If ObjectTURN Then
            ObjectTURN = False
            SetMouseIcon()
            Exit Sub
        End If

        If ObjectSIZE Then
            ObjectSIZE = False
            SetMouseIcon()
            Exit Sub
        End If

        If LineTURN Then
            LineTURN = False
            SetMouseIcon()
            Exit Sub
        End If

        If LandWaterRasON Then
            LandWaterRasON = False
            SetMouseIcon()
            Exit Sub
        End If

        If ExcludeSizeIndex > 0 Then
            ExcludeSizeIndex = 0
            SetMouseIcon()
            Exit Sub
        End If

        SetMouseIcon()

    End Sub

    Private Sub FrmStart_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel

        Dim X As Integer = e.X
        Dim Y As Integer = e.Y

        If WAIT Then Exit Sub
        If Not ViewON Then Exit Sub

        If CenterOnMouseWheel Then
            SetDispCenter(X - DisplayCenterX, Y - DisplayCenterY)
        End If

        If e.Delta > 0 Then ZoomInOut(1)
        If e.Delta < 0 Then ZoomInOut(2)

        SetDispCenter(0, 0)
        RebuildDisplay()
        If PtLineCounter > 0 Then DrawLineSegment(X, Y)
        If PtPolyCounter > 0 Then DrawPolySegment(X, Y)

    End Sub


    Private Sub MeasureToolMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MeasureToolMenuItem.Click

        If MeasureTool Then
            MeasureTool = False
            MeasureON = False
            MeasureToolMenuItem.Checked = False
        ElseIf ViewON And Not MoveON And Not PanON Then
            If PointerON Then MeasureTool = True
            If LineON And PtLineCounter = 0 Then MeasureTool = True
            If PolyON And PtPolyCounter = 0 Then MeasureTool = True
            If ObjectON Then MeasureTool = True
            If MeasureTool Then MeasureToolMenuItem.Checked = True
        End If
        SetMouseIcon()

    End Sub

    Private Sub InvertSelectionMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles InvertSelectionMenuItem.Click

        SelectInvertLines()
        SelectInvertPolys()
        SelectInvertObjects()
        RebuildDisplay()

    End Sub

    Private Sub ExitMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExitMenuItem.Click
        Close()
    End Sub

    Private Sub Level2MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level2MenuItem.Click

        ' Level2MenuItem.Checked = True
        QMIDLevel = 2
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level3MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level3MenuItem.Click

        ' Level3MenuItem.Checked = True
        QMIDLevel = 3
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level4MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level4MenuItem.Click

        '  Level4MenuItem.Checked = True
        QMIDLevel = 4
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level5MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level5MenuItem.Click

        ' Level5MenuItem.Checked = True
        QMIDLevel = 5
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub


    Private Sub Level6MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level6MenuItem.Click

        'Level6MenuItem.Checked = True
        QMIDLevel = 6
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level7MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level7MenuItem.Click

        ' Level7MenuItem.Checked = True
        QMIDLevel = 7
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level8MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level8MenuItem.Click

        'Level8MenuItem.Checked = True
        QMIDLevel = 8
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level9MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level9MenuItem.Click

        'Level9MenuItem.Checked = True
        QMIDLevel = 9
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level10MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level10MenuItem.Click

        ' Level10MenuItem.Checked = True
        QMIDLevel = 10
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub


    Private Sub Level11MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level11MenuItem.Click

        'Level11MenuItem.Checked = True
        QMIDLevel = 11
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level12MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level12MenuItem.Click

        ' Level12MenuItem.Checked = True
        QMIDLevel = 12
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level13MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level13MenuItem.Click

        ' Level13MenuItem.Checked = True
        QMIDLevel = 13
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level14MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level14MenuItem.Click

        ' Level14MenuItem.Checked = True
        QMIDLevel = 14
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level15MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level15MenuItem.Click

        ' Level15MenuItem.Checked = True
        QMIDLevel = 15
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub


    Private Sub Level16MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level16MenuItem.Click

        QMIDLevel = 16
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level17MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level17MenuItem.Click

        'Level17MenuItem.Checked = True
        QMIDLevel = 17
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level18MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level18MenuItem.Click

        ' Level18MenuItem.Checked = True
        QMIDLevel = 18
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level19MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level19MenuItem.Click

        ' Level19MenuItem.Checked = True
        QMIDLevel = 19
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level20MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level20MenuItem.Click

        'Level20MenuItem.Checked = True
        QMIDLevel = 20
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub


    Private Sub Level21MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level21MenuItem.Click

        'Level21MenuItem.Checked = True
        QMIDLevel = 21
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level22MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level22MenuItem.Click

        ' Level22MenuItem.Checked = True
        QMIDLevel = 22
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level23MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level23MenuItem.Click

        'Level23MenuItem.Checked = True
        QMIDLevel = 23
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level24MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level24MenuItem.Click

        'Level24MenuItem.Checked = True
        QMIDLevel = 24
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level25MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level25MenuItem.Click

        ' Level25MenuItem.Checked = True
        QMIDLevel = 25
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub


    Private Sub Level26MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level26MenuItem.Click

        'Level26MenuItem.Checked = True
        QMIDLevel = 26
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level27MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level27MenuItem.Click

        ' Level27MenuItem.Checked = True
        QMIDLevel = 27
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level28MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level28MenuItem.Click

        ' Level28MenuItem.Checked = True
        QMIDLevel = 28
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub Level29MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Level29MenuItem.Click

        'Level29MenuItem.Checked = True
        QMIDLevel = 29
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub NoGridMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NoGridMenuItem.Click

        ' NoGridMenuItem.Checked = True
        QMIDLevel = 0
        ResetLevelGrid(False)
        SetLatLonDeltas()
        RebuildDisplay()

    End Sub

    Private Sub SetLatLonDeltas()

        LatitudeDelta = 90 / (2 ^ (QMIDLevel - 2))
        LongitudeDelta = 120 / (2 ^ (QMIDLevel - 2))

    End Sub
    Private Sub ShowMeasure(ByVal X1 As Integer, ByVal Y1 As Integer)

        Dim DX, DY As Double
        Dim A As String
        Dim CLat, CLon As Double

        CLat = LatDispNorth - CDbl(((AuxYInt + Y1) / 2) / PixelsPerLatDeg)
        CLon = CDbl(((AuxXInt + X1) / 2) / PixelsPerLonDeg) + LonDispWest

        DX = CDbl((X1 - AuxXInt) / PixelsPerLonDeg)     ' delta X in degrees of longitude
        DX = DX * MetersPerDegLon(CLat)                 ' in meters

        DY = CDbl((AuxYInt - Y1) / PixelsPerLatDeg)
        DY = DY * MetersPerDegLat


        If DX > -0.0001 And DX < 0.0001 Then
            If DY >= 0 Then ObjMHead = 0
            If DY < 0 Then ObjMHead = 180
        Else
            ObjMHead = System.Math.Atan(DY / DX) * (180.0# / PI)
            If DX > 0 Then
                ObjMHead = 90 - ObjMHead
            Else
                ObjMHead = 270 - ObjMHead
            End If
        End If

        DX = DX * DX + DY * DY
        DX = System.Math.Sqrt(DX)

        If MeasuringMeters Then
            A = "Lenght= " & Format(DX, "0.000") & " m"
        Else
            DX = DX * 3.2808
            A = "Lenght= " & Format(DX, "0.000") & " ft"
        End If
        A = A & vbCrLf & "Heading = " & Format(ObjMHead, "0.000") & " deg"
        A = A & vbCrLf & vbCrLf & "Lat. = " & Lat2Str(CLat) & vbCrLf & "Lon. = " & Lon2Str(CLon)

        Dim g As System.Drawing.Graphics
        Dim p As New System.Drawing.Pen(DefaultLineColor)

        g = CreateGraphics()
        If Not BitmapBuffer Is Nothing Then g.DrawImageUnscaled(BitmapBuffer, 0, 0)
        g.DrawLine(p, AuxXInt, AuxYInt, X1, Y1)
        p.Color = UnselectedPointColor
        g.DrawEllipse(p, AuxXInt - 3, AuxYInt - 3, 6, 6)

        g.DrawEllipse(p, X1 - 3, Y1 - 3, 6, 6)

        X1 = CInt((X1 + AuxXInt) / 2)
        Y1 = CInt((Y1 + AuxYInt) / 2)

        g.DrawEllipse(p, X1 - 3, Y1 - 3, 6, 6)

        TextBoxMeasure.Text = A
        TextBoxMeasure.Visible = True
        TextBoxMeasure.Refresh()

        If ObjectON Then ObjMYes = True

        g.Dispose()
        p.Dispose()

    End Sub


    Private Sub DrawLineLabel(ByVal X As Integer, ByVal Y As Integer, ByVal N As Integer)


        Dim A As String

        If N > 0 Then
            If Lines(N).Name = "" Then
                A = "Line #" & Str(N)
            Else
                A = Lines(N).Name
            End If

            Dim g As System.Drawing.Graphics
            g = CreateGraphics()

            If Not BitmapBuffer Is Nothing Then g.DrawImageUnscaled(BitmapBuffer, 0, 0)

            Dim drawFont As New Font("Arial", 10)

            g.FillRectangle(Brushes.Beige, New Rectangle(X - 3, Y - 20, Len(A) * 7, 18))
            g.DrawString(A, drawFont, Brushes.Black, X, Y - 20)

            drawFont.Dispose()
            g.Dispose()

        End If

    End Sub

    Private Sub DrawPolyLabel(ByVal X As Integer, ByVal Y As Integer, ByVal N As Integer, ByVal M As Integer)

        Dim A As String

        If N > 0 Then
            If Polys(N).Name = "" Then
                A = "Poly #" & Str(N)
            Else
                A = Polys(N).Name
            End If

            If M > 0 Then
                A = "Pt#" & M & " Alt = " & Format(Polys(N).GPoints(M).alt, "0.00")
            End If

            Dim g As System.Drawing.Graphics
            g = CreateGraphics()

            If Not BitmapBuffer Is Nothing Then g.DrawImageUnscaled(BitmapBuffer, 0, 0)

            Dim drawFont As New Font("Arial", 10)

            g.FillRectangle(Brushes.Beige, New Rectangle(X - 3, Y - 20, Len(A) * 7, 18))
            g.DrawString(A, drawFont, Brushes.Black, X, Y - 20)

            drawFont.Dispose()
            g.Dispose()

        End If

    End Sub

    Private Sub DrawParentSelectLabel(ByVal X As Integer, ByVal Y As Integer)

        Dim A As String = "Click to Select Parent Poly"
        Dim g As System.Drawing.Graphics
        g = CreateGraphics()
        If Not BitmapBuffer Is Nothing Then g.DrawImageUnscaled(BitmapBuffer, 0, 0)
        Dim drawFont As New Font("Arial", 10)
        g.FillRectangle(Brushes.Beige, New Rectangle(X - 3, Y - 20, Len(A) * 6 + 3, 18))
        g.DrawString(A, drawFont, Brushes.Black, X, Y - 20)
        drawFont.Dispose()
        g.Dispose()

    End Sub

    Private Sub DrawObjectLabel(ByVal X As Integer, ByVal Y As Integer, ByVal N As Integer)

        Dim A As String

        If N > 0 Then

            A = "Object #" & Str(N)

            Dim g As System.Drawing.Graphics
            g = CreateGraphics()

            If Not BitmapBuffer Is Nothing Then g.DrawImageUnscaled(BitmapBuffer, 0, 0)

            Dim drawFont As New Font("Arial", 10)

            g.FillRectangle(Brushes.Beige, New Rectangle(X - 3, Y - 20, Len(A) * 7, 18))
            g.DrawString(A, drawFont, Brushes.Black, X, Y - 20)

            drawFont.Dispose()
            g.Dispose()

        End If

    End Sub


    Private Sub DrawLineSegment(ByVal X As Integer, ByVal Y As Integer)

        Dim X0, Y0 As Integer

        X0 = CInt((AuxLonLine - LonDispWest) * PixelsPerLonDeg)
        Y0 = CInt((LatDispNorth - AuxLatLine) * PixelsPerLatDeg)


        Dim g As System.Drawing.Graphics
        Dim p As New System.Drawing.Pen(DefaultLineColor) With {
            .Width = LinePenWidth
        }
        g = CreateGraphics()

        If Not BitmapBuffer Is Nothing Then g.DrawImageUnscaled(BitmapBuffer, 0, 0)

        If PtLineCounter > 2 Then DrawNewLine(g)

        g.DrawLine(p, X0, Y0, X, Y)

        p.Color = UnselectedPointColor
        g.DrawRectangle(p, X0 - 3, Y0 - 3, 6, 6)

        p.Dispose()
        g.Dispose()

    End Sub

    Private Sub DrawNewLine(ByVal gr As Graphics)

        Dim K As Integer
        Dim PX0, PY0, PX1, PY1 As Integer

        Dim P1, P2 As Integer  ' to draw the points
        P1 = 2
        If LinePenWidth = 2 Then P1 = 3
        P2 = 2 * P1

        Dim myPen As New System.Drawing.Pen(DefaultLineColor, LinePenWidth)
        Dim myBrush As New System.Drawing.SolidBrush(UnselectedPointColor)

        PX1 = CInt((NewLine.GLPoints(1).lon - LonDispWest) * PixelsPerLonDeg)
        PY1 = CInt((LatDispNorth - NewLine.GLPoints(1).lat) * PixelsPerLatDeg)

        For K = 2 To NewLine.NoOfPoints
            PX0 = PX1
            PY0 = PY1
            PX1 = CInt((NewLine.GLPoints(K).lon - LonDispWest) * PixelsPerLonDeg)
            PY1 = CInt((LatDispNorth - NewLine.GLPoints(K).lat) * PixelsPerLatDeg)
            gr.DrawLine(myPen, PX0, PY0, PX1, PY1)
        Next K

        ' now draw point
        For K = 1 To NewLine.NoOfPoints - 1
            PX0 = CInt((NewLine.GLPoints(K).lon - LonDispWest) * PixelsPerLonDeg)
            PY0 = CInt((LatDispNorth - NewLine.GLPoints(K).lat) * PixelsPerLatDeg)
            gr.FillRectangle(myBrush, PX0 - P1, PY0 - P1, P2, P2)
        Next K

        myPen.Dispose()
        myBrush.Dispose()

    End Sub

    Private Sub DrawPolySegment(ByVal X As Integer, ByVal Y As Integer)

        'AddGridsToBitmapBuffer()

        Dim K, X0, Y0 As Integer

        Dim P1, P2 As Integer  ' to draw the points
        P1 = 2
        If PolyPenWidth = 2 Then P1 = 3
        P2 = 2 * P1

        X0 = CInt((AuxLonPoly - LonDispWest) * PixelsPerLonDeg)
        Y0 = CInt((LatDispNorth - AuxLatPoly) * PixelsPerLatDeg)

        Dim g As System.Drawing.Graphics
        Dim myPen As New System.Drawing.Pen(PolyColorBorder, PolyPenWidth)
        Dim myBrush As New System.Drawing.SolidBrush(DefaultPolyColor)
        g = CreateGraphics()

        If Not BitmapBuffer Is Nothing Then g.DrawImageUnscaled(BitmapBuffer, 0, 0)

        If PtPolyCounter > 2 Then

            ReDim PTS(0 To NewPoly.NoOfPoints)

            For K = 0 To NewPoly.NoOfPoints - 1
                PTS(K).X = CInt((NewPoly.GPoints(K + 1).lon - LonDispWest) * PixelsPerLonDeg)
                PTS(K).Y = CInt((LatDispNorth - NewPoly.GPoints(K + 1).lat) * PixelsPerLatDeg)
            Next
            PTS(NewPoly.NoOfPoints).X = X
            PTS(NewPoly.NoOfPoints).Y = Y

            g.FillPolygon(myBrush, PTS)
            g.DrawPolygon(myPen, PTS)

            ' now draw points
            myBrush.Color = UnselectedPointColor
            For K = 0 To NewPoly.NoOfPoints - 1
                g.FillRectangle(myBrush, PTS(K).X - P1, PTS(K).Y - P1, P2, P2)
            Next K

        Else
            myBrush.Color = UnselectedPointColor
            g.DrawLine(myPen, X0, Y0, X, Y)
            g.FillRectangle(myBrush, X - P1, Y - P1, P2, P2)
            g.FillRectangle(myBrush, X0 - P1, Y0 - P1, P2, P2)
        End If

        myPen.Dispose()
        myBrush.Dispose()
        g.Dispose()

    End Sub



    Private Sub DeleteMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteMenuItem.Click

        Dim X, N As Integer


        N = NoOfPointsSelected + NoOfLinesSelected + NoOfPolysSelected
        If N > 0 Then
            If AskDelete Then
                X = MsgBox("Delete " & Str(N) & " item(s) ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, AppTitle)
            Else
                X = MsgBoxResult.Yes
            End If
            If X = MsgBoxResult.Yes Then
                BackUp()
                SkipBackUp = True
                DeleteSelected()
                SkipBackUp = False
                RebuildDisplay()
                Exit Sub
            Else
                Exit Sub
            End If
        End If

    End Sub

    Private Sub Timer1_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles Timer1.Elapsed

        DELAY = False
        Timer1.Enabled = False

    End Sub


    Private Sub RecentFile1MenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RecentFile1MenuItem.Click

        Dim FileName As String

        If Dirty Then
            MsgBox("You did not save your data! Continue ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2)
            If MsgBoxResult.Yes Then ' User chose Yes.
            Else ' User chose No.
                Exit Sub
            End If
        End If

        FileName = RecentFiles(1)
        If FileName = "" Then Exit Sub
        FileOpenHeader()
        OpenFile(FileName)
        WorkFile = FileName
        FileOpenTrailer()

    End Sub

    Private Sub RecentFile2MenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RecentFile2MenuItem.Click

        Dim FileName As String

        If Dirty Then
            MsgBox("You did not save your data! Continue ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2)
            If MsgBoxResult.Yes Then ' User chose Yes.
            Else ' User chose No.
                Exit Sub
            End If
        End If

        FileName = RecentFiles(2)
        FileOpenHeader()
        OpenFile(FileName)
        If FileName = "" Then Exit Sub
        WorkFile = FileName
        FileOpenTrailer()

    End Sub

    Private Sub RecentFile3MenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RecentFile3MenuItem.Click

        Dim FileName As String

        If Dirty Then
            MsgBox("You did not save your data! Continue ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2)
            If MsgBoxResult.Yes Then ' User chose Yes.
            Else ' User chose No.
                Exit Sub
            End If
        End If

        FileName = RecentFiles(3)
        If FileName = "" Then Exit Sub
        FileOpenHeader()
        OpenFile(FileName)
        WorkFile = FileName
        FileOpenTrailer()

    End Sub

    Private Sub RecentFile4MenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RecentFile4MenuItem.Click

        Dim FileName As String

        If Dirty Then
            MsgBox("You did not save your data! Continue ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2)
            If MsgBoxResult.Yes Then ' User chose Yes.
            Else ' User chose No.
                Exit Sub
            End If
        End If

        FileName = RecentFiles(4)
        If FileName = "" Then Exit Sub
        FileOpenHeader()
        OpenFile(FileName)
        WorkFile = FileName
        FileOpenTrailer()

    End Sub

    Private Sub FallMapMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FallMapMenuItem.Click

        Season = "Fall"
        SetBitmapSeason()
        ViewON = True
        RebuildDisplay()

    End Sub

    Private Sub HardWinterMapMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles HardWinterMapMenuItem.Click

        Season = "Hard"
        SetBitmapSeason()
        ViewON = True
        RebuildDisplay()

    End Sub

    Private Sub MeshMapMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Season = "Mesh"
        SetBitmapSeason()
        ViewON = True
        RebuildDisplay()

    End Sub

    Private Sub SpringMapMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SpringMapMenuItem.Click

        Season = "Spring"
        SetBitmapSeason()
        ViewON = True
        RebuildDisplay()

    End Sub

    Private Sub AlphaMapMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Season = "Alpha"
        SetBitmapSeason()
        ViewON = True
        RebuildDisplay()

    End Sub

    Private Sub SummerMapMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SummerMapMenuItem.Click

        Season = "Summer"
        SetBitmapSeason()
        ViewON = True
        RebuildDisplay()

    End Sub

    Private Sub WinterMapMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles WinterMapMenuItem.Click

        Season = "Winter"
        SetBitmapSeason()
        ViewON = True
        RebuildDisplay()

    End Sub

    Private Sub NightMapMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NightMapMenuItem.Click

        Season = "Night"
        SetBitmapSeason()
        ViewON = True
        RebuildDisplay()

    End Sub

    Private Sub ClassMapMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Season = "Class"
        SetBitmapSeason()
        ViewON = True
        RebuildDisplay()

    End Sub

    Private Sub CenterPopUPMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CenterPopUPMenu.Click

        SetDispCenter(XPOP - DisplayCenterX, YPOP - DisplayCenterY)
        RebuildDisplay()

    End Sub

    Private Sub ZoomInPopUPMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZoomInPopUPMenu.Click

        SetDispCenter(XPOP - DisplayCenterX, YPOP - DisplayCenterY)
        ZoomInOut(1)
        SetDispCenter(0, 0)
        RebuildDisplay()

    End Sub

    Private Sub ZoomOutPopUPMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZoomOutPopUPMenu.Click

        SetDispCenter(XPOP - DisplayCenterX, YPOP - DisplayCenterY)
        ZoomInOut(2)
        SetDispCenter(0, 0)
        RebuildDisplay()

    End Sub

    Private Sub DeletePopUPMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeletePopUPMenu.Click

        Dim X As Single

        If POPType = "PtInLine" Then
            X = MsgBox("Delete this Point from this Line ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, AppTitle)
            If X = MsgBoxResult.Yes Then
                DeletePointInLine(POPIndex, POPIndexPT)
                RebuildDisplay()
            Else
                Exit Sub
            End If
        End If

        If POPType = "PtInPoly" Then
            X = MsgBox("Delete this Point from this Poly ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, AppTitle)
            If X = MsgBoxResult.Yes Then
                If Polys(POPIndex).NoOfPoints < 3 Then
                    DeletePoly(POPIndex)
                    Dirty = True
                Else
                    DeletePointInPoly(POPIndex, POPIndexPT)
                End If
                RebuildDisplay()
            Else
                Exit Sub
            End If
        End If


        If POPType = "Line" Then
            X = MsgBox("Delete this Line ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, AppTitle)
            If X = MsgBoxResult.Yes Then
                DeleteLine(POPIndex)
                RebuildDisplay()
            Else
                Exit Sub
            End If
        End If

        If POPType = "Poly" Then
            X = MsgBox("Delete this Poly ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, AppTitle)
            If X = MsgBoxResult.Yes Then
                DeletePoly(POPIndex)
                RebuildDisplay()
            Else
                Exit Sub
            End If
        End If


        If POPType = "Exclude" Then
            X = MsgBox("Delete this Exclude ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, AppTitle)
            If X = MsgBoxResult.Yes Then
                DeleteExclude(POPIndex)
                RebuildDisplay()
            Else
                Exit Sub
            End If
        End If

        If POPType = "Land" Then
            X = MsgBox("Delete this Land Class ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, AppTitle)
            If X = MsgBoxResult.Yes Then
                DeleteLandPopUp(POPIndex)
                RebuildDisplay()
            Else
                Exit Sub
            End If
        End If

        If POPType = "Water" Then
            X = MsgBox("Delete this Water Class ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, AppTitle)
            If X = MsgBoxResult.Yes Then
                DeleteWaterPopUp(POPIndex)
                RebuildDisplay()
            Else
                Exit Sub
            End If
        End If


        If POPType = "Object" Then
            X = MsgBox("Delete this Object ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, AppTitle)
            If X = MsgBoxResult.Yes Then
                DeleteThisObject(POPIndex)
                RebuildDisplay()
            Else
                Exit Sub
            End If
        End If

        If POPType = "Map" Then
            X = MsgBox("Delete this Map ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, AppTitle)
            If X = MsgBoxResult.Yes Then
                DeleteMap(POPIndex)
                RebuildDisplay()
            Else
                Exit Sub
            End If
        End If


    End Sub

    Private Sub MakeLinePopUPMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MakeLinePopUPMenu.Click

        Dim L, N, K, P As Integer

        BackUp()

        P = POPIndex

        L = NoOfLines + 1
        NoOfLines = L
        ReDim Preserve Lines(L)

        N = Polys(P).NoOfPoints

        Lines(L).NoOfPoints = N
        If MakeClosedLineFromPoly Then Lines(L).NoOfPoints = N + 1

        ReDim Lines(L).GLPoints(Lines(L).NoOfPoints)

        Lines(L).Color = DefaultLineColor

        For K = 1 To N
            Lines(L).GLPoints(K).lat = Polys(P).GPoints(N - K + 1).lat
            Lines(L).GLPoints(K).lon = Polys(P).GPoints(N - K + 1).lon
        Next K

        If MakeClosedLineFromPoly Then Lines(L).GLPoints(N + 1) = Lines(L).GLPoints(1)

        AddLatLonToLine(L)
        If LineVIEW = False Then ViewAllLinesMenuItem_Click(ViewAllLinesMenuItem, New System.EventArgs())
        RebuildDisplay()

    End Sub

    Private Sub MakePolyPopUPMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MakePolyPopUPMenu.Click

        Dim L, N, K, P As Integer

        BackUp()

        P = POPIndex

        L = NoOfPolys + 1
        NoOfPolys = L
        ReDim Preserve Polys(L)

        N = Lines(P).NoOfPoints

        Polys(L).NoOfPoints = N

        ReDim Polys(L).GPoints(Polys(L).NoOfPoints)
        Polys(L).Color = DefaultPolyColor

        For K = 1 To N
            Polys(L).GPoints(K).lat = Lines(P).GLPoints(K).lat
            Polys(L).GPoints(K).lon = Lines(P).GLPoints(K).lon
        Next K

        AddLatLonToPoly(L)
        If PolyVIEW = False Then ViewAllPolysMenuItem_Click(ViewAllPolysMenuItem, New System.EventArgs())
        RebuildDisplay()

    End Sub

    Private Sub ConvertToPolyPopUpMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConvertToPolyPopUpMenu.Click

        BackUp()
        SkipBackUp = True
        MakePolyFromLine(POPIndex)
        DeleteLine(POPIndex)
        SkipBackUp = False
        PolyToolStripButton_Click(PolyToolStripButton, New System.EventArgs())


    End Sub


    Private Sub SmoothPopUPMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SmoothPopUPMenu.Click

        FrmLPSmooth.ShowDialog()

    End Sub


    Private Sub SamplePopUPMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SamplePopUPMenu.Click

        FrmLPSample.ShowDialog()

    End Sub

    Private Sub SetAltitudePopUpMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SetAltitudePopUpMenu.Click

        If POPType = "Line" Then
            FrmAltitudeLine.ShowDialog()
        End If

        If POPType = "Poly" Then
            FrmAltitudePoly.ShowDialog()
        End If

    End Sub

    Private Sub SetWidthPopUpMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SetWidthPopUpMenu.Click

        FrmLWidth.ShowDialog()

    End Sub


    Private Sub ManualCheckPopUPMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ManualCheckPopUPMenu.Click

        Dim A As String

        A = "Use Up and Down Arrows to scroll" + vbCrLf
        A = A & "through Points. Press Delete as" + vbCrLf
        A = A & "required. Scroll through Lines" + vbCrLf
        A = A & "or Polys with Right and Left Arrows." + vbCrLf
        A = A & "Press <Esc> to exit."

        MsgBox(A, MsgBoxStyle.Information, "SBuilderX - Checking Lines or Polys")

        If POPType = "Line" Then
            UnSelectAll()
            CheckPoly = 0
            CheckLine = POPIndex
            CheckLinePt = 1
            Lines(POPIndex).GLPoints(1).Selected = True
            LatDispCenter = Lines(POPIndex).GLPoints(1).lat
            LonDispCenter = Lines(POPIndex).GLPoints(1).lon
            SetDispCenter(0, 0)
            RebuildDisplay()
        End If

        If POPType = "Poly" Then
            UnSelectAll()
            CheckLine = 0
            CheckPoly = POPIndex
            CheckPolyPt = 1
            Polys(POPIndex).GPoints(1).Selected = True
            LatDispCenter = Polys(POPIndex).GPoints(1).lat
            LonDispCenter = Polys(POPIndex).GPoints(1).lon
            SetDispCenter(0, 0)
            RebuildDisplay()
        End If

    End Sub

    Friend Sub CalibratePopUPMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CalibratePopUPMenu.Click

        frmCalibrate.ShowDialog()

    End Sub

    Private Sub PropertiesPopUPMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PropertiesPopUPMenu.Click

        If POPType = "Map" Then
            FrmMapsP.ShowDialog()
            Exit Sub
        End If

        If POPType = "Project" Then
            FrmProjectP.ShowDialog()
            Exit Sub
        End If

        If POPType = "Line" Then
            FrmLinesP.ShowDialog()
            Exit Sub
        End If

        If POPType = "Poly" Then
            FrmPolysP.ShowDialog()
            Exit Sub
        End If

        If POPType = "PtInLine" Then
            FrmLPPointsP.ShowDialog()
            Exit Sub
        End If

        If POPType = "PtInPoly" Then
            FrmLPPointsP.ShowDialog()
            Exit Sub
        End If

        If POPType = "Exclude" Then
            FrmExcludesP.ShowDialog()
            Exit Sub
        End If

        If POPType = "Object" Then

            FrmObjectsP.ShowObjectProperties(POPIndex)
            FrmObjectsP.ShowDialog()

            Exit Sub
        End If

        'If POPType = "Land" Then
        '    frmLandsP.Show()
        '    'frmLandsP.ShowLandProperty(POPIndex)
        '    Exit Sub
        'End If

        'If POPType = "Water" Then
        '    frmWatersP.Show()
        '    'frmWatersP.ShowWaterProperty(POPIndex)
        '    Exit Sub
        'End If


    End Sub

    Private Sub FromDiskMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FromDiskMenuItem.Click

        MapVIEW = False
        ViewAllMapsMenuItem_Click(ViewAllMapsMenuItem, New System.EventArgs())
        AddNewMap()
        Cursor = Cursors.Default

    End Sub


    Private Sub ExportSBXMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExportSBXMenuItem.Click

        Dim a, b As String

        If NoOfLines = 0 And NoOfPolys = 0 Then
            If NoOfExcludes And NoOfObjects = 0 Then
                If NoOfLLXYs = 0 And NoOfWWXYs = 0 Then
                    If NoOfMaps = 0 And NoOfLWCIs = 0 Then
                        MsgBox("There are no items to export!", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                End If
            End If
        End If

        a = "SBuilderX Imp/Exp (*.SBX)|*.SBX"
        b = "SBuilderX - Export Project As"

        a = FileNameToSave(a, b, "SBX")
        If a = "" Then
            Exit Sub
        End If

        'WorkFile = a$
        ExportSBX(a)
        Dirty = False

    End Sub

    Private Sub ImportSBXMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImportSBXMenuItem.Click

        Dim b, a, FileName As String
        Dim x As Integer

        lbDonation.Visible = False

        If Dirty Then
            x = MsgBox("You did not save your data! Continue ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2)
            If x = MsgBoxResult.Yes Then ' User chose Yes.
            Else ' User chose No.
                Exit Sub
            End If
        End If

        a = "SBuilderX Imp/Exp (*.SBX)|*.SBX"
        b = "SBuilderX - Import Project"

        FileName = FileNameToOpen(a, b, "SBX")
        If FileName = "" Then Exit Sub

        FileOpenHeader()
        ImportSBX(FileName)
        WorkFile = Path.GetFileNameWithoutExtension(FileName) & ".SBP"

        FileOpenTrailer()
        'MsgBox("Save file will be = " & WorkFile)
        Dirty = False

    End Sub

    Private Sub AppendSBXMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AppendSBXMenuItem.Click

        Dim a, b As String

        a = "SBuilderX Imp/Exp (*.SBX)|*.SBX"
        b = "SBuilderX - Append Project"

        a = FileNameToOpen(a, b, "SBX")

        If a = "" Then
            Exit Sub
        End If

        AppendSBX(a)
        FileOpenTrailer()
        Dirty = True

    End Sub

    Private Sub ExportSHPMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExportSHPMenuItem.Click

        Dim a, b As String


        If NoOfLines = 0 And NoOfPolys = 0 Then
            MsgBox("There is no line or polygon to export!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If NoOfLines > 0 And NoOfPolys > 0 Then
            MsgBox("Lines and Polys will be saved in 2 separated files!", MsgBoxStyle.Information)
        End If

        If NoOfLines > 0 Then
            a = "Esri Shape file (*.SHP)|*.SHP"
            b = "SBuilderX: Export LINES As"
            a = FileNameToSave(a, b, "SHP")
            If a <> "" Then
                ExportSHPLines(a)
            End If
        End If

        If NoOfPolys > 0 Then
            a = "Esri Shape file (*.SHP)|*.SHP"
            b = "SBuilderX: Export POLYGONS As"
            a = FileNameToSave(a, b, "SHP")
            If a <> "" Then
                ExportSHPPolys(a)
            End If
        End If

    End Sub

    Private Sub AppendSHPMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AppendSHPMenuItem.Click

        Dim A, B As String

        A = "Esri Shape file (*.SHP)|*.SHP"
        B = "SBuilderX: Append ShapeFile"

        A = FileNameToOpen(A, B, "SHP")

        If A = "" Then
            Exit Sub
        End If

        AppendSHPFile(A)
        FileOpenTrailer()
        Dirty = True

    End Sub

    Private Sub AppendObjMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AppendObjMenuItem.Click

        Dim A, B, C As String

        A = "Object BGL file (*.BGL)|*.bgl"
        A = A & "|Object XML file (*.XML)|*.xml"
        B = "SBuilderX: Append Object file"

        A = FileNameToOpen(A, B, "OBJ")

        If A = "" Then
            Exit Sub
        End If

        B = Path.GetExtension(A).Substring(1).ToUpper

        Try

            If B = "BGL" Then
                C = A & ".XML"
                Dim SceneryFile As New SceneryFile(A, FileType.BGL)   ' was = then I placed As
                SceneryFile.Bgl2Xml(C)
                SceneryFile.Dispose()
                AppendOBJFile(C)
            ElseIf B = "XML" Then
                AppendOBJFile(A)
            Else
                MsgBox("Wrong File Extension", MsgBoxStyle.Critical)
            End If

            FileOpenTrailer()
            Dirty = True

        Catch ex As Exception
            MsgBox("SBuilderX can not decompile file " & A, MsgBoxStyle.Critical)
        End Try



    End Sub


    Private Sub SetLineTypes()

        Dim A, B, C, D, File As String
        Dim Marker, N, K As Integer

        ReDim LineTypes(2000)

        On Error GoTo erro1

        File = My.Application.Info.DirectoryPath & "\Tools\Lines.txt"
        FileOpen(2, File, OpenMode.Input)
        N = LOF(2)
        Marker = 0
        K = 0
        Do While Marker < N
            A = LineInput(2)
            Marker = Marker + Len(A) + 2
            B = Trim(Mid(A, 1, 4))

            If B = "[Tex" Then
                K = K + 1
                C = Trim(Mid(A, 10))
                C = Replace(C, "]", "")
                LineTypes(K).TerrainIndex = C
            End If

            If B = "Name" Then
                C = Trim(Mid(A, 6))
                LineTypes(K).Name = C
            End If

            If B = "Colo" Then
                C = Trim(Mid(A, 7))
                LineTypes(K).Color = Color.FromArgb(Convert.ToInt32(C, 16))
            End If

            If B = "Text" Then
                C = Trim(Mid(A, 10))
                LineTypes(K).Texture = C
            End If

            If B = "Guid" Then
                C = Trim(Mid(A, 6))
                LineTypes(K).Guid = C
            End If

            If B = "Type" Then
                C = Trim(Mid(A, 6, 3)) '''' skip legacy
                LineTypes(K).Type = C
            End If

        Loop

        FileClose()

        DefaultLineNoneGuid = LineTypes(1).Guid
        DefaultLineFS9Guid = LineTypes(2).Guid

        NoOfLineTypes = K
        ReDim Preserve LineTypes(K)

        Exit Sub

erro1:
        MsgBox("Check your Lines.txt file!", MsgBoxStyle.Critical)

    End Sub

    Private Sub SetPolyTypes()

        Dim A, B, C, D, File As String
        Dim Marker, N, K As Integer

        On Error GoTo erro1

        ReDim PolyTypes(2000)

        File = My.Application.Info.DirectoryPath & "\tools\Polys.txt"
        FileOpen(2, File, OpenMode.Input)
        N = LOF(2)
        Marker = 0
        K = 0
        Do While Marker < N
            A = LineInput(2)
            Marker = Marker + Len(A) + 2
            B = UCase(Trim(Mid(A, 1, 4)))
            If B = "[TEX" Then
                K = K + 1
                C = Trim(Mid(A, 10))
                C = Replace(C, "]", "")
                PolyTypes(K).TerrainIndex = C
            End If

            If B = "NAME" Then
                C = Trim(Mid(A, 6))
                PolyTypes(K).Name = C
            End If

            If B = "COLO" Then
                C = Trim(Mid(A, 7))
                PolyTypes(K).Color = Color.FromArgb(Convert.ToInt32(C, 16))
            End If

            If B = "TEXT" Then
                C = Trim(Mid(A, 10))
                PolyTypes(K).Texture = C
            End If

            If B = "GUID" Then
                C = Trim(Mid(A, 6))
                PolyTypes(K).Guid = C
            End If

            If B = "TYPE" Then
                C = Trim(Mid(A, 6, 3)) '''' landclasses
                'C = Mid(C, 1, 3)
                PolyTypes(K).Type = C
            End If
        Loop

        FileClose()

        DefaultPolyNoneGuid = PolyTypes(1).Guid
        DefaultPolyFS9Guid = PolyTypes(2).Guid
        DefaultPolyGPSGuid = PolyTypes(3).Guid

        NoOfPolyTypes = K
        ReDim Preserve PolyTypes(K)

        Exit Sub
erro1:
        MsgBox("Check your Polys.txt file!", MsgBoxStyle.Critical)

    End Sub

    Private Sub SetLandTypes()

        Dim A, B, C, File As String
        Dim N, Marker, K As Integer

        On Error GoTo erro

        'ReDim LC(255) we do that in the declaration

        File = My.Application.Info.DirectoryPath & "\tools\lands.txt"
        FileOpen(2, File, OpenMode.Input)
        N = LOF(2)
        Marker = 0
        K = 0
        Do While Marker < N
            A = LineInput(2)
            Marker = Marker + Len(A) + 2

            B = UCase(Trim(Mid(A, 1, 4)))
            If B = "NAME" Then
                K = K + 1
                C = Trim(Mid(A, 6))
                LC(K).Index = CInt(Mid(C, 1, 3))
                LC(K).Caption = C
            End If

            If B = "TEXT" Then
                C = Trim(Mid(A, 10))
                LC(K).Texture = C
            End If

            If B = "COLO" Then
                C = Trim(Mid(A, 7))
                LC(K).Color = Color.FromArgb(Convert.ToInt32(C, 16))
            End If
        Loop
        FileClose()

        NoOfLCs = K

        B = "sel"
        For K = 1 To NoOfLCs
            LC(K + 128).Color = Color.FromArgb(255, 0, 255, 0)
            LC(K + 128).Index = LC(K).Index
            ILC(LC(K).Index) = K   ' added in March 2009
            LC(K + 128).Texture = B
        Next K

        Exit Sub

erro:

        MsgBox("Check your Lands.txt file!", MsgBoxStyle.Critical)

    End Sub

    Private Sub SetWaterTypes()

        Dim A, B, C, File As String
        Dim N, Marker, K As Integer

        On Error GoTo erro

        ReDim WC(255)

        File = My.Application.Info.DirectoryPath & "\tools\Waters.txt"
        FileOpen(2, File, OpenMode.Input)
        N = LOF(2)
        Marker = 0
        K = 0
        Do While Marker < N
            A = LineInput(2)
            Marker = Marker + Len(A) + 2

            B = UCase(Trim(Mid(A, 1, 4)))
            If B = "NAME" Then
                K = K + 1
                C = Trim(Mid(A, 6))
                WC(K).Index = CInt(Mid(C, 1, 3))
                WC(K).Caption = C
            End If

            If B = "TEXT" Then
                C = Trim(Mid(A, 10))
                WC(K).Texture = C
            End If

            If B = "COLO" Then
                C = Trim(Mid(A, 7))
                WC(K).Color = Color.FromArgb(Convert.ToInt32(C, 16))
            End If
        Loop
        FileClose()

        NoOfWCs = K

        B = "sel"
        For K = 1 To NoOfLCs
            WC(K + 128).Color = Color.FromArgb(255, 0, 255, 0)
            WC(K + 128).Index = WC(K).Index
            IWC(WC(K).Index) = K   ' added in March 2009
            WC(K + 128).Texture = B
        Next K

        Exit Sub

erro:

        MsgBox("Check your Waters.txt file!", MsgBoxStyle.Critical)

    End Sub

    Private Sub SetExtrusionsTypes()

        Dim A, B, C, File As String
        Dim N, Marker, K As Integer

        On Error GoTo erro

        ReDim ExtrusionTypes(255)

        File = My.Application.Info.DirectoryPath & "\Tools\Bridges.txt"
        FileOpen(2, File, OpenMode.Input)
        N = LOF(2)
        Marker = 0
        K = 0
        Do While Marker < N
            A = LineInput(2)
            Marker = Marker + Len(A) + 2

            B = UCase(Trim(Mid(A, 1, 3)))
            If B = "NAM" Then
                K = K + 1
                ExtrusionTypes(K).Name = Trim(A.Substring(5))
            End If

            If B = "COL" Then
                ExtrusionTypes(K).Color = ColorFromArgb(Trim(A.Substring(6)))
            End If

            If B = "WID" Then
                ExtrusionTypes(K).Width = Val(Trim(A.Substring(6)))
            End If

            If B = "PRO" Then
                ExtrusionTypes(K).Profile = Trim(A.Substring(8))
            End If

            If B = "MAT" Then
                ExtrusionTypes(K).Material = Trim(A.Substring(9))
            End If

            If B = "PYL" Then
                ExtrusionTypes(K).Pylon = Trim(A.Substring(6))
            End If
        Loop
        FileClose()

        NoOfExtrusionTypes = K

        ReDim Preserve ExtrusionTypes(K)

        Exit Sub

erro:

        MsgBox("Check your Bridges.txt file!", MsgBoxStyle.Critical)

    End Sub

    Private Function ColorFromArgb(ByVal argb As String) As Color

        ColorFromArgb = Color.FromArgb(Convert.ToInt32(argb, 16))

    End Function

    Private Sub SetBmpTextures()

        Dim Command, FileSource, FileTarget, FileJPG As String

        Dim BmpFolder As String = AppPath & "\Tools\Bmps\"
        Dim ImageTool As String = "imagetool -nogui -nomip "
        Dim Flags1 As String = "-32 Bmps\"
        Dim Flags2 As String = "-fallback Bmps\"
        Dim bmp As String = ".bmp"
        Dim jpg As String = ".jpg"

        Dim myImage As Image
        Dim K As Integer

        Dim ShowWait As Boolean = False

        Try

            ChDrive(My.Application.Info.DirectoryPath)
            ChDir(My.Application.Info.DirectoryPath & "\Tools\")

            If Not My.Computer.FileSystem.FileExists(BmpFolder & "000b2su1.jpg") Then
                ShowWait = True
                frmWaiting.Show()
                frmWaiting.bar.Maximum = NoOfWCs + NoOfLCs + NoOfLineTypes + NoOfPolyTypes
                frmWaiting.bar.Value = 0
            End If

            For K = 1 To NoOfWCs
                FileJPG = BmpFolder & WC(K).Texture & jpg
                If Not My.Computer.FileSystem.FileExists(FileJPG) Then
                    If ShowWait Then
                        frmWaiting.bar.Value += 1
                        frmWaiting.labelFile.Text = WC(K).Texture & jpg
                        frmWaiting.Refresh()
                    End If
                    FileTarget = BmpFolder & WC(K).Texture & bmp
                    FileSource = FSTextureFolder & WC(K).Texture & bmp
                    FileCopy(FileSource, FileTarget)
                    Command = ImageTool & Flags1 & WC(K).Texture & bmp
                    ExecCmd(Command)
                    If ShowWait Then frmWaiting.Refresh()
                    myImage = Image.FromFile(FileTarget)
                    myImage.Save(FileJPG, ImageFormat.Jpeg)
                    myImage.Dispose()
                    If ShowWait Then frmWaiting.Refresh()
                    Command = ImageTool & Flags2 & WC(K).Texture & bmp
                    ExecCmd(Command)
                End If
            Next
            If ShowWait Then frmWaiting.Refresh()
            For K = 1 To NoOfLCs
                FileJPG = BmpFolder & LC(K).Texture & jpg
                If Not My.Computer.FileSystem.FileExists(FileJPG) Then
                    If ShowWait Then
                        frmWaiting.bar.Value += 1
                        frmWaiting.labelFile.Text = LC(K).Texture & jpg
                        frmWaiting.Refresh()
                    End If
                    FileTarget = BmpFolder & LC(K).Texture & bmp
                    FileSource = FSTextureFolder & LC(K).Texture & bmp
                    FileCopy(FileSource, FileTarget)
                    Command = ImageTool & Flags1 & LC(K).Texture & bmp
                    ExecCmd(Command)
                    If ShowWait Then frmWaiting.Refresh()
                    myImage = Image.FromFile(FileTarget)
                    myImage.Save(FileJPG, ImageFormat.Jpeg)
                    myImage.Dispose()
                    If ShowWait Then frmWaiting.Refresh()
                    Command = ImageTool & Flags2 & LC(K).Texture & bmp
                    ExecCmd(Command)
                End If
            Next

            For K = 1 To NoOfLineTypes
                FileJPG = BmpFolder & LineTypes(K).Texture & jpg
                If Not My.Computer.FileSystem.FileExists(FileJPG) Then
                    If ShowWait Then
                        frmWaiting.bar.Value += 1
                        frmWaiting.labelFile.Text = LineTypes(K).Texture & jpg
                        frmWaiting.Refresh()
                    End If
                    FileTarget = BmpFolder & LineTypes(K).Texture & bmp
                    FileSource = FSTextureFolder & LineTypes(K).Texture & bmp
                    FileCopy(FileSource, FileTarget)
                    Command = ImageTool & Flags1 & LineTypes(K).Texture & bmp
                    ExecCmd(Command)
                    If ShowWait Then frmWaiting.Refresh()
                    myImage = Image.FromFile(FileTarget)
                    myImage.Save(FileJPG, ImageFormat.Jpeg)
                    myImage.Dispose()
                    File.Delete(FileTarget)
                End If
            Next

            For K = 1 To NoOfPolyTypes
                FileJPG = BmpFolder & PolyTypes(K).Texture & jpg
                If Not My.Computer.FileSystem.FileExists(FileJPG) Then
                    If ShowWait Then
                        frmWaiting.bar.Value += 1
                        frmWaiting.labelFile.Text = PolyTypes(K).Texture & jpg
                        frmWaiting.Refresh()
                    End If
                    FileTarget = BmpFolder & PolyTypes(K).Texture & bmp
                    FileSource = FSTextureFolder & PolyTypes(K).Texture & bmp
                    FileCopy(FileSource, FileTarget)
                    Command = ImageTool & Flags1 & PolyTypes(K).Texture & bmp
                    ExecCmd(Command)
                    If ShowWait Then frmWaiting.Refresh()
                    myImage = Image.FromFile(FileTarget)
                    myImage.Save(FileJPG, ImageFormat.Jpeg)
                    myImage.Dispose()
                    File.Delete(FileTarget)
                End If
            Next

            frmWaiting.Close()
            frmWaiting.Dispose()

        Catch ex As Exception
            MsgBox("Some textures referred to in polys.txt, lines.txt, lands.txt, or waters.txt could not be copied into the \Tools\Bmps\ folder!", MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub OuterPopUpMenu_Click(sender As Object, e As EventArgs) Handles OuterPopUpMenu.Click

        Dim K As Integer
        Dim Flag As Boolean

        If PointerON Then  ' make just one outer
            Flag = False
            Flag = MakeThisOuter(POPIndex)
        End If

        ' make bulk
        If PolyON Then
            Polys(POPIndex).Selected = True ' make sure POPIndex is selected 
            Flag = True
            For K = 1 To NoOfPolys
                If Polys(K).Selected Then ' it is selected then is a candidate
                    If Polys(K).NoOfChilds < 0 Then  ' it is a hole
                        If Not MakeThisOuter(K) Then Flag = False
                    End If
                End If
            Next
        End If

        If Flag = False Then MsgBox("Error in setting an outer polygon")

        RebuildDisplay()

    End Sub

    Private Function MakeThisOuter(ByVal N As Integer) As Boolean

        ' returns true if Polys(N) is made an outer; returns false if it fails

        MakeThisOuter = False

        Dim J, K, C As Integer

        K = Polys(N).NoOfChilds
        If K >= 0 Then Exit Function

        K = -K
        Polys(N).NoOfChilds = 0

        C = 0
        For J = 1 To Polys(K).NoOfChilds
            If Polys(K).Childs(J) = N Then
                C = J
                Exit For
            End If
        Next

        If C > 0 Then
            DeleteThisChild(K, J)
        Else
            Exit Function
        End If

        MakeThisOuter = True

    End Function

    Private Sub HolePopUpMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles HolePopUpMenu.Click

        SelectParent = True

    End Sub

    Private Sub SetTransparencyPopUpMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SetTransparencyPopUpMenu.Click

        If POPType = "Line" Then ARGBColor = Lines(POPIndex).Color
        If POPType = "Poly" Then ARGBColor = Polys(POPIndex).Color
        If FrmTransparency.ShowDialog = Windows.Forms.DialogResult.OK Then
            If POPType = "Line" Then Lines(POPIndex).Color = ARGBColor
            If POPType = "Poly" Then Polys(POPIndex).Color = ARGBColor
        End If

        RebuildDisplay()

    End Sub

    Private Sub GoToPositionMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GoToPositionMenuItem.Click

        FrmGotoPos.ShowDialog()

    End Sub

    Private Sub ShowAircraftMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowAircraftMenuItem.Click

        If AircraftVIEW = False Then

            Try
                FSUIPCConnection.Open()
                FSUIPCConnection.Process()
                AircraftLatitude = CDbl(LatAircraft.Value)
                AircraftLatitude = AircraftLatitude * 90.0 / (10001750.0 * 65536.0 * 65536.0)
                AircraftLongitude = CDbl(LonAircraft.Value)
                AircraftLongitude = AircraftLongitude * 360.0 / (65536.0 * 65536.0 * 65536.0 * 65536.0)
            Catch ex As Exception
                FSUIPCConnection.Close()
                MsgBox("Error communicating with FSUIPC!", MsgBoxStyle.Information)
                Exit Sub
            End Try

            LonDispCenter = AircraftLongitude
            LatDispCenter = AircraftLatitude
            SetDispCenter(0, 0)
            ShowAircraftMenuItem.Checked = True
            UpdateAircraft(ShowAircraftPeriod)
            AircraftVIEW = True

        Else

            FSUIPCConnection.Close()
            UpdateAircraft(0)
            ShowAircraftMenuItem.Checked = False
            AircraftVIEW = False

        End If

        RebuildDisplay()

    End Sub

    Friend Sub UpdateAircraft(ByVal N As Integer)

        If N > 0 Then
            Timer2.Interval = N
            Timer2.Enabled = True
            Exit Sub
        End If

        If N = 0 Then
            Timer2.Enabled = False
        End If

    End Sub

    Private Sub Timer2_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles Timer2.Elapsed

        Try
            FSUIPCConnection.Process()
            AircraftLatitude = CDbl(LatAircraft.Value)
            AircraftLatitude = AircraftLatitude * 90.0 / (10001750.0 * 65536.0 * 65536.0)
            AircraftLongitude = CDbl(LonAircraft.Value)
            AircraftLongitude = AircraftLongitude * 360.0 / (65536.0 * 65536.0 * 65536.0 * 65536.0)
        Catch ex As Exception
            MsgBox("Error communicating with FSUIPC!", MsgBoxStyle.Information)
            ShowAircraftMenuItem.Checked = False
            Timer2.Enabled = False
            AircraftVIEW = False
            FSUIPCConnection.Close()
            Exit Sub
        End Try

        UpdateDisplay()

    End Sub

    Private Sub FlyAircraftToMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FlyAircraftToMenuItem.Click

        Dim A As String
        Dim Lat, Lon As Double

        Try
            If AircraftVIEW Then   ' this on May 25 2009
                FSUIPCConnection.Close()
                UpdateAircraft(0)
                ShowAircraftMenuItem.Checked = False
                AircraftVIEW = False
            End If
            FSUIPCConnection.Open()
            A = "Fly your Aircraft to this position:" & vbCrLf
            A = A & Lat2Str(LatDispCenter) & "   " & Lon2Str(LonDispCenter) & " ?"
            If MsgBox(A, MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then Exit Sub
            Lat = LatDispCenter
            Lat = Lat * (10001750.0 * 65536.0 * 65536.0) / 90.0
            Lon = LonDispCenter
            Lon = Lon * (65536.0 * 65536.0 * 65536.0 * 65536.0) / 360.0
            LatAircraft.Value = CLng(Lat)
            LonAircraft.Value = CLng(Lon)
            FSUIPCConnection.Process()
            FSUIPCConnection.Close()
        Catch ex As Exception
            MsgBox("Error communicating with FSUIPC!", MsgBoxStyle.Information)
        End Try

    End Sub

    Private Sub FlyToPopUPMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FlyToPopUPMenu.Click

        Dim Lat, Lon As Double

        Try

            If AircraftVIEW Then   ' this on May 25 2009
                FSUIPCConnection.Close()
                UpdateAircraft(0)
                ShowAircraftMenuItem.Checked = False
                AircraftVIEW = False
            End If

            FSUIPCConnection.Open()
            Lat = LatDispNorth - YPOP / PixelsPerLatDeg
            Lat = Lat * (10001750.0 * 65536.0 * 65536.0) / 90.0
            Lon = LonDispWest + XPOP / PixelsPerLonDeg
            Lon = Lon * (65536.0 * 65536.0 * 65536.0 * 65536.0) / 360.0
            LatAircraft.Value = CLng(Lat)
            LonAircraft.Value = CLng(Lon)
            FSUIPCConnection.Process()
            FSUIPCConnection.Close()
        Catch ex As Exception
            MsgBox("Error communicating with FSUIPC!", MsgBoxStyle.Information)
        End Try

    End Sub

    Private Sub DisplayAircraft(ByVal g As Graphics)

        Dim myPen As New System.Drawing.Pen(Color.Red)

        Dim X1, Y1, X2, Y2 As Integer
        Dim CenterX, CenterY As Integer

        CenterX = (AircraftLongitude - LonDispWest) * PixelsPerLonDeg
        CenterY = (LatDispNorth - AircraftLatitude) * PixelsPerLatDeg

        X1 = CenterX
        X2 = CenterX
        Y1 = CenterY - 10
        Y2 = CenterY + 10
        g.DrawLine(myPen, X1, Y1, X2, Y2)

        X1 = CenterX - 10
        X2 = CenterX + 10
        Y1 = CenterY
        Y2 = CenterY
        g.DrawLine(myPen, X1, Y1, X2, Y2)

        myPen.Dispose()

    End Sub


    Private Sub PropertiesMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PropertiesMenuItem.Click

        FrmProjectP.ShowDialog()

    End Sub

    Private Sub ExportBLNMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportBLNMenuItem.Click

        Dim a, b As String

        If NoOfLines = 0 And NoOfPolys = 0 Then
            MsgBox("There is no line or polygon to export!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        a = "Surfer File (*.BLN)|*.BLN"
        b = "SBuilderX: Export As Surfer File"

        a = FileNameToSave(a, b, "SUR")
        If a = "" Then Exit Sub

        'WorkFile = A$
        ExportSurfer((a))
        Dirty = False

    End Sub

    Private Sub AppendBLNMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AppendBLNMenuItem.Click

        FrmSurfer.ShowDialog()

        FileOpenTrailer()
        Dirty = True

    End Sub

    Private Sub AboutMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AboutMenuItem.Click


        lbDonation.Visible = False

        FrmAbout.ShowDialog()

    End Sub

    'Private Sub PreferencesMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreferencesMenuItem.Click

    '    frmPreferences.ShowDialog()

    'End Sub

    Private Sub FindMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FindMenuItem.Click

        FrmFind.ShowDialog()

    End Sub


    Private Sub ShowBackgroundMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowBackgroundMenuItem.Click

        TilesDownloading.Clear()
        TilesFailed.Clear()
        TilesToCome = 0

        If TileVIEW = False Then
            ShowBackgroundMenuItem.Checked = True
            FromBackgroundMapMenuItem.Enabled = True
            TileVIEW = True
            MakeBackground()
        Else
            ShowBackgroundMenuItem.Checked = False
            FromBackgroundMapMenuItem.Enabled = False
            TileVIEW = False
            lbTilesRemaining.Visible = False
        End If

        RebuildDisplay()

    End Sub

    Private Sub SaveBackGroundPopUpMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveBackGroundPopUpMenu.Click

        If FrmBackground.ShowDialog = Windows.Forms.DialogResult.OK Then

            Dim a, b As String

            a = "Geotiff image (*.TIF)|*.TIF"
            b = "SBuilderX: Save Background as a Geotiff image"

            a = FileNameToSave(a, b, "BMP")
            If a = "" Then
                Exit Sub
            End If

            'WorkFile = a$
            SaveBackground(a)
            Dirty = False

        End If

    End Sub


    Private Sub ColorFromMap(ByVal X As Integer, ByVal y As Integer)

        Dim myColor As Color = BitmapBuffer.GetPixel(X, y)

        Dim N As Integer = FrmProjectP.lstClassItems.SelectedIndex + 1

        LWCIs(N).Color = myColor
        FrmProjectP.lbClassItem.BackColor = myColor
        FrmProjectP.lbClassItem.ForeColor = InvertColor(myColor)
        FrmProjectP.Show()

    End Sub

    Public Sub MakeBackground()

        If TileVIEW = False Then Exit Sub

        Dim Download As Boolean = False

        If Zoom > GlobeOrTiles Then

            If ActiveTileFolder = "" Then Exit Sub
            If TilesToCome > 0 Then Exit Sub
            TimeToUpdate = False

            Dim X, Y, X0, X1, Y0, Y1 As Integer
            Dim HH As Integer
            Dim H(6) As Integer

            Dim img_tile As Bitmap

            Dim TileExtension As String = TileServer.ImageType

            Dim TilePrefix As String = "\L" & Trim(Zoom) & "X"
            TileFolder = AppPath & "\Tiles\" & TileServer.ServerName
            Dim TileName, TileFull, TileTemp As String

            If Not My.Computer.FileSystem.DirectoryExists(TileFolder) Then
                Directory.CreateDirectory(TileFolder)
            End If

            Dim myDownloadTileHandler As DownloadTileHandler = AddressOf TileServer.DownloadTile
            Dim myTileHandlerState As TileHandlerState

            Dim AR As System.IAsyncResult
            Dim TileDir As String

            Dim box As Rectangle
            box.Width = 256

            X = XTilesFromLon(LonDispCenter, Zoom)
            Y = YTilesFromLat(LatDispCenter, Zoom)
            X0 = X - 5
            Y0 = Y - 3
            X1 = X + 5
            Y1 = Y + 3
            HH = 0
            PixelHeightFromY(Y0, H, 7, Zoom)
            For Y = 0 To 6
                HH = HH + H(Y)
            Next

            'ImageBackground = ImageBackground0
            ImageBackground0 = Nothing
            ImageBackground0 = New Bitmap(2816, HH)

            Dim g As Graphics = Graphics.FromImage(ImageBackground0)

            box.Y = 0
            For Y = Y0 To Y1
                box.Height = H(Y - Y0)
                box.X = 0
                For X = X0 To X1

                    TileDir = TileDirFromXYZ(X, Y, Zoom)
                    TileName = TilePrefix & Trim(X) & "Y" & Trim(Y) & TileExtension

                    If Zoom > MaximumZoom Then
                        img_tile = najpg
                    Else
                        Try
                            TileFull = TileFolder & TileDir & TileName
                            img_tile = Image.FromFile(TileFull)
                        Catch ex As Exception
                            img_tile = blankjpg
                            If Not TilesFailed.Contains(TileName) Then
                                If Not TilesDownloading.Contains(TileName) Then
                                    Download = True
                                    TileTemp = AppPath & "\Tiles" & TileName
                                    TilesDownloading.Add(TileName)
                                    TilesToCome = TilesToCome + 1
                                    TileHasArrived(TilesToCome)
                                    myTileHandlerState.handler = myDownloadTileHandler
                                    myTileHandlerState.tile = TileName
                                    myTileHandlerState.dir = TileDir
                                    AR = myDownloadTileHandler.BeginInvoke(X, Y, Zoom, TileTemp, myDownloadTileCallback, myTileHandlerState)
                                End If
                            End If
                        End Try
                    End If

                    g.DrawImage(img_tile, box)

                    box.X = box.X + 256
                Next
                box.Y = box.Y + H(Y - Y0)
            Next

            g.Dispose()

            MapBackground0.WLON = LonFromXMerc(X0, Zoom)
            MapBackground0.ELON = LonFromXMerc(X1 + 1, Zoom)
            MapBackground0.NLAT = LatFromYMerc(Y0, Zoom)
            MapBackground0.SLAT = LatFromYMerc(Y1 + 1, Zoom)
            MapBackground0.COLS = ImageBackground0.Width
            MapBackground0.ROWS = ImageBackground0.Height
            TimeToUpdate = True

        Else

            MapBackground0.NLAT = 90
            MapBackground0.SLAT = -90
            MapBackground0.ELON = 180
            MapBackground0.WLON = -180
            MapBackground0.COLS = ImageGlobe.Width
            MapBackground0.ROWS = ImageGlobe.Height

        End If

        If Download = False Then
            MapBackground = MapBackground0
            ImageBackground = ImageBackground0
        End If

    End Sub

    Friend Sub DownloadTileCallback(ByVal ar As IAsyncResult)

        '*** this code fires at completion of each asynchronous method call
        Dim myTileHandlerState As TileHandlerState
        myTileHandlerState = CType(ar.AsyncState, TileHandlerState)

        Dim caller As DownloadTileHandler = myTileHandlerState.handler
        Dim Tilename As String = myTileHandlerState.tile
        Dim TileDir As String = myTileHandlerState.dir


        Dim TileFull As String = TileFolder & TileDir & Tilename
        Dim TileTemp As String = AppPath & "\Tiles" & Tilename

        Dim retval As Boolean = False
        Try
            retval = caller.EndInvoke(ar)
        Catch ex As Exception

        End Try

        TilesDownloading.Remove(Tilename)
        TilesToCome = TilesToCome - 1

        If retval Then ' not failed
            If My.Computer.FileSystem.FileExists(TileTemp) Then
                My.Computer.FileSystem.CopyFile(TileTemp, TileFull)
                My.Computer.FileSystem.DeleteFile(TileTemp)
                'Debug.Print("Success = " & Tilename)
            Else ' as if failed
                TilesFailed.Add(Tilename)
                'Debug.Print("Failed = " & Tilename)
            End If
        Else '
            If My.Computer.FileSystem.FileExists(TileTemp) Then
                My.Computer.FileSystem.DeleteFile(TileTemp)
            End If
            TilesFailed.Add(Tilename)
            'Debug.Print("Failed = " & Tilename)
        End If

        If TilesToCome < 0 Then TilesToCome = 0
        UpdateUI(TilesToCome)

    End Sub

    Private Delegate Sub UpdateUIHandler(ByVal remain As Integer)
    Friend Sub UpdateUI(ByVal remain As Integer)

        '*** check to see if thread switch is required
        If InvokeRequired Then
            Dim handler As New UpdateUIHandler(AddressOf TileHasArrived)
            BeginInvoke(handler, remain)
        Else
            TileHasArrived(remain)
        End If

    End Sub


    Private Sub JoinAllPopUPMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles JoinAllPopUPMenu.Click

        If LineON Then
            TryAllLineJoin()
        End If

        If PolyON Then
            TryAllPolyJoin()
        End If

        RebuildDisplay()

    End Sub

    Private Sub FromBackgroundMapMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FromBackgroundMapMenuItem.Click

        If Zoom > GlobeOrTiles Then
            PointerToolStripButton_Click(PointerToolStripButton, New System.EventArgs())
            FrmTiles.ShowDialog()
        End If

    End Sub


    Private Sub Timer3_Elapsed(ByVal sender As System.Object, ByVal e As System.Timers.ElapsedEventArgs) Handles Timer3.Elapsed

        BackUpFileCounter = BackUpFileCounter + 1
        If BackUpFileCounter = 100 Then BackUpFileCounter = 0

        Dim A As String = Format(BackUpFileCounter, "00") & ".SBP"
        SaveFile(BackUpFileName & A)

    End Sub



    Private Sub CmdLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim HTMLFile As String

        HTMLFile = "https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=ptsim%40ptsim%2ecom&item_name=Donation%20for%20SBuilderX&page_style=SBuilderX&no_shipping=1&return=http%3a%2f%2fwww%2eptsim%2ecom%2fsbuilderx%2fthankyou%2easp&cancel_return=http%3a%2f%2fwww%2eptsim%2ecom%2fsbuilderx%2fthankyou%2easp&cn=Optional%20Note&tax=0&currency_code=EUR&lc=GB&bn=PP%2dDonationsBF&charset=UTF%2d8"
        Process.Start(HTMLFile)

        lbDonation.Visible = False

    End Sub

    Private Sub SbuilderHelpMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SbuilderHelpMenuItem.Click


        lbDonation.Visible = False

        Dim HelpFile As String

        HelpFile = My.Application.Info.DirectoryPath & "\Help\SbuilderX313.chm"
        If File.Exists(HelpFile) Then
            Process.Start(HelpFile)
            Exit Sub
        End If

        'HelpFile = My.Application.Info.DirectoryPath & "\Help\Sbuilder.htm"
        'If File.Exists(HelpFile) Then
        '    Process.Start(HelpFile)
        '    Exit Sub
        'End If

        'HelpFile = "http://www.ptsim.com/forum/viewtopic.php?f=22&t=1056"
        'Process.Start(HelpFile)

    End Sub

    Private Sub ForumMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForumMenuItem.Click


        lbDonation.Visible = False

        Dim HTMLFile As String
        HTMLFile = "http://www.ptsim.com/forum/viewforum.php?f=22"
        Process.Start(HTMLFile)

    End Sub

    Private Sub GetMapMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetMapMenuItem.Click


        lbDonation.Visible = False

        Dim HTMLFile As String
        HTMLFile = "http://www.ptsim.com/sbuilder/gmaps.asp?Lat=" & Str(LatIniCenter) & "&Lon=" & Str(LonIniCenter) & "&Zoom=" & Zoom
        Process.Start(HTMLFile)


    End Sub

    Private Sub FrmStart_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        If ShowDonation Then

            BackColorGray = True
            StatusStrip.Visible = False
            lbScale.Visible = False
            lbScaleBar.Visible = False
            lbDonation.Visible = True

            FrmAbout.ShowDialog()

            BackColorGray = False
            StatusStrip.Visible = True
            lbScale.Visible = True
            lbScaleBar.Visible = True
            RebuildDisplay()

        End If

    End Sub

    Private Sub AppendRAWMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AppendRAWMenuItem.Click

        Dim A, B, C As String
        Dim Good As Boolean = False
        Dim J, K As Integer

        A = "Land or Water class Raw file (*.RAW)|*.RAW"
        B = "SBuilderX: Append Raw File"

        C = FileNameToOpen(A, B, "RAW")

        If C = "" Then
            Exit Sub
        End If

        A = Path.GetFileNameWithoutExtension(C).ToUpper
        B = A.Substring(0, 3)

        If B = "LC_" Or B = "WC_" Then Good = True

        If Good Then
            Try
                J = CInt(A.Substring(3, 2))
                K = CInt(A.Substring(5, 2))
            Catch ex As Exception
                Good = False
            End Try
        End If

        If Good Then
            If J < 0 Or J > 95 Or K < 0 Or K > 63 Then
                Good = False
            End If
        End If

        If Good = False Then
            FrmRAW.ShowDialog()
            If FrmRAW.DialogResult = Windows.Forms.DialogResult.OK Then
                J = FrmRAW.J
                K = FrmRAW.K
                Good = True
                If J < 0 Or J > 95 Or K < 0 Or K > 63 Then
                    Good = False
                End If
                B = FrmRAW.C
                frmSCREEN.Dispose()
            Else
                frmSCREEN.Dispose()
                Exit Sub
            End If
        End If

        If Not Good Then
            MsgBox("Wrong File Specification!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        ' is good!
        If B = "LC_" Then
            If AppendRawLand(C, J, K) Then
                FileOpenTrailer()
                Dirty = True
            Else
                MsgBox("Error in reading land class file!", MsgBoxStyle.Critical)
            End If
        ElseIf B = "WC_" Then
            If AppendRawWater(C, J, K) Then
                FileOpenTrailer()
                Dirty = True
            Else
                MsgBox("Error in reading waster class file!", MsgBoxStyle.Critical)
            End If
        End If

    End Sub

    Private Sub ObjLibManagerMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ObjLibManagerMenuItem.Click

        FrmLibrary.Show()

    End Sub

    Private SliceGuid As String  ' to put a copy and then use it for deleting

    Private Sub SliceQMIDPopUpMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SliceQMIDPopUpMenu.Click

        Dim NP As Integer = NoOfPolys
        Dim K As Integer

        If AutoLinePolyJoin Then
            Dim A As String = "It is recommended to edit the INI file " & vbCrLf
            A = A & "and set AutoLinePolyJoin=False. Do you want to continue?"
            If MsgBox(A, MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        End If

        If BackUpON Then BackUp()
        Dim N As Integer
        If POPType = "Poly" Then
            If POPMode = "Many" Then 'many to set
                For N = 1 To NP
                    If Polys(N).Selected Then
                        If Polys(N).NoOfChilds >= 0 Then
                            SliceGuid = Polys(N).Guid
                            SliceThisQMIDPoly(N)
                            Polys(N).Guid = "Delete!"
                            For K = 1 To Polys(N).NoOfChilds
                                Polys(Polys(N).Childs(K)).Guid = "Delete!"
                            Next
                        End If
                    End If
                Next
            ElseIf POPMode = "One" Then
                If Polys(POPIndex).NoOfChilds >= 0 Then
                    SliceGuid = Polys(POPIndex).Guid
                    SliceThisQMIDPoly(POPIndex)
                    Polys(POPIndex).Guid = "Delete!"
                    For K = 1 To Polys(POPIndex).NoOfChilds
                        Polys(Polys(POPIndex).Childs(K)).Guid = "Delete!"
                    Next
                End If
            End If
            SkipBackUp = True
            For N = NP To 1 Step -1
                If Polys(N).Guid = "Delete!" Then
                    DeletePoly(N)
                End If
            Next
            SkipBackUp = False
        End If


        If POPType = "Line" Then
            If POPMode = "Many" Then 'many to set
                For N = 1 To NoOfLines
                    If Lines(N).Selected Then
                        SliceThisQMIDLine(N)
                    End If
                Next
            ElseIf POPMode = "One" Then
                SliceThisQMIDLine(POPIndex)
            End If
        End If

        Dirty = True
        RebuildDisplay()

    End Sub

    Private Sub SliceThisQMIDPoly(ByVal Pl As Integer)

        If Polys(Pl).NoOfChilds < 0 Then Exit Sub
        If Polys(Pl).NoOfChilds = 0 Then MakePolyClockWise(Pl)

        Dim N As Integer
        Dim LA, LAS, LAN As Double
        Dim LO, LOW, LOE As Double

        LAN = Polys(Pl).NLAT
        LAS = Polys(Pl).SLAT

        LOW = Polys(Pl).WLON
        LOE = Polys(Pl).ELON

        N = Int(LAS / LatitudeDelta)
        LAS = (N + 1) * LatitudeDelta
        N = Int(LAN / LatitudeDelta)
        LA = LAN Mod LatitudeDelta
        If LA = 0 Then N = N - 1
        LAN = N * LatitudeDelta

        N = Int(LOW / LongitudeDelta)
        LOW = (N + 1) * LongitudeDelta
        N = Int(LOE / LongitudeDelta)
        LO = LOE Mod LongitudeDelta
        If LO = 0 Then N = N - 1
        LOE = N * LongitudeDelta

        Dim P As New ClipPoly
        NoOfSlices = 0
        P.SetPoly(Pl)

        For LA = LAS To LAN Step LatitudeDelta
            P.InsertLatCrossing(LA)
        Next LA

        For LO = LOW To LOE Step LongitudeDelta
            P.InsertLonCrossing(LO)
        Next LO

        LAS = LAS - LatitudeDelta / 2
        LAN = LAN + LatitudeDelta / 2
        LOW = LOW - LongitudeDelta / 2
        LOE = LOE + LongitudeDelta / 2

        ' going trought the center of quads
        For LA = LAS To LAN Step LatitudeDelta
            For LO = LOW To LOE Step LongitudeDelta
                P.SetQuad(QMIDLevel, LA, LO)
                P.Clip2Quad()
                Slices2Polys(Pl)
            Next LO
        Next LA

        ' Level11MenuItem.Checked = True
        'QMIDLevel = 11
        'ResetLevelGrid()
        'SetLatLonDeltas()
        'RebuildDisplay()

    End Sub


    Private Sub SliceThisQMIDLine(ByVal Ln As Integer)

        Dim N As Integer
        Dim LA, LAS, LAN As Double
        Dim LO, LOW, LOE As Double

        LAN = Lines(Ln).NLAT
        LAS = Lines(Ln).SLAT

        LOW = Lines(Ln).WLON
        LOE = Lines(Ln).ELON

        N = Int(LAS / LatitudeDelta)
        LAS = (N + 1) * LatitudeDelta
        N = Int(LAN / LatitudeDelta)
        LA = LAN Mod LatitudeDelta
        If LA = 0 Then N = N - 1
        LAN = N * LatitudeDelta

        N = Int(LOW / LongitudeDelta)
        LOW = (N + 1) * LongitudeDelta
        N = Int(LOE / LongitudeDelta)
        LO = LOE Mod LongitudeDelta
        If LO = 0 Then N = N - 1
        LOE = N * LongitudeDelta

        Dim L As New ClipLine

        NoOfSlices = 0
        L.SetLine(Ln)

        For LA = LAS To LAN Step LatitudeDelta
            L.InsertLatCrossing(LA)
        Next LA

        For LO = LOW To LOE Step LongitudeDelta
            L.InsertLonCrossing(LO)
        Next LO

        LAS = LAS - LatitudeDelta / 2
        LAN = LAN + LatitudeDelta / 2
        LOW = LOW - LongitudeDelta / 2
        LOE = LOE + LongitudeDelta / 2

        ' going trought the center of quads
        For LA = LAS To LAN Step LatitudeDelta
            For LO = LOW To LOE Step LongitudeDelta
                L.SetQuad(QMIDLevel, LA, LO)
                L.Fragment2Quad()
            Next LO
        Next LA

        Fragments2Lines(Ln)

        'Level11MenuItem.Checked = True
        'QMIDLevel = 11
        'ResetLevelGrid()
        'SetLatLonDeltas()
        'RebuildDisplay()

    End Sub


    Private Sub FillQMIDPopUpMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FillQMIDPopUpMenu.Click

        Dim NP As Integer = NoOfPolys
        Dim K As Integer

        If AutoLinePolyJoin Then
            Dim A As String = "It is recommended to edit the INI file " & vbCrLf
            A = A & "and set AutoLinePolyJoin=False. Do you want to continue?"
            If MsgBox(A, MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        End If

        If BackUpON Then BackUp()
        Dim N As Integer
        If POPType = "Poly" Then
            If POPMode = "Many" Then 'many to set
                For N = 1 To NP
                    If Polys(N).Selected Then
                        If Polys(N).NoOfChilds >= 0 Then
                            SliceGuid = Polys(N).Guid
                            FillThisQMIDPoly(N)
                            Polys(N).Guid = "Delete!"
                            For K = 1 To Polys(N).NoOfChilds
                                Polys(Polys(N).Childs(K)).Guid = "Delete!"
                            Next
                        End If
                    End If
                Next
            ElseIf POPMode = "One" Then
                If Polys(POPIndex).NoOfChilds >= 0 Then
                    SliceGuid = Polys(POPIndex).Guid
                    FillThisQMIDPoly(POPIndex)
                    Polys(POPIndex).Guid = "Delete!"
                    For K = 1 To Polys(POPIndex).NoOfChilds
                        Polys(Polys(POPIndex).Childs(K)).Guid = "Delete!"
                    Next
                End If
            End If
            SkipBackUp = True
            For N = NP To 1 Step -1
                If Polys(N).Guid = "Delete!" Then
                    DeletePoly(N)
                End If
            Next
            SkipBackUp = False
        End If

        Dirty = True
        RebuildDisplay()

    End Sub

    Private Sub FillThisQMIDPoly(ByVal Pl As Integer)

        If Polys(Pl).NoOfChilds < 0 Then Exit Sub
        If Polys(Pl).NoOfChilds = 0 Then MakePolyClockWise(Pl)

        Dim N As Integer
        Dim LA, LAS, LAN As Double
        Dim LO, LOW, LOE As Double

        LAN = Polys(Pl).NLAT
        LAS = Polys(Pl).SLAT

        LOW = Polys(Pl).WLON
        LOE = Polys(Pl).ELON

        N = Int(LAS / LatitudeDelta)
        LAS = (N + 1) * LatitudeDelta
        N = Int(LAN / LatitudeDelta)
        LA = LAN Mod LatitudeDelta
        If LA = 0 Then N = N - 1
        LAN = N * LatitudeDelta

        N = Int(LOW / LongitudeDelta)
        LOW = (N + 1) * LongitudeDelta
        N = Int(LOE / LongitudeDelta)
        LO = LOE Mod LongitudeDelta
        If LO = 0 Then N = N - 1
        LOE = N * LongitudeDelta

        Dim P As New ClipPoly
        NoOfSlices = 0
        P.SetPoly(Pl)

        For LA = LAS To LAN Step LatitudeDelta
            P.InsertLatCrossing(LA)
        Next LA

        For LO = LOW To LOE Step LongitudeDelta
            P.InsertLonCrossing(LO)
        Next LO

        LAS = LAS - LatitudeDelta / 2
        LAN = LAN + LatitudeDelta / 2
        LOW = LOW - LongitudeDelta / 2
        LOE = LOE + LongitudeDelta / 2

        ' going trought the center of quads
        For LA = LAS To LAN Step LatitudeDelta
            For LO = LOW To LOE Step LongitudeDelta
                P.SetQuad(QMIDLevel, LA, LO)
                P.Fill2Quad()
                Slices2Polys(Pl)
            Next LO
        Next LA

        ' Level11MenuItem.Checked = True
        'QMIDLevel = 11
        'ResetLevelGrid()
        'SetLatLonDeltas()
        'RebuildDisplay()


    End Sub

    Private Sub Slices2Polys(ByVal P As Integer)

        If NoOfSlices = 0 Then Exit Sub

        Dim N, K, J As Integer

        Dim Name As String = Polys(P).Name
        Dim Type As String = Polys(P).Type
        Dim Color As Color = Polys(P).Color
        Dim Selected As Boolean = Polys(P).Selected

        Dim n0, n1, n2 As Integer
        Dim k1, k2, k3, lat, lon As Double

        Get3Points(P, n0, n1, n2, lat)
        GetSlopes(P, n0, n1, n2, k1, k2, k3)

        ReDim Preserve Polys(NoOfPolys + NoOfSlices)

        For N = 1 To NoOfSlices
            Polys(NoOfPolys + N).Name = Name
            Polys(NoOfPolys + N).Type = Type
            Polys(NoOfPolys + N).Guid = SliceGuid
            Polys(NoOfPolys + N).Color = Color
            Polys(NoOfPolys + N).Selected = Selected
            Polys(NoOfPolys + N).NoOfPoints = Slices(N).N
            ReDim Polys(NoOfPolys + N).GPoints(Slices(N).N)
            For K = 1 To Slices(N).N
                lat = Slices(N).P(K).Y
                lon = Slices(N).P(K).X
                Polys(NoOfPolys + N).GPoints(K).lat = lat
                Polys(NoOfPolys + N).GPoints(K).lon = lon
                Polys(NoOfPolys + N).GPoints(K).alt = k1 * lon + k2 * lat + k3
            Next
            ' now the parents and childs problem!!!
            If Slices(N).NC = 0 Then    'is single
                Polys(NoOfPolys + N).NoOfChilds = 0
            ElseIf Slices(N).NC < 0 Then    'is child
                Polys(NoOfPolys + N).NoOfChilds = Slices(N).NC - NoOfPolys
            Else       ' is parent
                Polys(NoOfPolys + N).NoOfChilds = Slices(N).NC
                ReDim Polys(NoOfPolys + N).Childs(Slices(N).NC)
                For J = 1 To Slices(N).NC
                    Polys(NoOfPolys + N).Childs(J) = NoOfPolys + Slices(N).C(J)
                Next
            End If

            AddLatLonToPoly(NoOfPolys + N)
        Next

        NoOfPolys = NoOfPolys + NoOfSlices
        NoOfSlices = 0
        ReDim Slices(1)


    End Sub

    Private Sub Fragments2Lines(ByVal L As Integer)

        Dim N, K As Integer

        Dim Name As String = Lines(L).Name
        Dim Type As String = Lines(L).Type
        Dim Guid As String = Lines(L).Guid
        Dim Color As Color = Lines(L).Color
        Dim Selected As Boolean = Lines(L).Selected

        Lines(L).Name = Name
        Lines(L).Type = Type
        Lines(L).Guid = Guid
        Lines(L).Color = Color
        Lines(L).Selected = Selected
        Lines(L).NoOfPoints = Fragments(1).N
        ReDim Lines(L).GLPoints(Fragments(1).N)
        For K = 1 To Fragments(1).N
            Lines(L).GLPoints(K).lat = Fragments(1).P(K).Y
            Lines(L).GLPoints(K).lon = Fragments(1).P(K).X
            Lines(L).GLPoints(K).alt = Fragments(1).P(K).Z
            Lines(L).GLPoints(K).wid = Fragments(1).P(K).W
        Next
        AddLatLonToLine(L)


        ReDim Preserve Lines(NoOfLines + NoOfFragments - 1)
        Dim M As Integer
        For N = 2 To NoOfFragments
            M = NoOfLines + N - 1
            Lines(M).Name = Name
            Lines(M).Type = Type
            Lines(M).Guid = Guid
            Lines(M).Color = Color
            Lines(M).Selected = Selected
            Lines(M).NoOfPoints = Fragments(N).N
            ReDim Lines(M).GLPoints(Fragments(N).N)
            For K = 1 To Fragments(N).N
                Lines(M).GLPoints(K).lat = Fragments(N).P(K).Y
                Lines(M).GLPoints(K).lon = Fragments(N).P(K).X
                Lines(M).GLPoints(K).alt = Fragments(N).P(K).Z
                Lines(M).GLPoints(K).wid = Fragments(N).P(K).W
            Next
            AddLatLonToLine(M)
        Next

        NoOfLines = NoOfLines + NoOfFragments - 1
        NoOfFragments = 0
        ReDim Fragments(1)


    End Sub

    Private Sub PointFromAircraftPopUpMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PointFromAircraftPopUpMenu.Click

        Dim Altitude As Double
        Dim Latitude As Double
        Dim Longitude As Double

        Try
            If AircraftVIEW Then
                FSUIPCConnection.Close()
                UpdateAircraft(0)
                ShowAircraftMenuItem.Checked = False
                AircraftVIEW = False
            End If
            FSUIPCConnection.Open()
            FSUIPCConnection.Process()
            Latitude = CDbl(LatAircraft.Value)
            Latitude = Latitude * 90.0 / (10001750.0 * 65536.0 * 65536.0)
            Longitude = CDbl(LonAircraft.Value)
            Longitude = Longitude * 360.0 / (65536.0 * 65536.0 * 65536.0 * 65536.0)
            Altitude = CDbl(Alt1Aircraft.Value)
            Altitude = Altitude + CDbl(Alt2Aircraft.Value) / (65536.0! * 65536.0!)
            Altitude = Altitude - AircraftAltitudeOffset
            FSUIPCConnection.Close()
            POPType = POPType & "X"
            FrmLPPointsP.Altitude = Altitude
            FrmLPPointsP.Latitude = Latitude
            FrmLPPointsP.Longitude = Longitude
            FrmLPPointsP.ShowDialog()
        Catch ex As Exception
            FSUIPCConnection.Close()
            MsgBox("Error communicating with FSUIPC!", MsgBoxStyle.Information)
            Exit Sub
        End Try

    End Sub


    Private Sub TilePathToClipboardPopUpMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TilePathToClipboardPopUpMenu.Click

        If Zoom <= GlobeOrTiles Or ActiveTileFolder = "" Then
            MsgBox("Can not get Tile Path", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim TileExtension As String = TileServer.ImageType
        Dim TilePrefix As String = "\L" & Trim(Zoom) & "X"
        TileFolder = AppPath & "\Tiles\" & TileServer.ServerName

        Dim X As Integer = XTilesFromLon(LonDispCenter, Zoom)
        Dim Y As Integer = YTilesFromLat(LatDispCenter, Zoom)
        Dim TileDir As String = TileDirFromXYZ(X, Y, Zoom)
        Dim TileName As String = TilePrefix & Trim(X) & "Y" & Trim(Y) & TileExtension

        Dim TileFull As String = TileFolder & TileDir & TileName

        My.Computer.Clipboard.SetText(TileFull)

    End Sub

    Private Sub FSXSettingsMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FSXSettingsMenuItem.Click
        FrmFSXSettings.ShowDialog()
    End Sub


    'Private Sub FromHereNokiaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FromHereNokiaMenuItem.Click
    '    AddHereNokiaMap()
    'End Sub

    Private Sub FromGoogleMapsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FromGoogleMapsToolStripMenuItem.Click
        AddGoogleMap()
    End Sub


    Private Sub FromArcGisToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FromArcGisToolStripMenuItem.Click

        AddArcGisMap()

    End Sub

    Private Sub NoLODMenuItem_Click(sender As Object, e As EventArgs) Handles NoLODMenuItem.Click
        LODLevel = -1
        LODGridMenuItem.Text = "LOD Grid"
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD0MenuItem_Click(sender As Object, e As EventArgs) Handles LOD0MenuItem.Click
        LODLevel = 0
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub


    Private Sub LOD1MenuItem_Click(sender As Object, e As EventArgs) Handles LOD1MenuItem.Click
        LODLevel = 1
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD2MenuItem_Click(sender As Object, e As EventArgs) Handles LOD2MenuItem.Click
        LODLevel = 2
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD3MenuItem_Click(sender As Object, e As EventArgs) Handles LOD3MenuItem.Click
        LODLevel = 3
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD4MenuItem_Click(sender As Object, e As EventArgs) Handles LOD4MenuItem.Click
        LODLevel = 4
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD5MenuItem_Click(sender As Object, e As EventArgs) Handles LOD5MenuItem.Click
        LODLevel = 5
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD6MenuItem_Click(sender As Object, e As EventArgs) Handles LOD6MenuItem.Click
        LODLevel = 6
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD7MenuItem_Click(sender As Object, e As EventArgs) Handles LOD7MenuItem.Click
        LODLevel = 7
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD8MenuItem_Click(sender As Object, e As EventArgs) Handles LOD8MenuItem.Click
        LODLevel = 8
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD9MenuItem_Click(sender As Object, e As EventArgs) Handles LOD9MenuItem.Click
        LODLevel = 9
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub
    Private Sub LOD10MenuItem_Click(sender As Object, e As EventArgs) Handles LOD10MenuItem.Click
        LODLevel = 10
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD11MenuItem_Click(sender As Object, e As EventArgs) Handles LOD11MenuItem.Click
        LODLevel = 11
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD12MenuItem_Click(sender As Object, e As EventArgs) Handles LOD12MenuItem.Click
        LODLevel = 12
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD13MenuItem_Click(sender As Object, e As EventArgs) Handles LOD13MenuItem.Click
        LODLevel = 13
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD14MenuItem_Click(sender As Object, e As EventArgs) Handles LOD14MenuItem.Click
        LODLevel = 14
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD15MenuItem_Click(sender As Object, e As EventArgs) Handles LOD15MenuItem.Click
        LODLevel = 15
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD16MenuItem_Click(sender As Object, e As EventArgs) Handles LOD16MenuItem.Click
        LODLevel = 16
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD17MenuItem_Click(sender As Object, e As EventArgs) Handles LOD17MenuItem.Click
        LODLevel = 17
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD18MenuItem_Click(sender As Object, e As EventArgs) Handles LOD18MenuItem.Click
        LODLevel = 18
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD19MenuItem_Click(sender As Object, e As EventArgs) Handles LOD19MenuItem.Click
        LODLevel = 19
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD20MenuItem_Click(sender As Object, e As EventArgs) Handles LOD20MenuItem.Click
        LODLevel = 20
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD21MenuItem_Click(sender As Object, e As EventArgs) Handles LOD21MenuItem.Click
        LODLevel = 21
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD22MenuItem_Click(sender As Object, e As EventArgs) Handles LOD22MenuItem.Click
        LODLevel = 22
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD23MenuItem_Click(sender As Object, e As EventArgs) Handles LOD23MenuItem.Click
        LODLevel = 23
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD24MenuItem_Click(sender As Object, e As EventArgs) Handles LOD24MenuItem.Click
        LODLevel = 24
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD25MenuItem_Click(sender As Object, e As EventArgs) Handles LOD25MenuItem.Click
        LODLevel = 25
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD26MenuItem_Click(sender As Object, e As EventArgs) Handles LOD26MenuItem.Click
        LODLevel = 26
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub

    Private Sub LOD27MenuItem_Click(sender As Object, e As EventArgs) Handles LOD27MenuItem.Click
        LODLevel = 27
        ResetLevelGrid(True)
        RebuildDisplay()
    End Sub


    Private Sub SnapToQMIDMenuItem_Click(sender As Object, e As EventArgs) Handles SnapToQMIDMenuItem.Click

        If QMIDLevel < 4 Then Exit Sub

        If NoOfPolysSelected = 0 Then
            MsgBox("No polygons are selected!", MsgBoxStyle.Information, AppTitle)
            Exit Sub
        End If

        Dim A As String = "Snap Points in Selected Polys to"
        A = A + vbCrLf + "a QMID Level " & QMIDLevel & " grid ?"
        Dim x As Integer
        x = MsgBox(A, MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, AppTitle)
        If Not x = MsgBoxResult.Yes Then Exit Sub

        If NoOfPolysSelected > 0 Then SnapPolys()


        'BackUp()
        'SkipBackUp = True


        RebuildDisplay()

        Beep()
        ' SkipBackUp = False


    End Sub

    Private Sub SnapQMIDPopUPMenu_Click(sender As Object, e As EventArgs) Handles SnapQMIDPopUPMenu.Click

        If POPType = "Poly" Then
            SetDeltaLatLon()
            SnapThisPoly(POPIndex)
            RebuildDisplay()
        End If

    End Sub


    Private Sub ExportKMLMenuItem_Click(sender As Object, e As EventArgs) Handles ExportKMLMenuItem.Click

        Dim a, b As String

        If NoOfLines = 0 And NoOfPolys = 0 Then
            MsgBox("There is no line or polygon to export!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If NoOfLines > 0 Then
            a = "Google KML file (*.KML)|*.KML"
            b = "SBuilderX: Export As a KML file"
            a = FileNameToSave(a, b, "KML")
            If a <> "" Then
                ExportKML(a)
            End If
        End If

    End Sub

    Private Sub EditINIFileMenuItem_Click(sender As Object, e As EventArgs) Handles EditINIFileMenuItem.Click

        Hide()
        Dim myProcess As New Process
        myProcess = Process.Start("notepad.exe", AppIni)
        myProcess.WaitForExit()
        myProcess.Dispose()
        Show()

        GetSettings()

    End Sub

    Private Sub ObjectFoldersMenuItem_Click(sender As Object, e As EventArgs) Handles ObjectFoldersMenuItem.Click

        FrmObjectFolders.ShowDialog()

    End Sub

    Private Sub TileServerMenuItem_Click(sender As Object, e As EventArgs) Handles TileServerMenuItem.Click

        frmTilesServers.ShowDialog()

    End Sub

    Private Sub EnableUndoRedoMenuItem_Click(sender As Object, e As EventArgs) Handles EnableUndoRedoMenuItem.Click

        If EnableUndoRedoMenuItem.Checked Then
            EnableUndoRedoMenuItem.Checked = False
        Else
            EnableUndoRedoMenuItem.Checked = True
        End If

        BackUpON = False
        If EnableUndoRedoMenuItem.CheckState = 1 Then BackUpON = True

        WriteSettings()

        If BackUpON Then
            BackUpInit()
        Else
            BackUpFinit()
        End If

        EditMenuItem.ShowDropDown()

    End Sub

    Private Sub EditMenuItem_Click(sender As Object, e As EventArgs) Handles EditMenuItem.Click

        'added this in October 2017 because of the cosmetic change on Enable Undo Redo! 
        EnableUndoRedoMenuItem.CheckState = System.Windows.Forms.CheckState.Unchecked
        If BackUpON Then
            EnableUndoRedoMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        End If

    End Sub

    Private Sub WhatIsNewMenuItem_Click(sender As Object, e As EventArgs) Handles WhatIsNewMenuItem.Click

        On Error GoTo erro1
        Process.Start(AppPath & "\Help\whatisnewin315.pdf")
        Exit Sub
erro1:
        Dim A As String = "SBuilderX could not find the file: whatisnewin315.pdf which should" & vbCrLf
        A = A & "exist in the SBuilderX\Help folder! You can download the most recent" & vbCrLf
        A = A & "version of this file from here:" & vbCrLf
        A = A & "http://www.ptsim.com/sbuilderx/whatisnewin315.pdf"

        MsgBox(A, MsgBoxStyle.Exclamation)

    End Sub

End Class




