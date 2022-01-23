using System;
using System.Linq;

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
                throw InvalidStateException(new TaskState[] { TaskState.Idle }, State);
            _state = TaskState.Running;
            UnityEngine.Debug.Log("Started task: " + this);
            OnStart();
            OnDidStart?.Invoke(this);
        }

        public void Cancel()
        {
            UnityEngine.Debug.Log("Canceled task: " + this);
            Fail();
        }

        public void Clear()
        {
            if(State != TaskState.Failed && State != TaskState.Completed)
                throw InvalidStateException(new TaskState[] { TaskState.Completed, TaskState.Failed }, State);
            OnClear();
            _state = TaskState.Idle;
        }

        protected void Complete()
        {
            if(_state != TaskState.Running)
                throw InvalidStateException(new TaskState[] { TaskState.Running }, State);
            _state = TaskState.Completed;
            UnityEngine.Debug.Log("Completed task: " + this);
            OnStop(true);
            OnDidComplete?.Invoke(this, true);
        }

        protected void Fail()
        {
            if(_state != TaskState.Running)
                throw InvalidStateException(new TaskState[] { TaskState.Running }, State);
            _state = TaskState.Failed;
            UnityEngine.Debug.Log("Failed task: " + this);
            OnStop(false);
            OnDidComplete?.Invoke(this, false);
        }

        protected virtual void OnStart() {}
        protected virtual void OnStop(bool success) {}
        protected virtual void OnClear() {}


        private InvalidOperationException InvalidStateException(TaskState[] allowed, TaskState current)
        {
            return new InvalidOperationException("Invalid task state: " + current + ". Allowed are: " + allowed.Select(i => i.ToString()).Aggregate((a,b) => a + "," + b ));
        }
    }
}