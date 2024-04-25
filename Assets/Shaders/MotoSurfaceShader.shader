Shader "Custom/VespaSurfaceShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("Mormal Map", 2D) = "bump"{}
        _MetallicMap("Metallic Map", 2D) = "white" {}
        _SpecularMap("Specular Map", 2D) = "white"{}
        
        _Glossiness ("Specular Level", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _MetallicMap;
        sampler2D _SpecularMap;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb*c.a + _Color * (1-c.a);
            o.Metallic = tex2D(_MetallicMap, IN.uv_MainTex) * _Metallic;
            o.Smoothness = tex2D(_SpecularMap, IN.uv_MainTex) * _Glossiness;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
        }
        ENDCG
    }
    FallBack "Diffuse"
}
