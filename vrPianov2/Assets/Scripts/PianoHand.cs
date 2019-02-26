using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class PianoHand : MonoBehaviour
{
    public SteamVR_Action_Vibration haptic;
    private SteamVR_Behaviour_Pose pose;

    [SerializeField]
    private float vibrationDuration = .15f;
    [SerializeField]
    private float vibrationFrequency = 160f;
    [SerializeField]
    private float vibrationStrength = .2f;

    public AudioSource currentSource;


    public bool readyToPlay = true;    

    private void Awake()
    {
        pose = GetComponentInParent<SteamVR_Behaviour_Pose>();        
    }   

    public void HapticVibration()
    {
        haptic.Execute(0f, vibrationDuration, vibrationFrequency, vibrationStrength, pose.inputSource);
    }
   



}
