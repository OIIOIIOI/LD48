using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QTEManager : MonoBehaviour
{
    
    public static QTEManager instance;
 
    public enum QTEType { Debris, Expedition, Research };

    public RectTransform targetCanvas;
    public GameObject QTEUIPrefab;
    
    private List<IEnumerator> activeCoroutines;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        activeCoroutines = new List<IEnumerator>();
    }

    public void DeactivateAll ()
    {
        foreach (IEnumerator coroutine in activeCoroutines)
            StopCoroutine(coroutine);
        
        activeCoroutines.Clear();
    }

    public void Activate(QTEType type, float frequency)
    {
        IEnumerator coroutine = TriggerQTE(type, frequency);
        activeCoroutines.Add(coroutine);
        StartCoroutine(coroutine);
    }

    private IEnumerator TriggerQTE(QTEType type, float frequency)
    {
        while (true)
        {
            Debug.Log("Trigger QTE " + type.ToString());

            GameObject go = Instantiate(QTEUIPrefab, targetCanvas);
            go.GetComponent<QTEScript>().Init(type);
            
            // TODO delete objects when phase changes
            
            yield return new WaitForSeconds(frequency);
        }
    }

    public void ResolveQTE(QTEScript qte)
    {
        Debug.Log(qte.type.ToString() + " QTE is over: " + (qte.success ? "SUCCESS!" : "FAIL!"));
    }

}
