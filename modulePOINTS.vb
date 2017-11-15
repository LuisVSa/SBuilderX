Option Strict On
Option Explicit On
Module modulePOINTS
    Friend CheckPoints As Integer '0=no ; 1=manual; 2=auto
    Friend AutoCheckDistance As Single

    Friend SelectedPointColor As Color
    Friend UnselectedPointColor As Color

    <Serializable()> Friend Structure GPoint
        Dim lon As Double
        Dim lat As Double
        Dim alt As Double
        Dim Selected As Boolean
    End Structure

    <Serializable()> Friend Structure GLPoint
        Dim lon As Double
        Dim lat As Double
        Dim alt As Double
        Dim wid As Double
        Dim Selected As Boolean
    End Structure


    Friend Function IsPoint(ByVal X1 As Double, ByVal Y1 As Double, ByVal X As Double, ByVal Y As Double) As Boolean

        ' X1 and Y1 are lon / lat
        ' X and Y are pixels


        X1 = X1 * PixelsPerLonDeg
        Y1 = Y1 * PixelsPerLatDeg

        IsPoint = False
        If X > X1 + 5 Then Exit Function
        If X < X1 - 5 Then Exit Function
        If Y > Y1 + 5 Then Exit Function
        If Y < Y1 - 5 Then Exit Function
        IsPoint = True

    End Function


    Friend Function IsPointInSegment(ByVal X0 As Double, ByVal Y0 As Double, ByVal X1 As Double, ByVal Y1 As Double, ByVal X As Double, ByVal Y As Double) As Boolean
        ' on entry X Y contain distance from earth in pixels
        ' other parameters are lon and lat of line extremes

        X0 = X0 * PixelsPerLonDeg
        X1 = X1 * PixelsPerLonDeg

        Y0 = Y0 * PixelsPerLatDeg
        Y1 = Y1 * PixelsPerLatDeg

        IsPointInSegment = False

        On Error GoTo erro1

        If X > X0 + 5 And X > X1 + 5 Then Exit Function
        If X < X0 - 5 And X < X1 - 5 Then Exit Function
        If Y > Y0 + 5 And Y > Y1 + 5 Then Exit Function
        If Y < Y0 - 5 And Y < Y1 - 5 Then Exit Function

        X1 = X1 - X0
        Y1 = Y1 - Y0

        If System.Math.Abs(X1) > System.Math.Abs(Y1) Then
            X = Y0 + Y1 * (X - X0) / X1
            If X > Y + 5 Then Exit Function
            If X < Y - 5 Then Exit Function
        Else
            Y = X0 + X1 * (Y - Y0) / Y1
            If Y > X + 5 Then Exit Function
            If Y < X - 5 Then Exit Function
        End If

        IsPointInSegment = True

        Exit Function

erro1:

    End Function
End Module