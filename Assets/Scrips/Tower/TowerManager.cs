using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask uiLayer;
    [SerializeField] private GameObject selectedTowerEffect;
    private Tower activeTower;

    private Tower indicator;
    private CapsuleCollider indicatorCapsule;
    private bool isPlacing;
    private Camera mainCamera;

    public event Action OnNotEnoughMoney;

    private Tower selectedTower;

    public Tower GetSelectedTower() => selectedTower;

    public void SelectTower(Tower desiredTower)
    {
        if (isPlacing)
        {
            return;
        }

        if(selectedTower != null)
        {
            UnselectTower();
        }

        selectedTowerEffect.transform.position = desiredTower.transform.position;
        selectedTowerEffect.SetActive(true);
        selectedTower = desiredTower;

        selectedTower.ShowTowerRange();

        ServiceLocator.GetService<UpgradePanelGraphics>().OpenUpgradePanel();
    }

    public void UnselectTower()
    {
        selectedTower.HideTowerRange();
        selectedTower = null;
        selectedTowerEffect.SetActive(false);
    }

    public void StartTowerPlacement(Tower towerToPlace)
    {
        if (!ServiceLocator.GetService<LevelManager>().IsLevelActive())
        {
            return;
        }

        if(indicator != null)
        {
            Destroy(indicator);
        }

        activeTower = towerToPlace;

        indicator = Instantiate(towerToPlace);
        indicator.enabled = false;

        var rangeModel = indicator.GetRangeModel();

        rangeModel.transform.localScale = new Vector3(indicator.GetRange(), 1, indicator.GetRange()); ;
        rangeModel.SetActive(true);
        indicatorCapsule = indicator.GetTowerModel().GetComponent<CapsuleCollider>();
        indicatorCapsule.enabled = false;

        isPlacing = true;
    }

    private Vector3 GetGridPosition()
    {
        Vector3 location = Vector3.zero;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            location = hit.point;
        }

        return location;
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        ServiceLocator.RegisterService<TowerManager>(this);
    }

    private void Update()
    {
        if (!isPlacing)
        {
            return;
        }

        if(indicator == null)
        {
            return;
        }

        Vector3 pos = GetGridPosition();

        indicator.transform.position = pos;

        if(Physics.CheckCapsule(pos, pos + new Vector3(0f, indicatorCapsule.height, 0f), 
            indicatorCapsule.radius, obstacleLayer))
        {
            indicator.gameObject.SetActive(false);
            return;
        }

        bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();

        if (isOverUI)
        {
            indicator.gameObject.SetActive(false);
            return;
        }

        indicator.gameObject.SetActive(true);

        if (Input.GetMouseButtonDown(0))
        {
            var moneyManager = ServiceLocator.GetService<MoneyManager>();
            if (!moneyManager.HasMoneyAmount(activeTower.GetTowerCost()))
            {
                OnNotEnoughMoney?.Invoke();
                return;
            }

            moneyManager.SpendMoney(activeTower.GetTowerCost());

            isPlacing = false;
            ServiceLocator.GetService<AudioManager>().PlaySFX(SFXEnum.TowerPlace);
            Instantiate(activeTower, indicator.transform.position, indicator.transform.rotation);
            Destroy(indicator.gameObject);
        }
    }

    public void SellSelectedTower()
    {
        var sellPrice = selectedTower.GetTowerSellPrice();
        ServiceLocator.GetService<MoneyManager>().GiveMoney(sellPrice);
        ServiceLocator.GetService<AudioManager>().PlaySFX(SFXEnum.TowerRemove);
        Destroy(selectedTower.gameObject);
        UnselectTower();
    }

    private void OnDestroy()
    {
        ServiceLocator.DeregisterService<TowerManager>();
    }
}
