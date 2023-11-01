using UnityEngine;

namespace Visual_Effects
{
    public class RandomRotation : MonoBehaviour
    {
        [Header("Rotation Settings")] [Range(1f, 500f)] [SerializeField]
        private float rotationSpeed = 50f;

        [SerializeField] private bool randomDirection;

        private void Start()
        {
            if (!randomDirection) return;

            var direction = Random.Range(0, 2);

            if (direction == 1) rotationSpeed *= -1;
        }

        private void FixedUpdate()
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }
}