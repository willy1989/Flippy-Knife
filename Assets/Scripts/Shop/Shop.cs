using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : Singleton<Shop>
{
    public ShopItem[] ShopItems;

    private void Awake()
    {
        SetInstance();
    }
}
