using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PingPongImageColor : MonoBehaviour
{
    Image image;
    public float minAlpha = .75f;
    public float maxAlpha = 1f;
    public float pulseRate = 1f;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        var color = image.color;
        color.a = Mathf.PingPong(Time.time * pulseRate, maxAlpha - minAlpha) + minAlpha;
        image.color = color;
    }
}
