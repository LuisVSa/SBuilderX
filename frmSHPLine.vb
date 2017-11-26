Option Strict Off
Option Explicit On

Imports System.Windows.Forms

Friend Class FrmSHPLine

    Private Sub FrmSHPLine_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Dim N As Integer

        If IsZ Then
            cmbAltitude.Items.Remove("From Shape file")
        End If

        For N = 1 To NoOfFields
            cmbName.Items.Remove(FieldNames(N - 1))
            cmbGUID.Items.Remove(FieldNames(N - 1))
            cmbWidth.Items.Remove(FieldNames(N - 1))
            cmbColor.Items.Remove(FieldNames(N - 1))
            cmbAltitude.Items.Remove(FieldNames(N - 1))
        Next

    End Sub

    Private Sub FrmSHPLine_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim N As Integer

        If IsZ Then
            cmbAltitude.Items.Add("From Shape file")
        End If

        For N = 1 To NoOfFields
            cmbName.Items.Add(FieldNames(N - 1))
            cmbGUID.Items.Add(FieldNames(N - 1))
            cmbWidth.Items.Add(FieldNames(N - 1))
            cmbColor.Items.Add(FieldNames(N - 1))
            cmbAltitude.Items.Add(FieldNames(N - 1))
        Next

        cmbName.SelectedIndex = 0
        For N = 0 To NoOfFields - 1
            If FieldNames(N).ToUpper = "NAME" Then
                cmbName.SelectedIndex = N + 1
                Exit For
            End If
        Next

        cmbGUID.SelectedIndex = 0
        For N = 0 To NoOfFields - 1
            If FieldNames(N).ToUpper = "GUID" Then
                cmbGUID.SelectedIndex = N + 1
                Exit For
            End If
        Next

        cmbWidth.SelectedIndex = 0
        For N = 0 To NoOfFields - 1
            If FieldNames(N).ToUpper = "WIDTH" Then
                cmbWidth.SelectedIndex = N + 1
                Exit For
            End If
        Next

        cmbColor.SelectedIndex = 0
        For N = 0 To NoOfFields - 1
            If FieldNames(N).ToUpper = "COLOR" Then
                cmbColor.SelectedIndex = N + 1
                Exit For
            End If
        Next

        If IsZ Then
            cmbAltitude.SelectedIndex = 1
            txtAltitude.Enabled = False
        Else
            cmbAltitude.SelectedIndex = 0
            For N = 0 To NoOfFields - 1
                If FieldNames(N).ToUpper = "ALTITUDE" Then
                    cmbAltitude.SelectedIndex = N + 2
                    Exit For
                End If
            Next
        End If

        txtGUID.Text = ShapeLineGuid
        txtAltitude.Text = ShapeLineAltitude
        txtWidth.Text = ShapeLineWidth
        lbColor.BackColor = ShapeLineColor


    End Sub


    Private Sub CmbAltitude_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAltitude.SelectedIndexChanged

        If cmbAltitude.SelectedIndex = 0 Then
            txtAltitude.Enabled = True
        Else
            txtAltitude.Enabled = False
        End If

    End Sub



    Private Sub CmbStartWidth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbWidth.SelectedIndexChanged

        If cmbWidth.SelectedIndex = 0 Then
            txtWidth.Enabled = True
        Else
            txtWidth.Enabled = False
        End If

    End Sub

    Private Sub CmbGUID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGUID.SelectedIndexChanged

        If cmbGUID.SelectedIndex = 0 Then
            txtGUID.Enabled = True
        Else
            txtGUID.Enabled = False
        End If

    End Sub

    Private Sub CmbColor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbColor.SelectedIndexChanged

        If cmbColor.SelectedIndex = 0 Then
            lbColor.Visible = True
        Else
            lbColor.Visible = False
        End If

    End Sub

    Private Sub CmbName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbName.SelectedIndexChanged

        If cmbName.SelectedIndex = 0 Then
            txtName.Enabled = True
        Else
            txtName.Enabled = False
        End If

    End Sub



    Private Sub LbColor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbColor.Click

        'frmStart.ColorDialog1.Color = lbColor.BackColor
        '' Update the color if the user clicks OK 
        'If frmStart.ColorDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
        '    lbColor.BackColor = frmStart.ColorDialog1.Color
        'End If

        ARGBColor = lbColor.BackColor
        If FrmTransparency.ShowDialog = Windows.Forms.DialogResult.OK Then
            lbColor.BackColor = ARGBColor
        End If

    End Sub

    Private Sub TxtGUID_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGUID.Click

        POPMode = "SHP"
        FrmLinesP.ShowDialog()
        txtGUID.Text = ShapeLineGuid

    End Sub

    Private Sub CmdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        ShapeLineGuid = txtGUID.Text
        ShapeLineName = txtName.Text
        ShapeLineAltitude = CDbl(txtAltitude.Text)
        ShapeLineWidth = CDbl(txtWidth.Text)
        ShapeLineColor = lbColor.BackColor

        ShapeLineGuidField = cmbGUID.SelectedIndex
        ShapeLineNameField = cmbName.SelectedIndex
        ShapeLineAltitudeField = cmbAltitude.SelectedIndex
        ShapeLineWidthField = cmbWidth.SelectedIndex
        ShapeLineColorField = cmbColor.SelectedIndex
        ShapeLineCancel = False

        'WriteShapesSettings()
        WriteSettings()

        Dispose()

    End Sub

    Private Sub CmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        ShapeLineCancel = True
        Dispose()

    End Sub

End Class
