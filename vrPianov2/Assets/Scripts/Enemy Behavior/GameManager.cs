using System.Collections;
using UnityEngine;

public class GameManager : SimpleSingleton<GameManager>
{
    public bool isHintAvailable = true;

    private void Start()
    {
        NextEnemy();
    }

    IEnumerator WaitToSpawnEnemy()
    {
        Debug.Log(3);
        EnemyManager.Instance.tracker.trackingMonitor.PrintToScreen("3");
        yield return new WaitForSeconds(1f);
        Debug.Log(2);
        EnemyManager.Instance.tracker.trackingMonitor.PrintToScreen("2");
        yield return new WaitForSeconds(1f);
        Debug.Log(1);
        EnemyManager.Instance.tracker.trackingMonitor.PrintToScreen("1");
        yield return new WaitForSeconds(1f);
        EnemyManager.Instance.SpawnEnemy();
    }

    public void NextEnemy()
    {
        StartCoroutine(WaitToSpawnEnemy());
    }
}
