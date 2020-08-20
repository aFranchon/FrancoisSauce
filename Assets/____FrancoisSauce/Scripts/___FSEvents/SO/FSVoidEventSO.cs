using System;
using UnityEngine;

namespace FrancoisSauce.Scripts.FSEvents.SO
{
    /// <summary>
    /// Scriptable object that contains an action that trigger all <see cref="IFSEventListener"/> that subscribe to it.
    /// </summary>
    [CreateAssetMenu(fileName = "FSVoidEvent", menuName = "FSEvents/FSVoidEvents", order = 1)]
    public class FSVoidEventSO : ScriptableObject
    {
        /// <value> The action that trigger all <see cref="IFSEventListener"/> that registered</value>
        public Action fsVoidEvent;

        #region Listener Handler

        /// <summary>
        /// Function called by original raiser of the event.
        /// </summary>
        public void Invoke()
        {
            fsVoidEvent?.Invoke();
        }

        #endregion
    }
}