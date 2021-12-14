using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject StartUI;
    [SerializeField] private GameObject GameOverUI;

    public void ToggleStartUI(bool OnOff)
    {
        StartUI.SetActive(OnOff);
    }

    public void ToggleGameOverUI(bool OnOff)
    {
        GameOverUI.SetActive(OnOff);
    }
}
