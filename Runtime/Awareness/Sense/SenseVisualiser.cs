namespace HandcraftedGames.AIKit.Sense
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SenseVisualiser : MonoBehaviour
    {
        [SerializeReference]
        public ISense sense;

        private void OnEnable()
        {
            sense = GetComponent<ISense>();
        }

        private void OnDrawGizmosSelected()
        {
            if(sense == null || sense.memory == null)
                return;

            foreach(var signal in sense.memory.Buffer)
            {
                Gizmos.color = new Color(0, 0, 1, 1.0f - (Time.realtimeSinceStartup - signal.Timestamp) / sense.memory.TimeLimit);
                Gizmos.DrawSphere(signal.Position.center, signal.Position.radius);
            }
        }
    }
}