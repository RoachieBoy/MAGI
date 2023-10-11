using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "Triangle", menuName = "SynthModules/Oscillation/Triangle")]
    public class Triangle : SynthModule
    {
        public override (float value, float updatedPhase) GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            var phaseIncrement = (frequency / SampleRate);
            
            initialPhase = (initialPhase + phaseIncrement) % 1;

            var value = (Mathf.PingPong(initialPhase * 2.0f, 1.0f) - 0.5f) * 2.0f * amplitude;
            
            return (value, initialPhase);
        }
    }
}