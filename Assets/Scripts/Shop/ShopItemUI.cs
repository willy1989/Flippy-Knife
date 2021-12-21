using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private ShopItem shopItem;

    [SerializeField] private RawImage selectHighlight;

    [SerializeField] private Transform selectHighlightPosition;

    [Header("UI elements")]

    [SerializeField] private Text itemPrice;
    [SerializeField] private Button buyButton;

    private void OnEnable()
    {
        DisplayShopItemInfo();
    }

    public void DisplayShopItemInfo()
    {
        itemPrice.text = shopItem.Price.ToString();

        if (shopItem.Unlocked == true)
        {
            itemPrice.text = "";
            buyButton.gameObject.SetActive(false);
        }   
    }

    public void BuyItem()
    {
        if (shopItem.Price <= ScoreManager.Instance.TotalMoney)
        {
            shopItem.Unlocked = true;
            itemPrice.text = "";
            ScoreManager.Instance.TotalMoney -= shopItem.Price;
            UIManager.Instance.UpdateTotalMoneyText();
        }
    }

    public void SelectItem()
    {
        GamePhaseManager.Instance.ChangeKnife(shopItem.Sword);
        GamePhaseManager.Instance.SpawnNewKnife();
        selectHighlight.rectTransform.position = selectHighlightPosition.GetComponent<RectTransform>().position;
    }
}
