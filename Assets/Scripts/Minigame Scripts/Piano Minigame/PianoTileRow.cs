using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoTileRow : MonoBehaviour
{
    [SerializeField] GameObject tile;
    
    public bool locked;
    public bool failed;
    public bool correct;
    public PianoPatternCreator parentCreator;
    public int index;
    public GameObject[] tiles = new GameObject[4];
    //int currentRow = 0;
    // Start is called before the first frame update
    void Start()
    {
        //locked = true;
        failed = false;
        correct = false;
        //createTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setLockTiles(bool l)
    {
        for (int i = 0; i < 4; i++)
        {
            PianoTileController temp = tiles[i].GetComponent(typeof(PianoTileController)) as PianoTileController;
            temp.locked = l;
        }
    }

    public void setFailedTiles(bool f)
    {
        for (int i = 0; i < 4; i++)
        {
            PianoTileController temp = tiles[i].GetComponent(typeof(PianoTileController)) as PianoTileController;
            temp.failed = f;
        }
    }

    public void createTiles()
    {
        for(int i = 0; i < 4; i++)
        {
            tiles[i] = Instantiate(tile, new Vector3((float)(1.75 + (i * 3.75)), transform.position.y, 0), Quaternion.identity);
            PianoTileController temp = tiles[i].GetComponent(typeof(PianoTileController)) as PianoTileController;
            temp.parentRow = gameObject.GetComponent(typeof(PianoTileRow)) as PianoTileRow;
            temp.black = parentCreator.pattern[index, i];
            temp.locked = locked;

        }
    }

    public void advanceRow()
    {
        setLockTiles(true);
        if (parentCreator.currentRow != 9)
        {
            parentCreator.currentRow += 1;
            PianoTileRow temp = parentCreator.rows[parentCreator.currentRow].GetComponent(typeof(PianoTileRow)) as PianoTileRow;
            temp.setLockTiles(false);
            parentCreator.updateTargetVertical(6f - (7.5f*parentCreator.currentRow));
        }
        else
        {
            parentCreator.updateTargetVertical(parentCreator.transform.position.y - 300f);
            //00FF16
            parentCreator.winGame();
        }

    }

    public void failGame()
    {
        parentCreator.failGame();
    }
}
