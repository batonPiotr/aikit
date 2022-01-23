namespace HandcraftedGames.AIKit.Sense
{
    using UnityEngine;
    public class AudioDataSense: BaseSense, IAudioSense
    {
        public AudioDataSense(ISenseMemory memory): base(memory)
        {

        }

        public void OnDetect(AudioDataSenseEmitter emitter, float distance)
        {
            EmitSignal(new AudioSenseSignal(Time.realtimeSinceStartup, new ApproximatePosition(emitter.audioSource.transform.position, distance)));
        }
    }
}