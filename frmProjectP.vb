Option Strict Off
Option Explicit On

Imports System.Windows.Forms

Friend Class FrmProjectP

    Private DoBackUp As Boolean
    Private DoCenter As Boolean
    Private MouseButton As Short


    Private Sub FrmProjectP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim N As Integer

        lbMaps.Text = "Maps = " & NoOfMaps
        lbLines.Text = "Lines = " & NoOfLines
        lbPolys.Text = "Polys = " & NoOfPolys
        lbWaters.Text = "Waters = " & NoOfWaters
        lbObjects.Text = "Objects = " & NoOfObjects
        lbExcludes.Text = "Excludes = " & NoOfExcludes
        lbLands.Text = "Lands = " & NoOfLands

        txtName.Text = ProjectName
        txtBGLFolder.Text = BGLProjectFolder

        For N = 1 To NoOfLWCIs
            lstClassItems.Items.Add(LWCIs(N).Text)
        Next N

        N = NoOfLWCIs
        If N > 0 Then
            ' lbClassItem.Text = VB6.GetItemString(lstClassItems, N - 1)
            lbClassItem.Text = lstClassItems.GetItemText(N - 1)
            lbClassItem.BackColor = LWCIs(N).Color
            lbClassItem.ForeColor = InvertColor(LWCIs(N).Color)
            lstClassItems.SelectedIndex = N - 1
        End If

        DoBackUp = False
        DoCenter = False


    End Sub

    Private Sub CmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        DialogResult = System.Windows.Forms.DialogResult.OK

        If DoBackUp Then BackUp()

        ProjectName = txtName.Text
        frmStart.Text = AppTitle & "  " & UCase(ProjectName)
        If txtBGLFolder.Text <> "" Then BGLProjectFolder = txtBGLFolder.Text
        CheckFolders()

        If ckBGLFolder.Checked Then
            BGLFolder = BGLProjectFolder
            WriteIniValue(AppIni, "Main", "BGLFolder", CStr(BGLFolder))
        End If

        ckBGLFolder.Checked = False

        ViewON = True

        RebuildDisplay()

        Dispose()

    End Sub

    Private Sub CmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Dispose()

    End Sub

    Private Sub CmdBGL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBGL.Click

        Dim A As String = "Choose the Scenery Folder where BGL Files are copied into."

        frmStart.FolderBrowserDialog1.Description = A
        If frmStart.FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtBGLFolder.Text = frmStart.FolderBrowserDialog1.SelectedPath
        End If

    End Sub

    Private Sub CmdClassIndexAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClassIndexAdd.Click

        Zoom = 1
        Dim A, B As String
        Dim IsLand As Boolean
        Dim N2, N, N1 As Integer
        Dim P0, P1, P2, P3 As Byte

        On Error GoTo erro

        A = "Each definition starts either with the word Land or Water and then 3 numbers "
        A = A & "separated by spaces. You can also add text at the end "
        A = A & "as in the example. The 3 numbers refer to 3 classes "
        A = A & "and the definition is assigned to a color. "
        A = A & "Whenever this color is found on a Class Map, the 1st class is used 60% of the time, the 2nd "
        A = A & "class 25% and the 3rd class 15%. You can mix Land and Water definitions."

        A = Trim(InputBox(A, , "Land 101 102 103 City Textures")) & " "

        N1 = 1
        N2 = InStr(1, A, " ")
        B = UCase(Mid(A, 1, N2 - N1))
        If B = "LAND" Then
            IsLand = True
        ElseIf B = "WATER" Then
            IsLand = False
        Else
            GoTo erro
        End If

        N1 = N2 + 1
        N2 = InStr(N1, A, " ")
        P0 = CByte(Mid(A, N1, N2 - N1))
        If IsLand Then
            For N = 1 To NoOfLCs
                If P0 = LC(N).Index Then Exit For
            Next N
            If N > NoOfLCs Then GoTo erro
        Else
            For N = 1 To NoOfWCs
                If P0 = WC(N).Index Then Exit For
            Next N
            If N > NoOfWCs Then GoTo erro
        End If
        P1 = CByte(N)

        N1 = N2 + 1
        N2 = InStr(N1, A, " ")
        P0 = CByte(Mid(A, N1, N2 - N1))
        If IsLand Then
            For N = 1 To NoOfLCs
                If P0 = LC(N).Index Then Exit For
            Next N
            If N > NoOfLCs Then GoTo erro
        Else
            For N = 1 To NoOfWCs
                If P0 = WC(N).Index Then Exit For
            Next N
            If N > NoOfWCs Then GoTo erro
        End If
        P2 = CByte(N)

        N1 = N2 + 1
        N2 = InStr(N1, A, " ")
        P0 = CByte(Mid(A, N1, N2 - N1))
        If IsLand Then
            For N = 1 To NoOfLCs
                If P0 = LC(N).Index Then Exit For
            Next N
            If N > NoOfLCs Then GoTo erro
        Else
            For N = 1 To NoOfWCs
                If P0 = WC(N).Index Then Exit For
            Next N
            If N > NoOfWCs Then GoTo erro
        End If
        P3 = CByte(N)

        BackUp()

        If NoOfLWCIs = 0 Then
            NoOfLWCIs = 1
        Else
            NoOfLWCIs = NoOfLWCIs + 1
            ReDim Preserve LWCIs(NoOfLWCIs)
        End If

        N = NoOfLWCIs


        LWCIs(N).Class1 = P1
        LWCIs(N).Class2 = P2
        LWCIs(N).Class3 = P3
        If IsLand Then
            LWCIs(N).Color = Color.GreenYellow
            LWCIs(N).IsLand = True
        Else
            LWCIs(N).Color = Color.Blue
            LWCIs(N).IsLand = False
        End If
        LWCIs(N).Text = A

        lstClassItems.Items.Add(A)
        ' lbClassItem.Text = VB6.GetItemString(lstClassItems, N - 1)
        lbClassItem.Text = lstClassItems.GetItemText(N - 1)
        lbClassItem.BackColor = LWCIs(N).Color
        lbClassItem.ForeColor = InvertColor(LWCIs(N).Color)

        Dirty = True

        Exit Sub

