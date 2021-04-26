using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoopState = CycleManager.LoopState;

[RequireComponent(typeof(AudioSource))]
public class TimeManager : MonoBehaviour
{
    
    public static TimeManager instance;

    private AudioSource audioSource;
    private float audioLoopDuration;
    private int fallDurationInAudioLoops = 1;
    private int stasisDurationInAudioLoops = 1;
    private int audioLoopsLeft;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    public void StartPhase(LoopState state)
    {
        audioLoopsLeft = (state == LoopState.Fall) ? fallDurationInAudioLoops : stasisDurationInAudioLoops;
        audioSource.Play();
        audioLoopDuration = audioSource.clip.length;
        StartCoroutine(nameof(LoopAudio));
    }
    
    private IEnumerator LoopAudio()
    {
        yield return new WaitForSeconds(audioLoopDuration);
        AudioLoopComplete();
    }

    private void AudioLoopComplete()
    {
        audioLoopsLeft--;
        if (audioLoopsLeft > 0)
            StartCoroutine(nameof(LoopAudio));
        else
            EndPhase();
    }

    private void EndPhase()
    {
        audioSource.Stop();
        StopCoroutine(nameof(LoopAudio));
        
        // Notify CycleManager
        CycleManager.instance.EndPhase();
    }

    
}
