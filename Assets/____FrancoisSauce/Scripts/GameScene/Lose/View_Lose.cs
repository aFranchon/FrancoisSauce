using FrancoisSauce.Scripts.FSSingleton;
using FrancoisSauce.Scripts.FSUtils;
using FrancoisSauce.Scripts.FSViews;
using UnityEngine;

namespace FrancoisSauce.Scripts.GameScene.Lose
{
    /// <summary>
    /// <see cref="Scene_Game"/> implementation of <see cref="IFSView{T}"/> of the Lose view
    /// </summary>
    /// <inheritdoc/>
    public class View_Lose : IFSView<Scene_Game>
    {
        /// <summary>
        /// Method to initialize this view
        /// </summary>
        /// <param name="scene">The <see cref="Scene_Game"/> that own this view</param>
        public void Awake(Scene_Game scene)
        {
            var viewUI = scene.loseUI.GetComponentInChildren<LoseScript>();

            viewUI.replayButton.onClick.AddListener(() =>
            {
                LevelManager.Instance.ReloadActualLevel();
                scene.ChangeView(scene.waitingToStart);
            });
            viewUI.quitButton.onClick.AddListener(() => {scene.LoadScene(SceneList.Instance.scenes["MainMenu"]);});
        }

        public override void OnViewEnter(Scene_Game scene)
        {
            scene.loseUI.SetActive(true);
            //Time.timeScale = 0f;
        }

        public override void OnViewExit(Scene_Game scene)
        {
            scene.loseUI.SetActive(false);
            Time.timeScale = 1f;
        }

        public override void OnUpdate(Scene_Game scene)
        {
        }
    }
}