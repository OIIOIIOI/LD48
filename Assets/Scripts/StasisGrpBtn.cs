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

    public List<StasisBtn> stasisBtns;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var selectedBtn = stasisBtns.Find(b => b.isSelected);
        // If a button is selected disable others
        if (selectedBtn)
        {
            stasisBtns.ForEach(b =>
            {
                buildLab.interactable = buildLab.name == selectedBtn.name;
                buildXpCenter.interactable = buildLab.name == selectedBtn.name;
                goXp.interactable = buildLab.name == selectedBtn.name;
                repair.interactable = buildLab.name == selectedBtn.name;
            });
        }
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
