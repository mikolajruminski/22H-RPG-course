using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExitScript : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private PlayerAreaEnterPossibilitesScript.AreaEntrances nextAreaEntrance;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            PlayerAreaEnterPossibilitesScript.SetAreaEntrance(nextAreaEntrance);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}