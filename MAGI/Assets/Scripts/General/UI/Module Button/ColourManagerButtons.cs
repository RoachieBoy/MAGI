using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace General.UI.Module_Button
{
    public class ColourManagerButtons : MonoBehaviour
    {
        private Color _defaultColor;
        private readonly Dictionary<Button, Image> _buttonImageMap = new();
        
        [SerializeField] private List<Button> buttons = new();
        [SerializeField] private Button defaultButton;
        [SerializeField] private Color selectedColor = Color.magenta;

        private void Start()
        {
            foreach (var btn in buttons)
            {
                var img = btn.GetComponent<Image>();

                if (img == null) continue;

                _buttonImageMap[btn] = img;

                // Store the default color of the first button
                if (_defaultColor == default) _defaultColor = img.color;

                btn.onClick.AddListener(delegate
                {
                    UpdateButton(btn);
                });
            }

            // Select the default button
            UpdateButton(defaultButton);
        }

        private void UpdateButton(Object clickedButton)
        {
            foreach (var (key, value) in _buttonImageMap)
            {
                value.color = key == clickedButton ? selectedColor : _defaultColor;
            }
        }
    }
}
