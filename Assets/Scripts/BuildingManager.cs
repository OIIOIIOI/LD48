using System;
using System.Collections;
using System.Collections.Generic;
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

    // type used for stacking prepared actions
    public enum StasisActionsType
    {
        GoXp,
        Repair,
        BuildLab,
        BuildXp
    }
    
    // Building Script
    public Building laboratory;
    public Building harpoonStation;
    public Building expeditionCenter;
    public Building house1;
    public Building house2;
    
    // Building gameObject (used for Instanciate)
    public GameObject laboratoryGO;
    public GameObject harpoonStationGO;
    public GameObject expeditionCenterGO;
    public GameObject house1GO;
    public GameObject house2GO;
    
    // PlaceHolder script
    public Placeholder leftPlace;
    public Placeholder rightPlace;
    public Placeholder bottomPlace;
    public Placeholder bottomLeftPlace;
    public Placeholder bottomRightPlace;

    // PlaceHolder gameObject (use for Instanciate)
    public GameObject leftPlaceGO;
    public GameObject rightPlaceGO;
    public GameObject bottomPlaceGO;
    public GameObject bottomLeftPlaceGO;
    public GameObject bottomRightPlaceGO;

    // List of built facilities
    [HideInInspector]
    public List<Building> inGameBuildings = new List<Building>();
    [HideInInspector]
    public List<Placeholder> placeholders = new List<Placeholder>();
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
        // Feed placeholder list and placeholder scripts
        placeholders.Add(leftPlace);
        placeholders.Add(rightPlace);
        placeholders.Add(bottomPlace);
        placeholders.Add(bottomLeftPlace);
        placeholders.Add(bottomRightPlace);
        Build(BuildingType.HarpoonStation, PlaceholderType.Bottom);
    }
    
    /* Fall action functions */
    // TODO To call at the end of preparation step (fall cycle)
    public void PerformFallActions()
    {
        foreach (var building in inGameBuildings)
        {
            building.PerformFallAction();
        }
    }

    // Check if a building is present in placeholder todo link with hit impact
    public bool EvaluateImpact(PlaceholderType placeholder)
    {
        var targetedPlaceholder = GetTargetedPlaceholder(placeholder);
        if (!targetedPlaceholder.isHosting)
            return false;
        DealDamage(targetedPlaceholder.buildingType);
        return true;
    }
    
    // damage building
    private void DealDamage(BuildingType type) {
        var targetedBuilding = GetTargetedBuilding(type);
        var damage = GameManager.GameInstance.damageHitValue;
        var isDestroyed = targetedBuilding.ReceiveDamage(damage);
        // If destroyed remove it from inGameBuilding list and from placeholder
        if (isDestroyed)
        {
            DestroyBuilding(type);
        }
    }
    
    // Destroy Building
    public void DestroyBuilding(BuildingType buildingType)
    {
        // Adjust population
        if (buildingType == BuildingType.House1 || buildingType == BuildingType.House2)
        {
            GameManager.GameInstance.population -= GameManager.GameInstance.actionPopulationRatio;
        }
        // Remove from building script list
        var destroyedBuilding = GetTargetedBuilding(buildingType);
        inGameBuildings.Remove(destroyedBuilding);
        // Set placeholder as empty
        placeholders.Find(p => p.buildingType == buildingType).isHosting = false;
        // Destroy GameObject
        Destroy(destroyedBuilding.gameObject);
    }
    
    /* Stase actions functions */
    // Build
    public void Build(BuildingType buildingType, PlaceholderType placeholderType)
    {
        Vector2 v = new Vector2(0, 0);
        var buildingScript = GetTargetedBuilding(buildingType);
        var buildingGO = GetTargetedBuildingGO(buildingType);
        var placeholderScript = GetTargetedPlaceholder(placeholderType);
        var placeholderGO = GetTargetedPlaceholderGO(placeholderType);
        Instantiate(buildingGO, placeholderGO.transform);
        // Add building script to inGame list
        inGameBuildings.Add(buildingScript);
        // Set Placeholder
        placeholderScript.isHosting = true;
        placeholderScript.buildingType = buildingType;
    }

    // repair
    public void Repair(BuildingType buildingType, int Hp)
    {
        // var maxAmount = (int)Mathf.Floor(GameManager.GameInstance.materials / GameManager.GameInstance.repairRatio);
        // var rest = inGameBuildings.Find(b => b.buildingType == buildingType).RepairBuilding(maxAmount);
        inGameBuildings.Find(b => b.buildingType == buildingType).RepairBuilding(Hp);
        // GameManager.GameInstance.materials -= (maxAmount - rest) * GameManager.GameInstance.repairRatio;
    }
    public int SelectRepairAction(int maxHpAmount, BuildingType buildingType)
    {
        return inGameBuildings.Find(b => b.buildingType == buildingType).CalculateMaterialsLeft(maxHpAmount);
    }
    // launch expedition
     
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
    /* Buildings & placeholders getters */
    // Get Building Script
    private Building GetTargetedBuilding(BuildingType type) {
        switch (type)
        {
            case BuildingType.HarpoonStation:
                return harpoonStation;
            case BuildingType.ExpeditionCenter:
                return expeditionCenter;
            case BuildingType.Laboratory:
                return laboratory;
            case BuildingType.House1:
                return house1;
            case BuildingType.House2:
                return house2;
            default:
                return null;
        }
    }
    // Get Placeholder Script
    private Placeholder GetTargetedPlaceholder(PlaceholderType type) {
        switch (type)
        {
            case PlaceholderType.Left:
                return leftPlace;
            case PlaceholderType.Right:
                return rightPlace;
            case PlaceholderType.Bottom:
                return bottomPlace;
            case PlaceholderType.BottomLeft:
                return bottomLeftPlace;
            case PlaceholderType.BottomRight:
                return bottomRightPlace;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

    }
    // Get Building GameObject
    private GameObject GetTargetedBuildingGO(BuildingType type) {
        switch (type)
        {
            case BuildingType.HarpoonStation:
                return harpoonStationGO;
            case BuildingType.ExpeditionCenter:
                return expeditionCenterGO;
            case BuildingType.Laboratory:
                return laboratoryGO;
            case BuildingType.House1:
                return house1GO;
            case BuildingType.House2:
                return house2GO;
            default:
                return null;
        }
    }
    // Get Placeholder GameObject
    private GameObject GetTargetedPlaceholderGO(PlaceholderType type) {
        switch (type)
        {
            case PlaceholderType.Left:
                return leftPlaceGO;
            case PlaceholderType.Right:
                return rightPlaceGO;
            case PlaceholderType.Bottom:
                return bottomPlaceGO;
            case PlaceholderType.BottomLeft:
                return bottomLeftPlaceGO;
            case PlaceholderType.BottomRight:
                return bottomRightPlaceGO;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

    }
}
