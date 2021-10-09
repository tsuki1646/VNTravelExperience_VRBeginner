Shader "MyGame/Shader_ArtofCode"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Saturation ("Saturation", Range(0,4)) = 0.0

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;        
        half _Glossiness; //half, float, fixed
        half _Metallic;
        fixed4 _Color;    //vector => rgba

        struct Input
        {
            float2 uv_MainTex;
        };

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            float2 uv = IN.uv_MainTex;
            //color is between 0 & 1
            uv.y += sin(uv.x * 6.2831 +_Time.y) * .1;
            //fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

            fixed4 c = tex2D (_MainTex, uv) * _Color;

            //o.Albedo = c.rgb;
            //o.Albedo = float3(1,1,0) * float3(1,0,0);
            //o.Albedo = float3(1,1,0) + float3(1,0,0);

            //black & white
            //o.Albedo = c.b;

            //black & white (light)
            //o.Albedo = (c.r + c.b + c.g)/3;
            //o.Albedo = lerp((c.r + c.b + c.g)/3, c, 1);
            //o.Albedo = lerp((c.r + c.b + c.g)/3, c, _Saturation);

            //black & white gradient
            //o.Albedo =uv.y;
            //o.Albedo =uv.x;

            //float saturation = uv.x * _Saturation;
            //1 dai blue o giua
            float saturation = sin(uv.x * 6.2831) *.5 +.5;
            o.Albedo = lerp((c.r + c.b + c.g)/3, c, saturation);

            //o.Albedo = 1- c;

            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
