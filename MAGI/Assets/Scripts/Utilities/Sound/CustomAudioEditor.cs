using UnityEditor;
using UnityEngine;

namespace Utilities.Sound
{
#if UNITY_EDITOR

    [CustomEditor(typeof(AudioEvent), true)]
    public class CustomAudioEditor : Editor
    {
        [SerializeField] private AudioSource preview;

        private void OnEnable()
        {
            preview = EditorUtility.CreateGameObjectWithHideFlags("Audio Preview", HideFlags.HideAndDontSave,
                typeof(AudioSource)).GetComponent<AudioSource>();
        }

        private void OnDisable()
        {
            DestroyImmediate(preview.gameObject);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GUILayout.Space(10);

            EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);

            if (GUILayout.Button("Preview")) ((AudioEvent) target).Play(preview);

            GUILayout.Space(10);

            if (GUILayout.Button("Stop")) preview.Stop();

            EditorGUI.EndDisabledGroup();
        }
    }

#endif
}