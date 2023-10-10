using UnityEngine;
 
 namespace Synth_Engine.Modules.Oscillation_Modules
 {
     [CreateAssetMenu(fileName = "Sine", menuName = "SynthModules/Oscillation/Sine")]
     public class Sine: SynthModule
     {
         public override (float value, float updatedPhase) GenerateSample(float frequency, float amplitude, float initialPhase)
         {
             // Calculate the phase increment
             var phaseIncrement = frequency / SampleRate;
             
             // Calculate the sine waveform
             var waveForm = Mathf.Sin(initialPhase * 2.0f * Mathf.PI) * amplitude;
             
             // add the next phase to the current phase to get the updated phase
             var updatedPhase = (initialPhase + phaseIncrement) % 1;
             
             return (waveForm, updatedPhase);
         }
     }
 }