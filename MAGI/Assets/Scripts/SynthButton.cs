using BasicSynthModules;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SynthButton: MonoBehaviour
{
    private Button _base;
    
    [Header("Feed me Mommy")] [SerializeField]
    private SynthModule synthModule;
    
    [Header("Where do I need to regurgitate?")] [SerializeField]
    private SynthModuleUnityEvent synthModuleUnityEvent;
    
    private void Awake()
    {
        _base = GetComponent<Button>();
        
        _base.onClick.AddListener(() => synthModuleUnityEvent.Invoke(synthModule));
    }
}