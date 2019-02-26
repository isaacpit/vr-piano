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

    
    public NoteMonitor REMOVE_NOTE_MONITOR;

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


    public void SpawnEnemy(MusicalNote rootNote, ChordType chordType, GameObject targetObjective)
    {
        if (m_enemies.Count > 0)
        {
            Enemy enemy = m_enemies.Dequeue().GetComponent<Enemy>();
            enemy.SetChord(rootNote, chordType);
            enemy.objective = targetObjective;
            enemy.gameObject.SetActive(true);

            REMOVE_NOTE_MONITOR.UpdateNoteMonitor(enemy);
                        
            Debug.Log("spawnEnemy | Queue: " + m_enemies.Count);
        }
        else
        {
            Debug.Log("No more enemies to spawn, returning null");
        }

    }
}
