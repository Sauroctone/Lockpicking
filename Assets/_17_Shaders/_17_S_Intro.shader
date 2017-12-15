// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33218,y:31995,varname:node_3138,prsc:2|emission-8169-RGB,clip-6982-OUT;n:type:ShaderForge.SFN_Color,id:8169,x:32832,y:32053,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_8169,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Tex2d,id:9657,x:32637,y:32304,ptovrint:False,ptlb:KeyHole,ptin:_KeyHole,varname:node_9657,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d16fb8524eb6c9c429e5bc5e1102d114,ntxv:0,isnm:False|UVIN-9948-OUT;n:type:ShaderForge.SFN_TexCoord,id:3979,x:31742,y:32296,varname:node_3979,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:5982,x:32001,y:32297,varname:node_5982,prsc:2,frmn:0,frmx:1,tomn:-0.5,tomx:0.5|IN-3979-UVOUT;n:type:ShaderForge.SFN_Multiply,id:4527,x:32210,y:32297,varname:node_4527,prsc:2|A-5982-OUT,B-8728-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8728,x:31987,y:32514,ptovrint:False,ptlb:Scale,ptin:_Scale,varname:node_8728,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Add,id:9948,x:32462,y:32300,varname:node_9948,prsc:2|A-4527-OUT,B-3229-OUT;n:type:ShaderForge.SFN_Vector2,id:3229,x:32177,y:32558,varname:node_3229,prsc:2,v1:0.5,v2:0.5;n:type:ShaderForge.SFN_ComponentMask,id:6982,x:32889,y:32291,varname:node_6982,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-9657-RGB;proporder:8169-9657-8728;pass:END;sub:END;*/

Shader "Shader Forge/S_Intro" {
    Properties {
        _Color ("Color", Color) = (1,0,0,1)
        _KeyHole ("KeyHole", 2D) = "white" {}
        _Scale ("Scale", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _KeyHole; uniform float4 _KeyHole_ST;
            uniform float _Scale;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float2 node_9948 = (((i.uv0*1.0+-0.5)*_Scale)+float2(0.5,0.5));
                float4 _KeyHole_var = tex2D(_KeyHole,TRANSFORM_TEX(node_9948, _KeyHole));
                clip(_KeyHole_var.rgb.r - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = _Color.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
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
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _KeyHole; uniform float4 _KeyHole_ST;
            uniform float _Scale;
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
                float2 node_9948 = (((i.uv0*1.0+-0.5)*_Scale)+float2(0.5,0.5));
                float4 _KeyHole_var = tex2D(_KeyHole,TRANSFORM_TEX(node_9948, _KeyHole));
                clip(_KeyHole_var.rgb.r - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
