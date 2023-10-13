using System.Collections;
using UnityEngine;

namespace Visual_Effects.Audio_Visuals.Effects
{
    public class ColourChanger : AudioEffect
    {
        [SerializeField] private Color[] colours;
        
        private Color _currentColor;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            
            _currentColor = colours[0];
            
            _spriteRenderer.color = _currentColor;
        }

        /// <summary>
        ///  Coroutine to change the color of the sprite renderer
        /// </summary>
        /// <param name="target"> target colour </param>
        private IEnumerator MoveToColour(Color target)
        {
            var timer = 0f;
            
            var currentColor = _spriteRenderer.color;

            while (currentColor != target)
            {
                currentColor = Color.Lerp(currentColor, target, timer / duration);
                
                _spriteRenderer.color = currentColor;
                
                timer += Time.deltaTime;
                
                yield return null;
            }
        }

        protected override void OnBeat()
        {
            base.OnBeat();

            // Choose a random color from the 'colours' array
            var randomColorIndex = Random.Range(0, colours.Length);
            
            var newColour = colours[randomColorIndex];

            StopCoroutine(nameof(MoveToColour));
            StartCoroutine(nameof(MoveToColour), newColour);
        }
    }
}