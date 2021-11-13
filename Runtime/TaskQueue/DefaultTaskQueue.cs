using System.Collections.Generic;

namespace HandcraftedGames.AIKit.TaskQueue
{
    public class DefaultTaskQueue : ITaskQueue
    {
        bool _isRunning = false;
        public bool IsRunning => _isRunning;
        private List<ITask> tasks = new List<ITask>();
        private ITask currentTask = null;
        public ITask Remove(ITask task)
        {
            if(task == null)
                return null;


            if(task == currentTask)
            {
                task.Stop();
                currentTask = null;
                Update();
                return task;
            }

            if(tasks.Remove(task))
                return task;

            return null;
        }

        public void Enqueue(ITask task)
        {
            tasks.Add(task);
            Update();
        }

        public void Start()
        {
            if(_isRunning)
                return;
            _isRunning = true;
            if(currentTask != null)
                currentTask.Start();
            else
                Update();
        }

        public void Stop()
        {
            if(currentTask != null)
                currentTask.Stop();
            _isRunning = false;
        }

        private void Update()
        {
            if(!IsRunning)
                return;

            if(currentTask == null)
            {
                StartNewTask();
            }
        }

        private void StartNewTask()
        {
            if(currentTask != null)
                return;
            if(tasks.Count > 0)
            {
                currentTask = tasks[0];
                tasks.RemoveAt(0);
                currentTask.OnDidComplete += OnTaskComplete;
                currentTask.Start();
            }
        }

        private void OnTaskComplete(ITask task, bool success)
        {
            if(task != currentTask)
            {
                // Error
                return;
            }

            currentTask.OnDidComplete -= OnTaskComplete;
            currentTask = null;
            Update();
        }
    }
}