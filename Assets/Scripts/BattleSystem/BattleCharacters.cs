using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacters : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] string[] attacksAvailable;

    public string characterName;
    public int currentHP, maxHP, currentMana, maxMana, dexterity, defence, weaponPow, armorDef;

    public bool isDead;

    public bool IsPlayer()
    {
        return isPlayer;
    }

    public string[] GetAttacksAvailable()
    {
        return attacksAvailable;
    }

    public void TakeDamage(int damageToReceive)
    {
        currentHP -= damageToReceive;

        if (currentHP < 0)
        {
            currentHP = 0;
        }
    }
}
