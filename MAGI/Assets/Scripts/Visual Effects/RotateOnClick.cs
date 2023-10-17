using UnityEngine;

namespace Visual_Effects
{
    public class RotateOnClick : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 45.0f; 
        [SerializeField] private bool startRotating;

        private void FixedUpdate()
        {
            if (!startRotating) return;
            // Calculate the rotation in the local space of the object
            var rotationAmount = rotationSpeed * Time.deltaTime;
            
            transform.Rotate(Vector3.forward, -rotationAmount);
        }

        public void StartRotating()
        {
            startRotating = true;
        }

        public void StopRotating()
        {
            startRotating = false;
        }
    }
}