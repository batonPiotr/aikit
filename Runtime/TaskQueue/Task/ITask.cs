namespace HandcraftedGames.AIKit.TaskQueue
{
    public enum TaskState
    {
        Idle,
        Running,
        Failed,
        Completed
    }

    public interface ITask
    {
        TaskState State { get; }
        event System.Action<ITask, bool> OnDidComplete;
        event System.Action<ITask> OnDidStart;

        /// <summary>
        /// Used to start task. Callable only in `Idle` state.
        /// </summary>
        void Start();

        /// <summary>
        /// Used to cancel running task. Callable only in `Running` state.
        /// </summary>
        void Cancel();

        /// <summary>
        /// Used to clear status and reset its internals after completion/failure. Can be called only when state is Failed or Completed
        /// </summary>
        void Clear();
    }
}