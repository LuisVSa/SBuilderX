;ASDesign Compatible Macro
;Name=Tank_Fence, Type=Airfield Objects, Bitmap=Tank_Fence.jpg, \
;FixedLength=5, FixedWidth=5, \
;Latitude, Longitude, Rotation=0, \
;Visibility=15000, Elevation=0, Density=3, Scale=1, \

;
;   Macro Parameters:
;   1 = latitude
;   2 = longitude
;   3 = rotation
;   4 = visibility (in meters)
;   5 = elevation
;   6 = complexity
;   7 = scale

Area( 5 %1 %2 50 )

	IfVarRange( :next@ 346  %6  5 )  ;check complexity
	PerspectiveCall( :pcall@ )
	Jump( :next@ )

:pcall@
	Perspective

mif( %5  )
	RefPoint(  2  :ret@  %7  %1 %2  v1= %4   E= %5  v2= [20 + %5]   )
melse
	RefPoint(  7  :ret@  %7  %1 %2  v1= %4  v2= 20  )
mifend

	RotatedCall( :start@ 0 0 %3 )

	Return

:start@

	CallLibObj(0 1E11566C 26730104 022E0DB1 C0F65F5C )
:ret@
	Return

:next@

EndA