


Public Class FrmLibrary

    Private TempCategory As New ArrayList
    Private ChangeIsOFF As Boolean = False
    Private IsLib As Boolean = True
    Private LibCategoriesX() As LibCategory
    Private NoOfLibCategoriesX As Integer
    Private RemovedObjs As New ArrayList  ' go there the origin category and the guid separated by a space
    Private CatOrderChanged() As Boolean
    Private CatObjectsAdded() As Boolean

    Private Sub FrmLibrary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim K As Integer
        Dim a, LibCat As String

        IsLib = True

        If NoOfLibCategories = 0 Then Exit Sub

        cmbLibCat.Items.Clear()
        lstLib.Items.Clear()

        NoOfLibCategoriesX = NoOfLibCategories ' make the copies
        ReDim LibCategoriesX(NoOfLibCategoriesX)
        ReDim CatOrderChanged(NoOfLibCategoriesX)
        ReDim CatObjectsAdded(NoOfLibCategoriesX)
        For K = 1 To NoOfLibCategoriesX
            LibCategoriesX(K).Objs = New ArrayList
            CatOrderChanged(K) = False
            CatObjectsAdded(K) = False
        Next K
        LibCategoriesX = LibCategories

        For K = 1 To NoOfLibCategories
            cmbLibCat.Items.Add(LibCategories(K).Name)   ' display
        Next K

        'Dim g As LibCategory
        Dim g As LibObject
        For Each g In LibCategories(1).Objs
            lstLib.Items.Add(g.Name)
        Next

        cmbLibCat.SelectedIndex = 0
        lstLib.SelectedIndex = 0

        LibCat = LibCategories(1).Name
        Dim myLibObj As LibObject = LibCategories(1).Objs(0)

        txtLibID.Text = myLibObj.ID
        txtLibWidth.Text = CStr(myLibObj.Width)
        txtLibLength.Text = CStr(myLibObj.Length)
        txtLibScale.Text = CStr(myLibObj.Scaling)
        txtLibName.Text = myLibObj.Name

        ObjLibType = CInt(myLibObj.Type)

        labelFSTemp.Text = "Categorized " & GetFSType(ObjLibType)

        a = LibObjectsPath & "\" & LibCat & "\" & txtLibID.Text & ".jpg"
        ImageFileNameTrue = a
        If Not File.Exists(a) Then
            a = My.Application.Info.DirectoryPath & "\tools\bmps\none.jpg"
        End If

        imgLib.Image = System.Drawing.Image.FromFile(a)
        ImageFileName = a

        CheckUI()

    End Sub


    Private Sub CmbLibCat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLibCat.SelectedIndexChanged

        If ChangeIsOFF Then Exit Sub

        IsLib = True

        Dim K As Integer
        Dim a, LibCat As String

        'first remove all listings
        lstLib.Items.Clear()

        ' get the category index
        K = cmbLibCat.SelectedIndex + 1

        'Dim g As LibCategory
        Dim g As LibObject

        For Each g In LibCategories(K).Objs
            lstLib.Items.Add(g.Name)
        Next

        If LibCategories(K).Objs.Count = 0 Then
            txtLibWidth.Text = ""
            txtLibLength.Text = ""
            txtLibScale.Text = ""
            txtLibName.Text = ""
            CheckUI()
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

        labelFSTemp.Text = "Categorized " & GetFSType(ObjLibType)

        a = LibObjectsPath & "\" & LibCat & "\" & txtLibID.Text & ".jpg"
        ImageFileNameTrue = a
        If Not File.Exists(a) Then
            a = My.Application.Info.DirectoryPath & "\tools\bmps\none.jpg"
        End If

        imgLib.Image = System.Drawing.Image.FromFile(a)
        ImageFileName = a
        CheckUI()

    End Sub

    Private Sub LstLib_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstLib.SelectedIndexChanged

        If ChangeIsOFF Then Exit Sub

        Dim N, K As Integer
        Dim a, LibCat As String

        ' get the category index
        K = cmbLibCat.SelectedIndex + 1
        If K < 1 Then Exit Sub
        N = lstLib.SelectedIndex
        If N < 0 Then Exit Sub

        IsLib = True
        LibCat = LibCategories(K).Name
        Dim myLibObj As LibObject = LibCategories(K).Objs(N)

        txtLibID.Text = myLibObj.ID
        txtLibWidth.Text = CStr(myLibObj.Width)
        txtLibLength.Text = CStr(myLibObj.Length)
        txtLibScale.Text = CStr(myLibObj.Scaling)
        txtLibName.Text = myLibObj.Name

        ObjLibType = CInt(myLibObj.Type)

        '' after 205
        'txtComment.Text = myLibObj.Name

        labelFSTemp.Text = "Categorized " & GetFSType(ObjLibType)

        'cmdUpDefault.Enabled = False

        a = LibObjectsPath & "\" & LibCat & "\" & txtLibID.Text & ".jpg"
        ImageFileNameTrue = a
        If Not File.Exists(a) Then
            a = My.Application.Info.DirectoryPath & "\tools\bmps\none.jpg"
        End If

        imgLib.Image = System.Drawing.Image.FromFile(a)
        ImageFileName = a

        CheckUI()

    End Sub

    Private Sub CmdRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemove.Click

        If lstLib.SelectedItems.Count = 0 Then Exit Sub
        ChangeIsOFF = True

        Dim N, J, K As Integer

        K = cmbLibCat.SelectedIndex + 1
        If K < 1 Then Exit Sub

        Dim JJ As ListBox.SelectedIndexCollection
        JJ = lstLib.SelectedIndices

        lstBGL.ClearSelected()
        N = lstBGL.Items.Count
        For Each J In JJ
            lstBGL.Items.Add(LibCategories(K).Objs(J).Name)
            lstBGL.SetSelected(N, True)
            N = N + 1
        Next

        CatOrderChanged(K) = True

        Dim Flag As Boolean = (K <= NoOfLibCategoriesX)
        Dim A As String = K.ToString & " "
        For Each J In JJ
            TempCategory.Add(LibCategories(K).Objs(J))
            If Flag Then RemovedObjs.Add(A & LibCategories(K).Objs(J).ID)
        Next

        N = JJ.Count - 1
        For J = N To 0 Step -1
            LibCategories(K).Objs.RemoveAt(JJ(J))
        Next

        lstLib.Items.Clear()

        'Dim g As LibCategory
        Dim g As LibObject
        For Each g In LibCategories(K).Objs
            lstLib.Items.Add(g.Name)
        Next

        ChangeIsOFF = False
        IsLib = False
        CheckUI()

    End Sub

    Private Sub CmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click

        If lstBGL.SelectedItems.Count = 0 Then Exit Sub
        ChangeIsOFF = True

        Dim N, J, K As Integer

        K = cmbLibCat.SelectedIndex + 1
        If K < 1 Then
            MsgBox("Make a Category first")
            Exit Sub
        End If

        CatOrderChanged(K) = True
        CatObjectsAdded(K) = True

        Dim JJ As ListBox.SelectedIndexCollection
        JJ = lstBGL.SelectedIndices

        lstLib.ClearSelected()
        N = lstLib.Items.Count
        For Each J In JJ
            lstLib.Items.Add(TempCategory(J).Name)
            lstLib.SetSelected(N, True)
            N = N + 1
        Next
        'N = N - 1

        For Each J In JJ
            LibCategories(K).Objs.Add(TempCategory(J))
        Next

        N = JJ.Count - 1
        For J = N To 0 Step -1
            TempCategory.RemoveAt(JJ(J))
        Next

        lstBGL.Items.Clear()

        Dim g As LibObject

        For Each g In TempCategory
            lstBGL.Items.Add(g.Name)
        Next

        ChangeIsOFF = False
        IsLib = True
        CheckUI()

    End Sub

    Private Sub CheckUI()

        LabelNoBGLs.Text = (TempCategory.Count).ToString
        Dim K As Integer = cmbLibCat.SelectedIndex + 1
        If K < 1 Then Exit Sub
        LabelNoLIBs.Text = (LibCategories(K).Objs.Count).ToString

        If lstBGL.SelectedItems.Count > 0 Then
            cmdAdd.Enabled = True
            txtBGLID.Text = (TempCategory(lstBGL.SelectedIndex).ID).ToString
            labelFSTemp.Text = "Temporary " & GetFSType(TempCategory(lstBGL.SelectedIndex).Type)
        Else
            cmdAdd.Enabled = False
            txtBGLID.Text = ""
            labelFSTemp.Text = ""
        End If

        If lstLib.SelectedItems.Count > 0 Then
            cmdRemove.Enabled = True
            txtLibID.Text = (LibCategories(K).Objs(lstLib.SelectedIndex).ID).ToString
            labelFS.Text = "Categorized " & GetFSType(LibCategories(K).Objs(lstLib.SelectedIndex).Type)
        Else
            cmdRemove.Enabled = False
            txtLibID.Text = ""
            labelFS.Text = ""
        End If


        cmdUP.Enabled = False
        cmdDown.Enabled = False
        If lstLib.SelectedItems.Count = 1 Then
            If lstLib.SelectedIndex > 0 Then
                cmdUP.Enabled = True
            End If
            If lstLib.SelectedIndex < lstLib.Items.Count - 1 Then
                cmdDown.Enabled = True
            End If
        End If

        If lstLib.Items.Count = 0 Then
            txtLibID.Text = ""
            labelFS.Text = ""
        End If

        If lstBGL.Items.Count = 0 Then
            txtBGLID.Text = ""
            labelFSTemp.Text = ""
        End If

        cmdUpdate.Enabled = False

    End Sub

    Private Sub CmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        NoOfLibCategories = NoOfLibCategoriesX  ' get the copies
        ReDim LibCategories(NoOfLibCategories)
        LibCategories = LibCategoriesX
        Dispose()

    End Sub

    Private Sub CmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        Try
            Try
                imgLib.Image.Dispose()
            Catch ex As Exception
            End Try

            Dim A, B, C As String
            Dim K, N As Integer
            Dim newCatNames As New ArrayList
            Dim stream As FileStream
            Dim fileWriter As System.IO.StreamWriter
            Dim fileReader As System.IO.StreamReader
            Dim data As String = Replace(Date.Now.ToString, " ", "_")
            data = Replace(data, "-", "_")
            data = Replace(data, ":", "_") & "_"
            Dim outfile As String = ""
            Dim LibCatFolder As String = ""

            ' put removed jpegs in the bin
            A = LibObjectsPath & "\NewJpegs\"
            For N = 0 To RemovedObjs.Count - 1
                B = RemovedObjs(N).ToString
                K = InStr(B, " ")
                C = B.Substring(K) & ".jpg"
                K = CInt(B.Substring(0, K - 1))
                B = LibObjectsPath & "\" & LibCategories(K).Name & "\" & C
                If My.Computer.FileSystem.FileExists(B) Then
                    'Try
                    '    My.Computer.FileSystem.MoveFile(B, A & C, True)
                    'Catch ex As Exception
                    My.Computer.FileSystem.CopyFile(B, A & C, True)
                    'End Try
                End If
            Next

            'Count Objects and initiate NoOfJpegs
            Dim counter As _
            System.Collections.ObjectModel.ReadOnlyCollection(Of String)
            counter = My.Computer.FileSystem.GetFiles(LibObjectsPath & "\NewJpegs")
            NoOfJpegs = counter.Count

            Dim g As LibObject

            For N = 1 To NoOfLibCategories
                ' put backups of changed TXTs in LibObjects\BackUps and create new one in LibObjects
                If LibCategories(N).Objs.Count > 0 Then
                    ' this cat exists
                    newCatNames.Add(LibCategories(N).Name)
                    If CatOrderChanged(N) Then ' if order changed
                        A = LibObjectsPath & "\" & LibCategories(N).Name & ".txt"
                        If File.Exists(A) Then
                            B = LibObjectsPath & "\BackUps\" & data & LibCategories(N).Name & ".txt"
                            My.Computer.FileSystem.MoveFile(A, B, True)
                        End If
                        stream = New FileStream(LibObjectsPath & "\" & LibCategories(N).Name & ".txt", FileMode.Create)
                        fileWriter = New System.IO.StreamWriter(stream)
                        fileWriter.WriteLine("[" & LibCategories(N).Name & "]")
                        For Each g In LibCategories(N).Objs
                            A = g.ID & " " & g.Type & " " & g.Width & " " & g.Length & " " & g.Scaling & " " & g.Name
                            fileWriter.WriteLine(A)
                        Next
                        'should I realy comment the following? October 2017
                        'fileWriter.Close()
                        stream.Close()
                    End If
                    If CatObjectsAdded(N) Then
                        If NoOfJpegs > 0 Then
                            For Each g In LibCategories(N).Objs
                                LibCatFolder = LibObjectsPath & "\" & LibCategories(N).Name
                                B = LibCatFolder & "\" & g.ID & ".jpg"
                                If Not My.Computer.FileSystem.FileExists(B) Then
                                    C = g.ID & "*.jpg"
                                    Dim myfiles As System.Collections.ObjectModel.ReadOnlyCollection(Of String) _
                                             = My.Computer.FileSystem.GetFiles(LibObjectsPath & "\NewJpegs",
                                             FileIO.SearchOption.SearchAllSubDirectories, C)
                                    For Each myfile As String In myfiles
                                        My.Computer.FileSystem.MoveFile(myfile, B, True)
                                        NoOfJpegs = NoOfJpegs - 1
                                    Next
                                End If
                            Next
                        End If

                    End If
                End If
            Next

            ' now change objects.txt for next startups
            If My.Computer.FileSystem.FileExists(LibObjectsPath & "\objects.txt") Then
                ' rewrite objects.txt
                stream = New FileStream(LibObjectsPath & "\objects.txt", FileMode.Open)
                fileReader = New System.IO.StreamReader(stream)
                Dim line As String = ""
                Do Until fileReader.EndOfStream
                    line = fileReader.ReadLine()
                    If line.Length > 0 Then
                        If line.Substring(0, 1) = "i" Then
                            A = line.Substring(8)
                            N = A.Length
                            A = A.Substring(0, N - 4)
                            If newCatNames.Contains(A) Then
                                newCatNames.Remove(A)
                            Else
                                line = "; comment on " & Date.Now.ToString & vbCrLf & "; " & line
                            End If
                        End If
                    End If
                    outfile = outfile & line & vbCrLf
                Loop
                'should I realy comment the following? October 2017
                'fileReader.Close()
                stream.Close()
            End If

            If newCatNames.Count > 0 Then
                outfile = outfile & vbCrLf & "; added in " & Date.Now.ToString & vbCrLf
                For Each name As String In newCatNames
                    outfile = outfile & "include=" & name & ".txt" & vbCrLf
                Next
            End If

            A = LibObjectsPath & "\objects.txt"
            If My.Computer.FileSystem.FileExists(A) Then
                B = LibObjectsPath & "\BackUps\" & data & "objects.txt"
                My.Computer.FileSystem.CopyFile(A, B, True)
            End If

            stream = New FileStream(A, FileMode.Create)
            fileWriter = New System.IO.StreamWriter(stream)
            fileWriter.Write(outfile)
            'should I realy comment the following? October 2017
            'fileWriter.Close()
            stream.Close()

            'SetLibObjects()

            ' move rest of New to BackUp jpegs
            A = LibObjectsPath & "\NewJpegs"
            B = LibObjectsPath & "\BackUps\"

            For Each foundFile As String In My.Computer.FileSystem.GetFiles(
                A, FileIO.SearchOption.SearchAllSubDirectories, "*.*")
                Dim foundFileInfo As New System.IO.FileInfo(foundFile)
                My.Computer.FileSystem.MoveFile(foundFile, B & foundFileInfo.Name, True)
            Next

        Catch ex As Exception
            MsgBox("Could not update Categories!", MsgBoxStyle.Critical)
        End Try

        Dispose()

    End Sub


    Private Sub LstBGL_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstBGL.SelectedIndexChanged

        If ChangeIsOFF Then Exit Sub

        Dim N, K As Integer
        Dim a, LibCat As String

        ' get the category index
        K = cmbLibCat.SelectedIndex + 1
        N = lstBGL.SelectedIndex
        If K < 1 Then Exit Sub
        If N < 0 Then Exit Sub

        IsLib = False
        LibCat = LibCategories(K).Name

        Dim myLibObj As LibObject = TempCategory(N)

        txtBGLID.Text = myLibObj.ID
        txtLibWidth.Text = CStr(myLibObj.Width)
        txtLibLength.Text = CStr(myLibObj.Length)
        txtLibScale.Text = CStr(myLibObj.Scaling)
        txtLibName.Text = myLibObj.Name

        ObjLibType = CInt(myLibObj.Type)

        labelFSTemp.Text = "Temporary " & GetFSType(ObjLibType)

        a = LibObjectsPath & "\" & LibCat & "\" & txtBGLID.Text & ".jpg"
        ImageFileNameTrue = a
        If Not File.Exists(a) Then
            a = LibObjectsPath & "\" & LibCat & "\" & txtBGLID.Text & ".bmp"
            If Not File.Exists(a) Then
                a = My.Application.Info.DirectoryPath & "\tools\bmps\none.jpg"
            End If
        End If

        imgLib.Image = System.Drawing.Image.FromFile(a)
        ImageFileName = a

        CheckUI()

    End Sub

    Private Sub CmdNewCat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNewCat.Click

        On Error GoTo erro

        Dim A As String = "Enter a Name for a Category without spaces. The "
        A = A & "category will be empty until you add objects from the "
        A = A & "temporary container on the right. If the category remains "
        A = A & "empty when you press OK, it will be eliminated."

        A = Trim(InputBox(A, , "FSX_New_Category"))
        Dim N As Integer = A.Length
        If N = 0 Then Exit Sub

        A = Replace(A, " ", "_")
        For N = 1 To NoOfLibCategories
            If LibCategories(N).Name = A Then
                MsgBox("The name already exists!", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        Next

        Dim K As Integer = NoOfLibCategories + 1
        ReDim Preserve LibCategories(K)
        ReDim Preserve CatOrderChanged(K)
        ReDim Preserve CatObjectsAdded(K)

        'CatOrderChanged(K) = True ' not needed because it is created empty
        'CatObjectsAdded(K) = True

        LibCategories(K).Name = A
        LibCategories(K).Objs = New ArrayList

        ChangeIsOFF = True
        cmbLibCat.Items.Add(LibCategories(K).Name)
        cmbLibCat.SelectedIndex = K - 1
        lstLib.Items.Clear()
        ChangeIsOFF = False

        NoOfLibCategories = NoOfLibCategories + 1
        Exit Sub

erro:
        MsgBox("Could not create a New Category!", MsgBoxStyle.Exclamation)

    End Sub

    Private Sub CmdEditCat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditCat.Click

        Dim K As Integer = cmbLibCat.SelectedIndex + 1
        If K < 1 Then Exit Sub

        Dim B As String = LibCategories(K).Name
        Dim A As String = "Enter a New name for this Category without spaces."
        A = Trim(InputBox(A, , B))
        Dim N As Integer = A.Length
        If N = 0 Then Exit Sub

        A = Replace(A, " ", "_")
        For N = 1 To NoOfLibCategories
            If LibCategories(N).Name = A Then
                MsgBox("The name already exists!", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        Next

        CatOrderChanged(K) = True

        LibCategories(K).Name = A

        cmbLibCat.Items.Clear()
        For N = 1 To NoOfLibCategories
            cmbLibCat.Items.Add(LibCategories(N).Name)   ' display
        Next N
        cmbLibCat.SelectedIndex = K - 1

    End Sub

    Private Sub CmdAZ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAZ.Click

        Dim K As Integer = cmbLibCat.SelectedIndex + 1
        If K <= 0 Then
            Exit Sub
        End If

        ChangeIsOFF = True
        LibCategories(K).Objs.Sort()
        lstLib.Items.Clear()
        'Dim g As LibCategory
        Dim g As LibObject
        For Each g In LibCategories(K).Objs
            lstLib.Items.Add(g.Name)
        Next
        ChangeIsOFF = False
        CatOrderChanged(K) = True


    End Sub

    Private Sub CmdUP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUP.Click

        Dim N As Integer = lstLib.SelectedIndex
        If N <= 0 Then
            Exit Sub
        Else
            Dim K As Integer = cmbLibCat.SelectedIndex + 1
            LibCategories(K).Objs.Reverse(N - 1, 2)
            lstLib.Items.Clear()
            'Dim g As LibCategory
            Dim g As LibObject
            For Each g In LibCategories(K).Objs
                lstLib.Items.Add(g.Name)
            Next
            lstLib.SelectedIndex = N - 1
            CatOrderChanged(K) = True
        End If


    End Sub

    Private Sub CmdDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDown.Click

        Dim N As Integer = lstLib.SelectedIndex
        If N >= lstLib.Items.Count - 1 Then
            Exit Sub
        Else
            Dim K As Integer = cmbLibCat.SelectedIndex + 1
            LibCategories(K).Objs.Reverse(N, 2)
            lstLib.Items.Clear()
            'Dim g As LibCategory
            Dim g As LibObject
            For Each g In LibCategories(K).Objs
                lstLib.Items.Add(g.Name)
            Next
            lstLib.SelectedIndex = N + 1
            CatOrderChanged(K) = True
        End If

    End Sub

    Private Function GetFSType(ByVal N As Integer) As String

        GetFSType = ""
        If N = 0 Then
            GetFSType = "FS8 Library Object"
        ElseIf N = 1 Then
            GetFSType = "FS9 Library Object"
        ElseIf N = 2 Then
            GetFSType = "FSX Library Object"
        End If

    End Function

    Private Sub CmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click

        Dim myObj As New LibObject
        If IsLib Then
            Dim K As Integer = cmbLibCat.SelectedIndex + 1
            If K < 1 Then Exit Sub
            Dim N As Integer = lstLib.SelectedIndex
            If N < 0 Then Exit Sub
            myObj.ID = (LibCategories(K).Objs(N).ID).ToString
            myObj.Name = txtLibName.Text
            myObj.Type = LibCategories(K).Objs(N).Type
            myObj.Width = CSng(Val(txtLibWidth.Text))
            myObj.Length = CSng(Val(txtLibLength.Text))
            myObj.Scaling = CSng(Val(txtLibScale.Text))
            LibCategories(K).Objs(N) = myObj
            lstLib.Items.Clear()
            'Dim g As LibCategory
            Dim g As LibObject
            For Each g In LibCategories(K).Objs
                lstLib.Items.Add(g.Name)
            Next
            lstLib.SelectedIndex = N
            CatOrderChanged(K) = True
        Else
            Dim N As Integer = lstBGL.SelectedIndex
            If N < 0 Then Exit Sub
            myObj.ID = TempCategory(N).ID
            myObj.Name = txtLibName.Text
            myObj.Type = CInt(TempCategory(N).Type)
            myObj.Width = CSng(Val(txtLibWidth.Text))
            myObj.Length = CSng(Val(txtLibLength.Text))
            myObj.Scaling = CSng(Val(txtLibScale.Text))
            TempCategory(N) = myObj
            lstBGL.Items.Clear()
            'Dim g As LibCategory
            Dim g As LibObject
            For Each g In TempCategory
                lstBGL.Items.Add(g.Name)
            Next
            lstBGL.SelectedIndex = N
        End If

        cmdUpdate.Enabled = False

    End Sub

    Private Sub CmdBGL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBGL.Click

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        Dim A, B As String

        A = "Object Library file (*.BGL)|*.bgl"
        B = "SBuilderX: Open a Library BGL file"

        A = FileNameToOpen(A, B, "LIB")

        If A = "" Then
            Exit Sub
        End If

        Cursor = System.Windows.Forms.Cursors.WaitCursor

        Dim fs As New FileStream(A, FileMode.Open, FileAccess.Read)
        Dim reader As New BinaryReader(fs)

        Dim bgl As New BGLReader

        If bgl.read(reader) = False Then
            Cursor = System.Windows.Forms.Cursors.Default
            MsgBox("Not a valid BGL!", MsgBoxStyle.Exclamation)
            'should I realy comment the following? October 2017
            'reader.Close()
            fs.Dispose()
            Exit Sub
        End If
        'should I realy comment the following? October 2017
        'reader.Close()
        fs.Dispose()

        If bgl.NoOfMDLs = 0 Then
            Cursor = System.Windows.Forms.Cursors.Default
            MsgBox("The BGL file does not contain objects!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        txtBGLFile.Text = Path.GetFileName(A)

        ChangeIsOFF = True
        IsLib = False

        lstBGL.Items.Clear()
        TempCategory.Clear()

        A = Path.GetFileNameWithoutExtension(A)
        If bgl.Type = 2 Then A = ""
        Dim myLibObj As New LibObject
        Dim N As Integer
        For N = 1 To bgl.NoOfMDLs
            myLibObj.ID = bgl.MDLs(N).Guid
            myLibObj.Name = A & bgl.MDLs(N).Name
            myLibObj.Width = bgl.MDLs(N).W
            myLibObj.Length = bgl.MDLs(N).L
            myLibObj.Type = bgl.MDLs(N).Type
            ' myLibObj.Type = bgl.Type  ' remove this after Lance
            ' myLibObj.Type = 2 ' Luis Feliz Tirado
            myLibObj.Scaling = 1
            lstBGL.Items.Add(myLibObj.Name)
            TempCategory.Add(myLibObj)
        Next

        lstBGL.SelectedIndex = 0

        CheckUI()
        ChangeIsOFF = False

        Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub TxtLibLength_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLibLength.TextChanged
        cmdUpdate.Enabled = True
    End Sub

    Private Sub TxtLibWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLibWidth.TextChanged
        cmdUpdate.Enabled = True
    End Sub

    Private Sub TxtLibScale_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLibScale.TextChanged
        cmdUpdate.Enabled = True
    End Sub

    Private Sub TxtLibName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLibName.TextChanged
        cmdUpdate.Enabled = True
    End Sub

End Class