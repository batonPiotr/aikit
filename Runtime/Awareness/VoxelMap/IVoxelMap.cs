namespace HandcraftedGames.AIKit.Awareness
{
    using System.Collections.Generic;
    using HandcraftedGames.AIKit.Sense;
    using UnityEngine;

    public struct Voxel
    {
        public Vector3 position;
        public List<ISenseSignal> signals;
    }

    public interface IVoxelMap
    {
        void Add(ISenseSignal signal);
        void Remove(IEnumerable<ISenseSignal> signals);
        IEnumerable<Voxel> Query(System.Func<Voxel, bool> predicate);
        IEnumerable<Voxel> All();
    }
}