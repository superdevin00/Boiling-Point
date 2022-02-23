using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNet : MonoBehaviour
{
    [SerializeField] Vector3 pointA;
    [SerializeField] Vector3 pointB;
    [SerializeField] float speed = 1f;
    public bool canMove;
    public bool firstMove;
    void Start()
    {

        firstMove = true;

    }
    void Update()
    {
        if (transform.position == pointA)
        {
            firstMove = false;
        }
        if (transform.position == pointB)
        {
            firstMove = true;
        }
        if (canMove)
        {
            if (firstMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, pointA, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, pointB, speed * Time.deltaTime);
            }
        }

    }
}
