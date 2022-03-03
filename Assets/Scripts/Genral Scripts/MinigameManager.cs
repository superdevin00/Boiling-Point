using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    int score;
    int lives;

    bool winConditionMet;
    bool minigameStarted;
    bool minigameEnded;

    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text timeText;
    public TMP_Text promptText;

    public GameObject canvas;
    public GameObject screenFloodImage;

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
        gameSpeed = 1.0f;
        sceneLoader = GetComponent<SceneLoader>();
        //lives = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //Set Score Text
        scoreText.text = score.ToString("D3");

        //Set Lives Text
        switch (lives)
        {
            case 3: 
                livesText.text = "~~~";
                break;
            case 2:
                livesText.text = "~~";
                break;
            case 1:
                livesText.text = "~";
                break;
            case 0:
                livesText.text = "X";
                break;
            default:
                livesText.text = "err";
                break;

        }


        //Turn Off minigame UI on Main Menu
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            canvas.SetActive(false);
        }
        else
        {
            canvas.SetActive(true);
        }

        //Update Timescale
        Time.timeScale = gameSpeed;

    }

    public void mainGameBegin()
    {
        score = 0;
        lives = 3;
        gameSpeed = 1.0f;
        playedGames.Clear();
        chooseNewMinigame();
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
        else
        {
            //New minigame
            chooseNewMinigame();
        }
        

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
        else
        {
            //New minigame
            chooseNewMinigame();
        }
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

        sceneLoader.StartSceneTransitionOut( minigameScenes[chosenGameID] );

    }

    public void initMinigame(string prompt, float time)
    {
        setWinConditionMet(false);
        StartCoroutine(minigameStartSequence(prompt, time));
    }

    IEnumerator minigameStartSequence(string prompt, float time)
    {
        setMinigameEnded(false);
        setMinigameStarted(false);
        promptText.text = prompt;
        yield return new WaitForSecondsRealtime(2);
        promptText.text = "Start!";
        yield return new WaitForSecondsRealtime(0.75f);
        promptText.text = "";
        StartCoroutine(minigameTimer(time));
        setMinigameStarted(true);

    }

    IEnumerator minigameTimer(float clockTime)
    {
        while (clockTime > 0)
        {
            clockTime -= Time.deltaTime;
            timeText.text = (clockTime).ToString("0");
            yield return null;
        }

        setMinigameEnded(true);

        if (winConditionMet)
        {
            minigameWin();
        }
        else
        {
            minigameFail();
        }
    }

    public void speedUp()
    {
        float newGameSpeed = 1.0f;
        if (gameSpeed <= 1.0f)
        {
            newGameSpeed = 2.0f;
        }

        StartCoroutine(speedUpSequence(newGameSpeed));
    }
    IEnumerator speedUpSequence(float newGameSpeed)
    {
        promptText.text = "Speed Up!";
        yield return new WaitForSecondsRealtime(2);
        gameSpeed = newGameSpeed;
        promptText.text = "";
        chooseNewMinigame();
    }
    public int getScore()
    {
        return score;
    }
    public void setWinConditionMet(bool winCon)
    {
        winConditionMet = winCon;
    }

    // Get/Set MinigameStarted
    public bool getMinigameStarted()
    {
        return minigameStarted;
    }
    public void setMinigameStarted(bool minigameStartSet)
    {
        minigameStarted = minigameStartSet;
    }

    // Get/Set MinigameEnded
    public bool getMinigameEnded()
    {
        return minigameEnded;
    }
    public void setMinigameEnded(bool minigameEndSet)
    {
        minigameEnded = minigameEndSet;
    }

    public void setScreenFlood(bool flood)
    {
        screenFloodImage.SetActive(flood);
    }


    public void mainGameEnd()
    {
        int highscore = PlayerPrefs.GetInt("highscore");

        if (score > highscore)
        {
            highscore = score;
        }

        PlayerPrefs.SetInt("highscore", highscore);
        PlayerPrefs.Save();

        sceneLoader.StartSceneTransitionOut("MainMenu");
    }

}
