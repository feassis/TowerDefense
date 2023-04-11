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
