using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// ObjectPool class manages the pooling of game objects to optimize performance.
    /// </summary>
    public class ObjectPool : SingletonBehaviour<ObjectPool>
    {
        [SerializeField] private PoolSettings[] _pool;
        private List<GameObject> poolList;

        [SerializeField, ReadOnly] private SerializableDictionary<GameObject, List<GameObject>> _poolDictionary;

        private void Awake()
        {
            foreach (var poolSettings in Instance._pool)
            {
                var poolObjects = new List<GameObject>();
                for (var i = 0; i < poolSettings.Size; i++)
                {
                    var go = Instantiate(poolSettings.Prefab, transform);
                    go.SetActive(false);
                    poolObjects.Add(go);
                }

                _poolDictionary.Add(poolSettings.Prefab, poolObjects);
            }
        }

        public void DeleteDictionary()
        {
            foreach (var obj in _poolDictionary)
            {
                if (obj.Key.activeInHierarchy)
                {
                    obj.Value.ForEach(val =>
                    {
                        if (val.activeInHierarchy)
                        {
                            Recycle(val);
                        }
                    });
                }
            }
        }

        // Coroutine to dispose of a game object after a specified time.
        public IEnumerator Dispose(GameObject go, float time)
        {
            yield return new WaitForSeconds(time);
            go.Recycle();
        }

        // Method to spawn a game object from the pool.
        public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            if (!_poolDictionary.ContainsKey(prefab))
            {
                _poolDictionary.Add(prefab, new List<GameObject>());
            }

            _poolDictionary[prefab].RemoveAll(o => !o);

            if (_poolDictionary[prefab].All(o => o.activeSelf) || _poolDictionary[prefab].Count == 0)
            {
                var go = Instantiate(prefab, position, rotation, parent);
                _poolDictionary[prefab].Add(go);
                return go;
            }
            else
            {
                var go = _poolDictionary[prefab].Find(o => !o.activeSelf);
                go.transform.SetParent(parent);
                go.transform.position = position;
                go.transform.rotation = rotation;
                go.SetActive(true);
                return go;
            }
        }

        // Method to recycle a game object back into the pool.
        public void Recycle(GameObject go)
        {
            if (go != null)
            {
                go.SetActive(false);
                go.transform.SetParent(transform);
            }
            else
            {
                Debug.LogError("<OBJECTPOOL> <RECYLE> Gameobject NULL");
            }

        }

        // Generic method to spawn a component from the pool.
        public T Spawn<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : Component
        {
            return Spawn(prefab.gameObject, position, rotation, parent).GetComponent<T>();
        }

        // Generic method to recycle a component back into the pool.
        public void Recycle<T>(T component) where T : Component
        {
            Recycle(component.gameObject);
        }

        [Serializable]
        public struct PoolSettings
        {
            public GameObject Prefab;
            public int Size;
        }
    }

    
    /// <summary>
    /// Extension methods for the ObjectPool class.
    /// </summary>
    public static class ObjectPoolExtensions
    {
        public static GameObject Spawn(this GameObject prefab)
        {
            return Spawn(prefab, prefab.transform.position, prefab.transform.rotation, null);
        }

        public static GameObject Spawn(this GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return Spawn(prefab, position, rotation, null);
        }

        public static GameObject Spawn(this GameObject prefab, Transform parent)
        {
            return Spawn(prefab, prefab.transform.position, prefab.transform.rotation, parent);
        }

        public static GameObject Spawn(this GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            return ObjectPool.Instance.Spawn(prefab, position, rotation, parent);
        }

        public static void Recycle(this GameObject gameObject)
        {
            ObjectPool.Instance.Recycle(gameObject);
        }

        public static T Spawn<T>(this T prefab) where T : Component
        {
            return Spawn(prefab, prefab.transform.position, prefab.transform.rotation, null);
        }

        public static T Spawn<T>(this T prefab, Vector3 position, Quaternion rotation) where T : Component
        {
            return Spawn(prefab, position, rotation, null);
        }

        public static T Spawn<T>(this T prefab, Transform parent) where T : Component
        {
            return Spawn(prefab, prefab.transform.position, prefab.transform.rotation, parent);
        }

        public static T Spawn<T>(this T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : Component
        {
            return ObjectPool.Instance.Spawn(prefab, position, rotation, parent);
        }

        public static void Recycle<T>(this T component) where T : Component
        {
            ObjectPool.Instance.Recycle(component);
        }
    }
}
