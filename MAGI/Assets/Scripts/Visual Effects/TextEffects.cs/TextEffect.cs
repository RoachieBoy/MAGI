using System;
using TMPro;
using UnityEngine;

namespace Visual_Effects.TextEffects.cs
{
    public class TextEffect : MonoBehaviour
    {
        private TMP_Text _textObject;
        
        [Header("What effect do I have?")]
        [SerializeField] private TextEffects textEffectType;
        
        [Header("How do I move?")]
        
        [Range(0.5f, 10f)]
        [SerializeField] private float sinMovementSpeed = 4f;
        
        [Range(0.5f, 10f)]
        [SerializeField] private float sinWaveSpeed = 5f;
        
        private void Awake()
        {
            _textObject = GetComponent<TMP_Text>();
        }

        private void FixedUpdate()
        {
            _textObject.ForceMeshUpdate();

            switch (textEffectType)
            {
                case TextEffects.CharacterWobble:
                    TextMovementEffectManager.CharacterWobble(Time.time, _textObject, sinMovementSpeed, sinWaveSpeed);
                    break;
                case TextEffects.WordWobble:
                    TextMovementEffectManager.WordWobble(Time.time, _textObject, sinMovementSpeed, sinWaveSpeed);
                    break; 
                case TextEffects.VertexWobble:
                    TextMovementEffectManager.VertexWobble(Time.time, _textObject, sinMovementSpeed, sinWaveSpeed);
                    break;
                default:
                    throw new Exception("No effect selected or effect does not exist");
            }
        }
    }
}