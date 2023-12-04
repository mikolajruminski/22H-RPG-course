using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHandler : MonoBehaviour
{
    public string[] sentences;
    private bool canActivate;

    [SerializeField] private bool shouldActivateQuest;
    [SerializeField] private string questToMark;
    [SerializeField] private bool markAsComplete;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canActivate && Input.GetKeyDown(KeyCode.E) && !DialogueController.Instance.IsDialogActive())
        {
            DialogueController.Instance.ActivateDialog(sentences);

            if (shouldActivateQuest) 
            {
                DialogueController.Instance.ActivateQuestAtTheEnd(questToMark, markAsComplete);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            canActivate = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            canActivate = false;
        }
    }
}
