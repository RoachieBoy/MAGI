using UnityEngine;

namespace Visual_Effects.Audio_Visuals_New
{
    [RequireComponent(typeof(LineRenderer))]
    public class SpectrumLineRenderer: MonoBehaviour
    {
        [Header("Analyser Settings")]
        [SerializeField] private FrequencyAnalyser frequencyAnalyser;
        [SerializeField] private BandTypes frequencyBandCount;
        
        [Header("Line Settings")]
        [Range(1f, 25f)]
        [SerializeField] private float lineLength = 2;
        
        [Range(1f, 20f)]
        [SerializeField] private float strength = 1;
        
        [Range(0, 20f)]
        [SerializeField] private float smoothRate = 1;
        
        private float _spacing = .2f;
        private LineRenderer _line;

        private const float MinimumYPosition = 0.01f; 

        private void Start()
        {
            _line = gameObject.GetComponent<LineRenderer>();

            _line.positionCount = frequencyBandCount == BandTypes.Eight ? (int) BandTypes.Eight : (int)BandTypes.SixtyFour;
            
            _spacing = lineLength / _line.positionCount;
        }

        private void FixedUpdate()
        {
            for (var i = 0; i < _line.positionCount; i++)
            {
                var xPos = i * _spacing;

                // Get the current y position from the audio data and scale it
                var yPos = frequencyAnalyser.GetBandAmount(frequencyBandCount, i) * strength;

                // If there's no data, smoothly interpolate the y position to 0
                if (yPos <= MinimumYPosition)
                {
                    yPos = Mathf.Lerp(_line.GetPosition(i).y, 0, Time.deltaTime * smoothRate);
                }

                var pos = new Vector3(xPos, yPos, 0);

                _line.SetPosition(i, pos);
            }
        }
    }
}