using BasicSynthModules;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GeneralSynth
{
    [RequireComponent(typeof(AudioSource))]
    public class Synth : MonoBehaviour
    {
        private InputActionMap _inputActionMap;
        private float _frequency; 
        
        [Header("Settings")] 
        [SerializeField, Range(0f, 1f)]
        private float amplitude = 0.5f;

        [SerializeField] 
        private bool isPlaying = true;
        
        [SerializeField]
        private FrequencyTable frequencyTable;
        
        [SerializeField]
        private KeyTable pianoKeyTable;
        
        [SerializeField]
        private InputActionAsset inputActionAsset;
        
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
        
        private void Start()
        {
            MapKeyToFrequencies();
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
            if (frequencyTable == null || pianoKeyTable == null) return;
            
            _inputActionMap = inputActionAsset.FindActionMap("White Keys");
            
            // Iterate through the keys in the pianoKeyTable and map input actions to frequencies
            for (var i = 0; i < pianoKeyTable.Count; i++)
            {
                var inputActionName = $"Key {i}";
                
                var inputAction = _inputActionMap.FindAction(inputActionName);
                
                // If the input exists, then we can map it to a frequency
                if (inputAction != null)
                {
                    var frequency = frequencyTable[i];
                    
                    // Map the frequency to the input action
                    inputAction.performed += context => { _frequency = frequency; };
                    
                    // If the key is released, then we set the frequency to 0 again 
                    inputAction.canceled += context => { _frequency = 0f; };
                }
                else
                {
                    Debug.Log($"Input action {inputActionName} not found.");
                }
            }
            
            _inputActionMap.Enable();
        }
    }
}
