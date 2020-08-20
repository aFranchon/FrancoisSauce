using UnityEngine;
using UnityEngine.Events;

namespace FrancoisSauce.Scripts.FSEvents.SO
{
    /// <summary>
    /// Implementation of <see cref="UnityEvent<int>"/> used in <see cref="FSIntEventSO"/>
    /// </summary>
    [System.Serializable]
    public class FSIntEvent : UnityEvent<int>
    {
    }
    
    /// <inheritdoc cref="FSOneArgumentEventSO{T}>"/>
    [CreateAssetMenu(fileName = "FSIntEvent", menuName = "FSEvents/FSIntEvent", order = 1)]
    public class FSIntEventSO : FSOneArgumentEventSO<int>
    {
    }
}