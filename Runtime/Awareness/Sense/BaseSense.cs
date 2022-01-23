namespace HandcraftedGames.AIKit.Sense
{
    public abstract class BaseSense: ISense
    {
        private ISenseMemory _memory;
        public ISenseMemory memory => _memory;

        private bool _isEnabled = false;
        public bool IsEnabled => _isEnabled;

        public event System.Action<ISense, ISenseSignal> OnReceiveSignal;

        protected virtual void OnEnable() {}
        protected virtual void OnDisable() {}

        public BaseSense(ISenseMemory memory)
        {
            _memory = memory;
        }

        public void Enable()
        {
            if(IsEnabled)
                return;

            _isEnabled = true;
            OnEnable();
        }

        public void Disable()
        {
            if(!IsEnabled)
                return;

            _isEnabled = false;
            OnDisable();
        }

        protected void EmitSignal(ISenseSignal signal)
        {
            if(!IsEnabled)
                return;
            if(memory != null)
                memory.Append(signal);
            OnReceiveSignal?.Invoke(this, signal);
        }
    }
}