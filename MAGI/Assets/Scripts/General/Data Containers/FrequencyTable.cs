using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace General.Data_Containers
{
    [CreateAssetMenu(fileName = "FrequencyTable", menuName = "Tables/FrequencyTable", order = 1)]
    public class FrequencyTable : ScriptableObject, IReadOnlyDictionary<float, string>
    {
        // pitch ratio for a semitone
        private const float TwelfthRootOfTwo = 1.059463094359f;
        
        private Dictionary<float, string> _frequencies;
        
        [Header("Frequency Settings")]
        [SerializeField] private int numKeys = 88;
        [SerializeField] private float baseFrequency = 440.0f;
        [SerializeField] private int baseKeyNumber = 49;
        
        [Header("Note settings")]
        [SerializeField] private Notes baseNote = Notes.A;
        [SerializeField] private int startingOctave = 4;

        /// <summary>
        ///  This is the index of A4 in the standard frequency table
        /// </summary>
        public int BaseKeyNumber => baseKeyNumber; 

        /// <summary>
        ///  Calculate the frequency of a given key number 
        /// </summary>
        /// <param name="keyNumber"> which key is pressed </param>
        private float CalculateFrequency(int keyNumber)
        {
            var semitoneOffset = keyNumber - baseKeyNumber;
     
            var frequency = baseFrequency * Mathf.Pow(TwelfthRootOfTwo, semitoneOffset);
            
            return frequency;
        }

        /// <summary>
        ///  Get the note of a given key number
        /// </summary>
        /// <param name="keyNumber"> the current key in the table </param>
        public string GetNote(int keyNumber)
        {
            // get the note index
            var noteIndex = (int) baseNote + (keyNumber - baseKeyNumber) % (int) Notes.B;
            
            // get the octave
            var octave = startingOctave + (keyNumber - baseKeyNumber) / (int) Notes.B;
            
            // get the note
            var note = (Notes) (noteIndex + 1);
            
            // return the note and the octave
            return $"{note.ToFormattedString()}{octave}";
        }

        /// <summary>
        /// Get all the frequencies for the piano keys that are in the frequency table
        /// </summary>
        private Dictionary<float, string> GetAllFrequenciesAndNotes()
        {
            // get the frequency and use the note as value
            var frequencies = new Dictionary<float, string>(numKeys);
            
            // add all the frequencies to the dictionary and use the note as value
            for (var i = 1; i < numKeys; i++)
            {
               frequencies[CalculateFrequency(i)] = GetNote(i);
               
               Debug.Log($"Key: {i} Frequency: {CalculateFrequency(i)} Note: {GetNote(i)}");
            }
            
            return frequencies; 
        }

        private void OnValidate()
        {
            // update the frequencies when the inspector values change 
            // this only happens in the editor!!!
            _frequencies = GetAllFrequenciesAndNotes();
        }

        private void OnEnable()
        {
            // update the frequencies when the scriptable object is loaded
            // this happens in the editor and at runtime
            _frequencies = GetAllFrequenciesAndNotes();
        }

        IEnumerator<KeyValuePair<float, string>> IEnumerable<KeyValuePair<float, string>>.GetEnumerator()
        {
            return _frequencies.GetEnumerator();
        }

        public IEnumerator<float> GetEnumerator()
        {
            return _frequencies.Keys.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    
        public int Count => _frequencies.Count;
        
        public float this[int index] => _frequencies.Keys.ElementAt(index);
        
        public bool ContainsKey(float key)
        {
            return _frequencies.ContainsKey(key);
        }

        public bool TryGetValue(float key, out string value)
        {
            return _frequencies.TryGetValue(key, out value);
        }

        public string this[float key] => _frequencies[key];

        public IEnumerable<float> Keys => _frequencies.Keys;
        
        public IEnumerable<string> Values => _frequencies.Values;
    }
}