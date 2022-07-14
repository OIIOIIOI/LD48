using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using ActionPhase = CycleManager.ActionPhase;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager BuildingManagerInstance;
    public enum BuildingType
    {
        Laboratory,
        HarpoonStation,
        ExpeditionCenter,
        House1,
        House2,
        Core
    }

    // type used for stacking prepared actions before falling phase
    public enum BuildingActionsType
    {
        GoXp,
        Research,
        Harpoon
    }
    
    // Building Script
    public Building laboratory;
    public Building harpoonStation;
    public Building expeditionCenter;
    public Building house1;
    public Building house2;
    public Building core;

    // List of built facilities in game
    [HideInInspector] 
    public List<Building> inGameBuildings;
    // List of built facilities selected
    [HideInInspector] 
    public List<Building> activatedBuildings = new List<Building>();
    
    private void Awake()
    {
        if (BuildingManagerInstance == null)
            BuildingManagerInstance = this;
        else if (BuildingManagerInstance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Initialise building list
        inGameBuildings = new List<Building>() {laboratory, house1, house2, core, harpoonStation, expeditionCenter};
    }

    //Todo trigger at the end of preparation
    public void SetBuildingSelection()
    {
        ClearSelectedBuildingList();
        foreach (var building in inGameBuildings.Where(building => building.isSelected))
        {
            activatedBuildings.Add(building);
        }
    }
    
    // Clear buildings selected list
    private void ClearSelectedBuildingList()
    {
        activatedBuildings = new List<Building>();
    }

    /* Fall preparation functions */
    // Todo trigger it at the beginning of the preparation phase
    // Reset population dispatch
    public void ResetBuildingState()
    {
        foreach (var building in inGameBuildings.Where(building => building.isSelected))
        {
            building.isSelected = false;
        }
        ClearSelectedBuildingList();
    }
    
    /* Fall functions */
    // Todo trigger it at the end of the fall phase/beginning of stasis phase
    // validate and trigger selected actions
    public void performSelectedActions()
    {
        foreach (var building in activatedBuildings.Where(building => building.isSelected))
        {
            FallAction(building.actionType);
        }
    }

    // todo implement actions
    private void FallAction(BuildingActionsType buildingAction)
    {
        switch (buildingAction)
        {
            case BuildingActionsType.Research:
                print("execute Analyze relic function");
                break;
            case BuildingActionsType.GoXp:
                print("execute Prepare expedition function");
                break;
            case BuildingActionsType.Harpoon:
                print("execute harpoon function");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    
    
    
    
    
    
    
    

    
    /* Stase actions functions */
    
    // repair
    public void Repair(BuildingType buildingType, int Hp)
    {
        inGameBuildings.Find(b => b.buildingType == buildingType).RepairBuilding(Hp);
    }
    public int SelectRepairAction(int maxHpAmount, BuildingType buildingType)
    {
        return inGameBuildings.Find(b => b.buildingType == buildingType).CalculateMaterialsLeft(maxHpAmount);
    }
    // launch expedition
    
    //
    /* Stase actions functions */
    
     
    // Reset Building selection at each state Todo to call at each phase
    public void SetUpPhase(ActionPhase phase)
    {
        switch (phase)
            // Todo hide actionSprite
        {
            case ActionPhase.PrepareFall:
                inGameBuildings.ForEach(b =>
                {
                    b.isSelected = false;
                });
                break;
            case ActionPhase.Fall:
                break;
            case ActionPhase.PrepareStasis:
                inGameBuildings.ForEach(b => b.isSelected = false);
                break;
            case ActionPhase.Stasis:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(phase), phase, null);
        }
    }
}
