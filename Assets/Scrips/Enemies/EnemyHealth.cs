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
        ServiceLocator.GetService<AudioManager>().PlaySFX(SFXEnum.EnemySpawn);

        ServiceLocator.GetService<LevelManager>().RegisterEnemy(this);
    }


    public void TakeDamage(float dmg)
    {
        currentHp = Mathf.Clamp(currentHp - dmg, 0, maxHp);

        ServiceLocator.GetService<AudioManager>().PlaySFX(SFXEnum.EnemyImpact);

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
        ServiceLocator.GetService<AudioManager>().PlaySFX(SFXEnum.EnemyDeath);
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
