using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    [SerializeField] AudioSource hitGroundAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ground")
        {
            hitGroundAudio.Play();
            Destroy(gameObject, .3f);
        }
    }
}
