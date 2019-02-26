using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHull : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ontriggerEnter for Ship Hull with " + other.name);
    }

}
