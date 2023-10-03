using System.Runtime.InteropServices;

namespace Synth_Engine.Buffering_System.Buffer_Data
{
    [StructLayout(LayoutKind.Sequential)]
    public struct StereoData
    {
        public float LeftChannel;
        public float RightChannel;
        
        /// <summary>
        /// Creates a new StereoData object with the given left and right channel values.
        /// Only to be used when the left and right channels are different.
        /// </summary>
        /// <param name="leftChannel"> left channel data </param>
        /// <param name="rightChannel"> right channel data </param>
        public StereoData(float leftChannel, float rightChannel)
        {
            LeftChannel = leftChannel;
            RightChannel = rightChannel;
        }
        
        /// <summary>
        /// Creates a new StereoData object with the given mono value.
        /// Only to be used when the left and right channels are the same.
        /// </summary>
        /// <param name="mono"> mono channel data </param>
        public StereoData(float mono)
        {
            LeftChannel = mono;
            RightChannel = mono;
        }
    }
}