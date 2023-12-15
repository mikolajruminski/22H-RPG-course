using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    private bool isBattleActive;

    [SerializeField] private GameObject battleScene;

    [SerializeField] private Transform[] playersPositions;
    [SerializeField] private Transform[] enemiesPositions;

    [SerializeField] BattleCharacters[] playersPrefabs, enemiesPrefabs;

    [SerializeField] private int currentTurn;
    [SerializeField] private bool waitingForTurn;
    [SerializeField] private GameObject UIButtonHolder;

    [SerializeField] BattleMoves[] battleMovesList;
    [SerializeField] ParticleSystem turnParticle;
    [SerializeField] CharacterDamageGUI damageText;
    [SerializeField] private TextMeshProUGUI[] playerNamesText;
    [SerializeField] private GameObject[] playerBattleStats;
    [SerializeField] Slider[] playerHealth, playerMana;
    [SerializeField] GameObject enemyTargetPanel;
    [SerializeField] BattleTargetButtons[] targetButtons;

    [SerializeField] public GameObject magicChoicePanel;
    [SerializeField] BattleMagicButtons[] magicButtons;


    [SerializeField] private List<BattleCharacters> activeCharacters = new List<BattleCharacters>();
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartBattle(new string[] { "Warlock", "Blue Face" });
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            NextTurn();
        }

        if (isBattleActive)
        {
            if (waitingForTurn)
            {
                if (activeCharacters[currentTurn].IsPlayer())
                {
                    UIButtonHolder.SetActive(true);
                }
                else
                {
                    UIButtonHolder.SetActive(false);
                    StartCoroutine(EnemyMoveCoroutine());
                }
            }
        }
    }

    public void StartBattle(string[] enemiesToSpawn)
    {
        if (!isBattleActive)
        {
            SettingUpBattle();
            ImportPlayers();
            UpdatePlayerStats();
            AddingEnemies(enemiesToSpawn);

            waitingForTurn = true;
            // currentTurn = Random.Range(0, activeCharacters.Count);
            currentTurn = 0;

        }

    }

    private void AddingEnemies(string[] enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            if (enemiesToSpawn[i] != "")
            {
                for (int j = 0; j < enemiesPrefabs.Length; j++)
                {
                    if (enemiesPrefabs[j].characterName == enemiesToSpawn[i])
                    {
                        BattleCharacters newEnemy = Instantiate(enemiesPrefabs[j],
                        enemiesPositions[i].position,
                        enemiesPositions[i].rotation,
                        enemiesPositions[i]);

                        activeCharacters.Add(newEnemy);
                    }
                }
            }
        }
    }

    private void ImportPlayers()
    {
        for (int i = 0; i < GameManager.Instance.GetPlayerStats().Length; i++)
        {
            if (GameManager.Instance.GetPlayerStats()[i].gameObject.activeInHierarchy)
            {
                for (int j = 0; j < playersPrefabs.Length; j++)
                {
                    if (playersPrefabs[j].characterName == GameManager.Instance.GetPlayerStats()[i].playerName)
                    {
                        BattleCharacters newPlayer = Instantiate(
                        playersPrefabs[j],
                        playersPositions[i].position,
                        playersPositions[i].rotation,
                        playersPositions[i]);

                        activeCharacters.Add(newPlayer);
                        UpdatePlayerStats(i);
                    }
                }
            }
        }
    }

    private void UpdatePlayerStats(int i)
    {
        PlayerStats player = GameManager.Instance.GetPlayerStats()[i];

        activeCharacters[i].currentHP = player.currentHP;
        activeCharacters[i].maxHP = player.maxHP;
        activeCharacters[i].currentMana = player.currentMana;
        activeCharacters[i].maxMana = player.maxMana;

        activeCharacters[i].dexterity = player.dexterity;
        activeCharacters[i].defence = player.defence;

        activeCharacters[i].weaponPow = player.weaponPower;
        activeCharacters[i].armorDef = player.armorDef;
    }

    public void SettingUpBattle()
    {

        isBattleActive = true;
        GameManager.Instance.battleIsActive = true;

        transform.position = new Vector3(Camera.main.transform.position.x,
        Camera.main.transform.position.y, transform.position.z
         );
        battleScene.SetActive(true);

    }

    private void NextTurn()
    {
        currentTurn++;
        if (currentTurn >= activeCharacters.Count)
        {
            currentTurn = 0;
        }

        waitingForTurn = true;
        UpdateBattle();
        UpdatePlayerStats();
    }
    private void UpdateBattle()
    {
        bool allEnemiesAreDead = true;
        bool allPlayersAreDead = true;

        for (int i = 0; i < activeCharacters.Count; i++)
        {
            if (activeCharacters[i].currentHP < 0)
            {
                activeCharacters[i].currentHP = 0;
            }

            if (activeCharacters[i].currentHP == 0)
            {
                //kill character
            }
            else
            {
                if (activeCharacters[i].IsPlayer())
                {
                    allPlayersAreDead = false;
                }
                else
                {
                    allEnemiesAreDead = false;
                }
            }
        }

        if (allPlayersAreDead || allEnemiesAreDead)
        {
            if (allPlayersAreDead)
            {
                Debug.Log("you lose sucker");
            }
            else if (allEnemiesAreDead)
            {
                Debug.Log("you won");
            }

            battleScene.SetActive(false);
            GameManager.Instance.battleIsActive = false;
            isBattleActive = false;
        }
        else
        {
            while (activeCharacters[currentTurn].currentHP == 0)
            {
                currentTurn++;
                if (currentTurn >= activeCharacters.Count)
                {
                    currentTurn = 0;
                }
            }
        }
    }

    public IEnumerator EnemyMoveCoroutine()
    {
        waitingForTurn = false;
        yield return new WaitForSeconds(1);
        EnemyAtack();

        yield return new WaitForSeconds(1);
        NextTurn();
    }

    private void EnemyAtack()
    {
        List<int> players = new List<int>();


        for (int i = 0; i < activeCharacters.Count; i++)
        {
            if (activeCharacters[i].IsPlayer() && activeCharacters[i].currentHP > 0)
            {
                players.Add(i);
            }
        }

        int selectedPlayerToAttack = players[Random.Range(0, players.Count)];

        int selectedAttack = Random.Range(0, activeCharacters[currentTurn].GetAttacksAvailable().Length);
        int movePower = 0;

        for (int i = 0; i < battleMovesList.Length; i++)
        {
            if (battleMovesList[i].moveAttack == activeCharacters[currentTurn].GetAttacksAvailable()[selectedAttack])
            {
                movePower = GettingMovePower(selectedPlayerToAttack, i);
            }
        }

        InstantiateEffectOnAttackingCharacter();

        DealDamageToCharacters(selectedPlayerToAttack, movePower);
        UpdatePlayerStats();
    }

    private void InstantiateEffectOnAttackingCharacter()
    {
        Instantiate(turnParticle, activeCharacters[currentTurn].transform.position - Vector3.down * -1, Quaternion.identity);
    }

    private void DealDamageToCharacters(int selectedCharacterToAttack, int movePower)
    {
        float attackPower = activeCharacters[currentTurn].dexterity + activeCharacters[currentTurn].weaponPow;
        float defencAmount = activeCharacters[selectedCharacterToAttack].defence + activeCharacters[selectedCharacterToAttack].armorDef;

        float damageAmount = (attackPower / defencAmount) * movePower * Random.Range(0.9f, 1.1f);
        int damageToGive = (int)damageAmount;

        damageToGive = CriticalDamage(damageToGive);
        Debug.Log(activeCharacters[currentTurn].name + "just dealt" + damageAmount + "(" + damageToGive + ") to " + activeCharacters[selectedCharacterToAttack]);

        activeCharacters[selectedCharacterToAttack].TakeDamage(damageToGive);


        CharacterDamageGUI characterDamageText = Instantiate(damageText, activeCharacters[selectedCharacterToAttack].transform.position, Quaternion.identity);

        characterDamageText.SetDamage(damageToGive);
    }

    private int CriticalDamage(int damageAmount)
    {
        if (Random.value <= 0.1f)
        {
            Debug.Log("critical damage, instaed of " + damageAmount + "points. " + (damageAmount * 2)
            + "was dealt");

            return (damageAmount * 2);
        }
        else
        {
            return damageAmount;
        }

    }


    private void UpdatePlayerStats()
    {
        for (int i = 0; i < playerNamesText.Length; i++)
        {
            if (activeCharacters.Count > i)
            {

                if (activeCharacters[i].IsPlayer())
                {
                    BattleCharacters playerData = activeCharacters[i];

                    playerNamesText[i].text = playerData.characterName;

                    playerHealth[i].maxValue = playerData.maxHP;
                    playerHealth[i].value = playerData.currentHP;

                    playerMana[i].maxValue = playerData.maxMana;
                    playerMana[i].value = playerData.currentMana;
                }
                else
                {
                    playerBattleStats[i].gameObject.SetActive(false);

                }

            }
            else
            {
                playerBattleStats[i].gameObject.SetActive(false);
            }

        }
    }

    public void PlayerAttack(string moveName, int attackTarget)
    {
        int movePower = 0;

        for (int i = 0; i < battleMovesList.Length; i++)
        {
            if (battleMovesList[i].moveAttack == moveName)
            {
                movePower = GettingMovePower(attackTarget, i);
            }
        }

        InstantiateEffectOnAttackingCharacter();
        DealDamageToCharacters(attackTarget, movePower);
        NextTurn();
        enemyTargetPanel.SetActive(false);

    }

    private int GettingMovePower(int selectedEnemyTarget, int i)
    {
        int movePower;
        Instantiate(battleMovesList[i].effectToUse, activeCharacters[selectedEnemyTarget].transform.position, Quaternion.identity);

        movePower = battleMovesList[i].movePower;
        return movePower;
    }

    public void OpenTargetMenu(string moveName)
    {
        List<int> activeEnemies = new List<int>();
        enemyTargetPanel.SetActive(true);

        for (int i = 0; i < activeCharacters.Count; i++)
        {
            if (!activeCharacters[i].IsPlayer())
            {
                activeEnemies.Add(i);
            }
        }

        for (int i = 0; i < targetButtons.Length; i++)
        {
            if (activeEnemies.Count > i)
            {
                targetButtons[i].gameObject.SetActive(true);
                targetButtons[i].moveName = moveName;
                targetButtons[i].activeBattleTarget = activeEnemies[i];
                targetButtons[i].enemyName.text = activeCharacters[activeEnemies[i]].characterName;
            }
        }
    }
    public void OpenMagicPanel()
    {
        magicChoicePanel.SetActive(true);

        for (int i = 0; i < magicButtons.Length; i++)
        {
            if (activeCharacters[currentTurn].GetAttacksAvailable().Length > i)
            {
                magicButtons[i].gameObject.SetActive(true);
                magicButtons[i].spellName = GetCurrentActiveCharacter().GetAttacksAvailable()[i];
                magicButtons[i].spellNameText.text = magicButtons[i].spellName;

                for (int j = 0; j < battleMovesList.Length; j++)
                {
                    if (battleMovesList[j].moveAttack == magicButtons[i].spellName)
                    {
                        magicButtons[i].spellCost = battleMovesList[j].manaCost;
                        magicButtons[i].spellCostText.text = magicButtons[i].spellCost.ToString();
                    }
                }
            }
            else
            {
                magicButtons[i].gameObject.SetActive(false);
            }

        }
    }

    public BattleCharacters GetCurrentActiveCharacter()
    {
        return activeCharacters[currentTurn];
    }
}
