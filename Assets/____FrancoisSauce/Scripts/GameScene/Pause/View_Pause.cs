using FrancoisSauce.Scripts.FSUtils;
using FrancoisSauce.Scripts.FSViews;
using UnityEngine;

namespace FrancoisSauce.Scripts.GameScene.Pause
{
    /// <summary>
    /// <see cref="Scene_Game"/> implementation of <see cref="IFSView{T}"/> of the Pause view
    /// </summary>
    /// <inheritdoc/>
    public class View_Pause : IFSView<Scene_Game>
    {
        /// <summary>
        /// Method to initialize this view
        /// </summary>
        /// <param name="scene">The <see cref="Scene_Game"/> that own this view</param>
        public void Awake(Scene_Game scene)
        {
            var viewUI = scene.pauseUI.GetComponentInChildren<PauseScript>();

            viewUI.resumeButton.onClick.AddListener(() => scene.ChangeView(scene.playing));
            viewUI.quitButton.onClick.AddListener(() => {scene.LoadScene(SceneList.Instance.scenes["MainMenu"]);});
        }

        public override void OnViewEnter(Scene_Game scene)
        {
            scene.OnPause();
            scene.pauseUI.SetActive(true);
            Time.timeScale = 0f;
        }

        public override void OnViewExit(Scene_Game scene)
        {
            scene.OnResume();
            scene.pauseUI.SetActive(false);
            Time.timeScale = 1f;
        }

        public override void OnUpdate(Scene_Game scene)
        {
        }
    }
}