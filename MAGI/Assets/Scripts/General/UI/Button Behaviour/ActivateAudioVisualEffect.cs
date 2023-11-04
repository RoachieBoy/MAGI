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
        
        [Header("Where do I need to activate this effect?")]
        [SerializeField] private AudioVisualEffectUnityEvent onEffectActivated;
        
        private Button _button;
        
        private void Awake()
        {
            _button = gameObject.GetComponent<Button>();
        }
        
        private void OnEnable()
        {   
            _button.onClick.AddListener(ActivateEffect);
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(ActivateEffect);
        }
        
        private void ActivateEffect()
        {
            onEffectActivated.Invoke(effect);
        }
    }
}