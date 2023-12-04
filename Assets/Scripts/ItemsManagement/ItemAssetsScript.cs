using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssetsScript : MonoBehaviour
{
    public static ItemAssetsScript Instance { get; private set; }

    [SerializeField] ItemsManager[] itemsAvailable;
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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public ItemsManager GetItemAsset(string itemsToGet)
    {
        foreach (ItemsManager item in itemsAvailable)
        {
            if (item.itemName == itemsToGet)
            {
                return item;
            }
        }

        return null;
    }
}
