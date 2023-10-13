using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Utilities.Custom_Event_Types;

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
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
        
        /// <summary>
        ///  Removes the audio mixer group from the audio mixer group unity event
        /// </summary>
        public void RemoveAudioMixerGroup() => audioMixerGroupUnityEvent.Invoke(null);

        /// <summary>
        ///  Adds the audio mixer group to the audio mixer group unity event
        /// </summary>
        public void AddAudioMixerGroup() => audioMixerGroupUnityEvent.Invoke(audioMixerGroup);
    }
}