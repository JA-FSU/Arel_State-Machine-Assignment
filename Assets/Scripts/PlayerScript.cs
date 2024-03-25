using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject winText;
    public GameObject loseText;

    private float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        winText.SetActive(false);
        loseText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float moveAxis = Input.GetAxis("Vertical");
        transform.Translate(moveAxis * Vector3.forward * speed * Time.deltaTime);

        float moveAxisB = Input.GetAxis("Horizontal");
        transform.Translate(moveAxisB * Vector3.right * speed * Time.deltaTime);

        // Sprint script
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed += 1;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed -= 1;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Test");
            GameWon();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            GameLost();
        }
    }

    void GameWon()
    {
        gameManager.gameActive = false;
        Time.timeScale = 0.0f;
        winText.SetActive(true);
    }

    void GameLost()
    {
        gameManager.gameActive = false;
        Time.timeScale = 0.0f;
        loseText.SetActive(true);
    }
}
