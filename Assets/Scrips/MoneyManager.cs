using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private bool hasPassiveIncome = true;
    [SerializeField] private int passiveIncome = 1;
    [SerializeField] private float passiveIncomeRate = 1f;

    [SerializeField] private int initialMoney = 200;
    private int currentMoney;

    public int GetCurrentMoney() => currentMoney;

    public event Action<int> OnMoneyChange; 
    
    private void Awake()
    {
        ServiceLocator.RegisterService<MoneyManager>(this);
        currentMoney = initialMoney;
        if (hasPassiveIncome)
        {
            StartCoroutine(PassiveIncomeCorotine());
        }
    }

    private void OnDestroy()
    {
        ServiceLocator.DeregisterService<MoneyManager>();
    }

    private IEnumerator PassiveIncomeCorotine()
    {
        var wait = new WaitForSeconds(passiveIncomeRate);
        while (true)
        {
            yield return wait;

            GiveMoney(passiveIncome);
        }
    }

    public bool HasMoneyAmount(int amount)
    {
        return currentMoney >= amount;
    }

    public void GiveMoney(int amount)
    {
        currentMoney += amount;
        OnMoneyChange?.Invoke(currentMoney);
    }

    public void SpendMoney(int amount)
    {
        currentMoney -= amount;
        OnMoneyChange?.Invoke(currentMoney);
    }
}
