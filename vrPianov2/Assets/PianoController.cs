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

    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        hand = pose.inputSource;
    }

    private void Update()
    {
        var padPos = SteamVR_Actions.default_ModAndPitch.GetAxis(hand);
        RotateJoystick(padPos);
        if (padPos.sqrMagnitude > 0f)
        {
            RotateSphere(padPos);
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
