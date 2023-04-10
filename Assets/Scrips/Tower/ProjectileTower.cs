using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTower : ShooterTower
{
    [SerializeField]private Tower tower;
    [SerializeField] private Projectile projectile;
    [SerializeField] private GameObject rotatingTower;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private GameObject shotEffect;

    private void Awake()
    {
        tower.EnemiesUpdated += UpdateTarget;
    }

    private void OnDestroy()
    {
        tower.EnemiesUpdated -= UpdateTarget;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        var enemies = tower.GetEnemiesInRange();

        if(target != null)
        {
            rotatingTower.transform.rotation = Quaternion.Slerp( rotatingTower.transform.rotation, 
                Quaternion.LookRotation(target.position - transform.position), 
                rotationSpeed * Time.deltaTime);
            rotatingTower.transform.rotation = 
                Quaternion.Euler(0f, rotatingTower.transform.rotation.eulerAngles.y, 0f);
        }

        if (enemies.Count == 0)
        {
            target = null;
            return;
        }

        if(timer <= 0 && target != null)
        {
            timer = 1 / GetFireRate();

            spawnPoint.LookAt(target.position);

            var projectileInstance = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
            projectileInstance.SetDamage(GetDamage());
            Instantiate(shotEffect, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
