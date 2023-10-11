using UnityEngine;

namespace Synth_Engine.Modules
{
    [CreateAssetMenu (fileName = "Envelope", menuName = "SynthModules/Envelope")]
    public class Envelope: ScriptableObject
    {
        public float attackTime = 0.1f;
        public float decayTime = 0.1f;
        public float sustainLevel = 0.5f;
        public float releaseTime = 0.1f;
    }
}