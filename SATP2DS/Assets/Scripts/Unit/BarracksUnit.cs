using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class BarracksUnit : BaseUnit
{
    public List<Transform> SpawnPoints;
    public delegate void SoldierUnitSpawn();
    public event SoldierUnitSpawn OnUnitSpawned;

    private Object[] soldierDatas;
    private bool isSpawning;
    private float timer;
    private void Start()
    {
        isSpawning = true;
        OnUnitDestroyed += DestroyBarracks;
        OnUnitSpawned += ProduceSoldier;
        OnUnitSpawned?.Invoke();
    }

    public void ProduceSoldier()
    {
        soldierDatas = Resources.LoadAll("SoldierDatas/");
        
        SoldierSpawner(() =>
        {
            timer += Time.deltaTime;
            if (timer >= 5f)
            {
                timer = 0f;
                var randomSpawn = Random.Range(0, SpawnPoints.Count);
                var randomSoldier = Random.Range(0, soldierDatas.Length);
                var soldier = soldierDatas[randomSoldier] as UnitData;
                
                
                if (soldier != null && UnitPlacementManager.Instance.placementService.CanPlaceUnit((int)SpawnPoints[randomSpawn].position.x,(int)SpawnPoints[randomSpawn].position.y, soldier.size))
                {
                    CreateSoldier((int)SpawnPoints[randomSpawn].position.x,(int)SpawnPoints[randomSpawn].position.y,soldier);
                }
                else
                {
                    Debug.Log("Cannot place unit here.");
                }
            }
        }, () => isSpawning);
    }

    private void CreateSoldier(int x, int y, UnitData unitData)
    {
        for (int i = 0; i < unitData.size.x; i++)
        {
            for (int j = 0; j < unitData.size.y; j++)
            {
                UnitPlacementManager.Instance.gridManager._gridCells[x + i, y + j].Occupy();
            }
        }
        
        Vector3 unitPosition = UnitPlacementManager.Instance.gridManager.GetWorldPosition(x, y);
        GameObject unitObject = Instantiate(unitData.unitPrefab, new Vector2(x, y), Quaternion.identity);
        unitObject.transform.position = unitPosition;
        unitObject.GetComponent<BaseUnit>().OnUnitDestroyed += () => ReleaseUnit(x, y,unitData.size);
        unitObject.GetComponent<BaseUnit>().UnitAction();
    }
    
    
    private void SoldierSpawner(Action action, Func<bool> endCondition)
    {
        StartCoroutine(SoldierSpawEnumerator(action, () => endCondition()));
    }
    
    private IEnumerator SoldierSpawEnumerator(Action action, Func<bool> endCondition)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        if (!endCondition())
        {
            yield break;
        }
        while (endCondition())
        {
            action?.Invoke();
            yield return wait;
        }
    }
    
    private void ReleaseUnit(int x, int y, Vector2Int size)
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                UnitPlacementManager.Instance.gridManager._gridCells[x + i, y + j].Release();
            }
        }
    }
    
    private void DestroyBarracks()
    {
        isSpawning = false;
    }
    
    public override void UnitAction()
    {
    }
}
