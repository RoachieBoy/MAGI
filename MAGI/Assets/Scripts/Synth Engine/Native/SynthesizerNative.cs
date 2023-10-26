using System.Runtime.InteropServices;
using Synth_Engine.Buffering_System.Buffer_Data;

namespace Synth_Engine.Native
{
    public static class SynthesizerNative
    {
        private const string DllName = "synthesizer_native";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern SampleState generate_sine_sample(float frequency, float amplitude, float initialPhase,
            float sampleRate);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern SampleState generate_square_sample(float frequency, float amplitude, float initialPhase,
            float sampleRate,
            float volumeModifier);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern SampleState generate_sawtooth_sample(float frequency, float amplitude, float initialPhase,
            float sampleRate,
            float volumeModifier);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern SampleState generate_triangle_sample(float frequency, float amplitude, float initialPhase,
            float sampleRate);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern SampleState generate_pulse_sample(float frequency, float amplitude, float initialPhase,
            float sampleRate,
            float volumeModifier, float dutyCycle);
    }
}