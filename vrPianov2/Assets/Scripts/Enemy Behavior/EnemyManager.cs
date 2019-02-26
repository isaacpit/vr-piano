using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class EnemyManager : SimpleSingleton<EnemyManager>
{
    [Header("Spawner Settings")]
    public List<Spawner> m_spawners;
    public int m_spawnerIndex;

    public List<Enemy> m_liveEnemies;
    public Enemy m_currentEnemy;

    [SerializeField]
    private GameObject targetObjective;
    
    private void Awake()
    {
        m_liveEnemies = new List<Enemy>();
    }

    public void AddLiveEnemy(Enemy e)
    {
        m_liveEnemies.Add(e);

        //m_currentEnemy = m_liveEnemies[0];
        Debug.Log("m_liveEnemies: " + m_liveEnemies.Count);
    }

    public void RemoveLiveEnemy(Enemy e)
    {
        m_liveEnemies.Remove(e);
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
                    ChordType chord = (ChordType)Random.Range(0, (int)ChordType.NUM_CHORDS);
                    MusicalNote rootNote = (MusicalNote)Random.Range(0, System.Enum.GetValues(typeof(MusicalNote)).Length);
                    m_spawners[m_spawnerIndex].SpawnEnemy(rootNote, chord, targetObjective);
                } 
            }
        }
        if (m_currentEnemy != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("Triggered 1 ");
                if (m_currentEnemy.chord.chordType == ChordType.Major)
                {
                    m_currentEnemy.gameObject.SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("Triggered 2 ");
                if (m_currentEnemy.chord.chordType == ChordType.Minor)
                {
                    m_currentEnemy.gameObject.SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("Triggered 3 ");
                if (m_currentEnemy.chord.chordType == ChordType.Diminished)
                {
                    m_currentEnemy.gameObject.SetActive(false);
                }
            }
        }


    }
}
