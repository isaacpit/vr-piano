﻿using System.Collections;
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
    public List<WeightedEnemy> weightedEnemyList;// Ask about clusters vs this... Do they overlap/contradict?
    public List<WeightedNote> weightedMusicalNoteList;
}