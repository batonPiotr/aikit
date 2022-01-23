using HandcraftedGames.AgentController.Abilities;
using UnityEngine;

namespace HandcraftedGames.AIKit.TaskQueue.AbilityTasks
{
    public class GoToAbilityTask : AbilityTask
    {
        private Vector3 target;
        public GoToAbilityTask(IGoToAbility ability, Vector3 target): base(ability)
        {
            this.target = target;
        }

        protected override bool ActivateAbility()
        {
            // Debug.Log("Activate goto ability task");
            var gotoAbility = ability as IGoToAbility;
            if(gotoAbility == null)
                return false;
            var agent = gotoAbility.Agent;
            if(agent == null)
                return false;
            return gotoAbility.GoTo(target);
        }
    }
}