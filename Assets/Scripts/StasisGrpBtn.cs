using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StasisGrpBtn : MonoBehaviour
{
    public Button buildLab;
    public Button buildXpCenter;
    public Button repair;
    public Button goXp;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // compare action point to disable all
        if (GameManager.GameInstance.actionLeft > 0)
        {
            buildLab.interactable = false;
            buildXpCenter.interactable = false;
            goXp.interactable = false;
            repair.interactable = false;
        }
        // compare materials to disable buying functions
        buildLab.interactable = GameManager.GameInstance.materials >= GameManager.GameInstance.buildingCost;
        buildXpCenter.interactable = GameManager.GameInstance.materials >= GameManager.GameInstance.buildingCost;
        repair.interactable = GameManager.GameInstance.materials / GameManager.GameInstance.repairRatio > GameManager.GameInstance.repairRatio;
    }
}
