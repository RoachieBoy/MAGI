using System;
using UnityEngine.Audio;
using UnityEngine.Events;

namespace Utilities.Custom_Event_Types
{
    [Serializable]
    public class AudioMixerGroupUnityEvent : UnityEvent<AudioMixerGroup>
    {
    }
}