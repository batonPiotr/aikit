namespace HandcraftedGames.AIKit.Awareness
{
    using System;
    using System.Collections.Generic;
    using HandcraftedGames.AIKit.Sense;

    public class BaseVoxelMap : IVoxelMap
    {
        List<Voxel> voxels = new List<Voxel>();

        public void Add(ISenseSignal signal)
        {
            var roundedPos = signal.Position.center;
            roundedPos.x = ((int)roundedPos.x);
            roundedPos.y = ((int)roundedPos.y);
            roundedPos.z = ((int)roundedPos.z);
            int found = -1;
            try
            {
                found = voxels.FindIndex(i => i.position.x == roundedPos.x && i.position.y == roundedPos.y && i.position.z == roundedPos.z);
            }
            catch (System.Exception)
            {
            }
            if(found == -1)
            {
                voxels.Add(new Voxel());
                found = voxels.Count - 1;
            }
            if(voxels[found].signals == null)
            {
                var v = voxels[found];
                v.signals = new List<ISenseSignal>();
                voxels[found] = v;
            }
            var voxel = voxels[found];
            voxel.signals.Add(signal);
            voxel.position = roundedPos;
            voxels[found] = voxel;
        }

        public IEnumerable<Voxel> All()
        {
            return voxels;
        }

        public IEnumerable<Voxel> Query(Func<Voxel, bool> predicate)
        {
            return voxels.FindAll(i => predicate(i));
        }

        public void Remove(IEnumerable<ISenseSignal> signals)
        {
            foreach(var voxel in voxels)
            {
                foreach(var signal in signals)
                    voxel.signals.Remove(signal);
            }
        }
    }
}