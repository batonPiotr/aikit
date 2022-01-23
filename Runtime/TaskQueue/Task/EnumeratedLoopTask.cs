using System.Collections.Generic;

namespace HandcraftedGames.AIKit.TaskQueue
{
    public class EnumeratedLoopTask: LoopTask
    {
        private int count;
        private int currentIndex = -1;
        public EnumeratedLoopTask(IList<ITask> tasks, int count): base(tasks)
        {
            this.count = count;
        }
        protected override bool ShouldStartNextLoop()
        {
            currentIndex++;
            return currentIndex < count;
        }
    }
}