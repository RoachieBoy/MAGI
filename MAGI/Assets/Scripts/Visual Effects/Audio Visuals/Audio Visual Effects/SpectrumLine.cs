using UnityEngine;

namespace Visual_Effects.Audio_Visuals.Audio_Visual_Effects
{
    [RequireComponent(typeof(LineRenderer))]
    public class SpectrumLine: AudioVisualEffect
    {
        private const float MinimumYPosition = 0.01f;
        
        [Header("Line Settings")] 
        [Range(1f, 25f)] 
        [SerializeField] private float lineLength = 2;

        [Range(1f, 20f)] 
        [SerializeField] private float strength = 1;

        [Range(0, 20f)] 
        [SerializeField] private float smoothRate = 1;
        
        private LineRenderer _line;
        private float _spacing;

        public override void InitializeEffect()
        {
            _line = GetComponent<LineRenderer>();
            
            _line.positionCount = FrequencyBandCount == BandTypes.Eight
                ? (int) BandTypes.Eight
                : (int) BandTypes.SixtyFour;
            
            _spacing = lineLength / _line.positionCount;
        }

        public override void ApplyEffect()
        {
            for (var i = 0; i < _line.positionCount; i++)
            {
                var xPos = i * _spacing;

                // Get the current y position from the audio data and scale it
                var yPos = FrequencyAnalyser.GetBandAmount(FrequencyBandCount, i) * strength;

                // If there's no data, smoothly interpolate the y position to 0
                if (yPos <= MinimumYPosition) yPos = Mathf.Lerp(_line.GetPosition(i).y, 0, Time.deltaTime * smoothRate);

                var pos = new Vector3(xPos, yPos, 0);

                _line.SetPosition(i, pos);
            }
        }
    }
}