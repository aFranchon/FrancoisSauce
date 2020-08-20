using UnityEngine.Events;

namespace FrancoisSauce.Scripts.FSEvents.Listeners
{
    /// <summary>
    /// Interface used for listeners to two arguments FSEventSO
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <inheritdoc/>
    public abstract class IFTwoArgumentEventListener<T0, T1> : IFSEventListener
    {
        #region Listener Handler

        
        
        /// <summary>
        /// Method called when FSEventSO raise this listener. It raise the <see cref="UnityEvent"/>
        /// </summary>
        /// <param name="valueOne">the second value to be pass to all subscribers</param>
        /// <param name="valueTwo">the first value to be pass to all subscribers</param>
        protected abstract void Invoke(T0 valueOne, T1 valueTwo);

        #endregion
    }
}