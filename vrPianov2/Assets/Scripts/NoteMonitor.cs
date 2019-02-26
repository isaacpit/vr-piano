using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteMonitor : TextReadout
{
    public void UpdateNoteMonitor(Enemy enemy)
    {
        if (enemy != null)
        {
            Debug.Log($"{enemy.chord.chordType.ToString()}  {enemy.chord.RootNote.ToString()} {enemy.chord.SecondNote.ToString()} {enemy.chord.ThirdNote.ToString()}");

            string str = "<color=#FF0000>" + enemy.chord.RootNote.ToString() + "</color>\n";
            string str1 = str + "<color=#00FF00>" + enemy.chord.SecondNote.ToString() + "</color>\n";

            Debug.Log(str1);
            PrintToScreen(str1);
        }
        else
        {
            PrintToScreen("");
        }

    }

}
