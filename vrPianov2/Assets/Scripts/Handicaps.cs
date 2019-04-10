public class Handicaps
{
    public bool showNotesOnDisplay;
    public bool showUpdatedChordsOnDisplay;
    public bool showNotesOnKeys;
    public bool showColorOnKeysWhenHit;

    public void SetAllHandicapsFalse()
    {
        showNotesOnDisplay = false;
        showUpdatedChordsOnDisplay = false;
        showNotesOnKeys = false;
        showColorOnKeysWhenHit = false;
    }
}
