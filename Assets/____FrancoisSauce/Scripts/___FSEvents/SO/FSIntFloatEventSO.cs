using UnityEngine;
using UnityEngine.Events;

namespace FrancoisSauce.Scripts.FSEvents.SO
{
    /// <summary>
    /// Implementation of <see cref="UnityEvent<int, float>"/> used in <see cref="FSIntEventSO"/>
    /// </summary>
    [System.Serializable]
    public class FSIntFloatEvent : UnityEvent<int, float>
    {
    }
    
    /// <inheritdoc cref="FSTwoArgumentEventSO{T0, T1}>"/>
    [CreateAssetMenu(fileName = "FSIntFloatEvent", menuName = "FSEvents/FSIntFloatEvent", order = 1)]
    public class FSIntFloatEventSO : FSTwoArgumentEventSO<int, float>
    {
    }
}