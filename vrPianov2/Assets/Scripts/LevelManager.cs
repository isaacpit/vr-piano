using System.Collections.Generic;
using UnityEngine;
using Types;

public class LevelManager : SimpleSingleton<LevelManager>
{
    [Header("Settings")]
    public StageType currentStageType;
    public int streakAmountToChangeHandicaps;

    [Header("References")]
    [Tooltip("Make sure to have 1 scriptable per diffculty type")]
    public List<StageObject> stageObjectsList;

    [Header("Accessors")]
    public StageObject currentStageObject;
    public Handicaps currentHandicaps;

    [Header("Privates")]
    StageType previousStageType;
    int handicapLevel = 2;
    int failStreak;//If get a win reset to 0
    int winStreak;//If get a fail reset to 0


    void Awake()
    {
        currentHandicaps = new Handicaps();
        currentStageObject = GetNextStageObjectsFromType(currentStageType, 0);//Just in case something else needs it
        AdjustHandicap(); 
    }

    void Update()
    {
        if (currentStageType != previousStageType)//This allows change in inspector, for quick runtime change
        {
            currentStageObject = GetNextStageObjectsFromType(currentStageType, 0);
            //TODO Show new stage intro
        }
        previousStageType = currentStageType;

        if (failStreak >= streakAmountToChangeHandicaps)
        {
            handicapLevel = Mathf.Clamp(handicapLevel++, 0, 5);
            AdjustHandicap();
        }
        else if (winStreak >= streakAmountToChangeHandicaps)
        {
            handicapLevel = Mathf.Clamp(handicapLevel--, 0, 5);
            AdjustHandicap();
        }
    }

    void AdjustHandicap()
    {
        currentHandicaps.SetAllHandicapsFalse();
        if (handicapLevel > 3)//All hints
        {
            currentHandicaps.showColorOnKeysWhenHit = true;
        }
        if (handicapLevel > 2)
        {
            currentHandicaps.showNotesOnKeys = true;
        }
        if (handicapLevel > 1)
        {
            currentHandicaps.showUpdatedChordsOnDisplay = true;
        }
        if (handicapLevel > 0)//Only 1 hint
        {
            currentHandicaps.showNotesOnDisplay = true;
        }
    }

    public void SetStage(StageType stageType)
    {
        currentStageObject = GetNextStageObjectsFromType(0, (int)stageType);
    }

    public void IncreaseStage()
    {
        currentStageObject = GetNextStageObjectsFromType(currentStageObject.stageType, 1);
    }

    public void DecreaseStage()
    {
        currentStageObject = GetNextStageObjectsFromType(currentStageObject.stageType, -1);
    }

    StageObject GetNextStageObjectsFromType(StageType stageType, int increaseAmount)//negative for down
    {
        StageType newStageType = stageType + increaseAmount;
        bool isNextValueDefined = StageType.IsDefined(typeof(StageType), newStageType);
        if (!isNextValueDefined)
        {
            return currentStageObject;
        }

        foreach (StageObject stageObject in stageObjectsList)
        {
            if (stageObject.stageType == newStageType)
            {
                currentStageType = newStageType;
                return stageObject;
            }
        }

        Debug.LogWarning("Stage Object for " + newStageType + " StageType is not defined");
        return currentStageObject;
    }
}
