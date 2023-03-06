using Core.Player;
using Core.ScriptableObjects;
using UnityEngine;

namespace GameSections.Battles_DEPRECATED
{
    public class NoteController : MonoBehaviour
    {
        public KeyCode keyToPress;
        private bool canBePressed;

        private GameObject effectSpawn;
        public GameObject hitEffect, goodEffect, perfectEffect;

        public DamageType noteDamageType;

        // Start is called before the first frame update
        void Start()
        {
            effectSpawn = GameObject.FindGameObjectWithTag("Spawn");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(keyToPress))
            {
                if (canBePressed)
                {
                    //DETERMINE NOTE TYPE
                    //healing
                    if (gameObject.tag == "Attack")
                    {
                        Destroy(gameObject);
                    } 
                    //enemy attack
                    else if (noteDamageType.name == "Healing")
                    {
                        PlayersManager.instance.HealPlayer(noteDamageType.BaseDamage);
                    }
                    else
                    {
                        //DETERMINE HOW ACCURATE THE HIT IS
                        //Normal Hit
                        if (Mathf.Abs(transform.position.x) > 0.25)
                        {
                            Debug.Log("Normal");
                            GameManager.instance.NoteHit(noteDamageType, noteDamageType.BaseDamage);
                            Instantiate(hitEffect, effectSpawn.transform.position, Quaternion.identity);
                        }
                        //Good Hit
                        else if (Mathf.Abs(transform.position.x) > 0.05F)
                        {
                            Debug.Log("Good");
                            GameManager.instance.NoteHit(noteDamageType, noteDamageType.BaseDamage + 2);
                            Instantiate(goodEffect, effectSpawn.transform.position, Quaternion.identity);
                        }
                        //Perfect Hit
                        else
                        {
                            Debug.Log("Perfect");
                            GameManager.instance.NoteHit(noteDamageType, noteDamageType.BaseDamage + 4);
                            Instantiate(perfectEffect, effectSpawn.transform.position, Quaternion.identity);
                        }
                    }

                    Destroy(gameObject);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Activator")
            {
                canBePressed = true;
            }

            if(collision.tag == "Destroyer")
            {
                //Do damage or remove multipliyer 
                GameManager.instance.NoteMissed();
                Destroy(gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Activator")
            {
                canBePressed = false;
            }
        }
    }
}
