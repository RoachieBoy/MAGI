using UnityEngine;

namespace Visual_Effects.Audio_Visuals
{
    public class GetSpectrumData: MonoBehaviour
    {
        private float[] _spectrumData; 
        
        [Header("What settings to use")]
        [SerializeField] private SpectrumDataSettings settings;
        
        /// <summary>
        ///  The value of the spectrum data at the first index
        /// </summary>
        public static float SpectrumValue { get; private set; }
        
        private void Start()
        {
            _spectrumData = new float[settings.SpectrumSize];
        }

        private void Update()
        {
            AudioListener.GetSpectrumData(_spectrumData, 0, settings.FftWindow);
            
            if(_spectrumData is {Length: > 0})
                SpectrumValue = _spectrumData[0] * settings.EffectMultiplier;
        }
    }
}