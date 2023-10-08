using General.Custom_Event_Types;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace General
{
    public class ActivateEffectButton: MonoBehaviour
    {
        private Button _base;
        private int _clickCount;
            
        [SerializeField] private AudioMixerGroup audioMixerGroup;
        
        [SerializeField] private AudioMixerGroupUnityEvent audioMixerGroupUnityEvent;
        
        private void Awake()
        {
            _base = GetComponent<Button>();
            _base.onClick.AddListener(() => audioMixerGroupUnityEvent.Invoke(audioMixerGroup));
            
            // when clicked, set the active effect to the audio mixer group
            _base.onClick.AddListener(() => _clickCount++);
            
            // when clicked twice, set the active effect to null
            _base.onClick.AddListener(() =>
            {
                if (_clickCount == 2)
                {
                    audioMixerGroupUnityEvent.Invoke(null);
                    _clickCount = 0;
                }
            });
        }
    }
}