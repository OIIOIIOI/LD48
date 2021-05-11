using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StasisBtn : MonoBehaviour
{
    [HideInInspector]
    public bool isSelected;
    
    //When selected , it turns to this color (red)
    [HideInInspector]
    public Color selectedColor = Color.red; // TODO change when selected
    //This stores the GameObject’s original color
    [HideInInspector]
    private Color _originalColor;
    
    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
    private MeshRenderer _renderer;

    
    // Start is called before the first frame update
    void Start()
    {        
        _renderer = GetComponent<MeshRenderer>();
        _originalColor = _renderer.material.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        isSelected = !isSelected;
        _renderer.material.color = isSelected ? selectedColor : _originalColor;
    }
}
