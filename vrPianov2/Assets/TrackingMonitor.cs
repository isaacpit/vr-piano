using System.Collections;
using System.Collections.Generic;

public class TrackingMonitor : TextReadout
{
    public void PrintTrackingToScreen(string text)
    {
        text = $"Tracking:\n\n{text}";
        PrintToScreen(text);
    }    
}
