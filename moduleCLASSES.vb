Module moduleCLASSES

    ' This structure is used for the classes of land and water.
    Friend Structure LWClass
        Dim Index As Byte
        Dim Texture As String
        Dim Caption As String
        Dim Color As Color
    End Structure

    ' File Lands.txt holds 120 classes of land. When SBuilderX starts it
    ' fills the array LC() which has 256 elements (0 to 255) by reading
    ' that file. For example the 3rd entry in Lands.txt is:
    ' Name=135_Y_Golf_Course
    ' Color=FF646946
    ' Textures=135b2su1
    ' This data will be used to fill element LC(3) (we do not use index 0)
    ' as follows:
    ' LC(3).Index = 135
    ' LC(3).Texture = 135b2su1
    ' LC(3).Caption = Y_Golf_Course
    ' LC(3).Color = FF646946
    ' Since elements from 121 to 255 are not used, we use the free space
    ' to handle the selection state of class tiles (shown in green in SBuilderX). So:
    ' LC(3 + 128).Index = 135
    ' LC(3 + 128).Texture = sel        sel.bmp is a green texture
    ' LC(3 + 128).Caption =            leave blank as we do not use it
    ' LC(3 + 128).Color = FF00FF00     which is green

    Friend LC(255) As LWClass
    Friend NoOfLCs As Integer ' taken from Lands.txt =120 at present
    Friend ILC(255) As Byte  ' we set this array at the start. 
    ' For the example above when we read the 3rd element in Lands.txt
    ' ILC(135) = 3
    ' this is useful to get the index of LC() given the FSX landclass
    Friend DefaultLC As Byte = 12 ' we need one > INI file


    Friend Structure JKCR
        Dim J As Integer
        Dim K As Integer
        Dim C As Integer
        Dim R As Integer
    End Structure

    ' Land (or Water) Class Tiles mean quads (of LOD13 size) with a
    ' position defined by values J K C R. Both Land and Water Class Tiles
    ' use the following structure. J and K refer to the LOD5 quad position
    ' to which the Tile belongs. J varies from 0 to 95 (west to east) and
    ' K varies from 0 to 63 (north to south). C and R (Column and Row) refer
    ' to the position of the Tile inside the LOD5 quad. They vary from 0 to
    ' 256 (C from west to east and R from north to south). Note that each
    ' LOD5 quad has 257 x 257 tiles and they overlap. Two adjacent LOD5 quads
    ' share a common border. For example when we change the value of tile
    ' (J=12 K=23 C=256 R=154) we need to change also the value of the adjacent
    ' tile (J=13 K=23 C=0 R=154).

    ' There are 96 X 64 = 6,144 LOD5 quads in the world and each LOD5 quad contains
    ' 256 x 256 = 65,536 LOD13 quads (we do not count twice the replicated quads
    ' at the LOD5 borders. So the total number of LOD13 quads is 402,653,184. In
    ' order to store the values (bytes) that define the classes of land for all
    ' 402,653,184 tiles, we would need an array of bytes of that dimension. And another
    ' array of the same dimension to represent all the tiles of water.

    ' In order to minimize the size of the arrays that represent the land and water
    ' tiles, we use the following structure:
    <Serializable()> Friend Structure LWXY
        Dim Pointer As Integer     ' points to the 3d dimension of LLands(,,)
        Dim NoOfLWs As Integer     ' number of "live" tiles in the LOD13
    End Structure

    ' the following 2 dimensional array:
    Friend LL_XY(95, 63) As LWXY
    Friend NoOfLLXYs As Integer

    ' and the following 3-D array of bytes
    Friend LLands(,,) As Byte
    Friend NoOfLands As Integer = 0 ' number of land tiles 

    ' To understand this, suppose we have no land tile defined. In that situation
    ' all the elements of LL_XY() will have their Field NoOfLWs = 0. This means
    ' that no "live" LOD13 exist in all the LOD5s. Now suppose we add the class
    ' definition of Y_Golf_Course to the tile (J=13 K=23 C=55 R=154). We need to make:
    ' LL_XY(13,23).NoOfLWs=1
    ' meaning that this LOD5 has now an "active" LOD13. Also NoOfLLXYs is incremented
    ' (in this case from 0 to 1). Now an array of size LLands(256,256,1) is created and
    ' the value of 3 (see above) is place into element (55,154,1). We only need to set 
    ' LL_XY(13,23).Pointer = 1 (see below). We also need to increment NoOfLands by one.
    ' Say you add the same class to tile (J=13 K=23 C=55 R=164). As it is on the same
    ' LOD13 you only increment LL_XY(13,23).NoOfLWs and NoOfLands and set
    ' LLands(55,154,1) = 3. Finaly say you add the same class (index 3) to tile
    ' (J=88 K=89 C=50 R=100). Now we need to create a new array LLands(256,256,2)
    ' and increment LL_XY(88,89).NoOfLWs and set LL_XY(88,89).Pointer = 2 to point
    ' to the LLands(256,256,2) array ....

    ' some parameters that control display
    Friend NoOfLandsSelected As Integer = 0
    Friend LandVIEW As Boolean = False
    Friend LandON As Boolean
    Friend LandsSelected As Boolean

    ' NOW ALL THE EQUIVALENT FOR WATER WITHOUT COMMENTS

    Friend WC() As LWClass
    Friend NoOfWCs As Integer
    Friend IWC(255) As Byte  ' added 2009 March
    Friend DefaultWC As Byte = 1

    Friend WW_XY(95, 63) As LWXY
    Friend NoOfWWXYs As Integer

    Friend WWaters(,,) As Byte
    Friend NoOfWaters As Integer = 0

    Friend NoOfWatersSelected As Integer = 0
    Friend WaterVIEW As Boolean = False
    Friend WaterON As Boolean
    Friend WatersSelected As Boolean

    Friend LColPickON As Boolean

    ' common to land and  water
    Friend BrushSize As Integer
    Friend LandWaterDELETE As Boolean
    Friend LandWaterRASTER As Boolean
    Friend LandWaterRasON As Boolean

    ' the following are used with Class Maps
    <Serializable()> Friend Structure LWCIndex
        Dim Text As String
        Dim IsLand As Boolean ' not exported 
        Dim Class1 As Byte ' not exported 
        Dim Class2 As Byte ' not exported 
        Dim Class3 As Byte ' not exported 
        Dim Color As Color
    End Structure
    Friend LWCIs(1) As LWCIndex
    Friend NoOfLWCIs As Integer

    Friend Sub LandInsertMode(ByRef Button As Short, ByVal X As Integer, ByVal Y As Integer)
        ' here is what happens when we insert a land tile

        Dim X1, Y1 As Double

        If Button = 1 Then
            X1 = LonDispWest + X / PixelsPerLonDeg
            Y1 = LatDispNorth - Y / PixelsPerLatDeg
            FormLand(X1, Y1)
            RebuildDisplay()
        End If
        If Button = 2 Then
            RebuildDisplay()
            ProcessPopUp(X, Y)
        End If

    End Sub

    Friend Sub WaterInsertMode(ByRef Button As Short, ByVal X As Integer, ByVal Y As Integer)

        Dim X1, Y1 As Double

        If Button = 1 Then
            X1 = LonDispWest + X / PixelsPerLonDeg
            Y1 = LatDispNorth - Y / PixelsPerLatDeg
            FormWater(X1, Y1)
            RebuildDisplay()
        End If
        If Button = 2 Then
            RebuildDisplay()
            ProcessPopUp(X, Y)
        End If


    End Sub

    Friend Sub LandRasterMode(ByVal X As Integer, ByVal Y As Integer)

        Dim X1, Y1 As Double

        X1 = LonDispWest + X / PixelsPerLonDeg
        Y1 = LatDispNorth - Y / PixelsPerLatDeg
        FormLand(X1, Y1)
        RebuildDisplay()

    End Sub

    Friend Sub WaterRasterMode(ByVal X As Integer, ByVal Y As Integer)

        Dim X1, Y1 As Double

        X1 = LonDispWest + X / PixelsPerLonDeg
        Y1 = LatDispNorth - Y / PixelsPerLatDeg
        FormWater(X1, Y1)
        RebuildDisplay()

    End Sub

    Private Sub FormLand(ByVal X As Double, ByVal Y As Double)

        Dim J, K, C, R As Integer

        If BackUpON Then BackUp()

        X = X + 180
        X = X / D5Lon
        J = CInt(Fix(X))

        Y = 90 - Y
        Y = Y / D5Lat
        K = CInt(Fix(Y))

        C = CInt(Math.Round((X - J) * D5Lon / D13Lon))
        R = CInt(Math.Round((Y - K) * D5Lat / D13Lat))

        Dim N1, N2, L, M, CC, RR As Integer
        N1 = 1 - BrushSize
        N2 = BrushSize - 1
        For L = N1 To N2
            For M = N1 To N2
                CC = C + L
                RR = R + M
                If CC < 0 Then CC = 0
                If CC > 256 Then CC = 256
                If RR < 0 Then RR = 0
                If RR > 256 Then RR = 256
                FormOneLand(J, K, CC, RR, DefaultLC)
            Next
        Next

    End Sub

    Friend Sub FormWater(ByVal X As Double, ByVal Y As Double)

        Dim J, K, C, R As Integer

        If BackUpON Then BackUp()

        X = X + 180
        X = X / D5Lon
        J = CInt(Fix(X))

        Y = 90 - Y
        Y = Y / D5Lat
        K = CInt(Fix(Y))

        C = CInt(Math.Round((X - J) * D5Lon / D13Lon))
        R = CInt(Math.Round((Y - K) * D5Lat / D13Lat))

        Dim N1, N2, L, M, CC, RR As Integer
        N1 = 1 - BrushSize
        N2 = BrushSize - 1
        For L = N1 To N2
            For M = N1 To N2
                CC = C + L
                RR = R + M
                If CC < 0 Then CC = 0
                If CC > 256 Then CC = 256
                If RR < 0 Then RR = 0
                If RR > 256 Then RR = 256
                FormOneWater(J, K, CC, RR, DefaultWC)
            Next
        Next

    End Sub

    Private Sub FormOneLand(ByVal J As Integer, ByVal K As Integer, ByVal C As Integer, ByVal R As Integer, ByVal LC As Byte)

        ' check if the tiles should be replicated in adjacent LOD5 quads
        FormLandJKCR(J, K, C, R, LC)
        If C = 0 Then
            FormLandJKCR(J - 1, K, 256, R, LC)
            If R = 0 Then FormLandJKCR(J - 1, K - 1, 256, 256, LC)
            If R = 256 Then FormLandJKCR(J - 1, K + 1, 256, 0, LC)
        End If
        If C = 256 Then
            FormLandJKCR(J + 1, K, 0, R, LC)
            If R = 0 Then FormLandJKCR(J + 1, K - 1, 0, 256, LC)
            If R = 256 Then FormLandJKCR(J + 1, K + 1, 0, 0, LC)
        End If
        If R = 0 Then FormLandJKCR(J, K - 1, C, 256, LC)
        If R = 256 Then FormLandJKCR(J, K + 1, C, 0, LC)

    End Sub

    Private Sub FormOneWater(ByVal J As Integer, ByVal K As Integer, ByVal C As Integer, ByVal R As Integer, ByVal WC As Byte)

        FormWaterJKCR(J, K, C, R, WC)
        If C = 0 Then
            FormWaterJKCR(J - 1, K, 256, R, WC)
            If R = 0 Then FormWaterJKCR(J - 1, K - 1, 256, 256, WC)
            If R = 256 Then FormWaterJKCR(J - 1, K + 1, 256, 0, WC)
        End If
        If C = 256 Then
            FormWaterJKCR(J + 1, K, 0, R, WC)
            If R = 0 Then FormWaterJKCR(J + 1, K - 1, 0, 256, WC)
            If R = 256 Then FormWaterJKCR(J + 1, K + 1, 0, 0, WC)
        End If
        If R = 0 Then FormWaterJKCR(J, K - 1, C, 256, WC)
        If R = 256 Then FormWaterJKCR(J, K + 1, C, 0, WC)

    End Sub

    Private Sub FormLandJKCR(ByVal J As Integer, ByVal K As Integer, ByVal C As Integer, ByVal R As Integer, ByVal LC As Byte)

        Dim N, P As Integer

        N = LL_XY(J, K).NoOfLWs
        P = LL_XY(J, K).Pointer

        If N = 0 Then

            If P = 0 Then
                LL_XY(J, K).Pointer = NoOfLLXYs
                ReDim Preserve LLands(256, 256, NoOfLLXYs)
                LLands(C, R, NoOfLLXYs) = LC
                LL_XY(J, K).NoOfLWs = 1
                NoOfLands = NoOfLands + 1
                NoOfLLXYs = NoOfLLXYs + 1
            Else
                LLands(C, R, P) = LC
                LL_XY(J, K).NoOfLWs = 1
                NoOfLands = NoOfLands + 1
            End If

        Else

            If LLands(C, R, P) = 0 Then ' means this is an add! not a edit!
                NoOfLands = NoOfLands + 1
                LL_XY(J, K).NoOfLWs = N + 1
            End If
            LLands(C, R, P) = LC

        End If

    End Sub

    Private Sub FormWaterJKCR(ByVal J As Integer, ByVal K As Integer, ByVal C As Integer, ByVal R As Integer, ByVal WC As Byte)

        Dim N, P As Integer

        N = WW_XY(J, K).NoOfLWs
        P = WW_XY(J, K).Pointer

        If N = 0 Then

            If P = 0 Then
                WW_XY(J, K).Pointer = NoOfWWXYs
                ReDim Preserve WWaters(256, 256, NoOfWWXYs)
                WWaters(C, R, NoOfWWXYs) = WC
                WW_XY(J, K).NoOfLWs = 1
                NoOfWaters = NoOfWaters + 1
                NoOfWWXYs = NoOfWWXYs + 1
            Else
                WWaters(C, R, P) = WC
                WW_XY(J, K).NoOfLWs = 1
                NoOfWaters = NoOfWaters + 1
            End If

        Else

            If WWaters(C, R, P) = 0 Then
                NoOfWaters = NoOfWaters + 1
                WW_XY(J, K).NoOfLWs = N + 1
            End If
            WWaters(C, R, P) = WC

        End If

    End Sub

    Friend Function AppendRawLand(ByVal fname As String, ByVal J As Integer, ByVal K As Integer) As Boolean

        frmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor
        AppendRawLand = False

        Dim A As String = Path.GetFileNameWithoutExtension(fname)

        Dim LL(256, 256) As Byte
        Try
            FileOpen(3, fname, OpenMode.Binary)
            FileGet(3, LL)
            FileClose(3)
        Catch ex As Exception
            Exit Function
        End Try

        Dim C, R As Integer

        For C = 0 To 256
            For R = 0 To 256
                If LL(C, R) <> 254 Then
                    FormOneLand(J, K, C, R, ILC(LL(C, R)))
                End If
            Next
        Next

        LonDispCenter = (J + 0.5) * D5Lon - 180
        LatDispCenter = 90 - (K + 0.5) * D5Lat
        Zoom = 7

        AppendRawLand = True

    End Function

    Friend Function AppendRawWater(ByVal fname As String, ByVal J As Integer, ByVal K As Integer) As Boolean

        frmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor
        AppendRawWater = False
        Dim A As String = Path.GetFileNameWithoutExtension(fname)

        Dim WW(256, 256) As Byte
        Try
            FileOpen(3, fname, OpenMode.Binary)
            FileGet(3, WW)
            FileClose(3)
        Catch ex As Exception
            Exit Function
        End Try

        Dim C, R As Integer

        For C = 0 To 256
            For R = 0 To 256
                If WW(C, R) <> 254 Then
                    FormOneWater(J, K, C, R, IWC(WW(C, R)))
                End If
            Next
        Next

        LonDispCenter = (J + 0.5) * D5Lon - 180
        LatDispCenter = 90 - (K + 0.5) * D5Lat
        Zoom = 7

        AppendRawWater = True

    End Function

    Friend Sub SelectAllLands(ByRef Flag As Boolean)

        Dim C, R, P, N, K, LA, LO As Integer
        Dim BT0 As Byte
        Dim BY127 As Byte = 127    ' October 2017
        Dim BY128 As Byte = 128

        FrmStart.SelectAllLandsMenuItem.Checked = Flag

        If Not LandVIEW Then Exit Sub
        If Flag = False And LandsSelected = False Then Exit Sub

        WAIT = True
        frmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor

        If Flag Then
            NoOfLands = 0
            For LO = 0 To 95
                For LA = 0 To 63
                    If LL_XY(LO, LA).NoOfLWs > 0 Then
                        P = LL_XY(LO, LA).Pointer
                        N = LL_XY(LO, LA).NoOfLWs
                        K = 0
                        For C = 0 To 256
                            For R = 0 To 256
                                BT0 = LLands(C, R, P)
                                If BT0 > 0 Then
                                    NoOfLands = NoOfLands + 1
                                    If BT0 < 128 Then NoOfLandsSelected = NoOfLandsSelected + 1
                                    LLands(C, R, P) = BY128 Or BT0
                                    K = K + 1
                                    If K = N Then GoTo next_LL_XY1
                                End If
                            Next
                        Next
                    End If
