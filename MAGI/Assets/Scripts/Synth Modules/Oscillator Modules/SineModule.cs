using UnityEngine;

namespace Synth_Modules.Oscillator_Modules
{
    [CreateAssetMenu(fileName = "SineModule", menuName = "BasicSynthModules/SineModule")]
    public class SineModule : SynthModule
    {
        private const float TwoPi = Mathf.PI * 2.0f;

        [SerializeField] [Range(0.1f, 3.0f)] private float loudnessModifier = 1.0f; 

        public override void GenerateSamples(float[] data, int channels, float frequency, float amplitude)
        {
            var phaseIncrement = frequency * TwoPi / SampleRate;

            for (var sample = 0; sample < data.Length; sample += channels)
            {
                var sampleValue = amplitude * loudnessModifier * Mathf.Sin(Phase);

                if (channels == 2) data[sample + 1] = sampleValue;
                
                // Update the phase
                Phase += phaseIncrement;

                // Ensure the phase stays within [0, 2π)
                while (Phase >= TwoPi) Phase -= TwoPi; 
            }
        }
    }
}

