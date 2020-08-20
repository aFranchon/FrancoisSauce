using FrancoisSauce.Scripts.FSUI.ProgressBar;
using FrancoisSauce.Scripts.FSUtils;
using FrancoisSauce.Scripts.FSViews;
using UnityEngine.UI;

namespace FrancoisSauce.Scripts.MainMenu.LoadingMenu
{
    /// <summary>
    /// <see cref="Scene_MainMenu"/> implementation of <see cref="IFSView{T}"/> of the LoadingMenu
    /// </summary>
    /// <inheritdoc/>
    public class View_LoadingMenu : IFSView<Scene_MainMenu>
    {
        private ProgressBar progressBar;
        private Button startGameButton;

        /// <summary>
        /// Method to initialize this view
        /// </summary>
        /// <param name="scene">The <see cref="Scene_MainMenu"/> that own this view</param>
        public void Awake(Scene_MainMenu scene)
        {
            progressBar = scene.loadingMenuUI.GetComponentInChildren<ProgressBar>();

            startGameButton = scene.loadingMenuUI.GetComponent<LoadingMenuScript>().startGameButton;
            startGameButton.onClick.AddListener(() =>
            {
                scene.UnloadScene(SceneList.Instance.scenes["MainMenu"]);
                startGameButton.enabled = false;
            });
            startGameButton.gameObject.SetActive(false);
        }

        public override void OnViewEnter(Scene_MainMenu scene)
        {
            scene.loadingMenuUI.SetActive(true);
        }

        public override void OnViewExit(Scene_MainMenu scene)
        {
            scene.loadingMenuUI.SetActive(false);
        }

        public override void OnUpdate(Scene_MainMenu scene)
        {
            progressBar.UpdateProgression(scene.currentProgression);

            if (!startGameButton.gameObject.activeInHierarchy && scene.currentProgression > .999f)
                startGameButton.gameObject.SetActive(true);
        }
    }
}