using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyController> enemiesToSpawn;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float timeBetweenSpawns = 1f;
    [SerializeField] private int spawnLimit = 10;
    [SerializeField] private List<Path> possiblePaths;
    [SerializeField] private List<Castle> possibleCastles;

    private float spawnTimer = 0f;
    private int enemyCount = 0;

    private bool isCastlesDead;

    public bool IsSpawnerActive() => enemyCount < spawnLimit;

    private void Start()
    {
        //ServiceLocator.GetService<LevelManager>().RegisterSpawner(this);
        foreach (var castle in possibleCastles)
        {
            castle.OnCasleDeath += HandleCastleDeath;
        }
    }

    private void OnDestroy()
    {
        foreach (var castle in possibleCastles)
        {
            castle.OnCasleDeath -= HandleCastleDeath;
        }
    }

    private void HandleCastleDeath()
    {
        foreach (var castle in possibleCastles)
        {
            if (castle.IsDead())
            {
                isCastlesDead = true;
                return;
            }
        }
    }

    private void Update()
    {
        if (isCastlesDead)
        {
            return;
        }

        if(enemyCount >= spawnLimit)
        {
            return;
        }

        if (!ServiceLocator.GetService<LevelManager>().IsLevelActive())
        {
            return;
        }


        spawnTimer += Time.deltaTime;

        if(spawnTimer >= timeBetweenSpawns)
        {
            EnemyController enemy = Instantiate(GetRandomEnemy(), spawnPoint.position, spawnPoint.rotation);

            enemy.Setup(possiblePaths[0], possibleCastles[0]);
            spawnTimer = 0;
            enemyCount++;
        }
    }

    private EnemyController GetRandomEnemy()
    {
        return enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)];
    }
}
