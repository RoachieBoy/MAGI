using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "PulseWave", menuName = "SynthModules/Oscillation/Pulse")]
    public class Pulse : SynthModule
    {
        // Duty cycle, a value between 0 and 1 where 0 means always low, and 1 means always high.
        public float dutyCycle = 0.25f;

        // used to make it softer  
        [SerializeField, Range(0f, 0.8f)] private float volumeModifier; 

        public override (float value, float updatedPhase) GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            // Calculate the pulse waveform with the specified duty cycle
            var waveForm = Mathf.Sin(AngularFrequency(frequency) * initialPhase) < (1.0f - dutyCycle) ? -amplitude : amplitude;
            
            // Make it softer
            waveForm *= volumeModifier;

            // Increment the phase
            var updatedPhase = initialPhase + 1.0f;

            return (waveForm, updatedPhase);
        }
    }
}