// 2023-12-02 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

Shader "Custom/Skybox3D" {
// 2023-12-02 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

Properties {
    _MainTex ("Panorama", 2D) = "white" {}
    _DepthTex ("Depth Map", 2D) = "gray" {}
    _Exposure ("Exposure", Range(0, 3)) = 1
}

    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
#include "UnityCG.cginc"

struct appdata
{
    float4 vertex : POSITION;
};

struct v2f
{
    float3 worldDir : TEXCOORD0;
    float4 vertex : SV_POSITION;
};

sampler2D _MainTex;
sampler2D _DepthTex;
uniform float _Exposure;

v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.worldDir = normalize(v.vertex.xyz);
    return o;
}


// 2023-12-02 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

fixed4 frag(v2f i) : SV_Target
{
    float2 uv = float2(atan2(i.worldDir.x, i.worldDir.z) / (2.0 * UNITY_PI) + 0.5, acos(i.worldDir.y) / UNITY_PI);
    half3 panorama = tex2D(_MainTex, uv).rgb;
    half depth = tex2D(_DepthTex, uv).r;
    
    float3 viewDirection = normalize(i.worldDir);
    float3 displacedPos = viewDirection * depth;
    
    float2 displacedUV = float2(atan2(displacedPos.x, displacedPos.z) / (2.0 * UNITY_PI) + 0.5, acos(displacedPos.y) / UNITY_PI);
    
    half3 finalColor = tex2D(_MainTex, displacedUV).rgb;

    // Apply exposure
    finalColor *= _Exposure;

    return fixed4(finalColor, 1.0);
}

            ENDCG
        }
    }
}
