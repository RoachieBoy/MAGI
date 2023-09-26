using BasicSynthModules;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GeneralSynth
{
    [RequireComponent(typeof(AudioSource))]
    public class Synth : MonoBehaviour
    {
        private InputActionMap _inputActionMap;
        private float _frequency = 440f; // TODO: Use key mapped frequencies
        
        [Header("Settings")] 
        [SerializeField, Range(0f, 1f)]
        private float amplitude = 0.5f;

        [SerializeField] 
        private bool isPlaying = true;
        
        [SerializeField]
        private FrequencyTable frequencyTable;
        
        [SerializeField]
        private KeyTable pianoKeyTable;
        
        // Key table for saving things like octave up octave down etc
        [SerializeField]
        private KeyTable transformKeyTable;
            
        [Header("Debug")] 
        [SerializeField] 
        private SynthModule activeSynthDebug;

        /// <summary>
        /// The active synth module.
        /// </summary>
        public SynthModule ActiveSynth
        {
            get => activeSynthDebug;
            set => activeSynthDebug = value;
        }

        private void OnAudioFilterRead(float[] data, int channels)
        {
            if (!isPlaying)
                return;

            if (activeSynthDebug == null)
                return;

            ActiveSynth.GenerateSamples(data, channels, _frequency, amplitude);
        }

        private void MapKeyToFrequencies()
        {
            // index 10 of key table is the same as frequency index 49 aka 440hz or A4
            
            // TODO: Programatically map keys to frequencies
        }
    }
}