using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private HealthBarGraphics health;
    [SerializeField] private int monetaryReward = 50;

    private float currentHp;

    public Action OnDeath;

    private void Start()
    {
        currentHp = maxHp;
        health.UpdateHealthBar(currentHp, maxHp);

        ServiceLocator.GetService<LevelManager>().RegisterEnemy(this);
    }


    public void TakeDamage(float dmg)
    {
        currentHp = Mathf.Clamp(currentHp - dmg, 0, maxHp);

        health.gameObject.SetActive(true);

        health.UpdateHealthBar(currentHp, maxHp);

        if(currentHp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        ServiceLocator.GetService<MoneyManager>().GiveMoney(monetaryReward);
        ServiceLocator.GetService<LevelManager>().DeregisterEnemy(this);
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
