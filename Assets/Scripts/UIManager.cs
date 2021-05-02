using System;
using System.Collections.Generic;
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
    // public IconInGame leftWIP;
    // public IconInGame bottomtWIP;
    // public IconInGame rightWIP;
    
    // Stase state preparation phase
    public StasisGrpBtn grpBtnLeft;
    public StasisGrpBtn grpBtnRight;
    public StasisGrpBtn grpBtnBottom;
    
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
        // Add event listener on Btn
        foreach (var placeholder in BuildingManager.BuildingManagerInstance.placeholders)
        {
            if (placeholder.placeholderType == PlaceholderType.Left)
            {
                AddEventListenerStasisBtn(grpBtnLeft, placeholder);
            }
            if (placeholder.placeholderType == PlaceholderType.Bottom)
            {
                AddEventListenerStasisBtn(grpBtnBottom, placeholder);
            }
            if (placeholder.placeholderType == PlaceholderType.Right)
            {
                AddEventListenerStasisBtn(grpBtnRight, placeholder);
            }
        }
        UpdatePrepareStasis();

    }
    public void Update()
    {
        // Todo Only during stasis state
        UpdatePrepareStasis();
    }
    // TODO link phase with cycleManager
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
        // If building selected 
        // Pop and Animate Work In Progress Icon
    }
    private void UpdatePrepareStasis()
    {
        var inGameBuilding = BuildingManager.BuildingManagerInstance.inGameBuildings;
        foreach (var placeholder in BuildingManager.BuildingManagerInstance.placeholders)
        {
            if (placeholder.placeholderType == PlaceholderType.Left)
            {
                UpdateStasisButtons(placeholder, grpBtnLeft, inGameBuilding);
            }
            if (placeholder.placeholderType == PlaceholderType.Bottom)
            {
                UpdateStasisButtons(placeholder, grpBtnBottom, inGameBuilding);
            }
            if (placeholder.placeholderType == PlaceholderType.Right)
            {
                UpdateStasisButtons(placeholder, grpBtnRight, inGameBuilding);
            }
        }
        // Manage population variation (Action point & houses
        ManagePopulation();
    }
    private void SetUpStasis()
    {
        
    }
    
    private static void UpdateStasisButtons(Placeholder placeholder, StasisGrpBtn grpBtn, List<Building> inGameBuilding)
    {
        if (placeholder.isHosting)
        {
            // Deactivate buttons
            grpBtn.buildXpCenter.gameObject.SetActive(false);
            grpBtn.buildLab.gameObject.SetActive(false);
            // Activate buttons
            grpBtn.repair.gameObject.SetActive(true);
            if(placeholder.buildingType == BuildingType.ExpeditionCenter)
                grpBtn.goXp.gameObject.SetActive(true);
            // Compare life to disable repair & build
            var building = inGameBuilding.Find(b => b.buildingType == placeholder.buildingType);
            grpBtn.repair.enabled = building.currentHealthPoints < building.maxHealthPoints;

        }
        else
        {
            // Deactivate buttons
            grpBtn.repair.gameObject.SetActive(false);
            grpBtn.goXp.gameObject.SetActive(false);
            // Set Expedition center building button if not exist
            grpBtn.buildXpCenter.gameObject.SetActive(!inGameBuilding.Exists(b => b.buildingType == BuildingType.ExpeditionCenter));
            // Set lab building center button if not exist
            grpBtn.buildLab.gameObject.SetActive(!inGameBuilding.Exists(b => b.buildingType == BuildingType.Laboratory));
        }
    }
    
    private static void AddEventListenerStasisBtn(StasisGrpBtn grpBtn, Placeholder placeholder)
    {
        grpBtn.repair.onClick.AddListener(() =>BuildingManager.BuildingManagerInstance.Repair(placeholder.buildingType));
        grpBtn.buildLab.onClick.AddListener(() =>BuildingManager.BuildingManagerInstance.Build(BuildingType.Laboratory, placeholder.placeholderType));
        grpBtn.buildXpCenter.onClick.AddListener(() =>BuildingManager.BuildingManagerInstance.Build(BuildingType.ExpeditionCenter, placeholder.placeholderType));
        // grpBtn.goXp.onClick.AddListener(() =>BuildingManager.BuildingManagerInstance.Build(BuildingType.ExpeditionCenter, placeholder.placeholderType)); // call function in event manager
    }
    
    private void ManagePopulation()
    {
        // If population / ratio == && if not built
        
    }
}
