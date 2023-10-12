using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace General.UI_Buttons.Filter_Button
{
    public class ButtonSelectionManager : MonoBehaviour
    {
        private readonly Dictionary<Button, Image> _buttonImageMap = new();
        
        private Button _selectedButton;
        private Color _defaultColor;

        [Header("Who are my friends?")]
        [SerializeField] private List<Button> buttons;

        [Header("What color do I use when I'm selected?")]
        [SerializeField] private Color selectedColor = Color.cyan;
        
        private void Start()
        {
            buttons.ForEach(button =>
            {
                var image = button.GetComponent<Image>();

                if (image != null) _buttonImageMap[button] = image;

                _defaultColor = image.color;

                button.onClick.AddListener(() => ToggleButton(button));
            });
        }

        /// <summary>
        /// Toggles the selected state of the button when clicked.
        /// </summary>
        /// <param name="clickedButton">The button that was clicked.</param>
        private void ToggleButton(Button clickedButton)
        {
            if (_selectedButton == clickedButton)
            {
                // Deselect the currently selected button
                DeselectButton(_selectedButton);
                _selectedButton = null;
            }
            else
            {
                // Deselect the previously selected button (if any)
                if (_selectedButton != null)
                {
                    DeselectButton(_selectedButton);
                }

                // Select the newly clicked button
                SelectButton(clickedButton);
                _selectedButton = clickedButton;
            }
        }
        
        /// <summary>
        ///  Selects the button by changing its color
        /// </summary>
        /// <param name="button"></param>
        private void SelectButton(Button button)
        {
            if (_buttonImageMap.TryGetValue(button, out var image))
                image.color = selectedColor;
        }

        /// <summary>
        ///  Deselects the button by changing its color
        /// </summary>
        /// <param name="button"> the button to deselect </param>
        private void DeselectButton(Button button)
        {
            if (_buttonImageMap.TryGetValue(button, out var image))
                image.color = _defaultColor;
        }
    }
}
