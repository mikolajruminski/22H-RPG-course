using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    private bool canOpenShop;

    [SerializeField] private List<ItemsManager> itemsForSale;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canOpenShop && Input.GetKeyDown(KeyCode.E) && !Player.Instance.deactivatedMovement && !ShopManager.Instance.shopMenu.activeInHierarchy)
        {
            ShopManager.Instance.itemsForSale = itemsForSale;
            ShopManager.Instance.OpenShopMenu();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            canOpenShop = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            canOpenShop = false;
        }
    }
}
