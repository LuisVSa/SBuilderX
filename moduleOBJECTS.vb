Option Strict Off
Option Explicit On

Imports System.xml
Imports System.text
Imports VB = Microsoft.VisualBasic

Module moduleOBJECTS

    <Serializable()> Friend Structure Objecto
        Dim Type As Integer ' 0=FSX 1=FS8 2=FS9 3=Rwy12 4=API 5=ASD 8=TaxiwaySign   128=FSX MDL 129=FS9 MDL
        Dim Description As String ' code the type
        Dim Selected As Boolean
        Dim Width As Single
        Dim Length As Single
        Dim Heading As Single
        Dim Pitch As Single
        Dim Bank As Single
        Dim BiasX As Single
        Dim BiasY As Single
        Dim BiasZ As Single
        Dim lat As Double
        Dim lon As Double
        Dim Altitude As Double
        Dim AGL As Integer
        Dim Complexity As Integer
        ' the following are not exported
        Dim NLAT As Double
        Dim SLAT As Double
        Dim WLON As Double
        Dim ELON As Double
        Dim HDX As Single
        Dim HDY As Single
        Dim P1Y As Single
        Dim P1X As Single
        Dim P2Y As Single
        Dim P2X As Single
        Dim P3Y As Single
        Dim P3X As Single
        Dim P4Y As Single
        Dim P4X As Single
    End Structure

    Friend Objects() As Objecto
    Friend NoOfObjects As Integer
    Friend NoOfObjectsSelected As Integer

    Friend Structure LibObject
        Implements IComparable
        Dim ID As String
        Dim Name As String
        Dim Scaling As Single
        Dim Width As Single
        Dim Length As Single
        Dim Type As Integer ' 0=FS8 1=FS9 2=FSX
        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Dim tmp As LibObject = DirectCast(obj, LibObject)
            Return Name.CompareTo(obj.Name)
        End Function
    End Structure

    Structure GenBObject
        Dim type As Integer
        Dim sizeX As Single
        Dim sizeZ As Single
        Dim scale As Single
        Dim textures As String
        Dim indexes As String
        Dim name As String
    End Structure

    Friend GenBObjects() As GenBObject
    Friend NoOfGenBObjects As Integer


    Structure LibCategory
        Dim Name As String
        Dim Objs As ArrayList
    End Structure

    Friend LibCategories() As LibCategory
    Friend NoOfLibCategories As Integer

    Friend LibObjectsPath As String
    Friend LibObjectsIsOn As Boolean

    'Friend IncFiles() As String 'include files in objects.txt
    'Friend NoIncFiles As Integer

    ' Rwy12 objects
    ' *************************************************

    Friend Structure Rwy12Object
        Dim ID As String
        Dim Name As String
        Dim Texture As String
    End Structure

    Friend Structure Rwy12Category
        Dim Name As String
        Dim NOB As Integer
        Dim Rwy12Objects() As Rwy12Object
    End Structure

    Friend Rwy12Categories() As Rwy12Category
    Friend NoOfRwy12Categories As Integer

    Friend Rwy12Path As String
    Friend Rwy12IsOn As Boolean


    Friend ObjComment As String
    Friend ObjOrder As Boolean

    'start default = to last one used
    'Friend IniObjType As Integer


    Friend ObjMDLFile As String
    Friend ObjMDLGuid As String
    Friend ObjMDLName As String
    Friend ObjMDLScale As Single

    Friend ObjTaxSize As Integer
    Friend ObjTaxJust As String
    Friend ObjTaxLabel As String

    Friend ObjWinLight As Integer
    Friend ObjWinLength As Single
    Friend ObjWinHeight As Single
    Friend ObjWindPoleColor As Integer
    Friend ObjWindSockColor As Integer

    Friend ObjEffName As String
    Friend ObjEffParameters As String

    Friend ObjBeaCivil As Integer
    Friend ObjBeaMil As Integer
    Friend ObjBeaAirport As Integer
    Friend ObjBeaSeaBase As Integer
    Friend ObjBeaHeliport As Integer

    Friend ObjLibID As String
    Friend ObjLibScale As Single
    Friend ObjLibType As Integer   ' 0=FS8 1=FS9 2=FSX ' not an error!!!!
    Friend ObjLibV1 As Single
    Friend ObjLibV2 As Single

    Friend ObjectTURN As Boolean
    Friend ObjectSIZE As Boolean
    Friend ObjectON As Boolean
    Friend ObjectVIEW As Boolean
    Friend ObjectDispON As Boolean

    Friend ObjMYes As Boolean  ' used by Measure Tool ????
    Friend ObjMHead As Double   ' to give objects a prefixed heading

    Private ObjectID As Integer
    Friend LatObj As Double
    Friend LonObj As Double

    Friend Sub ObjectInsertMode(ByRef Button As Short, ByRef Shift As Short, ByVal X As Integer, ByVal Y As Integer)

        If Button = 1 Then

            frmStart.CopyMenuItem.Enabled = False
            frmStart.DeleteMenuItem.Enabled = False

            If Shift = 1 Then ' pick the SHIFT DOWN
                SomeSelected = SomeSelected Or IsObjectSelected(LonDispWest * PixelsPerLonDeg + X, LatDispNorth * PixelsPerLatDeg - Y)
                RebuildDisplay()
                If SomeSelected Then
                    SetDelay(200)
                    MoveON = True
                    FirstMOVE = True
                    frmStart.CopyMenuItem.Enabled = True
                    frmStart.DeleteMenuItem.Enabled = True
                    AuxXInt = X
                    AuxYInt = Y
                End If
                Exit Sub
            End If

            If IsObjectTurn(X, Y) Then
                'RebuildDisplay()
                SetDelay(200)
                ObjectTURN = True
                frmStart.SetMouseIcon()
                Exit Sub
            End If

            If IsObjectSize(X, Y) Then
                'RebuildDisplay()
                SetDelay(200)
                ObjectSIZE = True
                frmStart.SetMouseIcon()
                Exit Sub
            End If

            SelectAllObjects(False)
            SomeSelected = IsObjectSelected(LonDispWest * PixelsPerLonDeg + X, LatDispNorth * PixelsPerLatDeg - Y)
            RebuildDisplay()
            If SomeSelected Then
                SetDelay(200)
                frmStart.CopyMenuItem.Enabled = True
                frmStart.DeleteMenuItem.Enabled = True
                MoveON = True
                FirstMOVE = True
                AuxXInt = X
                AuxYInt = Y
                Exit Sub
            Else
                FormObject(X, Y)
            End If
        End If

        If Button = 2 Then
            XPOP = X
            YPOP = Y
            ProcessPopUp(X, Y)
        End If

    End Sub

    Private Sub FormObject(ByVal X As Integer, ByVal Y As Integer)

        LonObj = LonDispWest + X / PixelsPerLonDeg
        LatObj = LatDispNorth - Y / PixelsPerLatDeg

        POPIndex = NoOfObjects + 1
        frmObjectsP.ShowObjectProperties(POPIndex)
        frmObjectsP.ShowDialog()

    End Sub

    Friend Sub SelectAllObjects(ByRef Flag As Boolean)

        Dim N As Integer

        If Not ObjectVIEW Then Exit Sub

        If Flag Then

            frmStart.SelectAllObjectsMenuItem.Checked = True
        Else
            frmStart.SelectAllObjectsMenuItem.Checked = False
        End If

        For N = 1 To NoOfObjects
            If Flag Then
                If Not Objects(N).Selected Then NoOfObjectsSelected = NoOfObjectsSelected + 1
                SomeSelected = True
            Else
                If Objects(N).Selected Then NoOfObjectsSelected = NoOfObjectsSelected - 1
            End If
            Objects(N).Selected = Flag
        Next N

    End Sub

    Friend Sub DisplayObjects(ByVal g As Graphics)

        Dim a As String

        Dim X, Y As Single
        Dim P0, P4, P2, P1, P3, P5 As PointF

        Dim N As Integer
        Dim Flag As Boolean
        Dim type As Integer

        Dim myPen As New System.Drawing.Pen(Color.Black)
        Dim myBrush As New System.Drawing.SolidBrush(Color.Green)
        Dim myImage As Image

        For N = 1 To NoOfObjects
            If Objects(N).NLAT < LatDispSouth Then GoTo JumpHere
            If Objects(N).SLAT > LatDispNorth Then GoTo JumpHere
            If Objects(N).WLON > LonDispEast Then GoTo JumpHere
            If Objects(N).ELON < LonDispWest Then GoTo JumpHere

            type = Objects(N).Type

            If Objects(N).Selected Then
                myBrush.Color = Color.SpringGreen
                myPen.Color = Color.Green
            Else
                If type < 3 Then
                    myBrush.Color = Color.SkyBlue
                    myPen.Color = Color.Black
                ElseIf type > 255 Then
                    myBrush.Color = Color.Chocolate
                    myPen.Color = Color.Black
                Else
                    myBrush.Color = Color.Yellow
                    myPen.Color = Color.Black
                End If
            End If

            X = (Objects(N).lon - LonDispWest) * PixelsPerLonDeg
            Y = (LatDispNorth - Objects(N).lat) * PixelsPerLatDeg

            Flag = (Objects(N).Width + Objects(N).Length) * PixelsPerMeter < 20
            If Flag Then
                g.FillRectangle(myBrush, X - 3, Y - 3, 6, 6)
                g.DrawRectangle(myPen, X - 3, Y - 3, 6, 6)
                GoTo JumpHere
            End If

            If type = 8 Then ' taxi sign
                a = My.Application.Info.DirectoryPath & "\tools\bmps\taxisign.bmp"
                myImage = System.Drawing.Image.FromFile(a)
                g.DrawImage(myImage, X - 40, Y - 20, 80, 40)
            End If

            If type = 32 Then ' beacon
                a = My.Application.Info.DirectoryPath & "\tools\bmps\beacon.gif"
                myImage = System.Drawing.Image.FromFile(a)
                g.DrawImage(myImage, X - 20, Y - 20, 40, 40)
            End If

            If type = 64 Then ' windsock
                a = My.Application.Info.DirectoryPath & "\tools\bmps\windsock.gif"
                myImage = System.Drawing.Image.FromFile(a)
                g.DrawImage(myImage, X - 20, Y - 20, 40, 40)
            End If

            If type = 16 Then ' effect
                a = My.Application.Info.DirectoryPath & "\tools\bmps\effect.gif"
                myImage = System.Drawing.Image.FromFile(a)
                g.DrawImage(myImage, X - 20, Y - 20, 40, 40)
            End If


            g.FillRectangle(myBrush, X - 3, Y - 3, 6, 6)
            g.DrawRectangle(myPen, X - 3, Y - 3, 6, 6)
            myPen.DashStyle = Drawing2D.DashStyle.Dash

            P0.X = X
            P0.Y = Y
            P1.X = Objects(N).P1X * PixelsPerMeter + X
            P2.X = Objects(N).P2X * PixelsPerMeter + X
            P3.X = Objects(N).P3X * PixelsPerMeter + X
            P4.X = Objects(N).P4X * PixelsPerMeter + X
            P5.X = Objects(N).HDX * PixelsPerMeter + X

            P1.Y = -Objects(N).P1Y * PixelsPerMeter + Y
            P2.Y = -Objects(N).P2Y * PixelsPerMeter + Y
            P3.Y = -Objects(N).P3Y * PixelsPerMeter + Y
            P4.Y = -Objects(N).P4Y * PixelsPerMeter + Y
            P5.Y = -Objects(N).HDY * PixelsPerMeter + Y

            g.DrawLine(myPen, P1, P2)
            g.DrawLine(myPen, P2, P3)
            g.DrawLine(myPen, P3, P4)
            g.DrawLine(myPen, P4, P1)
            g.DrawLine(myPen, P0, P5)

            If ObjectON Then
                myPen.DashStyle = Drawing2D.DashStyle.Solid
                g.DrawRectangle(myPen, P1.X - 2, P1.Y - 2, 4, 4)
                g.DrawRectangle(myPen, P2.X - 2, P2.Y - 2, 4, 4)
                g.DrawRectangle(myPen, P3.X - 2, P3.Y - 3, 4, 4)
                g.DrawRectangle(myPen, P4.X - 2, P4.Y - 2, 4, 4)
                g.DrawRectangle(myPen, P5.X - 2, P5.Y - 2, 4, 4)
            End If

