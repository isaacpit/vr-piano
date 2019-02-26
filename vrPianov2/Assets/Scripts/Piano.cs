using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

public class Piano : MonoBehaviour
{
    public Instrument instrument;

    [HideInInspector]
    public PianoKey[] keys;

    private void Awake()
    {
        keys = GetComponentsInChildren<PianoKey>();       
    }

    private void Start()
    {
        AssignPianoSounds();
    }

    public void AssignPianoSounds()
    {
        for (int i = 0; i < keys.Length; ++i)
        {
            //Debug.Log(keys[i]);
            keys[i].source.clip = instrument.GetPianoNoteAudio((MusicalNote)i);
            keys[i].note = (MusicalNote)i;
            //Debug.Log(keys[i].source.clip);
        }
    }
}
