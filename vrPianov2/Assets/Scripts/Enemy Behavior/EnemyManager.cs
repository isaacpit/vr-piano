using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Types;
using System.Linq;
using System;

public class EnemyManager : SimpleSingleton<EnemyManager>
{
    [Header("Spawner Settings")]
    public List<Spawner> m_spawners;
    public int m_spawnerIndex;

    public List<Enemy> m_liveEnemies;
    public List<Enemy> m_idleEnemies;
    public List<Enemy> m_pooledEnemies;
    public Enemy m_currentEnemy;

    [SerializeField]
    private GameObject targetObjective;
    [SerializeField]
    private GameObject destroyCollider;

    public EnemyTracker tracker;

    [Header("Special FX")]
    public float hurtFXDurationSec;
    public AudioSource audioSource;
    public Color defaultLightColor;
    public Color hurtLightColor;
    public List<Light> cockpitLights = new List<Light>();

    [Space]
    public ParticleSystem explosionFX;
    Coroutine hurtFXCoroutine;

    private void Awake()
    {
        m_liveEnemies = new List<Enemy>();
        m_idleEnemies = new List<Enemy>();
        tracker = GetComponent<EnemyTracker>();
        defaultLightColor = cockpitLights[0].color;
        for (int i = 0; i < m_spawners.Count; ++i)
        {
            m_spawners.ElementAt(i).SetObjective(targetObjective, destroyCollider);
        }
    }

    private void Start()
    {

    }

    public void TrackNextEnemy()
    {
        if (m_liveEnemies.Count > 0)
        {
            
            tracker.TrackEnemy(m_liveEnemies.ElementAt(0));
        }
    }

    public void AddLiveEnemy(Enemy e)
    {
        m_liveEnemies.Add(e);
        TrackNextEnemy();
        //if (m_liveEnemies.Count == 1 && m_liveEnemies.ElementAt(0) == e)
        //{
        //    tracker.TrackEnemy(e);
        //}
        //else
        //{
        //    Debug.Log("Adding idle enemy, but currently tracking another live enemy...");
        //}
        
        

        //m_currentEnemy = m_liveEnemies[0];
        Debug.Log("EnemyManager | m_liveEnemies: " + m_liveEnemies.Count);
    }

    public void GetNextEnemy()
    {
        StartCoroutine(WaitToSpawn());
    }

    private IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(LevelManager.Instance.currentStageObject.spawnDelay);
        GameManager.Instance.NextEnemy();
    }

    public void AddIdleEnemy(Enemy e)
    {
        m_idleEnemies.Add(e);

        Debug.Log("EnemyManager | m_idleEnemies: " + m_idleEnemies.Count);

    }

    public void RemoveLiveEnemy(Enemy e)
    {
        e.OnRemoved();
        m_liveEnemies.Remove(e);
        tracker.StopTracking();
        TrackNextEnemy();

        // TODO: REPLACE WITH DIFFERENT MECHANIC
        //GameManager.Instance.Spawn();

        //Enemy front = m_liveEnemies[0];
        //if (m_liveEnemies.Count > 0)
        //{
        //    m_currentEnemy = m_liveEnemies.Peek();
        //}
        //else
        //{
        //    m_currentEnemy = null;
        //}

        //return front;
    } 

    public void RemoveIdleEnemy(Enemy e)
    {
        m_idleEnemies.Remove(e);

    }

    public void PoolDestroyEnemy(Enemy e)
    {
        m_pooledEnemies.Add(e);
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SpawnEnemy();
        //}
        //if (m_currentEnemy != null)
        //{
        //    if (Input.GetKeyDown(KeyCode.Alpha1))
        //    {
        //        Debug.Log("Triggered 1 ");
        //        if (m_currentEnemy.chord.chordType == ChordType.Major)
        //        {
        //            m_currentEnemy.gameObject.SetActive(false);
        //        }
        //    }
        //    if (Input.GetKeyDown(KeyCode.Alpha2))
        //    {
        //        Debug.Log("Triggered 2 ");
        //        if (m_currentEnemy.chord.chordType == ChordType.Minor)
        //        {
        //            m_currentEnemy.gameObject.SetActive(false);
        //        }
        //    }
        //    if (Input.GetKeyDown(KeyCode.Alpha3))
        //    {
        //        Debug.Log("Triggered 3 ");
        //        if (m_currentEnemy.chord.chordType == ChordType.Diminished)
        //        {
        //            m_currentEnemy.gameObject.SetActive(false);
        //        }
        //    }
        //}
    }

    public void DestroyAllEnemies()
    {
        Debug.Log("idle enemies count: " + m_idleEnemies.Count);
        int c = m_idleEnemies.Count;
       while (m_idleEnemies.Count > 0)
        {
            Enemy e = m_idleEnemies.First();
            RemoveIdleEnemy(e);
            e.gameObject.SetActive(false);
        }
        //m_idleEnemies.Clear();
        for (int i = 0; i < m_liveEnemies.Count; ++i)
        {
            Enemy e = m_liveEnemies.First();
            RemoveLiveEnemy(e);
            e.gameObject.SetActive(false);
        }
        //m_liveEnemies.Clear();
    }

    public void SpawnLiveEnemy()
    {
        if (m_idleEnemies.Count > 0)
        {
            int idx = UnityEngine.Random.Range(0, m_idleEnemies.Count);
            m_idleEnemies.ElementAt(idx).m_spawner.SpawnLiveEnemy(targetObjective);
        }
        //else if (m_idleEnemies.Count < 1 && m_liveEnemies.Count > 0)
        //{
        //    Debug.Log("Still some enemies coming at the player...");
        //}
        //else
        //{
        //    Debug.Log("GAME OVER, NO ENEMIES LEFT");
        //}
        //m_spawnerIndex = UnityEngine.Random.Range(0, m_spawners.Count);
        //if (0 <= m_spawnerIndex && m_spawnerIndex < m_spawners.Count)
        //{
        //    if (m_spawners[m_spawnerIndex] != null)
        //    {               
        //        m_spawners[m_spawnerIndex].SpawnLiveEnemy(targetObjective);
        //    }
        //    else
        //    {
        //        Debug.LogError("Missing spawners");
        //    }
        //}
        //else
        //{
        //    Debug.LogError("Spawner index out of bounds");
        //}
    }

    public void PlayExplosionFX(Transform location, int particleCount)
    {
        explosionFX.transform.position = location.position;
        explosionFX.Emit(particleCount);
    }

    public void PlayHurtFX()
    {
        if (hurtFXCoroutine != null)
        {
            StopCoroutine(hurtFXCoroutine);
        }

        hurtFXCoroutine = StartCoroutine(HurtFXCoroutine(hurtFXDurationSec));
    }

    IEnumerator HurtFXCoroutine(float durationSec)
    {           
        foreach (Light cockpitLight in cockpitLights)
        {
            if(cockpitLight.GetComponent<PingPongIntensity>())
                cockpitLight.GetComponent<PingPongIntensity>().TogglePingPongLight(true);
            cockpitLight.color = hurtLightColor;
        }


        yield return new WaitForSeconds(durationSec);

        foreach (Light cockpitLight in cockpitLights)
        {
            if (cockpitLight.GetComponent<PingPongIntensity>())
                cockpitLight.GetComponent<PingPongIntensity>().TogglePingPongLight(false);
            cockpitLight.color = defaultLightColor;
        }

        yield break;
    }
}
