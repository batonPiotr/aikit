namespace HandcraftedGames.AIKit.TaskQueue
{
    public class MockedTask : BaseTask
    {
        public void SetAsCompleted()
        {
            Complete();
        }

        public void SetAsFailed()
        {
            Fail();
        }
    }
}