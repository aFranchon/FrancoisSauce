using System.Collections;
using FrancoisSauce.Scripts.FSSingleton;
using FrancoisSauce.Scripts.FSUtils;
using UnityEngine;

namespace FrancoisSauce.Scripts
{
    public class PreLoaderEnd : MonoBehaviour
    {
        private IEnumerator Start()
        {
            StartCoroutine(GameManager.Instance.LoadSceneAsync(SceneList.Instance.scenes["MainMenu"], true, false));

            yield return new WaitForSeconds(.5f);
                
            GameManager.Instance.UnloadAsync(SceneList.Instance.scenes["PreLoad"]);
        }
    }
}