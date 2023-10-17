using Synth_Engine.Modules;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Utilities.Custom_Event_Types;

namespace General.UI.Module_Button
{
    [RequireComponent(typeof(Button))]
    public class ActivateModuleButton : MonoBehaviour
    {
        private Button _button;
        private SynthModule _synthModule;
        
        [Header("Feed me a module")]
        [SerializeField] private SynthModule synthModule;
        
        [Header("Feed me an effect")]
        [SerializeField] private AudioMixerGroup audioMixerGroup;

        [Header("Where do I need to apply this effect and module?")]
        [SerializeField] private AudioMixerGroupUnityEvent audioMixerGroupUnityEvent;
        [SerializeField] private SynthModuleUnityEvent synthModuleUnityEvent;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(() =>
            {
                synthModuleUnityEvent.Invoke(synthModule);
                audioMixerGroupUnityEvent.Invoke(audioMixerGroup);
            });
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}