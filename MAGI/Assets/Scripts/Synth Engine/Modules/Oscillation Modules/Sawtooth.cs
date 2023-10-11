using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "Sawtooth", menuName = "SynthModules/Oscillation/Sawtooth")]
    public class Sawtooth : SynthModule
    {
        [SerializeField, Range(0f, 0.8f)] private float volumeModifier = 1f;
        
        public override (float value, float updatedPhase) GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            var phaseIncrement = frequency / SampleRate;

            // make it softer cause otherwise it's too loud
            amplitude *= volumeModifier;
            
            // sawtooth wave is a linear function from 0 to 1 and then from 1 to 0
            var value = (initialPhase + 0.5f) % 1 * amplitude;
            
            var updatedPhase = (initialPhase + phaseIncrement) % 1;
            
            return (value, updatedPhase);
        }
    }
}