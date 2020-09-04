using DG.Tweening;
using FrancoisSauce.Scripts.FSSingleton;
using FrancoisSauce.Scripts.FSScenes;
using FrancoisSauce.Scripts.FSUtils;
using FrancoisSauce.Scripts.FSViews;
using FrancoisSauce.Scripts.MainMenu.BaseMenu;
using FrancoisSauce.Scripts.MainMenu.LoadingMenu;
using FrancoisSauce.Scripts.MainMenu.OptionMenu;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FrancoisSauce.Scripts.MainMenu
{
    /// <summary>
    /// MainMenu implementation of <see cref="IFSScene{T}"/>
    /// </summary>
    /// <inheritdoc/>
    public class Scene_MainMenu : IFSScene<Scene_MainMenu>
    {
        /// <summary>
        /// UI for the BaseMenu <see cref="IFSView{T}"/>
        /// </summary>
        [Header("UIs of the views")]
        [Tooltip("UI for View_BaseMenu")]
        public GameObject baseMenuUI;
        /// <summary>
        /// UI for the OptionMenu <see cref="IFSView{T}"/>
        /// </summary>
        [Tooltip("UI for the View_OptionMenu")]
        public GameObject optionMenuUI;
        /// <summary>
        /// UI for the LoadingMenu <see cref="IFSView{T}"/>
        /// </summary>
        [Tooltip("UI for the View_LoadingMenu")]
        public GameObject loadingMenuUI;
    
        /// <summary>
        /// To keep track of current progression for the progress bar when loading on the loading menu
        /// </summary>
        [HideInInspector]
        public float currentProgression = 0f;
        
        /// <summary>
        /// Finite state machine implementation, BaseMenu
        /// </summary>
        public readonly View_BaseMenu baseMenu = new View_BaseMenu();
        /// <summary>
        /// Finite state machine implementation, OptionMenu
        /// </summary>
        public readonly View_OptionMenu optionMenu = new View_OptionMenu();
        /// <summary>
        /// Finite state machine implementation, LoadingMenu
        /// </summary>
        public readonly View_LoadingMenu loadingMenu = new View_LoadingMenu();
    
        /// <summary>
        /// Image for transitioning out of the scene
        /// </summary>
        [Space] public Image transition;
        
        //TODO comment, don't remember what is it
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField] private AudioSource mainMenuMusic = null;
        /// <summary>
        /// The audioMixer use to change 
        /// </summary>
        [Tooltip("")]
        public AudioMixer mainAudioMixer = null;
    
        /// <summary>
        /// <see cref="MonoBehaviour"/>'s Awake method
        /// Initializing finite state machine
        /// </summary>
        private void Awake()
        {
            baseMenu.Awake(this);
            optionMenu.Awake(this);
            loadingMenu.Awake(this);
    
            baseMenuUI.SetActive(false);
            optionMenuUI.SetActive(false);
            loadingMenuUI.SetActive(false);
            
            mainMenuMusic.Play();
            
            ChangeView(baseMenu);
        }
        
        public override void OnSceneLoaded(int index)
        {
            if (index != SceneList.Instance.scenes["GameScene"]) return;
            
            mainMenuMusic.Stop();
            StartCoroutine(GameManager.Instance.EndLoadingActivation(SceneList.Instance.scenes["GameScene"], true));
        }
    
        public override void OnSceneChanged(int index)
        {
        }
    
        public override void OnLoadProgression(int index, float progression)
        {
            if (index != SceneList.Instance.scenes["GameScene"]) return;
            currentProgression = progression;
        }
    
        public override void OnSceneUnloaded(int index)
        {
        }
    
        public sealed override void OnUpdate()
        {
            currentView.OnUpdate(this);
        }
        
        public override void ChangeView(IFSView<Scene_MainMenu> newView)
        {
            currentView?.OnViewExit(this);
            currentView = newView;
            currentView.OnViewEnter(this);
        }
    
        /// <summary>
        /// Called from views to change scene when needed.
        /// </summary>
        /// <param name="index">Index of the <see cref="Scene"/> to load.</param>
        public void LoadScene(int index)
        {
            StartCoroutine(GameManager.Instance.LoadSceneAsync(index));
        }
    
        /// <summary>
        /// Called from views to unload scene when needed
        /// </summary>
        /// <param name="index">Index of the <see cref="Scene"/> to unload.</param>
        public void UnloadScene(int index)
        {
            transition.DOFade(1, 1f)
                .From(0f)
                .SetEase(Ease.Linear)
                .onComplete += () => GameManager.Instance.UnloadAsync(index);
        }
    }
}

