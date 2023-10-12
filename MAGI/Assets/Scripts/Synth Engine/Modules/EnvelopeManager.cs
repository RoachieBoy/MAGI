namespace Synth_Engine.Modules
{
    public static class EnvelopeManager
    {
        public static float SetAttack(float currentAmplitude, float targetAmplitude, float attackTime, float phase)
        {
            if(currentAmplitude < targetAmplitude)
            {
                currentAmplitude += attackTime * phase;
            }

            return currentAmplitude; 
        }
    }
}