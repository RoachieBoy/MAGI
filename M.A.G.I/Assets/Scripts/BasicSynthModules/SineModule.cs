using UnityEngine;

namespace BasicSynthModules
{ 
    [CreateAssetMenu(fileName = "SineModule", menuName = "BasicSynthModules/SineModule")]
    public class SineModule : SynthModule
    {
        public override void GenerateSamples(float[] data, int channels, float frequency, float amplitude)
        {
            // this represents the number of radians to increment the phase by for each sample
            var phaseIncrement = 2 * Mathf.PI * frequency / SampleRate;
            
            // iterate over each sample in the data array
            for (var sample = 0; sample < data.Length; sample += channels)
            {
                // sine wave formula, which is y = amplitude * sin(2 * pi * frequency * time)
                var sampleValue = amplitude * Mathf.Sin((float) Phase);
                
                // increment the phase by the phase increment
                Phase += phaseIncrement;

                // iterate through channels and set the sample value for each channel to the sample value
                for (var channel = 0; channel < channels; channel++)
                {
                    data[sample + channel] = sampleValue;
                }
            }
        }
    }
}