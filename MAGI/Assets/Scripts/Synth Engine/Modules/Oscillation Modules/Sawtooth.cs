using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "Sawtooth", menuName = "SynthModules/Oscillation/Sawtooth")]
    public class Sawtooth : SynthModule
    {
        // The offset of the sawtooth wave which determines the symmetry of the wave
        [SerializeField] private float waveOffset = 0.5f;
            
        private const float TwoPi = 2 * Mathf.PI;
        
        public override (float value, float updatedPhase) GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            // wrap the phase between 0 and 2pi
            var phase = initialPhase % (TwoPi); 
            
            // Calculate the sawtooth waveform
            var waveForm = amplitude * ((phase / (TwoPi)) - waveOffset);

            // Update the phase by incrementing it
            var updatedPhase = (phase + AngularFrequency(frequency)) % (TwoPi);

            return (waveForm, updatedPhase);
        }

    }
}