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
        //0
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
        //1
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
          //  sampler2D _Pattern;
            fixed4 _Color;
            fixed4 _ColorBck;

            fixed4 frag (v2f i) : SV_Target
            {
               // float aspect = _ScreenParams.x / _ScreenParams.y;
                fixed4 col = tex2D(_MainTex, i.uv);
              //  fixed4 patternCol = tex2D(_Pattern, float2(i.uv.x*aspect,i.uv.y)*6.0f);
                float avg = max(0.0f,col.r-col.g-col.b);
                // just invert the colors
                col.rgb = lerp(col.rgb,_ColorBck,1.0-step(0.1,col.rgb));
                col.rgb = lerp(col.rgb, _Color,avg);
                
                return col;
            }
            ENDCG
        }
        //2
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
        //3
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
                float4 screenPos : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            sampler2D _MainTex;
            float _Radius;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
              
                col = lerp(col, 1-col,i.uv.y - 1.0f + 2.0f*_Radius);
                return col;
            }
            ENDCG
        }
        //4
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
                float4 screenPos : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            float2 tile(float2 _st, float _zoom){
                _st *= _zoom;
                return frac(_st);
            }

            float box(float2 _st, float2 _size, float _smoothEdges){
                _size = float2(0.5,0.5) -_size*0.5;
                float2 aa = float2(_smoothEdges,_smoothEdges)*0.5;
                float2 uv = smoothstep(_size,_size+aa,_st);
                uv *= smoothstep(_size,_size+aa,float2(1.0,1.0)-_st);
                return uv.x*uv.y;
            }

            sampler2D _MainTex;
            float _Trans;
            fixed4 _ColorBck;

            fixed4 frag (v2f i) : SV_Target
            {
                float aspect = _ScreenParams.x / _ScreenParams.y;
                fixed4 col = tex2D(_MainTex, i.uv);
                
                float2 st = tile(float2(i.uv.x*aspect,i.uv.y)*5.0f,8);

                float b = box(st,float2(1.0,1.0),2.0*_Trans);

                col = lerp(col,_ColorBck,b);
                return col;
            }
            ENDCG
        }
         //4
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
                float4 screenPos : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            float2 tile(float2 _st, float _zoom){
                _st *= _zoom;
                return frac(_st);
            }

            float box(float2 _st, float2 _size, float _smoothEdges){
                _size = float2(0.5,0.5) -_size*0.5;
                float2 aa = float2(_smoothEdges,_smoothEdges)*0.5;
                float2 uv = smoothstep(_size,_size+aa,_st);
                uv *= smoothstep(_size,_size+aa,float2(1.0,1.0)-_st);
                return uv.x*uv.y;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
    
                fixed4 col = tex2D(_MainTex, i.uv);

                return col*1.5f;
            }
            ENDCG
        }
    }
}
