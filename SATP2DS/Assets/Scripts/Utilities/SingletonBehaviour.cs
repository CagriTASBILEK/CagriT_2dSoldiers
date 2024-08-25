using System;
using UnityEngine;

namespace Utilities
{
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static readonly Lazy<T> LazyInstance = new Lazy<T>(CreateSingleton);
        
        private static bool _shuttingDown = false;
 
        public static T Instance => LazyInstance.Value;

        private static T CreateSingleton()
        {
            if (_shuttingDown)
            {
                Debug.LogWarning($"[Singleton] Instance '{typeof(T).Name}' already destroyed. Returning null.");
                return null;
            }

            var instance = (T)FindObjectOfType(typeof(T));

            if (instance) return instance;
            
            var ownerObject = new GameObject($"{typeof(T).Name} (singleton)");
            instance = ownerObject.AddComponent<T>();
            DontDestroyOnLoad(ownerObject);
            return instance;
        }
 
 
        protected virtual void OnApplicationQuit()
        {
            _shuttingDown = true;
        }
 
 
        protected virtual void OnDestroy()
        {
            _shuttingDown = true;
        }
    }
}