JumpHere:
        Next N

        myBrush.Dispose()
        myPen.Dispose()

    End Sub

    Friend Sub AddLatLonToObjects(ByVal N As Integer)

        Dim W2, L2 As Single
        Dim P As PointF
        Dim C, teta, S As Single

        If Objects(N).Type < 8 Or Objects(N).Type > 127 Then
            W2 = Objects(N).Width / 2.0!
            L2 = Objects(N).Length / 2.0!
        ElseIf Objects(N).Type = 8 Then
            L2 = 3
            W2 = 1.5
        Else
            W2 = 3
            L2 = 3
        End If

        teta = Objects(N).Heading * PI / 180
        C = System.Math.Cos(teta)
        S = System.Math.Sin(teta)

        P.X = -W2 : P.Y = L2
        RotateXY(P, C, S)
        Objects(N).P1X = P.X
        Objects(N).P1Y = P.Y

        P.X = W2 : P.Y = L2
        RotateXY(P, C, S)
        Objects(N).P2X = P.X
        Objects(N).P2Y = P.Y

        P.X = W2 : P.Y = -L2
        RotateXY(P, C, S)
        Objects(N).P3X = P.X
        Objects(N).P3Y = P.Y

        P.X = -W2 : P.Y = -L2
        RotateXY(P, C, S)
        Objects(N).P4X = P.X
        Objects(N).P4Y = P.Y

        P.X = 0 : P.Y = L2
        RotateXY(P, C, S)
        Objects(N).HDX = P.X
        Objects(N).HDY = P.Y

        AddObjectLimits(N)


    End Sub

    Private Sub AddObjectLimits(ByVal N As Integer)

        Dim EL, NL, SL, WL As Double
        Dim X As Double
        Dim Y As Double

        NL = -90
        SL = 90
        EL = -180 : WL = 180

        X = Objects(N).lon + Objects(N).P1X * PixelsPerMeter / PixelsPerLonDeg
        Y = Objects(N).lat + Objects(N).P1Y * PixelsPerMeter / PixelsPerLatDeg
        If NL < Y Then NL = Y
        If SL > Y Then SL = Y
        If EL < X Then EL = X
        If WL > X Then WL = X

        X = Objects(N).lon + Objects(N).P2X * PixelsPerMeter / PixelsPerLonDeg
        Y = Objects(N).lat + Objects(N).P2Y * PixelsPerMeter / PixelsPerLatDeg
        If NL < Y Then NL = Y
        If SL > Y Then SL = Y
        If EL < X Then EL = X
        If WL > X Then WL = X

        X = Objects(N).lon + Objects(N).P3X * PixelsPerMeter / PixelsPerLonDeg
        Y = Objects(N).lat + Objects(N).P3Y * PixelsPerMeter / PixelsPerLatDeg
        If NL < Y Then NL = Y
        If SL > Y Then SL = Y
        If EL < X Then EL = X
        If WL > X Then WL = X

        X = Objects(N).lon + Objects(N).P4X * PixelsPerMeter / PixelsPerLonDeg
        Y = Objects(N).lat + Objects(N).P4Y * PixelsPerMeter / PixelsPerLatDeg
        If NL < Y Then NL = Y
        If SL > Y Then SL = Y
        If EL < X Then EL = X
        If WL > X Then WL = X
        Objects(N).ELON = EL
        Objects(N).WLON = WL
        Objects(N).NLAT = NL
        Objects(N).SLAT = SL


    End Sub

    Friend Sub AppendOBJFile(ByVal filename As String)

        frmStart.Cursor = System.Windows.Forms.Cursors.WaitCursor
        BackUp()

        Dim NoOfObjectsIni As Integer = NoOfObjects

        On Error GoTo erro1

        Dim A As String
        Dim FN As String
        Dim R, G, B As Integer
        Dim HasBias As Boolean = False
        Dim AppendNewObject As Boolean = False
        Dim alt As Double

        FN = Path.GetFileNameWithoutExtension(filename)

        Dim K As Integer = 1
        Dim N As Integer = NoOfObjects + 1

        NoOfObjects = NoOfObjects + 10
        ReDim Preserve Objects(NoOfObjects)

        Dim reader As XmlReader = XmlReader.Create(filename)
        reader.MoveToContent()

        ' Parse the file and display each of the nodes.
        While reader.Read()
            If reader.NodeType = XmlNodeType.Element Then

                AppendNewObject = False

                If reader.Name = "SceneryObject" Then
                    Objects(N).lat = BGLStr2Lat(reader.GetAttribute("lat"))
                    Objects(N).lon = BGLStr2Lon(reader.GetAttribute("lon"))
                    Objects(N).Altitude = BGLStr2Alt(reader.GetAttribute("alt"))
                    Objects(N).AGL = BGLStr2AGL(reader.GetAttribute("altitudeIsAgl"))
                    Objects(N).Pitch = CSng(reader.GetAttribute("pitch"))
                    Objects(N).Bank = CSng(reader.GetAttribute("bank"))
                    Objects(N).Heading = CSng(reader.GetAttribute("heading"))
                    Objects(N).Complexity = BGLStr2Comp(reader.GetAttribute("imageComplexity"))

                    Objects(N).Width = 10
                    Objects(N).Length = 10

                    Do
                        reader.Read()
                    Loop Until reader.NodeType = XmlNodeType.Element

                    If reader.Name = "BiasXYZ" Then
                        Objects(N).BiasX = CSng(reader.GetAttribute("biasX"))
                        Objects(N).BiasY = CSng(reader.GetAttribute("biasY"))
                        Objects(N).BiasZ = CSng(reader.GetAttribute("biasZ"))
                        HasBias = True
                    Else
                        Objects(N).BiasX = 0
                        Objects(N).BiasY = 0
                        Objects(N).BiasZ = 0
                        HasBias = False
                    End If

                    If HasBias Then
                        Do
                            reader.Read()
                        Loop Until reader.NodeType = XmlNodeType.Element
                    End If

                    If reader.Name = "Beacon" Then
                        AppendNewObject = True
                        Objects(N).Type = 32
                        ObjComment = "Beacon_from_" & FN & "_#" & K
                        ObjBeaCivil = 0
                        ObjBeaMil = 0
                        A = reader.GetAttribute("type").Substring(0, 1)
                        If A = "C" Then ObjBeaCivil = 1
                        If A = "M" Then ObjBeaMil = 1
                        ObjBeaAirport = 0
                        ObjBeaSeaBase = 0
                        ObjBeaHeliport = 0
                        A = reader.GetAttribute("baseType").Substring(0, 1)
                        If A = "A" Then ObjBeaAirport = 1
                        If A = "S" Then ObjBeaSeaBase = 1
                        If A = "H" Then ObjBeaHeliport = 1
                        Objects(N).Description = MakeDescription(N)
                    End If

                    If reader.Name = "Windsock" Then
                        AppendNewObject = True
                        Objects(N).Type = 64
                        ObjComment = "Windsock_from_" & FN & "_#" & K
                        ObjWinHeight = Val(reader.GetAttribute("poleHeight"))
                        ObjWinLength = Val(reader.GetAttribute("sockLength"))
                        ObjWinLight = 0
                        If reader.GetAttribute("lighted").Substring(0, 1) = "T" Then ObjWinLight = 1
                        ObjWindPoleColor = CInt(System.Drawing.ColorTranslator.ToOle(Color.Gray))
                        ObjWindSockColor = CInt(System.Drawing.ColorTranslator.ToOle(Color.Orange))
                        Do
                            reader.Read()
                            If reader.NodeType = XmlNodeType.EndElement Then Exit Do
                            If reader.Name = "PoleColor" Then
                                R = CInt(reader.GetAttribute("red"))
                                B = CInt(reader.GetAttribute("blue"))
                                G = CInt(reader.GetAttribute("green"))
                                ObjWindPoleColor = CInt(System.Drawing.ColorTranslator.ToOle(Color.FromArgb(255, R, G, B)))
                            End If
                            If reader.Name = "SockColor" Then
                                R = CInt(reader.GetAttribute("red"))
                                B = CInt(reader.GetAttribute("blue"))
                                G = CInt(reader.GetAttribute("green"))
                                ObjWindSockColor = CInt(System.Drawing.ColorTranslator.ToOle(Color.FromArgb(255, R, G, B)))
                            End If
                        Loop
                        Objects(N).Description = MakeDescription(N)
                    End If

                    If reader.Name = "Effect" Then
                        AppendNewObject = True
                        Objects(N).Type = 16
                        ObjComment = "Effect_from_" & FN & "_#" & K
                        ObjEffName = reader.GetAttribute("effectName")
                        ObjEffParameters = reader.GetAttribute("effectParams")
                        Objects(N).Description = MakeDescription(N)
                    End If

                    If reader.Name = "LibraryObject" Then
                        AppendNewObject = True
                        Objects(N).Type = 0
                        ObjComment = "LibObject_from_" & FN & "_#" & K
                        ObjLibID = reader.GetAttribute("name")
                        ObjLibScale = Val(reader.GetAttribute("scale"))
                        Objects(N).Description = MakeDescription(N)
                    End If

                    If reader.Name = "GenericBuilding" Then
                        AppendNewObject = True
                        ObjComment = "GenBuilding_from_" & FN & "_#" & K
                        scale_gb = Val(reader.GetAttribute("scale"))
                        bottomTexture = CInt(reader.GetAttribute("bottomTexture"))
                        roofTexture = CInt(reader.GetAttribute("roofTexture"))
                        topTexture = CInt(reader.GetAttribute("topTexture"))
                        windowTexture = CInt(reader.GetAttribute("windowTexture"))


                        Do
                            reader.Read()
                        Loop Until reader.NodeType = XmlNodeType.Element

                        If reader.Name = "RectangularBuilding" Then
                            If reader.GetAttribute("roofType") = "FLAT" Then
                                Objects(N).Type = 256
                            End If
                            If reader.GetAttribute("roofType") = "PEAKED" Then
                                Objects(N).Type = 257
                            End If
                            If reader.GetAttribute("roofType") = "RIDGE" Then
                                Objects(N).Type = 258
                            End If
                            If reader.GetAttribute("roofType") = "SLANT" Then
                                Objects(N).Type = 259
                            End If
                        End If
                        If reader.Name = "PyramidalBuilding" Then
                            Objects(N).Type = 260
                        End If
                        If reader.Name = "MultiSidedBuilding" Then
                            Objects(N).Type = 261
                        End If


                        If Objects(N).Type = 256 Or Objects(N).Type = 257 _
                                         Or Objects(N).Type = 258 Or Objects(N).Type = 259 Then ' Rect flat

                            sizeX = Val(reader.GetAttribute("sizeX"))
                            sizeZ = Val(reader.GetAttribute("sizeZ"))
                            sizeBottomY = Val(reader.GetAttribute("sizeBottomY"))
                            textureIndexBottomX = CInt(reader.GetAttribute("textureIndexBottomX"))
                            textureIndexBottomZ = CInt(reader.GetAttribute("textureIndexBottomZ"))
                            sizeWindowY = Val(reader.GetAttribute("sizeWindowY"))
                            textureIndexWindowX = CInt(reader.GetAttribute("textureIndexWindowX"))
                            textureIndexWindowY = CInt(reader.GetAttribute("textureIndexWindowY"))
                            textureIndexWindowZ = CInt(reader.GetAttribute("textureIndexWindowZ"))
                            sizeTopY = Val(reader.GetAttribute("sizeTopY"))
                            textureIndexTopX = CInt(reader.GetAttribute("textureIndexTopX"))
                            textureIndexTopZ = CInt(reader.GetAttribute("textureIndexTopZ"))
                            textureIndexRoofX = CInt(reader.GetAttribute("textureIndexRoofX"))
                            textureIndexRoofZ = CInt(reader.GetAttribute("textureIndexRoofZ"))
                        End If

                        If Objects(N).Type = 257 Then  ' Rect peaked
                            sizeRoofY = Val(reader.GetAttribute("sizeRoofY"))
                            textureIndexRoofY = CInt(reader.GetAttribute("textureIndexRoofY"))
                        End If

                        If Objects(N).Type = 258 Then  ' Rect Ridge
                            sizeRoofY = Val(reader.GetAttribute("sizeRoofY"))
                            gableTexture = CInt(reader.GetAttribute("gableTexture"))
                            textureIndexGableY = CInt(reader.GetAttribute("textureIndexGableY"))
                            textureIndexGableZ = CInt(reader.GetAttribute("textureIndexGableZ"))
                        End If

                        If Objects(N).Type = 259 Then  ' Rect slant
                            sizeRoofY = Val(reader.GetAttribute("sizeRoofY"))
                            gableTexture = CInt(reader.GetAttribute("gableTexture"))
                            textureIndexGableY = CInt(reader.GetAttribute("textureIndexGableY"))
                            textureIndexGableZ = CInt(reader.GetAttribute("textureIndexGableZ"))
                            faceTexture = CInt(reader.GetAttribute("faceTexture"))
                            textureIndexFaceX = CInt(reader.GetAttribute("textureIndexFaceX"))
                            textureIndexFaceY = CInt(reader.GetAttribute("textureIndexFaceY"))
                        End If

                        If Objects(N).Type = 260 Then  ' pyramidal
                            sizeX = Val(reader.GetAttribute("sizeX"))
                            sizeZ = Val(reader.GetAttribute("sizeZ"))
                            sizeTopX = Val(reader.GetAttribute("sizeTopX"))
                            sizeTopZ = Val(reader.GetAttribute("sizeTopZ"))
                            sizeBottomY = Val(reader.GetAttribute("sizeBottomY"))
                            textureIndexBottomX = CInt(reader.GetAttribute("textureIndexBottomX"))
                            textureIndexBottomZ = CInt(reader.GetAttribute("textureIndexBottomZ"))
                            sizeWindowY = Val(reader.GetAttribute("sizeWindowY"))
                            textureIndexWindowX = CInt(reader.GetAttribute("textureIndexWindowX"))
                            textureIndexWindowY = CInt(reader.GetAttribute("textureIndexWindowY"))
                            textureIndexWindowZ = CInt(reader.GetAttribute("textureIndexWindowZ"))
                            sizeTopY = Val(reader.GetAttribute("sizeTopY"))
                            textureIndexTopX = CInt(reader.GetAttribute("textureIndexTopX"))
                            textureIndexTopZ = CInt(reader.GetAttribute("textureIndexTopZ"))
                            textureIndexRoofX = CInt(reader.GetAttribute("textureIndexRoofX"))
                            textureIndexRoofZ = CInt(reader.GetAttribute("textureIndexRoofZ"))
                        End If

                        If Objects(N).Type = 261 Then ' multi sided
                            buildingSides = CInt(reader.GetAttribute("buildingSides"))
                            smoothing = CBool(reader.GetAttribute("smoothing"))
                            sizeX = Val(reader.GetAttribute("sizeX"))
                            sizeZ = Val(reader.GetAttribute("sizeZ"))
                            sizeBottomY = Val(reader.GetAttribute("sizeBottomY"))
                            textureIndexBottomX = CInt(reader.GetAttribute("textureIndexBottomX"))
                            sizeWindowY = Val(reader.GetAttribute("sizeWindowY"))
                            textureIndexWindowX = CInt(reader.GetAttribute("textureIndexWindowX"))
                            textureIndexWindowY = CInt(reader.GetAttribute("textureIndexWindowY"))
                            sizeTopY = Val(reader.GetAttribute("sizeTopY"))
                            textureIndexTopX = CInt(reader.GetAttribute("textureIndexTopX"))
                            sizeRoofY = Val(reader.GetAttribute("sizeRoofY"))
                            textureIndexRoofX = CInt(reader.GetAttribute("textureIndexRoofX"))
                            textureIndexRoofY = CInt(reader.GetAttribute("textureIndexRoofY"))
                            textureIndexRoofZ = CInt(reader.GetAttribute("textureIndexRoofZ"))
                        End If
                        Objects(N).Width = sizeX
                        Objects(N).Length = sizeZ
                        Objects(N).Description = MakeDescription(N)

                    End If

                End If


                If reader.Name = "Airport" Then
                    ' taxiwaysigns
                    alt = BGLStr2Alt(reader.GetAttribute("alt"))
                End If

                If reader.Name = "TaxiwaySign" Then
                    AppendNewObject = True
                    Objects(N).Type = 8
                    ObjComment = "TaxiwaySign_from_" & FN & "_#" & K
                    Objects(N).lat = BGLStr2Lat(reader.GetAttribute("lat"))
                    Objects(N).lon = BGLStr2Lon(reader.GetAttribute("lon"))
                    Objects(N).Altitude = alt
                    Objects(N).AGL = 0
                    Objects(N).Pitch = 0
                    Objects(N).Bank = 0
                    Objects(N).Heading = Val(reader.GetAttribute("heading"))
                    Objects(N).Complexity = 0
                    Objects(N).Width = 10
                    Objects(N).Length = 10
                    ObjTaxLabel = reader.GetAttribute("label")
                    ObjTaxSize = CInt(CStr(reader.GetAttribute("size")).Substring(4, 1))
                    ObjTaxJust = reader.GetAttribute("justification")
                    Objects(N).Description = MakeDescription(N)
                End If

                If AppendNewObject = True Then
                    AddLatLonToObjects(N)
                    If N = NoOfObjects Then
                        NoOfObjects = NoOfObjects + 10
                        ReDim Preserve Objects(NoOfObjects)
                    End If
                    N = N + 1
                    K = K + 1
                End If

            End If

        End While
        reader.Close()

        NoOfObjects = NoOfObjectsIni + K - 1
        ReDim Preserve Objects(NoOfObjects)
        frmStart.Cursor = System.Windows.Forms.Cursors.Default

        Exit Sub

