using System.Runtime.InteropServices;

namespace Synth_Engine.Buffering_System.Buffer_Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SampleState
    {
        public readonly float Sample;
        public readonly float Phase;

        public SampleState(float sample, float updatedPhase)
        {
            Sample = sample;
            Phase = updatedPhase;
        }
    }
}