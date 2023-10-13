using System.Collections;
using UnityEngine;

namespace Visual_Effects.Audio_Visuals.Effects
{
    public class ScaleEffect: AudioEffect
    {
        [Header("Scale Settings")]
        [SerializeField] private Vector3 minSize = Vector3.one;
        [SerializeField] private Vector3 maxSize = Vector3.one * 2;
        
        private Vector3 _currentScale;
        
        private void Start()
        {
            _currentScale = minSize;
            
            transform.localScale = _currentScale;
        }
        
        /// <summary>
        ///  Coroutine to change the scale of the sprite renderer
        /// </summary>
        /// <param name="newScale"> new target scale for the object </param>
        private IEnumerator ScaleCoroutine(Vector3 newScale)
        {
            var timer = 0f;
            
            while (_currentScale != newScale)
            {
               _currentScale = Vector3.Lerp(_currentScale, newScale, timer / duration);
               
               timer += Time.deltaTime;

               transform.localScale = _currentScale;

               yield return null; 
            }
            transform.localScale = minSize;
        }
        
        
        protected override void OnBeat()
        {
            base.OnBeat();
            
            var newScale = new Vector3(Random.Range(minSize.x, maxSize.x), Random.Range(minSize.y, maxSize.y));

            StopCoroutine(nameof(ScaleCoroutine));
            StartCoroutine(nameof(ScaleCoroutine), newScale);
        }
    }
}