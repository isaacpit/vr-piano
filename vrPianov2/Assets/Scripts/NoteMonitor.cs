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
        Debug.Log($"{enemy.chord.chordType.ToString()}  {enemy.chord.RootNote.ToString()} {enemy.chord.SecondNote.ToString()} {enemy.chord.ThirdNote.ToString()}");
        //Debug.Log($"Chord Type {enemy.chord.SecondNote.ToString()}");
        //Debug.Log($"Chord Type {enemy.chord.SecondNote.ToString()}");

        string str = "<color=#FF0000>" + enemy.chord.RootNote.ToString() + "</color>\n";
        string str1 = str + "<color=#00FF00>" + enemy.chord.SecondNote.ToString() + "</color>\n";





        Debug.Log(str1);
        PrintToScreen(str1);

    }

    public string GetNotesAndColor(Enemy enemy)
    {
        string rootNote = enemy.chord.RootNote.ToString();
        rootNote = ShortNote(rootNote);

        string secondNote = enemy.chord.SecondNote.ToString();
        ShortNote(secondNote);

        string thirdNote = enemy.chord.SecondNote.ToString();
        thirdNote = ShortNote(thirdNote);


        string str = "Notes: \n\n<color=" + trackingColor.ToString();

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
        string str = "";
        if (str.Length > 1)
        {
            str = s[0] + "#";
        }
        else
        {
            str = s;
        }

        return str;
    }

    public string AddNoteColor(string str, string note, bool beenPlayed)
    {
        if (beenPlayed)
        {
            str += "<color=" + trackingColor.ToString() + "> ";
        }
        else
        {
            str += "<color=" + hintColor.ToString() + "> ";
        }
        str += note + " ";

        return str;
    }
}