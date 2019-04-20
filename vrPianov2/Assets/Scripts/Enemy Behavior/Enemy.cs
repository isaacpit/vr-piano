using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Types;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public enum FlightMode
    {
        FLY_TO_PLAYER,
        FLY_IDLE,
        FLIGHT_MODE_COUNT
    }

    [Header("Position Data")]
    public Spawner m_spawner;
    public Vector3 m_startPos;
    public Vector3 m_endPos;
    public float m_stepSize;
    public float m_speed;
    public float m_rotateSpeed;
    public GameObject m_objective;
    public GameObject m_destroyCollider;
    public GameObject m_spawnNextEnemyCollider;

    [Space]
    [Header("Chord Info")]
    public Chord chord;
    //public ChordType chordType = ChordType.Major;
    private MeshRenderer m_meshRenderer;
    //public List<Material> m_materials;

    public bool hasRootNoteBeenPlayed = false;
    public bool hasSecondNoteBeenPlayed = false;
    public bool hasThirdNoteBeenPlayed = false;

    [Header("Death FX")]
    public AudioClip deathSound;

    [Header("Pathing Info")]
    public IdlePath pathingBox;

    public FlightMode m_currMode = FlightMode.FLY_IDLE;
    private bool hasPassedThreshold;


    // Start is called before the first frame update
    private void Awake()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        //PrintEnemy();
    }

    public void SetObjective(GameObject obj, GameObject destCollider)
    {
        m_objective = obj;
        m_destroyCollider = destCollider;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_destroyCollider || other.gameObject == m_objective)
        {
            PoolDestroy(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == m_spawnNextEnemyCollider)
        {
            hasPassedThreshold = true;
            Debug.Log("Spawning next enemy...");
            EnemyManager.Instance.GetNextEnemy();
            //GameManager.Instance.NextEnemy();
        }
    }

    private void OnEnable()
    {
        //InputManager.Instance.currentEnemy = this;
        spawnIdleEnemy();

    }

    private void OnDisable()
    {        
        hideEnemy();
        hasPassedThreshold = false;
    }

    public void beginIdle()
    {
        m_currMode = FlightMode.FLY_IDLE;
    }

    public void spawnLiveEnemy()
    {
        // TODO: add an animation to target enemies that are "live"
        m_currMode = FlightMode.FLY_TO_PLAYER;
        m_speed = UnityEngine.Random.Range(LevelManager.Instance.currentStageObject.minEnemySpeed, LevelManager.Instance.currentStageObject.minEnemySpeed);
        //transform.position = m_startPos; 
        //ChangeMaterial();
        //m_endPos = m_objective.transform.position;
        //// add this enemy to m_liveEnemies queue
        //EnemyManager.Instance.AddLiveEnemy(this);
        EnemyManager.Instance.AddLiveEnemy(this);
        EnemyManager.Instance.RemoveIdleEnemy(this);
    }

    private void spawnIdleEnemy()
    {
        m_currMode = FlightMode.FLY_IDLE;
        transform.position = m_startPos;
        ChangeMaterial();
        m_endPos = m_objective.transform.position;
        // add this enemy to m_liveEnemies queue
        //EnemyManager.Instance.AddLiveEnemy(this);
        EnemyManager.Instance.AddIdleEnemy(this);
    }

    private void hideEnemy()
    {
        // place back into spawner queue
        hasSecondNoteBeenPlayed = false;
        hasThirdNoteBeenPlayed = false;
        m_spawner.m_hiddenEnemies.Enqueue(this.gameObject);
        // remove from InputManager's live queue
        EnemyManager.Instance.RemoveLiveEnemy(this);
        EnemyManager.Instance.PoolDestroyEnemy(this);
        GameManager.Instance.CheckGameState(hasPassedThreshold);
    }

    private void ChangeMaterial()
    {
        Debug.Log("chordType: " + chord.chordType);
        switch (chord.chordType)
        {
            case ChordType.Major:
            case ChordType.NUM_CHORDS:
            default:
                m_meshRenderer.material.color = GameManager.Instance.colors.majorColor;
                m_meshRenderer.material.SetColor("_EmissionColor", GameManager.Instance.colors.majorColor);
                break;
            case ChordType.Minor:
                m_meshRenderer.material.color = GameManager.Instance.colors.minorColor;
                m_meshRenderer.material.SetColor("_EmissionColor", GameManager.Instance.colors.minorColor);
                break;
            case ChordType.Diminished:
                m_meshRenderer.material.color = GameManager.Instance.colors.diminishedColor;
                m_meshRenderer.material.SetColor("_EmissionColor", GameManager.Instance.colors.diminishedColor);
                break;
        }
        //m_material = m_materials[(int)chord.chordType];
        //Debug.Log("m_material: " + m_material.name);
        //GetComponent<MeshRenderer>().material = m_material;
    }

    public void SetChord(MusicalNote note, ChordType chordType)
    {
        chord = new Chord(note, chordType);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_currMode == FlightMode.FLY_TO_PLAYER)
        {
            float step = m_stepSize * Time.deltaTime * m_speed;
            Vector3 p = Vector3.MoveTowards(transform.position, m_endPos, step);

            p[1] += Mathf.Sin(p[2]) / 20;
            p[0] += Mathf.Cos(p[2]) / 20;

            // pathing based on chord type
            //switch (chord.chordType)
            //{
            //    case ChordType.Major:
            //    case ChordType.NUM_CHORDS:
            //    default:
            //        // swirly 
            //        p[1] += Mathf.Sin(p[2]) / 30;
            //        p[0] += Mathf.Cos(p[2]) / 30;
            //        break;
            //    case ChordType.Minor:
            //        // downward descent
            //        p[1] -= Mathf.PerlinNoise(p[1], p[0]) / 20;
            //        p[0] -= Mathf.Sin(p[2]) / 20;
            //        break;
            //    case ChordType.Diminished:
            //        // ??? 
            //        p[1] += Mathf.Cos(p[2]) / 20;
            //        p[0] -= Mathf.PerlinNoise(p[1], p[2]) / 20;
            //        break;
            //}

            transform.position = p;


            // rotation towards m_objective
            float rotStep = m_rotateSpeed * Time.deltaTime;
            Vector3 targetDir = (m_endPos - transform.position).normalized;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
        else if (m_currMode == FlightMode.FLY_IDLE)
        {
            // handled by IdlePath class that is associated... kinda convoluted...
        }

    }

    public void PoolDestroy(bool isDamageToPlayer)
    {
        
        if (isDamageToPlayer)
        {
            //  Debug.Log("Player Hit");
            EnemyManager.Instance.PlayHurtFX();
            LevelManager.Instance.winStreak = 0;
            LevelManager.Instance.failStreak++;
        }
        else
        {
            LevelManager.Instance.winStreak++;
            LevelManager.Instance.failStreak = 0;
            // TODO : activate flying animation here and place back into idle pool
        }
        GetComponent<EnemyReticle>().RemoveReticle();
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
        if (note == chord.RootNote)
        {
            hasRootNoteBeenPlayed = true;
            noteHit = true;
            if (LevelManager.Instance.currentHandicaps.showColorOnKeysWhenHit)
            {
                GameManager.Instance.piano.keys.Where(x => x.note == chord.RootNote).First().EnableCorrectColor();
            }
        }
        else if (note == chord.SecondNote)
        {
            hasSecondNoteBeenPlayed = true;
            noteHit = true;
            if (LevelManager.Instance.currentHandicaps.showColorOnKeysWhenHit)
            {
                GameManager.Instance.piano.keys.Where(x => x.note == chord.SecondNote).First().EnableCorrectColor();
            }
        }
        else if (note == chord.ThirdNote)
        {
            hasThirdNoteBeenPlayed = true;
            noteHit = true;
            if (LevelManager.Instance.currentHandicaps.showColorOnKeysWhenHit)
            {
                GameManager.Instance.piano.keys.Where(x => x.note == chord.ThirdNote).First().EnableCorrectColor();
            }
        }
        else
        {
            if (LevelManager.Instance.currentHandicaps.showColorOnKeysWhenHit)
            {
                StartCoroutine(GameManager.Instance.piano.keys.Where(x => x.note == note).First().FlashIncorrectColor());
            }
        }
        if (noteHit)
        {
            CheckForDeath();
        }

        return noteHit;
    }

    private void CheckForDeath()
    {
        if (hasRootNoteBeenPlayed && hasSecondNoteBeenPlayed && hasThirdNoteBeenPlayed)
        {
            PoolDestroy(false);
        }
    }

    public void OnRemoved()
    {
        EnemyManager.Instance.audioSource.PlayOneShot(deathSound);
        EnemyManager.Instance.PlayExplosionFX(this.transform, 50);
    }
}