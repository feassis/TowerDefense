using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTower : MonoBehaviour
{
    [SerializeField] private Tower theTower;
    [SerializeField] private float speedModifier = 0.5f;
    [SerializeField] private Transform effectRing;

    private float upgradeModifier = 1;

    public float GetUpgradeModifier() => upgradeModifier;
    public void SetUpgradeModifier(float newModifier) => upgradeModifier = newModifier;

    private List<EnemyController> affectedEnemies = new List<EnemyController>();

    private void Start()
    {
        effectRing.localScale = new Vector3(theTower.GetRange(), 1f, theTower.GetRange());
    }

    private void Update()
    {
        foreach (var enemy in affectedEnemies)
        {
            enemy.SetSpeedModifier(1f);
        }

        affectedEnemies.Clear();

        foreach (EnemyController enemy in theTower.GetEnemiesInRange())
        {
            enemy.SetSpeedModifier(speedModifier * upgradeModifier);
            affectedEnemies.Add(enemy);
        }
    }
}
