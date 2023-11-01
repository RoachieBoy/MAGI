using System;
using UnityEngine;

namespace Visual_Effects.Audio_Visuals
{
    public class AudioVisualsManager: MonoBehaviour
    {
        [Header("General Settings")]
        [SerializeField] private AudioVisualEffect defaultEffect;
        
        [Header("Debugging View")]
        [SerializeField] private AudioVisualEffect activeEffect;
        
        /// <summary>
        ///  The currently active effect.
        /// </summary>
        public AudioVisualEffect ActiveEffect
        {
            get => activeEffect;
            set => activeEffect = value;
        }

        private void Start()
        {
            activeEffect = defaultEffect;
            activeEffect.InitializeEffect();
        }

        private void FixedUpdate()
        {
            ActiveEffect.ApplyEffect();
        }
    }
}