erro:
        MsgBox("Please check your entry!")

    End Sub

    Private Sub CmdClassIndexDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClassIndexDelete.Click

        Dim N, K As Integer

        If NoOfLWCIs = 0 Then Exit Sub

        BackUp()

        If NoOfLWCIs = 1 Then
            NoOfLWCIs = 0
            lstClassItems.Items.RemoveAt(0)
            lbClassItem.Text = ""
            lbClassItem.BackColor = System.Drawing.ColorTranslator.FromOle(RGB(255, 255, 255))
            Exit Sub
        End If

        N = lstClassItems.SelectedIndex + 1

        If N = NoOfLWCIs Then
            ReDim Preserve LWCIs(N - 1)
            NoOfLWCIs = NoOfLWCIs - 1
            lstClassItems.Items.RemoveAt(N - 1)
            lstClassItems.SelectedIndex = N - 2
            ' lstClassItems_SelectedIndexChanged(lstClassItems, New System.EventArgs())
            Exit Sub
        End If

        For K = N To NoOfLWCIs - 1
            LWCIs(K).Class1 = LWCIs(K + 1).Class1
            LWCIs(K).Class2 = LWCIs(K + 1).Class2
            LWCIs(K).Class3 = LWCIs(K + 1).Class3
            LWCIs(K).Color = LWCIs(K + 1).Color
            LWCIs(K).IsLand = LWCIs(K + 1).IsLand
            LWCIs(K).Text = LWCIs(K + 1).Text
        Next K

        NoOfLWCIs = NoOfLWCIs - 1

        ReDim Preserve LWCIs(NoOfLWCIs)

        lstClassItems.Items.RemoveAt(N - 1)
        lstClassItems.SelectedIndex = N - 1
        'lstClassItems_SelectedIndexChanged(lstClassItems, New System.EventArgs())

        Dirty = True



    End Sub

    Private Sub CmdClassIndexEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClassIndexEdit.Click

        Dim A, B As String
        Dim P2, N2, N, N1, P1, P3 As Integer
        Dim P0 As Byte
        Dim N0 As Integer
        Dim IsLand As Boolean

        On Error GoTo erro

        If NoOfLWCIs = 0 Then Exit Sub
        N0 = lstClassItems.SelectedIndex + 1

        A = "Each definition starts either with the word Land or Water and then 3 numbers "
        A = A & "separated by spaces. You can also add text at the end "
        A = A & "as in the example. The 3 numbers refer to 3 classes "
        A = A & "and the definition is assigned to a color. "
        A = A & "Whenever this color is found on a Class Map, the 1st class is used 60% of the time, the 2nd "
        A = A & "class 25% and the 3rd class 15%. You can mix Land and Water definitions."

        A = Trim(InputBox(A, , LWCIs(N0).Text)) & " "

        N1 = 1
        N2 = InStr(1, A, " ")
        B = UCase(Mid(A, 1, N2 - N1))
        If B = "LAND" Then
            IsLand = True
        ElseIf B = "WATER" Then
            IsLand = False
        Else
            GoTo erro
        End If

        N1 = N2 + 1
        N2 = InStr(N1, A, " ")
        P0 = CByte(Mid(A, N1, N2 - N1))
        If IsLand Then
            For N = 1 To NoOfLCs
                If P0 = LC(N).Index Then Exit For
            Next N
            If N > NoOfLCs Then GoTo erro
        Else
            For N = 1 To NoOfWCs
                If P0 = WC(N).Index Then Exit For
            Next N
            If N > NoOfWCs Then GoTo erro
        End If
        P1 = CByte(N)

        N1 = N2 + 1
        N2 = InStr(N1, A, " ")
        P0 = CByte(Mid(A, N1, N2 - N1))
        If IsLand Then
            For N = 1 To NoOfLCs
                If P0 = LC(N).Index Then Exit For
            Next N
            If N > NoOfLCs Then GoTo erro
        Else
            For N = 1 To NoOfWCs
                If P0 = WC(N).Index Then Exit For
            Next N
            If N > NoOfWCs Then GoTo erro
        End If
        P2 = CByte(N)

        N1 = N2 + 1
        N2 = InStr(N1, A, " ")
        P0 = CByte(Mid(A, N1, N2 - N1))
        If IsLand Then
            For N = 1 To NoOfLCs
                If P0 = LC(N).Index Then Exit For
            Next N
            If N > NoOfLCs Then GoTo erro
        Else
            For N = 1 To NoOfWCs
                If P0 = WC(N).Index Then Exit For
            Next N
            If N > NoOfWCs Then GoTo erro
        End If
        P3 = CByte(N)

        BackUp()

        LWCIs(N0).Class1 = P1
        LWCIs(N0).Class2 = P2
        LWCIs(N0).Class3 = P3
        LWCIs(N0).Text = A
        LWCIs(N0).IsLand = IsLand

        ' VB6.SetItemString(lstClassItems, N0 - 1, A)
        lstClassItems.Items(N0 - 1) = A

        lbClassItem.Text = A

        Dirty = True
        Exit Sub

