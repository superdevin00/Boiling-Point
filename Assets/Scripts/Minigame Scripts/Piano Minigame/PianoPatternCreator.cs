using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PianoPatternCreator : MonoBehaviour
{
    [SerializeField] GameObject tile;
    [SerializeField] GameObject row;
    [SerializeField] Camera mainCamera;
    MinigameManager minigameManager;
    public bool[,] pattern = new bool[10, 4];
    public GameObject[] rows = new GameObject[10];
    public int currentRow;
    float targetVertical;
    public bool failed = false;

    void Start()
    {
        minigameManager = GameObject.FindGameObjectWithTag("MinigameManager").GetComponent<MinigameManager>();
        minigameManager.initMinigame("Tap 10 Keys!", 10);
        //Debug.Log("Fill Pattern Start");
        fillPattern();
        //Debug.Log("Fill Pattern Done");
        createRows();
        //Debug.Log("Create Rows Done");
        targetVertical = transform.position.y;
        
    }

    void Update()
    {
        if (transform.position.y > targetVertical)
        {
            //transform.position.Set(transform.position.x, transform.position.y + Mathf.Sign(targetVertical - transform.position.y) * Mathf.Min(Mathf.Abs(targetVertical - transform.position.y), 0.5f) * Time.deltaTime, transform.position.z);
            float changeValue = Mathf.Max(targetVertical - transform.position.y, -0.5f);
            transform.position = new Vector3(transform.position.x, transform.position.y + (changeValue * Time.deltaTime * 100), transform.position.z);
            for (int i = 0; i < 10; i++)
            {
                rows[i].transform.position = new Vector3(rows[i].transform.position.x, rows[i].transform.position.y + (changeValue * Time.deltaTime * 100), rows[i].transform.position.z);
                for(int j = 0; j < 4; j++)
                {
                    PianoTileRow theRow = rows[i].GetComponent(typeof(PianoTileRow)) as PianoTileRow;
                    theRow.tiles[j].transform.position = new Vector3(theRow.tiles[j].transform.position.x, theRow.tiles[j].transform.position.y + (changeValue * Time.deltaTime * 100), theRow.tiles[j].transform.position.z);
                }
            }

        }
        
    }

    void fillPattern()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                //Debug.Log(i);
                //Debug.Log(j);
                pattern[i, j] = false;
            }

        }

        int previous = -1;
        for(int i = 0; i < 10; i++)
        {
            int temp;
            do
            {
                temp = Random.Range(0, 4);
            } while (previous == temp);
            //Debug.Log(temp);
            pattern[i, temp] = true;
        }
    }

    void createRows()
    {
        for (int i = 0; i < 10; i++)
        {
            rows[i] = Instantiate(row, new Vector3(0,(float)(3.75 + (i * 7.5)), 0),Quaternion.identity);
            PianoTileRow temp = rows[i].GetComponent(typeof(PianoTileRow)) as PianoTileRow;
            temp.parentCreator = gameObject.GetComponent(typeof(PianoPatternCreator)) as PianoPatternCreator;
            temp.index = i;
            if (i == 0)
            {
                temp.locked = false;
            }
            else
            {
                temp.locked = true;
            }
            temp.minigameManager = minigameManager;
            temp.createTiles();
        }
    }

    public void updateTargetVertical(float f)
    {
        targetVertical = f;
    }

    public void failGame()
    {
        targetVertical = transform.position.y;
        for (int i = 0; i < 10; i++)
        {
            PianoTileRow temp = rows[i].GetComponent(typeof(PianoTileRow)) as PianoTileRow;
            temp.setLockTiles(true);
            failed = true;
        }
        mainCamera.backgroundColor = new Color(255f, 0f, 0f);
        minigameManager.setWinConditionMet(false);
    }

    public void winGame()
    {
        mainCamera.backgroundColor = new Color(0f, 255f, 0f);
        minigameManager.setWinConditionMet(true);
    }
}
