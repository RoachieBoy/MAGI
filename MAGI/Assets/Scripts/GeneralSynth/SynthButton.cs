using BasicSynthModules;
using UnityEngine;
using UnityEngine.UI;

namespace GeneralSynth
{
    [RequireComponent(typeof(Button))]
    public class SynthButton: MonoBehaviour
    {
        private Button _base;
    
        [Header("Feed me")] 
        [SerializeField] private SynthModule synthModule;
    
        [Header("Where do I need to regurgitate this?")]
        [SerializeField] private SynthModuleUnityEvent synthModuleUnityEvent;
    
        private void Awake()
        {
            _base = GetComponent<Button>();

            // turn off the navigation so that the button doesn't get highlighted when using WASD keys 
            var baseNavigation = _base.navigation;
            baseNavigation.mode = Navigation.Mode.None;
            _base.navigation = baseNavigation;
        
            _base.onClick.AddListener(() => synthModuleUnityEvent.Invoke(synthModule));
        }
    }
}