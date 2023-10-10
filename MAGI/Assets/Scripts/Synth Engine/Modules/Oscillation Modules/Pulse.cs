﻿using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "PulseWave", menuName = "SynthModules/Oscillation/Pulse")]
    public class Pulse : SynthModule
    {
        // Duty cycle, a value between 0 and 1 where 0 means always low, and 1 means always high.
        [SerializeField] private float dutyCycle = 0.25f;
        
        [SerializeField, Range(0f, 0.8f)] private float volumeModifier = 1f;

        public override (float value, float updatedPhase) GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            var phaseIncrement = frequency / SampleRate;
            
            // add volume modification to ensure that the volume is not too loud
            amplitude *= volumeModifier;

            var value = initialPhase < dutyCycle ? amplitude : -amplitude;

            var updatedPhase = (initialPhase + phaseIncrement) % 1;

            return (value, updatedPhase);
        }
    }
}