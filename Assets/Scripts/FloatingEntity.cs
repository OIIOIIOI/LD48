using System;
using UnityEngine;

public class FloatingEntity : MonoBehaviour
{
    
    public float offset;
    private float duration = 1f;
    private float elapsedTime;

    private void FixedUpdate()
    {
        elapsedTime = 0f;
    }
}
