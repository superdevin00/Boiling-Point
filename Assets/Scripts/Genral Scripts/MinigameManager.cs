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
    bool loseConditionMet;
    bool minigameStarted;
    bool minigameEnded;

    public TMP_Text scoreText;
    public TMP_Text livesText;
    public Image livesImage;
    public Sprite[] livesSprites;
    public TMP_Text timeText;
    public TMP_Text promptText;

    public GameObject canvas;
    public GameObject screenFloodImage;

    public ParticleSystem confetti;
    public ParticleSystem flames;
    private Vector3 confettiBasePos;
    private Vector3 confettiBaseScale;
    private Vector3 flamesBasePos;
    private Vector3 flamesBaseScale;

    float gameSpeed;
    int gameDifficulty;
    float timeRemaining;

    Queue<string> playedGames = new Queue<string>();
    public string[] minigameScenes;
    SceneLoader sceneLoader;
    //GameObject mainCamera;

    public AudioSource audioSource;
    public AudioClip winSFX;
    public AudioClip loseSFX;
    


    private void Awake()
    {
        //Make sure that Minigame Manager persists thru scenes
        GameObject[] objects = GameObject.FindGameObjectsWithTag("MinigameManager");

        if (objects.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        confettiBasePos = confetti.transform.position;
        confettiBaseScale = confetti.transform.localScale;
        flamesBasePos = flames.transform.position;
        flamesBaseScale = flames.transform.localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameDifficulty = 1;
        gameSpeed = 1.0f;
        sceneLoader = GetComponent<SceneLoader>();
        //mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
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
                livesImage.sprite = livesSprites[3];
                break;
            case 2:
                livesImage.sprite = livesSprites[2];
                break;
            case 1:
                livesImage.sprite = livesSprites[1];
                break;
            case 0:
                livesImage.sprite = livesSprites[0];
                break;
            default:
                livesImage.sprite = livesSprites[0];
                //livesText.text = "err";
                break;

        }


        //Turn Off minigame UI on Main Menu
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            canvas.SetActive(false);
            setLoseConditionMet(false);
            setWinConditionMet(false);
        }
        else
        {
            canvas.SetActive(true);
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MiniGamePiano"))
        {
            confetti.transform.position = new Vector3(7.2f, 31.8f,-3.1f);
            flames.transform.position = new Vector3(8f, -1f, -1.6f);
            flames.transform.localScale = new Vector3(1.4f, 1.4f, 1);
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ObjectDetection"))
        {
            confetti.transform.position = new Vector3(0, 7.4f, -6.0f);
            confetti.transform.localScale = new Vector3(0.32f, 0.32f, 0.32f);
            flames.transform.position = new Vector3(0, -6.8f, -5f);
            flames.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("QuickTap"))
        {
            confetti.transform.position = new Vector3(0, 17.8f, 12.2f);
            confetti.transform.localScale = new Vector3(1, 1, 1);
            flames.transform.position = new Vector3(-0.1f, -5.9f, 0.2f);
            flames.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("BallPop"))
        {
            confetti.transform.position = new Vector3(0, 17.8f, 3.8f);
            confetti.transform.localScale = new Vector3(1, 1, 1);
            flames.transform.position = new Vector3(-0.1f, -2.7f, 0.2f);
            flames.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            confetti.transform.position = confettiBasePos;
            flames.transform.position = flamesBasePos;
            confetti.transform.localScale = confettiBaseScale;
            flames.transform.localScale = flamesBaseScale;
        }

        //Check for winstate for confetti
        if (winConditionMet)
        {
            if (!confetti.isPlaying)
            {
                confetti.Play();
                audioSource.PlayOneShot(winSFX,10.0f);
            }

        }
        else
        {
            if (confetti.isPlaying)
            {
                confetti.Stop();
                confetti.Clear();
            }
        }

        //Check for losestate for flames
        if (loseConditionMet)
        {
            if (!flames.isPlaying)
            {
                flames.Play();
                audioSource.PlayOneShot(winSFX);
            }
        }
        else
        {
            if (flames.isPlaying)
            {
                flames.Stop();
                flames.Clear();
            }
        }

        //Update Timescale
        Time.timeScale = gameSpeed;

    }

    public void mainGameBegin()
    {
        score = 0;
        lives = 3;
        gameSpeed = 1.0f;
        gameDifficulty = 1;
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

        //Limit queue to 1 games
        if (playedGames.Count >= 2)
        {
            playedGames.Dequeue();
        }
    }
    public void chooseNewMinigame()
    {
        addGameToQueue();

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
        setLoseConditionMet(false);
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
            timeRemaining = clockTime;
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
        //increase difficulty

        gameDifficulty++;
        float newGameSpeed = 1.0f;
        switch (gameDifficulty)
        {
            case 0: newGameSpeed = 1.0f; break;
            case 1: newGameSpeed = 1.0f; break;
            case 2: newGameSpeed = 1.5f; break;
            case 3: newGameSpeed = 1.75f; break;
            case 4: newGameSpeed = 2; break;
            case 5: newGameSpeed = 2.25f; break;
            default: newGameSpeed = 2.5f; break;

        }
        if (gameDifficulty <= 1)
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
    public void setLoseConditionMet(bool loseCon)
    {
        loseConditionMet = loseCon;
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

    public float getTimeRemaining()
    {
        return timeRemaining;
    }
    public int getDifficultyLevel()
    {
        return gameDifficulty;
    }
    public void mainGameEnd()
    {
        gameSpeed = 1.0f;
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
