using Synth_Engine.Buffering_System.Buffer_Data;
using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "PulseWave", menuName = "SynthModules/Oscillation/Pulse")]
    public class Pulse : SynthModule
    {
        [SerializeField] private float dutyCycle = 0.25f;
        [SerializeField, Range(0f, 0.8f)] private float volumeModifier = 1f;

        public override SampleState GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            var phaseIncrement = frequency / SampleRate;
            
            // add volume modification to ensure that the volume is not too loud
            amplitude *= volumeModifier;

            var value = initialPhase < dutyCycle ? amplitude : -amplitude;

            var updatedPhase = (initialPhase + phaseIncrement) % 1;

            return new SampleState(value, updatedPhase);
        }
    }
}