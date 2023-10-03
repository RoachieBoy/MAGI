using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "Sine", menuName = "SynthModules/Oscillation/Sine")]
    public class Sine: SynthModule
    {
        public override (float value, float updatedPhase) GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            var angularFrequency = 2.0f * Mathf.PI * frequency / SampleRate;
            
            var waveForm = amplitude * Mathf.Sin(angularFrequency * initialPhase);
            
            var updatedPhase = initialPhase + 1.0f;
            
            return (waveForm, updatedPhase);
        }
    }
}