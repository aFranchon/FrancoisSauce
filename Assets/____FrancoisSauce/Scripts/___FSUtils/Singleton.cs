using UnityEngine;

namespace FrancoisSauce.Scripts.FSUtils
{
    /// <summary>
    /// Singleton design pattern very simple implementation
    /// </summary>
    /// <typeparam name="T">Template to fit every possible type</typeparam>
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        /// <summary>
        /// The current instance of the singleton
        /// </summary>
        private static T instance;

        /// <summary>
        /// getter to the instance of the singleton
        /// </summary>
        public static T Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Call this method to know if the singleton is Initialized or not
        /// </summary>
        public static bool IsInitialized
        {
            get { return instance != null; }
        }

        /// <summary>
        /// <see cref="MonoBehaviour"/>'s Awake method. Singleton logic.
        /// </summary>
        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                Debug.LogError("[Singletont] Multiple Instance of singleton of type: " + typeof(T));
                return;
            }

            //casting this to fit the type of instance
            instance = (T) this;
        }

        /// <summary>
        /// <see cref="MonoBehaviour"/>'s OnDestroy
        /// </summary>
        protected virtual void OnDestroy()
        {
            instance = null;
        }
    }
}