using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HandcraftedGames.AgentController.Abilities;
using HandcraftedGames.AIKit.TaskQueue;
using HandcraftedGames.AIKit.TaskQueue.AbilityTasks;
using UnityEngine;

namespace HandcraftedGames.AIKit.Samples
{
    public class SimpleTaskGenerator : MonoBehaviour
    {
        public List<GameObject> targets;
        public MonoAIAgent aIAgent;

        private void Start()
        {
            if(aIAgent == null)
            {
                Debug.LogError("AI Agent is null");
                return;
            }

            var tasks = targets
                .Select(i =>
                    {
                        return new GoToAbilityTask(
                            aIAgent
                                .agentController
                                .GetAbility<IGoToAbility>(),
                            i.transform.position
                        ) as ITask;
                    }
                );

            aIAgent.taskQueue.Enqueue(new EnumeratedLoopTask(tasks.ToList(), 2));
        }
    }
}