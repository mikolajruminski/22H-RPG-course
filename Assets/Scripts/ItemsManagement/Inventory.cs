using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    private List<ItemsManager> itemsList;
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
        itemsList = new List<ItemsManager>();
        Debug.Log("new inv is created");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItems(ItemsManager item)
    {
        if (item.isStackable)
        {
            bool itemAlreadyInInventory = false;


            foreach (ItemsManager itemInInventory in itemsList)
            {
                if (itemInInventory.itemName == item.itemName)
                {
                    itemInInventory.amountOfStacks += item.amountOfStacks;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemsList.Add(item);
            }
        }
        else
        {
            itemsList.Add(item);
        }
    }

    public void RemoveItems(ItemsManager itemToRemove)
    {
        if (itemToRemove.isStackable)
        {
            ItemsManager inventoryItem = null;

            foreach (ItemsManager item in itemsList)
            {
                if (item.itemName == itemToRemove.itemName)
                {
                    item.amountOfStacks--;
                    inventoryItem = item;
                }
            }

            if (inventoryItem != null && inventoryItem.amountOfStacks <= 0)
            {
                itemsList.Remove(inventoryItem);
            }
        }
        else
        {
            itemsList.Remove(itemToRemove);
        }
    }

    public List<ItemsManager> ReturnItemsList()
    {
        return itemsList;
    }
}
