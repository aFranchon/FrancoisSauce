using UnityEngine;
using UnityEngine.Events;

namespace FrancoisSauce.Scripts.FSEvents.SO
{
    /// <summary>
    /// Implementation of <see cref="UnityEvent<int>"/> used in <see cref="FSIntEventSO"/>
    /// </summary>
    [System.Serializable]
    public class FSIntIntEvent : UnityEvent<int, int>
    {
    }
    
    /// <inheritdoc cref="FSOneArgumentEventSO{T}>"/>
    [CreateAssetMenu(fileName = "FSIntIntEvent", menuName = "FSEvents/FSIntIntEvent", order = 1)]
    public class FSIntIntEventSO : FSTwoArgumentEventSO<int, int>
    {
    }
}