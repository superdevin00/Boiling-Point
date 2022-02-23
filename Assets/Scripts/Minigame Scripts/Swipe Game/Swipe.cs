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
    bool canSwipe = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && canSwipe)
        {
            touchTimeStart = Time.time;
            startPos = Input.GetTouch(0).position;
            canSwipe = false;
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touchTimeFinish = Time.time;
            touchInterval = touchTimeFinish - touchTimeStart;
            endPos = Input.GetTouch(0).position;
            direction = startPos - endPos;
            rb.isKinematic = false;
            rb.AddForce(-direction.x * throwForceInXandY, direction.y * throwForceInXandY, throwForceInZ / touchInterval);
            canSwipe = true;
            Destroy(gameObject, 3f);
        }
    }
}
