using UnityEngine;

namespace FrancoisSauce.Scripts.FSUtils.FSGlobalVariables
{
    /// <summary>
    /// <see cref="bool"/> implementation of the <see cref="FSGlobalVariableSO{T}"/>
    /// </summary>
    /// <inheritdoc/>
    [CreateAssetMenu(fileName = "FSGlobalBool", menuName = "FSGlobalVariables/FSGlobalBool", order = 1)]
    public class FSGlobalBoolSO : FSGlobalVariableSO<bool>
    {
    }
}