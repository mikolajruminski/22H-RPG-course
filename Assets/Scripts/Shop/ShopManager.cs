using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    // Start is called before the first frame update
    public GameObject shopMenu, buyPanel, sellPanel;
    [SerializeField] private TextMeshProUGUI currentGoldAmount;
    public List<ItemsManager> itemsForSale;

    [SerializeField] private GameObject itemSlotContainer;
    [SerializeField] private Transform itemSlotBuyContainerParent;
    [SerializeField] private Transform itemSlotSellContainerParent;

    [SerializeField] private ItemsManager selectedItem;
    [SerializeField] private TextMeshProUGUI buyItemName, buyItemDescription, buyItemValue;
    [SerializeField] private TextMeshProUGUI sellItemName, sellItemDescription, sellItemValue;


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenShopMenu()
    {
        shopMenu.SetActive(true);
        GameManager.Instance.shopOpened = true;

        currentGoldAmount.text = "Gold: " + GameManager.Instance.currentGold.ToString();
        buyPanel.SetActive(true);
    }
    public void CloseShopMenu()
    {
        shopMenu.SetActive(false);
        GameManager.Instance.shopOpened = false;
    }

    public void OpenBuyPanel()
    {
        buyPanel.SetActive(true);
        sellPanel.SetActive(false);
        UpdateItemsInShop(itemSlotBuyContainerParent, itemsForSale);
    }

    public void OpenSellPanel()
    {
        sellPanel.SetActive(true);
        buyPanel.SetActive(false);

        UpdateItemsInShop(itemSlotSellContainerParent, Inventory.Instance.ReturnItemsList());
    }

    private void UpdateItemsInShop(Transform itemSlotContainerParent, List<ItemsManager> itemsToLookThrough)
    {
        foreach (Transform itemSlot in itemSlotContainerParent)
        {
            Destroy(itemSlot.gameObject);
        }

        foreach (ItemsManager item in itemsToLookThrough)
        {
            RectTransform itemSlot = Instantiate(itemSlotContainer, itemSlotContainerParent).GetComponent<RectTransform>();

            Image itemImage = itemSlot.Find("ItemsImage").GetComponent<Image>();
            itemImage.sprite = item.itemImage;

            TextMeshProUGUI amountText = itemSlot.Find("AmountText").GetComponent<TextMeshProUGUI>();

            if (item.amountOfStacks > 1)
            {
                amountText.text = "";
            }
            else
            {
                amountText.text = "";
            }

            itemSlot.GetComponent<ItemButton>().itemOnButton = item;
        }
    }

    public void SelectedBuyItem(ItemsManager itemToBuy)
    {
        selectedItem = itemToBuy;
        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.itemDescription;
        buyItemValue.text = "Value: " + selectedItem.valueCoins;
    }

    public void SelectedSellItem(ItemsManager itemToSell)
    {
        selectedItem = itemToSell;
        sellItemName.text = selectedItem.itemName;
        sellItemDescription.text = selectedItem.itemDescription;
        sellItemValue.text = "Value: " + (int)(selectedItem.valueCoins * 0.75f);
    }

    public void BuyItem()
    {
        if (GameManager.Instance.currentGold >= selectedItem.valueCoins)
        {
            GameManager.Instance.currentGold -= selectedItem.valueCoins;
            Inventory.Instance.AddItems(selectedItem);

            currentGoldAmount.text = "Gold: " + GameManager.Instance.currentGold;

            OpenBuyPanel();
        }
    }

    public void SellItem()
    {
        if (selectedItem)
        {
            GameManager.Instance.currentGold += (int)(selectedItem.valueCoins * 0.75f);

            Inventory.Instance.RemoveItems(selectedItem);

            currentGoldAmount.text = "Gold: " + GameManager.Instance.currentGold;

            selectedItem = null;

            OpenSellPanel();
        }
    }
}
