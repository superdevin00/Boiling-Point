using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionManager : MonoBehaviour
{
    [SerializeField] Button[] buttons;

    [SerializeField] MinigameManager minigameManager;

    [SerializeField] GameObject win;
    [SerializeField] GameObject lose;

    [SerializeField] GameObject triangle;
    [SerializeField] GameObject square;
    [SerializeField] GameObject pentagon;

    GameObject[] shapes = new GameObject[3];
    int selectedIndex = -1;
    int correctAnswer;

    private void Start()
    {
        foreach (Button option in buttons)
        {
            option.interactable = true;
        }

        buttons[0].onClick.AddListener(clickZero);
        buttons[1].onClick.AddListener(clickOne);
        buttons[2].onClick.AddListener(clickTwo);

        minigameManager.initMinigame("Look Closely!", 8);

        correctAnswer = Random.Range(0, 3);

        shapes[0] = triangle;
        shapes[1] = square;
        shapes[2] = pentagon;
        
        for (int i = 0; i < 3; i++)
        {
            shapes[i].SetActive(i == correctAnswer);
        }
    }

    private void Update()
    {


        shapes[correctAnswer].transform.position = new Vector3(shapes[correctAnswer].transform.position.x + (10 * Time.deltaTime), shapes[correctAnswer].transform.position.y, shapes[correctAnswer].transform.position.z);

        if(selectedIndex != -1)
        {
            if(selectedIndex == correctAnswer)
            {
                win.SetActive(true);
                minigameManager.setWinConditionMet(true);
            }
            else
            {
                lose.SetActive(true);
                minigameManager.setWinConditionMet(false);
            }
        }
    }

    void clickZero()
    {
        buttons[0].Select();
        buttons[0].OnSelect(null);
        
        selectedIndex = 0;
        DisableAllTiles();
        
    }

    void clickOne()
    {
        buttons[1].Select();
        buttons[1].OnSelect(null);
        selectedIndex = 1;
        DisableAllTiles();
    }

    void clickTwo()
    {
        buttons[2].Select();
        buttons[2].OnSelect(null);
        selectedIndex = 2;
        DisableAllTiles();
    }

    void DisableAllTiles()
    {
        for(int i = 0; i < 3; i++)
        {
            if(i != selectedIndex)
            {
                buttons[i].interactable = false;
            }
            else
            {
                buttons[i].enabled = false;
            }
        }
    }

    
}
