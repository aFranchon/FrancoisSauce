using UnityEngine.Events;

namespace FrancoisSauce.Scripts.FSEvents.SO
{
    /// <summary>
    /// Implementation of <see cref="UnityEvent<bool>"/> used in <see cref="FSBoolEventSO"/>
    /// </summary>
    [System.Serializable]
    public class FSBoolEvent : UnityEvent<bool>
    {
    }
    
    /// <inheritdoc cref="FSOneArgumentEventSO{T}"/>
    public class FSBoolEventSO : FSOneArgumentEventSO<bool>
    {
    }
}