using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SimpleSingleton<InputManager>
{
    [Header("Spawner Settings")]
    public List<Spawner> m_spawners;
    public int m_spawnerIndex;

    public Queue<Enemy> m_liveEnemies;
    public Enemy m_currentEnemy;


    protected InputManager() { }

    private void Awake()
    {
        m_liveEnemies = new Queue<Enemy>();
    }

    public void addLiveEnemy(Enemy e)
    {
        InputManager.Instance.m_liveEnemies.Enqueue(e);

        m_currentEnemy = m_liveEnemies.Peek();
        Debug.Log("m_liveEnemies: " + m_liveEnemies);
    }

    public Enemy removeLiveEnemy()
    {

        Enemy front = InputManager.Instance.m_liveEnemies.Dequeue();
        if (m_liveEnemies.Count > 0)
        {
            m_currentEnemy = m_liveEnemies.Peek();
        }
        else
        {
            m_currentEnemy = null;
        }


        return front;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_spawnerIndex = Random.Range(0, m_spawners.Count);
            if (0 <= m_spawnerIndex && m_spawnerIndex < m_spawners.Count)
            {
                if (m_spawners[m_spawnerIndex] != null)
                {
                    Chord chord = (Chord)Random.Range(0, (int)Chord.NUM_CHORDS);
                    m_spawners[m_spawnerIndex].spawnEnemy(chord);
                } 
            }
        }
        if (m_currentEnemy != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("Triggered 1 ");
                if (m_currentEnemy.chordType == Chord.Major)
                {
                    m_currentEnemy.gameObject.SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("Triggered 2 ");
                if (m_currentEnemy.chordType == Chord.Minor)
                {
                    m_currentEnemy.gameObject.SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("Triggered 3 ");
                if (m_currentEnemy.chordType == Chord.Diminished)
                {
                    m_currentEnemy.gameObject.SetActive(false);
                }
            }
        }


    }
}
