using System.Collections;
using Types;
using UnityEngine;

public class PianoKey : MonoBehaviour
{
    [HideInInspector]
    public AudioSource source;

    public MusicalNote note;
    [SerializeField]
    private float minimumWaitToPlay = .2f;
    public bool readyToPlay = true;

    public bool soundStarted = false;

    private Light keyLight;
    
    private Color originalColor;

    [SerializeField]
    private float touchLightIntensity = 1.2f;
    [SerializeField]
    private float lightIntensityDuration = .5f;
    private float startingIntensity;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        readyToPlay = true;
        keyLight = GetComponentInChildren<Light>();
        startingIntensity = keyLight.intensity;        
        originalColor = GetComponent<Renderer>().material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("HandController") && readyToPlay)
        {
            var hand = collision.gameObject.GetComponent<PianoHand>();
            StartCoroutine(ChangeLightIntensityOnTouch());
            if (hand.readyToPlay)
            {
                readyToPlay = false;
                hand.readyToPlay = false;
                hand.HapticVibration();
                if (hand.currentSource != null && hand.currentSource != source)
                {
                    hand.currentSource.Stop();
                }
                hand.currentSource = source;
                source.Play();
                EnemyManager.Instance.tracker.CheckNoteToEnemy(note);
                StartCoroutine(WaitToPlayAgain(hand));
            }
        }

    }

    IEnumerator WaitToPlayAgain(PianoHand hand)
    {
        yield return new WaitForSeconds(minimumWaitToPlay);
        readyToPlay = true;
        hand.readyToPlay = true;
    }

    //private void Update()
    //{
    //    if (soundStarted && !source.isPlaying)
    //    {
    //        soundStarted = false;
    //        physicalKey.layer = LayerMask.NameToLayer("PianoKeys");
    //    }
    //}

    //public void CheckForSound(PianoHand hand)
    //{
    //    if (hand.readyToPlay)
    //    {
    //        hand.readyToPlay = false;
    //        if (!source.isPlaying)
    //            source.Play();
    //        else
    //        {
    //            source.Stop();
    //            source.Play();
    //        }
    //        soundStarted = true;
    //    }
    //}

    public void EnableLightHint()
    {
        GetComponent<Renderer>().material.color = GameManager.Instance.colors.hintColor;
        GetComponent<Renderer>().material.SetColor("_EmissionColor", GameManager.Instance.colors.hintColor);
    }

    public void EnableLightTracking()
    {
        GetComponent<Renderer>().material.color = GameManager.Instance.colors.trackingColor;
        GetComponent<Renderer>().material.SetColor("_EmissionColor", GameManager.Instance.colors.trackingColor);
    }

    public void RestoreKeyColor()
    {
        keyLight.intensity = startingIntensity;
        GetComponent<Renderer>().material.color = originalColor;
        GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color());

    }

    IEnumerator ChangeLightIntensityOnTouch()
    {
        keyLight.intensity = touchLightIntensity;
        yield return new WaitForSeconds(lightIntensityDuration);
        keyLight.intensity = startingIntensity;
    }
}
