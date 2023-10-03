using UnityEngine;

namespace Synth_Engine.Synth_Modules.Oscillator_Modules
{
    [CreateAssetMenu(fileName = "PulseModule", menuName = "BasicSynthModules/PulseModule")]
    public class PulseModule : SynthModule
    {
        private const float TwoPi = Mathf.PI * 2.0f;
        
        [Tooltip("determines the ratio of time spent in the \"high\" " +
                 "state (positive values) compared to the \"low\" state (negative values)")]
        
        [Range(0.0f, 1.0f)] public float dutyCycle = 0.25f; 

        public override void GenerateSamples(float[] data, int channels, float frequency, float amplitude)
        {
            var phaseIncrement = frequency * TwoPi / SampleRate;

            for (var sample = 0; sample < data.Length; sample += channels)
            {
                var sampleValue = amplitude * (Phase < dutyCycle * TwoPi ? 1.0f : -1.0f);
                
                // Write the sample 
                for (var channel = 0; channel < channels; channel++)
                {
                    data[sample + channel] = sampleValue;
                }
                
                // Update the phase
                Phase += phaseIncrement;

                // Ensure the phase stays within [0, 2π) 
                while (Phase >= TwoPi) Phase -= TwoPi;
            }
        }
    }
}