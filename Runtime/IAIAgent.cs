namespace HandcraftedGames.AIKit
{
    using System.Collections;
    using System.Collections.Generic;
    using HandcraftedGames.AgentController;
    using HandcraftedGames.AIKit.Awareness;
    using HandcraftedGames.AIKit.Sense;
    using HandcraftedGames.AIKit.TaskQueue;
    using UnityEngine;

    public interface IAIAgent
    {
        GameObject gameObject { get; }
        IAgentController agentController { get; }
        ITaskQueue taskQueue { get; }

        bool IsEnabled { get; }

        void Enable();
        void Disable();
    }
}