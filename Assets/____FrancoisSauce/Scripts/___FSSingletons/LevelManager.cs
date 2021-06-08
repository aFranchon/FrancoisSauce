using FrancoisSauce.Scripts.FSUtils;
using FrancoisSauce.Scripts.FSUtils.FSGlobalVariables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FrancoisSauce.Scripts.FSSingleton
{
    /// <summary>
    /// Level manager, use it to load levels. Use the list from <see cref="SceneList"/> to know witch level load
    /// </summary>
    public class LevelManager : FrancoisSauce.Scripts.FSUtils.Singleton<LevelManager>
    {
        /// <summary>
        /// current level used to load good level at each time, without using playerPrefs
        /// </summary>
        [Tooltip("The FSGlobalVariable that handle the current level value")]
        [SerializeField] private FSGlobalIntSO currentLevel = null;
        /// <summary>
        /// Total level of not levels scenes
        /// </summary>
        [Tooltip("The FSGlobalVariable that handle the not Level Scene number")]
        [SerializeField] private FSGlobalIntSO notLevelsSceneNumber = null;

        /// <summary>
        /// private variable to keep track of witch level to unload next
        /// </summary>
        private int levelToUnload = -1;
        /// <summary>
        /// private variable to keep track of the number of non level scene
        /// </summary>
        private int totalLevelsNumber = -1;

        /// <summary>
        /// Awake method from <see cref="MonoBehaviour"/>
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(this);

            totalLevelsNumber = SceneManager.sceneCountInBuildSettings - notLevelsSceneNumber.value;
            currentLevel.value = PlayerPrefs.GetInt("CurrentLevel");
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            PlayerPrefs.SetInt("CurrentLevel", currentLevel.value);
        }

        /// <summary>
        /// call it when you want to load the next level of the game
        /// also unload actual level
        /// </summary>
        public void LoadNextLevel()
        {
            levelToUnload = currentLevel.value % totalLevelsNumber;
            UnloadLevel();

            currentLevel.value += 1;
            
            StartCoroutine(GameManager.Instance.LoadSceneAsync(
                SceneList.Instance.scenes["Level" + (currentLevel.value % totalLevelsNumber + 1).ToString("0000")], 
                true, false)
            );
        }

        /// <summary>
        /// call it when you want to load the current level
        /// </summary>
        /// <param name="doUnloadLevel">if false also unload the previous level of with the same name</param>
        public void ReloadActualLevel(bool doUnloadLevel = true)
        {
            levelToUnload = currentLevel.value % totalLevelsNumber;
            
            if (doUnloadLevel) UnloadLevel();
            
            StartCoroutine(GameManager.Instance.LoadSceneAsync(
                SceneList.Instance.scenes["Level" + (currentLevel.value % totalLevelsNumber + 1).ToString("0000")], 
                true, false)
            );
        }

        /// <summary>
        /// call when needed to unload the levelToUnloadLevel
        /// </summary>
        private void UnloadLevel()
        {
            if (levelToUnload == -1)
            {
                Debug.LogError("levelToUnload should not be equal to -1");
                return;
            }

            GameManager.Instance.UnloadAsync(
                SceneList.Instance.scenes["Level" + (currentLevel.value % totalLevelsNumber + 1).ToString("0000")]);
            levelToUnload = -1;
        }

        /// <summary>
        /// particular use to unload a scene when leaving <see cref="GameScene"/>
        /// </summary>
        public void UnloadCurrentLevel()
        {
            GameManager.Instance.UnloadAsync(SceneList.Instance.scenes["Level" + (currentLevel.value % totalLevelsNumber + 1).ToString("0000")]);
        }
    }
}