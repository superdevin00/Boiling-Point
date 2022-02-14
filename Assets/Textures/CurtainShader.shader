// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/CurtainShader"
{
    Properties
    {
        
        _Cutoff("Cutoff", Range(0, 1.0)) = 0.5
        _Color("Color", Color) = (1, 1, 1, 1)
        _MainTex("Texture", 2D) = "white"
        _TransitionTex("Transition", 2D) = "white"
    }

    SubShader
    {  
        ZTest Always

        Pass
        {
            /*
            CGPROGRAM
            //#pragma target 4.0
            #pragma vertex vert
            #pragma fragment frag
            

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Cutoff;
            float4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
                float uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                if (i.uv.x < _Cutoff) {
                    return _Color;
                }

                return tex2D(_MainTex, i.uv);
            }
            ENDCG
            */
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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _Cutoff;
            float4 _Color;
            sampler2D _TransitionTex;

            /*
            fixed4 text(v2f i) : SV_Target
            {
                fixed4 transit = tex2D(_TransitionTex, i.uv);
                
                if (transit.b < _Cutoff)
                    return _Color;

                return tex2D(_MainTex, i.uv);
            }
            */
            
            float4 frag(v2f i) : SV_Target
            {
                fixed4 transit = tex2D(_TransitionTex, i.uv);

                if (transit.b < _Cutoff)
                    return _Color;

                return tex2D(_MainTex, i.uv);
            }
            
            ENDCG
        }
    }
}
