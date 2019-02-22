﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject m_enemy;
    public Queue<GameObject> m_enemies;
    public int m_enemyPoolCount;
    public float randomDistanceRange = 0.1f;


    private Random rnd;
    private Vector3 spawnPosition;

    private void Awake()
    {

    }

    void Start()
    {
        // Dynamically create all a pool of enemies of type m_enemy

        createPool();

    }

    void createPool()
    {
        spawnPosition = transform.position;

        m_enemies = new Queue<GameObject>();

        for (int i = 0; i < m_enemyPoolCount; ++i)
        {
            Vector3 v = getRandomPoint();

            GameObject obj = Instantiate(m_enemy);

            // make all enemies children of spawner for simplicity
            obj.transform.parent = this.transform;

            // set meta data of enemies
            Enemy enemyRef = obj.GetComponent<Enemy>();
            enemyRef.m_startPos = v;
            enemyRef.m_spawner = this;
            m_enemies.Enqueue(obj);
            obj.SetActive(false);

        }
    }

    Vector3 getRandomPoint()
    {
        
        float xpos = Random.Range(spawnPosition[0] - randomDistanceRange, 
            spawnPosition[0] + randomDistanceRange);
        float ypos = Random.Range(spawnPosition[1] - randomDistanceRange,
            spawnPosition[1] + randomDistanceRange);
        float zpos = Random.Range(spawnPosition[2] - randomDistanceRange,
            spawnPosition[2] + randomDistanceRange);

        return new Vector3(xpos, ypos, zpos);
    }


    public GameObject spawnEnemy(Chord chord)
    {
        GameObject obj = null;
        if (m_enemies.Count > 0)
        {
            obj = m_enemies.Dequeue();
            obj.GetComponent<Enemy>().setChordType(chord);
            obj.SetActive(true);
            Debug.Log("spawnEnemy | Queue: " + m_enemies.Count);
        }
        else
        {
            Debug.Log("No more enemies to spawn, returning null");
        }
        return obj;

    }
}
