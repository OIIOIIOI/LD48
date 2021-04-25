using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EventInterface : MonoBehaviour
{
    public GameObject spritePlaceholder;
    public TextMeshProUGUI descriptionUI;
    public TextMeshProUGUI gainUI;
    public TextMeshProUGUI lostUI;
    
    private void Awake()
    {
        gameObject.SetActive(true);
        // todo link with event generator
        spritePlaceholder.GetComponent<SpriteRenderer>();
        descriptionUI.SetText("This is an event description");
        gainUI.SetText("Gain + 1");
        lostUI.SetText("Loose - 1");
    }
    
    public void ValidateEvent()
    {
        // Todo update game variables with loot results
        gameObject.SetActive(false);
    }
}
