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
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        state = LoopState.Fall;
        TimeManager.instance.StartPhase(state);
    }

    public void EndPhase()
    {
        if (state == LoopState.Stasis)
            currentCycle++;

        if (currentCycle < totalCycles)
        {
            state = (state == LoopState.Fall) ? LoopState.Stasis : LoopState.Fall;
            TimeManager.instance.StartPhase(state);
        }
        else
        {
            Debug.Log("GAME OVER");
        }
    }

    private void FixedUpdate()
    {
        stateUI.text = state.ToString();
        cycleUI.text = "CYCLE #" + currentCycle + " OF " + (totalCycles - 1);
    }
}
