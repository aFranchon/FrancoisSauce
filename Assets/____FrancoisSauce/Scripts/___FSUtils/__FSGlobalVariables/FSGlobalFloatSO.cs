using UnityEngine;

namespace FrancoisSauce.Scripts.FSUtils.FSGlobalVariables
{
    /// <summary>
    /// <see cref="float"/> implementation of the <see cref="FSGlobalVariableSO{T}"/>
    /// </summary>
    /// <inheritdoc/>
    [CreateAssetMenu(fileName = "FSGlobalFloat", menuName = "FSGlobalVariables/FSGlobalFloat", order = 1)]
    public class FSGlobalFloatSO : FSGlobalVariableSO<float>
    {
    }
}