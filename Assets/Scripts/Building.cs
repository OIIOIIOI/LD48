using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using BuildingType = BuildingManager.BuildingType;
using ActionType = BuildingManager.BuildingActionsType;

public class Building : MonoBehaviour
{
    public int maxHealthPoints;
    public int currentHealthPoints;
    public BuildingType buildingType;
    [HideInInspector]
    public ActionType actionType;
    // Whether or not building is selected for fall phase
    public bool isSelected = false;
    // Whether or not building is disabled (destroyed)
    public bool isDestroyed = false;
    // Icon displayed when action is performed (fall step)
    public IconInGame.IconType actionIcon;
    public Tooltip tooltip;


    //When selected over the GameObject, it turns to this color (red)
    private Color _selectedColor = Color.red; // TODO
    //This stores the GameObject’s original color
    Color _originalColor;
    
    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
    private MeshRenderer _renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        SetAction();
        _renderer = GetComponent<MeshRenderer>();
        _originalColor = _renderer.material.color;
    }

    private void Update()
    {
        // Animate icon during fall && gameManager.phase == fall AND delete outline
        if (isSelected)
        {
            //
        }
        // Renderer destroyed
        if (isDestroyed)
        {
            //
        }
        // Update HP in building tooltip
        SetTooltipMessage();
    }

    // Fall preparation phase: selection of actions
    public void OnMouseDown()
    {
        // TODO Switch case according to cycle step phase for enable / disable selection & calculation
        if ((GameManager.GameInstance.actionLeft == 0 && !isSelected)|| isDestroyed)
        {
            print("FX ERROR"); // TODO
        }
        else
        {
            isSelected = !isSelected;
            // Todo display action icon
            print(isSelected ? "FX SELECT" : "FX UNSELECT"); // TODO
            _renderer.material.color = isSelected ? _selectedColor : _originalColor;
            var operation = isSelected ? -1 : 1;
            GameManager.GameInstance.UpdateActionsLeft(operation);//todo do event
        }
    }

    // Set action according to building type
    private void SetAction()
    {
        actionType = buildingType switch
        {
            BuildingType.Laboratory => ActionType.Research,
            BuildingType.ExpeditionCenter => ActionType.GoXp,
            BuildingType.HarpoonStation => ActionType.Harpoon,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    // damage building
    public void GetDamage() {
        var damage = GameManager.GameInstance.damageHitValue;
        currentHealthPoints -= damage;
        if (currentHealthPoints <= 0)
        {
            isDestroyed = true;
            currentHealthPoints = 0;
        }
    }

    public void RepairBuilding(int amount)
    {
        currentHealthPoints += amount;
        if (currentHealthPoints > 0)
            isDestroyed = false;
    }
    
    //TOdo manage repair reserve
    // Calculate materials left after hp recovery
    public int CalculateMaterialsLeft(int maxHpAmount)
    {
        return (currentHealthPoints + maxHpAmount > maxHealthPoints) ? currentHealthPoints + maxHpAmount - maxHealthPoints : 0;
    }

    private void SetTooltipMessage()
    {
        switch (buildingType)
        {
            case BuildingType.Laboratory:
                tooltip.message = "To do: Laboratory description \n  HP " + currentHealthPoints + "/" + maxHealthPoints;
                break;
            case BuildingType.ExpeditionCenter:
                tooltip.message = "To do: Expedition Center description \n  HP " + currentHealthPoints + "/" + maxHealthPoints;
                break;
            case BuildingType.HarpoonStation:
                tooltip.message = "To do: Harpoon Station description \n  HP " + currentHealthPoints + "/" + maxHealthPoints;
                break;
            case BuildingType.House1:
                tooltip.message = "Houses \n  HP " + currentHealthPoints + "/" + maxHealthPoints;
                break;
            case BuildingType.House2:
                tooltip.message = "Houses \n  HP " + currentHealthPoints + "/" + maxHealthPoints;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
