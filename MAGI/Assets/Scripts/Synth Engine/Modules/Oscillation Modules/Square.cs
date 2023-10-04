using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "SquareWave", menuName = "SynthModules/Oscillation/Square")]
    public class Square : SynthModule
    {
        [SerializeField] private float dutyCycle = 0.5f;
        
        private const float TwoPi = 2 * Mathf.PI;

        public override (float value, float updatedPhase) GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            // Ensure dutyCycle is within the range [0, 1]
            dutyCycle = Mathf.Clamp01(dutyCycle);

            // Calculate the waveform value
            var normalizedPhase = (initialPhase % (TwoPi)) / (TwoPi);
            
            var waveForm = normalizedPhase > dutyCycle ? 1.0f : -1.0f;

            // Update the phase by incrementing it
            var updatedPhase = initialPhase + AngularFrequency(frequency);

            return (amplitude * waveForm, updatedPhase);
        }
    }
}