using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Visual_Effects.Audio_Visuals
{
    [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
    public abstract class AudioEffect: MonoBehaviour
    {
        [Header("Effect Settings")]
        [SerializeField, Range(0, 25)] private float bias = 100f;
        [SerializeField, Range(0f, 0.5f)] private float timeStep = 0.1f;
        [SerializeField, Range(0f, 1f)] public float duration = 0.1f;
        
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

            if (Mathf.Sign(_previousAudioValue - bias) != Mathf.Sign(_audioValue - bias) && _timer > timeStep)
            {
                OnBeat();
            }

            _timer += Time.deltaTime;
        }

    }
}