using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpinner : MonoBehaviour
{

    int spinDir;
    // Start is called before the first frame update
    void Start()
    {
        spinDir = Random.Range(-1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += (Vector3.forward * (Time.deltaTime * 180f * spinDir));
    }
}
