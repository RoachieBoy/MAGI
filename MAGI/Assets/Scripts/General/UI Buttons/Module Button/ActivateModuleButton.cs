using Synth_Engine.Modules;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Custom_Event_Types;

namespace General.UI_Buttons.Module_Button
{
    [RequireComponent(typeof(Button))]
    public class ActivateModuleButton : MonoBehaviour
    {
        private Button _button;
        private SynthModule _synthModule;
        
        [Header("Feed me")]
        [SerializeField] private SynthModule synthModule;

        [Header("Where do I need to apply this effect?")]
        [SerializeField] private SynthModuleUnityEvent synthModuleUnityEvent;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(() => { synthModuleUnityEvent.Invoke(synthModule);});
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}