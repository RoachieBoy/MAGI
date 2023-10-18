using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace General.UI.Module_Button
{
    public class ColourManagerButtons : MonoBehaviour
    {
        private readonly Dictionary<Button, Color> _buttonDefaultColors = new();
        
        [SerializeField] private List<Button> buttons = new();
        [SerializeField] private Color selectedColor = Color.magenta;
        [SerializeField] private Button defaultButton; 

        private void Start()
        {
            // Store the default colors for each button
            foreach (var btn in buttons)
            {
                var img = btn.GetComponent<Image>();
                
                if (img != null)
                {
                    _buttonDefaultColors[btn] = img.color;

                    btn.onClick.AddListener(delegate
                    {
                        UpdateButton(btn);
                    });
                }
            }

            // Select the default button
            UpdateButton(defaultButton); 
        }

        private void UpdateButton([NotNull] Button clickedButton)
        {
            if (clickedButton == null) throw new ArgumentNullException(nameof(clickedButton));
            
            foreach (var btn in buttons)
            {
                var img = btn.GetComponent<Image>();
                
                if (img != null)
                {
                    img.color = (btn == clickedButton) ? selectedColor : _buttonDefaultColors[btn];
                }
            }
        }
    }
}