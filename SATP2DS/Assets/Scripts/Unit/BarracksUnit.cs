using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Scriptables;
using UnityEngine;
using Utilities;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Unit
{
    /// <summary>
    /// BarracksUnit class represents a barracks unit that can spawn soldiers.
    /// </summary>
    public class BarracksUnit : BaseUnit
    {
        /// <summary>
        /// List of spawn points for soldiers.
        /// </summary>
        public List<Transform> SpawnPoints;

        /// <summary>
        /// Delegate for soldier unit spawn events.
        /// </summary>
        public delegate void SoldierUnitSpawn();

        /// <summary>
        /// Event triggered when a soldier unit is spawned.
        /// </summary>
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

        /// <summary>
        /// Produces a soldier at a random spawn point.
        /// </summary>
        public void ProduceSoldier()
        {
            soldierDatas = Resources.LoadAll<UnitData>("SoldierDatas/");
        
            SoldierSpawner(() =>
            {
                timer += Time.deltaTime;
                if (timer >= 5f)
                {
                    timer = 0f;
                    var randomSpawn = Random.Range(0, SpawnPoints.Count);
                    var randomSoldier = Random.Range(0, soldierDatas.Length);
                    var soldier = soldierDatas[randomSoldier] as UnitData;
                
                    if (soldier != null && UnitPlacementManager.Instance.placementService.CanPlaceUnit((int)SpawnPoints[randomSpawn].position.x, (int)SpawnPoints[randomSpawn].position.y, soldier.size))
                    {
                        CreateSoldier((int)SpawnPoints[randomSpawn].position.x, (int)SpawnPoints[randomSpawn].position.y, soldier);
                    }
                    else
                    {
                        Debug.Log("Cannot place unit here.");
                    }
                }
            }, () => isSpawning);
        }

        /// <summary>
        /// Creates a soldier at the specified grid position.
        /// </summary>
        /// <param name="x">The x-coordinate of the position.</param>
        /// <param name="y">The y-coordinate of the position.</param>
        /// <param name="unitData">The data of the unit to be created.</param>
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
            GameObject unitObject = unitData.unitPrefab.Spawn(new Vector2(x, y), Quaternion.identity);
            unitObject.transform.position = unitPosition;
            unitObject.GetComponent<BaseUnit>().OnUnitDestroyed += () => ReleaseUnit(x, y, unitData.size);
            unitObject.GetComponent<BaseUnit>().UnitAction();
        
        }

        /// <summary>
        /// Spawns soldiers at regular intervals.
        /// </summary>
        /// <param name="action">The action to perform each interval.</param>
        /// <param name="endCondition">The condition to end the spawning.</param>
        private void SoldierSpawner(Action action, Func<bool> endCondition)
        {
            StartCoroutine(SoldierSpawEnumerator(action, () => endCondition()));
        }

        /// <summary>
        /// Enumerator for soldier spawning.
        /// </summary>
        /// <param name="action">The action to perform each interval.</param>
        /// <param name="endCondition">The condition to end the spawning.</param>
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

        /// <summary>
        /// Releases the grid cells occupied by the unit.
        /// </summary>
        /// <param name="x">The x-coordinate of the position.</param>
        /// <param name="y">The y-coordinate of the position.</param>
        /// <param name="size">The size of the unit.</param>
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

        /// <summary>
        /// Stops the spawning of soldiers when the barracks is destroyed.
        /// </summary>
        private void DestroyBarracks()
        {
            isSpawning = false;
        }

        public override void UnitAction()
        {
        }

        public override bool CanBeAttackedBy(BaseUnit attacker)
        {
            return false;
        }
    }
}