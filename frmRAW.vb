Imports System.Windows.Forms



Public Class FrmRAW

    Private _K As Integer
    Friend ReadOnly Property K() As Integer
        Get
            Return _K
        End Get
    End Property

    Private _J As Integer
    Friend ReadOnly Property J() As Integer
        Get
            Return _J
        End Get
    End Property

    Private _C As String  ' LC_ is land WC_ is water
    Friend ReadOnly Property C() As String
        Get
            Return _C
        End Get
    End Property


    Private Sub CmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        DialogResult = System.Windows.Forms.DialogResult.Cancel

    End Sub

    Private Sub CmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        If optWater.Checked Then
            _C = "WC_"
        Else
            _C = "LC_"
        End If

        Try
            _J = CInt(txtJ.Text)
        Catch ex As Exception
            _J = -1
            'txtJ.Text = "-1"
        End Try
        Try
            _K = CInt(txtK.Text)
        Catch ex As Exception
            _K = -1
            'txtK.Text = "-1"
        End Try


        DialogResult = System.Windows.Forms.DialogResult.OK

    End Sub

    Private Sub OptLand_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optLand.CheckedChanged

        'If optLand.Checked Then
        '    _C = "LC_"
        'Else
        '    _C = "WC_"
        'End If

    End Sub

    Private Sub OptWater_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optWater.CheckedChanged

        'If optWater.Checked Then
        '    _C = "WC_"
        'Else
        '    _C = "LC_"
        'End If

    End Sub

    Private Sub TxtJ_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtJ.TextChanged

        'Try
        '    _J = CInt(txtJ.Text)
        'Catch ex As Exception
        '    _J = -1
        '    txtJ.Text = "-1"
        'End Try

    End Sub

    Private Sub TxtK_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtK.TextChanged

        'Try
        '    _K = CInt(txtK.Text)
        'Catch ex As Exception
        '    _K = -1
        '    txtK.Text = "-1"
        'End Try

    End Sub

End Class
