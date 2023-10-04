using UnityEngine;

namespace Synth_Engine.Modules
{
    public abstract class SynthModule: ScriptableObject
    {
        private int _sampleRate;
        
        private const float TwoPi = 2.0f * Mathf.PI;

        private void Awake()
        {
            _sampleRate = AudioSettings.outputSampleRate;
        }
        
        /// <summary>
        ///  The angular frequency of the given frequency, which represents the number of radians per second.
        /// </summary>
        /// <param name="frequency"> the current frequency of the waveform </param>
        /// <returns> float value representing the angular frequency </returns>
        protected float AngularFrequency(float frequency)
        {
            return TwoPi * frequency / _sampleRate;
        }

        /// <summary>
        ///   Generates a sample for the given frequency, amplitude and initial phase.
        /// </summary>
        /// <param name="frequency"> the frequency of the oscillation wave, which determines the note</param>
        /// <param name="amplitude"> the amplitude of the oscillation wave, which determines the volume of the note</param>
        /// <param name="initialPhase"> what phase should the wave start in </param>
        /// <returns></returns>
        public abstract (float value, float updatedPhase) GenerateSample(float frequency, float amplitude,
            float initialPhase);
    }
}