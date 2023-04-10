using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyWave> waves;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private List<Path> possiblePaths;
    [SerializeField] private List<Castle> possibleCastles;

    private bool isCastlesDead = false;

    private int finishedWaves;

    private static int waveCounter;

    public bool IsSpawnerActive() => finishedWaves < waves.Count;

    private void Start()
    {
        ServiceLocator.GetService<LevelManager>().RegisterSpawner(this);
        foreach (var castle in possibleCastles)
        {
            castle.OnCasleDeath += HandleCastleDeath;
        }

        StartCoroutine(StartSpawner());
    }

    private void OnDestroy()
    {
        foreach (var castle in possibleCastles)
        {
            castle.OnCasleDeath -= HandleCastleDeath;
        }

        waveCounter = 0;
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

    private IEnumerator StartSpawner()
    {
        foreach (var wave in waves)
        {
            waveCounter++;
            ServiceLocator.GetService<WaveDisplayGraphics>().ShowNewWave($"Wave {waveCounter} Started");
            StartCoroutine(StartWaveSpawn(wave.GetEnemySpawnList(), wave.timeBetweenEnemies));

            yield return new WaitForSeconds(wave.timeToNextWave);
        }
    }

    private IEnumerator StartWaveSpawn(List<EnemyController> enemies, float timeBeteenSpawns)
    {
        foreach (var enemy in enemies)
        {
            if (isCastlesDead)
            {
                yield break;
            }

            if (!ServiceLocator.GetService<LevelManager>().IsLevelActive())
            {
                yield break;
            }

            EnemyController enemyInstance = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
            var path = ListTools.GetRandomEntryFromList<Path>(possiblePaths);
            enemyInstance.Setup(path, path.GetMyCastle());

            yield return new WaitForSeconds(timeBeteenSpawns);
        }

        finishedWaves++;
    }
}

