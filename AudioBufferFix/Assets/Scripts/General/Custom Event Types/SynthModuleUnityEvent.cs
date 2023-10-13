using System;
using Synth_Engine.Modules;
using UnityEngine.Events;

namespace General.Custom_Event_Types
{
    [Serializable]
    public class SynthModuleUnityEvent: UnityEvent<SynthModule> {}
}