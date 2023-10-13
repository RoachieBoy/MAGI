using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Visual_Effects.Audio_Visuals
{
    [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
    public abstract class AudioEffect: MonoBehaviour
    {
        [Header("What are my papa settings?")]
        [SerializeField] protected AudioVisualsSettings settings;
        
        private float _audioValue;
        private float _previousAudioValue;
        private float _timer;

        /// <summary>
        ///   What happens when the audio value is "on beat"
        /// </summary>
        protected virtual void OnBeat()
        {
            _timer = 0f;
        }
        
        public void FixedUpdate()
        {
            _previousAudioValue = _audioValue;
            
            _audioValue = GetSpectrumData.SpectrumValue;

            if (Mathf.Sign(_previousAudioValue - settings.Bias) != 
                Mathf.Sign(_audioValue - settings.Bias) && _timer > settings.TimeStep)
            {
                OnBeat();
            }

            _timer += Time.deltaTime;
        }

    }
}