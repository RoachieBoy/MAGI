namespace Synth_Engine.Modules
{
    public static class EnvelopeManager
    {
        /// <summary>
        ///     Sets the attack of the current amplitude to the target amplitude.
        /// </summary>
        /// <param name="currentAmplitude"></param>
        /// <param name="targetAmplitude"></param>
        /// <param name="attackTime"></param>
        /// <param name="phase"></param>
        /// <returns></returns>
        public static float SetAttack(float currentAmplitude, float targetAmplitude, float attackTime, float phase)
        {
            if (currentAmplitude < targetAmplitude) currentAmplitude += attackTime * phase;

            return currentAmplitude;
        }
    }
}