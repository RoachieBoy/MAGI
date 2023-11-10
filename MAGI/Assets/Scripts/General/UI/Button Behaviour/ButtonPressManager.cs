using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using General.Data_Containers;
using JetBrains.Annotations;
using Utilities;

namespace General.UI.Button_Behaviour
{
    public class ButtonPressManager : MonoBehaviour
    {
        [Header("general settings")]
        [SerializeField] private List<Button> buttons = new();
        [SerializeField] private Color selectedColor = Color.magenta;
        [SerializeField] private KeyTable keyTable;

        private readonly Dictionary<Key, Button> _buttonKeyMap = new();
        private readonly InputActionMap _inputMap = new();

        private void Start()
        {
            StoreDefaultColors();
            MapButtonsToKeys();
            
            _inputMap.Enable();
        }

        private void OnDestroy()
        {
            foreach (var btn in buttons)
            {
                btn.onClick.RemoveAllListeners();
            }
        }

        /// <summary>
        ///  Store the default starting colours of each button for later usage 
        /// </summary>
        private void StoreDefaultColors()
        {
            foreach (var btn in buttons)
            {
                var img = btn.GetComponent<Image>();

                if (img == null) continue;
                
                var index = buttons.IndexOf(btn);
                var key = keyTable[index];
                _buttonKeyMap[key] = btn;
                img.color = Color.white;
            }
        }

        /// <summary>
        ///  Create an input action map containing the correct keys and then use that go
        ///  update the buttons properly 
        /// </summary>
        private void MapButtonsToKeys()
        {
            foreach (var key in keyTable)
            {
                var keyActionName = key.ToString().ToLower();
                var action = InputActionMapsHelper.CreateInputActionMapStandard(_inputMap, keyActionName);
                
                action.started += _ => UpdateButtonColour(key);
                action.canceled += _ => ResetButtonColor(key);
            }
        }

        /// <summary>
        ///  Depending on which key is pressed, update the colour of the buttons accordingly 
        /// </summary>
        /// <param name="key"> currently pressed key </param>
        private void UpdateButtonColour(Key key)
        {
            if (!_buttonKeyMap.TryGetValue(key, out var btn)) return;
            
            var img = btn.GetComponent<Image>();
            
            if (img != null) img.color = selectedColor;
            
            ResetOtherButtonColors(btn);
        }

        /// <summary>
        ///  Reset the colour of the currently selected button.
        /// </summary>
        /// <param name="key"> which key has been pressed </param>
        private void ResetButtonColor(Key key)
        {
            if (!_buttonKeyMap.TryGetValue(key, out var btn)) return;
            
            var img = btn.GetComponent<Image>();
            if (img != null)
            {
                img.color = Color.white;
            }
        }

        /// <summary>
        ///  Reset the colours of all the other buttons 
        /// </summary>
        /// <param name="currentButton"> current button that is selected </param>
        private void ResetOtherButtonColors([NotNull] Button currentButton)
        {
            if (currentButton == null) throw new ArgumentNullException(nameof(currentButton));

            foreach (var img in from button in buttons
                     where button != currentButton
                     select button.GetComponent<Image>()
                     into img
                     where img != null
                     select img)
            {
                img.color = Color.white;
            }
        }
    }
}
