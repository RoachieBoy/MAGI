Shader "Custom/Hologram"
{
    Properties
    {
        // Texture properties
        _MainTex ("Main Texture", 2D) = "white" {}
        _BlendMap ("Blend Map", 2D) = "white" {}

        // Color properties
        _HologramColor ("Hologram Color", Color) = (1, 1, 1, 1)
        _RimColor ("Rim Color", Color) = (0, 1, 0, 1)

        // Wave properties
        _WaveFrequency ("Wave Frequency", Range(0, 10)) = 1.0
        _WaveAmplitude ("Wave Amplitude", Range(0, 1)) = 0.1

        // Pulse properties
        _PulseIntensity ("Pulse Intensity", Range(0, 10)) = 1.0
        _PulsingFactor("Pulse Factor", Range(0, 5)) = 1.0

        // Rim properties
        _RimBlend ("Rim Blend", Range(0, 1)) = 0.5

        // Scanning properties
        _ScanningFrequency ("Scanning Frequency", Range(0, 200)) = 100
        _ScanningSpeed ("Scanning Speed", Range(0, 200)) = 100
        
        _HoloIntensityFactor ("Hologram Intensity Factor", Range(0, 2)) = 1.0
        _RimBlendFactor ("Rim Blend Factor", Range(0, 2)) = 1.0
        _GlitchAmplitude ("Glitch Amplitude", Range(0, 1)) = 0.1
        _GlitchFrequency ("Glitch Frequency", Range(0, 10)) = 1.0
        _LightingDirection ("Lighting Direction", Vector) = (1, 3, 1)

        // Enable/disable flags for effects
        _PulsingEnabled ("Enable Pulsing", Range(0, 1)) = 1.0
        _GlitchEnabled ("Enable Glitching", Range(0, 1)) = 1.0
        _ScanningEnabled ("Enable Scanning", Range(0, 1)) = 1.0
        _RimBlendEnabled ("Enable Rim Blend", Range(0, 1)) = 1.0
        
        // Glitch properties
        _GlitchStrength("Glitch Strength", Range(0, 1)) = 0.5
        
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Transparent" 
        }
        
        LOD 100
        Cull Off
        ZWrite Off

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // Constants for effect thresholds
            const float GlitchPhaseThreshold = 0.1;
            const float LineThicknessStep = 0.02;
            const float PulsingFactor = 0.5;
            const float ScanningThreshold = 0;
            const float RimBlendThreshold = 0.5;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2_f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            // Shader properties
            sampler2D main_tex;
            sampler2D blend_map;
            
            float4 _HologramColor;
            float4 _RimColor;
            
            float _WaveFrequency;
            float _WaveAmplitude;
            
            float _PulseIntensity;
            float _PulsingEnabled;
            float _PulsingFactor;
            
            float _RimBlend;
            float _RimBlendFactor;
            float _RimBlendEnabled;
            
            float _ScanningFrequency;
            float _ScanningSpeed;
            float _ScanningEnabled;
            
            float _HoloIntensityFactor;
            
            float _GlitchEnabled;
            float _GlitchAmplitude;
            float _GlitchFrequency;
            
            float3 _LightingDirection;

            v2_f vert(appdata_t v)
            {
                v2_f output;

                output.vertex = UnityObjectToClipPos(v.vertex);

                // Apply vertical wave distortion
                output.vertex.y += _WaveAmplitude * sin(_WaveFrequency * _Time.y + v.vertex.x);

                // glitch glitch 
                const float glitchOffset = _GlitchAmplitude * (smoothstep(0, 1, frac(_Time.y * _GlitchFrequency))
                    - GlitchPhaseThreshold);

                // determines the offset of the glitch on the horizontal axis
                output.vertex.x += glitchOffset;

                // sets the output uv to the input uv to ensure the texture is not distorted `
                output.uv = v.uv;

                return output;
            }

            // The frag function is called for each pixel on the screen
            half4 frag(v2_f i) : SV_Target
            {
                // Sample the main texture and blend map to get the base color
                const half4 startingColour = tex2D(main_tex, i.uv); // Sample the main texture
                half4 blendMapColor = tex2D(blend_map, i.uv); // Sample the blend map

                // Calculate hologram intensity and rim blend based on blend map colors
                const half hologramIntensity = blendMapColor.r * _HoloIntensityFactor;
                const half rimBlend = blendMapColor.b * _RimBlendFactor; 

                // Define the normalized direction of the light source
                const half3 lightDir = normalize(_LightingDirection);

                // Calculate the intensity of light reflection based on the vertex normal and light direction
                const half intensity = max(0, dot(lightDir, normalize(i.vertex.y)));

                // Calculate basic hologram color using light effect
                half4 mainHoloColour = _HologramColor * intensity * hologramIntensity;

                // Apply scanning effect
                if (_ScanningEnabled > 0) mainHoloColour *= 1 -
                    max(ScanningThreshold, cos(i.vertex.y *_ScanningFrequency + _Time.x * _ScanningSpeed));

                // Apply pulsing effect
                if (_PulsingEnabled > 0)
                    mainHoloColour *= (PulsingFactor + _PulsingFactor * sin(_PulseIntensity * _Time.y));

                // this will represent the final colour that the hologram will take on 
                half4 finalColor;

                if (_RimBlendEnabled > 0) finalColor = lerp(mainHoloColour, startingColour, rimBlend);
                else finalColor = mainHoloColour;

                // rim color to the final colour of the hologram 
                finalColor.rgb = lerp(finalColor.rgb, _RimColor.rgb, _RimBlend);

                return finalColor;
            }
            ENDCG
        }
    }
}