using System;
using System.Collections;
using UnityEngine;

public class GameManager : SimpleSingleton<GameManager>
{
    public bool readyToSpawn = true;    
    
    public Piano piano;
    public CommonColors colors;

    public float m_SpawnWaveWait = 1.0f;


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

    IEnumerator SpawnEnemyWave()
    {
        //Debug.Log(3);
        //EnemyManager.Instance.tracker.trackingMonitor.PrintToScreen("3");
        //yield return new WaitForSeconds(1f);
        //if (!readyToSpawn)
        //    yield break;
        //Debug.Log(2);
        //EnemyManager.Instance.tracker.trackingMonitor.PrintToScreen("2");
        //yield return new WaitForSeconds(1f);
        //if (!readyToSpawn)
        //    yield break;
        //Debug.Log(1);
        //EnemyManager.Instance.tracker.trackingMonitor.PrintToScreen("1");
        //yield return new WaitForSeconds(1f);
        //if (!readyToSpawn)
        //    yield break;
        //EnemyManager.Instance.SpawnLiveEnemy();


        //while (EnemyManager.Instance.m_idleEnemies.Count > 0)
        //{

        //    if (!readyToSpawn)
        //        yield break;
        //    EnemyManager.Instance.SpawnLiveEnemy();
        //    yield return new WaitForSeconds(m_SpawnWaveWait);

        //}
        //yield return new WaitForSeconds(m_SpawnWaveWait);
        //if (!readyToSpawn)
        //    yield break;
        yield return new WaitForSeconds(m_SpawnWaveWait);
        EnemyManager.Instance.SpawnLiveEnemy();

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
        FirstEnemy();
    }

    public void FirstEnemy()
    {
        // old
        //if(readyToSpawn)
        //    StartCoroutine(WaitToSpawnEnemy());

        if (readyToSpawn)
            StartCoroutine(WaitToSpawnEnemy());
    }

    public void CheckGameState()
    {
        Debug.Log("checking game state...");
        if (EnemyManager.Instance.m_liveEnemies.Count < 1 && EnemyManager.Instance.m_idleEnemies.Count < 1)
        {
            Debug.Log("GAME OVER");
        }
    }

    public void NextEnemy()
    {
        if (readyToSpawn)
            StartCoroutine(SpawnEnemyWave());
    }
}