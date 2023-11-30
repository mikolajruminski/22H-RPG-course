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

    [SerializeField] private GameObject[] statsButtons;

    private PlayerStats[] playerStats;
    [SerializeField] TextMeshProUGUI[] nameText, hpText, manaText, lvlText, xpText;
    [SerializeField] Slider[] xpSlider;
    [SerializeField] Image[] playerImage;
    [SerializeField] GameObject[] characterPanel;

    [SerializeField] private TextMeshProUGUI statName, statHP, statMana, statDex, statDef, statEquippedWeapon, statEquippedArmor;
    [SerializeField] private TextMeshProUGUI statWeaponPower, statArmorDefence;
    [SerializeField] private Image charStatImage;

    [SerializeField] private GameObject itemSlotContainer;
    [SerializeField] private Transform itemSlotContainerParent;

    public TextMeshProUGUI itemNameText, itemDescriptionText;
    public ItemsManager activeItem;

    [SerializeField] private GameObject characterChoicePanel;
    [SerializeField] private TextMeshProUGUI[] itemsCharacterChoiceName;
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

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("we've quit the game");
    }

    public void StatsMenu()
    {
        for (int i = 0; i < playerStats.Length; i++)
        {
            statsButtons[i].SetActive(true);
            statsButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = playerStats[i].playerName;

            StatsMenuUpdate(0);
        }
    }

    public void StatsMenuUpdate(int playerNumber)
    {
        PlayerStats playerSelected = playerStats[playerNumber];
        statName.text = playerSelected.playerName;
        statHP.text = playerSelected.currentHP.ToString() + "/" + playerSelected.maxHP;
        statMana.text = playerSelected.currentMana.ToString() + "/" + playerSelected.maxMana;
        statDex.text = playerSelected.dexterity.ToString();
        statDef.text = playerSelected.defence.ToString();

        charStatImage.sprite = playerSelected.characterImage;

        statEquippedWeapon.text = playerSelected.equippedWeaponName;
        statEquippedArmor.text = playerSelected.equippedArmorName;

        statWeaponPower.text = playerSelected.weaponPower.ToString();
        statArmorDefence.text = playerSelected.armorDef.ToString();

    }

    public void UpdateItemsInventory()
    {
        foreach (Transform itemSlot in itemSlotContainerParent)
        {
            Destroy(itemSlot.gameObject);
        }

        foreach (ItemsManager item in Inventory.Instance.ReturnItemsList())
        {
            RectTransform itemSlot = Instantiate(itemSlotContainer, itemSlotContainerParent).GetComponent<RectTransform>();

            Image itemImage = itemSlot.Find("ItemsImage").GetComponent<Image>();
            itemImage.sprite = item.itemImage;

            TextMeshProUGUI amountText = itemSlot.Find("AmountText").GetComponent<TextMeshProUGUI>();

            if (item.amountOfStacks > 1)
            {
                amountText.text = item.amountOfStacks.ToString();
            }
            else
            {
                amountText.text = "";
            }

            itemSlot.GetComponent<ItemButton>().itemOnButton = item;
        }
    }

    public void DiscardItem()
    {
        Inventory.Instance.RemoveItems(activeItem);
        UpdateItemsInventory();
    }

    public void UseItem(int characterToUseOn)
    {
        activeItem.UseItem(characterToUseOn);
        OpenCharacterChoice();
        DiscardItem();
    }

    public void OpenCharacterChoice()
    {
        characterChoicePanel.gameObject.SetActive(true);

        if (activeItem)
        {
            for (int i = 0; i < playerStats.Length; i++)
            {
                PlayerStats activePlayer = GameManager.Instance.GetPlayerStats()[i];
                itemsCharacterChoiceName[i].text = activePlayer.playerName;

                bool activePlayerAvailable = activePlayer.gameObject.activeInHierarchy;
                itemsCharacterChoiceName[i].transform.parent.gameObject.SetActive(activePlayerAvailable);
            }
        }

    }

    public void CloseCharacterChoicePanel()
    {
        characterChoicePanel.SetActive(false);
    }
}
