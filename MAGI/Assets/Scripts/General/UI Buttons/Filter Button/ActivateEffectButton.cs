using General.Custom_Event_Types;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace General.UI_Buttons.Filter_Button
{
    [RequireComponent(typeof(Button))]
    public class ActivateEffectButton : MonoBehaviour
    {
        private Button _button;

        [Header("What effect do I activate?")]
        [SerializeField] private AudioMixerGroup audioMixerGroup;

        [Header("Where do I need to apply this effect?")]
        [SerializeField] private AudioMixerGroupUnityEvent audioMixerGroupUnityEvent;

        private void Awake()
        {
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(() =>
            {
                audioMixerGroupUnityEvent.Invoke(audioMixerGroup);
            });
        }
    }
}