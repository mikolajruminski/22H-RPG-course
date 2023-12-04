using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogueController : MonoBehaviour
{
    public static DialogueController Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI dialogueText, nameText;
    [SerializeField] private GameObject dialogBox, nameBox;
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private int currentLine;
    private bool dialogueJustStarted;

    private string questToMark;
    private bool markTheQuestComplete;
    private bool shouldMarkQuest;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        dialogueText.text = dialogueLines[currentLine];
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                if (!dialogueJustStarted)
                {

                    currentLine++;


                    if (currentLine >= dialogueLines.Length)
                    {
                        dialogBox.SetActive(false);
                        GameManager.Instance.dialogBoxOpened = false;

                        if (shouldMarkQuest)
                        {
                            shouldMarkQuest = false;
                            if (markTheQuestComplete)
                            {
                                QuestManager.Instance.MarkQuestComplete(questToMark);
                            }
                            else
                            {
                                QuestManager.Instance.MarkQuestComplete(questToMark);

                            }
                        }
                    }
                    else
                    {
                        CheckForName();
                        dialogueText.text = dialogueLines[currentLine];
                    }
                }
                else
                {
                    dialogueJustStarted = false;
                }
            }
        }
    }

    public void ActivateQuestAtTheEnd(string questName, bool markAsComplete)  
    {
      questToMark = questName;
      markTheQuestComplete = markAsComplete;
      shouldMarkQuest = true;
    }

    public void ActivateDialog(string[] newsSentecesToUse)
    {
        dialogueLines = newsSentecesToUse;
        currentLine = 0;

        CheckForName();
        dialogueText.text = dialogueLines[currentLine];
        dialogBox.SetActive(true);
        dialogueJustStarted = true;
        GameManager.Instance.dialogBoxOpened = true;
    }

    public bool IsDialogActive()
    {
        return dialogBox.activeInHierarchy;
    }

    private void CheckForName()
    {
        if (dialogueLines[currentLine].StartsWith('#'))
        {
            nameText.text = dialogueLines[currentLine].Replace("#", "");
            currentLine++;
        }
    }
}
