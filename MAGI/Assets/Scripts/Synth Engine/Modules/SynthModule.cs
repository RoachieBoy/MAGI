using UnityEngine;

namespace Synth_Engine.Modules
{
    public abstract class SynthModule: ScriptableObject
    {
        /// <summary>
        ///  The sample rate of the audio system
        /// </summary>
        protected int SampleRate;

        private void Awake()
        {
            SampleRate = AudioSettings.outputSampleRate;
        }
        
        /// <summary>
        ///   Generates a sample for the given frequency, amplitude and initial phase.
        /// </summary>
        /// <param name="frequency"> the frequency of the oscillation wave, which determines the note</param>
        /// <param name="amplitude"> the amplitude of the oscillation wave, which determines the volume of the note</param>
        /// <param name="initialPhase"> what phase should the wave start in </param>
        public abstract (float value, float updatedPhase) GenerateSample(float frequency, float amplitude,
            float initialPhase);
    }
}