using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenu(menuName = "DifficultyObject")]
public class DifficultyObject : ScriptableObject
{
    [Header("Settings")]
    public bool showMusicalNotesOnKeyboard;
    public int numberOfHintKeys;
    public float minEnemySpeed;
    public float maxEnemySpeed;
    public List<WeightedEnemy> weightedEnemyList;// Ask about clusters vs this... Do they overlap/contradict?
    public List<WeightedNote> weightedMusicalNoteList;
}
