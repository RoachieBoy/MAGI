using UnityEngine;

namespace Visual_Effects.Audio_Visuals
{
    [CreateAssetMenu(fileName = "SpectrumDataSettings", menuName = "Visual Effects/Spectrum Data Settings")]
    public class SpectrumDataSettings: ScriptableObject
    {
        [SerializeField] private FFTWindow fftWindow = FFTWindow.BlackmanHarris;
        [SerializeField] private int spectrumSize = 64;
        [SerializeField, Range(0, 200)] private float effectMultiplier = 100f;
        
        public FFTWindow FftWindow => fftWindow;
        public int SpectrumSize => spectrumSize;
        public float EffectMultiplier => effectMultiplier;
    }
}