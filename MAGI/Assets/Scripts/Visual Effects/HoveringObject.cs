using Synth_Engine;
using UnityEngine;

namespace Visual_Effects
{
    public class HoveringObject : MonoBehaviour
    {
        [SerializeField] private float minHoverHeight = 1.0f;
        [SerializeField] private float maxHoverHeight = 3.0f;
        [SerializeField] private float minHoverSpeed = 1.0f;
        [SerializeField] private float maxHoverSpeed = 2.0f;
        
        [Header("What game object am I linked to?")]
        [SerializeField] private Synth synth;

        private Vector3 _initialPosition;
        private float _randomHeight;
        private float _randomSpeed;

        private void Start()
        {
            _initialPosition = transform.position;
            RandomizeParameters();
        }

        private void FixedUpdate()
        {
            if (synth.IsPlaying)
            {
                transform.position = _initialPosition + new Vector3(0, 
                    Mathf.Sin(Time.time * _randomSpeed) * _randomHeight, 0);
            }
            
            // reset the original transform position if the synth is not playing
            else
            {
                transform.position = _initialPosition;
            }
        }

        private void RandomizeParameters()
        {
            _randomHeight = Random.Range(minHoverHeight, maxHoverHeight);
            _randomSpeed = Random.Range(minHoverSpeed, maxHoverSpeed);
        }
    }
}