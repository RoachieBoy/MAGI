using Synth_Engine.Buffering_System.Buffer_Data;
using Synth_Engine.Native;
using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "Triangle", menuName = "SynthModules/Oscillation/Triangle")]
    public class Triangle : SynthModule
    {
        public override SampleState GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            return SynthesizerNative.generate_triangle_sample(frequency, amplitude, initialPhase, SampleRate);
        }
    }
}