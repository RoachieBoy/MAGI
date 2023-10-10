using General.Custom_Event_Types;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace General.Activation_Buttons
{
    public class ActivateEffectButton : MonoBehaviour
    {
        private Button _base;

        [Header("What effect do I activate?")]
        [SerializeField] private AudioMixerGroup audioMixerGroup;

        [Header("Where do I need to poop this out?")]
        [SerializeField] private AudioMixerGroupUnityEvent audioMixerGroupUnityEvent;

        [Header("Debugging")]
        [SerializeField] private int clickCountDebug;

        private static ActivateEffectButton _currentlySelectedButton; 

        private void Awake()
        {
            _base = GetComponent<Button>();

            _base.onClick.AddListener(() =>
            {
                // If another button is currently selected, turn off its effect
                if (_currentlySelectedButton != null && _currentlySelectedButton != this)
                {
                    _currentlySelectedButton.clickCountDebug = 0;
                    _currentlySelectedButton.audioMixerGroupUnityEvent.Invoke(null);
                }

                // Update the currently selected button
                _currentlySelectedButton = this;

                // Increment the click count for this button
                clickCountDebug++;

                // When clicked, set the active effect to the audio mixer group
                audioMixerGroupUnityEvent.Invoke(audioMixerGroup);

                // Check if clicked twice and reset the click count
                if (clickCountDebug <= 1) return;
                
                clickCountDebug = 0;
                audioMixerGroupUnityEvent.Invoke(null);
            });
        }
    }
}