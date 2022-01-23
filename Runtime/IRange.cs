using UnityEngine;

namespace HandcraftedGames.AIKit
{
    public interface IRange
    {
        Transform transform { get; }

        /// <summary>
        /// Computes intensity of overlapping of the point in this range.
        /// </summary>
        /// <param name="position">Position in world space</param>
        /// <returns>Value between 1.0 and 0.0</returns>
        float OverlappingValue(Vector3 position);
    }
}