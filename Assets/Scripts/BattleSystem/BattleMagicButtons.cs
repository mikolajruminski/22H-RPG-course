using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleMagicButtons : MonoBehaviour
{
    public string spellName;
    public int spellCost;

    public TextMeshProUGUI spellNameText, spellCostText;

    public void Press()
    {
        if (BattleManager.Instance.GetCurrentActiveCharacter().currentMana >= spellCost)
        {
            BattleManager.Instance.magicChoicePanel.SetActive(false);
            BattleManager.Instance.OpenTargetMenu(spellName);
            BattleManager.Instance.GetCurrentActiveCharacter().currentMana -= spellCost;
        }

    }
}
