using UnityEngine;

namespace GameSections.Battles_DEPRECATED
{
    public class SpawnOnBeat : MonoBehaviour
    {
        public float threshold = 0.1f;
        public float spawnDelay = 0.25f;
        public int band = 0;

        private float[] spectrum = new float[1024];
        private float lastSpawnTime = 0f;

        private BeatScroller bs;
        private EnemyManager em;
        private void Start()
        {
            bs = GetComponent<BeatScroller>();
            em = FindObjectOfType<EnemyManager>();
        }

        void Update()
        {
            if (!em.isAlive) return;
            AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
            float average = 0;
            for (int i = 0; i < spectrum.Length; i++)
            {
                average += spectrum[i];
            }
            average /= spectrum.Length;

            if (average > threshold && Time.time - lastSpawnTime >= spawnDelay)
            {
                bs.SpawnEnemyNote();
                lastSpawnTime = Time.time;
            }
        }
    }
}

