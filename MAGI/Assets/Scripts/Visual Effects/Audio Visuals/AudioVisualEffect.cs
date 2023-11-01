using UnityEngine;

namespace Visual_Effects.Audio_Visuals
{
    public abstract class AudioVisualEffect: MonoBehaviour
    {
        /// <summary>
        ///   Initializes the effect by getting the component(s) it needs and setting up any variables it needs.
        /// </summary>
        public abstract void InitializeEffect();
        
        /// <summary>
        ///  Applies the effect by getting the data it needs and applying it to the component(s) it needs.
        /// </summary>
        public abstract void ApplyEffect();
    }
}