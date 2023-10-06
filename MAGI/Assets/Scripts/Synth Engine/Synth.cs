using System.Collections.Generic;
using General.Data_Containers;
using Synth_Engine.Buffering_System;
using Synth_Engine.Modules;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

namespace Synth_Engine
{
    [RequireComponent(typeof(AudioSource))]
    public class Synth : MonoBehaviour
    {
        private const int SemiTones = 12;
        private float _frequency;
        private int _octaveShift;
        private AudioSource _audioSource;

        [Header("How loud am I?")] 
        [SerializeField, Range(0f, 0.5f)] private float amplitude = 0.1f;
        
        [Header("Feed me keys & frequencies!")]
        [SerializeField] private FrequencyTable frequencyTable;
        [SerializeField] private KeyTable pianoKeyTable;
        [SerializeField] private List<AudioMixerGroup> filters; 
        
        [Header("Debug View")] 
        [SerializeField] private SynthModule activeSynthDebug;
        [SerializeField] private bool isPlaying;
        [SerializeField] private InputActionMap inputActionMap;

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
                AudioBufferManager.FillPreloadAudioBuffers(frequencyTable, value.GenerateSample, Amplitude);
                
                activeSynthDebug = value;
            }
        }
        
        /// <summary>
        ///  Represents the current state that the synth is in (playing or not).
        /// </summary>
        public bool IsPlaying
        {
            get => isPlaying;
            set => isPlaying = value;
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
                AudioBufferManager.FillPreloadAudioBuffers(frequencyTable, ActiveSynth.GenerateSample, value);
                
                amplitude = value;
            }
        }
        
        #endregion

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
            
            // remap the keys to the new frequencies
            MapKeyToFrequencies();
        }

        #endregion

        #region Unity Event Functions
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _audioSource.outputAudioMixerGroup = filters[0]; 
            
            // Get index of base key in frequency table 
            // the -1 is done because the frequency table is 1-indexed
            _octaveShift = frequencyTable.BaseKeyNumber - pianoKeyTable.Count - 1;

            CreateInputActionMap();
            MapKeyToFrequencies();
            
            AudioBufferManager.InitializePreloadBuffers(frequencyTable);
        }

        private void OnDisable()
        {
            inputActionMap?.Disable();
        }
        
        private void OnAudioFilterRead(float[] data, int channels)
        {
            if (!IsPlaying) return;
            
            if(ActiveSynth == null) return;

            if (channels != 2) return; 
            
            // Generate samples for the given data array, channels, frequency, and amplitude
            AudioBufferManager.GetAudioBuffer(data);
            AudioBufferManager.FillNextAudioBuffer(ActiveSynth.GenerateSample, _frequency, Amplitude);
            AudioBufferManager.SwitchAudioBuffers();
        }
        
        #endregion

        #region Note Mapping
        
        /// <summary>
        /// Creates the input action map and binds keys to actions.
        /// </summary>
        private void CreateInputActionMap()
        {
            inputActionMap = new InputActionMap();

            foreach (var key in pianoKeyTable)
            {
                var type = inputActionMap.AddAction(key.ToString(), InputActionType.Button);
                
                type.AddBinding("<Keyboard>/" + key.ToString().ToLower(), "Hold");
            }

            inputActionMap.Enable();
        }

        /// <summary>
        /// Maps piano keys to corresponding frequencies and input actions.
        /// </summary>
        private void MapKeyToFrequencies()
        {
            for (var i = 0; i < pianoKeyTable.Count; i++)
            {
                var action = inputActionMap.FindAction(pianoKeyTable[i].ToString());
                var frequency = frequencyTable[i + _octaveShift];

                // When a key is pressed, set the frequency and start playing
                action.started += _ => { _frequency = frequency; };
            }
        }
        
        #endregion
    }
}