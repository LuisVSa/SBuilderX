Option Strict Off
Option Explicit On
Imports System.xml
Imports System.text
Module moduleEXCLUDES
    <Serializable()> Friend Structure Exclude
        Dim Flag As Integer
        Dim Selected As Boolean
        Dim NLAT As Double
        Dim SLAT As Double
        Dim WLON As Double
        Dim ELON As Double
        'Dim CornerSel As Integer ' not saved 1=NW 2=SE 0=none
    End Structure

    Friend AuxX As Double
    Friend AuxY As Double
    Friend Excludes() As Exclude
    Friend NoOfExcludes As Integer
    Friend ExcludeON As Boolean
    Friend ExcludeVIEW As Boolean
    Friend NoOfExcludesSelected As Integer = 0
    Friend ExcludeSizeIndex As Integer = 0
    Friend DrawExclude As Boolean = False

    Private excludeAllObjects As String
    Private excludeBeaconObjects As String
    Private excludeEffectObjects As String
    Private excludeExtrusionBridgeObjects As String
    Private excludeGenericBuildingObjects As String
    Private excludeLibraryObjects As String
    Private excludeTaxiwaySignObjects As String
    Private excludeTriggerObjects As String
    Private excludeWindsockObjects As String



    Friend Sub ExcludeInsertMode(ByRef Button As Short, ByRef Shift As Short, ByVal X As Integer, ByVal Y As Integer)

        If Button = 1 Then

            If Shift = 1 Then ' pick the SHIFT DOWN
                SomeSelected = SomeSelected Or IsExcludeSelected(LonDispWest * PixelsPerLonDeg + X, LatDispNorth * PixelsPerLatDeg - Y)
                RebuildDisplay()
                If SomeSelected Then
                    SetDelay(200)
                    frmStart.CopyMenuItem.Enabled = False
                    frmStart.DeleteMenuItem.Enabled = False
                    MoveON = True
                    FirstMOVE = True
                    AuxXInt = X
                    AuxYInt = Y
                End If
                Exit Sub
            End If

            ExcludeSizeIndex = IsExcludeSizeIndex(X, Y)
            If ExcludeSizeIndex > 0 Then
                frmStart.SetMouseIcon()
                Exit Sub
            End If

            SelectAllExcludes(False)
            SomeSelected = IsExcludeSelected(LonDispWest * PixelsPerLonDeg + X, LatDispNorth * PixelsPerLatDeg - Y)
            RebuildDisplay()
            If SomeSelected Then
                SetDelay(200)
                frmStart.CopyMenuItem.Enabled = False
                frmStart.DeleteMenuItem.Enabled = False
                MoveON = True
                FirstMOVE = True
                AuxXInt = X
                AuxYInt = Y
                Exit Sub
            Else
                AuxXInt = X
                AuxYInt = Y
                DrawExclude = True
            End If
        End If

        If Button = 2 Then
            XPOP = X
            YPOP = Y
            ProcessPopUp(X, Y)
        End If

    End Sub

    Friend Sub FormExclude(ByVal X As Integer, ByVal Y As Double)

        POPIndex = NoOfExcludes + 1
        frmExcludesP.AddNewExclude(X, Y)

    End Sub

    Friend Function IsExcludeSizeIndex(ByVal X As Integer, ByVal Y As Integer) As Integer

        ' on entry X Y contain distance from NW corner display in pixels

        Dim N As Integer
        Dim P1, P2, P3, P4 As PointF

        Dim dlat, dlon As Double
        Dim Flag As Boolean

        IsExcludeSizeIndex = 0

        For N = 1 To NoOfExcludes

            dlat = Excludes(N).NLAT - Excludes(N).SLAT
            dlon = Excludes(N).ELON - Excludes(N).WLON
            Flag = dlat * PixelsPerLatDeg + dlon * PixelsPerLonDeg < 20
            If Flag Then GoTo next_N

            P1.X = (Excludes(N).WLON - LonDispWest) * PixelsPerLonDeg
            P1.Y = (LatDispNorth - Excludes(N).NLAT) * PixelsPerLatDeg

            If P1.X > X + 5 Then GoTo next_2
            If P1.X < X - 5 Then GoTo next_2
            If P1.Y < Y - 5 Then GoTo next_2
            If P1.Y > Y + 5 Then GoTo next_2
            IsExcludeSizeIndex = N
            If Not Excludes(N).Selected Then NoOfExcludesSelected = NoOfExcludesSelected + 1
            Excludes(N).Selected = True
            Exit Function

