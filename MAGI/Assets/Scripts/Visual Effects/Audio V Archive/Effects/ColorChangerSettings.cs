using UnityEngine;

namespace Visual_Effects.Audio_Visuals.Effects
{
    [CreateAssetMenu (fileName = "ColorChangerSettings", menuName = "Visual Effects/Color Changer Settings")]
    public class ColorChangerSettings: ScriptableObject
    {
        [SerializeField] private Color[] colours; 
        public Color[] Colours => colours;
    }
}