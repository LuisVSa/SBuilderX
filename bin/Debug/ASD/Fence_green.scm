;ASDesign Compatible Macro
;Name=Fence_green, Type=General Objects, Bitmap=fence_green.jpg, \
;Latitude, Longitude, Range=30, \
;Density=2, Visibility=10000, Elevation=0, Rotation=0, \
;Scale=1, Width=2, Height=2, Length=20, Texture=fence_green.bmp, \

;   Fence using selectable texture

;   Author: Luis Vieira de Sa 
;   Texture: Luis Feliz-Tirado
;
;   Macro Parameters:
;   1 = latitude
;   2 = longitude
;   3 = range
;   4 = scenery complexity ( 0 ... 5 )
;   5 = visibility (in meters)
;   6 = elevation
;   7 = rotation
;   8 = scale
;   9 = width  (not used, except for footprint)
;  10 = height
;  11 = length
;  12 = texture


UVar( $V2 int[%11] )

UVar( $H0 0 )    ; ground
UVar( $H1 %10 )	 ; height	


UVar( $WN [ -0.5 * %11 ] )   ; negative
UVar( $WP [  0.5 * %11 ] )   ; positive

; each section of the fence will have a 2:1 ratio (width:height)
Uvar( $Tiling int[ (%11 / %10 / 2) + 0.5 ] )

; now form the Texture Mapping value
UVar( $T [$Tiling] )

; add 5 pixels to repeat the post at the right end
UVar( $T [$T + (5 / 256)] )


Area( 5  %1 %2  %3 )
IfVarRange( : 346 %4 5 )
PerspectiveCall( :pcall )
Jump( : )
:pcall
mif( %6 )
	RefPoint(  abs  :return  %8 %1 %2  v1= %5  E= %6  v2= [$V2]   )
melse
	RefPoint(  rel  :return  %8 %1 %2  v1= %5  v2= [$V2]  )
mifend
RotatedCall( :start 0 0 [%7 + 90] )
:return
Return
:start
BGLVersion( 0800 )
TextureList( 0
    6 FF 255 255 255 0 50.0 "%12"
    )
MaterialList( 0
    0.75 0.75 0.75 1.00  0.25 0.25 0.25 1.00   0 0 0 1.00   0 0 0 1.00 0 )
VertexList( 0
    [$WN] [$H1] 0  0 0 -1  0 1
    [$WN] [$H0] 0  0 0 -1  0 0
    [$WP] [$H0] 0  0 0 -1  [$T] 0
    [$WP] [$H1] 0  0 0 -1  [$T] 1
    [$WP] [$H1] 0  0 0  1  [$T] 1
    [$WP] [$H0] 0  0 0  1  [$T] 0
    [$WN] [$H0] 0  0 0  1  0 0
    [$WN] [$H1] 0  0 0  1  0 1
   )
SetMaterial( 0 0 )
DrawTriList( 0
    0 1 2
    0 2 3
    4 5 6
    4 6 7
    )
EndVersion
Return
EndA
  