using System;
using System.Collections.Generic;
using UnityEngine;
using ActionPhase = CycleManager.ActionPhase;
using BuildingType = BuildingManager.BuildingType;
using PlaceholderType = BuildingManager.PlaceholderType;


public class UIManager : MonoBehaviour
{
    [HideInInspector]
    public static UIManager uiManagerInstance;
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
    public StasisGrpBtn grpBtnBottomLeft;
    public StasisGrpBtn grpBtnBottomRight;
    
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
        // Add event listener on Btn
        foreach (var placeholder in BuildingManager.BuildingManagerInstance.placeholders)
        {
            switch (placeholder.placeholderType)
            {
                case PlaceholderType.Left:
                    AddEventListenerStasisBtn(grpBtnLeft, placeholder);
                    break;
                case PlaceholderType.Bottom:
                    AddEventListenerStasisBtn(grpBtnBottom, placeholder);
                    break;
                case PlaceholderType.Right:
                    AddEventListenerStasisBtn(grpBtnRight, placeholder);
                    break;
                case PlaceholderType.BottomLeft:
                    AddEventListenerStasisBtn(grpBtnBottomLeft, placeholder);
                    break;
                case PlaceholderType.BottomRight:
                    AddEventListenerStasisBtn(grpBtnBottomRight, placeholder);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        // stasisPreparationGroup.gameObject.SetActive(false); // Todo to uncomment
        // SetUpFallPrep(); // Todo to delete when CycleManager is working
    }
    public void Update()
    {
        // Update only during stasis preparation phase
        /*while (CycleManager.instance.phase == ActionPhase.PrepareStasis)
        {
            UpdateStasisPrep();
        }*/
        // UpdateStasisPrep(); // Todo to delete and uncomment top when CycleManager is working
    }

    public void SetUpFallPrep()
    {
        // Activate PrepFallGroup UI
        fallPreparationGroup.gameObject.SetActive(true);
        // Adjust actions number
        UpdateActionsAvailable();
        // Display action icons
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

    public void SetUpFall()
    {
        // Deactivate PrepFallGroup UI
        fallPreparationGroup.gameObject.SetActive(false);
        
        // If building selected
        // Pop and Animate Work In Progress Icon
        
    }

    public void SetUpStasisPrep()
    {
        // Update actions available
        UpdateActionsAvailable();
        UpdateStasisPrep();
    }
    private void UpdateStasisPrep()
    {
        var inGameBuilding = BuildingManager.BuildingManagerInstance.inGameBuildings;
        foreach (var placeholder in BuildingManager.BuildingManagerInstance.placeholders)
        {
            switch (placeholder.placeholderType)
            {
                case PlaceholderType.Left:
                    UpdateStasisButtons(placeholder, grpBtnLeft, inGameBuilding);
                    break;
                case PlaceholderType.Bottom:
                    UpdateStasisButtons(placeholder, grpBtnBottom, inGameBuilding);
                    break;
                case PlaceholderType.Right:
                    UpdateStasisButtons(placeholder, grpBtnRight, inGameBuilding);
                    break;
                case PlaceholderType.BottomLeft:
                    UpdateStasisButtons(placeholder, grpBtnBottomLeft, inGameBuilding);
                    break;
                case PlaceholderType.BottomRight:
                    UpdateStasisButtons(placeholder, grpBtnBottomRight, inGameBuilding);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        // Manage population variation (build houses, Action point are managed in the beginning of fall state)
        ManageHouse();
    }
    public void SetUpStasis()
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
            // Show build action only on right placeholder
            if (placeholder.placeholderType != PlaceholderType.BottomLeft && placeholder.placeholderType != PlaceholderType.BottomRight)
            {
                // Set Expedition center building button if not exist
                grpBtn.buildXpCenter.gameObject.SetActive(!inGameBuilding.Exists(b => b.buildingType == BuildingType.ExpeditionCenter));
                // Set lab building center button if not exist
                grpBtn.buildLab.gameObject.SetActive(!inGameBuilding.Exists(b => b.buildingType == BuildingType.Laboratory));
            } else
            {
                grpBtn.buildXpCenter.gameObject.SetActive(false);
                grpBtn.buildLab.gameObject.SetActive(false);
            }
        }
    }
    private static void AddEventListenerStasisBtn(StasisGrpBtn grpBtn, Placeholder placeholder)
    {
        // Repair button only for houses
        if (placeholder.placeholderType == PlaceholderType.BottomLeft || placeholder.placeholderType == PlaceholderType.BottomLeft)
        {
            grpBtn.repair.onClick.AddListener(() =>GameManager.GameInstance.StackStasisActions(placeholder, BuildingManager.StasisActionsType.Repair));
        }
        else
        {
            grpBtn.repair.onClick.AddListener(() => GameManager.GameInstance.StackStasisActions(placeholder, BuildingManager.StasisActionsType.Repair));
            grpBtn.buildLab.onClick.AddListener(() =>GameManager.GameInstance.StackStasisActions(placeholder, BuildingManager.StasisActionsType.BuildLab));
            grpBtn.buildXpCenter.onClick.AddListener(() =>GameManager.GameInstance.StackStasisActions(placeholder, BuildingManager.StasisActionsType.BuildXp));
            grpBtn.goXp.onClick.AddListener(() =>GameManager.GameInstance.StackStasisActions(placeholder, BuildingManager.StasisActionsType.GoXp)); 

        }
    }
    
    private void ManageHouse()
    {
        var placeholders = BuildingManager.BuildingManagerInstance.placeholders;
        // If population / ratio && if not built
        if (GameManager.GameInstance.population / GameManager.GameInstance.actionPopulationRatio >= 2 && !placeholders.Find(p => p.placeholderType == PlaceholderType.BottomRight).isHosting)
            BuildingManager.BuildingManagerInstance.Build(BuildingType.House1, PlaceholderType.BottomRight);
        if (GameManager.GameInstance.population / GameManager.GameInstance.actionPopulationRatio >= 3 && !placeholders.Find(p => p.placeholderType == PlaceholderType.BottomLeft).isHosting)
            BuildingManager.BuildingManagerInstance.Build(BuildingType.House2, PlaceholderType.BottomLeft);
        // if population lost
        if (GameManager.GameInstance.population / GameManager.GameInstance.actionPopulationRatio < 2 && placeholders.Find(p => p.placeholderType == PlaceholderType.BottomRight).isHosting)
            BuildingManager.BuildingManagerInstance.DestroyBuilding(BuildingType.House1);
        if (GameManager.GameInstance.population / GameManager.GameInstance.actionPopulationRatio < 3 && placeholders.Find(p => p.placeholderType == PlaceholderType.BottomLeft).isHosting)
            BuildingManager.BuildingManagerInstance.DestroyBuilding(BuildingType.House2);

    }

    private void UpdateActionsAvailable()
    {
        GameManager.GameInstance.actionAvailable = GameManager.GameInstance.population / GameManager.GameInstance.actionPopulationRatio;
    }
}
