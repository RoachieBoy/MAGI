using UnityEngine;

namespace Visual_Effects.Audio_Visuals
{
    [CreateAssetMenu(fileName = "AudioVisualsSettings", menuName = "Visual Effects/Main Settings")]
    public class AudioVisualsSettings: ScriptableObject
    {
        [Header("Audio Settings")]
        [SerializeField, Range(0, 25)] private float bias = 100f;
        [SerializeField, Range(0f, 0.5f)] private float timeStep = 0.1f;
        [SerializeField, Range(0f, 1f)] public float duration = 0.1f;

        public float Bias => bias;
        public float TimeStep => timeStep;
        public float Duration => duration;
    }
}