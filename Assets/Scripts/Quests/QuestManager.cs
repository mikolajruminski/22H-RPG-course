using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    [SerializeField] private string[] questNames;
    [SerializeField] private bool[] questMarkersCompleted;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        questMarkersCompleted = new bool[questNames.Length];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("data has been saved");
            SaveQuestData();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("data has been loaded");
            LoadQuestData();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            print(CheckIfComplete("Defeat Dragon"));
            MarkQuestComplete("Steal The Gem");
            MarkQuestInComplete("Take Monster Soul");
        }
    }

    public int GetQuestNumber(string questToFind)
    {
        for (int i = 0; i < questNames.Length; i++)
        {
            if (questNames[i] == questToFind)
            {
                return i;
            }
        }

        Debug.Log($"Quest: " + questToFind + " does not exist");
        return 0;
    }

    public void UpdateQuestObjects()
    {
        QuestObject[] questObjects = FindObjectsOfType<QuestObject>();

        if (questObjects.Length > 1)
        {
            foreach (QuestObject item in questObjects)
            {
                item.CheckForCompletion();
            }
        }
    }

    public bool CheckIfComplete(string questToCheck)
    {
        int questNumberToCheck = GetQuestNumber(questToCheck);

        if (questNumberToCheck != 0)
        {
            return questMarkersCompleted[questNumberToCheck];
        }
        return false;
    }

    public void MarkQuestComplete(string questToMark)
    {
        int questNumberToCheck = GetQuestNumber(questToMark);
        questMarkersCompleted[questNumberToCheck] = true;

        UpdateQuestObjects();
    }

    public void MarkQuestInComplete(string questToMark)
    {
        int questNumberToCheck = GetQuestNumber(questToMark);
        questMarkersCompleted[questNumberToCheck] = false;

        UpdateQuestObjects();
    }

    public void SaveQuestData()
    {
        for (int i = 0; i < questNames.Length; i++)
        {
            if (questMarkersCompleted[i])
            {
                PlayerPrefs.SetInt("QuestMarker_" + questNames[i], 1);
            }
            else
            {
                PlayerPrefs.SetInt("QuestMarker_" + questNames[i], 0);

            }
        }
    }

    public void LoadQuestData()
    {
        for (int i = 0; i < questNames.Length; i++)
        {
            int valueToSet = 0;
            string keyToUse = "QuestMarker_" + questNames[i];

            if (PlayerPrefs.HasKey(keyToUse))
            {
                valueToSet = PlayerPrefs.GetInt(keyToUse);
            }

            if (valueToSet == 0)
            {
                questMarkersCompleted[i] = false;
            }
            else if (valueToSet == 1)
            {
                questMarkersCompleted[i] = true;
            }
        }
    }
}
