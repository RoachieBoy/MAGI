using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "Triangle", menuName = "SynthModules/Oscillation/Triangle")]
    public class Triangle: SynthModule
    {
        public override (float value, float updatedPhase) GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            var angularFrequency = 2.0f * Mathf.PI * frequency / SampleRate;
            
            var waveForm = amplitude * Mathf.PingPong(angularFrequency * initialPhase, 1.0f);
            
            var updatedPhase = initialPhase + 1.0f;
            
            return (waveForm, updatedPhase);
        }
    }
}