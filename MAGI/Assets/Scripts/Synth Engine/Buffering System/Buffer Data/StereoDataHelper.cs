using System;

namespace Synth_Engine.Buffering_System.Buffer_Data
{
    public static class StereoDataHelper
    {
        /// <summary>
        ///     Copies the data from an array of StereoData to an array of floats.
        /// </summary>
        /// <param name="stereoData"> the stereo data to copy </param>
        /// <param name="outputData"> the output data of floats </param>
        /// <exception cref="ArgumentNullException"> throws exception if the arrays of data are null </exception>
        /// <exception cref="ArgumentException"> throws exception of the array is the wrong size </exception>
        public static void CopyToFloatArray(this StereoData[] stereoData, float[] outputData)
        {
            if (stereoData == null)
                throw new ArgumentNullException(nameof(stereoData));

            if (outputData == null)
                throw new ArgumentNullException(nameof(outputData));

            if (outputData.Length < stereoData.Length * 2)
                throw new ArgumentException("The output data array is too small to hold the stereo data.");

            unsafe
            {
                // Pinning both stereoData and outputFloatArray simultaneously
                fixed (StereoData* pStereoData = &stereoData[0])
                fixed (float* pData = &outputData[0])
                {
                    // Directly copying memory for maximum efficiency
                    Buffer.MemoryCopy(
                        pStereoData,
                        pData,
                        outputData.Length * sizeof(float),
                        stereoData.Length * sizeof(StereoData)
                    );
                }
            }
        }
    }
}