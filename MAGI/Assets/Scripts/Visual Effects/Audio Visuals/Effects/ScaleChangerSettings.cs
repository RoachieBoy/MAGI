using UnityEngine;

namespace Visual_Effects.Audio_Visuals.Effects
{
    [CreateAssetMenu (fileName = "ScaleChangerSettings", menuName = "Visual Effects/Scale Changer Settings")]
    public class ScaleChangerSettings: ScriptableObject
    {
        [SerializeField] private Vector3 minSize = Vector3.one;
        [SerializeField] private Vector3 maxSize = Vector3.one * 2;
        
        public Vector3 MinSize => minSize;
        public Vector3 MaxSize => maxSize;
    }
}