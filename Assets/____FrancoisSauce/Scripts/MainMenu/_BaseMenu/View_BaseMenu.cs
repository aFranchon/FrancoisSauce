using FrancoisSauce.Scripts.FSUtils;
using FrancoisSauce.Scripts.FSViews;
using Application = UnityEngine.Application;

namespace FrancoisSauce.Scripts.MainMenu.BaseMenu
{
    /// <summary>
    /// <see cref="Scene_MainMenu"/> implementation of <see cref="IFSView{T}"/> of the BaseMenu
    /// </summary>
    /// <inheritdoc/>
    public class View_BaseMenu : IFSView<Scene_MainMenu>
    {
        /// <summary>
        /// Method to initialize this view
        /// </summary>
        /// <param name="scene">The <see cref="Scene_MainMenu"/> that own this view</param>
        public void Awake(Scene_MainMenu scene)
        {
            var viewUI = scene.baseMenuUI.GetComponentInChildren<BaseMenuScript>();

            viewUI.optionMenuButton.onClick.AddListener(() => scene.ChangeView(scene.optionMenu));
            viewUI.playButton.onClick.AddListener(() =>
            {
                scene.ChangeView(scene.loadingMenu);
                scene.LoadScene(SceneList.Instance.scenes["GameScene"]);
            });
            viewUI.quitButton.onClick.AddListener(Application.Quit);
        }

        public override void OnViewEnter(Scene_MainMenu scene)
        {
            scene.baseMenuUI.SetActive(true);
        }

        public override void OnViewExit(Scene_MainMenu scene)
        {
            scene.baseMenuUI.SetActive(false);
        }

        public override void OnUpdate(Scene_MainMenu scene)
        {
            ;
        }
    }
}