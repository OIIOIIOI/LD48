using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipsManager : MonoBehaviour
{
    public static TooltipsManager TooltipsManagerInstance;

    public TextMeshProUGUI textComponent;
    
    private void Awake()
    {
        if (TooltipsManagerInstance != null && TooltipsManagerInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            TooltipsManagerInstance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = Input.mousePosition;
    }

    public void SetAndShowTooltip(string message)
    {
        
        gameObject.SetActive(true);
        textComponent.text = message;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
    }
}
