using System;
using BasicSynthModules;
using UnityEngine.Events;

namespace GeneralSynth
{
    [Serializable]
    public class SynthModuleUnityEvent: UnityEvent<SynthModule>
    {
        // Use this class to be able to serialize UnityEvents with SynthModule arguments,
        // this allows us to use the UnityEvent in the inspector
        
        // In the case of the Synth can be used to activate a given module 
    }
}