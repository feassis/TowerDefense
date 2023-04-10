using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTower : ShooterTower
{
    [SerializeField] private BombProjectile bombPrefab; 

    private void Update()
    {
        timer -= Time.deltaTime;

        if(myTower.GetEnemiesInRange().Count == 0)
        {
            return;
        }

        if(timer > 0)
        {
            return;
        }

        if(target == null)
        {
            return;
        }

        timer = 1 / GetFireRate();

        BombProjectile newBomb = Instantiate(bombPrefab, 
            spawnPoint.position, Quaternion.identity);
        newBomb.SetDamage(GetDamage());
        newBomb.SetTarget(target);
    }

    
}

public class ShooterTower : MonoBehaviour
{
    [SerializeField] protected Tower myTower;
    [SerializeField] private float fireRate;
    [SerializeField] protected Transform spawnPoint;
    [SerializeField] private float towerDamage = 10;

    protected Transform target;

    private float fireRateModifier = 1;
    private float damageMultiplier = 1f;

    public void ImproveFireRateMultiplier(float multiplier) => fireRateModifier *= multiplier;
    public void ImproveDamageMultiplier(float multiplier) => damageMultiplier *= multiplier;

    public float GetFireRate() => fireRate * fireRateModifier;
    public float GetDamage() => towerDamage * damageMultiplier;

    protected float timer;

    private void Awake()
    {
        timer = 1 / fireRate;
        myTower.EnemiesUpdated += UpdateTarget;
    }

    private void OnDestroy()
    {
        myTower.EnemiesUpdated -= UpdateTarget;
    }

    protected void UpdateTarget()
    {
        float minDistance = myTower.GetRange() + 1f;

        var enemies = myTower.GetEnemiesInRange();

        foreach (var enemy in enemies)
        {
            if (enemy == null)
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                target = enemy.GetAimPoint().transform;
            }
        }
    }
}