erro1:
        ReDim Preserve Objects(NoOfObjectsIni)
        MsgBox("SBuilderX could not import objects", MsgBoxStyle.Critical)
        frmStart.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Private Function BGLStr2Lat(ByVal S As String) As Double

        Dim A As String
        Dim N As Integer
        BGLStr2Lat = Val(S)
        If BGLStr2Lat = 0 Then
            A = S.Substring(0, 1)
            If A = "N" Then
                N = InStr(S, " ")
                'Debug.Print(S.Substring(1, N - 2))
                'Debug.Print(S.Substring(N))
                BGLStr2Lat = Val(S.Substring(1, N - 2))
                BGLStr2Lat = BGLStr2Lat + Val(S.Substring(N)) / 60
            ElseIf A = "S" Then
                N = InStr(S, " ")
                BGLStr2Lat = Val(S.Substring(1, N - 2))
                BGLStr2Lat = -BGLStr2Lat - Val(S.Substring(N)) / 60
            End If
        End If

    End Function

    Private Function BGLStr2Lon(ByVal S As String) As Double

        Dim A As String
        Dim N As Integer
        BGLStr2Lon = Val(S)
        If BGLStr2Lon = 0 Then
            A = S.Substring(0, 1)
            If A = "E" Then
                N = InStr(S, " ")
                'Debug.Print(S.Substring(1, N - 2))
                'Debug.Print(S.Substring(N))
                BGLStr2Lon = Val(S.Substring(1, N - 2))
                BGLStr2Lon = BGLStr2Lon + Val(S.Substring(N)) / 60
            ElseIf A = "W" Then
                N = InStr(S, " ")
                BGLStr2Lon = Val(S.Substring(1, N - 2))
                BGLStr2Lon = -BGLStr2Lon - Val(S.Substring(N)) / 60
            End If
        End If

    End Function

    Private Function BGLStr2Alt(ByVal S As String) As Double

        Dim N As Integer = S.Length
        S = S.Substring(0, N - 1)
        BGLStr2Alt = Val(S)

    End Function
    Private Function BGLStr2Comp(ByVal S As String) As Integer

        Dim N As String = S.Length

        If N = 6 Then
            'SPARSE or 'NORMAL
            Dim A As String = S.Substring(0, 1)
            If A = "N" Then
                BGLStr2Comp = 2
            Else
                BGLStr2Comp = 1
            End If
            Exit Function
        End If

        If N = 11 Then
            'VERY_SPARSE
            BGLStr2Comp = 0
            Exit Function
        End If

        If N = 5 Then
            'DENSE
            BGLStr2Comp = 3
            Exit Function
        End If

        If N = 15 Then
            'EXTREMELY_DENSE
            BGLStr2Comp = 5
            Exit Function
        End If

        BGLStr2Comp = 4 ' it is very_dense

    End Function

    Private Function BGLStr2AGL(ByVal S As String) As Integer

        Dim A As String = S.Substring(0, 1)
        If A = "T" Then
            BGLStr2AGL = 1
        Else
            BGLStr2AGL = 0
        End If

    End Function

    Private Sub RotateXY(ByRef P As PointF, ByVal Cos As Single, ByVal Sin As Single)

        Dim a As Single

        a = P.X
        P.X = Cos * P.X + Sin * P.Y
        P.Y = Cos * P.Y - Sin * a

    End Sub
    Friend Function IsObjectTurn(ByVal X As Integer, ByVal Y As Integer) As Boolean
        ' on entry X Y contain distance from NW corner display in pixels

        Dim N As Integer
        Dim PC, P As PointF

        IsObjectTurn = False

        For N = 1 To NoOfObjects

            If (Objects(N).Width + Objects(N).Length) * PixelsPerMeter < 20 Then GoTo next_N

            PC.X = CSng((Objects(N).lon - LonDispWest) * PixelsPerLonDeg)
            PC.Y = CSng((LatDispNorth - Objects(N).lat) * PixelsPerLatDeg)

            P.X = PC.X + Objects(N).HDX * PixelsPerMeter
            P.Y = PC.Y - Objects(N).HDY * PixelsPerMeter

            If P.X > X + 5 Then GoTo next_N
            If P.X < X - 5 Then GoTo next_N
            If P.Y < Y - 5 Then GoTo next_N
            If P.Y > Y + 5 Then GoTo next_N

            ObjectID = N
            Objects(N).Selected = True
            IsObjectTurn = True
            Exit Function

next_N:
        Next N

    End Function

    Friend Function IsObjectSize(ByVal X As Integer, ByVal Y As Integer) As Boolean
        ' on entry X Y contain distance from NW corner display in pixels

        Dim N As Integer
        Dim PC, P As Double_XY

        IsObjectSize = False

        For N = 1 To NoOfObjects

            If (Objects(N).Width + Objects(N).Length) * PixelsPerMeter < 20 Then GoTo next_N


            PC.X = CSng((Objects(N).lon - LonDispWest) * PixelsPerLonDeg)
            PC.Y = CSng((LatDispNorth - Objects(N).lat) * PixelsPerLatDeg)

            P.X = PC.X + Objects(N).P1X * PixelsPerMeter
            P.Y = PC.Y - Objects(N).P1Y * PixelsPerMeter
            If P.X > X + 5 Then GoTo next_2
            If P.X < X - 5 Then GoTo next_2
            If P.Y < Y - 5 Then GoTo next_2
            If P.Y > Y + 5 Then GoTo next_2
            ObjectID = N
            Objects(N).Selected = True
            IsObjectSize = True
            Exit Function

next_2:

            P.X = PC.X + Objects(N).P2X * PixelsPerMeter
            P.Y = PC.Y - Objects(N).P2Y * PixelsPerMeter
            If P.X > X + 5 Then GoTo next_3
            If P.X < X - 5 Then GoTo next_3
            If P.Y < Y - 5 Then GoTo next_3
            If P.Y > Y + 5 Then GoTo next_3
            ObjectID = N
            Objects(N).Selected = True
            IsObjectSize = True
            Exit Function

next_3:

            P.X = PC.X + Objects(N).P3X * PixelsPerMeter
            P.Y = PC.Y - Objects(N).P3Y * PixelsPerMeter
            If P.X > X + 5 Then GoTo next_4
            If P.X < X - 5 Then GoTo next_4
            If P.Y < Y - 5 Then GoTo next_4
            If P.Y > Y + 5 Then GoTo next_4
            ObjectID = N
            Objects(N).Selected = True
            IsObjectSize = True
            Exit Function

next_4:

            P.X = PC.X + Objects(N).P4X * PixelsPerMeter
            P.Y = PC.Y - Objects(N).P4Y * PixelsPerMeter
            If P.X > X + 5 Then GoTo next_N
            If P.X < X - 5 Then GoTo next_N
            If P.Y < Y - 5 Then GoTo next_N
            If P.Y > Y + 5 Then GoTo next_N
            ObjectID = N
            Objects(N).Selected = True
            IsObjectSize = True
            Exit Function

next_N:
        Next N


    End Function


    Friend Sub MoveSelectedObjects(ByVal X As Double, ByVal Y As Double)

        Dim N As Integer

        For N = 1 To NoOfObjects
            If Objects(N).Selected = False Then GoTo next_N
            Objects(N).lat = Objects(N).lat - Y
            Objects(N).lon = Objects(N).lon + X
            AddObjectLimits(N)

next_N:
        Next N

    End Sub
    Friend Sub DeleteThisObject(ByVal N As Integer)

        Dim K As Integer

        Dirty = True

        If Not SkipBackUp Then BackUp()

        If N < NoOfObjects Then
            For K = N To NoOfObjects - 1
                Objects(K) = Objects(K + 1)
            Next K
        End If

        If NoOfObjects > 1 Then
            ReDim Preserve Objects(NoOfObjects - 1)
        End If

        NoOfObjects = NoOfObjects - 1

    End Sub

    Friend Function IsObjectSelected(ByVal X As Double, ByVal Y As Double) As Boolean

        Dim N As Integer
        Dim PC As Double_XY
        Dim XS, YS As Double
        Dim retval As Boolean
        Dim WLON, NLAT, SLAT, ELON As Double

        'X and Y enter with pixels from the Earth center

        IsObjectSelected = False
        If Not ObjectVIEW Then Exit Function

        WLON = (X + 5) / PixelsPerLonDeg
        ELON = (X - 5) / PixelsPerLonDeg
        NLAT = (Y - 5) / PixelsPerLatDeg
        SLAT = (Y + 5) / PixelsPerLatDeg

        For N = 1 To NoOfObjects

            If WLON < Objects(N).WLON Then GoTo next_N
            If ELON > Objects(N).ELON Then GoTo next_N
            If SLAT < Objects(N).SLAT Then GoTo next_N
            If NLAT > Objects(N).NLAT Then GoTo next_N

            PC.X = Objects(N).lon * PixelsPerLonDeg
            PC.Y = Objects(N).lat * PixelsPerLatDeg

            If PC.X > X + 5 Then GoTo check_sides
            If PC.X < X - 5 Then GoTo check_sides
            If PC.Y < Y - 5 Then GoTo check_sides
            If PC.Y > Y + 5 Then GoTo check_sides

            If Objects(N).Selected = False Then NoOfObjectsSelected = NoOfObjectsSelected + 1
            Objects(N).Selected = True
            IsObjectSelected = True

            Exit Function

check_sides:

            XS = X - PC.X
            YS = Y - PC.Y

            retval = IsPointInObject(Objects(N).P1X, Objects(N).P1Y, Objects(N).P2X, Objects(N).P2Y, XS, YS)
            If Not retval Then
                retval = IsPointInObject(Objects(N).P2X, Objects(N).P2Y, Objects(N).P3X, Objects(N).P3Y, XS, YS)
            End If
            If Not retval Then
                retval = IsPointInObject(Objects(N).P3X, Objects(N).P3Y, Objects(N).P4X, Objects(N).P4Y, XS, YS)
            End If
            If Not retval Then
                retval = IsPointInObject(Objects(N).P4X, Objects(N).P4Y, Objects(N).P1X, Objects(N).P1Y, XS, YS)
            End If

            If retval Then
                If Objects(N).Selected = False Then NoOfObjectsSelected = NoOfObjectsSelected + 1
                Objects(N).Selected = True
                IsObjectSelected = True
                Exit Function
            End If

next_N:
        Next N

    End Function

    Private Function IsPointInObject(ByVal X0 As Double, ByVal Y0 As Double, ByVal X1 As Double, ByVal Y1 As Double, ByVal x As Double, ByVal y As Double) As Boolean

        ' on entry X Y contain distance in pixels from object center
        ' other parameters are distances in meters of line extremes in relation to object center

        IsPointInObject = False

        X0 = X0 * PixelsPerMeter
        X1 = X1 * PixelsPerMeter

        Y0 = Y0 * PixelsPerMeter
        Y1 = Y1 * PixelsPerMeter

        If x > X0 + 5 And x > X1 + 5 Then Exit Function
        If x < X0 - 5 And x < X1 - 5 Then Exit Function
        If y > Y0 + 5 And y > Y1 + 5 Then Exit Function
        If y < Y0 - 5 And y < Y1 - 5 Then Exit Function

        X1 = X1 - X0
        Y1 = Y1 - Y0

        If System.Math.Abs(X1) > System.Math.Abs(Y1) Then
            x = Y0 + Y1 * (x - X0) / X1
            If x > y + 5 Then Exit Function
            If x < y - 5 Then Exit Function
        Else
            y = X0 + X1 * (y - Y0) / Y1
            If y > x + 5 Then Exit Function
            If y < x - 5 Then Exit Function
        End If

        IsPointInObject = True

    End Function


    Friend Sub SelectObjectsInBox(ByVal X0 As Double, ByVal Y0 As Double, ByVal X1 As Double, ByVal Y1 As Double)

        Dim N As Integer

        If Not ObjectVIEW Then Exit Sub

        For N = 1 To NoOfObjects
            If Objects(N).ELON < X1 Then
                If Objects(N).WLON > X0 Then
                    If Objects(N).SLAT > Y1 Then
                        If Objects(N).NLAT < Y0 Then
                            If Not Objects(N).Selected Then NoOfObjectsSelected = NoOfObjectsSelected + 1
                            SomeSelected = True
                            Objects(N).Selected = True
                        End If
                    End If
                End If
            End If
        Next N

    End Sub

    Friend Sub TurnObject(ByVal X As Integer, ByVal Y As Integer)

        Dim Y0, X0, Head As Single

        X0 = (Objects(ObjectID).lon - LonDispWest) * PixelsPerLonDeg
        Y0 = (LatDispNorth - Objects(ObjectID).lat) * PixelsPerLatDeg

        X0 = X - X0
        Y0 = Y0 - Y

        Head = 0
        If Y0 > 0 Then
            Head = System.Math.Atan(X0 / Y0) * 180 / PI
        ElseIf Y0 < 0 Then
            Head = 180 + System.Math.Atan(X0 / Y0) * 180 / PI
        Else
            If X0 > 0 Then
                Head = 90
            Else
                Head = 270
            End If
        End If

        If Head < 0 Then Head = Head + 360

        Objects(ObjectID).Heading = Head
        AddLatLonToObjects(ObjectID)
        RebuildDisplay()

    End Sub

    Friend Sub SizeObject(ByVal X As Integer, ByVal Y As Integer)

        Dim Y0, X0, teta As Single
        Dim YD, XD, Head As Single

        X0 = (Objects(ObjectID).lon - LonDispWest) * PixelsPerLonDeg
        Y0 = (LatDispNorth - Objects(ObjectID).lat) * PixelsPerLatDeg

        XD = X - X0
        YD = Y - Y0

        If XD > 0 Then
            teta = 90 + System.Math.Atan(YD / XD) * 180 / PI
        ElseIf XD < 0 Then
            teta = 270 + System.Math.Atan(YD / XD) * 180 / PI
        Else
            If YD > 0 Then
                teta = 180
            Else
                teta = 0
            End If
        End If

        Head = Objects(ObjectID).Heading
        If Head < 0 Then Head = Head + 360

        teta = teta - Head
        teta = teta * PI / 180
        XD = XD * XD
        YD = YD * YD
        XD = XD + YD
        XD = System.Math.Sqrt(XD)
        YD = System.Math.Cos(teta)
        YD = System.Math.Abs(YD)
        YD = YD * XD
        XD = XD * XD - YD * YD
        XD = System.Math.Sqrt(XD)
        Objects(ObjectID).Width = 2 * XD / PixelsPerMeter
        Objects(ObjectID).Length = 2 * YD / PixelsPerMeter

        AddLatLonToObjects(ObjectID)
        RebuildDisplay()

    End Sub

    Friend Sub SetGenBObjects()

        Dim K, N1, N2 As Integer
        ReDim GenBObjects(1000)
        Dim stream As FileStream
        Dim fileReader As System.IO.StreamReader
        Dim line As String = ""
        Dim myFile As String = AppPath & "\GenBuildings\GenBuildings.txt"

        If Not File.Exists(myFile) Then
            System.IO.File.Create(myFile)
        End If

        K = 1

        Try
            stream = New FileStream(myFile, FileMode.Open)
            fileReader = New System.IO.StreamReader(stream)

            Do Until fileReader.EndOfStream
                line = fileReader.ReadLine()
                If line.Length > 0 Then
                    If line.Substring(0, 1) = "2" Then

                        GenBObjects(K).type = CInt(line.Substring(0, 3))

                        N1 = 4
                        N2 = InStr(N1 + 1, line, " ")
                        GenBObjects(K).sizeX = Val(line.Substring(N1, N2 - N1 - 1))

                        N1 = N2
                        N2 = InStr(N1 + 1, line, " ")
                        GenBObjects(K).sizeZ = Val(line.Substring(N1, N2 - N1 - 1))

                        N1 = N2
                        N2 = InStr(N1 + 1, line, " ")
                        GenBObjects(K).scale = Val(line.Substring(N1, N2 - N1 - 1))

                        N1 = N2
                        N2 = InStr(N1 + 1, line, " ")
                        GenBObjects(K).textures = line.Substring(N1, N2 - N1 - 1)

                        N1 = N2
                        N2 = InStr(N1 + 1, line, " ")
                        GenBObjects(K).indexes = line.Substring(N1, N2 - N1 - 1)

                        GenBObjects(K).name = line.Substring(N2)

                        K = K + 1

                    End If
                End If
            Loop

            ' should I reaaly comment the following? October 2017
            'fileReader.Close()
            stream.Close()

            NoOfGenBObjects = K - 1
            ReDim Preserve GenBObjects(NoOfGenBObjects)

        Catch ex As Exception

            NoOfGenBObjects = 0
            ReDim GenBObjects(0)
            MsgBox("There was an error reading GenBuildings.txt", MsgBoxStyle.Exclamation)

        End Try

    End Sub

    Public NoOfJpegs As Integer
    Friend Sub SetLibObjects()

        Try
            Dim a, b, File As String

            CheckLibObjects()
            If LibObjectsIsOn = False Then Exit Sub

            Dim Marker, N As Integer

            ReDim LibCategories(500)
            Dim IncFiles(500) As String

            NoOfLibCategories = 0
            Dim NoIncFiles As Integer = 0

            File = LibObjectsPath & "\objects.txt"

            FileOpen(2, File, OpenMode.Input)
            N = LOF(2)
            Marker = 0
            Do While Marker < N
                a = LineInput(2)
                Marker = Marker + Len(a) + 2
                a = Trim(a)
                If a = "" Then GoTo next_1
                b = Mid(a, 1, 8)
                If b = "include=" Then
                    NoIncFiles = NoIncFiles + 1
                    IncFiles(NoIncFiles) = Mid(a, 9)
                End If
