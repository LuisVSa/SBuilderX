Option Strict Off
Option Explicit On

Imports System.Windows.Forms


Friend Class FrmLPPointsP

    Private _Altitude As Double

    Public Property Altitude() As Double
        Get
            Return _Altitude
        End Get
        Set(ByVal value As Double)
            _Altitude = value
        End Set
    End Property

    Private _Latitude As Double
    Public Property Latitude() As Double
        Get
            Return _Latitude
        End Get
        Set(ByVal value As Double)
            _Latitude = value
        End Set
    End Property

    Private _Longitude As Double
    Public Property Longitude() As Double
        Get
            Return _Longitude
        End Get
        Set(ByVal value As Double)
            _Longitude = value
        End Set
    End Property


    Private Sub FrmPointsP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lbWidth.Text = "Width"

        If POPType = "PtInLineX" Then
            Text = "SBuilderX - Point from Aircraft"
            txtWidth.Visible = True
            lbWidth.Visible = True
            lbPt.Visible = True
            lbAltitude.Visible = True
            txtAltitude.Visible = True
            txtLon.Text = Lon2Str(_Longitude)
            txtLat.Text = Lat2Str(_Latitude)
            txtAltitude.Text = CStr(_Altitude)
            txtWidth.Text = CStr(Lines(POPIndex).GLPoints(POPIndexPT).wid)
            lbPt.Text = "PT # " & CStr(POPIndexPT)
        End If

        If POPType = "PtInPolyX" Then
            Text = "SBuilderX - Point from Aircraft"
            txtWidth.Visible = False
            lbWidth.Visible = False
            lbPt.Visible = True
            lbAltitude.Visible = True
            txtAltitude.Visible = True
            txtLon.Text = Lon2Str(_Longitude)
            txtLat.Text = Lat2Str(_Latitude)
            txtAltitude.Text = CStr(_Altitude)
            lbPt.Text = "PT # " & CStr(POPIndexPT)
        End If


        If POPType = "PtInLine" Then
            Text = "SBuilderX - Point Properties"
            txtWidth.Visible = True
            If Mid(Lines(POPIndex).Type, 1, 3) = "OBJ" Then
                lbWidth.Text = "Heading"
            End If
            lbWidth.Visible = True
            lbPt.Visible = True
            lbAltitude.Visible = True
            txtAltitude.Visible = True
            txtLon.Text = Lon2Str(Lines(POPIndex).GLPoints(POPIndexPT).lon)
            txtLat.Text = Lat2Str(Lines(POPIndex).GLPoints(POPIndexPT).lat)
            txtAltitude.Text = CStr(Lines(POPIndex).GLPoints(POPIndexPT).alt)
            txtWidth.Text = CStr(Lines(POPIndex).GLPoints(POPIndexPT).wid)
            lbPt.Text = "PT # " & CStr(POPIndexPT)
        End If

        If POPType = "PtInPoly" Then
            Text = "SBuilderX - Point Properties"
            txtWidth.Visible = False
            lbWidth.Visible = False
            lbPt.Visible = True
            lbAltitude.Visible = True
            txtAltitude.Visible = True
            txtLon.Text = Lon2Str(Polys(POPIndex).GPoints(POPIndexPT).lon)
            txtLat.Text = Lat2Str(Polys(POPIndex).GPoints(POPIndexPT).lat)
            txtAltitude.Text = CStr(Polys(POPIndex).GPoints(POPIndexPT).alt)
            lbPt.Text = "PT # " & CStr(POPIndexPT)
        End If

        If POPType = "Goto" Then
            Text = "SBuilderX - Goto this position"
            txtWidth.Visible = False
            lbWidth.Visible = False
            lbPt.Visible = False
            txtLon.Text = Lon2Str(LonDispCenter)
            txtLat.Text = Lat2Str(LatDispCenter)
            lbAltitude.Visible = False
            txtAltitude.Visible = False
        End If

    End Sub

    Private Function ValidateEntries() As Boolean

        Dim X As Double

        'On Error GoTo erro1


        ValidateEntries = False

        X = Str2Lat(txtLat.Text)
        If X > 90 Then Exit Function
        If X < -90 Then Exit Function
        X = Str2Lon(txtLon.Text)

        If X > 180 Then Exit Function
        If X < -180 Then Exit Function

        If Not POPType = "Goto" Then X = Val(txtAltitude.Text)
        If POPType = "PtInLine" Then X = Val(txtWidth.Text)

        ValidateEntries = True
        Exit Function

erro1:

    End Function

    Private Sub CmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click

        Dispose()

    End Sub

    Private Sub CmdOK_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click

        If ValidateEntries() Then
            If POPType = "PtInLine" Or POPType = "PtInLineX" Then
                Lines(POPIndex).GLPoints(POPIndexPT).alt = Val(txtAltitude.Text)
                Lines(POPIndex).GLPoints(POPIndexPT).wid = Val(txtWidth.Text)
                Lines(POPIndex).GLPoints(POPIndexPT).lon = Str2Lon(txtLon.Text)
                Lines(POPIndex).GLPoints(POPIndexPT).lat = Str2Lat(txtLat.Text)
                AddLatLonToLine(POPIndex)
            End If
            If POPType = "PtInPoly" Or POPType = "PtInPolyX" Then
                Polys(POPIndex).GPoints(POPIndexPT).alt = Val(txtAltitude.Text)
                Polys(POPIndex).GPoints(POPIndexPT).lon = Str2Lon(txtLon.Text)
                Polys(POPIndex).GPoints(POPIndexPT).lat = Str2Lat(txtLat.Text)
                AddLatLonToPoly(POPIndex)
            End If

            If POPType = "Goto" Then
                LatDispCenter = Str2Lat(txtLat.Text)
                LonDispCenter = Str2Lon(txtLon.Text)
                SetDispCenter(0, 0)
                RebuildDisplay()
            End If
            RebuildDisplay()
            Dispose()
        Else
            MsgBox("Check your entries!", MsgBoxStyle.Critical)
        End If
    End Sub

End Class
