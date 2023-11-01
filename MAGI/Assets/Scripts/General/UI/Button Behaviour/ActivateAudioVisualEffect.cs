using UnityEngine;
using UnityEngine.UI;
using Utilities.Custom_Event_Types;
using Visual_Effects.Audio_Visuals;

namespace General.UI.Button_Behaviour
{
    [RequireComponent(typeof(Button))]
    public class ActivateAudioVisualEffect: MonoBehaviour
    {
        [Header("feed me an effect")]
        [SerializeField] private AudioVisualEffect effect;
        
        [Header("Where do I need to apply this effect?")] 
        [SerializeField] private AudioVisualEffectUnityEvent applyEffectEvent;
        
        private Button _button;
        private AudioVisualEffect _effect;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => { applyEffectEvent.Invoke(effect); });
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}