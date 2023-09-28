using UnityEngine;

namespace BasicSynthModules
{
    [CreateAssetMenu(fileName = "SineModule", menuName = "BasicSynthModules/SineModule")]
    public class SineModule : SynthModule
    {
        private float _phaseIncrement;
        private float _smoothedPhase;

        public override void GenerateSamples(float[] data, int channels, float frequency, float amplitude)
        {
            _phaseIncrement = frequency * 2.0f * Mathf.PI / SampleRate;

            for (var sample = 0; sample < data.Length; sample += channels)
            {
                var sampleValue = amplitude * Mathf.Sin(Phase);

                if (channels == 2)
                {
                    data[sample + 1] = sampleValue;
                }

                // Update the phase
                Phase += _phaseIncrement;

                // Ensure the phase stays within [0, 2π)
                while (Phase >= Mathf.PI * 2)
                {
                    Phase -= Mathf.PI * 2;
                }
            }
        }
    }
}
