using UnityEngine;

namespace Utilities.Sound
{
    [CreateAssetMenu(menuName = "Utilities/Sound/Simple Audio Event")]
    public class SimpleAudioEvent: AudioEvent
    {
        [SerializeField] private AudioClip clip;
        
        [Range(0, 1)]
        [SerializeField] private float volume = 1f;
        
        [Range(0, 1)]
        [SerializeField] private float pitch = 1f;
        
        public float Volume
        {
            get => volume;
            set => volume = value;
        }
        
        public override void Play(AudioSource source)
        {
            source.clip = clip;
            source.volume = Volume;
            source.pitch = pitch;
            
            source.PlayOneShot(clip);
        }
    }
}