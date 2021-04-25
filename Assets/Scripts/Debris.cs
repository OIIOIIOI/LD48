using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    public GameObject debris;

    void Start()
    {
        debris.transform.position = new Vector3(Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize), -Camera.main.orthographicSize*2);
    }

    void Update()
    {
        
    }
}
