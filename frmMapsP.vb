Option Strict Off
Option Explicit On

Imports System.Windows.Forms

Friend Class FrmMapsP

    Private DoBackUp As Boolean

    Private Sub CmdData_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdData.Click

        Dim A, DataFile, DataPath, FullFile As String

        DataFile = Path.GetFileNameWithoutExtension(txtBMPSummer.Text) & ".TXT"
        DataPath = Path.GetDirectoryName(txtBMPSummer.Text)

        FullFile = DataPath & "\" & DataFile

        If File.Exists(FullFile) Then
            A = "The file:" & vbCrLf & vbCrLf & DataFile & vbCrLf & vbCrLf
            A = A & "already exists! Overwrite?"
            If MsgBox(A, 32 + 4) = 7 Then Exit Sub

        End If

        FileOpen(3, FullFile, OpenMode.Output)
        A = "[GEOGRAPHIC]"
        PrintLine(3, A)
        A = "Name=" & txtName.Text
        PrintLine(3, A)
        A = "North=" & CStr(Str2Lat(txtNLat.Text))
        PrintLine(3, A)
        A = "South=" & CStr(Str2Lat(txtSLat.Text))
        PrintLine(3, A)
        A = "West=" & CStr(Str2Lon(txtWLon.Text))
        PrintLine(3, A)
        A = "East=" & CStr(Str2Lon(txtELon.Text))
        PrintLine(3, A)
        PrintLine(3)

        FileClose(3)


    End Sub

    Private Sub CmdFall_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdFall.Click

        Dim A As String

        'A = FileBMPToOpen("")
        A = FileNameToOpen(ImageFilter, ImageTitle, "BMP")
        If A = "" Then Exit Sub
        txtBMPFall.Text = A
        Season = "Fall"
        ViewON = True
        Dirty = True
        DoBackUp = True


    End Sub
    Private Sub CmdHard_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdHard.Click

        Dim A As String

        'A = FileBMPToOpen("")
        A = FileNameToOpen(ImageFilter, ImageTitle, "BMP")
        If A = "" Then Exit Sub
        txtBMPHard.Text = A
        Season = "HardWinter"
        ViewON = True
        Dirty = True
        DoBackUp = True

    End Sub



    Private Sub CmdNight_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdNight.Click

        Dim A As String

        'A = FileBMPToOpen("")
        A = FileNameToOpen(ImageFilter, ImageTitle, "BMP")
        If A = "" Then Exit Sub
        txtBMPNight.Text = A
        Season = "Night"
        ViewON = True
        Dirty = True
        DoBackUp = True


    End Sub

    Private Sub CmdSpring_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSpring.Click

        Dim A As String

        'A = FileBMPToOpen("")
        A = FileNameToOpen(ImageFilter, ImageTitle, "BMP")
        If A = "" Then Exit Sub
        txtBMPSpring.Text = A
        ViewON = True
        Dirty = True
        DoBackUp = True
        Season = "Spring"

    End Sub


    Private Sub CmdSummer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSummer.Click

        Dim A As String

        'A = FileBMPToOpen("")
        A = FileNameToOpen(ImageFilter, ImageTitle, "BMP")
        If A = "" Then Exit Sub
        txtBMPSummer.Text = A
        ViewON = True
        Dirty = True
        DoBackUp = True
        Season = "Summer"

    End Sub

    Private Sub CmdWinter_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdWinter.Click

        Dim A As String

        'A = FileBMPToOpen("")
        A = FileNameToOpen(ImageFilter, ImageTitle, "BMP")
        If A = "" Then Exit Sub
        txtBMPWinter.Text = A
        Season = "Winter"
        ViewON = True
        Dirty = True
        DoBackUp = True

    End Sub

    Private Sub FrmMapsP_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim N As Integer
        Dim A As String

        DoBackUp = False

        N = POPIndex

        txtName.Text = Maps(N).Name
        txtBMPSummer.Text = Maps(N).BMPSu
        txtBMPSpring.Text = Maps(N).BMPSp
        txtBMPFall.Text = Maps(N).BMPFa
        txtBMPWinter.Text = Maps(N).BMPWi
        txtBMPHard.Text = Maps(N).BMPHw
        txtBMPNight.Text = Maps(N).BMPLm

        lbCols.Text = CStr(Maps(N).COLS)
        lbRows.Text = CStr(Maps(N).ROWS)
        txtNLat.Text = Lat2Str(Maps(N).NLAT)
        txtSLat.Text = Lat2Str(Maps(N).SLAT)
        txtELon.Text = Lon2Str(Maps(N).ELON)
        txtWLon.Text = Lon2Str(Maps(N).WLON)

        txtCellX.Text = CStr((Maps(N).ELON - Maps(N).WLON) / Maps(N).COLS)
        txtCellY.Text = CStr((Maps(N).NLAT - Maps(N).SLAT) / Maps(N).ROWS)

        A = "Possible formats:"
        A = A & "  N40 30 30,33"
        A = A & "  40:30:30,3333"
        A = A & "  40:30 30,33 N"


        A = "Possible formats:"
        A = A & "  E40 30 30,33"
        A = A & "  40:30:30,3333"
        A = A & "  40:30 30,33 E"



    End Sub

    Private Sub CmdOK_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click

        Dim N As Integer

        N = POPIndex

        If DoBackUp Then BackUp()

        Maps(N).Name = txtName.Text
        Maps(N).Selected = False

        Maps(N).BMPSu = txtBMPSummer.Text
        Maps(N).BMPSp = txtBMPSpring.Text
        Maps(N).BMPFa = txtBMPFall.Text
        Maps(N).BMPWi = txtBMPWinter.Text
        Maps(N).BMPHw = txtBMPHard.Text
        Maps(N).BMPLm = txtBMPNight.Text


        Maps(N).COLS = CInt(lbCols.Text)
        Maps(N).ROWS = CInt(lbRows.Text)
        Maps(N).NLAT = Str2Lat(txtNLat.Text)
        Maps(N).SLAT = Str2Lat(txtSLat.Text)
        Maps(N).ELON = Str2Lon(txtELon.Text)
        Maps(N).WLON = Str2Lon(txtWLon.Text)

        frmStart.SaveAsMenuItem.Enabled = True

        SetBitmapSeason()
        RebuildDisplay()

        Dirty = True
        Dispose()

    End Sub

    Private Sub CmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        Dispose()
    End Sub



    Private Sub CmdCalibrateMain_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCalibrateMain.Click

        Season = "Summer"
        SetBitmapSeason()
        ViewON = True
        RebuildDisplay()

        'Me.Hide()
        'frmStart.Show()
        'frmCalibrate.ShowDialog()
        'Me.Dispose()

        frmCalibrate.ShowDialog()
        ' frmStart.CalibratePopUPMenu_Click(sender, e)


    End Sub


    Private Sub CmdGeoTiff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGeoTiff.Click

        Dim GeoTiff As Image
        Dim Stream As New MemoryStream
        ImgMaps(POPIndex).Save(Stream, ImageFormat.Tiff)
        GeoTiff = Image.FromStream(Stream)


        Dim propItem As PropertyItem
        propItem = GeoTiff.GetPropertyItem(256)
        propItem.Len = 32
        propItem.Type = 1
        propItem.Value = MakeGeoKeyDirTag()
        propItem.Id = 34735
        GeoTiff.SetPropertyItem(propItem)


        Dim North As Double = Str2Lat(txtNLat.Text)
        Dim South As Double = Str2Lat(txtSLat.Text)
        Dim East As Double = Str2Lon(txtELon.Text)
        Dim West As Double = Str2Lon(txtWLon.Text)
        Dim Cols As Integer = CInt(lbCols.Text)
        Dim Rows As Integer = CInt(lbRows.Text)

        Dim LX, SX As Double
        Dim LY, SY As Double

        SX = (East - West) / Cols
        SY = (North - South) / Rows
        LX = West + SX / 2
        LY = North + SY / 2

        propItem.Id = 33922
        propItem.Len = 48
        propItem.Value = MakeTiePointTag(LX, LY)
        GeoTiff.SetPropertyItem(propItem)
        propItem.Len = 24
        propItem.Id = 33550

        propItem.Value = MakePixelScaleTag(SX, SY)
        GeoTiff.SetPropertyItem(propItem)

        Dim FileName As String
        Dim K As Integer
        FileName = Maps(POPIndex).BMPSu
        K = Len(FileName) - 4
        FileName = Mid(FileName, 1, K) & ".tif"

        GeoTiff.Save(FileName)
        Stream.Dispose()
        Dim buffer() As Byte
        buffer = File.ReadAllBytes(FileName)

        'Dim buffer() As Byte = Stream.GetBuffer
        'Stream.Close()
        'GeoTiff.Dispose()

        Dim N As Integer = buffer.GetUpperBound(0)
        Dim Problem As Boolean = False
        If buffer(N - 25) = 10 Then
            buffer(N - 25) = 12
        Else
            Problem = True
        End If

        If buffer(N - 37) = 10 Then
            buffer(N - 37) = 12
        Else
            Problem = True
        End If

        If Problem = True Then MsgBox("Geotiff file may have some problems!")

        File.WriteAllBytes(FileName, buffer)
        ReDim buffer(0)

    End Sub

    'Private Function Imag3eFilter() As String

    '    Dim A As String
    '    A = "Windows Bitmap Format (*.BMP)|*.bmp"
    '    A = A & "|Jpeg File Interchange Format (*.JPG)|*.jpg"
    '    A = A & "|Tag Image File Format (*.TIF)|*.tif"
    '    A = A & "|Graphics Interchange Format (*.GIF)|*.gif"
    '    A = A & "|Portable Network Graphics (*.PNG)|*.png"
    '    Return A

    'End Function

    Private Const ImageFilter As String = "Windows Bitmap Format (*.BMP)|*.bmp _" &
                                          "|Jpeg File Interchange Format (*.JPG)|*.jpg" &
                                          "|Tag Image File Format (*.TIF)|*.tif" &
                                          "|Graphics Interchange Format (*.GIF)|*.gif" &
                                          "|Portable Network Graphics (*.PNG)|*.png"

    Private Const ImageTitle As String = "SBuilderX: Open Image File"

End Class
