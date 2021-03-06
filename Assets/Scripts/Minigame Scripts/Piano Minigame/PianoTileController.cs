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
    public PianoTileRow parentRow;
    public bool black;
    public bool pressed;
    public bool locked;
    public bool failed;
    public AudioClip tapSFX;
    public MinigameManager minigameManager;
    



    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
        //locked = true;
        failed = false;

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position.Set(transform.position.x, transform.position.y - 1, transform.position.z);
        //Vector3 newPos = new Vector3(transform.position.x, transform.position.y - (Time.deltaTime), transform.position.z);
        //transform.position = newPos;
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

        if (minigameManager.getMinigameEnded() == false && minigameManager.getMinigameStarted() == true) {
            bool touched = false;
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Vector2 tapPos = Camera.main.ScreenToWorldPoint(touch.position);
                    touched = true;
                    if (Physics2D.OverlapPoint(tapPos) == col && !locked && !failed)
                    {
                        pressed = true;
                        if (black)
                        {
                            parentRow.advanceRow();
                        }
                        else
                        {
                            parentRow.failGame();
                        }
                    }
                }
            }

            if (Input.GetMouseButtonDown(0) && !touched)
            {
                //Debug.Log("Hype");
                Vector2 tapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (Physics2D.OverlapPoint(tapPos) == col && !locked && !failed)
                {

                    pressed = true;
                    if (black)
                    {
                        parentRow.advanceRow();
                        AudioSource.PlayClipAtPoint(tapSFX, new Vector3(7.3f,15,-10));
                    }
                    else
                    {
                        parentRow.failGame();
                    }
                }
            }
        }
    }
}
