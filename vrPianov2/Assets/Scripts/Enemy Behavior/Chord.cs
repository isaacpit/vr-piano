using System;
using Types;
using UnityEngine;

[System.Serializable]
public class Chord
{
    public MusicalNote RootNote;
    public ChordType chordType;

    public Chord(MusicalNote rootNote = MusicalNote.C,ChordType type = ChordType.Major)
    {
        this.chordType = type;
        RootNote = rootNote;
    }

    public MusicalNote SecondNote { get { return GetSecondNoteOfChord(); } }
    public MusicalNote ThirdNote { get { return GetThirdNoteOfChord(); } }

    private MusicalNote GetSecondNoteOfChord()
    {
        int noteNum = (int)RootNote;
        switch (chordType)
        {
            case ChordType.Major:
            default:
                noteNum += 4;
                break;
            case ChordType.Minor:
            case ChordType.Diminished:
                noteNum += 3;
                break;
        }

        return (MusicalNote)(noteNum % 12);
    }

    private MusicalNote GetThirdNoteOfChord()
    {
        int noteNum = (int)RootNote;
        switch (chordType)
        {
            case ChordType.Major:
            case ChordType.Minor:
            default:
                noteNum += 7;
                break;            
            case ChordType.Diminished:
                noteNum += 6;
                break;
        }
        return (MusicalNote)(noteNum % 12);
    }
    public override string ToString()
    {
        string chordTypeText = "";
        switch (chordType)
        {
            case ChordType.Major:
            default:
                chordTypeText = "<color=#" + ColorUtility.ToHtmlStringRGB(GameManager.Instance.colors.majorColor) + ">maj</color>";
                break;
            case ChordType.Minor:
                chordTypeText = "<color=#" + ColorUtility.ToHtmlStringRGB(GameManager.Instance.colors.minorColor) + ">min</color>";
                break;
            case ChordType.Diminished:
                chordTypeText = "<color=#" + ColorUtility.ToHtmlStringRGB(GameManager.Instance.colors.diminishedColor) + ">dim</color>";
                break;
            case ChordType.NUM_CHORDS:
                chordTypeText = "ENUM_ERR";            
                break;
        }
        string note = RootNote.ToString();
        if(note.Length > 1)
        {
            note = note[0] + "#";
        }
        return note + chordTypeText;
    }

    public static Chord GetRandomChord()
    {
        ChordType chordType = LevelManager.Instance.GetRandomChordType();
        MusicalNote rootNote = LevelManager.Instance.GetRandomNote();
        return new Chord(rootNote, chordType);
    }
}
