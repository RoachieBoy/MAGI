using UnityEngine;

namespace Visual_Effects.Audio_Visuals
{
    public abstract class AudioEffect: MonoBehaviour
    {
        protected bool ToTheBeat;
        
        private float _audioValue;
        private float _previousAudioValue;
        private float _timer;
        
        [Header("Effect Settings")]
        [SerializeField, Range(0, 25)] private float bias = 100f;
        [SerializeField, Range(0f, 0.5f)] private float timeStep = 0.1f;
        [SerializeField, Range(0f, 1f)] public float duration = 0.1f;

        /// <summary>
        ///   What happens when the audio value is "on beat".
        /// </summary>
        protected virtual void OnBeat()
        {
            _timer = 0f;
            ToTheBeat = true;
        }
        
        private void Update()
        {
            _previousAudioValue = _audioValue;
            _audioValue = GetSpectrumData.SpectrumValue;
            
            if (_previousAudioValue > bias && _audioValue <= bias)
            {
                if(_timer > timeStep) OnBeat();
            }
            
            if (_previousAudioValue <= bias && _audioValue > bias)
            {
                if(_timer > timeStep) OnBeat();
            }
            
            _timer += Time.deltaTime;
        }
    }
}