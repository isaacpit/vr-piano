using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteMonitor : TextReadout
{
    public Color hintColor;
    public Color trackingColor;

    public void UpdateNoteMonitor(Enemy enemy)
    {
        if (enemy != null)
        {
           // Debug.Log($"{enemy.chord.chordType.ToString()}  {enemy.chord.RootNote.ToString()} {enemy.chord.SecondNote.ToString()} {enemy.chord.ThirdNote.ToString()}");

            string str = GetNotesAndColor(enemy);

            PrintToScreen(str);
        }
        else
        {
            PrintToScreen("");
        }

    }

    public string GetNotesAndColor(Enemy enemy)
    {
        string rootNote = enemy.chord.RootNote.ToString();
        rootNote = ShortNote(rootNote);

        string secondNote = enemy.chord.SecondNote.ToString();
        secondNote = ShortNote(secondNote);

        string thirdNote = enemy.chord.ThirdNote.ToString();
        thirdNote = ShortNote(thirdNote);


        string str = "Notes: \n\n<color=#" + ColorUtility.ToHtmlStringRGB(trackingColor) + ">" + rootNote;

        //if (enemy.hasSecondNoteBeenPlayed)
        //{
        //    str += "<color=" + trackingColor.ToString() + "> ";
        //}
        //else
        //{
        //    str += "<color=" + hintColor.ToString() + "> ";
        //}
        //str += secondNote + " ";



        //if (enemy.hasThirdNoteBeenPlayed)
        //{
        //    str += "<color=" + trackingColor.ToString() + "> ";
        //}
        //else
        //{
        //    str += "<color=" + hintColor.ToString() + "> ";
        //}
        //str += thirdNote + " ";

        str = AddNoteColor(str, secondNote, enemy.hasSecondNoteBeenPlayed);
        str = AddNoteColor(str, thirdNote, enemy.hasThirdNoteBeenPlayed);

        return str;

    }

    public string ShortNote(string s)
    {        
        if (s.Length > 1)
        {
            s = s[0] + "#";
        }

        return s;
    }

    public string AddNoteColor(string str, string note, bool beenPlayed)
    {
        if (beenPlayed)
        {
            str += "<color=#" + ColorUtility.ToHtmlStringRGB(trackingColor) + "> ";
        }
        else
        {
            str += "<color=#" + ColorUtility.ToHtmlStringRGB(hintColor) + "> ";
        }
        str += note + " ";

        return str;
    }

}
