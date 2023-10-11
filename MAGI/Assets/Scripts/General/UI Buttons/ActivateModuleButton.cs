using General.Custom_Event_Types;
using Synth_Engine.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace General.UI_Buttons
{
    [RequireComponent(typeof(Button))]
    public class ActivateModuleButton : MonoBehaviour
    {
        private Button _button;
        private Image _image;
        private SynthModule _synthModule;
        
        private static ActivateModuleButton _current;

        [Header("Feed me")]
        [SerializeField] private SynthModule synthModule;

        [Header("Where do I need to apply this effect?")]
        [SerializeField] private SynthModuleUnityEvent synthModuleUnityEvent;

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
            if (_current != null && _current != this)
            {
                _current.Deselect();
            }

            Select();
        }

        private void Select()
        {
            _current = this;
            synthModuleUnityEvent.Invoke(synthModule);
            _image.color = colorSelected;
        }

        private void Deselect()
        {
            _image.color = colorUnselected;
        }
    }
}