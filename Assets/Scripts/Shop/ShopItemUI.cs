using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private ShopItem shopItem;

    [Header("UI elements")]

    [SerializeField] private Text itemName;
    [SerializeField] private Text itemPrice;
    [SerializeField] private Button buyButton;

    private void Start()
    {
        DisplayShopItemInfo();
    }

    public void DisplayShopItemInfo()
    {
        itemName.text = shopItem.Name;
        itemPrice.text = shopItem.Price.ToString();

        if (shopItem.Unlocked == true)
            buyButton.gameObject.SetActive(false);
    }

    public void BuyItem()
    {
        if (shopItem.Price <= ScoreManager.Instance.TotalMoney)
        {
            shopItem.Unlocked = true;
            ScoreManager.Instance.TotalMoney -= shopItem.Price;
            UIManager.Instance.UpdateTotalMoneyText();
        }
    }

    public void SelectItem()
    {
        GamePhaseManager.Instance.KnifePrefab = shopItem.Sword;
    }
}
