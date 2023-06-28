// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33596,y:32610,varname:node_3138,prsc:2|emission-9334-OUT;n:type:ShaderForge.SFN_NormalVector,id:9936,x:32265,y:32820,prsc:2,pt:False;n:type:ShaderForge.SFN_LightVector,id:2986,x:32265,y:32672,varname:node_2986,prsc:2;n:type:ShaderForge.SFN_Dot,id:4606,x:32469,y:32731,varname:node_4606,prsc:2,dt:0|A-2986-OUT,B-9936-OUT;n:type:ShaderForge.SFN_Tex2d,id:9571,x:32777,y:32214,ptovrint:False,ptlb:Fondo,ptin:_Fondo,varname:node_9571,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Step,id:9659,x:32540,y:32566,varname:node_9659,prsc:2|A-9827-OUT,B-4606-OUT;n:type:ShaderForge.SFN_Slider,id:9827,x:32186,y:32569,ptovrint:False,ptlb:Sombreado,ptin:_Sombreado,varname:node_9827,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.474359,max:1;n:type:ShaderForge.SFN_Slider,id:9128,x:32581,y:32916,ptovrint:False,ptlb:IntensidadSombra,ptin:_IntensidadSombra,varname:node_9128,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:2;n:type:ShaderForge.SFN_Vector3,id:3758,x:32738,y:32724,varname:node_3758,prsc:2,v1:1,v2:1,v3:1;n:type:ShaderForge.SFN_Multiply,id:9410,x:32918,y:32749,varname:node_9410,prsc:2|A-3758-OUT,B-9128-OUT;n:type:ShaderForge.SFN_Add,id:1799,x:32834,y:32564,varname:node_1799,prsc:2|A-9659-OUT,B-9410-OUT,C-2502-OUT;n:type:ShaderForge.SFN_Multiply,id:4549,x:33076,y:32464,varname:node_4549,prsc:2|A-5228-OUT,B-1799-OUT;n:type:ShaderForge.SFN_Max,id:8828,x:33169,y:32609,varname:node_8828,prsc:2|A-1799-OUT,B-8189-OUT;n:type:ShaderForge.SFN_Vector1,id:8189,x:32925,y:32664,varname:node_8189,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Divide,id:5228,x:33089,y:32293,varname:node_5228,prsc:2|A-9571-RGB,B-879-OUT;n:type:ShaderForge.SFN_Vector1,id:879,x:32803,y:32409,varname:node_879,prsc:2,v1:2.5;n:type:ShaderForge.SFN_Divide,id:2702,x:32388,y:32344,varname:node_2702,prsc:2|A-9827-OUT,B-8956-OUT;n:type:ShaderForge.SFN_Vector1,id:8956,x:32181,y:32331,varname:node_8956,prsc:2,v1:2;n:type:ShaderForge.SFN_Step,id:2502,x:32540,y:32401,varname:node_2502,prsc:2|A-2702-OUT,B-4606-OUT;n:type:ShaderForge.SFN_Tex2d,id:1593,x:33350,y:31905,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_1593,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-2057-OUT;n:type:ShaderForge.SFN_Vector2,id:6812,x:32797,y:32009,varname:node_6812,prsc:2,v1:1,v2:1;n:type:ShaderForge.SFN_Slider,id:9986,x:32640,y:32112,ptovrint:False,ptlb:Desplazamiento,ptin:_Desplazamiento,varname:node_9986,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.329292,max:1;n:type:ShaderForge.SFN_Multiply,id:6970,x:32970,y:32020,varname:node_6970,prsc:2|A-6812-OUT,B-9986-OUT;n:type:ShaderForge.SFN_Slider,id:2614,x:32582,y:31888,ptovrint:False,ptlb:Escala,ptin:_Escala,varname:node_2614,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_TexCoord,id:9486,x:32838,y:31740,varname:node_9486,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:4953,x:33072,y:31841,varname:node_4953,prsc:2|A-9486-UVOUT,B-2738-OUT;n:type:ShaderForge.SFN_Add,id:2057,x:33157,y:31972,varname:node_2057,prsc:2|A-4953-OUT,B-6970-OUT;n:type:ShaderForge.SFN_Multiply,id:9334,x:33424,y:32252,varname:node_9334,prsc:2|A-2731-OUT,B-4549-OUT;n:type:ShaderForge.SFN_Step,id:1068,x:33612,y:31905,varname:node_1068,prsc:2|A-3292-OUT,B-1593-RGB;n:type:ShaderForge.SFN_Slider,id:3292,x:33422,y:31817,ptovrint:False,ptlb:Sensible,ptin:_Sensible,varname:node_3292,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4615385,max:1;n:type:ShaderForge.SFN_Add,id:2731,x:33581,y:32033,varname:node_2731,prsc:2|A-1068-OUT,B-2880-RGB;n:type:ShaderForge.SFN_Color,id:2880,x:33369,y:32078,ptovrint:False,ptlb:Manchas,ptin:_Manchas,varname:node_2880,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:9188,x:32718,y:31952,varname:node_9188,prsc:2,v1:10;n:type:ShaderForge.SFN_Subtract,id:2738,x:32919,y:31908,varname:node_2738,prsc:2|A-9188-OUT,B-2614-OUT;proporder:9571-9827-9128-1593-9986-2614-3292-2880;pass:END;sub:END;*/

