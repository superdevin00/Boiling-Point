using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisions : MonoBehaviour
{
    [SerializeField] GameObject activeBall;
    [SerializeField] GameObject newBall;
    [SerializeField] Transform ballSpawn;

    public Swipe swipe;

    [SerializeField] ParticleSystem goalParticles;
    [SerializeField] AudioSource goalAudio;

    [SerializeField] AudioSource resetAudio;

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
            Debug.Log("GOAL!");

            goalParticles.Play();
            goalAudio.Play();

            NewShot();
        }

        else if (collision.gameObject.tag.Equals("boundry"))
        {
            resetAudio.Play();

            NewShot();
        }
    }

    private void NewShot()
    {
        Instantiate(newBall, ballSpawn.position, Quaternion.identity);
        Destroy(gameObject, .2f);
        swipe.canSwipe = true;
        Debug.Log("canSwipe = true");
    }
}
