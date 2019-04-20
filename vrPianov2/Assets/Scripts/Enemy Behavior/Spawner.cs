using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Types;
using System.Linq;
using System;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject m_enemy;
    public List<GameObject> m_idleEnemies;
    public List<GameObject> m_hiddenEnemies;
    public int m_enemyPoolCount;
    public float randomDistanceRange = 0.1f;

    [Header("Pathing Settings")]
    public int m_enemyIdleCount;
    public GameObject m_pathingBox;
    public List<GameObject> m_pathingBoxes;
    public GameObject m_nextEnemyCheckpoint;

    private Vector3 spawnPosition;
    private GameObject targetObjective = null;
    private GameObject destroyCollider = null;

    private void Awake()
    {
        m_hiddenEnemies = new List<GameObject>();
        m_idleEnemies = new List<GameObject>();
        m_pathingBoxes = new List<GameObject>();
        createPool(m_enemyPoolCount);

    }

    void Start()
    {
        // Dynamically create all a pool of enemies of type m_enemy
        

    }

    public void SetObjective(GameObject obj, GameObject destCol)
    {
        targetObjective = obj;
        destroyCollider = destCol;
    }

    void createPool(int size = 0)
    {
        //Debug.Log("createPOOl: " + size);
        spawnPosition = transform.position;



        // creates pathing boxes
        // TODO : don't spawn all enemies into idle, leave some disabled in pool
        for (int i = 0; i < size; ++i)
        {
            GameObject obj ;
            //obj.SetActive(false);
            obj = Instantiate(m_pathingBox);
            obj.transform.parent = this.transform;
            obj.transform.position = transform.position;
            m_pathingBoxes.Add(obj);
            obj.SetActive(true);
        }

        createIdleEnemies(size);

    }

    void createIdleEnemies(int size = 0)
    {
        for (int i = 0; i < size; ++i)
        {
            Vector3 v = GetRandomPoint();

            GameObject obj = m_enemy;
            obj.SetActive(false);

            // make all enemies children of spawner for simplicity

            obj = Instantiate(m_enemy);
            obj.transform.parent = this.transform;
            // set meta data of enemies
            Enemy enemyRef = obj.GetComponent<Enemy>();
            enemyRef.m_startPos = v;
            enemyRef.m_spawner = this;
            if (m_nextEnemyCheckpoint == null)
            {
                Debug.Log("No checkpoint to spawn next enemy...");
            }
            else
            {
                enemyRef.m_spawnNextEnemyCollider = m_nextEnemyCheckpoint;
            }

            //enemyRef.chord = Chord.GetRandomChord();
            //enemyRef.pathingBox = m_pathingBoxes[i].GetComponent<IdlePath>();
            //Debug.Log("m_pathing boxes.count: " + m_pathingBoxes.Count);
            IdlePath emptyBox = m_pathingBoxes.Find(x => x.GetComponent<IdlePath>().enemy == null).GetComponent<IdlePath>();
            enemyRef.pathingBox = emptyBox;
            emptyBox.enemy = enemyRef.gameObject;


            //enemyRef.m_objective = targetObjective;
            enemyRef.SetObjective(targetObjective, destroyCollider);
            //m_pathingBoxes[i].GetComponent<IdlePath>().enemy = enemyRef.gameObject;
            //enemyRef.m
            //enemyRef.m_endPos = enemyObjective.transform.position;
            m_hiddenEnemies.Add(obj);
            //obj.SetActive(true);


        }
    }


    

    Vector3 GetRandomPoint()
    {
        
        float xpos = UnityEngine.Random.Range(transform.position.x - randomDistanceRange, 
            transform.position.x + randomDistanceRange);
        float ypos = UnityEngine.Random.Range(transform.position.y - randomDistanceRange,
            transform.position.y + randomDistanceRange);
        float zpos = UnityEngine.Random.Range(transform.position.z - randomDistanceRange,
            transform.position.z + randomDistanceRange);

        return new Vector3(xpos, ypos, zpos);
    }

    public void SpawnIdleEnemies(int sz)
    {
        for (int i = 0; i < sz; ++i)
        {
            
            if (m_hiddenEnemies.Count < 1)
                Debug.Log("All out of enemies in this spawner!!");
            Enemy e = m_hiddenEnemies.First().GetComponent<Enemy>();
            e.chord = Chord.GetRandomChord();
            e.m_spawner = this;
            m_idleEnemies.Add(e.gameObject);
            m_hiddenEnemies.Remove(e.gameObject);
            e.gameObject.SetActive(true);
        }
    }

    public void SpawnLiveEnemy(GameObject targetObjective)
    {
        if (m_idleEnemies.Count > 0)
        {
            
            Enemy enemy = m_idleEnemies.First().GetComponent<Enemy>();
            m_idleEnemies.Remove(enemy.gameObject);
            enemy.m_startPos = GetRandomPoint();
            //enemy.gameObject.SetActive(true);
            enemy.spawnLiveEnemy();
                        
            Debug.Log("spawnEnemy | idle Queue: " + m_idleEnemies.Count);

        }
        else
        {

            Debug.Log("No more enemies to spawn, returning null");

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1f);
    }

    public void CheckPoolAndSpawn(int sz)
    {
        // creates enemies if more are needed to satisfy the request
        if (m_hiddenEnemies.Count < sz)
            createPool(sz - m_hiddenEnemies.Count);

        // spawn from pooled resources
        SpawnIdleEnemies(sz);

    }
}
