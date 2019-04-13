using System.Collections;
using Types;
using UnityEngine;
using TMPro;

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
    private float incorrectLightFlashDuration = .1f;
    private int incorrectLightFlashAmount = 3;

    public TextMeshProUGUI noteDisplay;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        readyToPlay = true;
        keyLight = GetComponentInChildren<Light>();
        startingIntensity = keyLight.intensity;
        originalColor = GetComponent<Renderer>().material.color;
        ShowNote();
    }

    void Update()//TODO Take out of Update
    {
        if (LevelManager.Instance.currentHandicaps.showNotesOnKeys ^ noteDisplay.gameObject.activeSelf)//XOR
        {
            noteDisplay.gameObject.SetActive(!noteDisplay.gameObject.activeSelf);
        }
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

    public void EnableCorrectColor()
    {
        GetComponent<Renderer>().material.color = GameManager.Instance.colors.correctColor;
        GetComponent<Renderer>().material.SetColor("_EmissionColor", GameManager.Instance.colors.correctColor);
    }

    public IEnumerator FlashIncorrectColor()
    {
        for (int i = 0; i < incorrectLightFlashAmount; i++)
        {
            GetComponent<Renderer>().material.color = GameManager.Instance.colors.incorrectColor;
            GetComponent<Renderer>().material.SetColor("_EmissionColor", GameManager.Instance.colors.incorrectColor);
            yield return new WaitForSeconds(incorrectLightFlashDuration);
        }
        RestoreKeyColor();
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

    void ShowNote()
    {
        noteDisplay.gameObject.SetActive(true);

        string noteString = note.ToString();
        if (noteString.Length > 1)
        {
            noteString = noteString[0] + "#";
        }
        noteDisplay.text = noteString;
    }
}
