using UnityEngine;

namespace FrancoisSauce.Scripts.FSUtils.FSGlobalVariables
{
    /// <summary>
    /// Abstract class to implement for creating GlobalVariable.
    /// This is a <see cref="ScriptableObject"/> witch allow to changes the variable's value while playing the game and save it
    /// </summary>
    /// <typeparam name="T">Template to fit every possible type of variable</typeparam>
    [CreateAssetMenu(fileName = "FSGlobalVariable", menuName = "FSGlobalVariables/FSGlobalVariable", order = 1)]
    public abstract class FSGlobalVariableSO<T> : ScriptableObject
    {
        /// <summary>
        /// the global variable's value
        /// </summary>
        [Tooltip("Value of the type of the globalVariable")]
        public T value;
    }
}