Option Strict Off
Option Explicit On
Friend Class FrmExtraMacro
    Inherits System.Windows.Forms.Form


    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click

        If lbP9.Visible Then MacroP9Value = txtP9.Text
        If lbPA.Visible Then MacroPAValue = txtPA.Text
        If lbPB.Visible Then MacroPBValue = txtPB.Text
        If lbPC.Visible Then MacroPCValue = txtPC.Text
        If lbPD.Visible Then MacroPDValue = txtPD.Text

        Dispose()

    End Sub

    Private Sub FrmExtraMacro_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        If MacroP9Name <> "" Then
            lbP9.Visible = True
            txtP9.Visible = True
            lbP9.Text = MacroP9Name
            txtP9.Text = MacroP9Value
        Else
            Exit Sub
        End If

        If MacroPAName <> "" Then
            lbPA.Visible = True
            txtPA.Visible = True
            lbPA.Text = MacroPAName
            txtPA.Text = MacroPAValue
        Else
            Exit Sub
        End If

        If MacroPBName <> "" Then
            lbPB.Visible = True
            txtPB.Visible = True
            lbPB.Text = MacroPBName
            txtPB.Text = MacroPBValue
        Else
            Exit Sub
        End If

        If MacroPCName <> "" Then
            lbPC.Visible = True
            txtPC.Visible = True
            lbPC.Text = MacroPCName
            txtPC.Text = MacroPCValue
        Else
            Exit Sub
        End If

        If MacroPDName <> "" Then
            lbPD.Visible = True
            txtPD.Visible = True
            lbPD.Text = MacroPDName
            txtPD.Text = MacroPDValue
        Else
            Exit Sub
        End If


    End Sub
End Class