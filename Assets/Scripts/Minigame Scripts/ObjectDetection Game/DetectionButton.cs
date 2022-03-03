using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionButton : MonoBehaviour
{
    [SerializeField] Button self;
    [SerializeField] DetectionManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        self.Select();
        self.OnSelect(null);
    }
}
