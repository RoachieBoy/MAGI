using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General.Data_Containers
{
    [CreateAssetMenu(fileName = "FrequencyTable", menuName = "Tables/Frequencies", order = 1)]
    public class FrequencyDictionary: ScriptableObject, IReadOnlyDictionary<float, string>
    {
        private const float TwelfthRootOfTwo = 1.059463094359f;
        private Dictionary<float, string> _frequencies;
        
        [Header("Frequency Settings")]
        [SerializeField] private int numKeys = 88;
        [SerializeField] private float baseFrequency = 440.0f;
        [SerializeField] private int baseKeyNumber = 49;
        
        [Header("Note settings")]
        [SerializeField] private Notes baseNote = Notes.A;
        [SerializeField] private int baseOctave = 4;
        
        public int BaseKeyNumber => baseKeyNumber;

        public Notes BaseNote => baseNote;

        public int BaseOctave => baseOctave;

        private float CalculateFrequency(int keyNumber)
        {
            var semitoneOffset = keyNumber - baseKeyNumber;
     
            var frequency = baseFrequency * Mathf.Pow(TwelfthRootOfTwo, semitoneOffset);
            
            return frequency;
        }

        private string CalculateNote(int keyNumber)
        {
            // calculate the first note index based on the baseNote and the baseKeyNumber
            var noteValue = (int) baseNote + keyNumber - baseKeyNumber;
            var octaveValue = baseOctave;
            
            // ensure when the note is higher than B that the enum starts from A again
            while (noteValue> (int) Notes.B)
            {
                noteValue -= (int) Notes.B;
                octaveValue++;
            }

            while (noteValue < (int) Notes.C)
            {
                noteValue += (int) Notes.B;
                octaveValue--;
            }
            
            return $"{((Notes)noteValue).ToFormattedString()}{octaveValue}";
        }

        private void OnValidate()
        {
            BuildDictionary();
        }

        private void OnEnable()
        {
            BuildDictionary();
        }

        private void BuildDictionary()
        {
            _frequencies = new Dictionary<float, string>();
            
            for (var i = 1; i < numKeys; i++)
            {
                var frequency = CalculateFrequency(i);
                var note = CalculateNote(i);
                
                _frequencies.Add(frequency, note);
                
                Debug.Log($"Frequency: {frequency} Note: {note}");
            }
        }

        public IEnumerator<KeyValuePair<float, string>> GetEnumerator()
        {
            return _frequencies.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _frequencies.Count;
        
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