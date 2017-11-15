;ASDesign Compatible Macro
;Name=BlueLight, Type=Airport Objects, Bitmap=bluelight.jpg, \
;FixedLength=5, FixedWidth=5, \
;Latitude, Longitude,\
;Elevation=0, Visibility=10000, \


;   Parameters:
;   1 = latitude
;   2 = longitude
;   3 = elevation
;   4 = visibility


	PerspectiveCall( :layer@  )
	Jump(  :next@  )
:layer@
	Perspective

    mif( %3  )
	RefPoint(  ns  :ret@  %1 %2  v1= %4 E= %1 v2= 5  )
    melse
	RefPoint(  7  :ret@  1 %1 %2  v1= %4  v2= 5 )
    mifend

	SetScaleX( :ret@ 0 0 11 )
	RotateToAircraft( :start@  0 0 0 0 0 1 0 0 0 )
	Return

:start@

	Points( 0
	-3	0	0
	3	0	0
	3	12	0
	-3	12	0
	)

	LoadBitmap( 0 L6 EF 0 200 0 bluel.bmp )		       
	
	TexPoly( m 0 0 32767 0
	0    0		0
	1    255	0
	2    255	255
	3    0		255
	)

	IfVarAnd( :ret@ 28C 6 )
	RGBLColor( EF 0 0 255 )
	Dot( 0 10 1 )

:ret@
	Return

:next@

