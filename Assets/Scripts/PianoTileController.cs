using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PianoTileController : MonoBehaviour
{

    [SerializeField] Collider2D col;
    [SerializeField] SpriteRenderer sprRen;
    [SerializeField] Sprite whiteSprite;
    [SerializeField] Sprite blackSprite;
    [SerializeField] Sprite graySprite;
    [SerializeField] Sprite redSprite;
    public bool black;
    bool pressed;
    public bool locked;
    public bool failed;



    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
        locked = false;
        failed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (black)
        {
            if (pressed)
            {
                sprRen.sprite = graySprite;
            }
            else
            {
                sprRen.sprite = blackSprite;
            }
        }
        else
        {
            if (pressed)
            {
                sprRen.sprite = redSprite;
            }
            else
            {
                sprRen.sprite = whiteSprite;
            }
        }

        foreach(Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began)
            {
                Vector2 tapPos = Camera.main.ScreenToWorldPoint(touch.position);

                if (Physics2D.OverlapPoint(tapPos) == col && !locked && !failed)
                {
                    pressed = true;
                }
            }
        }
    }
}
