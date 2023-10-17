using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using General.Data_Containers;
using Synth_Engine.Buffering_System.Buffer_Data;
using Synth_Engine.Modules;

namespace Synth_Engine.Buffering_System
{
    public static class AudioBufferManager
    {
        #region Variable Declarations

        // Values concerning the audio buffer sizes
        private const int MonoBufferSize = 2048;
        private const int MonoBufferByteSize = MonoBufferSize * sizeof(float);
        private const int StereoBufferSize = MonoBufferSize / 2;

        // Current audio buffer in use
        private static readonly float[] CurrentAudioBuffer = new float[MonoBufferSize];

        // Buffer that will be used next
        private static readonly float[] NextAudioBuffer = new float[MonoBufferSize];

        // Stereo audio buffer storage
        private static readonly StereoData[] StereoAudioBuffer = new StereoData[StereoBufferSize];

        // First is sample storage, second is phase storage
        private static ConcurrentDictionary<float, AudioPreBuffer> _preloadAudioBuffers;
        private static StereoData _currentPhase;
        private static StereoData _currentAmplitude;
        private static StereoData _currentTargetAmplitude;
        private static float _currentAttackTime;

        #endregion

        #region Preload Buffers

        /// <summary>
        ///   Initializes the preload buffer storage based on a given frequency table.
        /// </summary>
        /// <param name="frequencyTable"> the frequency table scriptable object to use </param>
        public static void InitializePreloadBuffers(FrequencyTable frequencyTable)
        {
            _preloadAudioBuffers = new ConcurrentDictionary<float, AudioPreBuffer>(1, frequencyTable.Count);
        }

        public static void FillPreloadAudioBuffers(
            FrequencyTable frequencyTable,
            float attackTime,
            Func<float, float, float, SampleState> generator,
            float amplitude
        )
        {
            FillPreloadAudioBuffers(
                frequencyTable,
                attackTime,
                generator,
                generator,
                amplitude,
                amplitude
            );
        }

        private static void FillPreloadAudioBuffers(
            FrequencyTable frequencyTable,
            float attackTime,
            Func<float, float, float, SampleState> leftGenerator,
            Func<float, float, float, SampleState> rightGenerator,
            float leftAmplitude,
            float rightAmplitude
        )
        {
            Parallel.ForEach(frequencyTable, frequency =>
            {
                var stereoBuffer = new StereoData[StereoBufferSize];
                var phaseLeft = 0f;
                var phaseRight = 0f;
                
                _preloadAudioBuffers[frequency.Key] = GenerateAudioPreBuffer(
                    frequency.Key, attackTime, stereoBuffer,
                    ref phaseLeft, ref phaseRight,
                    leftAmplitude, rightAmplitude,
                    leftGenerator, rightGenerator
                );
            });
        }

        private static AudioPreBuffer GenerateAudioPreBuffer(
            float frequency,
            float attackTime,
            StereoData[] stereoBuffer,
            ref float phaseLeft,
            ref float phaseRight,
            float targetLeftAmplitude,
            float targetRightAmplitude,
            Func<float, float, float, SampleState> leftGenerator,
            Func<float, float, float, SampleState> rightGenerator
        )
        {
            var leftAmplitude = targetLeftAmplitude;
            var rightAmplitude = targetRightAmplitude;

            for (var i = 0; i < StereoBufferSize; i++)
            {
                leftAmplitude = EnvelopeManager.SetAttack(
                    leftAmplitude,
                    targetLeftAmplitude,
                    attackTime,
                    phaseLeft
                );

                rightAmplitude = EnvelopeManager.SetAttack(
                    rightAmplitude,
                    targetRightAmplitude,
                    attackTime,
                    phaseRight
                );

                // generate the left and right channels using the generators and the current phase
                var left = leftGenerator(frequency, leftAmplitude, phaseLeft);
                var right = rightGenerator(frequency, rightAmplitude, phaseRight);

                stereoBuffer[i] = new StereoData(left.Sample, right.Sample);

                phaseLeft = left.Phase;
                phaseRight = right.Phase;
            }

            return new AudioPreBuffer(
                stereoBuffer,
                new StereoData(phaseLeft, phaseRight),
                new StereoData(leftAmplitude, rightAmplitude),
                new StereoData(targetLeftAmplitude, targetRightAmplitude),
                attackTime
            );
        }

