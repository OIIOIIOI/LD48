using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
public class Cloud : MonoBehaviour
{

    public Sprite[] variants;

    private enum CloudLayer { Back, Front };
    private SpriteRenderer sr;
    private CloudLayer layer;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = variants[Random.Range(0, variants.Length)];
        // Random flip
        if (Random.value < 0.5f)
            sr.flipX = true;
        // Random alpha
        sr.color = Random.ColorHSV(0f, 0f, 0f, 0f, 1f, 1f, 0.65f, 0.95f);
        // Random layer
        layer = (Random.value < 0.5f) ? CloudLayer.Back : CloudLayer.Front;
    }

    private void Update()
    {
        // TODO Movement depending on layer
    }
    
}
