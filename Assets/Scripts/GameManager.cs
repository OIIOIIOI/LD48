using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    /** GameObject UI **/
    // Fall preparation buttons group
    public GameObject fallButtonsGroup;
    // Stase preparation buttons group
    public GameObject stasePrepButtonsGroup;
    
    /** Global UI **/
    // Start fall cycle button
    public Button startFallButton;
    // Text UI for population
    public TextMeshProUGUI populationUI;
    // Text UI for Relic
    public TextMeshProUGUI relicsUI;
    // Text UI for Relic
    public TextMeshProUGUI materialsUI;
    // Text UI for Relic
    // public Text knowledgeUI;
    
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

    // Ratio population / action
    public int actionPopulationRatio = 5; 
    // Number of actions available
    private int _actionAvailable = 1;

    // Static instance of GM
    public static GameManager GameInstance;

    // Start is called before the first frame update
    private void Start()
    {
        // Set actions to dispatch
        actionLeft = _actionAvailable;
        
        /* Disable UI components */
        // Disable the fall launch
        startFallButton.gameObject.SetActive(false);
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
        _actionAvailable = population % actionPopulationRatio;
    }

    public void UpdateActionsLeft(int amount)
    {
        actionLeft += amount;
        // While actions left, disable start button
        startFallButton.gameObject.SetActive(actionLeft == 0? true: false);
    }
}
