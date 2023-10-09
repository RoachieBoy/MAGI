using UnityEngine;
 
 namespace Synth_Engine.Modules.Oscillation_Modules
 {
     [CreateAssetMenu(fileName = "Sine", menuName = "SynthModules/Oscillation/Sine")]
     public class Sine: SynthModule
     {
         public override (float value, float updatedPhase) GenerateSample(float frequency, float amplitude, float initialPhase)
         {
             // Calculate the sine waveform
             var waveForm = amplitude * Mathf.Sin(AngularFrequency(frequency) * initialPhase);
             
             // add the next phase to the current phase to get the updated phase
             var updatedPhase = initialPhase + 1.0f;
             
             return (waveForm, updatedPhase);
         }
     }
 }