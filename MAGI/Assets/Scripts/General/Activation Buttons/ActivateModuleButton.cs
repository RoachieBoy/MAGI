using General.Custom_Event_Types;
using Synth_Engine.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace General.Activation_Buttons
{
    [RequireComponent(typeof(Button))]
    public class ActivateModuleButton: MonoBehaviour
    {
        private Button _base;
    
        [Header("Feed me")] 
        [SerializeField] private SynthModule synthModule;
    
        [Header("Where do I need to poop this out?")]
        [SerializeField] private SynthModuleUnityEvent synthModuleUnityEvent;
    
        private void Awake()
        {
            _base = GetComponent<Button>();
            _base.onClick.AddListener(() => synthModuleUnityEvent.Invoke(synthModule));
        }
    }
}