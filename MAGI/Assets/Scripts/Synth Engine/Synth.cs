using System.Linq;
using General.Data_Containers;
using Synth_Engine.Buffering_System;
using Synth_Engine.Modules;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using Utilities;
using Utilities.Custom_Event_Types;

namespace Synth_Engine
{
    [RequireComponent(typeof(AudioSource))]
    public class Synth : MonoBehaviour
    {
        private const int SemiTones = 12;
        
        private readonly InputActionMap _octaveShiftDownActionMap = new();
        private readonly InputActionMap _octaveShiftUpActionMap = new();

        private float _frequency;
        private int _frequencyIndex;
        private int _currentPressedKeyIndex;

        #region Serialized Fields
        
        [HideInInspector] public StringUnityEvent onNoteChanged;

        [Header("How loud am I?")] 
        [SerializeField] [Range(0f, 1f)] private float amplitude = 0.5f;

        [Header("Default Synth")]
        [SerializeField] private SynthModule defaultSynth;

        [Header("Feed me important objects!")] 
        [SerializeField] private FrequencyDictionary frequencyDictionary;

        [SerializeField] private KeyTable pianoKeyTable;

        [Header("ADSR Values (envelope)")] 
        [SerializeField] private float attackTime = 0.9f;
        
        [Header("Debug View")] 
        [SerializeField] private SynthModule activeSynthDebug;
        [SerializeField] private bool isPlaying;
        [SerializeField] private InputActionMap pianoKeyMap = new();
        #endregion

        #region Public Properties

        /// <summary>
        ///     The active synth module that is selected 
        /// </summary>
        public SynthModule ActiveSynth
        {
            get => activeSynthDebug;
            set
            {
                // fill the audio buffers with the new synth module 
                AudioBufferManager.FillPreloadAudioBuffers(
                    frequencyDictionary,
                    attackTime,
                    value.GetGenerator,
                    Amplitude
                );

                activeSynthDebug = value;
            }
        }

        /// <summary>
        ///     The active effect that is applied to the synth
        /// </summary>
        public AudioMixerGroup ActiveEffect
        {
            set => gameObject.GetComponent<AudioSource>().outputAudioMixerGroup = value;
        }

        /// <summary>
        ///     Represents the current state that the synth is in (playing or not)
        /// </summary>
        public bool IsPlaying
        {
            get => isPlaying;
            set => isPlaying = value;
        }

        /// <summary>
        ///  Represents the currently playing frequency of the synthesizer 
        /// </summary>
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
        ///     The amplitude of the synth aka the volume toggle.
        /// </summary>
        public float Amplitude
        {
            get => amplitude;
            set
            {
                // fill the audio buffers with the new amplitude value
                AudioBufferManager.FillPreloadAudioBuffers(
                    frequencyDictionary,
                    attackTime,
                    ActiveSynth.GetGenerator,
                    value
                );

                amplitude = value;
            }
        }

        /// <summary>
        ///     The note that is currently being played.
        /// </summary>
        public string Note
        {
            set => onNoteChanged.Invoke(value);
        }

        #endregion

        #region Note Behaviour
        
        /// <summary>
        ///   Shifts the octave by moving the frequency index a given amount 
        /// </summary>
        /// <param name="amount"> what amount should I shift the frequency index </param>
        private void ShiftOctave(int amount)
        {
            // Move base index twelve semitones
            _frequencyIndex += amount;
            
            // remap the keys to the new frequencies
            MapKeyToFrequencies();

            // update immediately to the new frequency
            Frequency = frequencyDictionary.Keys.ElementAt(_frequencyIndex + _currentPressedKeyIndex);
            
            Note = frequencyDictionary[Frequency];
        }

        /// <summary>
        ///     Shifts the octave up by 12 semitones.
        /// </summary>
        public void ShiftOctaveUp()
        {
            // check that index can be moved 
            if (_frequencyIndex + SemiTones > frequencyDictionary.Count - pianoKeyTable.Count)
                return;

            ShiftOctave(SemiTones);
        }

        /// <summary>
        ///     Shifts the octave down by 12 semitones.
        /// </summary>
        public void ShiftOctaveDown()
        {
            // check that index can be moved 
            if (_frequencyIndex - SemiTones < 0) return;

            ShiftOctave(-SemiTones);
        }

        #endregion

        #region Unity Event Functions

        private void Start()
        {
            // ensure that the frequency index is set to the base note of the software piano to start 
            _frequencyIndex = (int) (frequencyDictionary.BaseKeyNumber - frequencyDictionary.BaseNote);

            CreateInputActionMap();
            MapKeyToFrequencies();
            MapOctaveKeys();

            AudioBufferManager.InitializePreloadBuffers(frequencyDictionary);
            
            ActiveSynth = defaultSynth;
        }

        private void OnDisable()
        {
            pianoKeyMap?.Disable();
            _octaveShiftUpActionMap?.Disable();
            _octaveShiftDownActionMap?.Disable();
        }

        private void OnAudioFilterRead(float[] data, int channels)
        {
            if (!IsPlaying) return;

            if (channels != 2) return;

            // Generate samples for the given data array, channels, frequency, and amplitude
            AudioBufferManager.GetAudioBuffer(data);
            AudioBufferManager.FillNextAudioBuffer(Frequency, ActiveSynth.GetGenerator);
            AudioBufferManager.SwitchAudioBuffers();
        }

        #endregion

        #region Key & Note Mapping

        /// <summary>
        ///     Maps the octave shift keys to their corresponding actions.
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
        ///     Creates the input action map and binds keys to actions.
        /// </summary>
        private void CreateInputActionMap()
        {
            foreach (var key in pianoKeyTable)
                InputActionMapsHelper.CreateInputActionMapStandard(pianoKeyMap, key.ToString().ToLower());

            pianoKeyMap.Enable();
        }

        /// <summary>
        ///     Maps piano keys to corresponding frequencies and input actions
        ///     Also responsible for setting correct note and frequency 
        /// </summary>
        private void MapKeyToFrequencies()
        {
            for (var i = 0; i < pianoKeyTable.Count; i++)
            {
                var action = pianoKeyMap.FindAction(pianoKeyTable[i].ToString());
                var frequency = frequencyDictionary.Keys.ElementAt(i + _frequencyIndex);
                var note = frequencyDictionary[frequency];

                var index = i;
                
                action.started += _ =>
                {
                    Frequency = frequency;
                    Note = note;
                    _currentPressedKeyIndex = index; 
                };
            }
        }
        #endregion
    }
}