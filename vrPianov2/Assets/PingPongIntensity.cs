﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongIntensity : MonoBehaviour
{
    [SerializeField]
    private float minIntensity = 1f;
    [SerializeField]
    private float maxIntensity = 2f;
    [SerializeField]
    private float blinkRate = 1f;

    private Light light;
    
    void Awake()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        light.intensity = Mathf.PingPong(Time.time * blinkRate, maxIntensity-minIntensity) + minIntensity;
    }
}
