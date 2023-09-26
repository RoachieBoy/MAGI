using System;
using BasicSynthModules;
using UnityEngine;

namespace GeneralSynth
{
    [RequireComponent(typeof(AudioSource))]
    public class SynthManager: MonoBehaviour
    {
        [SerializeField] private SynthModule synthModule;
        [SerializeField, Range(10f, 1000f)] private float frequency = 440;
        [SerializeField, Range(0f, 1f)] private float amplitude = 0.5f;
        
        private void OnAudioFilterRead(float[] data, int channels)
        {
            synthModule.GenerateSamples(data, channels, frequency, amplitude);
        }
    }
}