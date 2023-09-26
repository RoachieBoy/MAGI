using UnityEngine;

namespace BasicSynthModules
{
    [CreateAssetMenu(fileName = "TriangleModule", menuName = "BasicSynthModules/TriangleModule")]
    public class TriangleModule: SynthModule
    {
        public override void GenerateSamples(float[] data, int channels, float frequency, float amplitude)
        {
            // this represents the number of radians to increment the phase by for each sample
            var phaseIncrement = frequency / SampleRate;

            // iterate over each sample in the data array
            for (var sample = 0; sample < data.Length; sample += channels)
            {
                // triangle wave formula, which is y = 2 * (abs(2 * (x - floor(x + 0.5))) - 1)
                var value = (2 * Mathf.Abs((float) (2 * (Phase - Mathf.Floor((float) (Phase + 0.5f))))) - 1) * amplitude;

                Phase = (Phase + phaseIncrement) % 1;

                for (var channel = 0; channel < channels; channel++)
                {
                    data[sample + channel] = value;
                }
            }
        }
    }
}