using General;
using General.Data_Containers;
using Synth_Engine.Buffering_System;
using Synth_Engine.Modules;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using Utilities.Custom_Event_Types;

namespace Synth_Engine
{
    [RequireComponent(typeof(AudioSource))]
    public class Synth : MonoBehaviour
    {
        private const int SemiTones = 12;

        private float _frequency;
        private int _octaveShift;

        public StringUnityEvent onNoteChanged; 

        private readonly InputActionMap _octaveShiftUpActionMap = new();
        private readonly InputActionMap _octaveShiftDownActionMap = new();

        [Header("How loud am I?")] 
        [SerializeField, Range(0f, 1f)] private float amplitude = 0.5f;

        [Header("Default Synth")] 
        [SerializeField] private SynthModule defaultSynth;

        [Header("Feed me important objects!")] 
        [SerializeField] private FrequencyTable frequencyTable;
        [SerializeField] private KeyTable pianoKeyTable;

        [Header("ADSR Values (envelope)")] 
        [SerializeField] private float attackTime = 0.9f;

        [Header("Debug View")] 
        [SerializeField] private SynthModule activeSynthDebug;
        [SerializeField] private bool isPlaying;
        [SerializeField] private InputActionMap inputActionMap = new();

        #region Public Properties

        /// <summary>
        /// The active synth module
        /// </summary>
        public SynthModule ActiveSynth
        {
            get => activeSynthDebug;
            set
            {
                // fill the audio buffers with the new synth module 
                AudioBufferManager.FillPreloadAudioBuffers(
                    frequencyTable,
                    attackTime,
                    value.GenerateSample,
                    Amplitude
                );

                activeSynthDebug = value;
            }
        }

        /// <summary>
        ///  The active effect that is applied to the synth
        /// </summary>
        public AudioMixerGroup ActiveEffect
        {
            set => GetComponent<AudioSource>().outputAudioMixerGroup = value;
        }

        /// <summary>
        ///  Represents the current state that the synth is in (playing or not)
        /// </summary>
        public bool IsPlaying
        {
            get => isPlaying;
            set => isPlaying = value;
        }

        private float Frequency
        {
            get => _frequency;
            set
            {
                _frequency = value;
                AudioBufferManager.SetPreloadAudioBuffer(value);
            }
        }

        /// <summary>
        ///  The amplitude of the synth aka the volume toggle.
        /// </summary>
        public float Amplitude
        {
            get => amplitude;
            set
            {
                // fill the audio buffers with the new amplitude value
                AudioBufferManager.FillPreloadAudioBuffers(
                    frequencyTable,
                    attackTime,
                    ActiveSynth.GenerateSample,
                    value
                );

                amplitude = value;
            }
        }
        
        /// <summary>
        ///  The note that is currently being played.
        /// </summary>
        public string Note
        {
            set => onNoteChanged.Invoke(value);
        }

        #endregion

        #region Note Behaviour

        /// <summary>
        /// Shifts the octave up by 12 semitones.
        /// </summary>
        public void ShiftOctaveUp()
        {
            // check that index can be moved 
            if (_octaveShift + SemiTones > frequencyTable.Count - pianoKeyTable.Count) return;

            // Move base index twelve semitones
            _octaveShift += SemiTones;

            // update immediately to the new frequency
            Frequency = frequencyTable[_octaveShift];

            // remap the keys to the new frequencies
            MapKeyToFrequencies();
        }

        /// <summary>
        /// Shifts the octave down by 12 semitones.
        /// </summary>
        public void ShiftOctaveDown()
        {
            // check that index can be moved 
            if (_octaveShift - SemiTones < 0) return;

            // Move base index twelve semitones
            _octaveShift -= SemiTones;

            // update immediately to the new frequency
            Frequency = frequencyTable[_octaveShift];

            // remap the keys to the new frequencies
            MapKeyToFrequencies();
        }

        #endregion

        #region Unity Event Functions

        private void Start()
        {
            _octaveShift = frequencyTable.BaseKeyNumber - pianoKeyTable.Count - 1; 

            CreateInputActionMap();
            MapKeyToFrequencies();
            MapOctaveKeys();

            AudioBufferManager.InitializePreloadBuffers(frequencyTable);

            // Set the default synth
            ActiveSynth = defaultSynth;
        }

        private void OnDisable()
        {
            inputActionMap?.Disable();
            _octaveShiftUpActionMap?.Disable();
            _octaveShiftDownActionMap?.Disable();
        }

        private void OnAudioFilterRead(float[] data, int channels)
        {
            if (!IsPlaying)
                return;

            if (channels != 2)
                return;

            // Generate samples for the given data array, channels, frequency, and amplitude
            AudioBufferManager.GetAudioBuffer(data);
            AudioBufferManager.FillNextAudioBuffer(Frequency, ActiveSynth.GenerateSample);
            AudioBufferManager.SwitchAudioBuffers();
        }

        #endregion

        #region Key & Note Mapping

        /// <summary>
        ///  Maps the octave shift keys to their corresponding actions.
        /// </summary>
        private void MapOctaveKeys()
        {
            // up and down keys are mapped to the octave shift functions
            InputActionMapsHelper.CreateInputActionMapWithActionMethod(
                _octaveShiftUpActionMap, 
                ShiftOctaveUp,
                "upArrow"
                );
            
            InputActionMapsHelper.CreateInputActionMapWithActionMethod(
                _octaveShiftDownActionMap, 
                ShiftOctaveDown,
                "downArrow"
                );
        }

        /// <summary>
        /// Creates the input action map and binds keys to actions.
        /// </summary>
        private void CreateInputActionMap()
        {
            foreach (var key in pianoKeyTable)
                InputActionMapsHelper.CreateInputActionMapStandard(inputActionMap, key.ToString().ToLower());

            inputActionMap.Enable();
        }

        /// <summary>
        /// Maps piano keys to corresponding frequencies and input actions
        /// </summary>
        private void MapKeyToFrequencies()
        {
            for (var i = 0; i < pianoKeyTable.Count; i++)
            {
                var action = inputActionMap.FindAction(pianoKeyTable[i].ToString());

                // Get the frequency of the key
                var frequency = frequencyTable[_octaveShift + i];

                // When a key is pressed, set the frequency 
                var index = i;
                
                action.started += _ =>
                {
                     Frequency = frequency;
                     
                     Note = frequencyTable.GetNote(_octaveShift + index);
                };
            }
        }

        #endregion
    }
}