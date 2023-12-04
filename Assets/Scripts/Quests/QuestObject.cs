using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private string questToCheck;
    [SerializeField] private bool activateIfComplete;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            CheckForCompletion();
        }
    }

    public void CheckForCompletion()
    {
        if (QuestManager.Instance.CheckIfComplete(questToCheck))
        {
            objectToActivate.SetActive(activateIfComplete);
        }
    }
}
