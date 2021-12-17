using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop Instance;

    public ShopItem[] ShopItems;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(this);
        }
    }
}
