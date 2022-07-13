using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StasisActionsType = BuildingManager.StasisActionsType;

public class GameManager : MonoBehaviour
{
    /** GameObject UI **/
    // Fall preparation buttons group
    public GameObject fallButtonsGroup;
    // Stase preparation buttons group
    public GameObject stasePrepButtonsGroup;
    
    /** Global UI **/
    // Start fall cycle button
    public Button startButton;
    // Text UI for population
    public TextMeshProUGUI populationUI;
    // Text UI for Relic
    public TextMeshProUGUI relicsUI;
    // Text UI for Relic
    public TextMeshProUGUI materialsUI;
    // Text UI for Relic
    // public Text knowledgeUI; // todo uncomment
    
    /** Game Variable **/
    // Population
    public int population = 5;
    // Number of action left
    public int actionLeft;
    // Relics in stock
    public int relics = 0;
    // Materials in stock
    public int materials = 0;
    // knowledge in stock
    public int knowledge = 0;
    // Damage per it. May increase during the game
    public int damageHitValue = 1;
    // Ratio resources / repair point
    public int repairRatio = 2;
    // building cost
    public int buildingCost = 20;

    // Ratio population / action
    public int actionPopulationRatio = 5; 
    // Number of actions available
    public int actionAvailable = 1;
    
    // Static instance of GM
    public static GameManager GameInstance;
    // Stacked action of prep stasis phase
    [HideInInspector]
    public class StackedAction
    {
        public Placeholder placeholder { get; set; }
        public StasisActionsType actionType { get; set; }
        public int cost { get; set; }

        public StackedAction(Placeholder placeholder, StasisActionsType actionType, int cost)
        {
            this.placeholder = placeholder;
            this.actionType = actionType;
            this.cost = cost;
        }
    }
    // Stacked actions list
    private List<StackedAction> StackedActions = new List<StackedAction>();

    // Start is called before the first frame update
    private void Start()
    {
        // Set actions to dispatch
        actionLeft = actionAvailable;
        
        /* Disable UI components */
        // Disable the fall launch
        startButton.gameObject.SetActive(false);
        // Init buttons group
        // fallButtonsGroup.SetActive(false);
        // stasePrepButtonsGroup.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        // TODO control UI buttons according to cycle step
    }
    
    // Less called than Update
    private void FixedUpdate()
    {
        populationUI.text = population.ToString();
        relicsUI.text = relics.ToString();
        materialsUI.text = materials.ToString();

    }
    
    // Start when script on GameObject is initialized
    void Awake()
    {
        // Check if GM instance exists
        if (GameInstance == null)
        {
            GameInstance = this;
        }
        // Check if GM instance is equal to current object
        if (GameInstance != this)
        {
            Destroy(gameObject);
        }
        // Keep object between scene
        DontDestroyOnLoad(gameObject);
    }
    
    // todo change cycle implementation (disable buttons group)

    // Update population
    public void UpdatePopulation(int amount)
    {
        population += amount;
        UpdateActionsNumber();
    }
    
    // Update number of actions according to population
    private void UpdateActionsNumber()
    {
        actionAvailable = population % actionPopulationRatio;
    }

    public void UpdateActionsLeft(int amount)
    {
        actionLeft += amount;
        // While actions left, disable start button
        startButton.interactable = actionLeft == 0;
    }
    
    // While selecting in stasis prep phase, actions are stacked here
    public void StackStasisActions(Placeholder placeholder, StasisActionsType action)
    {
        placeholder.isSelected = !placeholder.isSelected;
        // Apply action cost
        if (placeholder.isSelected)
        {
            switch (action)
            {
                case StasisActionsType.GoXp:
                    StackedActions.Add(new StackedAction(placeholder, action, 0));
                    break;
                case StasisActionsType.Repair:
                    // Calcul cost for repairing building (max hp with all material
                    var maxHpAmount = (int)Mathf.Floor(materials / repairRatio);
                    var rest = BuildingManager.BuildingManagerInstance.SelectRepairAction(maxHpAmount, placeholder.buildingType);
                    var cost = (maxHpAmount - rest) * repairRatio;
                    materials -= cost;
                    StackedActions.Add(new StackedAction(placeholder, action, cost));
                    break;
                case StasisActionsType.BuildLab:
                    materials -= buildingCost;
                    StackedActions.Add(new StackedAction(placeholder, action, buildingCost));
                    break;
                case StasisActionsType.BuildXp:
                    StackedActions.Add(new StackedAction(placeholder, action, buildingCost));
                    break;
            }
        }
        // Cancel action cost
        else
        {
            var sa = StackedActions.Find(stackedAction => stackedAction.placeholder == placeholder);
            materials += sa.cost;
            StackedActions.Remove(sa);
        }
    }
    // Execute actions selected during prep stasis phase // Todo call this function when stasis prep phase is over
    public void performAction()
    {
        var bm = BuildingManager.BuildingManagerInstance;
        StackedActions.ForEach(a =>
        {
            switch (a.actionType)
            {

                case StasisActionsType.GoXp:
                    // Todo call function in event manager
                    break;
                case StasisActionsType.Repair:
                    var hp = a.cost / repairRatio;
                    bm.Repair(a.placeholder.buildingType, hp);
                    break;
                case StasisActionsType.BuildLab:
                    bm.Build(a.placeholder.buildingType, a.placeholder.placeholderType);
                    break;
                case StasisActionsType.BuildXp:
                    bm.Build(a.placeholder.buildingType, a.placeholder.placeholderType);
                    break;
            }
        });
    }
}
