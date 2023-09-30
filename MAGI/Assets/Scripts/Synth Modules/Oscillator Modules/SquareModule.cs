using UnityEngine;

namespace Synth_Modules.Oscillator_Modules
{
    [CreateAssetMenu(fileName = "SquareModule", menuName = "BasicSynthModules/SquareModule")]
    public class SquareModule: SynthModule
    {
        public override void GenerateSamples(float[] data, int channels, float frequency, float amplitude)
        {
            // this represents the number of radians to increment the phase by for each sample
            var phaseIncrement = frequency / SampleRate;

            // iterate over each sample in the data array
            for (var sample = 0; sample < data.Length; sample += channels)
            {
                // square wave formula, which is y = amplitude if x < 0.5, otherwise -amplitude
                var value = (Phase < 0.5) ? amplitude : -amplitude;

                Phase = (Phase + phaseIncrement) % 1;

                for (var channel = 0; channel < channels; channel++)
                {
                    data[sample + channel] = value;
                }
            }
        }
    }
}