next_1:
            Loop
            FileClose()

            Dim counter As _
            System.Collections.ObjectModel.ReadOnlyCollection(Of String)
            counter = My.Computer.FileSystem.GetFiles(LibObjectsPath & "\NewJpegs")
            NoOfJpegs = counter.Count

            For N = 1 To NoIncFiles
                SetLibObjectFile(IncFiles(N))
            Next N

            If NoOfLibCategories > 0 Then
                ReDim Preserve LibCategories(NoOfLibCategories)
            End If

            ' copy New to Old jpegs
            a = LibObjectsPath & "\NewJpegs"
            b = LibObjectsPath & "\BackUps\"

            For Each foundFile As String In My.Computer.FileSystem.GetFiles( _
                a, FileIO.SearchOption.SearchAllSubDirectories, "*.*")
                Dim foundFileInfo As New System.IO.FileInfo(foundFile)
                My.Computer.FileSystem.MoveFile(foundFile, b & foundFileInfo.Name, True)
            Next

        Catch ex As Exception
            Dim s As String = "There was an error related to <objects.txt>. Library Objects were turned off."
            MsgBox(s, MsgBoxStyle.Exclamation)
            LibObjectsIsOn = False
        End Try

    End Sub

    Private Sub SetLibObjectFile(ByVal ThisFile As String)

        Dim a, b, c, File As String
        Dim Marker, N As Integer
        Dim J As Integer
        Dim M1, M2 As Integer

        Dim LibCatFolder As String = ""
        Dim myLibObj As New LibObject

        On Error GoTo erro1

        Dim NoC As Integer = NoOfLibCategories

        File = LibObjectsPath & "\" & ThisFile
        FileOpen(2, File, OpenMode.Input)
        N = LOF(2)
        Marker = 0

        J = NoOfLibCategories  ' for the categories
        Do While Marker < N
            a = LineInput(2)
            Marker = Marker + Len(a) + 2
            a = Trim(a)
            If a = "" Then GoTo next_1
            b = Mid(a, 1, 1)
            If b = "[" Then
                M2 = Len(a) - 2
                J = J + 1

                LibCategories(J).Name = Mid(a, 2, M2)
                LibCategories(J).Objs = New ArrayList

                LibCatFolder = Path.GetFileNameWithoutExtension(LibCategories(J).Name)
                LibCatFolder = LibObjectsPath & "\" & LibCatFolder

            ElseIf b <> ";" And b <> "i" Then
                M1 = 1
                M2 = InStr(M1, a, " ")
                myLibObj.ID = CStr(Mid(a, M1, M2 - M1))
                M1 = M2 + 1
                M2 = InStr(M1, a, " ")
                myLibObj.Type = CInt(Mid(a, M1, M2 - M1))
                M1 = M2 + 1
                M2 = InStr(M1, a, " ")
                myLibObj.Width = CSng(Mid(a, M1, M2 - M1))
                M1 = M2 + 1
                M2 = InStr(M1, a, " ")
                myLibObj.Length = CSng(Mid(a, M1, M2 - M1))
                M1 = M2 + 1
                M2 = InStr(M1, a, " ")
                myLibObj.Scaling = CSng(Mid(a, M1, M2 - M1))
                M1 = M2 + 1
                myLibObj.Name = CStr(Mid(a, M1))

                LibCategories(J).Objs.Add(myLibObj)

                If NoOfJpegs > 0 Then
                    b = LibCatFolder & "\" & myLibObj.ID & ".jpg"
                    If Not My.Computer.FileSystem.FileExists(b) Then
                        c = myLibObj.ID & "*.jpg"
                        Dim myfiles As System.Collections.ObjectModel.ReadOnlyCollection(Of String) _
                                 = My.Computer.FileSystem.GetFiles(LibObjectsPath & "\NewJpegs", _
                                 FileIO.SearchOption.SearchAllSubDirectories, c)
                        For Each myfile As String In myfiles
                            My.Computer.FileSystem.MoveFile(myfile, b, True)
                            NoOfJpegs = NoOfJpegs - 1
                        Next
                    End If
                End If

            End If
next_1:
        Loop

        FileClose()
        NoOfLibCategories = J

        Exit Sub

