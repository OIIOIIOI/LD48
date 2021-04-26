using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        House2
    }
    
    // Building gameObjectScript
    public Building laboratory;
    public Building harpoonStation;
    public Building expeditionCenter;
    public Building house1;
    public Building house2;
    
    // PlaceHolder gameObject
    public GameObject left;
    public GameObject right;
    public GameObject bottom;
    public GameObject bottomLeft;
    public GameObject bottomRight;

    // List of built facilities
    public List<Building> inGameBuildingScripts = new List<Building>();
    
    // Targeted building for damage (us Type)
    public Building targetedBuilding;
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
        inGameBuildingScripts.Add(harpoonStation);
    }
    
    /* Fall action functions */
    public void PerformFallActions()
    {
        foreach (var building in inGameBuildingScripts)
        {
            building.PerformFallAction();
            
        }
    }

    // damage building
    public void DealDamage(BuildingType type) {
        GetTargetedBuildingObj(type);
        var damage = GameManager.GameInstance.damageHitValue;
        targetedBuilding.ReceiveDamage(damage);
    }
    
    /* Stase actions functions */
    // Build
    
    // repair
    
    // launch expedition
    
    
    /* Building getters */
    // Get Building game object
    private void GetTargetedBuildingObj(BuildingType type) {
        switch (type)
        {
            case BuildingType.HarpoonStation:
                targetedBuilding = harpoonStation;
                break;
            case BuildingType.ExpeditionCenter:
                targetedBuilding = expeditionCenter;
                break;
            case BuildingType.Laboratory:
                targetedBuilding = laboratory;
                break;
            case BuildingType.House1:
                targetedBuilding = house1;
                break;
            case BuildingType.House2:
                targetedBuilding = house2;
                break;
            default:
                targetedBuilding = null;
                break;
        }
    }
}
