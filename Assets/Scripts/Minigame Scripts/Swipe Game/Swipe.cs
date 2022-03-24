using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    Vector2 startPos, endPos, direction;
    float touchTimeStart, touchTimeFinish, touchInterval;

    [SerializeField] float throwForceInXandY = 1f;
    [SerializeField] float throwForceInZ = 50f;

    Rigidbody rb;
    public bool canSwipe = true;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canSwipe = true;
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetMouseButtonDown(0) && canSwipe == true) //|| Input.GetTouch(0).phase == TouchPhase.Began && canSwipe == true)
            {
                touchTimeStart = Time.time;
                //startPos = Input.GetTouch(0).position;
                startPos = Input.mousePosition;

            }

            else if (Input.GetMouseButtonUp(0) && canSwipe == true) //|| Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touchTimeFinish = Time.time;
                touchInterval = touchTimeFinish - touchTimeStart;
                //endPos = Input.GetTouch(0).position;
                endPos = Input.mousePosition;
                direction = startPos - endPos;
                rb.isKinematic = false;
                rb.AddForce(-direction.x * throwForceInXandY, direction.y * throwForceInXandY, throwForceInZ / touchInterval);
                canSwipe = false;
                Debug.Log("canSwipe = false");
            }
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && canSwipe == true)
            {
                touchTimeStart = Time.time;
                startPos = Input.GetTouch(0).position;
            }

            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touchTimeFinish = Time.time;
                touchInterval = touchTimeFinish - touchTimeStart;
                endPos = Input.GetTouch(0).position;
                direction = startPos - endPos;
                rb.isKinematic = false;
                rb.AddForce(-direction.x * throwForceInXandY, direction.y * throwForceInXandY, throwForceInZ / touchInterval);
                canSwipe = false;
                Debug.Log("canSwipe = false");
            }
        }
    }

    public void resetSpeed()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
    }
}
