using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ActionPhase = CycleManager.ActionPhase;
using BuildingType = BuildingManager.BuildingType;
using PlaceholderType = BuildingManager.PlaceholderType;


public class UIManager : MonoBehaviour
{
    [HideInInspector]
    public UIManager uiManagerInstance;
    // Fall state preparation phase
    public GameObject fallPreparationGroup;
    public IconInGame leftActionIcon;
    public IconInGame bottomActionIcon;
    public IconInGame rightActionIcon;
    
    // Fall state exec phase
    public GameObject stasisPreparationGroup;
    public IconInGame leftWIP;
    public IconInGame bottomtWIP;
    public IconInGame rightWIP;
    
    // Stase state preparation phase
    // Icon place (3 / placeholder)
    // Stase state exec phase
    
    private void Awake()
    {
        if (uiManagerInstance == null)
            uiManagerInstance = this;
        else if (uiManagerInstance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // stasisPreparationGroup.gameObject.SetActive(false); todo to uncomment
        // SetUpPrepareFall(); // TO do to delete after cycle implementation
        SetUpPrepareStasis();

    }
    public void Update()
    {
        
    }
    // TODO link pahse with cycleManager
    public void InitPhase(ActionPhase phase)
    {
        switch (phase)
        {
            case ActionPhase.PrepareFall:
                SetUpPrepareFall();
                break;
            case ActionPhase.Fall:
                break;
            case ActionPhase.PrepareStasis:
                break;
            case ActionPhase.Stasis:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(phase), phase, null);
        }
    }

    private void SetUpPrepareFall()
    {
        foreach (var placeholder in BuildingManager.BuildingManagerInstance.placeholders)
        {
            if (placeholder.isHosting)
            {
                // Get action icon in building
                var icon = BuildingManager.BuildingManagerInstance.inGameBuildings.Find(b => b.buildingType == placeholder.buildingType).actionIcon;
                if (placeholder.placeholderType == PlaceholderType.Left)
                {
                    leftActionIcon.gameObject.SetActive(true);
                    leftActionIcon.SetType(icon);
                }
                if (placeholder.placeholderType == PlaceholderType.Bottom)
                {
                    bottomActionIcon.gameObject.SetActive(true);
                    bottomActionIcon.SetType(icon);
                }
                if (placeholder.placeholderType == PlaceholderType.Right)
                {
                    rightActionIcon.gameObject.SetActive(true);
                    rightActionIcon.SetType(icon);
                }
            }
        }
    }

    private void SetUpFall()
    {
        // If placeholder full
        
        // If building selected 
        // Pop and Animate Work In Progress Icon
    }
    private void SetUpPrepareStasis()
    {
        foreach (var placeholder in BuildingManager.BuildingManagerInstance.placeholders)
        {
            /*if (placeholder.placeholderType == PlaceholderType.Left)
            {
                leftActionIcon.gameObject.SetActive(true);
                leftActionIcon.SetType(icon);
            }
            if (placeholder.placeholderType == PlaceholderType.Bottom)
            {
                bottomActionIcon.gameObject.SetActive(true);
                bottomActionIcon.SetType(icon);
            }
            if (placeholder.placeholderType == PlaceholderType.Right)
            {
                rightActionIcon.gameObject.SetActive(true);
                rightActionIcon.SetType(icon);
            }
            if (placeholder.isHosting)
            {
                BuildingManager.BuildingManagerInstance.inGameBuildings.Find(b => b.buildingType == BuildingType.Laboratory)
                    
            }
            else
            {
                
            }*/
        }
    }

    private void SetUpStasis()
    {
        
    }
    
}
