using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Icon : MonoBehaviour
{

    public enum IconType { Relic, People, Material } 
    
    public Sprite[] sprites;
    public IconType type;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetType(IconType type)
    {
        switch (type)
        {
            case IconType.People:
                sr.sprite = sprites[0];
                break;
            default :
                sr.sprite = sprites[1];
                break;
        }
    }
}
