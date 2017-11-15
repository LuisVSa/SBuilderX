;ASDesign Compatible Macro
;Name=FuelTotal, Type=Airport Vehicles, Bitmap=FuelTotal.jpg, \
;FixedLength=2, FixedWidth=5, \
;Latitude, Longitude, Rotation=0, Scale=1, Density=2, \
;Visibility=0, Elevation=0, Shadow=0, \

   Macro Parameters:

;   1 = latitude
;   2 = longitude
;   3 = rotation
;   4 = scale
;   5 = scenery complexity ( 0 ... 5 )
;   6 = visibility (in meters)
;   7 = elevation
;   8 = shadow


Area( 5 %1 %2 30 )

	IfVarRange( :next@ 346  %5  5 )  ;check complexity

mif( %8 )
	ShadowCall( :scall@ )
mifend

	PerspectiveCall( :pcall@ )
	Jump( :next@ )

:pcall@

	Perspective

:scall@

mif( %7 )
	RefPoint(  ns  :ret@  %1 %2  v1= %6  E= %7  v2= [50 + %7]   )
	SetScale( :ret@ 0 0 [0.025 * %4] )

melse
	RefPoint(  rel  :ret@  1  %1 %2  v1= %6  v2= 50   )
	SetScale( :ret@ 0 0 [0.025 * %4] )
mifend

mif( %3 )
	RotatedCall( :start@ 0 0 %3 )
melse
	Call( :start@ )
mifend
	Return

:start@
	CallLibObj(0 1E115661 26730104 022E0DB1 C0F65F5C )
:ret@
	Return

:next@

EndA

