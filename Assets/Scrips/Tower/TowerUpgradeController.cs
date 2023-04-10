using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class TowerUpgradeController : MonoBehaviour
{
    [SerializeField] private List<UpgradeChoice> upgradeOptions;

    private List<UpgradeStage> upgradeStages = new List<UpgradeStage>();

    private void Awake()
    {
        foreach (var option in upgradeOptions)
        {
            upgradeStages.Add(new UpgradeStage(option, 0));
        }
    }

    public List<UpgradeStage> GetUpgradeStages() => upgradeStages;

    public bool TryUpgradeTower(UpgradeType type)
    {
        var upgradeOption = upgradeOptions.Find(u => u.Type == type);

        if (upgradeOptions == null)
        {
            return false;
        }

        var upgradeStage = upgradeStages.Find(s => s.upgrade == upgradeOption);

        var moneyManager = ServiceLocator.GetService<MoneyManager>();

        var currentMoney = moneyManager.GetCurrentMoney();

        var currentUpdateCost = upgradeStage.GetUpgradeCost();

        if(currentMoney < currentUpdateCost)
        {
            ServiceLocator.GetService<NotEnoughMoneyDisplayGraphics>().HandleNotEnoughMoney();
            return false;
        }

        if(upgradeStage.level >= upgradeOption.maxLevel)
        {
            return false;
        }

        return type switch
        {
            UpgradeType.Damage => TryIncreaseDamage(upgradeOption, upgradeStage),
            UpgradeType.FireRate => TryIncreaseFireRate(upgradeOption, upgradeStage),
            UpgradeType.Range => TryIncreaseRange(upgradeOption, upgradeStage),
            UpgradeType.SlowEnemy => TryToIncreseSlowModifier(upgradeOption, upgradeStage),
            _ => throw new NotImplementedException()
        };
    }

    private void UpgradeLevel(UpgradeStage upgradeStage)
    {
        var moneyManager = ServiceLocator.GetService<MoneyManager>();
        moneyManager.SpendMoney(upgradeStage.GetUpgradeCost());
        upgradeStage.level++;
    }

    private bool TryToIncreseSlowModifier(UpgradeChoice upgradeOption, UpgradeStage upgradeStage)
    {
        if(gameObject.TryGetComponent<SlowTower>(out SlowTower tower))
        {
            tower.SetUpgradeModifier(tower.GetUpgradeModifier() - upgradeOption.modifier);
            UpgradeLevel(upgradeStage);
            return true;
        }

        return false;
    }

    private bool TryIncreaseRange(UpgradeChoice upgradeOption, UpgradeStage upgradeStage)
    {
        if (gameObject.TryGetComponent<Tower>(out Tower tower))
        {
            tower.SetUpgradeRangeIncrease(tower.GetUpgradeRangeIncrease() + upgradeOption.modifier);
            UpgradeLevel(upgradeStage);
            tower.ShowTowerRange();
            return true;
        }

        return false;
    }

    private bool TryIncreaseFireRate(UpgradeChoice upgradeOption, UpgradeStage upgradeStage)
    {
        if (gameObject.TryGetComponent<ShooterTower>(out ShooterTower tower))
        {
            tower.ImproveFireRateMultiplier(upgradeOption.modifier);
            UpgradeLevel(upgradeStage);
            return true;
        }

        return false;
    }

    private bool TryIncreaseDamage(UpgradeChoice upgradeOption, UpgradeStage upgradeStage)
    {
        if (gameObject.TryGetComponent<ShooterTower>(out ShooterTower tower))
        {
            tower.ImproveDamageMultiplier(upgradeOption.modifier);
            UpgradeLevel(upgradeStage);
            return true;
        }

        return false;
    }
}

public class UpgradeStage
{
    public UpgradeChoice upgrade;
    public int level;

    public UpgradeStage(UpgradeChoice upgrade, int level)
    {
        this.upgrade = upgrade;
        this.level = level;
    }

    public int GetUpgradeCost() => upgrade.baseCost + level * upgrade.incrementalCostPerLevel;
}

[Serializable]
public class UpgradeChoice
{
    public UpgradeType Type;
    public int maxLevel;
    public int baseCost;
    public int incrementalCostPerLevel;
    public float modifier;
}

public enum UpgradeType
{
    Range = 0,
    Damage = 1,
    SlowEnemy = 2,
    FireRate = 3
}
