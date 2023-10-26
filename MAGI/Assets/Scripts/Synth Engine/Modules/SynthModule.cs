using System;
using Synth_Engine.Buffering_System.Buffer_Data;
using UnityEngine;

namespace Synth_Engine.Modules
{
    public abstract class SynthModule: ScriptableObject
    {
        /// <summary>
        ///  The sample rate of the audio system
        /// </summary>
        protected int SampleRate;
        
        /// <summary>
        ///  The function that generates the sample for the given frequency, amplitude and initial phase.
        /// </summary>
        public Func<float, float, float, SampleState> GetGenerator { get; private set; }

        private void Awake()
        {
            SampleRate = AudioSettings.outputSampleRate;
        }
        
        // Cache the function delegate to avoid the overhead of
        // allocating the delegate every time the function is called
        private void OnEnable()
        {
            GetGenerator = GenerateSample;
        }

        private void OnValidate()
        {
            GetGenerator = GenerateSample;
        }

        /// <summary>
        ///   Generates a sample for the given frequency, amplitude and initial phase.
        /// </summary>
        /// <param name="frequency"> the frequency of the oscillation wave, which determines the note</param>
        /// <param name="amplitude"> the amplitude of the oscillation wave, which determines the volume of the note</param>
        /// <param name="initialPhase"> what phase should the wave start in </param>
        protected abstract SampleState GenerateSample(float frequency, float amplitude, float initialPhase);
    }
}