using UnityEngine;

namespace Utilities.Sound
{
    public abstract class AudioEvent: ScriptableObject
    {
        /// <summary>
        ///  Play the audio event on the given audio source
        /// </summary>
        /// <param name="source"> source from which the audio event should play </param>
        public abstract void Play(AudioSource source);
    }
}