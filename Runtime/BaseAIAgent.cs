namespace HandcraftedGames.AIKit
{
    using System.Collections.Generic;
    using HandcraftedGames.AgentController;
    using HandcraftedGames.AIKit.Awareness;
    using HandcraftedGames.AIKit.Sense;
    using HandcraftedGames.AIKit.TaskQueue;
    using UnityEngine;

    public class BaseAIAgent : IAIAgent
    {
        private GameObject _gameObject;
        public GameObject gameObject => _gameObject;

        private IAgentController _agentController;
        public IAgentController agentController => _agentController;

        private ITaskQueue _taskQueue = new TaskQueue.DefaultTaskQueue();
        public ITaskQueue taskQueue => _taskQueue;

        private bool _isEnabled = false;
        public bool IsEnabled => _isEnabled;

        public BaseAIAgent(GameObject gameObject, IAgentController agentController)
        {
            _gameObject = gameObject;
            if(agentController != null)
                _agentController = agentController;
            else
                _agentController = new AgentController(gameObject);
        }

        public void Enable()
        {
            taskQueue.Start();
        }

        public void Disable()
        {
            taskQueue.Stop();
        }
    }
}