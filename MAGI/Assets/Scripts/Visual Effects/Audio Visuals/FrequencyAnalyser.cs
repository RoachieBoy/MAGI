using UnityEngine;

namespace Visual_Effects.Audio_Visuals
{
    [RequireComponent(typeof(AudioSource))]
    public class FrequencyAnalyser: MonoBehaviour
    {
        private const int FrequencyBins64 = 64;
        private const int FrequencyBins8 = 8;
        private const int BinAmounts = 512;
        
        private float[] _samples;
        private float[] _sampleBuffer;
        private float [] _freqBands8;
        private float [] _freqBands64;

        private readonly int[] _powerValues = {16, 32, 40, 48, 56};
        
        private AudioSource _audioSource;

        [Header("Frequency Band Settings")] 
        [SerializeField] private float smoothDownRate = 1;
        [SerializeField] private  float scalar = 1;
        [SerializeField] private FFTWindow fftWindow = FFTWindow.BlackmanHarris;
        

        private void Start()
        {
            _audioSource = gameObject.GetComponent<AudioSource>();
            
            // initialise arrays for storing frequency band data
            _freqBands8 = new float[FrequencyBins8];
            _freqBands64 = new float[FrequencyBins64];
            
            // initialise arrays for storing audio samples and sample buffer
            _samples = new float[BinAmounts];
            _sampleBuffer = new float[BinAmounts];
        }
        
        private void FixedUpdate()
        {
            _audioSource.GetSpectrumData(_samples, 0, fftWindow);

            for (var i = 0;  i < _samples.Length; i++)
            {
                // if the current sample is greater than the sample buffer, set the sample buffer to the current sample
                if (_sampleBuffer[i] < _samples[i]) _sampleBuffer[i] = _samples[i];

                else _sampleBuffer[i] -= Mathf.Lerp(_samples[i], _sampleBuffer[i], Time.deltaTime * smoothDownRate);
            }
            
            UpdateFreqBands8();
            UpdateFreqBands64();
        }

        /// <summary>
        ///  Updates the 64 frequency bands for visualisation
        /// </summary>
        private void UpdateFreqBands64()
        {
            var count = 0;
            var power = 0;
            var sampleCount = 1; 

            for (var i = 0; i < FrequencyBins64; i++)
            {
                var average = 0f;

                foreach (var value in _powerValues)
                {
                    if (i != value) continue;
                    
                    power++;
                    
                    sampleCount = (int) Mathf.Pow(2, power);
                    
                    if (power == 3) sampleCount -= 2;
                }
                
                for (var j = 0; j < sampleCount; j++)
                {
                    average += _samples[count] * (count + 1);
                    count++;
                }
                
                average /= count;
                
                _freqBands64[i] = average * scalar;
            }
        }

        /// <summary>
        ///  Updates the 8 frequency bands for visualisation
        /// </summary>
        private void UpdateFreqBands8()
        {
            var count = 0;
            
            for (var i = 0; i < FrequencyBins8; i++)
            {
                var average = 0f;
                var sampleCount = (int)Mathf.Pow(2, i) * 2;

                if (i == FrequencyBins8 - 1) sampleCount += 2;
                
                for (var j = 0; j < sampleCount; j++)
                {
                    average += _samples[count] * (count + 1);
                    count++;
                }
                
                average /= count;
                
                _freqBands8[i] = average * scalar;
            }
        }
        
        /// <summary>
        ///  Returns the amount of a given frequency band
        /// </summary>
        /// <param name="bands"> which band type is being used </param>
        /// <param name="index"> what is the current index </param>
        /// <returns></returns>
        public float GetBandAmount(BandTypes bands, int index)
        {
            return bands switch
            {
                BandTypes.Eight => _freqBands8[index],
                BandTypes.SixtyFour => _freqBands64[index],
                _ => 0
            };
        }
    }
}