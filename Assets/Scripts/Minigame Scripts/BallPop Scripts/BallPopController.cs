using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPopController : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] Transform spawn_L;
    [SerializeField] Transform spawn_M;
    [SerializeField] Transform spawn_R;
    GameObject spawnedBall;

    [SerializeField] float spawnSpeed = 1f;

    [SerializeField] ParticleSystem poppedParticles;
    [SerializeField] AudioSource poppedAudio;

    MinigameManager minigameManager;

    public int score = 0;

    private void Start()
    {
        minigameManager = GameObject.FindGameObjectWithTag("MinigameManager").GetComponent<MinigameManager>();
        minigameManager.initMinigame("Pop 12 Balls!", 12);

        ChooseSpawn();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) /*Input.touchCount > 0 /*&& Input.GetTouch(0).phase == TouchPhase.Began*/)
        {
            Debug.Log("Mouse click");

            Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition); //Input.GetTouch(0).position);
            RaycastHit raycastHit;

            if (Physics.Raycast(raycast, out raycastHit))
            {   
                if (raycastHit.collider.tag == "ball")
                {
                    Debug.Log("Ball clicked");
                    spawnedBall = raycastHit.collider.gameObject;

                    score += 1;

                    poppedParticles.transform.position = spawnedBall.transform.position;
                    poppedParticles.Play();

                    poppedAudio.Play();

                    Destroy(spawnedBall);  
                }
            }
        }

        if(score >= 12)
        {
            minigameManager.setWinConditionMet(true);
            Debug.Log("Win Condition Met");
        }
    }

    void ChooseSpawn()
    {
        var RandomSpawn = Random.Range(0, 3);

        if (RandomSpawn == 0)
        {
            spawnedBall = Instantiate(ball, spawn_L.transform.position, Quaternion.identity);
        }
        else if (RandomSpawn == 1)
        {
            spawnedBall = Instantiate(ball, spawn_M.transform.position, Quaternion.identity);
        }
        else if (RandomSpawn == 2)
        {
            spawnedBall = Instantiate(ball, spawn_R.transform.position, Quaternion.identity);
        }

        StartCoroutine(SpawnTimer());
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(spawnSpeed);
        ChooseSpawn();
    }
}
