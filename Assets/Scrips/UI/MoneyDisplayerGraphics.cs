using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Tools;

public class MoneyDisplayerGraphics : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyAmountText;

    private void Start()
    {
        var moneyManager = ServiceLocator.GetService<MoneyManager>();
        UpdateMoneyAmount(moneyManager.GetCurrentMoney());
        moneyManager.OnMoneyChange += UpdateMoneyAmount;
    }

    private void OnDestroy()
    {
        var moneyManager = ServiceLocator.GetService<MoneyManager>();

        if(moneyManager == null)
        {
            return;
        }

        moneyManager.OnMoneyChange -= UpdateMoneyAmount;
    }

    private void UpdateMoneyAmount(int amount)
    {
        moneyAmountText.text = amount.ToString();
    }
}
