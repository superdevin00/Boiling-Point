using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    MinigameManager minigameManager;
    public TMP_Text highscoreText;
    int highscore;

    // Start is called before the first frame update
    void Start()
    {
        minigameManager = GameObject.FindGameObjectWithTag("MinigameManager").GetComponent<MinigameManager>();

        highscore = PlayerPrefs.GetInt("highscore");
        highscoreText.text = "High Score:\n" + highscore.ToString("D3");
        
    }
    public void PlayButtonPressed()
    {
        minigameManager.mainGameBegin();
    }
}
