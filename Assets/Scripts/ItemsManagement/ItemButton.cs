using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public ItemsManager itemOnButton;


    public void OnButtonClick()
    {
        if (MenuManager.Instance.menu.activeInHierarchy)
        {
            MenuManager.Instance.itemNameText.text = itemOnButton.itemName;
            MenuManager.Instance.itemDescriptionText.text = itemOnButton.itemDescription;

            MenuManager.Instance.activeItem = itemOnButton;
        }

        if (ShopManager.Instance.shopMenu.activeInHierarchy)
        {
            if (ShopManager.Instance.buyPanel.activeInHierarchy)
            {
                ShopManager.Instance.SelectedBuyItem(itemOnButton);
            }
            else if (ShopManager.Instance.sellPanel.activeInHierarchy)
            {
                ShopManager.Instance.SelectedSellItem(itemOnButton);
            }

        }

    }
}
