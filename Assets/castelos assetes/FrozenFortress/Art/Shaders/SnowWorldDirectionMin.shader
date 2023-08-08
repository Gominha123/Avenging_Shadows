// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:Standard,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.8235294,fgcg:0.9488844,fgcb:1,fgca:1,fgde:0.007,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2865,x:32755,y:32779,varname:node_2865,prsc:2|diff-8366-OUT,spec-5365-R,gloss-1588-OUT,normal-8959-OUT,difocc-2523-OUT,spcocc-2523-OUT;n:type:ShaderForge.SFN_Tex2d,id:7736,x:32534,y:32204,ptovrint:True,ptlb:Base Color,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5964,x:31987,y:33085,ptovrint:False,ptlb:NormalMap,ptin:_NormalMap,varname:_BumpMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:943,x:32378,y:32588,ptovrint:False,ptlb:Snow Tex,ptin:_SnowTex,varname:node_943,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_NormalVector,id:6559,x:32361,y:32300,prsc:2,pt:True;n:type:ShaderForge.SFN_Slider,id:922,x:32645,y:32685,ptovrint:False,ptlb:Snow Amount,ptin:_SnowAmount,varname:node_922,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:4;n:type:ShaderForge.SFN_Tex2d,id:5365,x:32042,y:32753,ptovrint:False,ptlb:MetallicRoughness,ptin:_MetallicRoughness,varname:node_5365,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:7788,x:33248,y:32381,varname:node_7788,prsc:2|A-2656-OUT,B-943-RGB,T-3688-OUT;n:type:ShaderForge.SFN_Vector3,id:1874,x:32361,y:32459,varname:node_1874,prsc:2,v1:0,v2:1,v3:0;n:type:ShaderForge.SFN_Tex2d,id:4829,x:33662,y:32828,ptovrint:False,ptlb:Detail,ptin:_Detail,varname:node_4829,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7617,x:32301,y:33230,ptovrint:False,ptlb:AmbientOcclusion,ptin:_AmbientOcclusion,varname:node_7617,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Power,id:2523,x:32514,y:33210,varname:node_2523,prsc:2|VAL-7617-R,EXP-4848-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4848,x:32438,y:33386,ptovrint:False,ptlb:AO Intensity,ptin:_AOIntensity,varname:node_4848,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:2656,x:32831,y:32184,varname:node_2656,prsc:2|A-7736-RGB,B-2014-RGB;n:type:ShaderForge.SFN_Color,id:2014,x:32831,y:32331,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:node_2014,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Dot,id:1347,x:32552,y:32378,varname:node_1347,prsc:2,dt:0|A-6559-OUT,B-1874-OUT;n:type:ShaderForge.SFN_Multiply,id:6731,x:33096,y:32571,varname:node_6731,prsc:2|A-9099-OUT,B-922-OUT;n:type:ShaderForge.SFN_NormalBlend,id:8959,x:32538,y:32990,varname:node_8959,prsc:2|BSE-5964-RGB,DTL-1601-RGB;n:type:ShaderForge.SFN_Blend,id:8366,x:33326,y:32848,varname:node_8366,prsc:2,blmd:12,clmp:True|SRC-7788-OUT,DST-5460-OUT;n:type:ShaderForge.SFN_ComponentMask,id:7497,x:33419,y:33183,varname:node_7497,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-1531-OUT;n:type:ShaderForge.SFN_Multiply,id:1359,x:33685,y:33077,varname:node_1359,prsc:2|A-4829-RGB,B-7497-OUT;n:type:ShaderForge.SFN_RemapRange,id:1531,x:33238,y:33183,varname:node_1531,prsc:2,frmn:0,frmx:1,tomn:1,tomx:0|IN-3688-OUT;n:type:ShaderForge.SFN_ConstantClamp,id:5460,x:33106,y:32848,varname:node_5460,prsc:2,min:0.5,max:1|IN-1359-OUT;n:type:ShaderForge.SFN_Tex2d,id:1601,x:31987,y:33283,ptovrint:False,ptlb:DetailNormal,ptin:_DetailNormal,varname:node_1601,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7ca4329c032e2e84f9fb6d026f1a63a5,ntxv:3,isnm:True;n:type:ShaderForge.SFN_ConstantClamp,id:1958,x:32802,y:32474,varname:node_1958,prsc:2,min:0,max:1|IN-1347-OUT;n:type:ShaderForge.SFN_RemapRange,id:9099,x:32928,y:32501,varname:node_9099,prsc:2,frmn:0,frmx:1,tomn:-6,tomx:1.5|IN-1958-OUT;n:type:ShaderForge.SFN_ConstantClamp,id:3688,x:33248,y:32571,varname:node_3688,prsc:2,min:0,max:1|IN-6731-OUT;n:type:ShaderForge.SFN_Add,id:1588,x:32552,y:32876,varname:node_1588,prsc:2|A-5365-A,B-6954-OUT;n:type:ShaderForge.SFN_Slider,id:1870,x:31817,y:32970,ptovrint:False,ptlb:SnowGloss,ptin:_SnowGloss,varname:node_1870,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.25,max:1;n:type:ShaderForge.SFN_Clamp,id:6954,x:32321,y:32876,varname:node_6954,prsc:2|IN-3688-OUT,MIN-9770-OUT,MAX-1870-OUT;n:type:ShaderForge.SFN_Vector1,id:9770,x:32130,y:32910,varname:node_9770,prsc:2,v1:0;proporder:7736-943-922-5365-5964-4829-7617-4848-2014-1601-1870;pass:END;sub:END;*/

