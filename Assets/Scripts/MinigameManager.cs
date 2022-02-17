using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    int score;
    int lives;
    float gameSpeed;
    Queue<string> playedGames = new Queue<string>();
    public string[] minigameScenes;
    SceneLoader sceneLoader;

    private void Awake()
    {
        //Make sure that Minigame Manager persists thru scenes
        GameObject[] objects = GameObject.FindGameObjectsWithTag("MinigameManager");

        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void mainGameBegin()
    {
        score = 0;
        lives = 3;
        gameSpeed = 1.0f;
        playedGames.Clear();
    }
    public void minigameWin()
    {
        //Incriment score
        score++;
        
        //Check if score is multiple of 5
        //If so, speed up
        if (score % 5 == 0)
        {
            speedUp();
        }

        //New minigame
        chooseNewMinigame();
        

    }
    public void minigameFail()
    {
        //Take away one life
        lives--;

        //Check if player has 0 remainig lives
        //if so, end Main Game
        if (lives <= 0)
        {
            mainGameEnd();
        }

        //New minigame
        chooseNewMinigame();
    }
    public void addGameToQueue()
    {
        //Add current scene to queue
        Scene currentScene = SceneManager.GetActiveScene();
        playedGames.Enqueue(currentScene.name);

        //Limit queue to 4 games
        if (playedGames.Count >= 5)
        {
            playedGames.Dequeue();
        }
    }
    public void chooseNewMinigame()
    {
        int games = minigameScenes.Length;

        int chosenGameID = Random.Range(0, minigameScenes.Length);

        while (playedGames.Contains( minigameScenes[chosenGameID] ))
        {
            chosenGameID = Random.Range(0, minigameScenes.Length);
        }

        sceneLoader.SetScene( minigameScenes[chosenGameID] );

    }
    public void speedUp()
    {

    }
    public int getScore()
    {
        return score;
    }
    public void mainGameEnd()
    {
        sceneLoader.SetScene("MainMenu");
    }

}
