Option Strict Off
Option Explicit On

Imports System.Windows.Forms

Friend Class FrmTerrainExclude

    Private Guid As String
    Private Index As Integer
    Private Init As Boolean = True

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Dim name As String
        Dim LT As Integer
        LT = List.SelectedIndex + 1
        ' name = VB6.GetItemString(List, LT - 1)
        name = List.GetItemText(LT - 1)

        GetGuidAndIndex(name)
        ParticularExcludeGUID = Guid

        DialogResult = System.Windows.Forms.DialogResult.OK
        Dispose()

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click

        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Dispose()

    End Sub

    Private Sub CmdDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDetail.Click

        Dim name As String
        Dim LT As Integer
        LT = List.SelectedIndex + 1

        ' name = VB6.GetItemString(List, LT - 1)
        name = List.GetItemText(LT - 1)
        GetGuidAndIndex(name)

        If Index < 0 Then
            MsgBox("This type is not described in Terrain.cfg!")
            Exit Sub
        End If

        If Not IsFSX Then
            MsgBox("Terrain.cfg could not be found!")
            Exit Sub
        End If

        Dim TerrainFile, A, B, Key As String
        Dim N, Marker As Integer
        Dim F1 As Boolean

        TerrainFile = FSPath & "Terrain.cfg"
        Key = "[Texture." & Trim(Index) & "]"

        FileOpen(2, TerrainFile, OpenMode.Input)
        N = LOF(2)
        Marker = 0
        F1 = False
        B = ""
        Do While Marker < N
            A = LineInput(2)
            Marker = Marker + Len(A) + 2
            A = Trim(A)
            If F1 Then
                If A = "" Then Exit Do
                B = B & A & vbCrLf
            End If
            If Not F1 Then
                If A = Key Then F1 = True
            End If
        Loop
        MsgBox(B, MsgBoxStyle.Information, "Description from Terrain.cfg")
        FileClose()

    End Sub


    Private Sub FrmTerrainExclude_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        List.Items.Clear()

        Dim N, K, LT As Integer

        LT = 1
        K = 0
        For N = PolyInit To NoOfPolyTypes
            K = K + 1
            List.Items.Add(PolyTypes(N).Name)
            If PolyTypes(N).Guid = ParticularExcludeGUID Then LT = K
        Next
        For N = LineInit To NoOfLineTypes
            K = K + 1
            List.Items.Add(LineTypes(N).Name)
            If LineTypes(N).Guid = ParticularExcludeGUID Then LT = K
        Next

        List.SelectedIndex = LT - 1
        Init = False

    End Sub

    Private Sub List_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles List.SelectedIndexChanged

        If Init Then Exit Sub
        Dim LT As Integer
        LT = List.SelectedIndex + 1
        ' lbTex.Text = VB6.GetItemString(List, LT - 1)
        lbTex.Text = List.GetItemText(LT - 1)


    End Sub
    Private Sub GetGuidAndIndex(ByVal name As String)

        Dim N As Integer

        Guid = "{00000000-0000-0000-0000-000000000000}"
        Index = -1

        For N = PolyInit To NoOfPolyTypes
            If PolyTypes(N).Name = name Then
                Guid = PolyTypes(N).Guid
                Index = PolyTypes(N).TerrainIndex
                Exit Sub
            End If
        Next

        For N = LineInit To NoOfLineTypes
            If LineTypes(N).Name = name Then
                Guid = LineTypes(N).Guid
                Index = LineTypes(N).TerrainIndex
                Exit Sub
            End If
        Next

    End Sub

End Class