next_2:

            P2.X = (Excludes(N).ELON - LonDispWest) * PixelsPerLonDeg
            P2.Y = P1.Y
            If P2.X > X + 5 Then GoTo next_3
            If P2.X < X - 5 Then GoTo next_3
            If P2.Y < Y - 5 Then GoTo next_3
            If P2.Y > Y + 5 Then GoTo next_3
            IsExcludeSizeIndex = N
            If Not Excludes(N).Selected Then NoOfExcludesSelected = NoOfExcludesSelected + 1
            Excludes(N).Selected = True
            Exit Function

next_3:

            P3.X = P2.X
            P3.Y = (LatDispNorth - Excludes(N).SLAT) * PixelsPerLatDeg
            If P3.X > X + 5 Then GoTo next_4
            If P3.X < X - 5 Then GoTo next_4
            If P3.Y < Y - 5 Then GoTo next_4
            If P3.Y > Y + 5 Then GoTo next_4
            IsExcludeSizeIndex = N
            If Not Excludes(N).Selected Then NoOfExcludesSelected = NoOfExcludesSelected + 1
            Excludes(N).Selected = True
            Exit Function

next_4:

            P4.X = P1.X
            P4.Y = P3.Y
            If P4.X > X + 5 Then GoTo next_N
            If P4.X < X - 5 Then GoTo next_N
            If P4.Y < Y - 5 Then GoTo next_N
            If P4.Y > Y + 5 Then GoTo next_N
            IsExcludeSizeIndex = N
            If Not Excludes(N).Selected Then NoOfExcludesSelected = NoOfExcludesSelected + 1
            Excludes(N).Selected = True
            Exit Function

