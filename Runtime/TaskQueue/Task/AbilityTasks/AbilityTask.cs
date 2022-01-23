using HandcraftedGames.AgentController.Abilities;

namespace HandcraftedGames.AIKit.TaskQueue
{
    public abstract class AbilityTask: BaseTask
    {
        protected IAbility ability;
        public AbilityTask(IAbility ability)
        {
            this.ability = ability;
        }

        protected override void OnStart()
        {
            var agent = ability.Agent;
            if(agent == null)
            {
                Fail();
                return;
            }
            if(ability.IsActive)
                ability.Stop();

            ability.OnDidStop += OnAbilityDidStop;

            if(!ActivateAbility())
            {
                Fail();
                return;
            }
        }

        protected override void OnStop(bool success)
        {
            ability.OnDidStop -= OnAbilityDidStop;
            ability.Stop();
        }

        private void OnAbilityDidStop(IAbility ability, StopReason reason)
        {
            if(reason == StopReason.Completion)
                Complete();
            else if (reason == StopReason.Failure || reason == StopReason.Interruption)
                Fail();
        }

        protected abstract bool ActivateAbility();
    }
}