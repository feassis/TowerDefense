using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float range = 3f;
    [SerializeField] private LayerMask enemylayer;
    [SerializeField] private GameObject towerModel;
    [SerializeField] private GameObject rangeModel;
    [SerializeField] private int towerCost = 100;
    [SerializeField] private TowerUpgradeController towerUpgradeController;

    private float upgradeRangeIncrease = 0f;

    public float GetUpgradeRangeIncrease() => upgradeRangeIncrease;
    public void SetUpgradeRangeIncrease(float newRenageIncrease) 
        => upgradeRangeIncrease = newRenageIncrease;

    public int GetTowerCost() => towerCost;
    public int GetTowerSellPrice() => towerCost / 2;

    public GameObject GetTowerModel() => towerModel;
    public GameObject GetRangeModel() => rangeModel;

    public TowerUpgradeController GetTowerUpgradeController() => towerUpgradeController;

    [SerializeField]private List<EnemyController> enemies = new List<EnemyController>();

    private WaitForSeconds wait0dot2Seconds = new WaitForSeconds(0.2f);

    public List<EnemyController> GetEnemiesInRange() => enemies;
    public float GetRange() => range + upgradeRangeIncrease;

    public Action EnemiesUpdated;

    private void Awake()
    {
        StartCoroutine(LookForEnemies());
    }

    private IEnumerator LookForEnemies()
    {
        while (true)
        {
            yield return wait0dot2Seconds;
            var enemiesColliders = Physics.OverlapSphere(transform.position, GetRange(), enemylayer);

            enemies.Clear();

            foreach (var collider in enemiesColliders)
            {
                if (collider.TryGetComponent<EnemyController>(out EnemyController enemyControler))
                {
                    enemies.Add(enemyControler);
                }
            }

            EnemiesUpdated?.Invoke();
        }
    }

    public void ShowTowerRange()
    {
        rangeModel.transform.localScale = new Vector3(GetRange(), 0f, GetRange());
        rangeModel.SetActive(true);
    }

    public void HideTowerRange()
    {
        rangeModel.SetActive(false);
    }

    private void OnMouseDown()
    {
        ServiceLocator.GetService<TowerManager>().SelectTower(this);
    }
}
