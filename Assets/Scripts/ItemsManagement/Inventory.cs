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
        Debug.Log(item.itemName + "has been added to the inventory");
        itemsList.Add(item);
        print(itemsList.Count);
    }

    public List<ItemsManager> ReturnItemsList()
    {
        return itemsList;
    }
}
