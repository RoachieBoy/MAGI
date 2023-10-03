using UnityEngine;

namespace Synth_Engine.Modules
{
    public abstract class SynthModule: ScriptableObject
    {
        protected int SampleRate;

        private void Awake()
        {
            SampleRate = AudioSettings.outputSampleRate;
        }

        public abstract (float value, float updatedPhase) GenerateSample(float frequency, float amplitude,
            float initialPhase);
    }
}