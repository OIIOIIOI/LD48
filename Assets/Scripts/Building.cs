using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using BuildingType = BuildingManager.BuildingType;

public class Building : MonoBehaviour
{
    public int maxHealthPoints;
    public int currentHealthPoints;
    public BuildingType buildingType;
    // Whether or not building is selected for fall phase
    public bool isSelected = false;
    // Icon displayed when action is performed (fall step)
    public IconInGame.IconType actionIcon;
    public Tooltip tooltip;
    // save last HP number (await stasis actions finalization)
    private int oldHP;


    //When selected over the GameObject, it turns to this color (red)
    private Color _selectedColor = Color.red; // TODO
    //This stores the GameObject’s original color
    Color _originalColor;
    
    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
    private MeshRenderer _renderer;
    
    // Start is called before the first frame update
    void Start()
    {
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
        // Update HP in building tooltip
        SetTooltipMessage();
    }

    // Fall preparation phase: selection of actions
    public void OnMouseDown()
    {
        // TODO Switch case according to cycle step phase for enable / disable selection & calculation
        if (GameManager.GameInstance.actionLeft == 0 && !isSelected)
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
            GameManager.GameInstance.UpdateActionsLeft(operation);
        }
    }

    public void PerformFallAction()
    {
        if (isSelected)
        {   // TODO implement function
            switch (buildingType)
            {
                case BuildingType.Laboratory:
                    print("execute Analyze relic function");
                    break;
                case BuildingType.ExpeditionCenter:
                    print("execute Prepare expedition function");
                    break;
                case BuildingType.HarpoonStation:
                    print("execute harpoon function");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
    
    public int RepairBuilding(int amount) {
        if (currentHealthPoints + amount > maxHealthPoints)
        {
            var rest = currentHealthPoints + amount - maxHealthPoints;
            currentHealthPoints = maxHealthPoints;
            return rest;
        }
        currentHealthPoints += amount;
        return 0;
    }

    public bool ReceiveDamage(int damage) {
        currentHealthPoints -= damage;
        return currentHealthPoints <= 0;
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
