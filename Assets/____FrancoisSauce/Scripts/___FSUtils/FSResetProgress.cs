#if UNITY_EDITOR
using FrancoisSauce.Scripts.FSUtils.FSGlobalVariables;
using UnityEngine;
using UnityEditor;

namespace FrancoisSauce.Scripts.FSUtils
{
    /// <summary>
    /// Reset all global variables in a given folder
    /// int => 0
    /// float => 0f
    /// bool => false
    /// </summary>
    public class FSResetProgress : EditorWindow
    {
        /// <summary>
        /// This function reset all the global variables from the Assets/Ressources/GlobalVariablesToReset folder
        /// Only reset <see cref="FSGlobalIntSO"/>, <see cref="FSGlobalBoolSO"/> and <see cref="FSGlobalFloatSO"/>
        /// </summary>
        [MenuItem("FrancoisSauce/Reset Progress")]
        public static void ResetGlobalVariables()
        {
            Debug.Log("putain");
            
            // Resetting INT
            var globalVariablesInt = Resources.LoadAll("GlobalVariablesToReset", typeof(FSGlobalIntSO));
                
            foreach (var o in globalVariablesInt)
            {
                if (!(o is FSGlobalIntSO)) continue;
                var tmp = (FSGlobalIntSO)o;
                tmp.value = 0;
            }
            
            //Resetting BOOL
            var globalVariablesBool = Resources.LoadAll("GlobalVariablesToReset", typeof(FSGlobalBoolSO));

            foreach (var o in globalVariablesInt)
            {
                if (!(o is FSGlobalBoolSO)) continue;
                var tmp = (FSGlobalBoolSO)o;
                tmp.value = false;
            }
            
            // Resetting FLOAT
            var globalVariablesFloat = Resources.LoadAll("GlobalVariablesToReset", typeof(FSGlobalFloatSO));

            foreach (var o in globalVariablesInt)
            {
                if (!(o is FSGlobalFloatSO)) continue;
                var tmp = (FSGlobalFloatSO)o;
                tmp.value = 0;
            }
        }
    }
}
#endif