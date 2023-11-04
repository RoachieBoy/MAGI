using UnityEngine;

namespace Visual_Effects.Audio_Visuals.Audio_Visual_Effects
{
    [RequireComponent(typeof(LineRenderer))]
    public class SpectrumLine : AudioVisualEffect
    {
        private const float MinimumYPosition = 0.01f;

        [Header("Line Settings")] [Range(1f, 25f)] [SerializeField]
        private float lineLength = 2;

        [Range(1f, 20f)] [SerializeField] private float strength = 1;

        [Range(0, 20f)] [SerializeField] private float smoothRate = 1;

        [Range(1f, 10f)] [SerializeField] private float maxLineLengthHeight = 1;

        private LineRenderer _line;
        private float _spacing;

        private void Start()
        {
            _line = gameObject.GetComponent<LineRenderer>();

            // set the amount of points on the line to the amount of frequency bands 
            _line.positionCount = FrequencyBandCount == BandTypes.Eight
                ? (int) BandTypes.Eight
                : (int) BandTypes.SixtyFour;

            // set the spacing between the points on the line
            _spacing = lineLength / _line.positionCount;
        }

        private void FixedUpdate()
        {
            for (var i = 0; i < _line.positionCount; i++)
            {
                var xPos = i * _spacing;

                // Get the current y position from the audio data and scale it
                var yPos = FrequencyAnalyser.GetBandAmount(FrequencyBandCount, i) * strength;

                // clamp the yPos value to stay within a maximum value
                yPos = Mathf.Clamp(yPos, MinimumYPosition, maxLineLengthHeight);

                var newYPos = Mathf.Lerp(_line.GetPosition(i).y, yPos, Time.deltaTime * smoothRate);

                // If there's no data, smoothly interpolate the y position to 0
                if (yPos <= MinimumYPosition) yPos = newYPos;

                var pos = new Vector3(xPos, yPos, 0);

                _line.SetPosition(i, pos);
            }
        }

        public override void DisableEffect()
        {
            gameObject.SetActive(false);
        }

        public override void EnableEffect()
        {
            gameObject.SetActive(true);
        }
    }
}