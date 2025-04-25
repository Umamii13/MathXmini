Shader "Custom/TillingBG"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _ScrollSpeed ("Scroll Speed", Vector) = (0.1, 0.1, 0, 0)
        _Offset ("UV Offset", Vector) = (0, 0, 0, 0)
        _Tiling ("Tiling", Vector) = (1, 1, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
 
        CGPROGRAM
        #pragma surface surf Standard
        #include "UnityCG.cginc"
 
        sampler2D _MainTex;
        float4 _ScrollSpeed;
        float4 _Offset;
        float4 _Tiling;
 
        struct Input
        {
            float2 uv_MainTex;
        };
 
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 uv = IN.uv_MainTex;

            uv *= _Tiling.xy;

            uv += _Offset.xy + (_ScrollSpeed.xy * _Time.xy);

            uv = frac(uv);

            fixed4 color = tex2D(_MainTex, uv);

            o.Albedo = color.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
