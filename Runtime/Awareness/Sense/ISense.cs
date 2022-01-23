namespace HandcraftedGames.AIKit.Sense
{
    using HandcraftedGames.Common;

    /// <summary>
    /// Low level sensoric. Sends signal events wherever something has been detected.
    /// </summary>
    public interface ISense: IToggleable
    {
        ISenseMemory memory { get; }
        event System.Action<ISense, ISenseSignal> OnReceiveSignal;
    }
}