next_N:
        Next N


    End Function

    Friend Sub SizeExclude(ByVal X As Integer, ByVal Y As Integer, ByVal N As Integer)

        Dim lat, lon As Double
        Dim latY, lonX As Double
        Dim dlat, dlon As Double

        lat = (Excludes(N).NLAT + Excludes(N).SLAT) / 2
        lon = (Excludes(N).WLON + Excludes(N).ELON) / 2
        lonX = LonDispWest + X / PixelsPerLonDeg
        latY = LatDispNorth - Y / PixelsPerLatDeg
        dlat = Math.Abs(latY - lat)
        dlon = Math.Abs(lonX - lon)

        Excludes(N).NLAT = lat + dlat
        Excludes(N).SLAT = lat - dlat
        Excludes(N).WLON = lon - dlon
        Excludes(N).ELON = lon + dlon
        RebuildDisplay()

    End Sub

    Friend Function IsPtInExclude(ByVal N As Integer, ByVal X As Double, ByVal Y As Double) As Boolean

        Dim X2, X1, Y1, Y2 As Double

        X1 = Excludes(N).WLON * PixelsPerLonDeg
        X2 = Excludes(N).ELON * PixelsPerLonDeg

        Y1 = Excludes(N).NLAT * PixelsPerLatDeg
        Y2 = Excludes(N).SLAT * PixelsPerLatDeg

        IsPtInExclude = False

        If X > X2 + 3 Then Exit Function
        If X < X1 - 3 Then Exit Function
        If y < Y2 - 3 Then Exit Function
        If y > Y1 + 3 Then Exit Function

        If X < X2 - 3 Then
            If X > X1 + 3 Then
                If y > Y2 + 3 Then
                    If y < Y1 - 3 Then Exit Function
                End If
            End If
        End If

        IsPtInExclude = True

    End Function
    Friend Sub SelectExcludesInBox(ByVal X0 As Double, ByVal Y0 As Double, ByVal X1 As Double, ByVal Y1 As Double)

        Dim N As Integer

        If Not ExcludeVIEW Then Exit Sub

        For N = 1 To NoOfExcludes
            If Excludes(N).ELON < X1 Then
                If Excludes(N).WLON > X0 Then
                    If Excludes(N).SLAT > Y1 Then
                        If Excludes(N).NLAT < Y0 Then
                            If Not Excludes(N).Selected Then NoOfExcludesSelected = NoOfExcludesSelected + 1
                            SomeSelected = True
                            Excludes(N).Selected = True
                        End If
                    End If
                End If
            End If
        Next N

    End Sub


    Friend Sub SelectAllExcludes(ByRef Flag As Boolean)

        Dim N As Integer

        If Not ExcludeVIEW Then Exit Sub

        If Flag Then
            frmStart.SelectAllExcludesMenuItem.Checked = True
        Else
            frmStart.SelectAllExcludesMenuItem.Checked = False
        End If

        For N = 1 To NoOfExcludes
            If Flag Then
                If Not Excludes(N).Selected Then NoOfExcludesSelected = NoOfExcludesSelected + 1
                SomeSelected = True
            Else
                If Excludes(N).Selected Then NoOfExcludesSelected = NoOfExcludesSelected - 1
            End If
            Excludes(N).Selected = Flag
        Next N

    End Sub

    Friend Sub DisplayExcludes(ByVal g As Graphics)

        Dim X, Y As Single
        Dim P1, P2, P3, P4 As PointF

        Dim N As Integer
        Dim Flag As Boolean

        Dim myPen As New System.Drawing.Pen(Color.Black)
        Dim myBrush As New System.Drawing.SolidBrush(Color.SpringGreen)

        Dim lat, lon, dlat, dlon As Double

        For N = 1 To NoOfExcludes
            If Excludes(N).NLAT < LatDispSouth Then GoTo JumpHere
            If Excludes(N).SLAT > LatDispNorth Then GoTo JumpHere
            If Excludes(N).WLON > LonDispEast Then GoTo JumpHere
            If Excludes(N).ELON < LonDispWest Then GoTo JumpHere

            If Excludes(N).Selected Then
                'myBrush.Color = Color.SpringGreen
                myPen.Color = Color.SpringGreen
            Else
                ' myBrush.Color = Color.Yellow
                myPen.Color = Color.Black
            End If

            dlat = Excludes(N).NLAT - Excludes(N).SLAT
            dlon = Excludes(N).ELON - Excludes(N).WLON
            Flag = dlat * PixelsPerLatDeg + dlon * PixelsPerLonDeg < 20
            If Flag Then
                lat = (Excludes(N).NLAT + Excludes(N).SLAT) / 2
                lon = (Excludes(N).WLON + Excludes(N).ELON) / 2
                X = (lon - LonDispWest) * PixelsPerLonDeg
                Y = (LatDispNorth - lat) * PixelsPerLatDeg
                If Excludes(N).Selected Then
                    g.FillRectangle(myBrush, X - 3, Y - 3, 6, 6)
                Else
                    myPen.Width = 1
                    g.DrawRectangle(myPen, X - 3, Y - 3, 6, 6)
                End If
                GoTo JumpHere
            End If

            P1.X = (Excludes(N).WLON - LonDispWest) * PixelsPerLonDeg
            P2.X = (Excludes(N).ELON - LonDispWest) * PixelsPerLonDeg
            P3.X = P2.X
            P4.X = P1.X

            P1.Y = (LatDispNorth - Excludes(N).NLAT) * PixelsPerLatDeg
            P2.Y = P1.Y
            P3.Y = (LatDispNorth - Excludes(N).SLAT) * PixelsPerLatDeg
            P4.Y = P3.Y

            If ExcludeON Then
                myPen.Width = 1
                g.DrawRectangle(myPen, P1.X - 3, P1.Y - 3, 6, 6)
                g.DrawRectangle(myPen, P2.X - 3, P2.Y - 3, 6, 6)
                g.DrawRectangle(myPen, P3.X - 3, P3.Y - 3, 6, 6)
                g.DrawRectangle(myPen, P4.X - 3, P4.Y - 3, 6, 6)
            End If

            myPen.Width = 2
            g.DrawLine(myPen, P1, P2)
            g.DrawLine(myPen, P2, P3)
            g.DrawLine(myPen, P3, P4)
            g.DrawLine(myPen, P4, P1)

