using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.ScriptableObjects;

public class NoteController : MonoBehaviour
{
   // public float speed;
    //Rigidbody2D rb;
    public KeyCode keyToPress;
    private bool canBePressed;

    private GameObject effectSpawn;
    public GameObject hitEffect, goodEffect, perfectEffect;

    //public enum NoteType {Fire, Water, Earth, Lightning, Wind, Healing, Damage}
    //public NoteType nT;

    public DamageType noteDamageType;

    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        effectSpawn = GameObject.FindGameObjectWithTag("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector2(-speed, 0);

        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                //DETERMINE TYPE
                //healing
                if (gameObject.tag == "Healing")
                {
                    GameManager.instance.HealHero();
                } 
                //enemy attack
                else if (gameObject.tag == "Attack")
                {
                    Destroy(gameObject);
                }
                else
                {
                    //DETERMINE THE TYPE OF HIT
                    //Normal Hit
                    if (Mathf.Abs(transform.position.x) > 0.25)
                    {
                        Debug.Log("Normal");
                        GameManager.instance.NoteHit(noteDamageType, 1);
                        Instantiate(hitEffect, effectSpawn.transform.position, Quaternion.identity);
                    }
                    //Good Hit
                    else if (Mathf.Abs(transform.position.x) > 0.05F)
                    {
                        Debug.Log("Good");
                        GameManager.instance.NoteHit(noteDamageType, 3);
                        Instantiate(goodEffect, effectSpawn.transform.position, Quaternion.identity);
                    }
                    //Perfect Hit
                    else
                    {
                        Debug.Log("Perfect");
                        GameManager.instance.NoteHit(noteDamageType, 5);
                        Instantiate(perfectEffect, effectSpawn.transform.position, Quaternion.identity);
                    }
                }
                //enemy attack
                Destroy(gameObject);
            }
            else
            {
                GameManager.instance.ResetMultiplier();
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
            //MAYBE ONLY DO DAMAGE WHEN HIT BY THE ENEMY AND NOT WHEN THE PLAYER MISSES A NOTE - LOSE MULTIPLIER
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
