Namespace My

    ' The following events are availble for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.

    Partial Friend Class MyApplication

        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException

            Dim errorMessage As String = e.Exception.Message & vbCrLf & vbCrLf
            'errorMessage += e.Exception.ToString & vbCrLf & vbCrLf
            errorMessage += "This was an unexpected error that can lead to unpredictable results. If " & vbCrLf
            errorMessage += "you press NO SBuilderX will shut down. If you press YES you can continue " & vbCrLf
            errorMessage += "to work at your own risk. Do you want to continue?"

            Dim errorFile As String

            If MessageBox.Show(errorMessage, "SBuilderX - Globally Exception:", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                e.ExitApplication = False
            Else
                e.ExitApplication = True
                If WorkFile = "" Then
                    errorFile = AppPath & "\Tools\Work\PROJECT_ERR.SBP"
                Else
                    errorFile = Path.GetDirectoryName(WorkFile)
                    errorFile = errorFile & "\" & Path.GetFileNameWithoutExtension(WorkFile) & "_ERR.SBP"
                End If
                SaveFile(errorFile)
            End If

            errorMessage = "Error Report created by SBuilderX on " & Now.ToString & vbCrLf & vbCrLf
            errorMessage += e.Exception.Message & vbCrLf & vbCrLf
            errorMessage += e.Exception.ToString & vbCrLf & vbCrLf
            errorMessage += "Name=" & ProjectName.ToString & vbCrLf
            errorMessage += "NoOfMaps=" & NoOfMaps.ToString & vbCrLf
            errorMessage += "NoOfLands=" & NoOfLands.ToString & vbCrLf
            errorMessage += "NoOfLines=" & NoOfLines.ToString & vbCrLf
            errorMessage += "NoOfPolys=" & NoOfPolys.ToString & vbCrLf
            errorMessage += "NoOfWaters=" & NoOfWaters.ToString & vbCrLf
            errorMessage += "NoOfObjects=" & NoOfObjects.ToString & vbCrLf
            errorMessage += "NoOfExcludes=" & NoOfExcludes.ToString & vbCrLf
            errorMessage += "NoOfLWCIs=" & NoOfLWCIs.ToString & vbCrLf
            errorMessage += "BGLProjectFolder=" & BGLProjectFolder.ToString & vbCrLf
            errorMessage += "LatDispCenter=" & LatDispCenter.ToString & vbCrLf
            errorMessage += "LonDispCenter=" & LonDispCenter.ToString & vbCrLf
            errorMessage += "Zoom=" & Zoom.ToString & vbCrLf
            errorMessage += "PolyON=" & PolyON.ToString & vbCrLf
            errorMessage += "PolyVIEW=" & PolyVIEW.ToString & vbCrLf
            errorMessage += "LineON=" & LineON.ToString & vbCrLf
            errorMessage += "LineVIEW=" & LineVIEW.ToString & vbCrLf
            errorMessage += "MapVIEW=" & MapVIEW.ToString & vbCrLf
            errorMessage += "TilesToCome=" & TilesToCome.ToString & vbCrLf
            errorMessage += "NoOfServerTypes=" & NoOfServerTypes.ToString & vbCrLf
            errorMessage += "TileVIEW=" & TileVIEW.ToString & vbCrLf
            errorMessage += "ActiveTileFolder=" & ActiveTileFolder.ToString & vbCrLf
            errorMessage += "WaterVIEW=" & WaterVIEW.ToString & vbCrLf
            errorMessage += "WaterON=" & WaterON.ToString & vbCrLf
            errorMessage += "LandVIEW=" & LandVIEW.ToString & vbCrLf
            errorMessage += "LandON=" & LandON.ToString & vbCrLf
            errorMessage += "ObjectON=" & ObjectON.ToString & vbCrLf
            errorMessage += "ObjectVIEW=" & ObjectVIEW.ToString & vbCrLf
            errorMessage += "NoOfRwy12Categories=" & NoOfRwy12Categories.ToString & vbCrLf
            errorMessage += "NoOfLibCategories=" & NoOfLibCategories.ToString & vbCrLf
            errorMessage += "LibObjectsIsOn=" & LibObjectsIsOn.ToString & vbCrLf
            errorMessage += "NoOfGenBObjects=" & NoOfGenBObjects.ToString & vbCrLf
            errorMessage += "NoOfMacroCategories=" & NoOfMacroCategories.ToString & vbCrLf
            errorMessage += "MacroAPIIsOn=" & MacroAPIIsOn.ToString & vbCrLf
            errorMessage += "MacroASDIsOn=" & MacroASDIsOn.ToString & vbCrLf
            errorMessage += "MakeOnMany=" & MakeOnMany.ToString & vbCrLf
            errorMessage += "AllVIEW=" & AllVIEW.ToString & vbCrLf
            errorMessage += "ViewON=" & ViewON.ToString & vbCrLf
            errorMessage += "AircraftVIEW=" & AircraftVIEW.ToString & vbCrLf
            errorMessage += "PointerON=" & PointerON.ToString & vbCrLf
            errorMessage += "ZoomON=" & ZoomON.ToString & vbCrLf
            errorMessage += "PanON=" & PanON.ToString & vbCrLf
            errorMessage += "SelectON=" & SelectON.ToString & vbCrLf
            errorMessage += "MoveON=" & MoveON.ToString & vbCrLf
            errorMessage += "FirstMOVE=" & FirstMOVE.ToString & vbCrLf
            errorMessage += "InsertON=" & InsertON.ToString & vbCrLf
            errorMessage += "DeleteON=" & DeleteON.ToString & vbCrLf
            errorMessage += "AskDelete=" & AskDelete.ToString & vbCrLf
            errorMessage += "Dirty=" & Dirty.ToString & vbCrLf
            errorMessage += "DecimalDegrees=" & DecimalDegrees.ToString & vbCrLf
            errorMessage += "LatDispNorth=" & LatDispNorth.ToString & vbCrLf
            errorMessage += "LatDispSouth=" & LatDispSouth.ToString & vbCrLf
            errorMessage += "LonDispWest=" & LonDispWest.ToString & vbCrLf
            errorMessage += "LonDispEast=" & LonDispEast.ToString & vbCrLf

            errorFile = AppPath & "\Tools\Work\ERROR_REPORT.TXT"
            My.Computer.FileSystem.WriteAllText(errorFile, errorMessage, False)

        End Sub

    End Class

End Namespace

