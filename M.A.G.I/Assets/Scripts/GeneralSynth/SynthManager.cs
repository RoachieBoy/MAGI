using UnityEngine;
using BasicSynthModules;
using UnityEngine.UI;
using System.Collections.Generic;

namespace GeneralSynth
{
    [RequireComponent(typeof(AudioSource))]
    public class SynthManager : MonoBehaviour
    {
        [SerializeField] private List<SynthModule> synthModules;
        [SerializeField] private List<Button> synthModuleButtons; 
        [SerializeField, Range(0f, 1f)] private float amplitude = 0.5f;
        [SerializeField, Range(0f, 20000f)] private float frequency = 440f;

        private SynthModule _activeModule; 

        private void Start()
        {
            // Set the initial active module to null so that 
            // no audio is generated until a module is selected
            _activeModule = null;
        }

        private void OnEnable()
        {
            for (var i = 0; i < synthModuleButtons.Count; i++)
            {
                var index = i; 
                synthModuleButtons[i].onClick.AddListener(() => OnSynthModuleButtonClick(index));
            }
        }

        private void OnDisable()
        {
            foreach (var t in synthModuleButtons)
            {
                t.onClick.RemoveAllListeners();
            }
        }

        private void OnAudioFilterRead(float[] data, int channels)
        {
            if (_activeModule != null) _activeModule.GenerateSamples(data, channels, frequency, amplitude);
        }

        private void OnSynthModuleButtonClick(int index)
        {
            if (index >= 0 && index < synthModules.Count) _activeModule = synthModules[index];
        }
    }
}