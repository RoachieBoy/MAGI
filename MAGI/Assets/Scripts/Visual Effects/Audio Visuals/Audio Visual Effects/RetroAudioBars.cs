using System;
using System.Collections.Generic;
using UnityEngine;

namespace Visual_Effects.Audio_Visuals.Audio_Visual_Effects
{
    public class RetroAudioBars : AudioVisualEffect
    {
        [Header("Prefab and Bars")] [SerializeField]
        private GameObject barPrefab;

        [Range(1, 50)] [SerializeField] private int numberOfBars = 10;

        [Header("Bar Settings")] [Range(0.1f, 1)] [SerializeField]
        private float spacing = 1.0f;

        [Range(1, 10)] [SerializeField] private float barSpeed = 2.0f;
        [Range(1, 10)] [SerializeField] private float scaleFactor = 5.0f;
        [Range(1, 10)] [SerializeField] private float maxHeight = 1.0f;
        [Range(0.1f, 1)] [SerializeField] private float minHeight = 0.1f;

        private readonly List<GameObject> _bars = new();
        private Vector3 _initialPosition;

        private void Start()
        {
            _initialPosition = transform.position;

            for (int i = 0; i < numberOfBars; i++)
            {
                var bar = Instantiate(barPrefab, transform);

                bar.transform.position = new Vector3(_initialPosition.x + (spacing * i),
                    _initialPosition.y, _initialPosition.z);

                _bars.Add(bar);
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _bars.Count; i++)
            {
                var bar = _bars[i];
                var yPos = FrequencyAnalyser.GetBandAmount(FrequencyBandCount, i) * scaleFactor;

                // Clamp the yPos value to stay within a maximum value
                yPos = Mathf.Clamp(yPos, minHeight, maxHeight);

                // Calculate the bottom position of the bar
                var bottomPosition = bar.transform.position - Vector3.up * bar.transform.localScale.y / 2;

                // Smoothly interpolate the bar's height using Lerp
                var currentScale = bar.transform.localScale;
                var targetScale = new Vector3(currentScale.x, yPos, currentScale.z);

                // Ensure the bars only grow from the bottom and shrink from the top
                currentScale = Vector3.Lerp(currentScale, targetScale, Time.deltaTime * barSpeed);

                // Update the position to keep the top of the bar fixed
                bar.transform.position = bottomPosition + Vector3.up * currentScale.y / 2;
                bar.transform.localScale = currentScale;
            }
        }

        public override void DisableEffect()
        {
            gameObject.SetActive(false);
            BarState(false);
        }

        public override void EnableEffect()
        {
            gameObject.SetActive(true);
            BarState(true);
        }

        /// <summary>
        ///   Sets the state of the bars
        /// </summary>
        /// <param name="state"> the state of the bar </param>
        private void BarState(bool state)
        {
            foreach (var bar in _bars)
            {
                bar.SetActive(state);
            }
        }
    }
}