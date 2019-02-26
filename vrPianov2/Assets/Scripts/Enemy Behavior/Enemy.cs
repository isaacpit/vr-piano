using System;
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
    public float m_rotateSpeed;
    public GameObject objective;
    
    [Space]
    [Header("Chord Info")]
    public Chord chord;
    //public ChordType chordType = ChordType.Major;
    public Material m_material;
    public List<Material> m_materials;

    public bool hasSecondNoteBeenPlayed = false;
    public bool hasThirdNoteBeenPlayed = false;

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
        hasSecondNoteBeenPlayed = false;
        hasThirdNoteBeenPlayed = false;
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
        float step = m_stepSize * Time.deltaTime;
        Vector3 p = Vector3.MoveTowards(transform.position, m_endPos, step);

        p[1] += Mathf.Sin(p[2]) / 100;
        p[0] += Mathf.Cos(p[2]) / 100;
        transform.position = p;


        // rotation towards objective
        float rotStep = m_rotateSpeed * Time.deltaTime;
        Vector3 targetDir = (m_endPos - transform.position).normalized;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    public void PoolDestroy(bool isDamageToPlayer)
    {
        if (isDamageToPlayer)
        {
          //  Debug.Log("Player Hit");
        }
        else
        {
        //    Debug.Log($"Enemy destroyed by playing chord: {chord.ToString()}");
        }
        gameObject.SetActive(false);
    }

    public void PrintEnemy()
    {
        //Debug.Log("m_startPos: " + m_startPos.ToString("F4"));
        //Debug.Log("m_endPos: " + m_endPos.ToString("F4"));
    }

    public bool CheckNoteToChord(MusicalNote note)
    {
        var noteHit = false;
        if(note == chord.SecondNote)
        {
            hasSecondNoteBeenPlayed = true;
            noteHit = true;
        }else if(note == chord.ThirdNote)
        {
            hasThirdNoteBeenPlayed = true;
            noteHit = true;
        }
        if (noteHit)
        {
            CheckForDeath();
        }

        return noteHit;
    }

    private void CheckForDeath()
    {
        if(hasSecondNoteBeenPlayed && hasThirdNoteBeenPlayed)
        {
            PoolDestroy(false);
        }
    }
}
