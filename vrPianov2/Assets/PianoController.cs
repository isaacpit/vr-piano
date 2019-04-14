using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PianoController : MonoBehaviour
{
    SteamVR_Behaviour_Pose pose;
    GameObject thumbStick;
    SteamVR_Input_Sources hand;

    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        hand = pose.inputSource;
    }

    private void Update()
    {
        var padPos = SteamVR_Actions.pianoController_ModAndPitch.axis;
        var test = SteamVR_Actions.PianoController.ModAndPitch.GetAxis(hand);
        if(test.sqrMagnitude > 0f)
            Debug.Log(test);
    }
}
