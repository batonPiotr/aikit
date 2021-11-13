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
        void Start();
        void Stop();
    }
}