using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace General.Data_Containers
{
    [CreateAssetMenu(fileName = "FrequencyTable", menuName = "Tables/FrequencyTable", order = 1)]
    public class FrequencyTable : ScriptableObject, IReadOnlyList<float>
    {
        // common number in music used for pitch adjustment 
        private const float TwelfthRootOfTwo = 1.059463094359f;
        
        private List<float> _frequencies;
        
        [Header("Keyboard Settings")]
        [SerializeField] private int numKeys = 88;
        [SerializeField] private float baseFrequency = 440.0f;
        [SerializeField] private int baseKeyNumber = 49;

        /// <summary>
        ///  This is the index of A4 in the standard frequency table
        /// </summary>
        public int BaseKeyNumber => baseKeyNumber;

        /// <summary>
        ///  Calculate the frequency of a given key number 
        /// </summary>
        /// <param name="keyNumber"> which key is pressed </param>
        /// <returns> float representing the correct frequency of the key </returns>
        private float CalculateFrequency(int keyNumber)
        {
            var n = keyNumber - baseKeyNumber;

            return baseFrequency * Mathf.Pow(TwelfthRootOfTwo, n);
        }

        /// <summary>
        /// Get all the frequencies for the piano keys
        /// </summary>
        /// <returns> list of frequencies for all the keys </returns>
        private List<float> GetAllPianoKeyFrequencies()
        {
            var frequencies = new float[numKeys];

            for (var i = 1; i <= numKeys; i++)
            {
                frequencies[i - 1] = CalculateFrequency(i);
            }

            return frequencies.ToList();
        }

        private void OnValidate()
        {
            // update the frequencies when the inspector values change
            _frequencies = GetAllPianoKeyFrequencies();
        }
        
        public IEnumerator<float> GetEnumerator()
        {
            return _frequencies.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    
        public int Count => _frequencies.Count;
    
        public float this[int index] => _frequencies[index];
    }
}