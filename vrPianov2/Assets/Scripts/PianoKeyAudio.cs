using UnityEngine;

public class PianoKeyAudio : MonoBehaviour
{
    public AudioSource source;
    private DistanceReader reader;
    [SerializeField]
    private float playTolerance = .6f;

    public bool soundStarted = false;

    private PianoHand currentHand;
    public GameObject physicalKey;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        reader = GetComponent<DistanceReader>();
        physicalKey = transform.parent.gameObject;
    }

    private void Update()
    {
        if (soundStarted && !source.isPlaying)
        {
            //currentHand.ResetHand(this);
            soundStarted = false;
        }
    }

    public void CheckForSound(PianoHand hand)
    {
        if (hand.readyToPlay)
        {
            //var dist = reader.GetDistanceRatioY();
            //Debug.Log(dist);
            if (!soundStarted)
            {
                //currentHand = hand;
                //hand.readyToPlay = false;                
                if (!source.isPlaying)
                    source.Play();
                else
                {
                    source.Stop();
                    source.Play();
                }
                soundStarted = true;
            }
        }
    }
}
