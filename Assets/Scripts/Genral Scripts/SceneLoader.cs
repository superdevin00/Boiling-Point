using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    Camera mainCamera;
    SimpleBlit transitionMaterialScript;
    Material curtain;
    float time;
    float cutoffValue;

    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        transitionMaterialScript = mainCamera.GetComponent<SimpleBlit>();
        curtain = transitionMaterialScript.GetMaterial();

        StartSceneTransitionIn();
    }

    //private void OnEnable()
    //{
    //    StartSceneTransitionIn();
    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    if (curtain.GetFloat("_Cutoff") > 0.0f)
    //    {
    //        time = 0.0f;
    //        StartCoroutine(TransitionIn());
    //    }
    //}

    public void SetScene(string targetScene)
    {
        SceneManager.LoadScene(targetScene);
        curtain.SetFloat("_Cutoff", 1.01f);
        StartCoroutine(TransitionIn());
    }

    

    public void StartSceneTransitionOut(string targetScene)
    {
        curtain.SetFloat("_Cutoff", 0.0f);
        time = 0.0f;
        cutoffValue = 0.0f;

        StartCoroutine(TransitionOut(targetScene));

    }

    public void StartSceneTransitionIn()
    {
        curtain.SetFloat("_Cutoff", 1.01f);
        time = 0.0f;
        cutoffValue = 1.01f;

        StartCoroutine(TransitionIn());

    }

    IEnumerator TransitionOut(string targetScene)
    {
        //Curtain animation

        while (cutoffValue < 1.01f)
        {
            Debug.Log("Coroutine");
            cutoffValue = Mathf.Lerp(0.0f, 1.01f, time);

            curtain.SetFloat("_Cutoff", cutoffValue);
            time += 0.7f * Time.deltaTime;
            yield return null;
        }

        SetScene(targetScene);
    }

    IEnumerator TransitionIn()
    {
        //Curtain animation

        while (cutoffValue > 0.0f)
        {
            Debug.Log("Coroutine");
            cutoffValue = Mathf.Lerp(1.01f, 0.0f, time);

            curtain.SetFloat("_Cutoff", cutoffValue);
            time += 0.7f * Time.deltaTime;
            yield return null;
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartSceneTransitionOut("MainMenu");
        }
    }
}
