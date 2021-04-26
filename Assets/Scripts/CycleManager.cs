using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void StartPhase(ActionPhase p)
    {
        phase = p;
        state = (phase == ActionPhase.Fall) ? LoopState.Fall : LoopState.Stasis;
        
        TimeManager.instance.StartPhase(phase);
    }

    public void EndPhase()
    {
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

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        stateUI.text = phase.ToString() + " (" + state.ToString().ToUpper() + ")";
        cycleUI.text = "CYCLE #" + currentCycle + " OF " + (totalCycles - 1);
    }
    
}
