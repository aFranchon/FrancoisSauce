using UnityEngine;

namespace FrancoisSauce.Scripts.FSUtils.FSGlobalVariables
{
    /// <summary>
    /// <see cref="string"/> implementation of the <see cref="FSGlobalVariableSO{T}"/>
    /// </summary>
    /// <inheritdoc/>
    [CreateAssetMenu(fileName = "FSGlobalString", menuName = "FSGlobalVariables/FSGlobalString", order = 1)]
    public class FSGlobalStringSO : FSGlobalVariableSO<string>
    {
    }
}