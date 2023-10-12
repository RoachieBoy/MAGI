using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace General.UI_Buttons.Module_Button
{
    public class ButtonContainerManager : MonoBehaviour
    {
        private Color _defaultColor; 
        private readonly Color _selectedColor = Color.magenta;
        private readonly Dictionary<Button, Image> _buttonImageMap = new();
        
        [SerializeField] private List<Button> buttons = new();
        [SerializeField] private Button defaultButton;

        private void Start()
        {
            foreach (var btn in buttons)
            {
                var img = btn.GetComponent<Image>();
                
                if (img == null) continue;
                
                _buttonImageMap[btn] = img;

                // Store the default color from the first button 
                if (_defaultColor == default) _defaultColor = img.color;
                
                btn.onClick.AddListener(delegate { UpdateButtonColor(btn); });
            }
            
            // Select the default button
            UpdateButtonColor(defaultButton);
        }

        /// <summary>
        ///  Selects the button by changing its color
        /// </summary>
        /// <param name="clickedButton"> the button that has just been clicked </param>
        private void UpdateButtonColor(Object clickedButton)
        {
            foreach (var (key, value) in _buttonImageMap)
            {
                value.color = key == clickedButton ? _selectedColor : _defaultColor;
            }
        }
    }
}