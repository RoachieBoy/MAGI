using General.Custom_Event_Types;
using Synth_Engine.Synth_Modules;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    [RequireComponent(typeof(Button))]
    public class OscillatorButton: MonoBehaviour
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