﻿using System.Collections;
using System.Collections.Generic;
using Types;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject m_enemy;
    public Queue<GameObject> m_idleEnemies;
    public Queue<GameObject> m_hiddenEnemies;
    public int m_enemyPoolCount;
    public float randomDistanceRange = 0.1f;

    [Header("Pathing Settings")]
    public int m_enemyIdleCount;
    public GameObject m_pathingBox;
    public List<GameObject> m_pathingBoxes;


    private Random rnd;
    private Vector3 spawnPosition;
    private GameObject targetObjective = null;

    private void Awake()
    {
        
    }

    void Start()
    {
        // Dynamically create all a pool of enemies of type m_enemy

        createPool();

    }

    public void SetObjective(GameObject obj)
    {
        targetObjective = obj;
    }

    void createPool()
    {
        spawnPosition = transform.position;

        m_hiddenEnemies = new Queue<GameObject>();
        m_idleEnemies = new Queue<GameObject>();
        m_pathingBoxes = new List<GameObject>();

        // TODO : don't spawn all enemies into idle, leave some disabled in pool
        for (int i = 0; i < m_enemyPoolCount; ++i)
        {
            GameObject obj = new GameObject();
            obj.SetActive(false);
            obj = Instantiate(m_pathingBox);
            obj.transform.parent = this.transform;
            obj.transform.position = transform.position;
            m_pathingBoxes.Add(obj);
            obj.SetActive(true);
        }

        createIdleEnemies();

    }
    void createIdleEnemies()
    {
        for (int i = 0; i < m_enemyPoolCount; ++i)
        {
            Vector3 v = GetRandomPoint();

            GameObject obj = Instantiate(m_enemy);

            // make all enemies children of spawner for simplicity
            obj.transform.parent = this.transform;

            // set meta data of enemies
            Enemy enemyRef = obj.GetComponent<Enemy>();
            enemyRef.m_startPos = v;
            enemyRef.m_spawner = this;
            enemyRef.chord = Chord.GetRandomChord();
            enemyRef.pathingBox = m_pathingBoxes[i].GetComponent<DrawPoints>();

            m_pathingBoxes[i].GetComponent<DrawPoints>().enemy = enemyRef.gameObject;
            //enemyRef.m_objective = targetObjective;
            enemyRef.SetObjective(targetObjective);
            //enemyRef.m
            //enemyRef.m_endPos = enemyObjective.transform.position;
            m_idleEnemies.Enqueue(obj);
            obj.SetActive(true);


        }
    }

    Vector3 GetRandomPoint()
    {
        
        float xpos = Random.Range(transform.position.x - randomDistanceRange, 
            transform.position.x + randomDistanceRange);
        float ypos = Random.Range(transform.position.y - randomDistanceRange,
            transform.position.y + randomDistanceRange);
        float zpos = Random.Range(transform.position.z - randomDistanceRange,
            transform.position.z + randomDistanceRange);

        return new Vector3(xpos, ypos, zpos);
    }


    public void SpawnLiveEnemy(GameObject targetObjective)
    {
        if (m_idleEnemies.Count > 0)
        {

            Enemy enemy = m_idleEnemies.Dequeue().GetComponent<Enemy>();

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
}
