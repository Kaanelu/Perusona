Shader "Custom/SH_Grid"
{
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _GridSize("Grid Size", Range(1, 10)) = 5
        _GridColor("Grid Color", Color) = (1, 1, 1, 1)
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 200

            CGPROGRAM
            #pragma surface surf Lambert

            sampler2D _MainTex;
            fixed4 _GridColor;
            int _GridSize;

            struct Input {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutput o) {
                fixed2 tileUV = IN.uv_MainTex * _GridSize;
                fixed2 gridUV = frac(tileUV);
                fixed4 gridColor = _GridColor;
                fixed4 mainTex = tex2D(_MainTex, IN.uv_MainTex);

                if (gridUV.x < 0.02 || gridUV.y < 0.02) {
                    o.Albedo = gridColor.rgb;
                }
     else {
      o.Albedo = mainTex.rgb;
  }
  o.Alpha = mainTex.a;
}
ENDCG
        }
            FallBack "Diffuse"
}
