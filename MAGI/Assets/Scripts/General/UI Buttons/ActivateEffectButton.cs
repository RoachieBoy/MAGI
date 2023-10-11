using General.Custom_Event_Types;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace General.UI_Buttons
{
    [RequireComponent(typeof(Button))]
    public class ActivateEffectButton : MonoBehaviour
    {
        private Button _button;
        private Image _image;
        private int _clickCount;

        private static ActivateEffectButton _currentlySelectedButton;

        [Header("What effect do I activate?")]
        [SerializeField] private AudioMixerGroup audioMixerGroup;

        [Header("Where do I need to apply this effect?")]
        [SerializeField] private AudioMixerGroupUnityEvent audioMixerGroupUnityEvent;

        [Header("What color do I need to be?")]
        [SerializeField] private Color colorSelected;
        [SerializeField] private Color colorUnselected;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();

            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if (_currentlySelectedButton != null && _currentlySelectedButton != this)
                _currentlySelectedButton.Disable();
            
            Enable();
            
            if (_clickCount > 1) Disable();
        }

        private void Enable()
        {
            _image.color = colorSelected;
            _currentlySelectedButton = this;
            _clickCount++;
            audioMixerGroupUnityEvent.Invoke(audioMixerGroup);
        }

        private void Disable()
        {
            _image.color = colorUnselected;
            _clickCount = 0;
            audioMixerGroupUnityEvent.Invoke(null);
        }
    }
}