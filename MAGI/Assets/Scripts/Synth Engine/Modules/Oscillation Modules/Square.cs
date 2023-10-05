using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "SquareWave", menuName = "SynthModules/Oscillation/Square")]
    public class Square : SynthModule
    {
        // used to make it softer  
        [SerializeField, Range(0f, 0.8f)] private float volumeModifier; 
        
        public override (float value, float updatedPhase) GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            // Calculate the square waveform
            var waveForm = amplitude * Mathf.Sign(Mathf.Sin(AngularFrequency(frequency) * initialPhase));
            
            // Apply the volume modifier
            waveForm *= volumeModifier;
            
            // add the next phase to the current phase to get the updated phase
            var updatedPhase = initialPhase + 1.0f;
            
            return (waveForm, updatedPhase);
        }
    }
}