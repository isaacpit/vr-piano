using System.Collections.Generic;
using UnityEngine;
using Types;

public class DifficultyManager : SimpleSingleton<DifficultyManager>
{
    [Header("Settings")]
    public DifficultyType currentDifficultyType;

    [Header("References")]
    [Tooltip("Make sure to have 1 scriptable per diffculty type")]
    public List<DifficultySettings> difficultySettingsList;

    [Header("Accessors")]
    public DifficultySettings currentDifficultySettings;


    void Awake()
    {
        currentDifficultySettings = GetNextDifficultySettingsFromType(currentDifficultyType, 0);//Just in case something else needs it
    }

    void Update()
    {
        currentDifficultySettings = GetNextDifficultySettingsFromType(currentDifficultyType, 0);
    }

    public void IncreaseDifficulty()
    {
        currentDifficultySettings = GetNextDifficultySettingsFromType(currentDifficultySettings.difficultyType, 1);
    }

    public void DecreaseDifficulty()
    {
        currentDifficultySettings = GetNextDifficultySettingsFromType(currentDifficultySettings.difficultyType, -1);
    }

    DifficultySettings GetNextDifficultySettingsFromType(DifficultyType difficultyType, int increaseAmount)//negative for down
    {
        DifficultyType newDifficultyType = difficultyType + increaseAmount;
        bool isNextValueDefined = DifficultyType.IsDefined(typeof(DifficultyType), newDifficultyType);//If false return same one
        if (!isNextValueDefined)
        { 
            return currentDifficultySettings;
        }

        foreach (DifficultySettings difficultySettings in difficultySettingsList)
        {
            if(difficultySettings.difficultyType == newDifficultyType)
            {
                currentDifficultyType = newDifficultyType;
                return difficultySettings;
            }
        }

        Debug.LogWarning("Diffculty Settings for " + newDifficultyType + " DifficultyType is not defined");
        return currentDifficultySettings;
    }
}