JumpHere:
        Next N

        myBrush.Dispose()
        myPen.Dispose()

    End Sub
    Friend Sub DeleteExclude(ByVal N As Integer)

        Dim K As Integer

        Dirty = True

        If Not SkipBackUp Then BackUp()
        If Excludes(N).Selected Then NoOfExcludesSelected = NoOfExcludesSelected - 1

        If N < NoOfExcludes Then
            For K = N To NoOfExcludes - 1
                Excludes(K).Flag = Excludes(K + 1).Flag
                Excludes(K).Selected = Excludes(K + 1).Selected
                Excludes(K).NLAT = Excludes(K + 1).NLAT
                Excludes(K).SLAT = Excludes(K + 1).SLAT
                Excludes(K).ELON = Excludes(K + 1).ELON
                Excludes(K).WLON = Excludes(K + 1).WLON
            Next K
        End If

        If NoOfExcludes > 1 Then
            ReDim Preserve Excludes(NoOfExcludes - 1)
        End If

        NoOfExcludes = NoOfExcludes - 1

    End Sub

    Friend Function IsExcludeSelected(ByVal X As Double, ByVal Y As Double) As Boolean

        Dim N As Integer
        Dim retval As Boolean

        IsExcludeSelected = False
        If Not ExcludeVIEW Then Exit Function

        For N = 1 To NoOfExcludes
            retval = IsPtInExclude(N, X, Y)
            If retval Then
                If Excludes(N).Selected = False Then NoOfExcludesSelected = NoOfExcludesSelected + 1
                Excludes(N).Selected = True
                IsExcludeSelected = True
            End If
        Next N

    End Function

    Friend Function IsPointInExclude(ByVal X As Double, ByVal Y As Double) As Integer

        Dim X2, X1, Y1, Y2 As Double
        Dim N As Integer

        X = LonDispCenter * PixelsPerLonDeg + X
        y = LatDispCenter * PixelsPerLatDeg - y

        IsPointInExclude = 0

        For N = 1 To NoOfExcludes

            X1 = Excludes(N).WLON * PixelsPerLonDeg
            Y1 = Excludes(N).NLAT * PixelsPerLatDeg

            If X < X1 + 3 Then
                If X > X1 - 3 Then
                    If y > Y1 - 3 Then
                        If y < Y1 + 3 Then
                            AuxX = (Excludes(N).ELON - LonDispWest) * PixelsPerLonDeg
                            AuxY = (LatDispNorth - Excludes(N).SLAT) * PixelsPerLatDeg
                            IsPointInExclude = N
                            Exit Function
                        End If
                    End If
                End If
            End If

            X2 = Excludes(N).ELON * PixelsPerLonDeg
            Y2 = Excludes(N).SLAT * PixelsPerLatDeg

            If X < X2 + 3 Then
                If X > X2 - 3 Then
                    If y > Y2 - 3 Then
                        If y < Y2 + 3 Then
                            AuxX = (Excludes(N).WLON - LonDispWest) * PixelsPerLonDeg
                            AuxY = (LatDispNorth - Excludes(N).NLAT) * PixelsPerLatDeg
                            IsPointInExclude = N
                            Exit Function
                        End If
                    End If
                End If
            End If
        Next N

    End Function

    
    Friend Sub MoveSelectedExcludes(ByVal x As Double, ByVal y As Double)

        Dim N As Integer

        For N = 1 To NoOfExcludes
            If Excludes(N).Selected Then
                Excludes(N).NLAT = Excludes(N).NLAT - y
                Excludes(N).SLAT = Excludes(N).SLAT - y
                Excludes(N).WLON = Excludes(N).WLON + x
                Excludes(N).ELON = Excludes(N).ELON + x
            End If
        Next N

    End Sub

    Friend Sub MakeBGLExcludes(ByVal CopyBGLs As Boolean)

        Dim N As Integer
        Dim a, b As String

        Dim myFile As String = "000_" & ProjectName
        myFile = Replace(myFile, " ", "_")
        a = My.Application.Info.DirectoryPath & "\tools\work\" & myFile & ".xml"

        Dim settings As XmlWriterSettings = New XmlWriterSettings With {
            .Indent = True,
            .Encoding = Encoding.GetEncoding(28591),
            .NewLineOnAttributes = True
        }

        Dim writer As XmlWriter = XmlWriter.Create(a, settings)
        writer.WriteStartDocument()
        writer.WriteComment("Created by SBuilderX on " & Now)
        writer.WriteStartElement("FSData")
        writer.WriteAttributeString("version", "9.0")
        writer.WriteAttributeString("xmlns", "xsi", Nothing, "http://www.w3.org/2001/XMLSchema-instance")
        writer.WriteAttributeString("noNamespaceSchemaLocation", "http://www.w3.org/2001/XMLSchema-instance", "bglcomp.xsd")
        For N = 1 To NoOfExcludes
            If Excludes(N).Selected Then
                GetFlag(N)
                writer.WriteStartElement("ExclusionRectangle")
                writer.WriteAttributeString("latitudeMinimum", Str(Excludes(N).SLAT))
                writer.WriteAttributeString("latitudeMaximum", Str(Excludes(N).NLAT))
                writer.WriteAttributeString("longitudeMinimum", Str(Excludes(N).WLON))
                writer.WriteAttributeString("longitudeMaximum", Str(Excludes(N).ELON))
                writer.WriteAttributeString("excludeAllObjects", excludeAllObjects)
                writer.WriteAttributeString("excludeBeaconObjects", excludeBeaconObjects)
                writer.WriteAttributeString("excludeEffectObjects", excludeEffectObjects)
                writer.WriteAttributeString("excludeExtrusionBridgeObjects", excludeExtrusionBridgeObjects)
                writer.WriteAttributeString("excludeGenericBuildingObjects", excludeGenericBuildingObjects)
                writer.WriteAttributeString("excludeLibraryObjects", excludeLibraryObjects)
                writer.WriteAttributeString("excludeTaxiwaySignObjects", excludeTaxiwaySignObjects)
                writer.WriteAttributeString("excludeTriggerObjects", excludeTriggerObjects)
                writer.WriteAttributeString("excludeWindsockObjects", excludeWindsockObjects)
                writer.WriteEndElement()
            End If
        Next
        writer.WriteFullEndElement()
        writer.Close()

        ' delete BGL File
        Dim BGLFile As String = My.Application.Info.DirectoryPath & "\tools\work\" & myFile & ".BGL"
        If File.Exists(BGLFile) Then File.Delete(BGLFile)

        ChDrive(My.Application.Info.DirectoryPath)
        ChDir(My.Application.Info.DirectoryPath & "\tools\")

        a = My.Application.Info.DirectoryPath & "\tools\bglcomp.exe"
        b = "work\" & myFile & ".xml"

        Dim myProcess As New Process
        myProcess = Process.Start(a, b)
        myProcess.WaitForExit()
        myProcess.Dispose()

        ' added this to catch errors June 30 2009
        If Not File.Exists(BGLFile) Then
            a = "BGLComp could not produce the file" & vbCrLf & BGLFile & vbCrLf
            a = a & "Try to compile the file ..\tools\" & b & " in a MSDOS window" & vbCrLf
            a = a & "to read the error report!"
            MsgBox(a, MsgBoxStyle.Critical)
        End If

        If Not CopyBGLs Then Exit Sub

        Try
            a = My.Application.Info.DirectoryPath & "\tools\work\" & myFile & ".BGL"
            If File.Exists(a) Then
                File.Copy(a, BGLProjectFolder & "\" & myFile & ".BGL", True)
            End If
        Catch ex As Exception
            MsgBox("Copying BGL files failed! Try to close FSX.", MsgBoxStyle.Information)
        End Try

    End Sub

    Private Sub GetFlag(ByVal N As Integer)

        N = Excludes(N).Flag

        excludeAllObjects = "FALSE"
        excludeBeaconObjects = "FALSE"
        excludeEffectObjects = "FALSE"
        excludeExtrusionBridgeObjects = "FALSE"
        excludeGenericBuildingObjects = "FALSE"
        excludeLibraryObjects = "FALSE"
        excludeTaxiwaySignObjects = "FALSE"
        excludeTriggerObjects = "FALSE"
        excludeWindsockObjects = "FALSE"

        If N And 1 Then excludeAllObjects = "TRUE"
        If N And 2 Then excludeBeaconObjects = "TRUE"
        If N And 4 Then excludeEffectObjects = "TRUE"
        If N And 8 Then excludeGenericBuildingObjects = "TRUE"
        If N And 16 Then excludeLibraryObjects = "TRUE"
        If N And 32 Then excludeTaxiwaySignObjects = "TRUE"
        If N And 64 Then excludeTriggerObjects = "TRUE"
        If N And 128 Then excludeWindsockObjects = "TRUE"
        If N And 256 Then excludeExtrusionBridgeObjects = "TRUE"

    End Sub

End Module