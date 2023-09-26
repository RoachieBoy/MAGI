using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "KeyTable", menuName = "Tables/KeyTable", order = 1)]
// Inherit from the read only list so you can just use indexes, LINQ etc
public class KeyTable : ScriptableObject, IReadOnlyList<Key>
{
    [SerializeField]
    private List<Key> keys;
    
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