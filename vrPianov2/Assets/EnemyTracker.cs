using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Types;

public class EnemyTracker : MonoBehaviour
{
    public Enemy currentTrackingEnemy;

    public TrackingMonitor trackingMonitor;
    public NoteMonitor noteMonitor;

    [SerializeField]
    private Instrument trackingSounds;
    
    private AudioSource audioSource;

    //[SerializeField]
    //private NoteMonitor noteMonitor;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    // TODO: USE WHEN SWITCH IS AVAILABLE
    //public void TrackNextEnemy()
    //{
    //    if (EnemyManager.Instance.m_liveEnemies.Count > 0) {
    //        var index = EnemyManager.Instance.m_liveEnemies.IndexOf(currentTrackingEnemy);
    //        ++index;
    //        if (index == EnemyManager.Instance.m_liveEnemies.Count)
    //        {
    //            index = 0;
    //        }
    //        TrackEnemy(EnemyManager.Instance.m_liveEnemies[index]);            
    //    }
    //}

    public void TrackEnemy(Enemy e)
    {
        StopTracking();
        currentTrackingEnemy = e;        
        UpdateMonitors();
        PlayTrackingSound();
        TurnOnTrackingAndHintLights();

    }

    private void TurnOnTrackingAndHintLights()
    {
        GameManager.Instance.piano.keys.Where(x => x.note == currentTrackingEnemy.chord.RootNote).First().EnableLightTracking();
        if (GameManager.Instance.isHintAvailable)
        {
            GameManager.Instance.piano.keys.Where(x => x.note == currentTrackingEnemy.chord.SecondNote).First().EnableLightHint();
            GameManager.Instance.piano.keys.Where(x => x.note == currentTrackingEnemy.chord.ThirdNote).First().EnableLightHint();
        }
    }

    private void PlayTrackingSound()
    {
        var clip = trackingSounds.GetPianoNoteAudio(currentTrackingEnemy.chord.RootNote);
        audioSource.clip = clip;
        audioSource.Play();
        
    }

    public void StopTracking()
    {
        audioSource.Stop();
        currentTrackingEnemy = null;        
        trackingMonitor.PrintToScreen("");
        RestoreKeyLights();
    }

    private void RestoreKeyLights()
    {
        foreach(var k in GameManager.Instance.piano.keys)
        {
            k.RestoreLight();
        }
    }

    private void UpdateMonitors()
    {
        trackingMonitor.PrintTrackingToScreen(currentTrackingEnemy.chord.ToString());
        noteMonitor.UpdateNoteMonitor(currentTrackingEnemy);
    }

    public void CheckNoteToEnemy(MusicalNote note)
    {
        if (currentTrackingEnemy && currentTrackingEnemy.CheckNoteToChord(note))
        {
            noteMonitor.UpdateNoteMonitor(currentTrackingEnemy);
            Debug.Log($"Note hit: {note} ");
        }
        else
        {
            Debug.Log("wrong note");
        }
    }



}
