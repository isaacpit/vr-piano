using UnityEngine;

public class PauseSpawnerButton : MonoBehaviour
{
    public float buttonDelay = .5f;
    public bool isReadyToPush = true;

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

                Debug.Log("pause");
                GameManager.Instance.StopSpawning();

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
