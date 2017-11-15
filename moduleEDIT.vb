Option Strict Off
Option Explicit On
Module moduleEDIT

    Friend BackUpON As Boolean ' general switch
    Friend SkipBackUp As Boolean
    Private BackUpPointer As Integer
    Private BackUpUndos As Integer
    Private BackUpRedos As Integer

    Friend PasteON As Boolean

    Private ClipLine As GLine  ' kind of clipboard
    Private ClipPoly As GPoly
    Private ClipObject As Objecto

    Private mem1 As MemoryStream
    Private mem2 As MemoryStream
    Private mem3 As MemoryStream
    Private mem4 As MemoryStream
    Private mem5 As MemoryStream

    Friend Sub BackUpInit()

        BackUpPointer = 0
        BackUpUndos = 0
        BackUpRedos = 0

        mem1 = New MemoryStream
        mem2 = New MemoryStream
        mem3 = New MemoryStream
        mem4 = New MemoryStream
        mem5 = New MemoryStream

        frmStart.UndoMenuItem.Enabled = False
        FrmStart.UndoToolStripButton.Enabled = False

    End Sub

    Friend Sub BackUpFinit()
        
        If Not mem1 Is Nothing Then mem1.Dispose()
        If Not mem1 Is Nothing Then mem2.Dispose()
        If Not mem1 Is Nothing Then mem3.Dispose()
        If Not mem1 Is Nothing Then mem4.Dispose()
        If Not mem1 Is Nothing Then mem5.Dispose()

        FrmStart.UndoMenuItem.Enabled = False
        FrmStart.RedoMenuItem.Enabled = False

        ' added the following in October 2017
        FrmStart.UndoToolStripButton.Enabled = False
        FrmStart.RedoToolStripButton.Enabled = False

    End Sub
    Friend Sub BackUp()

        If Not BackUpON Then Exit Sub

        If BackUpUndos < 5 Then BackUpUndos = BackUpUndos + 1
        BackUpRedos = 0

        BackUpPointer = BackUpPointer + 1
        If BackUpPointer = 6 Then BackUpPointer = 1

        Store(BackUpPointer)

        frmStart.UndoMenuItem.Enabled = True
        frmStart.UndoToolStripButton.Enabled = True
        frmStart.RedoMenuItem.Enabled = False
        frmStart.RedoToolStripButton.Enabled = False


    End Sub

    Friend Sub Undo()

        If BackUpUndos > 0 Then BackUpUndos = BackUpUndos - 1
        If BackUpRedos < 5 Then BackUpRedos = BackUpRedos + 1

        If BackUpPointer = 5 Then
            Store(1)
        Else
            Store(BackUpPointer + 1)
        End If

        Restore(BackUpPointer)

        BackUpPointer = BackUpPointer - 1
        If BackUpPointer = 0 Then BackUpPointer = 5

        If BackUpUndos < 2 Then
            frmStart.UndoMenuItem.Enabled = False
            frmStart.UndoToolStripButton.Enabled = False
        End If

        frmStart.RedoMenuItem.Enabled = True
        frmStart.RedoToolStripButton.Enabled = True

    End Sub

    Friend Sub Redo()

        If BackUpRedos > 0 Then BackUpRedos = BackUpRedos - 1
        If BackUpUndos < 5 Then BackUpUndos = BackUpUndos + 1

        If BackUpPointer = 4 Then
            Restore(1)
        ElseIf BackUpPointer = 5 Then
            Restore(2)
        Else
            Restore(BackUpPointer + 2)
        End If

        BackUpPointer = BackUpPointer + 1
        If BackUpPointer = 6 Then BackUpPointer = 1

        If BackUpRedos = 0 Then
            frmStart.RedoMenuItem.Enabled = False
            frmStart.RedoToolStripButton.Enabled = False
        End If

        frmStart.UndoMenuItem.Enabled = True
        frmStart.UndoToolStripButton.Enabled = True

    End Sub

    Private Sub Store(ByVal N As Integer)

        If N = 1 Then StoreMem(mem1)
        If N = 2 Then StoreMem(mem2)
        If N = 3 Then StoreMem(mem3)
        If N = 4 Then StoreMem(mem4)
        If N = 5 Then StoreMem(mem5)

    End Sub

    Private Sub StoreMem(ByRef sfile As MemoryStream)

        Dim BFormatter As New BinaryFormatter

        sfile.Position = 0

        BFormatter.Serialize(sfile, NoOfMaps)
        BFormatter.Serialize(sfile, NoOfLands)
        BFormatter.Serialize(sfile, NoOfLines)
        BFormatter.Serialize(sfile, NoOfPolys)
        BFormatter.Serialize(sfile, NoOfWaters)
        BFormatter.Serialize(sfile, NoOfObjects)
        BFormatter.Serialize(sfile, NoOfExcludes)
        BFormatter.Serialize(sfile, NoOfLWCIs)

        BFormatter.Serialize(sfile, SomeSelected)
        BFormatter.Serialize(sfile, NoOfPointsSelected)

        If NoOfMaps > 0 Then
            BFormatter.Serialize(sfile, Maps)
            BFormatter.Serialize(sfile, NoOfMapsSelected)
        End If

        If NoOfLands > 0 Then
            BFormatter.Serialize(sfile, NoOfLLXYs)
            BFormatter.Serialize(sfile, LL_XY)
            BFormatter.Serialize(sfile, LLands)
            BFormatter.Serialize(sfile, NoOfLandsSelected)
        End If
        If NoOfLines > 0 Then
            BFormatter.Serialize(sfile, Lines)
            BFormatter.Serialize(sfile, NoOfLinesSelected)
        End If

        If NoOfPolys > 0 Then
            BFormatter.Serialize(sfile, Polys)
            BFormatter.Serialize(sfile, NoOfPolysSelected)
        End If

        If NoOfWaters > 0 Then
            BFormatter.Serialize(sfile, NoOfWWXYs)
            BFormatter.Serialize(sfile, WW_XY)
            BFormatter.Serialize(sfile, WWaters)
            BFormatter.Serialize(sfile, NoOfWatersSelected)
        End If
        If NoOfObjects > 0 Then
            BFormatter.Serialize(sfile, Objects)
            BFormatter.Serialize(sfile, NoOfObjectsSelected)
        End If

        If NoOfExcludes > 0 Then
            BFormatter.Serialize(sfile, Excludes)
            BFormatter.Serialize(sfile, NoOfExcludesSelected)
        End If

        If NoOfLWCIs > 0 Then BFormatter.Serialize(sfile, LWCIs)

    End Sub


    Private Sub Restore(ByVal N As Integer)


        If N = 1 Then RestoreMem(mem1)
        If N = 2 Then RestoreMem(mem2)
        If N = 3 Then RestoreMem(mem3)
        If N = 4 Then RestoreMem(mem4)
        If N = 5 Then RestoreMem(mem5)

    End Sub

    Private Sub RestoreMem(ByRef ofile As MemoryStream)

        Dim BFormatter As New BinaryFormatter

        ofile.Position = 0

        NoOfMaps = BFormatter.Deserialize(ofile)
        NoOfLands = BFormatter.Deserialize(ofile)
        NoOfLines = BFormatter.Deserialize(ofile)
        NoOfPolys = BFormatter.Deserialize(ofile)
        NoOfWaters = BFormatter.Deserialize(ofile)
        NoOfObjects = BFormatter.Deserialize(ofile)
        NoOfExcludes = BFormatter.Deserialize(ofile)
        NoOfLWCIs = BFormatter.Deserialize(ofile)

        SomeSelected = BFormatter.Deserialize(ofile)
        NoOfPointsSelected = BFormatter.Deserialize(ofile)

        If NoOfMaps > 0 Then
            Maps = BFormatter.Deserialize(ofile)
            NoOfMapsSelected = BFormatter.Deserialize(ofile)
        End If

        If NoOfLands > 0 Then
            NoOfLLXYs = BFormatter.Deserialize(ofile)
            LL_XY = BFormatter.Deserialize(ofile)
            LLands = BFormatter.Deserialize(ofile)
            NoOfLandsSelected = BFormatter.Deserialize(ofile)
        End If

        If NoOfLines > 0 Then
            Lines = BFormatter.Deserialize(ofile)
            NoOfLinesSelected = BFormatter.Deserialize(ofile)
        End If

        If NoOfPolys > 0 Then
            Polys = BFormatter.Deserialize(ofile)
            NoOfPolysSelected = BFormatter.Deserialize(ofile)
        End If


        If NoOfWaters > 0 Then
            NoOfWWXYs = BFormatter.Deserialize(ofile)
            WW_XY = BFormatter.Deserialize(ofile)
            WWaters = BFormatter.Deserialize(ofile)
            NoOfWatersSelected = BFormatter.Deserialize(ofile)
        End If

        If NoOfObjects > 0 Then
            Objects = BFormatter.Deserialize(ofile)
            NoOfObjectsSelected = BFormatter.Deserialize(ofile)
        End If

        If NoOfExcludes > 0 Then
            Excludes = BFormatter.Deserialize(ofile)
            NoOfExcludesSelected = BFormatter.Deserialize(ofile)

        End If

        If NoOfLWCIs > 0 Then LWCIs = BFormatter.Deserialize(ofile)

    End Sub
    Friend Sub EditCut()

        Dim N As Integer
        Dim GetIt As Boolean

        If LineON Then

            N = 1
            GetIt = False
            Do Until GetIt
                If Lines(N).Selected Then GetIt = True
                N = N + 1
                If N > NoOfLines + 1 Then Exit Sub
            Loop
            GetClipFromLine(N - 1)
            DeleteLine(N - 1)
            frmStart.PasteMenuItem.Enabled = True
            frmStart.CopyMenuItem.Enabled = False
            frmStart.DeleteMenuItem.Enabled = False
            RebuildDisplay()

        ElseIf PolyON Then

            N = 1
            GetIt = False
            Do Until GetIt
                If Polys(N).Selected Then GetIt = True
                N = N + 1
                If N > NoOfPolys + 1 Then Exit Sub
            Loop
            GetClipFromPoly(N - 1)
            DeletePoly(N - 1)
            frmStart.PasteMenuItem.Enabled = True
            frmStart.CopyMenuItem.Enabled = False
            frmStart.DeleteMenuItem.Enabled = False
            RebuildDisplay()

        ElseIf ObjectON Then

            N = 1
            GetIt = False
            Do Until GetIt
                If Objects(N).Selected Then GetIt = True
                N = N + 1
                If N > NoOfObjects + 1 Then Exit Sub
            Loop
            ClipObject = Objects(N - 1)
            DeleteThisObject(N - 1)
            frmStart.PasteMenuItem.Enabled = True
            frmStart.CopyMenuItem.Enabled = False
            frmStart.DeleteMenuItem.Enabled = False
            RebuildDisplay()

        End If

    End Sub
    Friend Sub EditCopy()

        Dim N As Integer
        Dim GetIt As Boolean

        If LineON Then
            N = 1
            GetIt = False
            Do Until GetIt
                If Lines(N).Selected Then GetIt = True
                N = N + 1
                If N > NoOfLines + 1 Then Exit Sub
            Loop
            GetClipFromLine(N - 1)
            frmStart.PasteMenuItem.Enabled = True
        ElseIf PolyON Then
            N = 1
            GetIt = False
            Do Until GetIt
                If Polys(N).Selected Then GetIt = True
                N = N + 1
                If N > NoOfPolys + 1 Then Exit Sub
            Loop
            GetClipFromPoly(N - 1)
            frmStart.PasteMenuItem.Enabled = True

        ElseIf ObjectON Then
            N = 1
            GetIt = False
            Do Until GetIt
                If Objects(N).Selected Then GetIt = True
                N = N + 1
                If N > NoOfObjects + 1 Then Exit Sub
            Loop
            ClipObject = Objects(N - 1)
            frmStart.PasteMenuItem.Enabled = True

        End If

    End Sub


    Friend Sub EditPasteXY(ByVal X As Integer, ByRef Y As Integer)

        Dim DX, DY As Double
        Dim N As Integer

        DX = LonDispWest + X / PixelsPerLonDeg
        DY = LatDispNorth - Y / PixelsPerLatDeg

        If BackUpON Then BackUp()

        If LineON Then
            SelectAllLines(False)
            NoOfLines = NoOfLines + 1
            ReDim Preserve Lines(NoOfLines)
            GetLineFromClip(NoOfLines)
            DX = DX - Lines(NoOfLines).GLPoints(1).lon
            DY = Lines(NoOfLines).GLPoints(1).lat - DY
            For N = 1 To Lines(NoOfLines).NoOfPoints
                Lines(NoOfLines).GLPoints(N).lat = Lines(NoOfLines).GLPoints(N).lat - DY
                Lines(NoOfLines).GLPoints(N).lon = Lines(NoOfLines).GLPoints(N).lon + DX
            Next N
            AddLatLonToLine(NoOfLines)
            Lines(NoOfLines).Selected = True
        ElseIf PolyON Then
            SelectAllPolys(False)
            NoOfPolys = NoOfPolys + 1
            ReDim Preserve Polys(NoOfPolys)
            GetPolyFromClip(NoOfPolys)
            DX = DX - Polys(NoOfPolys).GPoints(1).lon
            DY = Polys(NoOfPolys).GPoints(1).lat - DY
            For N = 1 To Polys(NoOfPolys).NoOfPoints
                Polys(NoOfPolys).GPoints(N).lat = Polys(NoOfPolys).GPoints(N).lat - DY
                Polys(NoOfPolys).GPoints(N).lon = Polys(NoOfPolys).GPoints(N).lon + DX
            Next N

            AddLatLonToPoly(NoOfPolys)
            Polys(NoOfPolys).Selected = True

        ElseIf ObjectON Then
            SelectAllObjects(False)
            NoOfObjects = NoOfObjects + 1
            ReDim Preserve Objects(NoOfObjects)
            Objects(NoOfObjects) = ClipObject

            DX = DX - Objects(NoOfObjects).lon
            DY = Objects(NoOfObjects).lat - DY

            Objects(NoOfObjects).lon = Objects(NoOfObjects).lon + DX
            Objects(NoOfObjects).lat = Objects(NoOfObjects).lat - DY

            AddLatLonToObjects(NoOfObjects)
            Objects(NoOfObjects).Selected = True

        End If

        RebuildDisplay()

    End Sub
    Private Sub GetLineFromClip(ByVal N As Integer)

        Dim K As Integer
        Lines(N).Name = ClipLine.Name
        Lines(N).Type = ClipLine.Type
        Lines(N).Guid = ClipLine.Guid
        Lines(N).Color = ClipLine.Color
        Lines(N).Selected = ClipLine.Selected
        Lines(N).NoOfPoints = ClipLine.NoOfPoints

        ReDim Lines(N).GLPoints(ClipLine.NoOfPoints)
        For K = 1 To ClipLine.NoOfPoints
            Lines(N).GLPoints(K) = ClipLine.GLPoints(K)
        Next
      
    End Sub
    Private Sub GetClipFromLine(ByVal N As Integer)

        Dim K As Integer
    
        ClipLine.Name = Lines(N).Name
        ClipLine.Type = Lines(N).Type
        ClipLine.Guid = Lines(N).Guid
        ClipLine.Color = Lines(N).Color
        ClipLine.Selected = Lines(N).Selected
        ClipLine.NoOfPoints = Lines(N).NoOfPoints

        ReDim ClipLine.GLPoints(Lines(N).NoOfPoints)
        For K = 1 To Lines(N).NoOfPoints
            ClipLine.GLPoints(K) = Lines(N).GLPoints(K)
        Next

    End Sub
    Private Sub GetPolyFromClip(ByVal N As Integer)

        ' no childs - lost when copied 
        Dim K As Integer
        Polys(N).Name = ClipPoly.Name
        Polys(N).Type = ClipPoly.Type
        Polys(N).Guid = ClipPoly.Guid
        Polys(N).Color = ClipPoly.Color
        Polys(N).Selected = ClipPoly.Selected
        Polys(N).NoOfChilds = 0
        Polys(N).NoOfPoints = ClipPoly.NoOfPoints

        ReDim Polys(N).GPoints(ClipPoly.NoOfPoints)
        For K = 1 To ClipPoly.NoOfPoints
            Polys(N).GPoints(K) = ClipPoly.GPoints(K)
        Next

    End Sub
    Private Sub GetClipFromPoly(ByVal N As Integer)

        Dim K As Integer

        ClipPoly.Name = Polys(N).Name
        ClipPoly.Type = Polys(N).Type
        ClipPoly.Guid = Polys(N).Guid
        ClipPoly.Color = Polys(N).Color
        ClipPoly.Selected = Polys(N).Selected
        ClipPoly.NoOfChilds = 0
        ClipPoly.NoOfPoints = Polys(N).NoOfPoints

        ReDim ClipPoly.GPoints(Polys(N).NoOfPoints)
        For K = 1 To Polys(N).NoOfPoints
            ClipPoly.GPoints(K) = Polys(N).GPoints(K)
        Next

    End Sub
    Friend Sub EditDelete()

        Dim N As Integer
        Dim GetIt As Boolean
        Dim X As Single

        BackUp()

        If LineON Then

            N = 1
            GetIt = False
            Do Until GetIt
                If Lines(N).Selected Then GetIt = True
                N = N + 1
                If N > NoOfLines + 1 Then Exit Sub
            Loop
            X = MsgBox("Delete this Line ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, AppTitle)
            If X = MsgBoxResult.Yes Then
                DeleteLine(N - 1)
                frmStart.DeleteMenuItem.Enabled = False
                RebuildDisplay()
            Else
                Exit Sub
            End If

        ElseIf PolyON Then
            N = 1
            GetIt = False
            Do Until GetIt
                If Polys(N).Selected Then GetIt = True
                N = N + 1
                If N > NoOfPolys + 1 Then Exit Sub
            Loop
            X = MsgBox("Delete this Poly ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, AppTitle)
            If X = MsgBoxResult.Yes Then
                DeletePoly(N - 1)
                frmStart.CopyMenuItem.Enabled = False
                frmStart.DeleteMenuItem.Enabled = False
                RebuildDisplay()
            Else
                Exit Sub
            End If

        ElseIf ObjectON Then
            N = 1
            GetIt = False
            Do Until GetIt
                If Objects(N).Selected Then GetIt = True
                N = N + 1
                If N > NoOfObjects + 1 Then Exit Sub
            Loop
            X = MsgBox("Delete this object ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, AppTitle)
            If X = MsgBoxResult.Yes Then
                DeleteThisObject(N - 1)
                frmStart.CopyMenuItem.Enabled = False
                frmStart.DeleteMenuItem.Enabled = False
                RebuildDisplay()
            Else
                Exit Sub
            End If

        End If

    End Sub

    
End Module