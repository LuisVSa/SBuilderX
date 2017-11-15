Option Strict Off
Option Explicit On
Friend Class FrmExcludesP
    Inherits System.Windows.Forms.Form
    Private ThisExc As Integer
    Private FlagExc As Integer
    Friend Sub AddNewExclude(ByVal X As Integer, ByVal Y As Integer)

        Dim X0, Y0, X1, Y1 As Double

        X0 = LonDispWest + AuxXInt / PixelsPerLonDeg
        Y0 = LatDispNorth - AuxYInt / PixelsPerLatDeg
        X1 = LonDispWest + X / PixelsPerLonDeg
        Y1 = LatDispNorth - Y / PixelsPerLatDeg

        If X0 > X1 Then
            X1 = X0 + X1
            X0 = X1 - X0
            X1 = X1 - X0
        End If

        If Y1 > Y0 Then
            Y1 = Y0 + Y1
            Y0 = Y1 - Y0
            Y1 = Y1 - Y0
        End If

        txtNorth.Text = Lat2Str(Y0)
        txtSouth.Text = Lat2Str(Y1)
        txtWest.Text = Lon2Str(X0)
        txtEast.Text = Lon2Str(X1)

        ShowDialog()

    End Sub
    Private Sub CmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click

        Dispose()

    End Sub

    Private Sub CmdOK_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click

        If POPIndex > NoOfExcludes Then
            BackUp()
            NoOfExcludes = NoOfExcludes + 1
            ReDim Preserve Excludes(NoOfExcludes)
            ThisExc = NoOfExcludes
        Else
            ThisExc = POPIndex
        End If

        Excludes(ThisExc).NLAT = Str2Lat(txtNorth.Text)
        Excludes(ThisExc).SLAT = Str2Lat(txtSouth.Text)
        Excludes(ThisExc).WLON = Str2Lon(txtWest.Text)
        Excludes(ThisExc).ELON = Str2Lon(txtEast.Text)

        FlagExc = 1 * ckAll.CheckState + 2 * ckBeacons.CheckState + 4 * ckEffects.CheckState + 8 * ckGenBuilds.CheckState
        FlagExc = FlagExc + 16 * ckLibrary.CheckState + 32 * ckTaxi.CheckState + 64 * ckTrigger.CheckState + 128 * ckWind.CheckState
        ' added in FSX
        FlagExc = FlagExc + 256 * ckBridges.CheckState

        Excludes(ThisExc).Flag = FlagExc

        RebuildDisplay()

        Dispose()

    End Sub
    Private Sub FrmExcludesP_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim N, ThisExc As Integer

        If POPIndex > NoOfExcludes Then Exit Sub

        ThisExc = POPIndex

        txtNorth.Text = Lat2Str(Excludes(ThisExc).NLAT)
        txtSouth.Text = Lat2Str(Excludes(ThisExc).SLAT)
        txtWest.Text = Lon2Str(Excludes(ThisExc).WLON)
        txtEast.Text = Lon2Str(Excludes(ThisExc).ELON)

        ckAll.CheckState = System.Windows.Forms.CheckState.Unchecked
        ckBeacons.CheckState = System.Windows.Forms.CheckState.Unchecked
        ckEffects.CheckState = System.Windows.Forms.CheckState.Unchecked
        ckGenBuilds.CheckState = System.Windows.Forms.CheckState.Unchecked
        ckLibrary.CheckState = System.Windows.Forms.CheckState.Unchecked
        ckTaxi.CheckState = System.Windows.Forms.CheckState.Unchecked
        ckTrigger.CheckState = System.Windows.Forms.CheckState.Unchecked
        ckWind.CheckState = System.Windows.Forms.CheckState.Unchecked
        ckBridges.CheckState = System.Windows.Forms.CheckState.Unchecked

        N = Excludes(ThisExc).Flag

        If N And 1 Then ckAll.CheckState = System.Windows.Forms.CheckState.Checked
        If N And 2 Then ckBeacons.CheckState = System.Windows.Forms.CheckState.Checked
        If N And 4 Then ckEffects.CheckState = System.Windows.Forms.CheckState.Checked
        If N And 8 Then ckGenBuilds.CheckState = System.Windows.Forms.CheckState.Checked
        If N And 16 Then ckLibrary.CheckState = System.Windows.Forms.CheckState.Checked
        If N And 32 Then ckTaxi.CheckState = System.Windows.Forms.CheckState.Checked
        If N And 64 Then ckTrigger.CheckState = System.Windows.Forms.CheckState.Checked
        If N And 128 Then ckWind.CheckState = System.Windows.Forms.CheckState.Checked
        If N And 256 Then ckBridges.CheckState = System.Windows.Forms.CheckState.Checked

    End Sub

End Class