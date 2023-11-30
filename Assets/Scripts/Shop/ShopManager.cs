using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    // Start is called before the first frame update
    public GameObject shopMenu, buyPanel, sellPanel;
    [SerializeField] private TextMeshProUGUI currentGoldAmount;

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
    }

    public void OpenSellPanel()
    {
        sellPanel.SetActive(true);
        buyPanel.SetActive(false);
    }
}
