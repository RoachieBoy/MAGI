using Synth_Engine.Buffering_System.Buffer_Data;
using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "Triangle", menuName = "SynthModules/Oscillation/Triangle")]
    public class Triangle : SynthModule
    {
        public override SampleState GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            var phaseIncrement = frequency / SampleRate;

            var value = Mathf.PingPong(initialPhase * 2.0f, 1.0f); 
            
            value *= amplitude;
            
            initialPhase = (initialPhase + phaseIncrement) % 1;
            
            return new SampleState(value, initialPhase);
        }
    }
}