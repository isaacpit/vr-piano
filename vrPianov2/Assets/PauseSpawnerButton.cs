using UnityEngine;

public class PauseSpawnerButton : MonoBehaviour
{
    [SerializeField]
    private Material stopMat;
    [SerializeField]
    private Material playMat;

    public float buttonDelay = .5f;
    public bool isReadyToPush = true;

    public bool isRunning = true;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("HandController"))
        {
            if (isReadyToPush)
            {
                isReadyToPush = false;
                //StartCoroutine(WaitToPushAgain());
                var hand = collision.gameObject.GetComponent<PianoHand>();
                hand.HapticVibration();
                if (isRunning)
                {
                    Debug.Log("pause");
                    GameManager.Instance.StopSpawning();
                    isRunning = false;
                    GetComponent<Renderer>().material = playMat;
                }
                else
                {
                    Debug.Log("start spawning");
                    GameManager.Instance.StartSpawning();
                    isRunning = true;
                    GetComponent<Renderer>().material = stopMat;
                }
                isReadyToPush = true;
            }
        }
    }

    //IEnumerator WaitToPushAgain()
    //{
    //    yield return new WaitForSeconds(buttonDelay);
    //    isReadyToPush = true;
    //}
}
