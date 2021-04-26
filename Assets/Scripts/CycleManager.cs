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
    
    public enum LoopState { Pause, Fall, Stasis };
    public enum ActionPhase { PrepareFall, Fall, PrepareStasis, Stasis }

    [HideInInspector]
    public LoopState state { get; private set; } = LoopState.Pause;
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
        StartPhase(LoopState.Fall);
    }

    private void StartPhase(LoopState s)
    {
        state = s;
        TimeManager.instance.StartPhase(s);
    }

    public void EndPhase()
    {
        if (state == LoopState.Stasis)
            currentCycle++;

        if (currentCycle < totalCycles)
            StartPhase((state == LoopState.Fall) ? LoopState.Stasis : LoopState.Fall);
        else
            Debug.Log("GAME OVER");
    }

    private void FixedUpdate()
    {
        stateUI.text = state.ToString();
        cycleUI.text = "CYCLE #" + currentCycle + " OF " + (totalCycles - 1);
    }
    
}
