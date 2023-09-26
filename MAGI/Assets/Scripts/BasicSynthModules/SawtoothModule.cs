using UnityEngine;

namespace BasicSynthModules
{
    [CreateAssetMenu(fileName = "SawtoothModule", menuName = "BasicSynthModules/SawtoothModule")]
    public class SawtoothModule: SynthModule
    {
        public override void GenerateSamples(float[] data, int channels, float frequency, float amplitude)
        {
            // this represents the number of radians to increment the phase by for each sample
            var phaseIncrement = frequency / SampleRate;

            // iterate over each sample in the data array
            for (var sample = 0; sample < data.Length; sample += channels)
            {
                // sawtooth wave formula, which is y = 2 * (x - floor(x + 0.5))
                var value = ((float) Phase - 0.5f) * 2 * amplitude;

                Phase = (Phase + phaseIncrement) % 1;

                for (var channel = 0; channel < channels; channel++)
                {
                    data[sample + channel] = value;
                }
            }
        }
    }
}