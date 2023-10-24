#nullable enable
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace General.UI.Buttons
{
    public class ColourManagerButtons : MonoBehaviour
    {
        private readonly Dictionary<Button, Color> _buttonDefaultColors = new();
        
        [SerializeField] private List<Button> buttons = new();
        [SerializeField] private Color selectedColor = Color.magenta;
        [SerializeField] private Button? defaultButton; 

        private void Start()
        {
            StoreButtonColours();
            
            if (defaultButton != null) UpdateButton(defaultButton);
        }
        
        private void OnDestroy()
        {
            foreach (var btn in buttons) btn.onClick.RemoveAllListeners();
        }
        
        /// <summary>
        ///  Stores the default color of each button and adds a listener to each button
        /// </summary>
        private void StoreButtonColours()
        {
            foreach (var btn in buttons)
            {
                var img = btn.GetComponent<Image>();

                if (img == null) continue;
                
                _buttonDefaultColors[btn] = img.color;
                
                btn.onClick.AddListener(delegate
                {
                    UpdateButton(btn);
                });
            }
        }

        /// <summary>
        ///   Updates the color of the clicked button and resets the color of the other buttons
        /// </summary>
        /// <param name="clickedButton"> current button that is being clicked </param>
        private void UpdateButton(Button clickedButton)
        {
            if (clickedButton == null) throw new ArgumentNullException(nameof(clickedButton));
            
            foreach (var btn in buttons)
            {
                var img = btn.GetComponent<Image>();
                
                if (img != null) img.color = (btn == clickedButton) ? selectedColor : _buttonDefaultColors[btn];
            }
        }
    }
}