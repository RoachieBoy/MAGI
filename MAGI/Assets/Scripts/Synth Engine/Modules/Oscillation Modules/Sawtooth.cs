using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "Sawtooth", menuName = "SynthModules/Oscillation/Sawtooth")]
    public class Sawtooth : SynthModule
    {
        private const float WaveOffset = 0.5f;
        
        [SerializeField, Range(0f, 0.8f)] private float volumeModifier = 1f;
        
        public override (float value, float updatedPhase) GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            var phaseIncrement = frequency / SampleRate;

            amplitude *= volumeModifier;

            var value = (initialPhase + WaveOffset) % 1 * amplitude;
            
            var updatedPhase = (initialPhase + phaseIncrement) % 1;
            
            return (value, updatedPhase);
        }
    }
}