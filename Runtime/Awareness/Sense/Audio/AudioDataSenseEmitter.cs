namespace HandcraftedGames.AIKit.Sense
{
    using UnityEngine;

    public class AudioDataSenseEmitter : MonoBehaviour
    {
        public SphereCollider sphereCollider;
        public AudioSource audioSource;
        public float maxRadius = 100.0f;
        public int samplesPowExponent = 3;
        public int channel = 0;

        private float[] samples;

        private void OnEnable()
        {
            samples = new float[(int)Mathf.Pow(2.0f, samplesPowExponent)];
        }

        private void FixedUpdate()
        {
            if(sphereCollider == null || audioSource == null)
                return;
            audioSource.GetOutputData(samples, channel);
            var sum = 0.0f;
            foreach(var s in samples)
            {
                sum += s;
            }
            sphereCollider.enabled = Mathf.Abs(sum) > 0.01f;
            sphereCollider.radius = (Mathf.Abs(sum) / (float)samples.Length) * maxRadius;
        }
    }
}