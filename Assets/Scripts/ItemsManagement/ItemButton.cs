using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public ItemsManager itemOnButton;


    public void OnButtonClick()
    {
        MenuManager.Instance.itemNameText.text = itemOnButton.itemName;
        MenuManager.Instance.itemDescriptionText.text = itemOnButton.itemDescription;

        MenuManager.Instance.activeItem = itemOnButton;
    }
}
