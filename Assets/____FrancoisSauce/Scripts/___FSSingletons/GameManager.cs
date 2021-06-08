using System.Collections;
using System.Collections.Generic;
using FrancoisSauce.Scripts.FSEvents.SO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace FrancoisSauce.Scripts.FSSingleton
{
    /// <summary>
    /// Game Manager's purpose is to handle scene loading/unloading. It raises events on every step of the process of loading/unloading scenes
    /// </summary>
    /// <inheritdoc/>
    public class GameManager : FrancoisSauce.Scripts.FSUtils.Singleton<GameManager>
    {
        /// <summary>
        /// reference to <see cref="FSIntEventSO"/> for loaded scene event occurs when scene is loaded at 90%
        /// </summary>
        [Header("Scene Events")]
        [Tooltip("Event raised when a scene as been loaded (scene is then still inactive at .9f loading progress)")]
        [SerializeField] private FSIntEventSO onSceneLoaded = null;
        /// <summary>
        /// reference to <see cref="FSIntEventSO"/> for changed scene event occurs when the scene become active
        /// </summary>
        [FormerlySerializedAs("onSceneChanged")]
        [Tooltip("Event raised when a scene has been changed to active")]
        [SerializeField] private FSIntEventSO onSceneBecameActive = null;
        /// <summary>
        /// reference to <see cref="FSIntFloatEventSO"/> for load scene progress event occurs during each loading frames
        /// </summary>
        [Tooltip("Event raised each frame (asynchronously) and send the progression percentage and the index of the loading scene")]
        [SerializeField] private FSIntFloatEventSO onLoadProgress = null;
        /// <summary>
        /// reference to <see cref="FSIntEventSO"/> for changed scene event occurs when a scene is completely unload
        /// </summary>
        [Tooltip("Event raised when a scene is completely unload")]
        [SerializeField] private FSIntEventSO onUnloadComplete = null;

        /// <summary>
        /// reference to <see cref="FSVoidEventSO"/> for frame update.
        /// The purpose of this is to replace Update() unity's method on each object of the game
        /// </summary>
        [Space] [Header("Management Events")] [SerializeField]
        [Tooltip("Event raised each frame (same as Update method)")]
        private FSVoidEventSO onUpdate = null;
        
        /// <summary>
        /// list of indexes of loading scenes, the key is the buildIndex of the <see cref="Scene"/>,
        /// the value is the index on <see cref="currentAsyncOperation"/> and <see cref="currentLoadingIndex"/>
        /// </summary>
        private readonly Dictionary<int, int> indexes = new Dictionary<int, int>();
        /// <summary>
        /// <see cref="List{T}"/> of <see cref="AsyncOperation"/> currently loading
        /// </summary>
        private readonly List<AsyncOperation> currentAsyncOperation = new List<AsyncOperation>();
        /// <summary>
        /// <see cref="List{T}"/> of the currently loading <see cref="Scene"/>'s indexes
        /// </summary>
        private readonly List<int> currentLoadingIndex = new List<int>();
        
        /// <summary>
        /// Awake method from <see cref="MonoBehaviour"/>
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            
            DontDestroyOnLoad(this);
        }
        
        /// <summary>
        /// Method to async load a new <see cref="Scene"/>. It's additive so the scene will be added without unloading previous ones
        /// </summary>
        /// <param name="index">The buildIndex of the <see cref="Scene"/> to load</param>
        /// <param name="directActivation">if true the scene will load instantly, without raising <see cref="onSceneLoaded"/></param>
        /// <param name="slowLoading">if false do not load game slowly</param>
        /// <returns>IEnumerator method (use StartCoroutine(LoadSceneAsync) to run the method</returns>
        public IEnumerator LoadSceneAsync(int index, bool directActivation = false, bool slowLoading = true)
        {
            var currentLoad = 0f;
            yield return null;
            
            //adding index to the current loading lists
            if (indexes.ContainsKey(index)) indexes[index] = currentLoadingIndex.Count;
            else indexes.Add(index, currentLoadingIndex.Count);
            currentLoadingIndex.Add(index);
            currentAsyncOperation.Add(SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive));

            //stop the scene from loading instantly
            currentAsyncOperation[indexes[index]].allowSceneActivation = false;
            
            while (!currentAsyncOperation[indexes[index]].isDone)
            {
                //raising progress event each frame
                yield return null;
                
                //slow loading
                if (slowLoading)
                {
                    if (currentLoad <= currentAsyncOperation[indexes[index]].progress) currentLoad += Time.deltaTime;
                    onLoadProgress.Invoke(index, currentLoad);
    
                    //do not end the loop while scene is not completely loaded
                    if (!(currentLoad >= .9f)) continue;
                }
                //normal loading
                else
                {
                    onLoadProgress.Invoke(index, currentAsyncOperation[indexes[index]].progress);

                    if (!(currentAsyncOperation[indexes[index]].progress >= .9f)) continue;
                }
                

                //direct activation of the scene
                if (directActivation) StartCoroutine(EndLoadingActivation(index, slowLoading));
                //normal behaviour
                else onSceneLoaded.Invoke(index);
                    
                yield break;
            }
        }

        /// <summary>
        /// Method to end the loading of a <see cref="Scene"/> and activate it
        /// </summary>
        /// <param name="index">index of the <see cref="Scene"/> to activate</param>
        /// <param name="slowLoading">if false do not load game slowly</param>
        /// <returns>IEnumerator method (use StartCoroutine(EndLoadingActivation) to run the method</returns>
        public IEnumerator EndLoadingActivation(int index, bool slowLoading = false)
        {
            if (!indexes.ContainsKey(index)) print("Merde => " + index);
            //allow activation of the scene to end the load
            currentAsyncOperation[indexes[index]].allowSceneActivation = true;
            
            var currentLoad = 0.9f;
            
            while (slowLoading ? currentLoad < 1f : currentAsyncOperation[indexes[index]].progress < 1f)
            {
                yield return null;
                if (slowLoading)
                {
                    if (currentLoad <= currentAsyncOperation[indexes[index]].progress) currentLoad += Time.deltaTime;
                    onLoadProgress.Invoke(index, currentLoad);
                }
                //normal loading
                else
                {
                    onLoadProgress.Invoke(index, currentAsyncOperation[indexes[index]].progress);
                }
                
            }

            onLoadProgress.Invoke(
                index,
                currentAsyncOperation[indexes[index]].progress
            );

            //letting time to the event to progress
            yield return null;
            
            onSceneBecameActive.Invoke(index);
            
            //scene is no more loading, removing her index
            /*currentAsyncOperation.Remove(currentAsyncOperation[indexes[index]]);
            currentLoadingIndex.Remove(currentLoadingIndex[indexes[index]]);
            indexes.Remove(index);*/
            
        }
        
        /// <summary>
        /// Asynchronous unloading of a <see cref="Scene"/>
        /// </summary>
        /// <param name="index">index of the <see cref="Scene"/> to unload</param>
        public void UnloadAsync(int index)
        {
            var tmp = SceneManager.UnloadSceneAsync(index);

            if (tmp == null) return;
            
            tmp.completed += operation => onUnloadComplete.Invoke(index);
        }

        /// <summary>
        /// <see cref="MonoBehaviour"/>'s Update method
        /// </summary>
        private void Update()
        {
            onUpdate.Invoke();
        }
    }
}

