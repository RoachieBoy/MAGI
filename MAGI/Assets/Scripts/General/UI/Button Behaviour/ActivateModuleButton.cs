using Synth_Engine.Modules;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Utilities.Custom_Event_Types;
using Utilities.Sound;

namespace General.UI.Button_Behaviour
{
    [RequireComponent(typeof(Button), typeof(AudioSource))]
    public class ActivateModuleButton : MonoBehaviour
    {
        [Header("Feed me a module")] 
        [SerializeField] private SynthModule synthModule;

        [Header("Feed me an effect")] 
        [SerializeField] private AudioMixerGroup audioMixerGroup;

        [Header("Where do I need to apply this effect and module?")] 
        [SerializeField] private AudioMixerGroupUnityEvent audioMixerGroupUnityEvent;

        [SerializeField] private SynthModuleUnityEvent synthModuleUnityEvent;

        [Header("What sound do I make when I'm clicked?")] 
        [SerializeField] private AudioEvent audioEvent;

        private AudioSource _audioSource;
        private Button _button;
        private SynthModule _synthModule;

        private void Awake()
        {
            _audioSource = gameObject.GetComponent<AudioSource>();
            _button = gameObject.GetComponent<Button>();

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