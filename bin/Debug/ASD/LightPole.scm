;ASDesign Compatible Macro
;Name=LightPole, Type=Airport Objects, Bitmap=LightPole.jpg, \
;FixedLength=5, FixedWidth=5, \
;Latitude, Longitude, Rotation=0, \
;Visibility=2000, Elevation=0, Density=3, \

;   Macro to add a ilumination post

;   Author: Luis Vieira de Sa <luis@it.uc.pt>
;   http://www.it.uc.pt/~luis/fltsim.html
;
;   Macro Parameters:
;   1 = latitude
;   2 = longitude
;   3 = rotation
;   4 = visibility (in meters)
;   5 = elevation
;   6 = complexity

	IfVarRange( :next@ 346  %6  5 )  ;check complexity
	PerspectiveCall( :pcall@ )
	Jump( :next@ )

:pcall@
	Perspective

mif( %5  )
	RefPoint(  2  :ret@  0.1  %1 %2  v1= %4   E= %5  v2= 20   )
melse
	RefPoint(  7  :ret@  0.1  %1 %2  v1= %4  v2= 20  )
mifend

	RotatedCall( :start@ 0 0 %3 )
	Return


:start@

	CallLibObj(0 5469BABC 267300F0 022E0DB1 C0F65F5C )
:ret@
	Return

:next@

