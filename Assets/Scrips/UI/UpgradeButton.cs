using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine;
using Tools;

[Serializable]
public class UpgradeButton : MonoBehaviour
{
    public Button MyButton;
    public TextMeshProUGUI UpgradeText;
    private UpgradeType type;

    public void SetupButton(UpgradeType type, int upgradeCost)
    {
        this.type = type;
        UpgradeText.text = $"Upgrade\n{type}\n{upgradeCost}";
    }

    private void Awake()
    {
        MyButton.onClick.AddListener(UpdateButtonClicked);
    }

    private void UpdateButtonClicked()
    {
        var tower = ServiceLocator.GetService<TowerManager>().GetSelectedTower();

        var towerControler = tower.GetTowerUpgradeController();

        towerControler.TryUpgradeTower(type);

        var stages = towerControler.GetUpgradeStages();

        var stage = stages.Find(s => s.upgrade.Type == type);

        if(stage != null)
        {
            SetupButton(type, stage.GetUpgradeCost());

            if (stage.level >= stage.upgrade.maxLevel)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
