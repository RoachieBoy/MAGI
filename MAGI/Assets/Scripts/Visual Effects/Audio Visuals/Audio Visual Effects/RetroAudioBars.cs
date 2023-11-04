using System.Collections.Generic;
using UnityEngine;

namespace Visual_Effects.Audio_Visuals.Audio_Visual_Effects
{
    public class RetroAudioBars : AudioVisualEffect
    {
        private enum BarState
        {
            Growing,
            Shrinking
        }

        [Header("Scale Settings")]
        [Range(1f, 5f)] [SerializeField] private float maxScaleY = 2.0f;
        [Range(1f, 10f)] [SerializeField] private float growingSpeed = 4.0f;
        [Range(1f, 10f)] [SerializeField] private float shrinkingSpeed = 3.0f;
        [Range(0.1f, 1f)] [SerializeField] private float minimalScaleBack = 0.1f;

        [Header("Prefab and Bars")]
        [SerializeField] private GameObject barPrefab;
        [Range(1, 50)] [SerializeField] private int numberOfBars = 10;
        [SerializeField] private float spacing = 1.0f;

        private readonly List<GameObject> _gameObjects = new();
        private Vector3 _startPosition;
        private BarState _currentState = BarState.Growing; // Start in the Growing state

        // Add a boolean to track if the bars should start growing
        private bool _shouldStartGrowing;

        private float[] _previousAmplitudes;

        private void Start()
        {
            _startPosition = transform.position;
            
            CreateBars();
            
            // start with the bars disabled
            foreach (var bar in _gameObjects)
            {
                bar.SetActive(false);
            }
            
            _shouldStartGrowing = false; 
            _previousAmplitudes = new float[numberOfBars];
        }

        protected override void UpdateEffect()
        {
            if (_shouldStartGrowing)
            {
                UpdateBars();
            }
        }

        public override void DisableEffect()
        {
            gameObject.SetActive(false);
            
            // temporarily disable the bars
            foreach (var bar in _gameObjects)
            {
                bar.SetActive(false);
            }
        }

        public override void EnableEffect()
        {
            gameObject.SetActive(true);
            
            // re-enable the bars
            foreach (var bar in _gameObjects)
            {
                bar.SetActive(true);
            }
        }

        private void CreateBars()
        {
            for (var i = 0; i < numberOfBars; i++)
            {
                var spawnPosition = new Vector3(_startPosition.x + (i * spacing), _startPosition.y + minimalScaleBack / 2, _startPosition.z); 
                var newBar = Instantiate(barPrefab, spawnPosition, Quaternion.identity);
                _gameObjects.Add(newBar);
            }
        }

        private void UpdateBars()
        {
            foreach (var bar in _gameObjects)
            {
                UpdateBar(bar);
            }
        }

        private void UpdateBar(GameObject bar)
        {
            var bandIndex = _gameObjects.IndexOf(bar);

            var currentAmplitude = FrequencyAnalyser.GetBandAmount(FrequencyBandCount, bandIndex);

            // Compare current amplitude to the previous amplitude and start growing if it changed significantly.
            if (Mathf.Abs(currentAmplitude - _previousAmplitudes[bandIndex]) > 0.1f)
            {
                _shouldStartGrowing = true;
            }

            _previousAmplitudes[bandIndex] = currentAmplitude;

            // Rest of the UpdateBar method remains the same...
            var scaleY = maxScaleY * currentAmplitude;

            // Reset the scale if it's too small to avoid the bars getting stuck
            if (scaleY < minimalScaleBack) scaleY = minimalScaleBack;

            var targetScaleY = scaleY;

            var speed = _currentState == BarState.Growing ? growingSpeed : shrinkingSpeed;

            var newYPosition = _startPosition.y + scaleY / 2;
            var positionDifference = newYPosition - bar.transform.position.y;
            var newPosition = bar.transform.position + Vector3.up * (positionDifference * Time.deltaTime * speed);
            
            bar.transform.position = newPosition;

            scaleY = Mathf.Lerp(bar.transform.localScale.y, targetScaleY, Time.deltaTime * speed);

            bar.transform.localScale = new Vector3(bar.transform.localScale.x, scaleY, bar.transform.localScale.z);

            if (Mathf.Approximately(bar.transform.localScale.y, targetScaleY))
            {
                _currentState = _currentState == BarState.Growing ? BarState.Shrinking : BarState.Growing;
            }
        }
    }
}
