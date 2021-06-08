using FrancoisSauce.Scripts.FSViews;
using UnityEngine;

namespace FrancoisSauce.Scripts.GameScene.Playing
{
    /// <summary>
    /// <see cref="Scene_Game"/> implementation of <see cref="IFSView{T}"/> of the Playing view
    /// </summary>
    /// <inheritdoc/>
    public class View_Playing : IFSView<Scene_Game>
    {
        /// <summary>
        /// Method to initialize this view
        /// </summary>
        /// <param name="scene">The <see cref="Scene_Game"/> that own this view</param>
        public void Awake(Scene_Game scene)
        {
            var viewUI = scene.playingUI.GetComponentInChildren<PlayingScript>();
            
            viewUI.pauseButton.onClick.AddListener(() =>
            {
                scene.clickSound.Play();
                scene.ChangeView(scene.pause);
            });
        }

        public override void OnViewEnter(Scene_Game scene)
        {
            scene.playingUI.SetActive(true);
        }

        public override void OnViewExit(Scene_Game scene)
        {
            scene.playingUI.SetActive(false);
        }

        public override void OnUpdate(Scene_Game scene)
        {
        }
    }
}