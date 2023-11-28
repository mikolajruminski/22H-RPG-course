using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
    [SerializeField] private Image imageToFade;
    [SerializeField] private GameObject menu;

    private PlayerStats[] playerStats;
    [SerializeField] TextMeshProUGUI[] nameText, hpText, manaText, lvlText, xpText;
    [SerializeField] Slider[] xpSlider;
    [SerializeField] Image[] playerImage;
    [SerializeField] GameObject[] characterPanel;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (menu.activeInHierarchy)
            {

                menu.SetActive(false);
                GameManager.Instance.gameMenuOpened = false;
            }
            else
            {
                UpdateStats();
                menu.SetActive(true);
                GameManager.Instance.gameMenuOpened = true;
            }
        }

    }

    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("StartFade");
    }

    public void UpdateStats()
    {
        playerStats = GameManager.Instance.GetPlayerStats();

        for (int i = 0; i < playerStats.Length; i++)
        {
            characterPanel[i].gameObject.SetActive(true);

            nameText[i].text = playerStats[i].playerName;
            hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
            manaText[i].text = "MP: " + playerStats[i].currentMana + "/" + playerStats[i].maxMana;

            lvlText[i].text = "Current XP: " + playerStats[i].currentXP;

            xpText[i].text = playerStats[i].currentXP.ToString() + "/" + playerStats[i].xpForEachLevel[playerStats[i].playerLevel];

            xpSlider[i].maxValue = playerStats[i].xpForEachLevel[playerStats[i].playerLevel];

            xpSlider[i].value = playerStats[i].currentXP;

            playerImage[i].sprite = playerStats[i].characterImage;
        }
    }
}
