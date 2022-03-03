using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisions : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] Transform ballSpawn;

    MinigameManager minigameManager;
    private void Start()
    {
        minigameManager = GameObject.FindGameObjectWithTag("MinigameManager").GetComponent<MinigameManager>();
        minigameManager.initMinigame("Make 1 Goal!", 5);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("net"))
        {
            //add score
            //play particles
            //play sound
            Destroy(gameObject, .1f);
            Instantiate(ball, ballSpawn.position, Quaternion.identity);
        }

        if (collision.gameObject.tag.Equals("corner") || collision.gameObject.tag.Equals("boundry"))
        {
            //play particles
            //play sound
            Destroy(gameObject, .1f);
            Instantiate(ball, ballSpawn.position, Quaternion.identity);
        }

        void Start()
        {
            StartCoroutine(SelfDestruct());
        }

        IEnumerator SelfDestruct()
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
            Instantiate(ball, ballSpawn.position, Quaternion.identity);
        }
    }
}
