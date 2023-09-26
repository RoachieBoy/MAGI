using UnityEngine;

namespace BasicSynthModules
{
    public abstract class SynthModule: ScriptableObject
    {
        /// <summary>
        ///  The sample rate of the audio output, which is the number of samples per second
        /// </summary>
        protected int SampleRate;
        
        /// <summary>
        ///  The phase of the audio wave, which represents the number of radians to
        /// increment the phase by for each sample
        /// </summary>
        protected double Phase; 

        private void Awake()
        {
            SampleRate = AudioSettings.outputSampleRate;
        }

        /// <summary>
        ///   Generate samples for the given data array, channels, frequency, and amplitude.
        /// </summary>
        /// <param name="data"> audio data </param>
        /// <param name="channels"> channels to generate audio from </param>
        /// <param name="frequency"> the frequency the generated audio should have </param>
        /// <param name="amplitude"> the amplitude of the audio wave </param>
        public abstract void GenerateSamples(float[] data, int channels, float frequency, float amplitude); 
    }
}