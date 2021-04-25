using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public string message;

    private void OnMouseEnter()
    {
        StartCoroutine(nameof(TooltipsCoroutine));
    }

    private void OnMouseExit()
    {
        StopCoroutine(nameof(TooltipsCoroutine));
        TooltipsManager.TooltipsManagerInstance.HideTooltip();
    }
    
    // Delay tooltip display
    private IEnumerator TooltipsCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.5f);
        // Display tooltip
        TooltipsManager.TooltipsManagerInstance.SetAndShowTooltip(message);
    }
    // TEST
    public void OnMouseEnterUI(string inputMessage)
    {
        message = inputMessage;
        // Display tooltip
        StartCoroutine(nameof(TooltipsCoroutine));
    }
    public void OnMouseExitUI()
    {
        StopCoroutine(nameof(TooltipsCoroutine));
        TooltipsManager.TooltipsManagerInstance.HideTooltip();
    }
    
    
    
}
