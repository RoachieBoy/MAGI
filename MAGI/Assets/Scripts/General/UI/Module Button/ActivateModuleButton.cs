using Synth_Engine.Modules;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Utilities.Custom_Event_Types;
using Utilities.Sound;

namespace General.UI.Module_Button
{
    [RequireComponent(typeof(Button), typeof(AudioSource))]
    public class ActivateModuleButton : MonoBehaviour
    {
        private Button _button;
        private SynthModule _synthModule;
        private AudioSource _audioSource;
        
        [Header("Feed me a module")]
        [SerializeField] private SynthModule synthModule;
        
        [Header("Feed me an effect")]
        [SerializeField] private AudioMixerGroup audioMixerGroup;

        [Header("Where do I need to apply this effect and module?")]
        [SerializeField] private AudioMixerGroupUnityEvent audioMixerGroupUnityEvent;
        [SerializeField] private SynthModuleUnityEvent synthModuleUnityEvent;
        
        [Header("What sound do I make when I'm clicked?")]
        [SerializeField] private AudioEvent audioEvent;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(() =>
            {
                synthModuleUnityEvent.Invoke(synthModule);
                audioMixerGroupUnityEvent.Invoke(audioMixerGroup);
                audioEvent.Play(_audioSource);
            });
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}