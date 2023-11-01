using UnityEditor;

namespace Synth_Engine
{
#if UNITY_EDITOR
    [CustomEditor(typeof(Synth))]
    public class AudioEditor : Editor
    {
    }
#endif
}