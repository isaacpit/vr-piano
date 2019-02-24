using System.Collections;
using UnityEngine;

public class PianoKeyAudio : MonoBehaviour
{
    [HideInInspector]
    public AudioSource source;
    private DistanceReader reader;
    [SerializeField]
    private float minimumWaitToPlay = .2f;
    public bool readyToPlay = true;

    public bool soundStarted = false;

    //private PianoHand currentHand;
    //public GameObject physicalKey;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        reader = GetComponent<DistanceReader>();
        readyToPlay = true;
       // physicalKey = transform.parent.gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //soundStarted = true;
        //if (!source.isPlaying && soundStarted)
        //{
        //    source.Stop();
        //}
        if (collision.gameObject.layer == LayerMask.NameToLayer("HandController") && readyToPlay)
        {               
            var hand = collision.gameObject.GetComponent<PianoHand>();
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
    }
