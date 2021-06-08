using System;
using FrancoisSauce.Scripts.FSEvents.Listeners;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace FrancoisSauce.Scripts.FSEvents.SO
{
    /// <summary>
    /// Scriptable object that contains an action that trigger all <see cref="IFSEventListener"/> that subscribe to it.
    /// </summary>
    [CreateAssetMenu(fileName = "FSOneArgumentEvents", menuName = "FSEvents/FSOneArgumentEvents", order = 1)]
    public class FSOneArgumentEventSO<T> : ScriptableObject
    {
        /// <value> The action that trigger all <see cref="IFSEventListener"/> that registered</value>
        public Action<T> fsOneArgumentAction;
        
        /// <summary>
        /// Function called by original raiser of the event.
        /// </summary>
        /// <param name="value"> the <see cref="float"/> value to be raised</param>
        public void Invoke(T value)
        {
            fsOneArgumentAction?.Invoke(value);
        }
    }
}