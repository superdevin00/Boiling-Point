using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button[] buttons;
    int index;
    int indexNew;
    bool firstTileSet;

    int score = 0;
    public Text scoreTxt;

    float changeSpeed = 1f;

    public MinigameManager minigameManager;

    [SerializeField] AudioSource clickAudio;

    private void Start()
    {

        minigameManager = GameObject.FindGameObjectWithTag("MinigameManager").GetComponent<MinigameManager>();

        firstTileSet = false;
        minigameManager.initMinigame("Tap 15 Tiles!", 15);
    }

    private void Update()
    {
        if (minigameManager.getMinigameEnded() == false)
        {
            if (minigameManager.getMinigameStarted() == true && firstTileSet == false)
            {
                ChooseTile();
                firstTileSet = true;
            }

            if (changeSpeed <= 0)
            {
                score -= 1;
            }

            if (score >= 15)
            {
                minigameManager.setWinConditionMet(true);
                Debug.Log("Win Condition Met");
            }
        }
        else
        {
            DisableAllTiles();
        }
    }

    public void OnClick()
    {
        score += 1;
        scoreTxt.text = score.ToString();
        buttons[index].interactable = false;
        ChooseTile();

        clickAudio.Play();
    }

    public void DisableAllTiles()
    {
        foreach (Button tile in buttons)
        {
            tile.interactable = false;
        }
    }

    private void ChooseTile()
    {
        //Pick new tile
        indexNew = Random.Range(0, buttons.Length);

        //Make sure new tile isnt same as old.
        while (index == indexNew)
        {
            indexNew = Random.Range(0, buttons.Length);
        }
        index = indexNew;

        //Allow new tile to be clicked
        buttons[index].interactable = true;

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        changeSpeed -= Time.deltaTime;
        yield return new WaitForSeconds(changeSpeed);
    }

}
