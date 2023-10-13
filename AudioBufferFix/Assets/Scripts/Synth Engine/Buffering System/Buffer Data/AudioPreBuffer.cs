using System.Runtime.InteropServices;

namespace Synth_Engine.Buffering_System.Buffer_Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AudioPreBuffer
    {
        public StereoData[] AudioStereoBuffer;
        public StereoData FinalPhase;
        public StereoData FinalAmplitude;
        public StereoData TargetAmplitude;
        public float AttackTime;

        /// <summary>
        ///  Creates a new AudioPreBuffer object with the given parameters
        /// </summary>
        /// <param name="audioStereoBuffer"> audio stereo buffer to use </param>
        /// <param name="finalPhase"> what is the final phase of the generated sample </param>
        /// <param name="finalAmplitude"> what is the final amplitude of the generated sample </param>
        /// <param name="targetAmplitude"> what is the desired amplitude to work towards </param>
        /// <param name="attackTime"> how long does the attack phase stay active </param>
        public AudioPreBuffer(
            StereoData[] audioStereoBuffer,
            StereoData finalPhase,
            StereoData finalAmplitude,
            StereoData targetAmplitude,
            float attackTime
        )
        {
            AudioStereoBuffer = audioStereoBuffer;
            FinalPhase = finalPhase;
            FinalAmplitude = finalAmplitude;
            TargetAmplitude = targetAmplitude;
            AttackTime = attackTime;
        }
    }
}