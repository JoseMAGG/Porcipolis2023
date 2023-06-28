// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2741,x:32866,y:32423,varname:node_2741,prsc:2|emission-6990-OUT,clip-4240-OUT;n:type:ShaderForge.SFN_Tex2d,id:7588,x:32344,y:32835,ptovrint:False,ptlb:Ojos Abiertos,ptin:_OjosAbiertos,varname:node_7588,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Time,id:5563,x:31729,y:32248,varname:node_5563,prsc:2;n:type:ShaderForge.SFN_Sin,id:271,x:32067,y:32190,varname:node_271,prsc:2|IN-5563-T;n:type:ShaderForge.SFN_Tex2d,id:6152,x:32107,y:32644,ptovrint:False,ptlb:Ojos Cerrados,ptin:_OjosCerrados,varname:node_6152,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_If,id:6990,x:32412,y:32519,varname:node_6990,prsc:2|A-851-OUT,B-5282-OUT,GT-7588-RGB,EQ-6152-RGB,LT-6152-RGB;n:type:ShaderForge.SFN_Vector1,id:5282,x:32135,y:32546,varname:node_5282,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:1700,x:32297,y:32251,varname:node_1700,prsc:2|A-271-OUT,B-8869-OUT;n:type:ShaderForge.SFN_Vector1,id:8869,x:32067,y:32441,varname:node_8869,prsc:2,v1:10;n:type:ShaderForge.SFN_Abs,id:851,x:32399,y:32388,varname:node_851,prsc:2|IN-1700-OUT;n:type:ShaderForge.SFN_If,id:4240,x:32677,y:32572,varname:node_4240,prsc:2|A-851-OUT,B-5282-OUT,GT-7588-A,EQ-6152-A,LT-6152-A;proporder:7588-6152;pass:END;sub:END;*/

Shader "Unlit/Morion 9" {
    Properties {
        _OjosAbiertos ("Ojos Abiertos", 2D) = "white" {}
        _OjosCerrados ("Ojos Cerrados", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _OjosAbiertos; uniform float4 _OjosAbiertos_ST;
            uniform sampler2D _OjosCerrados; uniform float4 _OjosCerrados_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 node_5563 = _Time;
                float node_851 = abs((sin(node_5563.g)*10.0));
                float node_5282 = 1.0;
                float node_4240_if_leA = step(node_851,node_5282);
                float node_4240_if_leB = step(node_5282,node_851);
                float4 _OjosCerrados_var = tex2D(_OjosCerrados,TRANSFORM_TEX(i.uv0, _OjosCerrados));
                float4 _OjosAbiertos_var = tex2D(_OjosAbiertos,TRANSFORM_TEX(i.uv0, _OjosAbiertos));
                clip(lerp((node_4240_if_leA*_OjosCerrados_var.a)+(node_4240_if_leB*_OjosAbiertos_var.a),_OjosCerrados_var.a,node_4240_if_leA*node_4240_if_leB) - 0.5);
////// Lighting:
////// Emissive:
                float node_6990_if_leA = step(node_851,node_5282);
                float node_6990_if_leB = step(node_5282,node_851);
                float3 emissive = lerp((node_6990_if_leA*_OjosCerrados_var.rgb)+(node_6990_if_leB*_OjosAbiertos_var.rgb),_OjosCerrados_var.rgb,node_6990_if_leA*node_6990_if_leB);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _OjosAbiertos; uniform float4 _OjosAbiertos_ST;
            uniform sampler2D _OjosCerrados; uniform float4 _OjosCerrados_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 node_5563 = _Time;
                float node_851 = abs((sin(node_5563.g)*10.0));
                float node_5282 = 1.0;
                float node_4240_if_leA = step(node_851,node_5282);
                float node_4240_if_leB = step(node_5282,node_851);
                float4 _OjosCerrados_var = tex2D(_OjosCerrados,TRANSFORM_TEX(i.uv0, _OjosCerrados));
                float4 _OjosAbiertos_var = tex2D(_OjosAbiertos,TRANSFORM_TEX(i.uv0, _OjosAbiertos));
                clip(lerp((node_4240_if_leA*_OjosCerrados_var.a)+(node_4240_if_leB*_OjosAbiertos_var.a),_OjosCerrados_var.a,node_4240_if_leA*node_4240_if_leB) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
