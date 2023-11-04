using System.Collections.Generic;
using UnityEngine;

namespace Visual_Effects.Audio_Visuals
{
    public class AudioVisualsManager: MonoBehaviour
    {
        [Header("Starting effect")]
        [SerializeField] private AudioVisualEffect defaultEffect;
        
        [Header("What are all my effects?")]
        [SerializeField] private List<AudioVisualEffect> effects;

        [Header("Debugging")] 
        [SerializeField] private AudioVisualEffect activeEffect;

        public AudioVisualEffect ActiveEffect
        {
            set
            {
                activeEffect = value;
                ActivateEffect(activeEffect);
            }
        }
        
        private void Start()
        {
            // enable the default effect
            ActiveEffect = defaultEffect;
            
            // Disable all effects except the default one
            foreach (var effect in effects)
            {
                if (effect == defaultEffect) continue;
                effect.DisableEffect();
            }
        }
        
        /// <summary>
        ///  Activates the given effect and deactivates all other effects.
        /// </summary>
        /// <param name="effect"> the effect to activate </param>
        private void ActivateEffect(AudioVisualEffect effect)
        {
            foreach (var e in effects)
            {
                if (e == effect) e.EnableEffect();
                else e.DisableEffect();
            }
        }
    }
}