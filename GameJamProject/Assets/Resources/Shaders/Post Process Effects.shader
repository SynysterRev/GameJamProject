Shader "Hidden/Retro Screen Effect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Pattern ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb *= max(0.65f,sin(i.uv.y*1000.0f));
                return col;
            }
            ENDCG
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _Pattern;
            fixed4 _Color;
            fixed4 _ColorBck;

            fixed4 frag (v2f i) : SV_Target
            {
                float aspect = _ScreenParams.x / _ScreenParams.y;
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 patternCol = tex2D(_Pattern, float2(i.uv.x*aspect,i.uv.y)*6.0f);
                float avg = max(0.0f,col.r-col.g-col.b);
                // just invert the colors
                col.rgb = lerp(col.rgb,_ColorBck,1.0-step(0.1,col.rgb));
                col.rgb = lerp(col.rgb, patternCol*_Color,avg);
                
                return col;
            }
            ENDCG
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _Distortion;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 distortion = tex2D(_Distortion, i.uv);
                fixed4 col = tex2D(_MainTex, i.uv.xy+distortion.xy*0.03f);
                return col;
            }
            ENDCG
        }
    }
}
