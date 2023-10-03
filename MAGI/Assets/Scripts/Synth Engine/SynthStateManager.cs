using General.Custom_Event_Types;
using General.Data_Containers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Synth_Engine
{
    public class SynthStateManager: MonoBehaviour
    {   
        
        [SerializeField] private KeyTable pianoKeyTable;
        [SerializeField] private BoolUnityEvent isPlaying;

        [Header("Debugging viewer")] 
        [SerializeField] private int keysPressed;
        [SerializeField] private InputActionMap inputActionMap = new(); 
        
        // used for debugging 
        private int KeysPressed
        {
            get => keysPressed;
            set 
            {
                if (value < 0) keysPressed = 0;
                isPlaying.Invoke(value > 0);
                keysPressed = value;
            }
        }

        private void Start()
        {
            MapKeys();
        }

        private void OnDisable()
        {
            inputActionMap.Disable();
        }

        /// <summary>
        ///  Maps the keys to the input action map with the correct bindings.
        /// </summary>
        private void MapKeys()
        {
            foreach (var key in pianoKeyTable)
            {
                var action = inputActionMap.AddAction(key.ToString(), InputActionType.Button);

                action.AddBinding("<Keyboard>/" + key.ToString().ToLower(), "Hold");
                
                action.started += _ => { KeysPressed++; };
                action.canceled += _ => { KeysPressed--; };
            }
            
            inputActionMap.Enable();
        }
    }
}