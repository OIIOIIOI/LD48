using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingType = BuildingManager.BuildingType;
using PlaceholderType = BuildingManager.PlaceholderType;

public class Placeholder : MonoBehaviour
{
    // Whether or not a building is in this placeholder
    public bool isHosting;
    [HideInInspector]
    public bool isSelected = false;

    // Type of PlaceHolder
    public PlaceholderType placeholderType;
    
    // Type of building
    [HideInInspector]
    public BuildingType buildingType;

    //  Transform position of the placeholder
    public Vector3 placeholderPosition;

    private void Awake()
    {
        placeholderPosition = gameObject.transform.position;
    }
}

