using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button[] buttons;
    int index;

    int score = 0;
    public Text scoreTxt;

    float changeSpeed = 1f;

    private void Start()
    {
        ChooseTile();
    }

    private void Update()
    {
        if (changeSpeed <= 0)
        {
            score -= 1;
        }
    }

    public void OnClick()
    {
        score += 1;
        scoreTxt.text = "Score: " + score.ToString();
        buttons[index].interactable = false;
        ChooseTile();
    }

    private void ChooseTile()
    {
        index = Random.Range(0, buttons.Length);
        buttons[index].interactable = true;

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        changeSpeed -= Time.deltaTime;
        yield return new WaitForSeconds(changeSpeed);
    }

}
