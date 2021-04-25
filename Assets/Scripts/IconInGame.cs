using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class IconInGame : MonoBehaviour
{

    public enum IconType {        
        Population,
        Materials,
        Knowledge,
        Relic,
        ActionsPoint,
        Analyse,
        Build,
        Repair,
        Expedition,
        Collect,
        Upgrade
    } 
    
    public Sprite[] sprites;
    public IconType iconType;
    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        SetType(iconType);
    }

    public void SetType(IconType type)
    {
        switch (type)
        {
            case IconType.Population:
                _sr.sprite = sprites[0];
                break;
            case IconType.Materials:
                _sr.sprite = sprites[1];
                break;
            case IconType.Knowledge:
                _sr.sprite = sprites[2];
                break;
            case IconType.Relic:
                _sr.sprite = sprites[3];
                break;
            case IconType.Analyse:
                _sr.sprite = sprites[4];
                break;
            case IconType.Build:
                _sr.sprite = sprites[5];
                break;
            case IconType.Repair:
                _sr.sprite = sprites[6];
                break;
            case IconType.Expedition:
                _sr.sprite = sprites[7];
                break;
            case IconType.Collect:
                _sr.sprite = sprites[8];
                break;
            case IconType.Upgrade:
                _sr.sprite = sprites[9];
                break;
            case IconType.ActionsPoint:
                _sr.sprite = sprites[10];
                break;
            default :
                _sr.sprite = sprites[0];
                break;
        }
    }
}
