using System.Collections;
using System.Collections.Generic;
using HandcraftedGames.AgentController;
using HandcraftedGames.AIKit.Awareness;
using HandcraftedGames.AIKit.Sense;
using HandcraftedGames.AIKit.TaskQueue;
using UnityEngine;

namespace HandcraftedGames.AIKit
{
    public class MonoAIAgent : MonoBehaviour, IAIAgent
    {
        private IAIAgent aIAgent;
        public MonoAgentController _agentController;

        public IAgentController agentController => aIAgent?.agentController;

        public ITaskQueue taskQueue => aIAgent?.taskQueue;

        public bool IsEnabled => aIAgent?.IsEnabled ?? false;

        private void Awake()
        {
            aIAgent = new BaseAIAgent(gameObject, _agentController);
        }

        private void OnEnable()
        {
            aIAgent.Enable();
        }

        private void OnDisable()
        {
            aIAgent.Disable();
        }

        public void Enable()
        {
            this.enabled = true;
        }

        public void Disable()
        {
            this.enabled = false;
        }
    }
}