next_LL_XY1:
                Next
            Next
            SomeSelected = True
            LandsSelected = True

        Else
            For LO = 0 To 95
                For LA = 0 To 63
                    If LL_XY(LO, LA).NoOfLWs > 0 Then
                        P = LL_XY(LO, LA).Pointer
                        N = LL_XY(LO, LA).NoOfLWs
                        K = 0
                        For C = 0 To 256
                            For R = 0 To 256
                                BT0 = LLands(C, R, P)
                                If BT0 > 0 Then
                                    If BT0 > 127 Then NoOfLandsSelected = NoOfLandsSelected - 1
                                    LLands(C, R, P) = BY127 And BT0
                                    K = K + 1
                                    If K = N Then GoTo next_LL_XY2
                                End If
                            Next
                        Next
                    End If
next_LL_XY2:
                Next
            Next
            LandsSelected = False
        End If

        frmStart.SetMouseIcon()
        WAIT = False

    End Sub

    Friend Sub SelectAllWaters(ByRef Flag As Boolean)

        Dim C, R, P, N, K, LA, LO As Integer
        Dim BT0 As Byte
        Dim BY127 As Byte = 127    ' October 2017
        Dim BY128 As Byte = 128

        FrmStart.SelectAllWatersMenuItem.Checked = Flag

        If Not WaterVIEW Then Exit Sub
        If Flag = False And WatersSelected = False Then Exit Sub

        WAIT = True
        frmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor

        If Flag Then
            NoOfWaters = 0
            For LO = 0 To 95
                For LA = 0 To 63
                    If WW_XY(LO, LA).NoOfLWs > 0 Then
                        P = WW_XY(LO, LA).Pointer
                        N = WW_XY(LO, LA).NoOfLWs
                        K = 0
                        For C = 0 To 256
                            For R = 0 To 256
                                BT0 = WWaters(C, R, P)
                                If BT0 > 0 Then
                                    NoOfWaters = NoOfWaters + 1
                                    If BT0 < 128 Then NoOfWatersSelected = NoOfWatersSelected + 1
                                    WWaters(C, R, P) = BY128 Or BT0
                                    K = K + 1
                                    If K = N Then GoTo next_WW_XY1
                                End If
                            Next
                        Next
                    End If
