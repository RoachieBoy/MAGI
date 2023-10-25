using System.Collections;
using UnityEngine;

namespace Visual_Effects.Audio_Visuals.Effects
{
    public class ScaleEffect: AudioEffect
    {
        [Header("Scale Settings")] 
        [SerializeField] private ScaleChangerSettings scaleSettings; 
        
        private Vector3 _currentScale;
        
        private void Start()
        {
            _currentScale = scaleSettings.MinSize;
            
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
               _currentScale = Vector3.Lerp(_currentScale, newScale, timer / settings.Duration);
               
               timer += Time.deltaTime;

               transform.localScale = _currentScale;

               yield return null; 
            }
            transform.localScale = scaleSettings.MinSize;
        }
        
        
        protected override void OnBeat()
        {
            base.OnBeat();
            
            var newScale = new Vector3(Random.Range(scaleSettings.MinSize.x, scaleSettings.MaxSize.x), 
                Random.Range(scaleSettings.MinSize.y, scaleSettings.MaxSize.y));

            StopCoroutine(nameof(ScaleCoroutine));
            StartCoroutine(nameof(ScaleCoroutine), newScale);
        }
    }
}