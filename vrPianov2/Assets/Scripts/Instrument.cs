using UnityEngine;
using Types;

[CreateAssetMenu(menuName = "Create Instrument",fileName ="NewInstrument", order = 0)]
public class Instrument : ScriptableObject
{
    public AudioClip C;
    public AudioClip Csharp;
    public AudioClip D;
    public AudioClip Dsharp;
    public AudioClip E;
    public AudioClip F;
    public AudioClip Fsharp;
    public AudioClip G;
    public AudioClip Gsharp;
    public AudioClip A;
    public AudioClip Asharp;
    public AudioClip B;    

    public AudioClip GetPianoNoteAudio(MusicalNote note)
    {
        AudioClip clip;
        switch (note)
        {
            case MusicalNote.C:
            default:
                clip = C;
                break;
            case MusicalNote.Csharp:
                clip = Csharp;
                break;
            case MusicalNote.D:
                clip = D;
                break;
            case MusicalNote.Dsharp:
                clip = Dsharp;
                break;
            case MusicalNote.E:
                clip = E;
                break;
            case MusicalNote.F:
                clip = F;
                break;
            case MusicalNote.Fsharp:
                clip = Fsharp;
                break;
            case MusicalNote.G:
                clip = G;
                break;
            case MusicalNote.Gsharp:
                clip = Gsharp;
                break;
            case MusicalNote.A:
                clip = A;
                break;
            case MusicalNote.Asharp:
                clip = Asharp;
                break;
            case MusicalNote.B:
                clip = B;
                break;
        }
        return clip;
    }
}