using System;
using UnityEngine.Events;
using Visual_Effects.Audio_Visuals;

namespace Utilities.Custom_Event_Types
{
    [Serializable]
    public class AudioVisualEffectUnityEvent: UnityEvent<AudioVisualEffect>{}
}