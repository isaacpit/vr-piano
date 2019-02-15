using UnityEngine;

[CreateAssetMenu(menuName = "Create Instrument",fileName ="NewInstrument", order = 0)]
public class Instrument : ScriptableObject
{
    public AudioClip C;
    public AudioClip Csharp;
    public AudioClip D;
    public AudioClip Eb;
    public AudioClip E;
    public AudioClip F;
    public AudioClip Fsharp;
    public AudioClip G;
    public AudioClip Gsharp;
    public AudioClip A;
    public AudioClip Bb;
    public AudioClip B;

    public enum PianoNote { C, Csharp, D, Eb, E, F, Fsharp, G, Gsharp, A, Bb, B};

    public AudioClip GetPianoNoteAudio(PianoNote note)
    {
        AudioClip clip;
        switch (note)
        {
            case PianoNote.C:
            default:
                clip = C;
                break;
            case PianoNote.Csharp:
                clip = Csharp;
                break;
            case PianoNote.D:
                clip = D;
                break;
            case PianoNote.Eb:
                clip = Eb;
                break;
            case PianoNote.E:
                clip = E;
                break;
            case PianoNote.F:
                clip = F;
                break;
            case PianoNote.Fsharp:
                clip = Fsharp;
                break;
            case PianoNote.G:
                clip = G;
                break;
            case PianoNote.Gsharp:
                clip = Gsharp;
                break;
            case PianoNote.A:
                clip = A;
                break;
            case PianoNote.Bb:
                clip = Bb;
                break;
            case PianoNote.B:
                clip = B;
                break;
        }
        return clip;
    }
}