erro1:
        FileClose()
        NoOfLibCategories = NoC
        a = "Could not find/process the file:" & vbCrLf & ThisFile
        MsgBox(a, MsgBoxStyle.Exclamation)

    End Sub
   
    Friend Sub SetRwy12Objects()

        Dim a, File As String

        CheckRwy12()

        If Rwy12IsOn = False Then Exit Sub

        NoOfRwy12Categories = 0
        ReDim Rwy12Categories(1)

        File = Rwy12Path & "\add_*.xml"
        a = Dir(File)

        Do
            If a = "" Then Exit Do
            AddRwy12File(a)
            a = Dir()
        Loop

    End Sub
    Private Sub AddRwy12File(ByVal File As String)

        Dim a, b As String
        Dim M, N, Marker, K, L As Integer

        FileOpen(2, Rwy12Path & "\" & File, OpenMode.Input)

        N = LOF(2)
        Marker = 0

        Do While Marker < N
            a = LineInput(2)
            Marker = Marker + Len(a) + 2

            a = Replace(a, Chr(9), "")
            a = Trim(a)
            M = Len(a)

            b = Mid(a, 1, 15)
            If b = "<category name=" Then
                NoOfRwy12Categories = NoOfRwy12Categories + 1
                ReDim Preserve Rwy12Categories(NoOfRwy12Categories)
                L = InStr(17, a, Chr(34))
                Rwy12Categories(NoOfRwy12Categories).Name = Mid(a, 17, L - 17)
                K = 1
            End If

            b = Mid(a, 1, 10)
            If b = "<obj name=" Then
                Rwy12Categories(NoOfRwy12Categories).NOB = K
                ReDim Preserve Rwy12Categories(NoOfRwy12Categories).Rwy12Objects(K)
                L = InStr(12, a, Chr(34))
                Rwy12Categories(NoOfRwy12Categories).Rwy12Objects(K).Name = Mid(a, 12, L - 12)
                K = K + 1
            End If

            b = Mid(a, 1, 5)
            If b = "guid=" Then
                Rwy12Categories(NoOfRwy12Categories).Rwy12Objects(K - 1).ID = Mid(a, 7, 32)
            End If

            b = Mid(a, 1, 6)
            If b = "image=" Then
                L = InStr(8, a, Chr(34))
                Rwy12Categories(NoOfRwy12Categories).Rwy12Objects(K - 1).Texture = Mid(a, 8, L - 8)
            End If

        Loop

        FileClose()

    End Sub


    Friend Sub AnalyseLibObject(ByVal N As Integer)

        Dim a As String = ""
        Dim M1, M2 As Integer

        'If N = 0 Then a = RGNPointType1
        If N > 0 Then a = Objects(N).Description

        M1 = 1
        M2 = InStr(M1, a, "|")
        ObjLibID = Mid(a, M1, M2 - M1)

        If ObjLibType = 0 Then
            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            ObjLibScale = Val(Mid(a, M1, M2 - M1))
            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            ObjLibV1 = Val(Mid(a, M1, M2 - M1))
            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            If M2 = 0 Then
                ObjLibV2 = Val(Mid(a, M1))
            Else
                ObjLibV2 = Val(Mid(a, M1, M2 - M1))
                ObjComment = Mid(a, M2 + 1)
            End If
        Else
            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            If M2 = 0 Then
                ObjLibScale = Val(Mid(a, M1))
            Else
                ObjLibScale = Val(Mid(a, M1, M2 - M1))
                ObjComment = Mid(a, M2 + 1)
            End If
        End If


    End Sub

    Friend Sub AnalyseTaxiwayObject(ByVal N As Integer)

        Dim a As String = ""
        Dim M1, M2 As Integer

        'If N = 0 Then a = RGNPointType1
        If N > 0 Then a = Objects(N).Description

        M1 = 1
        M2 = InStr(M1, a, "|")
        ObjTaxSize = CInt(Mid(a, M1, M2 - M1))

        M1 = M2 + 1
        M2 = InStr(M1, a, "|")
        ObjTaxJust = Mid(a, M1, M2 - M1)

        M1 = M2 + 1
        M2 = InStrRev(a, "|")

        If M2 = 0 Then
            ObjTaxLabel = Mid(a, M1)
        Else
            ObjTaxLabel = Mid(a, M1, M2 - M1)
            ObjComment = Mid(a, M2 + 1)
        End If

    End Sub


    Friend Sub AnalyseEffectObject(ByVal N As Integer)

        Dim a As String = ""
        Dim M1, M2 As Integer

        'If N = 0 Then a = RGNPointType1
        If N > 0 Then a = Objects(N).Description

        M1 = 1
        M2 = InStr(M1, a, "|")
        ObjEffName = Mid(a, M1, M2 - M1)

        M1 = M2 + 1
        M2 = InStr(M1, a, "|")

        If M2 = 0 Then
            ObjEffParameters = Mid(a, M1)
        Else
            ObjEffParameters = Mid(a, M1, M2 - M1)
            ObjComment = Mid(a, M2 + 1)
        End If

    End Sub
    Friend Sub AnalyseBeaconObject(ByVal N As Integer)

        Dim a As String = ""
        Dim M1, M2 As Integer

        'If N = 0 Then a = RGNPointType1
        If N > 0 Then a = Objects(N).Description

        M1 = 1
        M2 = InStr(M1, a, "|")
        ObjBeaCivil = CInt(Mid(a, M1, M2 - M1))

        M1 = M2 + 1
        M2 = InStr(M1, a, "|")
        ObjBeaMil = CInt(Mid(a, M1, M2 - M1))

        M1 = M2 + 1
        M2 = InStr(M1, a, "|")
        ObjBeaAirport = CInt(Mid(a, M1, M2 - M1))

        M1 = M2 + 1
        M2 = InStr(M1, a, "|")
        ObjBeaSeaBase = CInt(Mid(a, M1, M2 - M1))

        M1 = M2 + 1
        M2 = InStr(M1, a, "|")

        If M2 = 0 Then
            ObjBeaHeliport = CInt(Mid(a, M1))
        Else
            ObjBeaHeliport = CInt(Mid(a, M1, M2 - M1))
            ObjComment = Mid(a, M2 + 1)
        End If

    End Sub

    Friend Sub AnalyseWindsockObject(ByVal N As Integer)

        Dim a As String = ""
        Dim M1, M2 As Integer

        'If N = 0 Then a = RGNPointType1
        If N > 0 Then a = Objects(N).Description

        M1 = 1
        M2 = InStr(M1, a, "|")
        ObjWinLight = CInt(Mid(a, M1, M2 - M1))

        M1 = M2 + 1
        M2 = InStr(M1, a, "|")
        ObjWinLength = Val(Mid(a, M1, M2 - M1))

        M1 = M2 + 1
        M2 = InStr(M1, a, "|")
        ObjWinHeight = Val(Mid(a, M1, M2 - M1))

        M1 = M2 + 1
        M2 = InStr(M1, a, "|")
        ObjWindPoleColor = CInt(Mid(a, M1, M2 - M1))

        M1 = M2 + 1
        M2 = InStr(M1, a, "|")

        If M2 = 0 Then
            ObjWindSockColor = CInt(Mid(a, M1))
        Else
            ObjWindSockColor = CInt(Mid(a, M1, M2 - M1))
            ObjComment = Mid(a, M2 + 1)
        End If


    End Sub

    Friend Sub AnalyseMDLObject(ByVal N As Integer)

        Dim a As String = ""
        Dim M1, M2 As Integer

        'If N = 0 Then a = RGNPointType1
        If N > 0 Then a = Objects(N).Description

        M1 = 1
        M2 = InStr(M1, a, "|")
        ObjMDLFile = Mid(a, M1, M2 - M1)

        M1 = M2 + 1
        M2 = InStr(M1, a, "|")
        ObjMDLName = Mid(a, M1, M2 - M1)

        M1 = M2 + 1
        M2 = InStr(M1, a, "|")
        ObjMDLScale = Val(Mid(a, M1, M2 - M1))

        M1 = M2 + 1
        M2 = InStr(M1, a, "|")

        If M2 = 0 Then
            ObjMDLGuid = Mid(a, M1)
        Else
            ObjMDLGuid = Mid(a, M1, M2 - M1)
            ObjComment = Mid(a, M2 + 1)
        End If

    End Sub


    Friend Function MakeDescription(ByVal N As Integer) As String

        Dim a As String = ""

        If Objects(N).Type = 1 Then
            a = ObjLibID & "|"
            a = a & Trim(Str(ObjLibScale)) & "|"
            a = a & Trim(Str(ObjLibV1)) & "|"
            a = a & Trim(Str(ObjLibV2)) & "|" & ObjComment
        End If

        ' new in FSX
        If Objects(N).Type = 0 Or Objects(N).Type = 2 Or Objects(N).Type = 3 Then
            a = ObjLibID & "|"
            a = a & Trim(Str(ObjLibScale)) & "|" & ObjComment
        End If

        If Objects(N).Type = 4 Then
            a = MacroID & "|Range="
            a = a & CStr(MacroRange) & ",Scale="
            a = a & Trim(Str(MacroScale)) & ",P6="
            a = a & MacroP6Value & ",P7="
            a = a & MacroP7Value & ",P8="
            a = a & MacroP8Value & ",P9="
            a = a & MacroP9Value & ",V1="
            a = a & Trim(Str(MacroVisibility)) & ",V2="
            a = a & Trim(Str(MacroV2Value)) & ",|"
            a = a & ObjComment
        End If

        If Objects(N).Type = 5 Then
            a = MacroID & "|"
            a = a & "Range=" & CStr(MacroRange)
            a = a & ",Scale=" & Trim(Str(MacroScale))
            a = a & ",V1=" & Trim(Str(MacroVisibility)) & ","

            If MacroP6Name <> "" Then a = a & MacroP6Name & "=" & MacroP6Value & ","
            If MacroP7Name <> "" Then a = a & MacroP7Name & "=" & MacroP7Value & ","
            If MacroP8Name <> "" Then a = a & MacroP8Name & "=" & MacroP8Value & ","
            If MacroP9Name <> "" Then a = a & MacroP9Name & "=" & MacroP9Value & ","
            If MacroPAName <> "" Then a = a & MacroPAName & "=" & MacroPAValue & ","
            If MacroPBName <> "" Then a = a & MacroPBName & "=" & MacroPBValue & ","
            If MacroPCName <> "" Then a = a & MacroPCName & "=" & MacroPCValue & ","
            If MacroPDName <> "" Then a = a & MacroPDName & "=" & MacroPDValue & ","

            a = a & "|" & ObjComment

        End If

        If Objects(N).Type = 8 Then
            a = CStr(ObjTaxSize) & "|"
            a = a & ObjTaxJust & "|"
            a = a & ObjTaxLabel & "|" & ObjComment
        End If

        If Objects(N).Type = 16 Then
            a = ObjEffName & "|"
            a = a & ObjEffParameters & "|" & ObjComment
        End If

        If Objects(N).Type = 32 Then
            a = CStr(ObjBeaCivil) & "|"
            a = a & CStr(ObjBeaMil) & "|"
            a = a & CStr(ObjBeaAirport) & "|"
            a = a & CStr(ObjBeaSeaBase) & "|"
            a = a & CStr(ObjBeaHeliport) & "|" & ObjComment
        End If

        If Objects(N).Type = 64 Then
            a = CStr(ObjWinLight) & "|"
            a = a & Trim(Str(ObjWinLength)) & "|"
            a = a & Trim(Str(ObjWinHeight)) & "|"
            a = a & CStr(ObjWindPoleColor) & "|"
            a = a & CStr(ObjWindSockColor) & "|" & ObjComment
        End If

        If Objects(N).Type = 128 Or Objects(N).Type = 129 Then
            a = ObjMDLFile & "|"
            a = a & ObjMDLName & "|"
            a = a & Str(ObjMDLScale) & "|"
            a = a & ObjMDLGuid & "|" & ObjComment
        End If

        If Objects(N).Type > 255 Then
            a = Trim(Str(scale_gb)) & "|" & MakeGBTextures() _
                                      & "|" & MakeGBIndexes(Objects(N).Type) & "|" & ObjComment
        End If

        MakeDescription = a

    End Function


    Friend Sub MakeBGLObjects(ByVal CopyBGLs As Boolean)

        Dim H_NLat As Double ' header borders
        Dim H_SLat As Double ' header borders
        Dim H_WLon As Double ' header borders
        Dim H_ELon As Double ' header borders

        Dim N, TaxCount As Integer
        Dim lat, lon, alt As Double
        Dim TaxLat, TaxLon, TaxAlt As Double
        Dim Latitude, Longitude As String
        Dim Altitude, AGL As String
        Dim Pitch, Heading, Bank As String
        Dim BiasY, BiasX, BiasZ As Double
        Dim Complex As String
        Dim V1, V2 As String
        Dim a, b As String

        Dim FS8, FS9, FSX, FS9xml As Boolean

        Dim BGLFile0 As String = ""
        Dim BGLFile1 As String = ""
        Dim BGLFile2 As String = ""
        Dim BGLFile3 As String = ""

        Dim lbNext As String
        Dim lbRet As String
        Dim lbPcall As String
        Dim lbStart As String

        Dim File3 As String = ""
        Dim File2 As String = ""
        Dim File1 As String = ""
        Dim File0 As String = ""

        H_NLat = -90
        H_SLat = 90
        H_WLon = 180
        H_ELon = -180

        TaxCount = 0
        TaxLat = 0
        TaxLon = 0
        TaxAlt = 0
        FS8 = False
        FS9 = False
        FS9Xml = False
        FSX = False

        For N = 1 To NoOfObjects
            If Objects(N).Selected Then
                If Objects(N).Type = 1 Or Objects(N).Type = 4 Or Objects(N).Type = 5 Then
                    lat = Objects(N).lat
                    lon = Objects(N).lon
                    If lon > H_ELon Then H_ELon = lon
                    If lon < H_WLon Then H_WLon = lon
                    If lat > H_NLat Then H_NLat = lat
                    If lat < H_SLat Then H_SLat = lat
                    FS8 = True
                ElseIf Objects(N).Type = 0 Or Objects(N).Type = 16 Or Objects(N).Type = 32 Or Objects(N).Type = 64 _
                    Or Objects(N).Type = 128 Or Objects(N).Type > 255 Then
                    FSX = True
                ElseIf Objects(N).Type = 2 Or Objects(N).Type = 3 Then
                    FS9 = True
                ElseIf Objects(N).Type = 129 Then
                    FS9xml = True
                ElseIf Objects(N).Type = 8 Then
                    lat = Objects(N).lat
                    lon = Objects(N).lon
                    alt = Objects(N).Altitude
                    TaxCount = TaxCount + 1
                    TaxLat = TaxLat + lat
                    TaxLon = TaxLon + lon
                    TaxAlt = TaxAlt + alt
                    FSX = True
                End If
            End If
        Next N

        If TaxCount > 0 Then
            TaxLat = TaxLat / TaxCount
            TaxLon = TaxLon / TaxCount
            TaxAlt = TaxAlt / TaxCount
        End If

        If FSX Then
            File2 = ProjectName & "_OBX"
            File2 = Replace(File2, " ", "_")
            a = My.Application.Info.DirectoryPath & "\tools\work\" & File2 & ".xml"

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

            writer.WriteComment("FSX library objects")
            For N = 1 To NoOfObjects
                If Objects(N).Selected Then
                    If Objects(N).Type = 0 Then
                        ObjLibType = 2
                        AnalyseLibObject(N)
                        ObjComment = ObjComment & "_Obj_" & CStr(N)
                        writer.WriteComment(ObjComment)
                        writer.WriteStartElement("SceneryObject")
                        writer.WriteAttributeString("lat", Trim(Str(Objects(N).lat)))
                        writer.WriteAttributeString("lon", Trim(Str(Objects(N).lon)))
                        writer.WriteAttributeString("alt", Trim(Str(Objects(N).Altitude)))
                        writer.WriteAttributeString("altitudeIsAgl", GetTR(Objects(N).AGL))
                        writer.WriteAttributeString("pitch", Trim(Str(Objects(N).Pitch)))
                        writer.WriteAttributeString("bank", Trim(Str(Objects(N).Bank)))
                        writer.WriteAttributeString("heading", Trim(Str(Objects(N).Heading)))
                        writer.WriteAttributeString("imageComplexity", GetComplex(Objects(N).Complexity))
                        BiasX = Objects(N).BiasX
                        BiasY = Objects(N).BiasY
                        BiasZ = Objects(N).BiasZ
                        If BiasX <> 0 Or BiasY <> 0 Or BiasZ <> 0 Then
                            writer.WriteStartElement("BiasXYZ")
                            writer.WriteAttributeString("biasX", Trim(Str(BiasX)))
                            writer.WriteAttributeString("biasY", Trim(Str(BiasY)))
                            writer.WriteAttributeString("biasZ", Trim(Str(BiasZ)))
                            writer.WriteEndElement()
                        End If
                        writer.WriteStartElement("LibraryObject")
                        writer.WriteAttributeString("name", ObjLibID)
                        writer.WriteAttributeString("scale", Trim(Str(ObjLibScale)))
                        writer.WriteEndElement()
                        writer.WriteFullEndElement()
                    End If
                End If
            Next

            writer.WriteComment("FSX Windsock objects")
            For N = 1 To NoOfObjects
                If Objects(N).Selected Then
                    If Objects(N).Type = 64 Then
                        AnalyseWindsockObject(N)
                        ObjComment = ObjComment & "_Obj_" & CStr(N)
                        writer.WriteComment(ObjComment)
                        writer.WriteStartElement("SceneryObject")
                        writer.WriteAttributeString("lat", Trim(Str(Objects(N).lat)))
                        writer.WriteAttributeString("lon", Trim(Str(Objects(N).lon)))
                        writer.WriteAttributeString("alt", Trim(Str(Objects(N).Altitude)))
                        writer.WriteAttributeString("altitudeIsAgl", GetTR(Objects(N).AGL))
                        writer.WriteAttributeString("pitch", Trim(Str(Objects(N).Pitch)))
                        writer.WriteAttributeString("bank", Trim(Str(Objects(N).Bank)))
                        writer.WriteAttributeString("heading", Trim(Str(Objects(N).Heading)))
                        writer.WriteAttributeString("imageComplexity", GetComplex(Objects(N).Complexity))
                        BiasX = Objects(N).BiasX
                        BiasY = Objects(N).BiasY
                        BiasZ = Objects(N).BiasZ
                        If BiasX <> 0 Or BiasY <> 0 Or BiasZ <> 0 Then
                            writer.WriteStartElement("BiasXYZ")
                            writer.WriteAttributeString("biasX", Trim(Str(BiasX)))
                            writer.WriteAttributeString("biasY", Trim(Str(BiasY)))
                            writer.WriteAttributeString("biasZ", Trim(Str(BiasZ)))
                            writer.WriteEndElement()
                        End If
                        writer.WriteStartElement("Windsock")
                        writer.WriteAttributeString("poleHeight", Trim(Str(ObjWinHeight)))
                        writer.WriteAttributeString("sockLength", Trim(Str(ObjWinLength)))
                        writer.WriteAttributeString("lighted", GetTR(ObjWinLight))
                        writer.WriteStartElement("PoleColor")
                        writer.WriteAttributeString("red", Trim(Str(RedFromInteger(ObjWindPoleColor))))
                        writer.WriteAttributeString("blue", Trim(Str(BlueFromInteger(ObjWindPoleColor))))
                        writer.WriteAttributeString("green", Trim(Str(GreenFromInteger(ObjWindPoleColor))))
                        writer.WriteEndElement()
                        writer.WriteStartElement("SockColor")

                        writer.WriteAttributeString("red", Trim(Str(RedFromInteger(ObjWindSockColor))))
                        writer.WriteAttributeString("blue", Trim(Str(BlueFromInteger(ObjWindSockColor))))
                        writer.WriteAttributeString("green", Trim(Str(GreenFromInteger(ObjWindSockColor))))

                        writer.WriteEndElement()

                        writer.WriteFullEndElement()
                        writer.WriteFullEndElement()
                    End If
                End If
            Next

            writer.WriteComment("FSX Effect objects")
            For N = 1 To NoOfObjects
                If Objects(N).Selected Then
                    If Objects(N).Type = 16 Then
                        AnalyseEffectObject(N)
                        ObjComment = ObjComment & "_Obj_" & CStr(N)
                        writer.WriteComment(ObjComment)
                        writer.WriteStartElement("SceneryObject")
                        writer.WriteAttributeString("lat", Trim(Str(Objects(N).lat)))
                        writer.WriteAttributeString("lon", Trim(Str(Objects(N).lon)))
                        writer.WriteAttributeString("alt", Trim(Str(Objects(N).Altitude)))
                        writer.WriteAttributeString("altitudeIsAgl", GetTR(Objects(N).AGL))
                        writer.WriteAttributeString("pitch", Trim(Str(Objects(N).Pitch)))
                        writer.WriteAttributeString("bank", Trim(Str(Objects(N).Bank)))
                        writer.WriteAttributeString("heading", Trim(Str(Objects(N).Heading)))
                        writer.WriteAttributeString("imageComplexity", GetComplex(Objects(N).Complexity))
                        BiasX = Objects(N).BiasX
                        BiasY = Objects(N).BiasY
                        BiasZ = Objects(N).BiasZ
                        If BiasX <> 0 Or BiasY <> 0 Or BiasZ <> 0 Then
                            writer.WriteStartElement("BiasXYZ")
                            writer.WriteAttributeString("biasX", Trim(Str(BiasX)))
                            writer.WriteAttributeString("biasY", Trim(Str(BiasY)))
                            writer.WriteAttributeString("biasZ", Trim(Str(BiasZ)))
                            writer.WriteEndElement()
                        End If
                        writer.WriteStartElement("Effect")
                        writer.WriteAttributeString("effectName", Trim(ObjEffName))
                        writer.WriteAttributeString("effectParams", Trim(ObjEffParameters))
                        writer.WriteEndElement()
                        writer.WriteFullEndElement()
                    End If
                End If
            Next

            If TaxCount > 0 Then

                writer.WriteComment("FSX TaxiwaySign objects")
                writer.WriteStartElement("Airport")
                writer.WriteAttributeString("lat", Trim(Str(TaxLat)))
                writer.WriteAttributeString("lon", Trim(Str(TaxLon)))
                writer.WriteAttributeString("alt", Trim(Str(TaxAlt)))
                writer.WriteAttributeString("ident", "AAAA")

                For N = 1 To NoOfObjects
                    If Objects(N).Selected Then
                        If Objects(N).Type = 8 Then
                            AnalyseTaxiwayObject(N)
                            ObjComment = ObjComment & "_Obj_" & CStr(N)
                            writer.WriteComment(ObjComment)
                            writer.WriteStartElement("TaxiwaySign")
                            writer.WriteAttributeString("lat", Trim(Str(Objects(N).lat)))
                            writer.WriteAttributeString("lon", Trim(Str(Objects(N).lon)))
                            writer.WriteAttributeString("heading", Trim(Str(Objects(N).Heading)))
                            writer.WriteAttributeString("label", Trim(ObjTaxLabel))
                            writer.WriteAttributeString("size", "SIZE" & Trim(Str(ObjTaxSize)))
                            writer.WriteAttributeString("justification", Trim(ObjTaxJust).ToUpper)
                            writer.WriteEndElement()
                        End If
                    End If
                Next N
                writer.WriteFullEndElement()
            End If

            writer.WriteComment("FSX Model objects")
            For N = 1 To NoOfObjects
                If Objects(N).Selected Then
                    If Objects(N).Type = 128 Then
                        AnalyseMDLObject(N)
                        ObjComment = ObjComment & "_Obj_" & CStr(N)
                        writer.WriteComment(ObjComment)
                        writer.WriteStartElement("SceneryObject")
                        writer.WriteAttributeString("lat", Trim(Str(Objects(N).lat)))
                        writer.WriteAttributeString("lon", Trim(Str(Objects(N).lon)))
                        writer.WriteAttributeString("alt", Trim(Str(Objects(N).Altitude)))
                        writer.WriteAttributeString("altitudeIsAgl", GetTR(Objects(N).AGL))
                        writer.WriteAttributeString("pitch", Trim(Str(Objects(N).Pitch)))
                        writer.WriteAttributeString("bank", Trim(Str(Objects(N).Bank)))
                        writer.WriteAttributeString("heading", Trim(Str(Objects(N).Heading)))
                        writer.WriteAttributeString("imageComplexity", GetComplex(Objects(N).Complexity))
                        writer.WriteStartElement("LibraryObject")
                        writer.WriteAttributeString("name", ObjMDLGuid)
                        writer.WriteAttributeString("scale", Trim(Str(ObjMDLScale)))
                        writer.WriteEndElement()
                        writer.WriteFullEndElement()
                        writer.WriteStartElement("ModelData")
                        writer.WriteAttributeString("sourceFile", "..\..\Mdls\" & ObjMDLFile)
                        writer.WriteEndElement()
                    End If
                End If
            Next

            writer.WriteComment("FSX Beacon objects")
            For N = 1 To NoOfObjects
                If Objects(N).Selected Then
                    If Objects(N).Type = 32 Then
                        AnalyseBeaconObject(N)
                        ObjComment = ObjComment & "_Obj_" & CStr(N)
                        writer.WriteComment(ObjComment)
                        writer.WriteStartElement("SceneryObject")
                        writer.WriteAttributeString("lat", Trim(Str(Objects(N).lat)))
                        writer.WriteAttributeString("lon", Trim(Str(Objects(N).lon)))
                        writer.WriteAttributeString("alt", Trim(Str(Objects(N).Altitude)))
                        writer.WriteAttributeString("altitudeIsAgl", GetTR(Objects(N).AGL))
                        writer.WriteAttributeString("pitch", Trim(Str(Objects(N).Pitch)))
                        writer.WriteAttributeString("bank", Trim(Str(Objects(N).Bank)))
                        writer.WriteAttributeString("heading", Trim(Str(Objects(N).Heading)))
                        writer.WriteAttributeString("imageComplexity", GetComplex(Objects(N).Complexity))
                        writer.WriteStartElement("Beacon")
                        writer.WriteAttributeString("type", GetBeaconType)
                        writer.WriteAttributeString("baseType", GetBeaconBaseType)
                        writer.WriteEndElement()
                        writer.WriteFullEndElement()
                    End If
                End If
            Next

            writer.WriteComment("FSX General Buildings")
            For N = 1 To NoOfObjects
                If Objects(N).Selected Then
                    If Objects(N).Type > 255 Then
                        AnalyseGenBObject(N)
                        ObjComment = ObjComment & "_Obj_" & CStr(N)
                        writer.WriteComment(ObjComment)
                        writer.WriteStartElement("SceneryObject")
                        writer.WriteAttributeString("lat", Trim(Str(Objects(N).lat)))
                        writer.WriteAttributeString("lon", Trim(Str(Objects(N).lon)))
                        writer.WriteAttributeString("alt", Trim(Str(Objects(N).Altitude)))
                        writer.WriteAttributeString("altitudeIsAgl", GetTR(Objects(N).AGL))
                        writer.WriteAttributeString("pitch", Trim(Str(Objects(N).Pitch)))
                        writer.WriteAttributeString("bank", Trim(Str(Objects(N).Bank)))
                        writer.WriteAttributeString("heading", Trim(Str(Objects(N).Heading)))
                        writer.WriteAttributeString("imageComplexity", GetComplex(Objects(N).Complexity))
                        BiasX = Objects(N).BiasX
                        BiasY = Objects(N).BiasY
                        BiasZ = Objects(N).BiasZ
                        If BiasX <> 0 Or BiasY <> 0 Or BiasZ <> 0 Then
                            writer.WriteStartElement("BiasXYZ")
                            writer.WriteAttributeString("biasX", Trim(Str(BiasX)))
                            writer.WriteAttributeString("biasY", Trim(Str(BiasY)))
                            writer.WriteAttributeString("biasZ", Trim(Str(BiasZ)))
                            writer.WriteEndElement()
                        End If
                        writer.WriteStartElement("GenericBuilding")
                        writer.WriteAttributeString("scale", Trim(Str(scale_gb)))
                        writer.WriteAttributeString("bottomTexture", Trim(Str(bottomTexture)))
                        writer.WriteAttributeString("roofTexture", Trim(Str(roofTexture)))
                        writer.WriteAttributeString("topTexture", Trim(Str(topTexture)))
                        writer.WriteAttributeString("windowTexture", Trim(Str(windowTexture)))
                        If Objects(N).Type = 256 Then
                            writer.WriteStartElement("RectangularBuilding")
                            writer.WriteAttributeString("roofType", "FLAT")
                        End If
                        If Objects(N).Type = 257 Then
                            writer.WriteStartElement("RectangularBuilding")
                            writer.WriteAttributeString("roofType", "PEAKED")
                        End If
                        If Objects(N).Type = 258 Then
                            writer.WriteStartElement("RectangularBuilding")
                            writer.WriteAttributeString("roofType", "RIDGE")
                        End If
                        If Objects(N).Type = 259 Then
                            writer.WriteStartElement("RectangularBuilding")
                            writer.WriteAttributeString("roofType", "SLANT")
                        End If
                        If Objects(N).Type = 260 Then
                            writer.WriteStartElement("PyramidalBuilding")
                        End If
                        If Objects(N).Type = 261 Then
                            writer.WriteStartElement("MultiSidedBuilding")
                        End If

                        If Objects(N).Type = 256 Or Objects(N).Type = 257 _
                            Or Objects(N).Type = 258 Or Objects(N).Type = 259 Then ' Rect flat
                            writer.WriteAttributeString("sizeX", Trim(Str(Objects(N).Width)))
                            writer.WriteAttributeString("sizeZ", Trim(Str(Objects(N).Length)))
                            writer.WriteAttributeString("sizeBottomY", Trim(Str(sizeBottomY)))
                            writer.WriteAttributeString("textureIndexBottomX", CStr(textureIndexBottomX))
                            writer.WriteAttributeString("textureIndexBottomZ", CStr(textureIndexBottomZ))
                            writer.WriteAttributeString("sizeWindowY", Trim(Str(sizeWindowY)))
                            writer.WriteAttributeString("textureIndexWindowX", CStr(textureIndexWindowX))
                            writer.WriteAttributeString("textureIndexWindowY", CStr(textureIndexWindowY))
                            writer.WriteAttributeString("textureIndexWindowZ", CStr(textureIndexWindowZ))
                            writer.WriteAttributeString("sizeTopY", Trim(Str(sizeTopY)))
                            writer.WriteAttributeString("textureIndexTopX", CStr(textureIndexTopX))
                            writer.WriteAttributeString("textureIndexTopZ", CStr(textureIndexTopZ))
                            writer.WriteAttributeString("textureIndexRoofX", CStr(textureIndexRoofX))
                            writer.WriteAttributeString("textureIndexRoofZ", CStr(textureIndexRoofZ))
                        End If


                        If Objects(N).Type = 257 Then  ' Rect peaked
                            writer.WriteAttributeString("sizeRoofY", Trim(Str(sizeRoofY)))
                            writer.WriteAttributeString("textureIndexRoofY", CStr(textureIndexRoofY))
                        End If

                        If Objects(N).Type = 258 Then  ' Rect Ridge
                            writer.WriteAttributeString("sizeRoofY", Trim(Str(sizeRoofY)))
                            writer.WriteAttributeString("gableTexture", CStr(gableTexture))
                            writer.WriteAttributeString("textureIndexGableY", CStr(textureIndexGableY))
                            writer.WriteAttributeString("textureIndexGableZ", CStr(textureIndexGableZ))
                        End If

                        If Objects(N).Type = 259 Then  ' Rect slant
                            writer.WriteAttributeString("sizeRoofY", Trim(Str(sizeRoofY)))
                            writer.WriteAttributeString("gableTexture", CStr(gableTexture))
                            writer.WriteAttributeString("textureIndexGableY", CStr(textureIndexGableY))
                            writer.WriteAttributeString("textureIndexGableZ", CStr(textureIndexGableZ))
                            writer.WriteAttributeString("faceTexture", CStr(faceTexture))
                            writer.WriteAttributeString("textureIndexFaceX", CStr(textureIndexFaceX))
                            writer.WriteAttributeString("textureIndexFaceY", CStr(textureIndexFaceY))
                        End If

                        If Objects(N).Type = 260 Then  ' pyramidal
                            writer.WriteAttributeString("sizeX", Trim(Str(Objects(N).Width)))
                            writer.WriteAttributeString("sizeZ", Trim(Str(Objects(N).Length)))
                            writer.WriteAttributeString("sizeTopX", Trim(Str(sizeTopX)))
                            writer.WriteAttributeString("sizeTopZ", Trim(Str(sizeTopZ)))
                            writer.WriteAttributeString("sizeBottomY", Trim(Str(sizeBottomY)))
                            writer.WriteAttributeString("textureIndexBottomX", CStr(textureIndexBottomX))
                            writer.WriteAttributeString("textureIndexBottomZ", CStr(textureIndexBottomZ))
                            writer.WriteAttributeString("sizeWindowY", Trim(Str(sizeWindowY)))
                            writer.WriteAttributeString("textureIndexWindowX", CStr(textureIndexWindowX))
                            writer.WriteAttributeString("textureIndexWindowY", CStr(textureIndexWindowY))
                            writer.WriteAttributeString("textureIndexWindowZ", CStr(textureIndexWindowZ))
                            writer.WriteAttributeString("sizeTopY", Trim(Str(sizeTopY)))
                            writer.WriteAttributeString("textureIndexTopX", CStr(textureIndexTopX))
                            writer.WriteAttributeString("textureIndexTopZ", CStr(textureIndexTopZ))
                            writer.WriteAttributeString("textureIndexRoofX", CStr(textureIndexRoofX))
                            writer.WriteAttributeString("textureIndexRoofZ", CStr(textureIndexRoofZ))
                        End If


                        If Objects(N).Type = 261 Then ' multi sided
                            writer.WriteAttributeString("buildingSides", CStr(buildingSides))
                            writer.WriteAttributeString("smoothing", CStr(smoothing).ToUpper)
                            writer.WriteAttributeString("sizeX", Trim(Str(Objects(N).Width)))
                            writer.WriteAttributeString("sizeZ", Trim(Str(Objects(N).Length)))
                            writer.WriteAttributeString("sizeBottomY", Trim(Str(sizeBottomY)))
                            writer.WriteAttributeString("textureIndexBottomX", CStr(textureIndexBottomX))
                            writer.WriteAttributeString("sizeWindowY", Trim(Str(sizeWindowY)))
                            writer.WriteAttributeString("textureIndexWindowX", CStr(textureIndexWindowX))
                            writer.WriteAttributeString("textureIndexWindowY", CStr(textureIndexWindowY))
                            writer.WriteAttributeString("sizeTopY", Trim(Str(sizeTopY)))
                            writer.WriteAttributeString("textureIndexTopX", CStr(textureIndexTopX))
                            writer.WriteAttributeString("sizeRoofY", Trim(Str(sizeRoofY)))
                            writer.WriteAttributeString("textureIndexRoofX", CStr(textureIndexRoofX))
                            writer.WriteAttributeString("textureIndexRoofY", CStr(textureIndexRoofY))
                            writer.WriteAttributeString("textureIndexRoofZ", CStr(textureIndexRoofZ))
                        End If

                        writer.WriteEndElement()
                        writer.WriteFullEndElement()
                        writer.WriteFullEndElement()
                    End If
                End If
            Next

            writer.WriteFullEndElement()
            writer.Close()

            ' delete BGL File2
            BGLFile2 = My.Application.Info.DirectoryPath & "\tools\work\" & File2 & ".BGL"
            If File.Exists(BGLFile2) Then File.Delete(BGLFile2)

            ChDrive(My.Application.Info.DirectoryPath)
            ChDir(My.Application.Info.DirectoryPath & "\tools\")

            a = My.Application.Info.DirectoryPath & "\tools\bglcomp.exe"
            b = "work\" & File2 & ".xml"

            Dim myProcess As New Process
            myProcess = Process.Start(a, b)
            myProcess.WaitForExit()
            myProcess.Dispose()

            If Not File.Exists(BGLFile2) Then
                a = "BGLComp could not produce the file" & vbCrLf & BGLFile2 & vbCrLf
                a = a & "Try to compile the file ..\tools\" & b & " in a MSDOS window" & vbCrLf
                a = a & "to read the error report!"
                MsgBox(a, MsgBoxStyle.Critical)
            End If

        End If


        If FS9xml Then
            File3 = ProjectName & "_OB1X"
            File3 = Replace(File3, " ", "_")
            a = My.Application.Info.DirectoryPath & "\tools\work\" & File3 & ".xml"

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

            writer.WriteComment("FS9 Model objects - to be compiled by FS9 BGLComp")
            For N = 1 To NoOfObjects
                If Objects(N).Selected Then
                    If Objects(N).Type = 129 Then
                        AnalyseMDLObject(N)
                        ObjComment = ObjComment & "_Obj_" & CStr(N)
                        writer.WriteComment(ObjComment)
                        writer.WriteStartElement("SceneryObject")
                        writer.WriteAttributeString("lat", Trim(Str(Objects(N).lat)))
                        writer.WriteAttributeString("lon", Trim(Str(Objects(N).lon)))
                        writer.WriteAttributeString("alt", Trim(Str(Objects(N).Altitude)))
                        writer.WriteAttributeString("altitudeIsAgl", GetTR(Objects(N).AGL))
                        writer.WriteAttributeString("pitch", Trim(Str(Objects(N).Pitch)))
                        writer.WriteAttributeString("bank", Trim(Str(Objects(N).Bank)))
                        writer.WriteAttributeString("heading", Trim(Str(Objects(N).Heading)))
                        writer.WriteAttributeString("imageComplexity", GetComplex(Objects(N).Complexity))
                        writer.WriteStartElement("LibraryObject")
                        writer.WriteAttributeString("name", ObjMDLGuid)
                        writer.WriteAttributeString("scale", Trim(Str(ObjMDLScale)))
                        writer.WriteEndElement()
                        writer.WriteFullEndElement()
                        writer.WriteStartElement("ModelData")
                        writer.WriteAttributeString("name", ObjMDLGuid)
                        writer.WriteAttributeString("sourceFile", "..\..\Mdls\" & ObjMDLFile)
                        writer.WriteEndElement()
                    End If
                End If

            Next
            writer.WriteFullEndElement()
            writer.Close()

            ' delete BGL File3
            BGLFile3 = My.Application.Info.DirectoryPath & "\tools\work\" & File3 & ".BGL"
            If File.Exists(BGLFile3) Then File.Delete(BGLFile3)

            ChDrive(My.Application.Info.DirectoryPath)
            ChDir(My.Application.Info.DirectoryPath & "\tools\")

            a = My.Application.Info.DirectoryPath & "\tools\bglcom9.exe" '  NOTE THE NAME!
            b = "work\" & File3 & ".xml"

            Dim myProcess As New Process
            myProcess = Process.Start(a, b)
            myProcess.WaitForExit()
            myProcess.Dispose()

        End If


        If FS9 Then
            File1 = ProjectName & "_OB1"
            File1 = Replace(File1, " ", "_")

            FileOpen(3, My.Application.Info.DirectoryPath & "\tools\work\" & File1 & ".scm", OpenMode.Output)
            a = "Header( 0x201 )"
            PrintLine(3, a)
            PrintLine(3)
            a = "; FS9 objects"

            For N = 1 To NoOfObjects
                If Objects(N).Selected Then
                    If Objects(N).Type = 2 Or Objects(N).Type = 3 Then
                        lat = Objects(N).lat
                        lon = Objects(N).lon
                        alt = Objects(N).Altitude
                        BiasX = Objects(N).BiasX
                        BiasY = Objects(N).BiasY
                        BiasZ = Objects(N).BiasZ
                        alt = alt + BiasZ
                        If BiasX <> 0 Or BiasY <> 0 Then
                            lon = lon + CDbl(BiasX / MetersPerDegLon(lat))
                            lat = lat + CDbl(BiasY / MetersPerDegLat)
                        End If
                        Latitude = Format(lat, "0.00000000")
                        Longitude = Format(lon, "0.00000000")
                        Altitude = Format(alt, "0.000")
                        AGL = Trim(CStr(Objects(N).AGL))
                        Heading = Format(Objects(N).Heading, "0.000")
                        Pitch = Format(Objects(N).Pitch, "0.000")
                        Bank = Format(Objects(N).Bank, "0.000")
                        Complex = Trim(CStr(Objects(N).Complexity))
                        Latitude = Replace(Latitude, ",", ".")
                        Longitude = Replace(Longitude, ",", ".")
                        Altitude = Replace(Altitude, ",", ".")
                        Heading = Replace(Heading, ",", ".")
                        Pitch = Replace(Pitch, ",", ".")
                        Bank = Replace(Bank, ",", ".")
                        ObjLibType = 1
                        AnalyseLibObject(N)
                        ObjComment = ObjComment & "_Obj_" & CStr(N)
                        a = "; Library_Object_" & ObjComment & vbCrLf
                        a = a & "LibraryObject( " & Latitude & " " & Longitude & " " & Altitude & " " & AGL
                        PrintLine(3, a)
                        a = "               " & Pitch & " " & Bank & " " & Heading & " " & Complex & "  " & FixLibID(ObjLibID) & " " & Str(ObjLibScale) & " )"
                        PrintLine(3, a)
                    End If
                End If
            Next N

            PrintLine(3)

            FileClose(3)

            ' delete BGL File1
            BGLFile1 = My.Application.Info.DirectoryPath & "\tools\work\" & File1 & ".BGL"
            If File.Exists(BGLFile1) Then File.Delete(BGLFile1)

            ChDrive(My.Application.Info.DirectoryPath)
            ChDir(My.Application.Info.DirectoryPath & "\tools\")

            a = "scasm work\" & File1 & ".scm -s -l"
            N = ExecCmd(a)

            If N > 0 Then
                a = "There was a compilation error in this project!" & vbCrLf
                a = a & "Do you want to read a SCASM report?"
                N = MsgBox(a, MsgBoxStyle.OkCancel)
                If N = 1 Then
                    a = "notepad SCAERROR.LOG"
                    N = ExecCmd(a)
                End If
                Exit Sub
            End If

        End If ' FS9!!!

        If FS8 Then

            File0 = ProjectName & "_OB0"
            File0 = Replace(File0, " ", "_")

            FileOpen(3, My.Application.Info.DirectoryPath & "\tools\work\" & File0 & ".scm", OpenMode.Output)

            a = "Header( 1 "
            a = a & Str(Int(H_NLat + 1.5)) & " "
            a = a & Str(Int(H_SLat - 0.5)) & " "
            a = a & Str(Int(H_ELon + 1.5)) & " "
            a = a & Str(Int(H_WLon - 0.5)) & " )"
            PrintLine(3, a)
            a = "LatRange( "
            a = a & Str(Int(H_SLat - 0.5)) & " "
            a = a & Str(Int(H_NLat + 1.5)) & " )"
            PrintLine(3, a)
            PrintLine(3)

            For N = 1 To NoOfObjects
                If Objects(N).Selected Then
                    If Objects(N).Type = 1 Then

                        lbNext = ":next_" & Trim(CStr(N))
                        lbRet = ":return_" & Trim(CStr(N))
                        lbPcall = ":pcall_" & Trim(CStr(N))
                        lbStart = ":start_" & Trim(CStr(N))

                        lat = Objects(N).lat
                        lon = Objects(N).lon
                        alt = Objects(N).Altitude
                        BiasX = Objects(N).BiasX
                        BiasY = Objects(N).BiasY
                        BiasZ = Objects(N).BiasZ
                        alt = alt + BiasZ
                        If BiasX <> 0 Or BiasY <> 0 Then
                            lon = lon + CDbl(BiasX / MetersPerDegLon(lat))
                            lat = lat + CDbl(BiasY / MetersPerDegLat)
                        End If

                        Latitude = Format(lat, "0.00000000")
                        Longitude = Format(lon, "0.00000000")
                        Altitude = Format(alt, "0.000")
                        AGL = Trim(CStr(Objects(N).AGL))
                        Heading = Format(Objects(N).Heading, "0.000")
                        Pitch = Format(Objects(N).Pitch, "0.000")
                        Bank = Format(Objects(N).Bank, "0.000")

                        Latitude = Replace(Latitude, ",", ".")
                        Longitude = Replace(Longitude, ",", ".")
                        Altitude = Replace(Altitude, ",", ".")
                        Heading = Replace(Heading, ",", ".")
                        Pitch = Replace(Pitch, ",", ".")
                        Bank = Replace(Bank, ",", ".")

                        Complex = Trim(CStr(Objects(N).Complexity))
                        ObjLibType = 0
                        AnalyseLibObject(N)

                        V1 = Trim(Str(ObjLibV1))
                        V2 = Trim(Str(ObjLibV2))

                        If ObjComment = "" Then ObjComment = "Object # " & CStr(N)
                        a = "; " & ObjComment & vbCrLf

                        a = a & "Area( 5 "
                        a = a & Latitude & " " & Longitude & " 50 )"
                        PrintLine(3, a)

                        a = "IfVarRange( " & lbNext & " 346 " & Complex & " 5 )"
                        PrintLine(3, a)

                        a = "PerspectiveCall( " & lbPcall & " )"
                        PrintLine(3, a)

                        a = "Jump( " & lbNext & " )"
                        PrintLine(3, a)

                        a = lbPcall
                        PrintLine(3, a)

                        a = "Perspective"
                        PrintLine(3, a)

                        If AGL = "1" Then

                            a = "RefPoint( 7 " & lbRet & " 1 " & Latitude & " " & Longitude
                            a = a & " v1= " & V1 & " v2= " & V2 & " )"
                            PrintLine(3, a)

                        Else
                            a = "RefPoint( 2 " & lbRet & " 1 " & Latitude & " " & Longitude
                            a = a & " E= " & Altitude & " v1= " & V1 & " v2= " & V2 & " )"
                            PrintLine(3, a)

                        End If

                        If ObjLibScale <> 1 Then
                            a = "SetScale( " & lbRet & " 0 0 " & Str(ObjLibScale) & " )"
                            PrintLine(3, a)
                        End If

                        a = "RotatedCall( " & lbStart & " 0 0 " & Heading & " )"
                        PrintLine(3, a)

                        a = lbRet
                        PrintLine(3, a)

                        a = "Return"
                        PrintLine(3, a)

                        a = lbStart
                        PrintLine(3, a)

                        a = "CallLibObj( 0 " & FixLibID(ObjLibID) & " )"
                        PrintLine(3, a)

                        a = "Return"
                        PrintLine(3, a)

                        a = lbNext
                        PrintLine(3, a)
                        a = "EndA"
                        PrintLine(3, a)
                        PrintLine(3)
                    End If

                    If Objects(N).Type = 4 Then 'API macros"

                        AnalyseAPIMacro(N)
                        If ObjComment = "" Then ObjComment = "Object # " & CStr(N)
                        a = "; " & ObjComment
                        PrintLine(3, a)
                        If MacroID = "AREAEND.API" Then
                            a = "EndA"
                        Else
                            a = PackAPIMacro(N)
                        End If
                        PrintLine(3, a)
                        PrintLine(3)

                    End If

                    If Objects(N).Type = 5 Then 'ASD macros"

                        AnalyseASDMacro(N)
                        If ObjComment = "" Then ObjComment = "Object # " & CStr(N)
                        a = "; " & ObjComment
                        PrintLine(3, a)
                        If MacroID = "AREAEND.SCM" Then
                            a = "EndA"
                        Else
                            a = PackASDMacro(N)
                        End If
                        PrintLine(3, a)
                        PrintLine(3)
                    End If
                End If
            Next N

            FileClose(3)

            ' delete BGL File0
            BGLFile0 = My.Application.Info.DirectoryPath & "\tools\work\" & File0 & ".BGL"
            If File.Exists(BGLFile0) Then File.Delete(BGLFile0)

            ChDrive(My.Application.Info.DirectoryPath)
            ChDir(My.Application.Info.DirectoryPath & "\tools\")

            a = "scasm work\" & File0 & ".scm -s -l"
            N = ExecCmd(a)

            If N > 0 Then
                a = "There was a compilation error in this project!" & vbCrLf
                a = a & "Do you want to read a SCASM report?"
                N = MsgBox(a, MsgBoxStyle.OkCancel)
                If N = 1 Then
                    a = "notepad SCAERROR.LOG"
                    N = ExecCmd(a)
                End If
                Exit Sub
            End If

        End If

        If Not CopyBGLs Then Exit Sub

        Dim BGLFileTarget As String
        ' copy BGL files
        Try
            If FS9 Then
                BGLFileTarget = BGLProjectFolder & "\" & File1 & ".BGL"
                If File.Exists(BGLFile1) Then File.Copy(BGLFile1, BGLFileTarget, True)
            End If
            If FS8 Then
                BGLFileTarget = BGLProjectFolder & "\" & File0 & ".BGL"
                If File.Exists(BGLFile0) Then File.Copy(BGLFile0, BGLFileTarget, True)
            End If
            If FSX Then
                BGLFileTarget = BGLProjectFolder & "\" & File2 & ".BGL"
                If File.Exists(BGLFile2) Then File.Copy(BGLFile2, BGLFileTarget, True)
            End If
            If FS9xml Then
                BGLFileTarget = BGLProjectFolder & "\" & File3 & ".BGL"
                If File.Exists(BGLFile3) Then File.Copy(BGLFile3, BGLFileTarget, True)
            End If
        Catch ex As Exception
            MsgBox("Copying BGL files failed! Try to close FSX.", MsgBoxStyle.Information)
        End Try

    End Sub

    Private Function FixLibID(ByVal ID As String) As String

        FixLibID = UCase(Mid(ID, 1, 8)) & " "
        FixLibID = FixLibID & UCase(Mid(ID, 9, 8)) & " "
        FixLibID = FixLibID & UCase(Mid(ID, 17, 8)) & " "
        FixLibID = FixLibID & UCase(Mid(ID, 25, 8))

    End Function


    Private Function Color2HexStr(ByVal Col As Integer) As String

        Dim X As Integer
        Dim C As Color

        ' this changed when making FSX version!!! hope it works!
        C = Color.FromArgb(Col)
        X = RGB(C.B, C.G, C.R)

        Color2HexStr = Hex(X)

    End Function

    Private Function RedFromInteger(ByVal Col As Integer) As Integer

        Dim C As Color
        C = Color.FromArgb(Col)
        RedFromInteger = C.B    ' troca com Blue

    End Function

    Private Function GreenFromInteger(ByVal Col As Integer) As Integer

        Dim C As Color
        C = Color.FromArgb(Col)
        GreenFromInteger = C.G

    End Function

    Private Function BlueFromInteger(ByVal Col As Integer) As Integer

        Dim C As Color
        C = Color.FromArgb(Col)
        BlueFromInteger = C.R            ' troca com red

    End Function


    Private Sub CheckLibObjects()

        LibObjectsIsOn = False
        If Dir(LibObjectsPath & "\objects.txt") <> "" Then LibObjectsIsOn = True

    End Sub

    Private Sub CheckRwy12()

        Rwy12IsOn = False
        If Dir(Rwy12Path & "\add_*.xml") <> "" Then Rwy12IsOn = True

    End Sub

    Friend Function IsMouseOnObject(ByVal x As Double, ByVal y As Double) As Integer

        ' returns the first Object that has the mouse over it! if none returns 0
        ' on entry X Y contains mouse distance from center display in pixels

        Dim N As Integer
        Dim PC As Double_XY

        IsMouseOnObject = 0
        If ObjectVIEW = False Then Exit Function

        x = LonDispCenter * PixelsPerLonDeg + x
        y = LatDispCenter * PixelsPerLatDeg - y

        For N = 1 To NoOfObjects

            PC.X = Objects(N).lon * PixelsPerLonDeg
            PC.Y = Objects(N).lat * PixelsPerLatDeg

            If PC.X > x + 5 Then GoTo Jump_Next
            If PC.X < x - 5 Then GoTo Jump_Next
            If PC.Y < y - 5 Then GoTo Jump_Next
            If PC.Y > y + 5 Then GoTo Jump_Next

            IsMouseOnObject = N
            Exit Function
Jump_Next:
        Next N

    End Function

    Private Function PackAPIMacro(ByVal N As Integer) As String

        Dim a As String
        Dim lat, lon As Double
        Dim Alt As Double
        Dim Latitude, Longitude As String
        Dim Altitude, AGL As String
        Dim Heading As String
        Dim BiasY, BiasX, BiasZ As Double
        Dim Complex As String
        Dim V1, V2 As String
        Dim Range, Scaling As String

        lat = Objects(N).lat
        lon = Objects(N).lon
        Alt = Objects(N).Altitude
        BiasX = Objects(N).BiasX
        BiasY = Objects(N).BiasY
        BiasZ = Objects(N).BiasZ
        Alt = Alt + BiasZ
        If BiasX <> 0 Or BiasY <> 0 Then
            lon = lon + CDbl(BiasX / MetersPerDegLon(lat))
            lat = lat + CDbl(BiasY / MetersPerDegLat)
        End If

        Latitude = Format(lat, "0.00000000")
        Longitude = Format(lon, "0.00000000")
        Altitude = Format(Alt, "0.000")
        AGL = Trim(CStr(Objects(N).AGL))
        Heading = Format(Objects(N).Heading, "0.000")

        Latitude = Replace(Latitude, ",", ".")
        Longitude = Replace(Longitude, ",", ".")
        Altitude = Replace(Altitude, ",", ".")
        Heading = Replace(Heading, ",", ".")

        Complex = Trim(CStr(Objects(N).Complexity))
        V1 = Trim(Str(MacroVisibility))
        V2 = Trim(Str(MacroV2Value))
        Range = Trim(Str(MacroRange))
        Scaling = Trim(Str(MacroScale))

        a = "macro( """ & MacroAPIPath & "\" & MacroID & """"
        a = a & " " & Latitude & " " & Longitude & " " & Range & " " & Scaling & " " & Heading & " "
        a = a & MacroP6Value & " " & MacroP7Value & " " & MacroP8Value & " " & MacroP9Value & " "
        PackAPIMacro = a & V1 & " " & Altitude & " " & Complex & " v2= " & V2 & " )"

    End Function

    Private Function PackASDMacro(ByVal N As Integer) As String

        Dim b, a, FileName As String
        Dim J, K As Integer
        Dim PN() As String
        Dim PV() As String
        Dim NP As Integer

        Dim lat, lon As Double
        Dim Alt As Double
        Dim Latitude, Longitude As String
        Dim Altitude, AGL As String
        Dim Heading As String
        Dim BiasY, BiasX, BiasZ As Double
        Dim Length, Complex, Width As String
        Dim V1 As String
        Dim Range, Scaling As String

        FileName = MacroASDPath & "\" & MacroID
        FileOpen(2, FileName, OpenMode.Input)

        a = LineInput(2)
        b = ""
        Do
            a = LineInput(2)
            K = InStr(1, a, "\")
            If K > 0 Then b = b & Mid(a, 2, K - 2) & ","
            If K = 0 Then
                b = b & Mid(a, 2) & ","
                Exit Do
            End If
        Loop
        FileClose(2)
        b = Replace(b, ", ", ",")
        b = Replace(b, " ,", ",")
        b = Replace(b, ",,", ",")
        b = Replace(b, ",,", ",")
        MacroString = Replace(b, "= ", "=")

        a = GetMacroValue("Name")
        a = GetMacroValue("Type")
        a = GetMacroValue("Autoscale")
        a = GetMacroValue("FixedLength")
        a = GetMacroValue("FixedWidth")
        a = GetMacroValue("Bitmap")

        NP = GetNoOfMacroParameters()

        If NP = 0 Then
            PackASDMacro = "macro( " & MacroASDPath & "\" & MacroID & " )"
            Exit Function
        End If

        ReDim PN(NP) ' parameter names
        ReDim PV(NP) ' parameter values

        For J = 1 To NP
            PN(J) = GetNextMacroParameterName()
        Next J

        lat = Objects(N).lat
        lon = Objects(N).lon
        Alt = Objects(N).Altitude
        BiasX = Objects(N).BiasX
        BiasY = Objects(N).BiasY
        BiasZ = Objects(N).BiasZ
        Alt = Alt + BiasZ
        If BiasX <> 0 Or BiasY <> 0 Then
            lon = lon + CDbl(BiasX / MetersPerDegLon(lat))
            lat = lat + CDbl(BiasY / MetersPerDegLat)
        End If

        Latitude = Format(lat, "0.00000000")
        Longitude = Format(lon, "0.00000000")
        Altitude = Format(Alt, "0.000")
        AGL = Trim(CStr(Objects(N).AGL))
        Heading = Format(Objects(N).Heading, "0.000")

        Complex = Trim(CStr(Objects(N).Complexity))
        V1 = Trim(Str(MacroVisibility))
        Range = Trim(Str(MacroRange))
        Scaling = Trim(Str(MacroScale))
        Length = Trim(Str(Objects(N).Length))
        Width = Trim(Str(Objects(N).Width))


        For J = 1 To NP
            If PN(J) = "Latitude" Then PV(J) = Latitude : GoTo next_j
            If PN(J) = "Longitude" Then PV(J) = Longitude : GoTo next_j
            If PN(J) = "Elevation" Then PV(J) = Altitude : GoTo next_j
            If PN(J) = "Rotation" Then PV(J) = Heading : GoTo next_j
            If PN(J) = "Visibility" Then PV(J) = V1 : GoTo next_j
            If PN(J) = "Range" Then PV(J) = Range : GoTo next_j
            If PN(J) = "Density" Then PV(J) = Complex : GoTo next_j
            If PN(J) = "Scale" Then PV(J) = Scaling : GoTo next_j
            If PN(J) = "Length" Then PV(J) = Length : GoTo next_j
            If PN(J) = "Width" Then PV(J) = Width : GoTo next_j

            If PN(J) = MacroP6Name Then PV(J) = MacroP6Value : GoTo next_j
            If PN(J) = MacroP7Name Then PV(J) = MacroP7Value : GoTo next_j
            If PN(J) = MacroP8Name Then PV(J) = MacroP8Value : GoTo next_j
            If PN(J) = MacroP9Name Then PV(J) = MacroP9Value : GoTo next_j
            If PN(J) = MacroPAName Then PV(J) = MacroPAValue : GoTo next_j
            If PN(J) = MacroPBName Then PV(J) = MacroPBValue : GoTo next_j
            If PN(J) = MacroPCName Then PV(J) = MacroPCValue : GoTo next_j
            If PN(J) = MacroPDName Then PV(J) = MacroPDValue
next_j:
        Next J


        a = "macro( """ & MacroASDPath & "\" & MacroID & """" & " "
        For J = 1 To NP
            a = a & Replace(PV(J), ",", ".") & " "
        Next J

        PackASDMacro = a & ")"

    End Function

    Private Function GetNextMacroParameterName() As String

        Dim N As Integer
        Dim a As String

        N = InStr(1, MacroString, ",")
        a = Mid(MacroString, 1, N)
        MacroString = Replace(MacroString, a, "")
        a = Mid(a, 1, N - 1)
        N = InStr(1, a, "=")

        If N = 0 Then
            GetNextMacroParameterName = a
        Else
            GetNextMacroParameterName = Mid(a, 1, N - 1)
        End If

    End Function

    Private Function GetNoOfMacroParameters() As Integer

        Dim N, K As Integer

        N = 0
        K = 0
        Do
            K = InStr(K + 1, MacroString, ",")
            If K = 0 Then Exit Do
            N = N + 1
        Loop

        GetNoOfMacroParameters = N

    End Function
    Friend Function GetObjectName(ByVal N As Integer) As String

        Dim K As Integer
        K = InStrRev(Objects(N).Description, "|")
        GetObjectName = Mid(Objects(N).Description, K + 1)

    End Function


    Friend Sub SelectInvertObjects()

        Dim N As Integer

        If Not ObjectVIEW Then Exit Sub

        For N = 1 To NoOfObjects
            If Objects(N).Selected Then
                NoOfObjectsSelected = NoOfObjectsSelected - 1
                Objects(N).Selected = False
                GoTo Jump_Next
            Else
                NoOfObjectsSelected = NoOfObjectsSelected + 1
                SomeSelected = True
                Objects(N).Selected = True
            End If
Jump_Next:
        Next N

    End Sub

    Private Function GetTR(ByVal X As Integer) As String

        GetTR = "FALSE"
        If X = 1 Then GetTR = "TRUE"

    End Function

    Private Function GetComplex(ByVal X As Integer) As String

        GetComplex = ""
        If X = 0 Then GetComplex = "VERY_SPARSE"
        If X = 1 Then GetComplex = "SPARSE"
        If X = 2 Then GetComplex = "NORMAL"
        If X = 3 Then GetComplex = "DENSE"
        If X = 4 Then GetComplex = "VERY_DENSE"
        If X = 5 Then GetComplex = "EXTREMELY_DENSE"

    End Function

    Private Function GetBeaconType() As String

        GetBeaconType = ""
        If ObjBeaCivil = 1 Then GetBeaconType = "CIVILIAN"
        If ObjBeaMil = 1 Then GetBeaconType = "MILITARY"

    End Function

    Private Function GetBeaconBaseType() As String

        GetBeaconBaseType = ""
        If ObjBeaAirport = 1 Then GetBeaconBaseType = "AIRPORT"
        If ObjBeaSeaBase = 1 Then GetBeaconBaseType = "SEA_BASE"
        If ObjBeaHeliport = 1 Then GetBeaconBaseType = "HELIPORT"

    End Function

    Friend BuildingType As Integer


    Friend sizeX As Single = 40
    Friend sizeZ As Single = 30

    Friend scale_gb As Single = 1
    Friend bottomTexture As Integer = 43
    Friend windowTexture As Integer = 43
    Friend topTexture As Integer = 43
    Friend roofTexture As Integer = 25

    Friend sizeBottomY As Single = 4
    Friend sizeWindowY As Single = 12
    Friend sizeTopY As Single = 4
    Friend sizeRoofY As Single = 4

    Friend sizeTopX As Single = 35
    Friend sizeTopZ As Single = 25

    Friend textureIndexBottomX As Integer = 256
    Friend textureIndexBottomZ As Integer = 256
    Friend textureIndexWindowX As Integer = 256
    Friend textureIndexWindowY As Integer = 768
    Friend textureIndexWindowZ As Integer = 256

    Friend textureIndexTopX As Integer = 256
    Friend textureIndexTopZ As Integer = 256

    Friend textureIndexRoofX As Integer = 256
    Friend textureIndexRoofY As Integer = 256
    Friend textureIndexRoofZ As Integer = 256

    Friend gableTexture As Integer = 1043
    Friend textureIndexGableY As Integer = 256
    Friend textureIndexGableZ As Integer = 256
    Friend faceTexture As Integer = 1043
    Friend textureIndexFaceX As Integer = 256
    Friend textureIndexFaceY As Integer = 256
    Friend buildingSides As Integer = 6
    Friend smoothing As Boolean = False

    Friend Sub AnalyseGenBObject(ByVal N As Integer)

        Dim a As String = ""
        Dim M1, M2 As Integer
        a = Objects(N).Description

        M1 = 1
        M2 = InStr(M1, a, "|")
        scale_gb = Val(Mid(a, M1, M2 - M1))

        M1 = M2 + 1
        M2 = InStr(M1, a, "|")
        bottomTexture = CInt(Mid(a, M1, M2 - M1))

        M1 = M2 + 1
        M2 = InStr(M1, a, "|")
        roofTexture = CInt(Mid(a, M1, M2 - M1))

        M1 = M2 + 1
        M2 = InStr(M1, a, "|")
        topTexture = CInt(Mid(a, M1, M2 - M1))

        M1 = M2 + 1
        M2 = InStr(M1, a, "|")
        windowTexture = CInt(Mid(a, M1, M2 - M1))

        If Objects(N).Type = 256 Or Objects(N).Type = 257 _
                   Or Objects(N).Type = 258 Or Objects(N).Type = 259 Then ' Rect flat

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            sizeBottomY = Val(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexBottomX = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexBottomZ = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            sizeWindowY = Val(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexWindowX = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexWindowY = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexWindowZ = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            sizeTopY = Val(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexTopX = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexTopZ = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexRoofX = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexRoofZ = CInt(Mid(a, M1, M2 - M1))

        End If


        If Objects(N).Type = 256 Then  ' Rect flat

            ObjComment = Mid(a, M2 + 1)

        End If


        If Objects(N).Type = 257 Then  ' Rect peaked

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            sizeRoofY = Val(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexRoofY = CInt(Mid(a, M1, M2 - M1))

            ObjComment = Mid(a, M2 + 1)

        End If

        If Objects(N).Type = 258 Then  ' Rect Ridge

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            sizeRoofY = Val(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            gableTexture = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexGableY = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexGableZ = CInt(Mid(a, M1, M2 - M1))

            ObjComment = Mid(a, M2 + 1)

        End If

        If Objects(N).Type = 259 Then  ' Rect slant

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            sizeRoofY = Val(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            gableTexture = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexGableY = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexGableZ = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            faceTexture = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexFaceX = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexFaceY = CInt(Mid(a, M1, M2 - M1))

            ObjComment = Mid(a, M2 + 1)

        End If

        If Objects(N).Type = 260 Then  ' pyramidal

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            sizeTopX = Val(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            sizeTopZ = Val(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            sizeBottomY = Val(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexBottomX = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexBottomZ = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            sizeWindowY = Val(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexWindowX = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexWindowY = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexWindowZ = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            sizeTopY = Val(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexTopX = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexTopZ = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexRoofX = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexRoofZ = CInt(Mid(a, M1, M2 - M1))

            ObjComment = Mid(a, M2 + 1)

        End If

        If Objects(N).Type = 261 Then ' multi sided

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            buildingSides = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            smoothing = CBool(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            sizeBottomY = Val(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexBottomX = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            sizeWindowY = Val(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexWindowX = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexWindowY = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            sizeTopY = Val(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexTopX = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            sizeRoofY = Val(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexRoofX = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexRoofY = CInt(Mid(a, M1, M2 - M1))

            M1 = M2 + 1
            M2 = InStr(M1, a, "|")
            textureIndexRoofZ = CInt(Mid(a, M1, M2 - M1))

            ObjComment = Mid(a, M2 + 1)

        End If



    End Sub

    Friend Function MakeGBTextures() As String

        Dim a As String = CStr(bottomTexture) & "|"
        a = a & CStr(roofTexture) & "|"
        a = a & CStr(topTexture) & "|"
        a = a & CStr(windowTexture)
        MakeGBTextures = a

    End Function

    Friend Function MakeGBIndexes(ByVal type As Integer) As String

        Dim a As String = ""

        If type = 256 Or type = 257 _
            Or type = 258 Or type = 259 Then ' Rect flat
            a = a & Trim(Str(sizeBottomY)) & "|"
            a = a & CStr(textureIndexBottomX) & "|"
            a = a & CStr(textureIndexBottomZ) & "|"
            a = a & Trim(Str(sizeWindowY)) & "|"
            a = a & CStr(textureIndexWindowX) & "|"
            a = a & CStr(textureIndexWindowY) & "|"
            a = a & CStr(textureIndexWindowZ) & "|"
            a = a & Trim(Str(sizeTopY)) & "|"
            a = a & CStr(textureIndexTopX) & "|"
            a = a & CStr(textureIndexTopZ) & "|"
            a = a & CStr(textureIndexRoofX) & "|"
            a = a & CStr(textureIndexRoofZ)
        End If

        If type = 257 Then  ' Rect peaked
            a = a & "|" & Trim(Str(sizeRoofY)) & "|"
            a = a & CStr(textureIndexRoofY)
        End If

        If type = 258 Then  ' Rect Ridge
            a = a & "|" & Trim(Str(sizeRoofY)) & "|"
            a = a & CStr(gableTexture) & "|"
            a = a & CStr(textureIndexGableY) & "|"
            a = a & CStr(textureIndexGableZ)
        End If

        If type = 259 Then  ' Rect slant
            a = a & "|" & Trim(Str(sizeRoofY)) & "|"
            a = a & CStr(gableTexture) & "|"
            a = a & CStr(textureIndexGableY) & "|"
            a = a & CStr(textureIndexGableZ) & "|"
            a = a & CStr(faceTexture) & "|"
            a = a & CStr(textureIndexFaceX) & "|"
            a = a & CStr(textureIndexFaceY)
        End If

        If type = 260 Then  ' pyramidal
            a = a & Trim(Str(sizeTopX)) & "|"
            a = a & Trim(Str(sizeTopZ)) & "|"
            a = a & Trim(Str(sizeBottomY)) & "|"
            a = a & CStr(textureIndexBottomX) & "|"
            a = a & CStr(textureIndexBottomZ) & "|"
            a = a & Trim(Str(sizeWindowY)) & "|"
            a = a & CStr(textureIndexWindowX) & "|"
            a = a & CStr(textureIndexWindowY) & "|"
            a = a & CStr(textureIndexWindowZ) & "|"
            a = a & Trim(Str(sizeTopY)) & "|"
            a = a & CStr(textureIndexTopX) & "|"
            a = a & CStr(textureIndexTopZ) & "|"
            a = a & CStr(textureIndexRoofX) & "|"
            a = a & CStr(textureIndexRoofZ)
        End If

        If type = 261 Then ' multi sided
            a = a & CStr(buildingSides) & "|"
            a = a & CStr(smoothing) & "|"
            a = a & Trim(Str(sizeBottomY)) & "|"
            a = a & CStr(textureIndexBottomX) & "|"
            a = a & Trim(Str(sizeWindowY)) & "|"
            a = a & CStr(textureIndexWindowX) & "|"
            a = a & CStr(textureIndexWindowY) & "|"
            a = a & Trim(Str(sizeTopY)) & "|"
            a = a & CStr(textureIndexTopX) & "|"
            a = a & Trim(Str(sizeRoofY)) & "|"
            a = a & CStr(textureIndexRoofX) & "|"
            a = a & CStr(textureIndexRoofY) & "|"
            a = a & CStr(textureIndexRoofZ)
        End If

        MakeGBIndexes = a

    End Function

End Module

