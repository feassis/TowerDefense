using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private HealthBarGraphics healthBar;
    [SerializeField] private Transform[] attackPoints;
    private float currentHealth;

    public Action<float, float> OnDamageTaken;
    public Action OnCasleDeath;

    public bool IsDead() => currentHealth <= 0;

    private void Awake()
    {
        currentHealth = maxHealth;
        OnDamageTaken += healthBar.UpdateHealthBar;
    }

    private void Start()
    {
        ServiceLocator.GetService<LevelManager>().RegisterCastle(this);
    }

    private void OnDestroy()
    {
        OnDamageTaken -= healthBar.UpdateHealthBar;
    }

    public Transform GetRandomAttackPoint() => attackPoints[UnityEngine.Random.Range(0, attackPoints.Length)];

    public void TakeDamage(float dmg)
    {
        currentHealth = Mathf.Clamp(currentHealth - dmg, 0, maxHealth);
        OnDamageTaken?.Invoke(currentHealth, maxHealth);

        var audioManager = ServiceLocator.GetService<AudioManager>();

        audioManager.PlaySFX(SFXEnum.CastleHit);

        if(currentHealth == 0)
        {
            audioManager.PlaySFX(SFXEnum.CastleDestroy);
            Death();
        }
    }

    private void Death()
    {
        gameObject.SetActive(false);
        OnCasleDeath?.Invoke();
    }
}
