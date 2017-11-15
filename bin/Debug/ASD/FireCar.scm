;ASDesign Compatible Macro
;Name=FireCar, Type=Airfield Objects, Bitmap=FireCar.jpg, \
;FixedLength=2, FixedWidth=5, \
;Latitude, Longitude, Rotation=0, Scale=1, Density=2, \
;Visibility=0, Range=8, Elevation=0, 


;   Parameters:
;   1 = latitude
;   2 = longitude
;   3 = rotation
;   4 = scale
;   5 = complexity
;   6 = visibility
;   7 = range
;   8 = elevation
 
Area( 5 %1 %2 %7 )
	IfVarRange( : 346  %5  5 )      ;check complexity
	PerspectiveCall( :pcall1 )
	PerspectiveCall( :pcall2 )
	Jump( : )

:pcall1
	Perspective

mif( %8 )
	RefPoint( abs :ret  %4  %1 %2  v1= %6  v2= 10 E= %8 )
melse
	RefPoint( rel :ret  %4  %1 %2  v1= %6  v2= 10 )
mifend

	RotatedCall( :start 0 0 %3 )
	Return

:start
	CallLibObj(0 1E115663 26730104 022E0DB1 C0F65F5C )
	Return

:pcall2
	Perspective

mif( %8 )
	RefPoint( abs :ret  [%4 * 0.01]  %1 %2  v1= %6  v2= 10 E= %8 )
melse
	RefPoint( rel :ret  [%4 * 0.01]   %1 %2  v1= %6  v2= 10 )
mifend

	RotatedCall( :start2 0 0 %3 )
	Return

:start2
	RotateToAircraft( :lanterna   80 0 180 0 0 1 0 0 0 )
	RotateToAircraft( :lanterna  -80 0 180 0 0 1 0 0 0 )
	Return
:lanterna
BGLVersion( 0800 )
TextureList( 0
    6 FF 255 255 255 0 50.000000 "luzazul.bmp"
    )
MaterialList( 0
    0.862745 0.862745 0.823529 1.000000   0.862745 0.862745 0.823529 1.000000   0.000000 0.000000 0.000000 1.000000   0.000000 0.000000 0.000000 1.000000 0.000000
    )
VertexList( 0
    -6.567812 8.271229 0.000000  0.000000 0.000000 1.000000  0.984375 1.000000
    6.567812 8.271229 0.000000  0.000000 0.000000 1.000000  0.000000 1.000000
    6.567812 -8.271229 0.000000  0.000000 0.000000 1.000000  0.000000 0.015625
    -6.567812 -8.271229 0.000000  0.000000 0.000000 1.000000  0.984375 0.015625
   )

Transform_Mat(
    0 236 0
    1.000000 0.000000 0.000000
    0.000000 1.000000 0.000000
    0.000000 0.000000 1.000000
    )
SetMaterial( 0 0 )
DrawTriList( 0
    0 1 2
    0 2 3
    )
TransformEnd
EndVersion


:ret
Return

EndA
  