using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlaceTowerButton : MonoBehaviour
{
    [SerializeField] private Tower towerToPlace;
    [SerializeField] private Button placeTowerButton;
    [SerializeField] private TextMeshProUGUI priceText;

    private void Awake()
    {
        placeTowerButton.onClick.AddListener(OnPlaceTowerButtonClicked);
        priceText.text = towerToPlace.GetTowerCost().ToString();
    }

    private void OnPlaceTowerButtonClicked()
    {
        SelectTower();
    }

    private void SelectTower()
    {
        ServiceLocator.GetService<TowerManager>().StartTowerPlacement(towerToPlace);
    }
}
