using System;
using UnityEngine;

namespace BasicSynthModules
{
    [CreateAssetMenu(fileName = "SineModule", menuName = "BasicSynthModules/SineModule")]
    public class SineModule : SynthModule
    {
        public override void GenerateSamples(float[] data, int channels, float frequency, float amplitude)
        {
            var phaseIncrement = frequency * 2.0 * Mathf.PI / SampleRate;

            // iterate over each sample in the data array
            for (var sample = 0; sample < data.Length; sample += channels)
            {
                Phase += (float) phaseIncrement;

                // Calculate the sample value based on the sine of the phase
                var sampleValue = (float) (amplitude * Math.Sin(Phase));

                if (channels == 2) data[sample + 1] = sampleValue;

                if (Phase > Mathf.PI * 2) Phase -= Mathf.PI * 2;
            }
        }
    }
}