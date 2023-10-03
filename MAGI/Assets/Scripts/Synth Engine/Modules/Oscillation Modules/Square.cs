using UnityEngine;

namespace Synth_Engine.Modules.Oscillation_Modules
{
    [CreateAssetMenu(fileName = "SquareWave", menuName = "SynthModules/Oscillation/Square")]
    public class Square : SynthModule
    {
        [SerializeField] private float dutyCycle = 0.5f;

        public override (float value, float updatedPhase) GenerateSample(float frequency, float amplitude, float initialPhase)
        {
            var angularFrequency = 2.0f * Mathf.PI * frequency / SampleRate;

            var threshold = dutyCycle % 1.0f;
            
            var waveForm = amplitude * (Mathf.Sin(angularFrequency * initialPhase) > threshold ? 1.0f : -1.0f);
            
            var updatedPhase = initialPhase + 1.0f;
            
            return (waveForm, updatedPhase);
        }
    }
}