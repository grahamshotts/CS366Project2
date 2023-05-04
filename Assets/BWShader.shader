Shader "Unlit/BWShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _MakeBW("MakeBW", Float) = 1
        _rChannel("rChannel", Float) = 1
        _gChannel("gChannel", Float) = 1
        _bChannel("bChannel", Float) = 1
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                // make fog work
                #pragma multi_compile_fog

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    UNITY_FOG_COORDS(1)
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float _MakeBW;
                float _rChannel;
                float _gChannel;
                float _bChannel;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    UNITY_TRANSFER_FOG(o,o.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // sample the texture
                    fixed4 col = tex2D(_MainTex, i.uv);

                    col.r = _rChannel;
                    col.g = _gChannel;
                    col.b = _bChannel;

                   /*if (_MakeBW == 0) {
                        if (col.r > 0.7 || col.g > 0.7 || col.b > 0.7) {
                            col.r = 1;
                            col.g = 1;
                            col.b = 1;
                        }
     else {
     col.r = 0;
      col.g = 0;
      col.b = 0;
  }
}
else {
 float total = col.r + col.g + col.b;
 total /= 3;
 col.r = total;
 col.g = total;
 col.b = total;
}*/
                   //Make it BW
                   /*float4 c;
                   c.r = c.g = 0;
                   c.b = 0;
                   float t = col.r;

                   if (col.r > 0.6 || col.g > 0.6 || col.b > 0.6){
                       c.r = 0.8;
                       c.g = 0.8;
                       c.b = 0.8;
                   }*/

                   // Invert
                   /*float4 c;
                   c.r = 1 - col.r;
                   c.g = 1 - col.g;
                   c.b = 1 - col.b;
                   c.a = 1;*/
                   //col = c;
                //}



                   UNITY_APPLY_FOG(i.fogCoord, col);
                   return col;
               }
               ENDCG
           }
        }
}
