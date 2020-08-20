using UnityEngine;
using UnityEngine.Events;

namespace FrancoisSauce.Scripts.FSEvents.SO
{
    /// <summary>
    /// Implementation of <see cref="UnityEvent<float>"/> used in <see cref="FSFloatEventSO"/>
    /// </summary>
    [System.Serializable]
    public class FSFloatEvent : UnityEvent<float>    
    {
    }
    
    /// <inheritdoc cref="FSOneArgumentEventSO{T}"/>
    [CreateAssetMenu(fileName = "FSFloatEvent", menuName = "FSEvents/FSFloatEvent", order = 1)]
    public class FSFloatEventSO : FSOneArgumentEventSO<float>
    {
    }
}