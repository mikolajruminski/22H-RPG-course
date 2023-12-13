using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    [SerializeField] private string newGameScene;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Player_Pos_X"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewGameButton()
    {
        SceneManager.LoadScene("Town2");
    }
    public void ExitButton()
    {
        Debug.Log("exit");
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}
