using FrancoisSauce.Scripts.FSViews;

namespace FrancoisSauce.Scripts.GameScene.WaitingToStart
{
    /// <summary>
    /// <see cref="Scene_Game"/> implementation of <see cref="IFSView{T}"/> of the WaitingToStart view
    /// </summary>
    /// <inheritdoc/>
    public class View_WaitingToStart : IFSView<Scene_Game>
    {
        /// <summary>
        /// Method to initialize this view
        /// </summary>
        /// <param name="scene">The <see cref="Scene_Game"/> that own this view</param>
        public void Awake(Scene_Game scene)
        {
            var viewUI = scene.waitingToStartUI.GetComponentInChildren<WaitingToStartScript>();

            viewUI.startButton.onClick.AddListener(() =>
            {
                scene.OnClickedStartButton();
                scene.ChangeView(scene.playing);
            });
        }

        public override void OnViewEnter(Scene_Game scene)
        {
            scene.waitingToStartUI.SetActive(true);
        }

        public override void OnViewExit(Scene_Game scene)
        {
            scene.waitingToStartUI.SetActive(false);
        }

        public override void OnUpdate(Scene_Game scene)
        {
        }
    }
}