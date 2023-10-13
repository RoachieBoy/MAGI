using Synth_Engine.Buffering_System.Buffer_Data;
using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "SquareWave", menuName = "SynthModules/Oscillation/Square")]
    public class Square : SynthModule
    {
        [SerializeField, Range(0f, 0.8f)] private float volumeModifier; 
        
        public override SampleState GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            var phaseIncrement = frequency / SampleRate;

            var value = Mathf.Sign(Mathf.Sin(initialPhase * 2.0f * Mathf.PI));

            var volume = amplitude * volumeModifier;
            
            value *= volume;
            
            var updatedPhase = (initialPhase + phaseIncrement) % 1;
            
            return new SampleState(value, updatedPhase);
        }
    }
}