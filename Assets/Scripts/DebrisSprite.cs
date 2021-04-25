using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
public class DebrisSprite : MonoBehaviour
{

    public Sprite[] variants;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = variants[Random.Range(0, variants.Length)];
        if (Random.value < 0.5f)
            sr.flipX = true;
    }

    private void Update()
    {
        // TODO Slight rotation while moving
    }
    
}
