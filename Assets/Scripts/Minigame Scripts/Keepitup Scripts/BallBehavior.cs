using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float upwardForce = 375f;
    //[SerializeField] float sidewaysForce = 0f;
    [SerializeField] Transform anchor_L;
    [SerializeField] Transform anchor_R;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.collider.name == "Ball")
                {
                    Debug.Log("Ball clicked");

                    if (Random.Range(0, 2) == 0)
                        rb.AddForce(anchor_L.position * upwardForce);
                    else
                        rb.AddForce(anchor_R.position * upwardForce);
                }
            }
        }
    }
}
