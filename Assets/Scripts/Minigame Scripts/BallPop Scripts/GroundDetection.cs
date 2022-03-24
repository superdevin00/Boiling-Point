using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    [SerializeField] AudioSource hitGroundAudio;

    public BallPopController BallPopController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ground")
        {
            BallPopController.score -= 1;
            //hitGroundAudio.Play();
            Destroy(gameObject, .3f);
        }
    }
}