Shader "Morion/Morion 8" {
    Properties {
        _Fondo ("Fondo", 2D) = "white" {}
        _Sombreado ("Sombreado", Range(0, 1)) = 0.474359
        _IntensidadSombra ("IntensidadSombra", Range(0, 2)) = 0.5
        _Noise ("Noise", 2D) = "white" {}
        _Desplazamiento ("Desplazamiento", Range(0, 1)) = 0.329292
        _Escala ("Escala", Range(0, 10)) = 1
        _Sensible ("Sensible", Range(0, 1)) = 0.4615385
        _Manchas ("Manchas", Color) = (0.5,0.5,0.5,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma target 3.0
            uniform sampler2D _Fondo; uniform float4 _Fondo_ST;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Sombreado)
                UNITY_DEFINE_INSTANCED_PROP( float, _IntensidadSombra)
                UNITY_DEFINE_INSTANCED_PROP( float, _Desplazamiento)
                UNITY_DEFINE_INSTANCED_PROP( float, _Escala)
                UNITY_DEFINE_INSTANCED_PROP( float, _Sensible)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Manchas)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
////// Emissive:
                float _Sensible_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Sensible );
                float _Escala_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Escala );
                float _Desplazamiento_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Desplazamiento );
                float2 node_2057 = ((i.uv0*(10.0-_Escala_var))+(float2(1,1)*_Desplazamiento_var));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(node_2057, _Noise));
                float4 _Manchas_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Manchas );
                float4 _Fondo_var = tex2D(_Fondo,TRANSFORM_TEX(i.uv0, _Fondo));
                float _Sombreado_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Sombreado );
                float node_4606 = dot(lightDirection,i.normalDir);
                float _IntensidadSombra_var = UNITY_ACCESS_INSTANCED_PROP( Props, _IntensidadSombra );
                float3 node_1799 = (step(_Sombreado_var,node_4606)+(float3(1,1,1)*_IntensidadSombra_var)+step((_Sombreado_var/2.0),node_4606));
                float3 emissive = ((step(_Sensible_var,_Noise_var.rgb)+_Manchas_var.rgb)*((_Fondo_var.rgb/2.5)*node_1799));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma target 3.0
            uniform sampler2D _Fondo; uniform float4 _Fondo_ST;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Sombreado)
                UNITY_DEFINE_INSTANCED_PROP( float, _IntensidadSombra)
                UNITY_DEFINE_INSTANCED_PROP( float, _Desplazamiento)
                UNITY_DEFINE_INSTANCED_PROP( float, _Escala)
                UNITY_DEFINE_INSTANCED_PROP( float, _Sensible)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Manchas)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float3 finalColor = 0;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
