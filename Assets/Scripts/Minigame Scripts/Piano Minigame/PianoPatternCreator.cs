using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PianoPatternCreator : MonoBehaviour
{

    bool[,] pattern = new bool[10, 4];

    void Start()
    {
        fillPattern();
    }

    void Update()
    {
        
    }

    void fillPattern()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 4; i++)
            {
                pattern[i, j] = false;
            }

        }

        int previous = -1;
        for(int i = 0; i < 10; i++)
        {
            int temp = Random.Range(0, 5);
            if(previous == temp)
            {
                temp = (temp + 1) % 4;
            }
            pattern[i, temp] = true;
        }
    }
}
