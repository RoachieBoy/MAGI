using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General.Data_Containers
{
    [CreateAssetMenu(fileName = "FrequencyTable", menuName = "Tables/Frequencies", order = 1)]
    public class FrequencyDictionary : ScriptableObject, IReadOnlyDictionary<float, string>
    {
        private const float TwelfthRootOfTwo = 1.059463094359f;

        [Header("Frequency Settings")] [SerializeField]
        private int numKeys = 88;

        [SerializeField] private float baseFrequency = 440.0f;
        [SerializeField] private int baseKeyNumber = 49;

        [Header("Note settings")] [SerializeField]
        private Notes baseNote = Notes.A;

        [SerializeField] private int baseOctave = 4;

        private Dictionary<float, string> _frequencies;

        private void OnEnable()
        {
            BuildDictionary();
        }

        private void OnValidate()
        {
            BuildDictionary();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection
        /// </summary>
        public IEnumerator<KeyValuePair<float, string>> GetEnumerator()
        {
            return _frequencies.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Checks if the dictionary contains the given frequency
        /// </summary>
        /// <param name="key"> what key are you looking for? </param>
        public bool ContainsKey(float key)
        {
            return _frequencies.ContainsKey(key);
        }

        /// <summary>
        ///     Tries to get the value of the frequency, returns false if it doesn't exist
        /// </summary>
        /// <param name="key"> the current frequency </param>
        /// <param name="value"> the value of the frequency (either it exists or it doesn't) </param>
        /// <returns> a boolean representing the state of the given frequency (existing or not ) </returns>
        public bool TryGetValue(float key, out string value)
        {
            return _frequencies.TryGetValue(key, out value);
        }

        /// <summary>
        ///     Calculates the frequency based on the key number and the base frequency
        /// </summary>
        /// <param name="keyNumber"> the current key being played (index) </param>
        /// <returns> the current frequency value represented as a float </returns>
        private float CalculateFrequency(int keyNumber)
        {
            var semitoneOffset = keyNumber - baseKeyNumber;

            var frequency = baseFrequency * Mathf.Pow(TwelfthRootOfTwo, semitoneOffset);

            return frequency;
        }

        /// <summary>
        ///     Calculates the note based on the key number and the base note and octave
        /// </summary>
        /// <param name="keyNumber"> the currently played key (index) </param>
        /// <returns> a string representing a musical note </returns>
        private string CalculateNote(int keyNumber)
        {
            // calculate the first note index based on the baseNote and the baseKeyNumber
            var noteValue = (int) baseNote + keyNumber - baseKeyNumber;
            var octaveValue = baseOctave;

            // ensure when the note is higher than B that the enum starts from A again
            while (noteValue > (int) Notes.B)
            {
                noteValue -= (int) Notes.B;
                octaveValue++;
            }

            while (noteValue < (int) Notes.C)
            {
                noteValue += (int) Notes.B;
                octaveValue--;
            }

            return $"{((Notes) noteValue).ToFormattedString()}{octaveValue}";
        }

        /// <summary>
        ///     Builds the dictionary of frequencies and notes based on the settings
        /// </summary>
        private void BuildDictionary()
        {
            _frequencies = new Dictionary<float, string>();

            for (var i = 1; i < numKeys; i++)
            {
                var frequency = CalculateFrequency(i);
                var note = CalculateNote(i);

                _frequencies.Add(frequency, note);
            }
        }

        #region Public Properties

        public int BaseKeyNumber => baseKeyNumber;
        public Notes BaseNote => baseNote;
        public string this[float key] => _frequencies[key];
        public IEnumerable<float> Keys => _frequencies.Keys;
        public IEnumerable<string> Values => _frequencies.Values;
        public int Count => _frequencies.Count;

        #endregion
    }
}