Shader "MyGame/Shader_Unlit_ArtOfCode"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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

            //vertext shader
            v2f vert (appdata v)
            {
                v2f o;
                //v.vertex.y += sin(v.vertext.x + _Time.y) *.3;
                o.vertex = UnityObjectToClipPos(v.vertex);

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                //o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            //pixel shader //or fragment shader
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                float2 uv = i.uv - .5;
                float a = _Time.y;
                float2 p = float2(sin(a), cos(a)) *.4;
                float2 distort = uv - p; 
                float d = length(distort);
                float m = smoothstep(.07, .0, d);
                distort = distort * 20 * m;

                fixed4 col = tex2D(_MainTex, i.uv + distort );

                //return m;

                return col;
                //return float4(1,0,0,1);
            }
            ENDCG
        }
    }
}
