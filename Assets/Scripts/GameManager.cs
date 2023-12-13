using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private PlayerStats[] playerStats;
    public bool gameMenuOpened, dialogBoxOpened, shopOpened, battleIsActive;
    public int currentGold;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerStats = FindObjectsOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenuOpened || dialogBoxOpened || shopOpened || battleIsActive)
        {
            Player.Instance.deactivatedMovement = true;
        }
        else
        {
            Player.Instance.deactivatedMovement = false;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("data has been saved");
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("data has been loaded");
            LoadData();
        }
    }

    public PlayerStats[] GetPlayerStats()
    {
        return playerStats;
    }

    public void SaveData()
    {
        SavingPlayerPosition();
        SavingPlayerStats();
        SaveCurrentScene();

        PlayerPrefs.SetInt("Number_Of_Items", Inventory.Instance.ReturnItemsList().Count);

        for (int i = 0; i < Inventory.Instance.ReturnItemsList().Count; i++)
        {
            ItemsManager itemsInInventory = Inventory.Instance.ReturnItemsList()[i];
            PlayerPrefs.SetString("Item_" + i + "_Name", itemsInInventory.itemName);

            if (itemsInInventory.isStackable)
            {
                PlayerPrefs.SetInt("Items_" + i + "_Name", itemsInInventory.amountOfStacks);
            }
        }
    }

    private void SavingPlayerPosition()
    {
        PlayerPrefs.SetFloat("Player_Pos_X", Player.Instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Pos_Y", Player.Instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Pos_Z", Player.Instance.transform.position.z);
    }

    private void SavingPlayerStats()
    {
        PlayerPrefs.SetFloat("Player_Pos_X", Player.Instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Pos_Y", Player.Instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Pos_Z", Player.Instance.transform.position.z);

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_active", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_active", 0);

            }

            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_Level", playerStats[i].playerLevel);
            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_CurrentXP", playerStats[i].currentXP);

            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_MaxHP", playerStats[i].maxHP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_CurrentHP", playerStats[i].currentHP);

            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_MaxMana", playerStats[i].maxMana);
            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_CurrentMana", playerStats[i].currentMana);

            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_Dexterity", playerStats[i].dexterity);
            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_Defence", playerStats[i].defence);

            PlayerPrefs.SetString("Player_" + playerStats[i].playerName + "_EquippedWeapon", playerStats[i].equippedWeaponName);
            PlayerPrefs.SetString("Player_" + playerStats[i].playerName + "_EquippedArmor", playerStats[i].equippedArmorName);

            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_WeaponPower", playerStats[i].weaponPower);
            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_ArmorDefence", playerStats[i].armorDef);
        }
    }

    public void LoadData()
    {
        LoadPlayerStats();
        LoadplayerPosition();

        for (int i = 0; i < PlayerPrefs.GetInt("Number_Of_Items"); i++)
        {
            string itemName = PlayerPrefs.GetString("Item_" + i + "_Name");
            ItemsManager itemToAdd = ItemAssetsScript.Instance.GetItemAsset(itemName);
            int itemAmount = 0;

            if (PlayerPrefs.HasKey("Items_" + i + "_Amount"))
            {
                itemAmount = PlayerPrefs.GetInt("Items_" + i + "_Amount");
            }

            Inventory.Instance.AddItems(itemToAdd);

            if (itemToAdd.isStackable && itemAmount > 1)
            {
                itemToAdd.amountOfStacks = itemAmount;
            }
        }
    }

    private void SaveCurrentScene()
    {
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
    }

    private void LoadPlayerStats()
    {
        for (int i = 0; i < playerStats.Length; i++)
        {
            if (PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_active") == 0)
            {
                playerStats[i].gameObject.SetActive(false);
            }
            else
            {
                playerStats[i].gameObject.SetActive(true);
            }

            playerStats[i].playerLevel = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_Level");
            playerStats[i].currentXP = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_CurrentXP");

            playerStats[i].maxHP = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_MaxHP");
            playerStats[i].currentHP = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_CurrentHP");

            playerStats[i].maxMana = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_MaxMana");
            playerStats[i].currentMana = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_CurrentMana");

            playerStats[i].dexterity = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_Dexterity");
            playerStats[i].defence = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_Defence");

            playerStats[i].equippedWeaponName = PlayerPrefs.GetString("Player_" + playerStats[i].playerName + "_EquippedWeapon");
            playerStats[i].equippedArmorName = PlayerPrefs.GetString("Player_" + playerStats[i].playerName + "_EquippedArmor");

            playerStats[i].weaponPower = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_WeaponPower"); playerStats[i].armorDef = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_ArmorDefence");
        }
    }

    private void LoadplayerPosition()
    {
        Vector3 savedPlayerPosition =
                        new Vector3(PlayerPrefs.GetFloat("Player_Pos_X"),
                        PlayerPrefs.GetFloat("Player_Pos_Y"),
                        PlayerPrefs.GetFloat("Player_Pos_Z"));

        Player.Instance.transform.position = savedPlayerPosition;
    }
}
