using UnityEngine;

namespace Synth_Modules.Oscillator_Modules
{
    [CreateAssetMenu(fileName = "SawtoothModule", menuName = "BasicSynthModules/SawtoothModule")]
    public class SawtoothModule : SynthModule
    {
        public override void GenerateSamples(float[] data, int channels, float frequency, float amplitude)
        {
            // Precompute values for performance
            var phaseIncrement = frequency / SampleRate;
            var dataLength = data.Length;

            for (var sample = 0; sample < dataLength; sample += channels)
            {
                // Calculate the sawtooth value for the current sample
                var value = (Phase * 2 - 1) * amplitude;

                Phase += phaseIncrement;

                // Wrap the phase back to [0, 1] when it exceeds 1
                // this is the same as Phase %= 1, but faster to compute
                if (Phase >= 1) Phase -= 1;
                
                for (var channel = 0; channel < channels; channel++)
                {
                    data[sample + channel] = value;
                }
            }
        }

    }
}