erro:
        MsgBox("Please check your entry!")


    End Sub



    Private Sub LstClassItems_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstClassItems.SelectedIndexChanged

        Dim N As Integer

        N = lstClassItems.SelectedIndex + 1

        ' lbClassItem.Text = VB6.GetItemString(lstClassItems, N - 1)
        lbClassItem.Text = lstClassItems.GetItemText(N - 1)
        lbClassItem.BackColor = LWCIs(N).Color
        lbClassItem.ForeColor = InvertColor(LWCIs(N).Color)

    End Sub

    Private Sub LbClassItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbClassItem.Click

        If NoOfLWCIs = 0 Then Exit Sub

        If MouseButton = 1 Then
            ColorFromPalette()
            Exit Sub
        End If

        If MouseButton = 2 Then
            frmStart.PointerToolStripButton.PerformClick()
            frmStart.SetMouseIcon()
            LColPickON = True
            Hide()
        End If

    End Sub

    Private Sub LbClassItem_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lbClassItem.MouseDown

        Dim Button As Short = e.Button \ &H100000

        If NoOfLWCIs = 0 Then Exit Sub

        If Button = 2 Then
            MouseButton = 2
            Exit Sub
        End If

        If Button = 1 Then
            MouseButton = 1
        End If

    End Sub

    Private Sub ColorFromPalette()

        ARGBColor = lbClassItem.BackColor
        If FrmTransparency.ShowDialog = Windows.Forms.DialogResult.OK Then
            lbClassItem.BackColor = ARGBColor
        End If

        Dim N As Integer

        N = lstClassItems.SelectedIndex + 1
        LWCIs(N).Color = ARGBColor
        lbClassItem.ForeColor = InvertColor(ARGBColor)

        BackUp()
        Dirty = True

    End Sub



End Class
