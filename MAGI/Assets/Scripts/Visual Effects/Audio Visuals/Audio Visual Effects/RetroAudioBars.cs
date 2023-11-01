using System.Collections.Generic;
using UnityEngine;

namespace Visual_Effects.Audio_Visuals.Audio_Visual_Effects
{
    public class RetroAudioBars : MonoBehaviour
    {
        [Header("General Settings")] 
        [SerializeField] private FrequencyAnalyser frequencyAnalyser;

        [SerializeField] private BandTypes frequencyBandCount = BandTypes.SixtyFour;

        [Header("Scale Settings")] 
        [Range(1f, 5f)]
        [SerializeField] private float maxScaleY = 2.0f;

        [Range(1f, 10f)] 
        [SerializeField] private float growingSpeed = 4.0f;

        [Range(1f, 10f)] 
        [SerializeField] private float shrinkingSpeed = 3.0f;

        [Header("Bar settings")] 
        [SerializeField] private GameObject barPrefab;

        [Range(1f, 20f)] [SerializeField] private int numberOfBars = 10;

        [SerializeField] private float spacing = 1.0f;
        private readonly List<GameObject> _gameObjects = new();
        private readonly List<Vector3> _positions = new();

        private bool _growing;

        private void Start()
        {
            for (var i = 0; i < numberOfBars; i++)
            {
                var spawnPosition = transform.position + Vector3.right * (i * spacing);
                var newBar = Instantiate(barPrefab, spawnPosition, Quaternion.identity);

                _gameObjects.Add(newBar);
                _positions.Add(newBar.transform.localScale);
            }
        }

        private void FixedUpdate()
        {
            for (var i = 0; i < _gameObjects.Count; i++)
            {
                var scaleY = maxScaleY * frequencyAnalyser.GetBandAmount(frequencyBandCount, i);

                // Use Lerp to smoothly transition the scale
                var targetScaleY = _growing ? scaleY : 0.1f;
                var speed = _growing ? growingSpeed : shrinkingSpeed;
                scaleY = Mathf.Lerp(_gameObjects[i].transform.localScale.y, targetScaleY, Time.deltaTime * speed);

                var scale = new Vector3(_positions[i].x, scaleY, _positions[i].z);

                // Update the position to keep the bottom of the bars fixed
                _gameObjects[i].transform.localScale = scale;
                _gameObjects[i].transform.position = new Vector3(_gameObjects[i].transform.position.x, scaleY / 2.0f,
                    _gameObjects[i].transform.position.z);

                // Check if bars should switch between growing and shrinking
                if (Mathf.Approximately(_gameObjects[i].transform.localScale.y, targetScaleY)) _growing = !_growing;
            }
        }
    }
}