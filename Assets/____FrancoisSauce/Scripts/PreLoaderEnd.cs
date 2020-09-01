using System.Collections;
using FrancoisSauce.Scripts.FSSingleton;
using FrancoisSauce.Scripts.FSUtils;
using UnityEngine;
using UnityEngine.UI;

//TODO comment
namespace FrancoisSauce.Scripts
{
    public class PreLoaderEnd : MonoBehaviour
    {
        [SerializeField] private GameObject mainAudioListener = null;
        
        private IEnumerator Start()
        {
            yield return null;
            
            DontDestroyOnLoad(mainAudioListener);
            
            StartCoroutine(GameManager.Instance.LoadSceneAsync(SceneList.Instance.scenes["MainMenu"], true, false));
            
            yield return new WaitForSecondsRealtime(.75f);
                
            GameManager.Instance.UnloadAsync(SceneList.Instance.scenes["PreLoad"]);
        }
    }
}