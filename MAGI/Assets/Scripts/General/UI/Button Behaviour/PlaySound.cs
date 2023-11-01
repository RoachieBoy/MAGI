using UnityEngine;
using UnityEngine.UI;
using Utilities.Sound;

namespace General.UI.Button_Behaviour
{
    [RequireComponent(typeof(AudioSource), typeof(Button))]
    public class PlaySound : MonoBehaviour
    {
        [Header("What sound should I play when clicked?")] 
        [SerializeField] private SimpleAudioEvent audioEvent;

        private AudioSource _audioSource;
        private Button _button;

        private void Awake()
        {
            _audioSource = gameObject.GetComponent<AudioSource>();
            _button = gameObject.GetComponent<Button>();

            // Add a listener to the button to play the sound when clicked
            _button.onClick.AddListener(Play);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Play);
        }

        /// <summary>
        ///   Play the sound
        /// </summary>
        private void Play()
        {
            audioEvent.Play(_audioSource);
        }
    }
}