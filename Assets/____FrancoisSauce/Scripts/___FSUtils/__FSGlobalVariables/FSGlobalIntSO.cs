using UnityEngine;

namespace FrancoisSauce.Scripts.FSUtils.FSGlobalVariables
{
    /// <summary>
    /// <see cref="int"/> implementation of the <see cref="FSGlobalVariableSO{T}"/>
    /// </summary>
    /// <inheritdoc/>
    [CreateAssetMenu(fileName = "FSGlobalInt", menuName = "FSGlobalVariables/FSGlobalInt", order = 1)]
    public class FSGlobalIntSO : FSGlobalVariableSO<int>
    {
    }
}