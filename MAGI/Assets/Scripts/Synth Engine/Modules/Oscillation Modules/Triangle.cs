using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "Triangle", menuName = "SynthModules/Oscillation/Triangle")]
    public class Triangle: SynthModule
    {
        public override (float value, float updatedPhase) GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            // Calculate the triangle waveform
            var waveForm = amplitude * (Mathf.PingPong(AngularFrequency(frequency) * initialPhase, 1.0f));
            
            // add the next phase to the current phase to get the updated phase
            var updatedPhase = initialPhase + 1.0f;
            
            return (waveForm, updatedPhase);
        }
    }
}