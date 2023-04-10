using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePanelGraphics : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    [SerializeField] private Button closeButton, removeButton;
    [SerializeField] private TextMeshProUGUI removeButtonText;
    [SerializeField] private UpgradeButton[] upgradeButtons;
    private void Awake()
    {
        ServiceLocator.RegisterService<UpgradePanelGraphics>(this);
        closeButton.onClick.AddListener(CloseUpgradePanel);
        removeButton.onClick.AddListener(RemoveTower);
    }

    private void OnDestroy()
    {
        ServiceLocator.DeregisterService<UpgradePanelGraphics>();
    }

    private void RemoveTower()
    {
        ServiceLocator.GetService<TowerManager>().SellSelectedTower();
        CloseUpgradePanel();
    }

    public void OpenUpgradePanel()
    {
        holder.SetActive(true);
        Tower tower = ServiceLocator.GetService<TowerManager>().GetSelectedTower();

        var upgradeStage = tower.GetTowerUpgradeController().GetUpgradeStages();

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            if(i > upgradeStage.Count - 1)
            {
                upgradeButtons[i].MyButton.gameObject.SetActive(false);
                continue;
            }

            upgradeButtons[i].SetupButton(upgradeStage[i].upgrade.Type,
                upgradeStage[i].GetUpgradeCost());

            if(upgradeStage[i].level >= upgradeStage[i].upgrade.maxLevel)
            {
                upgradeButtons[i].MyButton.gameObject.SetActive(false);
            }
        }

        removeButtonText.text = $"Remove (Gain {tower.GetTowerSellPrice()})";
    }

    public void CloseUpgradePanel()
    {
        ServiceLocator.GetService<TowerManager>().UnselectTower();
        holder.SetActive(false);
    }
}
