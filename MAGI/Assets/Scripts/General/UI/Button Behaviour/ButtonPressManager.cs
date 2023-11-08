using System;
using System.Collections.Generic;
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

        private void MapButtonsToKeys()
        {
            foreach (var key in keyTable)
            {
                var keyActionName = key.ToString().ToLower();
                InputAction action = InputActionMapsHelper.CreateInputActionMapStandard(_inputMap, keyActionName);
                
                action.started += _ => UpdateButtonColour(key);
                action.canceled += _ => ResetButtonColor(key);
            }
        }

        private void UpdateButtonColour(Key key)
        {
            if (!_buttonKeyMap.TryGetValue(key, out var btn)) return;
            
            var img = btn.GetComponent<Image>();
            if (img != null)
            {
                img.color = selectedColor;
            }

            ResetOtherButtonColors(btn);
        }

        private void ResetButtonColor(Key key)
        {
            if (!_buttonKeyMap.TryGetValue(key, out var btn)) return;
            
            var img = btn.GetComponent<Image>();
            if (img != null)
            {
                img.color = Color.white;
            }
        }

        private void ResetOtherButtonColors([NotNull] Button currentButton)
        {
            if (currentButton == null) throw new ArgumentNullException(nameof(currentButton));

            foreach (var button in buttons)
            {
                if (button == currentButton) continue;
                
                var img = button.GetComponent<Image>();
                if (img != null)
                {
                    img.color = Color.white;
                }
            }
        }
    }
}
