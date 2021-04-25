using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Building : MonoBehaviour
{
    //When selected over the GameObject, it turns to this color (red)
    private Color _selectedColor = Color.red; // TODO
    //This stores the GameObject’s original color
    Color _originalColor;
    
    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
    private MeshRenderer _renderer;
    
    private bool _selected = false;
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

    public void OnMouseDown()
    {
        // TODO Swtich case according to cycle step phase for enable / disable selection & calculation
        if (GameManager.GameInstance.actionLeft == 0 && !_selected)
        {
            print("FX ERROR"); // TODO
        }
        else
        {
            _selected = !_selected;
            print(_selected ? "FX SELECT" : "FX UNSELECT"); // TODO
            _renderer.material.color = _selected ? _selectedColor : _originalColor;
            var operation = _selected ? -1 : 1;
            GameManager.GameInstance.UpdateActionsLeft(operation);
        }
    }
}
