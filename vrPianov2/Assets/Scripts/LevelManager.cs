using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Types;
using TMPro;

public class LevelManager : SimpleSingleton<LevelManager>
{
    [Header("Settings")]
    public StageType currentStageType;
    public int streakAmountToChangeHandicaps;

    [Header("References")]
    [Tooltip("Make sure to have 1 scriptable per diffculty type")]
    public List<StageObject> stageObjectsList;
    public Transform stageSelectCanvas;//TODO Move to Canvas Manager?
    public Transform optionsCanvas;
    public GameObject startButton;
    public GameObject replayStageButton;
    public GameObject nextStageButton;

    [Header("Prefabs")]
    public GameObject stageButtonPrefab;

    [Header("Accessors")]
    public StageObject currentStageObject;
    public Handicaps currentHandicaps;

    [Header("Privates")]
    StageType previousStageType;//Allows for runtime change to take effect
    int previousHandicapLevel = 2;
    public int currentHandicapLevel = 2;
    public int failStreak;//If get a win reset to 0
    public int winStreak;//If get a fail reset to 0


    void Awake()
    {
        currentHandicaps = new Handicaps();
        currentStageObject = GetNextStageObjectsFromType(currentStageType, 0);//Just in case something else needs it
        previousHandicapLevel = currentHandicapLevel;
        AdjustHandicap();
        AddStageButtons();
        ShowMenu();
        startButton.SetActive(true);
        replayStageButton.SetActive(false);
        nextStageButton.SetActive(false);//TODO Logic for next
        //TODO Start the game with no enemies
    }

    void Update()
    {
        if (currentStageType != previousStageType)//This allows change in inspector, for quick runtime change
        {
            //currentStageObject = GetNextStageObjectsFromType(currentStageType, 0);
            //TODO Show new stage intro

        }
        previousStageType = currentStageType;

        if (failStreak >= streakAmountToChangeHandicaps)
        {
            currentHandicapLevel = Mathf.Clamp(++currentHandicapLevel, 0, 5);
            failStreak = 0;
            winStreak = 0;
        }
        else if (winStreak >= streakAmountToChangeHandicaps)
        {
            currentHandicapLevel = Mathf.Clamp(--currentHandicapLevel, 0, 5);
            failStreak = 0;
            winStreak = 0;
        }

        if (previousHandicapLevel != currentHandicapLevel)
        {
            AdjustHandicap();
            previousHandicapLevel = currentHandicapLevel;
        }
    }

    void AdjustHandicap()
    {
        currentHandicaps.SetAllHandicapsFalse();
        if (currentHandicapLevel > 3)//All hints
        {
            currentHandicaps.showColorOnKeysWhenHit = true;
        }
        if (currentHandicapLevel > 2)
        {
            currentHandicaps.showNotesOnKeys = true;
        }
        if (currentHandicapLevel > 1)
        {
            currentHandicaps.showUpdatedChordsOnDisplay = true;
        }
        if (currentHandicapLevel > 0)//Only 1 hint
        {
            currentHandicaps.showNotesOnDisplay = true;
        }
    }

    public void SetStage(int stageType)
    {
        currentStageObject = GetNextStageObjectsFromType(0, stageType);
        //previousStageType = currentStageType;
        stageSelectCanvas.gameObject.SetActive(false);
    }

    public void OnCallBackNextStage()
    {
        currentStageObject = GetNextStageObjectsFromType(currentStageObject.stageType, 1);
        //previousStageType = currentStageType;
        optionsCanvas.gameObject.SetActive(false);
    }

    public void DecreaseStage()
    {
        currentStageObject = GetNextStageObjectsFromType(currentStageObject.stageType, -1);
        //previousStageType = currentStageType;
        optionsCanvas.gameObject.SetActive(false);
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
                if (stageObject.totalNotesWeight == 0 || stageObject.totalChordsWeight == 0)
                {
                    CalculateTotalWeightsForThisStageObject(stageObject);
                }
                return stageObject;
            }
        }

        Debug.LogWarning("Stage Object for " + newStageType + " StageType is not defined");
        return currentStageObject;
    }

    void CalculateTotalWeightsForThisStageObject(StageObject stageObject)
    {
        int totalChordsWeight = 0;
        for (int i = 0; i < stageObject.weightedChordList.Count; i++)
        {
            totalChordsWeight += stageObject.weightedChordList[i].weight;
        }
        stageObject.totalChordsWeight = totalChordsWeight;

        int totalNotesWeight = 0;
        for (int i = 0; i < stageObject.weightedMusicalNoteList.Count; i++)
        {
            totalNotesWeight += stageObject.weightedMusicalNoteList[i].weight;
        }
        stageObject.totalNotesWeight = totalNotesWeight;
    }

    public ChordType GetRandomChordType()
    {
        int totalChordsWeight = Random.Range(0, currentStageObject.totalChordsWeight);
        for (int i = 0; i < currentStageObject.weightedChordList.Count; i++)
        {
            totalChordsWeight -= currentStageObject.weightedChordList[i].weight;
            if (totalChordsWeight <= 0)
            {
                return currentStageObject.weightedChordList[i].chordType;
            }
        }
        return currentStageObject.weightedChordList[0].chordType;
    }

    public MusicalNote GetRandomNote()
    {
        int totalNotesWeight = Random.Range(0, currentStageObject.totalNotesWeight);
        for (int i = 0; i < currentStageObject.weightedMusicalNoteList.Count; i++)
        {
            totalNotesWeight -= currentStageObject.weightedMusicalNoteList[i].weight;
            if (totalNotesWeight <= 0)
            {
                return currentStageObject.weightedMusicalNoteList[i].noteType;
            }
        }
        return currentStageObject.weightedMusicalNoteList[0].noteType;
    }

    public void AddStageButtons()
    {
        for (int i = 0; i < stageObjectsList.Count; i++)
        {
            GameObject stageButton = Instantiate(stageButtonPrefab, stageSelectCanvas);
            int stageNumber = i;//Enum starts at 0, stages start at 1
            stageButton.GetComponent<Button>().onClick.AddListener(() => SetStage(stageNumber));
            stageButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText((++stageNumber).ToString());
        }
    }

    public void ShowMenu()
    {
        optionsCanvas.gameObject.SetActive(true);
        stageSelectCanvas.gameObject.SetActive(false);
    }

    public void ShowStageSelect()
    {
        optionsCanvas.gameObject.SetActive(false);
        stageSelectCanvas.gameObject.SetActive(true);
    }

    public void OnCallbackReplay()
    {
        currentStageObject = GetNextStageObjectsFromType(currentStageType, 0);
        previousStageType = currentStageType;
        optionsCanvas.gameObject.SetActive(false);
    }

    public void OnCallBackStart()
    {
        SetStage(0);
        optionsCanvas.gameObject.SetActive(false);
        startButton.SetActive(false);
        replayStageButton.SetActive(true);
    }
}
