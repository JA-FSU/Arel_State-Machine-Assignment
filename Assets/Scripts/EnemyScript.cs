using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;

    public Transform Player;
    public Transform goal;
    public Transform[] navPoint;

    public int destPoint = 0;
    public float enemySpeed = 20.0f;
    public float playerDistance;
    public float awareAI = 0.1f;
    public float damping = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = goal.position;

    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(Player.position, transform.position);

        if (playerDistance < awareAI)
        {
            LookAtPlayer();
            Debug.Log("Seen");

            if (playerDistance < 2f)
            {
                Chase();
            }
        }
        else
        {
            GotoNextPoint();
        }


        if (agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }

    void LookAtPlayer()
    {
        transform.LookAt(Player);
    }


    void GotoNextPoint()
    {
        if (navPoint.Length == 0)
        {
            return;
        }
        agent.destination = navPoint[destPoint].position;
        destPoint = (destPoint + 1) % navPoint.Length;
    }


    void Chase()
    {
        transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
    }
}
