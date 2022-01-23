namespace HandcraftedGames.AIKit.Sense
{
    using System;
    using HandcraftedGames.Common;
    using UnityEngine;
    public class AudioDataSenseMono: MonoBehaviour
    {
        private AudioDataSense sense;

        private void Awake()
        {
            var memory = new BaseSenseMemory();
            sense = new AudioDataSense(memory);
            gameObject.GetGODependencyInjection().Register(sense);
            gameObject.GetGODependencyInjection().Register(memory);
        }

        private void OnEnable()
        {
            sense.Enable();
        }

        private void OnDisable()
        {
            sense.Disable();
        }

        private void OnTriggerStay(Collider other)
        {
            var audioDataEmitter = other.gameObject.GetComponent<AudioDataSenseEmitter>();
            if(audioDataEmitter != null)
                sense.OnDetect(audioDataEmitter, (transform.position - other.transform.position).magnitude * 0.5f);
        }

        private void OnDestroy()
        {
            sense = null;
        }
    }
}