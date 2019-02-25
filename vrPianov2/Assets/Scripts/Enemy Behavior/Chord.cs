using Types;

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
}
