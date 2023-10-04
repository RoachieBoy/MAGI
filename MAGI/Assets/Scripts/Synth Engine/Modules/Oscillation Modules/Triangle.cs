using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "Triangle", menuName = "SynthModules/Oscillation/Triangle")]
    public class Triangle: SynthModule
    {
        public override (float value, float updatedPhase) GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            // ping pong returns a value between 0 and 1, so we multiply it by the amplitude to get the correct range
            // the effect of this is that the wave will go from 0 to 1 to 0 to -1 to 0, etc.
            var waveForm = amplitude * Mathf.PingPong(AngularFrequency(frequency) * initialPhase, 1.0f);
            
            // update the phase to the next value of sample 
            var updatedPhase = initialPhase + 1.0f;
            
            return (waveForm, updatedPhase);
        }
    }
}