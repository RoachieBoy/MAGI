using UnityEngine;

namespace Synth_Engine.Synth_Modules.Oscillator_Modules
{
    [CreateAssetMenu(fileName = "TriangleModule", menuName = "BasicSynthModules/TriangleModule")]
    public class TriangleModule: SynthModule
    {
        [SerializeField] [Range(0.1f, 3.0f)] private float loudnessModifier = 1.0f; 
        
        public override void GenerateSamples(float[] data, int channels, float frequency, float amplitude)
        {
            // this represents the number of radians to increment the phase by for each sample
            var phaseIncrement = frequency / SampleRate;

            // iterate over each sample in the data array
            for (var sample = 0; sample < data.Length; sample += channels)
            {
                var value = (2 * Mathf.Abs( (2 * (Phase - Mathf.Floor((Phase + 0.5f))))) - 1) 
                            * amplitude * loudnessModifier;
                
                Phase = (Phase + phaseIncrement) % 1;

                for (var channel = 0; channel < channels; channel++)
                {
                    data[sample + channel] = value;
                }
            }
        }
    }
}