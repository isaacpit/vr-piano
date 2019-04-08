using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonColors : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color majorColor;
    [ColorUsage(true, true)]
    public Color minorColor;
    [ColorUsage(true, true)]
    public Color diminishedColor;

    [ColorUsage(true, true)]
    public Color correctColor;
    [ColorUsage(true, true)]
    public Color incorrectColor;
}
