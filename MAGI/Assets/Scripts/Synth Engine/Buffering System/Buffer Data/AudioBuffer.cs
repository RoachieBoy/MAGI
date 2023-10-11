using System.Runtime.InteropServices;

namespace Synth_Engine.Buffering_System.Buffer_Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AudioBuffer
    {
        public StereoData[] AudioStereoBuffer;
        public StereoData FinalPhase;
        
        public AudioBuffer(StereoData[] audioStereoBuffer, StereoData finalPhase)
        {
            AudioStereoBuffer = audioStereoBuffer;
            FinalPhase = finalPhase;
        }
        
        public AudioBuffer(StereoData[] audioStereoBuffer, float leftPhase, float rightPhase)
        {
            AudioStereoBuffer = audioStereoBuffer;
            FinalPhase = new StereoData(leftPhase, rightPhase);
        }
    }
}