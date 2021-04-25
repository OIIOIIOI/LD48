using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QteSystem : MonoBehaviour
{
    public Text displayBox;
    public Canvas canvas;
    public GameObject largeCircle;
    public GameObject largeCircleMask;
    public GameObject smallCircle;
    public GameObject smallCircleMask;
    public float neutralTime = 1f;
    public float timeToSuccess = 2f;
    
    public List<KeyCode> keys;

    public delegate void OnSuccess(Boolean success);

    private OnSuccess _callback;

    private float _timer = 0f;
    private int _currentKey;
    private Boolean _ready = false;
    private float _neutralPercentage = 0f;

    private Vector3 _initialLargeCircleScale;
    private Vector3 _initialLargeCircleMaskScale;
    private Vector3 _initialSmallCircleScale;
    private Vector3 _initialSmallCircleMaskScale;

    private void Start()
    {
        _initialLargeCircleScale = largeCircle.transform.localScale;
        _initialLargeCircleMaskScale = largeCircleMask.transform.localScale;

        _initialSmallCircleScale = smallCircle.transform.localScale;
        _initialSmallCircleMaskScale = smallCircleMask.transform.localScale;
    }

    public void Initialize(Camera camera, OnSuccess callback)
    {
        canvas.worldCamera = camera;
        _callback = callback;
        if (keys.Count == 1)
        {
            _currentKey = 0;
        }
        else
        {
            _currentKey = Random.Range(0, keys.Count);
        }
        displayBox.text = keys[_currentKey].ToString();
        _ready = true;
    }

    void Update()
    {
        if (!_ready)
        {
            return;
        }
        _timer += Time.deltaTime;
        
        if (_timer > neutralTime)
        {
            Destroy(largeCircle);
            Destroy(largeCircleMask);
            if (_timer > timeToSuccess+neutralTime)
            {
                _callback(false);
            }
            else
            {
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(keys[_currentKey]))
                    {
                        _callback(true);
                    }
                }
            }
        }
        else
        {
            _neutralPercentage = _timer * 100 / neutralTime;

            float newXLargeCircleScale = _initialLargeCircleScale.x - (_initialSmallCircleScale.x * _neutralPercentage / 100);
            float newYLargeCircleScale = _initialLargeCircleScale.y - (_initialSmallCircleScale.y * _neutralPercentage / 100);
            
            float newXLargeCicleMaskScale = _initialLargeCircleMaskScale.x - (_initialSmallCircleMaskScale.x * _neutralPercentage / 100);
            float newYLargeCicleMaskScale = _initialLargeCircleMaskScale.y - (_initialSmallCircleMaskScale.y * _neutralPercentage / 100);

            largeCircle.transform.localScale = new Vector3(newXLargeCircleScale, newYLargeCircleScale);
            largeCircleMask.transform.localScale = new Vector3(newXLargeCicleMaskScale, newYLargeCicleMaskScale);
        }
    }
}
