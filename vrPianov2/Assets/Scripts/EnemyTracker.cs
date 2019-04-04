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
        switch(DifficultyManager.Instance.currentDifficultySettings.difficultyObject.numberOfHintKeys)//TODO Change to ifs?
        {
            case 4:
                //TODO Check if the fourth note is needed to complete the chord
                //GameManager.Instance.piano.keys.Where(x => x.note == currentTrackingEnemy.chord.FourthNote).First().EnableLightHint();
                goto case 3;

            case 3:
                GameManager.Instance.piano.keys.Where(x => x.note == currentTrackingEnemy.chord.ThirdNote).First().EnableLightHint();
                goto case 2;

            case 2:
                GameManager.Instance.piano.keys.Where(x => x.note == currentTrackingEnemy.chord.SecondNote).First().EnableLightHint();
                goto default;

            default:
                GameManager.Instance.piano.keys.Where(x => x.note == currentTrackingEnemy.chord.RootNote).First().EnableLightTracking();
                break;
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
        RestoreKeyLights();
    }

    private void RestoreKeyLights()
    {
        foreach(var k in GameManager.Instance.piano.keys)
        {
            k.RestoreKeyColor();
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
