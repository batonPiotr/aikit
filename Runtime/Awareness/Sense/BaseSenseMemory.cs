namespace HandcraftedGames.AIKit.Sense
{
    using System.Collections.Generic;
    using UnityEngine;

    public class BaseSenseMemory: ISenseMemory
    {
        public float TimeLimit { get; set; }

        public int BufferSizeLimit { get; set; }

        private List<ISenseSignal> _internalBuffer;

        public event System.Action<ISenseMemory, ISenseSignal> OnSignalAdd;
        public event System.Action<ISenseMemory, ISenseSignal> OnSignalRemove;

        public IEnumerable<ISenseSignal> Buffer { get => _internalBuffer; }

        public BaseSenseMemory()
        {
            TimeLimit = 20.0f;
            BufferSizeLimit = 1000;
            _internalBuffer = new List<ISenseSignal>(BufferSizeLimit);
        }

        public void Append(ISenseSignal signal)
        {
            _internalBuffer.Add(signal);
            OnSignalAdd?.Invoke(this, signal);
            var currentTime = UnityEngine.Time.realtimeSinceStartup;
            _internalBuffer.RemoveAll((i) =>
            {
                var retVal = currentTime - i.Timestamp > TimeLimit;
                if(retVal)
                    OnSignalRemove?.Invoke(this, i);
                return retVal;
            });
            while(_internalBuffer.Count >= BufferSizeLimit - 1)
            {
                OnSignalRemove?.Invoke(this, _internalBuffer[_internalBuffer.Count - 1]);
                _internalBuffer.RemoveAt(_internalBuffer.Count - 1);
            }
        }
    }
}