
Imports System.Drawing
Friend Class FrmTiles

    Private MouseIsDown As Boolean = False

    Private Const XS As Integer = 15
    Private Const YS As Integer = 15


    'Private Const XW As Integer = 616  ' the size of the map in the form
    'Private Const YH As Integer = 440

    'Private X_1 As Integer = 630
    'Private Y_1 As Integer = 454

    Private Const XW As Integer = 770  ' the size of the map in the form
    Private Const YH As Integer = 490  ' each quad = 70 x 70

    Private X_1 As Integer = 784
    Private Y_1 As Integer = 504

    Private X_0 As Integer = 15
    Private Y_0 As Integer = 15

    Private X0(4) As Integer
    Private Y0(4) As Integer
    Private X1(4) As Integer
    Private Y1(4) As Integer
    Private X00 As Integer
    Private Y00 As Integer
    Private X11 As Integer
    Private Y11 As Integer
    Private ZZZ As Integer = Zoom
    Private H() As Integer
    'Private blankjpg As Bitmap = Image.FromFile(AppPath & "\Tiles\blank.jpg")  ' was like this in October 2017
    Private blankjpg As Bitmap = CType(Image.FromFile(AppPath & "\Tiles\blank.jpg"), Bitmap)
    Private ImgBuffer As Bitmap = New Bitmap(XW, YH)

    Private Sub FrmTiles_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        BuildImageBuffer()
        TimeToUpdate = False
        labelCount.Visible = False

    End Sub


    Private Sub FrmTiles_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown

        MouseIsDown = False
        Dim Button As Integer = e.Button \ &H100000
        If Button = 1 Then
            X_0 = e.X
            Y_0 = e.Y
            If XYIsOK(X_0, Y_0) Then MouseIsDown = True
        End If

    End Sub

    Private Sub FrmTiles_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove

        If Not MouseIsDown Then Exit Sub
        Dim X As Integer = e.X
        Dim Y As Integer = e.Y
        CheckXY(X, Y)
        DrawSelectBox(X, Y)

    End Sub

    Private Sub FrmTiles_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp

        If Not MouseIsDown Then Exit Sub
        'Debug.Print("mouse up")
        X_1 = e.X
        Y_1 = e.Y
        CheckXY(X_1, Y_1)
        ComputeExtents()
        DrawSelectBox(X_1, Y_1)
        MouseIsDown = False

    End Sub


    Private Function XYIsOK(ByVal X As Integer, ByVal Y As Integer) As Boolean

        XYIsOK = False
        If X >= XS And X < XW + XS Then
            If Y >= YS And Y < YH + YS Then
                XYIsOK = True
            End If
        End If

    End Function

    Private Sub CheckXY(ByRef X As Integer, ByRef Y As Integer)

        If X < XS Then X = XS
        If X > XW + XS - 1 Then X = XW + XS - 1
        If Y < YS Then Y = YS
        If Y > YH + YS - 1 Then Y = YH + YS - 1

    End Sub

    Private Sub DrawSelectBox(ByVal X As Integer, ByVal Y As Integer)

        Dim DX, DY As Integer
        Dim PX, PY As Integer

        UpDateDisplay()

        Dim p As New System.Drawing.Pen(Color.Red)
        Dim g As System.Drawing.Graphics
        p.DashStyle = Drawing2D.DashStyle.Dash
        g = CreateGraphics()
        DX = X - X_0
        DY = Y - Y_0
        PX = X_0
        PY = Y_0
        If X < X_0 Then
            DX = X_0 - X
            PX = X
        End If
        If Y < Y_0 Then
            DY = Y_0 - Y
            PY = Y
        End If
        g.DrawRectangle(p, New Rectangle(PX, PY, DX, DY))
        p.Dispose()
        g.Dispose()

    End Sub
    Private Sub BuildImageBuffer()

        Dim g As Graphics = Graphics.FromImage(ImgBuffer)
        g.DrawImage(ImageBackground, 0, 0, XW, YH)

        Dim Z As Integer = ZZZ - Zoom
        Z = CInt(2 ^ Z)
        'Dim NY As Integer = Z * 5
        Dim NY As Integer = Z * 7

        Dim Y As Integer
        Y = YTilesFromLat(LatDispCenter, ZZZ)
        Y = Y - CInt(Int(NY / 2))

        ReDim H(NY)
        'PixelHeight240FromY(Y, H, NY, ZZZ)
        PixelHeight440FromY(Y, H, NY, ZZZ)
        DisplayGrids(g)
        g.Dispose()

    End Sub
    Private Sub DisplayGrids(ByVal g As Graphics)
        Dim p As New Pen(GridColor) With {
            .DashStyle = Drawing2D.DashStyle.Dash
        }

        Dim R, C As Integer
        Dim NX, NY As Integer
        Dim DX, PX As Integer
        Dim PY As Integer

        Dim Z As Integer = ZZZ - Zoom
        Z = CInt(2 ^ Z)
        'NX = Z * 7
        'NY = Z * 5
        NX = Z * 11
        NY = Z * 7

        DX = CInt(XW / NX)

        PX = DX
        For C = 1 To NX - 1
            g.DrawLine(p, PX, 0, PX, YH)
            PX = PX + DX
        Next

        PY = H(0)
        For R = 1 To NY - 1
            g.DrawLine(p, 0, PY, XW, PY)
            PY = PY + H(R)
        Next

        p.Dispose()

    End Sub

    Private Sub UpDateDisplay()
        Dim gr As Graphics = CreateGraphics()
        gr.DrawImageUnscaled(ImgBuffer, XS, YS)   'copy buffer to display
        gr.Dispose()
    End Sub

    Private Sub FrmTiles_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

        UpDateDisplay()
        CheckXY(X_1, Y_1)
        DrawSelectBox(X_1, Y_1)

    End Sub

    Private Sub CmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Dispose()

    End Sub

    Private Sub CmdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        If MakeImageFromTiles() Then
            Dispose()
        Else
            labelCount.Visible = False
        End If

    End Sub

    Private Sub ComputeExtents()

        Dim N As Integer

        Dim S As Double

        Dim X As Integer
        If X_0 > X_1 Then
            X = X_0
            X_0 = X_1
            X_1 = X
        End If
        If Y_0 > Y_1 Then
            X = Y_0
            Y_0 = Y_1
            Y_1 = X
        End If

        Dim atzoom As String = " at Zoom = "

        Dim DLat As Double = MapBackground.NLAT - MapBackground.SLAT
        Dim DLon As Double = MapBackground.ELON - MapBackground.WLON
        Dim Lon0, Lon1 As Double
        Dim Lat0, Lat1 As Double
        Lon0 = ((X_0 - XS + 1) / XW) * DLon + MapBackground.WLON
        Lon1 = ((X_1 - XS) / XW) * DLon + MapBackground.WLON
        Lat0 = MapBackground.NLAT - ((Y_0 - YS + 1) / YH) * DLat
        Lat1 = MapBackground.NLAT - ((Y_1 - YS) / YH) * DLat

        For N = 0 To 4
            X0(N) = XTilesFromLon(Lon0, Zoom + N)
            Y0(N) = YTilesFromLat(Lat0, Zoom + N)
            X1(N) = XTilesFromLon(Lon1, Zoom + N)
            Y1(N) = YTilesFromLat(Lat1, Zoom + N)
        Next

        RadioButton1.Text = (X1(4) - X0(4) + 1).ToString & " x " & (Y1(4) - Y0(4) + 1).ToString & atzoom & (Zoom + 4).ToString
        RadioButton2.Text = (X1(3) - X0(3) + 1).ToString & " x " & (Y1(3) - Y0(3) + 1).ToString & atzoom & (Zoom + 3).ToString
        RadioButton3.Text = (X1(2) - X0(2) + 1).ToString & " x " & (Y1(2) - Y0(2) + 1).ToString & atzoom & (Zoom + 2).ToString
        RadioButton4.Text = (X1(1) - X0(1) + 1).ToString & " x " & (Y1(1) - Y0(1) + 1).ToString & atzoom & (Zoom + 1).ToString
        RadioButton5.Text = (X1(0) - X0(0) + 1).ToString & " x " & (Y1(0) - Y0(0) + 1).ToString & atzoom & (Zoom).ToString

        If RadioButton1.Checked Then
            X00 = X0(4)
            Y00 = Y0(4)
            X11 = X1(4)
            Y11 = Y1(4)
            ZZZ = Zoom + 4
        ElseIf RadioButton2.Checked Then
            X00 = X0(3)
            Y00 = Y0(3)
            X11 = X1(3)
            Y11 = Y1(3)
            ZZZ = Zoom + 3
        ElseIf RadioButton3.Checked Then
            X00 = X0(2)
            Y00 = Y0(2)
            X11 = X1(2)
            Y11 = Y1(2)
            ZZZ = Zoom + 2
        ElseIf RadioButton4.Checked Then
            X00 = X0(1)
            Y00 = Y0(1)
            X11 = X1(1)
            Y11 = Y1(1)
            ZZZ = Zoom + 1
        Else
            X00 = X0(0)
            Y00 = Y0(0)
            X11 = X1(0)
            Y11 = Y1(0)
            ZZZ = Zoom
        End If

        S = (X11 - X00 + 1) * (Y11 - Y00 + 1)
        GroupBox1.Text = "Number of Tiles = " & S

        S = S * 262.144
        labelSize.Text = "Size = " & Format(S, "###,000") & " KB"
        BuildImageBuffer()
        UpDateDisplay()
        DrawSelectBox(X_1, Y_1)


    End Sub


    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = False Then Exit Sub
        ComputeExtents()
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = False Then Exit Sub
        ComputeExtents()
    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = False Then Exit Sub
        ComputeExtents()
    End Sub

    Private Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton4.Checked = False Then Exit Sub
        ComputeExtents()
    End Sub

    Private Sub RadioButton5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton5.CheckedChanged
        If RadioButton5.Checked = False Then Exit Sub
        ComputeExtents()
    End Sub


    Private Sub PixelHeight440FromY(ByVal Y As Integer, ByRef H() As Integer, ByVal N As Integer, ByVal Z As Integer)

        Dim R As Integer
        Dim NS As Double
        Dim X(N) As Double

        For R = 0 To N
            X(R) = LatFromYMerc(Y + R, Z)
        Next

        'NS = 441 / (X(0) - X(N))
        NS = 491 / (X(0) - X(N))

        For R = 0 To N - 1
            H(R) = CInt((X(R) - X(R + 1)) * NS)
        Next

    End Sub


    Private Function MakeImageFromTiles() As Boolean

        MakeImageFromTiles = False

        Dim A As String

        If ActiveTileFolder = "" Or TilesToCome > 0 Then
            A = "Could not start the acquisition of the image." & vbCrLf
            A = A & "Hide and Show the Background, and try again!"
            MsgBox(A, MsgBoxStyle.Critical)
            Exit Function
        End If

        Cursor = Cursors.WaitCursor

        Try
            Dim R, C As Integer
            Dim NX, NY, K, KT As Integer

            NX = X11 - X00
            NY = Y11 - Y00

            Dim TileExtension As String = TileServer.ImageType
            Dim TilePrefix As String = "\L" & Trim(ZZZ.ToString) & "X"
            TileFolder = AppPath & "\Tiles\" & TileServer.ServerName
            Dim TileName, TileFull, TileDir, TileTemp As String

            Dim AR As System.IAsyncResult
            Dim myDownloadTileHandler As DownloadTileHandler = AddressOf TileServer.DownloadTile
            Dim myTileHandlerState As TileHandlerState

            Dim box As Rectangle
            box.Width = 256

            Dim HH As Integer
            Dim H(NY) As Integer
            HH = 0
            PixelHeightFromY(Y00, H, NY + 1, ZZZ)
            For R = 0 To NY
                HH = HH + H(R)
            Next
            Dim ImgTile As Bitmap = New Bitmap(256, 256)

            NoOfMaps = NoOfMaps + 1
            ReDim Preserve ImgMaps(NoOfMaps)
            ReDim Preserve Maps(NoOfMaps)
            ImgMaps(NoOfMaps) = New Bitmap(256 * (NX + 1), HH)
            Dim g As Graphics = Graphics.FromImage(ImgMaps(NoOfMaps))

            box.Y = 0
            KT = (NY + 1) * (NX + 1)
            K = 0
            labelCount.Visible = True
            For R = 0 To NY
                box.Height = H(R)
                box.X = 0
                For C = 0 To NX

                    K = K + 1
                    labelCount.Text = "Processing Tile " & K & " out of " & KT
                    labelCount.Refresh()

                    TileName = TilePrefix & Trim((X00 + C).ToString) & "Y" & Trim((Y00 + R).ToString) & TileExtension
                    TileDir = TileDirFromXYZ(X00 + C, Y00 + R, ZZZ)

                    Try
                        TileFull = TileFolder & TileDir & TileName
                        'ImgTile = Image.FromFile(TileFull)      'was like this in October 2017
                        ImgTile = CType(Image.FromFile(TileFull), Bitmap)
                    Catch ex As Exception
                        ImgTile = blankjpg
                        If Not TilesFailed.Contains(TileName) Then
                            If Not TilesDownloading.Contains(TileName) Then
                                TileTemp = AppPath & "\Tiles" & TileName
                                TilesDownloading.Add(TileName)
                                TilesToCome = TilesToCome + 1
                                TileHasArrived(TilesToCome)
                                myTileHandlerState.handler = myDownloadTileHandler
                                myTileHandlerState.tile = TileName
                                myTileHandlerState.dir = TileDir
                                AR = myDownloadTileHandler.BeginInvoke(X00 + C, Y00 + R, ZZZ, TileTemp, myDownloadTileCallback, myTileHandlerState)
                            End If
                        End If
                    End Try

                    g.DrawImage(ImgTile, box)
                    box.X = box.X + 256

                Next
                box.Y = box.Y + H(R)
            Next

            ImgTile.Dispose()
            g.Dispose()
        Catch ex As Exception
            MsgBox("Could not make an image!", MsgBoxStyle.Critical)
            Cursor = Cursors.Default
            NoOfMaps = NoOfMaps - 1
            ReDim Preserve ImgMaps(NoOfMaps)
            Exit Function
        End Try

        If TilesToCome > 0 Then
            A = TilesToCome.ToString & " tiles are being downloaded" & vbCrLf
            A = A & "at this moment. Please repeat this" & vbCrLf
            A = A & "operation when downloading is complete!"
            MsgBox(A, MsgBoxStyle.Information)
            Cursor = Cursors.Default
            NoOfMaps = NoOfMaps - 1
            ReDim Preserve ImgMaps(NoOfMaps)
            Exit Function
        End If

        Maps(NoOfMaps).Name = "Photo" & Format(NoOfMaps, "00")
        Maps(NoOfMaps).Selected = False

        Dim myfile As String = AppPath & "\Tools\Work\L"
        myfile = myfile & ZZZ.ToString & "X" & X00.ToString & "X" & X11.ToString
        myfile = myfile & "Y" & Y00.ToString & "Y" & Y11.ToString & ".BMP"

        Try
            If Not (ImgMaps(NoOfMaps) Is Nothing) Then
                ImgMaps(NoOfMaps).Save(myfile, ImageFormat.Bmp)
            End If
        Catch ex As Exception
            MsgBox("There was a problem saving the image!", MsgBoxStyle.Exclamation)
        End Try

        MakeImageFromTiles = True

        Maps(NoOfMaps).BMPSu = myfile
        Maps(NoOfMaps).BMPSp = myfile
        Maps(NoOfMaps).BMPFa = myfile
        Maps(NoOfMaps).BMPWi = myfile
        Maps(NoOfMaps).BMPHw = myfile
        Maps(NoOfMaps).BMPLm = myfile

        Maps(NoOfMaps).COLS = ImgMaps(NoOfMaps).Width
        Maps(NoOfMaps).ROWS = ImgMaps(NoOfMaps).Height

        Maps(NoOfMaps).WLON = LonFromXMerc(X00, ZZZ)
        Maps(NoOfMaps).ELON = LonFromXMerc(X11 + 1, ZZZ)
        Maps(NoOfMaps).NLAT = LatFromYMerc(Y00, ZZZ)
        Maps(NoOfMaps).SLAT = LatFromYMerc(Y11 + 1, ZZZ)

        SaveDataFile(myfile, Maps(NoOfMaps).NLAT, Maps(NoOfMaps).SLAT, Maps(NoOfMaps).WLON, Maps(NoOfMaps).ELON, Maps(NoOfMaps).Name)

        frmStart.ViewAllMapsMenuItem.Checked = True
        MapVIEW = True
        frmStart.SummerMapMenuItem.Checked = True
        frmStart.ShowBackgroundMenuItem.Checked = False
        TileVIEW = False
        frmStart.FromBackgroundMapMenuItem.Enabled = False

        LonDispCenter = (Maps(NoOfMaps).WLON + Maps(NoOfMaps).ELON) / 2
        LatDispCenter = (Maps(NoOfMaps).NLAT + Maps(NoOfMaps).SLAT) / 2

        ViewON = True
        Zoom = ZZZ
        ResetZoom()
        SetDispCenter(0, 0)
        RebuildDisplay()
        Dirty = True

        Cursor = Cursors.Default

    End Function

End Class