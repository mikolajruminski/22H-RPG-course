using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestZone : MonoBehaviour
{
    [SerializeField] private string questToMark;
    [SerializeField] private bool markAsComplete;
    [SerializeField] private bool markOnEnter;
    private bool canMark;

    public bool deactiveOnMarking;

    private void Update()
    {
        if (canMark && Input.GetKeyDown(KeyCode.Mouse0))
        {
            canMark = false;
            MarkTheQuest();
        }
    }
    public void MarkTheQuest()
    {
        if (markAsComplete)
        {
            QuestManager.Instance.MarkQuestComplete(questToMark);
        }
        else
        {
            QuestManager.Instance.MarkQuestInComplete(questToMark);
        }

        gameObject.SetActive(!deactiveOnMarking);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (markOnEnter)
            {
                MarkTheQuest();
            }
            else
            {
                canMark = true;
            }
        }
    }
}
