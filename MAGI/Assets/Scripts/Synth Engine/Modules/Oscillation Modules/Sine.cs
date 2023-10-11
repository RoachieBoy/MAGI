using UnityEngine;
 
 namespace Synth_Engine.Modules.Oscillation_Modules
 {
     [CreateAssetMenu(fileName = "Sine", menuName = "SynthModules/Oscillation/Sine")]
     public class Sine: SynthModule
     {
         public override (float value, float updatedPhase) GenerateSample(float frequency, float amplitude, float initialPhase)
         {
                var updatedPhase = initialPhase + (frequency / SampleRate);
                
                var sample = Mathf.Sin(initialPhase * Mathf.PI * 2) * amplitude;
                
                return (sample, updatedPhase);
         }
     }
 }