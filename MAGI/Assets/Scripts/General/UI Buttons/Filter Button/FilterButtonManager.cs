using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace General.UI_Buttons.Filter_Button
{
    public class FilterButtonManager : MonoBehaviour
    {
        private readonly Dictionary<Button, Image> _buttonImageMap = new();
        
        private Button _selectedButton;
        private Color _defaultColor;

        [Header("Who are my friends?")]
        [SerializeField] private List<Button> buttons;

        [Header("What color do I use when I'm selected?")]
        [SerializeField] private Color selectedColor = Color.cyan;
        
        [Header("Which button should be selected by default?")]
        [SerializeField] private Button defaultButton;
        
        private void Start()
        {
            buttons.ForEach(button =>
            {
                var image = button.GetComponent<Image>();

                // If the button has an image, add it to the map
                if (image != null) _buttonImageMap[button] = image;

                // set the default color of the button
                _defaultColor = image.color;

                // Add a listener to the button to toggle its selected state
                button.onClick.AddListener(() => SwitchState(button));
            });
        }

        /// <summary>
        ///  Toggles the selected state of the button when clicked
        /// </summary>
        /// <param name="clickedButton">The button that was clicked.</param>
        private void SwitchState(Button clickedButton)
        {
            if (_selectedButton == clickedButton)
            {
                // Deselect the currently selected button if it was clicked again
                DeselectButton(_selectedButton);
                
                // Set the selected button to null since it was deselected
                _selectedButton = null;
            }
            else
            {
                // Deselect the previously selected button 
                if (_selectedButton != null) DeselectButton(_selectedButton);

                // Select the newly clicked button
                SelectButton(clickedButton);
                
                // Set the selected button to the newly clicked button
                _selectedButton = clickedButton;
            }
        }
        
        /// <summary>
        ///  Selects the button by changing its color
        /// </summary>
        /// <param name="button"> the button that has been selected </param>
        private void SelectButton(Button button)
        {
            if (!_buttonImageMap.TryGetValue(button, out var image)) return;
            
            image.color = selectedColor;
                
            var effect = button.GetComponent<ActivateEffectButton>();
                
            // If the button has an effect, activate it
            if (effect != null) effect.AddAudioMixerGroup();
        }

        /// <summary>
        ///  Deselects the button by changing its color
        /// </summary>
        /// <param name="button"> the button to deselect </param>
        private void DeselectButton(Button button)
        {
            if (!_buttonImageMap.TryGetValue(button, out var image)) return;
            
            image.color = _defaultColor;

            var effect = button.GetComponent<ActivateEffectButton>(); 
            
            // If the button has an effect, deactivate it
            if (effect != null) effect.RemoveAudioMixerGroup();
        }
    }
}
