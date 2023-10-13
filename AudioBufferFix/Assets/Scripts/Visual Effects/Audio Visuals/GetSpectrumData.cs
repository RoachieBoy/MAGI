using UnityEngine;

namespace Visual_Effects.Audio_Visuals
{
    public class GetSpectrumData: MonoBehaviour
    {
        private float[] _spectrumData; 
        
        [Header("How does this interact with data?")] 
        [SerializeField] private FFTWindow fftWindow = FFTWindow.BlackmanHarris;

        [Tooltip("should be a power of 2, but default of 64 is fine")]
        [SerializeField, Range(32, 1024)] private int spectrumSize = 64;
        
        [SerializeField, Range(0, 200)] private float effectMultiplier = 100f;
        
        /// <summary>
        ///  The value of the spectrum data at the first index
        /// </summary>
        public static float SpectrumValue { get; private set; }
        
        private void Start()
        {
            _spectrumData = new float[spectrumSize];
        }

        private void Update()
        {
            AudioListener.GetSpectrumData(_spectrumData, 0, fftWindow);
            
            if(_spectrumData is {Length: > 0})
                SpectrumValue = _spectrumData[0] * effectMultiplier;
        }
    }
}