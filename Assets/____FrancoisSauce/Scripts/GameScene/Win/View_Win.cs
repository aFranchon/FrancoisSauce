using FrancoisSauce.Scripts.FSSingleton;
using FrancoisSauce.Scripts.FSUtils;
using FrancoisSauce.Scripts.FSViews;

namespace FrancoisSauce.Scripts.GameScene.Win
{
    /// <summary>
    /// <see cref="Scene_Game"/> implementation of <see cref="IFSView{T}"/> of the Win view
    /// </summary>
    /// <inheritdoc/>
    public class View_Win : IFSView<Scene_Game>
    {
        /// <summary>
        /// Method to initialize this view
        /// </summary>
        /// <param name="scene">The <see cref="Scene_Game"/> that own this view</param>
        public void Awake(Scene_Game scene)
        {
            var viewUI = scene.winUI.GetComponentInChildren<WinScript>();

            viewUI.nextButton.onClick.AddListener(() =>
            {
                scene.clickSound.Play();
                LevelManager.Instance.LoadNextLevel();
                scene.ChangeView(scene.waitingToStart);
            });
            viewUI.quitButton.onClick.AddListener(() =>
            {
                scene.clickSound.Play();
                scene.LoadScene(SceneList.Instance.scenes["MainMenu"]);
            });
        }

        public override void OnViewEnter(Scene_Game scene)
        {
            scene.winUI.SetActive(true);
        }

        public override void OnViewExit(Scene_Game scene)
        {
            scene.winUI.SetActive(false);
        }

        public override void OnUpdate(Scene_Game scene)
        {
        }
    }
}