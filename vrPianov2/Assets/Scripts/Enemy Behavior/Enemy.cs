using System.Collections.Generic;
using Types;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Position Data")]
    public Spawner m_spawner;
    public Vector3 m_startPos;
    public Vector3 m_endPos;
    public float m_stepSize;
    public GameObject objective;
    
    [Space]
    [Header("Chord Info")]
    public Chord chord;
    //public ChordType chordType = ChordType.Major;
    public Material m_material;
    public List<Material> m_materials;


    // Start is called before the first frame update
    private void Awake()
    {

        m_material = GetComponent<MeshRenderer>().material;        
    }

    void Start()
    {
        //PrintEnemy();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objective)
        {
            PoolDestroy(true);
        }

    }

    private void OnEnable()
    {
        //InputManager.Instance.currentEnemy = this;
        transform.position = m_startPos;
        ChangeMaterial();
        m_endPos = objective.transform.position;
        // add this enemy to m_liveEnemies queue
        EnemyManager.Instance.AddLiveEnemy(this);

    }

    private void OnDisable()
    {
        // place back into spawner queue
        m_spawner.m_enemies.Enqueue(this.gameObject);

        // remove from InputManager's live queue
        //Enemy front = 
        EnemyManager.Instance.RemoveLiveEnemy(this);
        //if (front != this)
        //{
        //    Debug.Log("ERROR: front of queue m_liveEnemies != this object");
        //}
    }

    private void ChangeMaterial()
    {
        Debug.Log("chordType: " + chord.chordType);

        m_material = m_materials[(int)chord.chordType];
        Debug.Log("m_material: " + m_material.name);
        GetComponent<MeshRenderer>().material = m_material;
    }

    public void SetChord(MusicalNote note, ChordType chordType)
    {
        chord = new Chord(note, chordType);
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 p = Vector3.MoveTowards(transform.position, m_endPos, m_stepSize * Time.deltaTime);

        p[1] += Mathf.Sin(p[2]) / 100;
        p[0] += Mathf.Cos(p[2]) / 100;
        transform.position = p;
    }

    private void PoolDestroy(bool isDamage)
    {
        this.gameObject.SetActive(false);
    }

    public void PrintEnemy()
    {
        Debug.Log("m_startPos: " + m_startPos.ToString("F4"));
        Debug.Log("m_endPos: " + m_endPos.ToString("F4"));
    }
}
