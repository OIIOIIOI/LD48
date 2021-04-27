using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SplashScreen : MonoBehaviour
{

    public Transform sun;
    public Transform mid;
    public Transform front;
    public SpriteRenderer title;

    private Vector3 sunStartPosition;
    private Vector3 midStartPosition;
    private Vector3 frontStartPosition;
    
    public float transitionDuration;

    public GameObject cloudPrefab;
    
    private float elapsedTime;
    private bool ready;
    private bool transitionOver;

    private bool titleReady;
    private bool titleTransitionOver;

    private float dir = 1f;

    private void Awake()
    {
        ready = titleReady = false;
        transitionOver = titleTransitionOver = false;
        
        sunStartPosition = sun.position;
        midStartPosition = mid.position;
        frontStartPosition = front.position;

        // title.color = new Color(1f, 1f, 1f, 0.5f);
        
        Invoke(nameof(StartTransition), 1f);
    }

    private void StartTransition()
    {
        elapsedTime = 0f;
        ready = true;
    }

    private void Update()
    {
        if (!ready)
            return;
        
        if (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            t = t * (2 - t);
            elapsedTime += Time.deltaTime;
        
            sun.position = Vector3.Lerp(sunStartPosition, Vector3.zero, t);
            mid.position = Vector3.Lerp(midStartPosition, Vector3.zero, t);
            front.position = Vector3.Lerp(frontStartPosition, Vector3.zero, t);
        }
        else if (!transitionOver)
        {
            transitionOver = true;
            
            sun.position = Vector3.zero;
            mid.position = Vector3.zero;
            front.position = Vector3.zero;
        
            InvokeRepeating(nameof(StartClouds), 0.5f, 5f);
        }
    }

    private void StartClouds()
    {
        var go = Instantiate(cloudPrefab);
        dir = -dir;
        go.transform.position = new Vector3(Random.Range(4f, 6f) * dir, -4f, 0f);

        // TODO Store clouds and clean up on scene change
    }
    
}
