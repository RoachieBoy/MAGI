using Synth_Engine.Buffering_System.Buffer_Data;
using Synth_Engine.Native;
using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "Sawtooth", menuName = "SynthModules/Oscillation/Sawtooth")]
    public class Sawtooth : SynthModule
    {
        [SerializeField] [Range(0f, 0.8f)] private float volumeModifier = 1f;

        protected override SampleState GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            return SynthesizerNative.generate_sawtooth_sample(frequency, amplitude, initialPhase, SampleRate,
                volumeModifier);
        }
    }
}