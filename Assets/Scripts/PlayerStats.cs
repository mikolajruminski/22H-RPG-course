using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [SerializeField] public string playerName;

    [SerializeField] public Sprite characterImage;

    [SerializeField] private int maxLevel = 50;
    public int playerLevel = 1;
    public int currentXP;
    public int[] xpForEachLevel;
    [SerializeField] private int baseLevelXP = 100;
    public int maxHP = 100;
    public int currentHP;

    public int maxMana = 30;
    public int currentMana;
    public string equippedWeaponName;
    public string equippedArmorName;

    public int weaponPower;
    public int armorDef;


    [SerializeField] public int dexterity;
    [SerializeField] public int defence;

    public ItemsManager equippedWeapon, equippedArmor;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        xpForEachLevel = new int[maxLevel];
        xpForEachLevel[1] = baseLevelXP;

        for (int i = 2; i < xpForEachLevel.Length; i++)
        {
            xpForEachLevel[i] = Mathf.FloorToInt(0.02f * Mathf.Pow(i, 3) + 30.06f * Mathf.Pow(i, 2) + 105.6f * i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddXP(int amountOfXP)
    {
        currentHP += amountOfXP;
        if (currentXP > xpForEachLevel[playerLevel])
        {
            currentXP -= xpForEachLevel[playerLevel];
            playerLevel++;

            if (playerLevel % 2 == 0)
            {
                dexterity++;
            }
            else
            {
                defence++;
            }

            maxHP = Mathf.FloorToInt(maxHP * 1.06f);
            currentHP = maxHP;

            maxMana = Mathf.FloorToInt(maxMana * 1.06f);
            currentMana = maxMana;
        }
    }

    public void AddHP(int amountOfHPtoAdd)
    {
        currentHP += amountOfHPtoAdd;

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void AddMana(int amountOfManaToAdd)
    {
        currentMana += amountOfManaToAdd;

        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }

    public void EquipWeapon(ItemsManager weaponToEquip)
    {
        equippedWeapon = weaponToEquip;
        equippedWeaponName = weaponToEquip.name;
        weaponPower = weaponToEquip.weaponDex;
    }

    public void EquipArmor(ItemsManager armorToEquip)
    {
        equippedArmor = armorToEquip;
        equippedArmorName = armorToEquip.name;
        armorDef = armorToEquip.armorDefence;
    }
}
