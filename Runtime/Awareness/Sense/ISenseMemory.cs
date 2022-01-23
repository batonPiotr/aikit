namespace HandcraftedGames.AIKit.Sense
{
    using System.Collections.Generic;

    public interface ISenseMemory
    {
        float TimeLimit { get; }
        int BufferSizeLimit { get; }
        IEnumerable<ISenseSignal> Buffer { get; }

        void Append(ISenseSignal signal);

        event System.Action<ISenseMemory, ISenseSignal> OnSignalAdd;
        event System.Action<ISenseMemory, ISenseSignal> OnSignalRemove;
    }
}