        /// <summary>
        ///  Fills the preload audio buffers using a given generator and amplitude.
        ///  Can be used to generate a mono buffer (left and right channels are the same).
        /// </summary>
        /// <param name="frequency"></param>
        public static void SetPreloadAudioBuffer(float frequency)
        {
            if (!_preloadAudioBuffers.TryGetValue(frequency, out var bufferData))
                return;

            bufferData.AudioStereoBuffer.CopyToFloatArray(CurrentAudioBuffer);

            _currentPhase = bufferData.FinalPhase;
            _currentAmplitude = bufferData.FinalAmplitude;
            _currentTargetAmplitude = bufferData.TargetAmplitude;
        }

        #endregion

        #region Audio Buffer Generation

        /// <summary>
        ///  Fills the next audio buffer using a given generator and amplitude and frequency.
        /// </summary>
        /// <param name="generator"> the generator of the audio </param>
        /// <param name="frequency"> the frequency of the given audio </param>
        public static void FillNextAudioBuffer(
            float frequency,
            Func<float, float, float, SampleState> generator
        )
        {
            FillNextAudioBuffer(
                frequency,
                generator,
                generator
            );
        }

        /// <summary>
        /// Fills the next audio buffer for both left and right channels using provided generators and amplitudes.
        /// </summary>
        private static void FillNextAudioBuffer(
            float frequency,
            Func<float, float, float, SampleState> generatorLeft,
            Func<float, float, float, SampleState> generatorRight
        )
        {
            for (var i = 0; i < StereoBufferSize; i++)
            {
                _currentAmplitude = new StereoData(
                    EnvelopeManager.SetAttack(
                        _currentAmplitude.LeftChannel,
                        _currentTargetAmplitude.LeftChannel,
                        _currentAttackTime,
                        _currentPhase.LeftChannel
                    ),
                    EnvelopeManager.SetAttack(
                        _currentAmplitude.RightChannel,
                        _currentTargetAmplitude.RightChannel,
                        _currentAttackTime,
                        _currentPhase.RightChannel
                    )
                );

                var left = generatorLeft(frequency, _currentAmplitude.LeftChannel, _currentPhase.LeftChannel);
                var right = generatorRight(frequency, _currentAmplitude.RightChannel, _currentPhase.RightChannel);

                StereoAudioBuffer[i] = new StereoData(left.Sample, right.Sample);
                
                _currentPhase = new StereoData(left.Phase, right.Phase);
            }

            // copy the stereo audio buffer to the next audio buffer
            StereoAudioBuffer.CopyToFloatArray(NextAudioBuffer);
        }

        /// <summary>
        /// Gets the current audio buffer.
        /// </summary>
        /// <param name="bufferOut">Array to copy current audio buffer data to.</param>
        public static void GetAudioBuffer(float[] bufferOut)
        {
            if (bufferOut is not {Length: MonoBufferSize})
            {
                throw new ArgumentException("Invalid buffer provided.", nameof(bufferOut));
            }

            CopyAudioBuffer(CurrentAudioBuffer, bufferOut);
        }

        /// <summary>
        /// Switches the audio buffers, making the next audio buffer the current one.
        /// </summary>
        public static void SwitchAudioBuffers()
        {
            CopyAudioBuffer(NextAudioBuffer, CurrentAudioBuffer);
        }

        /// <summary>
        /// Copies audio data from source to destination buffer.
        /// </summary>
        private static void CopyAudioBuffer(float[] source, float[] destination)
        {
            Buffer.BlockCopy(
                source,
                0,
                destination,
                0,
                MonoBufferByteSize
            );
        }

        #endregion
    }
}