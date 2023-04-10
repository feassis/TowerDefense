using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextLevelName; // Change here after levelProgression implementation

    private List<Castle> castles = new List<Castle>();

    private List<EnemyHealth> enemies = new List<EnemyHealth>();

    private List<EnemyWaveSpawner> spawners = new List<EnemyWaveSpawner>();

    private bool isLevelActive = true;
    private bool isLevelWin;

    public Action OnWin;
    public Action OnDefeat;

    public bool IsLevelActive() => isLevelActive;
    public string GetNextLevelName() => nextLevelName;

    private void Awake()
    {
        ServiceLocator.RegisterService<LevelManager>(this);
    }

    public void RegisterCastle(Castle castle)
    {
        castles.Add(castle);
        castle.OnCasleDeath += HandleCastleDeath;
    }

    private void HandleCastleDeath()
    {
        isLevelActive = false;
        isLevelWin = false;

        OnDefeat?.Invoke();
    }
    public void RegisterEnemy(EnemyHealth enemy)
    {
        enemies.Add(enemy);

        enemy.OnDeath += HandleEnemyDeath;
    }

    private void HandleEnemyDeath()
    {
        if(spawners.Count == 0)
        {
            return;
        }

        foreach (var spawner in spawners)
        {
            if (spawner.IsSpawnerActive())
            {
                return;
            }
        }

        if (isLevelActive && enemies.Count == 0)
        {
            isLevelActive = false;
            isLevelWin = true;

            OnWin?.Invoke();
        }
    }

    public void DeregisterEnemy(EnemyHealth enemy)
    {
        enemies.Remove(enemy);
    }

    public void RegisterSpawner(EnemyWaveSpawner spawner)
    {
        spawners.Add(spawner);
    }
}
