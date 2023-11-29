using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public enum ItemType
    {
        Item,
        Weapon,
        Armor,
    }

    public ItemType itemType;

    public string itemName, itemDescription;
    public int valueCoins;
    public Sprite itemImage;
    public int amountOfAffect;
    public enum AffectType { HP, Mana };
    public AffectType affectType;
    public int weaponDex;
    public int armorDefence;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Inventory.Instance.AddItems(this);
            Destroy(gameObject);
        }
    }
}