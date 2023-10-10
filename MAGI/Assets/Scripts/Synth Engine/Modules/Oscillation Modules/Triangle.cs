using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "Triangle", menuName = "SynthModules/Oscillation/Triangle")]
    public class Triangle : SynthModule
    {
        public override (float value, float updatedPhase) GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            var phaseIncrement = frequency / SampleRate;
            
            var value = 2.0f * Mathf.Abs(initialPhase - Mathf.Floor(initialPhase + 0.5f)) * amplitude - amplitude;
            
            var updatedPhase = (initialPhase + phaseIncrement) % 1;
            
            return (value, updatedPhase);
        }
    }
}