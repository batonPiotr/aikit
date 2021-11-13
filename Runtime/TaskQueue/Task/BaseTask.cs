namespace HandcraftedGames.AIKit.TaskQueue
{
    public abstract class BaseTask: ITask
    {
        TaskState _state = TaskState.Idle;
        public TaskState State => _state;
        public event System.Action<ITask, bool> OnDidComplete;
        public event System.Action<ITask> OnDidStart;

        public void Start()
        {
            if(State != TaskState.Idle)
                return;
            _state = TaskState.Running;
            OnStart();
        }

        public void Stop()
        {
            if(State != TaskState.Running)
                return;
            _state = TaskState.Idle;
            OnStop();
        }

        protected void Complete()
        {
            if(_state != TaskState.Running)
                return;
            _state = TaskState.Completed;
            OnStop();
            OnDidComplete?.Invoke(this, true);
        }

        protected void Fail()
        {
            if(_state != TaskState.Running)
                return;
            _state = TaskState.Failed;
            OnStop();
            OnDidComplete?.Invoke(this, false);
        }

        protected virtual void OnStart() {}
        protected virtual void OnStop() {}
    }
}