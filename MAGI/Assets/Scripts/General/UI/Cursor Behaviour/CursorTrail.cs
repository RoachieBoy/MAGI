using UnityEngine;

namespace General.UI.Cursor_Behaviour
{
    [RequireComponent(typeof(TrailRenderer))]
    public class CursorTrail : MonoBehaviour
    {
        private const float Z = 10f;

        private Camera _mainCamera;

        // Get mouse cursor and make trail follow it
        private TrailRenderer _trailRenderer;

        private void Start()
        {
            // Get the trail renderer
            _trailRenderer = GetComponent<TrailRenderer>();

            // Get the main camera
            _mainCamera = Camera.main;
        }

        private void FixedUpdate()
        {
            var mousePosition = Input.mousePosition;

            // arbitrary z value
            mousePosition.z = Z;

            // Convert the screen coordinates to world coordinates
            var worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);

            // Update the trail renderer's position to follow the mouse
            _trailRenderer.transform.position = worldPosition;
        }
    }
}