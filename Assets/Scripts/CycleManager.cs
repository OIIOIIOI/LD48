using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QTEType = QTEManager.QTEType;

public class CycleManager : MonoBehaviour
{
    
    public static CycleManager instance;
    
    public static int totalCycles = 3;// 15

    public Text stateUI;// DEBUG
    public Text cycleUI;// DEBUG
    
    public enum LoopState { Fall, Stasis };
    public enum ActionPhase { PrepareFall, Fall, PrepareStasis, Stasis }

    [HideInInspector]
    public LoopState state { get; private set; }
    [HideInInspector]
    public ActionPhase phase { get; private set; }
    [HideInInspector]
    public int currentCycle { get; private set; } = 0;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        StartPhase(ActionPhase.Fall);
    }
    
    // GENERAL PHASE STUFF

    private void StartPhase(ActionPhase p)
    {
        Debug.Log("START PHASE " + p.ToString());

        phase = p;
        state = (phase == ActionPhase.Fall) ? LoopState.Fall : LoopState.Stasis;
        
        TimeManager.instance.StartPhase(phase);
        
        // Start the current phase
        switch (phase)
        {
            case ActionPhase.PrepareFall:
                StartPhasePrepFall();
                break;
            case ActionPhase.Fall:
                StartPhaseFall();
                break;
            case ActionPhase.PrepareStasis:
                StartPhasePrepStasis();
                break;
            case ActionPhase.Stasis:
                StartPhaseStasis();
                break;
        }
    }

    public void EndPhase()
    {
        Debug.Log("END PHASE " + phase.ToString());
        
        // End the current phase
        switch (phase)
        {
            case ActionPhase.Fall:
                EndPhaseFall();
                break;
        }
        
        // Handle cycle increment and start next phase
        if (phase == ActionPhase.Stasis)
            currentCycle++;

        if (currentCycle < totalCycles)
        {
            switch (phase)
            {
                case ActionPhase.Fall:
                    StartPhase(ActionPhase.PrepareStasis);
                    break;
                case ActionPhase.PrepareStasis:
                    StartPhase(ActionPhase.Stasis);
                    break;
                case ActionPhase.Stasis:
                    StartPhase(ActionPhase.PrepareFall);
                    break;
                case ActionPhase.PrepareFall:
                    StartPhase(ActionPhase.Fall);
                    break;
            }
        }
        else
            Debug.Log("GAME OVER");
    }

    // SPECIFIC PHASE BEHAVIOURS
    private void StartPhasePrepFall()
    {
        // Unselect buildings
        BuildingManager.BuildingManagerInstance.inGameBuildings.ForEach(b => b.isSelected = false);
        // Init UI for this phase
        UIManager.uiManagerInstance.SetUpFallPrep();
    }
    private void StartPhaseFall()
    {
        if (true)
            QTEManager.instance.Activate(QTEType.Debris, 4f);
        if (false)
            QTEManager.instance.Activate(QTEType.Expedition, 1.5f);
        if (false)
            QTEManager.instance.Activate(QTEType.Research, 2.5f);
    }

    private void StartPhasePrepStasis()
    {
        // Unselect buildings
        BuildingManager.BuildingManagerInstance.inGameBuildings.ForEach(b => b.isSelected = false);
        // Stock materials value
        GameManager.GameInstance.oldMaterials = GameManager.GameInstance.materials;
        // Init UI for this phase
        UIManager.uiManagerInstance.SetUpStasisPrep();
    }
    
    private void StartPhaseStasis()
    {
        
    }
    
    private void EndPhaseFall()
    {
        QTEManager.instance.DeactivateAll();
    }

    // UPDATES

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        stateUI.text = phase.ToString() + " (" + state.ToString().ToUpper() + ")";
        cycleUI.text = "CYCLE #" + currentCycle + " OF " + (totalCycles - 1);
    }
    
}
