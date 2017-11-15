;ASDesign Compatible Macro
;Name=Cancel, Type=General Objects, Bitmap=cancel.jpg, \
;FixedLength=5, FixedWidth=5, \
;Latitude, Longitude, Rotation=0, \
;Shadow=1, Visibility=10000, Elevation=0, \
;Density=2, Scale=0.02, \ 

;   %1  Latitude
;   %2  Longitude
;   %3  Heading
;   %4  Shadow
;   %5  V1 value
;   %6  Elevation
;   %7  Scenery complexity
;   %8  Scale

	IfVarRange( :next@ 346  %7  5 )  ;check complexity

mif( %4 )
	ShadowCall( :scall@ )
mifend

	PerspectiveCall2( :pcall@ )
	Jump( :next@ )

:pcall@

	Perspective

:scall@

mif( %6 )
	RefPoint(  abs  :ret@  %8 %1 %2  v1= %5  E= %6  v2= 50   )
melse
	RefPoint(  rel  :ret@  %8 %1 %2  v1= %5  v2= 50   )
mifend

mif( %3 )
	RotatedCall( :start@ 0 0 %3 )
melse
	Call( :start@ )
mifend
	Return

:start@
	CallLibObj(0 5469BAB7 267300F0 022E0DB1 C0F65F5C )
:ret@
	Return

:next@

