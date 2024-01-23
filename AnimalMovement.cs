using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalMovement : MonoBehaviour
{
    GameObject animal;
    Animator animator; 
    NavMeshAgent agent;

    [SerializeField] LayerMask groundLayer, playerLayer;

    //patrol
    Vector3 destPoint;
    bool walkpointSet; //do we have a destination

    [SerializeField] float range;



    bool isTimerActive;
    float timerDuration;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animal = GameObject.Find("duck");
        animator = animal.GetComponent<Animator>();
        walkpointSet = false;
        isTimerActive = false;
        timerDuration = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimerActive)
        {
            Patrol();
        }
        else
        {
            // Duck is waiting, decrement timer
            timerDuration -= Time.deltaTime;
            if (timerDuration <= 0f)
            {
                // Timer expired, resume patrol
                isTimerActive = false;
            }
        }
    }


    void Patrol()
    {
        if (!walkpointSet) SearchForDest();
        if (walkpointSet)
        {
            agent.SetDestination(destPoint);
            animator.SetBool("isRunning", true);
        }

        if (Vector3.Distance(transform.position, destPoint) < 2)
        {
            walkpointSet = false;
            StartTimer();
            animator.SetBool("isRunning", false);
        }
    }

    void SearchForDest()
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destPoint, Vector3.down, groundLayer))
        {
            walkpointSet = true;
        }
    }

    void StartTimer()
    {
        timerDuration = Random.Range(5f, 15f);
        isTimerActive = true;
    }
}
