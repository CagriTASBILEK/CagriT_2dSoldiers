using System;
using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// SingletonBehaviour class provides a base class for creating singleton MonoBehaviour instances.
    /// </summary>
    /// <typeparam name="T">The type of the singleton class.</typeparam>
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static readonly Lazy<T> LazyInstance = new Lazy<T>(CreateSingleton);
        private static bool _shuttingDown = false;
        public static T Instance => LazyInstance.Value;

        /// <summary>
        /// Creates the singleton instance.
        /// </summary>
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

        /// <summary>
        /// Called when the application is quitting.
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            _shuttingDown = true;
        }

        /// <summary>
        /// Called when the object is being destroyed.
        /// </summary>
        protected virtual void OnDestroy()
        {
            _shuttingDown = true;
        }
    }
}