namespace HandcraftedGames.AIKit.Sense
{
    using UnityEngine;
    public struct AudioSenseSignal : ISenseSignal
    {
        public int Id { get; }
        public float Timestamp { get; }
        public ApproximatePosition Position { get; }

        public AudioSenseSignal(float timestamp, ApproximatePosition position)
        {
            this.Timestamp = timestamp;
            this.Position = position;
            this.Id = 0;
            this.Id = this.NextId();
        }
    }
}