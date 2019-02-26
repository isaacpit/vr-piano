using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSpawnerButton : MonoBehaviour
{
    [SerializeField]
    private Material stopMat;
    [SerializeField]
    private Material playMat;

    public bool isRunning = true;
}
