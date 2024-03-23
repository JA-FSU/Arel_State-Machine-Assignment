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
    public float enemySpeed = 5.0f;
    public float playerDistance;
    public float awareAI = 10f;
    public float damping = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
