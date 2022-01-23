using System.Collections.Generic;

namespace HandcraftedGames.AIKit.TaskQueue
{
    public class LoopTask: BaseTask
    {
        private IList<ITask> tasks;
        private int currentTaskIndex = -1;
        private ITask currentTask
        {
            get
            {
                if(currentTaskIndex < 0 || currentTaskIndex >= tasks.Count)
                    return null;
                return tasks[currentTaskIndex];
            }
        }

        public LoopTask(IList<ITask> tasks)
        {
            this.tasks = tasks;
            currentTaskIndex = tasks.Count;
        }

        protected override void OnStart()
        {
            AdvanceTask();
        }

        protected override void OnStop(bool success)
        {
            if(currentTask != null)
                currentTask.Cancel();
        }

        protected override void OnClear()
        {
            currentTaskIndex = tasks.Count;
        }

        private void AdvanceTask()
        {
            ClearCurrentTask();

            if(currentTaskIndex >= tasks.Count - 1)
            {
                currentTaskIndex = -1;
                if(!ShouldStartNextLoop())
                {
                    Complete();
                    return;
                }
            }
            currentTaskIndex++;

            UnityEngine.Debug.Log("Current task index: " + currentTaskIndex + " total: " + tasks.Count);
            currentTask.OnDidComplete += onTaskCompletion;
            currentTask.Start();
        }

        private void ClearCurrentTask()
        {
            if(currentTask != null)
            {
                currentTask.OnDidComplete -= onTaskCompletion;
                currentTask.Clear();
            }
        }

        private void onTaskCompletion(ITask task, bool success)
        {
            if(!success)
            {
                Fail();
            }
            else
            {
                AdvanceTask();
            }
        }

        protected virtual bool ShouldStartNextLoop() => true;
    }
}