namespace HandcraftedGames.AIKit.TaskQueue
{
    interface ITaskQueue
    {
        bool IsRunning { get; }
        void Enqueue(ITask task);
        ITask Remove(ITask task);

        void Start();
        void Stop();
    }
}