next_WW_XY1:
                Next
            Next
            SomeSelected = True
            WatersSelected = True

        Else
            For LO = 0 To 95
                For LA = 0 To 63
                    If WW_XY(LO, LA).NoOfLWs > 0 Then
                        P = WW_XY(LO, LA).Pointer
                        N = WW_XY(LO, LA).NoOfLWs
                        K = 0
                        For C = 0 To 256
                            For R = 0 To 256
                                BT0 = WWaters(C, R, P)
                                If BT0 > 0 Then
                                    If BT0 > 127 Then NoOfWatersSelected = NoOfWatersSelected - 1
                                    WWaters(C, R, P) = BY127 And BT0
                                    K = K + 1
                                    If K = N Then GoTo next_WW_XY2
                                End If
                            Next
                        Next
                    End If
next_WW_XY2:
                Next
            Next
            WatersSelected = False
        End If

        frmStart.SetMouseIcon()
        WAIT = False

    End Sub

    Friend Sub DisplayLands(ByVal gr As Graphics)

        If NoOfLands = 0 Or Zoom < 6 Then Exit Sub

        Dim A As String

        Dim X11, X1, X2, Y1, Y2 As Single
        Dim MyL As Byte
        Dim LA1, LA, LA2 As Integer
        Dim LO1, LO, LO2 As Integer

        Dim jpg As String = ".jpg"
        Dim bmp As String = ".bmp"

        Dim myImage As Image
        Dim myBrush As New System.Drawing.SolidBrush(Color.Yellow)

        Dim LatNorth As Double
        Dim LonWest As Double

        X1 = CSng(LonDispWest + 180)
        X1 = CSng(X1 / D5Lon)
        LO1 = CInt(Fix(X1))

        Y1 = CSng(90 - LatDispNorth)
        Y1 = CSng(Y1 / D5Lat)
        LA1 = CInt(Fix(Y1))

        X1 = CSng(LonDispEast + 180)
        X1 = CSng(X1 / D5Lon)
        LO2 = CInt(Fix(X1))

        Y1 = CSng(90 - LatDispSouth)
        Y1 = CSng(Y1 / D5Lat)
        LA2 = CInt(Fix(Y1))

        Dim C, C0, R, R0, N As Integer

        For LA = LA1 To LA2
            LatNorth = 90 - LA * D5Lat
            For LO = LO1 To LO2
                LonWest = LO * D5Lon - 180
                If LL_XY(LO, LA).NoOfLWs > 0 Then
                    N = LL_XY(LO, LA).Pointer
                    X11 = CSng((LonWest - LonDispWest - D14Lon) * PixelsPerLonDeg)
                    X2 = CSng(D13Lon * PixelsPerLonDeg)
                    Y1 = CSng((LatDispNorth - LatNorth - D14Lat) * PixelsPerLatDeg)
                    Y2 = CSng(D13Lat * PixelsPerLatDeg)
                    X2 = CSng(D13Lon * PixelsPerLonDeg)

                    R0 = CInt(Fix(-Y1 / Y2))
                    If R0 > 256 Then R0 = 256
                    If R0 < 0 Then R0 = 0
                    Y1 = Y1 + R0 * Y2

                    C0 = CInt(Fix(-X11 / X2))
                    If C0 > 256 Then C0 = 256
                    If C0 < 0 Then C0 = 0
                    X11 = X11 + C0 * X2

                    For R = R0 To 256
                        X1 = X11
                        For C = C0 To 256
                            MyL = LLands(C, R, N)
                            If MyL > 0 Then
                                If Zoom < 10 Then
                                    myBrush.Color = LC(MyL).Color
                                    gr.FillRectangle(myBrush, New Rectangle(CInt(X1), CInt(Y1), CInt(X2 + 1), CInt(Y2 + 1)))
                                ElseIf (Zoom < 15) And (Zoom > 9) Then
                                    A = My.Application.Info.DirectoryPath & "\tools\bmps\" & LC(MyL).Texture & bmp
                                    myImage = System.Drawing.Image.FromFile(A)
                                    gr.DrawImage(myImage, X1, Y1, X2 + 1, Y2 + 1)
                                ElseIf Zoom > 14 Then
                                    A = My.Application.Info.DirectoryPath & "\tools\bmps\" & LC(MyL).Texture & jpg
                                    myImage = System.Drawing.Image.FromFile(A)
                                    gr.DrawImage(myImage, X1, Y1, X2 + 1, Y2 + 1)
                                End If
                            End If
                            X1 = X1 + X2
                            If X1 > DisplayWidth Then Exit For
                        Next
                        Y1 = Y1 + Y2
                        If Y1 > DisplayHeight Then Exit For
                    Next
                End If
            Next
        Next

        myBrush.Dispose()

    End Sub

    Friend Sub DisplayWaters(ByVal gr As Graphics)

        If NoOfWaters = 0 Or Zoom < 6 Then Exit Sub

        Dim A As String

        Dim X11, X1, X2, Y1, Y2 As Single
        Dim MyL As Byte
        Dim LA1, LA, LA2 As Integer
        Dim LO1, LO, LO2 As Integer

        Dim jpg As String = ".jpg"
        Dim bmp As String = ".bmp"

        Dim myImage As Image
        Dim myBrush As New System.Drawing.SolidBrush(Color.Yellow)

        Dim LatNorth As Double
        Dim LonWest As Double

        X1 = CSng(LonDispWest + 180)
        X1 = CSng(X1 / D5Lon)
        LO1 = CInt(Fix(X1))

        Y1 = CSng(90 - LatDispNorth)
        Y1 = CSng(Y1 / D5Lat)
        LA1 = CInt(Fix(Y1))

        X1 = CSng(LonDispEast + 180)
        X1 = CSng(X1 / D5Lon)
        LO2 = CInt(Fix(X1))

        Y1 = CSng(90 - LatDispSouth)
        Y1 = CSng(Y1 / D5Lat)
        LA2 = CInt(Fix(Y1))

        Dim C, C0, R, R0, N As Integer

        For LA = LA1 To LA2
            LatNorth = 90 - LA * D5Lat
            For LO = LO1 To LO2
                LonWest = LO * D5Lon - 180
                If WW_XY(LO, LA).NoOfLWs > 0 Then
                    N = WW_XY(LO, LA).Pointer
                    X11 = CSng((LonWest - LonDispWest - D14Lon) * PixelsPerLonDeg)
                    X2 = CSng(D13Lon * PixelsPerLonDeg)
                    Y1 = CSng((LatDispNorth - LatNorth - D14Lat) * PixelsPerLatDeg)
                    Y2 = CSng(D13Lat * PixelsPerLatDeg)
                    X2 = CSng(D13Lon * PixelsPerLonDeg)

                    R0 = CInt(Fix(-Y1 / Y2))
                    If R0 > 256 Then R0 = 256
                    If R0 < 0 Then R0 = 0
                    Y1 = Y1 + R0 * Y2

                    C0 = CInt(Fix(-X11 / X2))
                    If C0 > 256 Then C0 = 256
                    If C0 < 0 Then C0 = 0
                    X11 = X11 + C0 * X2

                    For R = R0 To 256
                        X1 = X11
                        For C = C0 To 256
                            MyL = WWaters(C, R, N)
                            If MyL > 0 Then
                                If Zoom < 10 Then
                                    myBrush.Color = WC(MyL).Color
                                    gr.FillRectangle(myBrush, New Rectangle(CInt(X1), CInt(Y1), CInt(X2 + 1), CInt(Y2 + 1)))
                                ElseIf (Zoom < 15) And (Zoom > 9) Then
                                    A = My.Application.Info.DirectoryPath & "\tools\bmps\" & WC(MyL).Texture & bmp
                                    myImage = System.Drawing.Image.FromFile(A)
                                    gr.DrawImage(myImage, X1, Y1, X2 + 1, Y2 + 1)
                                ElseIf Zoom > 14 Then
                                    A = My.Application.Info.DirectoryPath & "\tools\bmps\" & WC(MyL).Texture & jpg
                                    myImage = System.Drawing.Image.FromFile(A)
                                    gr.DrawImage(myImage, X1, Y1, X2 + 1, Y2 + 1)
                                End If
                            End If
                            X1 = X1 + X2
                            If X1 > DisplayWidth Then Exit For
                        Next
                        Y1 = Y1 + Y2
                        If Y1 > DisplayHeight Then Exit For
                    Next
                End If
            Next
        Next

        myBrush.Dispose()

    End Sub

    Friend Sub DeleteSelectedLands()

        Dim C, R, P, LA, LO As Integer
        Dim BT0 As Byte

        WAIT = True
        frmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor

        ' If BackUpON Then BackUp()

        For LO = 0 To 95
            For LA = 0 To 63
                If LL_XY(LO, LA).NoOfLWs > 0 Then
                    P = LL_XY(LO, LA).Pointer
                    For C = 0 To 256
                        For R = 0 To 256
                            BT0 = LLands(C, R, P)
                            If BT0 > 0 Then
                                If BT0 > 127 Then
                                    LLands(C, R, P) = 0
                                    NoOfLands = NoOfLands - 1
                                    LL_XY(LO, LA).NoOfLWs = LL_XY(LO, LA).NoOfLWs - 1
                                End If
                                If LL_XY(LO, LA).NoOfLWs = 0 Then GoTo next_LL_XY1
                            End If
                        Next
                    Next
                End If
