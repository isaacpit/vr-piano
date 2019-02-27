using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    private AudioSource source;

    [SerializeField]
    private bool isPlaying;
    [SerializeField]
    private float loopDelay = .5f;
    [SerializeField]
    private bool startPlaying = false;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(LoopMusic());
    }

    private void Update()
    {
        if (startPlaying)
        {
            startPlaying = false;
            StartCoroutine(LoopMusic());
        }
    }

    IEnumerator LoopMusic()
    {
        while (true) {

            if (isPlaying)
            {
                source.Play();
                yield return new WaitForSeconds(loopDelay);
            }
            else
            {
                break;
            }
            yield return null;
        }        
    }
}
