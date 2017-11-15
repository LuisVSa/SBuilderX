;ASDesign Compatible Macro
;Name=CargoShip1, Type=Ships, Bitmap=cargoship1.jpg, \
;FixedLength=20, FixedWidth=20, \
;Latitude, Longitude,Elevation=0, \
;Visibility=10000, Density=2, Rotation=0, \


;   Parameters:
;   1 = latitude
;   2 = longitude
;   3 = elevation
;   4 = visibility
;   5 = complexity
;   6 = rotation

Area( 5 %1 %2 50 )
 
	IfVarRange( :next@ 346  %5  5 )      ;check complexity
	PerspectiveCall( :pcall@ )
	Jump( :next@ )

:pcall@
	Perspective

mif( %3 )
	RefPoint( ns  :ret@  %1 %2  v1= %4  v2= 100 E= %3 )
	SetScaleX( :ret@ 0 0 10 )
melse
	RefPoint( rel  :ret@  1 %1 %2  v1= %4  v2= 100 )
	SetScaleX( :ret@ 0 0 10 )
mifend

	RotatedCall( :start@ 0 0 %6 )
	Return

:start@
	CallLibObj(0 5469BABA 267300F0 022E0DB1 C0F65F5C )
:ret@
	Return

:next@

EndA