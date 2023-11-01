using UnityEngine;

namespace Visual_Effects.Audio_Visuals
{
    public class EffectManager: MonoBehaviour
    {
        [SerializeField] private AudioVisualEffect activeEffect;
        
        public AudioVisualEffect ActiveEffect
        {
            get => activeEffect;
            set => activeEffect = value;
        }
        
        private void FixedUpdate()
        {
            if (activeEffect == null) return;
            ActiveEffect.ApplyEffect();
        }
    }
}