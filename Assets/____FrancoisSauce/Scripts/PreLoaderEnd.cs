using System.Collections;
using FrancoisSauce.Scripts.FSSingleton;
using FrancoisSauce.Scripts.FSUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FrancoisSauce.Scripts
{
    /// <summary>
    /// This object is in the Preloader <see cref="Scene"/>. It allows the game to start correctly and then unload itself
    /// </summary>
    public class PreLoaderEnd : MonoBehaviour
    {
        /// <summary>
        /// the main <see cref="AudioListener"/> of the game
        /// </summary>
        [Tooltip("the main AudioListener of the game")]
        [SerializeField] private GameObject mainAudioListener = null;
        
        /// <summary>
        /// The <see cref="MonoBehaviour"/> start function
        /// </summary>
        /// <returns>This is a <see cref="Coroutine"/></returns>
        private IEnumerator Start()
        {
            yield return null;
            
            DontDestroyOnLoad(mainAudioListener);
            
            StartCoroutine(GameManager.Instance.LoadSceneAsync(SceneList.Instance.scenes["MainMenu"], true, false));
            
            yield return new WaitForSecondsRealtime(2f);
                
            GameManager.Instance.UnloadAsync(SceneList.Instance.scenes["PreLoad"]);
        }
    }
}