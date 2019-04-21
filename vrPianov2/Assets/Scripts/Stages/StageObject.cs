using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenu(menuName = "StageObject")]
public class StageObject : ScriptableObject
{
    [Header("Settings")]
    public StageType stageType;
    public float minEnemySpeed;
    public float maxEnemySpeed;
    public int minNumberOfEnemiesPerPool;
    public int maxNumberOfEnemiesPerPool;
    public float spawnDelay;
    public int correctChordHealAmount;
    public int wrongNoteDamageAmount;
    public int enemyDamageAmount;
    public List<WeightedChord> weightedChordList;// Ask about clusters vs this... Do they overlap/contradict?
    public List<WeightedNote> weightedMusicalNoteList;

    [Header("Accessors")]
    public int totalChordsWeight;
    public int totalNotesWeight;
    
}
