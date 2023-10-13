using UnityEditor;

namespace Utilities.Tools
{
    #if UNITY_EDITOR
    [CustomEditor(typeof(RepeatObject))]
    public class RepeatObjectEditor : Editor
    {
        private RepeatObject _repeatObject;
        private SerializedProperty _amountOfObjects;
        private SerializedProperty _offset;

        private void Awake()
        {
            _repeatObject = (RepeatObject) target;
            _amountOfObjects = serializedObject.FindProperty("amountOfObjects");
            _offset = serializedObject.FindProperty("offset");
        }

        public override void OnInspectorGUI()
        {
            var previousAmountOfObjects = _amountOfObjects.intValue;
            var previousOffset = _offset.vector2Value;

            base.OnInspectorGUI();

            serializedObject.Update();

            if (previousAmountOfObjects != _amountOfObjects.intValue) _repeatObject.CorrectChildCount();
            if (previousOffset != _offset.vector2Value) _repeatObject.RepositionChildren();
        }
    }
#endif
}
