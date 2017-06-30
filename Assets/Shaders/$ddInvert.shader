//ddInvert shader: Daniel DeEntremont
//Apply this shader to a mesh and watch all pixels behind the mesh become inverted!
Shader "ddShaders/ddInvert" {
    Properties
        {
            _Color ("Tint Color", Color) = (1,1,1,1)
        }
       
        SubShader
        {
            Tags { "Queue"="Transparent" }
     
            Pass
            {
               ZWrite On
               ColorMask 0
            }
			Pass
			{
				Blend OneMinusDstColor OneMinusSrcAlpha //invert blending, so long as FG color is 1,1,1,1
				BlendOp Add
				SetTexture [_Color] 
				{
					constantColor [_Color]
					combine constant
				}
			}
			
         }//end subshader
}//end shader