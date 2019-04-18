using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PianoController : MonoBehaviour
{
    private SteamVR_Behaviour_Pose pose;
    private GameObject thumbStick;
    private SteamVR_Input_Sources hand;

    [SerializeField]
    private Transform sphereToRotate;
    [SerializeField]
    private Transform joystick;

    [SerializeField]
    private float sphereRotateSpeed = 750f;
    [SerializeField]
    private float joystickMaxAngle = 60f;

    private ControllerMixerInterface mixerInterface;

    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        hand = pose.inputSource;
        mixerInterface = GetComponentInParent<ControllerMixerInterface>();
    }

    private void Update()
    {
        if (hand == SteamVR_Input_Sources.LeftHand)
        {
            var pitchPos = SteamVR_Actions.default_Pitch.GetAxis(hand);
            if (pitchPos.sqrMagnitude > 0f)
            {
                RotateSphere(pitchPos);
            }
            RotateJoystick(pitchPos);
            mixerInterface.PitchChange(pitchPos);
        }
        else if (hand == SteamVR_Input_Sources.RightHand)
        {
            var modPos = SteamVR_Actions.default_Modulation.GetAxis(hand);
            if (modPos.sqrMagnitude > 0f)
            {
                RotateSphere(modPos);
            }
            RotateJoystick(modPos);
            mixerInterface.ModulationChange(modPos);
        }

    }

    private void RotateJoystick(Vector2 padPos)
    {
        Debug.Log(padPos);
        joystick.localEulerAngles = new Vector3(padPos.y * joystickMaxAngle, 0, -padPos.x * joystickMaxAngle);
    }

    private void RotateSphere(Vector2 padPos)
    {
        sphereToRotate.Rotate(padPos * sphereRotateSpeed * Time.deltaTime, Space.World);
    }
}
