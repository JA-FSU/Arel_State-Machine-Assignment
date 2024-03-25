using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    private GameManager gameManager;

    public Transform Player;
    public Transform goal;
    public Transform[] navPoint;

    private int destPoint = 0;
    [SerializeField] private int randNum = 0;
    public float enemySpeed;
    private float playerDistance;
    private float awareAI = 3f;
    private bool isChasing = false;
    private bool isLooking = false;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = goal.position;

        agent.autoBraking = false;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(Player.position, transform.position);

        if (randNum != 10)
        {
            randNum = Random.Range(1, 1000);
        }

        if (playerDistance < awareAI)
        {
            if (playerDistance < awareAI)
            {
                LookAtPlayer();
                Debug.Log("Seen");
                StopCoroutine("LookAround");
                agent.isStopped = false;
                Chase();
            }
            else
            {
                GotoNextPoint();
            }
        }

        if (randNum == 10 && !isChasing && !isLooking)
        {
            StartCoroutine("LookAround");
        }
        else if (agent.remainingDistance < 0.5f)
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
        isChasing = true;
        transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);

        if (playerDistance > awareAI && gameManager.gameActive)
        {
            isChasing = false;
            GotoNextPoint();
        }
    }

    IEnumerator LookAround()
    {
        isLooking = true;
        agent.isStopped = true;
        int randTimes = Random.Range(1, 3);

        float turnTime = 50.0f;
        Quaternion targetO = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y+50.0f, transform.eulerAngles.z);
        Quaternion targetT = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y-100.0f, transform.eulerAngles.z);


        if (randTimes > 0)
        {
            yield return new WaitForSeconds(1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetO, turnTime * Time.deltaTime);
            yield return new WaitForSeconds(1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetT, turnTime * Time.deltaTime);
            yield return new WaitForSeconds(2);
            transform.LookAt(agent.destination);

            randTimes -= 1;
        }
        randNum = 0;
        agent.isStopped = false;
        isLooking = false;
        StopCoroutine("LookAround");
    }
}
