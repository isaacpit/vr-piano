using UnityEngine;

public class PauseSpawnerButton : MonoBehaviour
{
    public float buttonDelay = .5f;
    public bool isReadyToPush = true;

    public bool isRunning = true;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("HandController"))
        {
            if (isReadyToPush)
            {
                isReadyToPush = false;
                //StartCoroutine(WaitToPushAgain());
                var hand = other.gameObject.GetComponent<PianoHand>();
                hand.HapticVibration();
                GetComponent<AudioSource>().Play();
                GetComponent<Animator>().SetTrigger("isSquish");
                if (isRunning)
                {
                    Debug.Log("pause");
                    GameManager.Instance.StopSpawning();
                    isRunning = false;
                }
                else
                {
                    Debug.Log("start spawning");
                    GameManager.Instance.StartSpawning();
                    isRunning = true;
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
