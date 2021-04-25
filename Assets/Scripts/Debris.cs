using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    public GameObject debris;
    public QteSystem qtePrefab;

    private float _initialYDebrisPosition;
    private float _timer = 0f;

    void Start()
    {
        _initialYDebrisPosition = debris.transform.position.y;
        debris.transform.position = new Vector3(Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize), -Camera.main.orthographicSize*1.5f);
        qtePrefab = Instantiate(qtePrefab);
        qtePrefab.keys.Add(KeyCode.A);
        qtePrefab.Initialize(Camera.main, OnSuccess);
    }

    void OnSuccess(bool success)
    {
        if (success)
        {
            //Do some actions
            Debug.Log("Good job");
        }
        else
        {
            //Do some actions
            Debug.Log("Fail");
        }
        Destroy(debris);
        Destroy(this);
    }

    void Update()
    {
        _timer += Time.deltaTime;
        float newYPosition = Camera.main.orthographicSize*1.5f + _initialYDebrisPosition * _timer / (qtePrefab.neutralTime + qtePrefab.timeToSuccess);
        debris.transform.position = new Vector3(debris.transform.position.x, -newYPosition);
    }
}
