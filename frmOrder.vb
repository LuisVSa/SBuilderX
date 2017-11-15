Option Strict Off
Option Explicit On
Friend Class FrmOrder

    Inherits System.Windows.Forms.Form
    Dim O() As String

    Private Sub CmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click

        ObjOrder = False
        Dispose()

    End Sub

    Private Sub CmdDOWN_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDOWN.Click

        Dim K, N As Integer

        N = lstObject.SelectedIndex + 1

        If N = 0 Then Exit Sub
        If N = NoOfObjects Then Exit Sub

        ' remove the list
        For K = NoOfObjects To 1 Step -1
            ' O(K) = VB6.GetItemString(lstObject, K - 1)
            O(K) = lstObject.GetItemText(K - 1)
            lstObject.Items.RemoveAt(K - 1)
        Next K

        ' and add in new order
        For K = 1 To N - 1
            lstObject.Items.Add(O(K))
        Next K

        lstObject.Items.Add(O(N + 1))

        lstObject.Items.Add(O(N))

        For K = N + 2 To NoOfObjects
            lstObject.Items.Add(O(K))
        Next K

        lstObject.SelectedIndex = N

        ObjOrder = True

    End Sub

    Private Sub CmdOK_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click

        Dim ObjectCopies() As Objecto
        Dim N, K As Integer
        Dim A As String

        ReDim ObjectCopies(NoOfObjects)

        ' ObjectCopies = VB6.CopyArray(Objects)
        Array.Copy(Objects, ObjectCopies, NoOfObjects)

        For N = 1 To NoOfObjects
            A = lstObject.GetItemText(N - 1)
            A = Mid(A, 58, 5)
            K = CInt(A)
            ' added after 205
            'If K = ObjPOPIndex Then ObjPOPIndex = N
            If K = POPIndex Then POPIndex = N

            Objects(N) = ObjectCopies(K)
        Next N

        Dispose()

    End Sub

    Private Sub CmdUp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUP.Click

        Dim N, K As Integer

        N = lstObject.SelectedIndex + 1

        If N = 0 Then Exit Sub
        If N = 1 Then Exit Sub

        ' remove the list
        For K = NoOfObjects To 1 Step -1
            ' O(K) = VB6.GetItemString(lstObject, K - 1)
            O(K) = lstObject.GetItemText(K - 1)
            lstObject.Items.RemoveAt(K - 1)
        Next K

        ' and add in new order
        For K = 1 To N - 2
            lstObject.Items.Add(O(K))
        Next K

        lstObject.Items.Add(O(N))
        lstObject.Items.Add(O(N - 1))

        For K = N + 1 To NoOfObjects
            lstObject.Items.Add(O(K))
        Next K

        lstObject.SelectedIndex = N - 2

        ObjOrder = True


    End Sub

    Private Sub FrmOrder_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim J, N, K, M As Integer
        Dim A, B As String

        'UPGRADE_WARNING: Lower bound of array O was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
        ReDim O(NoOfObjects)

        For N = 1 To NoOfObjects

            K = Objects(N).Type

            If K = 0 Then
                A = Objects(N).Description
                M = InStrRev(A, "|")
                B = Mid(A, M + 1)
                If B = "" Then
                    M = InStr(1, A, "|")
                    B = Mid(A, 1, M - 1)
                End If
                O(N) = "FSX Library Object  " & B
            End If

            If K = 1 Then
                A = Objects(N).Description
                M = InStrRev(A, "|")
                B = Mid(A, M + 1)
                If B = "" Then
                    M = InStr(1, A, "|")
                    B = Mid(A, 1, M - 1)
                End If
                O(N) = "FS8 Library Object  " & B
            End If

            If K = 2 Then
                A = Objects(N).Description
                M = InStrRev(A, "|")
                B = Mid(A, M + 1)
                If B = "" Then
                    M = InStr(1, A, "|")
                    B = Mid(A, 1, M - 1)
                End If
                O(N) = "FS9 Library Object  " & B
            End If

            If K = 3 Then
                A = Objects(N).Description
                M = InStrRev(A, "|")
                B = Mid(A, M + 1)
                If B = "" Then
                    M = InStr(1, A, "|")
                    B = Mid(A, 1, M - 1)
                End If
                O(N) = "Rwy12 Object        " & B
            End If

            If K = 4 Then
                A = Objects(N).Description
                M = InStrRev(A, "|")
                B = Mid(A, M + 1)
                If B = "" Then
                    M = InStr(1, A, "|")
                    B = Mid(A, 1, M - 1)
                End If
                O(N) = "API Macro Object    " & B
            End If

            If K = 5 Then
                A = Objects(N).Description
                M = InStrRev(A, "|")
                B = Mid(A, M + 1)
                If B = "" Then
                    M = InStr(1, A, "|")
                    B = Mid(A, 1, M - 1)
                End If
                O(N) = "ASD Macro Object    " & B
            End If

            If K = 8 Then
                A = Objects(N).Description
                M = InStrRev(A, "|")
                B = Mid(A, M + 1)
                If B = "" Then
                    M = Len(A)
                    A = Mid(A, 1, M - 1)
                    M = InStrRev(A, "|")
                    B = Mid(A, M + 1)
                End If
                O(N) = "Taxiway Sign        " & B
            End If

            If K = 16 Then
                A = Objects(N).Description
                M = InStrRev(A, "|")
                B = Mid(A, M + 1)
                If B = "" Then
                    M = InStr(1, A, "|")
                    B = Mid(A, 1, M - 1)
                End If
                O(N) = "Effect Object       " & B
            End If


            If K = 32 Then
                A = Objects(N).Description
                M = InStrRev(A, "|")
                B = Mid(A, M + 1)
                If B = "" Then
                    B = "Object # " & Trim(Str(N))
                End If
                O(N) = "Beacon Object       " & B
            End If

            If K = 64 Then
                A = Objects(N).Description
                M = InStrRev(A, "|")
                B = Mid(A, M + 1)
                If B = "" Then
                    B = "Object # " & Trim(Str(N))
                End If
                O(N) = "Windsock Object     " & B
            End If

            O(N) = Mid(O(N), 1, 57)
            K = Len(O(N))
            For J = K + 1 To 57
                O(N) = O(N) & " "
            Next J

            B = Format(N, "00000")
            O(N) = O(N) & B
            lstObject.Items.Add(O(N))

        Next N

    End Sub

End Class