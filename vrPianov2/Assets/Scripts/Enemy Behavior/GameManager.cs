using System;
using System.Collections;
using UnityEngine;

public class GameManager : SimpleSingleton<GameManager>
{
    public bool readyToSpawn = true;    
    
    public Piano piano;
    public CommonColors colors;

    private void Awake()
    {
        if (!piano)
        {
            piano = FindObjectOfType<Piano>();
        }
        colors = GetComponent<CommonColors>();
    }

    private void Start()
    {
        StartSpawning();
    }

    IEnumerator WaitToSpawnEnemy()
    {
        Debug.Log(3);
        EnemyManager.Instance.tracker.trackingMonitor.PrintToScreen("3");
        yield return new WaitForSeconds(1f);
        if (!readyToSpawn)
            yield break;
        Debug.Log(2);
        EnemyManager.Instance.tracker.trackingMonitor.PrintToScreen("2");
        yield return new WaitForSeconds(1f);
        if (!readyToSpawn)
            yield break;
        Debug.Log(1);
        EnemyManager.Instance.tracker.trackingMonitor.PrintToScreen("1");
        yield return new WaitForSeconds(1f);
        if (!readyToSpawn)
            yield break;
        EnemyManager.Instance.SpawnLiveEnemy();
    }

    public void StopSpawning()
    {
        readyToSpawn = false;

        var enemy = EnemyManager.Instance.tracker.currentTrackingEnemy;
        if (enemy)
        {
           enemy.PoolDestroy(false);
        }
        EnemyManager.Instance.tracker.noteMonitor.PrintToScreen("JAM OUT");
    }

    public void StartSpawning()
    {
        readyToSpawn = true;
        NextEnemy();
    }

    public void NextEnemy()
    {
        if(readyToSpawn)
            StartCoroutine(WaitToSpawnEnemy());
    }
}