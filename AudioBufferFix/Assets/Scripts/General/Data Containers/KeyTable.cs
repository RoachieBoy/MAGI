using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace General.Data_Containers
{
    [CreateAssetMenu(fileName = "KeyTable", menuName = "Tables/KeyTable", order = 1)]
    public class KeyTable : ScriptableObject, IReadOnlyList<Key>
    {
        [SerializeField] private List<Key> keys;
        
        public IEnumerator<Key> GetEnumerator()
        {
            return keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => keys.Count;

        public Key this[int index] => keys[index];
    }
}