Shader "Novelty Theory/SnowWorldDirectionMin" {
    Properties {
        _MainTex ("Base Color", 2D) = "white" {}
        _SnowTex ("Snow Tex", 2D) = "white" {}
        _SnowAmount ("Snow Amount", Range(0, 4)) = 1
        _MetallicRoughness ("MetallicRoughness", 2D) = "white" {}
        _NormalMap ("NormalMap", 2D) = "bump" {}
        _Detail ("Detail", 2D) = "white" {}
        _AmbientOcclusion ("AmbientOcclusion", 2D) = "white" {}
        _AOIntensity ("AO Intensity", Float ) = 1
        _MainColor ("MainColor", Color) = (1,1,1,1)
        _DetailNormal ("DetailNormal", 2D) = "bump" {}
        _SnowGloss ("SnowGloss", Range(0, 1)) = 0.25
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
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform sampler2D _SnowTex; uniform float4 _SnowTex_ST;
            uniform float _SnowAmount;
            uniform sampler2D _MetallicRoughness; uniform float4 _MetallicRoughness_ST;
            uniform sampler2D _Detail; uniform float4 _Detail_ST;
            uniform sampler2D _AmbientOcclusion; uniform float4 _AmbientOcclusion_ST;
            uniform float _AOIntensity;
            uniform float4 _MainColor;
            uniform sampler2D _DetailNormal; uniform float4 _DetailNormal_ST;
            uniform float _SnowGloss;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(i.uv0, _NormalMap)));
                float3 _DetailNormal_var = UnpackNormal(tex2D(_DetailNormal,TRANSFORM_TEX(i.uv0, _DetailNormal)));
                float3 node_8959_nrm_base = _NormalMap_var.rgb + float3(0,0,1);
                float3 node_8959_nrm_detail = _DetailNormal_var.rgb * float3(-1,-1,1);
                float3 node_8959_nrm_combined = node_8959_nrm_base*dot(node_8959_nrm_base, node_8959_nrm_detail)/node_8959_nrm_base.z - node_8959_nrm_detail;
                float3 node_8959 = node_8959_nrm_combined;
                float3 normalLocal = node_8959;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _MetallicRoughness_var = tex2D(_MetallicRoughness,TRANSFORM_TEX(i.uv0, _MetallicRoughness));
                float node_3688 = clamp(((clamp(dot(normalDirection,float3(0,1,0)),0,1)*7.5+-6.0)*_SnowAmount),0,1);
                float gloss = (_MetallicRoughness_var.a+clamp(node_3688,0.0,_SnowGloss));
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                d.boxMax[0] = unity_SpecCube0_BoxMax;
                d.boxMin[0] = unity_SpecCube0_BoxMin;
                d.probePosition[0] = unity_SpecCube0_ProbePosition;
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.boxMax[1] = unity_SpecCube1_BoxMax;
                d.boxMin[1] = unity_SpecCube1_BoxMin;
                d.probePosition[1] = unity_SpecCube1_ProbePosition;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float4 _AmbientOcclusion_var = tex2D(_AmbientOcclusion,TRANSFORM_TEX(i.uv0, _AmbientOcclusion));
                float node_2523 = pow(_AmbientOcclusion_var.r,_AOIntensity);
                float3 specularAO = node_2523;
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float3 specularColor = _MetallicRoughness_var.r;
                float specularMonochrome;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _SnowTex_var = tex2D(_SnowTex,TRANSFORM_TEX(i.uv0, _SnowTex));
                float4 _Detail_var = tex2D(_Detail,TRANSFORM_TEX(i.uv0, _Detail));
                float3 diffuseColor = saturate((lerp((_MainTex_var.rgb*_MainColor.rgb),_SnowTex_var.rgb,node_3688) > 0.5 ?  (1.0-(1.0-2.0*(lerp((_MainTex_var.rgb*_MainColor.rgb),_SnowTex_var.rgb,node_3688)-0.5))*(1.0-clamp((_Detail_var.rgb*(node_3688*-1.0+1.0).r),0.5,1))) : (2.0*lerp((_MainTex_var.rgb*_MainColor.rgb),_SnowTex_var.rgb,node_3688)*clamp((_Detail_var.rgb*(node_3688*-1.0+1.0).r),0.5,1))) ); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, GGXTerm(NdotH, 1.0-gloss));
                float specularPBL = (NdotL*visTerm*normTerm) * (UNITY_PI / 4);
                if (IsGammaSpace())
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                specularPBL = max(0, specularPBL * NdotL);
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz)*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular) * specularAO;
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                indirectDiffuse *= node_2523; // Diffuse AO
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
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
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform sampler2D _SnowTex; uniform float4 _SnowTex_ST;
            uniform float _SnowAmount;
            uniform sampler2D _MetallicRoughness; uniform float4 _MetallicRoughness_ST;
            uniform sampler2D _Detail; uniform float4 _Detail_ST;
            uniform float4 _MainColor;
            uniform sampler2D _DetailNormal; uniform float4 _DetailNormal_ST;
            uniform float _SnowGloss;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(i.uv0, _NormalMap)));
                float3 _DetailNormal_var = UnpackNormal(tex2D(_DetailNormal,TRANSFORM_TEX(i.uv0, _DetailNormal)));
                float3 node_8959_nrm_base = _NormalMap_var.rgb + float3(0,0,1);
                float3 node_8959_nrm_detail = _DetailNormal_var.rgb * float3(-1,-1,1);
                float3 node_8959_nrm_combined = node_8959_nrm_base*dot(node_8959_nrm_base, node_8959_nrm_detail)/node_8959_nrm_base.z - node_8959_nrm_detail;
                float3 node_8959 = node_8959_nrm_combined;
                float3 normalLocal = node_8959;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _MetallicRoughness_var = tex2D(_MetallicRoughness,TRANSFORM_TEX(i.uv0, _MetallicRoughness));
                float node_3688 = clamp(((clamp(dot(normalDirection,float3(0,1,0)),0,1)*7.5+-6.0)*_SnowAmount),0,1);
                float gloss = (_MetallicRoughness_var.a+clamp(node_3688,0.0,_SnowGloss));
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float3 specularColor = _MetallicRoughness_var.r;
                float specularMonochrome;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _SnowTex_var = tex2D(_SnowTex,TRANSFORM_TEX(i.uv0, _SnowTex));
                float4 _Detail_var = tex2D(_Detail,TRANSFORM_TEX(i.uv0, _Detail));
                float3 diffuseColor = saturate((lerp((_MainTex_var.rgb*_MainColor.rgb),_SnowTex_var.rgb,node_3688) > 0.5 ?  (1.0-(1.0-2.0*(lerp((_MainTex_var.rgb*_MainColor.rgb),_SnowTex_var.rgb,node_3688)-0.5))*(1.0-clamp((_Detail_var.rgb*(node_3688*-1.0+1.0).r),0.5,1))) : (2.0*lerp((_MainTex_var.rgb*_MainColor.rgb),_SnowTex_var.rgb,node_3688)*clamp((_Detail_var.rgb*(node_3688*-1.0+1.0).r),0.5,1))) ); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, GGXTerm(NdotH, 1.0-gloss));
                float specularPBL = (NdotL*visTerm*normTerm) * (UNITY_PI / 4);
                if (IsGammaSpace())
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                specularPBL = max(0, specularPBL * NdotL);
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _SnowTex; uniform float4 _SnowTex_ST;
            uniform float _SnowAmount;
            uniform sampler2D _MetallicRoughness; uniform float4 _MetallicRoughness_ST;
            uniform sampler2D _Detail; uniform float4 _Detail_ST;
            uniform float4 _MainColor;
            uniform float _SnowGloss;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = 0;
                
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _SnowTex_var = tex2D(_SnowTex,TRANSFORM_TEX(i.uv0, _SnowTex));
                float node_3688 = clamp(((clamp(dot(normalDirection,float3(0,1,0)),0,1)*7.5+-6.0)*_SnowAmount),0,1);
                float4 _Detail_var = tex2D(_Detail,TRANSFORM_TEX(i.uv0, _Detail));
                float3 diffColor = saturate((lerp((_MainTex_var.rgb*_MainColor.rgb),_SnowTex_var.rgb,node_3688) > 0.5 ?  (1.0-(1.0-2.0*(lerp((_MainTex_var.rgb*_MainColor.rgb),_SnowTex_var.rgb,node_3688)-0.5))*(1.0-clamp((_Detail_var.rgb*(node_3688*-1.0+1.0).r),0.5,1))) : (2.0*lerp((_MainTex_var.rgb*_MainColor.rgb),_SnowTex_var.rgb,node_3688)*clamp((_Detail_var.rgb*(node_3688*-1.0+1.0).r),0.5,1))) );
                float specularMonochrome;
                float3 specColor;
                float4 _MetallicRoughness_var = tex2D(_MetallicRoughness,TRANSFORM_TEX(i.uv0, _MetallicRoughness));
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, _MetallicRoughness_var.r, specColor, specularMonochrome );
                float roughness = 1.0 - (_MetallicRoughness_var.a+clamp(node_3688,0.0,_SnowGloss));
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Standard"
    CustomEditor "ShaderForgeMaterialInspector"
}
