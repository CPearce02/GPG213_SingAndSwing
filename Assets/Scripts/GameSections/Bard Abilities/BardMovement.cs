using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BardMovement : MonoBehaviour
{
    public GameObject knight;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        knight = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(knight.transform.position);
    }
}
