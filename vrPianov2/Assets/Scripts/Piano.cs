using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    public Instrument instrument;

    PianoKeyAudio[] keys;

    private void Awake()
    {
        keys = GetComponentsInChildren<PianoKeyAudio>();       
    }

    private void Start()
    {
        AssignPianoSounds();
    }

    public void AssignPianoSounds()
    {
        for (int i = 0; i < keys.Length; ++i)
        {
            Debug.Log(keys[i]);
            keys[i].source.clip = instrument.GetPianoNoteAudio((Instrument.PianoNote)i);
            Debug.Log(keys[i].source.clip);
        }
    }
}
