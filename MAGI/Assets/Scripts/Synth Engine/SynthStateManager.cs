using General.Data_Containers;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;
using Utilities.Custom_Event_Types;

namespace Synth_Engine
{
    public class SynthStateManager : MonoBehaviour
    {
        [Header("General Settings")] [SerializeField]
        private KeyTable pianoKeyTable;

        [SerializeField] private BoolUnityEvent isPlaying;

        [Header("Debugging viewer")]
        [SerializeField] private int keysPressed;

        [SerializeField] private InputActionMap inputActionMap = new();

        private int KeysPressed
        {
            get => keysPressed;
            set
            {
                keysPressed = value < 0 ? 0 : value;
                isPlaying.Invoke(value > 0);
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
        ///     Maps the keys to the input action map with the correct bindings.
        /// </summary>
        private void MapKeys()
        {
            foreach (var key in pianoKeyTable)
            {
                var action = InputActionMapsHelper.CreateInputActionMapStandard(inputActionMap,
                    key.ToString().ToLower());

                action.started += _ => { KeysPressed++; };
                action.canceled += _ => { KeysPressed--; };
            }

            inputActionMap.Enable();
        }
    }
}