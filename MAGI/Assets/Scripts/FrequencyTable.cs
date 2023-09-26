using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FrequencyTable", menuName = "Tables/FrequencyTable", order = 1)]
// Inherit from the read only list so you can just use indexes, LINQ etc
public class FrequencyTable : ScriptableObject, IReadOnlyList<float>
{
    private const float TwelfthRootOfTwo = 1.059463094359f;

    private List<float> _frequencies;

    [SerializeField] private int numKeys = 88;

    [SerializeField] private float baseFrequency = 440.0f;

    [SerializeField] private int baseKeyNumber = 49;

    // This is the index you'll need for 440hz aka A4 yeet
    public float BaseKeyNumber => baseKeyNumber;

    private float CalculateFrequency(int keyNumber)
    {
        var n = keyNumber - baseKeyNumber;

        return baseFrequency * Mathf.Pow(TwelfthRootOfTwo, n);
    }

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