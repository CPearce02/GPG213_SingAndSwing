using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.ScriptableObjects;
using Core.Player;

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
                if (gameObject.tag == "Healing")
                {
                    PlayersManager.instance.HealPlayer();
                } 
                //enemy attack
                else if (gameObject.tag == "Attack")
                {
                    Destroy(gameObject);
                }
                else
                {
                    //DETERMINE HOW ACCURATE THE HIT IS
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
