using UnityEngine;

namespace General.UI.Cursor_Behaviour
{
    public class CustomCursor : MonoBehaviour
    {
        private Camera _camera;
        private Vector2 _targetPosition;

        private void Awake()
        {
            _camera = Camera.main;

            Cursor.visible = false;
        }

        private void FixedUpdate()
        {
            _targetPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            transform.position = _targetPosition;
        }
    }
}