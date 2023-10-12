using System.Runtime.InteropServices;

namespace Synth_Engine.Buffering_System.Buffer_Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AudioBuffer
    {
        public StereoData[] AudioStereoBuffer;
        public StereoData FinalPhase;
        public StereoData FinalAmplitude;
        public StereoData TargetAmplitude;
        
        public AudioBuffer(StereoData[] audioStereoBuffer, StereoData finalPhase, StereoData finalAmplitude, StereoData targetAmplitude)
        {
            AudioStereoBuffer = audioStereoBuffer;
            FinalPhase = finalPhase;
            FinalAmplitude = finalAmplitude;
            TargetAmplitude = targetAmplitude;
        }
        
        public AudioBuffer(StereoData[] audioStereoBuffer, float leftPhase, float rightPhase, float leftAmplitude, float rightAmplitude, float targetLeft, float targetRight)
        {
            AudioStereoBuffer = audioStereoBuffer;
            FinalPhase = new StereoData(leftPhase, rightPhase);
            FinalAmplitude = new StereoData(leftAmplitude, rightAmplitude);
            TargetAmplitude = new StereoData(targetLeft, targetRight);
        }
    }
}