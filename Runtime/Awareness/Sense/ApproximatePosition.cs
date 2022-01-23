namespace HandcraftedGames.AIKit.Sense
{
    /// <summary>
    /// It constitutes a single approximated position. It's radius is the accuracy of the information.
    /// </summary>
    public struct ApproximatePosition
    {
        /// <summary>
        /// Center of the sphere.
        /// </summary>
        public UnityEngine.Vector3 center;

        /// <summary>
        /// Radius of the sphere.
        /// </summary>
        public float radius;

        public ApproximatePosition(UnityEngine.Vector3 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }
    }
}