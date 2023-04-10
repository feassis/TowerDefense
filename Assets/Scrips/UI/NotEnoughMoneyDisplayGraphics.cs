using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class NotEnoughMoneyDisplayGraphics : MonoBehaviour
{
    [SerializeField] private GameObject notEnoughMoneyGameObject;
    [SerializeField] private float showMessageDuration = 1f;

    private void Awake()
    {
        ServiceLocator.RegisterService<NotEnoughMoneyDisplayGraphics>(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.DeregisterService<NotEnoughMoneyDisplayGraphics>();
    }

    private void Start()
    {
        ServiceLocator.GetService<TowerManager>().OnNotEnoughMoney += HandleNotEnoughMoney;
    }

    public void HandleNotEnoughMoney()
    {
        StartCoroutine(ShowNotEnoughMoney());
    }

    private IEnumerator ShowNotEnoughMoney()
    {
        notEnoughMoneyGameObject.SetActive(true);

        yield return new WaitForSeconds(showMessageDuration);

        notEnoughMoneyGameObject.SetActive(false);
    }
}
