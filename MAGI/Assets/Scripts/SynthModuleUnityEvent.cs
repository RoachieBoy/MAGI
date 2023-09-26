using System;
using BasicSynthModules;
using UnityEngine.Events;

[Serializable]
public class SynthModuleUnityEvent: UnityEvent<SynthModule>
{
    // Use this class to be able to serialize UnityEvents with SynthModule arguments,
    // this allows us to use the UnityEvent in the inspector
}