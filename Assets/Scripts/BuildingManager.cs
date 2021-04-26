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

    public enum PlaceholderType
    {
        Left,
        Right,
        Bottom,
        BottomLeft,
        BottomRight
    }
    
    // Building gameObjectScript
    public Building laboratory;
    public Building harpoonStation;
    public Building expeditionCenter;
    public Building house1;
    public Building house2;
    
    // PlaceHolder gameObject
    public Placeholder leftPlace;
    public Placeholder rightPlace;
    public Placeholder bottomPlace;
    public Placeholder bottomLeftPlace;
    public Placeholder bottomRightPlace;

    // List of built facilities
    public List<Building> inGameBuildingScripts = new List<Building>();
    
    // Targeted building for damage (us Type)
    private Building targetedBuilding;
    private Placeholder targetedPlaceholder;
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

    // Check if a building is present in placeholder
    public bool EvaluateImpact(PlaceholderType placeholder)
    {
        GetTargetedPlaceholder(placeholder);
        if (!targetedPlaceholder.isHosting)
            return false;
        DealDamage(targetedPlaceholder.buildingType);
        return true;
    }
    
    // damage building
    private void DealDamage(BuildingType type) {
        GetTargetedBuildingObj(type);
        var damage = GameManager.GameInstance.damageHitValue;
        targetedBuilding.ReceiveDamage(damage);
    }
    
    /* Stase actions functions */
    // Build
    // placeholder.hosting = true
    // placeholder.buildingtype
    
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
    // Get Placeholder game object
    private void GetTargetedPlaceholder(PlaceholderType type) {
        switch (type)
        {
            case PlaceholderType.Left:
                targetedPlaceholder = leftPlace;
                break;
            case PlaceholderType.Right:
                targetedPlaceholder = rightPlace;
                break;
            case PlaceholderType.Bottom:
                targetedPlaceholder = bottomPlace;
                break;
            case PlaceholderType.BottomLeft:
                targetedPlaceholder = bottomLeftPlace;
                break;
            case PlaceholderType.BottomRight:
                targetedPlaceholder = bottomRightPlace;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

    }
}
