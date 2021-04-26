using System;
using System.Collections;
using UnityEngine;
using LoopState = CycleManager.LoopState;
using Random = UnityEngine.Random;

public class GameCamera : MonoBehaviour
{

    [Header("Fall Position")]
    public Vector3 fallPositionMin;
    public Vector3 fallPositionMax;
    [Header("Fall Rotation")]
    public Vector3 fallRotationMin;
    public Vector3 fallRotationMax;
    [Header("Fall Shake")]
    public float fallShakeAmountMin;
    public float fallShakeAmountMax;
    
    [Header("Stasis")]
    public Vector3 stasisPosition;
    public Vector3 stasisRotation;

    [Header("Misc")]
    public float transitionDurationMin = 2f;
    public float transitionDurationMax = 0.5f;
    
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 endPosition;
    private Quaternion endRotation;
    private float startShakeAmount;
    private float endShakeAmount;
    
    private LoopState state = LoopState.Fall;
    private float transitionDuration;
    private float elapsedTime;
    private float cycleModifier;
    
    private void Start()
    {
        transform.position = stasisPosition;
        transform.rotation = Quaternion.Euler(stasisRotation);
    }

    public void SetState(LoopState ls)
    {
        state = ls;
        cycleModifier = (float) CycleManager.instance.currentCycle / (float) (CycleManager.totalCycles - 1);
        transitionDuration = Mathf.Lerp(transitionDurationMin, transitionDurationMax, cycleModifier);
        elapsedTime = 0f;

        switch (state)
        {
            case LoopState.Fall:
                startPosition = stasisPosition;
                startRotation = Quaternion.Euler(stasisRotation);
                startShakeAmount = 0f;
                endPosition = Vector3.Lerp(fallPositionMin, fallPositionMax, cycleModifier);
                endRotation = Quaternion.Euler(Vector3.Lerp(fallRotationMin, fallRotationMax, cycleModifier));
                endShakeAmount = Mathf.Lerp(fallShakeAmountMin, fallShakeAmountMax, cycleModifier);
                break;
            case LoopState.Stasis:
                startPosition = Vector3.Lerp(fallPositionMin, fallPositionMax, cycleModifier);
                startRotation = Quaternion.Euler(Vector3.Lerp(fallRotationMin, fallRotationMax, cycleModifier));
                startShakeAmount = Mathf.Lerp(fallShakeAmountMin, fallShakeAmountMax, cycleModifier);
                endPosition = stasisPosition;
                endRotation = Quaternion.Euler(stasisRotation);
                endShakeAmount = 0f;
                break;
        }
    }

    private void Update()
    {
        if (state != CycleManager.instance.state)
            SetState(CycleManager.instance.state);

        Vector3 position = (state == LoopState.Fall) ? Vector3.Lerp(fallPositionMin, fallPositionMax, cycleModifier) : stasisPosition;
        Quaternion rotation = (state == LoopState.Fall) ? Quaternion.Euler(Vector3.Lerp(fallRotationMin, fallRotationMax, cycleModifier)) : Quaternion.Euler(stasisRotation);
        float shakeAmount = (state == LoopState.Fall) ? Mathf.Lerp(fallShakeAmountMin, fallShakeAmountMax, cycleModifier) : 0f;
        
        if (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            
            shakeAmount = Mathf.Lerp(startShakeAmount, endShakeAmount, t);
            
            // Smooth step
            t = t * t * (3f - 2f * t);
            elapsedTime += Time.deltaTime;
        
            position = Vector3.Lerp(startPosition, endPosition, t);
            rotation = Quaternion.Lerp(startRotation, endRotation, t);
        }

        position += Random.insideUnitSphere * shakeAmount;

        transform.position = position;
        transform.rotation = rotation;
    }

}
