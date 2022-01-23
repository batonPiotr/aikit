namespace HandcraftedGames.AIKit.Samples
{
    using System.Collections;
    using System.Collections.Generic;
    using HandcraftedGames.AgentController;
    using HandcraftedGames.Common;
    using HandcraftedGames.AgentController.Abilities;
    using HandcraftedGames.AgentController.Properties;
    using UnityEngine;

    public class FootstepSound : MonoBehaviour
    {
        public GameObject agentSource;

        private IAgentController agentController;

        public AudioSource audioSource;

        public AudioClip slowFootstepSound;
        public AudioClip quickFootstepSound;

        private void Awake()
        {
            gameObject.GetGODependencyInjection().AddDependency(agentSource);
            gameObject.GetGODependencyInjection().AddDependencyRequest((resolver) => {
                agentController = resolver.Resolve<IAgentController>();
                return agentController != null;
            });
        }

        private IEnumerator Start()
        {
            while(agentController == null) yield return null;

            var moveAbility = agentController.GetAbility<IMoveAbility>();
            moveAbility.OnDidActivate += OnDidActivate;
            moveAbility.OnDidStop += OnDidStop;

            var changeSpeedAbility = agentController.GetAbility<IChangeSpeedAbility>();
            changeSpeedAbility.OnDidActivate += OnDidActivate;
            changeSpeedAbility.OnDidStop += OnDidStop;
        }

        private void OnDidActivate(IAbility ability)
        {
            if(ability is IMoveAbility)
                audioSource.Play();
        }

        private void OnDidStop(IAbility ability, StopReason stopReason)
        {

            if(ability is IChangeSpeedAbility)
            {
                audioSource.Stop();
                var movementProperties = agentController.GetProperties<MovementProperties>();
                if(movementProperties.MovementSpeed > 1.5f)
                    audioSource.clip = quickFootstepSound;
                else
                    audioSource.clip = slowFootstepSound;
                audioSource.Play();
            }
            else
                audioSource.Stop();
        }
    }
}