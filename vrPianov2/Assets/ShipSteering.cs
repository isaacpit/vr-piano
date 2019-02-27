using UnityEngine;

public class ShipSteering : MonoBehaviour
{
    [SerializeField]
    private float speedMultiplier = 1f;
    private Skybox skybox;

    private void Awake()
    {
        skybox = GetComponent<Skybox>();
    }

    void Update()
    {
        skybox.material.SetFloat("_Rotation", Time.time * speedMultiplier);
    }
}
