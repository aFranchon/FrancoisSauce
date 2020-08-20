using UnityEngine;
using UnityEngine.Events;

namespace FrancoisSauce.Scripts.FSEvents.Listeners
{
    /// <summary>
    /// Interface of all the listeners. A listener's goal is to subscribe to a FSEventSO and to raise an <see cref="UnityEvent"/> when the FSEventSO is raised
    /// </summary>
    /// <inheritdoc cref="MonoBehaviour"/>
    public abstract class IFSEventListener : MonoBehaviour
    {
        #region Listener Handler
        
        /// <summary>
        /// Function called to subscribe to FSEventSO
        /// </summary>
        protected abstract void AddListeners();
        /// <summary>
        /// Function called to unsubscribe to FSEventSO
        /// </summary>
        protected abstract void RemoveListeners();

        #endregion
        
        #region Unity Functions

        /// <summary>
        /// Unity Monobehaviour.Awake()
        /// </summary>
        protected abstract void Awake();
        /// <summary>
        /// Unity Monobehaviour.Awake()
        /// </summary>
        protected abstract void OnEnable();
        /// <summary>
        /// Unity Monobehaviour.Awake()
        /// </summary>
        protected abstract void OnDisable();
        /// <summary>
        /// Unity Monobehaviour.Awake()
        /// </summary>
        protected abstract void OnDestroy();

        #endregion
    }
}
