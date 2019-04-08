public class Handicaps
{
    public bool showNotesOnDisplay;
    public bool showRightOrWrongOnDisplay;
    public bool showNotesOnKeys;
    public bool showColorOnKeysWhenHit;

    public void SetAllHandicapsFalse()
    {
        showNotesOnDisplay = false;
        showRightOrWrongOnDisplay = false;
        showNotesOnKeys = false;
        showColorOnKeysWhenHit = false;
    }
}
