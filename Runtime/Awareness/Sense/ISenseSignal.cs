namespace HandcraftedGames.AIKit.Sense
{
    public interface ISenseSignal
    {
        /// <summary>
        /// Timestamp on receival.
        /// </summary>
        float Timestamp { get; }

        /// <summary>
        /// Approximate position of the receival.
        /// </summary>
        ApproximatePosition Position { get; }

        /// <summary>
        ///  Id of the signal. It's a random integer.
        /// </summary>
        int Id { get; }
    }

    public static class SenseSignalIDGenerator
    {
        static int createdSignals = 0;
        static int signalsPerMs = 1000;
        public static int NextId(this ISenseSignal signal)
        {
            createdSignals += 1;
            // return (System.DateTime.UtcNow.Millisecond * signalsPerMs) + (createdSignals % signalsPerMs);
            return createdSignals;
        }
    }
}