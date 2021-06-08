using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace FrancoisSauce.Scripts.FSEvents.SO
{
    [CreateAssetMenu(fileName = "FSTwoArgumentEvents", menuName = "FSEvents/FSTwoArgumentEvents", order = 1)]
    public class FSTwoArgumentEventSO<T0, T1> : ScriptableObject
    {
        public Action<T0, T1> fsTwoArgumentAction;
        
        /// <summary>
        /// Function called by original raiser of the event.
        /// </summary>
        /// <param name="valueOne"> the firs value to be raised</param>
        /// <param name="valueTwo"> the second value to be raised</param>
        public void Invoke(T0 valueOne, T1 valueTwo)
        {
            fsTwoArgumentAction?.Invoke(valueOne, valueTwo);
        }
    }
}