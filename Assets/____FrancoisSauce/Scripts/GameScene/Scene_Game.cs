using DG.Tweening;
using FrancoisSauce.Scripts.FSEvents.SO;
using FrancoisSauce.Scripts.FSSingleton;
using FrancoisSauce.Scripts.FSScenes;
using FrancoisSauce.Scripts.FSUtils;
using FrancoisSauce.Scripts.FSViews;
using FrancoisSauce.Scripts.GameScene.Lose;
using FrancoisSauce.Scripts.GameScene.Pause;
using FrancoisSauce.Scripts.GameScene.Playing;
using FrancoisSauce.Scripts.GameScene.WaitingToStart;
using FrancoisSauce.Scripts.GameScene.Win;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FrancoisSauce.Scripts.GameScene
{
    /// <summary>
    /// GameScene implementation of <see cref="IFSScene{T}"/>
    /// </summary>
    /// <inheritdoc/>
    public class Scene_Game : IFSScene<Scene_Game>
    {
        /// <summary>
        /// main <see cref="Camera"/> reference to activate it when needed
        /// </summary>
        [Tooltip("MainCamera of the gameScene")]
        [SerializeField] private Camera mainCamera = null;
        /// <summary>
        /// Image used for transition purpose
        /// </summary>
        [Tooltip("Image used for transition purpose")]
        [SerializeField] private Image transition = null;

        #region View variables

        /// <summary>
        /// UI for the waitingToStart <see cref="IFSView{T}"/>
        /// </summary>
        [Header("UIs of the views")]
        [Tooltip("UI for the View_WaitingToStart")]
        public GameObject waitingToStartUI;
        /// <summary>
        /// UI for the playing <see cref="IFSView{T}"/>
        /// </summary>
        [Tooltip("UI for the View_Playing")]
        public GameObject playingUI;
        /// <summary>
        /// UI for the pause <see cref="IFSView{T}"/>
        /// </summary>
        [Tooltip("UI for the View_Pause")]
        public GameObject pauseUI;
        /// <summary>
        /// UI for the win <see cref="IFSView{T}"/>
        /// </summary>
        [Tooltip("UI for the View_Win")]
        public GameObject winUI;
        /// <summary>
        /// UI for the lose <see cref="IFSView{T}"/>
        /// </summary>
        [Tooltip("UI for the View_Lose")]
        public GameObject loseUI;

        /// <summary>
        /// Finite state machine implementation, WaitingToStart
        /// </summary>
        public readonly View_WaitingToStart waitingToStart = new View_WaitingToStart();
        /// <summary>
        /// Finite state machine implementation, Playing
        /// </summary>
        public readonly View_Playing playing = new View_Playing();
        /// <summary>
        /// Finite state machine implementation, Pause
        /// </summary>
        public readonly View_Pause pause = new View_Pause();
        /// <summary>
        /// Finite state machine implementation, Win
        /// </summary>
        public readonly View_Win win = new View_Win();
        /// <summary>
        /// Finite state machine implementation, Lose
        /// </summary>
        public readonly View_Lose lose = new View_Lose();
        
        //TODO comment
        [SerializeField] private AudioSource gameSceneAudioSource = null;
        [SerializeField] private AudioSource winAudioSource = null;
        

        #endregion
        

        /// <summary>
        /// <see cref="MonoBehaviour"/>'s Awake method
        /// Initializing finite state machine
        /// </summary>
        private void Awake()
        {
            waitingToStart.Awake(this);
            playing.Awake(this);
            pause.Awake(this);
            win.Awake(this);
            lose.Awake(this);
            
            waitingToStartUI.SetActive(false);
            playingUI.SetActive(false);
            pauseUI.SetActive(false);
            winUI.SetActive(false);
            loseUI.SetActive(false);
            
            if (LevelManager.IsInitialized) LevelManager.Instance.ReloadActualLevel(false);
        }

        public override void OnSceneLoaded(int index)
        {
        }

        public override void OnSceneChanged(int index)
        {
            if (index != SceneList.Instance.scenes["MainMenu"]) return;
            
            GameManager.Instance.UnloadAsync(SceneList.Instance.scenes["GameScene"]);
            LevelManager.Instance.UnloadCurrentLevel();
        }

        public override void OnLoadProgression(int index, float progression)
        {
        }

        public override void OnSceneUnloaded(int index)
        {
            if (index != SceneList.Instance.scenes["MainMenu"]) return;

            mainCamera.gameObject.SetActive(true);

            ChangeView(waitingToStart);

            transition.DOFade(0, .5f)
                .SetEase(Ease.Linear)
                .From(1f);
        }

        public sealed override void OnUpdate()
        {
            currentView.OnUpdate(this);
        }

        public override void ChangeView(IFSView<Scene_Game> newView)
        {
            currentView?.OnViewExit(this);
            currentView = newView;
            currentView.OnViewEnter(this);
        }

        /// <summary>
        /// Called from views to change scene when needed.
        /// </summary>
        public void OnWin()
        {
            gameSceneAudioSource.Stop();
            winAudioSource.Play();
            ChangeView(win);
        }

        /// <summary>
        /// Called from views to change scene when needed.
        /// </summary>
        public void OnLose()
        {
            gameSceneAudioSource.Stop();
            ChangeView(lose);
        }

        /// <summary>
        /// Called from views to change scene when needed.
        /// </summary>
        /// <param name="index">Index of the <see cref="Scene"/> to load.</param>
        public void LoadScene(int index)
        {
            StartCoroutine(GameManager.Instance.LoadSceneAsync(index, true, false));
        }

        /// <summary>
        /// <see cref="MonoBehaviour"/>'s OnDestroy method
        /// reset Time.timeScale to 1
        /// </summary>
        private void OnDestroy()
        {
            //TODO temp => remove all Time.timeScale in the FrancoisSauce too dangerous
            Time.timeScale = 1f;
        }

        /// <summary>
        /// event raised when start button is clicked
        /// </summary>
        [Tooltip("Event raised when start button is clicked")]
        [SerializeField] private FSVoidEventSO onClickedStartButton = null;
        /// <summary>
        /// event raised when pause button is clicked
        /// </summary>
        [Tooltip("Event raised when pause button is clicked")]
        [SerializeField] private FSVoidEventSO onPause = null;
        /// <summary>
        /// event raised when resume button is clicked
        /// </summary>
        [Tooltip("Even raised when resume button is clicked")]
        [SerializeField] private FSVoidEventSO onResume = null;
        
        /// <summary>
        /// called from views to call event just above
        /// </summary>
        public void OnClickedStartButton()
        {
            onClickedStartButton.Invoke();
            gameSceneAudioSource.Play();
        }

        /// <summary>
        /// called from views to call event just above
        /// </summary>
        public void OnPause()
        {
            onPause.Invoke();
        }

        /// <summary>
        /// called from views to call event just above
        /// </summary>
        public void OnResume()
        {
            onResume.Invoke();
        }
    }
}