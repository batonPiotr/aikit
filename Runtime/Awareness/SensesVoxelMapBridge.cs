namespace HandcraftedGames.AIKit.Awareness
{
    using System.Collections;
    using System.Collections.Generic;
    using HandcraftedGames.AIKit.Sense;
    using HandcraftedGames.Common;
    using UnityEngine;

    public class SensesVoxelMapBridge : MonoBehaviour
    {
        private IVoxelMap voxelMap;
        private IList<ISense> senses;

        public List<GameObject> sensesDependencies;
        public GameObject voxelMapDependency;

        private void Awake()
        {
            var di = gameObject.GetGODependencyInjection();

            di.AddDependency(voxelMapDependency);
            di.AddDependencies(sensesDependencies);
            di.AddDependencyRequest(resolver => {
                this.voxelMap = resolver.Resolve<IVoxelMap>();
                this.senses = resolver.ResolveAll<ISense>();
                foreach(var sense in senses)
                {
                    sense.memory.OnSignalAdd += OnSignalAdd;
                    sense.memory.OnSignalRemove += OnSignalRemove;
                }
                return true;
            });

        }

        private void OnSignalAdd(ISenseMemory memory, ISenseSignal signal)
        {
            voxelMap.Add(signal);
        }

        private void OnSignalRemove(ISenseMemory memory, ISenseSignal signal)
        {
            voxelMap.Remove(new ISenseSignal[] { signal });
        }

        private void OnDrawGizmosSelected()
        {
            if(voxelMap == null || senses == null || senses.Count == 0)
                return;
            var voxels = voxelMap.All();
            foreach(var voxel in voxels)
            {
                Gizmos.color = new Color(0.0f, 1.0f, 0.0f, (float)voxel.signals.Count / 10.0f);
                Gizmos.DrawCube(voxel.position + new Vector3(0.5f, 0.5f, 0.5f), Vector3.one);
            }
        }
    }
}