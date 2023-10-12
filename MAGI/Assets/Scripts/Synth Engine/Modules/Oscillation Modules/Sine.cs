using Synth_Engine.Buffering_System.Buffer_Data;
using UnityEngine;
 
 namespace Synth_Engine.Modules.Oscillation_Modules
 {
     [CreateAssetMenu(fileName = "Sine", menuName = "SynthModules/Oscillation/Sine")]
     public class Sine: SynthModule
     {
         public override SampleState GenerateSample(float frequency, float amplitude, float initialPhase)
         {
                var phaseIncrement = frequency / SampleRate;
                
                initialPhase = (initialPhase + phaseIncrement) % 1;
                
                var sample = Mathf.Sin(initialPhase * Mathf.PI * 2) * amplitude;

                return new SampleState(sample, initialPhase);
         }
     }
 }