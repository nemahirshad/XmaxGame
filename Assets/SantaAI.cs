using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SantaAI : MonoBehaviour
{
    public List<Transform> points;

    NavMeshAgent agent;

    Transform followPos;

    int randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        //Get a random patrol point
        agent = GetComponent<NavMeshAgent>();
        randomNumber = Random.Range(0, 3);
        followPos = points[randomNumber];
    }

    // Update is called once per frame
    void Update()
    {
        //If reached destination then get a new random patrol point
        if (Vector3.Distance(transform.position, points[randomNumber].position) < 2)
        {
            randomNumber = Random.Range(0, 3);
            followPos = points[randomNumber];
        }

        agent.SetDestination(followPos.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Follow player as long as it is within range
        if (other.CompareTag("Player"))
        {
            followPos = other.transform;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If santa touches a player then respawn them outside of the game area
    }

    private void OnTriggerExit(Collider other)
    {
        //Return to patrolling
        if (other.CompareTag("Player"))
        {
            followPos = points[randomNumber];
        }
    }
}
