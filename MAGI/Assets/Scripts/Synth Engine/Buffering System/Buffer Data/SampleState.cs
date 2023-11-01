using System.Runtime.InteropServices;

namespace Synth_Engine.Buffering_System.Buffer_Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SampleState
    {
        public readonly float Sample;
        public readonly float Phase;

        /// <summary>
        ///     Creates a new SampleState object with the given sample and phase.
        /// </summary>
        /// <param name="sample"> generated audio sample </param>
        /// <param name="updatedPhase"> next phase of the sample </param>
        public SampleState(float sample, float updatedPhase)
        {
            Sample = sample;
            Phase = updatedPhase;
        }
    }
}