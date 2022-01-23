using System.Collections.Generic;
using HandcraftedGames.AIKit.TaskQueue;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;

namespace HandcraftedGames.AIKit.Tests
{
    public class TaskQueueTests
    {
        /// <summary>
        /// Tests if ability is being added to a valid agent.
        /// </summary>
        [Test]
        public void TestThreeTaskQueue()
        {
            var tasks = new List<MockedTask> { new MockedTask(), new MockedTask(), new MockedTask() };
            ITaskQueue taskQueue = new DefaultTaskQueue();

            taskQueue.Enqueue(tasks[0]);
            taskQueue.Enqueue(tasks[1]);
            taskQueue.Enqueue(tasks[2]);

            Assert.AreEqual(TaskState.Idle, tasks[0].State);
            Assert.AreEqual(TaskState.Idle, tasks[1].State);
            Assert.AreEqual(TaskState.Idle, tasks[2].State);

            taskQueue.Start();

            Assert.AreEqual(TaskState.Running, tasks[0].State);
            Assert.AreEqual(TaskState.Idle, tasks[1].State);
            Assert.AreEqual(TaskState.Idle, tasks[2].State);

            tasks[0].SetAsFailed();

            Assert.AreEqual(TaskState.Failed, tasks[0].State);
            Assert.AreEqual(TaskState.Running, tasks[1].State);
            Assert.AreEqual(TaskState.Idle, tasks[2].State);

            tasks[1].SetAsCompleted();

            Assert.AreEqual(TaskState.Failed, tasks[0].State);
            Assert.AreEqual(TaskState.Completed, tasks[1].State);
            Assert.AreEqual(TaskState.Running, tasks[2].State);

            taskQueue.Stop();

            Assert.AreEqual(TaskState.Failed, tasks[0].State);
            Assert.AreEqual(TaskState.Completed, tasks[1].State);
            Assert.AreEqual(TaskState.Idle, tasks[2].State);

            taskQueue.Start();

            Assert.AreEqual(TaskState.Failed, tasks[0].State);
            Assert.AreEqual(TaskState.Completed, tasks[1].State);
            Assert.AreEqual(TaskState.Running, tasks[2].State);

            tasks[2].SetAsCompleted();

            Assert.AreEqual(TaskState.Failed, tasks[0].State);
            Assert.AreEqual(TaskState.Completed, tasks[1].State);
            Assert.AreEqual(TaskState.Completed, tasks[2].State);
        }

        [Test]
        public void RemoveTaskWhileRunning()
        {
            var task = new MockedTask();
            var taskQueue = new DefaultTaskQueue();
            taskQueue.Enqueue(task);
            taskQueue.Start();

            Assert.AreEqual(TaskState.Running, task.State);

            taskQueue.Remove(task);
            Assert.AreEqual(TaskState.Idle, task.State);
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