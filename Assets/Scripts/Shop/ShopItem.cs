using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop item", menuName = "ScriptableObjects/shop item", order = 1)]
public class ShopItem : ScriptableObject
{
    public GameObject Sword;

    public string Name;

    public int Price;

    public bool Unlocked;
}
