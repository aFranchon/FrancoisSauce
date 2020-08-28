using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FrancoisSauce.Scripts.FSEvents.SO
{
    [CreateAssetMenu(fileName = "FSThreeArgumentEvents", menuName = "FSEvents/FSThreeArgumentEvents", order = 1)]
    public class FSThreeArgumentEventSO<T0, T1, T2> : ScriptableObject
    {
        public Action<T0, T1, T2> fsThreeArgumentAction;
    }
}