using UnityEngine;

namespace General.UI
{
    public class CustomCursor: MonoBehaviour
    {
        private Vector2 _targetPosition;
        private Camera _camera;

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