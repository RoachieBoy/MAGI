using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Visual_Effects.TextEffects.cs
{
    public static class TextMovementEffectManager
    {
        /// <summary>
        ///  Move the text in a wobbly fashion using this function
        /// </summary>
        /// <param name="time"> the time step to use </param>
        /// <param name="sinMovementSpeed"> the movement speed of the wobble </param>
        /// <param name="sinWaveSpeed"> the frequency of the wobble </param>
        /// <returns></returns>
        private static Vector3 Wobble(float time, float sinMovementSpeed, float sinWaveSpeed)
        {
            return new Vector3
            {
                x = Mathf.Sin(time * sinMovementSpeed),
                y = Mathf.Cos(time * sinWaveSpeed)
            };
        }
        
        /// <summary>
        ///  Move the characters in a wobbly fashion using this function
        /// </summary>
        /// <param name="time"> time step of the function </param>
        /// <param name="textObject"> the text object to wobble </param>
        /// <param name="sinMovementSpeed"> the movement speed of the wobble </param>
        /// <param name="sinWaveSpeed"> the frequency of the wobble </param>
        public static void CharacterWobble(float time, TMP_Text textObject, float sinMovementSpeed, float sinWaveSpeed)
        {
            var mesh = textObject.mesh;
            var vertices = mesh.vertices;
            var textInfo = textObject.textInfo;
            
            foreach (var character in textInfo.characterInfo)    
            {
                var index = character.vertexIndex;
                var offset = Wobble(time + index, sinMovementSpeed, sinWaveSpeed);
                
                vertices[index] += offset;
                vertices[index + 1] += offset;
                vertices[index + 2] += offset;
                vertices[index + 3] += offset;
            }
            
            mesh.vertices = vertices;
            textObject.canvasRenderer.SetMesh(mesh);
        }

        /// <summary>
        ///  Move the vertices in a wobbly fashion using this function
        /// </summary>
        /// <param name="time"> the time step of the function </param>
        /// <param name="textObject"> the text object to wobble </param>
        /// <param name="sinMovementSpeed"> the movement speed to wobble at </param>
        /// <param name="sinWaveSpeed"> the amplitude of the wobble </param>
        public static void VertexWobble(float time, TMP_Text textObject, float sinMovementSpeed, float sinWaveSpeed)
        {
            var mesh = textObject.mesh;
            var vertices = mesh.vertices;
            
            for (var i = 0; i < vertices.Length; i++)
            {
                var offset = Wobble(time + i, sinMovementSpeed, sinWaveSpeed);

                vertices[i] += offset;
            }

            mesh.vertices = vertices; 
            textObject.canvasRenderer.SetMesh(mesh);
        }
        
        /// <summary>
        ///  Move the words in a wobbly fashion using this function
        /// </summary>
        /// <param name="time"> time step of the function </param>
        /// <param name="textObject"> text object to wobble </param>
        /// <param name="sinMovementSpeed"> movement speed of the wobble </param>
        /// <param name="sinWaveSpeed"> amplitude of the wobble </param>
        public static void WordWobble(float time, TMP_Text textObject, float sinMovementSpeed, float sinWaveSpeed)
        {
            var mesh = textObject.mesh;
            var text = textObject.text;
            
            var wordLengths = new List<int>();
            var wordIndexes = new List<int> {0};
            
            // get the indexes of the words
            for (var index = text.IndexOf(' '); index > -1; index = text.IndexOf(' ', index + 1))
            {
                wordLengths.Add(index - wordIndexes[^1]);
                wordIndexes.Add(index + 1);
            }
            
            // add the last word to the list
            wordLengths.Add(text.Length - wordIndexes[^1]);
            
            var vertices = mesh.vertices;
            var textInfo = textObject.textInfo;
            var colors = mesh.colors; 

            for (var w = 0; w < wordIndexes.Count; w++)
            {
                var wordIndex = wordIndexes[w];
                var offset = Wobble(time + w, sinMovementSpeed, sinWaveSpeed);

                for (var i = 0; i < wordLengths[w]; i++)
                {
                    var c = textInfo.characterInfo[wordIndex + i];

                    var index = c.vertexIndex;

                    vertices[index] += offset;
                    
                    vertices[index + 1] += offset;
                    vertices[index + 2] += offset;
                    vertices[index + 3] += offset;
                }
            }
            
            mesh.colors = colors;
            mesh.vertices = vertices;
            textObject.canvasRenderer.SetMesh(mesh);
        }
    }
}