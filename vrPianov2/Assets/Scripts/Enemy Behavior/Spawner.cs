using System.Collections;
using System.Collections.Generic;
using Types;
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
            Vector3 v = GetRandomPoint();

            GameObject obj = Instantiate(m_enemy);

            // make all enemies children of spawner for simplicity
            obj.transform.parent = this.transform;

            // set meta data of enemies
            Enemy enemyRef = obj.GetComponent<Enemy>();
            enemyRef.m_startPos = v;
            enemyRef.m_spawner = this;
            //enemyRef.m_endPos = enemyObjective.transform.position;
            m_enemies.Enqueue(obj);
            obj.SetActive(false);

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


    public void SpawnEnemy(GameObject targetObjective)
    {
        if (m_enemies.Count > 0)
        {
            Enemy enemy = m_enemies.Dequeue().GetComponent<Enemy>();
            enemy.chord = Chord.GetRandomChord();
            enemy.objective = targetObjective;
            enemy.m_startPos = GetRandomPoint();
            enemy.gameObject.SetActive(true);
                        
            Debug.Log("spawnEnemy | Queue: " + m_enemies.Count);

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
