using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "Sawtooth", menuName = "SynthModules/Oscillation/Sawtooth")]
    public class Sawtooth : SynthModule
    {
        public override (float value, float updatedPhase) GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            var angularFrequency = 2.0f * Mathf.PI * frequency / SampleRate;

            // Calculate the sawtooth waveform
            var waveForm = amplitude * ((initialPhase * angularFrequency) 
                / (2.0f * Mathf.PI) - Mathf.Floor((initialPhase * angularFrequency) / (2.0f * Mathf.PI)));

            var updatedPhase = initialPhase + 1.0f;

            return (waveForm, updatedPhase);
        }
    }
}