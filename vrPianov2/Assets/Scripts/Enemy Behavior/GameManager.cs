using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SimpleSingleton<GameManager>
{
    public bool readyToSpawn = true;

    public Piano piano;
    public CommonColors colors;

    public float m_SpawnWaveWait = 1.0f;
    public int maxHealth = 100;

    [Header("References")]
    public Image healthBarImage;
    [Header("Acccessors")]
    int playerHealth;
    int PlayerHealth
    {
        get
        {
            return playerHealth;
        }
        set
        {
            playerHealth = value;
            if (playerHealth > 100)
            {
                playerHealth = 100;
            }
            healthBarImage.fillAmount = (float)playerHealth / (float)maxHealth;
            if (playerHealth <= 0)
            {
                //TODO Blow up ship?
                StopSpawning();
            }
        }
    }


    public void HealPlayer(int health)
    {
        //print(health);
        //print(playerHealth);
        PlayerHealth = PlayerHealth + health;
        //print(playerHealth);
    }
    public void DamagePlayer(int damage)
    {
        //print(damage);
        //print(playerHealth);
        PlayerHealth = PlayerHealth - damage;
        //print(playerHealth);
    }

    private void Awake()
    {
        if (!piano)
        {
            piano = FindObjectOfType<Piano>();
        }
        colors = GetComponent<CommonColors>();
        PlayerHealth = 100;
    }

    private void Start()
    {
        //StartSpawning();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            StopSpawning();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            StartSpawning();
        }
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


        //var enemy = EnemyManager.Instance.tracker.currentTrackingEnemy;
        //if (enemy)
        //{
        //   enemy.PoolDestroy(false);
        //}

        EnemyManager.Instance.DestroyAllEnemies();

        EnemyManager.Instance.tracker.noteMonitor.PrintToScreen("JAM OUT");
    }

    public void StartSpawning()
    {
        PlayerHealth = 100;
        CheckPoolAndSpawn();
        readyToSpawn = true;
        FirstEnemy();
    }

    public void CheckPoolAndSpawn()
    {
        EnemyManager.Instance.QuerySpawners();

    }

    public void FirstEnemy()
    {
        // old
        //if(readyToSpawn)
        //    StartCoroutine(WaitToSpawnEnemy());


        if (readyToSpawn)
            StartCoroutine(WaitToSpawnEnemy());
    }

    public void CheckGameState(bool hasPassedThreshold = false)
    {
        Debug.Log("checking game state...");
        if (EnemyManager.Instance.m_liveEnemies.Count < 1 && EnemyManager.Instance.m_idleEnemies.Count < 1)
        {
            Debug.Log("GAME OVER");
        }
        else
        {
            if (!hasPassedThreshold)
                EnemyManager.Instance.GetNextEnemy();
            //NextEnemy();
        }
    }

    public void NextEnemy()
    {
        if (readyToSpawn)
            StartCoroutine(SpawnEnemyWave());
    }
}