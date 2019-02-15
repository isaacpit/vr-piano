using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class PianoHand : MonoBehaviour
{
    public SteamVR_Action_Vibration haptic;
    private SteamVR_Behaviour_Pose pose;

    [SerializeField]
    private float vibrationDuration = 1f;
    [SerializeField]
    private float vibrationFrequency = 160f;
    [SerializeField]
    private float vibrationStrength = .5f;


    public bool readyToPlay = true;

    private void Awake()
    {
        pose = GetComponentInParent<SteamVR_Behaviour_Pose>();        
    }

    public void ResetHand(PianoKeyAudio keyAudio)
    {
        readyToPlay = true;
        keyAudio.physicalKey.layer = LayerMask.NameToLayer("PianoKeys");
        gameObject.layer = LayerMask.NameToLayer("HandController");
    }
    private void OnCollisionEnter(Collision collision)
    {
        HapticVibration();
        if (readyToPlay)
        {            
            collision.gameObject.GetComponentInChildren<PianoKeyAudio>().CheckForSound(this);
            collision.gameObject.layer = LayerMask.NameToLayer("PianoKeyPlaying");
            gameObject.layer = LayerMask.NameToLayer("HandPlayingKey");
        }
    }

    private void HapticVibration()
    {
        haptic.Execute(0f, vibrationDuration, vibrationFrequency, vibrationStrength, pose.inputSource);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!readyToPlay)
        {
            ResetHand(collision.gameObject.GetComponentInChildren<PianoKeyAudio>());
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (readyToPlay)
        {
            collision.gameObject.GetComponentInChildren<PianoKeyAudio>().CheckForSound(this);
            collision.gameObject.layer = LayerMask.NameToLayer("PianoKeyPlaying");
            gameObject.layer = LayerMask.NameToLayer("HandPlayingKey");
        }
    }



}
