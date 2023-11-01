using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities.Sound
{
    [CreateAssetMenu(menuName = "Utilities/Sound/Composite Audio Event", fileName = "CompositeAudioEffect")]
    public class CompositeAudioEffect : AudioEvent
    {
        [SerializeField] private CompositeEntry[] entries;

        public override void Play(AudioSource source)
        {
            float totalWeight = 0;

            for (var i = 0; i < entries.Length; ++i)
                totalWeight += entries[i].weight;

            var pick = Random.Range(0, totalWeight);

            for (var i = 0; i < entries.Length; ++i)
            {
                if (pick > entries[i].weight)
                {
                    pick -= entries[i].weight;
                    continue;
                }

                entries[i].aEvent.Play(source);
                return;
            }
        }

        [Serializable]
        public struct CompositeEntry
        {
            public AudioEvent aEvent;
            public float weight;
        }
    }
}