next_LL_XY1:
            Next
        Next

        NoOfLandsSelected = 0
        LandsSelected = False
        Dirty = True

        frmStart.SetMouseIcon()
        WAIT = False

    End Sub

    Friend Sub DeleteSelectedWaters()

        Dim C, R, P, LA, LO As Integer
        Dim BT0 As Byte

        WAIT = True
        frmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor

        For LO = 0 To 95
            For LA = 0 To 63
                If WW_XY(LO, LA).NoOfLWs > 0 Then
                    P = WW_XY(LO, LA).Pointer
                    For C = 0 To 256
                        For R = 0 To 256
                            BT0 = WWaters(C, R, P)
                            If BT0 > 0 Then
                                If BT0 > 127 Then
                                    WWaters(C, R, P) = 0
                                    NoOfWaters = NoOfWaters - 1
                                    WW_XY(LO, LA).NoOfLWs = WW_XY(LO, LA).NoOfLWs - 1
                                End If
                                If WW_XY(LO, LA).NoOfLWs = 0 Then GoTo next_WW_XY1
                            End If
                        Next
                    Next
                End If
next_WW_XY1:
            Next
        Next

        NoOfWatersSelected = 0
        WatersSelected = False
        Dirty = True

        frmStart.SetMouseIcon()
        WAIT = False

    End Sub

    Friend Sub DeleteLand(ByVal X1 As Integer, ByVal Y1 As Integer)

        Dim X, Y As Double

        If NoOfLands = 0 Then Exit Sub

        X = LonDispWest + X1 / PixelsPerLonDeg
        Y = LatDispNorth - Y1 / PixelsPerLatDeg

        Dim J, K, C, R As Integer

        X = X + 180
        X = X / D5Lon
        J = CInt(Fix(X))

        Y = 90 - Y
        Y = Y / D5Lat
        K = CInt(Fix(Y))

        If LL_XY(J, K).NoOfLWs = 0 Then Exit Sub

        C = CInt(Math.Round((X - J) * D5Lon / D13Lon))
        R = CInt(Math.Round((Y - K) * D5Lat / D13Lat))

        If LLands(C, R, LL_XY(J, K).Pointer) = 0 Then Exit Sub

        BackUp()
        DeleteOneLand(J, K, C, R)

    End Sub

    Friend Sub DeleteWater(ByVal X1 As Integer, ByVal Y1 As Integer)

        Dim X, Y As Double

        If NoOfWaters = 0 Then Exit Sub

        X = LonDispWest + X1 / PixelsPerLonDeg
        Y = LatDispNorth - Y1 / PixelsPerLatDeg

        Dim J, K, C, R As Integer

        X = X + 180
        X = X / D5Lon
        J = CInt(Fix(X))

        Y = 90 - Y
        Y = Y / D5Lat
        K = CInt(Fix(Y))

        If WW_XY(J, K).NoOfLWs = 0 Then Exit Sub

        C = CInt(Math.Round((X - J) * D5Lon / D13Lon))
        R = CInt(Math.Round((Y - K) * D5Lat / D13Lat))

        If WWaters(C, R, WW_XY(J, K).Pointer) = 0 Then Exit Sub

        BackUp()
        DeleteOneWater(J, K, C, R)

    End Sub

    Private Sub DeleteOneLand(ByVal J As Integer, ByVal K As Integer, ByVal C As Integer, ByVal R As Integer)

        DeleteLandJKCR(J, K, C, R)
        If C = 0 Then
            DeleteLandJKCR(J - 1, K, 256, R)
            If R = 0 Then DeleteLandJKCR(J - 1, K - 1, 256, 256)
            If R = 256 Then DeleteLandJKCR(J - 1, K + 1, 256, 0)
        End If
        If C = 256 Then
            DeleteLandJKCR(J + 1, K, 0, R)
            If R = 0 Then DeleteLandJKCR(J + 1, K - 1, 0, 256)
            If R = 256 Then DeleteLandJKCR(J + 1, K + 1, 0, 0)
        End If
        If R = 0 Then DeleteLandJKCR(J, K - 1, C, 256)
        If R = 256 Then DeleteLandJKCR(J, K + 1, C, 0)
        Dirty = True

    End Sub

    Private Sub DeleteOneWater(ByVal J As Integer, ByVal K As Integer, ByVal C As Integer, ByVal R As Integer)

        DeleteWaterJKCR(J, K, C, R)
        If C = 0 Then
            DeleteWaterJKCR(J - 1, K, 256, R)
            If R = 0 Then DeleteWaterJKCR(J - 1, K - 1, 256, 256)
            If R = 256 Then DeleteWaterJKCR(J - 1, K + 1, 256, 0)
        End If
        If C = 256 Then
            DeleteWaterJKCR(J + 1, K, 0, R)
            If R = 0 Then DeleteWaterJKCR(J + 1, K - 1, 0, 256)
            If R = 256 Then DeleteWaterJKCR(J + 1, K + 1, 0, 0)
        End If
        If R = 0 Then DeleteWaterJKCR(J, K - 1, C, 256)
        If R = 256 Then DeleteWaterJKCR(J, K + 1, C, 0)


    End Sub

    Private Sub DeleteLandJKCR(ByVal J As Integer, ByVal K As Integer, ByVal C As Integer, ByVal R As Integer)

        LLands(C, R, LL_XY(J, K).Pointer) = 0
        LL_XY(J, K).NoOfLWs = LL_XY(J, K).NoOfLWs - 1
        NoOfLands = NoOfLands - 1

        If NoOfLands = 0 Then
            ReDim LLands(256, 256, 0)
        End If

    End Sub

    Private Sub DeleteWaterJKCR(ByVal J As Integer, ByVal K As Integer, ByVal C As Integer, ByVal R As Integer)

        WWaters(C, R, WW_XY(J, K).Pointer) = 0
        WW_XY(J, K).NoOfLWs = WW_XY(J, K).NoOfLWs - 1
        NoOfWaters = NoOfWaters - 1

        If NoOfWaters = 0 Then
            ReDim WWaters(256, 256, 0)
        End If

    End Sub

    Friend Sub DeleteLandPopUp(ByVal N As Integer)

        Dim J, K, C, R As Integer

        R = N Mod 512
        N = N >> 9
        C = N Mod 512
        N = N >> 9
        K = N Mod 64
        N = N >> 6
        J = N Mod 128

        If BackUpON Then BackUp()
        DeleteOneLand(J, K, C, R)

    End Sub


    Friend Sub DeleteWaterPopUp(ByVal N As Integer)

        Dim J, K, C, R As Integer

        R = N Mod 512
        N = N >> 9
        C = N Mod 512
        N = N >> 9
        K = N Mod 64
        N = N >> 6
        J = N Mod 128

        If BackUpON Then BackUp()
        DeleteOneWater(J, K, C, R)

    End Sub

    Friend Sub SelectLandsInBox(ByVal X0 As Double, ByVal Y0 As Double, ByVal X1 As Double, ByVal Y1 As Double)

        If Not LandVIEW Or NoOfLands = 0 Then Exit Sub

        Dim BT0 As Byte
        Dim BT128 As Byte = 128
        Dim J, J0, J1, K, K0, K1, P As Integer
        Dim C, C0, C1, C00, C11, R, R0, R1, R00, R11 As Integer

        Dim PT As JKCR

        PT = JKCRFromLL(X0, Y0, True)
        J0 = PT.J
        K0 = PT.K
        C0 = PT.C
        R0 = PT.R

        PT = JKCRFromLL(X1, Y1, False)
        J1 = PT.J
        K1 = PT.K
        C1 = PT.C
        R1 = PT.R

        For J = J0 To J1
            C00 = 0
            If J = J0 Then C00 = C0
            C11 = 256
            If J = J1 Then C11 = C1
            For K = K0 To K1
                If LL_XY(J, K).NoOfLWs > 0 Then
                    R00 = 0
                    If K = K0 Then R00 = R0
                    R11 = 256
                    If K = K1 Then R11 = R1
                    P = LL_XY(J, K).Pointer
                    For R = R00 To R11
                        For C = C00 To C11
                            BT0 = LLands(C, R, P)
                            If BT0 > 0 Then
                                If BT0 < 128 Then
                                    NoOfLandsSelected = NoOfLandsSelected + 1
                                    SomeSelected = True
                                    LandsSelected = True
                                End If
                                LLands(C, R, P) = BT0 Or BT128
                            End If
                        Next
                    Next

                End If
            Next
        Next


    End Sub

    Friend Sub SelectWatersInBox(ByVal X0 As Double, ByVal Y0 As Double, ByVal X1 As Double, ByVal Y1 As Double)

        If Not WaterVIEW Or NoOfWaters = 0 Then Exit Sub

        Dim BT0 As Byte
        Dim BT128 As Byte = 128
        Dim J, J0, J1, K, K0, K1, P As Integer
        Dim C, C0, C1, C00, C11, R, R0, R1, R00, R11 As Integer

        Dim PT As JKCR

        PT = JKCRFromLL(X0, Y0, True)
        J0 = PT.J
        K0 = PT.K
        C0 = PT.C
        R0 = PT.R

        PT = JKCRFromLL(X1, Y1, False)
        J1 = PT.J
        K1 = PT.K
        C1 = PT.C
        R1 = PT.R

        For J = J0 To J1
            C00 = 0
            If J = J0 Then C00 = C0
            C11 = 256
            If J = J1 Then C11 = C1
            For K = K0 To K1
                If WW_XY(J, K).NoOfLWs > 0 Then
                    R00 = 0
                    If K = K0 Then R00 = R0
                    R11 = 256
                    If K = K1 Then R11 = R1
                    P = WW_XY(J, K).Pointer
                    For R = R00 To R11
                        For C = C00 To C11
                            BT0 = WWaters(C, R, P)
                            If BT0 > 0 Then
                                If BT0 < 128 Then
                                    NoOfWatersSelected = NoOfWatersSelected + 1
                                    SomeSelected = True
                                    WatersSelected = True
                                End If
                                WWaters(C, R, P) = BT0 Or BT128
                            End If
                        Next
                    Next

                End If
            Next
        Next


    End Sub

    Friend Sub MakeBglLand(ByRef CopyBGLs As Boolean)

        Dim J, K, C, R, P As Integer
        Dim Flag(95, 63) As Boolean
        Dim NoOfQuads As Integer = 0
        Dim Counter As Integer = 0

        For J = 0 To 95
            For K = 0 To 63
                If LL_XY(J, K).NoOfLWs = 0 Then
                    Flag(J, K) = False
                Else
                    P = LL_XY(J, K).Pointer
                    NoOfQuads = NoOfQuads + 1
                    For R = 0 To 256
                        For C = 0 To 256
                            If LLands(C, R, P) > 127 Then
                                Flag(J, K) = True
                            End If
                        Next
                    Next
                End If
            Next
        Next

        Dim InfFile, RawFile, BGLFile, BGLFileTarget As String
        Dim BGL(NoOfQuads) As String
        Dim Command(NoOfQuads) As String
        Dim Quad(256, 256) As Byte
        Dim A As String

        ' create RAW files
        For J = 0 To 95
            For K = 0 To 63
                If Flag(J, K) Then
                    Counter = Counter + 1
                    P = LL_XY(J, K).Pointer
                    For C = 0 To 256
                        For R = 0 To 256
                            If LLands(C, R, P) = 0 Then
                                Quad(C, R) = 254
                            Else
                                Quad(C, R) = LC(LLands(C, R, P)).Index
                            End If
                        Next
                    Next
                    RawFile = "LC_" & Format(J, "00")
                    RawFile = RawFile & Format(K, "00")
                    BGL(Counter) = RawFile & ".bgl"
                    RawFile = My.Application.Info.DirectoryPath & "\tools\work\" & RawFile & ".raw"
                    FileOpen(3, RawFile, OpenMode.Binary)
                    FilePut(3, Quad)
                    FileClose(3)
                End If
            Next
        Next

        ' delete BGL files
        For J = 1 To NoOfQuads
            BGLFile = My.Application.Info.DirectoryPath & "\tools\work\" & BGL(J)
            If File.Exists(BGLFile) Then File.Delete(BGLFile)
        Next J


        Counter = 0
        For J = 0 To 95
            For K = 0 To 63
                If Flag(J, K) Then
                    Counter = Counter + 1
                    InfFile = "LC_" & Format(J, "00")
                    InfFile = InfFile & Format(K, "00")
                    Command(Counter) = "resample " & " work\" & InfFile & ".inf"
                    FileOpen(3, My.Application.Info.DirectoryPath & "\tools\work\" & InfFile & ".inf", OpenMode.Output)
                    PrintLine(3, "[Source]")
                    PrintLine(3, "   Type = Raw")
                    PrintLine(3, "   SourceDir = " & Chr(34) & "." & Chr(34))
                    PrintLine(3, "   SourceFile = " & Chr(34) & InfFile & ".raw" & Chr(34))
                    PrintLine(3, "   Layer = LandClass")
                    PrintLine(3, "   SamplingMethod = Point")
                    PrintLine(3, "   SampleType = UINT8")
                    PrintLine(3, "   NullValue = 254")
                    PrintLine(3, "   ulyMap = " & Str(90.0# - K * D5Lat))
                    PrintLine(3, "   ulxMap = " & Str(J * D5Lon - 180.0#))
                    PrintLine(3, "   nCols = " & CStr(257))
                    PrintLine(3, "   nRows = " & CStr(257))
                    PrintLine(3, "   xDim = " & Str(D13Lon))
                    PrintLine(3, "   yDim = " & Str(D13Lat))
                    PrintLine(3)
                    PrintLine(3, "[Destination]")
                    PrintLine(3, "   DestDir = " & Chr(34) & "." & Chr(34))
                    PrintLine(3, "   DestBaseFileName = " & Chr(34) & InfFile & Chr(34))
                    PrintLine(3, "   DestFileType = BGL")
                    PrintLine(3, "   UseSourceDimensions = 1")
                    FileClose(3)
                End If
            Next
        Next

        ChDrive(My.Application.Info.DirectoryPath)
        ChDir(My.Application.Info.DirectoryPath & "\tools\")

        For J = 1 To NoOfQuads
            ExecCmd(Command(J))
        Next J

        If Not CopyBGLs Then Exit Sub

        On Error GoTo Erro4

        ' copy BGL files
        For J = 1 To NoOfQuads
            BGLFile = My.Application.Info.DirectoryPath & "\tools\work\" & BGL(J)
            BGLFileTarget = BGLProjectFolder & "\" & BGL(J)
            If File.Exists(BGLFile) Then
                File.Copy(BGLFile, BGLFileTarget, True)
                'MsgBox(BGLFile & vbCrLf & BGLFileTarget)
            End If
        Next J

        Exit Sub

Erro4:

        MsgBox("Copying BGL files failed! Try to close FSX.", MsgBoxStyle.Information)

    End Sub

    Friend Sub MakeBglWater(ByRef CopyBGLs As Boolean)


        Dim J, K, C, R, P As Integer
        Dim Flag(95, 63) As Boolean
        Dim NoOfQuads As Integer = 0
        Dim Counter As Integer = 0


        For J = 0 To 95
            For K = 0 To 63
                If WW_XY(J, K).NoOfLWs = 0 Then
                    Flag(J, K) = False
                Else
                    P = WW_XY(J, K).Pointer
                    NoOfQuads = NoOfQuads + 1
                    For R = 0 To 256
                        For C = 0 To 256
                            If WWaters(C, R, P) > 127 Then
                                Flag(J, K) = True
                            End If
                        Next
                    Next
                End If
            Next
        Next

        Dim InfFile, RawFile, BGLFile, BGLFileTarget As String
        Dim BGL(NoOfQuads) As String
        Dim Command(NoOfQuads) As String
        Dim Quad(256, 256) As Byte
        Dim A As String

        ' create RAW files
        For J = 0 To 95
            For K = 0 To 63
                If Flag(J, K) Then
                    Counter = Counter + 1
                    P = WW_XY(J, K).Pointer
                    For C = 0 To 256
                        For R = 0 To 256
                            If WWaters(C, R, P) = 0 Then
                                Quad(C, R) = 254
                            Else
                                Quad(C, R) = WC(WWaters(C, R, P)).Index
                            End If
                        Next
                    Next
                    RawFile = "WC_" & Format(J, "00")
                    RawFile = RawFile & Format(K, "00")
                    BGL(Counter) = RawFile & ".bgl"
                    RawFile = My.Application.Info.DirectoryPath & "\tools\work\" & RawFile & ".raw"
                    FileOpen(3, RawFile, OpenMode.Binary)
                    FilePut(3, Quad)
                    FileClose(3)
                End If
            Next
        Next

        ' delete BGL files
        For J = 1 To NoOfQuads
            BGLFile = My.Application.Info.DirectoryPath & "\tools\work\" & BGL(J)
            If File.Exists(BGLFile) Then File.Delete(BGLFile)
        Next J


        Counter = 0
        For J = 0 To 95
            For K = 0 To 63
                If Flag(J, K) Then
                    Counter = Counter + 1
                    InfFile = "WC_" & Format(J, "00")
                    InfFile = InfFile & Format(K, "00")
                    Command(Counter) = "resample " & " work\" & InfFile & ".inf"
                    FileOpen(3, My.Application.Info.DirectoryPath & "\tools\work\" & InfFile & ".inf", OpenMode.Output)
                    PrintLine(3, "[Source]")
                    PrintLine(3, "   Type = Raw")
                    PrintLine(3, "   SourceDir = " & Chr(34) & "." & Chr(34))
                    PrintLine(3, "   SourceFile = " & Chr(34) & InfFile & ".raw" & Chr(34))
                    PrintLine(3, "   Layer = WaterClass")
                    PrintLine(3, "   SamplingMethod = Point")
                    PrintLine(3, "   SampleType = UINT8")
                    PrintLine(3, "   NullValue = 254")
                    PrintLine(3, "   ulyMap = " & Str(90.0# - K * D5Lat))
                    PrintLine(3, "   ulxMap = " & Str(J * D5Lon - 180.0#))
                    PrintLine(3, "   nCols = " & CStr(257))
                    PrintLine(3, "   nRows = " & CStr(257))
                    PrintLine(3, "   xDim = " & Str(D13Lon))
                    PrintLine(3, "   yDim = " & Str(D13Lat))
                    PrintLine(3)
                    PrintLine(3, "[Destination]")
                    PrintLine(3, "   DestDir = " & Chr(34) & "." & Chr(34))
                    PrintLine(3, "   DestBaseFileName = " & Chr(34) & InfFile & Chr(34))
                    PrintLine(3, "   DestFileType = BGL")
                    PrintLine(3, "   UseSourceDimensions = 1")
                    FileClose(3)
                End If
            Next
        Next

        ChDrive(My.Application.Info.DirectoryPath)
        ChDir(My.Application.Info.DirectoryPath & "\tools\")

        For J = 1 To NoOfQuads
            ExecCmd(Command(J))
        Next J

        If Not CopyBGLs Then Exit Sub

        On Error GoTo Erro4

        ' copy BGL files
        For J = 1 To NoOfQuads
            BGLFile = My.Application.Info.DirectoryPath & "\tools\work\" & BGL(J)
            BGLFileTarget = BGLProjectFolder & "\" & BGL(J)
            If File.Exists(BGLFile) Then
                File.Copy(BGLFile, BGLFileTarget, True)
                'MsgBox(BGLFile & vbCrLf & BGLFileTarget)
            End If
        Next J

        Exit Sub

Erro4:

        MsgBox("Copying BGL files failed! Try to close FSX.", MsgBoxStyle.Exclamation)

    End Sub

    Friend Sub FillLand(ByVal Map As Integer)

        If BackUpON Then BackUp()

        On Error GoTo erro1

        Dim J, J0, J1, K, K0, K1 As Integer
        Dim C, C0, C1, C00, C11, R, R0, R1, R00, R11 As Integer

        Dim X0 As Double = Maps(Map).WLON
        Dim Y0 As Double = Maps(Map).NLAT
        Dim X1 As Double = Maps(Map).ELON
        Dim Y1 As Double = Maps(Map).SLAT
        Dim Cols As Integer = Maps(Map).COLS
        Dim Rows As Integer = Maps(Map).ROWS

        ' Dim image As Bitmap = Bitmap.FromFile(Maps(Map).BMPSu)     ' was like this in October 2017
        Dim image As Bitmap = CType(Drawing.Image.FromFile(Maps(Map).BMPSu), Bitmap)
        Dim myColor As Color

        Dim Lon, Lat, Lat0, Lon0, DX, DY As Double
        Dim N As Integer

        DX = (X1 - X0) / Cols
        DY = (Y0 - Y1) / Rows

        Dim msg As Single

        Dim PT As JKCR

        Dim LL As Double_XY

        Dim PX, PY As Integer

        ' March, 25, 2014 Lorenzo error
        ' shrink the generation area so that
        ' we do not look after pixels outside the bitmap
        ' PT = JKCRFromLL(X0, Y0, True)
        PT = JKCRFromLL(X0 + D14Lon, Y0 - D14Lat, True)

        J0 = PT.J
        K0 = PT.K
        C0 = PT.C
        R0 = PT.R

        ' PT = JKCRFromLL(X1, Y1, False)
        PT = JKCRFromLL(X1 - D14Lon, Y1 + D14Lat, False)

        J1 = PT.J
        K1 = PT.K
        C1 = PT.C
        R1 = PT.R

        For J = J0 To J1
            C00 = 0
            If J = J0 Then C00 = C0
            C11 = 256
            If J = J1 Then C11 = C1
            For K = K0 To K1
                R00 = 0
                If K = K0 Then R00 = R0
                R11 = 256
                If K = K1 Then R11 = R1

                LL = LLFromJKCR(J, K, 0, 0)
                Lat0 = LL.Y
                Lon0 = LL.X

                For R = R00 To R11
                    Lat = Lat0 - R * D13Lat
                    PY = CInt(Math.Round((Y0 - Lat) / DY))
                    For C = C00 To C11

                        Lon = Lon0 + C * D13Lon
                        PX = CInt(Math.Round((Lon - X0) / DX))
                        myColor = image.GetPixel(PX, PY)

                        For N = 1 To NoOfLWCIs
                            If LWCIs(N).IsLand Then
                                If LWCIs(N).Color = myColor Then

                                    msg = Rnd() * 100
                                    If msg < 58 Then
                                        FormOneLand(J, K, C, R, LWCIs(N).Class1)
                                        Exit For
                                    End If
                                    If msg < 86 Then
                                        FormOneLand(J, K, C, R, LWCIs(N).Class2)
                                        Exit For
                                    End If
                                    FormOneLand(J, K, C, R, LWCIs(N).Class3)
                                    Exit For
                                End If
                            End If
                        Next
                    Next
                Next
            Next
        Next

        Dirty = True

        Exit Sub
erro1:

        MsgBox("There has been a problem! Check the generated indexes!")

    End Sub

    Friend Sub FillWater(ByVal Map As Integer)

        On Error GoTo erro1

        If BackUpON Then BackUp()

        Dim J, J0, J1, K, K0, K1 As Integer
        Dim C, C0, C1, C00, C11, R, R0, R1, R00, R11 As Integer

        Dim X0 As Double = Maps(Map).WLON
        Dim Y0 As Double = Maps(Map).NLAT
        Dim X1 As Double = Maps(Map).ELON
        Dim Y1 As Double = Maps(Map).SLAT
        Dim Cols As Integer = Maps(Map).COLS
        Dim Rows As Integer = Maps(Map).ROWS

        ' Dim image As Bitmap = Bitmap.FromFile(Maps(Map).BMPSu)    ' was like this in October 2017
        Dim image As Bitmap = CType(Drawing.Image.FromFile(Maps(Map).BMPSu), Bitmap)
        Dim myColor As Color

        Dim Lon, Lat, Lat0, Lon0, DX, DY As Double
        Dim N As Integer

        DX = (X1 - X0) / Cols
        DY = (Y0 - Y1) / Rows

        Dim msg As Single

        Dim PT As JKCR

        Dim LL As Double_XY

        Dim PX, PY As Integer

        ' PT = JKCRFromLL(X0, Y0, True)
        PT = JKCRFromLL(X0 + D14Lon, Y0 - D14Lat, True)
        J0 = PT.J
        K0 = PT.K
        C0 = PT.C
        R0 = PT.R

        ' PT = JKCRFromLL(X1, Y1, False)
        PT = JKCRFromLL(X1 - D14Lon, Y1 + D14Lat, False)
        J1 = PT.J
        K1 = PT.K
        C1 = PT.C
        R1 = PT.R

        For J = J0 To J1
            C00 = 0
            If J = J0 Then C00 = C0
            C11 = 256
            If J = J1 Then C11 = C1
            For K = K0 To K1
                R00 = 0
                If K = K0 Then R00 = R0
                R11 = 256
                If K = K1 Then R11 = R1

                LL = LLFromJKCR(J, K, 0, 0)
                Lat0 = LL.Y
                Lon0 = LL.X

                For R = R00 To R11
                    Lat = Lat0 - R * D13Lat
                    PY = CInt(Math.Round((Y0 - Lat) / DY))
                    For C = C00 To C11

                        Lon = Lon0 + C * D13Lon
                        PX = CInt(Math.Round((Lon - X0) / DX))
                        myColor = image.GetPixel(PX, PY)

                        For N = 1 To NoOfLWCIs
                            If Not LWCIs(N).IsLand Then
                                If LWCIs(N).Color = myColor Then

                                    msg = Rnd() * 100
                                    If msg < 58 Then
                                        FormOneWater(J, K, C, R, LWCIs(N).Class1)
                                        Exit For
                                    End If
                                    If msg < 86 Then
                                        FormOneWater(J, K, C, R, LWCIs(N).Class2)
                                        Exit For
                                    End If
                                    FormOneWater(J, K, C, R, LWCIs(N).Class3)
                                    Exit For
                                End If
                            End If
                        Next
                    Next
                Next
            Next
        Next

        Exit Sub
erro1:

        MsgBox("There has been a problem! Check the generated indexes!")

    End Sub


End Module
