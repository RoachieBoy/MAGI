using Synth_Engine;
using TMPro;
using UnityEngine;

namespace General.UI
{
    public class DisplayNote: MonoBehaviour
    {
        private TMP_Text _note;
        
        [SerializeField] private Synth synth;

        private void Awake()
        {
            _note = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            synth.onNoteChanged.AddListener(SetNoteText);
        }
        
        private void OnDisable()
        {
            synth.onNoteChanged.RemoveListener(SetNoteText);
        }

        private void SetNoteText(string note)
        {
            _note.text = note;
        }
    }
}