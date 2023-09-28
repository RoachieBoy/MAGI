using BasicSynthModules;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GeneralSynth
{
    [RequireComponent(typeof(AudioSource))]
    public class Synth : MonoBehaviour
    {
        private const int SemiTones = 12;
        private float _frequency;
        private int _octaveShift;

        [Header("Settings")] [SerializeField, Range(0f, 1f)]
        private float amplitude = 0.5f;
        [SerializeField] private FrequencyTable frequencyTable;
        [SerializeField] private KeyTable pianoKeyTable;
        
        [Header("Debug View")] 
        [SerializeField] private SynthModule activeSynthDebug;
        [SerializeField] private bool isPlaying = true;
        [SerializeField] private InputActionMap inputActionMap;

        /// <summary>
        /// The active synth module.
        /// </summary>
        public SynthModule ActiveSynth
        {
            get => activeSynthDebug;
            set => activeSynthDebug = value;
        }

        #region  Tonal Shifts
        
        /// <summary>
        /// Shifts the octave up by 12 semitones.
        /// </summary>
        public void ShiftOctaveUp()
        {
            // check that index can be moved 
             
            if (_octaveShift + SemiTones > frequencyTable.Count - pianoKeyTable.Count) return;
            
            // Move base index twelve semitones
            _octaveShift += SemiTones;
            MapKeyToFrequencies();
        }

        /// <summary>
        /// Shifts the octave down by 12 semitones.
        /// </summary>
        public void ShiftOctaveDown()
        {
            // check that index can be moved 
            if (_octaveShift - SemiTones < 0) return;
            
            _octaveShift -= SemiTones;
            MapKeyToFrequencies();
        }

        #endregion

        
        /// <summary>
        /// Initializes the synthesizer settings and input bindings.
        /// </summary>
        private void Start()
        {
            // Get index of base key in frequency table 
            // the -1 is done because the frequency table is 1-indexed
            _octaveShift = frequencyTable.BaseKeyNumber - pianoKeyTable.Count - 1;

            CreateInputActionMap();
            MapKeyToFrequencies();
        }

        /// <summary>
        /// Disables the input action map when the component is disabled.
        /// </summary>
        private void OnDisable()
        {
            inputActionMap?.Disable();
        }

        /// <summary>
        /// Generates audio samples using the active synth module.
        /// </summary>
        /// <param name="data">The audio sample buffer to fill.</param>
        /// <param name="channels">The number of audio channels.</param>
        private void OnAudioFilterRead(float[] data, int channels)
        {
            if (!isPlaying)
                return;

            if (activeSynthDebug == null)
                return;

            ActiveSynth.GenerateSamples(data, channels, _frequency, amplitude);
        }

        #region NoteMapping
        
        /// <summary>
        /// Creates the input action map and binds keys to actions.
        /// </summary>
        private void CreateInputActionMap()
        {
            inputActionMap = new InputActionMap();

            foreach (var key in pianoKeyTable)
            {
                var type = inputActionMap.AddAction(key.ToString(), InputActionType.Button);

                // Bind keyboard keys to actions (e.g., A for 'A', B for 'B', etc.)
                type.AddBinding("<Keyboard>/" + key.ToString().ToLower(), "Hold");
            }

            inputActionMap.Enable();
        }

        /// <summary>
        /// Maps piano keys to corresponding frequencies and input actions.
        /// </summary>
        private void MapKeyToFrequencies()
        {
            if (frequencyTable == null || pianoKeyTable == null)
                return;

            for (var i = 0; i < pianoKeyTable.Count; i++)
            {
                var action = inputActionMap.FindAction(pianoKeyTable[i].ToString());

                var frequency = frequencyTable[i + _octaveShift];

                // When a key is pressed, set the frequency and start playing
                action.performed += _ =>
                {
                    isPlaying = true;
                    _frequency = frequency;
                };

                // When the key is released, stop playing the note
                action.canceled += OnActionOnCanceled;
            }
        }

        /// <summary>
        /// Stops playing when the input action is canceled (key released).
        /// </summary>
        /// <param name="obj">The callback context for the input action.</param>
        private void OnActionOnCanceled(InputAction.CallbackContext obj)
        {
            isPlaying = false;
        }
        
        #endregion
    }
}