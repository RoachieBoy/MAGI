using UnityEngine;

namespace BasicSynthModules
{
    [CreateAssetMenu(fileName = "SawtoothModule", menuName = "BasicSynthModules/SawtoothModule")]
    public class SawtoothModule: SynthModule
    {
        public override void GenerateSamples(float[] data, int channels, float frequency, float amplitude)
        {
            // Precompute values
            var phaseIncrement = frequency / SampleRate;
            var twoAmplitude = 2 * amplitude;
            var dataLength = data.Length;
            
            for (var sample = 0; sample < dataLength; sample += channels)
            {
                // Reduced multiplicative operations by precomputing 2*amplitude and using it directly in the formula
                var value = (Phase - 0.5f) * twoAmplitude;

                Phase = (Phase + phaseIncrement) % 1;

                for (var channel = 0; channel < channels; channel++)
                {
                    data[sample + channel] = value;
                }
            }
        }

    }
}