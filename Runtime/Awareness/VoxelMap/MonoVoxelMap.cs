namespace HandcraftedGames.AIKit.Awareness
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using HandcraftedGames.AIKit.Sense;
    using HandcraftedGames.Common;
    using UnityEngine;

    public class MonoVoxelMap : MonoBehaviour
    {
        private IVoxelMap _voxelMap;

        private void Awake()
        {
            _voxelMap = new BaseVoxelMap();
            gameObject.GetGODependencyInjection().Register(_voxelMap);
        }
    }
}