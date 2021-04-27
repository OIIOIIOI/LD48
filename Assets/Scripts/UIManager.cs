using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionPhase = CycleManager.ActionPhase;
using BuildingType = BuildingManager.BuildingType;


public class UIManager : MonoBehaviour
{
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
        stasisPreparationGroup.gameObject.SetActive(false);
        SetUpPrepareFall();
    }
    public void Update()
    {
        
    }
    public void InitPhase(ActionPhase phase)
    {
        switch (phase)
        {

            case ActionPhase.PrepareFall:
                
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

    public void SetUpPrepareFall()
    {
        foreach (var placeholder in BuildingManager.BuildingManagerInstance.placeholders)
        {
            if (placeholder.isHosting)
                if (placeholder.placeholderType == BuildingManager.PlaceholderType.Left)
                {
                    leftActionIcon.iconType = BindActionIcon(placeholder.buildingType);
                    leftActionIcon.SetType(leftActionIcon.iconType);
                }
                if (placeholder.placeholderType == BuildingManager.PlaceholderType.Bottom)
                {
                    bottomActionIcon.iconType = BindActionIcon(placeholder.buildingType);
                    bottomActionIcon.SetType(bottomActionIcon.iconType);
                }
                if (placeholder.placeholderType == BuildingManager.PlaceholderType.Right)
                {
                    rightActionIcon.iconType = BindActionIcon(placeholder.buildingType);
                    rightActionIcon.SetType(rightActionIcon.iconType);
                }
        }
    }

    private void SetUpPrepareStasis()
    {
        
    }
    
    private void SetUpFall()
    {
        
    }
    
    private void SetUpStasis()
    {
        
    }

    private IconInGame.IconType BindActionIcon(BuildingType type)
    {
        switch(type)
        {
            case BuildingType.Laboratory:
                return IconInGame.IconType.Analyse;
            case BuildingType.HarpoonStation:
                return IconInGame.IconType.Collect;
            case BuildingType.ExpeditionCenter:
                return IconInGame.IconType.Expedition;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

}
