using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;

[Serializable]
public class EnemyWave
{
    public List<EnemyWaveEntry> waveEnemies;
    public float timeBetweenEnemies;
    public float timeToNextWave;
    [SerializeField] private WaveSpawnMode spawnMode;

    private enum WaveSpawnMode
    {
        Random = 0,
        InOrder = 1
    }

    public List<EnemyController> GetEnemySpawnList()
    {
        return spawnMode switch
        {
            WaveSpawnMode.InOrder => GenerateEnemyListSetOrder(),
            WaveSpawnMode.Random => GenerateEnemyListRandomOrder(),
            _ => throw new NotImplementedException()
        };
    }

    private List<EnemyController> GenerateEnemyListRandomOrder()
    {
        List<EnemyController> enemyList = GenerateEnemyListSetOrder();

        return ListTools.RandomizeList<EnemyController>(enemyList);
    }

    private List<EnemyController> GenerateEnemyListSetOrder()
    {
        List<EnemyController> enemies = new List<EnemyController>();

        foreach (EnemyWaveEntry enemy in waveEnemies)
        {
            for (int i = 0; i < enemy.Amount; i++)
            {
                enemies.Add(enemy.Enemy);
            }
        }

        return enemies;
    }
}

