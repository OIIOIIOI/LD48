using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoopState = CycleManager.LoopState;
using ActionPhase = CycleManager.ActionPhase;

[RequireComponent(typeof(AudioSource))]
public class TimeManager : MonoBehaviour
{
    
    public static TimeManager instance;

    /*private AudioSource audioSource;
    private float audioLoopDuration;
    private int fallDurationInAudioLoops = 2;
    private int stasisDurationInAudioLoops = 3;
    private int audioLoopsLeft;*/
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        // audioSource = GetComponent<AudioSource>();
        // audioSource.loop = true;
    }

    public void StartPhase(ActionPhase phase)
    {
        // TODO make this better with the future sound-based system
        float phaseDuration = 5f;
        switch (phase)
        {
            case ActionPhase.Fall:
                phaseDuration = 20f;
                break;
            case ActionPhase.PrepareStasis:
                phaseDuration = 5f;
                break;
            case ActionPhase.Stasis:
                phaseDuration = 10f;
                break;
            case ActionPhase.PrepareFall:
                phaseDuration = 5f;
                break;
        }
        Invoke(nameof(EndPhase), phaseDuration);
        
        /*audioLoopsLeft = (state == LoopState.Fall) ? fallDurationInAudioLoops : stasisDurationInAudioLoops;
        audioSource.Play();
        audioLoopDuration = audioSource.clip.length;
        StartCoroutine(nameof(LoopAudio));*/
    }
    
    /*private IEnumerator LoopAudio()
    {
        yield return new WaitForSeconds(audioLoopDuration);
        AudioLoopComplete();
    }

    private void AudioLoopComplete()
    {
        audioLoopsLeft--;
        
        if (CycleManager.instance.state == LoopState.Stasis && audioLoopsLeft == 2)
            CycleManager.instance.
        
        if (audioLoopsLeft > 0)
            StartCoroutine(nameof(LoopAudio));
        else
            EndPhase();
    }*/

    private void EndPhase()
    {
        // audioSource.Stop();
        // StopCoroutine(nameof(LoopAudio));
        
        // Notify CycleManager
        CycleManager.instance.EndPhase();
    }

    
}
