using UnityEngine;

namespace GameSections.Battles_DEPRECATED
{
    public class BeatScroller : MonoBehaviour
    {
        public float beatTempo;

        public bool hasStarted;

        public GameObject spawnPostion;

        public Vector3 enemyNoteSpawn;
        public GameObject enemyNotePb;

        // Start is called before the first frame update
        void Start()
        {
            beatTempo = beatTempo / 60f;
            enemyNoteSpawn = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (!hasStarted) return;
            transform.position -= new Vector3(beatTempo * Time.deltaTime, 0f);
        }

        public void SpawnNote(GameObject note_pb)
        {
            GameObject note = Instantiate(note_pb, spawnPostion.transform.position, Quaternion.identity);
            note.transform.parent = transform;
        }

        public void SpawnEnemyNote()
        {
            GameObject enemyNote = Instantiate(enemyNotePb, enemyNoteSpawn, Quaternion.identity);
            enemyNote.transform.parent = transform;
        }
    }
}
