using HandcraftedGames.AIKit.TaskQueue;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;

namespace HandcraftedGames.AIKit.Tests
{
    public class BaseTaskTests
    {
        /// <summary>
        /// Tests if ability is being added to a valid agent.
        /// </summary>
        [Test]
        public void TestStartStopFailComplete()
        {
            var task = new MockedTask();
            Assert.AreEqual(TaskState.Idle, task.State);

            Assert.Throws<System.InvalidOperationException>(() => task.Cancel());
            Assert.AreEqual(TaskState.Idle, task.State);

            Assert.Throws<System.InvalidOperationException>(() => task.SetAsCompleted());
            Assert.AreEqual(TaskState.Idle, task.State);

            Assert.Throws<System.InvalidOperationException>(() => task.SetAsFailed());
            Assert.AreEqual(TaskState.Idle, task.State);


            task.Start();
            Assert.AreEqual(TaskState.Running, task.State);

            task.SetAsFailed();
            Assert.AreEqual(TaskState.Failed, task.State);

            Assert.Throws<System.InvalidOperationException>(() => task.Start());
            Assert.AreEqual(TaskState.Failed, task.State);

            var secondTask = new MockedTask();
            secondTask.Start();
            secondTask.Cancel();
            Assert.AreEqual(TaskState.Idle, secondTask.State);

            secondTask.Start();
            secondTask.SetAsCompleted();
            Assert.AreEqual(TaskState.Completed, secondTask.State);
        }

        // [Test, Performance]
        // public void TestPerformanceOfAbilityRetrieval()
        // {
        //     var validAgent = new ValidAgent();
        //     validAgent.AddAbility(new AbilityA());
        //     validAgent.AddAbility(new AbilityB());
        //     validAgent.AddAbility(new AbilityC());
        //     Measure.Method(() =>
        //     {
        //         validAgent.GetAbility<AbilityC>();
        //     })
        //     .WarmupCount(10)
        //     .MeasurementCount(50)
        //     .IterationsPerMeasurement(5000)
        //     .GC()
        //     .Run();
        // }
    }
}