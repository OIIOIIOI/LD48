using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class IconUI : MonoBehaviour
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
        Upgrade,
    } 
    
    public Sprite[] sprites;
    public IconType iconType;
    private Image _img;

    private void Awake()
    {
        _img = GetComponent<Image>();
        SetType(iconType);
    }

    public void SetType(IconType type)
    {
        switch (type)
        {
            case IconType.Population:
                _img.sprite = sprites[0];
                break;
            case IconType.Materials:
                _img.sprite = sprites[1];
                break;
            case IconType.Knowledge:
                _img.sprite = sprites[2];
                break;
            case IconType.Relic:
                _img.sprite = sprites[3];
                break;
            case IconType.Analyse:
                _img.sprite = sprites[4];
                break;
            case IconType.Build:
                _img.sprite = sprites[5];
                break;
            case IconType.Repair:
                _img.sprite = sprites[6];
                break;
            case IconType.Expedition:
                _img.sprite = sprites[7];
                break;
            case IconType.Collect:
                _img.sprite = sprites[8];
                break;
            case IconType.Upgrade:
                _img.sprite = sprites[9];
                break;
            default :
                _img.sprite = sprites[0];
                break;
        }
    }
}
