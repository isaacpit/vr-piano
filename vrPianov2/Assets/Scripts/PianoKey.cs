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

    [SerializeField]
    private Material hintMat;

    private Material originalMaterial;

    [SerializeField]
    private Color hintLightColor;
    [SerializeField]
    private Color trackingLightColor;
    private Color startingLightColor;

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
        startingLightColor = keyLight.color;
        originalMaterial = GetComponent<Renderer>().material;
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
        GetComponent<Renderer>().material = hintMat;
    }

    public void EnableLightTracking()
    {
        keyLight.color = trackingLightColor;
    }

    public void RestoreLight()
    {
        keyLight.color = startingLightColor;
        keyLight.intensity = startingIntensity;
        GetComponent<Renderer>().material = originalMaterial;

    }

    IEnumerator ChangeLightIntensityOnTouch()
    {
        keyLight.intensity = touchLightIntensity;
        yield return new WaitForSeconds(lightIntensityDuration);
        keyLight.intensity = startingIntensity;
    }
}
