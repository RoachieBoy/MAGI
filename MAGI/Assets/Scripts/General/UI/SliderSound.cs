using UnityEngine;
using UnityEngine.UI;
using Utilities.Sound;

namespace General.UI
{
    [RequireComponent(typeof(AudioSource), typeof(Slider))]
    public class SliderSound : MonoBehaviour
    {
        private Slider _slider;
        private AudioSource _audioSource;
        private float _previousValue;
        private float _initialVolume;

        [Header("What sound should I play when dragged?")]
        [SerializeField] private SimpleAudioEvent audioEvent;
        
        [Header("Debugging")]
        [SerializeField] private bool isInteracting;

        private void Start()
        {
            _slider = GetComponent<Slider>();
            _audioSource = GetComponent<AudioSource>();
            _previousValue = _slider.value;
            _initialVolume = audioEvent.Volume;
            
            _slider.onValueChanged.AddListener(delegate { OnDragValueChanged(); });
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(delegate { OnDragValueChanged(); });
        }

        private void OnDragValueChanged()
        {
            if (!isInteracting) return;

            // Calculate the change in slider value
            var valueChange = _slider.value - _previousValue;

            // Adjust the volume based on the direction of change
            const float volumeChange = 0.05f;
            const float volumeScalingFactor = 0.1f; 

            // update the volume of the audio event going up or down with a scaling factor
            audioEvent.Volume = valueChange > 0 ? volumeChange * volumeScalingFactor : -volumeChange * volumeScalingFactor;
            
            audioEvent.Play(_audioSource);
            
            // Update the previous value
            _previousValue = _slider.value;
            
        }


        private void Update()
        {
            isInteracting = Input.GetMouseButton(0); 

            if (isInteracting) return;

            // If not interacting, stop the sound and reset the previous value
            _audioSource.Stop();
            _previousValue = _slider.value;
            audioEvent.Volume = _initialVolume;
        }
    }
}