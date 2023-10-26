using Synth_Engine.Buffering_System.Buffer_Data;
using Synth_Engine.Native;
using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "SquareWave", menuName = "SynthModules/Oscillation/Square")]
    public class Square : SynthModule
    {
        [SerializeField, Range(0f, 0.8f)] private float volumeModifier; 
        
        public override SampleState GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            return SynthesizerNative.generate_square_sample(frequency, amplitude, initialPhase, SampleRate,
                volumeModifier);
        }
    }
}