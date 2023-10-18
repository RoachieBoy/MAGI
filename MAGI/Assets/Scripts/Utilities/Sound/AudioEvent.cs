using UnityEngine;

namespace Utilities.Sound
{
    public abstract class AudioEvent: ScriptableObject
    {
        public abstract void Play(AudioSource source);
    }
}