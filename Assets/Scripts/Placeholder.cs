using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingType = BuildingManager.BuildingType;

public class Placeholder : MonoBehaviour
{
    // Whether or not a building is in this placeholder
    public bool isHosting;

    // Type of building
    public BuildingType buildingType;

    //  Transform position of the placeholder
    public Vector3 placeholderPosition;

    private void Awake()
    {
        placeholderPosition = transform.position;
    }
}

