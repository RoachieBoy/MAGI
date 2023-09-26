using UnityEngine;

namespace BasicSynthModules
{
    public abstract class SynthModule : ScriptableObject
    {
        protected int SampleRate;
        protected float Phase;

        private void Awake()
        {
            SampleRate = AudioSettings.outputSampleRate;
            
            // Make sure the phase is reset when the synth module is created
            Phase = 0;
        }
        
        /// <summary>
        /// Generate samples for the given data array, channels, frequency, and amplitude.
        /// </summary>
        /// <param name="data">audio data</param>
        /// <param name="channels">channels to generate audio from</param>
        /// <param name="frequency">the frequency the generated audio should have</param>
        /// <param name="amplitude">the amplitude of the audio wave</param>
        public abstract void GenerateSamples(float[] data, int channels, float frequency, float amplitude);
    }
}